using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Experiments.PropertyParameters;

namespace UnitTests.PropertyParameters
{
    public class CalendarUserTypeTests
    {
        [Test, TestCaseSource(nameof(CalendarUserTypeUnhappyTestCases))]
        public void CalendarUserTypeUnhappyTests(string userType)
        {
            var ex = Assert.Throws<ArgumentException>(() => new CalendarUserType(userType));
            Assert.IsTrue(ex is ArgumentException);
        }
        
        public static IEnumerable<ITestCaseData> CalendarUserTypeUnhappyTestCases()
        {
            yield return new TestCaseData("nonsense")
                .SetName("nonsense value throws");
        }
        
        [Test, TestCaseSource(nameof(CalendarUserTypeTestCases))]
        public void CalendarUserTypeTest(string userType, string expected)
        {
            var cuType = new CalendarUserType(userType);
            Assert.AreEqual(cuType.ToString(), expected);
        }

        public static IEnumerable<ITestCaseData> CalendarUserTypeTestCases()
        {
            yield return new TestCaseData("", "CUTYPE=INDIVIDUAL")
                .SetName("Empty value is INDIVIDUAL");

            yield return new TestCaseData("GROUP", "CUTYPE=GROUP")
                .SetName("GROUP is GROUP");

            yield return new TestCaseData("X-FOO-BAR", "CUTYPE=X-FOO-BAR")
                .SetName("X- prefixed value is acceptable");
        }
    }
}