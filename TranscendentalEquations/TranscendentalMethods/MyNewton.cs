using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.Helper;
using TranscendentalEquations.Services;

namespace TranscendentalEquations.TranscendentalMethods;
public class MyNewton : FindFunction
{
    private StringBuilder intermediateData;

    public MyNewton(StringBuilder intermediateData)
    {
        this.intermediateData = intermediateData;
    }

    public double NewtonsMethod(string equation, int maxIterations, double tolerance)
    {
        double x = 1;
        bool isComplete = false;

        double fx = f(x, equation);
        double dfx = df(x, equation);

        for (int i = 0; i < maxIterations; i++)
        {
            fx = f(x, equation);
            dfx = df(x, equation);

            if (Math.Abs(fx) < tolerance)
            {
                isComplete = true;
                break;
            }

            x -= fx / dfx;

            // Добавление промежуточных данных в intermediateData
            intermediateData.AppendLine($"Iteration: {i + 1}");
            intermediateData.AppendLine($"x = {Math.Round(x, 4)}");
            intermediateData.AppendLine($"f(x) = {Math.Round(fx, 4)}");
            intermediateData.AppendLine($"df(x) = {Math.Round(dfx, 4)}");
            intermediateData.AppendLine();
        }

        if (!isComplete)
        {
            tolerance = tolerance.GetTolerance(fx);
            tolerance = tolerance == -0 ? 0 : tolerance;
        }

        return Math.Round(x, 4);
    }
}
