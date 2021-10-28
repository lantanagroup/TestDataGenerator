using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using LantanaGroup.TestDataGenerator.Shared.Data;

namespace LantanaGroup.TestDataGenerator.Configurator
{
    public partial class BuildXpathForm : Form
    {
        private XmlDocument doc = null;
        private XmlNamespaceManager nsManager = null;
        private XmlNode context = null;
        private bool forceElementSelection = false;
        private List<ActionConfigurationNamespace> namespaces = null;

        public string SelectedXpath
        {
            get
            {
                return this.selectedXpath.Text;
            }
        }

        public BuildXpathForm(List<ActionConfigurationNamespace> namespaces, XmlDocument doc, string context, bool forceElementSelection)
        {
            InitializeComponent();

            this.doc = doc;
            this.forceElementSelection = forceElementSelection;
            this.nsManager = new XmlNamespaceManager(this.doc.NameTable);

            if (namespaces != null)
            {
                this.namespaces = namespaces;

                foreach (ActionConfigurationNamespace cNs in namespaces)
                {
                    nsManager.AddNamespace(cNs.prefix, cNs.uri);
                }
            }
            else
            {
                this.namespaces = new List<ActionConfigurationNamespace>();
            }

            // Get the context
            if (string.IsNullOrEmpty(context))
            {
                this.context = doc;
            }
            else
            {
                try
                {
                    this.context = this.doc.SelectSingleNode(context, this.nsManager);
                }
                catch
                {
                    MessageBox.Show("Context is not valid. Setting context to document.");
                }
            }

            if (this.context == null)
            {
                MessageBox.Show("Could not find the context of the instance. Perhaps the namespaces are incorrect, or the XPATH is invalid.");
            }
            else
            {
                CreateBranch(null, this.context);
            }
        }

        private void CreateBranch(TreeNode parent, XmlNode node)
        {
            if (node == null)
                return;

            if (node.Attributes != null)
            {
                foreach (XmlAttribute cAttribute in node.Attributes)
                {
                    TreeNode newTreeNode = CreateTreeNode(cAttribute);

                    if (parent == null)
                    {
                        this.documentTree.Nodes.Add(newTreeNode);
                    }
                    else
                    {
                        parent.Nodes.Add(newTreeNode);
                    }
                }
            }

            foreach (XmlNode cNode in node.ChildNodes)
            {
                if (cNode.NodeType == XmlNodeType.Element)
                {
                    TreeNode newTreeNode = CreateTreeNode(cNode);

                    if (parent == null)
                    {
                        this.documentTree.Nodes.Add(newTreeNode);
                    }
                    else
                    {
                        parent.Nodes.Add(newTreeNode);
                    }

                    CreateBranch(newTreeNode, cNode);
                }
            }
        }

        private TreeNode CreateTreeNode(XmlNode node)
        {
            TreeNode newTreeNode = new TreeNode();
            newTreeNode.Tag = node;

            if (node.NodeType == XmlNodeType.Attribute)
            {
                XmlAttribute attribute = node as XmlAttribute;

                newTreeNode.Text = "@" + attribute.Name;
            }
            else if (node.NodeType == XmlNodeType.Element)
            {
                XmlElement element = node as XmlElement;
                string prefix = string.Empty;

                if (!string.IsNullOrEmpty(node.NamespaceURI))
                {
                    foreach (ActionConfigurationNamespace cNs in this.namespaces)
                    {
                        if (cNs.uri == node.NamespaceURI)
                        {
                            prefix = cNs.prefix;
                            break;
                        }
                    }
                }

                newTreeNode.Text =
                    !string.IsNullOrEmpty(prefix) ?
                    prefix + ":" + element.LocalName :
                    element.LocalName;
            }

            return newTreeNode;
        }

        private void AfterDocumentNodeSelected(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                XmlNode currentNode = e.Node.Tag as XmlNode;

                if (currentNode.ChildNodes != null && currentNode.ChildNodes.Count > 1)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(currentNode.OuterXml);

                    using (StringWriter sw = new StringWriter())
                    {
                        doc.Save(sw);
                        this.contentsText.Text = sw.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n", "");
                    }
                }
                else
                {
                    this.contentsText.Text = currentNode.OuterXml;
                }

                if (this.forceElementSelection && currentNode.NodeType != XmlNodeType.Element)
                {
                    this.selectButton.Enabled = false;
                }
                else
                {
                    this.selectButton.Enabled = true;
                }
            }
            else
            {
                this.contentsText.Text = string.Empty;
                this.selectButton.Enabled = false;
            }

            GenerateXpath();
        }

        private void GenerateXpath()
        {
            if (this.documentTree.SelectedNode == null || this.documentTree.SelectedNode.Tag == null)
            {
                return;
            }

            XmlNode currentNode = this.documentTree.SelectedNode.Tag as XmlNode;

            if (currentNode.NodeType == XmlNodeType.Attribute)
            {
                XmlAttribute currentAttribute = currentNode as XmlAttribute;
                this.selectedXpath.Text = GenerateXpathParent(currentAttribute.OwnerElement, "@" + currentAttribute.Name);
            }
            else if (currentNode.NodeType == XmlNodeType.Element)
            {
                string prefix = string.Empty;

                if (!string.IsNullOrEmpty(currentNode.NamespaceURI))
                {
                    foreach (ActionConfigurationNamespace cNs in this.namespaces)
                    {
                        if (cNs.uri == currentNode.NamespaceURI)
                        {
                            prefix = cNs.prefix;
                            break;
                        }
                    }
                }

                string startingXpath = !string.IsNullOrEmpty(prefix) ? prefix + ":" + currentNode.LocalName : currentNode.LocalName;

                // Determine what count this element is
                int thisCount = CountPreviousSiblings(currentNode, currentNode, 1);

                if (thisCount >= 1)
                {
                    startingXpath += string.Format("[{0}]", thisCount);
                }

                this.selectedXpath.Text = GenerateXpathParent(
                    currentNode.ParentNode,
                    startingXpath);
            }
        }

        private string GenerateXpathParent(XmlNode parentNode, string childXpath)
        {
            string prefix = string.Empty;

            if (parentNode == null || parentNode.NodeType == XmlNodeType.Document || parentNode == this.context)
            {
                return this.context != this.doc ? childXpath : "/" + childXpath;
            }

            if (!string.IsNullOrEmpty(parentNode.NamespaceURI))
            {
                foreach (ActionConfigurationNamespace cNs in this.namespaces)
                {
                    if (cNs.uri == parentNode.NamespaceURI)
                    {
                        prefix = cNs.prefix;
                        break;
                    }
                }
            }

            string thisNodeName = !string.IsNullOrEmpty(prefix) ? prefix + ":" + parentNode.LocalName : parentNode.LocalName;

            // Determine if this is the last iteration
            string nextXpath = parentNode.ParentNode == null ? "/" : string.Empty;

            // Add the name of this element
            nextXpath += thisNodeName;

            // Determine what count this element is
            int thisCount = CountPreviousSiblings(parentNode, parentNode, 1);

            if (thisCount >= 1)
            {
                nextXpath += string.Format("[{0}]", thisCount);
            }

            // Add the child
            nextXpath += "/" + childXpath;

            if (parentNode.ParentNode != null)
            {
                return GenerateXpathParent(parentNode.ParentNode, nextXpath);
            }

            return nextXpath;
        }

        private int CountPreviousSiblings(XmlNode originalSibling, XmlNode currentSibling, int currentCount)
        {
            if (currentSibling.PreviousSibling == null)
            {
                return currentCount;
            }

            return CountPreviousSiblings(
                originalSibling,
                currentSibling.PreviousSibling, 
                currentSibling.PreviousSibling.LocalName == originalSibling.LocalName ? currentCount + 1 : currentCount);
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
