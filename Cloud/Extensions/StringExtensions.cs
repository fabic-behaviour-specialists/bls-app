using System;
using System.Collections.Generic;

namespace BLS.Cloud.Helpers
{
    public static class StringExtensions
    {
        public static List<int> AllIndexesOf(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }

        /// <summary>
        /// To SQL format
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToSQLFormat(this string s)
        {
            s = s.Replace("'", "''");
            return "'" + s + "'";
        }
    }
}
