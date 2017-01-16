using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Microwin.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Formats the string using the invariant culture.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="args">The args.</param>
        /// <returns>The formatted string.</returns>
        public static string InvariantFormat(this string value, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, value, args);
        }

        /// <summary>
        /// Removes the diacritics.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string RemoveDiacritics(this string text)
        {
            string formD = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char ch in formD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string RemoveAllWhitespace(this string value)
        {
            return Regex.Replace(value, @"\s*", string.Empty);
        }

        /// <summary>
        /// UnEscapes unicode characters in a string.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <returns></returns>
        public static string UnEscapeUnicode(this string s)
        {
            if (!String.IsNullOrEmpty(s))
            {
                string esc = Regex.Replace(s, @"\\u([\dA-Fa-f]{4})", v => ((char)Convert.ToInt32(v.Groups[1].Value, 16)).ToString());
                return esc;
            }

            return s;
        }

        public static string ToLettersOnly(this string s)
        {
            return new string(s.ToLowerInvariant().Where(c => Char.IsLetter(c)).ToArray());
        }
    }
}
