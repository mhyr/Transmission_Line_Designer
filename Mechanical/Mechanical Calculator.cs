using System;
using System.Collections.Generic;
using System.Text;

namespace Mechanical
{
    public class MechanicalCalculator
    {
        static public double[,] MechanicalCharacteristic(double Area, double Elasticity, double Span, double WeightUnit, double SafetyFactor, double UTS, double alpha, double[,] mechanicalWeatherCondition,double diameter)
        {
            double Hc=UTS/SafetyFactor;
            double t2, t1, Pw, Wi, Ww, Cd, W, R;
            double a, b;
            double[,] Result = new double[4,5];
                        
            t2 = mechanicalWeatherCondition[4, 0];
                        
            if (diameter >= 15)
                Cd = 1;
            else if (diameter <= 12.5)
                Cd = 1.2;
            else
                Cd = 1.1;

            if (Span < 100)
                R = 1;
            else if (Span > 300)
                R = .6;
            else
                R = (600 - Span) / 500.0;

            for (int i = 0; i < 4; i++)
            {
                Wi = 0.913 * Math.PI * mechanicalWeatherCondition[i, 2] * (mechanicalWeatherCondition[i, 2] + diameter) * Math.Pow(10, -3);

                t1 = mechanicalWeatherCondition[i, 0];

                Pw = Math.Pow(mechanicalWeatherCondition[i, 1], 2) / 16.0;
                Ww = R * Pw * Cd * (diameter + 2 * mechanicalWeatherCondition[i, 2]) * Math.Pow(10, -3);

                W = Math.Sqrt(Math.Pow((Wi + WeightUnit), 2) + Math.Pow(Ww, 2));

                a = Area * Elasticity * Math.Pow(Span * W, 2) / (24 * Math.Pow(Hc, 2)) + Area * alpha * Elasticity * (t2 - t1) - Hc;
                b = Area * Elasticity * Math.Pow(Span * WeightUnit, 2) / 24.0;
                Result[i,0] = Math.Pow(108 * b - 8 * Math.Pow(a, 3) + 12 * Math.Sqrt(81 * Math.Pow(b, 2) - 12 * b * Math.Pow(a, 3)), 1 / 3.0) / 6.0 + 2 * Math.Pow(a, 2) / (3 * Math.Pow(108 * b - 8 * Math.Pow(a, 3) + 12 * Math.Sqrt(81 * Math.Pow(b, 2) - 12 * b * Math.Pow(a, 3)), 1 / 3.0)) - a / 3.0;
                Result[i, 1] = UTS / Result[i, 0];
                Result[i, 2] = 100 / Result[i, 1];
                Result[i, 4] = Result[i, 0] / WeightUnit;
                Result[i, 3] = Math.Pow(Span, 2) / (8 * Result[i, 4]);
            }

            return Result;

        }
        
    }
}
