using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranscendentalEquations.Services
{
    public class AdditionalForParser
    {
        protected List<(string arg, string exp)> GetComponentsForPow(string input)
        {
            List<(string arg, string exp)> components = new List<(string arg, string exp)>();
            Stack<int> openIndices = new Stack<int>();
            List<int> removeIndices = new List<int>();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    openIndices.Push(i);
                }
                else if (input[i] == ')')
                {
                    if (openIndices.Count > 0)
                    {
                        int openIndex = openIndices.Pop();
                        if (i + 2 < input.Length && input[i + 1] == '^' && input[i + 2] == '(')
                        {
                            int expStart = i + 3;
                            int expEnd = -1;
                            int expOpenCount = 1;
                            for (int j = expStart; j < input.Length; j++)
                            {
                                if (input[j] == '(')
                                {
                                    expOpenCount++;
                                }
                                else if (input[j] == ')')
                                {
                                    expOpenCount--;
                                    if (expOpenCount == 0)
                                    {
                                        expEnd = j - 1;
                                        break;
                                    }
                                }
                            }
                            if (expEnd != -1)
                            {
                                string arg = input.Substring(openIndex + 1, i - openIndex - 1);
                                string exp = input.Substring(expStart, expEnd - expStart + 1);
                                components.Add((arg, exp));
                            }
                        }
                    }
                }
            }
            return components;
        }

        protected List<string> GetArgumentsForTriginometry(string input)
        {
            List<string> arguments = new List<string>();
            string[] functions = new string[] { "cos", "sin", "tg", "ctg" };
            foreach (string function in functions)
            {
                int index = input.IndexOf(function + "(");
                while (index != -1)
                {
                    int openCount = 0;
                    for (int i = index + function.Length; i < input.Length; i++)
                    {
                        if (input[i] == '(')
                        {
                            openCount++;
                        }
                        else if (input[i] == ')')
                        {
                            openCount--;
                            if (openCount == 0)
                            {
                                arguments.Add(input.Substring(index + function.Length + 1, i - index - function.Length - 1));
                                break;
                            }
                        }
                    }
                    index = input.IndexOf(function + "(", index + 1);
                }
            }

            return arguments;
        }
    }
}
