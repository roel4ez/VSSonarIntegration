using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using VSSonarIntegration.Issues.Services;

namespace VSSonarIntegration.Tests
{
    [TestClass]
    public class SonarServiceTest
    {
        private ISonarService serviceUnderTest;

        [TestInitialize]
        public void Init()
        {
            serviceUnderTest = new SonarService();
        }

        [TestMethod]
        public void TestMethod1()
        {
            serviceUnderTest.GetIssues("fau");

        }
    }
}
