namespace ProCommon.UserDefControl
{
    partial class UsrCtrlLocationDebug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsrCtrlLocationDebug));
            this.tlstrpTool = new System.Windows.Forms.ToolStrip();
            this.tlstrpbtnAcquireOnce = new System.Windows.Forms.ToolStripButton();
            this.tlstrpbtnAcquireContinue = new System.Windows.Forms.ToolStripButton();
            this.tlstrpbtnSetCamera = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlstrpspltbtnCalibrate = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlstrpmitmCalibratePlate = new System.Windows.Forms.ToolStripMenuItem();
            this.tlstrpmitmCalibrate9Point = new System.Windows.Forms.ToolStripMenuItem();
            this.tlstrpbtnMatchModel = new System.Windows.Forms.ToolStripButton();
            this.tlstrpbtnRotateCenter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tlstrplblExposureTime = new System.Windows.Forms.ToolStripLabel();
            this.tlstrptxtbExposureTime = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tlstrplblMatchScore = new System.Windows.Forms.ToolStripLabel();
            this.tlstrptxtbMatchScore = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tlstrpbtnSaveImage = new System.Windows.Forms.ToolStripButton();
            this.hwnctrlDisplay = new HalconDotNet.HWindowControl();
            this.tlstrpTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlstrpTool
            // 
            this.tlstrpTool.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tlstrpTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlstrpbtnAcquireOnce,
            this.tlstrpbtnAcquireContinue,
            this.tlstrpbtnSetCamera,
            this.toolStripSeparator1,
            this.tlstrpspltbtnCalibrate,
            this.tlstrpbtnMatchModel,
            this.tlstrpbtnRotateCenter,
            this.toolStripSeparator2,
            this.tlstrplblExposureTime,
            this.tlstrptxtbExposureTime,
            this.toolStripSeparator3,
            this.tlstrplblMatchScore,
            this.tlstrptxtbMatchScore,
            this.toolStripSeparator4,
            this.tlstrpbtnSaveImage});
            this.tlstrpTool.Location = new System.Drawing.Point(0, 0);
            this.tlstrpTool.Name = "tlstrpTool";
            this.tlstrpTool.Size = new System.Drawing.Size(1068, 39);
            this.tlstrpTool.TabIndex = 0;
            this.tlstrpTool.Text = "toolStrip1";
            // 
            // tlstrpbtnAcquireOnce
            // 
            this.tlstrpbtnAcquireOnce.Image = ((System.Drawing.Image)(resources.GetObject("tlstrpbtnAcquireOnce.Image")));
            this.tlstrpbtnAcquireOnce.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlstrpbtnAcquireOnce.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlstrpbtnAcquireOnce.Name = "tlstrpbtnAcquireOnce";
            this.tlstrpbtnAcquireOnce.Size = new System.Drawing.Size(105, 36);
            this.tlstrpbtnAcquireOnce.Tag = "TSTRPBTN_ACQUIREONCE";
            this.tlstrpbtnAcquireOnce.Text = "单张采集";
            // 
            // tlstrpbtnAcquireContinue
            // 
            this.tlstrpbtnAcquireContinue.Image = ((System.Drawing.Image)(resources.GetObject("tlstrpbtnAcquireContinue.Image")));
            this.tlstrpbtnAcquireContinue.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlstrpbtnAcquireContinue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlstrpbtnAcquireContinue.Name = "tlstrpbtnAcquireContinue";
            this.tlstrpbtnAcquireContinue.Size = new System.Drawing.Size(105, 36);
            this.tlstrpbtnAcquireContinue.Tag = "TSTRPBTN_ACQUIRECONTINUE";
            this.tlstrpbtnAcquireContinue.Text = "连续采集";
            // 
            // tlstrpbtnSetCamera
            // 
            this.tlstrpbtnSetCamera.Image = ((System.Drawing.Image)(resources.GetObject("tlstrpbtnSetCamera.Image")));
            this.tlstrpbtnSetCamera.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlstrpbtnSetCamera.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlstrpbtnSetCamera.Name = "tlstrpbtnSetCamera";
            this.tlstrpbtnSetCamera.Size = new System.Drawing.Size(105, 36);
            this.tlstrpbtnSetCamera.Tag = "TLSTRPBTN_SETCAMERA";
            this.tlstrpbtnSetCamera.Text = "相机设置";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tlstrpspltbtnCalibrate
            // 
            this.tlstrpspltbtnCalibrate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem,
            this.tlstrpmitmCalibratePlate,
            this.tlstrpmitmCalibrate9Point});
            this.tlstrpspltbtnCalibrate.Image = ((System.Drawing.Image)(resources.GetObject("tlstrpspltbtnCalibrate.Image")));
            this.tlstrpspltbtnCalibrate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlstrpspltbtnCalibrate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlstrpspltbtnCalibrate.Name = "tlstrpspltbtnCalibrate";
            this.tlstrpspltbtnCalibrate.Size = new System.Drawing.Size(120, 36);
            this.tlstrpspltbtnCalibrate.Tag = "TLSTRPSPLTBTN_CALIBRATE";
            this.tlstrpspltbtnCalibrate.Text = "相机标定";
            // 
            // toolStripMenuItem
            // 
            this.toolStripMenuItem.Name = "toolStripMenuItem";
            this.toolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.toolStripMenuItem.Text = "______________";
            // 
            // tlstrpmitmCalibratePlate
            // 
            this.tlstrpmitmCalibratePlate.Image = ((System.Drawing.Image)(resources.GetObject("tlstrpmitmCalibratePlate.Image")));
            this.tlstrpmitmCalibratePlate.Name = "tlstrpmitmCalibratePlate";
            this.tlstrpmitmCalibratePlate.Size = new System.Drawing.Size(182, 26);
            this.tlstrpmitmCalibratePlate.Tag = "TLSTRPMITM_CALIBRATPLATE";
            this.tlstrpmitmCalibratePlate.Text = "标定板标定";
            // 
            // tlstrpmitmCalibrate9Point
            // 
            this.tlstrpmitmCalibrate9Point.Image = ((System.Drawing.Image)(resources.GetObject("tlstrpmitmCalibrate9Point.Image")));
            this.tlstrpmitmCalibrate9Point.Name = "tlstrpmitmCalibrate9Point";
            this.tlstrpmitmCalibrate9Point.Size = new System.Drawing.Size(182, 26);
            this.tlstrpmitmCalibrate9Point.Tag = "TLSTRPMITM_CALIBRATE9POINT";
            this.tlstrpmitmCalibrate9Point.Text = "九点标定";
            // 
            // tlstrpbtnMatchModel
            // 
            this.tlstrpbtnMatchModel.Image = ((System.Drawing.Image)(resources.GetObject("tlstrpbtnMatchModel.Image")));
            this.tlstrpbtnMatchModel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlstrpbtnMatchModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlstrpbtnMatchModel.Name = "tlstrpbtnMatchModel";
            this.tlstrpbtnMatchModel.Size = new System.Drawing.Size(105, 36);
            this.tlstrpbtnMatchModel.Tag = "TLSTRP_MATCHMODEL";
            this.tlstrpbtnMatchModel.Text = "模板匹配";
            // 
            // tlstrpbtnRotateCenter
            // 
            this.tlstrpbtnRotateCenter.Image = ((System.Drawing.Image)(resources.GetObject("tlstrpbtnRotateCenter.Image")));
            this.tlstrpbtnRotateCenter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlstrpbtnRotateCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlstrpbtnRotateCenter.Name = "tlstrpbtnRotateCenter";
            this.tlstrpbtnRotateCenter.Size = new System.Drawing.Size(105, 36);
            this.tlstrpbtnRotateCenter.Tag = "TLSTRPBTN_ROTATECENTER";
            this.tlstrpbtnRotateCenter.Text = "旋转中心";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // tlstrplblExposureTime
            // 
            this.tlstrplblExposureTime.Image = ((System.Drawing.Image)(resources.GetObject("tlstrplblExposureTime.Image")));
            this.tlstrplblExposureTime.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlstrplblExposureTime.Name = "tlstrplblExposureTime";
            this.tlstrplblExposureTime.Size = new System.Drawing.Size(32, 36);
            // 
            // tlstrptxtbExposureTime
            // 
            this.tlstrptxtbExposureTime.Name = "tlstrptxtbExposureTime";
            this.tlstrptxtbExposureTime.Size = new System.Drawing.Size(100, 39);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // tlstrplblMatchScore
            // 
            this.tlstrplblMatchScore.Image = ((System.Drawing.Image)(resources.GetObject("tlstrplblMatchScore.Image")));
            this.tlstrplblMatchScore.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlstrplblMatchScore.Name = "tlstrplblMatchScore";
            this.tlstrplblMatchScore.Size = new System.Drawing.Size(32, 36);
            // 
            // tlstrptxtbMatchScore
            // 
            this.tlstrptxtbMatchScore.Name = "tlstrptxtbMatchScore";
            this.tlstrptxtbMatchScore.Size = new System.Drawing.Size(100, 39);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // tlstrpbtnSaveImage
            // 
            this.tlstrpbtnSaveImage.Image = ((System.Drawing.Image)(resources.GetObject("tlstrpbtnSaveImage.Image")));
            this.tlstrpbtnSaveImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlstrpbtnSaveImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlstrpbtnSaveImage.Name = "tlstrpbtnSaveImage";
            this.tlstrpbtnSaveImage.Size = new System.Drawing.Size(105, 36);
            this.tlstrpbtnSaveImage.Tag = "TLSTRPBTN_SAVEIMAGE";
            this.tlstrpbtnSaveImage.Text = "图像保存";
            // 
            // hwnctrlDisplay
            // 
            this.hwnctrlDisplay.BackColor = System.Drawing.Color.Salmon;
            this.hwnctrlDisplay.BorderColor = System.Drawing.Color.Salmon;
            this.hwnctrlDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hwnctrlDisplay.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hwnctrlDisplay.Location = new System.Drawing.Point(0, 39);
            this.hwnctrlDisplay.Name = "hwnctrlDisplay";
            this.hwnctrlDisplay.Size = new System.Drawing.Size(1068, 593);
            this.hwnctrlDisplay.TabIndex = 1;
            this.hwnctrlDisplay.WindowSize = new System.Drawing.Size(1068, 593);
            // 
            // UsrCtrlLocationDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hwnctrlDisplay);
            this.Controls.Add(this.tlstrpTool);
            this.Name = "UsrCtrlLocationDebug";
            this.Size = new System.Drawing.Size(1068, 632);
            this.tlstrpTool.ResumeLayout(false);
            this.tlstrpTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected internal System.Windows.Forms.ToolStrip tlstrpTool;
        private System.Windows.Forms.ToolStripButton tlstrpbtnAcquireOnce;
        protected internal System.Windows.Forms.ToolStripButton tlstrpbtnAcquireContinue;
        private System.Windows.Forms.ToolStripButton tlstrpbtnSetCamera;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton tlstrpspltbtnCalibrate;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem;
        protected internal System.Windows.Forms.ToolStripMenuItem tlstrpmitmCalibratePlate;
        protected internal System.Windows.Forms.ToolStripMenuItem tlstrpmitmCalibrate9Point;
        protected internal System.Windows.Forms.ToolStripButton tlstrpbtnMatchModel;
        protected internal System.Windows.Forms.ToolStripButton tlstrpbtnRotateCenter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        protected internal System.Windows.Forms.ToolStripLabel tlstrplblExposureTime;
        protected internal System.Windows.Forms.ToolStripTextBox tlstrptxtbExposureTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        protected internal System.Windows.Forms.ToolStripLabel tlstrplblMatchScore;
        protected internal System.Windows.Forms.ToolStripTextBox tlstrptxtbMatchScore;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tlstrpbtnSaveImage;
        protected internal HalconDotNet.HWindowControl hwnctrlDisplay;
    }
}
