using System;
using Experiments.Utilities;

namespace Experiments.PropertyParameters
{
    /// <summary>
    /// Parameter Name:  RELATED
    ///
    /// To specify the relationship of the alarm trigger with respect to the start or end of the calendar component.
    ///
    /// Format Definition:  This property parameter is defined by the following notation:
    ///     trigrelparam       = "RELATED" "="
    ///                         ("START"       ; Trigger off of start
    ///                         / "END")        ; Trigger off of end
    ///
    /// This parameter can be specified on properties that specify an alarm trigger with a "DURATION" value type. The parameter specifies whether the alarm will
    /// trigger relative to the start or end of the calendar component. The parameter value START will set the alarm to trigger off the start of the calendar
    /// component; the parameter value END will set the alarm to trigger off the end of the calendar component.  If the parameter is not specified on an
    /// allowable property, then the default is START.
    ///
    /// https://tools.ietf.org/html/rfc5545#section-3.2.14
    /// </summary>
    /// <example>
    /// TRIGGER;RELATED=END:PT5M
    /// </example>
    public readonly struct AlarmTriggerRelationship :
        IValueType
    {
        public string Name => "RELATED";
        public string Value { get; }
        public bool IsEmpty => Value is null;

        public const string Start = "START";
        public const string End = "END";

        public AlarmTriggerRelationship(string relationship)
        {
            if (string.IsNullOrWhiteSpace(relationship))
            {
                Value = null;
                return;
            }

            if (string.Equals(Start, relationship, StringComparison.OrdinalIgnoreCase))
            {
                Value = Start;
                return;
            }

            if (string.Equals(End, relationship, StringComparison.OrdinalIgnoreCase))
            {
                Value = End;
                return;
            }

            throw new ArgumentException($"{relationship} is not a valid alarm trigger relationship. Must be either '{Start}' or '{End}'");
        }

        public override string ToString() => this.NameEqualsValue();
    }
}