using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.Model;
using TranscendentalEquations.Services;

namespace TranscendentalEquations.TranscendentalMethods
{
    public class Secant
    {
        private static double f(double x, string equation)
        {
            ParserService parserService = new ParserService();
            double result = parserService.GetValueFromEquation(equation);

            return result;
        }

        public static double SecantMethod(string equation)
        {
            double x0 = 1, x1 = 2, x2 = 0;
            double tolerance = 1e-6;
            int maxIterations = 100;

            for (int i = 0; i < maxIterations; i++)
            {
                x2 = x1 - f(x1, equation) * (x1 - x0) / (f(x1, equation) - f(x0, equation));
                if (Math.Abs(x2 - x1) < tolerance)
                    break;
                x0 = x1;
                x1 = x2;
            }

            return x2;
        }
    }
}
