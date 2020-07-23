namespace ProCommon.UserDefForm
{
    partial class FrmModifyRegion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmModifyRegion));
            this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.hWndCtrlDisplay = new HalconDotNet.HWindowControl();
            this.pcRoot = new DevExpress.XtraEditors.PanelControl();
            this.lblRemark = new DevExpress.XtraEditors.LabelControl();
            this.lblAreasList = new DevExpress.XtraEditors.LabelControl();
            this.lblPrompt = new DevExpress.XtraEditors.LabelControl();
            this.cmbeDefineAreaList = new DevExpress.XtraEditors.ComboBoxEdit();
            this.sbtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnLoadImage = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnEraseRegion = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnDefineRegion = new DevExpress.XtraEditors.SimpleButton();
            this.tlpRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcRoot)).BeginInit();
            this.pcRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeDefineAreaList.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpRoot
            // 
            this.tlpRoot.ColumnCount = 2;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.07738F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.92262F));
            this.tlpRoot.Controls.Add(this.hWndCtrlDisplay, 0, 0);
            this.tlpRoot.Controls.Add(this.pcRoot, 1, 0);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(0, 0);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 1;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRoot.Size = new System.Drawing.Size(1119, 649);
            this.tlpRoot.TabIndex = 0;
            // 
            // hWndCtrlDisplay
            // 
            this.hWndCtrlDisplay.BackColor = System.Drawing.Color.LightCoral;
            this.hWndCtrlDisplay.BorderColor = System.Drawing.Color.LightCoral;
            this.hWndCtrlDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWndCtrlDisplay.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWndCtrlDisplay.Location = new System.Drawing.Point(3, 3);
            this.hWndCtrlDisplay.Name = "hWndCtrlDisplay";
            this.hWndCtrlDisplay.Size = new System.Drawing.Size(800, 643);
            this.hWndCtrlDisplay.TabIndex = 0;
            this.hWndCtrlDisplay.WindowSize = new System.Drawing.Size(800, 643);
            // 
            // pcRoot
            // 
            this.pcRoot.Controls.Add(this.lblRemark);
            this.pcRoot.Controls.Add(this.lblAreasList);
            this.pcRoot.Controls.Add(this.lblPrompt);
            this.pcRoot.Controls.Add(this.cmbeDefineAreaList);
            this.pcRoot.Controls.Add(this.sbtnExit);
            this.pcRoot.Controls.Add(this.sbtnLoadImage);
            this.pcRoot.Controls.Add(this.sbtnEraseRegion);
            this.pcRoot.Controls.Add(this.sbtnDefineRegion);
            this.pcRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcRoot.Location = new System.Drawing.Point(809, 3);
            this.pcRoot.Name = "pcRoot";
            this.pcRoot.Size = new System.Drawing.Size(307, 643);
            this.pcRoot.TabIndex = 1;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblRemark.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRemark.Location = new System.Drawing.Point(2, 495);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(303, 146);
            this.lblRemark.TabIndex = 4;
            this.lblRemark.Tag = "LBLC_REAMARK";
            this.lblRemark.Text = "备注:";
            // 
            // lblAreasList
            // 
            this.lblAreasList.Location = new System.Drawing.Point(13, 265);
            this.lblAreasList.Name = "lblAreasList";
            this.lblAreasList.Size = new System.Drawing.Size(90, 18);
            this.lblAreasList.TabIndex = 3;
            this.lblAreasList.Tag = "LBLC_AREALIST";
            this.lblAreasList.Text = "定义区域列表";
            // 
            // lblPrompt
            // 
            this.lblPrompt.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPrompt.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPrompt.Location = new System.Drawing.Point(2, 2);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(303, 146);
            this.lblPrompt.TabIndex = 2;
            this.lblPrompt.Tag = "LBLC_PROMPT";
            this.lblPrompt.Text = "提示:";
            // 
            // cmbeDefineAreaList
            // 
            this.cmbeDefineAreaList.Location = new System.Drawing.Point(13, 289);
            this.cmbeDefineAreaList.Name = "cmbeDefineAreaList";
            this.cmbeDefineAreaList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbeDefineAreaList.Size = new System.Drawing.Size(100, 24);
            this.cmbeDefineAreaList.TabIndex = 1;
            // 
            // sbtnExit
            // 
            this.sbtnExit.Location = new System.Drawing.Point(170, 374);
            this.sbtnExit.Name = "sbtnExit";
            this.sbtnExit.Size = new System.Drawing.Size(107, 40);
            this.sbtnExit.TabIndex = 0;
            this.sbtnExit.Tag = "SBTN_EXIT";
            this.sbtnExit.Text = "退出";
            // 
            // sbtnLoadImage
            // 
            this.sbtnLoadImage.Location = new System.Drawing.Point(13, 154);
            this.sbtnLoadImage.Name = "sbtnLoadImage";
            this.sbtnLoadImage.Size = new System.Drawing.Size(107, 40);
            this.sbtnLoadImage.TabIndex = 0;
            this.sbtnLoadImage.Tag = "SBTN_LOAD";
            this.sbtnLoadImage.Text = "加载图像";
            // 
            // sbtnEraseRegion
            // 
            this.sbtnEraseRegion.Location = new System.Drawing.Point(170, 273);
            this.sbtnEraseRegion.Name = "sbtnEraseRegion";
            this.sbtnEraseRegion.Size = new System.Drawing.Size(107, 40);
            this.sbtnEraseRegion.TabIndex = 0;
            this.sbtnEraseRegion.Tag = "SBTN_ERASEREGION";
            this.sbtnEraseRegion.Text = "擦除区域";
            // 
            // sbtnDefineRegion
            // 
            this.sbtnDefineRegion.Location = new System.Drawing.Point(170, 154);
            this.sbtnDefineRegion.Name = "sbtnDefineRegion";
            this.sbtnDefineRegion.Size = new System.Drawing.Size(107, 40);
            this.sbtnDefineRegion.TabIndex = 0;
            this.sbtnDefineRegion.Tag = "SBTN_DEFINEREGION";
            this.sbtnDefineRegion.Text = "定义区域";
            // 
            // FrmModifyRegion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 649);
            this.Controls.Add(this.tlpRoot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmModifyRegion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "FRM_MODIFYREGION";
            this.Text = "区域定义与修改";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRegionModify_FormClosing);
            this.Load += new System.EventHandler(this.FrmRegionModify_Load);
            this.SizeChanged += new System.EventHandler(this.FrmRegionModify_SizeChanged);
            this.tlpRoot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcRoot)).EndInit();
            this.pcRoot.ResumeLayout(false);
            this.pcRoot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeDefineAreaList.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private DevExpress.XtraEditors.PanelControl pcRoot;
        protected internal HalconDotNet.HWindowControl hWndCtrlDisplay;
        protected internal DevExpress.XtraEditors.SimpleButton sbtnEraseRegion;
        protected internal DevExpress.XtraEditors.SimpleButton sbtnDefineRegion;
        protected internal DevExpress.XtraEditors.SimpleButton sbtnExit;
        protected internal DevExpress.XtraEditors.SimpleButton sbtnLoadImage;
        private DevExpress.XtraEditors.LabelControl lblAreasList;
        protected internal DevExpress.XtraEditors.LabelControl lblPrompt;
        protected internal DevExpress.XtraEditors.ComboBoxEdit cmbeDefineAreaList;
        protected internal DevExpress.XtraEditors.LabelControl lblRemark;
    }
}