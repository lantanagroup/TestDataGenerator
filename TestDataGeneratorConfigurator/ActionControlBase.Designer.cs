namespace LantanaGroup.TestDataGenerator.Configurator
{
    partial class ActionControlBase
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
            this.actionGroup = new System.Windows.Forms.GroupBox();
            this.locationXpathButton = new System.Windows.Forms.Button();
            this.locationLabel = new System.Windows.Forms.Label();
            this.locationText = new System.Windows.Forms.TextBox();
            this.actionGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // actionGroup
            // 
            this.actionGroup.Controls.Add(this.locationXpathButton);
            this.actionGroup.Controls.Add(this.locationLabel);
            this.actionGroup.Controls.Add(this.locationText);
            this.actionGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionGroup.Location = new System.Drawing.Point(0, 0);
            this.actionGroup.Name = "actionGroup";
            this.actionGroup.Size = new System.Drawing.Size(530, 125);
            this.actionGroup.TabIndex = 0;
            this.actionGroup.TabStop = false;
            this.actionGroup.Text = "Action Group";
            // 
            // locationXpathButton
            // 
            this.locationXpathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.locationXpathButton.Location = new System.Drawing.Point(441, 17);
            this.locationXpathButton.Name = "locationXpathButton";
            this.locationXpathButton.Size = new System.Drawing.Size(83, 23);
            this.locationXpathButton.TabIndex = 2;
            this.locationXpathButton.Text = "Build &XPATH";
            this.locationXpathButton.UseVisualStyleBackColor = true;
            this.locationXpathButton.Click += new System.EventHandler(this.LocationXpathClicked);
            // 
            // locationLabel
            // 
            this.locationLabel.AutoSize = true;
            this.locationLabel.Location = new System.Drawing.Point(6, 22);
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(51, 13);
            this.locationLabel.TabIndex = 1;
            this.locationLabel.Text = "Location:";
            // 
            // locationText
            // 
            this.locationText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.locationText.Location = new System.Drawing.Point(63, 19);
            this.locationText.Name = "locationText";
            this.locationText.Size = new System.Drawing.Size(372, 20);
            this.locationText.TabIndex = 0;
            // 
            // ActionControlBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.actionGroup);
            this.Name = "ActionControlBase";
            this.Size = new System.Drawing.Size(530, 125);
            this.actionGroup.ResumeLayout(false);
            this.actionGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button locationXpathButton;
        private System.Windows.Forms.Label locationLabel;
        protected System.Windows.Forms.GroupBox actionGroup;
        protected System.Windows.Forms.TextBox locationText;
    }
}
