using static SoundVisualizer.Transformation;
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
        SampleAggregator sampleAggregator;

        Config config = new Config();

        Bitmap bitmap;

        Thread SerialWriteTh;
        SerialPort _serialPort;

        byte[] serial_buffer;

        int picH;
        int picW;

        private float maxDb = 0;
        private float minDb = -90f;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;

            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDeviceCollection devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            sel_device.Items.AddRange(devices.ToArray());

            bool configOk = config.ReadConfig();

            sel_fft.SelectedItem = config.fftLen.ToString();
            num_cols.Value = config.cols;
            num_rows.Value = config.rows;
            sel_serial.SelectedIndex = config.com_port;
            sel_device.SelectedIndex = config.deviceIdx;
            serial_buffer = new byte[config.cols];

            if(configOk)
                _serialPort = new SerialPort(sel_serial.SelectedItem.ToString(), 19200, Parity.None, 8, StopBits.One);

            device = devices[config.deviceIdx];

            picH = pictureBox1.Height;
            picW = pictureBox1.Width;
            bitmap = new Bitmap(picW, picH);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            int cols = Convert.ToInt32(num_cols.Value);
            int rows = Convert.ToInt32(num_rows.Value);
            int com_port = sel_serial.SelectedIndex;
            int deviceIdx = sel_device.SelectedIndex;
            config.WriteConfig(config.fftLen, cols, rows, com_port, deviceIdx);

            if (waveIn != null)
                waveIn.StopRecording();

            if (SerialWriteTh != null)
                if (SerialWriteTh.IsAlive)
                    SerialWriteTh.Abort();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            this.ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
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
            int halfPlot = e.Result.Length / 2;
            for (int i = 0; i < halfPlot; i++)
            {
                double mX = e.Result[i].X;
                double mY = e.Result[i].Y;

                double magnitude = Math.Sqrt(mX * mX + mY * mY);
                float dbs = 20 * (float)Math.Log(magnitude, 10);

                float wM = map(i, 0, halfPlot, 0, picW);

                gr.FillRectangle(SystemBrushes.Control, wM - 1, 0, 2, picH);
                if (dbs > -90f)
                {
                    if (dbs < maxDb) maxDb = dbs;
                    if (dbs > minDb) minDb = dbs;

                    serial_buffer[(int)map(i, 0, halfPlot, 0, config.cols)] = (byte)map(dbs, maxDb, minDb, 0, config.rows);

                    var y = map(dbs, minDb, maxDb, 0, picH);
                    var height = picH - y;
                    gr.FillRectangle(Brushes.Black, wM - 1, y, 2, height);
                }
            }
            pictureBox1.Image = bitmap;
        }

        private void Sel_fft_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.fftLen = Convert.ToInt32(sel_fft.SelectedItem);
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

            enable_serial.Checked = false;
            _serialPort = new SerialPort(sel_serial.SelectedItem.ToString(), 19200, Parity.None, 8, StopBits.One);
        }

        private void Btn_start_Click(object sender, EventArgs e)
        {
            config.rows = (int)num_rows.Value;
            config.cols = (int)num_cols.Value;
            serial_buffer = new byte[config.cols];

            sampleAggregator = new SampleAggregator(config.fftLen);
            sampleAggregator.FftCalculated += new EventHandler<FftEventArgs>(FftCalculated);
            sampleAggregator.PerformFFT = true;

            if(_serialPort == null)
                _serialPort = new SerialPort(sel_serial.SelectedItem.ToString(), 19200, Parity.None, 8, StopBits.One);

            if (waveIn != null)
                waveIn.StopRecording();

            if (device != null)
                waveIn = new WasapiLoopbackCapture(device);

            waveIn.DataAvailable += OnDataAvailable;
            waveIn.StartRecording();
        }

        private void SerialWriteThread()
        {
            while (_serialPort.IsOpen && serial_buffer != null)
            {
                try
                {
                    _serialPort.Write(serial_buffer, 0, serial_buffer.Length);
                } catch
                {
                    Console.WriteLine("Невозможно записать данные в COM-порт");
                }
            }
        }

        private void Enable_serial_CheckedChanged(object sender, EventArgs e)
        {
            if(enable_serial.Checked)
            {
                try
                {
                    _serialPort.Open();
                    SerialWriteTh = new Thread(SerialWriteThread);
                    SerialWriteTh.Name = "SerialThread";
                    SerialWriteTh.Start();
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть COM-порт,\nвозможно устройство не подключено");
                    enable_serial.Checked = false;
                }
            }
            else
            {
                if(_serialPort.IsOpen)
                    _serialPort.Close();

                if (SerialWriteTh != null)
                    if (SerialWriteTh.IsAlive)
                        SerialWriteTh.Abort();
            }
        }
    }
}
