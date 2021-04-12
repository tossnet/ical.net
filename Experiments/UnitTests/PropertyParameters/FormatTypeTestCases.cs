using System;
using System.Collections.Generic;
using Experiments.PropertyParameters;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace UnitTests.PropertyParameters
{
    public class FormatTypeTestCases
    {
        [Test, TestCaseSource(nameof(FormatTypeHappyTestCases))]
        public string FormatTypeHappyTests(string mediaType)
        {
            var ft = new FormatType(mediaType);
            return ft.ToString();
        }

        public static IEnumerable<ITestCaseData> FormatTypeHappyTestCases()
        {
            yield return new TestCaseData("application/msword")
                .Returns("FMTTYPE=application/msword")
                .SetName("FMTTYPE=application/msword");

            yield return new TestCaseData("application/3gpdash-qoe-report+xml")
                .Returns("FMTTYPE=application/3gpdash-qoe-report+xml")
                .SetName("FMTTYPE=application/3gpdash-qoe-report+xml");
        }
        
        [Test, TestCaseSource(nameof(FormatTypeUnhappyTestCases))]
        public void FormatTypeUnhappyTests(string mediaType)
        {
            Assert.Throws<ArgumentException>(() => new FormatType(mediaType));
        }
        
        public static IEnumerable<ITestCaseData> FormatTypeUnhappyTestCases()
        {
            yield return new TestCaseData("applic+ation/msword")
                .SetName("applic+ation/msword throws");

            yield return new TestCaseData("application/foo/bar")
                .SetName("application/foo/bar throws");
        }
    }
}