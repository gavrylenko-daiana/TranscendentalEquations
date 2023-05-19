using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.Services;

namespace TranscendentalEquations.TranscendentalMethods;

public class MySecant : FindFunction
{
    private StringBuilder intermediateData;

    public MySecant(StringBuilder intermediateData)
    {
        this.intermediateData = intermediateData;
    }

    public double SecantMethod(string equation, int maxIterations, double tolerance, double x0, double x1)
    {
        double x2 = 0;

        double fx0 = f(x0, equation);
        double fx1 = f(x1, equation);

        for (int i = 0; i < maxIterations; i++)
        {
            if (Math.Abs(fx1) < tolerance)
            {
                return Math.Round(x1, 4);
            }

            x2 = x1 - fx1 * (x1 - x0) / (fx1 - fx0);
            x0 = x1;
            x1 = x2;
            fx0 = fx1;
            fx1 = f(x2, equation);

            intermediateData.AppendLine($"Iteration: {i + 1}");
            intermediateData.AppendLine($"x{i} = {Math.Round(x0, 4)}");
            intermediateData.AppendLine($"x{i + 1} = {Math.Round(x1, 4)}");
            intermediateData.AppendLine($"f(x{i}) = {Math.Round(fx0, 4)}");
            intermediateData.AppendLine($"f(x{i + 1}) = {Math.Round(fx1, 4)}");
            intermediateData.AppendLine();
        }

        return fx1;
    }
}

