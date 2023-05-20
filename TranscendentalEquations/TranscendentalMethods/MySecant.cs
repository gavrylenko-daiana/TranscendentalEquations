using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.Model;
using TranscendentalEquations.Services;

namespace TranscendentalEquations.TranscendentalMethods;

public class MySecant : TranscendentalEquation
{
    private StringBuilder intermediateData;

    public MySecant(StringBuilder intermediateData)
    {
        this.intermediateData = intermediateData;
    }

    public override double Solve()
    {
        FindFunction findFunction = new FindFunction();

        double x2 = 0;
        double fx0 = findFunction.f(A, Equation);
        double fx1 = findFunction.f(B, Equation);

        for (int i = 0; i < MaxIterations; i++)
        {
            if (Math.Abs(fx1) < Tolerance)
            {
                return Math.Round(B, 4);
            }

            x2 = B - fx1 * (B - A) / (fx1 - fx0);
            A = B;
            B = x2;
            fx0 = fx1;
            fx1 = findFunction.f(x2, Equation);

            intermediateData.AppendLine($"Iteration: {i + 1}");
            intermediateData.AppendLine($"x0 = {Math.Round(A, 4)}");
            intermediateData.AppendLine($"x1 = {Math.Round(B, 4)}");
            intermediateData.AppendLine($"f(x0) = {Math.Round(fx0, 4)}");
            intermediateData.AppendLine($"f(x1) = {Math.Round(fx1, 4)}");
            intermediateData.AppendLine();
        }

        return fx1;
    }
}

