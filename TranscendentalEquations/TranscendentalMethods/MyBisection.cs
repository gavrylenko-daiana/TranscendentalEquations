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
    public class MyBisection : FindFunction
    {
        private StringBuilder intermediateData;

        public MyBisection(StringBuilder intermediateData)
        {
            this.intermediateData = intermediateData;
        }

        public double BisectionMethod(string equation, int maxIterations, double tolerance, double a, double b)
        {
            double x = 1;
            bool isComplete = false;

            double fa = f(a, equation);
            double fb = f(b, equation);
            double fx = f(x, equation);

            if (fa * fb < 0)
            {
                for (int i = 0; i < maxIterations; i++)
                {
                    x = (a + b) / 2;
                    fx = f(x, equation);

                    intermediateData.AppendLine($"Iteration: {i + 1}");
                    intermediateData.AppendLine($"x = {Math.Round(x, 4)}");
                    intermediateData.AppendLine($"f(x) = {Math.Round(fx, 4)}");
                    intermediateData.AppendLine();

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
                    tolerance = tolerance == -0 ? 0 : tolerance;
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
