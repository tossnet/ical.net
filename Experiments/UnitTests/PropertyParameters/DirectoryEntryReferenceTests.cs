using System.Collections.Generic;
using Experiments.PropertyParameters;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace UnitTests.PropertyParameters
{
    public class DirectoryEntryReferenceTests
    {
        [Test, TestCaseSource(nameof(DirectoryEntryTestCases))]
        public string DirectoryEntryTesting(string value) => new DirectoryEntryReference(value).ToString();
        
        public static IEnumerable<ITestCaseData> DirectoryEntryTestCases()
        {
            yield return new TestCaseData("ldap://example.com:6666/o=ABC%20Industries,c=US???(cn=Jim%20Dolittle)")
                .SetName("LDAP")
                .Returns("DIR=\"ldap://example.com:6666/o=ABC%20Industries,c=US???(cn=Jim%20Dolittle)\"");
        }
    }
}