using System.Collections.Generic;
using Experiments.Utilities;

namespace Experiments.PropertyParameters
{
    /// <summary>
    /// Parameter Name:  CN
    ///
    /// To specify the common name to be associated with the calendar user specified by the property.
    ///
    /// Format Definition: This property parameter is defined by the following notation:
    /// cnparam    = "CN" "=" param-value
    ///
    /// This parameter can be specified on properties with a CAL-ADDRESS value type.  The parameter specifies the common name to be associated with the calendar
    /// user specified by the property. The parameter value is text.  The parameter value can be used for display text to be associated with the calendar
    /// address specified by the property.
    ///
    /// https://tools.ietf.org/html/rfc5545#section-3.2.2
    /// </summary>
    /// <example>
    /// ORGANIZER;CN="John Smith":mailto:jsmith@example.com
    /// </example>
    /// 
    public readonly struct CommonName :
        IValueType
    {
        public string Name => "CN";
        public string Value { get; }
        
        /// <summary>
        /// Unsupported operation
        /// </summary>
        public IReadOnlyList<string> Properties => null;

        public bool IsEmpty => Value is null;

        public CommonName(string value)
        {
            Value = string.IsNullOrWhiteSpace(value)
                ? null
                : value;
        }

        public override string ToString() => this.NameEqualsQuotedValue();
    }
}