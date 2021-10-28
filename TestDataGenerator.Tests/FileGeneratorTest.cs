using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using LantanaGroup.TestDataGenerator.Shared.Logic;
using LantanaGroup.TestDataGenerator.Shared.Data;

namespace TestDataGenerationToolTests
{
    
    
    /// <summary>
    ///This is a test class for FileGeneratorTest and is intended
    ///to contain all FileGeneratorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FileGeneratorTest
    {


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for ParseRowInfo
        ///</summary>
        [TestMethod()]
        public void ParseRowInfoTest()
        {
            string rowInfo = "1,2,3-4,7-5,8,10";
            List<int> actual = FileGenerator.ParseRowInfo(rowInfo);

            Assert.IsTrue(actual.Contains(1));
            Assert.IsTrue(actual.Contains(2));
            Assert.IsTrue(actual.Contains(3));
            Assert.IsTrue(actual.Contains(4));
            Assert.IsTrue(actual.Contains(5));
            Assert.IsTrue(actual.Contains(6));
            Assert.IsTrue(actual.Contains(7));
            Assert.IsTrue(actual.Contains(8));
            Assert.IsTrue(actual.Contains(10));
            Assert.IsFalse(actual.Contains(9));
        }

        /// <summary>
        ///A test for GenerateRandomHits
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TestDataGenerationTool.exe")]
        public void GenerateRandomHitsTest()
        {
            ActionConfiguration config = new ActionConfiguration();

            ActionConfigurationBad badInstances = new ActionConfigurationBad();

            badInstances.instances = new List<Instance>() {
                    new Instance()
                    {
                        filter = new InstanceFilter()
                        {
                            Item = new InstanceFilterRandomRowCount()
                            {
                                maxOccur = 5
                            }
                        }
                    } 
            };
            
            config.bad = new List<ActionConfigurationBad>() { badInstances };

            int numOutputFiles = 10;
            Dictionary<Instance, List<int>> actual = FileGenerator.GenerateRandomHits(config, numOutputFiles);

            Assert.AreEqual(1, actual.Count);

            // TODO: Fix
            // Assert.AreEqual(5, actual[0].);
        }
    }
}
