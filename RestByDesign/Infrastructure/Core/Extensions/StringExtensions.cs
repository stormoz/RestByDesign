﻿using System;
using System.Collections.Generic;

namespace RestByDesign.Infrastructure.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToUpperFirstLetter(this string source)
        {
            return ConvertFirstLetter(source, false);
        }

        public static string ToLowerFirstLetter(this string source)
        {
            return ConvertFirstLetter(source);
        }

        /// Equals ignore case
        public static bool EqualsIc(this string source, string value)
        {
            return source.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }

        public static IEnumerable<string> SplitCsv(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new List<string>();

            return value.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string ConvertFirstLetter(this string source, bool lower = true)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            var letters = source.ToCharArray();
            letters[0] = lower ? char.ToLower(letters[0]) : char.ToUpper(letters[0]);
            return new string(letters);
        }
    }
}