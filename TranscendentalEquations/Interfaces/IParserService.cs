using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TranscendentalEquations.Interfaces;

public interface IParserService
{
    double GetValueFromEquation(string input);
    string ReplaceConstants(string input);
}
