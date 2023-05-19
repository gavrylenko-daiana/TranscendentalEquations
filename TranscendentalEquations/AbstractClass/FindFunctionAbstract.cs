using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranscendentalEquations.AbstractClass;

public abstract class FindFunctionAbstract
{
    protected abstract double f(double x, string equation);

    protected abstract double df(double x, string equation);
}
