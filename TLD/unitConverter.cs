using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TLD
{
    public partial class unitConverter : Form
    {
        public unitConverter()
        {
            InitializeComponent();
        }

        double UnitConvert = 0;
        int ActivBox = 0;

        private void txtinch_Enter(object sender, EventArgs e)
        {
            ActivBox = 1;
        }

        private void txtfoot_Enter(object sender, EventArgs e)
        {
            ActivBox = 2;
        }

        private void txtyard_Enter(object sender, EventArgs e)
        {
            ActivBox = 3;
        }

        private void txtmile_Enter(object sender, EventArgs e)
        {
            ActivBox = 4;
        }

        private void txtmmeter_Enter(object sender, EventArgs e)
        {
            ActivBox = 5;
        }

        private void txtcmeter_Enter(object sender, EventArgs e)
        {
            ActivBox = 6;
        }

        private void txtmeter_Enter(object sender, EventArgs e)
        {
            ActivBox = 7;
        }

        private void txtkmeter_Enter(object sender, EventArgs e)
        {
            ActivBox = 8;
        }
        private void txtfahr_Enter(object sender, EventArgs e)
        {
            ActivBox = 9;
        }
        private void txtcels_Enter(object sender, EventArgs e)
        {
            ActivBox = 10;
        }

        private void txtinch_TextChanged(object sender, EventArgs e)
        {
            if (ActivBox == 1)
            {

                try
                {
                    UnitConvert = System.Convert.ToDouble(txtinch.Text);
                }
                catch (System.FormatException)
                {
                    txtinch.Text = "0";
                    return;
                }
                txtfoot.Text = System.Convert.ToString(Math.Round(UnitConvert / 12.0, 12));
                txtyard.Text = System.Convert.ToString(Math.Round(UnitConvert / 36.0, 12));
                txtmile.Text = System.Convert.ToString(Math.Round(UnitConvert / 63360.0, 12));
                txtmmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * 25.4, 12));
                txtcmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * 2.54, 12));
                txtmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * .0254, 12));
                txtkmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * .0000254, 12));
            }
        }

        private void txtfoot_TextChanged(object sender, EventArgs e)
        {
            if (ActivBox == 2)
            {

                try
                {
                    UnitConvert = System.Convert.ToDouble(txtfoot.Text);
                }
                catch (System.FormatException)
                {
                    txtfoot.Text = "0";
                    return;
                }
                txtinch.Text = System.Convert.ToString(Math.Round(UnitConvert * 12.0, 12));
                txtyard.Text = System.Convert.ToString(Math.Round(UnitConvert / 3.0, 12));
                txtmile.Text = System.Convert.ToString(Math.Round(UnitConvert / 5280.0, 12));
                txtmmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * 304.8, 12));
                txtcmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * 30.48, 12));
                txtmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * .3048, 12));
                txtkmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * .0003048, 12));
            }
        }

        private void txtyard_TextChanged(object sender, EventArgs e)
        {
            if (ActivBox == 3)
            {

                try
                {
                    UnitConvert = System.Convert.ToDouble(txtyard.Text);
                }
                catch (System.FormatException)
                {
                    txtyard.Text = "0";
                    return;
                }
                txtinch.Text = System.Convert.ToString(Math.Round(UnitConvert * 36.0, 12));
                txtfoot.Text = System.Convert.ToString(Math.Round(UnitConvert * 3.0, 12));
                txtmile.Text = System.Convert.ToString(Math.Round(UnitConvert / 1760.0, 12));
                txtmmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * 914.4, 12));
                txtcmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * 91.44, 12));
                txtmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * .9144, 12));
                txtkmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * .0009144, 12));
            }
        }

        private void txtmile_TextChanged(object sender, EventArgs e)
        {
            if (ActivBox == 4)
            {

                try
                {
                    UnitConvert = System.Convert.ToDouble(txtmile.Text);
                }
                catch (System.FormatException)
                {
                    txtmile.Text = "0";
                    return;
                }
                txtinch.Text = System.Convert.ToString(Math.Round(UnitConvert * 63360.0, 12));
                txtfoot.Text = System.Convert.ToString(Math.Round(UnitConvert * 5280.0, 12));
                txtyard.Text = System.Convert.ToString(Math.Round(UnitConvert * 1760.0, 12));
                txtmmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * 1609344, 12));
                txtcmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * 160934.4, 12));
                txtmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * 1609.344, 12));
                txtkmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * 1.609344, 12));
            }
        }

        private void txtmmeter_TextChanged(object sender, EventArgs e)
        {
            if (ActivBox == 5)
            {

                try
                {
                    UnitConvert = System.Convert.ToDouble(txtmmeter.Text);
                }
                catch (System.FormatException)
                {
                    txtmmeter.Text = "0";
                    return;
                }
                txtinch.Text = System.Convert.ToString(Math.Round(UnitConvert / 25.4, 12));
                txtfoot.Text = System.Convert.ToString(Math.Round(UnitConvert / 25.4 / 12.0, 12));
                txtyard.Text = System.Convert.ToString(Math.Round(UnitConvert / 25.4 / 36.0, 12));
                txtmile.Text = System.Convert.ToString(Math.Round(UnitConvert / 25.4 / 63360.0, 12));
                txtcmeter.Text = System.Convert.ToString(Math.Round(UnitConvert / 10, 12));
                txtmeter.Text = System.Convert.ToString(Math.Round(UnitConvert / 1000, 12));
                txtkmeter.Text = System.Convert.ToString(Math.Round(UnitConvert / 1000000, 12));
            }
        }

        private void txtcmeter_TextChanged(object sender, EventArgs e)
        {
            if (ActivBox == 6)
            {

                try
                {
                    UnitConvert = System.Convert.ToDouble(txtcmeter.Text);
                }
                catch (System.FormatException)
                {
                    txtcmeter.Text = "0";
                    return;
                }
                txtinch.Text = System.Convert.ToString(Math.Round(UnitConvert / 2.54, 12));
                txtfoot.Text = System.Convert.ToString(Math.Round(UnitConvert / 2.54 / 12.0, 12));
                txtyard.Text = System.Convert.ToString(Math.Round(UnitConvert / 2.54 / 36.0, 12));
                txtmile.Text = System.Convert.ToString(Math.Round(UnitConvert / 2.54 / 63360.0, 12));
                txtmmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * 10, 12));
                txtmeter.Text = System.Convert.ToString(Math.Round(UnitConvert / 100, 12));
                txtkmeter.Text = System.Convert.ToString(Math.Round(UnitConvert / 100000, 12));
            }
        }

        private void txtmeter_TextChanged(object sender, EventArgs e)
        {
            if (ActivBox == 7)
            {

                try
                {
                    UnitConvert = System.Convert.ToDouble(txtmeter.Text);
                }
                catch (System.FormatException)
                {
                    txtmeter.Text = "0";
                    return;
                }
                txtinch.Text = System.Convert.ToString(Math.Round(UnitConvert / .0254, 12));
                txtfoot.Text = System.Convert.ToString(Math.Round(UnitConvert / .0254 / 12.0, 12));
                txtyard.Text = System.Convert.ToString(Math.Round(UnitConvert / .0254 / 36.0, 12));
                txtmile.Text = System.Convert.ToString(Math.Round(UnitConvert / .0254 / 63360.0, 12));
                txtmmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * 1000, 12));
                txtcmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * 100, 12));
                txtkmeter.Text = System.Convert.ToString(Math.Round(UnitConvert / 1000, 12));
            }
        }

        private void txtkmeter_TextChanged(object sender, EventArgs e)
        {
            if (ActivBox == 8)
            {

                try
                {
                    UnitConvert = System.Convert.ToDouble(txtkmeter.Text);
                }
                catch (System.FormatException)
                {
                    txtkmeter.Text = "0";
                    return;
                }
                txtinch.Text = System.Convert.ToString(Math.Round(UnitConvert / .0000254, 12));
                txtfoot.Text = System.Convert.ToString(Math.Round(UnitConvert / .0000254 / 12.0, 12));
                txtyard.Text = System.Convert.ToString(Math.Round(UnitConvert / .0000254 / 36.0, 12));
                txtmile.Text = System.Convert.ToString(Math.Round(UnitConvert / .0000254 / 63360.0, 12));
                txtmmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * 1000000, 12));
                txtcmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * 100000, 12));
                txtmeter.Text = System.Convert.ToString(Math.Round(UnitConvert * 1000, 12));
            }
        }

        private void txtfahr_TextChanged(object sender, EventArgs e)
        {
            if (ActivBox == 9)
            {

                try
                {
                    UnitConvert = System.Convert.ToDouble(txtfahr.Text);
                }
                catch (System.FormatException)
                {
                    txtfahr.Text = "0";
                    return;
                }
                txtcels.Text = System.Convert.ToString(Math.Round((UnitConvert - 32) * 100.0 / 180.0, 12));
            }
        }

        private void txtcels_TextChanged(object sender, EventArgs e)
        {
            if (ActivBox == 10)
            {

                try
                {
                    UnitConvert = System.Convert.ToDouble(txtcels.Text);
                }
                catch (System.FormatException)
                {
                    txtcels.Text = "0";
                    return;
                }
                txtfahr.Text = System.Convert.ToString(Math.Round((UnitConvert * 1.8 + 32), 12));
            }
        }

    }
}