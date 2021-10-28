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
    public partial class ActionSetControl : ActionControlBase
    {
        public ActionSetControl()
        {
            InitializeComponent();
        }

        public string ActionValue
        {
            get
            {
                return this.valueText.Text;
            }
            set
            {
                this.valueText.Text = value;
            }
        }

        protected override void SetDisplayObject()
        {
            if (this.ActionDisplayObject is ActionSetDisplay)
            {
                this.setBinding.DataSource = this.ActionDisplayObject;
                this.setBinding.ResetBindings(false);
            }
            else
            {
                this.setBinding.DataSource = null;
            }
        }

        public override void Save()
        {
            base.Save();

            this.valueText.DataBindings["Text"].WriteValue();
        }
    }
}
