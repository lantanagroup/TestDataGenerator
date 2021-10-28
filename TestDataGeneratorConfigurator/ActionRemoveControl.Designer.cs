namespace LantanaGroup.TestDataGenerator.Configurator
{
    partial class ActionRemoveControl
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
            this.removeBinding = new System.Windows.Forms.BindingSource(this.components);
            this.actionGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.removeBinding)).BeginInit();
            this.SuspendLayout();
            // 
            // actionGroup
            // 
            this.actionGroup.Text = "Action Details: Remove";
            // 
            // locationText
            // 
            this.locationText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.removeBinding, "ActionLocation", true));
            // 
            // removeBinding
            // 
            this.removeBinding.DataSource = typeof(LantanaGroup.TestDataGenerator.Configurator.Data.ActionRemoveDisplay);
            // 
            // ActionRemoveControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ActionRemoveControl";
            this.actionGroup.ResumeLayout(false);
            this.actionGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.removeBinding)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource removeBinding;
    }
}
