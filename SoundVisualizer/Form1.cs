using System;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Threading;
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

        Thread SerialWriteTh;
        Bitmap bitmap;
        SerialPort _serialPort;

        char[] to_serial = new char[16];

        private float maxDb = 0;
        private float minDb = -90f;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDeviceCollection devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            sel_device.Items.AddRange(devices.ToArray());

            sel_serial.SelectedIndex = 8;
            _serialPort = new SerialPort(sel_serial.SelectedItem.ToString(), 19200, Parity.None, 8, StopBits.One);

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
            for (int i = 1; i < e.Result.Length / 2 - 1; i += 2)
            {
                double mX = e.Result[i].X;
                //double mX = (e.Result[i - 1].X + e.Result[i].X + e.Result[i + 1].X) / 3;
                //double mX = (e.Result[i - 1].X * 1 / 4) + (e.Result[i].X * 1 / 2) + (e.Result[i + 1].X * 1 / 4);
                double mY = e.Result[i].Y;
                //double mY = (e.Result[i - 1].Y + e.Result[i].Y + e.Result[i + 1].Y) / 3;
                //double mY = (e.Result[i - 1].Y * 1 / 4) + (e.Result[i].Y * 1 / 2) + (e.Result[i + 1].Y * 1 / 4);



                double magnitude = Math.Sqrt(mX * mX + mY * mY);
                float dbs = 20 * (float)Math.Log(magnitude, 10);

                float wM = Transformation.map(i, 0, 64, 0, 256);

                if (dbs > -90f)
                {
                    if (dbs < maxDb) maxDb = dbs;
                    if (dbs > minDb) minDb = dbs;

                    if ((i - 1) % 8 == 0)
                        to_serial[i / 8] = (char)Transformation.map(dbs, minDb, maxDb, 0, 8);

                    //gr.DrawLine(Pens.White, wM, 0,                  wM, pictureBox1.Height);
                    //gr.DrawLine(Pens.Black, wM, pictureBox1.Height, wM, pictureBox1.Height - dbs - 90);

                    gr.FillRectangle(Brushes.Black, wM - 2, 0, 4, pictureBox1.Height);
                    gr.FillRectangle(Brushes.White, wM - 2, 0, 4, Transformation.map(dbs, minDb, maxDb, 0, 144));
                }
            }
            pictureBox1.Image = bitmap;

            txt_min.Text = maxDb.ToString();
            txt_max.Text = minDb.ToString();
        }

        private void Sel_device_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sel_device.SelectedItem != null)
                device = (MMDevice)sel_device.SelectedItem;
        }

        private void Sel_serial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_serialPort != null)
                if (_serialPort.IsOpen)
                    _serialPort.Close();

            _serialPort = new SerialPort(sel_serial.SelectedItem.ToString(), 19200, Parity.None, 8, StopBits.One);
        }

        private void Btn_start_Click(object sender, EventArgs e)
        {
            sampleAggregator = new SampleAggregator(fftLength);
            sampleAggregator.FftCalculated += new EventHandler<FftEventArgs>(FftCalculated);
            sampleAggregator.PerformFFT = true;

            if (waveIn != null)
                waveIn.StopRecording();

            if (device != null)
                waveIn = new WasapiLoopbackCapture(device);

            waveIn.DataAvailable += OnDataAvailable;
            waveIn.StartRecording();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (waveIn != null)
                waveIn.StopRecording();

            if (SerialWriteTh != null)
                if (SerialWriteTh.IsAlive)
                    SerialWriteTh.Abort();
        }

        private void SerialWriteThread()
        {
            while (_serialPort.IsOpen && to_serial != null)
            {
                for (int i = 0; i < to_serial.Length; i++)
                    Console.Write(Convert.ToInt32(to_serial[i]));
                Console.WriteLine();
                _serialPort.Write(to_serial, 0, to_serial.Length);
            }
        }

        private void Enable_serial_CheckedChanged(object sender, EventArgs e)
        {
            if(enable_serial.Checked)
            {
                try
                {
                    _serialPort.Open();
                }
                catch
                {
                    MessageBox.Show("Can't open serial port, may be device not connected");
                    enable_serial.Checked = false;
                }
                SerialWriteTh = new Thread(SerialWriteThread);
                SerialWriteTh.Start();
            }
            else
                _serialPort.Close();
        }
    }
}
