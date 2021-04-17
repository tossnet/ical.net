using System;
using System.Collections.Generic;
using Experiments.PropertyParameters;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace UnitTests.PropertyParameters
{
    public class MembershipTests
    {
        [Test, TestCaseSource(nameof(MembershipHappyTestCases))]
        public void MembershipHappyTests(
            IEnumerable<string> memberships,
            IEnumerable<string> expectedMemberships,
            bool expectedIsEmpty,
            string expectedSerialization)
        {
            var membership = new Membership(memberships);

            if (expectedMemberships is null)
            {
                Assert.IsNull(membership.Memberships);
            }
            else
            {
                CollectionAssert.AreEquivalent(membership.Memberships, expectedMemberships);
            }
            
            Assert.AreEqual(membership.IsEmpty, expectedIsEmpty);
            var serialized = membership.ToString();
            Assert.AreEqual(expectedSerialization, serialized);
        }

        public static IEnumerable<ITestCaseData> MembershipHappyTestCases()
        {
            yield return new TestCaseData(
                new[]{"mailto:rstockbower@example.com"},
                new[]{"rstockbower@example.com"},
                false,
                "MEMBER=\"mailto:rstockbower@example.com\"")
            .SetName("mailto:rstockbower@example.com");
            
            yield return new TestCaseData(
                new[]{"rstockbower@example.com"},
                new[]{"rstockbower@example.com"},
                false,
                "MEMBER=\"mailto:rstockbower@example.com\"")
            .SetName("rstockbower@example.com");
            
            yield return new TestCaseData(
                new[]{"rstockbower@example.com", "foo@example.com"},
                new[]{"rstockbower@example.com", "foo@example.com"},
                false,
                "MEMBER=\"mailto:rstockbower@example.com\",\"mailto:foo@example.com\"")
            .SetName("rstockbower@example.com & foo@example.com");
            
            yield return new TestCaseData(
                new[]{"rstockbower@example.com", "rstockbower@example.com"},
                new[]{"rstockbower@example.com"},
                false,
                "MEMBER=\"mailto:rstockbower@example.com\"")
            .SetName("Double rstockbower@example.com is de-duplicated");
            
            yield return new TestCaseData(
                new[]{" "},
                null,
                true,
                null)
            .SetName("whitespace is empty and serializes to null");

            yield return new TestCaseData(
                new[] {"mailto:projectA@example.com", "mailto:projectB@example.com"},
                new[] {"projectA@example.com", "projectB@example.com"},
                false,
                "MEMBER=\"mailto:projectA@example.com\",\"mailto:projectB@example.com\"")
            .SetName("mailto:projectA@example.com and mailto:projectB@example.com both serialize");
        }
    }
}