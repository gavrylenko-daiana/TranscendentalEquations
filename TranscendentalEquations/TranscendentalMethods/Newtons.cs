using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.Model;
using TranscendentalEquations.Services;

namespace TranscendentalEquations.TranscendentalMethods
{
    public class Newtons
    {
        private static double f(double x, string equation)
        {
            ParserService parserService = new ParserService();
            double result = parserService.GetValueFromEquation(equation);

            return result;
        }

        private static double df(double x, string equation)
        {
            return (1 - x) * Math.Exp(-x);
        }

        //public void NewtonsMethod(string equation)
        //{
        //    double x0 = 1, x1;
        //    double tolerance = 1e-6;
        //    int maxIterations = 100;
        //    int i;

        //    for (i = 0; i < maxIterations; i++)
        //    {
        //        x1 = x0 - f(x0, equation) / df(x0, equation);
        //        if (Math.Abs(x1 - x0) < tolerance)
        //            break;
        //        x0 = x1;
        //    }
        //}


        public static double NewtonsMethod(string equation)
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
                string fxStr = fx.ToString();
                int decimalIndex = fxStr.IndexOf(".");

                if (decimalIndex != -1 && fxStr[decimalIndex + 1] == '0')
                {
                    fxStr = fxStr.Substring(0, decimalIndex + 1) + '1' + fxStr.Substring(decimalIndex + 2);
                    tolerance = Convert.ToDouble(fxStr);
                }
                else
                {
                    tolerance = Math.Ceiling(fx * 10) / 10;
                }
            }

            return x;
        }
    }
}
