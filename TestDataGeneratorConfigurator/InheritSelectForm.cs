using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LantanaGroup.TestDataGenerator.Shared.Data;

namespace LantanaGroup.TestDataGenerator.Configurator
{
    public partial class InheritSelectForm : Form
    {
        public InheritSelectForm(List<Instance> instances)
        {
            InitializeComponent();

            this.instanceList.DataSource = instances;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK && this.instanceList.SelectedIndex < 0)
            {
                MessageBox.Show("You must select an instance before closing");
                e.Cancel = true;
            }

            base.OnClosing(e);
        }

        public Instance SelectedInstance
        {
            get
            {
                if (this.instanceList.SelectedIndex >= 0)
                {
                    List<Instance> instances = this.instanceList.DataSource as List<Instance>;
                    return instances[this.instanceList.SelectedIndex];
                }

                return null;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
