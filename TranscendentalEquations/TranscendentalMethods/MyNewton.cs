using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.Helper;
using TranscendentalEquations.Model;
using TranscendentalEquations.Services;

namespace TranscendentalEquations.TranscendentalMethods;

public class MyNewton : TranscendentalEquation
{
    private StringBuilder intermediateData;

    public MyNewton(StringBuilder intermediateData)
    {
        this.intermediateData = intermediateData;
    }

    public override double Solve()
    {
        FindFunction findFunction = new FindFunction();

        double x = 1;
        bool isComplete = false;

        double fx = findFunction.f(x, Equation);
        double dfx = findFunction.df(x, Equation);

        for (int i = 0; i < MaxIterations; i++)
        {
            fx = findFunction.f(x, Equation);
            dfx = findFunction.df(x, Equation);

            if (Math.Abs(fx) < Tolerance)
            {
                isComplete = true;
                break;
            }

            x -= fx / dfx;

            intermediateData.AppendLine($"Iteration: {i + 1}");
            intermediateData.AppendLine($"x = {Math.Round(x, 4)}");
            intermediateData.AppendLine($"f(x) = {Math.Round(fx, 4)}");
            intermediateData.AppendLine($"df(x) = {Math.Round(dfx, 4)}");
            intermediateData.AppendLine();
        }

        if (!isComplete)
        {
            Tolerance = Tolerance.GetTolerance(fx);
            Tolerance = Tolerance == -0 ? 0 : Tolerance;
        }

        return Math.Round(x, 4);
    }
}
