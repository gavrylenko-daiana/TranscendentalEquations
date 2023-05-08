using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.Model;
using TranscendentalEquations.Services;

namespace TranscendentalEquations.TranscendentalMethods
{
    public class Newtons
    {
        private double f(double x, string equation)
        {
            EquationService equationService = new EquationService();
            double result = equationService.GetValueFromEquation(x, equation);

            return result;
        }

        private double df(double x, string equation)
        {
            return (1 - x) * Math.Exp(-x);
        }

        public void NewtonsMethod(string equation)
        {
            double x0 = 1, x1;
            double tolerance = 1e-6;
            int maxIterations = 100;
            int i;

            for (i = 0; i < maxIterations; i++)
            {
                x1 = x0 - f(x0, equation) / df(x0, equation);
                if (Math.Abs(x1 - x0) < tolerance)
                    break;
                x0 = x1;
            }
        }
    }
}
