using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.Interfaces;

namespace TranscendentalEquations.Services
{
    public class FindFunction : IFindFunction
    {
        public double f(double x, string equation)
        {
            ParserService parserService = new ParserService();
            equation = equation.Replace("x", x.ToString()).Replace(",", ".");
            if (equation.Contains("pi") || equation.Contains('e'))
                equation = parserService.ReplaceConstants(equation);
            double result = parserService.GetValueFromEquation(equation);

            return result;
        }

        public double df(double x, string equation)
        {
            ParserService parserService = new ParserService();
            equation = equation.Replace("x", x.ToString()).Replace(",", ".");
            if (equation.Contains("pi") || equation.Contains('e'))
                equation = parserService.ReplaceConstants(equation);
            double result = parserService.GetValueFromEquation(equation);

            return result;
        }
    }
}
