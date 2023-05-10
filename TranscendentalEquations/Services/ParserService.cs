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
    public class ParserService : IParserService
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

        public string ReplacePow(string input)
        {
            // string pattern = @"(\((?<Arg>[^\(\)]+)\))\^(\((?<Name>[^\(\)].+?)\)+)";
            string pattern = @"(\((?<Arg>[^\(\)]+)\))\^((?<Name>\(.+?\)))";
            MatchCollection data = new Regex(pattern).Matches(input);

            var parts = data.Select(m => new
            { Full = BalanceParentheses(m.Groups[0].Value), Arg = m.Groups[3].Value, Name = BalanceParentheses(m.Groups[2].Value) });
            
            string arg = "";

            foreach (var item in parts)
            {
                arg = item.Arg;
                arg = CheckTrigInPow(item.Arg);

                double baseResult = GetValueFromEquation(arg);

                double exponentResult = GetValueFromEquation(item.Name);

                double value = Math.Pow(baseResult, exponentResult);
                value = Math.Round(value, 4);
                input = input.Replace(item.Full, value.ToString());
            }

            return input.Replace(",", ".");
        }

        private string CheckTrigInPow(string input)
        {
            if (input.Contains("sin") || input.Contains("cos") || input.Contains("tg") || input.Contains("ctg"))
            {
                if (input.Contains("sin")) input = AddParentheses(input, "sin");
                else if (input.Contains("cos")) input = AddParentheses(input, "cos");
                else if (input.Contains("tg")) input = AddParentheses(input, "tg");
                else if (input.Contains("ctg")) input = AddParentheses(input, "ctg");
            }

            return input;
        }

        private string AddParentheses(string input, string word)
        {
            int startIndex = input.IndexOf(word) + word.Length;

            if (startIndex != -1)
            {
                input = input.Insert(startIndex, "(");
                input = input.Insert(input.Length, ")");
            }

            return input;
        }

        public string ReplaceTriginometry(string input)
        {
            string pattern = @"(?<Func>(cos|sin|tg|ctg)\((?<Name>.+?)\))";

            MatchCollection data = new Regex(pattern).Matches(input);

            if (data.Count == 0) return input;

            var parts = data.Select(m => new { Full = m.Groups[0].Value, Func = m.Groups[1].Value, Arg = m.Groups[3].Value });

            foreach (var item in parts)
            {
                double value = GetValueFromEquation(item.Arg);

                value = item.Func.ToLower() switch
                {
                    "sin" => Math.Sin(value),
                    "cos" => Math.Cos(value),
                    "tg" => Math.Tan(value),
                    "ctg" => 1 / Math.Tan(value),
                    _ => value
                };

                value = Math.Round(value, 4);
                input = input.Replace(item.Full, value.ToString());
            }

            return input.Replace(",", "."); ;
        }

        public string ReplaceSqrt(string input)
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

        public string ReplaceAbsolute(string input)
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

        public double GetResultValue(string input)
        {
            string str = input.Replace(",", ".");
            double value = Convert.ToDouble(Convert.ToDecimal(new DataTable().Compute(str, null)));
            value = Math.Round(value, 4);

            return value;
        }

        private string BalanceParentheses(string input)
        {
            int openCount = 0;
            int closeCount = 0;
            foreach (char c in input)
            {
                if (c == '(') openCount++;
                else if (c == ')') closeCount++;
            }
            int extraClosing = closeCount - openCount;
            if (extraClosing > 0)
            {
                string result = "";
                for (int i = input.Length - 1; i >= 0; i--)
                {
                    if (input[i] == ')' && extraClosing > 0)
                    {
                        extraClosing--;
                        continue;
                    }
                    result = input[i] + result;
                }
                return result;
            }
            else if (extraClosing < 0)
            {
                return input + new string(')', -extraClosing);
            }
            else
            {
                return input;
            }
        }
    }
}
