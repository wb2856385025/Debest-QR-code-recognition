namespace Pro2DBarcode.UI
{
    partial class FrmSetRoutinePara
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetRoutinePara));
            this.splitContainerControlRoot = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControlImage = new DevExpress.XtraEditors.SplitContainerControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblImageName = new DevExpress.XtraEditors.LabelControl();
            this.lblGrayValue = new DevExpress.XtraEditors.LabelControl();
            this.lblCoordinate = new DevExpress.XtraEditors.LabelControl();
            this.lblDirectoryText = new DevExpress.XtraEditors.LabelControl();
            this.lblDirectoryPrompt = new DevExpress.XtraEditors.LabelControl();
            this.hwndDisplay = new HalconDotNet.HWindowControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.chkeEnableAlgorithm = new DevExpress.XtraEditors.CheckEdit();
            this.chkeSaveImageNG = new DevExpress.XtraEditors.CheckEdit();
            this.chkeSaveImageOK = new DevExpress.XtraEditors.CheckEdit();
            this.chkeSaveImageAll = new DevExpress.XtraEditors.CheckEdit();
            this.sbtnNxtImage = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnLoadImage = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnLstImage = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnTstOffLine = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnCameraSet = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnGrabOnceAndTest = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnGrabStop = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnGrabContinue = new DevExpress.XtraEditors.SimpleButton();
            this.grpProcessParameter = new DevExpress.XtraEditors.GroupControl();
            this.speGain = new DevExpress.XtraEditors.SpinEdit();
            this.visionPara2DBarcodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblGain = new DevExpress.XtraEditors.LabelControl();
            this.sbtnSaveParameter = new DevExpress.XtraEditors.SimpleButton();
            this.lblExposureTime = new DevExpress.XtraEditors.LabelControl();
            this.speExposureTime = new DevExpress.XtraEditors.SpinEdit();
            this.grpAlgorithmParameter = new DevExpress.XtraEditors.GroupControl();
            this.speToBeFoundNum = new DevExpress.XtraEditors.SpinEdit();
            this.cmbeCodeType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbeFoundColor = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbeNotFoundColor = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblFoundColor = new DevExpress.XtraEditors.LabelControl();
            this.lblCodeType = new DevExpress.XtraEditors.LabelControl();
            this.lblNotFoundColor = new DevExpress.XtraEditors.LabelControl();
            this.lblToBeFoundNum = new DevExpress.XtraEditors.LabelControl();
            this.speTimeOut = new DevExpress.XtraEditors.SpinEdit();
            this.lblTimeOut = new DevExpress.XtraEditors.LabelControl();
            this.grpPreProcessParameter = new DevExpress.XtraEditors.GroupControl();
            this.sbtnApplyParameter = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnInspectArea = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControlRoot)).BeginInit();
            this.splitContainerControlRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControlImage)).BeginInit();
            this.splitContainerControlImage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeEnableAlgorithm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeSaveImageNG.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeSaveImageOK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeSaveImageAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpProcessParameter)).BeginInit();
            this.grpProcessParameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speGain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visionPara2DBarcodeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speExposureTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAlgorithmParameter)).BeginInit();
            this.grpAlgorithmParameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speToBeFoundNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeCodeType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeFoundColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeNotFoundColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speTimeOut.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpPreProcessParameter)).BeginInit();
            this.grpPreProcessParameter.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControlRoot
            // 
            this.splitContainerControlRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControlRoot.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControlRoot.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainerControlRoot.Name = "splitContainerControlRoot";
            this.splitContainerControlRoot.Panel1.Controls.Add(this.splitContainerControlImage);
            this.splitContainerControlRoot.Panel1.Text = "Panel1";
            this.splitContainerControlRoot.Panel2.Controls.Add(this.grpProcessParameter);
            this.splitContainerControlRoot.Panel2.Controls.Add(this.grpAlgorithmParameter);
            this.splitContainerControlRoot.Panel2.Controls.Add(this.grpPreProcessParameter);
            this.splitContainerControlRoot.Panel2.Text = "Panel2";
            this.splitContainerControlRoot.Size = new System.Drawing.Size(1504, 830);
            this.splitContainerControlRoot.SplitterPosition = 800;
            this.splitContainerControlRoot.TabIndex = 0;
            this.splitContainerControlRoot.Text = "splitContainerControl1";
            // 
            // splitContainerControlImage
            // 
            this.splitContainerControlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControlImage.Horizontal = false;
            this.splitContainerControlImage.IsSplitterFixed = true;
            this.splitContainerControlImage.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControlImage.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainerControlImage.Name = "splitContainerControlImage";
            this.splitContainerControlImage.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainerControlImage.Panel1.Controls.Add(this.hwndDisplay);
            this.splitContainerControlImage.Panel1.Text = "图像显示";
            this.splitContainerControlImage.Panel2.Controls.Add(this.separatorControl1);
            this.splitContainerControlImage.Panel2.Controls.Add(this.chkeEnableAlgorithm);
            this.splitContainerControlImage.Panel2.Controls.Add(this.chkeSaveImageNG);
            this.splitContainerControlImage.Panel2.Controls.Add(this.chkeSaveImageOK);
            this.splitContainerControlImage.Panel2.Controls.Add(this.chkeSaveImageAll);
            this.splitContainerControlImage.Panel2.Controls.Add(this.sbtnNxtImage);
            this.splitContainerControlImage.Panel2.Controls.Add(this.sbtnLoadImage);
            this.splitContainerControlImage.Panel2.Controls.Add(this.sbtnLstImage);
            this.splitContainerControlImage.Panel2.Controls.Add(this.sbtnTstOffLine);
            this.splitContainerControlImage.Panel2.Controls.Add(this.sbtnCameraSet);
            this.splitContainerControlImage.Panel2.Controls.Add(this.sbtnGrabOnceAndTest);
            this.splitContainerControlImage.Panel2.Controls.Add(this.sbtnGrabStop);
            this.splitContainerControlImage.Panel2.Controls.Add(this.sbtnGrabContinue);
            this.splitContainerControlImage.Panel2.Text = "操作按钮";
            this.splitContainerControlImage.Size = new System.Drawing.Size(800, 830);
            this.splitContainerControlImage.SplitterPosition = 566;
            this.splitContainerControlImage.TabIndex = 0;
            this.splitContainerControlImage.Text = "splitContainerControl1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Controls.Add(this.lblImageName, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblGrayValue, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCoordinate, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDirectoryText, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDirectoryPrompt, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 537);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 29);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // lblImageName
            // 
            this.lblImageName.Location = new System.Drawing.Point(644, 4);
            this.lblImageName.Margin = new System.Windows.Forms.Padding(4);
            this.lblImageName.Name = "lblImageName";
            this.lblImageName.Size = new System.Drawing.Size(72, 22);
            this.lblImageName.TabIndex = 2;
            this.lblImageName.Tag = "LBLC_IMAGENAME";
            this.lblImageName.Text = "图像名称";
            // 
            // lblGrayValue
            // 
            this.lblGrayValue.Location = new System.Drawing.Point(164, 4);
            this.lblGrayValue.Margin = new System.Windows.Forms.Padding(4);
            this.lblGrayValue.Name = "lblGrayValue";
            this.lblGrayValue.Size = new System.Drawing.Size(34, 22);
            this.lblGrayValue.TabIndex = 1;
            this.lblGrayValue.Tag = "LBLC_GRAYVALUE";
            this.lblGrayValue.Text = "RGB";
            // 
            // lblCoordinate
            // 
            this.lblCoordinate.Location = new System.Drawing.Point(4, 4);
            this.lblCoordinate.Margin = new System.Windows.Forms.Padding(4);
            this.lblCoordinate.Name = "lblCoordinate";
            this.lblCoordinate.Size = new System.Drawing.Size(36, 22);
            this.lblCoordinate.TabIndex = 0;
            this.lblCoordinate.Tag = "LBLC_COORDINATE";
            this.lblCoordinate.Text = "坐标";
            // 
            // lblDirectoryText
            // 
            this.lblDirectoryText.Location = new System.Drawing.Point(484, 4);
            this.lblDirectoryText.Margin = new System.Windows.Forms.Padding(4);
            this.lblDirectoryText.Name = "lblDirectoryText";
            this.lblDirectoryText.Size = new System.Drawing.Size(72, 22);
            this.lblDirectoryText.TabIndex = 1;
            this.lblDirectoryText.Tag = "LBLC_DIRECTORYNAME";
            this.lblDirectoryText.Text = "目录名称";
            // 
            // lblDirectoryPrompt
            // 
            this.lblDirectoryPrompt.Appearance.BackColor = System.Drawing.Color.DimGray;
            this.lblDirectoryPrompt.Appearance.Options.UseBackColor = true;
            this.lblDirectoryPrompt.Location = new System.Drawing.Point(324, 4);
            this.lblDirectoryPrompt.Margin = new System.Windows.Forms.Padding(4);
            this.lblDirectoryPrompt.Name = "lblDirectoryPrompt";
            this.lblDirectoryPrompt.Size = new System.Drawing.Size(72, 22);
            this.lblDirectoryPrompt.TabIndex = 1;
            this.lblDirectoryPrompt.Tag = "LBLC_DIRECTORYPROMPT";
            this.lblDirectoryPrompt.Text = "图像目录";
            // 
            // hwndDisplay
            // 
            this.hwndDisplay.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.hwndDisplay.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.hwndDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hwndDisplay.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hwndDisplay.Location = new System.Drawing.Point(0, 0);
            this.hwndDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.hwndDisplay.Name = "hwndDisplay";
            this.hwndDisplay.Size = new System.Drawing.Size(800, 566);
            this.hwndDisplay.TabIndex = 0;
            this.hwndDisplay.WindowSize = new System.Drawing.Size(800, 566);
            // 
            // separatorControl1
            // 
            this.separatorControl1.LineAlignment = DevExpress.XtraEditors.Alignment.Center;
            this.separatorControl1.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.separatorControl1.Location = new System.Drawing.Point(491, 37);
            this.separatorControl1.Margin = new System.Windows.Forms.Padding(4);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Padding = new System.Windows.Forms.Padding(11);
            this.separatorControl1.Size = new System.Drawing.Size(42, 121);
            this.separatorControl1.TabIndex = 0;
            // 
            // chkeEnableAlgorithm
            // 
            this.chkeEnableAlgorithm.Location = new System.Drawing.Point(14, 133);
            this.chkeEnableAlgorithm.Margin = new System.Windows.Forms.Padding(4);
            this.chkeEnableAlgorithm.Name = "chkeEnableAlgorithm";
            this.chkeEnableAlgorithm.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkeEnableAlgorithm.Properties.Appearance.Options.UseFont = true;
            this.chkeEnableAlgorithm.Properties.Caption = "测试算法";
            this.chkeEnableAlgorithm.Size = new System.Drawing.Size(188, 26);
            this.chkeEnableAlgorithm.TabIndex = 9;
            this.chkeEnableAlgorithm.Tag = "CHKE_ENABLEALGORITHM";
            // 
            // chkeSaveImageNG
            // 
            this.chkeSaveImageNG.Location = new System.Drawing.Point(14, 94);
            this.chkeSaveImageNG.Margin = new System.Windows.Forms.Padding(4);
            this.chkeSaveImageNG.Name = "chkeSaveImageNG";
            this.chkeSaveImageNG.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkeSaveImageNG.Properties.Appearance.Options.UseFont = true;
            this.chkeSaveImageNG.Properties.Caption = "保存NG";
            this.chkeSaveImageNG.Size = new System.Drawing.Size(119, 26);
            this.chkeSaveImageNG.TabIndex = 9;
            this.chkeSaveImageNG.Tag = "CHKE_SAVENG";
            // 
            // chkeSaveImageOK
            // 
            this.chkeSaveImageOK.Location = new System.Drawing.Point(14, 60);
            this.chkeSaveImageOK.Margin = new System.Windows.Forms.Padding(4);
            this.chkeSaveImageOK.Name = "chkeSaveImageOK";
            this.chkeSaveImageOK.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkeSaveImageOK.Properties.Appearance.Options.UseFont = true;
            this.chkeSaveImageOK.Properties.Caption = "保存OK";
            this.chkeSaveImageOK.Size = new System.Drawing.Size(119, 26);
            this.chkeSaveImageOK.TabIndex = 9;
            this.chkeSaveImageOK.Tag = "CHKE_SAVEOK";
            // 
            // chkeSaveImageAll
            // 
            this.chkeSaveImageAll.Location = new System.Drawing.Point(14, 24);
            this.chkeSaveImageAll.Margin = new System.Windows.Forms.Padding(4);
            this.chkeSaveImageAll.Name = "chkeSaveImageAll";
            this.chkeSaveImageAll.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkeSaveImageAll.Properties.Appearance.Options.UseFont = true;
            this.chkeSaveImageAll.Properties.Caption = "保存ALL";
            this.chkeSaveImageAll.Size = new System.Drawing.Size(119, 26);
            this.chkeSaveImageAll.TabIndex = 9;
            this.chkeSaveImageAll.Tag = "CHKE_SAVEALL";
            // 
            // sbtnNxtImage
            // 
            this.sbtnNxtImage.Location = new System.Drawing.Point(669, 111);
            this.sbtnNxtImage.Margin = new System.Windows.Forms.Padding(4);
            this.sbtnNxtImage.Name = "sbtnNxtImage";
            this.sbtnNxtImage.Size = new System.Drawing.Size(108, 49);
            this.sbtnNxtImage.TabIndex = 4;
            this.sbtnNxtImage.Tag = "SBTN_NEXT";
            this.sbtnNxtImage.Text = "下一张";
            // 
            // sbtnLoadImage
            // 
            this.sbtnLoadImage.Location = new System.Drawing.Point(534, 48);
            this.sbtnLoadImage.Margin = new System.Windows.Forms.Padding(4);
            this.sbtnLoadImage.Name = "sbtnLoadImage";
            this.sbtnLoadImage.Size = new System.Drawing.Size(108, 49);
            this.sbtnLoadImage.TabIndex = 5;
            this.sbtnLoadImage.Tag = "SBTN_LOADIMAGE";
            this.sbtnLoadImage.Text = "加载图像";
            // 
            // sbtnLstImage
            // 
            this.sbtnLstImage.Location = new System.Drawing.Point(669, 48);
            this.sbtnLstImage.Margin = new System.Windows.Forms.Padding(4);
            this.sbtnLstImage.Name = "sbtnLstImage";
            this.sbtnLstImage.Size = new System.Drawing.Size(108, 49);
            this.sbtnLstImage.TabIndex = 6;
            this.sbtnLstImage.Tag = "SBTN_LAST";
            this.sbtnLstImage.Text = "上一张";
            // 
            // sbtnTstOffLine
            // 
            this.sbtnTstOffLine.Location = new System.Drawing.Point(534, 111);
            this.sbtnTstOffLine.Margin = new System.Windows.Forms.Padding(4);
            this.sbtnTstOffLine.Name = "sbtnTstOffLine";
            this.sbtnTstOffLine.Size = new System.Drawing.Size(108, 49);
            this.sbtnTstOffLine.TabIndex = 7;
            this.sbtnTstOffLine.Tag = "SBTN_TESTOFFLINE";
            this.sbtnTstOffLine.Text = "离线测试";
            // 
            // sbtnCameraSet
            // 
            this.sbtnCameraSet.Location = new System.Drawing.Point(375, 48);
            this.sbtnCameraSet.Margin = new System.Windows.Forms.Padding(4);
            this.sbtnCameraSet.Name = "sbtnCameraSet";
            this.sbtnCameraSet.Size = new System.Drawing.Size(108, 49);
            this.sbtnCameraSet.TabIndex = 3;
            this.sbtnCameraSet.Tag = "SBTN_CAMERASET";
            this.sbtnCameraSet.Text = "相机设置";
            // 
            // sbtnGrabOnceAndTest
            // 
            this.sbtnGrabOnceAndTest.Location = new System.Drawing.Point(375, 112);
            this.sbtnGrabOnceAndTest.Margin = new System.Windows.Forms.Padding(4);
            this.sbtnGrabOnceAndTest.Name = "sbtnGrabOnceAndTest";
            this.sbtnGrabOnceAndTest.Size = new System.Drawing.Size(108, 49);
            this.sbtnGrabOnceAndTest.TabIndex = 3;
            this.sbtnGrabOnceAndTest.Tag = "SBTN_TESTONLINE";
            this.sbtnGrabOnceAndTest.Text = "在线测试";
            // 
            // sbtnGrabStop
            // 
            this.sbtnGrabStop.Location = new System.Drawing.Point(229, 111);
            this.sbtnGrabStop.Margin = new System.Windows.Forms.Padding(4);
            this.sbtnGrabStop.Name = "sbtnGrabStop";
            this.sbtnGrabStop.Size = new System.Drawing.Size(125, 49);
            this.sbtnGrabStop.TabIndex = 2;
            this.sbtnGrabStop.Tag = "SBTN_GRABSTOP";
            this.sbtnGrabStop.Text = "停止采集";
            // 
            // sbtnGrabContinue
            // 
            this.sbtnGrabContinue.Location = new System.Drawing.Point(229, 48);
            this.sbtnGrabContinue.Margin = new System.Windows.Forms.Padding(4);
            this.sbtnGrabContinue.Name = "sbtnGrabContinue";
            this.sbtnGrabContinue.Size = new System.Drawing.Size(125, 49);
            this.sbtnGrabContinue.TabIndex = 2;
            this.sbtnGrabContinue.Tag = "SBTN_GRABCONTINUE";
            this.sbtnGrabContinue.Text = "连续采集";
            // 
            // grpProcessParameter
            // 
            this.grpProcessParameter.Controls.Add(this.speGain);
            this.grpProcessParameter.Controls.Add(this.lblGain);
            this.grpProcessParameter.Controls.Add(this.sbtnSaveParameter);
            this.grpProcessParameter.Controls.Add(this.lblExposureTime);
            this.grpProcessParameter.Controls.Add(this.speExposureTime);
            this.grpProcessParameter.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpProcessParameter.Location = new System.Drawing.Point(0, 553);
            this.grpProcessParameter.Margin = new System.Windows.Forms.Padding(4);
            this.grpProcessParameter.Name = "grpProcessParameter";
            this.grpProcessParameter.Size = new System.Drawing.Size(696, 271);
            this.grpProcessParameter.TabIndex = 2;
            this.grpProcessParameter.Tag = "GRPC_PROCESSPARAMETER";
            this.grpProcessParameter.Text = "处理参数";
            // 
            // speGain
            // 
            this.speGain.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.visionPara2DBarcodeBindingSource, "CameraGain", true));
            this.speGain.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.speGain.Location = new System.Drawing.Point(206, 100);
            this.speGain.Margin = new System.Windows.Forms.Padding(4);
            this.speGain.Name = "speGain";
            this.speGain.Properties.Appearance.BackColor = System.Drawing.SystemColors.ControlDark;
            this.speGain.Properties.Appearance.ForeColor = System.Drawing.Color.Brown;
            this.speGain.Properties.Appearance.Options.UseBackColor = true;
            this.speGain.Properties.Appearance.Options.UseForeColor = true;
            this.speGain.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speGain.Properties.DisplayFormat.FormatString = "F2";
            this.speGain.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speGain.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.speGain.Properties.MaxValue = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.speGain.Size = new System.Drawing.Size(165, 28);
            this.speGain.TabIndex = 4;
            // 
            // visionPara2DBarcodeBindingSource
            // 
            this.visionPara2DBarcodeBindingSource.DataSource = typeof(Pro2DBarcode.Config.VisionPara.VisionPara2DBarcode);
            // 
            // lblGain
            // 
            this.lblGain.Location = new System.Drawing.Point(61, 119);
            this.lblGain.Margin = new System.Windows.Forms.Padding(4);
            this.lblGain.Name = "lblGain";
            this.lblGain.Size = new System.Drawing.Size(72, 22);
            this.lblGain.TabIndex = 0;
            this.lblGain.Tag = "LBLC_CAMERAGAIN";
            this.lblGain.Text = "相机增益";
            // 
            // sbtnSaveParameter
            // 
            this.sbtnSaveParameter.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbtnSaveParameter.Appearance.Options.UseFont = true;
            this.sbtnSaveParameter.Location = new System.Drawing.Point(449, 127);
            this.sbtnSaveParameter.Margin = new System.Windows.Forms.Padding(4);
            this.sbtnSaveParameter.Name = "sbtnSaveParameter";
            this.sbtnSaveParameter.Size = new System.Drawing.Size(188, 55);
            this.sbtnSaveParameter.TabIndex = 0;
            this.sbtnSaveParameter.Tag = "SBTN_SAVEPARAMETER";
            this.sbtnSaveParameter.Text = "保存参数";
            // 
            // lblExposureTime
            // 
            this.lblExposureTime.Location = new System.Drawing.Point(61, 170);
            this.lblExposureTime.Margin = new System.Windows.Forms.Padding(4);
            this.lblExposureTime.Name = "lblExposureTime";
            this.lblExposureTime.Size = new System.Drawing.Size(72, 22);
            this.lblExposureTime.TabIndex = 0;
            this.lblExposureTime.Tag = "LBLC_CAMERAEXPOSURE";
            this.lblExposureTime.Text = "相机曝光";
            // 
            // speExposureTime
            // 
            this.speExposureTime.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.visionPara2DBarcodeBindingSource, "CameraExposure", true));
            this.speExposureTime.EditValue = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.speExposureTime.Location = new System.Drawing.Point(206, 167);
            this.speExposureTime.Margin = new System.Windows.Forms.Padding(4);
            this.speExposureTime.Name = "speExposureTime";
            this.speExposureTime.Properties.Appearance.BackColor = System.Drawing.SystemColors.ControlDark;
            this.speExposureTime.Properties.Appearance.ForeColor = System.Drawing.Color.Brown;
            this.speExposureTime.Properties.Appearance.Options.UseBackColor = true;
            this.speExposureTime.Properties.Appearance.Options.UseForeColor = true;
            this.speExposureTime.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.speExposureTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speExposureTime.Properties.DisplayFormat.FormatString = "F2";
            this.speExposureTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speExposureTime.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.speExposureTime.Properties.MaxValue = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.speExposureTime.Size = new System.Drawing.Size(165, 26);
            this.speExposureTime.TabIndex = 3;
            this.speExposureTime.Tag = "SPE_CAMERAEXPOSURE";
            // 
            // grpAlgorithmParameter
            // 
            this.grpAlgorithmParameter.Controls.Add(this.speToBeFoundNum);
            this.grpAlgorithmParameter.Controls.Add(this.cmbeCodeType);
            this.grpAlgorithmParameter.Controls.Add(this.cmbeFoundColor);
            this.grpAlgorithmParameter.Controls.Add(this.cmbeNotFoundColor);
            this.grpAlgorithmParameter.Controls.Add(this.lblFoundColor);
            this.grpAlgorithmParameter.Controls.Add(this.lblCodeType);
            this.grpAlgorithmParameter.Controls.Add(this.lblNotFoundColor);
            this.grpAlgorithmParameter.Controls.Add(this.lblToBeFoundNum);
            this.grpAlgorithmParameter.Controls.Add(this.speTimeOut);
            this.grpAlgorithmParameter.Controls.Add(this.lblTimeOut);
            this.grpAlgorithmParameter.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpAlgorithmParameter.Location = new System.Drawing.Point(0, 216);
            this.grpAlgorithmParameter.Margin = new System.Windows.Forms.Padding(4);
            this.grpAlgorithmParameter.Name = "grpAlgorithmParameter";
            this.grpAlgorithmParameter.Size = new System.Drawing.Size(696, 337);
            this.grpAlgorithmParameter.TabIndex = 1;
            this.grpAlgorithmParameter.Tag = "GRPC_ALGORITHMPARAMETER";
            this.grpAlgorithmParameter.Text = "算法参数";
            // 
            // speToBeFoundNum
            // 
            this.speToBeFoundNum.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.visionPara2DBarcodeBindingSource, "ToBeFoundNumber", true));
            this.speToBeFoundNum.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.speToBeFoundNum.Location = new System.Drawing.Point(119, 88);
            this.speToBeFoundNum.Margin = new System.Windows.Forms.Padding(4);
            this.speToBeFoundNum.Name = "speToBeFoundNum";
            this.speToBeFoundNum.Properties.Appearance.BackColor = System.Drawing.Color.DarkGray;
            this.speToBeFoundNum.Properties.Appearance.ForeColor = System.Drawing.Color.Brown;
            this.speToBeFoundNum.Properties.Appearance.Options.UseBackColor = true;
            this.speToBeFoundNum.Properties.Appearance.Options.UseForeColor = true;
            this.speToBeFoundNum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speToBeFoundNum.Size = new System.Drawing.Size(144, 28);
            this.speToBeFoundNum.TabIndex = 4;
            // 
            // cmbeCodeType
            // 
            this.cmbeCodeType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.visionPara2DBarcodeBindingSource, "CodeType", true));
            this.cmbeCodeType.EditValue = "Data Matrix ECC 200";
            this.cmbeCodeType.Location = new System.Drawing.Point(388, 84);
            this.cmbeCodeType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbeCodeType.Name = "cmbeCodeType";
            this.cmbeCodeType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbeCodeType.Properties.Items.AddRange(new object[] {
            "Data Matrix ECC 200",
            "QR Code"});
            this.cmbeCodeType.Size = new System.Drawing.Size(212, 28);
            this.cmbeCodeType.TabIndex = 4;
            this.cmbeCodeType.Tag = "CMBE_CODETYPE";
            // 
            // cmbeFoundColor
            // 
            this.cmbeFoundColor.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.visionPara2DBarcodeBindingSource, "ColorStrForResultFound", true));
            this.cmbeFoundColor.EditValue = "red";
            this.cmbeFoundColor.Location = new System.Drawing.Point(388, 220);
            this.cmbeFoundColor.Margin = new System.Windows.Forms.Padding(4);
            this.cmbeFoundColor.Name = "cmbeFoundColor";
            this.cmbeFoundColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbeFoundColor.Properties.Items.AddRange(new object[] {
            "red",
            "green",
            "yellow"});
            this.cmbeFoundColor.Size = new System.Drawing.Size(212, 28);
            this.cmbeFoundColor.TabIndex = 4;
            this.cmbeFoundColor.Tag = "CMBE_FOUNDCOLOR";
            // 
            // cmbeNotFoundColor
            // 
            this.cmbeNotFoundColor.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.visionPara2DBarcodeBindingSource, "ColorStrForResultNotFound", true));
            this.cmbeNotFoundColor.EditValue = "red";
            this.cmbeNotFoundColor.Location = new System.Drawing.Point(388, 150);
            this.cmbeNotFoundColor.Margin = new System.Windows.Forms.Padding(4);
            this.cmbeNotFoundColor.Name = "cmbeNotFoundColor";
            this.cmbeNotFoundColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbeNotFoundColor.Properties.Items.AddRange(new object[] {
            "red",
            "green",
            "yellow"});
            this.cmbeNotFoundColor.Size = new System.Drawing.Size(212, 28);
            this.cmbeNotFoundColor.TabIndex = 4;
            this.cmbeNotFoundColor.Tag = "CMBE_NOTFOUNDCOLOR";
            // 
            // lblFoundColor
            // 
            this.lblFoundColor.Location = new System.Drawing.Point(299, 227);
            this.lblFoundColor.Margin = new System.Windows.Forms.Padding(4);
            this.lblFoundColor.Name = "lblFoundColor";
            this.lblFoundColor.Size = new System.Drawing.Size(60, 22);
            this.lblFoundColor.TabIndex = 2;
            this.lblFoundColor.Tag = "LBLC_FOUNDCOLOR";
            this.lblFoundColor.Text = "OK颜色";
            // 
            // lblCodeType
            // 
            this.lblCodeType.Location = new System.Drawing.Point(299, 89);
            this.lblCodeType.Margin = new System.Windows.Forms.Padding(4);
            this.lblCodeType.Name = "lblCodeType";
            this.lblCodeType.Size = new System.Drawing.Size(72, 22);
            this.lblCodeType.TabIndex = 2;
            this.lblCodeType.Tag = "LBLC_CODETYPE";
            this.lblCodeType.Text = "条码类型";
            // 
            // lblNotFoundColor
            // 
            this.lblNotFoundColor.Location = new System.Drawing.Point(299, 154);
            this.lblNotFoundColor.Margin = new System.Windows.Forms.Padding(4);
            this.lblNotFoundColor.Name = "lblNotFoundColor";
            this.lblNotFoundColor.Size = new System.Drawing.Size(60, 22);
            this.lblNotFoundColor.TabIndex = 2;
            this.lblNotFoundColor.Tag = "LBLC_NOTFOUNDCOLOR";
            this.lblNotFoundColor.Text = "NG颜色";
            // 
            // lblToBeFoundNum
            // 
            this.lblToBeFoundNum.Location = new System.Drawing.Point(31, 92);
            this.lblToBeFoundNum.Margin = new System.Windows.Forms.Padding(4);
            this.lblToBeFoundNum.Name = "lblToBeFoundNum";
            this.lblToBeFoundNum.Size = new System.Drawing.Size(72, 22);
            this.lblToBeFoundNum.TabIndex = 2;
            this.lblToBeFoundNum.Tag = "LBLC_TOBEFOUNDNUM";
            this.lblToBeFoundNum.Text = "识别数量";
            // 
            // speTimeOut
            // 
            this.speTimeOut.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.visionPara2DBarcodeBindingSource, "TimeOut", true));
            this.speTimeOut.EditValue = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.speTimeOut.Location = new System.Drawing.Point(119, 225);
            this.speTimeOut.Margin = new System.Windows.Forms.Padding(4);
            this.speTimeOut.Name = "speTimeOut";
            this.speTimeOut.Properties.Appearance.BackColor = System.Drawing.SystemColors.ControlDark;
            this.speTimeOut.Properties.Appearance.ForeColor = System.Drawing.Color.Brown;
            this.speTimeOut.Properties.Appearance.Options.UseBackColor = true;
            this.speTimeOut.Properties.Appearance.Options.UseForeColor = true;
            this.speTimeOut.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.speTimeOut.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speTimeOut.Size = new System.Drawing.Size(144, 26);
            this.speTimeOut.TabIndex = 3;
            // 
            // lblTimeOut
            // 
            this.lblTimeOut.Location = new System.Drawing.Point(30, 227);
            this.lblTimeOut.Margin = new System.Windows.Forms.Padding(4);
            this.lblTimeOut.Name = "lblTimeOut";
            this.lblTimeOut.Size = new System.Drawing.Size(72, 22);
            this.lblTimeOut.TabIndex = 2;
            this.lblTimeOut.Tag = "LBLC_TIMEOUT";
            this.lblTimeOut.Text = "识别超时";
            // 
            // grpPreProcessParameter
            // 
            this.grpPreProcessParameter.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpPreProcessParameter.Appearance.Options.UseFont = true;
            this.grpPreProcessParameter.Controls.Add(this.sbtnApplyParameter);
            this.grpPreProcessParameter.Controls.Add(this.sbtnInspectArea);
            this.grpPreProcessParameter.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpPreProcessParameter.Location = new System.Drawing.Point(0, 0);
            this.grpPreProcessParameter.Margin = new System.Windows.Forms.Padding(4);
            this.grpPreProcessParameter.Name = "grpPreProcessParameter";
            this.grpPreProcessParameter.Size = new System.Drawing.Size(696, 216);
            this.grpPreProcessParameter.TabIndex = 0;
            this.grpPreProcessParameter.Tag = "GRPC_PREPROCESS";
            this.grpPreProcessParameter.Text = "预处理";
            // 
            // sbtnApplyParameter
            // 
            this.sbtnApplyParameter.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbtnApplyParameter.Appearance.Options.UseFont = true;
            this.sbtnApplyParameter.Location = new System.Drawing.Point(414, 88);
            this.sbtnApplyParameter.Margin = new System.Windows.Forms.Padding(4);
            this.sbtnApplyParameter.Name = "sbtnApplyParameter";
            this.sbtnApplyParameter.Size = new System.Drawing.Size(188, 55);
            this.sbtnApplyParameter.TabIndex = 1;
            this.sbtnApplyParameter.Tag = "SBTN_APPLYPARAMETER";
            this.sbtnApplyParameter.Text = "应用参数";
            // 
            // sbtnInspectArea
            // 
            this.sbtnInspectArea.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbtnInspectArea.Appearance.Options.UseFont = true;
            this.sbtnInspectArea.Location = new System.Drawing.Point(72, 88);
            this.sbtnInspectArea.Margin = new System.Windows.Forms.Padding(4);
            this.sbtnInspectArea.Name = "sbtnInspectArea";
            this.sbtnInspectArea.Size = new System.Drawing.Size(188, 55);
            this.sbtnInspectArea.TabIndex = 2;
            this.sbtnInspectArea.Tag = "SBTN_INSPECTAREA";
            this.sbtnInspectArea.Text = "检测区域";
            // 
            // FrmSetRoutinePara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1504, 830);
            this.Controls.Add(this.splitContainerControlRoot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmSetRoutinePara";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "FRM_SETROUTINEPARAMETER";
            this.Text = "程式参数设置";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControlRoot)).EndInit();
            this.splitContainerControlRoot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControlImage)).EndInit();
            this.splitContainerControlImage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeEnableAlgorithm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeSaveImageNG.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeSaveImageOK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeSaveImageAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpProcessParameter)).EndInit();
            this.grpProcessParameter.ResumeLayout(false);
            this.grpProcessParameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speGain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.visionPara2DBarcodeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speExposureTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAlgorithmParameter)).EndInit();
            this.grpAlgorithmParameter.ResumeLayout(false);
            this.grpAlgorithmParameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speToBeFoundNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeCodeType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeFoundColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeNotFoundColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speTimeOut.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpPreProcessParameter)).EndInit();
            this.grpPreProcessParameter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControlRoot;
        protected internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public HalconDotNet.HWindowControl hwndDisplay;
        private DevExpress.XtraEditors.GroupControl grpProcessParameter;
        private DevExpress.XtraEditors.GroupControl grpAlgorithmParameter;
        private DevExpress.XtraEditors.GroupControl grpPreProcessParameter;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.LabelControl lblGain;
        private DevExpress.XtraEditors.LabelControl lblExposureTime;
        private DevExpress.XtraEditors.LabelControl lblTimeOut;
        private DevExpress.XtraEditors.LabelControl lblNotFoundColor;
        private DevExpress.XtraEditors.LabelControl lblCodeType;
        private DevExpress.XtraEditors.LabelControl lblToBeFoundNum;
        private DevExpress.XtraEditors.LabelControl lblFoundColor;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControlImage;
        private DevExpress.XtraEditors.LabelControl lblImageName;
        private DevExpress.XtraEditors.LabelControl lblGrayValue;
        private DevExpress.XtraEditors.LabelControl lblCoordinate;
        private DevExpress.XtraEditors.LabelControl lblDirectoryText;
        private DevExpress.XtraEditors.LabelControl lblDirectoryPrompt;
        private DevExpress.XtraEditors.CheckEdit chkeSaveImageAll;
        private DevExpress.XtraEditors.CheckEdit chkeSaveImageNG;
        private DevExpress.XtraEditors.CheckEdit chkeSaveImageOK;
        private DevExpress.XtraEditors.SimpleButton sbtnNxtImage;
        private DevExpress.XtraEditors.SimpleButton sbtnLoadImage;
        private DevExpress.XtraEditors.SimpleButton sbtnLstImage;
        private DevExpress.XtraEditors.SimpleButton sbtnTstOffLine;
        private DevExpress.XtraEditors.SimpleButton sbtnGrabOnceAndTest;
        private DevExpress.XtraEditors.SimpleButton sbtnGrabContinue;
        private DevExpress.XtraEditors.SimpleButton sbtnCameraSet;
        private DevExpress.XtraEditors.SimpleButton sbtnGrabStop;
        private DevExpress.XtraEditors.SimpleButton sbtnSaveParameter;
        private DevExpress.XtraEditors.SimpleButton sbtnApplyParameter;
        private DevExpress.XtraEditors.SimpleButton sbtnInspectArea;
        private DevExpress.XtraEditors.SpinEdit speTimeOut;
        private DevExpress.XtraEditors.ComboBoxEdit cmbeNotFoundColor;
        private DevExpress.XtraEditors.ComboBoxEdit cmbeCodeType;
        private DevExpress.XtraEditors.ComboBoxEdit cmbeFoundColor;
        private DevExpress.XtraEditors.SpinEdit speExposureTime;
        private System.Windows.Forms.BindingSource visionPara2DBarcodeBindingSource;
        protected internal DevExpress.XtraEditors.SpinEdit speGain;
        private DevExpress.XtraEditors.CheckEdit chkeEnableAlgorithm;
        internal DevExpress.XtraEditors.SpinEdit speToBeFoundNum;
    }
}