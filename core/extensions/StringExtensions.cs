﻿using System.Text.RegularExpressions;

namespace core.extensions
{
    public static class StringExtensions
    {
        public static string RemoveWhitespace(this string str)
        {
            return Regex.Replace(str, @"\s+", "");
        }
    }
}