using System;
using System.Collections.Generic;
using Experiments.ComponentProperties;

namespace Experiments.PropertyParameters
{
    /// <summary>
    /// Parameter Name:  CUTYPE
    ///
    /// To identify the type of calendar user specified by the property.
    ///
    /// Format Definition:  This property parameter is defined by the following notation:
    /// cutypeparam        = "CUTYPE" "="
    /// ("INDIVIDUAL"   ; An individual
    /// / "GROUP"        ; A group of individuals
    /// / "RESOURCE"     ; A physical resource
    /// / "ROOM"         ; A room resource
    /// / "UNKNOWN"      ; Otherwise not known
    /// / x-name         ; Experimental type
    /// / iana-token)    ; Other IANA-registered
    /// ; type
    /// ; Default is INDIVIDUAL
    /// This parameter can be specified on properties with a CAL-ADDRESS value type.The parameter identifies the type of calendar user specified by the
    /// property.If not specified on a property that allows this parameter, the default is INDIVIDUAL. Applications MUST treat x-name and iana-token values
    /// they don't recognize the same way as they would the UNKNOWN value.
    /// 
    /// https://tools.ietf.org/html/rfc5545#section-3.2.3
    /// </summary>
    /// <example>
    /// ATTENDEE;CUTYPE=GROUP:mailto:ietf-calsch@example.org
    /// </example>
    public readonly struct CalendarUserType :
        IValueType
    {
        public string Name => "CUTYPE";
        public string Value { get; }
        public bool IsEmpty => false;

        public CalendarUserType(string userType)
        {
            if (!CalendarUserTypes.IsValid(userType))
            {
                throw new ArgumentException($"{userType} isn't a recognized calendar user type.");
            }

            Value = string.IsNullOrWhiteSpace(userType)
                ? CalendarUserTypes.Default
                : userType;
        }

        public override string ToString() => $"{Name}={Value}";
    }
    
    /// <summary>
    /// Represents valid user type values for a CalendarUserType (CUTYPE)
    /// </summary>
    public static class CalendarUserTypes
    {
        public static string Default => Individual;
        public static string Individual => "INDIVIDUAL";
        public static string Group => "GROUP";
        public static string Resource => "RESOURCE";
        public static string Room => "ROOM";
        public static string Unknown => "UNKNOWN";

        private static readonly HashSet<string> _allowedValues = new HashSet<string>(StringComparer.Ordinal)
        {
            Individual, Group, Resource, Room, Unknown,
        };

        public static bool IsValid(string userType)
        {
            if (string.IsNullOrWhiteSpace(userType))
            {
                return true;
            }
            
            return userType.StartsWith("X-", StringComparison.Ordinal) || _allowedValues.Contains(userType);
        }
    }
}