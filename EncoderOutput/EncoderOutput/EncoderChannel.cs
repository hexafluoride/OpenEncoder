using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncoderOutput
{
    public partial class EncoderChannel : UserControl
    {
        public bool State { get; set; }

        private List<double> last_end_to_ends = new List<double>();

        public EncoderChannel()
        {
            InitializeComponent();
        }

        public void Draw(ushort[] v, bool state, int line_pos)
        {
            if (!v.Any())
                return;

            var values = v.Select(q => (int)q).ToArray();
            float x_scale = 4;

            try
            {
                var img = new Bitmap((int)(values.Length * x_scale) + 5, 125);
                var graphics = Graphics.FromImage(img);
                var scale = (img.Height / (256d - MainForm.Ceiling));

                graphics.DrawRectangle(Pens.Black, 0, 0, img.Width - 1, img.Height - 1);

                var calc_state = false;
                var total_variance = values.Max() - values.Min();

                int after_region_start = line_pos + (values.Length - line_pos) / 3;

               
                int after_region_end = v.Length - 1;

                int before_region_start = line_pos - (line_pos / 6);
                int before_region_end = line_pos;

                if (before_region_end - before_region_start > 2)
                    before_region_end--;


                before_region_start = Math.Min(7, before_region_start);

                var after = values.Skip(after_region_start).Take(after_region_end - after_region_start);
                var before = values.Skip(before_region_start).Take(before_region_end - before_region_start);

                double after_sum = after.Sum();
                double before_sum = before.Min() * before.Count();

                var ratio = (double)(after.Count()) / (double)(before.Count());

                before_sum *= ratio;

                double end_to_end = (after_sum - before_sum);

                last_end_to_ends.Add(end_to_end);

                while (last_end_to_ends.Count > MainForm.SampleCount)
                {

                    last_end_to_ends.RemoveAt(0);
                }

                end_to_end = (int)last_end_to_ends.Average();

                graphics.DrawString($"{end_to_end}", this.Font, Brushes.Black, 5, 5);

                if (end_to_end >= 10)
                {
                    calc_state = true;
                }

                State = calc_state;

                var pen = calc_state ? Pens.Red : Pens.Black;

                float last_x = 0;

                for (int x = 0; x < values.Length - 1; x++)
                {
                    int cur_val = img.Height - (int)(values[x] * scale);
                    int next_val = img.Height - (int)(values[x + 1] * scale);

                    float next_x = last_x + x_scale;
                    graphics.DrawLine(pen, last_x, cur_val, next_x, next_val);
                    last_x = next_x;
                }

                Action<Color, int> horiz = (c, y) => { graphics.DrawLine(new Pen(c), y * x_scale, 0, y * x_scale, img.Height); };
                horiz(Color.Orange, line_pos);
                horiz(Color.Aqua, before_region_start);
                horiz(Color.Aquamarine, before_region_end);
                horiz(Color.GreenYellow, after_region_start);
                horiz(Color.Green, after_region_end);

                pictureBox1.Image = img;

                if (pictureBox1.Width < img.Width)
                    pictureBox1.Width = img.Width;

                if (this.Width < img.Width)
                    this.Width = img.Width + 5;
            }
            catch { }
        }
    }
}
