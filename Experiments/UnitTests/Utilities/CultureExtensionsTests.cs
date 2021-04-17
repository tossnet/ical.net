using System;
using System.Collections.Generic;
using Experiments.Utilities;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace UnitTests.Utilities
{
    public class CultureExtensionsTests
    {
        [Test, TestCaseSource(nameof(GetNormalizedIetfLanguageTagHappyTestCases))]
        public string GetNormalizedIetfLanguageTagHappyTests(string languageTag)
            => CultureExtensions.GetNormalizedLanguageTag(languageTag);
        
        public static IEnumerable<ITestCaseData> GetNormalizedIetfLanguageTagHappyTestCases()
        {
            yield return new TestCaseData("en-GB")
                .Returns("en-GB")
                .SetName("en-GB");

            yield return new TestCaseData("en-gb")
                .Returns("en-GB")
                .SetName("en-gb returns en-GB");
        }
        
        [Test, TestCaseSource(nameof(GetNormalizedIetfLanguageTagUnhappyTestCases))]
        public void GetNormalizedIetfLanguageTagUnhappyTests(string languageTag)
        {
            Assert.Throws<ArgumentException>(() => CultureExtensions.GetNormalizedLanguageTag(languageTag));
        }

        public static IEnumerable<ITestCaseData> GetNormalizedIetfLanguageTagUnhappyTestCases()
        {
            yield return new TestCaseData("foo")
                .SetName("foo throws");
        }
    }
}