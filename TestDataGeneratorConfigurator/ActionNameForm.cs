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
    public partial class ActionNameForm : Form
    {
        public ActionNameForm()
        {
            InitializeComponent();
        }

        public string ActionName
        {
            get
            {
                return this.actionNameText.Text;
            }
            set
            {
                this.actionNameText.Text = value;
            }
        }
    }
}
