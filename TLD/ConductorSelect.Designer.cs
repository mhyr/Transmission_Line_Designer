namespace TLD
{
    partial class ConductorForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConductorForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.conductorsDataSet = new TLD.ConductorsDataSet();
            this.conductorsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.conductorsTableAdapter = new TLD.ConductorsDataSetTableAdapters.ConductorsTableAdapter();
            this.conductorsBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.conductorsBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSelect = new System.Windows.Forms.ToolStripButton();
            this.conductorsDataGridView = new System.Windows.Forms.DataGridView();
            this.ColumnConductorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDiameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnConductorAreaTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUTS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnElasticityFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAlpha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.conductorsDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.conductorsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.conductorsBindingNavigator)).BeginInit();
            this.conductorsBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conductorsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // conductorsDataSet
            // 
            this.conductorsDataSet.DataSetName = "ConductorsDataSet";
            this.conductorsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // conductorsBindingSource
            // 
            this.conductorsBindingSource.DataMember = "Conductors";
            this.conductorsBindingSource.DataSource = this.conductorsDataSet;
            // 
            // conductorsTableAdapter
            // 
            this.conductorsTableAdapter.ClearBeforeFill = true;
            // 
            // conductorsBindingNavigator
            // 
            this.conductorsBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.conductorsBindingNavigator.BindingSource = this.conductorsBindingSource;
            this.conductorsBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.conductorsBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.conductorsBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.conductorsBindingNavigatorSaveItem,
            this.toolStripButtonSelect});
            this.conductorsBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.conductorsBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.conductorsBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.conductorsBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.conductorsBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.conductorsBindingNavigator.Name = "conductorsBindingNavigator";
            this.conductorsBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.conductorsBindingNavigator.Size = new System.Drawing.Size(543, 25);
            this.conductorsBindingNavigator.TabIndex = 0;
            this.conductorsBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // conductorsBindingNavigatorSaveItem
            // 
            this.conductorsBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.conductorsBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("conductorsBindingNavigatorSaveItem.Image")));
            this.conductorsBindingNavigatorSaveItem.Name = "conductorsBindingNavigatorSaveItem";
            this.conductorsBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.conductorsBindingNavigatorSaveItem.Text = "Save Data";
            this.conductorsBindingNavigatorSaveItem.Click += new System.EventHandler(this.conductorsBindingNavigatorSaveItem_Click);
            // 
            // toolStripButtonSelect
            // 
            this.toolStripButtonSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSelect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSelect.Image")));
            this.toolStripButtonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelect.Name = "toolStripButtonSelect";
            this.toolStripButtonSelect.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSelect.Text = "Select";
            this.toolStripButtonSelect.Click += new System.EventHandler(this.toolStripButtonSelect_Click);
            // 
            // conductorsDataGridView
            // 
            this.conductorsDataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.conductorsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.conductorsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.conductorsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnConductorName,
            this.ColumnDiameter,
            this.dataGridViewTextBoxColumn3,
            this.ColumnConductorAreaTotal,
            this.ColumnWeight,
            this.ColumnUTS,
            this.dataGridViewTextBoxColumn7,
            this.ColumnElasticityFinal,
            this.ColumnAlpha,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12});
            this.conductorsDataGridView.DataSource = this.conductorsBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.conductorsDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.conductorsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conductorsDataGridView.Location = new System.Drawing.Point(0, 25);
            this.conductorsDataGridView.Name = "conductorsDataGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.conductorsDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.conductorsDataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.conductorsDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.conductorsDataGridView.Size = new System.Drawing.Size(543, 334);
            this.conductorsDataGridView.TabIndex = 1;
            // 
            // ColumnConductorName
            // 
            this.ColumnConductorName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnConductorName.DataPropertyName = "Conductor Name";
            this.ColumnConductorName.HeaderText = "Conductor Name";
            this.ColumnConductorName.Name = "ColumnConductorName";
            this.ColumnConductorName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnConductorName.Width = 93;
            // 
            // ColumnDiameter
            // 
            this.ColumnDiameter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnDiameter.DataPropertyName = "Overall Diameter(mm)";
            this.ColumnDiameter.HeaderText = "Overall Diameter(mm)";
            this.ColumnDiameter.Name = "ColumnDiameter";
            this.ColumnDiameter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnDiameter.Width = 113;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Area(mm2) AL";
            this.dataGridViewTextBoxColumn3.HeaderText = "Area(mm2) AL";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 79;
            // 
            // ColumnConductorAreaTotal
            // 
            this.ColumnConductorAreaTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnConductorAreaTotal.DataPropertyName = "Area(mm2) Total";
            this.ColumnConductorAreaTotal.HeaderText = "Area(mm2) Total";
            this.ColumnConductorAreaTotal.Name = "ColumnConductorAreaTotal";
            this.ColumnConductorAreaTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnConductorAreaTotal.Width = 90;
            // 
            // ColumnWeight
            // 
            this.ColumnWeight.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnWeight.DataPropertyName = "Unit Weight(kg/m)";
            this.ColumnWeight.HeaderText = "Unit Weight(kg/m)";
            this.ColumnWeight.Name = "ColumnWeight";
            this.ColumnWeight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnUTS
            // 
            this.ColumnUTS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnUTS.DataPropertyName = "UTS(kg)";
            this.ColumnUTS.HeaderText = "UTS(kg)";
            this.ColumnUTS.Name = "ColumnUTS";
            this.ColumnUTS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnUTS.Width = 53;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "E(Initial)(kg/mm2)";
            this.dataGridViewTextBoxColumn7.HeaderText = "E(Initial)(kg/mm2)";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn7.Width = 95;
            // 
            // ColumnElasticityFinal
            // 
            this.ColumnElasticityFinal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnElasticityFinal.DataPropertyName = "E(Final)(kg/mm2)";
            this.ColumnElasticityFinal.HeaderText = "E(Final)(kg/mm2)";
            this.ColumnElasticityFinal.Name = "ColumnElasticityFinal";
            this.ColumnElasticityFinal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnElasticityFinal.Width = 93;
            // 
            // ColumnAlpha
            // 
            this.ColumnAlpha.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnAlpha.DataPropertyName = "α(1/ °C)";
            this.ColumnAlpha.HeaderText = "α(1/ °C)";
            this.ColumnAlpha.Name = "ColumnAlpha";
            this.ColumnAlpha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnAlpha.Width = 51;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Equivalent Creep Temperature(°C)";
            this.dataGridViewTextBoxColumn10.HeaderText = "Equivalent Creep Temperature(°C)";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn10.Width = 174;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn11.DataPropertyName = "DC Resistance at 20°C(Ω/km)";
            this.dataGridViewTextBoxColumn11.HeaderText = "DC Resistance at 20°C(Ω/km)";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn11.Width = 156;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn12.DataPropertyName = "GMR(cm)";
            this.dataGridViewTextBoxColumn12.HeaderText = "GMR(cm)";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn12.Width = 58;
            // 
            // ConductorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 359);
            this.Controls.Add(this.conductorsDataGridView);
            this.Controls.Add(this.conductorsBindingNavigator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConductorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ConductorSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.conductorsDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.conductorsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.conductorsBindingNavigator)).EndInit();
            this.conductorsBindingNavigator.ResumeLayout(false);
            this.conductorsBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conductorsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ConductorsDataSet conductorsDataSet;
        private System.Windows.Forms.BindingSource conductorsBindingSource;
        private TLD.ConductorsDataSetTableAdapters.ConductorsTableAdapter conductorsTableAdapter;
        private System.Windows.Forms.BindingNavigator conductorsBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton conductorsBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView conductorsDataGridView;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnConductorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDiameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnConductorAreaTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnElasticityFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAlpha;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
    }
}