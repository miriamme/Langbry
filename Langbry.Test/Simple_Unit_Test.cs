using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Linq;

namespace Langbry.Test
{
    [TestClass]
    public class Simple_Unit_Test
    {
        private TestContext testContextInstance;

        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        public void TestMethod1()
        {
            TestContext.WriteLine("this is testing message.");

            List<Dic> list = new List<Dic>
            {
                new Dic { Code = "F", Name = "first" },
                new Dic { Code = "S", Name = "second" },
                new Dic { Code = "L", Name = "last" }
            };

            LanguageTable table = new LanguageTable(list.ToDictionary(d => d.Code, n => n.Name));
            Assert.AreEqual(3, table.LanguageDictionary.Count);
        }
    }

    public class Dic
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}