using System;

namespace TranscendentalEquations.Interfaces
{
    public interface IEquationManager
    {
        double GetValueFromEquation(double x, string input);
    }
}