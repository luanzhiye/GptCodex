using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WheelSpeed.Properties;

namespace WheelSpeed
{
    public partial class MainForm : Form
    {
        private double _angle = 0;
        private int _spokes = 6;
        private double _rpm = 0;
        private bool _colorMarked = false;
        private double _frequent = 25;

        public bool ColorMarked
        {
            get { return _colorMarked; }
            set { _colorMarked = value; }
        }

        public double Rpm
        {
            get { return _rpm; }
            set { _rpm = value; }
        }

        public double Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        public int Spokes
        {
            get { return _spokes; }
            set { _spokes = value; }
        }

        public double Frequent
        {
            get { return _frequent; }
            set { _frequent = value; }
        }

        public MainForm()
        {
            InitializeComponent();
            comboBoxFreq.SelectedIndex = 9;
            comboBoxDeltaRpm.SelectedIndex = 9;
            numericUpDownRpm.Value = 0;
            numericUpDownSpokes.Value = 6;
        }

        private void pictureBoxRGB_Paint(object sender, PaintEventArgs e)
        {
            int width = pictureBoxRGB.Width;
            int height = pictureBoxRGB.Height;
            int centerX = width/2;
            int centerY = height/2;
            Pen penBlack = new Pen(Color.Black, 24);
            Pen penPurple = new Pen(Color.Purple, 6);
            Brush brushPurple = Brushes.Purple;
            int maxR;
            int littleR;
            if (width > height)
            {
                maxR = height*8/20;
            }
            else
            {
                maxR = width*7/20;
            }
            littleR = maxR/4;
            Graphics g = e.Graphics;
            //g.DrawEllipse(penBlack, centerX - maxR, centerY - maxR, 2 * maxR, 2 * maxR);
            double ang = Angle;
            PointsCalc pc = new PointsCalc(centerX, centerY, maxR);
            for (int i = 0; i < Spokes; i++)
            {
                Brush b = Brushes.Black;
                var ps = pc.CalcPentagon(ang*Math.PI/180, 20d*Math.PI/180, 30);
                //g.DrawPolygon(Pens.BlueViolet, ps);
                if (ColorMarked && i == 0)
                {
                    b = Brushes.Blue;
                }
                //if (ColorMarked && i == 1)
                //{
                //    b = Brushes.Blue;
                //}

                g.FillPolygon(b, ps);
                ang += 360d/Spokes;
            }
            g.DrawEllipse(penBlack, centerX - maxR, centerY - maxR, 2*maxR, 2*maxR);
            g.FillEllipse(brushPurple, centerX - littleR, centerY - littleR, 2*littleR, 2*littleR);
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            timerRun.Enabled = !timerRun.Enabled;
            splitContainer1.Panel2.Refresh();
            if (timerRun.Enabled)
            {
                toolStripButtonRunStop.ToolTipText = "点击停转车轮";
                runStopToolStripMenuItem.ToolTipText = "点击停转车轮";
                toolStripStatusMessage.Text = "车轮转动中";
                toolStripButtonRunStop.Image = Resources.PauseWheel;
            }
            else
            {
                toolStripButtonRunStop.ToolTipText = "点击转动车轮";
                runStopToolStripMenuItem.ToolTipText = "点击转动车轮";
                toolStripStatusMessage.Text = "车轮停转中";
                toolStripButtonRunStop.Image = Resources.RunWheel;
            }
        }

        private void timerRun_Tick(object sender, EventArgs e)
        {
            Angle += Rpm*timerRun.Interval/1000d*360/60d;
            splitContainer1.Panel2.Refresh();
        }

        private void numericUpDownRpm_ValueChanged(object sender, EventArgs e)
        {
            //Math.PI/3*
            Rpm = (double) numericUpDownRpm.Value;
            splitContainer1.Panel2.Refresh();
        }

        private void checkBoxMarked_CheckedChanged(object sender, EventArgs e)
        {
            ColorMarked = checkBoxMarked.Checked;
            splitContainer1.Panel2.Refresh();
        }

        private void numericUpDownSpokes_ValueChanged(object sender, EventArgs e)
        {
            Spokes = (int) numericUpDownSpokes.Value;
            splitContainer1.Panel2.Refresh();
        }

        private void comboBoxFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            Frequent = Convert.ToDouble(comboBoxFreq.SelectedItem);
            timerRun.Interval = (int) (1000/Frequent);
            splitContainer1.Panel2.Refresh();
        }

        private void comboBoxDeltaRpm_SelectedIndexChanged(object sender, EventArgs e)
        {
            int delta = Convert.ToInt32(comboBoxDeltaRpm.SelectedItem);
            numericUpDownRpm.WheelIncrement = delta;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxForm about = new AboutBoxForm();
            about.ShowDialog(this);
        }
    }
}
