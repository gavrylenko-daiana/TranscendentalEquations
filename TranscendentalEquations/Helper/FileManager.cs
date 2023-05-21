using MathNet.Numerics.RootFinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranscendentalEquations.Model;
using TranscendentalEquations.TranscendentalMethods;

namespace TranscendentalEquations.Helper;

public class FileManager
{
    public void WriteToFile(string fileName, string data)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.Write(data);
        }
    }

    public void ReadFromFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            string intermediateData = File.ReadAllText(fileName);
            MessageBox.Show(intermediateData, "Intermediate Data");
        }
        else
        {
            MessageBox.Show("Intermediate data file not found.", "Error");
        }
    }

    public void AddMainData(StringBuilder intermediateData, TranscendentalEquation equation, string nameMethod) 
    {
        intermediateData.AppendLine($"Method: {nameMethod}");
        intermediateData.AppendLine($"Equation: {equation.Equation}");
        intermediateData.AppendLine($"Max Iterations: {equation.MaxIterations}");
        intermediateData.AppendLine($"Tolerance: {equation.Tolerance}");
        intermediateData.AppendLine();
    }

    public void AddResultData(StringBuilder intermediateData, TranscendentalEquation equation)
    {
        intermediateData.AppendLine($"Result: {equation.Result}");
        intermediateData.AppendLine();
    }
}

