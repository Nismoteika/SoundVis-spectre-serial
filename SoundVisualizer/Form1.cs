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
        //Bitmap bitmap;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDeviceCollection devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            sel_device.Items.AddRange(devices.ToArray());
            //bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
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
            foreach(NAudio.Dsp.Complex C in e.Result)
            {
                Console.Write("X: " + C.X + "; Y: " + C.Y + " --- ");

            }
            Console.WriteLine();
        }

        private void Sel_device_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sel_device.SelectedItem != null)
                device = (MMDevice)sel_device.SelectedItem;
        }

        private void Btn_start_Click(object sender, EventArgs e)
        {
            sampleAggregator.FftCalculated += new EventHandler<FftEventArgs>(FftCalculated);
            sampleAggregator.PerformFFT = true;

            if (device != null)
                waveIn = new WasapiLoopbackCapture(device);

            waveIn.DataAvailable += OnDataAvailable;

            waveIn.StartRecording();
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

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
