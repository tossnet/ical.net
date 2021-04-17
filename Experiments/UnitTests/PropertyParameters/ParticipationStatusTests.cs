using System;
using System.Collections.Generic;
using Experiments.PropertyParameters;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace UnitTests.PropertyParameters
{
    public class ParticipationStatusTests
    {
        [Test, TestCaseSource(nameof(ParticipationStatusHappyTestCases))]
        public void ParticipationStatusHappyTests(string participationStatus, string expectedValue, string expectedSerialization, bool expectedIsEmpty)
        {
            var partStat = new ParticipationStatus(participationStatus);
            Assert.AreEqual(expectedValue, partStat.Value);
            Assert.AreEqual(expectedIsEmpty, partStat.IsEmpty);
            var serialized = partStat.ToString();
            Assert.AreEqual(expectedSerialization, serialized);
        }
        
        public static IEnumerable<ITestCaseData> ParticipationStatusHappyTestCases()
        {
            yield return new TestCaseData("TENTATIVE", "TENTATIVE", "PARTSTAT=TENTATIVE", false)
                .SetName("TENTATIVE is valid");

            yield return new TestCaseData("tentative", "TENTATIVE", "PARTSTAT=TENTATIVE", false)
                .SetName("tentative is normalized to TENTATIVE");

            yield return new TestCaseData("X-Foo", "X-Foo", "PARTSTAT=X-Foo", false)
                .SetName("X-Foo is an allowed value");

            yield return new TestCaseData(" ", null, null, true)
                .SetName("Whitespace is normalized to null");
        }

        [Test]
        public void ParticipationStatusUnhappyTests()
        {
            Assert.Throws<ArgumentException>(() => new ParticipationStatus("invalid value"));
        }
    }
}