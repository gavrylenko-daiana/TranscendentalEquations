using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.Helper;
using TranscendentalEquations.Services;

namespace TranscendentalEquations.TranscendentalMethods
{
    public class Newtons : FindFunction
    {
        public double NewtonsMethod(string equation)
        {
            double x = 1;
            double tolerance = 0.0001;
            int maxIterations = 100;
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
            }

            if (!isComplete)
            {
                tolerance = tolerance.GetTolerance(fx);
            }

            return x;
        }
    }
}
