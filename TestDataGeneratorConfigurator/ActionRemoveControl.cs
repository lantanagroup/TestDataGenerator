using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LantanaGroup.TestDataGenerator.Configurator.Data;

namespace LantanaGroup.TestDataGenerator.Configurator
{
    public partial class ActionRemoveControl : ActionControlBase
    {
        public ActionRemoveControl()
        {
            InitializeComponent();
        }

        protected override void SetDisplayObject()
        {
            if (this.ActionDisplayObject is ActionRemoveDisplay)
            {
                this.removeBinding.DataSource = this.ActionDisplayObject;
                this.removeBinding.ResetBindings(false);
            }
            else
            {
                this.removeBinding.DataSource = null;
                this.ActionDisplayObject = null;
            }
        }
    }
}
