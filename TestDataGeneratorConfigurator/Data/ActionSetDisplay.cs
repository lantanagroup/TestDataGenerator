using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using LantanaGroup.TestDataGenerator.Shared.Data;

namespace LantanaGroup.TestDataGenerator.Configurator.Data
{
    public class ActionSetDisplay : ActionDisplay
    {
        private InstanceActionSet actionSet = null;

        public ActionSetDisplay(InstanceActionSet actionSet)
        {
            this.actionSet = actionSet;
        }

        public string ActionLocation
        {
            get
            {
                return this.actionSet.location;
            }
            set
            {
                this.actionSet.location = value;
                this.FirePropertyChanged("ActionLocation");
            }
        }

        public string ActionValue
        {
            get
            {
                return this.actionSet.value;
            }
            set
            {
                this.actionSet.value = value;
                this.FirePropertyChanged("ActionValue");
            }
        }
    }
}
