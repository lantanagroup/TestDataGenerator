using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml;

using LantanaGroup.TestDataGenerator.Shared.Data;

namespace LantanaGroup.TestDataGenerator.Configurator.Data
{
    public class ActionAddDisplay : ActionDisplay
    {
        private InstanceActionAdd instanceActionAdd = null;

        public ActionAddDisplay(InstanceActionAdd instanceActionAdd)
        {
            this.instanceActionAdd = instanceActionAdd;
        }

        public string ActionLocation
        {
            get
            {
                return this.instanceActionAdd.location;
            }
            set
            {
                this.instanceActionAdd.location = value;
                this.FirePropertyChanged("ActionLocation");
            }
        }

        public bool IsActionAddElement
        {
            get
            {
                return this.instanceActionAdd.Item is InstanceActionAddElement;
            }
            set
            {
                if (value && !(this.instanceActionAdd.Item is InstanceActionAddElement))
                {
                    this.instanceActionAdd.Item = new InstanceActionAddElement();
                    this.FirePropertyChanged("IsActionAddElement");
                }
                else if (!value && (this.instanceActionAdd.Item is InstanceActionAddElement))
                {
                    this.instanceActionAdd.Item = null;
                }
            }
        }

        public bool IsActionAddAttribute
        {
            get
            {
                return this.instanceActionAdd.Item is InstanceActionAddAttribute;
            }
            set
            {
                if (value && !(this.instanceActionAdd.Item is InstanceActionAddAttribute))
                {
                    this.instanceActionAdd.Item = new InstanceActionAddAttribute();
                    this.FirePropertyChanged("IsActionAddAttribute");
                }
                else if (!value && (this.instanceActionAdd.Item is InstanceActionAddAttribute))
                {
                    this.instanceActionAdd.Item = null;
                }
            }
        }

        public string ActionElementName
        {
            get
            {
                InstanceActionAddElement actionAddElement = this.instanceActionAdd.Item as InstanceActionAddElement;

                if (actionAddElement != null)
                {
                    return actionAddElement.name;
                }

                return string.Empty;
            }
            set
            {
                if (this.instanceActionAdd.Item is InstanceActionAddElement)
                {
                    ((InstanceActionAddElement)this.instanceActionAdd.Item).name = value;
                    this.FirePropertyChanged("ActionElementName");
                }
            }
        }

        public string ActionElementData
        {
            get
            {
                InstanceActionAddElement actionAddElement = this.instanceActionAdd.Item as InstanceActionAddElement;

                if (actionAddElement == null)
                {
                    return string.Empty;
                }

                return actionAddElement.external as string;
            }
            set
            {
                if (this.instanceActionAdd.Item is InstanceActionAddElement)
                {
                    InstanceActionAddElement actionAddElement = this.instanceActionAdd.Item as InstanceActionAddElement;

                    actionAddElement.external = value;

                    this.FirePropertyChanged("ActionElementData");
                }
            }
        }

        public string ActionElementBefore
        {
            get
            {
                InstanceActionAddElement actionAddElement = this.instanceActionAdd.Item as InstanceActionAddElement;

                if (actionAddElement != null)
                {
                    return actionAddElement.before;
                }

                return string.Empty;
            }
            set
            {
                if (this.instanceActionAdd.Item is InstanceActionAddElement)
                {
                    ((InstanceActionAddElement)this.instanceActionAdd.Item).before = value;
                    this.FirePropertyChanged("ActionElementBefore");
                }
            }
        }

        public string ActionElementAfter
        {
            get
            {
                InstanceActionAddElement actionAddElement = this.instanceActionAdd.Item as InstanceActionAddElement;

                if (actionAddElement != null)
                {
                    return actionAddElement.after;
                }

                return string.Empty;
            }
            set
            {
                if (this.instanceActionAdd.Item is InstanceActionAddElement)
                {
                    ((InstanceActionAddElement)this.instanceActionAdd.Item).after = value;
                    this.FirePropertyChanged("ActionElementAfter");
                }
            }
        }

        public string ActionAttributeName
        {
            get
            {
                InstanceActionAddAttribute actionAddAttribute = this.instanceActionAdd.Item as InstanceActionAddAttribute;

                if (actionAddAttribute != null)
                {
                    return actionAddAttribute.name;
                }

                return string.Empty;
            }
            set
            {
                if (this.instanceActionAdd.Item is InstanceActionAddAttribute)
                {
                    ((InstanceActionAddAttribute)this.instanceActionAdd.Item).name = value;
                    this.FirePropertyChanged("ActionAttributeName");
                }
            }
        }

        public string ActionAttributeValue
        {
            get
            {
                InstanceActionAddAttribute actionAddAttribute = this.instanceActionAdd.Item as InstanceActionAddAttribute;

                if (actionAddAttribute != null)
                {
                    return actionAddAttribute.value;
                }

                return string.Empty;
            }
            set
            {
                if (this.instanceActionAdd.Item is InstanceActionAddAttribute)
                {
                    ((InstanceActionAddAttribute)this.instanceActionAdd.Item).value = value;
                    this.FirePropertyChanged("ActionAttributeValue");
                }
            }
        }
    }
}
