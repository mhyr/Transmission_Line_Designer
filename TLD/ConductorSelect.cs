using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TLD
{
    public partial class ConductorForm : Form
    {
        public ConductorForm()
        {
            InitializeComponent();
        }

        private void conductorsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.conductorsBindingSource.EndEdit();
            this.conductorsTableAdapter.Update(this.conductorsDataSet.Conductors);

        }

        private void ConductorSelect_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'conductorsDataSet.Conductors' table. You can move, or remove it, as needed.
            this.conductorsTableAdapter.Fill(this.conductorsDataSet.Conductors);

        }

        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            string gooz;
            gooz = conductorsDataGridView.Rows[conductorsDataGridView.CurrentCellAddress.Y].Cells["ColumnConductorName"].Value.ToString();
            TLD.Main.conductorDiameter = Convert.ToDouble(conductorsDataGridView.Rows[conductorsDataGridView.CurrentCellAddress.Y].Cells["ColumnDiameter"].Value) * .001;
            TLD.Main.conductorArea = Convert.ToDouble(conductorsDataGridView.Rows[conductorsDataGridView.CurrentCellAddress.Y].Cells["ColumnConductorAreaTotal"].Value);
            TLD.Main.Elasticity = Convert.ToDouble(conductorsDataGridView.Rows[conductorsDataGridView.CurrentCellAddress.Y].Cells["ColumnElasticityFinal"].Value);
            TLD.Main.Alpha = Convert.ToDouble(conductorsDataGridView.Rows[conductorsDataGridView.CurrentCellAddress.Y].Cells["ColumnAlpha"].Value);
            TLD.Main.UTS = Convert.ToDouble(conductorsDataGridView.Rows[conductorsDataGridView.CurrentCellAddress.Y].Cells["ColumnUTS"].Value);
            TLD.Main.normalWeight = Convert.ToDouble(conductorsDataGridView.Rows[conductorsDataGridView.CurrentCellAddress.Y].Cells["ColumnWeight"].Value);
            this.Close();
        }
    }
}