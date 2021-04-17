using System;
using System.Collections.Generic;
using Experiments.Utilities;

namespace Experiments.PropertyParameters
{
    /// <summary>
    /// Parameter Name:  PARTSTAT
    ///
    /// To specify the participation status for the calendar user specified by the property.
    ///
    /// Format Definition:  This property parameter is defined by the following notation:
    ///     partstatparam    = "PARTSTAT" "="
    ///                     (partstat-event
    ///                     / partstat-todo
    ///                     / partstat-jour)
    ///
    /// partstat-event   = ("NEEDS-ACTION"    ; Event needs action
    ///                     / "ACCEPTED"      ; Event accepted
    ///                     / "DECLINED"      ; Event declined
    ///                     / "TENTATIVE"     ; Event tentatively accepted
    ///                     / "DELEGATED"     ; Event delegated
    ///                     / x-name          ; Experimental status
    ///                     / iana-token)     ; Other IANA-registered status
    /// These are the participation statuses for a "VEVENT". Default is NEEDS-ACTION.
    ///
    /// partstat-todo    = ("NEEDS-ACTION"    ; To-do needs action
    ///                     / "ACCEPTED"      ; To-do accepted
    ///                     / "DECLINED"      ; To-do declined
    ///                     / "TENTATIVE"     ; To-do tentatively accepted
    ///                     / "DELEGATED"     ; To-do delegated
    ///                     / "COMPLETED"     ; To-do completed COMPLETED property has DATE-TIME completed
    ///                     / "IN-PROCESS"    ; To-do in process of being completed
    ///                     / x-name          ; Experimental status
    ///                     / iana-token)     ; Other IANA-registered status
    /// These are the participation statuses for a "VTODO". Default is NEEDS-ACTION.
    /// 
    /// partstat-jour    = ("NEEDS-ACTION"    ; Journal needs action
    ///                     / "ACCEPTED"         ; Journal accepted
    ///                     / "DECLINED"         ; Journal declined
    ///                     / x-name             ; Experimental status
    ///                     / iana-token)        ; Other IANA-registered status
    /// These are the participation statuses for a "VJOURNAL". Default is NEEDS-ACTION.
    ///
    /// This parameter can be specified on properties with a CAL-ADDRESS value type.  The parameter identifies the participation status for the calendar user
    /// specified by the property value.  The parameter values differ depending on whether they are associated with a group-scheduled "VEVENT", "VTODO", or
    /// "VJOURNAL".  The values MUST match one of the values allowed for the given calendar component.  If not specified on a property that allows this
    /// parameter, the default value is NEEDS-ACTION. Applications MUST treat x-name and iana-token values they don't recognize the same way as they would the
    /// NEEDS-ACTION value.
    ///
    /// https://tools.ietf.org/html/rfc5545#section-3.2.12
    /// </summary>
    /// <example>
    /// ATTENDEE;PARTSTAT=DECLINED:mailto:jsmith@example.com
    /// </example>
    public readonly struct ParticipationStatus :
        IValueType
    {
        public string Name => "PARTSTAT";
        public string Value { get; }
        public bool IsEmpty => Value is null;

        public ParticipationStatus(string participationStatus)
        {
            if (string.IsNullOrWhiteSpace(participationStatus))
            {
                Value = null;
            }

            Value = ParticipationStatuses.GetEventStatus(participationStatus);
        }

        public override string ToString() => this.NameEqualsValue();
    }

    public static class ParticipationStatuses
    {
        public const string NeedsAction = "NEEDS-ACTION";
        public const string Accepted = "ACCEPTED";
        public const string Declined = "DECLINED";
        public const string Delegated = "DELEGATED";
        public const string Tentative = "TENTATIVE";
        public const string Completed = "COMPLETED";
        public const string InProcess = "IN-PROCESS";
        
        /// <summary>
        /// The default status for calendar entry types
        /// </summary>
        public const string Default = NeedsAction;
        
        private static readonly Dictionary<string, string> _allStatuses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            [NeedsAction] = NeedsAction,
            [Accepted] = Accepted,
            [Declined] = Declined,
            [Tentative] = Tentative,
            [Delegated] = Delegated,
            [Completed] = Completed,
            [InProcess] = InProcess,
        };

        /// <summary>
        /// Returns a normalized version of the participation status for a VTODO
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">When status is not an allowed value</exception>
        public static string GetValidStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return null;
            }

            if (_allStatuses.TryGetValue(status, out var normalized))
            {
                return normalized;
            }

            if (status.StartsWith("X-", StringComparison.OrdinalIgnoreCase))
            {
                return status;
            }

            throw new ArgumentException($"{status} does not appear to be a valid VEVENT, VTODO, or VJOURNAL status");
        }
        

        private static readonly Dictionary<string, string> _eventStatuses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            [NeedsAction] = NeedsAction,
            [Accepted] = Accepted,
            [Declined] = Declined,
            [Tentative] = Tentative,
            [Delegated] = Delegated,
        };

        /// <summary>
        /// Returns a normalized version of the participation status for a VEVENT
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">When status is not an allowed value</exception>
        public static string GetEventStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return null;
            }

            if (_eventStatuses.TryGetValue(status, out var normalized))
            {
                return normalized;
            }

            if (status.StartsWith("X-", StringComparison.OrdinalIgnoreCase))
            {
                return status;
            }

            throw new ArgumentException($"{status} does not appear to be a valid VEVENT status");
        }
        
        private static readonly Dictionary<string, string> _todoStatuses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            [NeedsAction] = NeedsAction,
            [Accepted] = Accepted,
            [Declined] = Declined,
            [Tentative] = Tentative,
            [Delegated] = Delegated,
            [Completed] = Completed,
            [InProcess] = InProcess,
        };

        /// <summary>
        /// Returns a normalized version of the participation status for a VTODO
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">When status is not an allowed value</exception>
        public static string GetTodoStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return null;
            }

            if (_todoStatuses.TryGetValue(status, out var normalized))
            {
                return normalized;
            }

            if (status.StartsWith("X-", StringComparison.OrdinalIgnoreCase))
            {
                return status;
            }

            throw new ArgumentException($"{status} does not appear to be a valid VTODO status");
        }
        
        private static readonly Dictionary<string, string> _journalStatuses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            [NeedsAction] = NeedsAction,
            [Accepted] = Accepted,
            [Declined] = Declined,
        };

        /// <summary>
        /// Returns a normalized version of the participation status for a VJOURNAL
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">When status is not an allowed value</exception>
        public static string GetJournalStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return null;
            }

            if (_journalStatuses.TryGetValue(status, out var normalized))
            {
                return normalized;
            }

            if (status.StartsWith("X-", StringComparison.OrdinalIgnoreCase))
            {
                return status;
            }

            throw new ArgumentException($"{status} does not appear to be a valid VJOURNAL status");
        }
    }
}