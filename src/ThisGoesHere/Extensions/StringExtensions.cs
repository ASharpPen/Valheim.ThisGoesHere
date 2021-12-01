using System.Collections.Generic;
using System.Linq;

namespace System;

internal static class StringExtensions
{
    private static char[] Comma = new[] { ',' };
    private static char[] Slash = new[] { '/', '\\' };

    public static string[] SplitByComma(this string value, bool toUpper = false)
        => SplitBy(value, Comma, toUpper).ToArray();

    public static string[] SplitBySlash(this string value, bool toUpper = false)
        => SplitBy(value, Slash, toUpper).ToArray();

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
}
