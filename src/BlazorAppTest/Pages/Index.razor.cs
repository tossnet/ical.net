using System;
using System.Collections.Generic;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

namespace BlazorAppTest.Pages
{
    public partial class Index
    {
        public string SerializedCalendar { get; set; }

        private void CreateICS()
        {
            // FIRST EVENT
            var now = DateTime.Now;
            var later = now.AddHours(1);

            //Repeat daily for 5 days
            var rrule = new RecurrencePattern(FrequencyType.Daily, 1) { Count = 5 };

            var e = new CalendarEvent
            {
                LastModified = new CalDateTime(now),
                Created = new CalDateTime(now),
                Start = new CalDateTime(now),
                End = new CalDateTime(later),
                Description ="Description",
                IsAllDay = false,
                Name = "VEVENT",
                Uid = "1234",
                Location = "LILLE",
                Summary = "summary",
                Url = new Uri("https://www.sodeasoft.net"),
                RecurrenceRules = new List<RecurrencePattern> { rrule },
            };

            var calendar = new Calendar();
            calendar.Events.Add(e);


            // 2ND EVENT
            now = DateTime.Now.AddDays(1);
            later = now.AddHours(1);

            e = new CalendarEvent
            {
                LastModified = new CalDateTime(now),
                Created = new CalDateTime(now),
                Start = new CalDateTime(now),
                End = new CalDateTime(later),
                Description = "Description 2",
                IsAllDay = false,
                Name = "VEVENT",
                Uid = "9874984",
                Location = "ARRAS",
                Summary = "summary",
                Url = new Uri("https://www.sodeasoft.net"),
            };
            calendar.Events.Add(e);

            var serializer = new CalendarSerializer();
            SerializedCalendar = serializer.SerializeToString(calendar);

            StateHasChanged();
        }
    }
}
