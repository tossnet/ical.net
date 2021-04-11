using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;

namespace Experiments.PropertyParameters
{
    /// <summary>
    /// Parameter Name:  DELEGATED-FROM
    ///
    /// To specify the calendar users that have delegated their participation to the calendar user specified by the property.
    ///
    /// Format Definition:  This property parameter is defined by the following notation:
    ///
    /// delfromparam       = "DELEGATED-FROM" "=" DQUOTE cal-address DQUOTE *("," DQUOTE cal-address DQUOTE)
    ///
    /// This parameter can be specified on properties with a CAL-ADDRESS value type.  This parameter specifies those calendar users that have delegated their
    /// participation in a group-scheduled event or to-do to the calendar user specified by the property. The individual calendar address parameter values MUST
    /// each be specified in a quoted-string.
    ///
    /// https://tools.ietf.org/html/rfc5545#section-3.2.4
    /// </summary>
    /// <example>
    /// ATTENDEE;DELEGATED-FROM="mailto:jsmith@example.com":mailto:
    ///  jdoe@example.com
    /// </example>
    public readonly struct DelegatedFrom :
        IValueType
    {
        public string Name => "DELEGATED-FROM";
        public string Value => ToString();
        public IReadOnlyList<string> Delegates { get; }

        public bool IsEmpty => Delegates is null || Delegates.Count == 0;

        /// <summary>
        /// An email address in the form "mailto:foo@example.com"
        /// </summary>
        /// <param name="delegate"></param>
        public DelegatedFrom(string @delegate) :
            this(new []{@delegate}) { }

        public DelegatedFrom(IEnumerable<string> delegates)
        {
            Delegates = (delegates ?? Enumerable.Empty<string>())
                .Where(e => !string.IsNullOrEmpty(e))
                .Select(e =>
                {
                    if (Uri.TryCreate(e, UriKind.Absolute, out var uri) && uri.Scheme.Equals(Uri.UriSchemeMailto, StringComparison.Ordinal))
                    {
                        return $"\"{e}\"";
                    }
                    
                    var addr = new MailAddress(e);
                    return $"\"{Uri.UriSchemeMailto}:{addr.Address}\"";
                })
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        public override string ToString()
            => $"{Name}={string.Join(",", Delegates)}";
    }
}