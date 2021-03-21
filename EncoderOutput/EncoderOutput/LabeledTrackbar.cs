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
    public partial class LabeledTrackbar : UserControl
    {
        public int Maximum { get => trackBar1.Maximum; set => trackBar1.Maximum = value; }
        public int Minimum { get => trackBar1.Minimum; set => trackBar1.Minimum = value; }
        public int Value { get => trackBar1.Value; set => trackBar1.Value = value; }
        public string Label { get => label1.Text; set => label1.Text = value; }

        public new event EventHandler Scroll;

        public LabeledTrackbar()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar1.Value.ToString();
            Scroll?.Invoke(sender, e);
        }

        private void LabeledTrackbar_Load(object sender, EventArgs e)
        {
            trackBar1_Scroll(null, null);
        }
    }
}
