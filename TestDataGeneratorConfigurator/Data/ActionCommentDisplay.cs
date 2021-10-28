using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using LantanaGroup.TestDataGenerator.Shared.Data;

namespace LantanaGroup.TestDataGenerator.Configurator.Data
{
    public class ActionCommentDisplay : ActionDisplay
    {
        private InstanceActionComment actionComment = null;

        public ActionCommentDisplay(InstanceActionComment actionComment)
        {
            this.actionComment = actionComment;   
        }

        public string ActionLocation
        {
            get
            {
                return this.actionComment.location;
            }
            set
            {
                this.actionComment.location = value;
                this.FirePropertyChanged("ActionLocation");
            }
        }

        public string ActionComment
        {
            get
            {
                return this.actionComment.value;
            }
            set
            {
                this.actionComment.value = value;
                this.FirePropertyChanged("ActionComment");
            }
        }
    }
}
