using LantanaGroup.TestDataGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using LantanaGroup.TestDataGenerator.Shared;

namespace TestDataGenerationToolTests
{
    
    
    /// <summary>
    ///This is a test class for LogFactoryTest and is intended
    ///to contain all LogFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LogFactoryTest
    {
        private static XmlDocument errorLogDoc = null;
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            errorLogDoc = new XmlDocument();
            LogFactory.errorLogRoot = errorLogDoc.CreateElement("root");
            errorLogDoc.AppendChild(LogFactory.errorLogRoot);

            LogFactory.errorLogDoc = errorLogDoc;
        }
        
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
        }
        
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
        }
        
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
        }
        
        #endregion

        /// <summary>
        /// A test for Log
        /// </summary>
        [TestMethod()]
        public void LogTest()
        {
            errorLogDoc.RemoveChild(LogFactory.errorLogRoot);
            LogFactory.errorLogRoot = errorLogDoc.CreateElement("root");
            errorLogDoc.AppendChild(LogFactory.errorLogRoot);

            string file = "testFile.xml";
            string messageFormat = "testMessage {0}";
            object[] formatArgs = new object[] { "1" };
            string message = string.Format(messageFormat, formatArgs);

            LogFactory.Log(LogFactory.Severities.Error, LogFactory.MessageTypes.Generation, file, true, messageFormat, formatArgs);
            LogFactory.Log(LogFactory.Severities.Warning, LogFactory.MessageTypes.ContentValidation, file, true, messageFormat, formatArgs);
            LogFactory.Log(LogFactory.Severities.Info, LogFactory.MessageTypes.SchemaValidation, file, true, messageFormat, formatArgs);

            XmlNode messageNode1 = errorLogDoc.DocumentElement.SelectSingleNode("message[1]");
            XmlNode messageNode2 = errorLogDoc.DocumentElement.SelectSingleNode("message[2]");
            XmlNode messageNode3 = errorLogDoc.DocumentElement.SelectSingleNode("message[3]");

            Assert.IsNotNull(messageNode1);
            Assert.IsNotNull(messageNode1.Attributes["severity"]);
            Assert.AreEqual("Error", messageNode1.Attributes["severity"].Value);
            Assert.IsNotNull(messageNode1.Attributes["type"]);
            Assert.AreEqual("Generation", messageNode1.Attributes["type"].Value);
            Assert.IsNotNull(messageNode1.Attributes["message"]);
            Assert.AreEqual(message, messageNode1.Attributes["message"].Value);

            Assert.IsNotNull(messageNode2);
            Assert.IsNotNull(messageNode2.Attributes["severity"]);
            Assert.AreEqual("Warning", messageNode2.Attributes["severity"].Value);
            Assert.IsNotNull(messageNode2.Attributes["type"]);
            Assert.AreEqual("Content Validation", messageNode2.Attributes["type"].Value);

            Assert.IsNotNull(messageNode3);
            Assert.IsNotNull(messageNode3.Attributes["severity"]);
            Assert.AreEqual("Information", messageNode3.Attributes["severity"].Value);
            Assert.IsNotNull(messageNode3.Attributes["type"]);
            Assert.AreEqual("Schema Validation", messageNode3.Attributes["type"].Value);
        }

        /// <summary>
        /// A test for Log
        /// </summary>
        [TestMethod()]
        public void LogTest1()
        {
            errorLogDoc.RemoveChild(LogFactory.errorLogRoot);
            LogFactory.errorLogRoot = errorLogDoc.CreateElement("root");
            errorLogDoc.AppendChild(LogFactory.errorLogRoot);

            string file = "testFile.xml";
            string messageFormat = "testMessage {0}";
            object[] formatArgs = new object[] { "1" };
            string message = string.Format(messageFormat, formatArgs);

            LogFactory.Log(LogFactory.Severities.Error, file, 10, 11, message);

            XmlNode messageNode = errorLogDoc.DocumentElement.SelectSingleNode("message[1]");

            Assert.IsNotNull(messageNode);

            XmlNode locationNode = messageNode.SelectSingleNode("location");

            Assert.IsNotNull(locationNode);
            Assert.IsNotNull(locationNode.Attributes["line"]);
            Assert.AreEqual("10", locationNode.Attributes["line"].Value);
            Assert.IsNotNull(locationNode.Attributes["position"]);
            Assert.AreEqual("11", locationNode.Attributes["position"].Value);
        }

        /// <summary>
        /// A test for Log
        /// </summary>
        [TestMethod()]
        public void LogTest2()
        {
            errorLogDoc.RemoveChild(LogFactory.errorLogRoot);
            LogFactory.errorLogRoot = errorLogDoc.CreateElement("root");
            errorLogDoc.AppendChild(LogFactory.errorLogRoot);

            string file = "testFile.xml";
            string messageFormat = "testMessage {0}";
            object[] formatArgs = new object[] { "1" };
            string message = string.Format(messageFormat, formatArgs);
            string context = "testContext";
            string test = "testTest";

            LogFactory.Log(LogFactory.Severities.Error, file, context, test, message);

            XmlNode messageNode = errorLogDoc.DocumentElement.SelectSingleNode("message[1]");

            Assert.IsNotNull(messageNode);

            XmlNode contextNode = messageNode.SelectSingleNode("context");
            Assert.IsNotNull(contextNode);
            Assert.AreEqual(context, contextNode.InnerText);

            XmlNode testNode = messageNode.SelectSingleNode("test");
            Assert.IsNotNull(testNode);
            Assert.AreEqual(test, testNode.InnerText);
        }
    }
}
