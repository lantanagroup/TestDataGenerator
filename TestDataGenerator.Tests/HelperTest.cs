using LantanaGroup.TestDataGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using LantanaGroup.TestDataGenerator.Shared;
using LantanaGroup.TestDataGenerator.Shared.Data;

namespace TestDataGenerationToolTests
{
    /// <summary>
    ///This is a test class for HelperTest and is intended
    ///to contain all HelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HelperTest
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

        [TestMethod()]
        public void CloneTest()
        {
            Instance testInstance = new Instance();
            testInstance.name = "test1";

            Instance clonedTestInstance = Helper.Clone<Instance>(testInstance);
            clonedTestInstance.name = "test2";

            Assert.AreNotEqual(testInstance.name, clonedTestInstance.name);
        }

        /// <summary>
        ///A test for MakeAbsolutePath
        ///</summary>
        [TestMethod()]
        public void MakeAbsolutePathTest()
        {
            string matchFileName = "c:\\test\\test1.xml";
            string input = "test2.xls";
            string expected = "c:\\test\\test2.xls";
            string actual = Helper.MakeAbsolutePath(matchFileName, input);
            Assert.AreEqual(expected, actual);

            matchFileName = "c:\\test\\test1.xml";
            input = "c:\\test1\\test2\\test2.xls";
            actual = Helper.MakeAbsolutePath(matchFileName, input);
            Assert.AreEqual(input, actual);

            matchFileName = "c:\\test\\test1.xml";
            input = "Driver={Microsoft Excel Driver (*.xls)};driverid=790;dbq=TestDataSource.xls";
            expected = "Driver={Microsoft Excel Driver (*.xls)};driverid=790;dbq=c:\\test\\TestDataSource.xls";
            actual = Helper.MakeAbsolutePath(matchFileName, input);
            Assert.AreEqual(expected, actual);

            matchFileName = "c:\\test\\test1.xml";
            input = "Driver={Microsoft Excel Driver (*.xls)};driverid=790;dbq=c:\\test\\TestDataSource.xls";
            actual = Helper.MakeAbsolutePath(matchFileName, input);
            Assert.AreEqual(input, actual);

            matchFileName = "c:\\test\\test1.xml";
            input = "Driver={Microsoft Excel Driver (*.xls)};driverid=790;dbq=c:\\test1\\test2\\TestDataSource.xls";
            actual = Helper.MakeAbsolutePath(matchFileName, input);
            Assert.AreEqual(input, actual);
        }

        /// <summary>
        ///A test for MakeRelativePath
        ///</summary>
        [TestMethod()]
        public void MakeRelativePathTest()
        {
            string matchFileName = "C:\\test\\test1.xml";
            string input = "c:\\test\\test2.xls";
            string expected = "test2.xls";
            string actual = Helper.MakeRelativePath(matchFileName, input);
            Assert.AreEqual(expected, actual);

            matchFileName = "C:\\test\\test1.xml";
            input = "c:\\test1\\test2\\test2.xls";
            actual = Helper.MakeRelativePath(matchFileName, input);
            Assert.AreEqual(input, actual);

            matchFileName = "C:\\test\\test1.xml";
            input = "Driver={Microsoft Excel Driver (*.xls)};driverid=790;dbq=c:\\test\\TestDataSource.xls";
            expected = "Driver={Microsoft Excel Driver (*.xls)};driverid=790;dbq=TestDataSource.xls";
            actual = Helper.MakeRelativePath(matchFileName, input);
            Assert.AreEqual(expected, actual);

            matchFileName = "C:\\test\\test1.xml";
            input = "Driver={Microsoft Excel Driver (*.xls)};driverid=790;dbq=TestDataSource.xls";
            actual = Helper.MakeRelativePath(matchFileName, input);
            Assert.AreEqual(input, actual);

            matchFileName = "C:\\test\\test1.xml";
            input = "Driver={Microsoft Excel Driver (*.xls)};driverid=790;dbq=c:\\test1\\test2\\TestDataSource.xls";
            actual = Helper.MakeRelativePath(matchFileName, input);
            Assert.AreEqual(input, actual);
        }
    }
}
