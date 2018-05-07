using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TLD
{
    public partial class openProjectForm : Form
    {
        public openProjectForm()
        {
            InitializeComponent();
        }

        private void openProjectForm_Load(object sender, EventArgs e)
        {
            txtProjectName.Text = TLD.Main.projectName;
            txtDescription.Text = TLD.Main.projectDescription;
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}