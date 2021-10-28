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
    public partial class AddElementDataForm : Form
    {
        public AddElementDataForm(string elementData)
        {
            InitializeComponent();

            this.elementDataText.Text = elementData;
        }

        public AddElementDataForm()
        {
            InitializeComponent();
        }

        public string AddElementData
        {
            get
            {
                return this.elementDataText.Text;
            }
        }

        private void OkButtonClicked(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButtonClicked(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
