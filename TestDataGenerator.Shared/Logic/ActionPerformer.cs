using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;

using LantanaGroup.TestDataGenerator.Shared.Data;

namespace LantanaGroup.TestDataGenerator.Shared.Logic
{
    
    public class ActionPerformer
    {
        internal XmlDocument document = null;
        private Instance instance = null;
        private XmlNode context = null;
        private XmlNamespaceManager nsManager = null;
        private List<ActionConfigurationNamespace> namespaces;

        /// <summary>
        /// Initializes a new instance of ActionPerformer
        /// </summary>
        /// <param name="document">The document that the instance actions should be performed on.</param>
        /// <param name="instance">The instance action configuration</param>
        public ActionPerformer(XmlDocument document, List<ActionConfigurationNamespace> namespaces, Instance instance)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }

            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            this.document = document;
            this.instance = instance;
            this.context = document;
            this.namespaces = namespaces != null ? namespaces : new List<ActionConfigurationNamespace>();

            // Setup namespaces
            this.nsManager = new XmlNamespaceManager(this.document.NameTable);

            if (this.namespaces != null)
            {
                foreach (ActionConfigurationNamespace cNs in this.namespaces)
                {
                    this.nsManager.AddNamespace(cNs.prefix, cNs.uri);
                }
            }

            if (!string.IsNullOrEmpty(instance.rootContext))
            {
                try
                {
                    if (instance.rootContext.LastIndexOf('/') == instance.rootContext.Length - 1)
                    {
                        instance.rootContext = instance.rootContext.Substring(0, instance.rootContext.Length - 1);
                    }

                    this.context = document.SelectSingleNode(instance.rootContext, this.nsManager);
                    if (this.context == null)
                        throw new Exception("Context is null");
                }
                catch (Exception ex)
                {
                    LogFactory.Log(LogFactory.Severities.Error, LogFactory.MessageTypes.Generation, string.Empty, "rootContext of '" + instance.name + "' could not be selected. Invalid action configuration!");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Executes the actions performed in the instance.
        /// </summary>
        public void Execute()
        {
            if (this.instance.Items != null)
            {
                foreach (object cAction in this.instance.Items)
                {
                    InstanceActionAdd actionAdd = cAction as InstanceActionAdd;
                    InstanceActionRemove actionRemove = cAction as InstanceActionRemove;
                    InstanceActionSet actionSet = cAction as InstanceActionSet;
                    InstanceActionComment actionComment = cAction as InstanceActionComment;

                    if (actionAdd != null)
                    {
                        Add(actionAdd);
                    }
                    else if (actionRemove != null)
                    {
                        Remove(actionRemove);
                    }
                    else if (actionSet != null)
                    {
                        Set(actionSet);
                    }
                    else if (actionComment != null)
                    {
                        Comment(actionComment);
                    }
                }
            }
        }

        internal void Add(InstanceActionAdd actionAdd)
        {
            XmlNodeList locationList = this.context.SelectNodes(actionAdd.location, this.nsManager);

            if (locationList.Count == 0)
            {
                LogFactory.Log(LogFactory.Severities.Warning, LogFactory.MessageTypes.Generation, string.Empty, "Could not find matching location '{0}' for add action.", actionAdd.location);
                return;
            }

            foreach (XmlNode cLocation in locationList)
            {
                InstanceActionAddAttribute attribute = actionAdd.Item as InstanceActionAddAttribute;
                InstanceActionAddElement element = actionAdd.Item as InstanceActionAddElement;

                // Ensure that we are trying to add to an element
                if (cLocation.NodeType != XmlNodeType.Element)
                {
                    string msg = string.Format("The location '{0}' of the add action must be an element!", actionAdd.location);
                    throw new Exception(msg);
                }

                if (element != null)
                {
                    List<XmlNode> importedExternalNodes = null;
                    XmlElement beforeElement = null;
                    XmlElement afterElement = null;

                    // Import external nodes into document
                    if (element.external != null)
                    {
                        try
                        {
                            string externalContent = (string)element.external;
                            importedExternalNodes = new List<XmlNode>();

                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(externalContent);

                            importedExternalNodes.Add(
                                this.document.ImportNode(doc.DocumentElement, true));
                        }
                        catch (Exception ex)
                        {
                            LogFactory.Log(
                                LogFactory.Severities.Error, 
                                LogFactory.MessageTypes.Generation, 
                                string.Empty, 
                                "Could not import 'external' data for add action due to error: {0}", ex.Message);
                        }
                    }

                    // Find the 'before' node
                    if (!string.IsNullOrEmpty(element.before))
                    {
                        try
                        {
                            XmlNodeList beforeElements = cLocation.SelectNodes(element.before, this.nsManager);

                            if (beforeElements.Count > 0)
                            {
                                beforeElement = beforeElements[0] as XmlElement;
                            }
                        }
                        catch { }

                        if (beforeElement == null)
                        {
                            LogFactory.Log(
                                LogFactory.Severities.Warning, 
                                LogFactory.MessageTypes.Generation, 
                                string.Empty, 
                                "Add action's 'before' attribute '{0}' cannot be mapped to an existing element.", element.before);
                        }
                    }

                    // Find the 'after' node
                    if (!string.IsNullOrEmpty(element.after))
                    {
                        try
                        {
                            XmlNodeList afterElements = cLocation.SelectNodes(element.after, this.nsManager);

                            if (afterElements.Count > 0)
                            {
                                afterElement = afterElements[0] as XmlElement;
                            }
                        }
                        catch { }

                        if (afterElement == null)
                        {
                            LogFactory.Log(
                                LogFactory.Severities.Warning, 
                                LogFactory.MessageTypes.Generation, 
                                string.Empty, 
                                "Add action's 'after' attribute '{0}' cannot be mapped to an existing element.", element.after);
                        }
                    }

                    if (string.IsNullOrEmpty(element.name))
                    {
                        if (importedExternalNodes == null)
                        {
                            throw new Exception("To add an element that does not have an explicit name, you must specify the element definition in the external section");
                        }

                        foreach (XmlNode cImportedExternalNode in importedExternalNodes)
                        {
                            // Determine where too add the new node at
                            if (beforeElement != null)
                            {
                                cLocation.InsertBefore(cImportedExternalNode, beforeElement);
                            }
                            else if (afterElement != null)
                            {
                                cLocation.InsertAfter(cImportedExternalNode, afterElement);
                            }
                            else
                            {
                                cLocation.AppendChild(cImportedExternalNode);
                            }
                        }
                    }
                    else
                    {
                        string elementPrefix = string.Empty;
                        string elementName = element.name;

                        if (element.name.IndexOf(":") > 0)
                        {
                            elementPrefix = element.name.Substring(0, element.name.IndexOf(":"));
                            elementName = element.name.Substring(element.name.IndexOf(":") + 1);
                        }

                        string foundNamespaceUri = string.Empty;

                        foreach (ActionConfigurationNamespace cNs in this.namespaces)
                        {
                            if (cNs.prefix == elementPrefix)
                            {
                                foundNamespaceUri = cNs.uri;
                                break;
                            }
                        }

                        XmlElement newElement = elementPrefix != null && !string.IsNullOrEmpty(foundNamespaceUri) ?
                            this.document.CreateElement(elementPrefix, elementName, foundNamespaceUri) :
                            this.document.CreateElement(elementName);

                        // Add the imported nodes to the new element defined by name
                        if (importedExternalNodes != null)
                        {
                            foreach (XmlNode cImportedExternalNode in importedExternalNodes)
                            {
                                newElement.AppendChild(cImportedExternalNode);
                            }
                        }

                        // Determine where too add the new node at
                        if (beforeElement != null)
                        {
                            cLocation.InsertBefore(newElement, beforeElement);
                        }
                        else if (afterElement != null)
                        {
                            cLocation.InsertAfter(newElement, afterElement);
                        }
                        else
                        {
                            cLocation.AppendChild(newElement);
                        }
                    }
                }
                else if (attribute != null)
                {
                    XmlAttribute newAttribute = this.document.CreateAttribute(attribute.name);

                    if (!string.IsNullOrEmpty(attribute.value))
                    {
                        newAttribute.Value = attribute.value;
                    }

                    cLocation.Attributes.Append(newAttribute);
                }
            }
        }

        internal void Comment(InstanceActionComment actionComment)
        {
            XmlNodeList locationList = this.context.SelectNodes(actionComment.location, this.nsManager);

            if (locationList.Count == 0)
            {
                LogFactory.Log(
                    LogFactory.Severities.Warning,
                    LogFactory.MessageTypes.Generation,
                    string.Empty,
                    "Could not find matching location '{0}' for add comment.", actionComment.location);
                return;
            }

            foreach (XmlNode cLocation in locationList)
            {
                XmlElement cLocationEle = cLocation as XmlElement;

                if (cLocationEle == null)
                {
                    cLocationEle = cLocationEle.ParentNode as XmlElement;
                }

                if (cLocationEle == null)
                {
                    LogFactory.Log(
                        LogFactory.Severities.Warning,
                        LogFactory.MessageTypes.Generation,
                        string.Empty,
                        "Could not identify element that comment should be set for. Location should be an element or object whose parent is an element.");
                    continue;
                }

                XmlComment commentNode = this.document.CreateComment(actionComment.value);

                if (cLocationEle.ParentNode == null)
                {
                    // Add the comment at the top of the location element.
                    if (cLocationEle.ChildNodes.Count > 0)
                    {
                        cLocationEle.InsertBefore(commentNode, cLocationEle.ChildNodes[0]);
                    }
                    else
                    {
                        cLocationEle.AppendChild(commentNode);
                    }
                }
                else
                {
                    // Add the comment before the location node.
                    cLocationEle.ParentNode.InsertBefore(commentNode, cLocationEle);
                }
            }
        }

        internal void Remove(InstanceActionRemove actionRemove)
        {
            XmlNodeList locationList = this.context.SelectNodes(actionRemove.location, this.nsManager);

            if (locationList.Count == 0)
            {
                LogFactory.Log(
                    LogFactory.Severities.Warning, 
                    LogFactory.MessageTypes.Generation, 
                    string.Empty, 
                    "Could not find matching location '{0}' for add action.", actionRemove.location);
                return;
            }

            foreach (XmlNode cLocation in locationList)
            {
                XmlElement cElement = cLocation as XmlElement;
                XmlAttribute cAttribute = cLocation as XmlAttribute;

                // Ensure that we are trying to remove an attribute or element
                if (cElement == null && cAttribute == null)
                {
                    string msg = string.Format("The location '{0}' of the remove action must be an attribute or element!", actionRemove.location);
                    throw new Exception(msg);
                }

                if (cElement != null)
                {
                    cElement.ParentNode.RemoveChild(cElement);
                }
                else if (cAttribute != null)
                {
                    cAttribute.OwnerElement.Attributes.Remove(cAttribute);
                }
            }
        }

        internal void Set(InstanceActionSet actionSet)
        {
            XmlNodeList locationList = this.context.SelectNodes(actionSet.location, this.nsManager);

            // Maybe this is an attribute query where the attribute doesn't exist yet
            // Maybe this is a text() query where the text() doesn't exist yet
            if (locationList.Count == 0)
            {
                // This is an attribute query
                if (actionSet.location.LastIndexOf('/') > 0 && actionSet.location[actionSet.location.LastIndexOf('/') + 1] == '@')
                {
                    string parentElementLocation = actionSet.location.Substring(0, actionSet.location.LastIndexOf('/'));
                    string attributeName = actionSet.location.Substring(actionSet.location.LastIndexOf('/') + 2);

                    locationList = this.context.SelectNodes(parentElementLocation, this.nsManager);

                    // Add the attribute exists in each location
                    foreach (XmlNode cLocation in locationList)
                    {
                        XmlAttribute newAttribute = this.document.CreateAttribute(attributeName);
                        cLocation.Attributes.Append(newAttribute);
                    }

                    locationList = this.context.SelectNodes(actionSet.location, this.nsManager);
                }
                else if (actionSet.location.Length > 7 && actionSet.location.LastIndexOf("/text()") == actionSet.location.Length - 7)
                {
                    string parentElementLocation = actionSet.location.Substring(0, actionSet.location.LastIndexOf('/'));

                    locationList = this.context.SelectNodes(parentElementLocation, this.nsManager);

                    foreach (XmlNode cLocation in locationList)
                    {
                        XmlText newText = this.document.CreateTextNode(string.Empty);
                        cLocation.AppendChild(newText);
                    }

                    locationList = this.context.SelectNodes(actionSet.location, this.nsManager);
                }
            }

            if (locationList.Count == 0)
            {
                LogFactory.Log(
                    LogFactory.Severities.Warning, 
                    LogFactory.MessageTypes.Generation, 
                    string.Empty, 
                    "Could not find matching location '{0}' for set action.", 
                    actionSet.location);
                return;
            }

            foreach (XmlNode cLocation in locationList)
            {
                XmlAttribute cAttribute = cLocation as XmlAttribute;
                XmlText cText = cLocation as XmlText;

                // Ensure that we are trying to remove an attribute or element
                if (cAttribute == null && cText == null)
                {
                    string msg = string.Format("The location '{0}' of the set action must be an attribute or text()!", actionSet.location);
                    throw new Exception(msg);
                }

                if (!string.IsNullOrEmpty(actionSet.value))
                {
                    if (cText != null)
                    {
                        cText.Value = actionSet.value;
                    }
                    else if (cAttribute != null)
                    {
                        cLocation.Value = actionSet.value;
                    }
                }
            }
        }
    }
}
