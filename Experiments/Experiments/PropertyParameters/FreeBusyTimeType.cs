using System;
using Experiments.Utilities;

namespace Experiments.PropertyParameters
{
    /// <summary>
    /// Parameter Name:  FBTYPE
    ///
    /// To specify the free or busy time type.
    ///
    /// Format Definition:  This property parameter is defined by the following notation:
    /// fbtypeparam        = "FBTYPE" "=" ("FREE" / "BUSY"
    ///     / "BUSY-UNAVAILABLE" / "BUSY-TENTATIVE"
    ///     / x-name
    /// ; Some experimental iCalendar free/busy type.
    ///     / iana-token)
    /// ; Some other IANA-registered iCalendar free/busy type.
    ///
    /// This parameter specifies the free or busy time type. The value FREE indicates that the time interval is free for scheduling. The value BUSY indicates
    /// that the time interval is busy because one or more events have been scheduled for that interval. The value BUSY-UNAVAILABLE indicates that the time
    /// interval is busy and that the interval can not be scheduled. The value BUSY-TENTATIVE indicates that the time interval is busy because one or more
    /// events have been tentatively scheduled for that interval.  If not specified on a property that allows this parameter, the default is BUSY. Applications
    /// MUST treat x-name and iana-token values they don't recognize the same way as they would the BUSY value.
    /// 
    /// https://tools.ietf.org/html/rfc5545#section-3.2.9
    /// </summary>
    /// <example>
    /// The following is an example of this parameter on a "FREEBUSY" property.
    ///     FREEBUSY;FBTYPE=BUSY:19980415T133000Z/19980415T170000Z
    /// </example>
    public readonly struct FreeBusyTimeType :
        IValueType
    {
        public string Name => "FBTYPE";
        public string Value { get; }
        public bool IsEmpty => Value is null;
        public bool IsBusy => Value is object && !string.Equals(Value, Free, StringComparison.OrdinalIgnoreCase);

        public const string Free = "FREE";
        public const string Busy = "BUSY";
        public const string BusyUnavailable = "BUSY-UNAVAILABLE";
        public const string BusyTentative = "BUSY-TENTATIVE";

        public FreeBusyTimeType(string fbTimeType)
        {
            if (string.IsNullOrWhiteSpace(fbTimeType))
            {
                Value = null;
                return;
            }

            if (fbTimeType.Equals(Free, StringComparison.OrdinalIgnoreCase))
            {
                Value = Free;
                return;
            }

            if (fbTimeType.Equals(Busy, StringComparison.OrdinalIgnoreCase))
            {
                Value = Busy;
                return;
            }
            
            if (fbTimeType.Equals(BusyUnavailable, StringComparison.OrdinalIgnoreCase))
            {
                Value = BusyUnavailable;
                return;
            }
            
            if (fbTimeType.Equals(BusyTentative, StringComparison.OrdinalIgnoreCase))
            {
                Value = BusyTentative;
                return;
            }

            if (fbTimeType.StartsWith("X-", StringComparison.OrdinalIgnoreCase))
            {
                Value = fbTimeType;
                return;
            }

            throw new ArgumentException($"{fbTimeType} is not a recognized free/busy time type");
        }

        public override string ToString() => this.NameEqualsValue();
    }
}