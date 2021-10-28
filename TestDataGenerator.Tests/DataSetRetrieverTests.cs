using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using LantanaGroup.TestDataGenerator.Shared.Data;
using LantanaGroup.TestDataGenerator.Shared.Logic;


namespace LantanaGroup.TestDataGenerator
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class DataSetRetrieverTests
    {
        public DataSetRetrieverTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestGetFromXLS()
        {
            string path = Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            FileInfo inputFile = new FileInfo(path.Substring(6) + "\\data\\TestDataSource.xls");

            SampleDataSet dataSet = SampleDataSetRetriever.GetFromXLS(inputFile);

            //Check the set
            Assert.AreEqual(11, dataSet.Sections.Count, "Should be elleven sections");
            Assert.IsTrue(dataSet.Sections.ContainsKey("Header"));
            Assert.IsTrue(dataSet.Sections.ContainsKey("Results"));
            Assert.IsTrue(dataSet.Sections.ContainsKey("AdvanceDirective"));
            Assert.IsTrue(dataSet.Sections.ContainsKey("VitalSigns"));
            Assert.IsTrue(dataSet.Sections.ContainsKey("Procedures"));
            Assert.IsTrue(dataSet.Sections.ContainsKey("Problems"));
            Assert.IsTrue(dataSet.Sections.ContainsKey("PlanOfCare"));
            Assert.IsTrue(dataSet.Sections.ContainsKey("Payers"));
            Assert.IsTrue(dataSet.Sections.ContainsKey("Encounters"));
            Assert.IsTrue(dataSet.Sections.ContainsKey("Allergies"));

            Assert.AreEqual(51, dataSet.Sections["Header"].Data.Length, "Incorrect number of rows in first section");
            Assert.AreEqual(8, dataSet.Sections["Results"].Data.Length, "Incorrect number of rows in second section");

            Assert.AreEqual("Jones", dataSet.Sections["Header"].Data[0][0], "Incorrect first value in first section");
            Assert.AreEqual("Virgil", dataSet.Sections["Header"].Data[50][45], "Incorrect last value in first section");


            //Check the configuration
            Assert.AreEqual(151, dataSet.Tokens.Count, "Should be 151 Tokens");
            Assert.AreEqual("Header", dataSet.Tokens["%Header_Given%"].SectionName, "First configuration entry section name incorrect");
            Assert.AreEqual(2, dataSet.Tokens["%Header_Given%"].Column, "First configuration entry column incorrect");

            Assert.AreEqual("AdvanceDirective", dataSet.Tokens["%AdvanceDirective_ObservationId%"].SectionName, "Section configuration entry section name incorrect");
            Assert.AreEqual(1, dataSet.Tokens["%AdvanceDirective_ObservationId%"].Column, "Second configuration entry column incorrect");
        }
    }
}
