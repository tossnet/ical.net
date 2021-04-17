using System;
using System.Collections.Generic;
using System.Linq;
using Experiments.Utilities;

namespace Experiments.PropertyParameters
{
    /// <summary>
    /// Parameter Name:  DELEGATED-TO
    /// Purpose:  To specify the calendar users to whom the calendar user specified by the property has delegated participation.
    ///
    /// Format Definition:  This property parameter is defined by the following notation:
    /// deltoparam = "DELEGATED-TO" "=" DQUOTE cal-address DQUOTE*("," DQUOTE cal-address DQUOTE)
    ///
    /// This parameter can be specified on properties with a CAL-ADDRESS value type.  This parameter specifies those calendar users whom have been delegated
    /// participation in a group-scheduled event or to-do by the calendar user specified by the property. The individual calendar address parameter values MUST
    /// each be specified in a quoted-string.
    ///
    /// https://tools.ietf.org/html/rfc5545#section-3.2.5
    /// </summary>
    /// <example>
    /// ATTENDEE;DELEGATED-TO="mailto:jdoe@example.com","mailto:jqpublic
    ///  @example.com":mailto:jsmith@example.com
    /// </example>
    public readonly struct DelegatedTo :
        IValueType
    {
        public string Name => "DELEGATED-TO";
        public string Value => ToString();
        public IReadOnlyList<string> Delegates { get; }

        public bool IsEmpty => Delegates is null || Delegates.Count == 0;

        /// <summary>
        /// An email address in almost any parseable formats
        /// </summary>
        /// <param name="delegate"></param>
        public DelegatedTo(string @delegate) :
            this(new []{@delegate}) { }

        /// <summary>
        /// A collection of email addresses in almost any parseable format
        /// </summary>
        /// <param name="delegates"></param>
        public DelegatedTo(IEnumerable<string> delegates)
        {
            Delegates = (delegates ?? Enumerable.Empty<string>())
                .Where(e => !string.IsNullOrEmpty(e))
                .Select(e => $"\"mailto:{e.ParseAndExtractEmailAddress()}\"")
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        public override string ToString()
        {
            return IsEmpty
                ? null
                : $"{Name}={string.Join(",", Delegates)}";
        }
    }
}