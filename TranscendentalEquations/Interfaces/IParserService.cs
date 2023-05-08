using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TranscendentalEquations.Interfaces
{
    public interface IParserService
    {
        string ReplaceTriginometry(double x, string input);

        string ReplacePow(double x, string input);

        string ReplaceAbsolute(double x, string input);

        string ReplaceConstants(string input);

        double GetResultValue(double x, string input);
    }
}
