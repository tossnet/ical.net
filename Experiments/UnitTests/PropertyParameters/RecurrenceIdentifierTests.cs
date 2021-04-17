using System;
using System.Collections.Generic;
using Experiments.PropertyParameters;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace UnitTests.PropertyParameters
{
    public class RecurrenceIdentifierTests
    {
        [Test, TestCaseSource(nameof(RecurrenceRangeIdentifierHappyTestCases))]
        public void RecurrenceRangeIdentifierHappyTests(string range, string expectedValue, bool expectedIsEmpty, string expectedSerialization)
        {
            var rangeId = new RecurrenceIdentifierRange(range);
            Assert.AreEqual(expectedValue, rangeId.Value);
            Assert.AreEqual(expectedIsEmpty, rangeId.IsEmpty);
            Assert.AreEqual(expectedSerialization, rangeId.ToString());
        }

        public static IEnumerable<ITestCaseData> RecurrenceRangeIdentifierHappyTestCases()
        {
            const string thisAndFuture = "THISANDFUTURE";
            yield return new TestCaseData(thisAndFuture, thisAndFuture, false, $"RANGE={thisAndFuture}")
                .SetName(thisAndFuture + " is valid");

            yield return new TestCaseData(" ", null, true, null)
                .SetName("Whitespace is allowed but produces no serialized output and is empty");
        }

        [Test]
        public void RecurrenceRangeIdentifierUnhappyTests()
        {
            Assert.Throws<ArgumentException>(() => new RecurrenceIdentifierRange("invalid"));
        }
    }
}