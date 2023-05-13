using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranscendentalEquations.Interfaces
{
    internal interface IFindFunction
    {
        public double f(double x, string equation);

        public double df(double x, string equation);
    }
}
