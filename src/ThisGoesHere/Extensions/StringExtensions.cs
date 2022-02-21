using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Valheim.ThisGoesHere.Extensions;

internal static class StringExtensions
{
    private static char[] Comma = new[] { ',' };
    private static char[] Slash = new[] { '/', '\\' };

    private static Regex Whitespace = new Regex(@"^\s*$", RegexOptions.Compiled);

    public static string[] SplitByComma(this string value, bool toUpper = false)
        => value.SplitBy(Comma, toUpper).ToArray();

    public static string[] SplitBySlash(this string value, bool toUpper = false)
        => value.SplitBy(Slash, toUpper).ToArray();

    public static IEnumerable<string> SplitBy(this string value, char[] chars, bool toUpper = false)
    {
        var split = value.Split(chars, StringSplitOptions.RemoveEmptyEntries);

        if ((split?.Length ?? 0) == 0)
        {
            return Enumerable.Empty<string>();
        }

        return split.Select(Clean);

        string Clean(string x)
        {
            var result = x.Trim();
            if (toUpper)
            {
                return result.ToUpperInvariant();
            }
            return result;
        }
    }

    public static bool IsEmpty(this string input)
    {
        if (input is null)
        {
            return true;
        }

        if (input.Length == 0)
        {
            return true;
        }

        if (Whitespace.IsMatch(input))
        {
            return true;
        }

        return false;
    }
}
