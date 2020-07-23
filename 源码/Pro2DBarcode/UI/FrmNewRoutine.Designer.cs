namespace Pro2DBarcode.UI
{
    partial class FrmNewRoutine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNewRoutine));
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.txteNewRoutineName = new DevExpress.XtraEditors.TextEdit();
            this.lblPromotion = new DevExpress.XtraEditors.LabelControl();
            this.sbtnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txteNewRoutineName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(36, 22);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Size = new System.Drawing.Size(63, 48);
            this.pictureEdit1.TabIndex = 0;
            // 
            // txteNewRoutineName
            // 
            this.txteNewRoutineName.Location = new System.Drawing.Point(124, 36);
            this.txteNewRoutineName.Name = "txteNewRoutineName";
            this.txteNewRoutineName.Size = new System.Drawing.Size(229, 24);
            this.txteNewRoutineName.TabIndex = 1;
            this.txteNewRoutineName.Tag = "";
            // 
            // lblPromotion
            // 
            this.lblPromotion.Appearance.Font = new System.Drawing.Font("DengXian", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPromotion.Appearance.Options.UseFont = true;
            this.lblPromotion.Location = new System.Drawing.Point(124, 12);
            this.lblPromotion.Name = "lblPromotion";
            this.lblPromotion.Size = new System.Drawing.Size(190, 19);
            this.lblPromotion.TabIndex = 2;
            this.lblPromotion.Tag = "LBLC_PROMOTION";
            this.lblPromotion.Text = "请输入新程式的名称：";
            // 
            // sbtnConfirm
            // 
            this.sbtnConfirm.Location = new System.Drawing.Point(124, 85);
            this.sbtnConfirm.Name = "sbtnConfirm";
            this.sbtnConfirm.Size = new System.Drawing.Size(82, 38);
            this.sbtnConfirm.TabIndex = 3;
            this.sbtnConfirm.Tag = "SBTN_CONFIRM";
            this.sbtnConfirm.Text = "确认";
            // 
            // sbtnCancel
            // 
            this.sbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbtnCancel.Location = new System.Drawing.Point(271, 85);
            this.sbtnCancel.Name = "sbtnCancel";
            this.sbtnCancel.Size = new System.Drawing.Size(82, 38);
            this.sbtnCancel.TabIndex = 3;
            this.sbtnCancel.Tag = "SBTN_CANCEL";
            this.sbtnCancel.Text = "取消";
            // 
            // FrmNewRoutine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 147);
            this.ControlBox = false;
            this.Controls.Add(this.sbtnCancel);
            this.Controls.Add(this.sbtnConfirm);
            this.Controls.Add(this.lblPromotion);
            this.Controls.Add(this.txteNewRoutineName);
            this.Controls.Add(this.pictureEdit1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmNewRoutine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "FRM_NEWROUTINE";
            this.Text = "创建新程式对话框";
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txteNewRoutineName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.TextEdit txteNewRoutineName;
        public DevExpress.XtraEditors.SimpleButton sbtnConfirm;
        public DevExpress.XtraEditors.SimpleButton sbtnCancel;
        protected internal DevExpress.XtraEditors.LabelControl lblPromotion;
    }
}