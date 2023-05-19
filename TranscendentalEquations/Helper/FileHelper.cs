using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranscendentalEquations.Helper
{
    public class FileHelper
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
    }
}
