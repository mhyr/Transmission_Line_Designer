using System;
using System.Collections.Generic;
using System.Text;

namespace Insulation
{
    public class InsulationCalculator
    {
        static public double[] Lightning(double IKL, double Altitude, double towerHeight, double lineVoltage, double insulatorLength, double NSF, double towerFootingResistance, double Tf, double K1)
        {
            double K0 = 1.31, K = .9, C = .3, Zt = 0;
            double M, Pl, Ilt, a, V50, Z, Z0, N, L;
            double[] result = new double[2];

            M = 43 * Math.Sqrt(towerHeight / 25.0) * IKL / 32.5;
            Pl = NSF / M / .5 * 100;
            Pl = Math.Log10(Pl);
            Ilt = -0.05692 * Math.Pow(Pl, 5) + 0.0599 * Math.Pow(Pl, 4) + 0.06161 * Math.Pow(Pl, 3) - 0.1443 * Pl * Pl - 0.3168 * Pl + 2.08;
            Ilt = Math.Pow(10, Ilt);
            if (Tf == 2)
                Zt = (5.5 * towerFootingResistance + 575) / 20.0 + .15 * (towerHeight - 40);
            if (Tf == 4)
                Zt = (7.5 * towerFootingResistance + 385) / 20.0 + .1 * (towerHeight - 40);
            a = 1.1 + (Altitude - 1000) / 10000.0;
            V50 = a / K0 * (Ilt * (K - C) * Zt + Math.Sqrt(2) / Math.Sqrt(3) * lineVoltage);
            Z = (V50 - 80) / .55;
            Z0 = Z / K1;
            N = Math.Ceiling(Z0 / insulatorLength);
            L = (1.115 * Z + 21) / 1000;

            result[0] = L * 1000;
            result[1] = N;
            return result;
        }

        static public double[] SwitchingSurge(double s_Voltage, double s_OverVoltage, double SwitchingPU, double h, double A)
        {
            double[] result = new double[2];
            double Crest = 0.816496580925, Em, CFO, S1, S2, n, RAD;
            A = A * .001;
            h = h * .001;

            int t = 0;
            Em = s_Voltage * s_OverVoltage * Crest * SwitchingPU;
            RAD = 0.997 - 0.106 * A;
            S2 = 1;
            do
            {
                t++;
                S1 = S2;
                n = 1.12 - 0.12 * S1;
                if (S1 < 1) n = 1;
                CFO = Em * 1.03 / .85 / .96 / Math.Pow(RAD, n);
                S2 = 7.3 / ((3830 / CFO) - 1);
            } while (Math.Abs(S1 - S2) > .01 && t < 1500);

            result[0] = S2 * 1000;
            result[1] = Math.Ceiling(S2 * s_OverVoltage / h);
            return result;
        }

        static public double[] Contamination(double s_Voltage, double s_OverVoltage, double contamination, double insulatorcreepage)
        {
            double[] result = new double[2];
            double MinCreepDist, N;
            MinCreepDist = s_Voltage * s_OverVoltage * contamination / Math.Sqrt(3);
            N = Math.Ceiling(MinCreepDist / insulatorcreepage);
            
            result[0] = MinCreepDist;
            result[1] = N;
            return result;
        }

        static public double[] PowerFreq(double s_Voltage, double s_OverVoltage, double contamination, double insulatorcreepage, double A)
        {
            double[] result = new double[2];
            double MinCreepDist, N, RAD, ContaminationFactor;
            double Crest = 0.816496580925, Humidity = 1.04, FaultFactor = 1.3;
            A = A * .001;
            RAD = 0.997 - 0.106 * A;
            ContaminationFactor = contamination / 100 + 1;
            MinCreepDist = s_Voltage * s_OverVoltage * ContaminationFactor * Crest * FaultFactor * Humidity * (1.0 / RAD);
            N = Math.Ceiling(MinCreepDist / insulatorcreepage);

            result[0] = MinCreepDist;
            result[1] = N;
            return result;
        }
    }
}
