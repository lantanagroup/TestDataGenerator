using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using LantanaGroup.TestDataGenerator.Shared.Data;

namespace LantanaGroup.TestDataGenerator.Configurator.Data
{
    public class ActionRemoveDisplay : ActionDisplay
    {
        private InstanceActionRemove actionRemove = null;

        public ActionRemoveDisplay(InstanceActionRemove actionRemove)
        {
            this.actionRemove = actionRemove;
        }

        public string ActionLocation
        {
            get
            {
                return this.actionRemove.location;
            }
            set
            {
                this.actionRemove.location = value;
                this.FirePropertyChanged("ActionLocation");
            }
        }
    }
}
