using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TranscendentalEquations.Interfaces;

namespace TranscendentalEquations.Services;

public class ParserService : AdditionalForParser, IParserService
{
    public double GetValueFromEquation(string input)
    {
        if (input.Contains("sin") || input.Contains("cos") || input.Contains("tg") || input.Contains("ctg"))
            input = ReplaceTriginometry(input);
        if (input.Contains('^'))
            input = ReplacePow(input);
        if (input.Contains("log"))
            input = ReplaceLogarithm(input);
        if (input.Contains("sqrt"))
            input = ReplaceSqrt(input);
        if (input.Contains('|'))
            input = ReplaceAbsolute(input);

        return GetResultValue(input);
    }

    private string ReplacePow(string input)
    {
        List<(string arg, string exp)> components = GetComponentsForPow(input);
        while (components.Count > 0)
        {
            var component = components[0];
            double baseResult = GetValueFromEquation(component.arg);
            double exponentResult = GetValueFromEquation(component.exp);

            double value = CalculatePower(baseResult, exponentResult);

            value = Math.Round(value, 4);
            input = input.Replace($"({component.arg})^({component.exp})", value.ToString());
            components = GetComponentsForPow(input);
        }
        return input.Replace(",", ".");
    }

    private string ReplaceLogarithm(string input)
    {
        List<string> arguments = GetLogArguments(input);
        int index = 0;
        while (index < input.Length)
        {
            index = FindNextLogKeyword(input, index);
            if (index == -1) break;
            int startIndex = index;
            index += 3;
            index = SkipWhitespace(input, index);
            if (IsOpeningParenthesis(input, index))
            {
                index++;
                int endIndex = FindClosingParenthesis(input, index);
                if (endIndex != -1)
                {
                    string baseAndArgument = input.Substring(index, endIndex - index);
                    int semicolonIndex = baseAndArgument.IndexOf(';');
                    if (semicolonIndex != -1)
                    {
                        string baseString = baseAndArgument.Substring(0, semicolonIndex).Trim();
                        double baseValue = GetValueFromEquation(baseString);
                        string argumentString = arguments[0];
                        arguments.RemoveAt(0);
                        double argumentValue = GetValueFromEquation(argumentString);
                        double result = Math.Log(argumentValue, baseValue);
                        input = input.Remove(startIndex, endIndex - startIndex + 1).Insert(startIndex, result.ToString());
                        index = startIndex + result.ToString().Length;
                    }
                }
            }
        }
        return input;
    }

    private string ReplaceTriginometry(string input)
    {
        string pattern = @"(?<Func>(cos|sin|tg|ctg))";
        List<string> arguments = GetArgumentsForTriginometry(input);

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

        try
        {
            double value = Convert.ToDouble(Convert.ToDecimal(new DataTable().Compute(str, null)));
            value = Math.Round(value, 4);

            return value;
        }
        catch
        {
            return double.NaN;
        }
    }
}
