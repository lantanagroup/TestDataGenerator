﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// 
namespace LantanaGroup.TestDataGenerator.Shared.Data {
    using System.Xml.Serialization;
    using System.Collections.Generic;
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class ActionConfiguration
    {
        private List<ActionConfigurationNamespace> namespacesField;

        private List<ActionConfigurationGood> goodField = new List<ActionConfigurationGood>();

        private List<ActionConfigurationBad> badField = new List<ActionConfigurationBad>();

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("namespace", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<ActionConfigurationNamespace> namespaces
        {
            get
            {
                return this.namespacesField;
            }
            set
            {
                this.namespacesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("good", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<ActionConfigurationGood> good
        {
            get
            {
                return this.goodField;
            }
            set
            {
                this.goodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("bad", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<ActionConfigurationBad> bad
        {
            get
            {
                return this.badField;
            }
            set
            {
                this.badField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public List<Instance> GoodInstances
        {
            get
            {
                List<Instance> goodInstances = new List<Instance>();

                if (good != null)
                {
                    foreach (ActionConfigurationGood cGood in good)
                    {
                        if (cGood.instances != null)
                        {
                            goodInstances.AddRange(cGood.instances);
                        }
                    }
                }

                return goodInstances;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public List<Instance> BadInstances
        {
            get
            {
                List<Instance> badInstances = new List<Instance>();

                if (bad != null)
                {
                    foreach (ActionConfigurationBad cBad in bad)
                    {
                        if (cBad.instances != null)
                        {
                            badInstances.AddRange(cBad.instances);
                        }
                    }
                }

                return badInstances;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public List<Instance> AllInstances
        {
            get
            {
                List<Instance> instances = new List<Instance>();

                if (bad != null)
                {
                    foreach (ActionConfigurationBad cBad in bad)
                    {
                        if (cBad.instances != null)
                        {
                            instances.AddRange(cBad.instances);
                        }
                    }

                    foreach (ActionConfigurationGood cGood in good)
                    {
                        if (cGood.instances != null)
                        {
                            instances.AddRange(cGood.instances);
                        }
                    }
                }

                return instances;
            }
        }

        public void AddGoodInstance(Instance instance)
        {
            if (this.good.Count == 0)
            {
                this.good.Add(new ActionConfigurationGood());
            }

            this.good[this.good.Count - 1].instances.Add(instance);
        }

        public void AddBadInstance(Instance instance)
        {
            if (this.bad.Count == 0)
            {
                this.bad.Add(new ActionConfigurationBad());
            }

            this.bad[this.bad.Count - 1].instances.Add(instance);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ActionConfigurationNamespace
    {

        private string prefixField;

        private string uriField;

        private string locationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string prefix
        {
            get
            {
                return this.prefixField;
            }
            set
            {
                this.prefixField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string uri
        {
            get
            {
                return this.uriField;
            }
            set
            {
                this.uriField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", this.prefix, this.uri);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ActionConfigurationGood
    {

        private List<Instance> instancesField = new List<Instance>();

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("instance", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<Instance> instances
        {
            get
            {
                return this.instancesField;
            }
            set
            {
                this.instancesField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ActionConfigurationBad
    {

        private List<Instance> instancesField = new List<Instance>();

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("instance", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<Instance> instances
        {
            get
            {
                return this.instancesField;
            }
            set
            {
                this.instancesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Instance {

        private InstanceFilter filterField;

        private List<InstanceInherit> inheritField = new List<InstanceInherit>();
        
        private List<object> itemsField = new List<object>();
        
        private string nameField;
        
        private string rootContextField;

        private bool createNewField = true;

        private bool isAbstractField = false;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public InstanceFilter filter {
            get {
                return this.filterField;
            }
            set {
                this.filterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("inherit", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<InstanceInherit> inherit
        {
            get
            {
                return this.inheritField;
            }
            set
            {
                this.inheritField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("action-add", typeof(InstanceActionAdd), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("action-remove", typeof(InstanceActionRemove), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("action-set", typeof(InstanceActionSet), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("action-comment", typeof(InstanceActionComment), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<object> Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string rootContext {
            get {
                return this.rootContextField;
            }
            set {
                this.rootContextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool createNew
        {
            get
            {
                return this.createNewField;
            }
            set
            {
                this.createNewField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool isAbstract
        {
            get
            {
                return this.isAbstractField;
            }
            set
            {
                this.isAbstractField = value;
            }
        }

        public override string ToString()
        {
            return this.name;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class InstanceInherit
    {

        private string instanceNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string instanceName
        {
            get
            {
                return this.instanceNameField;
            }
            set
            {
                this.instanceNameField = value;
            }
        }

        public override string ToString()
        {
            return this.instanceName;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class InstanceFilter {
        
        private object itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("randomRowCount", typeof(InstanceFilterRandomRowCount), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("specificRowCount", typeof(InstanceFilterSpecificRowCount), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class InstanceFilterRandomRowCount {
        private int maxOccurField = 1;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int maxOccur
        {
            get
            {
                return this.maxOccurField;
            }
            set
            {
                this.maxOccurField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class InstanceFilterSpecificRowCount {
        
        private string recordValueField;
        
        private bool valueFieldSpecified;
        
        private string sectionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("value")]
        public string recordValue {
            get {
                return this.recordValueField;
            }
            set {
                this.recordValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool valueSpecified {
            get {
                return this.valueFieldSpecified;
            }
            set {
                this.valueFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string section {
            get {
                return this.sectionField;
            }
            set {
                this.sectionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class InstanceActionAdd {
        
        private object itemField;
        
        private string locationField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("attribute", typeof(InstanceActionAddAttribute), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("element", typeof(InstanceActionAddElement), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string location {
            get {
                return this.locationField;
            }
            set {
                this.locationField = value;
            }
        }

        public override string ToString()
        {
            if (this.location == null)
            {
                return string.Format("Add ({0})", this.Item);
            }

            return string.Format(
                "Add ({0}): {1}", 
                this.Item == null ? "N/A" : this.Item.ToString(), 
                this.location.Length > 20 ? this.location.Substring(this.location.Length - 20) : this.location);
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class InstanceActionAddAttribute {
        
        private string nameField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }

        public override string ToString()
        {
            return "Attribute";
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class InstanceActionAddElement {
        
        private object externalField;
        
        private string nameField;
        
        private string beforeField;
        
        private string afterField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Type=typeof(string))]
        public object external {
            get {
                return this.externalField;
            }
            set {
                this.externalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string before {
            get {
                return this.beforeField;
            }
            set {
                this.beforeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string after {
            get {
                return this.afterField;
            }
            set {
                this.afterField = value;
            }
        }

        public override string ToString()
        {
            return "Element";
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class InstanceActionRemove {
        
        private string locationField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string location {
            get {
                return this.locationField;
            }
            set {
                this.locationField = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Remove: {0}", this.location);
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class InstanceActionSet {
        
        private string locationField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string location {
            get {
                return this.locationField;
            }
            set {
                this.locationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Set: {0}", this.location);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class InstanceActionComment
    {
        private string locationField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Comment: {0}", this.location);
        }
    }
}
