using System.Collections.Generic;
using Experiments.PropertyParameters;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace UnitTests.PropertyParameters
{
    public class DelegatedFromTests
    {
        [Test, TestCaseSource(nameof(DelegatedFromTestCases))]
        public void DelegatedFromTest(IEnumerable<string> emails, IEnumerable<string> expecteds, string expectedSerialization)
        {
            var delegates = new DelegatedFrom(emails);
            CollectionAssert.AreEquivalent(expecteds, delegates.Delegates);
            var serialized = delegates.ToString();
            Assert.AreEqual(expectedSerialization, serialized);
        }

        public static IEnumerable<ITestCaseData> DelegatedFromTestCases()
        {
            yield return new TestCaseData(
                new[]{"mailto:rstockbower@example.com"},
                new[]{"\"mailto:rstockbower@example.com\""},
                "DELEGATED-FROM=\"mailto:rstockbower@example.com\"")
            .SetName("mailto:rstockbower@example.com");

            yield return new TestCaseData(
                new[]{"rstockbower@example.com"},
                new[]{"\"mailto:rstockbower@example.com\""},
                "DELEGATED-FROM=\"mailto:rstockbower@example.com\"")
            .SetName("rstockbower@example.com");
            
            yield return new TestCaseData(
                new[]{"rstockbower@example.com", "foo@example.com"},
                new[]{"\"mailto:rstockbower@example.com\"", "\"mailto:foo@example.com\""},
                "DELEGATED-FROM=\"mailto:rstockbower@example.com\",\"mailto:foo@example.com\"")
            .SetName("rstockbower@example.com & foo@example.com");
            
            yield return new TestCaseData(
                new[]{"rstockbower@example.com", "rstockbower@example.com"},
                new[]{"\"mailto:rstockbower@example.com\""},
                "DELEGATED-FROM=\"mailto:rstockbower@example.com\"")
            .SetName("Double rstockbower@example.com is de-duplicated");
        }
    }
}