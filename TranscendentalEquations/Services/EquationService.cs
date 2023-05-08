using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.Interfaces;

namespace TranscendentalEquations.Services
{
    public class EquationService : IEquationManager
    {
        public double GetValueFromEquation(double x, string input)
        {
            // ParserService ParserService = new ParserService();
            double result = 0;
            bool isSolved = true;

            while (isSolved)
            {
                if (input.Contains("pi") || input.Contains("e"))
                {
                    input = ParserService.ReplaceConstants(input);
                }
                else if (input.Contains("^"))
                {
                    input = ParserService.ReplacePow(x, input);
                }
                else if (input.Contains("sin") || input.Contains("cos") || input.Contains("tg") || input.Contains("ctg"))
                {
                    input = ParserService.ReplaceTriginometry(x, input);
                }
                else if (input.Contains("|"))
                {
                    input = ParserService.ReplaceAbsolute(x, input);
                }
                else
                { 
                    isSolved = false;
                    result = ParserService.GetResultValue(x, input);
                }
            }

            return result;
        }
    }
}
