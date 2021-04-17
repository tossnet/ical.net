using System;
using System.Collections.Generic;
using Experiments.PropertyParameters;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace UnitTests.PropertyParameters
{
    public class AlarmTriggerRelationshipTests
    {
        [Test, TestCaseSource(nameof(AlarmTriggerRelationshipHappyTestCases))]
        public void AlarmTriggerRelationshipHappyTests(string relationship, string expectedValue, string expectedSerialization, bool expectedEmpty)
        {
            var alarmTriggerRelationship = new AlarmTriggerRelationship(relationship);
            Assert.AreEqual(expectedValue, alarmTriggerRelationship.Value);
            Assert.AreEqual(expectedEmpty, alarmTriggerRelationship.IsEmpty);
            Assert.AreEqual(expectedSerialization, alarmTriggerRelationship.ToString());
        }
        
        public static IEnumerable<ITestCaseData> AlarmTriggerRelationshipHappyTestCases()
        {
            yield return new TestCaseData("START", "START", "RELATED=START", false)
                .SetName("START is not empty");
            
            yield return new TestCaseData("start", "START", "RELATED=START", false)
                .SetName("start is normalized to START, and is not empty");

            yield return new TestCaseData("end", "END", "RELATED=END", false)
                .SetName("end is normalized to END, and is not empty");
            
            yield return new TestCaseData(null, null, null, true)
                .SetName("null is empty");

            yield return new TestCaseData("  ", null, null, true)
                .SetName("Whitespace is empty");
        }

        [Test]
        public void AlarmTriggerRelationshipUnhappyTests()
        {
            Assert.Throws<ArgumentException>(() => new AlarmTriggerRelationship("lskjdflksdjf"));
        }
    }
}