namespace LantanaGroup.TestDataGenerator.Configurator
{
    partial class BuildXpathForm
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
            this.documentTree = new System.Windows.Forms.TreeView();
            this.contentsGroup = new System.Windows.Forms.GroupBox();
            this.contentsText = new System.Windows.Forms.TextBox();
            this.selectButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.selectedXpath = new System.Windows.Forms.TextBox();
            this.contentsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // documentTree
            // 
            this.documentTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.documentTree.Location = new System.Drawing.Point(12, 12);
            this.documentTree.Name = "documentTree";
            this.documentTree.Size = new System.Drawing.Size(419, 394);
            this.documentTree.TabIndex = 0;
            this.documentTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.AfterDocumentNodeSelected);
            // 
            // contentsGroup
            // 
            this.contentsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.contentsGroup.Controls.Add(this.contentsText);
            this.contentsGroup.Location = new System.Drawing.Point(437, 12);
            this.contentsGroup.Name = "contentsGroup";
            this.contentsGroup.Padding = new System.Windows.Forms.Padding(10, 5, 10, 10);
            this.contentsGroup.Size = new System.Drawing.Size(411, 394);
            this.contentsGroup.TabIndex = 1;
            this.contentsGroup.TabStop = false;
            this.contentsGroup.Text = "Contents";
            // 
            // contentsText
            // 
            this.contentsText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentsText.Location = new System.Drawing.Point(10, 18);
            this.contentsText.Multiline = true;
            this.contentsText.Name = "contentsText";
            this.contentsText.ReadOnly = true;
            this.contentsText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.contentsText.Size = new System.Drawing.Size(391, 366);
            this.contentsText.TabIndex = 0;
            // 
            // selectButton
            // 
            this.selectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.selectButton.Location = new System.Drawing.Point(692, 412);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(75, 23);
            this.selectButton.TabIndex = 3;
            this.selectButton.Text = "&Select";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(773, 412);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // selectedXpath
            // 
            this.selectedXpath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedXpath.Location = new System.Drawing.Point(12, 414);
            this.selectedXpath.Name = "selectedXpath";
            this.selectedXpath.ReadOnly = true;
            this.selectedXpath.Size = new System.Drawing.Size(674, 20);
            this.selectedXpath.TabIndex = 2;
            // 
            // BuildXpathForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 447);
            this.Controls.Add(this.selectedXpath);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.contentsGroup);
            this.Controls.Add(this.documentTree);
            this.MinimizeBox = false;
            this.Name = "BuildXpathForm";
            this.Text = "Build XPATH";
            this.contentsGroup.ResumeLayout(false);
            this.contentsGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView documentTree;
        private System.Windows.Forms.GroupBox contentsGroup;
        private System.Windows.Forms.TextBox contentsText;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox selectedXpath;
    }
}