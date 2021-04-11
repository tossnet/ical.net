using System.Collections.Generic;
using Experiments.Utilities;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace UnitTests.Utilities
{
    public class EmailAddressUtilsTests
    {
        [Test, TestCaseSource(nameof(ParseWithMailtoTestCases))]
        public string ParseAndExtractEmailAddressTests(string e)
            => e.ParseAndExtractEmailAddress();
        
        public static IEnumerable<ITestCaseData> ParseWithMailtoTestCases()
        {
            yield return new TestCaseData("mailto:foo@example.com")
                .Returns("foo@example.com")
                .SetName("mailto:foo@example.com");
            yield return new TestCaseData("foo@example.com")
                .Returns("foo@example.com")
                .SetName("foo@example.com");
        }
    }
}