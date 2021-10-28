namespace LantanaGroup.TestDataGenerator.Configurator
{
    partial class NewConfigForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.configLocationText = new System.Windows.Forms.TextBox();
            this.configLocationLabel = new System.Windows.Forms.Label();
            this.masterFileLocationLabel = new System.Windows.Forms.Label();
            this.masterFileLocationText = new System.Windows.Forms.TextBox();
            this.dataSourceLocationLabel = new System.Windows.Forms.Label();
            this.dataSourceLocationText = new System.Windows.Forms.TextBox();
            this.browseConfigButton = new System.Windows.Forms.Button();
            this.browseMasterFileButton = new System.Windows.Forms.Button();
            this.browseDataSourceLocation = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // configLocationText
            // 
            this.configLocationText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.configLocationText.Location = new System.Drawing.Point(132, 12);
            this.configLocationText.Name = "configLocationText";
            this.configLocationText.ReadOnly = true;
            this.configLocationText.Size = new System.Drawing.Size(257, 20);
            this.configLocationText.TabIndex = 1;
            // 
            // configLocationLabel
            // 
            this.configLocationLabel.AutoSize = true;
            this.configLocationLabel.Location = new System.Drawing.Point(12, 15);
            this.configLocationLabel.Name = "configLocationLabel";
            this.configLocationLabel.Size = new System.Drawing.Size(84, 13);
            this.configLocationLabel.TabIndex = 0;
            this.configLocationLabel.Text = "&Config Location:";
            // 
            // masterFileLocationLabel
            // 
            this.masterFileLocationLabel.AutoSize = true;
            this.masterFileLocationLabel.Location = new System.Drawing.Point(12, 41);
            this.masterFileLocationLabel.Name = "masterFileLocationLabel";
            this.masterFileLocationLabel.Size = new System.Drawing.Size(105, 13);
            this.masterFileLocationLabel.TabIndex = 3;
            this.masterFileLocationLabel.Text = "&Master File Location:";
            // 
            // masterFileLocationText
            // 
            this.masterFileLocationText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.masterFileLocationText.Location = new System.Drawing.Point(132, 38);
            this.masterFileLocationText.Name = "masterFileLocationText";
            this.masterFileLocationText.ReadOnly = true;
            this.masterFileLocationText.Size = new System.Drawing.Size(257, 20);
            this.masterFileLocationText.TabIndex = 4;
            // 
            // dataSourceLocationLabel
            // 
            this.dataSourceLocationLabel.AutoSize = true;
            this.dataSourceLocationLabel.Location = new System.Drawing.Point(12, 67);
            this.dataSourceLocationLabel.Name = "dataSourceLocationLabel";
            this.dataSourceLocationLabel.Size = new System.Drawing.Size(114, 13);
            this.dataSourceLocationLabel.TabIndex = 6;
            this.dataSourceLocationLabel.Text = "&Data Source Location:";
            // 
            // dataSourceLocationText
            // 
            this.dataSourceLocationText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataSourceLocationText.Location = new System.Drawing.Point(132, 64);
            this.dataSourceLocationText.Name = "dataSourceLocationText";
            this.dataSourceLocationText.Size = new System.Drawing.Size(257, 20);
            this.dataSourceLocationText.TabIndex = 7;
            this.toolTip.SetToolTip(this.dataSourceLocationText, "Can be either an ODBC connection string or an XLS file path.");
            // 
            // browseConfigButton
            // 
            this.browseConfigButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseConfigButton.Location = new System.Drawing.Point(395, 10);
            this.browseConfigButton.Name = "browseConfigButton";
            this.browseConfigButton.Size = new System.Drawing.Size(75, 23);
            this.browseConfigButton.TabIndex = 2;
            this.browseConfigButton.Text = "Br&owse";
            this.browseConfigButton.UseVisualStyleBackColor = true;
            this.browseConfigButton.Click += new System.EventHandler(this.browseConfigButton_Click);
            // 
            // browseMasterFileButton
            // 
            this.browseMasterFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseMasterFileButton.Location = new System.Drawing.Point(395, 36);
            this.browseMasterFileButton.Name = "browseMasterFileButton";
            this.browseMasterFileButton.Size = new System.Drawing.Size(75, 23);
            this.browseMasterFileButton.TabIndex = 5;
            this.browseMasterFileButton.Text = "Bro&wse";
            this.browseMasterFileButton.UseVisualStyleBackColor = true;
            this.browseMasterFileButton.Click += new System.EventHandler(this.browseMasterFileButton_Click);
            // 
            // browseDataSourceLocation
            // 
            this.browseDataSourceLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseDataSourceLocation.Location = new System.Drawing.Point(395, 62);
            this.browseDataSourceLocation.Name = "browseDataSourceLocation";
            this.browseDataSourceLocation.Size = new System.Drawing.Size(75, 23);
            this.browseDataSourceLocation.TabIndex = 8;
            this.browseDataSourceLocation.Text = "Brow&se";
            this.browseDataSourceLocation.UseVisualStyleBackColor = true;
            this.browseDataSourceLocation.Click += new System.EventHandler(this.browseDataSourceLocation_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(395, 91);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(314, 91);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // NewConfigForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(482, 124);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.browseDataSourceLocation);
            this.Controls.Add(this.browseMasterFileButton);
            this.Controls.Add(this.browseConfigButton);
            this.Controls.Add(this.dataSourceLocationLabel);
            this.Controls.Add(this.dataSourceLocationText);
            this.Controls.Add(this.masterFileLocationLabel);
            this.Controls.Add(this.masterFileLocationText);
            this.Controls.Add(this.configLocationLabel);
            this.Controls.Add(this.configLocationText);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(700, 162);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(498, 162);
            this.Name = "NewConfigForm";
            this.Text = "New Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox configLocationText;
        private System.Windows.Forms.Label configLocationLabel;
        private System.Windows.Forms.Label masterFileLocationLabel;
        private System.Windows.Forms.TextBox masterFileLocationText;
        private System.Windows.Forms.Label dataSourceLocationLabel;
        private System.Windows.Forms.TextBox dataSourceLocationText;
        private System.Windows.Forms.Button browseConfigButton;
        private System.Windows.Forms.Button browseMasterFileButton;
        private System.Windows.Forms.Button browseDataSourceLocation;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ToolTip toolTip;
    }
}