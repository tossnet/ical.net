using System;
using System.Collections.Generic;
using Experiments.PropertyParameters;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace UnitTests.PropertyParameters
{
    public class RelationshipTypeTests
    {
        [Test, TestCaseSource(nameof(RelationshipTypeHappyTestCases))]
        public void RelationshipTypeHappyTests(
            string relationshipType,
            string expectedValue,
            string expectedSerialization,
            bool expectedIsEmpty)
        {
            var relType = new RelationshipType(relationshipType);
            Assert.AreEqual(expectedValue, relType.Value);
            Assert.AreEqual(expectedIsEmpty, relType.IsEmpty);
            Assert.AreEqual(expectedSerialization, relType.ToString());
        }
        
        public static IEnumerable<ITestCaseData> RelationshipTypeHappyTestCases()
        {
            yield return new TestCaseData("PARENT", "PARENT", "RELTYPE=PARENT", false)
                .SetName("PARENT is valid");

            yield return new TestCaseData("parent", "PARENT", "RELTYPE=PARENT", false)
                .SetName("parent is normalized to PARENT");

            yield return new TestCaseData("X-Foo", "X-Foo", "RELTYPE=X-Foo", false)
                .SetName("X-Foo is an allowed value");

            yield return new TestCaseData(" ", null, null, true)
                .SetName("Whitespace is normalized to null");
        }
        
        [Test]
        public void ParticipationStatusUnhappyTests()
        {
            Assert.Throws<ArgumentException>(() => new RelationshipType("invalid value"));
        }
    }
}