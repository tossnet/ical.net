using System;
using System.Collections.Generic;
using Experiments.ComponentProperties;

namespace Experiments.PropertyParameters
{
    /// <summary>
    /// This parameter can be specified on properties with a CAL-ADDRESS value type.The parameter identifies the type of calendar user specified by the
    /// property.If not specified on a property that allows this parameter, the default is INDIVIDUAL. Applications MUST treat x-name and iana-token values
    /// they don't recognize the same way as they would the UNKNOWN value.
    /// https://tools.ietf.org/html/rfc5545#section-3.2.3
    /// </summary>
    public struct CalenderUserType :
        INameValueProperty
    {
        public string Name => "CUTYPE";
        public string Value { get; }
        public IReadOnlyList<string> Properties => null;

        public CalenderUserType(string userType)
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
    /// Represents valid status values for a VEVENT, which are TENTATIVE, CONFIRMED, or CANCELLED
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