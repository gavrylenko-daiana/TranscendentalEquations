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
        public static string ReplaceTriginometry(double x, string input)
        {
            string pattern = @"(?<Func>(cos|sin|tg|ctg)\((?<Name>.+?)\))";

            MatchCollection data = new Regex(pattern).Matches(input);

            if (data.Count == 0) return input;

            var parts = data.Select(m => new
            { Full = m.Groups[0].Value, Func = m.Groups[1].Value, Arg = m.Groups[3].Value });

            foreach (var item in parts)
            {
                string arg = item.Arg.Replace("x", x.ToString()).Replace(",", ".");
                double value = Convert.ToDouble(Convert.ToDecimal(new DataTable().Compute(arg, null)));

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

            return input;
        }

        public static string ReplacePow(double x, string input)
        {
            string pattern = @"(\((?<Arg>[^\(\)]+)\))\^\d+";

            MatchCollection data = new Regex(pattern).Matches(input);

            if (data.Count == 0) return input;

            var parts = data.Select(m => new { Full = m.Groups[0].Value, Arg = m.Groups[1].Value });

            foreach (var item in parts)
            {
                string baseValue = item.Arg.Replace("x", x.ToString()).Replace(",", ".");
                double baseResult = Convert.ToDouble(Convert.ToDecimal(new DataTable().Compute(baseValue, null)));

                double value = Math.Pow(baseResult, 2);
                value = Math.Round(value, 4);
                input = input.Replace(item.Full, value.ToString());
            }

            return input;
        }

        public static string ReplaceAbsolute(double x, string input)
        {
            string pattern = @"\|(?<Arg>[^\|]+)\|";

            MatchCollection data = new Regex(pattern).Matches(input);

            if (data.Count == 0) return input;

            var parts = data.Select(m => new { Full = m.Groups[0].Value, Arg = m.Groups[1].Value });

            foreach (var item in parts)
            {
                string value = item.Arg.Replace("x", x.ToString()).Replace(",", ".");
                double result = Convert.ToDouble(Convert.ToDecimal(new DataTable().Compute(value, null)));
                result = Math.Abs(result);
                result = Math.Round(result, 4);
                input = input.Replace(item.Full, result.ToString());
            }

            return input;
        }

        public static string ReplaceConstants(string input)
        {
            input = input.Replace("pi", Math.Round(Math.PI, 4).ToString());
            input = input.Replace("e", Math.Round(Math.E, 4).ToString());

            return input;
        }

        public static double GetResultValue(double x, string input)
        {
            string result = input.Replace("x", x.ToString()).Replace(",", ".");
            double value = Convert.ToDouble(Convert.ToDecimal(new DataTable().Compute(result, null)));
            value = Math.Round(value, 4);

            return value;
        }
    }
}
