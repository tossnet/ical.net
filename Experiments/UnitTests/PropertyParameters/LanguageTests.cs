using System;
using System.Collections.Generic;
using Experiments.PropertyParameters;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace UnitTests.PropertyParameters
{
    public class LanguageTests
    {
        [Test, TestCaseSource(nameof(LanguageHappyTestCases))]
        public void LanguageHappyTests(string language, bool expectedIsEmpty, string expectedValue, string expectedSerialized)
        {
            var lang = new Language(language);
            Assert.AreEqual(expectedIsEmpty, lang.IsEmpty);
            Assert.AreEqual(expectedValue, lang.Value);
            Assert.AreEqual(expectedSerialized, lang.ToString());
        }
        
        public static IEnumerable<ITestCaseData> LanguageHappyTestCases()
        {
            yield return new TestCaseData("en-GB", false, "en-GB", "LANGUAGE=en-GB")
                .SetName("en-GB");

            yield return new TestCaseData("en-gb", false, "en-GB", "LANGUAGE=en-GB")
                .SetName("en-gb");

            yield return new TestCaseData(null, true, null, null)
                .SetName("null is empty and doesn't serialize");
            
            yield return new TestCaseData(" ", true, null, null)
                .SetName("Whitespace is empty and doesn't serialize");
        }

        [Test, TestCaseSource(nameof(LanguageUnhappyTestCases))]
        public void LanguageUnhappyTests(string language)
        {
            Assert.Throws<ArgumentException>(() => new Language(language));
        }

        public static IEnumerable<ITestCaseData> LanguageUnhappyTestCases()
        {
            yield return new TestCaseData("nope")
                .SetName("nope throws");
        }
    }
}