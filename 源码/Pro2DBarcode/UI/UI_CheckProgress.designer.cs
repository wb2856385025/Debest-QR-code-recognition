namespace Pro2DBarcode.UI
{
    partial class UI_CheckProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_CheckProgress));
            ((System.ComponentModel.ISupportInitialize)(this.mqpbCtrlProcess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peIndustril.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mqpbCtrlProcess
            // 
            this.mqpbCtrlProcess.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblCtrlRight
            // 
            this.lblCtrlRight.Text = "Copyright © 2015-2020";
            // 
            // lblCtrlPrompt
            // 
            this.lblCtrlPrompt.Size = new System.Drawing.Size(59, 18);
            this.lblCtrlPrompt.Text = "Starting..";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(311, 302);
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Size = new System.Drawing.Size(259, 93);
            // 
            // peIndustril
            // 
            this.peIndustril.EditValue = ((object)(resources.GetObject("peIndustril.EditValue")));
            this.peIndustril.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peIndustril.Properties.Appearance.Options.UseBackColor = true;
            // 
            // UI_CheckProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(628, 407);
            this.Name = "UI_CheckProgress";
            ((System.ComponentModel.ISupportInitialize)(this.mqpbCtrlProcess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peIndustril.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
