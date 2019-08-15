using System;
using System.IO;

namespace SoundVisualizer
{
    class Config
    {
        private string path = "startup.cfg";

        public int fftLen = 256;
        public int cols = 16;
        public int rows = 8;
        public int com_port = 0;
        public int deviceIdx = 0;

        public bool ReadConfig()
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    fftLen = Convert.ToInt32(sr.ReadLine());
                    cols = Convert.ToInt32(sr.ReadLine());
                    rows = Convert.ToInt32(sr.ReadLine());
                    com_port = Convert.ToInt32(sr.ReadLine());
                    deviceIdx = Convert.ToInt32(sr.ReadLine());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                new FileInfo(path).Create();
                return false;
            }
            return true;
        }

        public bool WriteConfig(int fftlen, int cols, int rows, int com_port, int deviceIdx)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(fftlen.ToString());
                    sw.WriteLine(cols.ToString());
                    sw.WriteLine(rows.ToString());
                    sw.WriteLine(com_port.ToString());
                    sw.WriteLine(deviceIdx.ToString());
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message); return false;
            }
            return true;
        }
    }
}
