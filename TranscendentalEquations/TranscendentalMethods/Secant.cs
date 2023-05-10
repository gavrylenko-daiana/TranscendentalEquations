using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.Services;

namespace TranscendentalEquations.TranscendentalMethods
{
    public class Secant : FindFunction
    {
        public static double SecantMethod(string equation)
        {
            double x0 = 0.8;
            double x1 = 1.1;
            double x2 = 0;
            double tolerance = 0.0001;
            int maxIterations = 100;

            double fx0 = f(x0, equation);
            double fx1 = f(x1, equation);

            for (int i = 0; i < maxIterations; i++)
            {
                if (Math.Abs(fx1) < tolerance)
                {
                    return x1;
                }

                x2 = x1 - fx1 * (x1 - x0) / (fx1 - fx0);
                x0 = x1;
                x1 = x2;
                fx0 = fx1;
                fx1 = f(x2, equation);
            }

            return double.NaN;
        }
    }
}
