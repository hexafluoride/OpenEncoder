using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncoderOutput
{
    public partial class MainForm : Form
    {
        EncoderChannel[] channels;
        Label RotationIndicator = new Label();
        SerialPort port;
        bool closed;

        int bytes = 0;
        int updates = 0;

        private void WriteConfig(byte reg, byte val)
        {

port.Write(new byte[] { 0xcc, 0xcc, 0xcc, 0xcc, 0xcc, reg, val }, 0, 7); 
        }

        public MainForm()
        {
            Form.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            channels = new EncoderChannel[6];

            for (int i = 0; i < 6; i++)
            {
                channels[i] = new EncoderChannel();
                flowLayoutPanel1.Controls.Add(channels[i]);
            }

            RotationIndicator.Font = new Font("Sans Serif", 64);
            RotationIndicator.Size = new Size(300, 300);
            flowLayoutPanel1.Controls.Add(RotationIndicator);

            port = new SerialPort("COM7", 2000000);
            port.Open();

            //WriteConfig(0xb0, (byte)trackBar4.Value);
            //WriteConfig(0xb2, (byte)trackBar3.Value);
            //WriteConfig(0xb3, 0);
            //WriteConfig(0xb4, 0);
            //WriteConfig(0xb5, 0);

            new Thread(new ThreadStart(ReaderThread)).Start();
            new Thread(new ThreadStart(delegate
            {
                var sw = Stopwatch.StartNew();
                var last_updates = 0;
                var last_ms = 0d;
                var last_bytes = 0;

                while (true)
                {
                    Thread.Sleep(1000);
                    var diff = updates - last_updates;
                    double delta_t = sw.ElapsedMilliseconds - last_ms;
                    var delta_b = bytes - last_bytes;
                    last_updates = updates;
                    last_ms = sw.ElapsedMilliseconds;
                    last_bytes = bytes;
                    this.Text = $"{(diff / delta_t * 1000d):0.00} updates per second, {(delta_b / delta_t * 1000d / 1024d):0.00} kb/s";
                }
            })).Start();
        }

        Graphics RotationGraphics;

        public void ShowRotation()
        {
            var rotation = 0d;
            var total = Math.Pow(2, channels.Length);

            for (int i = 0; i < channels.Length; i++)
            {
                var k = (channels.Length - 1) - i;
                var f = Math.Pow(2, k);

                if (channels[i].State)
                {
                    rotation += f;
                }
            }
            var angle = GrayToBinary((uint)rotation);

            RotationIndicator.Text = $"{angle}\n{rotation}";

            //RotationGraphics.FillRectangle(Brushes.White, 0, 0, RotationIndicator.Width, RotationIndicator.Height);
            //RotationGraphics.DrawLine(Pens.Black, 50, 0, 50, 100);
            //RotationGraphics.DrawString($"{rotation}", this.Font, Brushes.Black, PointF.Empty);
        }

        uint GrayToBinary(uint num)
        {
            uint mask = num;
            while (mask > 0)
            {           // Each Gray code bit is exclusive-ored with all more significant bits.
                mask >>= 1;
                num ^= mask;
            }
            return num;
        }

        public void ReaderThread()
        {
            while (!closed && port.IsOpen)
            {
                int preamble_count = 0;
                int tries = 0;

                while (preamble_count < 4 && tries < 64)
                {
                    var b = port.ReadByte();

                    //if (checkBox1.Checked)
                    //{
                    //    if (b == 0xFE)
                    //        textBox1.Text += ".";
                    //    else
                    //        textBox1.Text += (b.ToString("X2"));
                    //}

                    if (b == 0xFE)
                        preamble_count++;
                    else
                        preamble_count = 0;

                    tries++;
                    bytes++;
                }

                if (preamble_count < 4)
                    continue;

                var channel_state = port.ReadByte();

                bytes++;
                if (channel_state != 0xCC && channel_state != 0xDD)
                {
                    continue;
                }

                var channel_id = port.ReadByte();

                bytes++;
                if (channel_id > 5)
                    continue;

                var line = port.ReadByte();

                bytes++;
                var count = port.ReadByte();

                bytes++;
                if (count < 0 || count > 1000)
                    continue;

                var length = count * 2;
                var block = new byte[length];
                var data = new ushort[count];

                while (port.BytesToRead < block.Length) ;

                var read = port.Read(block, 0, block.Length);
                bytes += read;

                if (read != block.Length)
                    continue;

                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = BitConverter.ToUInt16(block, i * 2);
                }

                //Console.WriteLine($"{data.Length} reaidngs from channel {channel_id}: {(channel_state)}");
                channels[channel_id].Draw(data, channel_state == 0xCC, line);
                ShowRotation();
                updates++;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            closed = true;
            port.Close();
        }

        public static int SampleCount = 1;

        //private void trackBar1_Scroll(object sender, EventArgs e)
        //{
        //    SampleCount = trackBar1.Value;
        //}

        public static int Ceiling = 0;

        private void labeledTrackbar1_Scroll(object sender, EventArgs e)
        {
            SampleCount = labeledTrackbar1.Value;
        }

        private void labeledTrackbar1_Load(object sender, EventArgs e)
        {

        }

        private void labeledTrackbar2_Scroll(object sender, EventArgs e)
        {

            // period
            WriteConfig(0xb0, (byte)labeledTrackbar2.Value);
        }

        private void labeledTrackbar3_Scroll(object sender, EventArgs e)
        {
            // sample count
            WriteConfig(0xb2, (byte)labeledTrackbar3.Value);
        }

        private void labeledTrackbar4_Scroll(object sender, EventArgs e)
        {
            Ceiling = labeledTrackbar4.Value;

        }

        private void labeledTrackbar5_Scroll(object sender, EventArgs e)
        {
            // switch time
            int switch_time = labeledTrackbar5.Value < 0 ? 127 + -(labeledTrackbar5.Value) : labeledTrackbar5.Value;
            WriteConfig(0xb3, (byte)switch_time);
        }

        private void labeledTrackbar6_Scroll(object sender, EventArgs e)
        {
            WriteConfig(0xb4, (byte)labeledTrackbar6.Value);
        }

        private void labeledTrackbar7_Scroll(object sender, EventArgs e)
        {
            WriteConfig(0xb5, (byte)labeledTrackbar7.Value);
        }

        private void labeledTrackbar8_Scroll(object sender, EventArgs e)
        {
            WriteConfig(0xb1, (byte)labeledTrackbar8.Value);
        }
        //private void trackBar2_Scroll(object sender, EventArgs e)
        //{
        //    Ceiling = trackBar2.Value;
        //}

        //private void trackBar4_Scroll(object sender, EventArgs e)
        //{
        //    WriteConfig(0xb0, (byte)trackBar4.Value);
        //}

        //private void trackBar3_Scroll(object sender, EventArgs e)
        //{

        //    WriteConfig(0xb2, (byte)trackBar3.Value);
        //}

        //private void trackBar5_Scroll(object sender, EventArgs e)
        //{
        //    WriteConfig(0xb3, (byte)trackBar5.Value);
        //}
    }
}
