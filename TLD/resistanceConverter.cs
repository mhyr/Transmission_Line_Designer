using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TLD
{
    public partial class resistanceConverter : Form
    {
        public resistanceConverter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double rdc, r2, temp1, temp2, freq, x, k, rac;
            double[] XtoK = new double[40] { 1, 1, 1.00001, 1.00004, 1.00013, 1.00032, 1.00067, 1.00124, 1.00212, 1.00340, 
                1.00519, 1.00758, 1.01071, 1.01470, 1.01969, 1.02582, 1.03323, 1.04205, 1.05240, 1.06440, 
                1.07816, 1.09375, 1.11126, 1.13069, 1.15207, 1.17538, 1.20056, 1.22753, 1.25620, 1.28644, 
                1.31809, 1.35102, 1.38504, 1.41999, 1.45570, 1.49202, 1.52879, 1.56587, 1.60314, 1.64051};

            rdc = Convert.ToDouble(textBox1.Text);
            temp1 = Convert.ToDouble(textBox2.Text);
            temp2 = Convert.ToDouble(textBox3.Text);
            freq = Convert.ToDouble(textBox4.Text);

            r2 = rdc * (228.1 + temp2) / (228.1 + temp1) * 1.609;
            x = 0.063598 * Math.Sqrt(freq / r2);
            x = Math.Round(x, 1) * 10;
            k = XtoK[(int)x];
            rac = k * r2 / 1.609;
            textBox5.Text = Convert.ToString(rac);
        }
    }
}