namespace LantanaGroup.TestDataGenerator.Configurator
{
    partial class ActionAddControl
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
            this.elementRadio = new System.Windows.Forms.RadioButton();
            this.addBinding = new System.Windows.Forms.BindingSource(this.components);
            this.elementNameLabel = new System.Windows.Forms.Label();
            this.elementNameText = new System.Windows.Forms.TextBox();
            this.elementDataButton = new System.Windows.Forms.Button();
            this.attributeRadio = new System.Windows.Forms.RadioButton();
            this.attributeNameLabel = new System.Windows.Forms.Label();
            this.attributeNameText = new System.Windows.Forms.TextBox();
            this.attributeValueLabel = new System.Windows.Forms.Label();
            this.attributeValueText = new System.Windows.Forms.TextBox();
            this.elementBeforeText = new System.Windows.Forms.TextBox();
            this.elementAfterText = new System.Windows.Forms.TextBox();
            this.beforeLabel = new System.Windows.Forms.Label();
            this.afterLabel = new System.Windows.Forms.Label();
            this.actionGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addBinding)).BeginInit();
            this.SuspendLayout();
            // 
            // actionGroup
            // 
            this.actionGroup.Controls.Add(this.afterLabel);
            this.actionGroup.Controls.Add(this.beforeLabel);
            this.actionGroup.Controls.Add(this.elementAfterText);
            this.actionGroup.Controls.Add(this.elementBeforeText);
            this.actionGroup.Controls.Add(this.attributeValueLabel);
            this.actionGroup.Controls.Add(this.attributeValueText);
            this.actionGroup.Controls.Add(this.attributeNameLabel);
            this.actionGroup.Controls.Add(this.attributeNameText);
            this.actionGroup.Controls.Add(this.attributeRadio);
            this.actionGroup.Controls.Add(this.elementRadio);
            this.actionGroup.Controls.Add(this.elementNameLabel);
            this.actionGroup.Controls.Add(this.elementNameText);
            this.actionGroup.Controls.Add(this.elementDataButton);
            this.actionGroup.Text = "Action Details: Add";
            this.actionGroup.Controls.SetChildIndex(this.locationText, 0);
            this.actionGroup.Controls.SetChildIndex(this.elementDataButton, 0);
            this.actionGroup.Controls.SetChildIndex(this.elementNameText, 0);
            this.actionGroup.Controls.SetChildIndex(this.elementNameLabel, 0);
            this.actionGroup.Controls.SetChildIndex(this.elementRadio, 0);
            this.actionGroup.Controls.SetChildIndex(this.attributeRadio, 0);
            this.actionGroup.Controls.SetChildIndex(this.attributeNameText, 0);
            this.actionGroup.Controls.SetChildIndex(this.attributeNameLabel, 0);
            this.actionGroup.Controls.SetChildIndex(this.attributeValueText, 0);
            this.actionGroup.Controls.SetChildIndex(this.attributeValueLabel, 0);
            this.actionGroup.Controls.SetChildIndex(this.elementBeforeText, 0);
            this.actionGroup.Controls.SetChildIndex(this.elementAfterText, 0);
            this.actionGroup.Controls.SetChildIndex(this.beforeLabel, 0);
            this.actionGroup.Controls.SetChildIndex(this.afterLabel, 0);
            // 
            // locationText
            // 
            this.locationText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.addBinding, "ActionLocation", true));
            this.locationText.TextChanged += new System.EventHandler(this.ValuesChanged);
            // 
            // elementRadio
            // 
            this.elementRadio.AutoSize = true;
            this.elementRadio.Checked = true;
            this.elementRadio.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.addBinding, "IsActionAddElement", true));
            this.elementRadio.Location = new System.Drawing.Point(9, 45);
            this.elementRadio.Name = "elementRadio";
            this.elementRadio.Size = new System.Drawing.Size(63, 17);
            this.elementRadio.TabIndex = 3;
            this.elementRadio.TabStop = true;
            this.elementRadio.Text = "Element";
            this.elementRadio.UseVisualStyleBackColor = true;
            this.elementRadio.CheckedChanged += new System.EventHandler(this.ActionAddTypeChanged);
            // 
            // addBinding
            // 
            this.addBinding.DataSource = typeof(LantanaGroup.TestDataGenerator.Configurator.Data.ActionAddDisplay);
            // 
            // elementNameLabel
            // 
            this.elementNameLabel.AutoSize = true;
            this.elementNameLabel.Location = new System.Drawing.Point(6, 71);
            this.elementNameLabel.Name = "elementNameLabel";
            this.elementNameLabel.Size = new System.Drawing.Size(38, 13);
            this.elementNameLabel.TabIndex = 4;
            this.elementNameLabel.Text = "Name:";
            // 
            // elementNameText
            // 
            this.elementNameText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.addBinding, "ActionElementName", true));
            this.elementNameText.Location = new System.Drawing.Point(50, 68);
            this.elementNameText.Name = "elementNameText";
            this.elementNameText.Size = new System.Drawing.Size(116, 20);
            this.elementNameText.TabIndex = 5;
            this.elementNameText.TextChanged += new System.EventHandler(this.ValuesChanged);
            // 
            // elementDataButton
            // 
            this.elementDataButton.Location = new System.Drawing.Point(50, 94);
            this.elementDataButton.Name = "elementDataButton";
            this.elementDataButton.Size = new System.Drawing.Size(116, 23);
            this.elementDataButton.TabIndex = 6;
            this.elementDataButton.Text = "&Set Element Data";
            this.elementDataButton.UseVisualStyleBackColor = true;
            this.elementDataButton.Click += new System.EventHandler(this.ElementDataButtonClicked);
            // 
            // attributeRadio
            // 
            this.attributeRadio.AutoSize = true;
            this.attributeRadio.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.addBinding, "IsActionAddAttribute", true));
            this.attributeRadio.Location = new System.Drawing.Point(372, 47);
            this.attributeRadio.Name = "attributeRadio";
            this.attributeRadio.Size = new System.Drawing.Size(64, 17);
            this.attributeRadio.TabIndex = 7;
            this.attributeRadio.Text = "Attribute";
            this.attributeRadio.UseVisualStyleBackColor = true;
            this.attributeRadio.CheckedChanged += new System.EventHandler(this.ActionAddTypeChanged);
            // 
            // attributeNameLabel
            // 
            this.attributeNameLabel.AutoSize = true;
            this.attributeNameLabel.Location = new System.Drawing.Point(343, 71);
            this.attributeNameLabel.Name = "attributeNameLabel";
            this.attributeNameLabel.Size = new System.Drawing.Size(38, 13);
            this.attributeNameLabel.TabIndex = 8;
            this.attributeNameLabel.Text = "Name:";
            // 
            // attributeNameText
            // 
            this.attributeNameText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.addBinding, "ActionAttributeName", true));
            this.attributeNameText.Location = new System.Drawing.Point(386, 68);
            this.attributeNameText.Name = "attributeNameText";
            this.attributeNameText.Size = new System.Drawing.Size(138, 20);
            this.attributeNameText.TabIndex = 9;
            this.attributeNameText.TextChanged += new System.EventHandler(this.ValuesChanged);
            // 
            // attributeValueLabel
            // 
            this.attributeValueLabel.AutoSize = true;
            this.attributeValueLabel.Location = new System.Drawing.Point(343, 99);
            this.attributeValueLabel.Name = "attributeValueLabel";
            this.attributeValueLabel.Size = new System.Drawing.Size(37, 13);
            this.attributeValueLabel.TabIndex = 10;
            this.attributeValueLabel.Text = "Value:";
            // 
            // attributeValueText
            // 
            this.attributeValueText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.addBinding, "ActionAttributeValue", true));
            this.attributeValueText.Location = new System.Drawing.Point(386, 97);
            this.attributeValueText.Name = "attributeValueText";
            this.attributeValueText.Size = new System.Drawing.Size(138, 20);
            this.attributeValueText.TabIndex = 11;
            this.attributeValueText.TextChanged += new System.EventHandler(this.ValuesChanged);
            // 
            // elementBeforeText
            // 
            this.elementBeforeText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.addBinding, "ActionElementBefore", true));
            this.elementBeforeText.Location = new System.Drawing.Point(220, 68);
            this.elementBeforeText.Name = "elementBeforeText";
            this.elementBeforeText.Size = new System.Drawing.Size(117, 20);
            this.elementBeforeText.TabIndex = 12;
            this.elementBeforeText.TextChanged += new System.EventHandler(this.ValuesChanged);
            // 
            // elementAfterText
            // 
            this.elementAfterText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.addBinding, "ActionElementAfter", true));
            this.elementAfterText.Location = new System.Drawing.Point(220, 97);
            this.elementAfterText.Name = "elementAfterText";
            this.elementAfterText.Size = new System.Drawing.Size(117, 20);
            this.elementAfterText.TabIndex = 13;
            this.elementAfterText.TextChanged += new System.EventHandler(this.ValuesChanged);
            // 
            // beforeLabel
            // 
            this.beforeLabel.AutoSize = true;
            this.beforeLabel.Location = new System.Drawing.Point(172, 71);
            this.beforeLabel.Name = "beforeLabel";
            this.beforeLabel.Size = new System.Drawing.Size(41, 13);
            this.beforeLabel.TabIndex = 14;
            this.beforeLabel.Text = "Before:";
            // 
            // afterLabel
            // 
            this.afterLabel.AutoSize = true;
            this.afterLabel.Location = new System.Drawing.Point(172, 99);
            this.afterLabel.Name = "afterLabel";
            this.afterLabel.Size = new System.Drawing.Size(32, 13);
            this.afterLabel.TabIndex = 15;
            this.afterLabel.Text = "After:";
            // 
            // ActionAddControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ActionAddControl";
            this.actionGroup.ResumeLayout(false);
            this.actionGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addBinding)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label attributeValueLabel;
        private System.Windows.Forms.TextBox attributeValueText;
        private System.Windows.Forms.Label attributeNameLabel;
        private System.Windows.Forms.TextBox attributeNameText;
        private System.Windows.Forms.RadioButton attributeRadio;
        private System.Windows.Forms.RadioButton elementRadio;
        private System.Windows.Forms.Label elementNameLabel;
        private System.Windows.Forms.TextBox elementNameText;
        private System.Windows.Forms.Button elementDataButton;
        private System.Windows.Forms.Label afterLabel;
        private System.Windows.Forms.Label beforeLabel;
        private System.Windows.Forms.TextBox elementAfterText;
        private System.Windows.Forms.TextBox elementBeforeText;
        private System.Windows.Forms.BindingSource addBinding;

    }
}
