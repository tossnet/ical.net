using System;
using Experiments.PropertyParameters;
using NUnit.Framework;

namespace UnitTests.PropertyParameters
{
    public class AltRepTests
    {
        [Test]
        public void UriTests()
        {
            var altRep = new AltRep("CID:part3.msg.970415T083000@example.com");
            var serialized = altRep.ToString();
            const string expected = "ALTREP=\"CID:part3.msg.970415T083000@example.com\"";
            
            // Some URIs have their casing normalized when instantiated
            var areSame = string.Equals(expected, serialized, StringComparison.OrdinalIgnoreCase);
            Assert.IsTrue(areSame);
        }
    }
}