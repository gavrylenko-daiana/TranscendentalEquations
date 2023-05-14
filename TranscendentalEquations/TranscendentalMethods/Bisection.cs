using Sprache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.Services;
using TranscendentalEquations.Helper;

namespace TranscendentalEquations.TranscendentalMethods
{
    public class Bisection : FindFunction
    {
        public double BisectionMethod(string equation)
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
                    tolerance = tolerance.GetTolerance(fx);
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
