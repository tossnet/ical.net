using System;
using System.Collections.Generic;
using Experiments.PropertyParameters;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace UnitTests.PropertyParameters
{
    public class FreeBusyTimeTypeTests
    {
        [Test, TestCaseSource(nameof(FreeBusyTimeTypeHappyTestCases))]
        public void FreeBusyTimeTypeHappyTests(string fbTimeType, string expectedValue, bool expectedBusy, bool expectedEmpty)
        {
            var type = new FreeBusyTimeType(fbTimeType);
            Assert.AreEqual(expectedValue, type.Value);
            Assert.AreEqual(expectedBusy, type.IsBusy);
            Assert.AreEqual(expectedEmpty, type.IsEmpty);
        }
        
        public static IEnumerable<ITestCaseData> FreeBusyTimeTypeHappyTestCases()
        {
            yield return new TestCaseData("BUSY", "BUSY", true, false)
                .SetName("BUSY has value, and is BUSY");

            yield return new TestCaseData("FREE", "FREE", false, false)
                .SetName("FREE has value, and is not busy");

            yield return new TestCaseData("busy", "BUSY", true, false)
                .SetName("busy is normalized to BUSY, and is busy");

            yield return new TestCaseData("X-Foo", "X-Foo", true, false)
                .SetName("X-Foo is accepted and treated as BUSY");

            yield return new TestCaseData(" ", null, false, true)
                .SetName("Whitespace is treated as empty, with no serialized value");

            yield return new TestCaseData(null, null, false, true)
                .SetName("Null is treated as empty, with no serialized value");
        }
        
        [Test, TestCaseSource(nameof(FreeBusyTimeTypeUnhappyTestCases))]
        public void FreeBusyTimeTypeUnhappyTests(string fbTimeType)
        {
            Assert.Throws<ArgumentException>(() => new FreeBusyTimeType(fbTimeType));
        }

        public static IEnumerable<ITestCaseData> FreeBusyTimeTypeUnhappyTestCases()
        {
            yield return new TestCaseData("foo")
                .SetName("foo is not a recognized value and throws");
        }
    }
}