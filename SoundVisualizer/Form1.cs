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

        IWaveIn waveIn;
        static int fftLength = 256;
        SampleAggregator sampleAggregator;

        char[] to_serial = new char[16];

        Bitmap bitmap;

        private float min = 0;
        private float max = -90f;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDeviceCollection devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            sel_device.Items.AddRange(devices.ToArray());

            sel_device.SelectedIndex = 1;
            device = devices[1];

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
            for (int i = 0; i < e.Result.Length/2; i ++)
            {
                double mX = e.Result[i].X;
                //double mX = (e.Result[i - 1].X + e.Result[i].X + e.Result[i + 1].X) / 3;
                double mY = e.Result[i].Y;
                //double mY = (e.Result[i - 1].Y + e.Result[i].Y + e.Result[i + 1].Y) / 3;
                double magnitude = Math.Sqrt(mX * mX + mY * mY);
                float dbs = 20 * (float)Math.Log(magnitude, 10);

                gr.DrawLine(Pens.White, i * 10, 0, i * 10, pictureBox1.Height);
                if (dbs > -90f)
                {
                    if (dbs < min) min = dbs;
                    if (dbs > max) max = dbs;

                    if(i % 8 == 0)
                        to_serial[i/8] = (char)map(dbs, min, max, 0, 8);
                    gr.DrawLine(Pens.White, i * 10, 0, i * 10, pictureBox1.Height);
                    gr.DrawLine(Pens.Black, i * 10, pictureBox1.Height, i * 10, pictureBox1.Height - dbs - 90);
                    pictureBox1.Image = bitmap;
                }
            }
            txt_min.Text = min.ToString();
            txt_max.Text = max.ToString();
            serial_port.Write(to_serial, 0, to_serial.Length);
        }

        private void Sel_device_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sel_device.SelectedItem != null)
                device = (MMDevice)sel_device.SelectedItem;
        }

        private void Btn_start_Click(object sender, EventArgs e)
        {
            sampleAggregator = new SampleAggregator(fftLength);
            sampleAggregator.FftCalculated += new EventHandler<FftEventArgs>(FftCalculated);
            sampleAggregator.PerformFFT = true;
            if(waveIn != null)
            {
                waveIn.StopRecording();
            }
            if (device != null)
            {
                waveIn = new WasapiLoopbackCapture(device);
            }

            waveIn.DataAvailable += OnDataAvailable;

            waveIn.StartRecording();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(waveIn != null)
                waveIn.StopRecording();
        }

        public static float lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static float map(float x, float x0, float x1, float a, float b)
        {
            float t = (x - x0) / (x1 - x0);
            return lerp(a, b, t);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach(char c in to_serial)
            {
                Console.Write(Convert.ToInt32(c) + " ");
            }
            Console.WriteLine();
        }
    }
}
