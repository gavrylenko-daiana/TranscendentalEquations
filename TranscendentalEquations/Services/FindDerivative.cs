using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TranscendentalEquations.Interfaces;

namespace TranscendentalEquations.Services
{
    public class FindDerivative : AdditionalForParser, IFindDerivative
    {
        public string GetDerivativeFromString(string input)
        {
            if (input.Contains("sin") || input.Contains("cos") || input.Contains("tg") || input.Contains("ctg"))
                input = DerivativeTrigonometricFunctions(input);
            if (input.Contains('^'))
                input = DerivativePowers(input);
            if (input.Contains("sqrt"))
                input = DerivativeSqrt(input);
            if (input.Contains('|'))
                input = DerivativeAbsolute(input);

            return input;
        }

        public string ReplaceWithDerivative(string input)
        {
            var matches = Regex.Matches(input, @"(?<!\()\b(x|\d+)\b(?!\))");
            foreach (Match match in matches)
            {
                var derivative = match.Value == "x" ? "1" : match.Value;

                input = input.Remove(match.Index, match.Length).Insert(match.Index, derivative);
            }
            return input;
        }

        private string DerivativeTrigonometricFunctions(string input)
        {
            string regex = @"(?<Func>(cos|sin|tg|ctg))";
            List<string> arguments = GetArgumentsForTriginometry(input);

            MatchCollection data = new Regex(regex).Matches(input);
            int argumentIndex = 0;

            foreach (Match match in data)
            {
                string function = match.Groups["Func"].Value;
                string argument = arguments[argumentIndex];
                argumentIndex++;

                string fullMatch = $"{function}({argument})";
                string derivative = GetTrigonometricFunctionDerivative(function, argument);

                int startIndex = input.IndexOf(fullMatch);
                if (startIndex > 0 && IsOperator(input[startIndex - 1]) && derivative.StartsWith("-"))
                {
                    derivative = $"({derivative})";
                }

                input = input.Replace(fullMatch, derivative);
            }

            return input;
        }

        private string DerivativePowers(string input)
        {
            var components = GetComponentsForPow(input);
            foreach (var component in components)
            {
                string fullMatch = $"({component.arg})^({component.exp})";
                if (!int.TryParse(component.exp, out int power)) continue;
                string replacement = power == 0 ? "1" : (power == 1 ? $"{component.arg}" : $"{power}*({component.arg})^({power - 1})");
                input = input.Replace(fullMatch, replacement);
            }
            return input;
        }

        private string DerivativeSqrt(string input)
        {
            input = input.Replace(",", ".");
            string pattern = @"sqrt\((?<Arg>[^\(\)]+)\)";
            MatchCollection data = new Regex(pattern).Matches(input);

            if (data.Count == 0) return input;

            var parts = data.Select(m => new { Full = m.Groups[0].Value, Arg = m.Groups["Arg"].Value });

            foreach (var item in parts)
            {
                string replacement = $"(1/(2*sqrt({item.Arg})))";
                input = input.Replace(item.Full, replacement);
            }

            return input.Replace(",", ".");
        }

        private string DerivativeAbsolute(string input)
        {
            input = input.Replace(",", ".");
            string pattern = @"\|(?<Arg>[^\|]+)\|";

            MatchCollection data = new Regex(pattern).Matches(input);

            if (data.Count == 0) return input;

            var parts = data.Select(m => new { Full = m.Groups[0].Value, Arg = m.Groups[1].Value });

            foreach (var item in parts)
            {
                string replacement = $"({item.Arg})/|{item.Arg}|";
                input = input.Replace(item.Full, replacement);
            }

            return input.Replace(",", ".");
        }
    }
}
