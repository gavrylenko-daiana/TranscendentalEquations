using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TranscendentalEquations.Interfaces;

namespace TranscendentalEquations.Services
{
    public class ParserService
    {
        public double GetValueFromEquation(string input)
        {
            if (input.Contains('^'))
                input = ReplacePow(input);
            if (input.Contains("sin") || input.Contains("cos") || input.Contains("tg") || input.Contains("ctg"))
                input = ReplaceTriginometry(input);
            if (input.Contains("sqrt"))
                input = ReplaceSqrt(input);
            if (input.Contains('|'))
                input = ReplaceAbsolute(input);

            return GetResultValue(input);
        }

        private string ReplacePow(string input)
        {
            List<(string arg, string exp)> components = GetExponentiationComponents(input);
            foreach (var component in components)
            {
                double baseResult = GetValueFromEquation(component.arg);
                double exponentResult = GetValueFromEquation(component.exp);
                double value = Math.Pow(baseResult, exponentResult);
                value = Math.Round(value, 4);
                input = input.Replace($"({component.arg})^({component.exp})", value.ToString());
            }
            return input.Replace(",", ".");
        }

        private List<(string arg, string exp)> GetExponentiationComponents(string input)
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

        private string ReplaceTriginometry(string input)
        {
            string pattern = @"(?<Func>(cos|sin|tg|ctg))";
            List<string> arguments = GetFunctionArguments(input);

            MatchCollection data = new Regex(pattern).Matches(input);
            int argumentIndex = 0;

            if (data.Count == 0) return input;
            if (data.Count != arguments.Count) throw new Exception("There is no '(' sign after the trigonometric function!");

            var parts = data.Select(m => new { Func = m.Groups[1].Value });

            foreach (var item in parts)
            {
                string argument = arguments[argumentIndex];
                argumentIndex++;

                string fullMatch = $"{item.Func}({argument})";

                double value = GetValueFromEquation(argument);

                value = item.Func.ToLower() switch
                {
                    "sin" => Math.Sin(value),
                    "cos" => Math.Cos(value),
                    "tg" => Math.Tan(value),
                    "ctg" => 1 / Math.Tan(value),
                    _ => value
                };

                value = Math.Round(value, 4);
                input = input.Replace(fullMatch, value.ToString());
            }

            return input.Replace(",", "."); ;
        }

        private List<string> GetFunctionArguments(string input)
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

        private string ReplaceSqrt(string input)
        {
            input = input.Replace(",", ".");
            string pattern = @"sqrt\((?<Arg>[^\(\)]+)\)";
            MatchCollection data = new Regex(pattern).Matches(input);

            if (data.Count == 0) return input;

            var parts = data.Select(m => new { Full = m.Groups[0].Value, Arg = m.Groups["Arg"].Value });

            foreach (var item in parts)
            {
                double baseResult = GetValueFromEquation(item.Arg);
                double value = Math.Sqrt(baseResult);
                value = Math.Round(value, 4);
                input = input.Replace(item.Full, value.ToString());
            }

            return input.Replace(",", "."); ;
        }

        private string ReplaceAbsolute(string input)
        {
            input = input.Replace(",", ".");
            string pattern = @"\|(?<Arg>[^\|]+)\|";

            MatchCollection data = new Regex(pattern).Matches(input);

            if (data.Count == 0) return input;

            var parts = data.Select(m => new { Full = m.Groups[0].Value, Arg = m.Groups[1].Value });

            foreach (var item in parts)
            {
                double result = GetValueFromEquation(item.Arg);
                result = Math.Abs(result);
                result = Math.Round(result, 4);
                input = input.Replace(item.Full, result.ToString());
            }

            return input.Replace(",", "."); ;
        }

        public string ReplaceConstants(string input)
        {
            input = input.Replace("pi", Math.Round(Math.PI, 4).ToString());
            input = input.Replace("e", Math.Round(Math.E, 4).ToString());

            return input.Replace(",", ".");
        }

        private double GetResultValue(string input)
        {
            string str = input.Replace(",", ".");
            double value = Convert.ToDouble(Convert.ToDecimal(new DataTable().Compute(str, null)));
            value = Math.Round(value, 4);

            return value;
        }
    }
}
