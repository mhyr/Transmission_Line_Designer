using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Electrical
{
    public class ElectricalCalculator
    {
        static public string[,] Zero_Sequence(int circuitNumber, int shieldNumber, double[,] linePos, double[,] shieldPos, double[,] dataprp, double Frequency, double earthResistivity)
        {            
            //string Zero_Sequence = "";
            string[,] Zero_Sequence = new string[circuitNumber, circuitNumber];
            double[,] dataPos = new double[14, 2];
            double re,xe;
            double[,] Space = new double[3*circuitNumber + shieldNumber, 3*circuitNumber + shieldNumber];
            double[,] xd = new double[circuitNumber + shieldNumber +1, circuitNumber + shieldNumber +1];
            double[,] Zr = new double[circuitNumber + 1, circuitNumber + 1];
            double[,] Zi = new double[circuitNumber + 1, circuitNumber + 1];
            double[,] Z0r = new double[circuitNumber + 1, circuitNumber + 1];
            double[,] Z0i = new double[circuitNumber + 1, circuitNumber + 1];
                        
            //Make Data Ready for Use
            if (shieldNumber == 2)
            {
                if (circuitNumber == 1)
                {
                    dataprp[1, 0] = (dataprp[1, 0] * dataprp[2, 0]) / (dataprp[1, 0] + dataprp[2, 0]);
                    dataprp[2, 0] = 0;
                    dataprp[1, 1] = (dataprp[1, 1] * dataprp[2, 1]) / (dataprp[1, 1] + dataprp[2, 1]);
                    dataprp[2, 1] = 0;
                }
                if (circuitNumber == 2)
                {
                    dataprp[2, 0] = (dataprp[2, 0] * dataprp[3, 0]) / (dataprp[2, 0] + dataprp[3, 0]);
                    dataprp[3, 0] = 0;
                    dataprp[2, 1] = (dataprp[2, 1] * dataprp[3, 1]) / (dataprp[2, 1] + dataprp[3, 1]);
                    dataprp[3, 1] = 0;
                }
                if (circuitNumber == 3)
                {
                    dataprp[3, 0] = (dataprp[3, 0] * dataprp[4, 0]) / (dataprp[3, 0] + dataprp[4, 0]);
                    dataprp[4, 0] = 0;
                    dataprp[3, 1] = (dataprp[3, 1] * dataprp[4, 1]) / (dataprp[3, 1] + dataprp[4, 1]);
                    dataprp[4, 1] = 0;
                }
                if (circuitNumber == 4)
                {
                    dataprp[4, 0] = (dataprp[4, 0] * dataprp[5, 0]) / (dataprp[4, 0] + dataprp[5, 0]);
                    dataprp[5, 0] = 0;
                    dataprp[4, 1] = (dataprp[4, 1] * dataprp[5, 1]) / (dataprp[4, 1] + dataprp[5, 1]);
                    dataprp[5, 1] = 0;
                }
            }
            for (int i = 0; i <= 3*circuitNumber - 1; i++)
            {
                dataPos[i, 0] = linePos[i, 0] * 3.2808399;
                dataPos[i, 1] = linePos[i, 1] * 3.2808399;
            }
            if (shieldNumber == 1)
            {
                dataPos[3*circuitNumber, 0] = shieldPos[0, 0] * 3.2808399;
                dataPos[3*circuitNumber, 1] = shieldPos[0, 1] * 3.2808399;
            }
            else if (shieldNumber == 2)
            {
                dataPos[3*circuitNumber, 0] = shieldPos[0, 0] * 3.2808399;
                dataPos[3*circuitNumber, 1] = shieldPos[0, 1] * 3.2808399;
                dataPos[3*circuitNumber + 1, 0] = shieldPos[1, 0] * 3.2808399;
                dataPos[3*circuitNumber + 1, 1] = shieldPos[1, 1] * 3.2808399;
            }
            re = .00477 * Frequency;
            xe = 0.006985 * Frequency * Math.Log10(4.6655 * 1000000 * earthResistivity / Frequency);
            
            //Calculate X
            for (int i = 0; i <= 3*circuitNumber+shieldNumber-1; i++)
                for (int j = 0; j <= 3*circuitNumber+shieldNumber-1; j++)
                    Space[i, j] = Math.Sqrt(Math.Pow(dataPos[i, 0] - dataPos[j, 0], 2) + Math.Pow(dataPos[i, 1] - dataPos[j, 1], 2));
            //Changing Space to X
            for (int i = 0; i <= 3*circuitNumber+shieldNumber-1; i++)
                for (int j = 0; j <= 3*circuitNumber+shieldNumber-1; j++)
                    Space[i, j] = 0.2328 * Math.Log10(Space[i, j]);
            
            //Calculate Z            
            /* Calculating Xd*/
            for (int i = 0; i <= circuitNumber-1; i++)
                xd[i, i] = Math.Pow(3, -1) * (Space[3 * i , 3 * i + 1] + Space[3 * i, 3 * i+2] + Space[3 * i + 1, 3 * i+2]);
            if (shieldNumber == 1)
                xd[circuitNumber, circuitNumber] = 0;
            if (shieldNumber == 2)
                xd[circuitNumber, circuitNumber] = Space[3*circuitNumber, 3*circuitNumber+1];            
            for (int i = 0; i <= circuitNumber-1; i++)
                for (int j = i +1; j <= circuitNumber-1; j++)
                    xd[i, j] = Math.Pow(9, -1) * (Space[3 * i , 3 * j ] + Space[3 * i , 3 * j + 1] + Space[3 * i , 3 * j+2]
                        + Space[3 * i +1, 3 * j ] + Space[3 * i +1, 3 * j + 1] + Space[3 * i + 1, 3 * j+2]
                        + Space[3 * i+2, 3 * j ] + Space[3 * i+2, 3 * j + 1] + Space[3 * i+2, 3 * j+2]);
            if (shieldNumber == 2)
                for (int i = 0; i <= circuitNumber-1; i++)
                    xd[i, circuitNumber] = Math.Pow(6, -1) * (Space[3 * i , 3*circuitNumber] + Space[3 * i , 3*circuitNumber+1] + Space[3 * i +1, 3*circuitNumber] + Space[3 * i + 1, 3*circuitNumber+1]
                        + Space[3 * i+2, 3*circuitNumber] + Space[3 * i+2, 3*circuitNumber+1]);
            if (shieldNumber == 1)
                for (int i = 0; i <= circuitNumber-1; i++)
                    xd[i, circuitNumber] = Math.Pow(3, -1) * (Space[3 * i , 3*circuitNumber] + Space[3 * i + 1, 3*circuitNumber] + Space[3 * i+2, 3*circuitNumber]);
            /* Calculating Z*/
            for (int i = 0; i <= circuitNumber-1; i++)
                Zr[i, i] = dataprp[i, 0] + re;
            for (int i = 0; i <= circuitNumber-1; i++)
                for (int j = i + 1; j <= circuitNumber; j++)
                    Zr[i, j] = re;
            if(shieldNumber>=1)
                Zr[circuitNumber, circuitNumber] = 3 * dataprp[circuitNumber, 0] + re;
            else if(shieldNumber==0)
                Zr[circuitNumber, circuitNumber] = re;
            for (int i = 0; i <= circuitNumber-1; i++)
                Zi[i, i] = dataprp[i, 1] + xe - 2 * xd[i, i];
            for (int i = 0; i <= circuitNumber-1; i++)
                for (int j = i + 1; j <= circuitNumber; j++)
                    Zi[i, j] = xe - 3 * xd[i, j];
            if(shieldNumber>=1)
                Zi[circuitNumber, circuitNumber] = 3 * dataprp[circuitNumber, 1] + xe - 3 * xd[circuitNumber, circuitNumber] * Math.Pow(shieldNumber, -1);
            else if(shieldNumber==0)
                Zi[circuitNumber, circuitNumber] = xe - 3 * xd[circuitNumber, circuitNumber] * Math.Pow(shieldNumber, -1);
            //Calculate Z0
            double a, b;            
            for (int i = 0; i <= circuitNumber-1; i++)
                for (int j = i; j <= circuitNumber-1; j++)
                    if (shieldNumber == 0)
                    {
                        Z0r[i, j] = Zr[i, j];
                        Z0i[i, j] = Zi[i, j];
                    }
                    else
                    {
                        Z0r[i, j] = Zr[i, circuitNumber] * Zr[j, circuitNumber] - Zi[i, circuitNumber] * Zi[j, circuitNumber];
                        Z0i[i, j] = Zr[i, circuitNumber] * Zi[j, circuitNumber] + Zi[i, circuitNumber] * Zr[j, circuitNumber];
                        a = (Z0r[i, j] * Zr[circuitNumber, circuitNumber] + Z0i[i, j] * Zi[circuitNumber, circuitNumber]) * Math.Pow(Zr[circuitNumber, circuitNumber] * Zr[circuitNumber, circuitNumber] + Zi[circuitNumber, circuitNumber] * Zi[circuitNumber, circuitNumber], -1);
                        b = (Z0i[i, j] * Zr[circuitNumber, circuitNumber] - Z0r[i, j] * Zi[circuitNumber, circuitNumber]) * Math.Pow(Zr[circuitNumber, circuitNumber] * Zr[circuitNumber, circuitNumber] + Zi[circuitNumber, circuitNumber] * Zi[circuitNumber, circuitNumber], -1);
                        Z0r[i, j] = Zr[i, j] - a;
                        Z0i[i, j] = Zi[i, j] - b;
                    }

            for (int i = 0; i <= circuitNumber - 1; i++)
                for (int j = i; j <= circuitNumber - 1; j++)
                {
                    string sign;
                    if (Z0i[i, j] < 0)
                        sign = "-";
                    else
                        sign = "+";
                    Zero_Sequence[i, j] = Z0r[i, j].ToString()+ " " + sign + " " + Z0i[i,j].ToString()+ "i";
                    Zero_Sequence[j, i] = Z0r[i, j].ToString()+ " " + sign + " " + Z0i[i,j].ToString()+ "i";
                }

                   // Zero_Sequence = Zero_Sequence + "Z0(" + (i+1).ToString() + "," + (j+1).ToString() + ")= " + Convert.ToString(Z0r[i, j]) + " +i " + Convert.ToString(Z0i[i, j]) + "\n";
            return Zero_Sequence;
        }        

        static public double[] Gradient(int circuitNumber, double lineVoltage,double[,] linePos,double Req,double epsilon,double pi,double rConductor,double bundleNumber)
        {            
            double[,] pMatrix = new double[3 * circuitNumber, 3 * circuitNumber];           
            double[] eMatrix = new double[3 * circuitNumber];
            double[] vReal = new double[3 * circuitNumber];
            double[] vImage = new double[3 * circuitNumber];  
            double[] qReal=new double[3*circuitNumber];
            double[] qImage=new double[3*circuitNumber];
            double[] qMatrix = new double[3 * circuitNumber];
            double sumReal, sumImage;
            double eMax;
            double n=0;

            for (int i = 0; i <= 3 * circuitNumber - 1; i++)
            {                
                if (((i + 1) * 10) % 3 == 0)
                {
                    n = 2 * pi / 3;
                }
                else if (((i + 1) * 10) % 3 == 1)
                {
                    n = 0;
                }
                else if (((i + 1) * 10) % 3 == 2)
                {                    
                    n = -2 * pi / 3;
                }
               
                vReal[i] = (lineVoltage / Math.Sqrt(3)) * Math.Cos(n);
                vImage[i] = (lineVoltage / Math.Sqrt(3)) * Math.Sin(n);
            }
                        
            for (int i = 0; i <= 3 * circuitNumber - 1; i++)
            {
                for (int j = 0; j <= 3 * circuitNumber - 1; j++)
                {
                    if (i == j)
                    {                        
                        pMatrix[i, j] = Math.Log(2 * linePos[i, 1] / Req);
                    }
                    else
                    {
                        pMatrix[i, j] = Math.Log(Math.Sqrt(Math.Pow(linePos[i, 1] + linePos[j, 1], 2) + Math.Pow(linePos[i, 0] - linePos[j, 0], 2)) / Math.Sqrt(Math.Pow(linePos[i, 1] - linePos[j, 1], 2) + Math.Pow(linePos[i, 0] - linePos[j, 0], 2)));
                    }
                }
            }
            
            for (int j = 0; j <= 3 * circuitNumber - 2; j++)
            {
                for (int i = j + 1; i <= 3 * circuitNumber - 1; i++)
                {
                    double k;
                    k = -pMatrix[i, j] / pMatrix[j, j];
                    for (int m = 0; m <= 3 * circuitNumber - 1; m++)
                    {
                        pMatrix[i, m] = pMatrix[j, m] * (-pMatrix[i, j] / pMatrix[j, j]) + pMatrix[i, m];
                        
                    }
                    vReal[i] = vReal[j] * k + vReal[i];
                    vImage[i] = vImage[j] * k + vImage[i];
                }
            }
            qReal[3*circuitNumber-1]=vReal[3*circuitNumber-1]/pMatrix[3*circuitNumber-1,3*circuitNumber-1];
            qImage[3*circuitNumber-1]=vImage[3*circuitNumber-1]/pMatrix[3*circuitNumber-1,3*circuitNumber-1];

            for(int i=3*circuitNumber-2;i>=0;i--)
            {
                sumReal=0;
                sumImage=0;
                for(int j=i+1;j<=3*circuitNumber-1;j++)
                {
                    sumReal=sumReal-pMatrix[i,j]*qReal[j];
                    sumImage=sumImage-pMatrix[i,j]*qImage[j];
                }
                qReal[i]=(vReal[i]+sumReal)/pMatrix[i,i];
                qImage[i]=(vImage[i]+sumImage)/pMatrix[i,i];
            }
            eMax = (1 + (bundleNumber - 1) * rConductor / Req);
            for (int i = 0; i <= 3 * circuitNumber - 1; i++)
            {
                qMatrix[i] = Math.Sqrt(Math.Pow(qReal[i], 2) + Math.Pow(qImage[i], 2));
                eMatrix[i] = eMax* qMatrix[i] / (bundleNumber*rConductor * 100);
            }
           
            return eMatrix;
        }

        static public double[] lineParameters(int boundle, double[,] linePos, double bundleSpacing, int circuitNumber, double c_Stdia,double res,double frequency)
        {
            double[] RXYZ = new double[4];
            double[] posx = new double[48];
            double[] posy = new double[48];
            double c_Nodia=c_Stdia * Math.Pow(Math.E, -.25);            
            int conductorNo;

            //Position

            //End of Position

            //Pozitions
            Double r = 0, x = 0;
            boundle = boundle - 1;
            int a = 0;
            if (boundle > 0)
                r = bundleSpacing / 100.0;
            if (boundle == 2)
                x = .25 * Math.Sqrt(3) * r;
            if (circuitNumber == 1)
            {
                a = 1;
                switch (boundle)
                {
                    case 0:
                        posx[0] = linePos[0, 0]; posy[0] = linePos[0, 1];
                        posx[1] = linePos[1, 0]; posy[1] = linePos[1, 1];
                        posx[2] = linePos[2, 0]; posy[2] = linePos[2, 1];
                        break;
                    case 1:
                        posx[0] = linePos[0, 0] - r * .5; posy[0] = linePos[0, 1];
                        posx[1] = linePos[0, 0] + r * .5; posy[1] = linePos[0, 1];
                        posx[2] = linePos[1, 0] - r * .5; posy[2] = linePos[1, 1];
                        posx[3] = linePos[1, 0] + r * .5; posy[3] = linePos[1, 1];
                        posx[4] = linePos[2, 0] - r * .5; posy[4] = linePos[2, 1];
                        posx[5] = linePos[2, 0] + r * .5; posy[5] = linePos[2, 1];
                        break;
                    case 2:
                        posx[0] = linePos[0, 0] - r * .5; posy[0] = linePos[0, 1] + x;
                        posx[1] = linePos[0, 0] + r * .5; posy[1] = linePos[0, 1] + x;
                        posx[2] = linePos[0, 0]; posy[2] = linePos[0, 1] - x;
                        posx[3] = linePos[1, 0] - r * .5; posy[3] = linePos[1, 1] + x;
                        posx[4] = linePos[1, 0] + r * .5; posy[4] = linePos[1, 1] + x;
                        posx[5] = linePos[1, 0]; posy[5] = linePos[1, 1] - x;
                        posx[6] = linePos[2, 0] - r * .5; posy[6] = linePos[2, 1] + x;
                        posx[7] = linePos[2, 0] + r * .5; posy[7] = linePos[2, 1] + x;
                        posx[8] = linePos[2, 0]; posy[8] = linePos[2, 1] - x;
                        break;
                    case 3:
                        posx[0] = linePos[0, 0] - r * .5; posy[0] = linePos[0, 1] + r * .5;
                        posx[1] = linePos[0, 0] + r * .5; posy[1] = linePos[0, 1] + r * .5;
                        posx[2] = linePos[0, 0] - r * .5; posy[2] = linePos[0, 1] - r * .5;
                        posx[3] = linePos[0, 0] + r * .5; posy[3] = linePos[0, 1] - r * .5;
                        posx[4] = linePos[1, 0] - r * .5; posy[4] = linePos[1, 1] + r * .5;
                        posx[5] = linePos[1, 0] + r * .5; posy[5] = linePos[1, 1] + r * .5;
                        posx[6] = linePos[1, 0] - r * .5; posy[6] = linePos[1, 1] - r * .5;
                        posx[7] = linePos[1, 0] + r * .5; posy[7] = linePos[1, 1] - r * .5;
                        posx[8] = linePos[2, 0] - r * .5; posy[8] = linePos[2, 1] + r * .5;
                        posx[9] = linePos[2, 0] + r * .5; posy[9] = linePos[2, 1] + r * .5;
                        posx[10] = linePos[2, 0] - r * .5; posy[10] = linePos[2, 1] - r * .5;
                        posx[11] = linePos[2, 0] + r * .5; posy[11] = linePos[2, 1] - r * .5;
                        break;
                }
            }
            if (circuitNumber == 2)
            {
                a = 2;
                switch (boundle)
                {
                    case 0:
                        posx[0] = linePos[0, 0]; posy[0] = linePos[0, 1];
                        posx[1] = linePos[3, 0]; posy[1] = linePos[3, 1];
                        posx[2] = linePos[1, 0]; posy[2] = linePos[1, 1];
                        posx[3] = linePos[4, 0]; posy[3] = linePos[4, 1];
                        posx[4] = linePos[2, 0]; posy[4] = linePos[2, 1];
                        posx[5] = linePos[5, 0]; posy[5] = linePos[5, 1];
                        break;
                    case 1:
                        posx[0] = linePos[0, 0] - r * .5; posy[0] = linePos[0, 1];
                        posx[1] = linePos[0, 0] + r * .5; posy[1] = linePos[0, 1];
                        posx[2] = linePos[3, 0] - r * .5; posy[2] = linePos[3, 1];
                        posx[3] = linePos[3, 0] + r * .5; posy[3] = linePos[3, 1];
                        posx[4] = linePos[1, 0] - r * .5; posy[4] = linePos[1, 1];
                        posx[5] = linePos[1, 0] + r * .5; posy[5] = linePos[1, 1];
                        posx[6] = linePos[4, 0] - r * .5; posy[6] = linePos[4, 1];
                        posx[7] = linePos[4, 0] + r * .5; posy[7] = linePos[4, 1];
                        posx[8] = linePos[2, 0] - r * .5; posy[8] = linePos[2, 1];
                        posx[9] = linePos[2, 0] + r * .5; posy[9] = linePos[2, 1];
                        posx[10] = linePos[5, 0] - r * .5; posy[10] = linePos[5, 1];
                        posx[11] = linePos[5, 0] + r * .5; posy[11] = linePos[5, 1];
                        break;
                    case 2:
                        posx[0] = linePos[0, 0] - r * .5; posy[0] = linePos[0, 1] + x;
                        posx[1] = linePos[0, 0] + r * .5; posy[1] = linePos[0, 1] + x;
                        posx[2] = linePos[0, 0]; posy[2] = linePos[0, 1] - x;
                        posx[3] = linePos[3, 0] - r * .5; posy[3] = linePos[3, 1] + x;
                        posx[4] = linePos[3, 0] + r * .5; posy[4] = linePos[3, 1] + x;
                        posx[5] = linePos[3, 0]; posy[5] = linePos[3, 1] - x;
                        posx[6] = linePos[1, 0] - r * .5; posy[6] = linePos[1, 1] + x;
                        posx[7] = linePos[1, 0] + r * .5; posy[7] = linePos[1, 1] + x;
                        posx[8] = linePos[1, 0]; posy[8] = linePos[1, 1] - x;
                        posx[9] = linePos[4, 0] - r * .5; posy[9] = linePos[4, 1] + x;
                        posx[10] = linePos[4, 0] + r * .5; posy[10] = linePos[4, 1] + x;
                        posx[11] = linePos[4, 0]; posy[11] = linePos[4, 1] - x;
                        posx[12] = linePos[2, 0] - r * .5; posy[12] = linePos[2, 1] + x;
                        posx[13] = linePos[2, 0] + r * .5; posy[13] = linePos[2, 1] + x;
                        posx[14] = linePos[2, 0]; posy[14] = linePos[2, 1] - x;
                        posx[15] = linePos[5, 0] - r * .5; posy[15] = linePos[5, 1] + x;
                        posx[16] = linePos[5, 0] + r * .5; posy[16] = linePos[5, 1] + x;
                        posx[17] = linePos[5, 0]; posy[17] = linePos[5, 1] - x;
                        break;
                    case 3:
                        posx[0] = linePos[0, 0] - r * .5; posy[0] = linePos[0, 1] + r * .5;
                        posx[1] = linePos[0, 0] + r * .5; posy[1] = linePos[0, 1] + r * .5;
                        posx[2] = linePos[0, 0] - r * .5; posy[2] = linePos[0, 1] - r * .5;
                        posx[3] = linePos[0, 0] + r * .5; posy[3] = linePos[0, 1] - r * .5;
                        posx[4] = linePos[3, 0] - r * .5; posy[4] = linePos[3, 1] + r * .5;
                        posx[5] = linePos[3, 0] + r * .5; posy[5] = linePos[3, 1] + r * .5;
                        posx[6] = linePos[3, 0] - r * .5; posy[6] = linePos[3, 1] - r * .5;
                        posx[7] = linePos[3, 0] + r * .5; posy[7] = linePos[3, 1] - r * .5;
                        posx[8] = linePos[1, 0] - r * .5; posy[8] = linePos[1, 1] + r * .5;
                        posx[9] = linePos[1, 0] + r * .5; posy[9] = linePos[1, 1] + r * .5;
                        posx[10] = linePos[1, 0] - r * .5; posy[10] = linePos[1, 1] - r * .5;
                        posx[11] = linePos[1, 0] + r * .5; posy[11] = linePos[1, 1] - r * .5;
                        posx[12] = linePos[4, 0] - r * .5; posy[12] = linePos[4, 1] + r * .5;
                        posx[13] = linePos[4, 0] + r * .5; posy[13] = linePos[4, 1] + r * .5;
                        posx[14] = linePos[4, 0] - r * .5; posy[14] = linePos[4, 1] - r * .5;
                        posx[15] = linePos[4, 0] + r * .5; posy[15] = linePos[4, 1] - r * .5;
                        posx[16] = linePos[2, 0] - r * .5; posy[16] = linePos[2, 1] + r * .5;
                        posx[17] = linePos[2, 0] + r * .5; posy[17] = linePos[2, 1] + r * .5;
                        posx[18] = linePos[2, 0] - r * .5; posy[18] = linePos[2, 1] - r * .5;
                        posx[19] = linePos[2, 0] + r * .5; posy[19] = linePos[2, 1] - r * .5;
                        posx[20] = linePos[5, 0] - r * .5; posy[20] = linePos[5, 1] + r * .5;
                        posx[21] = linePos[5, 0] + r * .5; posy[21] = linePos[5, 1] + r * .5;
                        posx[22] = linePos[5, 0] - r * .5; posy[22] = linePos[5, 1] - r * .5;
                        posx[23] = linePos[5, 0] + r * .5; posy[23] = linePos[5, 1] - r * .5;
                        break;
                }
            }
            if (circuitNumber == 4)
            {
                a = 4;
                switch (boundle)
                {
                    case 0:
                        posx[0] = linePos[0, 0]; posy[0] = linePos[0, 1];
                        posx[1] = linePos[3, 0]; posy[1] = linePos[3, 1];
                        posx[2] = linePos[6, 0]; posy[2] = linePos[6, 1];
                        posx[3] = linePos[9, 0]; posy[3] = linePos[9, 1];
                        posx[4] = linePos[1, 0]; posy[4] = linePos[1, 1];
                        posx[5] = linePos[4, 0]; posy[5] = linePos[4, 1];
                        posx[6] = linePos[7, 0]; posy[6] = linePos[7, 1];
                        posx[7] = linePos[10, 0]; posy[7] = linePos[10, 1];
                        posx[8] = linePos[2, 0]; posy[8] = linePos[2, 1];
                        posx[9] = linePos[5, 0]; posy[9] = linePos[5, 1];
                        posx[10] = linePos[8, 0]; posy[10] = linePos[8, 1];
                        posx[11] = linePos[11, 0]; posy[11] = linePos[11, 1];
                        break;
                    case 1:
                        posx[0] = linePos[0, 0] - r * .5; posy[0] = linePos[0, 1];
                        posx[1] = linePos[0, 0] + r * .5; posy[1] = linePos[0, 1];
                        posx[2] = linePos[3, 0] - r * .5; posy[2] = linePos[3, 1];
                        posx[3] = linePos[3, 0] + r * .5; posy[3] = linePos[3, 1];
                        posx[4] = linePos[6, 0] - r * .5; posy[4] = linePos[6, 1];
                        posx[5] = linePos[6, 0] + r * .5; posy[5] = linePos[6, 1];
                        posx[6] = linePos[9, 0] - r * .5; posy[6] = linePos[9, 1];
                        posx[7] = linePos[9, 0] + r * .5; posy[7] = linePos[9, 1];
                        posx[8] = linePos[1, 0] - r * .5; posy[8] = linePos[1, 1];
                        posx[9] = linePos[1, 0] + r * .5; posy[9] = linePos[1, 1];
                        posx[10] = linePos[4, 0] - r * .5; posy[10] = linePos[4, 1];
                        posx[11] = linePos[4, 0] + r * .5; posy[11] = linePos[4, 1];
                        posx[12] = linePos[7, 0] - r * .5; posy[12] = linePos[7, 1];
                        posx[13] = linePos[7, 0] + r * .5; posy[13] = linePos[7, 1];
                        posx[14] = linePos[10, 0] - r * .5; posy[14] = linePos[10, 1];
                        posx[15] = linePos[10, 0] + r * .5; posy[15] = linePos[10, 1];
                        posx[16] = linePos[2, 0] - r * .5; posy[16] = linePos[2, 1];
                        posx[17] = linePos[2, 0] + r * .5; posy[17] = linePos[2, 1];
                        posx[18] = linePos[5, 0] - r * .5; posy[18] = linePos[5, 1];
                        posx[19] = linePos[5, 0] + r * .5; posy[19] = linePos[5, 1];
                        posx[20] = linePos[8, 0] - r * .5; posy[20] = linePos[8, 1];
                        posx[21] = linePos[8, 0] + r * .5; posy[21] = linePos[8, 1];
                        posx[22] = linePos[11, 0] - r * .5; posy[22] = linePos[11, 1];
                        posx[23] = linePos[11, 0] + r * .5; posy[23] = linePos[11, 1];
                        break;
                    case 2:
                        posx[0] = linePos[0, 0] - r * .5; posy[0] = linePos[0, 1] + x;
                        posx[1] = linePos[0, 0] + r * .5; posy[1] = linePos[0, 1] + x;
                        posx[2] = linePos[0, 0]; posy[2] = linePos[0, 1] - x;
                        posx[3] = linePos[3, 0] - r * .5; posy[3] = linePos[3, 1] + x;
                        posx[4] = linePos[3, 0] + r * .5; posy[4] = linePos[3, 1] + x;
                        posx[5] = linePos[3, 0]; posy[5] = linePos[3, 1] - x;
                        posx[6] = linePos[6, 0] - r * .5; posy[6] = linePos[6, 1] + x;
                        posx[7] = linePos[6, 0] + r * .5; posy[7] = linePos[6, 1] + x;
                        posx[8] = linePos[6, 0]; posy[8] = linePos[6, 1] - x;
                        posx[9] = linePos[9, 0] - r * .5; posy[9] = linePos[9, 1] + x;
                        posx[10] = linePos[9, 0] + r * .5; posy[10] = linePos[9, 1] + x;
                        posx[11] = linePos[9, 0]; posy[11] = linePos[9, 1] - x;
                        posx[12] = linePos[1, 0] - r * .5; posy[12] = linePos[1, 1] + x;
                        posx[13] = linePos[1, 0] + r * .5; posy[13] = linePos[1, 1] + x;
                        posx[14] = linePos[1, 0]; posy[14] = linePos[1, 1] - x;
                        posx[15] = linePos[4, 0] - r * .5; posy[15] = linePos[4, 1] + x;
                        posx[16] = linePos[4, 0] + r * .5; posy[16] = linePos[4, 1] + x;
                        posx[17] = linePos[4, 0]; posy[17] = linePos[4, 1] - x;
                        posx[18] = linePos[7, 0] - r * .5; posy[18] = linePos[7, 1] + x;
                        posx[19] = linePos[7, 0] + r * .5; posy[19] = linePos[7, 1] + x;
                        posx[20] = linePos[7, 0]; posy[20] = linePos[7, 1] - x;
                        posx[21] = linePos[10, 0] - r * .5; posy[21] = linePos[10, 1] + x;
                        posx[22] = linePos[10, 0] + r * .5; posy[22] = linePos[10, 1] + x;
                        posx[23] = linePos[10, 0]; posy[23] = linePos[10, 1] - x;
                        posx[24] = linePos[2, 0] - r * .5; posy[24] = linePos[2, 1] + x;
                        posx[25] = linePos[2, 0] + r * .5; posy[25] = linePos[2, 1] + x;
                        posx[26] = linePos[2, 0]; posy[26] = linePos[2, 1] - x;
                        posx[27] = linePos[5, 0] - r * .5; posy[27] = linePos[5, 1] + x;
                        posx[28] = linePos[5, 0] + r * .5; posy[28] = linePos[5, 1] + x;
                        posx[29] = linePos[5, 0]; posy[29] = linePos[5, 1] - x;
                        posx[30] = linePos[8, 0] - r * .5; posy[30] = linePos[8, 1] + x;
                        posx[31] = linePos[8, 0] + r * .5; posy[31] = linePos[8, 1] + x;
                        posx[32] = linePos[8, 0]; posy[32] = linePos[8, 1] - x;
                        posx[33] = linePos[11, 0] - r * .5; posy[33] = linePos[11, 1] + x;
                        posx[34] = linePos[11, 0] + r * .5; posy[34] = linePos[11, 1] + x;
                        posx[35] = linePos[11, 0]; posy[35] = linePos[11, 1] - x;
                        break;
                    case 3:
                        posx[0] = linePos[0, 0] - r * .5; posy[0] = linePos[0, 1] + r * .5;
                        posx[1] = linePos[0, 0] + r * .5; posy[1] = linePos[0, 1] + r * .5;
                        posx[2] = linePos[0, 0] - r * .5; posy[2] = linePos[0, 1] - r * .5;
                        posx[3] = linePos[0, 0] + r * .5; posy[3] = linePos[0, 1] - r * .5;
                        posx[4] = linePos[3, 0] - r * .5; posy[4] = linePos[3, 1] + r * .5;
                        posx[5] = linePos[3, 0] + r * .5; posy[5] = linePos[3, 1] + r * .5;
                        posx[6] = linePos[3, 0] - r * .5; posy[6] = linePos[3, 1] - r * .5;
                        posx[7] = linePos[3, 0] + r * .5; posy[7] = linePos[3, 1] - r * .5;
                        posx[8] = linePos[6, 0] - r * .5; posy[8] = linePos[6, 1] + r * .5;
                        posx[9] = linePos[6, 0] + r * .5; posy[9] = linePos[6, 1] + r * .5;
                        posx[10] = linePos[6, 0] - r * .5; posy[10] = linePos[6, 1] - r * .5;
                        posx[11] = linePos[6, 0] + r * .5; posy[11] = linePos[6, 1] - r * .5;
                        posx[12] = linePos[9, 0] - r * .5; posy[12] = linePos[9, 1] + r * .5;
                        posx[13] = linePos[9, 0] + r * .5; posy[13] = linePos[9, 1] + r * .5;
                        posx[14] = linePos[9, 0] - r * .5; posy[14] = linePos[9, 1] - r * .5;
                        posx[15] = linePos[9, 0] + r * .5; posy[15] = linePos[9, 1] - r * .5;
                        posx[16] = linePos[1, 0] - r * .5; posy[16] = linePos[1, 1] + r * .5;
                        posx[17] = linePos[1, 0] + r * .5; posy[17] = linePos[1, 1] + r * .5;
                        posx[18] = linePos[1, 0] - r * .5; posy[18] = linePos[1, 1] - r * .5;
                        posx[19] = linePos[1, 0] + r * .5; posy[19] = linePos[1, 1] - r * .5;
                        posx[20] = linePos[4, 0] - r * .5; posy[20] = linePos[4, 1] + r * .5;
                        posx[21] = linePos[4, 0] + r * .5; posy[21] = linePos[4, 1] + r * .5;
                        posx[22] = linePos[4, 0] - r * .5; posy[22] = linePos[4, 1] - r * .5;
                        posx[23] = linePos[4, 0] + r * .5; posy[23] = linePos[4, 1] - r * .5;
                        posx[24] = linePos[7, 0] - r * .5; posy[24] = linePos[7, 1] + r * .5;
                        posx[25] = linePos[7, 0] + r * .5; posy[25] = linePos[7, 1] + r * .5;
                        posx[26] = linePos[7, 0] - r * .5; posy[26] = linePos[7, 1] - r * .5;
                        posx[27] = linePos[7, 0] + r * .5; posy[27] = linePos[7, 1] - r * .5;
                        posx[28] = linePos[10, 0] - r * .5; posy[28] = linePos[10, 1] + r * .5;
                        posx[29] = linePos[10, 0] + r * .5; posy[29] = linePos[10, 1] + r * .5;
                        posx[30] = linePos[10, 0] - r * .5; posy[30] = linePos[10, 1] - r * .5;
                        posx[31] = linePos[10, 0] + r * .5; posy[31] = linePos[10, 1] - r * .5;
                        posx[32] = linePos[2, 0] - r * .5; posy[32] = linePos[2, 1] + r * .5;
                        posx[33] = linePos[2, 0] + r * .5; posy[33] = linePos[2, 1] + r * .5;
                        posx[34] = linePos[2, 0] - r * .5; posy[34] = linePos[2, 1] - r * .5;
                        posx[35] = linePos[2, 0] + r * .5; posy[35] = linePos[2, 1] - r * .5;
                        posx[36] = linePos[5, 0] - r * .5; posy[36] = linePos[5, 1] + r * .5;
                        posx[37] = linePos[5, 0] + r * .5; posy[37] = linePos[5, 1] + r * .5;
                        posx[38] = linePos[5, 0] - r * .5; posy[38] = linePos[5, 1] - r * .5;
                        posx[39] = linePos[5, 0] + r * .5; posy[39] = linePos[5, 1] - r * .5;
                        posx[40] = linePos[8, 0] - r * .5; posy[40] = linePos[8, 1] + r * .5;
                        posx[41] = linePos[8, 0] + r * .5; posy[41] = linePos[8, 1] + r * .5;
                        posx[42] = linePos[8, 0] - r * .5; posy[42] = linePos[8, 1] - r * .5;
                        posx[43] = linePos[8, 0] + r * .5; posy[43] = linePos[8, 1] - r * .5;
                        posx[44] = linePos[11, 0] - r * .5; posy[44] = linePos[11, 1] + r * .5;
                        posx[45] = linePos[11, 0] + r * .5; posy[45] = linePos[11, 1] + r * .5;
                        posx[46] = linePos[11, 0] - r * .5; posy[46] = linePos[11, 1] - r * .5;
                        posx[47] = linePos[11, 0] + r * .5; posy[47] = linePos[11, 1] - r * .5;
                        break;
                }
            }
            conductorNo = 3 * a * (boundle + 1);
            //End of Pozition

            //GMD
            double dm = 1;
            for (int i = 0; i < conductorNo / 3; i++)
                for (int j = conductorNo / 3; j < conductorNo; j++)
                    dm = dm * Math.Pow(Math.Sqrt(Math.Pow(posx[i] - posx[j], 2) + Math.Pow(posy[i] - posy[j], 2)), Math.Pow(Math.Pow(conductorNo, 2) * Math.Pow(3, -1), -1));
            for (int i = conductorNo / 3; i < 2 * conductorNo / 3; i++)
                for (int j = 2 * conductorNo / 3; j < conductorNo; j++)
                    dm = dm * Math.Pow(Math.Sqrt(Math.Pow(posx[i] - posx[j], 2) + Math.Pow(posy[i] - posy[j], 2)), Math.Pow(Math.Pow(conductorNo, 2) * Math.Pow(3, -1), -1));
            //End of GMD

			//GMRds
            double ds = 1;
            for (int i = 0; i < conductorNo / 3.0; i++)
                for (int j = 0; j < conductorNo / 3.0; j++)
                    if (i == j)
                        ds = ds * Math.Pow(c_Stdia, Math.Pow(conductorNo, -2) * 3);
                    else
                        ds = ds * Math.Pow(Math.Sqrt(Math.Pow(posx[i] - posx[j], 2) + Math.Pow(posy[i] - posy[j], 2)), Math.Pow(conductorNo, -2) * 3);
            for (int i = conductorNo / 3; i < 2 * conductorNo / 3.0; i++)
                for (int j = conductorNo / 3; j < 2 * conductorNo / 3.0; j++)
                    if (i == j)
                        ds = ds * Math.Pow(c_Stdia, Math.Pow(conductorNo, -2) * 3);
                    else
                        ds = ds * Math.Pow(Math.Sqrt(Math.Pow(posx[i] - posx[j], 2) + Math.Pow(posy[i] - posy[j], 2)), Math.Pow(conductorNo, -2) * 3);
            for (int i = 2 * conductorNo / 3; i < conductorNo; i++)
                for (int j = 2 * conductorNo / 3; j < conductorNo; j++)
                    if (i == j)
                        ds = ds * Math.Pow(c_Stdia, Math.Pow(conductorNo, -2) * 3);
                    else
                        ds = ds * Math.Pow(Math.Sqrt(Math.Pow(posx[i] - posx[j], 2) + Math.Pow(posy[i] - posy[j], 2)), Math.Pow(conductorNo, -2) * 3);
            //End of GMRds

            //GMRdsc
            double dsc = 1;
            for (int i = 0; i < conductorNo / 3.0; i++)
                for (int j = 0; j < conductorNo / 3.0; j++)
                    if (i == j)
                        dsc = dsc * Math.Pow(c_Stdia, Math.Pow(conductorNo, -2) * 3);
                    else
                        dsc = dsc * Math.Pow(Math.Sqrt(Math.Pow(posx[i] - posx[j], 2) + Math.Pow(posy[i] - posy[j], 2)), Math.Pow(conductorNo, -2) * 3);
            for (int i = conductorNo / 3; i < 2 * conductorNo / 3.0; i++)
                for (int j = conductorNo / 3; j < 2 * conductorNo / 3.0; j++)
                    if (i == j)
                        dsc = dsc * Math.Pow(c_Stdia, Math.Pow(conductorNo, -2) * 3);
                    else
                        dsc = dsc * Math.Pow(Math.Sqrt(Math.Pow(posx[i] - posx[j], 2) + Math.Pow(posy[i] - posy[j], 2)), Math.Pow(conductorNo, -2) * 3);
            for (int i = 2 * conductorNo / 3; i < conductorNo; i++)
                for (int j = 2 * conductorNo / 3; j < conductorNo; j++)
                    if (i == j)
                        dsc = dsc * Math.Pow(c_Stdia, Math.Pow(conductorNo, -2) * 3);
                    else
                        dsc = dsc * Math.Pow(Math.Sqrt(Math.Pow(posx[i] - posx[j], 2) + Math.Pow(posy[i] - posy[j], 2)), Math.Pow(conductorNo, -2) * 3);
            //End of GMRdsc

            //GMDh
            double hm = 1;
            for (int i = 0; i < conductorNo / 3; i++)
                for (int j = conductorNo / 3; j < conductorNo; j++)
                    hm = hm * Math.Pow(Math.Sqrt(Math.Pow(posx[i] - posx[j], 2) + Math.Pow(posy[i] + posy[j], 2)), Math.Pow(Math.Pow(conductorNo, 2) * Math.Pow(3, -1), -1));
            for (int i = conductorNo / 3; i < 2 * conductorNo / 3; i++)
                for (int j = 2 * conductorNo / 3; j < conductorNo; j++)
                    hm = hm * Math.Pow(Math.Sqrt(Math.Pow(posx[i] - posx[j], 2) + Math.Pow(posy[i] + posy[j], 2)), Math.Pow(Math.Pow(conductorNo, 2) * Math.Pow(3, -1), -1));
			//End of GMDh

            //GMRh
            double hs = 1;
            for (int i = 0; i < conductorNo / 3; i++)
                for (int j = 0; j < conductorNo / 3; j++)
                    hs = hs * Math.Pow(Math.Sqrt(Math.Pow(posx[i] - posx[j], 2) + Math.Pow(posy[i] + posy[j], 2)), Math.Pow(conductorNo, -2) * 3);
            for (int i = conductorNo / 3; i < 2 * conductorNo / 3; i++)
                for (int j = conductorNo / 3; j < 2 * conductorNo / 3; j++)
                    hs = hs * Math.Pow(Math.Sqrt(Math.Pow(posx[i] - posx[j], 2) + Math.Pow(posy[i] + posy[j], 2)), Math.Pow(conductorNo, -2) * 3);
            for (int i = 2 * conductorNo / 3; i < conductorNo; i++)
                for (int j = 2 * conductorNo / 3; j < conductorNo; j++)
                    hs = hs * Math.Pow(Math.Sqrt(Math.Pow(posx[i] - posx[j], 2) + Math.Pow(posy[i] + posy[j], 2)), Math.Pow(conductorNo, -2) * 3);
			//End of GMRh

            double l = 2 * Math.Pow(10, -7) * Math.Log(dm * Math.Pow(ds, -1));
            double c = 2 * 3.14159265 * 8.85 * Math.Pow(10, -12) * Math.Pow(Math.Log(dm * hs * Math.Pow(dsc * hm, -1)), -1);
            double xl = 2000 * 3.14159265 * frequency * l;
            double yc = 2000 * 3.14159265 * frequency * c;

            RXYZ[0] = res / Convert.ToDouble(boundle + 1);
            RXYZ[1] = xl;
            RXYZ[2] = yc;
            RXYZ[3] = Math.Sqrt((Math.Sqrt(Math.Pow(RXYZ[0], 2) + Math.Pow(RXYZ[1], 2))) / RXYZ[2]);

            return RXYZ;
        }

        static public double[,] power(double R, double X, double Y, double PMax, double Cost, double Vs, double Length, double pStep, double phase)
        {
            int resultIndex = Convert.ToInt16(PMax / pStep) + 1;
            double[,] result = new double[resultIndex, 8];
            double P;
            double SizZ, TetZ, ImgZ, RelZ;
            double SizZc, TetZc, ImgZc, RelZc;
            double SizDl, TetDl, ImgDl, RelDl;
            double SizSinhDl, TetSinhDl, ImgSinhDl, RelSinhDl;
            double SizCoshDl, TetCoshDl, ImgCoshDl, RelCoshDl;
            double pr, qr;
            double Vr1, Vrn;
            double SizIr, TetIr;
            double SizVs2, TetVs2, ImgVs2, RelVs2;
            double SizVs3, TetVs3, ImgVs3, RelVs3;
            double SizIs2, TetIs2, ImgIs2, RelIs2;
            double SizIs3, TetIs3, ImgIs3, RelIs3;
            double SizS1, TetS1, ImgS1, RelS1;
            double SizS2, TetS2, ImgS2, RelS2;
            double SizA, TetA, ImgA, RelA;
            double SizB, TetB, ImgB, RelB;
            double SizC, TetC, ImgC, RelC;
            double AA, BB, CC, phi;
            double Reg;

            phi = phase * Math.Acos(Cost);

            SizZ = Math.Sqrt(Math.Pow(R, 2) + Math.Pow(X, 2));
            TetZ = Math.Atan(X / R);
            RelZ = SizZ * Math.Cos(TetZ);
            ImgZ = SizZ * Math.Sin(TetZ);

            SizZc = Math.Sqrt(SizZ / Y);
            TetZc = (TetZ - .5 * Math.PI) * .5;
            RelZc = SizZc * Math.Cos(TetZc);
            ImgZc = SizZc * Math.Sin(TetZc);

            SizDl = Length * Math.Sqrt(SizZ * Y);
            TetDl = .5 * (TetZ + .5 * Math.PI);
            RelDl = SizDl * Math.Cos(TetDl);
            ImgDl = SizDl * Math.Sin(TetDl);

            RelSinhDl = Math.Sinh(RelDl) * Math.Cos(ImgDl);
            ImgSinhDl = Math.Cosh(RelDl) * Math.Sin(ImgDl);
            SizSinhDl = Math.Sqrt(Math.Pow(RelSinhDl, 2) + Math.Pow(ImgSinhDl, 2));
            TetSinhDl = Math.Atan(ImgSinhDl / RelSinhDl);
            if (RelSinhDl < 0) TetSinhDl = TetSinhDl + Math.PI;

            RelCoshDl = Math.Cosh(RelDl) * Math.Cos(ImgDl);
            ImgCoshDl = Math.Sinh(RelDl) * Math.Sin(ImgDl);
            SizCoshDl = Math.Sqrt(Math.Pow(RelCoshDl, 2) + Math.Pow(ImgCoshDl, 2));
            TetCoshDl = Math.Atan(ImgCoshDl / RelCoshDl);
            if (RelCoshDl < 0) TetCoshDl = TetCoshDl + Math.PI;

            RelA = RelCoshDl;
            ImgA = ImgCoshDl;
            SizA = SizCoshDl;
            TetA = TetCoshDl;

            SizB = SizSinhDl * SizZc;
            TetB = TetSinhDl + TetZc;
            RelB = SizB * Math.Cos(TetB);
            ImgB = SizB * Math.Sin(TetB);

            SizC = SizSinhDl / SizZc;
            TetC = TetSinhDl - TetZc;
            RelC = SizC * Math.Cos(TetC);
            ImgC = SizC * Math.Sin(TetC);

            Vrn = Vs / SizCoshDl;
            for (P = pStep; P <= PMax; P = P + pStep)
            {
                pr = P / 3.0;
                qr = pr * Math.Tan(phi);

                AA = Math.Pow(SizA / Vs, 2);
                BB = 2 * SizA * SizB / Math.Pow(Vs, 2) * (pr * Math.Cos(TetB - TetA) + qr * Math.Sin(TetB - TetA)) - 1;
                CC = Math.Pow(SizB / Vs, 2) * (pr * pr + qr * qr);

                Vr1 = Math.Sqrt(1.0 / 2.0 / AA * (-BB + Math.Sqrt(BB * BB - 4.0 * AA * CC)));

                SizIr = P / (3.0 * Cost * Vr1);
                TetIr = -phi;

                SizVs2 = SizCoshDl * Vr1;
                TetVs2 = TetCoshDl;
                RelVs2 = SizVs2 * Math.Cos(TetVs2);
                ImgVs2 = SizVs2 * Math.Sin(TetVs2);

                SizVs3 = SizSinhDl * SizIr * SizZc;
                TetVs3 = TetSinhDl + TetIr + TetZc;
                RelVs3 = SizVs3 * Math.Cos(TetVs3);
                ImgVs3 = SizVs3 * Math.Sin(TetVs3);

                RelVs2 = RelVs2 + RelVs3;
                ImgVs2 = ImgVs2 + ImgVs3;
                SizVs2 = Math.Sqrt(Math.Pow(RelVs2, 2) + Math.Pow(ImgVs2, 2));
                TetVs2 = Math.Atan(ImgVs2 / RelVs2);
                if (RelVs2 < 0) TetVs2 = TetVs2 + Math.PI;

                SizIs2 = SizCoshDl * SizIr;
                TetIs2 = TetCoshDl + TetIr;
                RelIs2 = SizIs2 * Math.Cos(TetIs2);
                ImgIs2 = SizIs2 * Math.Sin(TetIs2);

                SizIs3 = SizSinhDl * Vr1 / SizZc;
                TetIs3 = TetSinhDl - TetZc;
                RelIs3 = SizIs3 * Math.Cos(TetIs3);
                ImgIs3 = SizIs3 * Math.Sin(TetIs3);

                RelIs2 = RelIs2 + RelIs3;
                ImgIs2 = ImgIs2 + ImgIs3;
                SizIs2 = Math.Sqrt(Math.Pow(RelIs2, 2) + Math.Pow(ImgIs2, 2));
                TetIs2 = Math.Atan(ImgIs2 / RelIs2);
                if (RelIs2 < 0) TetIs2 = TetIs2 + Math.PI;

                SizS1 = 3 * SizIs2 * SizVs2;
                TetS1 = (TetVs2 - TetIs2);
                RelS1 = SizS1 * Math.Cos(TetS1);
                ImgS1 = SizS1 * Math.Sin(TetS1);

                SizS2 = 3 * SizIr * Vr1;
                TetS2 = -TetIr;
                RelS2 = SizS2 * Math.Cos(TetS2);
                ImgS2 = SizS2 * Math.Sin(TetS2);

                Reg = 100 * (Vrn - Vr1) / Vr1;

                result[Convert.ToInt16(P / pStep - 1), 0] = SizVs2 * Math.Sqrt(3);
                result[Convert.ToInt16(P / pStep - 1), 1] = RelS1;
                result[Convert.ToInt16(P / pStep - 1), 2] = Vr1 * Math.Sqrt(3);
                result[Convert.ToInt16(P / pStep - 1), 3] = P;
                result[Convert.ToInt16(P / pStep - 1), 4] = SizIr;
                result[Convert.ToInt16(P / pStep - 1), 5] = RelS1 - RelS2;
                result[Convert.ToInt16(P / pStep - 1), 6] = ImgS1 - ImgS2;
                result[Convert.ToInt16(P / pStep - 1), 7] = Reg;
            }

            return result;
        }

        static public double[] coronaLoss(double frequency, double pressure, double temperature, double conductorDiameter, double GMD, double Vline, double Req,double mm0,double Rain,double[] eMatrix)
        {
            double delta = 3.92 * pressure / (273 + temperature);
            double Vph = Vline / Math.Sqrt(3);
            double Vcr = 21.1 * mm0 * Math.Pow(delta, .6667) * Req * Math.Log(GMD / conductorDiameter);
            double Vcrv = 21.1 * mm0 * delta * Req * (1 + .3 / Math.Sqrt(delta * Req)) * Math.Log(GMD / conductorDiameter);
            double ratio = Math.Round(Vph / Vcr, 2);
            double F = 1.821 * Math.Pow(10, 10) * Math.Exp(-Math.Pow((ratio - 2.06) / 0.01166, 2)) + 3.701 * Math.Exp(-Math.Pow((ratio - 2.063) / 0.1222, 2)) +
                3.58 * Math.Exp(-Math.Pow((ratio - 1.913) / 0.1592, 2)) + 2.509 * Math.Exp(-Math.Pow((ratio - 1.712) / 0.1748, 2)) +
                1.89 * Math.Exp(-Math.Pow((ratio - 2.456) / 0.716, 2));
            double Pr, j, Ei = eMatrix[0];
            double[] coronaLoss = new double[4];

            if (ratio > 1.8)
                coronaLoss[0] = 3 * 244 * ((frequency + 25) / delta) * Math.Sqrt(conductorDiameter / GMD) * Math.Pow((Vph - Vcr), 2) * Math.Pow(10, -5);
            else
                coronaLoss[0] = 3 * 21.1 * frequency * F * Math.Pow(Vph / Math.Log10(GMD / conductorDiameter), 2) * Math.Pow(10, -6);

            if (Vline < 400)
                j = 4.375 * Math.Pow(10, -6);
            else
                j = 3.325 * Math.Pow(10, -6);

            for (int i = 0; i < eMatrix.Length; i++)
                if (eMatrix[i] > Ei)
                    Ei = eMatrix[i];


            Pr = coronaLoss[0] + Vph * j * Math.Pow(conductorDiameter, 2) * Math.Log(1 + 10 * Rain) * Math.Pow(Ei, 5);

            coronaLoss[1] = Vcr;
            coronaLoss[2] = Vcrv;
            coronaLoss[3] = Pr;

            return coronaLoss;
        }

    }
}
