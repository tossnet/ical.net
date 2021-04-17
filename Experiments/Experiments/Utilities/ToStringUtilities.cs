using System.Collections.Generic;
using System.Text;
using Experiments.PropertyParameters;

namespace Experiments.Utilities
{
    public static class ToStringUtilities
    {
        /// <summary>
        /// If the value type is not empty, a quoted representation of the Name and Value is returned. If the value type is empty, null is returned.
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns>Label="Value"</returns>
        public static string NameEqualsQuotedValue(this IValueType valueType)
        {
            return valueType.IsEmpty
                ? null
                : $"{valueType.Name}=\"{valueType.Value}\"";
        }

        /// <summary>
        /// If the value type is not empty, a formatted representation of the Name and Value is returned. If the value type is empty, null is returned.
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns>Label=Value</returns>
        public static string NameEqualsValue(this IValueType valueType)
        {
            return valueType.IsEmpty
                ? null
                : $"{valueType.Name}={valueType.Value}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueType"></param>
        /// <param name="addresses"></param>
        /// <returns></returns>
        public static string QuotedValuesWithMailto(this IValueType valueType, IEnumerable<string> addresses)
        {
            if (valueType.IsEmpty)
            {
                return null;
            }

            var needsComma = false;
            var sb = new StringBuilder();
            sb.Append(valueType.Name).Append("=");
            foreach (var element in addresses)
            {
                if (needsComma)
                {
                    sb.Append(",");
                }
                sb.Append("\"mailto:").Append(element).Append("\"");
                needsComma = true;
            }
            return sb.ToString();
        }
    }
}