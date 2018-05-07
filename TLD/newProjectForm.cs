using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TLD
{
    public partial class newProjectForm : Form
    {
        public newProjectForm()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (txtProjectName.Text == "")
                MessageBox.Show("Please Enter a Name for Your Project", "Project Name");
            else
            {
                TLD.Main.projectName = txtProjectName.Text;
                TLD.Main.projectDescription = txtDescription.Text;
                Close();
            }            
        }
    }
}