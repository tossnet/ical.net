using System.Collections.Generic;
using Experiments.PropertyParameters;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace UnitTests.PropertyParameters
{
    public class CommonNameTests
    {
        [Test, TestCaseSource(nameof(CommonNameTestCases))]
        public void CommonNameTest(string name, bool isEmpty, string expected)
        {
            var cn = new CommonName(name);
            var serialized = cn.ToString();
            Assert.AreEqual(cn.IsEmpty, isEmpty);
            var areSame = string.Equals(serialized, expected);
            Assert.IsTrue(areSame);
        }

        public static IEnumerable<ITestCaseData> CommonNameTestCases()
        {
            yield return new TestCaseData("Rian Stockbower", false, "CN=\"Rian Stockbower\"")
                .SetName("Rian Stockbower is not empty");

            yield return new TestCaseData("  ", true, null)
                .SetName("Whitespace is empty and null");
        }
    }
}