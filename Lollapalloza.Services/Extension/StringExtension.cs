using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lollapalooza.Services.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// Format band name to create distribution list
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharacter(this string value)
        {
            var formattedValue = new string(value.Where(x => char.IsLetterOrDigit(x)).ToArray());

            formattedValue = Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-8").GetBytes(formattedValue));

            return formattedValue;
        }
    }
}
