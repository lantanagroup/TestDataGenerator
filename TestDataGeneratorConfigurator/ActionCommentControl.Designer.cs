namespace LantanaGroup.TestDataGenerator.Configurator
{
    partial class ActionCommentControl
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
            this.commentText = new System.Windows.Forms.TextBox();
            this.commentBinding = new System.Windows.Forms.BindingSource(this.components);
            this.commentLabel = new System.Windows.Forms.Label();
            this.actionGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commentBinding)).BeginInit();
            this.SuspendLayout();
            // 
            // actionGroup
            // 
            this.actionGroup.Controls.Add(this.commentText);
            this.actionGroup.Controls.Add(this.commentLabel);
            this.actionGroup.Text = "Action Details: Comment";
            this.actionGroup.Controls.SetChildIndex(this.locationText, 0);
            this.actionGroup.Controls.SetChildIndex(this.commentLabel, 0);
            this.actionGroup.Controls.SetChildIndex(this.commentText, 0);
            // 
            // locationText
            // 
            this.locationText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.commentBinding, "ActionLocation", true));
            // 
            // commentText
            // 
            this.commentText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.commentText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.commentBinding, "ActionComment", true));
            this.commentText.Location = new System.Drawing.Point(63, 45);
            this.commentText.Name = "commentText";
            this.commentText.Size = new System.Drawing.Size(461, 20);
            this.commentText.TabIndex = 3;
            // 
            // commentBinding
            // 
            this.commentBinding.DataSource = typeof(LantanaGroup.TestDataGenerator.Configurator.Data.ActionCommentDisplay);
            // 
            // commentLabel
            // 
            this.commentLabel.AutoSize = true;
            this.commentLabel.Location = new System.Drawing.Point(6, 48);
            this.commentLabel.Name = "commentLabel";
            this.commentLabel.Size = new System.Drawing.Size(54, 13);
            this.commentLabel.TabIndex = 4;
            this.commentLabel.Text = "Comment:";
            // 
            // ActionCommentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ActionCommentControl";
            this.actionGroup.ResumeLayout(false);
            this.actionGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commentBinding)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox commentText;
        private System.Windows.Forms.Label commentLabel;
        private System.Windows.Forms.BindingSource commentBinding;
    }
}
