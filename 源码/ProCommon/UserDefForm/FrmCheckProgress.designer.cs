namespace ProCommon.UserDefForm
{
    partial class FrmCheckProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCheckProgress));
            this.mqpbCtrlProcess = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.lblCtrlRight = new DevExpress.XtraEditors.LabelControl();
            this.lblCtrlPrompt = new DevExpress.XtraEditors.LabelControl();
            this.peIndustril = new DevExpress.XtraEditors.PictureEdit();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.mqpbCtrlProcess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peIndustril.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mqpbCtrlProcess
            // 
            this.mqpbCtrlProcess.EditValue = 0;
            this.mqpbCtrlProcess.Location = new System.Drawing.Point(31, 262);
            this.mqpbCtrlProcess.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mqpbCtrlProcess.Name = "mqpbCtrlProcess";
            this.mqpbCtrlProcess.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mqpbCtrlProcess.Size = new System.Drawing.Size(539, 34);
            this.mqpbCtrlProcess.TabIndex = 5;
            // 
            // lblCtrlRight
            // 
            this.lblCtrlRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lblCtrlRight.Location = new System.Drawing.Point(31, 330);
            this.lblCtrlRight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lblCtrlRight.Name = "lblCtrlRight";
            this.lblCtrlRight.Size = new System.Drawing.Size(154, 18);
            this.lblCtrlRight.TabIndex = 6;
            this.lblCtrlRight.Text = "Copyright © 1998-2013";
            // 
            // lblCtrlPrompt
            // 
            this.lblCtrlPrompt.Location = new System.Drawing.Point(31, 236);
            this.lblCtrlPrompt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lblCtrlPrompt.Name = "lblCtrlPrompt";
            this.lblCtrlPrompt.Size = new System.Drawing.Size(64, 18);
            this.lblCtrlPrompt.TabIndex = 7;
            this.lblCtrlPrompt.Text = "Starting...";
            // 
            // peIndustril
            // 
            this.peIndustril.Cursor = System.Windows.Forms.Cursors.Default;
            this.peIndustril.Location = new System.Drawing.Point(16, 14);
            this.peIndustril.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.peIndustril.Name = "peIndustril";
            this.peIndustril.Properties.AllowFocused = false;
            this.peIndustril.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peIndustril.Properties.Appearance.Options.UseBackColor = true;
            this.peIndustril.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peIndustril.Properties.ShowMenu = false;
            this.peIndustril.Properties.ZoomAccelerationFactor = 1D;
            this.peIndustril.Size = new System.Drawing.Size(568, 208);
            this.peIndustril.TabIndex = 9;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(402, 302);
            this.pictureEdit1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Properties.ZoomAccelerationFactor = 1D;
            this.pictureEdit1.Size = new System.Drawing.Size(177, 63);
            this.pictureEdit1.TabIndex = 8;
            // 
            // FrmCheckProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 377);
            this.Controls.Add(this.peIndustril);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.lblCtrlPrompt);
            this.Controls.Add(this.lblCtrlRight);
            this.Controls.Add(this.mqpbCtrlProcess);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FrmCheckProgress";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.mqpbCtrlProcess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peIndustril.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected internal DevExpress.XtraEditors.MarqueeProgressBarControl mqpbCtrlProcess;
        protected internal DevExpress.XtraEditors.LabelControl lblCtrlRight;
        protected internal DevExpress.XtraEditors.LabelControl lblCtrlPrompt;
        protected internal DevExpress.XtraEditors.PictureEdit pictureEdit1;
        protected internal DevExpress.XtraEditors.PictureEdit peIndustril;
    }
}
