using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Content.Generator.Extensions
{
    internal static class StrigifyExtensions
    {
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public static string ToTableRecordValueString(this int integerValue)
        {
            return integerValue.ToString();
        }

        public static string ToTableRecordValueString(this string stringValue)
        {
            return $"'{stringValue}'";
        }

        public static string ToTableRecordValueString(this DateTime dateTimeValue)
        {
            return $"'{dateTimeValue.ToString(DateTimeFormat)}'";
        }

        public static string ToTableRecordValueString(this Guid guidValue)
        {
            return $"'{guidValue}'";
        }
    }
}
