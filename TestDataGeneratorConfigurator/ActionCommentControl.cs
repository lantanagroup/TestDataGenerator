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
    public partial class ActionCommentControl : ActionControlBase
    {
        public ActionCommentControl()
        {
            InitializeComponent();
        }

        protected override void SetDisplayObject()
        {
            if (this.ActionDisplayObject is ActionCommentDisplay)
            {
                this.commentBinding.DataSource = this.ActionDisplayObject;
                this.commentBinding.ResetBindings(false);
            }
            else
            {
                this.commentBinding.DataSource = null;
            }
        }

        public override void Save()
        {
            base.Save();

            this.commentText.DataBindings["Text"].WriteValue();
        }
    }
}
