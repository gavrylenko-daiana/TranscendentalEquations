using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranscendentalEquations.Validation;

public class CorrectionEquation
{
    public string RemoveSpaces(string input)
    {
        return input.Replace(" ", "");
    }

    public string ReplaceDotsWithCommas(string input)
    {
        return input.Replace(".", ",");
    }

    public string ToLower(string input)
    {
        return input.ToLower();
    }

    public void CheckResult(double result)
    {
        if (double.IsNaN(result))
            MessageBox.Show("It is not possible to calculate this equation using this method.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

