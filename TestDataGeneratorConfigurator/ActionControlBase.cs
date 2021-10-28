using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LantanaGroup.TestDataGenerator.Configurator
{
    public partial class ActionControlBase : UserControl
    {
        public delegate void ActionControlUpdatedHandler();
        public event ActionControlUpdatedHandler ActionControlUpdated;

        public delegate BuildXpathForm XpathFormRequestedHandler(bool forceElementSelection);
        public event XpathFormRequestedHandler XpathFormRequested;

        private Data.ActionDisplay actionDisplayObject = null;

        public Data.ActionDisplay ActionDisplayObject
        {
            get { return actionDisplayObject; }
            set
            {
                actionDisplayObject = value;
                this.SetDisplayObject();
            }
        }

        public ActionControlBase()
        {
            InitializeComponent();
        }

        protected virtual void SetDisplayObject()
        {

        }

        protected void FireActionControlChanged()
        {
            if (this.ActionControlUpdated != null)
                this.ActionControlUpdated();
        }

        public virtual void Save()
        {
            this.locationText.DataBindings["Text"].WriteValue();
        }

        private void LocationXpathClicked(object sender, EventArgs e)
        {
            if (this.XpathFormRequested != null)
            {
                BuildXpathForm xpathForm = this.XpathFormRequested(false);

                if (xpathForm.ShowDialog() == DialogResult.OK)
                {
                    this.locationText.Text = xpathForm.SelectedXpath;
                    this.Save();
                }
            }
        }
    }
}
