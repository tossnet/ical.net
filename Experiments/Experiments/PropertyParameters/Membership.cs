using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Experiments.Utilities;

namespace Experiments.PropertyParameters
{
    /// <summary>
    /// Parameter Name:  MEMBER
    ///
    /// To specify the group or list membership of the calendar user specified by the property.
    ///
    /// Format Definition:  This property parameter is defined by the following notation:
    ///     memberparam        = "MEMBER" "=" DQUOTE cal-address DQUOTE
    ///     *("," DQUOTE cal-address DQUOTE)
    ///
    /// This parameter can be specified on properties with a CAL-ADDRESS value type.  The parameter identifies the groups or list membership for the calendar
    /// user specified by the property. The parameter value is either a single calendar address in a quoted-string or a COMMA-separated list of calendar
    /// addresses, each in a quoted-string.  The individual calendar address parameter values MUST each be specified in a quoted-string.
    ///
    /// <see href="https://tools.ietf.org/html/rfc5545#section-3.2.11">https://tools.ietf.org/html/rfc5545#section-3.2.11</see>
    /// </summary>
    /// <example>
    /// ATTENDEE;MEMBER="mailto:ietf-calsch@example.org":mailto:
    ///  jsmith@example.com
    /// </example>
    /// <example>
    /// ATTENDEE;MEMBER="mailto:projectA@example.com","mailto:pr
    ///  ojectB@example.com":mailto:janedoe@example.com
    /// </example>
    public readonly struct Membership :
        IValueType
    {
        public string Name => "MEMBER";
        public string Value => ToString();
        public IReadOnlyList<string> Memberships { get; }

        public bool IsEmpty => Memberships is null || Memberships.Count == 0;
        
        /// <summary>
        /// An email address in almost any parseable format
        /// </summary>
        /// <param name="membership"></param>
        public Membership(string membership) :
            this(new []{membership}) { }

        /// <summary>
        /// A collection of email addresses in almost any parseable format
        /// </summary>
        /// <param name="memberships"></param>
        public Membership(IEnumerable<string> memberships)
        {
            var intermediate = (memberships ?? Enumerable.Empty<string>())
                .Where(e => !string.IsNullOrWhiteSpace(e))
                .Select(e => e.ParseAndExtractEmailAddress())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            Memberships = intermediate.Any()
                ? intermediate
                : null;
        }
        
        public override string ToString()
        {
            if (IsEmpty)
            {
                return null;
            }

            var needsComma = false;
            var sb = new StringBuilder();
            sb.Append(Name).Append("=");
            foreach (var element in Memberships)
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