using System;
using System.Text.RegularExpressions;

namespace core.extensions
{
    public static class StringExtensions
    {
        public static string RemoveWhitespace(this string str)
        {
            return Regex.Replace(str, @"\s+", "");
        }

        public static string CutBefore(this string str, string match)
        {
            if (match == null)
                return str;

            var start_index = str.IndexOf(match);

            if (start_index == -1)
                return str;
            return str.Substring(start_index, str.Length - start_index);
        }

        public static string GetLinesUntilBlankLine(this string str)
        {
            if(str == null)
                return null;

            var first_blank_line_match = Regex.Match(str, @"(?<=\r?\n)[ \t]*(\r?\n|$)");
            var first_blank_line_index = first_blank_line_match.Success ? first_blank_line_match.Index : -1;

            if (first_blank_line_index == -1)
                return str;
            return str.Substring(0, first_blank_line_index).Trim();
        }
    }
}