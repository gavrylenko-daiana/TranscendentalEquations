using MathNet.Numerics.RootFinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.Model;
using TranscendentalEquations.Validation;

namespace TranscendentalEquations.Helper
{
    public class EquationManager
    {
        public void EquationSolver(StringBuilder data, TranscendentalEquation equation, string filePath)
        {
            FileManager fileManager = new FileManager();
            fileManager.AddMainData(data, equation);

            equation.Result = equation.Solve();

            CorrectionEquation correctionEquation = new CorrectionEquation();
            correctionEquation.CheckResult(equation.Result);

            fileManager.AddResultData(data, equation);

            fileManager.WriteToFile(filePath, data.ToString());
        }
    }
}
