
namespace EncoderOutput
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labeledTrackbar8 = new EncoderOutput.LabeledTrackbar();
            this.labeledTrackbar7 = new EncoderOutput.LabeledTrackbar();
            this.labeledTrackbar6 = new EncoderOutput.LabeledTrackbar();
            this.labeledTrackbar5 = new EncoderOutput.LabeledTrackbar();
            this.labeledTrackbar4 = new EncoderOutput.LabeledTrackbar();
            this.labeledTrackbar3 = new EncoderOutput.LabeledTrackbar();
            this.labeledTrackbar2 = new EncoderOutput.LabeledTrackbar();
            this.labeledTrackbar1 = new EncoderOutput.LabeledTrackbar();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labeledTrackbar8);
            this.panel1.Controls.Add(this.labeledTrackbar7);
            this.panel1.Controls.Add(this.labeledTrackbar6);
            this.panel1.Controls.Add(this.labeledTrackbar5);
            this.panel1.Controls.Add(this.labeledTrackbar4);
            this.panel1.Controls.Add(this.labeledTrackbar3);
            this.panel1.Controls.Add(this.labeledTrackbar2);
            this.panel1.Controls.Add(this.labeledTrackbar1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 345);
            this.panel1.TabIndex = 5;
            // 
            // labeledTrackbar8
            // 
            this.labeledTrackbar8.AutoSize = true;
            this.labeledTrackbar8.Label = "Channel delay (us)";
            this.labeledTrackbar8.Location = new System.Drawing.Point(9, 286);
            this.labeledTrackbar8.Maximum = 255;
            this.labeledTrackbar8.Minimum = 0;
            this.labeledTrackbar8.Name = "labeledTrackbar8";
            this.labeledTrackbar8.Size = new System.Drawing.Size(228, 31);
            this.labeledTrackbar8.TabIndex = 7;
            this.labeledTrackbar8.Value = 100;
            this.labeledTrackbar8.Scroll += new System.EventHandler(this.labeledTrackbar8_Scroll);
            // 
            // labeledTrackbar7
            // 
            this.labeledTrackbar7.AutoSize = true;
            this.labeledTrackbar7.Label = "Switch delay (us)";
            this.labeledTrackbar7.Location = new System.Drawing.Point(10, 248);
            this.labeledTrackbar7.Maximum = 255;
            this.labeledTrackbar7.Minimum = 0;
            this.labeledTrackbar7.Name = "labeledTrackbar7";
            this.labeledTrackbar7.Size = new System.Drawing.Size(223, 31);
            this.labeledTrackbar7.TabIndex = 6;
            this.labeledTrackbar7.Value = 0;
            this.labeledTrackbar7.Scroll += new System.EventHandler(this.labeledTrackbar7_Scroll);
            // 
            // labeledTrackbar6
            // 
            this.labeledTrackbar6.AutoSize = true;
            this.labeledTrackbar6.Label = "Per-period delay (us)";
            this.labeledTrackbar6.Location = new System.Drawing.Point(10, 210);
            this.labeledTrackbar6.Maximum = 255;
            this.labeledTrackbar6.Minimum = 0;
            this.labeledTrackbar6.Name = "labeledTrackbar6";
            this.labeledTrackbar6.Size = new System.Drawing.Size(237, 31);
            this.labeledTrackbar6.TabIndex = 5;
            this.labeledTrackbar6.Value = 0;
            this.labeledTrackbar6.Scroll += new System.EventHandler(this.labeledTrackbar6_Scroll);
            // 
            // labeledTrackbar5
            // 
            this.labeledTrackbar5.AutoSize = true;
            this.labeledTrackbar5.Label = "Switch head start";
            this.labeledTrackbar5.Location = new System.Drawing.Point(9, 169);
            this.labeledTrackbar5.Maximum = 10;
            this.labeledTrackbar5.Minimum = -10;
            this.labeledTrackbar5.Name = "labeledTrackbar5";
            this.labeledTrackbar5.Size = new System.Drawing.Size(224, 34);
            this.labeledTrackbar5.TabIndex = 4;
            this.labeledTrackbar5.Value = 0;
            this.labeledTrackbar5.Scroll += new System.EventHandler(this.labeledTrackbar5_Scroll);
            // 
            // labeledTrackbar4
            // 
            this.labeledTrackbar4.AutoSize = true;
            this.labeledTrackbar4.Label = "Vertical scale";
            this.labeledTrackbar4.Location = new System.Drawing.Point(9, 129);
            this.labeledTrackbar4.Maximum = 255;
            this.labeledTrackbar4.Minimum = 0;
            this.labeledTrackbar4.Name = "labeledTrackbar4";
            this.labeledTrackbar4.Size = new System.Drawing.Size(205, 34);
            this.labeledTrackbar4.TabIndex = 3;
            this.labeledTrackbar4.Value = 220;
            this.labeledTrackbar4.Scroll += new System.EventHandler(this.labeledTrackbar4_Scroll);
            // 
            // labeledTrackbar3
            // 
            this.labeledTrackbar3.AutoSize = true;
            this.labeledTrackbar3.Label = "Sample count";
            this.labeledTrackbar3.Location = new System.Drawing.Point(9, 89);
            this.labeledTrackbar3.Maximum = 16;
            this.labeledTrackbar3.Minimum = 1;
            this.labeledTrackbar3.Name = "labeledTrackbar3";
            this.labeledTrackbar3.Size = new System.Drawing.Size(224, 34);
            this.labeledTrackbar3.TabIndex = 2;
            this.labeledTrackbar3.Value = 2;
            this.labeledTrackbar3.Scroll += new System.EventHandler(this.labeledTrackbar3_Scroll);
            // 
            // labeledTrackbar2
            // 
            this.labeledTrackbar2.AutoSize = true;
            this.labeledTrackbar2.Label = "Period";
            this.labeledTrackbar2.Location = new System.Drawing.Point(9, 49);
            this.labeledTrackbar2.Maximum = 16;
            this.labeledTrackbar2.Minimum = 1;
            this.labeledTrackbar2.Name = "labeledTrackbar2";
            this.labeledTrackbar2.Size = new System.Drawing.Size(224, 34);
            this.labeledTrackbar2.TabIndex = 1;
            this.labeledTrackbar2.Value = 4;
            this.labeledTrackbar2.Scroll += new System.EventHandler(this.labeledTrackbar2_Scroll);
            // 
            // labeledTrackbar1
            // 
            this.labeledTrackbar1.AutoSize = true;
            this.labeledTrackbar1.Label = "Averaging factor";
            this.labeledTrackbar1.Location = new System.Drawing.Point(9, 9);
            this.labeledTrackbar1.Maximum = 10;
            this.labeledTrackbar1.Minimum = 1;
            this.labeledTrackbar1.Name = "labeledTrackbar1";
            this.labeledTrackbar1.Size = new System.Drawing.Size(224, 34);
            this.labeledTrackbar1.TabIndex = 0;
            this.labeledTrackbar1.Value = 1;
            this.labeledTrackbar1.Scroll += new System.EventHandler(this.labeledTrackbar1_Scroll);
            this.labeledTrackbar1.Load += new System.EventHandler(this.labeledTrackbar1_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private LabeledTrackbar labeledTrackbar1;
        private LabeledTrackbar labeledTrackbar5;
        private LabeledTrackbar labeledTrackbar4;
        private LabeledTrackbar labeledTrackbar3;
        private LabeledTrackbar labeledTrackbar2;
        private LabeledTrackbar labeledTrackbar7;
        private LabeledTrackbar labeledTrackbar6;
        private LabeledTrackbar labeledTrackbar8;
    }
}

