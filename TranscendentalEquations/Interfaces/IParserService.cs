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
        string ReplaceTriginometry(string input);

        string ReplacePow(string input);

        string ReplaceAbsolute(string input);

        string ReplaceConstants(string input);

        double GetResultValue(string input);
    }
}
