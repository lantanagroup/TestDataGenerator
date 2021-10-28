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
    public partial class NewConfigForm : Form
    {
        public NewConfigForm()
        {
            InitializeComponent();
        }

        public NewConfigForm(string configLocation, string masterFileLocation, string dataSourceLocation)
        {
            InitializeComponent();

            this.configLocationText.Text = configLocation;
            this.masterFileLocationText.Text = masterFileLocation;
            this.dataSourceLocationText.Text = dataSourceLocation;

            this.browseConfigButton.Enabled = false;
            this.Text = "Change Configuration";
        }

        public string ConfigLocation
        {
            get
            {
                return this.configLocationText.Text;
            }
        }

        public string MasterFileLocation
        {
            get
            {
                return this.masterFileLocationText.Text;
            }
        }

        public string DataSourceLocation
        {
            get
            {
                return this.dataSourceLocationText.Text;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK &&
                (string.IsNullOrEmpty(this.configLocationText.Text) ||
                string.IsNullOrEmpty(this.masterFileLocationText.Text) ||
                string.IsNullOrEmpty(this.dataSourceLocationText.Text)))
            {
                MessageBox.Show("You must specify all fields before continuing, or cancel.");
                return;
            }

            base.OnClosing(e);
        }

        private void browseConfigButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML Files (*.xml)|*.xml";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.configLocationText.Text = sfd.FileName;
            }
        }

        private void browseMasterFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Files (*.xml)|*.xml";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.masterFileLocationText.Text = ofd.FileName;
            }
        }

        private void browseDataSourceLocation_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files (*.xls)|*.xls";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.dataSourceLocationText.Text = ofd.FileName;
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
