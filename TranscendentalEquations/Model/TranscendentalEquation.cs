using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.TranscendentalMethods;

namespace TranscendentalEquations.Model
{
    public abstract class TranscendentalEquation
    {
        public string Equation { get; set; } = string.Empty;
        public int MaxIterations { get; set; }
        public double Tolerance { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public double Result { get; set; }

        public abstract double Solve();
    }
}
