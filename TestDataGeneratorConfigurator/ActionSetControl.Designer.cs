namespace LantanaGroup.TestDataGenerator.Configurator
{
    partial class ActionSetControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.valueText = new System.Windows.Forms.TextBox();
            this.setBinding = new System.Windows.Forms.BindingSource(this.components);
            this.valueLabel = new System.Windows.Forms.Label();
            this.actionGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.setBinding)).BeginInit();
            this.SuspendLayout();
            // 
            // actionGroup
            // 
            this.actionGroup.Controls.Add(this.valueText);
            this.actionGroup.Controls.Add(this.valueLabel);
            this.actionGroup.Text = "Action Details: Set";
            this.actionGroup.Controls.SetChildIndex(this.locationText, 0);
            this.actionGroup.Controls.SetChildIndex(this.valueLabel, 0);
            this.actionGroup.Controls.SetChildIndex(this.valueText, 0);
            // 
            // locationText
            // 
            this.locationText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.setBinding, "ActionLocation", true));
            // 
            // valueText
            // 
            this.valueText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.valueText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.setBinding, "ActionValue", true));
            this.valueText.Location = new System.Drawing.Point(63, 45);
            this.valueText.Name = "valueText";
            this.valueText.Size = new System.Drawing.Size(461, 20);
            this.valueText.TabIndex = 3;
            // 
            // setBinding
            // 
            this.setBinding.DataSource = typeof(LantanaGroup.TestDataGenerator.Configurator.Data.ActionSetDisplay);
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.Location = new System.Drawing.Point(6, 48);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(37, 13);
            this.valueLabel.TabIndex = 4;
            this.valueLabel.Text = "Value:";
            // 
            // ActionSetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ActionSetControl";
            this.actionGroup.ResumeLayout(false);
            this.actionGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.setBinding)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox valueText;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.BindingSource setBinding;
    }
}
