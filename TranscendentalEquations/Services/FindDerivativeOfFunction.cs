using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TranscendentalEquations.Services
{
    public class FindDerivativeOfFunction : AdditionalForParser
    {
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

        private string GetTrigonometricFunctionDerivative(string function, string argument)
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

        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
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
