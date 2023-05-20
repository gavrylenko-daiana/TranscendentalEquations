using Sprache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.Services;
using TranscendentalEquations.Helper;
using TranscendentalEquations.Model;

namespace TranscendentalEquations.TranscendentalMethods;

public class MyBisection : TranscendentalEquation
{
    private StringBuilder intermediateData;

    public MyBisection(StringBuilder intermediateData)
    {
        this.intermediateData = intermediateData;
    }

    public override double Solve()
    {
        FindFunction findFunction = new FindFunction();

        double x = 1;
        bool isComplete = false;

        double fa = findFunction.f(A, Equation);
        double fb = findFunction.f(B, Equation);
        double fx = findFunction.f(x, Equation);

        if (fa * fb < 0)
        {
            for (int i = 0; i < MaxIterations; i++)
            {
                x = (A + B) / 2;
                fx = findFunction.f(x, Equation);

                intermediateData.AppendLine($"Iteration: {i + 1}");
                intermediateData.AppendLine($"x = {Math.Round(x, 4)}");
                intermediateData.AppendLine($"f(x) = {Math.Round(fx, 4)}");
                intermediateData.AppendLine();

                if (Math.Abs(fx) < Tolerance)
                {
                    isComplete = true;
                    fx = findFunction.f(x, Equation);
                    break;
                }
                if (fa * fx < 0)
                {
                    B = x;
                }
                else
                {
                    A = x;
                }
            }

            if (!isComplete)
            {
                Tolerance = Tolerance.GetTolerance(fx);
                Tolerance = Tolerance == -0 ? 0 : Tolerance;
            }
        }
        else
        {
            return double.NaN;
        }

        return Math.Round(x, 4);
    }
}
