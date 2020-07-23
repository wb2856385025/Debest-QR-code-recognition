namespace ProCommon.UserDefForm
{
    partial class FrmLogIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogIn));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.grpcOperationPromotion = new DevExpress.XtraEditors.GroupControl();
            this.lblErrorPromotion = new DevExpress.XtraEditors.LabelControl();
            this.sbtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnLogIn = new DevExpress.XtraEditors.SimpleButton();
            this.grpcAccountAndPassWord = new DevExpress.XtraEditors.GroupControl();
            this.txtePassWord = new DevExpress.XtraEditors.TextEdit();
            this.cmbeUserList = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblPassWord = new DevExpress.XtraEditors.LabelControl();
            this.lblAccount = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.peUser = new DevExpress.XtraEditors.PictureEdit();
            this.peLogo = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpcOperationPromotion)).BeginInit();
            this.grpcOperationPromotion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpcAccountAndPassWord)).BeginInit();
            this.grpcAccountAndPassWord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtePassWord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeUserList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peLogo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.grpcOperationPromotion);
            this.layoutControl1.Controls.Add(this.grpcAccountAndPassWord);
            this.layoutControl1.Controls.Add(this.peLogo);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(926, 161, 562, 500);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(564, 349);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // grpcOperationPromotion
            // 
            this.grpcOperationPromotion.Controls.Add(this.lblErrorPromotion);
            this.grpcOperationPromotion.Controls.Add(this.sbtnCancel);
            this.grpcOperationPromotion.Controls.Add(this.sbtnLogIn);
            this.grpcOperationPromotion.Location = new System.Drawing.Point(16, 211);
            this.grpcOperationPromotion.Name = "grpcOperationPromotion";
            this.grpcOperationPromotion.Size = new System.Drawing.Size(532, 108);
            this.grpcOperationPromotion.TabIndex = 6;
            this.grpcOperationPromotion.Tag = "GRPC_OPERATIONPROMOTION";
            this.grpcOperationPromotion.Text = "操作与提示";
            // 
            // lblErrorPromotion
            // 
            this.lblErrorPromotion.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorPromotion.Appearance.Options.UseFont = true;
            this.lblErrorPromotion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblErrorPromotion.Location = new System.Drawing.Point(2, 88);
            this.lblErrorPromotion.Name = "lblErrorPromotion";
            this.lblErrorPromotion.Size = new System.Drawing.Size(35, 18);
            this.lblErrorPromotion.TabIndex = 1;
            this.lblErrorPromotion.Tag = "LBL_ERRORPROMOTION";
            this.lblErrorPromotion.Text = "提示:";
            // 
            // sbtnCancel
            // 
            this.sbtnCancel.Location = new System.Drawing.Point(382, 44);
            this.sbtnCancel.Name = "sbtnCancel";
            this.sbtnCancel.Size = new System.Drawing.Size(75, 30);
            this.sbtnCancel.TabIndex = 0;
            this.sbtnCancel.Tag = "SBTN_CANCEL";
            this.sbtnCancel.Text = "取消";
            // 
            // sbtnLogIn
            // 
            this.sbtnLogIn.Location = new System.Drawing.Point(114, 44);
            this.sbtnLogIn.Name = "sbtnLogIn";
            this.sbtnLogIn.Size = new System.Drawing.Size(75, 30);
            this.sbtnLogIn.TabIndex = 0;
            this.sbtnLogIn.Tag = "SBTN_LOGIN";
            this.sbtnLogIn.Text = "登录";
            // 
            // grpcAccountAndPassWord
            // 
            this.grpcAccountAndPassWord.Controls.Add(this.txtePassWord);
            this.grpcAccountAndPassWord.Controls.Add(this.cmbeUserList);
            this.grpcAccountAndPassWord.Controls.Add(this.lblPassWord);
            this.grpcAccountAndPassWord.Controls.Add(this.lblAccount);
            this.grpcAccountAndPassWord.Controls.Add(this.pictureEdit1);
            this.grpcAccountAndPassWord.Controls.Add(this.peUser);
            this.grpcAccountAndPassWord.Location = new System.Drawing.Point(16, 80);
            this.grpcAccountAndPassWord.Name = "grpcAccountAndPassWord";
            this.grpcAccountAndPassWord.Size = new System.Drawing.Size(532, 125);
            this.grpcAccountAndPassWord.TabIndex = 5;
            this.grpcAccountAndPassWord.Tag = "GRPC_ACCOUNTPROMOTION";
            this.grpcAccountAndPassWord.Text = "用户与密码";
            // 
            // txtePassWord
            // 
            this.txtePassWord.Location = new System.Drawing.Point(190, 84);
            this.txtePassWord.Name = "txtePassWord";
            this.txtePassWord.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtePassWord.Properties.Appearance.Options.UseFont = true;
            this.txtePassWord.Properties.PasswordChar = '*';
            this.txtePassWord.Size = new System.Drawing.Size(267, 30);
            this.txtePassWord.TabIndex = 3;
            // 
            // cmbeUserList
            // 
            this.cmbeUserList.Location = new System.Drawing.Point(190, 41);
            this.cmbeUserList.Name = "cmbeUserList";
            this.cmbeUserList.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbeUserList.Properties.Appearance.Options.UseFont = true;
            this.cmbeUserList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbeUserList.Size = new System.Drawing.Size(267, 30);
            this.cmbeUserList.TabIndex = 2;
            // 
            // lblPassWord
            // 
            this.lblPassWord.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassWord.Appearance.Options.UseFont = true;
            this.lblPassWord.Location = new System.Drawing.Point(24, 87);
            this.lblPassWord.Name = "lblPassWord";
            this.lblPassWord.Size = new System.Drawing.Size(47, 24);
            this.lblPassWord.TabIndex = 1;
            this.lblPassWord.Tag = "LBL_PASSWORD";
            this.lblPassWord.Text = "密码:";
            // 
            // lblAccount
            // 
            this.lblAccount.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccount.Appearance.Options.UseFont = true;
            this.lblAccount.Location = new System.Drawing.Point(24, 41);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(47, 24);
            this.lblAccount.TabIndex = 1;
            this.lblAccount.Tag = "LBL_ACCOUNT";
            this.lblAccount.Text = "用户:";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(114, 76);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Size = new System.Drawing.Size(40, 40);
            this.pictureEdit1.TabIndex = 0;
            // 
            // peUser
            // 
            this.peUser.Cursor = System.Windows.Forms.Cursors.Default;
            this.peUser.EditValue = ((object)(resources.GetObject("peUser.EditValue")));
            this.peUser.Location = new System.Drawing.Point(114, 30);
            this.peUser.Name = "peUser";
            this.peUser.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peUser.Size = new System.Drawing.Size(40, 40);
            this.peUser.TabIndex = 0;
            // 
            // peLogo
            // 
            this.peLogo.Cursor = System.Windows.Forms.Cursors.Default;
            this.peLogo.Location = new System.Drawing.Point(16, 16);
            this.peLogo.Name = "peLogo";
            this.peLogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peLogo.Size = new System.Drawing.Size(532, 55);
            this.peLogo.StyleController = this.layoutControl1;
            toolTipTitleItem1.Text = "供应商";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "深圳市汇众智慧科技有限公司";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.peLogo.SuperTip = superToolTip1;
            this.peLogo.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.simpleSeparator1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(564, 349);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.peLogo;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(538, 61);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 309);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(538, 14);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 61);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(538, 3);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.grpcAccountAndPassWord;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 64);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(538, 131);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.grpcOperationPromotion;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 195);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(538, 114);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // FrmLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 349);
            this.Controls.Add(this.layoutControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "FRM_LOGIN";
            this.Text = "用户登录";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpcOperationPromotion)).EndInit();
            this.grpcOperationPromotion.ResumeLayout(false);
            this.grpcOperationPromotion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpcAccountAndPassWord)).EndInit();
            this.grpcAccountAndPassWord.ResumeLayout(false);
            this.grpcAccountAndPassWord.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtePassWord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeUserList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peLogo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.PictureEdit peLogo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        protected internal DevExpress.XtraEditors.ComboBoxEdit cmbeUserList;
        protected internal DevExpress.XtraEditors.TextEdit txtePassWord;
        protected internal DevExpress.XtraEditors.GroupControl grpcAccountAndPassWord;
        protected internal DevExpress.XtraEditors.LabelControl lblPassWord;
        protected internal DevExpress.XtraEditors.LabelControl lblAccount;
        protected internal DevExpress.XtraEditors.PictureEdit pictureEdit1;
        protected internal DevExpress.XtraEditors.PictureEdit peUser;
        protected internal DevExpress.XtraEditors.GroupControl grpcOperationPromotion;
        protected internal DevExpress.XtraEditors.SimpleButton sbtnCancel;
        protected internal DevExpress.XtraEditors.SimpleButton sbtnLogIn;
        protected internal DevExpress.XtraEditors.LabelControl lblErrorPromotion;
    }
}