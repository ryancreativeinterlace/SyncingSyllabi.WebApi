using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace SyncingSyllabi.Common.Tools.Extensions
{

    public static class StringExtension
    {
        public static bool IsDigitsOnly(this string stringValue)
        {
            foreach (char c in stringValue)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public static string ToCamelCase(this string stringValue)
        {
            return $"{ stringValue.Substring(0, 1).ToLower() }{ stringValue.Substring(1) }";
        }

        public static string ToPascalCase(this string stringValue)
        {
            TextInfo info = Thread.CurrentThread.CurrentCulture.TextInfo;
            stringValue = info.ToTitleCase(stringValue);
            string[] parts = stringValue.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
            string result = String.Join(String.Empty, parts);
            return result;
        }

        public static string ToProperCase(this string stringValue)
        {
            const string pattern = @"(?<=\w)(?=[A-Z])";
            string result = Regex.Replace(stringValue, pattern, " ", RegexOptions.None);
            return result.Substring(0, 1).ToUpper() + result.Substring(1);
        }

        public static string RemoveUnnecessary(this string stringValue)
        {
            string result = stringValue.Trim().Replace("\n", "").Replace("\r", "");
            return result;
        }
    }
}
