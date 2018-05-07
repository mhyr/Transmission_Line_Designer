using System;
using System.Collections.Generic;
using System.Text;

namespace Noise
{
    public class NoiseCalculator
    {
        static public double[] radioNoise(int circuitNumber, double[] gm, double conductorDiameter, double[,] linePos, int weatherCondition, double Temperature, double Pressure, double windSpeed, double earthResistivity,double radioFrequency)
        {
            double[,] RI=new double[3*circuitNumber,101];
            double[,] phaseRI = new double[3, 101];
            double[] overallRI = new double[101];
            double frequencyAdder;
            double delta;
            double RI0=0;          

            if (weatherCondition == 0)
                RI0 = RI0 - 30;
            else if (weatherCondition == 1)
                RI0 = RI0 - 15;
            else if (weatherCondition == 2)
                RI0 = RI0 - 10;
            delta = 3.92 * Pressure / (273 + Temperature);
            RI0 = RI0 + 40 * (1 - delta) + Math.Pow(windSpeed, .3) + 4.5 * Math.Log10(100 / earthResistivity);

            if (circuitNumber == 1)
            {
                if (linePos[0, 1] == linePos[1, 1] && linePos[2, 1] == linePos[1, 1])
                    frequencyAdder = 23 * Math.Log10(radioFrequency) + 12 * Math.Pow(Math.Log10(radioFrequency), 2) + 5.8;
                else
                    frequencyAdder = 18 * Math.Log10(radioFrequency) + 12 * Math.Pow(Math.Log10(radioFrequency), 2) + 4.3;
            }
            else
                frequencyAdder = 18 * Math.Log10(radioFrequency) + 12 * Math.Pow(Math.Log10(radioFrequency), 2) + 4.3;
            
            for (int i = 0; i <= 3 * circuitNumber - 1; i++)
            {
                for (int j =-50 ; j <= 50; j++)
                {
                    double aerialDistance;
                    aerialDistance = Math.Sqrt(Math.Pow(linePos[i, 0] - j, 2) + Math.Pow(linePos[i, 1], 2));
                    RI[i, j + 50] = 3.5 * gm[i] + 6 * conductorDiameter - 33 * Math.Log10(aerialDistance / 20.0) + RI0 - frequencyAdder;                    
                }
            }
            
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 101; k++)
                {
                    double sumPhase = 0;
                    for (int i = j; i < 3 * circuitNumber; i = i + 3)
                    {
                        sumPhase = sumPhase + Math.Pow(RI[i, k], 2);
                    }
                    phaseRI[j, k] = Math.Sqrt(sumPhase);
                }
            }
            int minPhase = 0;
            int maxPhase = 0;
            for (int i = 1; i < 3; i++)
            {
                if (phaseRI[i, 0] > phaseRI[maxPhase, 0]) maxPhase = i;
                if (phaseRI[i, 0] < phaseRI[minPhase, 0]) minPhase = i;
            }
            int middlePhase = 0;
            for (int i = 0; i < 3; i++)
                if (i != minPhase && i != maxPhase) middlePhase = i;
            if (phaseRI[maxPhase, 0] - phaseRI[middlePhase, 0] >= 3)
            {
                for (int i = 0; i < 101; i++)
                    overallRI[i] = phaseRI[maxPhase, i];
            }
            else
            {
                for (int i = 0; i < 101; i++)
                    overallRI[i] = (phaseRI[maxPhase, i] + phaseRI[middlePhase, i]) / 2.0 + 1.5;
            }

            /*minRI = 50;
            for (int i = 51; i < 101; i++)
            {
                if (overallRI[i] <= overallRI[minRI])
                    minRI = i;
                else
                    break;
            }

            for (int i = minRI; i < 101; i++)
                overallRI[i] = overallRI[minRI] - 1;

            minRI = 50;
            for (int i = 49; i >= 0; i--)
            {
                if (overallRI[i] <= overallRI[minRI])
                    minRI = i;
                else
                    break;
            }

            for (int i = minRI; i >= 0; i--)
                overallRI[i] = overallRI[minRI] - 1;*/

            return overallRI;
        }

        static public double[] TVNoise(double[] RI, int circuitNumber, double bandwidth, double TVFrequency)
        {
            double[] TVI = new double[101];
            double deltaBW = 20 * Math.Log10(Math.Sqrt(bandwidth * 1000 / 5));
            double deltaF = 20 * Math.Log10(TVFrequency);

            for (int i = 0; i < 101; i++)
                TVI[i] = RI[i] - deltaF + bandwidth;

            return TVI;
        }

        static public double[] audibleNoise(double bundleNumber,double conductorDiameter,int circuitNumber,double [] eMatrix,double[,] linePos,double Req,int weatherCondition)
        {
            double[,] AN = new double[3 * circuitNumber, 101];
            double[,] phaseAN = new double[3, 101];
            double[] totalAN = new double[101];
            double Awc = 0;
            double Ec = 0;
            double Kn = 0;
            if (bundleNumber == 1)
                Kn = 7.5;
            else if (bundleNumber == 2)
                Kn = 2.6;
            if (bundleNumber <= 8)
                Ec = 24.4 / Math.Pow(conductorDiameter, .24);
            else
                Ec = 24.4 / Math.Pow(conductorDiameter, .24) - .25 * (bundleNumber - 8);
            for (int i = 0; i < 3 * circuitNumber; i++)
            {
                if (bundleNumber < 3)
                    if (weatherCondition == 2)
                        Awc = 8.2 - 14.2 * Ec / eMatrix[i];
                    else
                        if (weatherCondition == 2)
                            Awc = 10.4 - 14.2 * Ec / eMatrix[i] + 8 * (bundleNumber - 1) * conductorDiameter / (2.0 * Req);
                for (int j = -50; j <= 50; j++)
                {
                    double aerialDistance;
                    aerialDistance = Math.Sqrt(Math.Pow(linePos[i, 0] - j, 2) + Math.Pow(linePos[i, 1], 2));
                    if (bundleNumber < 3)
                        AN[i, j + 50] = 20 * Math.Log10(bundleNumber) + 44 * Math.Log10(conductorDiameter) - 655 / eMatrix[i] - 10 * Math.Log10(aerialDistance) - .02 * aerialDistance + Kn + 75.2 + Awc;
                    else
                        AN[i, j + 50] = 20 * Math.Log10(bundleNumber) + 44 * Math.Log10(conductorDiameter) - 655 / eMatrix[i] - 10 * Math.Log10(aerialDistance) - .02 * aerialDistance + 22.9 * (bundleNumber - 1) * conductorDiameter / (2.0 * Req) + 67.9 + Awc;
                }
            }

            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 101; k++)
                {
                    double sumPhase = 0;
                    for (int i = j; i < 3 * circuitNumber; i = i + 3)
                    {
                        sumPhase = sumPhase + Math.Pow(AN[i, k], 2);
                    }
                    phaseAN[j, k] = Math.Sqrt(sumPhase);
                }
            }

            for (int i = 0; i < 101; i++)
                totalAN[i] = 10 * Math.Log10(Math.Pow(10, phaseAN[0, i] / 10.0) + Math.Pow(10, phaseAN[1, i] / 10.0) + Math.Pow(10, phaseAN[2, i] / 10.0));

            return totalAN;
        }

    }
}
