using System;
using System.Collections.Generic;
using Experiments.PropertyParameters;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace UnitTests.PropertyParameters
{
    public class InlineEncodingTests
    {
        [Test, TestCaseSource(nameof(InlineEncodingHappyTestCases))]
        public void InlineEncodingHappyTests(string encoding, string value, string expectedSerialization)
        {
            var inlineEncoding = new InlineEncoding(encoding, value);
            var serialized = inlineEncoding.ToString();
            Assert.AreEqual(expectedSerialization, serialized);
        }

        public static IEnumerable<ITestCaseData> InlineEncodingHappyTestCases()
        {
            yield return new TestCaseData(null, null, null);

            yield return new TestCaseData("8BIT", null, "ENCODING=8BIT")
                .SetName("ENCODING=8BIT");

            yield return new TestCaseData("8BIT", "someValue", "ENCODING=8BIT;VALUE=someValue")
                .SetName("ENCODING=8BIT;VALUE=someValue");

            yield return new TestCaseData("BASE64", "BINARY", "ENCODING=BASE64;VALUE=BINARY")
                .SetName("ENCODING=BASE64;VALUE=BINARY");
        }

        [Test, TestCaseSource(nameof(InlineEncodingUnhappyTestCases))]
        public void InlineEncodingUnhappyTests(string encoding, string value)
        {
            var ex = Assert.Throws<ArgumentException>(() => new InlineEncoding(encoding, value));
            Assert.IsTrue(ex is ArgumentException);
        }

        public static IEnumerable<ITestCaseData> InlineEncodingUnhappyTestCases()
        {
            yield return new TestCaseData("foo", null)
                .SetName("foo is not one of the two allowed encoding values");
            
            yield return new TestCaseData("8bit", "BINARY")
                .SetName("BINARY without BASE64 throws");
        }
    }
}