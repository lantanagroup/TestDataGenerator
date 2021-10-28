using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using System.IO;

using LantanaGroup.TestDataGenerator.Shared.Logic;
using LantanaGroup.TestDataGenerator.Shared.Data;

namespace LantanaGroup.TestDataGenerator.Test
{
    
    
    /// <summary>
    ///This is a test class for ActionPerformerTest and is intended
    ///to contain all ActionPerformerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ActionPerformerTest
    {
        private static ActionPerformer performer = null;

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
            XmlDocument document = GetTestMasterFile();
            Instance instance = new Instance();
            performer = new ActionPerformer(document, null, instance);
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
        //
        #endregion

        private static XmlDocument GetTestMasterFile()
        {
            Stream s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("TestDataGenerationToolTests.TestMasterFile.xml");

            if (s != null)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(s);

                return doc;
            }
            else
            {
                Assert.Inconclusive("Embedded Resource TestMasterFile.xml cannot be loaded.");
            }

            return null;
        }

        /// <summary>
        ///A test for ActionPerformer Constructor
        ///</summary>
        [TestMethod()]
        public void ActionPerformerConstructorTest()
        {
            XmlDocument document = GetTestMasterFile();
            Instance instance = new Instance();
            ActionPerformer target = new ActionPerformer(document, null, instance);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TestDataGenerationTool.exe")]
        public void AddTest()
        {
            InstanceActionAddElement addElement = new InstanceActionAddElement();
            addElement.name = "NewElement1";

            InstanceActionAdd actionAdd = new InstanceActionAdd();
            actionAdd.location = "/root/myNode";
            actionAdd.Item = addElement;

            performer.Add(actionAdd);

            addElement.name = "NewElement2";
            addElement.before = "NewElement1";
            performer.Add(actionAdd);

            addElement.name = "NewElement3";
            addElement.before = string.Empty;
            addElement.after = "NewElement2";
            performer.Add(actionAdd);

            XmlNode firstAdditionNode = performer.document.SelectSingleNode("/root/myNode/NewElement1");

            Assert.IsNotNull(firstAdditionNode);

            Assert.IsNotNull(firstAdditionNode.PreviousSibling);
            Assert.AreEqual("NewElement3", firstAdditionNode.PreviousSibling.LocalName);

            Assert.IsNotNull(firstAdditionNode.PreviousSibling.PreviousSibling);
            Assert.AreEqual("NewElement2", firstAdditionNode.PreviousSibling.PreviousSibling.LocalName);
        }

        /// <summary>
        ///A test for Set
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TestDataGenerationTool.exe")]
        public void SetTest()
        {
            InstanceActionSet actionSet = new InstanceActionSet();
            actionSet.location = "/root/myNode/@myAttr";
            actionSet.value = "newValue1";
   
            performer.Set(actionSet);

            // Test existing attribute value
            XmlNode myNode = performer.document.SelectSingleNode("/root/myNode");

            Assert.IsNotNull(myNode);
            Assert.IsNotNull(myNode.Attributes["myAttr"]);
            Assert.AreEqual("newValue1", myNode.Attributes["myAttr"].Value);

            // Text new attribute value
            actionSet.location = "/root/myNode/@myNewAttr";
            actionSet.value = "newValue2";
            performer.Set(actionSet);

            Assert.IsNotNull(myNode.Attributes["myNewAttr"]);
            Assert.AreEqual("newValue2", myNode.Attributes["myNewAttr"].Value);

            // Test element text/value
            actionSet.location = "/root/myNode/text()";
            actionSet.value = "newValue3";
            performer.Set(actionSet);

            Assert.IsNotNull(myNode.SelectSingleNode("text()"));
            Assert.AreEqual("newValue3", myNode.SelectSingleNode("text()").Value);
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TestDataGenerationTool.exe")]
        public void RemoveTest()
        {
            InstanceActionRemove actionRemove = new InstanceActionRemove();
            actionRemove.location = "/root/myNode2";

            // Element
            performer.Remove(actionRemove);

            Assert.IsNull(performer.document.SelectSingleNode("/root/myNode2"));

            // Attribute
            actionRemove.location = "/root/myNode/@myAttr";
            performer.Remove(actionRemove);

            Assert.IsNotNull(performer.document.SelectSingleNode("/root/myNode"));
            Assert.IsNull(performer.document.SelectSingleNode("/root/myNode").Attributes["myAttr"]);
        }

        /// <summary>
        /// A test for Comment
        /// </summary>
        [TestMethod()]
        [DeploymentItem("TestDataGenerationTool.exe")]
        public void CommentTest()
        {
            InstanceActionComment actionComment = new InstanceActionComment();
            actionComment.location = "/root/myNode2";
            actionComment.value = "This is my comment";

            performer.Comment(actionComment);

            Assert.IsNotNull(performer.document.DocumentElement);
            Assert.IsNotNull(performer.document.DocumentElement.ChildNodes);
            Assert.AreEqual(3, performer.document.DocumentElement.ChildNodes.Count);
            Assert.IsNotNull(performer.document.DocumentElement.ChildNodes[1]);
            Assert.IsInstanceOfType(performer.document.DocumentElement.ChildNodes[1], typeof(XmlComment));
        }
    }
}
