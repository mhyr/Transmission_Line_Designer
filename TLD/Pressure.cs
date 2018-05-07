using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TLD
{
    public partial class Pressure : Form
    {
        int convertFrom;

        public Pressure()
        {
            InitializeComponent();
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            double temp;
            if (convertFrom == 1)
            {
                temp = Convert.ToDouble(txtPressure.Text);
                temp = temp * 101325 / 76.0;
                temp = (5 - Math.Log10(temp)) * 15500;
                txtAltitude.Text = temp.ToString();       
            }
            else if (convertFrom == 2)
            {
                temp = Convert.ToDouble(txtAltitude.Text);
                temp = Math.Pow(10, 5 - temp / 15500);
                temp = temp * 76.0 / 101325;
                txtPressure.Text = temp.ToString();
            }
        }

        private void txtPressure_TextChanged(object sender, EventArgs e)
        {
            convertFrom = 1;
        }       

        private void txtAltitude_TextChanged(object sender, EventArgs e)
        {
            convertFrom = 2;
        }
    }
}