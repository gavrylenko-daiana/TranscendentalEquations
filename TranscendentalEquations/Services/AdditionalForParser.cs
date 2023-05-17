using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TranscendentalEquations.Services
{
    public class AdditionalForParser
    {
        #region pow
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

        protected double CalculatePower(double baseValue, double exponentValue)
        {
            bool isNegative = false;

            if (baseValue < 0)
            {
                baseValue = Math.Abs(baseValue);
                isNegative = !isNegative;
            }
            if (exponentValue < 0)
            {
                exponentValue = Math.Abs(exponentValue);
                isNegative = !isNegative;
            }

            double value = Math.Pow(baseValue, exponentValue);

            if (isNegative && exponentValue % 2 != 0)
            {
                value = -value;
            }

            return value;
        }
        #endregion

        #region trigonometry
        protected List<string> GetArgumentsForTriginometry(string input)
        {
            List<string> arguments = new List<string>();
            string pattern = @"(?<Func>(cos|sin|tg|ctg))";
            MatchCollection matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                int index = match.Index + match.Length;
                if (index < input.Length && input[index] == '(')
                {
                    int openCount = 1;
                    int endIndex = -1;
                    for (int i = index + 1; i < input.Length; i++)
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
                                endIndex = i;
                                break;
                            }
                        }
                    }
                    if (endIndex != -1)
                    {
                        arguments.Add(input.Substring(index + 1, endIndex - index - 1));
                    }
                }
            }
            return arguments;
        }

        protected string GetTrigonometricFunctionDerivative(string function, string argument)
        {
            switch (function)
            {
                case "sin":
                    return $"cos({argument})";

                case "cos":
                    return $"-sin({argument})";

                case "ctg":
                    return $"-1/(sin({argument}))^(2)";

                case "tg":
                    return $"1/(cos({argument}))^(2)";

                default:
                    return string.Empty;
            }
        }
        #endregion

        protected bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }
    }
}
