using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZedGraph;

namespace TLD
{
    public partial class Main : Form
    {
        //Public Data
        const double epsilon = 0.000000000008841941282883074209382431298473;
        const double pi = Math.PI;      

        //Data from general Panel
        static public double[,] linePos=new double[12,2];
        static public double[,] shieldPos = new double[2, 2];
        static public double towerHeight, adjacentDistance, shieldRadius, lineLength;
        static public int circuitNumber, shieldNumber, bundlesNumber;

        public int circuitSelectChange, shieldwireSelectChange = 0, bundleSelectChange;

        //Data from ConductorSelect Form
        static public double conductorDiameter, Req, conductorRadius, GMD, mm0, conductorArea, Elasticity, Alpha, UTS, normalWeight;

        //Data for Electrical Calculation
        static public double lineVoltage;
        static public double[,] pMatrix;       
        static public double[] eMatrix;
        static public double[] RXYZ = new double[4];
        static public double[] coronaLoss = new double[4];

        static public double[,] lineParameter;
        static public double Frequency, earthResistivity, ACResistance, maxPower, powerStep, cosPhi, phase;
        static public string[,] zeroSequence;

        static public double[,] powerResult;

        //Data from Environment Panel
        static public int weatherCondition;
        static public double Temperature, Pressure, windSpeed, minFreq, maxFreq, Rain, Altitude;

        //Data for Mechanical Calculation
        static public double[,] mechanicalWeatherCondition = new double[5, 3];
        static public double SafetyFactor, Span;
        static public double[,] mechanicalResult = new double[4, 5];
        

        //Data for Noise Calculation
        static public double radioFrequency, TVFrequency, TVBandwidth;

        static public double[] RI;
        static public double[] TVI;
        static public double[] AN;
        static public string noiseResultView;

        //data for Insulation Calculation
        static public double IKL, insulatorlength, numberofShieldingFailure, towerFootingResistance, archingHornFactor, WaveFrontTime;
        static public double overVoltageFactor, switchingSurgePU, contamination, insulatorCreepage;
        static public double[] lightningInsulation = new double[2];
        static public double[] switchingInsulation = new double[2];
        static public double[] powerInsulation = new double[2];
        static public double[] contaminationInsulation = new double[2];

        //Variables for Project File
        public bool runningProject;
        public string filePath;
        static public string projectName,projectDescription;
        public int projectCondition;
        public bool projectOpenedCorrectly;       
                      
        public Main()
        {
            InitializeComponent();            
            cmbShieldNumber.SelectedIndex = 0;
            cmbbundlesNumber.SelectedIndex = 0;
            cmbFrequency.SelectedIndex = 0;
            cmbWeatherCondition.SelectedIndex = 0;
            cmbRegionCondition.SelectedIndex = 0;
            cmbTf.SelectedIndex = 0;
            cmbInsulatorType.SelectedIndex = 0;
            GraphPane resetGraph = schematicGraph.GraphPane;
            resetGraph.Title.IsVisible = false;
            resetGraph.XAxis.IsVisible = false;
            resetGraph.YAxis.IsVisible = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Transmission Line Designer\nVersion 1.0", "About TLD");
        }           
        
        private void toolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolbarToolStripMenuItem.Checked == false)
            {
                toolbarToolStripMenuItem.Checked = true;
                toolStrip.Visible = true;
            }
            else
            {
                toolbarToolStripMenuItem.Checked = false;
                toolStrip.Visible = false;
            }                
        }
                
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (runningProject == true)
            {
                DialogResult YesNoCancel = new DialogResult();
                YesNoCancel = MessageBox.Show("There's an Open Project in the Workspace.\nDo You Want to Save the Changes?", "TLD", MessageBoxButtons.YesNoCancel);
                if (YesNoCancel == DialogResult.Yes)
                {
                    saveProject();
                    newProject();                    
                }
                else if (YesNoCancel == DialogResult.No)
                    newProject();                
            }
            else
                newProject();               
        }

        private void newProject()
        {            
            newProjectForm makeProject = new newProjectForm();
            makeProject.ShowDialog();
            if (projectName != null)
            {
                Main.ActiveForm.Text = projectName + " - TLD";
                splitContainerMain.Visible = true;               
                treeViewMain.ExpandAll();
                richTextBoxDescription.Text = projectDescription;
                TreeNode mainSelect = treeViewMain.TopNode;
                treeViewMain.SelectedNode = mainSelect;             
                closeProjectToolStripMenuItem.Enabled = true;
                runningProject = true;
                projectCondition = 0;
                schematicGraph.GraphPane.CurveList.Clear();
                projectReset();
            }
        }       

        private void getGeneralData()
        {
            linePos[0, 0] = Convert.ToDouble(tx1.Text);
            linePos[0, 1] = Convert.ToDouble(ty1.Text);
            linePos[1, 0] = Convert.ToDouble(tx2.Text);
            linePos[1, 1] = Convert.ToDouble(ty2.Text);
            linePos[2, 0] = Convert.ToDouble(tx3.Text);
            linePos[2, 1] = Convert.ToDouble(ty3.Text);
            linePos[3, 0] = Convert.ToDouble(tx4.Text);
            linePos[3, 1] = Convert.ToDouble(ty4.Text);
            linePos[4, 0] = Convert.ToDouble(tx5.Text);
            linePos[4, 1] = Convert.ToDouble(ty5.Text);
            linePos[5, 0] = Convert.ToDouble(tx6.Text);
            linePos[5, 1] = Convert.ToDouble(ty6.Text);
            linePos[6, 0] = Convert.ToDouble(tx7.Text);
            linePos[6, 1] = Convert.ToDouble(ty7.Text);
            linePos[7, 0] = Convert.ToDouble(tx8.Text);
            linePos[7, 1] = Convert.ToDouble(ty8.Text);
            linePos[8, 0] = Convert.ToDouble(tx9.Text);
            linePos[8, 1] = Convert.ToDouble(ty9.Text);
            linePos[9, 0] = Convert.ToDouble(tx10.Text);
            linePos[9, 1] = Convert.ToDouble(ty10.Text);
            linePos[10, 0] = Convert.ToDouble(tx11.Text);
            linePos[10, 1] = Convert.ToDouble(ty11.Text);
            linePos[11, 0] = Convert.ToDouble(tx12.Text);
            linePos[11, 1] = Convert.ToDouble(ty12.Text);

            shieldPos[0, 0] = Convert.ToDouble(txs1.Text);
            shieldPos[0, 1] = Convert.ToDouble(tys1.Text);
            shieldPos[1, 0] = Convert.ToDouble(txs2.Text);
            shieldPos[1, 1] = Convert.ToDouble(tys2.Text);

            towerHeight = Convert.ToDouble(textBoxHeight.Text);
            circuitNumber = cmbCircuitNumber.SelectedIndex + 1;
            shieldNumber = cmbShieldNumber.SelectedIndex;
            bundlesNumber = cmbbundlesNumber.SelectedIndex + 1;
            adjacentDistance = Convert.ToDouble(txtAdjacent.Text);
            shieldRadius = Convert.ToDouble(txtRadiusofShieldwires.Text);
            conductorRadius = conductorDiameter / 2.0;
            lineLength = Convert.ToDouble(txtLineLength.Text);
            mm0 = Convert.ToDouble(txtmm0.Text);

            conductorEqualRadius();
            calculateGMD();          
            drawBundle();         
            toolStripButtonMechanical.Enabled = true;
            mechanicalToolStripMenuItem.Enabled = true;
        }
        
        private void drawLine()
        {
            linePos[0, 0] = Convert.ToDouble(tx1.Text);
            linePos[0, 1] = Convert.ToDouble(ty1.Text);
            linePos[1, 0] = Convert.ToDouble(tx2.Text);
            linePos[1, 1] = Convert.ToDouble(ty2.Text);
            linePos[2, 0] = Convert.ToDouble(tx3.Text);
            linePos[2, 1] = Convert.ToDouble(ty3.Text);
            linePos[3, 0] = Convert.ToDouble(tx4.Text);
            linePos[3, 1] = Convert.ToDouble(ty4.Text);
            linePos[4, 0] = Convert.ToDouble(tx5.Text);
            linePos[4, 1] = Convert.ToDouble(ty5.Text);
            linePos[5, 0] = Convert.ToDouble(tx6.Text);
            linePos[5, 1] = Convert.ToDouble(ty6.Text);
            linePos[6, 0] = Convert.ToDouble(tx7.Text);
            linePos[6, 1] = Convert.ToDouble(ty7.Text);
            linePos[7, 0] = Convert.ToDouble(tx8.Text);
            linePos[7, 1] = Convert.ToDouble(ty8.Text);
            linePos[8, 0] = Convert.ToDouble(tx9.Text);
            linePos[8, 1] = Convert.ToDouble(ty9.Text);
            linePos[9, 0] = Convert.ToDouble(tx10.Text);
            linePos[9, 1] = Convert.ToDouble(ty10.Text);
            linePos[10, 0] = Convert.ToDouble(tx11.Text);
            linePos[10, 1] = Convert.ToDouble(ty11.Text);
            linePos[11, 0] = Convert.ToDouble(tx12.Text);
            linePos[11, 1] = Convert.ToDouble(ty12.Text);

            shieldPos[0, 0] = Convert.ToDouble(txs1.Text);
            shieldPos[0, 1] = Convert.ToDouble(tys1.Text);
            shieldPos[1, 0] = Convert.ToDouble(txs2.Text);
            shieldPos[1, 1] = Convert.ToDouble(tys2.Text);

            towerHeight = Convert.ToDouble(textBoxHeight.Text);
            circuitNumber = cmbCircuitNumber.SelectedIndex + 1;
            shieldNumber = cmbShieldNumber.SelectedIndex; 

            GraphPane lineSchematic = schematicGraph.GraphPane;
            lineSchematic.CurveList.Clear();            
            
            lineSchematic.Title.IsVisible = false;
            lineSchematic.XAxis.IsVisible = false;
            lineSchematic.YAxis.IsVisible = false;

            double[] maxPointX = new double[1];
            double[] maxPointY = new double[1];
            maxPointX[0] = 0;
            maxPointY[0] = 0;
            for (int i = 0; i < 3 * circuitNumber; i++)
                if (Math.Abs(linePos[i, 0]) >Math.Abs(maxPointX[0]))
                {
                    maxPointX[0] = -linePos[i, 0];
                    maxPointY[0] = linePos[i, 1];
                }
            for(int j=0;j<shieldNumber;j++)
                if (Math.Abs(shieldPos[j, 0]) > Math.Abs(maxPointX[0]))
                {
                    maxPointX[0] = -shieldPos[j, 0];
                    maxPointY[0] = shieldPos[j, 1];
                }
            bool changeAxis=true;
            for (int i = 0; i < 3 * circuitNumber; i++)
                if (maxPointX[0] == linePos[i, 0] && maxPointY[0] == linePos[i, 1])
                {
                    changeAxis = false;
                    break;
                }
            for(int j=0;j<shieldNumber;j++)
                if (maxPointX[0] == shieldPos[j, 0] && maxPointY[0] == shieldPos[j, 1])
                {
                    changeAxis = false;
                    break;
                }
            if (changeAxis == true)
            {
                PointPairList extraPoint = new PointPairList(maxPointX, maxPointY);
                LineItem extraLine = lineSchematic.AddCurve("", extraPoint, Color.White);
            }

            int zeroCounter=0;
            for (int i = 0; i < 3 * circuitNumber; i++)
                if (linePos[i, 0] == 0)
                    zeroCounter = zeroCounter + 1;
            for (int j = 0; j < shieldNumber; j++)
                if (shieldPos[j, 0] == 0)
                    zeroCounter = zeroCounter + 1;
            if (zeroCounter == 3 * circuitNumber + shieldNumber)
            {
                double[] zeroX ={ -1, 1 };
                double[] zeroY ={ 1, 1 };
                PointPairList zeroPoint = new PointPairList(zeroX, zeroY);
                LineItem zeroLine = lineSchematic.AddCurve("", zeroPoint, Color.White);
            }
    
            double[] x = new double[3 * circuitNumber];
            double[] y = new double[3 * circuitNumber];

            for (int i = 0; i < 3 * circuitNumber; i++)
            {
                x[i] = linePos[i, 0];
                y[i] = linePos[i, 1];
            }            

            PointPairList linePoint = new PointPairList(x, y);
            LineItem lineDraw = lineSchematic.AddCurve("", linePoint, Color.Red, SymbolType.Circle);
            lineDraw.Symbol.Size = 14;
            lineDraw.Symbol.Fill = new Fill(Color.Red);
            lineDraw.Line.IsVisible = false;

            double[] xShield = new double[shieldNumber];
            double[] yShield = new double[shieldNumber];

            for (int i = 0; i < shieldNumber; i++)
            {
                xShield[i] = shieldPos[i, 0];
                yShield[i] = shieldPos[i, 1];
            }

            PointPairList shiledPoint = new PointPairList(xShield, yShield);
            LineItem shieldDraw = lineSchematic.AddCurve("", shiledPoint, Color.Blue, SymbolType.Circle);
            shieldDraw.Symbol.Size = 12;
            shieldDraw.Symbol.Fill = new Fill(Color.Blue);
            shieldDraw.Line.IsVisible = false;


            double[] xTower ={ 0, 0 };
            double[] yTower ={ 0, towerHeight };

            PointPairList towerPoint = new PointPairList(xTower, yTower);
            LineItem towerDraw = lineSchematic.AddCurve("", towerPoint, Color.Black);
            towerDraw.Line.Width = 3.0f;            

            schematicGraph.AxisChange();
            schematicGraph.Refresh();                        
        }

        private void drawBundle()
        {
            int x, y;
            x = panelBundleDraw.ClientRectangle.Width;
            y = panelBundleDraw.ClientRectangle.Height;
            Graphics bundleDraw = panelBundleDraw.CreateGraphics();
            Pen pen = new Pen(Color.Black, 2);
            Pen arrrowPen = new Pen(Color.Black, 4);
            arrrowPen.EndCap = LineCap.ArrowAnchor;
            Pen dashPen = new Pen(Color.Black, 2);
            dashPen.DashPattern = new float[] { 4.0F, 2.0F, 1.0F, 3.0F };
            Font font = new Font("Aerial", 12, FontStyle.Bold);            
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            bundleDraw.Clear(Color.White);
            if (bundlesNumber == 1)
            {
                bundleDraw.DrawEllipse(pen, (x / 2)-25, (y / 2)-25, 50, 50);
                bundleDraw.DrawLine(arrrowPen, new Point(x/2, y/2), new Point((x/2)+20,(y/2)-15));
                bundleDraw.DrawString("r",font,drawBrush,new PointF((x/2)-5,(y/2)-25));               
            }
            if (bundlesNumber == 2)
            {
                bundleDraw.DrawEllipse(pen, (x / 2) - 120, (y / 2) - 25, 50, 50);
                bundleDraw.DrawEllipse(pen, (x / 2) + 70, (y / 2) - 25, 50, 50);
                bundleDraw.DrawLine(dashPen, new Point((x / 2) - 95, (y / 2)), new Point((x / 2) + 95, (y / 2)));
                bundleDraw.DrawString("D", font, drawBrush, new PointF((x / 2) - 5, (y / 2) - 20));
                bundleDraw.DrawLine(arrrowPen, new Point((x / 2) - 95, (y / 2)), new Point((x / 2) - 75, (y / 2) - 15));
                bundleDraw.DrawString("r", font, drawBrush, new PointF((x / 2) - 100, (y / 2) - 25));
            }
            if (bundlesNumber == 3)
            {
                bundleDraw.DrawEllipse(pen, (x / 2)-120, (y / 2) - 100, 50, 50);
                bundleDraw.DrawEllipse(pen, (x/2)-25, (y / 2) + 50, 50, 50);
                bundleDraw.DrawEllipse(pen, (x/2) + 70, (y / 2) - 100, 50, 50);
                bundleDraw.DrawLine(dashPen, new Point((x / 2) - 95, (y / 2) - 75), new Point((x / 2) + 95, (y / 2) - 75));
                bundleDraw.DrawLine(dashPen, new Point((x / 2), (y / 2) + 75), new Point((x / 2) + 95, (y / 2) - 75));
                bundleDraw.DrawLine(dashPen, new Point((x / 2) - 95, (y / 2) - 75), new Point((x / 2), (y / 2) + 75));
                bundleDraw.DrawString("D", font, drawBrush, new PointF((x / 2) - 5, (y / 2) - 95));
                bundleDraw.DrawLine(arrrowPen, new Point((x / 2) - 95, (y / 2) - 75), new Point((x / 2) - 75, (y / 2) - 90));
                bundleDraw.DrawString("r", font, drawBrush, new PointF((x / 2) - 100, (y / 2) - 100));
            }
            if (bundlesNumber == 4)
            {
                bundleDraw.DrawEllipse(pen, (x / 2) - 120, (y / 2) - 100, 50, 50);
                bundleDraw.DrawEllipse(pen, (x / 2) + 70, (y / 2) - 100, 50, 50);
                bundleDraw.DrawEllipse(pen, (x / 2) - 120, (y / 2) + 50, 50, 50);
                bundleDraw.DrawEllipse(pen, (x / 2) + 70, (y / 2) + 50, 50, 50);
                bundleDraw.DrawLine(dashPen, new Point((x / 2) - 95, (y / 2) - 75), new Point((x / 2) + 95, (y / 2) - 75));
                bundleDraw.DrawLine(dashPen, new Point((x / 2) + 95, (y / 2) - 75), new Point((x / 2) + 95, (y / 2) + 75));
                bundleDraw.DrawLine(dashPen, new Point((x / 2) + 95, (y / 2) + 75), new Point((x / 2) - 95, (y / 2) + 75));
                bundleDraw.DrawLine(dashPen, new Point((x / 2) - 95, (y / 2) + 75), new Point((x / 2) - 95, (y / 2) - 75));
                bundleDraw.DrawString("D", font, drawBrush, new PointF((x / 2) - 5, (y / 2) - 75));
                bundleDraw.DrawLine(arrrowPen, new Point((x / 2) - 95, (y / 2) - 75), new Point((x / 2) - 75, (y / 2) - 90));
                bundleDraw.DrawString("r", font, drawBrush, new PointF((x / 2) - 100, (y / 2) - 100));
            }
            bundleDraw.Dispose();
        }

        private void conductorEqualRadius()
        {
            double n =Convert.ToDouble(bundlesNumber);
            double R,r;
            r=conductorDiameter/2.0;
            R = adjacentDistance * .005 / Math.Sin(pi / n);
            Req = Math.Pow(n * r * Math.Pow(R, n - 1), 1 / n);
        }

        private void calculateGMD()
        {
            double Dab=1, Dbc=1, Dac=1, root;
            root = 1 / (Math.Pow(circuitNumber, 2));

            for(int i=0;i<3*circuitNumber;i=i+3)
                for(int j=1;j<3*circuitNumber;j=j+3)
                    Dab=Dab*Math.Sqrt(Math.Pow(linePos[i,0]-linePos[j,0],2)+Math.Pow(linePos[i,1]-linePos[j,1],2));
            Dab = Math.Pow(Dab, root);

            for (int i = 1; i < 3 * circuitNumber; i = i + 3)
                for (int j = 2; j < 3 * circuitNumber; j = j + 3)
                    Dbc = Dbc * Math.Sqrt(Math.Pow(linePos[i, 0] - linePos[j, 0], 2) + Math.Pow(linePos[i, 1] - linePos[j, 1], 2));
            Dbc = Math.Pow(Dbc, root);

            for (int i = 0; i < 3 * circuitNumber; i = i + 3)
                for (int j = 2; j < 3 * circuitNumber; j = j + 3)
                    Dac = Dac * Math.Sqrt(Math.Pow(linePos[i, 0] - linePos[j, 0], 2) + Math.Pow(linePos[i, 1] - linePos[j, 1], 2));
            Dac = Math.Pow(Dac, root);

            GMD = Math.Pow(Dab * Dbc * Dac, 1 / 3.0);           
        }      

        private void tx1_Leave(object sender, EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tx1.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                tx1.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tx1.Text = "0";
                return;
            }
        }
        private void ty1_Leave(object sender, EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(ty1.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                ty1.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                ty1.Text = "0";
                return;
            }
        }
        private void tx2_Leave(object sender, EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tx2.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                tx2.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tx2.Text = "0";
                return;
            }
        }
        private void ty2_Leave(object sender, EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(ty2.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                ty2.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                ty2.Text = "0";
                return;
            }
        }
        private void tx3_Leave(object sender, EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tx3.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                tx3.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tx3.Text = "0";
                return;
            }
        }

        private void ty3_Leave(object sender, EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(ty3.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                ty3.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                ty3.Text = "0";
                return;
            }
        }

        private void tx4_Leave(object sender, EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tx4.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                tx4.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tx4.Text = "0";
                return;
            }
        }

        private void ty4_Leave(object sender, EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(ty4.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                ty4.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                ty4.Text = "0";
                return;
            }
        }

        private void tx5_Leave(object sender, EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tx5.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                tx5.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tx5.Text = "0";
                return;
            }
        }

        private void ty5_Leave(object sender, EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(ty5.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                ty5.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                ty5.Text = "0";
                return;
            }
        }

        private void tx6_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tx6.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                tx6.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tx6.Text = "0";
                return;
            }
        }

        private void ty6_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(ty6.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                ty6.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                ty6.Text = "0";
                return;
            }
        }

        private void tx7_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tx7.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                tx7.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tx7.Text = "0";
                return;
            }
        }

        private void ty7_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(ty7.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                ty7.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                ty7.Text = "0";
                return;
            }
        }

        private void tx8_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tx8.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                tx8.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tx8.Text = "0";
                return;
            }
        }

        private void ty8_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(ty8.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                ty8.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                ty8.Text = "0";
                return;
            }
        }

        private void tx9_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tx9.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                tx9.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tx9.Text = "0";
                return;
            }
        }

        private void ty9_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(ty9.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                ty9.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                ty9.Text = "0";
                return;
            }
        }

        private void tx10_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tx10.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                tx10.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tx10.Text = "0";
                return;
            }
        }

        private void ty10_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(ty10.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                ty10.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                ty10.Text = "0";
                return;
            }
        }

        private void tx11_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tx11.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                tx11.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tx11.Text = "0";
                return;
            }
        }

        private void ty11_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(ty11.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                ty11.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                ty11.Text = "0";
                return;
            }
        }

        private void tx12_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tx12.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                tx12.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tx12.Text = "0";
                return;
            }
        }

        private void ty12_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(ty12.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                ty12.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                ty12.Text = "0";
                return;
            }
        }

        private void txs1_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(txs1.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                txs1.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                txs1.Text = "0";
                return;
            }
        }

        private void tys1_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tys1.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                tys1.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tys1.Text = "0";
                return;
            }
        }

        private void txs2_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(txs2.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                txs2.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                txs2.Text = "0";
                return;
            }
        }

        private void tys2_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tys2.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                tys2.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tys2.Text = "0";
                return;
            }
        }

        private void textBoxHeight_Leave(object sender, EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(textBoxHeight.Text);
                drawLine();
            }
            catch (System.FormatException)
            {
                textBoxHeight.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                textBoxHeight.Text = "0";
                return;
            }
        }        
        private void txtAdjacent_Leave(object sender, EventArgs e)
        {
            double d;
            try
            {
                d = Convert.ToDouble(txtAdjacent.Text);
            }
            catch (FormatException)
            {
                txtAdjacent.Text = "0";
            }
        }
        private void txtRadiusofShieldwires_Leave(object sender, EventArgs e)
        {
            double rSW;
            try
            {
                rSW = Convert.ToDouble(txtRadiusofShieldwires.Text);
            }
            catch (FormatException)
            {
                txtRadiusofShieldwires.Text = "0";
                return;
            }
            catch (OverflowException)
            {
                txtRadiusofShieldwires.Text = "0";
            }
        }
        private void txtDiameterofConductors_Leave(object sender, EventArgs e)
        {
            double rC;
            try
            {
                rC = Convert.ToDouble(txtDiameterofConductors.Text);
            }
            catch (FormatException)
            {
                txtDiameterofConductors.Text = "0";
                return;
            }
            catch (OverflowException)
            {
                txtDiameterofConductors.Text = "0";
            }

        }

        private void cmbCircuitNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            circuitSelectChange = cmbCircuitNumber.SelectedIndex;

            toolStripButtonElectrical.Enabled = true;
            electricalToolStripMenuItem.Enabled = true;

            //Menu Items that'll become Active
            printToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            toolStripButtonSave.Enabled = true;
            toolStripButtonPrint.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;

            if (cmbCircuitNumber.SelectedIndex == 0)
            {
                tx1.Enabled = true;
                ty1.Enabled = true;
                tx2.Enabled = true;
                ty2.Enabled = true;
                tx3.Enabled = true;
                ty3.Enabled = true;
                tx4.Enabled = false;
                ty4.Enabled = false;
                tx5.Enabled = false;
                ty5.Enabled = false;
                tx6.Enabled = false;
                ty6.Enabled = false;
                tx7.Enabled = false;
                ty7.Enabled = false;
                tx8.Enabled = false;
                ty8.Enabled = false;
                tx9.Enabled = false;
                ty9.Enabled = false;
                tx10.Enabled = false;
                ty10.Enabled = false;
                tx11.Enabled = false;
                ty11.Enabled = false;
                tx12.Enabled = false;
                ty12.Enabled = false;

                //Data from Elecrical Panel that'll become Active
                tr1.Enabled = true;
                tl1.Enabled = true;                
            }
            if (cmbCircuitNumber.SelectedIndex == 1)
            {
                tx1.Enabled = true;
                ty1.Enabled = true;
                tx2.Enabled = true;
                ty2.Enabled = true;
                tx3.Enabled = true;
                ty3.Enabled = true;
                tx4.Enabled = true;
                ty4.Enabled = true;
                tx5.Enabled = true;
                ty5.Enabled = true;
                tx6.Enabled = true;
                ty6.Enabled = true;
                tx7.Enabled = false;
                ty7.Enabled = false;
                tx8.Enabled = false;
                ty8.Enabled = false;
                tx9.Enabled = false;
                ty9.Enabled = false;
                tx10.Enabled = false;
                ty10.Enabled = false;
                tx11.Enabled = false;
                ty11.Enabled = false;
                tx12.Enabled = false;
                ty12.Enabled = false;

                //Data from Elecrical Panel that'll become Active
                tr1.Enabled = true;
                tl1.Enabled = true;                
            }
            if (cmbCircuitNumber.SelectedIndex == 2)
            {
                tx1.Enabled = true;
                ty1.Enabled = true;
                tx2.Enabled = true;
                ty2.Enabled = true;
                tx3.Enabled = true;
                ty3.Enabled = true;
                tx4.Enabled = true;
                ty4.Enabled = true;
                tx5.Enabled = true;
                ty5.Enabled = true;
                tx6.Enabled = true;
                ty6.Enabled = true;
                tx7.Enabled = true;
                ty7.Enabled = true;
                tx8.Enabled = true;
                ty8.Enabled = true;
                tx9.Enabled = true;
                ty9.Enabled = true;
                tx10.Enabled = false;
                ty10.Enabled = false;
                tx11.Enabled = false;
                ty11.Enabled = false;
                tx12.Enabled = false;
                ty12.Enabled = false;

                //Data from Elecrical Panel that'll become Active
                tr1.Enabled = true;
                tl1.Enabled = true;                
            }
            if (cmbCircuitNumber.SelectedIndex == 3)
            {
                tx1.Enabled = true;
                ty1.Enabled = true;
                tx2.Enabled = true;
                ty2.Enabled = true;
                tx3.Enabled = true;
                ty3.Enabled = true;
                tx4.Enabled = true;
                ty4.Enabled = true;
                tx5.Enabled = true;
                ty5.Enabled = true;
                tx6.Enabled = true;
                ty6.Enabled = true;
                tx7.Enabled = true;
                ty7.Enabled = true;
                tx8.Enabled = true;
                ty8.Enabled = true;
                tx9.Enabled = true;
                ty9.Enabled = true;
                tx10.Enabled = true;
                ty10.Enabled = true;
                tx11.Enabled = true;
                ty11.Enabled = true;
                tx12.Enabled = true;
                ty12.Enabled = true;

                //Data from Elecrical Panel that'll become Active
                tr1.Enabled = true;
                tl1.Enabled = true;                
            }
        }

        private void cmbShieldNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            shieldwireSelectChange = cmbShieldNumber.SelectedIndex;

            if (cmbShieldNumber.SelectedIndex == 0)
            {
                txtRadiusofShieldwires.Enabled = false;
                txs1.Enabled = false;
                tys1.Enabled = false;
                txs2.Enabled = false;
                tys2.Enabled = false;

                //Data from Elecrical Panel that'll become Active
                trs1.Enabled = false;
                tls1.Enabled = false;               
            }
            if (cmbShieldNumber.SelectedIndex == 1)
            {
                txtRadiusofShieldwires.Enabled = true;
                txs1.Enabled = true;
                tys1.Enabled = true;
                txs2.Enabled = false;
                tys2.Enabled = false;

                //Data from Elecrical Panel that'll become Active
                trs1.Enabled = true;
                tls1.Enabled = true;                
            }
            if (cmbShieldNumber.SelectedIndex == 2)
            {
                txtRadiusofShieldwires.Enabled = true;
                txs1.Enabled = true;
                tys1.Enabled = true;
                txs2.Enabled = true;
                tys2.Enabled = true;

                //Data from Elecrical Panel that'll become Active
                trs1.Enabled = true;
                tls1.Enabled = true;                
            }
        }

        private void cmbCircuitNumber_Leave(object sender, EventArgs e)
        {
            cmbCircuitNumber.SelectedIndex = circuitSelectChange;
        }

        private void cmbShieldNumber_Leave(object sender, EventArgs e)
        {
            cmbShieldNumber.SelectedIndex = shieldwireSelectChange;
        }

        private void cmbbundlesNumber_Leave(object sender, EventArgs e)
        {
            cmbbundlesNumber.SelectedIndex = bundleSelectChange;
        }

        private void cmbbundlesNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            bundlesNumber = cmbbundlesNumber.SelectedIndex + 1;
            bundleSelectChange = cmbbundlesNumber.SelectedIndex;
            drawBundle();
            if (cmbbundlesNumber.SelectedIndex == 0)
                txtAdjacent.Enabled = false;
            else
                txtAdjacent.Enabled = true;
        }

        private void toolStripButtonElectrical_Click(object sender, EventArgs e)
        {
            getGeneralData();
            getEnvironmentData();
            lineParameter = new double[circuitNumber + shieldNumber, 2];
            zeroSequence = new string[circuitNumber, circuitNumber];
       
            if (circuitNumber == 1 && shieldNumber == 0)
            {
                lineParameter[0, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[0, 1] = Convert.ToDouble(tl1.Text);
            }
            if (circuitNumber == 2 && shieldNumber == 0)
            {
                lineParameter[0, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[0, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[1, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[1, 1] = Convert.ToDouble(tl1.Text);
            }
            if (circuitNumber == 3 && shieldNumber == 0)
            {
                lineParameter[0, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[0, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[1, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[1, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[2, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[2, 1] = Convert.ToDouble(tl1.Text);
            }
            if (circuitNumber == 4 && shieldNumber == 0)
            {
                lineParameter[0, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[0, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[1, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[1, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[2, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[2, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[3, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[3, 1] = Convert.ToDouble(tl1.Text);
            }
            if (circuitNumber == 1 && shieldNumber == 1)
            {
                lineParameter[0, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[0, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[1, 0] = Convert.ToDouble(trs1.Text);
                lineParameter[1, 1] = Convert.ToDouble(tls1.Text);
            }
            if (circuitNumber == 2 && shieldNumber == 1)
            {
                lineParameter[0, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[0, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[1, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[1, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[2, 0] = Convert.ToDouble(trs1.Text);
                lineParameter[2, 1] = Convert.ToDouble(tls1.Text);
            }
            if (circuitNumber == 3 && shieldNumber == 1)
            {
                lineParameter[0, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[0, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[1, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[1, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[2, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[2, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[3, 0] = Convert.ToDouble(trs1.Text);
                lineParameter[3, 1] = Convert.ToDouble(tls1.Text);
            }
            if (circuitNumber == 4 && shieldNumber == 1)
            {
                lineParameter[0, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[0, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[1, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[1, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[2, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[2, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[3, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[3, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[4, 0] = Convert.ToDouble(trs1.Text);
                lineParameter[4, 1] = Convert.ToDouble(tls1.Text);
            }
            if (circuitNumber == 1 && shieldNumber == 2)
            {
                lineParameter[0, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[0, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[1, 0] = Convert.ToDouble(trs1.Text);
                lineParameter[1, 1] = Convert.ToDouble(tls1.Text);
                lineParameter[2, 0] = Convert.ToDouble(trs1.Text);
                lineParameter[2, 1] = Convert.ToDouble(tls1.Text);
            }
            if (circuitNumber == 2 && shieldNumber == 2)
            {
                lineParameter[0, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[0, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[1, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[1, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[2, 0] = Convert.ToDouble(trs1.Text);
                lineParameter[2, 1] = Convert.ToDouble(tls1.Text);
                lineParameter[3, 0] = Convert.ToDouble(trs1.Text);
                lineParameter[3, 1] = Convert.ToDouble(tls1.Text);
            }
            if (circuitNumber == 3 && shieldNumber == 2)
            {
                lineParameter[0, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[0, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[1, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[1, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[2, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[2, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[3, 0] = Convert.ToDouble(trs1.Text);
                lineParameter[3, 1] = Convert.ToDouble(tls1.Text);
                lineParameter[4, 0] = Convert.ToDouble(trs1.Text);
                lineParameter[4, 1] = Convert.ToDouble(tls1.Text);
            }
            if (circuitNumber == 4 && shieldNumber == 2)
            {
                lineParameter[0, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[0, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[1, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[1, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[2, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[2, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[3, 0] = Convert.ToDouble(tr1.Text);
                lineParameter[3, 1] = Convert.ToDouble(tl1.Text);
                lineParameter[4, 0] = Convert.ToDouble(trs1.Text);
                lineParameter[4, 1] = Convert.ToDouble(tls1.Text);
                lineParameter[5, 0] = Convert.ToDouble(trs1.Text);
                lineParameter[5, 1] = Convert.ToDouble(tls1.Text);
            }

            Frequency = Convert.ToDouble(cmbFrequency.Text);
            earthResistivity = Convert.ToDouble(txtEarthResistivity.Text);
            lineVoltage = Convert.ToDouble(txtLineVoltage.Text);
            ACResistance = Convert.ToDouble(txtACResistance.Text);
            maxPower = Convert.ToDouble(txtMaxPower.Text);
            powerStep = Convert.ToDouble(txtPowerStep.Text);
            cosPhi = Convert.ToDouble(txtCosPhi.Text);
            if (radioButtonLag.Checked == true)
                phase = 1;
            else
                phase = -1;

            eMatrix = new double[3 * circuitNumber];
            eMatrix = Electrical.ElectricalCalculator.Gradient(circuitNumber, lineVoltage, linePos, Req, epsilon, pi, conductorRadius, Convert.ToDouble(bundlesNumber));
            zeroSequence = Electrical.ElectricalCalculator.Zero_Sequence(circuitNumber, shieldNumber, linePos, shieldPos, lineParameter, Frequency, earthResistivity);
            RXYZ = Electrical.ElectricalCalculator.lineParameters(bundlesNumber, linePos, adjacentDistance, circuitNumber, conductorDiameter * .5, ACResistance, Frequency);
            int powerResultIndex = Convert.ToInt16(maxPower / powerStep) + 1;
            powerResult = new double[powerResultIndex, 8];
            powerResult = Electrical.ElectricalCalculator.power(RXYZ[0], RXYZ[1], RXYZ[2], maxPower, cosPhi, lineVoltage / Math.Sqrt(3), lineLength, powerStep, phase);
            coronaLoss = Electrical.ElectricalCalculator.coronaLoss(Frequency, Pressure, Temperature, conductorDiameter*50.0, GMD*100.0, lineVoltage, Req*100.0,mm0,Rain,eMatrix);

            txtCoronaPerKm.Text = coronaLoss[0].ToString();
            txtCoronaTotal.Text = Convert.ToString(coronaLoss[0] * lineLength);
            txtCriticalVoltage.Text = coronaLoss[1].ToString();
            txtVisualCriticalVoltage.Text = coronaLoss[2].ToString();
            txtRainyCorona.Text = coronaLoss[3].ToString();

            txtSIL.Text = Convert.ToString(Math.Pow(lineVoltage, 2) / RXYZ[3]);
                        
            dataGridViewGradient.Rows.Clear();
            int j = 0;
            for (int i = 0; i <= 3 * circuitNumber - 1; i++)
            {
                if (i % 3 == 0)
                {                    
                    dataGridViewGradient.Rows.Add(Convert.ToString(j + 1), eMatrix[3 * j], eMatrix[3 * j + 1], eMatrix[3 * j + 2]);                    
                    j++;
                }
            }

            txtR.Text = RXYZ[0].ToString();
            txtXL.Text = RXYZ[1].ToString();
            txtYC.Text = RXYZ[2].ToString();
            txtZC.Text = RXYZ[3].ToString();

            dataGridViewPower.Rows.Clear();
            for (int m = 0; m <= powerResultIndex - 1; m++)
                dataGridViewPower.Rows.Add(powerResult[m, 0], powerResult[m, 1], powerResult[m, 2], powerResult[m, 3], powerResult[m, 4], powerResult[m, 5], Math.Abs(powerResult[m, 6]), powerResult[m, 7]);

            listBox2.Items.Clear();           
            for (int n = 0; n < circuitNumber; n++)
            {
                for (int m = 0; m < circuitNumber; m++)
                {
                    listBox2.Items.Add(zeroSequence[n, m]);                    
                }               
            }
           
            lblNoData.Visible = false;
            groupBoxCorona.Visible = true;
            groupBoxParameters.Visible = true;

            lblGradientResult.Visible = true;
            lblR.Visible = true;
            txtR.Visible = true;
            lblXL.Visible = true;
            txtXL.Visible = true;
            lblYC.Visible = true;
            txtYC.Visible = true;
            lblZc.Visible = true;
            txtZC.Visible = true;
            lblCoronaPerKm.Visible = true;
            txtCoronaPerKm.Visible = true;
            lblCoronaTotal.Visible = true;
            txtCoronaTotal.Visible = true;
            lblRainyCorona.Visible = true;
            txtRainyCorona.Visible = true;
            lblCriticalVoltage.Visible = true;
            txtCriticalVoltage.Visible = true;
            lblVisualCriticalVoltage.Visible = true;
            txtVisualCriticalVoltage.Visible = true;
            lblSIL.Visible = true;
            txtSIL.Visible = true;
            dataGridViewGradient.Visible = true;
            dataGridViewPower.Visible = true;
            listBox2.Visible = true;
            tabControl.SelectedIndex = 1;
            toolStripButtonNoise.Enabled = true;
            noiseToolStripMenuItem.Enabled = true;
            toolStripButtonInsulation.Enabled = true;
            insulationToolStripMenuItem.Enabled = true;
        }        

        private void txtLineVoltage_Leave(object sender, EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(txtLineVoltage.Text);
            }
            catch (System.FormatException)
            {
                txtLineVoltage.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                txtLineVoltage.Text = "0";
                return;
            }
        }

        private void cmbFrequency_Leave(object sender, EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(cmbFrequency.Text);
            }
            catch (System.FormatException)
            {
                cmbFrequency.Text = "50";
                return;
            }
            catch (System.OverflowException)
            {
                cmbFrequency.Text = "50";
                return;
            }
        }

        private void txtEarthResistivity_Leave(object sender, EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(txtEarthResistivity.Text);
            }
            catch (System.FormatException)
            {
                txtEarthResistivity.Text = "100";
                return;
            }
            catch (System.OverflowException)
            {
                txtEarthResistivity.Text = "100";
                return;
            }
        }

        private void tr1_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tr1.Text);
            }
            catch (System.FormatException)
            {
                tr1.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tr1.Text = "0";
                return;
            }
        }

        private void tl1_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tl1.Text);
            }
            catch (System.FormatException)
            {
                tl1.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tl1.Text = "0";
                return;
            }
        }

        private void trs1_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(trs1.Text);
            }
            catch (System.FormatException)
            {
                trs1.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                trs1.Text = "0";
                return;
            }
        }

        private void tls1_Leave(object sender, System.EventArgs e)
        {
            Double x1;
            try
            {
                x1 = System.Convert.ToDouble(tls1.Text);
            }
            catch (System.FormatException)
            {
                tls1.Text = "0";
                return;
            }
            catch (System.OverflowException)
            {
                tls1.Text = "0";
                return;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (runningProject == true)
            {
                DialogResult YesNoCancel = new DialogResult();
                YesNoCancel = MessageBox.Show("There's an Open Project in the Workspace.\nDo You Want to Save the Changes?", "TLD", MessageBoxButtons.YesNoCancel);
                if (YesNoCancel == DialogResult.Yes)
                {
                    saveProject();
                    openFileDialog.ShowDialog();
                    Main.ActiveForm.Text = projectName + " - TLD";
                    openProjectForm openProjectDescription = new openProjectForm();
                    if (projectOpenedCorrectly == true)
                        openProjectDescription.ShowDialog();                        
                    projectOpenedCorrectly = false;
                }
                else if (YesNoCancel == DialogResult.No)
                {
                    openFileDialog.ShowDialog();
                    Main.ActiveForm.Text = projectName + " - TLD";
                    openProjectForm openProjectDescription = new openProjectForm();
                    if (projectOpenedCorrectly == true)
                        openProjectDescription.ShowDialog();
                    projectOpenedCorrectly = false;
                }
            }
            else
            {
                openFileDialog.ShowDialog();
                Main.ActiveForm.Text = projectName + " - TLD";
                openProjectForm openProjectDescription = new openProjectForm();
                if(projectOpenedCorrectly==true)                
                    openProjectDescription.ShowDialog();
                projectOpenedCorrectly = false;
            }
       
            richTextBoxDescription.Text = projectDescription;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (projectCondition == 0)
                saveFileDialog.ShowDialog();
            else if (projectCondition == 1)
            {
                saveProject();
                projectCondition = 1;
            }
        }

        private void saveProject()
        {
            using (StreamWriter projectFile = File.CreateText(filePath))
            {
                projectFile.WriteLine(projectName);
                projectFile.WriteLine("X1:");
                projectFile.WriteLine(tx1.Text.ToString());
                projectFile.WriteLine("Y1:");
                projectFile.WriteLine(ty1.Text.ToString());
                projectFile.WriteLine("X2");
                projectFile.WriteLine(tx2.Text.ToString());
                projectFile.WriteLine("Y2");
                projectFile.WriteLine(ty2.Text.ToString());
                projectFile.WriteLine("X3");
                projectFile.WriteLine(tx3.Text.ToString());
                projectFile.WriteLine("Y3");
                projectFile.WriteLine(ty3.Text.ToString());
                projectFile.WriteLine("X4");
                projectFile.WriteLine(tx4.Text.ToString());
                projectFile.WriteLine("Y4");
                projectFile.WriteLine(ty4.Text.ToString());
                projectFile.WriteLine("X5");
                projectFile.WriteLine(tx5.Text.ToString());
                projectFile.WriteLine("Y5");
                projectFile.WriteLine(ty5.Text.ToString());
                projectFile.WriteLine("X6");
                projectFile.WriteLine(tx6.Text.ToString());
                projectFile.WriteLine("Y6");
                projectFile.WriteLine(ty6.Text.ToString());
                projectFile.WriteLine("X7");
                projectFile.WriteLine(tx7.Text.ToString());
                projectFile.WriteLine("Y7");
                projectFile.WriteLine(ty7.Text.ToString());
                projectFile.WriteLine("X8");
                projectFile.WriteLine(tx8.Text.ToString());
                projectFile.WriteLine("Y8");
                projectFile.WriteLine(ty8.Text.ToString());
                projectFile.WriteLine("X9");
                projectFile.WriteLine(tx9.Text.ToString());
                projectFile.WriteLine("Y9");
                projectFile.WriteLine(ty9.Text.ToString());
                projectFile.WriteLine("X10");
                projectFile.WriteLine(tx10.Text.ToString());
                projectFile.WriteLine("Y10");
                projectFile.WriteLine(ty10.Text.ToString());
                projectFile.WriteLine("X11");
                projectFile.WriteLine(tx11.Text.ToString());
                projectFile.WriteLine("Y11");
                projectFile.WriteLine(ty11.Text.ToString());
                projectFile.WriteLine("X12");
                projectFile.WriteLine(tx12.Text.ToString());
                projectFile.WriteLine("Y12");
                projectFile.WriteLine(ty12.Text.ToString());
                projectFile.WriteLine("XS1");
                projectFile.WriteLine(txs1.Text.ToString());
                projectFile.WriteLine("YS2");
                projectFile.WriteLine(tys1.Text.ToString());
                projectFile.WriteLine("XS2");
                projectFile.WriteLine(txs2.Text.ToString());
                projectFile.WriteLine("YS2");
                projectFile.WriteLine(tys2.Text.ToString());
                projectFile.WriteLine("TOWER HEIGHT");
                projectFile.WriteLine(textBoxHeight.Text.ToString());
                projectFile.WriteLine("CIRCUIT NUMBER");
                projectFile.WriteLine(cmbCircuitNumber.SelectedIndex.ToString());
                projectFile.WriteLine("SHIELDWIRE NUMBER");
                projectFile.WriteLine(cmbShieldNumber.SelectedIndex.ToString());
                projectFile.WriteLine("BUNDLES NUMBER");
                projectFile.WriteLine(cmbbundlesNumber.SelectedIndex.ToString());
                projectFile.WriteLine("BUNDLE ADJACENT");
                projectFile.WriteLine(txtAdjacent.Text.ToString());
                projectFile.WriteLine("CONDUCTOR DIAMETER");
                projectFile.WriteLine(txtDiameterofConductors.Text.ToString());
                projectFile.WriteLine("SHIELDWIRE RADIUS");
                projectFile.WriteLine(txtRadiusofShieldwires.Text.ToString());
                projectFile.WriteLine("LINE VOLTAGE");
                projectFile.WriteLine(txtLineVoltage.Text.ToString());
                projectFile.WriteLine("FREQUENCY");
                projectFile.WriteLine(cmbFrequency.SelectedIndex.ToString());
                projectFile.WriteLine("EARTH RESISTIVITY");
                projectFile.WriteLine(txtEarthResistivity.Text.ToString());
                projectFile.WriteLine("R1");
                projectFile.WriteLine(tr1.Text.ToString());
                projectFile.WriteLine("L1");
                projectFile.WriteLine(tl1.Text.ToString());
                projectFile.WriteLine("R2");
                projectFile.WriteLine(tr1.Text.ToString());
                projectFile.WriteLine("L2");
                projectFile.WriteLine(tl1.Text.ToString());
                projectFile.WriteLine("R3");
                projectFile.WriteLine(tr1.Text.ToString());
                projectFile.WriteLine("L3");
                projectFile.WriteLine(tl1.Text.ToString());
                projectFile.WriteLine("R4");
                projectFile.WriteLine(tr1.Text.ToString());
                projectFile.WriteLine("L4");
                projectFile.WriteLine(tl1.Text.ToString());
                projectFile.WriteLine("RS1");
                projectFile.WriteLine(trs1.Text.ToString());
                projectFile.WriteLine("LS1");
                projectFile.WriteLine(tls1.Text.ToString());
                projectFile.WriteLine("RS2");
                projectFile.WriteLine(trs1.Text.ToString());
                projectFile.WriteLine("LS2");
                projectFile.WriteLine(tls1.Text.ToString());//line100
                projectFile.WriteLine("Line Length");
                projectFile.WriteLine(txtLineLength.Text);
                projectFile.WriteLine("Altitude");
                projectFile.WriteLine(txtAltitude.Text);
                projectFile.WriteLine("mm0");
                projectFile.WriteLine(txtmm0.Text);
                projectFile.WriteLine("Conductor Area");
                projectFile.WriteLine(txtConductorArea.Text);
                projectFile.WriteLine("Elasticity");
                projectFile.WriteLine(txtElasticity.Text);
                projectFile.WriteLine("Alpha");
                projectFile.WriteLine(txtAlpha.Text);
                projectFile.WriteLine("Cos Phi");
                projectFile.WriteLine(txtCosPhi.Text);
                projectFile.WriteLine("Max Power");
                projectFile.WriteLine(txtMaxPower.Text);
                projectFile.WriteLine("Power Step");
                projectFile.WriteLine(txtPowerStep.Text);
                projectFile.WriteLine("AC Resistance");
                projectFile.WriteLine(txtACResistance.Text);
                projectFile.WriteLine("Weather Condition");
                projectFile.WriteLine(cmbWeatherCondition.SelectedIndex.ToString());
                projectFile.WriteLine("Temperature");
                projectFile.WriteLine(txtTemerature.Text);
                projectFile.WriteLine("Wind Speed");
                projectFile.WriteLine(txtWindSpeed.Text);
                projectFile.WriteLine("Rain");
                projectFile.WriteLine(txtRain.Text);
                projectFile.WriteLine("Radio Frequency");
                projectFile.WriteLine(txtRadioFreq.Text);
                projectFile.WriteLine("TV Frequency");
                projectFile.WriteLine(txtTVFreq.Text);
                projectFile.WriteLine("TV Bandwidth");
                projectFile.WriteLine(txtTVBandwidth.Text);
                projectFile.WriteLine("Region Condition");
                projectFile.WriteLine(cmbRegionCondition.SelectedIndex.ToString());
                projectFile.WriteLine("Safety Factor");
                projectFile.WriteLine(txtSafetyFactor.Text);
                projectFile.WriteLine("Span");
                projectFile.WriteLine(txtSpan.Text);
                projectFile.WriteLine("Isokeraunic Level");
                projectFile.WriteLine(txtIKL.Text);
                projectFile.WriteLine("Tower Footing Resistance");
                projectFile.WriteLine(txtTowerFootingResistance.Text);
                projectFile.WriteLine("Nsf");
                projectFile.WriteLine(txtNSF.Text);
                projectFile.WriteLine("Arching Horn Factor");
                projectFile.WriteLine(txtK1.Text);
                projectFile.WriteLine("Tf");
                projectFile.WriteLine(cmbTf.SelectedIndex.ToString());
                projectFile.WriteLine("Insulator Type");
                projectFile.WriteLine(cmbInsulatorType.SelectedIndex.ToString());
                projectFile.WriteLine("Over Voltage Factor");
                projectFile.WriteLine(txtOverVoltageFactor.Text);
                projectFile.WriteLine("Switching Surge Over Voltage");
                projectFile.WriteLine(txtSwitchingSurgePU.Text);
                projectFile.WriteLine("Contamination");
                projectFile.WriteLine(txtContamination.Text);
                projectFile.WriteLine("Insulator Length");
                projectFile.WriteLine(txtInsulatorLength.Text);
                projectFile.WriteLine("Insulator Creepage Length");
                projectFile.WriteLine(txtInsulatorCreepageLength.Text);
                projectFile.WriteLine("Lag or Lead");
                projectFile.WriteLine(phase.ToString());
                projectFile.WriteLine("Description:");
                projectFile.WriteLine(projectDescription);//102
            }
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {            
            string[] readData;
            filePath = openFileDialog.FileName;
            readData = File.ReadAllLines(filePath);
            if (readData.Length != 0)
            {
                tx1.Text = readData[2];
                ty1.Text = readData[4];
                tx2.Text = readData[6];
                ty2.Text = readData[8];
                tx3.Text = readData[10];
                ty3.Text = readData[12];
                tx4.Text = readData[14];
                ty4.Text = readData[16];
                tx5.Text = readData[18];
                ty5.Text = readData[20];
                tx6.Text = readData[22];
                ty6.Text = readData[24];
                tx7.Text = readData[26];
                ty7.Text = readData[28];
                tx8.Text = readData[30];
                ty8.Text = readData[32];
                tx9.Text = readData[34];
                ty9.Text = readData[36];
                tx10.Text = readData[38];
                ty10.Text = readData[40];
                tx11.Text = readData[42];
                ty11.Text = readData[44];
                tx12.Text = readData[46];
                ty12.Text = readData[48];
                txs1.Text = readData[50];
                tys1.Text = readData[52];
                txs2.Text = readData[54];
                tys2.Text = readData[56];
                textBoxHeight.Text = readData[58];
                cmbCircuitNumber.SelectedIndex = Convert.ToInt16(readData[60]);
                cmbShieldNumber.SelectedIndex = Convert.ToInt16(readData[62]);
                cmbbundlesNumber.SelectedIndex = Convert.ToInt16(readData[64]);
                txtAdjacent.Text = readData[66];
                txtDiameterofConductors.Text = readData[68];
                txtRadiusofShieldwires.Text = readData[70];
                txtLineVoltage.Text = readData[72];
                cmbFrequency.SelectedIndex = Convert.ToInt16(readData[74]);
                txtEarthResistivity.Text = readData[76];
                tr1.Text = readData[78];
                tl1.Text = readData[80];                
                trs1.Text = readData[94];
                tls1.Text = readData[96];
                ////////////////////////
                txtLineLength.Text = readData[102];
                txtAltitude.Text = readData[104];
                txtmm0.Text = readData[106];
                txtConductorArea.Text = readData[108];
                txtElasticity.Text = readData[110];
                txtAlpha.Text = readData[112];
                txtCosPhi.Text = readData[114];
                txtMaxPower.Text = readData[116];
                txtPowerStep.Text = readData[118];
                txtACResistance.Text = readData[120];
                cmbWeatherCondition.SelectedIndex = Convert.ToInt16(readData[122]);
                txtTemerature.Text = readData[124];
                txtWindSpeed.Text = readData[126];
                txtRain.Text = readData[128];
                txtRadioFreq.Text = readData[130];
                txtTVFreq.Text = readData[132];
                txtTVBandwidth.Text = readData[134];
                cmbRegionCondition.SelectedIndex = Convert.ToInt16(readData[136]);
                txtSafetyFactor.Text = readData[138];
                txtSpan.Text = readData[140];
                txtIKL.Text = readData[142];
                txtTowerFootingResistance.Text = readData[144];
                txtNSF.Text = readData[146];
                txtK1.Text = readData[148];
                cmbTf.SelectedIndex = Convert.ToInt16(readData[150]);
                cmbInsulatorType.SelectedIndex = Convert.ToInt16(readData[152]);
                txtOverVoltageFactor.Text = readData[154];
                txtSwitchingSurgePU.Text = readData[156];
                txtContamination.Text = readData[158];
                txtInsulatorLength.Text = readData[160];
                txtInsulatorCreepageLength.Text = readData[162];
                phase = Convert.ToDouble(readData[164]);
                if (phase == 1)
                    radioButtonLag.Checked = true;
                else
                    radioButtonLead.Checked = true;
                projectName = readData[0];
                projectDescription = readData[166];
            }
            //Open Project          
            splitContainerMain.Visible = true;           
            treeViewMain.ExpandAll();
            TreeNode mainSelect = treeViewMain.TopNode;
            treeViewMain.SelectedNode = mainSelect;
            printToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            toolStripButtonSave.Enabled = true;
            toolStripButtonPrint.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            closeProjectToolStripMenuItem.Enabled = true;
            runningProject = true;
            projectCondition = 1;
            resultsReset();
            drawLine();
            projectOpenedCorrectly = true;
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(runningProject==true)
                if (saveToolStripMenuItem.Enabled == true)
                {
                    DialogResult YesNoCancel = new DialogResult();
                    YesNoCancel = MessageBox.Show("Do You Want to Save the Changes?", "TLD", MessageBoxButtons.YesNoCancel);
                    if (YesNoCancel == DialogResult.Yes)
                    {
                        saveProject();
                        splitContainerMain.Visible = false;                     
                        printToolStripMenuItem.Enabled = false;
                        saveToolStripMenuItem.Enabled = false;
                        toolStripButtonSave.Enabled = false;
                        toolStripButtonPrint.Enabled = false;
                        saveAsToolStripMenuItem.Enabled = false;
                        closeProjectToolStripMenuItem.Enabled = false;
                        runningProject = false;
                        Main.ActiveForm.Text = "TLD";
                    }
                    else if (YesNoCancel == DialogResult.No)
                    {
                        splitContainerMain.Visible = false;
                        printToolStripMenuItem.Enabled = false;
                        saveToolStripMenuItem.Enabled = false;
                        toolStripButtonSave.Enabled = false;
                        toolStripButtonPrint.Enabled = false;
                        saveAsToolStripMenuItem.Enabled = false;
                        closeProjectToolStripMenuItem.Enabled = false;
                        runningProject = false;
                        Main.ActiveForm.Text = "TLD";
                    }
                }
                else
                {
                    splitContainerMain.Visible = false;                  
                    printToolStripMenuItem.Enabled = false;
                    saveToolStripMenuItem.Enabled = false;
                    toolStripButtonSave.Enabled = false;
                    toolStripButtonPrint.Enabled = false;
                    saveAsToolStripMenuItem.Enabled = false;
                    closeProjectToolStripMenuItem.Enabled = false;
                    runningProject = false;
                    Main.ActiveForm.Text = "TLD";
                }
            projectReset();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog printProject = new PrintDialog();
            printProject.ShowDialog();
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            filePath = saveFileDialog.FileName;
            projectCondition = 1;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
            saveProject();
        }

        private void toolStripButtonNoise_Click(object sender, EventArgs e)
        {
            getNoiseFrequencyData();            
            RI = new double[101];
            RI = Noise.NoiseCalculator.radioNoise(circuitNumber, eMatrix, conductorDiameter * 100, linePos,weatherCondition,Temperature,Pressure,windSpeed,earthResistivity,radioFrequency);
            TVI = new double[101];
            TVI = Noise.NoiseCalculator.TVNoise(RI, circuitNumber, TVBandwidth, TVFrequency);
            AN = new double[101];
            AN = Noise.NoiseCalculator.audibleNoise((double)bundlesNumber, conductorDiameter * 100, circuitNumber, eMatrix, linePos, Req, weatherCondition);
            lblNoDataNoise.Visible = false;           
            dataGridViewNoise.Visible = true;
            noiseGraph.Visible = true;
            cmbNoiseResultView.Visible = true;
            lblShowResultNoise.Visible = true;
            tabControl.SelectedIndex = 2;
            cmbNoiseResultView.SelectedIndex = 0;
            noiseResult();
        }        

        private void noiseResult()
        {
            GraphPane drawGraph = noiseGraph.GraphPane;
            drawGraph.CurveList.Clear();

            PointPairList graphPoint = new PointPairList();

            if (cmbNoiseResultView.SelectedIndex == 0)
            {
                drawGraph.Title.Text = "Radio Interference";
                drawGraph.YAxis.Title.Text = "RI(dB)";
                for (int i = 0; i <= 100; i++)
                {
                    double x = (double)(i - 50);
                    double y = RI[i];
                    graphPoint.Add(x, y);                   
                }
            }
            else if (cmbNoiseResultView.SelectedIndex == 1)
            {
                drawGraph.Title.Text = "TV Interference";
                drawGraph.YAxis.Title.Text = "TVI(dB)";
                for (int i = 0; i <= 100; i++)
                {
                    double x = (double)(i - 50);
                    double y = TVI[i];
                    graphPoint.Add(x, y);                   
                }
            }
            else if (cmbNoiseResultView.SelectedIndex == 2)
            {
                drawGraph.Title.Text = "Audible Noise";
                drawGraph.YAxis.Title.Text = "AN(dB/20μPa)";
                for (int i = 0; i <= 100; i++)
                {
                    double x = (double)(i - 50);
                    double y = AN[i];
                    graphPoint.Add(x, y);                  
                }
            }            
            drawGraph.XAxis.Title.Text = "Distance(m)";          
                     
            dataGridViewNoise.Rows.Clear();
            dataGridViewNoise.Columns[1].HeaderText = "Radio Interference(dB) for " + radioFrequency.ToString() + "MHz";
            dataGridViewNoise.Columns[2].HeaderText = "TV Interference(dB) for " + TVFrequency.ToString() + "MHz";
            for (int i = 0; i <= 100; i++)
                dataGridViewNoise.Rows.Add((i - 50), RI[i], TVI[i], AN[i]);
            
            LineItem myGraph = drawGraph.AddCurve("RI", graphPoint, Color.Black);           
            
            drawGraph.XAxis.MajorGrid.IsVisible = true;
            drawGraph.YAxis.MajorGrid.IsVisible = true;
            noiseGraph.AxisChange();
            noiseGraph.Refresh();           
        }       

        private void treeViewMain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedNode = treeViewMain.SelectedNode.Text;
            tabControl.SelectedIndex = 0;

            if (selectedNode == "Electrical")
            {
                panelInsulation.Enabled = false;
                panelGeneral.Enabled = false;
                panelElectrical.Enabled = true;
                panelEnvironment.Enabled = false;
                panelNoise.Enabled = false;
                panelMechanical.Enabled = false;
                panelDescription.Enabled = false;
                panelElectrical.BringToFront();
            }
            else if (selectedNode == "General")
            {
                panelInsulation.Enabled = false;
                panelGeneral.Enabled = true;
                panelElectrical.Enabled = false;
                panelEnvironment.Enabled = false;
                panelNoise.Enabled = false;
                panelMechanical.Enabled = false;
                panelDescription.Enabled = false;
                panelGeneral.BringToFront();
            }
            else if (selectedNode == "Environment")
            {
                panelInsulation.Enabled = false;
                panelGeneral.Enabled = false;
                panelElectrical.Enabled = false;
                panelEnvironment.Enabled = true;
                panelNoise.Enabled = false;
                panelMechanical.Enabled = false;
                panelDescription.Enabled = false;
                panelEnvironment.BringToFront();
            }
            else if (selectedNode == "Noise")
            {
                panelInsulation.Enabled = false;
                panelGeneral.Enabled = false;
                panelElectrical.Enabled = false;
                panelEnvironment.Enabled = false;
                panelNoise.Enabled = true;
                panelMechanical.Enabled = false;
                panelDescription.Enabled = false;
                panelNoise.BringToFront();
            }
            else if (selectedNode == "Mechanical")
            {
                panelInsulation.Enabled = false;
                panelGeneral.Enabled = false;
                panelElectrical.Enabled = false;
                panelEnvironment.Enabled = false;
                panelNoise.Enabled = false;
                panelMechanical.Enabled = true;
                panelDescription.Enabled = false;
                panelMechanical.BringToFront();
            }
            else if (selectedNode == "Main")
            {
                panelInsulation.Enabled = false;
                panelGeneral.Enabled = false;
                panelElectrical.Enabled = false;
                panelEnvironment.Enabled = false;
                panelNoise.Enabled = false;
                panelMechanical.Enabled = false;
                panelDescription.Enabled = true;
                panelDescription.BringToFront();
            }
            else if (selectedNode == "Insulation")
            {
                panelInsulation.Enabled = true;
                panelGeneral.Enabled = false;
                panelElectrical.Enabled = false;
                panelEnvironment.Enabled = false;
                panelNoise.Enabled = false;
                panelMechanical.Enabled = false;
                panelDescription.Enabled = false;
                panelInsulation.BringToFront();
            }
        }

        private void treeViewMain_Enter(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 0;
        }

        private void getNoiseFrequencyData()
        {
            radioFrequency = Convert.ToDouble(txtRadioFreq.Text);
            TVFrequency = Convert.ToDouble(txtTVFreq.Text);
            TVBandwidth = Convert.ToDouble(txtTVBandwidth.Text);
        }
        
        private void getEnvironmentData()
        {            
            Altitude = Convert.ToDouble(txtAltitude.Text);
            Pressure = Math.Pow(10, 5 - Altitude / 15500) * 76 / 101325;
            weatherCondition = cmbWeatherCondition.SelectedIndex;
            Temperature = Convert.ToDouble(txtTemerature.Text);          
            windSpeed = Convert.ToDouble(txtWindSpeed.Text);
            minFreq = Convert.ToDouble(txtRadioFreq.Text);
            maxFreq = Convert.ToDouble(txtTVFreq.Text);
            Rain = Convert.ToDouble(txtRain.Text);
        }

        private void cmbNoiseResultView_SelectedIndexChanged(object sender, EventArgs e)
        {           
            noiseResult();
        }

        private void cmbRegionCondition_SelectedIndexChanged(object sender, EventArgs e)
        {            
            dataGridViewRegionCondition.Rows.Clear();
            if (cmbRegionCondition.SelectedIndex == 0)
            {
                dataGridViewRegionCondition.Rows.Add("Winter", -5, 24, 0);
                dataGridViewRegionCondition.Rows.Add("Storm", 0, 45, 0);
                dataGridViewRegionCondition.Rows.Add("Maximum Temperature", 85, 0, 0);
                dataGridViewRegionCondition.Rows.Add("Minimum Temperature", -10, 0, 0);
            }
            else if (cmbRegionCondition.SelectedIndex == 1)
            {
                dataGridViewRegionCondition.Rows.Add("Winter", -5, 24, 10);
                dataGridViewRegionCondition.Rows.Add("Storm", 0, 40, 0);
                dataGridViewRegionCondition.Rows.Add("Maximum Temperature", 75, 0, 0);
                dataGridViewRegionCondition.Rows.Add("Minimum Temperature", -26, 0, 0);
            }
            else if (cmbRegionCondition.SelectedIndex == 2)
            {
                dataGridViewRegionCondition.Rows.Add("Winter", -5, 24, 18);
                dataGridViewRegionCondition.Rows.Add("Storm", -10, 40, 0);
                dataGridViewRegionCondition.Rows.Add("Maximum Temperature", 65, 0, 0);
                dataGridViewRegionCondition.Rows.Add("Minimum Temperature", -30, 0, 0);
            }
            else if (cmbRegionCondition.SelectedIndex == 3)
            {
                dataGridViewRegionCondition.Rows.Add("Winter", -5, 24, 38);
                dataGridViewRegionCondition.Rows.Add("Storm", -10, 40, 0);
                dataGridViewRegionCondition.Rows.Add("Maximum Temperature", 65, 0, 0);
                dataGridViewRegionCondition.Rows.Add("Minimum Temperature", -30, 0, 0);
            }

            dataGridViewRegionCondition.Rows.Add("Normal", 20, 0, 0);            
        }

        private void richTextBoxDescription_TextChanged(object sender, EventArgs e)
        {
            projectDescription = richTextBoxDescription.Text;
        }

        private void toolStripButtonMechanical_Click(object sender, EventArgs e)
        {
            getMechanicalData();
            mechanicalResult = Mechanical.MechanicalCalculator.MechanicalCharacteristic(conductorArea, Elasticity, Span, normalWeight, SafetyFactor, UTS, Alpha, mechanicalWeatherCondition, conductorDiameter * 1000.0);
            lblNoDataMechanical.Visible = false;
            dataGridViewMechanical.Visible = true;

            dataGridViewMechanical.Rows.Clear();

            dataGridViewMechanical.Rows.Add(mechanicalWeatherCondition[0, 0], mechanicalWeatherCondition[1, 0], mechanicalWeatherCondition[2, 0], mechanicalWeatherCondition[3, 0]);
            dataGridViewMechanical.Rows[0].HeaderCell.Value = "Temperature(°C)";
            dataGridViewMechanical.Rows.Add(mechanicalWeatherCondition[0, 1], mechanicalWeatherCondition[1, 1], mechanicalWeatherCondition[2, 1], mechanicalWeatherCondition[3, 1]);
            dataGridViewMechanical.Rows[1].HeaderCell.Value = "Wind Velocity(m/s)";
            dataGridViewMechanical.Rows.Add(mechanicalWeatherCondition[0, 2], mechanicalWeatherCondition[1, 2], mechanicalWeatherCondition[2, 2], mechanicalWeatherCondition[3, 2]);
            dataGridViewMechanical.Rows[2].HeaderCell.Value = "Ice Thickness(mm)";
            dataGridViewMechanical.Rows.Add(mechanicalResult[0, 0], mechanicalResult[1, 0], mechanicalResult[2, 0], mechanicalResult[3, 0]);
            dataGridViewMechanical.Rows[3].HeaderCell.Value = "Tension(kg)";
            dataGridViewMechanical.Rows.Add(mechanicalResult[0, 1], mechanicalResult[1, 1], mechanicalResult[2, 1], mechanicalResult[3, 1]);
            dataGridViewMechanical.Rows[4].HeaderCell.Value = "S.F.";
            dataGridViewMechanical.Rows.Add(mechanicalResult[0, 2], mechanicalResult[1, 2], mechanicalResult[2, 2], mechanicalResult[3, 2]);
            dataGridViewMechanical.Rows[5].HeaderCell.Value = "% of UTS";
            dataGridViewMechanical.Rows.Add(mechanicalResult[0, 3], mechanicalResult[1, 3], mechanicalResult[2, 3], mechanicalResult[3, 3]);
            dataGridViewMechanical.Rows[6].HeaderCell.Value = "Sag(m)";            
            dataGridViewMechanical.Rows.Add(mechanicalResult[0, 4], mechanicalResult[1, 4], mechanicalResult[2, 4], mechanicalResult[3, 4]);
            dataGridViewMechanical.Rows[7].HeaderCell.Value = "a(m)";

            tabControl.SelectedIndex = 4;
           
        }

        private void getMechanicalData()
        {
            for (int i = 0; i < 5; i++)
            {
                mechanicalWeatherCondition[i, 0] = Convert.ToDouble(dataGridViewRegionCondition.Rows[i].Cells["ColumnTemperature"].Value);
                mechanicalWeatherCondition[i, 1] = Convert.ToDouble(dataGridViewRegionCondition.Rows[i].Cells["ColumnWind"].Value);
                mechanicalWeatherCondition[i, 2] = Convert.ToDouble(dataGridViewRegionCondition.Rows[i].Cells["ColumnIce"].Value);
            }
            SafetyFactor = Convert.ToDouble(txtSafetyFactor.Text);
            Span = Convert.ToDouble(txtSpan.Text);
        }

        private void buttonSelectConductor_Click(object sender, EventArgs e)
        {
            ConductorForm selectConductor = new ConductorForm();
            selectConductor.ShowDialog();
            txtDiameterofConductors.Text = conductorDiameter.ToString();
            txtConductorArea.Text = conductorArea.ToString();
            txtElasticity.Text = Elasticity.ToString();
            txtAlpha.Text = Alpha.ToString();
        }

        private void projectReset()
        {
            cmbCircuitNumber.SelectedIndex = -1;
            textBoxHeight.Text = "0";
            cmbShieldNumber.SelectedIndex = 0;
            cmbbundlesNumber.SelectedIndex = 0;
            txtAdjacent.Text = "45.7";
            txtLineLength.Text = "0";
            txtAltitude.Text = "0";
            txtmm0.Text = ".8";
            conductorArea = 0;
            conductorDiameter = 0;
            Elasticity = 0;
            Alpha = 0;

            tx1.Enabled = false;
            ty1.Enabled = false;
            tx2.Enabled = false;
            ty2.Enabled = false;
            tx3.Enabled = false;
            ty3.Enabled = false;
            tx4.Enabled = false;
            ty4.Enabled = false;
            tx5.Enabled = false;
            ty5.Enabled = false;
            tx6.Enabled = false;
            ty6.Enabled = false;
            tx7.Enabled = false;
            ty7.Enabled = false;
            tx8.Enabled = false;
            ty8.Enabled = false;
            tx9.Enabled = false;
            ty9.Enabled = false;
            tx10.Enabled = false;
            ty10.Enabled = false;
            tx11.Enabled = false;
            ty11.Enabled = false;
            tx12.Enabled = false;
            ty12.Enabled = false;

            tx1.Text = "0";
            ty1.Text = "0";
            tx2.Text = "0";
            ty2.Text = "0";
            tx3.Text = "0";
            ty3.Text = "0";
            tx4.Text = "0";
            ty4.Text = "0";
            tx5.Text = "0";
            ty5.Text = "0";
            tx6.Text = "0";
            ty6.Text = "0";
            tx7.Text = "0";
            ty7.Text = "0";
            tx8.Text = "0";
            ty8.Text = "0";
            tx9.Text = "0";
            ty9.Text = "0";
            tx10.Text = "0";
            ty10.Text = "0";
            tx11.Text = "0";
            ty11.Text = "0";
            tx12.Text = "0";
            ty12.Text = "0";

            txs1.Text = "0";
            tys1.Text = "0";
            txs2.Text = "0";
            tys2.Text = "0";

            txtLineVoltage.Text = "0";
            txtEarthResistivity.Text = "100";
            cmbFrequency.SelectedIndex = 0;
            txtACResistance.Text = "0";
            txtMaxPower.Text = "0";
            txtPowerStep.Text = "0";
            txtCosPhi.Text = "1";

            cmbWeatherCondition.SelectedIndex = 0;
            txtTemerature.Text = "30";
            txtWindSpeed.Text = "0";
            txtRain.Text = "0";

            txtRadioFreq.Text = ".5";
            txtTVFreq.Text = "50";
            txtTVBandwidth.Text = "6";

            cmbRegionCondition.SelectedIndex = 0;
            txtSafetyFactor.Text = "2";
            txtSpan.Text = "0";

            txtIKL.Text = "0";
            txtTowerFootingResistance.Text = "0";
            txtNSF.Text = "0";
            txtK1.Text = ".8";
            cmbTf.SelectedIndex = 0;
            txtInsulatorLength.Text = "0";
            txtOverVoltageFactor.Text = "0";
            txtSwitchingSurgePU.Text = "0";

            resultsReset();
        }

        private void resultsReset()
        {
            lblGradientResult.Visible = false;
            dataGridViewGradient.Visible = false;
            lblR.Visible = false;
            txtR.Visible = false;
            lblYC.Visible = false;
            txtYC.Visible = false;
            lblXL.Visible = false;
            txtXL.Visible = false;
            lblZc.Visible = false;
            txtZC.Visible = false;
            lblCoronaPerKm.Visible = false;
            txtCoronaPerKm.Visible = false;
            lblCoronaTotal.Visible = false;
            txtCoronaTotal.Visible = false;
            lblRainyCorona.Visible = false;
            txtRainyCorona.Visible = false;
            lblCriticalVoltage.Visible = false;
            txtCriticalVoltage.Visible = false;
            lblVisualCriticalVoltage.Visible = false;
            txtVisualCriticalVoltage.Visible = false;
            lblSIL.Visible = false;
            txtSIL.Visible = false;
            listBox2.Visible = false;
            dataGridViewPower.Visible = false;
            lblNoData.Visible = true;

            noiseGraph.Visible = false;
            dataGridViewNoise.Visible = false;
            lblShowResultNoise.Visible = false;
            cmbNoiseResultView.Visible = false;
            lblNoDataNoise.Visible = true;

            dataGridViewMechanical.Visible = false;
            lblNoDataMechanical.Visible = true;            
        }

        private void pressureConvertorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pressure converter = new Pressure();
            converter.ShowDialog();
        }

        private void toolStripButtonInsulation_Click(object sender, EventArgs e)
        {
            IKL = Convert.ToDouble(txtIKL.Text);
            insulatorlength = Convert.ToDouble(txtInsulatorLength.Text);
            numberofShieldingFailure = Convert.ToDouble(txtNSF.Text);
            towerFootingResistance = Convert.ToDouble(txtTowerFootingResistance.Text);
            WaveFrontTime = Convert.ToDouble(cmbTf.Text);
            archingHornFactor = Convert.ToDouble(txtK1.Text);
            contamination =(double)trackBarContamination.Value;
            insulatorCreepage = Convert.ToDouble(txtInsulatorCreepageLength.Text);

            overVoltageFactor = Convert.ToDouble(txtOverVoltageFactor.Text);
            switchingSurgePU = Convert.ToDouble(txtSwitchingSurgePU.Text);

            lightningInsulation = Insulation.InsulationCalculator.Lightning(IKL, Altitude, towerHeight, lineVoltage, insulatorlength, numberofShieldingFailure, towerFootingResistance, WaveFrontTime, archingHornFactor);
            switchingInsulation = Insulation.InsulationCalculator.SwitchingSurge(lineVoltage, overVoltageFactor, switchingSurgePU, insulatorlength, Altitude);
            powerInsulation = Insulation.InsulationCalculator.PowerFreq(lineVoltage, overVoltageFactor, contamination, insulatorCreepage, Altitude);
            contaminationInsulation = Insulation.InsulationCalculator.Contamination(lineVoltage, overVoltageFactor, contamination, insulatorCreepage);

            groupBoxLightning.BackColor = Color.Transparent;
            groupBoxSwitching.BackColor = Color.Transparent;
            groupBoxPowerFrequency.BackColor = Color.Transparent;
            groupBoxContamination.BackColor = Color.Transparent;

            double[] compare = new double[4];
            compare[0] = lightningInsulation[1];
            compare[1] = switchingInsulation[1];
            compare[2] = powerInsulation[1];
            compare[3] = contaminationInsulation[1];
            int maxN = 0;
            for (int i = 0; i < 4; i++)
                if (compare[i] > compare[maxN])
                    maxN = i;
            switch (maxN)
            {
                case 0:
                    groupBoxLightning.BackColor = SystemColors.ActiveBorder;
                    break;
                case 1:
                    groupBoxSwitching.BackColor = SystemColors.ActiveBorder;
                    break;
                case 2:
                    groupBoxPowerFrequency.BackColor = SystemColors.ActiveBorder;
                    break;
                case 3:
                    groupBoxContamination.BackColor = SystemColors.ActiveBorder;
                    break;
            }

            lblNoDataInsulation.Visible = false;
            groupBoxLightning.Visible = true;
            groupBoxSwitching.Visible = true;
            groupBoxPowerFrequency.Visible = true;
            groupBoxContamination.Visible = true;
            double adder = (double)(cmbInsulatorType.SelectedIndex);
            txtLLightning.Text = lightningInsulation[0].ToString();
            txtNLightning.Text = Convert.ToString(lightningInsulation[1] + adder);
            txtLSwitching.Text = switchingInsulation[0].ToString();
            txtNSwitching.Text = Convert.ToString(switchingInsulation[1] + adder);
            txtLPowerFrequency.Text = powerInsulation[0].ToString();
            txtNPowerFrequency.Text = Convert.ToString(powerInsulation[1] + adder);
            txtLContamination.Text = contaminationInsulation[0].ToString();
            txtNcontamination.Text = Convert.ToString(contaminationInsulation[1] + adder);
            
            tabControl.SelectedIndex = 3;
        }

        private void Main_Paint(object sender, PaintEventArgs e)
        {
            drawBundle();   
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
                drawBundle();
        }

        private void trackBarContamination_Scroll(object sender, EventArgs e)
        {
            txtContamination.Text = trackBarContamination.Value.ToString();
        }

        private void unitConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            unitConverter converter = new unitConverter();
            converter.ShowDialog();
        }

        private void resistanceConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resistanceConverter converter = new resistanceConverter();
            converter.ShowDialog();
        }

        private void radioButtonLag_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLag.Checked == true)
            {
                radioButtonLead.Checked = false;
                phase = 1;
            }
        }

        private void radioButtonLead_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLead.Checked == true)
            {
                radioButtonLag.Checked = false;
                phase = -1;
            }
        }                    

    }
}