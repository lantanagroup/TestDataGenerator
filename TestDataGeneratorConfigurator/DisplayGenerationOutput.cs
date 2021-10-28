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
    public partial class DisplayGenerationOutput : Form
    {
        public DisplayGenerationOutput(string messagesLocation)
        {
            InitializeComponent();

            this.webBrowser1.Url = new Uri("file://" + messagesLocation);
        }
    }
}
