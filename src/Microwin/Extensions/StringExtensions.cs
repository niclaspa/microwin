using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
