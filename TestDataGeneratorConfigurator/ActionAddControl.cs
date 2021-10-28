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
    public partial class ActionAddControl : ActionControlBase
    {
        public ActionAddControl()
        {
            InitializeComponent();
        }

        protected override void SetDisplayObject()
        {
            if (this.ActionDisplayObject is ActionAddDisplay)
            {
                this.addBinding.DataSource = this.ActionDisplayObject;
                this.addBinding.ResetBindings(false);
            }
            else
            {
                this.addBinding.DataSource = null;
            }
        }

        private void ActionAddTypeChanged(object sender, EventArgs e)
        {
            this.elementNameText.Enabled =
                this.elementDataButton.Enabled = 
                this.elementBeforeText.Enabled = 
                this.elementAfterText.Enabled = this.elementRadio.Checked;

            this.attributeNameText.Enabled =
                this.attributeValueText.Enabled = this.attributeRadio.Checked;

            FireActionControlChanged();
        }

        private void ElementDataButtonClicked(object sender, EventArgs e)
        {
            if (!(this.ActionDisplayObject is ActionAddDisplay))
            {
                return;
            }

            ActionAddDisplay actionAddDisplayObject = this.ActionDisplayObject as ActionAddDisplay;
            AddElementDataForm aedf = new AddElementDataForm(actionAddDisplayObject.ActionElementData);

            if (aedf.ShowDialog() == DialogResult.OK)
            {
                actionAddDisplayObject.ActionElementData = aedf.AddElementData;
                FireActionControlChanged();
            }
        }

        private void ValuesChanged(object sender, EventArgs e)
        {
            FireActionControlChanged();
        }

        public override void Save()
        {
            this.locationText.DataBindings["Text"].WriteValue();

            if (this.elementNameText.Focused)
                this.elementNameText.DataBindings["Text"].WriteValue();

            if (this.elementBeforeText.Focused)
                this.elementBeforeText.DataBindings["Text"].WriteValue();

            if (this.elementAfterText.Focused)
                this.elementAfterText.DataBindings["Text"].WriteValue();

            if (this.attributeNameText.Focused)
                this.attributeNameText.DataBindings["Text"].WriteValue();

            if (this.attributeValueText.Focused)
                this.attributeValueText.DataBindings["Text"].WriteValue();

            if (this.elementRadio.Focused)
                this.elementRadio.DataBindings["Checked"].WriteValue();

            if (this.attributeRadio.Focused)
                this.attributeRadio.DataBindings["Checked"].WriteValue();
        }
    }
}
