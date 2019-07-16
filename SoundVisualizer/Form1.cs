using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace SoundVisualizer
{
    public partial class Form1 : Form
    {
        MMDevice device;
        private IWaveIn waveIn;
        private static int fftLength = 256;
        private SampleAggregator sampleAggregator = new SampleAggregator(fftLength);
        private float volume = 1.0f;
        Bitmap bitmap;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDeviceCollection devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            sel_device.Items.AddRange(devices.ToArray());
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<WaveInEventArgs>(OnDataAvailable), sender, e);
            }
            else
            {
                byte[] buffer = e.Buffer;
                int bytesRecorded = e.BytesRecorded;
                int bufferIncrement = waveIn.WaveFormat.BlockAlign;
                for (int index = 0; index < bytesRecorded; index += bufferIncrement)
                {
                    float sample32 = BitConverter.ToSingle(buffer, index);

                    sampleAggregator.Add(sample32);
                }
            }
        }

        private void FftCalculated(object sender, FftEventArgs e)
        {
            Graphics gr = Graphics.FromImage(bitmap);
            for (int i = 1; i < e.Result.Length/2 - 1; i++)
            {
                double mX = (e.Result[i - 1].X + e.Result[i].X + e.Result[i + 1].X) / 3;
                double mY = (e.Result[i - 1].Y + e.Result[i].Y + e.Result[i + 1].Y) / 3;
                double magnitude = Math.Sqrt(mX * mX + mY * mY);
                float res = 20 * (float)Math.Log(magnitude, 10);
                if (res > -90f)
                {
                    gr.DrawLine(Pens.White, i * 10, 0, i * 10, pictureBox1.Height);
                    gr.DrawLine(Pens.Black, i * 10, pictureBox1.Height, i * 10, pictureBox1.Height - res - 90);
                    pictureBox1.Image = bitmap;
                }
            }
            
        }

        private void Sel_device_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sel_device.SelectedItem != null)
                device = (MMDevice)sel_device.SelectedItem;
        }

        private void Btn_start_Click(object sender, EventArgs e)
        {
            sampleAggregator.FftCalculated -= new EventHandler<FftEventArgs>(FftCalculated);
            sampleAggregator.FftCalculated += new EventHandler<FftEventArgs>(FftCalculated);
            sampleAggregator.PerformFFT = true;

            if (device != null)
            {
                waveIn = new WasapiLoopbackCapture(device);
                volume = device.AudioEndpointVolume.MasterVolumeLevelScalar;
            }

            waveIn.DataAvailable -= OnDataAvailable;
            waveIn.DataAvailable += OnDataAvailable;

            waveIn.StartRecording();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            waveIn.StopRecording();
        }

        //public static float lerp(float a, float b, float t)
        //{
        //    return a + (b - a) * t;
        //}

        //public static float map(float x, float x0, float x1, float a, float b)
        //{
        //    float t = (x - x0) / (x1 - x0);
        //    return lerp(a, b, t);
        //}
    }
}
