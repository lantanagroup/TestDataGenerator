using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LantanaGroup.TestDataGenerator.Shared.Data;

namespace LantanaGroup.TestDataGenerator.Configurator.Data
{
    public class InstanceDisplay
    {
        private Instance instance = null;

        public InstanceDisplay()
        {
            this.instance = new Instance();
        }

        public InstanceDisplay(Instance instance)
        {
            this.instance = instance;
        }

        public string Name
        {
            get
            {
                return this.instance.name;
            }
            set
            {
                this.instance.name = value;
            }
        }

        public string Context
        {
            get
            {
                return this.instance.rootContext;
            }
            set
            {
                this.instance.rootContext = value;
            }
        }

        public bool ApplyActionsToLast
        {
            get
            {
                return !this.instance.createNew;
            }
            set
            {
                this.instance.createNew = !value;
            }
        }

        public bool ApplyToEveryInstance
        {
            get
            {
                return this.instance.filter == null;
            }
            set
            {
                this.instance.filter = null;
            }
        }

        public bool IsAbstract
        {
            get
            {
                return this.instance.isAbstract;
            }
            set
            {
                this.instance.isAbstract = value;
            }
        }

        public bool ApplyRandomly
        {
            get
            {
                return this.instance.filter != null && this.instance.filter.Item is InstanceFilterRandomRowCount;
            }
            set
            {
                if (this.instance.filter == null || !(this.instance.filter.Item is InstanceFilterRandomRowCount))
                {
                    this.instance.filter = new InstanceFilter();
                    this.instance.filter.Item = new InstanceFilterRandomRowCount();
                }
            }
        }

        public bool ApplySpecific
        {
            get
            {
                return this.instance.filter != null && this.instance.filter.Item is InstanceFilterSpecificRowCount;
            }
            set
            {
                if (this.instance.filter == null || !(this.instance.filter.Item is InstanceFilterSpecificRowCount))
                {
                    this.instance.filter = new InstanceFilter();
                    this.instance.filter.Item = new InstanceFilterSpecificRowCount();
                }
            }
        }

        public string ApplySpecificSection
        {
            get
            {
                if (ApplySpecific)
                {
                    InstanceFilterSpecificRowCount filterSpecific = this.instance.filter.Item as InstanceFilterSpecificRowCount;
                    return filterSpecific.section;
                }

                return string.Empty;
            }
            set
            {
                if (ApplySpecific)
                {
                    InstanceFilterSpecificRowCount filterSpecific = this.instance.filter.Item as InstanceFilterSpecificRowCount;
                    filterSpecific.section = value;
                }
            }
        }

        public string ApplySpecificRecords
        {
            get
            {
                if (ApplySpecific)
                {
                    InstanceFilterSpecificRowCount filterSpecific = this.instance.filter.Item as InstanceFilterSpecificRowCount;
                    return filterSpecific.recordValue;
                }

                return string.Empty;
            }
            set
            {
                if (ApplySpecific)
                {
                    InstanceFilterSpecificRowCount filterSpecific = this.instance.filter.Item as InstanceFilterSpecificRowCount;
                    filterSpecific.recordValue = value;
                }
            }
        }
    }
}
