using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LantanaGroup.TestDataGenerator.Configurator
{
    public partial class NamespaceForm : Form
    {
        public NamespaceForm()
        {
            InitializeComponent();
        }

        public string NamespacePrefix
        {
            get
            {
                return this.prefixText.Text;
            }
        }

        public string NamespaceUri
        {
            get
            {
                return this.uriText.Text;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK && (string.IsNullOrEmpty(this.prefixLabel.Text) || string.IsNullOrEmpty(this.uriLabel.Text)))
            {
                MessageBox.Show("You must specify both a prefix and URI, or cancel.");
                e.Cancel = true;
                return;
            }

            base.OnClosing(e);
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
