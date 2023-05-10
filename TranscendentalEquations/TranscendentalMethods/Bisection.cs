using Sprache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.Services;

namespace TranscendentalEquations.TranscendentalMethods
{
    public class Bisection : FindFunction
    {
        public static double BisectionMethod(string equation)
        {
            double a = 0.8, b = 1.1, x = 1;

            double fa = f(a, equation);
            double fb = f(b, equation);
            double fx = f(x, equation);

            double tolerance = 0.0001;
            int maxIterations = 100;
            bool isComplete = false;

            if (fa * fb < 0)
            {
                for (int i = 0; i < maxIterations; i++)
                {
                    x = (a + b) / 2;
                    fx = f(x, equation);

                    if (Math.Abs(fx) < tolerance)
                    {
                        isComplete = true;
                        fx = f(x, equation);
                        break;
                    }
                    if (fa * fx < 0)
                    {
                        b = x;
                    }
                    else
                    {
                        a = x;
                    }
                }

                if (!isComplete)
                {
                    string fxStr = fx.ToString();
                    int decimalIndex = fxStr.IndexOf(".");

                    if (decimalIndex != -1 && fxStr[decimalIndex + 1] == '0')
                    {
                        fxStr = fxStr.Substring(0, decimalIndex + 1) + '1' + fxStr.Substring(decimalIndex + 2);
                        tolerance = Convert.ToDouble(fxStr);
                    }
                    else
                    {
                        tolerance = Math.Ceiling(fx * 10) / 10;
                    }
                }
            } 
            else
            {
                return double.NaN;
            }

            return Math.Round(x, 4);
        }
    }
}
