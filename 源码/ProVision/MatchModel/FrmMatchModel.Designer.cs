namespace ProVision.MatchModel
{
    partial class FrmMatchModel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMatchModel));
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerModel = new System.Windows.Forms.SplitContainer();
            this.grpOperation = new System.Windows.Forms.GroupBox();
            this.cmbOpLevel = new System.Windows.Forms.ComboBox();
            this.btnCreateModel = new System.Windows.Forms.Button();
            this.pRegionFunction = new System.Windows.Forms.Panel();
            this.rdbtnModelSearch = new System.Windows.Forms.RadioButton();
            this.rdbtnModelExtract = new System.Windows.Forms.RadioButton();
            this.pModelType = new System.Windows.Forms.Panel();
            this.rdbtnContourShape = new System.Windows.Forms.RadioButton();
            this.rdbtnRegionShape = new System.Windows.Forms.RadioButton();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.btnSaveModel = new System.Windows.Forms.Button();
            this.btnLoadModel = new System.Windows.Forms.Button();
            this.grpbShapeOption = new System.Windows.Forms.GroupBox();
            this.rdbtnAnnulus = new System.Windows.Forms.RadioButton();
            this.rdbtnFreeDraw = new System.Windows.Forms.RadioButton();
            this.rdbtnLine = new System.Windows.Forms.RadioButton();
            this.rdbtnCircle = new System.Windows.Forms.RadioButton();
            this.btnClearROI = new System.Windows.Forms.Button();
            this.rdbtnRectangle1 = new System.Windows.Forms.RadioButton();
            this.rdbtnRectangle2 = new System.Windows.Forms.RadioButton();
            this.rdbtnCircleArc = new System.Windows.Forms.RadioButton();
            this.grpbViewOption = new System.Windows.Forms.GroupBox();
            this.rdbtnMagnifyImage = new System.Windows.Forms.RadioButton();
            this.rdbtnZoomImage = new System.Windows.Forms.RadioButton();
            this.rdbtnMoveImage = new System.Windows.Forms.RadioButton();
            this.rdbtnNone = new System.Windows.Forms.RadioButton();
            this.grpbBrushOption = new System.Windows.Forms.GroupBox();
            this.chkbBrushShape = new System.Windows.Forms.CheckBox();
            this.chkbBrushSize = new System.Windows.Forms.CheckBox();
            this.chkbBrushOnOff = new System.Windows.Forms.CheckBox();
            this.chkbFillErase = new System.Windows.Forms.CheckBox();
            this.splitContainerView = new System.Windows.Forms.SplitContainer();
            this.lblCoordinateGrayValue = new System.Windows.Forms.Label();
            this.hWindowControl = new HalconDotNet.HWindowControl();
            this.tabControlModel = new System.Windows.Forms.TabControl();
            this.tbpCreateModelPara = new System.Windows.Forms.TabPage();
            this.pnlCreateModelPara = new System.Windows.Forms.Panel();
            this.chkbMinContrast = new System.Windows.Forms.CheckBox();
            this.chkbOption = new System.Windows.Forms.CheckBox();
            this.chkbNumLevel = new System.Windows.Forms.CheckBox();
            this.chkbAngleStep = new System.Windows.Forms.CheckBox();
            this.chkbScaleStep = new System.Windows.Forms.CheckBox();
            this.chkbContrast = new System.Windows.Forms.CheckBox();
            this.trkbMinContrast = new System.Windows.Forms.TrackBar();
            this.trkbStartAngle = new System.Windows.Forms.TrackBar();
            this.trkbScaleStep = new System.Windows.Forms.TrackBar();
            this.trkbNumLevels = new System.Windows.Forms.TrackBar();
            this.trkbMaxScale = new System.Windows.Forms.TrackBar();
            this.trkbAngleStep = new System.Windows.Forms.TrackBar();
            this.trkbMinScale = new System.Windows.Forms.TrackBar();
            this.trkbAngleExtent = new System.Windows.Forms.TrackBar();
            this.trkbContrast = new System.Windows.Forms.TrackBar();
            this.trkbDisplayLevel = new System.Windows.Forms.TrackBar();
            this.cmbOptimization = new System.Windows.Forms.ComboBox();
            this.cmbMetric = new System.Windows.Forms.ComboBox();
            this.numUpDwnMinContrast = new System.Windows.Forms.NumericUpDown();
            this.numUpDwnAngleStep = new System.Windows.Forms.NumericUpDown();
            this.numUpDwnAngleExtent = new System.Windows.Forms.NumericUpDown();
            this.numUpDwnMaxScale = new System.Windows.Forms.NumericUpDown();
            this.numUpDwnStartAngle = new System.Windows.Forms.NumericUpDown();
            this.numUpDwnNumLevels = new System.Windows.Forms.NumericUpDown();
            this.numUpDwnMinScale = new System.Windows.Forms.NumericUpDown();
            this.numUpDwnScaleStep = new System.Windows.Forms.NumericUpDown();
            this.numUpDwnContrast = new System.Windows.Forms.NumericUpDown();
            this.numUpDwnDisplayLevel = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbpFindModelPara = new System.Windows.Forms.TabPage();
            this.pnlFindModelPara = new System.Windows.Forms.Panel();
            this.cmbSubPixel = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.trkbLastLevel = new System.Windows.Forms.TrackBar();
            this.numUpDwnLastLevel = new System.Windows.Forms.NumericUpDown();
            this.trkbMaxOverlap = new System.Windows.Forms.TrackBar();
            this.numUpDwnMaxOverlap = new System.Windows.Forms.NumericUpDown();
            this.trkbGreediness = new System.Windows.Forms.TrackBar();
            this.numUpDwnGreediness = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.trkbNumToMatch = new System.Windows.Forms.TrackBar();
            this.label16 = new System.Windows.Forms.Label();
            this.numUpDwnNumToMatch = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.trkbMinScore = new System.Windows.Forms.TrackBar();
            this.label14 = new System.Windows.Forms.Label();
            this.numUpDwnMinScore = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.tbpInspectModel = new System.Windows.Forms.TabPage();
            this.splitContainerInspect = new System.Windows.Forms.SplitContainer();
            this.pnlOperation = new System.Windows.Forms.Panel();
            this.chkbAlwaysFind = new System.Windows.Forms.CheckBox();
            this.btnFindModel = new System.Windows.Forms.Button();
            this.btnDisplayImage = new System.Windows.Forms.Button();
            this.btnClearImageList = new System.Windows.Forms.Button();
            this.btnDeleteImage = new System.Windows.Forms.Button();
            this.btnLoadImageList = new System.Windows.Forms.Button();
            this.lstbTestImages = new System.Windows.Forms.ListBox();
            this.dgvMatchResult = new System.Windows.Forms.DataGridView();
            this.ColNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpOptimize = new System.Windows.Forms.TabPage();
            this.pnlOptimization = new System.Windows.Forms.Panel();
            this.btnOptimize = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lblOptElapse = new System.Windows.Forms.Label();
            this.lblLastElapse = new System.Windows.Forms.Label();
            this.lblOptRecogRate = new System.Windows.Forms.Label();
            this.lblLastRecogRate = new System.Windows.Forms.Label();
            this.lblOptGreediness = new System.Windows.Forms.Label();
            this.lblLastGreediness = new System.Windows.Forms.Label();
            this.lblOptMinScore = new System.Windows.Forms.Label();
            this.lblLastMinScore = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblOptimizationStatus = new System.Windows.Forms.Label();
            this.grpbMatchModel = new System.Windows.Forms.GroupBox();
            this.trkbRecogRate = new System.Windows.Forms.TrackBar();
            this.cmbRecogRateOption = new System.Windows.Forms.ComboBox();
            this.numUpDwnRecogRate = new System.Windows.Forms.NumericUpDown();
            this.numUpDwnSpecifiedNum = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.rdbtnMaxNum = new System.Windows.Forms.RadioButton();
            this.label19 = new System.Windows.Forms.Label();
            this.rdbtnAtLeastOne = new System.Windows.Forms.RadioButton();
            this.rdbtnSpecifiedNum = new System.Windows.Forms.RadioButton();
            this.tbpStatistic = new System.Windows.Forms.TabPage();
            this.grpbStatistic = new System.Windows.Forms.GroupBox();
            this.btnStatistic = new System.Windows.Forms.Button();
            this.numUpDwnMaxNumMatch = new System.Windows.Forms.NumericUpDown();
            this.lblInspectRangeColScale = new System.Windows.Forms.Label();
            this.lblInspectRangeRowScale = new System.Windows.Forms.Label();
            this.lblInspectMaxColScale = new System.Windows.Forms.Label();
            this.lblInspectMaxRowScale = new System.Windows.Forms.Label();
            this.lblInspectRangeAngle = new System.Windows.Forms.Label();
            this.lblInspectMinColScale = new System.Windows.Forms.Label();
            this.lblInspectMaxAngle = new System.Windows.Forms.Label();
            this.lblInspectRangeCol = new System.Windows.Forms.Label();
            this.lblInspectMinRowScale = new System.Windows.Forms.Label();
            this.lblInspectMaxCol = new System.Windows.Forms.Label();
            this.lblInspectRangeRow = new System.Windows.Forms.Label();
            this.lblInspectMinAngle = new System.Windows.Forms.Label();
            this.lblInspectMaxRow = new System.Windows.Forms.Label();
            this.lblInspectMinCol = new System.Windows.Forms.Label();
            this.lblInspectRangeElapse = new System.Windows.Forms.Label();
            this.lblInspectMinRow = new System.Windows.Forms.Label();
            this.lblInspectMaxElapse = new System.Windows.Forms.Label();
            this.lblInspectRangeScore = new System.Windows.Forms.Label();
            this.lblInspectMaxScore = new System.Windows.Forms.Label();
            this.lblInspectMinElapse = new System.Windows.Forms.Label();
            this.lblInspectMinScore = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.grpbRecogRate = new System.Windows.Forms.GroupBox();
            this.lblToMaxNum = new System.Windows.Forms.Label();
            this.lblToSpecifiedNum = new System.Windows.Forms.Label();
            this.lblMaxNum = new System.Windows.Forms.Label();
            this.lblSpecifiedNum = new System.Windows.Forms.Label();
            this.lblAtleastOne = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerModel)).BeginInit();
            this.splitContainerModel.Panel1.SuspendLayout();
            this.splitContainerModel.Panel2.SuspendLayout();
            this.splitContainerModel.SuspendLayout();
            this.grpOperation.SuspendLayout();
            this.pRegionFunction.SuspendLayout();
            this.pModelType.SuspendLayout();
            this.grpbShapeOption.SuspendLayout();
            this.grpbViewOption.SuspendLayout();
            this.grpbBrushOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerView)).BeginInit();
            this.splitContainerView.Panel1.SuspendLayout();
            this.splitContainerView.Panel2.SuspendLayout();
            this.splitContainerView.SuspendLayout();
            this.tabControlModel.SuspendLayout();
            this.tbpCreateModelPara.SuspendLayout();
            this.pnlCreateModelPara.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkbMinContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbStartAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbScaleStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbNumLevels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbMaxScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbAngleStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbMinScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbAngleExtent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbDisplayLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnMinContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnAngleStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnAngleExtent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnMaxScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnStartAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnNumLevels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnMinScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnScaleStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnDisplayLevel)).BeginInit();
            this.tbpFindModelPara.SuspendLayout();
            this.pnlFindModelPara.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkbLastLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnLastLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbMaxOverlap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnMaxOverlap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbGreediness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnGreediness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbNumToMatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnNumToMatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbMinScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnMinScore)).BeginInit();
            this.tbpInspectModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInspect)).BeginInit();
            this.splitContainerInspect.Panel1.SuspendLayout();
            this.splitContainerInspect.Panel2.SuspendLayout();
            this.splitContainerInspect.SuspendLayout();
            this.pnlOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatchResult)).BeginInit();
            this.tbpOptimize.SuspendLayout();
            this.pnlOptimization.SuspendLayout();
            this.grpbMatchModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkbRecogRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnRecogRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnSpecifiedNum)).BeginInit();
            this.tbpStatistic.SuspendLayout();
            this.grpbStatistic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnMaxNumMatch)).BeginInit();
            this.grpbRecogRate.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerModel);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerView);
            this.splitContainerMain.Size = new System.Drawing.Size(1151, 603);
            this.splitContainerMain.SplitterDistance = 91;
            this.splitContainerMain.SplitterWidth = 3;
            this.splitContainerMain.TabIndex = 0;
            // 
            // splitContainerModel
            // 
            this.splitContainerModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerModel.Location = new System.Drawing.Point(0, 0);
            this.splitContainerModel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainerModel.Name = "splitContainerModel";
            // 
            // splitContainerModel.Panel1
            // 
            this.splitContainerModel.Panel1.Controls.Add(this.grpOperation);
            this.splitContainerModel.Panel1MinSize = 50;
            // 
            // splitContainerModel.Panel2
            // 
            this.splitContainerModel.Panel2.Controls.Add(this.grpbShapeOption);
            this.splitContainerModel.Panel2.Controls.Add(this.grpbViewOption);
            this.splitContainerModel.Panel2.Controls.Add(this.grpbBrushOption);
            this.splitContainerModel.Panel2MinSize = 100;
            this.splitContainerModel.Size = new System.Drawing.Size(1151, 91);
            this.splitContainerModel.SplitterDistance = 416;
            this.splitContainerModel.SplitterWidth = 3;
            this.splitContainerModel.TabIndex = 0;
            // 
            // grpOperation
            // 
            this.grpOperation.Controls.Add(this.cmbOpLevel);
            this.grpOperation.Controls.Add(this.btnCreateModel);
            this.grpOperation.Controls.Add(this.pRegionFunction);
            this.grpOperation.Controls.Add(this.pModelType);
            this.grpOperation.Controls.Add(this.btnLoadImage);
            this.grpOperation.Controls.Add(this.btnSaveModel);
            this.grpOperation.Controls.Add(this.btnLoadModel);
            this.grpOperation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOperation.Location = new System.Drawing.Point(0, 0);
            this.grpOperation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpOperation.Name = "grpOperation";
            this.grpOperation.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpOperation.Size = new System.Drawing.Size(416, 91);
            this.grpOperation.TabIndex = 4;
            this.grpOperation.TabStop = false;
            this.grpOperation.Text = "操作选项";
            // 
            // cmbOpLevel
            // 
            this.cmbOpLevel.FormattingEnabled = true;
            this.cmbOpLevel.Items.AddRange(new object[] {
            "普通",
            "专业"});
            this.cmbOpLevel.Location = new System.Drawing.Point(184, 10);
            this.cmbOpLevel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbOpLevel.Name = "cmbOpLevel";
            this.cmbOpLevel.Size = new System.Drawing.Size(92, 20);
            this.cmbOpLevel.TabIndex = 4;
            this.cmbOpLevel.Tag = "OPERATIONLEVEL";
            this.cmbOpLevel.Text = "普通";
            this.cmbOpLevel.SelectedIndexChanged += new System.EventHandler(this.Cmb_SelectedIdxChanged);
            // 
            // btnCreateModel
            // 
            this.btnCreateModel.Location = new System.Drawing.Point(94, 20);
            this.btnCreateModel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCreateModel.Name = "btnCreateModel";
            this.btnCreateModel.Size = new System.Drawing.Size(75, 24);
            this.btnCreateModel.TabIndex = 0;
            this.btnCreateModel.Tag = "CREATEMODEL";
            this.btnCreateModel.Text = "创建模板";
            this.btnCreateModel.UseVisualStyleBackColor = true;
            this.btnCreateModel.Click += new System.EventHandler(this.Btn_Click);
            // 
            // pRegionFunction
            // 
            this.pRegionFunction.Controls.Add(this.rdbtnModelSearch);
            this.pRegionFunction.Controls.Add(this.rdbtnModelExtract);
            this.pRegionFunction.Location = new System.Drawing.Point(286, 30);
            this.pRegionFunction.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pRegionFunction.Name = "pRegionFunction";
            this.pRegionFunction.Size = new System.Drawing.Size(122, 57);
            this.pRegionFunction.TabIndex = 4;
            // 
            // rdbtnModelSearch
            // 
            this.rdbtnModelSearch.AutoSize = true;
            this.rdbtnModelSearch.Location = new System.Drawing.Point(2, 34);
            this.rdbtnModelSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnModelSearch.Name = "rdbtnModelSearch";
            this.rdbtnModelSearch.Size = new System.Drawing.Size(119, 16);
            this.rdbtnModelSearch.TabIndex = 1;
            this.rdbtnModelSearch.Tag = "MODELSEARCHRESGION";
            this.rdbtnModelSearch.Text = "绘制模板搜索区域";
            this.rdbtnModelSearch.UseVisualStyleBackColor = true;
            this.rdbtnModelSearch.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // rdbtnModelExtract
            // 
            this.rdbtnModelExtract.AutoSize = true;
            this.rdbtnModelExtract.Checked = true;
            this.rdbtnModelExtract.Location = new System.Drawing.Point(2, 7);
            this.rdbtnModelExtract.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnModelExtract.Name = "rdbtnModelExtract";
            this.rdbtnModelExtract.Size = new System.Drawing.Size(119, 16);
            this.rdbtnModelExtract.TabIndex = 1;
            this.rdbtnModelExtract.TabStop = true;
            this.rdbtnModelExtract.Tag = "MODELEXTRACTREGION";
            this.rdbtnModelExtract.Text = "绘制模板提取区域";
            this.rdbtnModelExtract.UseVisualStyleBackColor = true;
            this.rdbtnModelExtract.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // pModelType
            // 
            this.pModelType.Controls.Add(this.rdbtnContourShape);
            this.pModelType.Controls.Add(this.rdbtnRegionShape);
            this.pModelType.Location = new System.Drawing.Point(182, 30);
            this.pModelType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pModelType.Name = "pModelType";
            this.pModelType.Size = new System.Drawing.Size(99, 55);
            this.pModelType.TabIndex = 4;
            // 
            // rdbtnContourShape
            // 
            this.rdbtnContourShape.AutoSize = true;
            this.rdbtnContourShape.Location = new System.Drawing.Point(2, 34);
            this.rdbtnContourShape.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnContourShape.Name = "rdbtnContourShape";
            this.rdbtnContourShape.Size = new System.Drawing.Size(95, 16);
            this.rdbtnContourShape.TabIndex = 0;
            this.rdbtnContourShape.Tag = "CONTOURMODEL";
            this.rdbtnContourShape.Text = "轮廓形状模板";
            this.rdbtnContourShape.UseVisualStyleBackColor = true;
            this.rdbtnContourShape.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // rdbtnRegionShape
            // 
            this.rdbtnRegionShape.AutoSize = true;
            this.rdbtnRegionShape.Checked = true;
            this.rdbtnRegionShape.Location = new System.Drawing.Point(2, 7);
            this.rdbtnRegionShape.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnRegionShape.Name = "rdbtnRegionShape";
            this.rdbtnRegionShape.Size = new System.Drawing.Size(95, 16);
            this.rdbtnRegionShape.TabIndex = 0;
            this.rdbtnRegionShape.TabStop = true;
            this.rdbtnRegionShape.Tag = "REGIONMODEL";
            this.rdbtnRegionShape.Text = "区域形状模板";
            this.rdbtnRegionShape.UseVisualStyleBackColor = true;
            this.rdbtnRegionShape.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(10, 20);
            this.btnLoadImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(75, 24);
            this.btnLoadImage.TabIndex = 0;
            this.btnLoadImage.Tag = "LOADTRAINIMAGE";
            this.btnLoadImage.Text = "加载图像";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.Btn_Click);
            // 
            // btnSaveModel
            // 
            this.btnSaveModel.Location = new System.Drawing.Point(94, 61);
            this.btnSaveModel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSaveModel.Name = "btnSaveModel";
            this.btnSaveModel.Size = new System.Drawing.Size(75, 24);
            this.btnSaveModel.TabIndex = 0;
            this.btnSaveModel.Tag = "SAVEMODEL";
            this.btnSaveModel.Text = "保存模板";
            this.btnSaveModel.UseVisualStyleBackColor = true;
            this.btnSaveModel.Click += new System.EventHandler(this.Btn_Click);
            // 
            // btnLoadModel
            // 
            this.btnLoadModel.Location = new System.Drawing.Point(10, 61);
            this.btnLoadModel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLoadModel.Name = "btnLoadModel";
            this.btnLoadModel.Size = new System.Drawing.Size(75, 24);
            this.btnLoadModel.TabIndex = 0;
            this.btnLoadModel.Tag = "LOADMODEL";
            this.btnLoadModel.Text = "加载模板";
            this.btnLoadModel.UseVisualStyleBackColor = true;
            this.btnLoadModel.Click += new System.EventHandler(this.Btn_Click);
            // 
            // grpbShapeOption
            // 
            this.grpbShapeOption.Controls.Add(this.rdbtnAnnulus);
            this.grpbShapeOption.Controls.Add(this.rdbtnFreeDraw);
            this.grpbShapeOption.Controls.Add(this.rdbtnLine);
            this.grpbShapeOption.Controls.Add(this.rdbtnCircle);
            this.grpbShapeOption.Controls.Add(this.btnClearROI);
            this.grpbShapeOption.Controls.Add(this.rdbtnRectangle1);
            this.grpbShapeOption.Controls.Add(this.rdbtnRectangle2);
            this.grpbShapeOption.Controls.Add(this.rdbtnCircleArc);
            this.grpbShapeOption.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpbShapeOption.Location = new System.Drawing.Point(0, 0);
            this.grpbShapeOption.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpbShapeOption.Name = "grpbShapeOption";
            this.grpbShapeOption.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpbShapeOption.Size = new System.Drawing.Size(250, 91);
            this.grpbShapeOption.TabIndex = 0;
            this.grpbShapeOption.TabStop = false;
            this.grpbShapeOption.Text = "形状选项";
            // 
            // rdbtnAnnulus
            // 
            this.rdbtnAnnulus.AutoSize = true;
            this.rdbtnAnnulus.Location = new System.Drawing.Point(92, 60);
            this.rdbtnAnnulus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnAnnulus.Name = "rdbtnAnnulus";
            this.rdbtnAnnulus.Size = new System.Drawing.Size(71, 16);
            this.rdbtnAnnulus.TabIndex = 0;
            this.rdbtnAnnulus.Tag = "ANNULUS";
            this.rdbtnAnnulus.Text = "闭合圆环";
            this.rdbtnAnnulus.UseVisualStyleBackColor = true;
            this.rdbtnAnnulus.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // rdbtnFreeDraw
            // 
            this.rdbtnFreeDraw.AutoSize = true;
            this.rdbtnFreeDraw.Location = new System.Drawing.Point(169, 20);
            this.rdbtnFreeDraw.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnFreeDraw.Name = "rdbtnFreeDraw";
            this.rdbtnFreeDraw.Size = new System.Drawing.Size(71, 16);
            this.rdbtnFreeDraw.TabIndex = 0;
            this.rdbtnFreeDraw.Tag = "FREEDRAW";
            this.rdbtnFreeDraw.Text = "自由绘制";
            this.rdbtnFreeDraw.UseVisualStyleBackColor = true;
            this.rdbtnFreeDraw.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // rdbtnLine
            // 
            this.rdbtnLine.AutoSize = true;
            this.rdbtnLine.Enabled = false;
            this.rdbtnLine.Location = new System.Drawing.Point(12, 20);
            this.rdbtnLine.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnLine.Name = "rdbtnLine";
            this.rdbtnLine.Size = new System.Drawing.Size(71, 16);
            this.rdbtnLine.TabIndex = 0;
            this.rdbtnLine.Tag = "LINE";
            this.rdbtnLine.Text = "矢量线段";
            this.rdbtnLine.UseVisualStyleBackColor = true;
            this.rdbtnLine.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // rdbtnCircle
            // 
            this.rdbtnCircle.AutoSize = true;
            this.rdbtnCircle.Location = new System.Drawing.Point(92, 40);
            this.rdbtnCircle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnCircle.Name = "rdbtnCircle";
            this.rdbtnCircle.Size = new System.Drawing.Size(71, 16);
            this.rdbtnCircle.TabIndex = 0;
            this.rdbtnCircle.Tag = "CIRCLE";
            this.rdbtnCircle.Text = "闭合圆形";
            this.rdbtnCircle.UseVisualStyleBackColor = true;
            this.rdbtnCircle.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // btnClearROI
            // 
            this.btnClearROI.Location = new System.Drawing.Point(170, 50);
            this.btnClearROI.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClearROI.Name = "btnClearROI";
            this.btnClearROI.Size = new System.Drawing.Size(56, 20);
            this.btnClearROI.TabIndex = 0;
            this.btnClearROI.Tag = "CLEARROI";
            this.btnClearROI.Text = "清空ROI";
            this.btnClearROI.UseVisualStyleBackColor = true;
            this.btnClearROI.Click += new System.EventHandler(this.Btn_Click);
            // 
            // rdbtnRectangle1
            // 
            this.rdbtnRectangle1.AutoSize = true;
            this.rdbtnRectangle1.Checked = true;
            this.rdbtnRectangle1.Location = new System.Drawing.Point(12, 40);
            this.rdbtnRectangle1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnRectangle1.Name = "rdbtnRectangle1";
            this.rdbtnRectangle1.Size = new System.Drawing.Size(71, 16);
            this.rdbtnRectangle1.TabIndex = 0;
            this.rdbtnRectangle1.TabStop = true;
            this.rdbtnRectangle1.Tag = "RECTANGLE1";
            this.rdbtnRectangle1.Text = "齐轴矩形";
            this.rdbtnRectangle1.UseVisualStyleBackColor = true;
            this.rdbtnRectangle1.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // rdbtnRectangle2
            // 
            this.rdbtnRectangle2.AutoSize = true;
            this.rdbtnRectangle2.Location = new System.Drawing.Point(12, 60);
            this.rdbtnRectangle2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnRectangle2.Name = "rdbtnRectangle2";
            this.rdbtnRectangle2.Size = new System.Drawing.Size(71, 16);
            this.rdbtnRectangle2.TabIndex = 0;
            this.rdbtnRectangle2.Tag = "RECTANGLE2";
            this.rdbtnRectangle2.Text = "仿射矩形";
            this.rdbtnRectangle2.UseVisualStyleBackColor = true;
            this.rdbtnRectangle2.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // rdbtnCircleArc
            // 
            this.rdbtnCircleArc.AutoSize = true;
            this.rdbtnCircleArc.Enabled = false;
            this.rdbtnCircleArc.Location = new System.Drawing.Point(92, 20);
            this.rdbtnCircleArc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnCircleArc.Name = "rdbtnCircleArc";
            this.rdbtnCircleArc.Size = new System.Drawing.Size(71, 16);
            this.rdbtnCircleArc.TabIndex = 0;
            this.rdbtnCircleArc.Tag = "CIRCULARARC";
            this.rdbtnCircleArc.Text = "有向圆弧";
            this.rdbtnCircleArc.UseVisualStyleBackColor = true;
            this.rdbtnCircleArc.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // grpbViewOption
            // 
            this.grpbViewOption.Controls.Add(this.rdbtnMagnifyImage);
            this.grpbViewOption.Controls.Add(this.rdbtnZoomImage);
            this.grpbViewOption.Controls.Add(this.rdbtnMoveImage);
            this.grpbViewOption.Controls.Add(this.rdbtnNone);
            this.grpbViewOption.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpbViewOption.Location = new System.Drawing.Point(356, 0);
            this.grpbViewOption.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpbViewOption.Name = "grpbViewOption";
            this.grpbViewOption.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpbViewOption.Size = new System.Drawing.Size(180, 91);
            this.grpbViewOption.TabIndex = 3;
            this.grpbViewOption.TabStop = false;
            this.grpbViewOption.Text = "视图操作";
            // 
            // rdbtnMagnifyImage
            // 
            this.rdbtnMagnifyImage.AutoSize = true;
            this.rdbtnMagnifyImage.Location = new System.Drawing.Point(91, 50);
            this.rdbtnMagnifyImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnMagnifyImage.Name = "rdbtnMagnifyImage";
            this.rdbtnMagnifyImage.Size = new System.Drawing.Size(71, 16);
            this.rdbtnMagnifyImage.TabIndex = 0;
            this.rdbtnMagnifyImage.Tag = "MAGNIFYIMAGE";
            this.rdbtnMagnifyImage.Text = "局部放大";
            this.rdbtnMagnifyImage.UseVisualStyleBackColor = true;
            this.rdbtnMagnifyImage.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // rdbtnZoomImage
            // 
            this.rdbtnZoomImage.AutoSize = true;
            this.rdbtnZoomImage.Location = new System.Drawing.Point(91, 19);
            this.rdbtnZoomImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnZoomImage.Name = "rdbtnZoomImage";
            this.rdbtnZoomImage.Size = new System.Drawing.Size(71, 16);
            this.rdbtnZoomImage.TabIndex = 0;
            this.rdbtnZoomImage.Tag = "ZOOMIMAGE";
            this.rdbtnZoomImage.Text = "缩放图像";
            this.rdbtnZoomImage.UseVisualStyleBackColor = true;
            this.rdbtnZoomImage.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // rdbtnMoveImage
            // 
            this.rdbtnMoveImage.AutoSize = true;
            this.rdbtnMoveImage.Location = new System.Drawing.Point(12, 50);
            this.rdbtnMoveImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnMoveImage.Name = "rdbtnMoveImage";
            this.rdbtnMoveImage.Size = new System.Drawing.Size(71, 16);
            this.rdbtnMoveImage.TabIndex = 0;
            this.rdbtnMoveImage.Tag = "MOVEIMAGE";
            this.rdbtnMoveImage.Text = "移动图像";
            this.rdbtnMoveImage.UseVisualStyleBackColor = true;
            this.rdbtnMoveImage.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // rdbtnNone
            // 
            this.rdbtnNone.AutoSize = true;
            this.rdbtnNone.Checked = true;
            this.rdbtnNone.Location = new System.Drawing.Point(12, 19);
            this.rdbtnNone.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnNone.Name = "rdbtnNone";
            this.rdbtnNone.Size = new System.Drawing.Size(59, 16);
            this.rdbtnNone.TabIndex = 0;
            this.rdbtnNone.TabStop = true;
            this.rdbtnNone.Tag = "NONE";
            this.rdbtnNone.Text = "无操作";
            this.rdbtnNone.UseVisualStyleBackColor = true;
            this.rdbtnNone.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // grpbBrushOption
            // 
            this.grpbBrushOption.Controls.Add(this.chkbBrushShape);
            this.grpbBrushOption.Controls.Add(this.chkbBrushSize);
            this.grpbBrushOption.Controls.Add(this.chkbBrushOnOff);
            this.grpbBrushOption.Controls.Add(this.chkbFillErase);
            this.grpbBrushOption.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpbBrushOption.Location = new System.Drawing.Point(536, 0);
            this.grpbBrushOption.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpbBrushOption.Name = "grpbBrushOption";
            this.grpbBrushOption.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpbBrushOption.Size = new System.Drawing.Size(196, 91);
            this.grpbBrushOption.TabIndex = 1;
            this.grpbBrushOption.TabStop = false;
            this.grpbBrushOption.Text = "笔刷选项";
            // 
            // chkbBrushShape
            // 
            this.chkbBrushShape.AutoSize = true;
            this.chkbBrushShape.Location = new System.Drawing.Point(28, 51);
            this.chkbBrushShape.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkbBrushShape.Name = "chkbBrushShape";
            this.chkbBrushShape.Size = new System.Drawing.Size(72, 16);
            this.chkbBrushShape.TabIndex = 5;
            this.chkbBrushShape.Tag = "BRUSHSHAPE";
            this.chkbBrushShape.Text = "圆形笔刷";
            this.chkbBrushShape.UseVisualStyleBackColor = true;
            this.chkbBrushShape.CheckedChanged += new System.EventHandler(this.Chkb_CheckedChanged);
            // 
            // chkbBrushSize
            // 
            this.chkbBrushSize.AutoSize = true;
            this.chkbBrushSize.Location = new System.Drawing.Point(28, 26);
            this.chkbBrushSize.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkbBrushSize.Name = "chkbBrushSize";
            this.chkbBrushSize.Size = new System.Drawing.Size(72, 16);
            this.chkbBrushSize.TabIndex = 5;
            this.chkbBrushSize.Tag = "BRUSHSIZE";
            this.chkbBrushSize.Text = "小号笔刷";
            this.chkbBrushSize.UseVisualStyleBackColor = true;
            this.chkbBrushSize.CheckedChanged += new System.EventHandler(this.Chkb_CheckedChanged);
            // 
            // chkbBrushOnOff
            // 
            this.chkbBrushOnOff.AutoSize = true;
            this.chkbBrushOnOff.Location = new System.Drawing.Point(110, 26);
            this.chkbBrushOnOff.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkbBrushOnOff.Name = "chkbBrushOnOff";
            this.chkbBrushOnOff.Size = new System.Drawing.Size(72, 16);
            this.chkbBrushOnOff.TabIndex = 1;
            this.chkbBrushOnOff.Tag = "BRUSHONOFF";
            this.chkbBrushOnOff.Text = "笔刷禁用";
            this.chkbBrushOnOff.UseVisualStyleBackColor = true;
            this.chkbBrushOnOff.CheckedChanged += new System.EventHandler(this.Chkb_CheckedChanged);
            // 
            // chkbFillErase
            // 
            this.chkbFillErase.AutoSize = true;
            this.chkbFillErase.Location = new System.Drawing.Point(110, 51);
            this.chkbFillErase.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkbFillErase.Name = "chkbFillErase";
            this.chkbFillErase.Size = new System.Drawing.Size(72, 16);
            this.chkbFillErase.TabIndex = 1;
            this.chkbFillErase.Tag = "FILLERASE";
            this.chkbFillErase.Text = "填充模式";
            this.chkbFillErase.UseVisualStyleBackColor = true;
            this.chkbFillErase.CheckedChanged += new System.EventHandler(this.Chkb_CheckedChanged);
            // 
            // splitContainerView
            // 
            this.splitContainerView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerView.Location = new System.Drawing.Point(0, 0);
            this.splitContainerView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainerView.Name = "splitContainerView";
            // 
            // splitContainerView.Panel1
            // 
            this.splitContainerView.Panel1.Controls.Add(this.lblCoordinateGrayValue);
            this.splitContainerView.Panel1.Controls.Add(this.hWindowControl);
            // 
            // splitContainerView.Panel2
            // 
            this.splitContainerView.Panel2.Controls.Add(this.tabControlModel);
            this.splitContainerView.Size = new System.Drawing.Size(1151, 509);
            this.splitContainerView.SplitterDistance = 464;
            this.splitContainerView.SplitterWidth = 3;
            this.splitContainerView.TabIndex = 0;
            // 
            // lblCoordinateGrayValue
            // 
            this.lblCoordinateGrayValue.AutoSize = true;
            this.lblCoordinateGrayValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCoordinateGrayValue.Location = new System.Drawing.Point(0, 486);
            this.lblCoordinateGrayValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCoordinateGrayValue.Name = "lblCoordinateGrayValue";
            this.lblCoordinateGrayValue.Size = new System.Drawing.Size(101, 12);
            this.lblCoordinateGrayValue.TabIndex = 1;
            this.lblCoordinateGrayValue.Text = "坐标与像素灰度值";
            // 
            // hWindowControl
            // 
            this.hWindowControl.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.hWindowControl.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.hWindowControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.hWindowControl.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl.Location = new System.Drawing.Point(0, 0);
            this.hWindowControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.hWindowControl.Name = "hWindowControl";
            this.hWindowControl.Size = new System.Drawing.Size(464, 486);
            this.hWindowControl.TabIndex = 0;
            this.hWindowControl.WindowSize = new System.Drawing.Size(464, 486);
            // 
            // tabControlModel
            // 
            this.tabControlModel.Controls.Add(this.tbpCreateModelPara);
            this.tabControlModel.Controls.Add(this.tbpFindModelPara);
            this.tabControlModel.Controls.Add(this.tbpInspectModel);
            this.tabControlModel.Controls.Add(this.tbpOptimize);
            this.tabControlModel.Controls.Add(this.tbpStatistic);
            this.tabControlModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlModel.Location = new System.Drawing.Point(0, 0);
            this.tabControlModel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControlModel.Name = "tabControlModel";
            this.tabControlModel.SelectedIndex = 0;
            this.tabControlModel.Size = new System.Drawing.Size(684, 509);
            this.tabControlModel.TabIndex = 0;
            // 
            // tbpCreateModelPara
            // 
            this.tbpCreateModelPara.Controls.Add(this.pnlCreateModelPara);
            this.tbpCreateModelPara.Location = new System.Drawing.Point(4, 22);
            this.tbpCreateModelPara.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbpCreateModelPara.Name = "tbpCreateModelPara";
            this.tbpCreateModelPara.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbpCreateModelPara.Size = new System.Drawing.Size(676, 483);
            this.tbpCreateModelPara.TabIndex = 0;
            this.tbpCreateModelPara.Text = "创建模型参数";
            this.tbpCreateModelPara.UseVisualStyleBackColor = true;
            // 
            // pnlCreateModelPara
            // 
            this.pnlCreateModelPara.Controls.Add(this.chkbMinContrast);
            this.pnlCreateModelPara.Controls.Add(this.chkbOption);
            this.pnlCreateModelPara.Controls.Add(this.chkbNumLevel);
            this.pnlCreateModelPara.Controls.Add(this.chkbAngleStep);
            this.pnlCreateModelPara.Controls.Add(this.chkbScaleStep);
            this.pnlCreateModelPara.Controls.Add(this.chkbContrast);
            this.pnlCreateModelPara.Controls.Add(this.trkbMinContrast);
            this.pnlCreateModelPara.Controls.Add(this.trkbStartAngle);
            this.pnlCreateModelPara.Controls.Add(this.trkbScaleStep);
            this.pnlCreateModelPara.Controls.Add(this.trkbNumLevels);
            this.pnlCreateModelPara.Controls.Add(this.trkbMaxScale);
            this.pnlCreateModelPara.Controls.Add(this.trkbAngleStep);
            this.pnlCreateModelPara.Controls.Add(this.trkbMinScale);
            this.pnlCreateModelPara.Controls.Add(this.trkbAngleExtent);
            this.pnlCreateModelPara.Controls.Add(this.trkbContrast);
            this.pnlCreateModelPara.Controls.Add(this.trkbDisplayLevel);
            this.pnlCreateModelPara.Controls.Add(this.cmbOptimization);
            this.pnlCreateModelPara.Controls.Add(this.cmbMetric);
            this.pnlCreateModelPara.Controls.Add(this.numUpDwnMinContrast);
            this.pnlCreateModelPara.Controls.Add(this.numUpDwnAngleStep);
            this.pnlCreateModelPara.Controls.Add(this.numUpDwnAngleExtent);
            this.pnlCreateModelPara.Controls.Add(this.numUpDwnMaxScale);
            this.pnlCreateModelPara.Controls.Add(this.numUpDwnStartAngle);
            this.pnlCreateModelPara.Controls.Add(this.numUpDwnNumLevels);
            this.pnlCreateModelPara.Controls.Add(this.numUpDwnMinScale);
            this.pnlCreateModelPara.Controls.Add(this.numUpDwnScaleStep);
            this.pnlCreateModelPara.Controls.Add(this.numUpDwnContrast);
            this.pnlCreateModelPara.Controls.Add(this.numUpDwnDisplayLevel);
            this.pnlCreateModelPara.Controls.Add(this.label12);
            this.pnlCreateModelPara.Controls.Add(this.label11);
            this.pnlCreateModelPara.Controls.Add(this.label8);
            this.pnlCreateModelPara.Controls.Add(this.label7);
            this.pnlCreateModelPara.Controls.Add(this.label10);
            this.pnlCreateModelPara.Controls.Add(this.label4);
            this.pnlCreateModelPara.Controls.Add(this.label6);
            this.pnlCreateModelPara.Controls.Add(this.label9);
            this.pnlCreateModelPara.Controls.Add(this.label3);
            this.pnlCreateModelPara.Controls.Add(this.label5);
            this.pnlCreateModelPara.Controls.Add(this.label2);
            this.pnlCreateModelPara.Controls.Add(this.label1);
            this.pnlCreateModelPara.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCreateModelPara.Location = new System.Drawing.Point(2, 2);
            this.pnlCreateModelPara.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlCreateModelPara.Name = "pnlCreateModelPara";
            this.pnlCreateModelPara.Size = new System.Drawing.Size(672, 479);
            this.pnlCreateModelPara.TabIndex = 0;
            // 
            // chkbMinContrast
            // 
            this.chkbMinContrast.AutoSize = true;
            this.chkbMinContrast.Location = new System.Drawing.Point(426, 439);
            this.chkbMinContrast.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkbMinContrast.Name = "chkbMinContrast";
            this.chkbMinContrast.Size = new System.Drawing.Size(72, 16);
            this.chkbMinContrast.TabIndex = 1;
            this.chkbMinContrast.Tag = "MINCONTRAST";
            this.chkbMinContrast.Text = "手动调整";
            this.chkbMinContrast.UseVisualStyleBackColor = true;
            this.chkbMinContrast.CheckedChanged += new System.EventHandler(this.Chkb_CheckedChanged);
            // 
            // chkbOption
            // 
            this.chkbOption.AutoSize = true;
            this.chkbOption.Location = new System.Drawing.Point(426, 400);
            this.chkbOption.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkbOption.Name = "chkbOption";
            this.chkbOption.Size = new System.Drawing.Size(72, 16);
            this.chkbOption.TabIndex = 1;
            this.chkbOption.Tag = "OPTIMIZATION";
            this.chkbOption.Text = "手动调整";
            this.chkbOption.UseVisualStyleBackColor = true;
            this.chkbOption.CheckedChanged += new System.EventHandler(this.Chkb_CheckedChanged);
            // 
            // chkbNumLevel
            // 
            this.chkbNumLevel.AutoSize = true;
            this.chkbNumLevel.Location = new System.Drawing.Point(426, 318);
            this.chkbNumLevel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkbNumLevel.Name = "chkbNumLevel";
            this.chkbNumLevel.Size = new System.Drawing.Size(72, 16);
            this.chkbNumLevel.TabIndex = 1;
            this.chkbNumLevel.Tag = "NUMLEVELS";
            this.chkbNumLevel.Text = "手动调整";
            this.chkbNumLevel.UseVisualStyleBackColor = true;
            this.chkbNumLevel.CheckedChanged += new System.EventHandler(this.Chkb_CheckedChanged);
            // 
            // chkbAngleStep
            // 
            this.chkbAngleStep.AutoSize = true;
            this.chkbAngleStep.Location = new System.Drawing.Point(426, 280);
            this.chkbAngleStep.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkbAngleStep.Name = "chkbAngleStep";
            this.chkbAngleStep.Size = new System.Drawing.Size(72, 16);
            this.chkbAngleStep.TabIndex = 1;
            this.chkbAngleStep.Tag = "ANGLESTEP";
            this.chkbAngleStep.Text = "手动调整";
            this.chkbAngleStep.UseVisualStyleBackColor = true;
            this.chkbAngleStep.CheckedChanged += new System.EventHandler(this.Chkb_CheckedChanged);
            // 
            // chkbScaleStep
            // 
            this.chkbScaleStep.AutoSize = true;
            this.chkbScaleStep.Location = new System.Drawing.Point(426, 168);
            this.chkbScaleStep.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkbScaleStep.Name = "chkbScaleStep";
            this.chkbScaleStep.Size = new System.Drawing.Size(72, 16);
            this.chkbScaleStep.TabIndex = 1;
            this.chkbScaleStep.Tag = "SCALESTEP";
            this.chkbScaleStep.Text = "手动调整";
            this.chkbScaleStep.UseVisualStyleBackColor = true;
            this.chkbScaleStep.CheckedChanged += new System.EventHandler(this.Chkb_CheckedChanged);
            // 
            // chkbContrast
            // 
            this.chkbContrast.AutoSize = true;
            this.chkbContrast.Location = new System.Drawing.Point(426, 57);
            this.chkbContrast.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkbContrast.Name = "chkbContrast";
            this.chkbContrast.Size = new System.Drawing.Size(72, 16);
            this.chkbContrast.TabIndex = 1;
            this.chkbContrast.Tag = "CONTRAST";
            this.chkbContrast.Text = "手动调整";
            this.chkbContrast.UseVisualStyleBackColor = true;
            this.chkbContrast.CheckedChanged += new System.EventHandler(this.Chkb_CheckedChanged);
            // 
            // trkbMinContrast
            // 
            this.trkbMinContrast.AutoSize = false;
            this.trkbMinContrast.Location = new System.Drawing.Point(258, 434);
            this.trkbMinContrast.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkbMinContrast.Maximum = 30;
            this.trkbMinContrast.Name = "trkbMinContrast";
            this.trkbMinContrast.Size = new System.Drawing.Size(146, 25);
            this.trkbMinContrast.TabIndex = 3;
            this.trkbMinContrast.Tag = "MINCONTRAST";
            this.trkbMinContrast.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkbMinContrast.Value = 10;
            this.trkbMinContrast.Scroll += new System.EventHandler(this.Trkb_Scroll);
            // 
            // trkbStartAngle
            // 
            this.trkbStartAngle.AutoSize = false;
            this.trkbStartAngle.Location = new System.Drawing.Point(258, 199);
            this.trkbStartAngle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkbStartAngle.Maximum = 180;
            this.trkbStartAngle.Minimum = -180;
            this.trkbStartAngle.Name = "trkbStartAngle";
            this.trkbStartAngle.Size = new System.Drawing.Size(146, 25);
            this.trkbStartAngle.TabIndex = 3;
            this.trkbStartAngle.Tag = "STARTANGLE";
            this.trkbStartAngle.TickFrequency = 20;
            this.trkbStartAngle.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkbStartAngle.Scroll += new System.EventHandler(this.Trkb_Scroll);
            // 
            // trkbScaleStep
            // 
            this.trkbScaleStep.AutoSize = false;
            this.trkbScaleStep.Location = new System.Drawing.Point(258, 163);
            this.trkbScaleStep.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkbScaleStep.Maximum = 190;
            this.trkbScaleStep.Minimum = 1;
            this.trkbScaleStep.Name = "trkbScaleStep";
            this.trkbScaleStep.Size = new System.Drawing.Size(146, 25);
            this.trkbScaleStep.TabIndex = 3;
            this.trkbScaleStep.Tag = "SCALESTEP";
            this.trkbScaleStep.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkbScaleStep.Value = 10;
            this.trkbScaleStep.Scroll += new System.EventHandler(this.Trkb_Scroll);
            // 
            // trkbNumLevels
            // 
            this.trkbNumLevels.AutoSize = false;
            this.trkbNumLevels.Location = new System.Drawing.Point(258, 314);
            this.trkbNumLevels.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkbNumLevels.Maximum = 6;
            this.trkbNumLevels.Minimum = 1;
            this.trkbNumLevels.Name = "trkbNumLevels";
            this.trkbNumLevels.Size = new System.Drawing.Size(146, 25);
            this.trkbNumLevels.TabIndex = 3;
            this.trkbNumLevels.Tag = "NUMLEVELS";
            this.trkbNumLevels.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkbNumLevels.Value = 6;
            this.trkbNumLevels.Scroll += new System.EventHandler(this.Trkb_Scroll);
            // 
            // trkbMaxScale
            // 
            this.trkbMaxScale.AutoSize = false;
            this.trkbMaxScale.Location = new System.Drawing.Point(258, 126);
            this.trkbMaxScale.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkbMaxScale.Maximum = 200;
            this.trkbMaxScale.Minimum = 1;
            this.trkbMaxScale.Name = "trkbMaxScale";
            this.trkbMaxScale.Size = new System.Drawing.Size(146, 25);
            this.trkbMaxScale.TabIndex = 3;
            this.trkbMaxScale.Tag = "MAXSCALE";
            this.trkbMaxScale.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkbMaxScale.Value = 100;
            this.trkbMaxScale.Scroll += new System.EventHandler(this.Trkb_Scroll);
            // 
            // trkbAngleStep
            // 
            this.trkbAngleStep.AutoSize = false;
            this.trkbAngleStep.Location = new System.Drawing.Point(258, 275);
            this.trkbAngleStep.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkbAngleStep.Maximum = 112;
            this.trkbAngleStep.Minimum = 1;
            this.trkbAngleStep.Name = "trkbAngleStep";
            this.trkbAngleStep.Size = new System.Drawing.Size(146, 25);
            this.trkbAngleStep.TabIndex = 3;
            this.trkbAngleStep.Tag = "ANGLESTEP";
            this.trkbAngleStep.TickFrequency = 10;
            this.trkbAngleStep.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkbAngleStep.Value = 10;
            this.trkbAngleStep.Scroll += new System.EventHandler(this.Trkb_Scroll);
            // 
            // trkbMinScale
            // 
            this.trkbMinScale.AutoSize = false;
            this.trkbMinScale.Location = new System.Drawing.Point(258, 87);
            this.trkbMinScale.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkbMinScale.Maximum = 200;
            this.trkbMinScale.Minimum = 1;
            this.trkbMinScale.Name = "trkbMinScale";
            this.trkbMinScale.Size = new System.Drawing.Size(146, 25);
            this.trkbMinScale.TabIndex = 3;
            this.trkbMinScale.Tag = "MINSCALE";
            this.trkbMinScale.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkbMinScale.Value = 100;
            this.trkbMinScale.Scroll += new System.EventHandler(this.Trkb_Scroll);
            // 
            // trkbAngleExtent
            // 
            this.trkbAngleExtent.AutoSize = false;
            this.trkbAngleExtent.Location = new System.Drawing.Point(258, 239);
            this.trkbAngleExtent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkbAngleExtent.Maximum = 360;
            this.trkbAngleExtent.Name = "trkbAngleExtent";
            this.trkbAngleExtent.Size = new System.Drawing.Size(146, 25);
            this.trkbAngleExtent.TabIndex = 3;
            this.trkbAngleExtent.Tag = "ANGLEEXTENT";
            this.trkbAngleExtent.TickFrequency = 20;
            this.trkbAngleExtent.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkbAngleExtent.Scroll += new System.EventHandler(this.Trkb_Scroll);
            // 
            // trkbContrast
            // 
            this.trkbContrast.AutoSize = false;
            this.trkbContrast.Location = new System.Drawing.Point(258, 52);
            this.trkbContrast.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkbContrast.Maximum = 255;
            this.trkbContrast.Name = "trkbContrast";
            this.trkbContrast.Size = new System.Drawing.Size(146, 25);
            this.trkbContrast.TabIndex = 3;
            this.trkbContrast.Tag = "CONTRAST";
            this.trkbContrast.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkbContrast.Value = 30;
            this.trkbContrast.Scroll += new System.EventHandler(this.Trkb_Scroll);
            // 
            // trkbDisplayLevel
            // 
            this.trkbDisplayLevel.AutoSize = false;
            this.trkbDisplayLevel.Location = new System.Drawing.Point(258, 16);
            this.trkbDisplayLevel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkbDisplayLevel.Maximum = 6;
            this.trkbDisplayLevel.Minimum = 1;
            this.trkbDisplayLevel.Name = "trkbDisplayLevel";
            this.trkbDisplayLevel.Size = new System.Drawing.Size(146, 25);
            this.trkbDisplayLevel.TabIndex = 3;
            this.trkbDisplayLevel.Tag = "DISPLAYLEVEL";
            this.trkbDisplayLevel.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkbDisplayLevel.Value = 1;
            this.trkbDisplayLevel.Scroll += new System.EventHandler(this.Trkb_Scroll);
            // 
            // cmbOptimization
            // 
            this.cmbOptimization.FormattingEnabled = true;
            this.cmbOptimization.Items.AddRange(new object[] {
            "none",
            "point_reduction_low",
            "point_reduction_medium",
            "point_reduction_high"});
            this.cmbOptimization.Location = new System.Drawing.Point(149, 398);
            this.cmbOptimization.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbOptimization.Name = "cmbOptimization";
            this.cmbOptimization.Size = new System.Drawing.Size(256, 20);
            this.cmbOptimization.TabIndex = 2;
            this.cmbOptimization.Tag = "OPTIMIZATION";
            this.cmbOptimization.SelectedIndexChanged += new System.EventHandler(this.Cmb_SelectedIdxChanged);
            // 
            // cmbMetric
            // 
            this.cmbMetric.FormattingEnabled = true;
            this.cmbMetric.Items.AddRange(new object[] {
            "use_polarity",
            "ignore_global_polarity",
            "ignore_local_polarity",
            "ignore_color_polarity"});
            this.cmbMetric.Location = new System.Drawing.Point(149, 358);
            this.cmbMetric.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbMetric.Name = "cmbMetric";
            this.cmbMetric.Size = new System.Drawing.Size(256, 20);
            this.cmbMetric.TabIndex = 2;
            this.cmbMetric.Tag = "METRIC";
            this.cmbMetric.SelectedIndexChanged += new System.EventHandler(this.Cmb_SelectedIdxChanged);
            // 
            // numUpDwnMinContrast
            // 
            this.numUpDwnMinContrast.Location = new System.Drawing.Point(150, 437);
            this.numUpDwnMinContrast.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnMinContrast.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numUpDwnMinContrast.Name = "numUpDwnMinContrast";
            this.numUpDwnMinContrast.Size = new System.Drawing.Size(90, 21);
            this.numUpDwnMinContrast.TabIndex = 1;
            this.numUpDwnMinContrast.Tag = "MINCONTRAST";
            this.numUpDwnMinContrast.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numUpDwnMinContrast.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // numUpDwnAngleStep
            // 
            this.numUpDwnAngleStep.Location = new System.Drawing.Point(150, 278);
            this.numUpDwnAngleStep.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnAngleStep.Maximum = new decimal(new int[] {
            112,
            0,
            0,
            0});
            this.numUpDwnAngleStep.Name = "numUpDwnAngleStep";
            this.numUpDwnAngleStep.Size = new System.Drawing.Size(90, 21);
            this.numUpDwnAngleStep.TabIndex = 1;
            this.numUpDwnAngleStep.Tag = "ANGLESTEP";
            this.numUpDwnAngleStep.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numUpDwnAngleStep.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // numUpDwnAngleExtent
            // 
            this.numUpDwnAngleExtent.Location = new System.Drawing.Point(150, 242);
            this.numUpDwnAngleExtent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnAngleExtent.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numUpDwnAngleExtent.Name = "numUpDwnAngleExtent";
            this.numUpDwnAngleExtent.Size = new System.Drawing.Size(90, 21);
            this.numUpDwnAngleExtent.TabIndex = 1;
            this.numUpDwnAngleExtent.Tag = "ANGLEEXTENT";
            this.numUpDwnAngleExtent.Value = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numUpDwnAngleExtent.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // numUpDwnMaxScale
            // 
            this.numUpDwnMaxScale.Location = new System.Drawing.Point(150, 129);
            this.numUpDwnMaxScale.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnMaxScale.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUpDwnMaxScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDwnMaxScale.Name = "numUpDwnMaxScale";
            this.numUpDwnMaxScale.Size = new System.Drawing.Size(90, 21);
            this.numUpDwnMaxScale.TabIndex = 1;
            this.numUpDwnMaxScale.Tag = "MAXSCALE";
            this.numUpDwnMaxScale.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numUpDwnMaxScale.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // numUpDwnStartAngle
            // 
            this.numUpDwnStartAngle.Location = new System.Drawing.Point(150, 202);
            this.numUpDwnStartAngle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnStartAngle.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numUpDwnStartAngle.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.numUpDwnStartAngle.Name = "numUpDwnStartAngle";
            this.numUpDwnStartAngle.Size = new System.Drawing.Size(90, 21);
            this.numUpDwnStartAngle.TabIndex = 1;
            this.numUpDwnStartAngle.Tag = "STARTANGLE";
            this.numUpDwnStartAngle.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // numUpDwnNumLevels
            // 
            this.numUpDwnNumLevels.Location = new System.Drawing.Point(150, 316);
            this.numUpDwnNumLevels.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnNumLevels.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numUpDwnNumLevels.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDwnNumLevels.Name = "numUpDwnNumLevels";
            this.numUpDwnNumLevels.Size = new System.Drawing.Size(90, 21);
            this.numUpDwnNumLevels.TabIndex = 1;
            this.numUpDwnNumLevels.Tag = "NUMLEVELS";
            this.numUpDwnNumLevels.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numUpDwnNumLevels.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // numUpDwnMinScale
            // 
            this.numUpDwnMinScale.Location = new System.Drawing.Point(150, 90);
            this.numUpDwnMinScale.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnMinScale.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUpDwnMinScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDwnMinScale.Name = "numUpDwnMinScale";
            this.numUpDwnMinScale.Size = new System.Drawing.Size(90, 21);
            this.numUpDwnMinScale.TabIndex = 1;
            this.numUpDwnMinScale.Tag = "MINSCALE";
            this.numUpDwnMinScale.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numUpDwnMinScale.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // numUpDwnScaleStep
            // 
            this.numUpDwnScaleStep.Location = new System.Drawing.Point(150, 166);
            this.numUpDwnScaleStep.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnScaleStep.Maximum = new decimal(new int[] {
            190,
            0,
            0,
            0});
            this.numUpDwnScaleStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDwnScaleStep.Name = "numUpDwnScaleStep";
            this.numUpDwnScaleStep.Size = new System.Drawing.Size(90, 21);
            this.numUpDwnScaleStep.TabIndex = 1;
            this.numUpDwnScaleStep.Tag = "SCALESTEP";
            this.numUpDwnScaleStep.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numUpDwnScaleStep.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // numUpDwnContrast
            // 
            this.numUpDwnContrast.Location = new System.Drawing.Point(150, 54);
            this.numUpDwnContrast.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnContrast.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numUpDwnContrast.Name = "numUpDwnContrast";
            this.numUpDwnContrast.Size = new System.Drawing.Size(90, 21);
            this.numUpDwnContrast.TabIndex = 1;
            this.numUpDwnContrast.Tag = "CONTRAST";
            this.numUpDwnContrast.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numUpDwnContrast.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // numUpDwnDisplayLevel
            // 
            this.numUpDwnDisplayLevel.Location = new System.Drawing.Point(150, 18);
            this.numUpDwnDisplayLevel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnDisplayLevel.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numUpDwnDisplayLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDwnDisplayLevel.Name = "numUpDwnDisplayLevel";
            this.numUpDwnDisplayLevel.Size = new System.Drawing.Size(90, 21);
            this.numUpDwnDisplayLevel.TabIndex = 1;
            this.numUpDwnDisplayLevel.Tag = "DISPLAYLEVEL";
            this.numUpDwnDisplayLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDwnDisplayLevel.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(22, 441);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "最小对比度";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 402);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "最优化选项";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 282);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "角步长(x10)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 246);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "角范围(度)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 362);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "度量选项";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 133);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "最大缩放系数(x100)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 206);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "起始角(度)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 320);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "最大金字塔等级数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 94);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "最小缩放系数(x100)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 170);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "缩放系数步长(x1000)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "对比度";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "显示图层等级";
            // 
            // tbpFindModelPara
            // 
            this.tbpFindModelPara.Controls.Add(this.pnlFindModelPara);
            this.tbpFindModelPara.Location = new System.Drawing.Point(4, 22);
            this.tbpFindModelPara.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbpFindModelPara.Name = "tbpFindModelPara";
            this.tbpFindModelPara.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbpFindModelPara.Size = new System.Drawing.Size(675, 482);
            this.tbpFindModelPara.TabIndex = 1;
            this.tbpFindModelPara.Text = "查找模型参数";
            this.tbpFindModelPara.UseVisualStyleBackColor = true;
            // 
            // pnlFindModelPara
            // 
            this.pnlFindModelPara.Controls.Add(this.cmbSubPixel);
            this.pnlFindModelPara.Controls.Add(this.label18);
            this.pnlFindModelPara.Controls.Add(this.trkbLastLevel);
            this.pnlFindModelPara.Controls.Add(this.numUpDwnLastLevel);
            this.pnlFindModelPara.Controls.Add(this.trkbMaxOverlap);
            this.pnlFindModelPara.Controls.Add(this.numUpDwnMaxOverlap);
            this.pnlFindModelPara.Controls.Add(this.trkbGreediness);
            this.pnlFindModelPara.Controls.Add(this.numUpDwnGreediness);
            this.pnlFindModelPara.Controls.Add(this.label17);
            this.pnlFindModelPara.Controls.Add(this.trkbNumToMatch);
            this.pnlFindModelPara.Controls.Add(this.label16);
            this.pnlFindModelPara.Controls.Add(this.numUpDwnNumToMatch);
            this.pnlFindModelPara.Controls.Add(this.label15);
            this.pnlFindModelPara.Controls.Add(this.trkbMinScore);
            this.pnlFindModelPara.Controls.Add(this.label14);
            this.pnlFindModelPara.Controls.Add(this.numUpDwnMinScore);
            this.pnlFindModelPara.Controls.Add(this.label13);
            this.pnlFindModelPara.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFindModelPara.Location = new System.Drawing.Point(2, 2);
            this.pnlFindModelPara.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlFindModelPara.Name = "pnlFindModelPara";
            this.pnlFindModelPara.Size = new System.Drawing.Size(671, 478);
            this.pnlFindModelPara.TabIndex = 0;
            // 
            // cmbSubPixel
            // 
            this.cmbSubPixel.FormattingEnabled = true;
            this.cmbSubPixel.Items.AddRange(new object[] {
            "none",
            "interpolation",
            "least_squares",
            "least_squares_high",
            "least_squares_very_high"});
            this.cmbSubPixel.Location = new System.Drawing.Point(170, 223);
            this.cmbSubPixel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbSubPixel.Name = "cmbSubPixel";
            this.cmbSubPixel.Size = new System.Drawing.Size(256, 20);
            this.cmbSubPixel.TabIndex = 8;
            this.cmbSubPixel.Tag = "SUBPIXEL";
            this.cmbSubPixel.SelectedIndexChanged += new System.EventHandler(this.Cmb_SelectedIdxChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(38, 226);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 12);
            this.label18.TabIndex = 7;
            this.label18.Text = "亚像素等级";
            // 
            // trkbLastLevel
            // 
            this.trkbLastLevel.AutoSize = false;
            this.trkbLastLevel.Location = new System.Drawing.Point(278, 258);
            this.trkbLastLevel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkbLastLevel.Maximum = 6;
            this.trkbLastLevel.Minimum = 1;
            this.trkbLastLevel.Name = "trkbLastLevel";
            this.trkbLastLevel.Size = new System.Drawing.Size(146, 25);
            this.trkbLastLevel.TabIndex = 6;
            this.trkbLastLevel.Tag = "LASTLEVEL";
            this.trkbLastLevel.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkbLastLevel.Value = 1;
            this.trkbLastLevel.Scroll += new System.EventHandler(this.Trkb_Scroll);
            // 
            // numUpDwnLastLevel
            // 
            this.numUpDwnLastLevel.Location = new System.Drawing.Point(170, 262);
            this.numUpDwnLastLevel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnLastLevel.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numUpDwnLastLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDwnLastLevel.Name = "numUpDwnLastLevel";
            this.numUpDwnLastLevel.Size = new System.Drawing.Size(90, 21);
            this.numUpDwnLastLevel.TabIndex = 5;
            this.numUpDwnLastLevel.Tag = "LASTLEVEL";
            this.numUpDwnLastLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDwnLastLevel.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // trkbMaxOverlap
            // 
            this.trkbMaxOverlap.AutoSize = false;
            this.trkbMaxOverlap.Location = new System.Drawing.Point(278, 174);
            this.trkbMaxOverlap.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkbMaxOverlap.Maximum = 100;
            this.trkbMaxOverlap.Name = "trkbMaxOverlap";
            this.trkbMaxOverlap.Size = new System.Drawing.Size(146, 25);
            this.trkbMaxOverlap.TabIndex = 6;
            this.trkbMaxOverlap.Tag = "MAXOVERLAP";
            this.trkbMaxOverlap.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkbMaxOverlap.Value = 50;
            this.trkbMaxOverlap.Scroll += new System.EventHandler(this.Trkb_Scroll);
            // 
            // numUpDwnMaxOverlap
            // 
            this.numUpDwnMaxOverlap.Location = new System.Drawing.Point(170, 174);
            this.numUpDwnMaxOverlap.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnMaxOverlap.Name = "numUpDwnMaxOverlap";
            this.numUpDwnMaxOverlap.Size = new System.Drawing.Size(90, 21);
            this.numUpDwnMaxOverlap.TabIndex = 5;
            this.numUpDwnMaxOverlap.Tag = "MAXOVERLAP";
            this.numUpDwnMaxOverlap.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numUpDwnMaxOverlap.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // trkbGreediness
            // 
            this.trkbGreediness.AutoSize = false;
            this.trkbGreediness.Location = new System.Drawing.Point(278, 123);
            this.trkbGreediness.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkbGreediness.Maximum = 100;
            this.trkbGreediness.Name = "trkbGreediness";
            this.trkbGreediness.Size = new System.Drawing.Size(146, 25);
            this.trkbGreediness.TabIndex = 6;
            this.trkbGreediness.Tag = "GREEDINESS";
            this.trkbGreediness.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkbGreediness.Value = 75;
            this.trkbGreediness.Scroll += new System.EventHandler(this.Trkb_Scroll);
            // 
            // numUpDwnGreediness
            // 
            this.numUpDwnGreediness.Location = new System.Drawing.Point(170, 131);
            this.numUpDwnGreediness.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnGreediness.Name = "numUpDwnGreediness";
            this.numUpDwnGreediness.Size = new System.Drawing.Size(90, 21);
            this.numUpDwnGreediness.TabIndex = 5;
            this.numUpDwnGreediness.Tag = "GREEDINESS";
            this.numUpDwnGreediness.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            this.numUpDwnGreediness.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(38, 270);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(89, 12);
            this.label17.TabIndex = 4;
            this.label17.Text = "上次金字塔等级";
            // 
            // trkbNumToMatch
            // 
            this.trkbNumToMatch.AutoSize = false;
            this.trkbNumToMatch.Location = new System.Drawing.Point(278, 83);
            this.trkbNumToMatch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkbNumToMatch.Maximum = 100;
            this.trkbNumToMatch.Name = "trkbNumToMatch";
            this.trkbNumToMatch.Size = new System.Drawing.Size(146, 25);
            this.trkbNumToMatch.TabIndex = 6;
            this.trkbNumToMatch.Tag = "NUMTOMATCH";
            this.trkbNumToMatch.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkbNumToMatch.Value = 1;
            this.trkbNumToMatch.Scroll += new System.EventHandler(this.Trkb_Scroll);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(38, 181);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(113, 12);
            this.label16.TabIndex = 4;
            this.label16.Text = "最大重叠系数(x100)";
            // 
            // numUpDwnNumToMatch
            // 
            this.numUpDwnNumToMatch.Location = new System.Drawing.Point(170, 88);
            this.numUpDwnNumToMatch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnNumToMatch.Name = "numUpDwnNumToMatch";
            this.numUpDwnNumToMatch.Size = new System.Drawing.Size(90, 21);
            this.numUpDwnNumToMatch.TabIndex = 5;
            this.numUpDwnNumToMatch.Tag = "NUMTOMATCH";
            this.numUpDwnNumToMatch.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDwnNumToMatch.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(38, 136);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(101, 12);
            this.label15.TabIndex = 4;
            this.label15.Text = "贪婪度系数(x100)";
            // 
            // trkbMinScore
            // 
            this.trkbMinScore.AutoSize = false;
            this.trkbMinScore.Location = new System.Drawing.Point(278, 40);
            this.trkbMinScore.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkbMinScore.Maximum = 100;
            this.trkbMinScore.Name = "trkbMinScore";
            this.trkbMinScore.Size = new System.Drawing.Size(146, 25);
            this.trkbMinScore.TabIndex = 6;
            this.trkbMinScore.Tag = "MINSCORE";
            this.trkbMinScore.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkbMinScore.Value = 50;
            this.trkbMinScore.Scroll += new System.EventHandler(this.Trkb_Scroll);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(38, 91);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(113, 12);
            this.label14.TabIndex = 4;
            this.label14.Text = "预期匹配模型实例数";
            // 
            // numUpDwnMinScore
            // 
            this.numUpDwnMinScore.Location = new System.Drawing.Point(170, 45);
            this.numUpDwnMinScore.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnMinScore.Name = "numUpDwnMinScore";
            this.numUpDwnMinScore.Size = new System.Drawing.Size(90, 21);
            this.numUpDwnMinScore.TabIndex = 5;
            this.numUpDwnMinScore.Tag = "MINSCORE";
            this.numUpDwnMinScore.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numUpDwnMinScore.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(38, 46);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(113, 12);
            this.label13.TabIndex = 4;
            this.label13.Text = "最小匹配得分(x100)";
            // 
            // tbpInspectModel
            // 
            this.tbpInspectModel.Controls.Add(this.splitContainerInspect);
            this.tbpInspectModel.Location = new System.Drawing.Point(4, 22);
            this.tbpInspectModel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbpInspectModel.Name = "tbpInspectModel";
            this.tbpInspectModel.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbpInspectModel.Size = new System.Drawing.Size(675, 482);
            this.tbpInspectModel.TabIndex = 2;
            this.tbpInspectModel.Text = "检测模型";
            this.tbpInspectModel.UseVisualStyleBackColor = true;
            // 
            // splitContainerInspect
            // 
            this.splitContainerInspect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerInspect.Location = new System.Drawing.Point(2, 2);
            this.splitContainerInspect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainerInspect.Name = "splitContainerInspect";
            this.splitContainerInspect.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerInspect.Panel1
            // 
            this.splitContainerInspect.Panel1.Controls.Add(this.pnlOperation);
            this.splitContainerInspect.Panel1.Controls.Add(this.lstbTestImages);
            // 
            // splitContainerInspect.Panel2
            // 
            this.splitContainerInspect.Panel2.Controls.Add(this.dgvMatchResult);
            this.splitContainerInspect.Size = new System.Drawing.Size(671, 478);
            this.splitContainerInspect.SplitterDistance = 308;
            this.splitContainerInspect.SplitterWidth = 3;
            this.splitContainerInspect.TabIndex = 0;
            // 
            // pnlOperation
            // 
            this.pnlOperation.Controls.Add(this.chkbAlwaysFind);
            this.pnlOperation.Controls.Add(this.btnFindModel);
            this.pnlOperation.Controls.Add(this.btnDisplayImage);
            this.pnlOperation.Controls.Add(this.btnClearImageList);
            this.pnlOperation.Controls.Add(this.btnDeleteImage);
            this.pnlOperation.Controls.Add(this.btnLoadImageList);
            this.pnlOperation.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlOperation.Location = new System.Drawing.Point(445, 0);
            this.pnlOperation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlOperation.Name = "pnlOperation";
            this.pnlOperation.Size = new System.Drawing.Size(223, 308);
            this.pnlOperation.TabIndex = 1;
            // 
            // chkbAlwaysFind
            // 
            this.chkbAlwaysFind.AutoSize = true;
            this.chkbAlwaysFind.Location = new System.Drawing.Point(52, 266);
            this.chkbAlwaysFind.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkbAlwaysFind.Name = "chkbAlwaysFind";
            this.chkbAlwaysFind.Size = new System.Drawing.Size(72, 16);
            this.chkbAlwaysFind.TabIndex = 1;
            this.chkbAlwaysFind.Tag = "ALWAYSFIND";
            this.chkbAlwaysFind.Text = "总是查找";
            this.chkbAlwaysFind.UseVisualStyleBackColor = true;
            this.chkbAlwaysFind.CheckedChanged += new System.EventHandler(this.Chkb_CheckedChanged);
            // 
            // btnFindModel
            // 
            this.btnFindModel.Location = new System.Drawing.Point(52, 223);
            this.btnFindModel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnFindModel.Name = "btnFindModel";
            this.btnFindModel.Size = new System.Drawing.Size(75, 24);
            this.btnFindModel.TabIndex = 0;
            this.btnFindModel.Tag = "FINDMODEL";
            this.btnFindModel.Text = "查找模板";
            this.btnFindModel.UseVisualStyleBackColor = true;
            this.btnFindModel.Click += new System.EventHandler(this.Btn_Click);
            // 
            // btnDisplayImage
            // 
            this.btnDisplayImage.Location = new System.Drawing.Point(52, 178);
            this.btnDisplayImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDisplayImage.Name = "btnDisplayImage";
            this.btnDisplayImage.Size = new System.Drawing.Size(75, 24);
            this.btnDisplayImage.TabIndex = 0;
            this.btnDisplayImage.Tag = "DISPLAYTESTIMAGE";
            this.btnDisplayImage.Text = "显示图像";
            this.btnDisplayImage.UseVisualStyleBackColor = true;
            this.btnDisplayImage.Click += new System.EventHandler(this.Btn_Click);
            // 
            // btnClearImageList
            // 
            this.btnClearImageList.Location = new System.Drawing.Point(52, 134);
            this.btnClearImageList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClearImageList.Name = "btnClearImageList";
            this.btnClearImageList.Size = new System.Drawing.Size(75, 24);
            this.btnClearImageList.TabIndex = 0;
            this.btnClearImageList.Tag = "CLEARTESTIMAGES";
            this.btnClearImageList.Text = "清空图像";
            this.btnClearImageList.UseVisualStyleBackColor = true;
            this.btnClearImageList.Click += new System.EventHandler(this.Btn_Click);
            // 
            // btnDeleteImage
            // 
            this.btnDeleteImage.Location = new System.Drawing.Point(52, 89);
            this.btnDeleteImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDeleteImage.Name = "btnDeleteImage";
            this.btnDeleteImage.Size = new System.Drawing.Size(75, 24);
            this.btnDeleteImage.TabIndex = 0;
            this.btnDeleteImage.Tag = "DELETETESTIMAGE";
            this.btnDeleteImage.Text = "删除图像";
            this.btnDeleteImage.UseVisualStyleBackColor = true;
            this.btnDeleteImage.Click += new System.EventHandler(this.Btn_Click);
            // 
            // btnLoadImageList
            // 
            this.btnLoadImageList.Location = new System.Drawing.Point(52, 44);
            this.btnLoadImageList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLoadImageList.Name = "btnLoadImageList";
            this.btnLoadImageList.Size = new System.Drawing.Size(75, 24);
            this.btnLoadImageList.TabIndex = 0;
            this.btnLoadImageList.Tag = "LOADTESTIMAGES";
            this.btnLoadImageList.Text = "图像列表";
            this.btnLoadImageList.UseVisualStyleBackColor = true;
            this.btnLoadImageList.Click += new System.EventHandler(this.Btn_Click);
            // 
            // lstbTestImages
            // 
            this.lstbTestImages.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstbTestImages.FormattingEnabled = true;
            this.lstbTestImages.ItemHeight = 12;
            this.lstbTestImages.Location = new System.Drawing.Point(0, 0);
            this.lstbTestImages.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lstbTestImages.Name = "lstbTestImages";
            this.lstbTestImages.Size = new System.Drawing.Size(445, 308);
            this.lstbTestImages.TabIndex = 0;
            this.lstbTestImages.SelectedIndexChanged += new System.EventHandler(this.Lstb_SelectedIndexChanged);
            // 
            // dgvMatchResult
            // 
            this.dgvMatchResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatchResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColNumber,
            this.ColScore});
            this.dgvMatchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatchResult.Location = new System.Drawing.Point(0, 0);
            this.dgvMatchResult.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvMatchResult.Name = "dgvMatchResult";
            this.dgvMatchResult.RowTemplate.Height = 27;
            this.dgvMatchResult.Size = new System.Drawing.Size(671, 167);
            this.dgvMatchResult.TabIndex = 0;
            this.dgvMatchResult.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_RowEnter);
            // 
            // ColNumber
            // 
            this.ColNumber.HeaderText = "序号";
            this.ColNumber.Name = "ColNumber";
            this.ColNumber.ReadOnly = true;
            // 
            // ColScore
            // 
            this.ColScore.HeaderText = "得分";
            this.ColScore.Name = "ColScore";
            this.ColScore.ReadOnly = true;
            // 
            // tbpOptimize
            // 
            this.tbpOptimize.Controls.Add(this.pnlOptimization);
            this.tbpOptimize.Location = new System.Drawing.Point(4, 22);
            this.tbpOptimize.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbpOptimize.Name = "tbpOptimize";
            this.tbpOptimize.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbpOptimize.Size = new System.Drawing.Size(675, 482);
            this.tbpOptimize.TabIndex = 3;
            this.tbpOptimize.Text = "优化模型";
            this.tbpOptimize.UseVisualStyleBackColor = true;
            // 
            // pnlOptimization
            // 
            this.pnlOptimization.Controls.Add(this.btnOptimize);
            this.pnlOptimization.Controls.Add(this.label23);
            this.pnlOptimization.Controls.Add(this.label22);
            this.pnlOptimization.Controls.Add(this.label27);
            this.pnlOptimization.Controls.Add(this.label26);
            this.pnlOptimization.Controls.Add(this.lblOptElapse);
            this.pnlOptimization.Controls.Add(this.lblLastElapse);
            this.pnlOptimization.Controls.Add(this.lblOptRecogRate);
            this.pnlOptimization.Controls.Add(this.lblLastRecogRate);
            this.pnlOptimization.Controls.Add(this.lblOptGreediness);
            this.pnlOptimization.Controls.Add(this.lblLastGreediness);
            this.pnlOptimization.Controls.Add(this.lblOptMinScore);
            this.pnlOptimization.Controls.Add(this.lblLastMinScore);
            this.pnlOptimization.Controls.Add(this.label25);
            this.pnlOptimization.Controls.Add(this.label24);
            this.pnlOptimization.Controls.Add(this.label21);
            this.pnlOptimization.Controls.Add(this.lblOptimizationStatus);
            this.pnlOptimization.Controls.Add(this.grpbMatchModel);
            this.pnlOptimization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOptimization.Location = new System.Drawing.Point(2, 2);
            this.pnlOptimization.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlOptimization.Name = "pnlOptimization";
            this.pnlOptimization.Size = new System.Drawing.Size(671, 478);
            this.pnlOptimization.TabIndex = 0;
            // 
            // btnOptimize
            // 
            this.btnOptimize.Location = new System.Drawing.Point(369, 418);
            this.btnOptimize.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOptimize.Name = "btnOptimize";
            this.btnOptimize.Size = new System.Drawing.Size(90, 34);
            this.btnOptimize.TabIndex = 4;
            this.btnOptimize.Tag = "OPTIMIZE";
            this.btnOptimize.Text = "开启优化";
            this.btnOptimize.UseVisualStyleBackColor = true;
            this.btnOptimize.Click += new System.EventHandler(this.Btn_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(386, 238);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(77, 12);
            this.label23.TabIndex = 3;
            this.label23.Text = "优化运行结果";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(172, 238);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(77, 12);
            this.label22.TabIndex = 3;
            this.label22.Text = "上次运行结果";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(27, 376);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(53, 12);
            this.label27.TabIndex = 3;
            this.label27.Text = "平均耗时";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(27, 342);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(41, 12);
            this.label26.TabIndex = 3;
            this.label26.Text = "识别率";
            // 
            // lblOptElapse
            // 
            this.lblOptElapse.BackColor = System.Drawing.Color.Tomato;
            this.lblOptElapse.Location = new System.Drawing.Point(386, 366);
            this.lblOptElapse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOptElapse.Name = "lblOptElapse";
            this.lblOptElapse.Size = new System.Drawing.Size(72, 22);
            this.lblOptElapse.TabIndex = 3;
            this.lblOptElapse.Text = "-";
            this.lblOptElapse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLastElapse
            // 
            this.lblLastElapse.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblLastElapse.Location = new System.Drawing.Point(172, 366);
            this.lblLastElapse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLastElapse.Name = "lblLastElapse";
            this.lblLastElapse.Size = new System.Drawing.Size(72, 22);
            this.lblLastElapse.TabIndex = 3;
            this.lblLastElapse.Text = "-";
            this.lblLastElapse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOptRecogRate
            // 
            this.lblOptRecogRate.BackColor = System.Drawing.Color.Tomato;
            this.lblOptRecogRate.Location = new System.Drawing.Point(386, 332);
            this.lblOptRecogRate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOptRecogRate.Name = "lblOptRecogRate";
            this.lblOptRecogRate.Size = new System.Drawing.Size(72, 22);
            this.lblOptRecogRate.TabIndex = 3;
            this.lblOptRecogRate.Text = "-";
            this.lblOptRecogRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLastRecogRate
            // 
            this.lblLastRecogRate.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblLastRecogRate.Location = new System.Drawing.Point(172, 332);
            this.lblLastRecogRate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLastRecogRate.Name = "lblLastRecogRate";
            this.lblLastRecogRate.Size = new System.Drawing.Size(72, 22);
            this.lblLastRecogRate.TabIndex = 3;
            this.lblLastRecogRate.Text = "-";
            this.lblLastRecogRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOptGreediness
            // 
            this.lblOptGreediness.BackColor = System.Drawing.Color.Tomato;
            this.lblOptGreediness.Location = new System.Drawing.Point(386, 298);
            this.lblOptGreediness.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOptGreediness.Name = "lblOptGreediness";
            this.lblOptGreediness.Size = new System.Drawing.Size(72, 22);
            this.lblOptGreediness.TabIndex = 3;
            this.lblOptGreediness.Text = "-";
            this.lblOptGreediness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLastGreediness
            // 
            this.lblLastGreediness.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblLastGreediness.Location = new System.Drawing.Point(172, 298);
            this.lblLastGreediness.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLastGreediness.Name = "lblLastGreediness";
            this.lblLastGreediness.Size = new System.Drawing.Size(72, 22);
            this.lblLastGreediness.TabIndex = 3;
            this.lblLastGreediness.Text = "-";
            this.lblLastGreediness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOptMinScore
            // 
            this.lblOptMinScore.BackColor = System.Drawing.Color.Tomato;
            this.lblOptMinScore.Location = new System.Drawing.Point(386, 263);
            this.lblOptMinScore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOptMinScore.Name = "lblOptMinScore";
            this.lblOptMinScore.Size = new System.Drawing.Size(72, 22);
            this.lblOptMinScore.TabIndex = 3;
            this.lblOptMinScore.Text = "-";
            this.lblOptMinScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLastMinScore
            // 
            this.lblLastMinScore.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblLastMinScore.Location = new System.Drawing.Point(172, 263);
            this.lblLastMinScore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLastMinScore.Name = "lblLastMinScore";
            this.lblLastMinScore.Size = new System.Drawing.Size(72, 22);
            this.lblLastMinScore.TabIndex = 3;
            this.lblLastMinScore.Text = "-";
            this.lblLastMinScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(27, 307);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(41, 12);
            this.label25.TabIndex = 3;
            this.label25.Text = "贪婪度";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(27, 273);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(77, 12);
            this.label24.TabIndex = 3;
            this.label24.Text = "最小匹配得分";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(27, 238);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 12);
            this.label21.TabIndex = 3;
            this.label21.Text = "统计";
            // 
            // lblOptimizationStatus
            // 
            this.lblOptimizationStatus.BackColor = System.Drawing.SystemColors.Info;
            this.lblOptimizationStatus.Font = new System.Drawing.Font("SimHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOptimizationStatus.Location = new System.Drawing.Point(26, 192);
            this.lblOptimizationStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOptimizationStatus.Name = "lblOptimizationStatus";
            this.lblOptimizationStatus.Size = new System.Drawing.Size(483, 18);
            this.lblOptimizationStatus.TabIndex = 2;
            this.lblOptimizationStatus.Text = "优化状态:";
            // 
            // grpbMatchModel
            // 
            this.grpbMatchModel.Controls.Add(this.trkbRecogRate);
            this.grpbMatchModel.Controls.Add(this.cmbRecogRateOption);
            this.grpbMatchModel.Controls.Add(this.numUpDwnRecogRate);
            this.grpbMatchModel.Controls.Add(this.numUpDwnSpecifiedNum);
            this.grpbMatchModel.Controls.Add(this.label20);
            this.grpbMatchModel.Controls.Add(this.rdbtnMaxNum);
            this.grpbMatchModel.Controls.Add(this.label19);
            this.grpbMatchModel.Controls.Add(this.rdbtnAtLeastOne);
            this.grpbMatchModel.Controls.Add(this.rdbtnSpecifiedNum);
            this.grpbMatchModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpbMatchModel.Location = new System.Drawing.Point(0, 0);
            this.grpbMatchModel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpbMatchModel.Name = "grpbMatchModel";
            this.grpbMatchModel.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpbMatchModel.Size = new System.Drawing.Size(671, 164);
            this.grpbMatchModel.TabIndex = 1;
            this.grpbMatchModel.TabStop = false;
            this.grpbMatchModel.Text = "匹配选项";
            // 
            // trkbRecogRate
            // 
            this.trkbRecogRate.AutoSize = false;
            this.trkbRecogRate.Location = new System.Drawing.Point(267, 110);
            this.trkbRecogRate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trkbRecogRate.Maximum = 100;
            this.trkbRecogRate.Minimum = 1;
            this.trkbRecogRate.Name = "trkbRecogRate";
            this.trkbRecogRate.Size = new System.Drawing.Size(168, 23);
            this.trkbRecogRate.TabIndex = 4;
            this.trkbRecogRate.Tag = "RECOGRATE";
            this.trkbRecogRate.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkbRecogRate.Value = 90;
            this.trkbRecogRate.Scroll += new System.EventHandler(this.Trkb_Scroll);
            // 
            // cmbRecogRateOption
            // 
            this.cmbRecogRateOption.FormattingEnabled = true;
            this.cmbRecogRateOption.Items.AddRange(new object[] {
            "=",
            ">="});
            this.cmbRecogRateOption.Location = new System.Drawing.Point(90, 113);
            this.cmbRecogRateOption.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbRecogRateOption.Name = "cmbRecogRateOption";
            this.cmbRecogRateOption.Size = new System.Drawing.Size(80, 20);
            this.cmbRecogRateOption.TabIndex = 3;
            this.cmbRecogRateOption.Tag = "RECOGRATEOPTION";
            this.cmbRecogRateOption.SelectedIndexChanged += new System.EventHandler(this.Cmb_SelectedIdxChanged);
            // 
            // numUpDwnRecogRate
            // 
            this.numUpDwnRecogRate.Location = new System.Drawing.Point(173, 113);
            this.numUpDwnRecogRate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnRecogRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDwnRecogRate.Name = "numUpDwnRecogRate";
            this.numUpDwnRecogRate.Size = new System.Drawing.Size(71, 21);
            this.numUpDwnRecogRate.TabIndex = 2;
            this.numUpDwnRecogRate.Tag = "RECOGRATE";
            this.numUpDwnRecogRate.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numUpDwnRecogRate.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // numUpDwnSpecifiedNum
            // 
            this.numUpDwnSpecifiedNum.Location = new System.Drawing.Point(364, 27);
            this.numUpDwnSpecifiedNum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnSpecifiedNum.Name = "numUpDwnSpecifiedNum";
            this.numUpDwnSpecifiedNum.Size = new System.Drawing.Size(71, 21);
            this.numUpDwnSpecifiedNum.TabIndex = 2;
            this.numUpDwnSpecifiedNum.Tag = "SPECIFIEDNUM";
            this.numUpDwnSpecifiedNum.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(23, 115);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 12);
            this.label20.TabIndex = 0;
            this.label20.Text = "识别率";
            // 
            // rdbtnMaxNum
            // 
            this.rdbtnMaxNum.AutoSize = true;
            this.rdbtnMaxNum.Checked = true;
            this.rdbtnMaxNum.Location = new System.Drawing.Point(88, 83);
            this.rdbtnMaxNum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnMaxNum.Name = "rdbtnMaxNum";
            this.rdbtnMaxNum.Size = new System.Drawing.Size(191, 16);
            this.rdbtnMaxNum.TabIndex = 1;
            this.rdbtnMaxNum.TabStop = true;
            this.rdbtnMaxNum.Tag = "FINDMAXNUM";
            this.rdbtnMaxNum.Text = "查找尽可能多的匹配模板的实例";
            this.rdbtnMaxNum.UseVisualStyleBackColor = true;
            this.rdbtnMaxNum.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(23, 29);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.TabIndex = 0;
            this.label19.Text = "识别模式";
            // 
            // rdbtnAtLeastOne
            // 
            this.rdbtnAtLeastOne.AutoSize = true;
            this.rdbtnAtLeastOne.Location = new System.Drawing.Point(88, 55);
            this.rdbtnAtLeastOne.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnAtLeastOne.Name = "rdbtnAtLeastOne";
            this.rdbtnAtLeastOne.Size = new System.Drawing.Size(227, 16);
            this.rdbtnAtLeastOne.TabIndex = 1;
            this.rdbtnAtLeastOne.Tag = "FINDATLEASTONE";
            this.rdbtnAtLeastOne.Text = "查找每张图至少有一个匹配模板的实例";
            this.rdbtnAtLeastOne.UseVisualStyleBackColor = true;
            this.rdbtnAtLeastOne.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // rdbtnSpecifiedNum
            // 
            this.rdbtnSpecifiedNum.AutoSize = true;
            this.rdbtnSpecifiedNum.Location = new System.Drawing.Point(88, 27);
            this.rdbtnSpecifiedNum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbtnSpecifiedNum.Name = "rdbtnSpecifiedNum";
            this.rdbtnSpecifiedNum.Size = new System.Drawing.Size(191, 16);
            this.rdbtnSpecifiedNum.TabIndex = 1;
            this.rdbtnSpecifiedNum.Tag = "FINDSPECIFIEDNUM";
            this.rdbtnSpecifiedNum.Text = "查找指定数目的匹配模板的实例";
            this.rdbtnSpecifiedNum.UseVisualStyleBackColor = true;
            this.rdbtnSpecifiedNum.CheckedChanged += new System.EventHandler(this.Rdbtn_CheckedChanged);
            // 
            // tbpStatistic
            // 
            this.tbpStatistic.Controls.Add(this.grpbStatistic);
            this.tbpStatistic.Controls.Add(this.grpbRecogRate);
            this.tbpStatistic.Location = new System.Drawing.Point(4, 22);
            this.tbpStatistic.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbpStatistic.Name = "tbpStatistic";
            this.tbpStatistic.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbpStatistic.Size = new System.Drawing.Size(675, 482);
            this.tbpStatistic.TabIndex = 4;
            this.tbpStatistic.Text = "测试统计";
            this.tbpStatistic.UseVisualStyleBackColor = true;
            // 
            // grpbStatistic
            // 
            this.grpbStatistic.Controls.Add(this.btnStatistic);
            this.grpbStatistic.Controls.Add(this.numUpDwnMaxNumMatch);
            this.grpbStatistic.Controls.Add(this.lblInspectRangeColScale);
            this.grpbStatistic.Controls.Add(this.lblInspectRangeRowScale);
            this.grpbStatistic.Controls.Add(this.lblInspectMaxColScale);
            this.grpbStatistic.Controls.Add(this.lblInspectMaxRowScale);
            this.grpbStatistic.Controls.Add(this.lblInspectRangeAngle);
            this.grpbStatistic.Controls.Add(this.lblInspectMinColScale);
            this.grpbStatistic.Controls.Add(this.lblInspectMaxAngle);
            this.grpbStatistic.Controls.Add(this.lblInspectRangeCol);
            this.grpbStatistic.Controls.Add(this.lblInspectMinRowScale);
            this.grpbStatistic.Controls.Add(this.lblInspectMaxCol);
            this.grpbStatistic.Controls.Add(this.lblInspectRangeRow);
            this.grpbStatistic.Controls.Add(this.lblInspectMinAngle);
            this.grpbStatistic.Controls.Add(this.lblInspectMaxRow);
            this.grpbStatistic.Controls.Add(this.lblInspectMinCol);
            this.grpbStatistic.Controls.Add(this.lblInspectRangeElapse);
            this.grpbStatistic.Controls.Add(this.lblInspectMinRow);
            this.grpbStatistic.Controls.Add(this.lblInspectMaxElapse);
            this.grpbStatistic.Controls.Add(this.lblInspectRangeScore);
            this.grpbStatistic.Controls.Add(this.lblInspectMaxScore);
            this.grpbStatistic.Controls.Add(this.lblInspectMinElapse);
            this.grpbStatistic.Controls.Add(this.lblInspectMinScore);
            this.grpbStatistic.Controls.Add(this.label38);
            this.grpbStatistic.Controls.Add(this.label37);
            this.grpbStatistic.Controls.Add(this.label36);
            this.grpbStatistic.Controls.Add(this.label47);
            this.grpbStatistic.Controls.Add(this.label46);
            this.grpbStatistic.Controls.Add(this.label44);
            this.grpbStatistic.Controls.Add(this.label43);
            this.grpbStatistic.Controls.Add(this.label45);
            this.grpbStatistic.Controls.Add(this.label41);
            this.grpbStatistic.Controls.Add(this.label42);
            this.grpbStatistic.Controls.Add(this.label40);
            this.grpbStatistic.Controls.Add(this.label39);
            this.grpbStatistic.Controls.Add(this.label35);
            this.grpbStatistic.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpbStatistic.Location = new System.Drawing.Point(2, 165);
            this.grpbStatistic.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpbStatistic.Name = "grpbStatistic";
            this.grpbStatistic.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpbStatistic.Size = new System.Drawing.Size(671, 310);
            this.grpbStatistic.TabIndex = 1;
            this.grpbStatistic.TabStop = false;
            this.grpbStatistic.Text = "结果统计";
            // 
            // btnStatistic
            // 
            this.btnStatistic.Location = new System.Drawing.Point(358, 266);
            this.btnStatistic.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStatistic.Name = "btnStatistic";
            this.btnStatistic.Size = new System.Drawing.Size(90, 34);
            this.btnStatistic.TabIndex = 6;
            this.btnStatistic.Tag = "STATISTIC";
            this.btnStatistic.Text = "开启统计";
            this.btnStatistic.UseVisualStyleBackColor = true;
            this.btnStatistic.Click += new System.EventHandler(this.Btn_Click);
            // 
            // numUpDwnMaxNumMatch
            // 
            this.numUpDwnMaxNumMatch.Location = new System.Drawing.Point(122, 265);
            this.numUpDwnMaxNumMatch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numUpDwnMaxNumMatch.Name = "numUpDwnMaxNumMatch";
            this.numUpDwnMaxNumMatch.Size = new System.Drawing.Size(71, 21);
            this.numUpDwnMaxNumMatch.TabIndex = 5;
            this.numUpDwnMaxNumMatch.Tag = "SPECIFIEDNUM";
            this.numUpDwnMaxNumMatch.ValueChanged += new System.EventHandler(this.NumUpDwn_ValueChanged);
            // 
            // lblInspectRangeColScale
            // 
            this.lblInspectRangeColScale.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectRangeColScale.Location = new System.Drawing.Point(366, 231);
            this.lblInspectRangeColScale.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectRangeColScale.Name = "lblInspectRangeColScale";
            this.lblInspectRangeColScale.Size = new System.Drawing.Size(72, 22);
            this.lblInspectRangeColScale.TabIndex = 4;
            this.lblInspectRangeColScale.Text = "-";
            this.lblInspectRangeColScale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectRangeRowScale
            // 
            this.lblInspectRangeRowScale.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectRangeRowScale.Location = new System.Drawing.Point(366, 205);
            this.lblInspectRangeRowScale.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectRangeRowScale.Name = "lblInspectRangeRowScale";
            this.lblInspectRangeRowScale.Size = new System.Drawing.Size(72, 22);
            this.lblInspectRangeRowScale.TabIndex = 4;
            this.lblInspectRangeRowScale.Text = "-";
            this.lblInspectRangeRowScale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectMaxColScale
            // 
            this.lblInspectMaxColScale.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectMaxColScale.Location = new System.Drawing.Point(244, 231);
            this.lblInspectMaxColScale.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectMaxColScale.Name = "lblInspectMaxColScale";
            this.lblInspectMaxColScale.Size = new System.Drawing.Size(72, 22);
            this.lblInspectMaxColScale.TabIndex = 4;
            this.lblInspectMaxColScale.Text = "-";
            this.lblInspectMaxColScale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectMaxRowScale
            // 
            this.lblInspectMaxRowScale.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectMaxRowScale.Location = new System.Drawing.Point(244, 205);
            this.lblInspectMaxRowScale.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectMaxRowScale.Name = "lblInspectMaxRowScale";
            this.lblInspectMaxRowScale.Size = new System.Drawing.Size(72, 22);
            this.lblInspectMaxRowScale.TabIndex = 4;
            this.lblInspectMaxRowScale.Text = "-";
            this.lblInspectMaxRowScale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectRangeAngle
            // 
            this.lblInspectRangeAngle.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectRangeAngle.Location = new System.Drawing.Point(366, 178);
            this.lblInspectRangeAngle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectRangeAngle.Name = "lblInspectRangeAngle";
            this.lblInspectRangeAngle.Size = new System.Drawing.Size(72, 22);
            this.lblInspectRangeAngle.TabIndex = 4;
            this.lblInspectRangeAngle.Text = "-";
            this.lblInspectRangeAngle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectMinColScale
            // 
            this.lblInspectMinColScale.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectMinColScale.Location = new System.Drawing.Point(122, 231);
            this.lblInspectMinColScale.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectMinColScale.Name = "lblInspectMinColScale";
            this.lblInspectMinColScale.Size = new System.Drawing.Size(72, 22);
            this.lblInspectMinColScale.TabIndex = 4;
            this.lblInspectMinColScale.Text = "-";
            this.lblInspectMinColScale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectMaxAngle
            // 
            this.lblInspectMaxAngle.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectMaxAngle.Location = new System.Drawing.Point(244, 178);
            this.lblInspectMaxAngle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectMaxAngle.Name = "lblInspectMaxAngle";
            this.lblInspectMaxAngle.Size = new System.Drawing.Size(72, 22);
            this.lblInspectMaxAngle.TabIndex = 4;
            this.lblInspectMaxAngle.Text = "-";
            this.lblInspectMaxAngle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectRangeCol
            // 
            this.lblInspectRangeCol.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectRangeCol.Location = new System.Drawing.Point(366, 152);
            this.lblInspectRangeCol.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectRangeCol.Name = "lblInspectRangeCol";
            this.lblInspectRangeCol.Size = new System.Drawing.Size(72, 22);
            this.lblInspectRangeCol.TabIndex = 4;
            this.lblInspectRangeCol.Text = "-";
            this.lblInspectRangeCol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectMinRowScale
            // 
            this.lblInspectMinRowScale.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectMinRowScale.Location = new System.Drawing.Point(122, 205);
            this.lblInspectMinRowScale.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectMinRowScale.Name = "lblInspectMinRowScale";
            this.lblInspectMinRowScale.Size = new System.Drawing.Size(72, 22);
            this.lblInspectMinRowScale.TabIndex = 4;
            this.lblInspectMinRowScale.Text = "-";
            this.lblInspectMinRowScale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectMaxCol
            // 
            this.lblInspectMaxCol.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectMaxCol.Location = new System.Drawing.Point(244, 152);
            this.lblInspectMaxCol.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectMaxCol.Name = "lblInspectMaxCol";
            this.lblInspectMaxCol.Size = new System.Drawing.Size(72, 22);
            this.lblInspectMaxCol.TabIndex = 4;
            this.lblInspectMaxCol.Text = "-";
            this.lblInspectMaxCol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectRangeRow
            // 
            this.lblInspectRangeRow.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectRangeRow.Location = new System.Drawing.Point(366, 126);
            this.lblInspectRangeRow.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectRangeRow.Name = "lblInspectRangeRow";
            this.lblInspectRangeRow.Size = new System.Drawing.Size(72, 22);
            this.lblInspectRangeRow.TabIndex = 4;
            this.lblInspectRangeRow.Text = "-";
            this.lblInspectRangeRow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectMinAngle
            // 
            this.lblInspectMinAngle.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectMinAngle.Location = new System.Drawing.Point(122, 178);
            this.lblInspectMinAngle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectMinAngle.Name = "lblInspectMinAngle";
            this.lblInspectMinAngle.Size = new System.Drawing.Size(72, 22);
            this.lblInspectMinAngle.TabIndex = 4;
            this.lblInspectMinAngle.Text = "-";
            this.lblInspectMinAngle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectMaxRow
            // 
            this.lblInspectMaxRow.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectMaxRow.Location = new System.Drawing.Point(244, 126);
            this.lblInspectMaxRow.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectMaxRow.Name = "lblInspectMaxRow";
            this.lblInspectMaxRow.Size = new System.Drawing.Size(72, 22);
            this.lblInspectMaxRow.TabIndex = 4;
            this.lblInspectMaxRow.Text = "-";
            this.lblInspectMaxRow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectMinCol
            // 
            this.lblInspectMinCol.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectMinCol.Location = new System.Drawing.Point(122, 152);
            this.lblInspectMinCol.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectMinCol.Name = "lblInspectMinCol";
            this.lblInspectMinCol.Size = new System.Drawing.Size(72, 22);
            this.lblInspectMinCol.TabIndex = 4;
            this.lblInspectMinCol.Text = "-";
            this.lblInspectMinCol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectRangeElapse
            // 
            this.lblInspectRangeElapse.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectRangeElapse.Location = new System.Drawing.Point(366, 73);
            this.lblInspectRangeElapse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectRangeElapse.Name = "lblInspectRangeElapse";
            this.lblInspectRangeElapse.Size = new System.Drawing.Size(72, 22);
            this.lblInspectRangeElapse.TabIndex = 4;
            this.lblInspectRangeElapse.Text = "-";
            this.lblInspectRangeElapse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectMinRow
            // 
            this.lblInspectMinRow.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectMinRow.Location = new System.Drawing.Point(122, 126);
            this.lblInspectMinRow.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectMinRow.Name = "lblInspectMinRow";
            this.lblInspectMinRow.Size = new System.Drawing.Size(72, 22);
            this.lblInspectMinRow.TabIndex = 4;
            this.lblInspectMinRow.Text = "-";
            this.lblInspectMinRow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectMaxElapse
            // 
            this.lblInspectMaxElapse.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectMaxElapse.Location = new System.Drawing.Point(244, 73);
            this.lblInspectMaxElapse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectMaxElapse.Name = "lblInspectMaxElapse";
            this.lblInspectMaxElapse.Size = new System.Drawing.Size(72, 22);
            this.lblInspectMaxElapse.TabIndex = 4;
            this.lblInspectMaxElapse.Text = "-";
            this.lblInspectMaxElapse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectRangeScore
            // 
            this.lblInspectRangeScore.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectRangeScore.Location = new System.Drawing.Point(366, 46);
            this.lblInspectRangeScore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectRangeScore.Name = "lblInspectRangeScore";
            this.lblInspectRangeScore.Size = new System.Drawing.Size(72, 22);
            this.lblInspectRangeScore.TabIndex = 4;
            this.lblInspectRangeScore.Text = "-";
            this.lblInspectRangeScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectMaxScore
            // 
            this.lblInspectMaxScore.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectMaxScore.Location = new System.Drawing.Point(244, 46);
            this.lblInspectMaxScore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectMaxScore.Name = "lblInspectMaxScore";
            this.lblInspectMaxScore.Size = new System.Drawing.Size(72, 22);
            this.lblInspectMaxScore.TabIndex = 4;
            this.lblInspectMaxScore.Text = "-";
            this.lblInspectMaxScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectMinElapse
            // 
            this.lblInspectMinElapse.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectMinElapse.Location = new System.Drawing.Point(122, 73);
            this.lblInspectMinElapse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectMinElapse.Name = "lblInspectMinElapse";
            this.lblInspectMinElapse.Size = new System.Drawing.Size(72, 22);
            this.lblInspectMinElapse.TabIndex = 4;
            this.lblInspectMinElapse.Text = "-";
            this.lblInspectMinElapse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspectMinScore
            // 
            this.lblInspectMinScore.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblInspectMinScore.Location = new System.Drawing.Point(122, 46);
            this.lblInspectMinScore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInspectMinScore.Name = "lblInspectMinScore";
            this.lblInspectMinScore.Size = new System.Drawing.Size(72, 22);
            this.lblInspectMinScore.TabIndex = 4;
            this.lblInspectMinScore.Text = "-";
            this.lblInspectMinScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(379, 26);
            this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(53, 12);
            this.label38.TabIndex = 1;
            this.label38.Text = "范围区间";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(264, 26);
            this.label37.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(41, 12);
            this.label37.TabIndex = 1;
            this.label37.Text = "最大值";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(136, 26);
            this.label36.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(41, 12);
            this.label36.TabIndex = 1;
            this.label36.Text = "最小值";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(25, 266);
            this.label47.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(65, 12);
            this.label47.TabIndex = 1;
            this.label47.Text = "最大匹配数";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(25, 236);
            this.label46.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(41, 12);
            this.label46.TabIndex = 1;
            this.label46.Text = "列缩放";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(25, 183);
            this.label44.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(41, 12);
            this.label44.TabIndex = 1;
            this.label44.Text = "旋转角";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(25, 157);
            this.label43.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(41, 12);
            this.label43.TabIndex = 1;
            this.label43.Text = "列坐标";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(25, 210);
            this.label45.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(41, 12);
            this.label45.TabIndex = 1;
            this.label45.Text = "行缩放";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(25, 104);
            this.label41.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(53, 12);
            this.label41.TabIndex = 1;
            this.label41.Text = "位置边界";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(25, 130);
            this.label42.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(41, 12);
            this.label42.TabIndex = 1;
            this.label42.Text = "行坐标";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(25, 78);
            this.label40.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(53, 12);
            this.label40.TabIndex = 1;
            this.label40.Text = "平均耗时";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(25, 51);
            this.label39.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(53, 12);
            this.label39.TabIndex = 1;
            this.label39.Text = "匹配得分";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(25, 26);
            this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(29, 12);
            this.label35.TabIndex = 1;
            this.label35.Text = "识别";
            // 
            // grpbRecogRate
            // 
            this.grpbRecogRate.Controls.Add(this.lblToMaxNum);
            this.grpbRecogRate.Controls.Add(this.lblToSpecifiedNum);
            this.grpbRecogRate.Controls.Add(this.lblMaxNum);
            this.grpbRecogRate.Controls.Add(this.lblSpecifiedNum);
            this.grpbRecogRate.Controls.Add(this.lblAtleastOne);
            this.grpbRecogRate.Controls.Add(this.label29);
            this.grpbRecogRate.Controls.Add(this.label34);
            this.grpbRecogRate.Controls.Add(this.label33);
            this.grpbRecogRate.Controls.Add(this.label32);
            this.grpbRecogRate.Controls.Add(this.label31);
            this.grpbRecogRate.Controls.Add(this.label30);
            this.grpbRecogRate.Controls.Add(this.label28);
            this.grpbRecogRate.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpbRecogRate.Location = new System.Drawing.Point(2, 2);
            this.grpbRecogRate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpbRecogRate.Name = "grpbRecogRate";
            this.grpbRecogRate.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpbRecogRate.Size = new System.Drawing.Size(671, 163);
            this.grpbRecogRate.TabIndex = 0;
            this.grpbRecogRate.TabStop = false;
            this.grpbRecogRate.Text = "识别率";
            // 
            // lblToMaxNum
            // 
            this.lblToMaxNum.AutoSize = true;
            this.lblToMaxNum.Location = new System.Drawing.Point(244, 134);
            this.lblToMaxNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblToMaxNum.Name = "lblToMaxNum";
            this.lblToMaxNum.Size = new System.Drawing.Size(149, 12);
            this.lblToMaxNum.TabIndex = 2;
            this.lblToMaxNum.Text = "100.00 % (1 of 1  model)";
            // 
            // lblToSpecifiedNum
            // 
            this.lblToSpecifiedNum.AutoSize = true;
            this.lblToSpecifiedNum.Location = new System.Drawing.Point(244, 110);
            this.lblToSpecifiedNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblToSpecifiedNum.Name = "lblToSpecifiedNum";
            this.lblToSpecifiedNum.Size = new System.Drawing.Size(149, 12);
            this.lblToSpecifiedNum.TabIndex = 2;
            this.lblToSpecifiedNum.Text = "100.00 % (1 of 1  model)";
            // 
            // lblMaxNum
            // 
            this.lblMaxNum.AutoSize = true;
            this.lblMaxNum.Location = new System.Drawing.Point(244, 78);
            this.lblMaxNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaxNum.Name = "lblMaxNum";
            this.lblMaxNum.Size = new System.Drawing.Size(149, 12);
            this.lblMaxNum.TabIndex = 2;
            this.lblMaxNum.Text = "100.00 % (1 of 1  image)";
            // 
            // lblSpecifiedNum
            // 
            this.lblSpecifiedNum.AutoSize = true;
            this.lblSpecifiedNum.Location = new System.Drawing.Point(244, 50);
            this.lblSpecifiedNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSpecifiedNum.Name = "lblSpecifiedNum";
            this.lblSpecifiedNum.Size = new System.Drawing.Size(149, 12);
            this.lblSpecifiedNum.TabIndex = 2;
            this.lblSpecifiedNum.Text = "100.00 % (1 of 1  image)";
            // 
            // lblAtleastOne
            // 
            this.lblAtleastOne.AutoSize = true;
            this.lblAtleastOne.Location = new System.Drawing.Point(244, 23);
            this.lblAtleastOne.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAtleastOne.Name = "lblAtleastOne";
            this.lblAtleastOne.Size = new System.Drawing.Size(149, 12);
            this.lblAtleastOne.TabIndex = 2;
            this.lblAtleastOne.Text = "100.00 % (1 of 1  image)";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(25, 110);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(29, 12);
            this.label29.TabIndex = 1;
            this.label29.Text = "匹配";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(93, 134);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(149, 12);
            this.label34.TabIndex = 1;
            this.label34.Text = "相对最大数目的匹配的比例";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(93, 110);
            this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(149, 12);
            this.label33.TabIndex = 1;
            this.label33.Text = "相对指定数目的匹配的比例";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(93, 78);
            this.label32.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(149, 12);
            this.label32.TabIndex = 1;
            this.label32.Text = "尽可能多的匹配模板的实例";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(93, 50);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(149, 12);
            this.label31.TabIndex = 1;
            this.label31.Text = "指定数目的匹配模板的实例";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(93, 23);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(149, 12);
            this.label30.TabIndex = 1;
            this.label30.Text = "至少有一个匹配模板的实例";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(25, 23);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(41, 12);
            this.label28.TabIndex = 1;
            this.label28.Text = "图像：";
            // 
            // FrmMatchModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 603);
            this.Controls.Add(this.splitContainerMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmMatchModel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模板匹配模型";
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerModel.Panel1.ResumeLayout(false);
            this.splitContainerModel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerModel)).EndInit();
            this.splitContainerModel.ResumeLayout(false);
            this.grpOperation.ResumeLayout(false);
            this.pRegionFunction.ResumeLayout(false);
            this.pRegionFunction.PerformLayout();
            this.pModelType.ResumeLayout(false);
            this.pModelType.PerformLayout();
            this.grpbShapeOption.ResumeLayout(false);
            this.grpbShapeOption.PerformLayout();
            this.grpbViewOption.ResumeLayout(false);
            this.grpbViewOption.PerformLayout();
            this.grpbBrushOption.ResumeLayout(false);
            this.grpbBrushOption.PerformLayout();
            this.splitContainerView.Panel1.ResumeLayout(false);
            this.splitContainerView.Panel1.PerformLayout();
            this.splitContainerView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerView)).EndInit();
            this.splitContainerView.ResumeLayout(false);
            this.tabControlModel.ResumeLayout(false);
            this.tbpCreateModelPara.ResumeLayout(false);
            this.pnlCreateModelPara.ResumeLayout(false);
            this.pnlCreateModelPara.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkbMinContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbStartAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbScaleStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbNumLevels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbMaxScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbAngleStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbMinScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbAngleExtent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbDisplayLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnMinContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnAngleStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnAngleExtent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnMaxScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnStartAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnNumLevels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnMinScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnScaleStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnDisplayLevel)).EndInit();
            this.tbpFindModelPara.ResumeLayout(false);
            this.pnlFindModelPara.ResumeLayout(false);
            this.pnlFindModelPara.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkbLastLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnLastLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbMaxOverlap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnMaxOverlap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbGreediness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnGreediness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbNumToMatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnNumToMatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbMinScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnMinScore)).EndInit();
            this.tbpInspectModel.ResumeLayout(false);
            this.splitContainerInspect.Panel1.ResumeLayout(false);
            this.splitContainerInspect.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInspect)).EndInit();
            this.splitContainerInspect.ResumeLayout(false);
            this.pnlOperation.ResumeLayout(false);
            this.pnlOperation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatchResult)).EndInit();
            this.tbpOptimize.ResumeLayout(false);
            this.pnlOptimization.ResumeLayout(false);
            this.pnlOptimization.PerformLayout();
            this.grpbMatchModel.ResumeLayout(false);
            this.grpbMatchModel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkbRecogRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnRecogRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnSpecifiedNum)).EndInit();
            this.tbpStatistic.ResumeLayout(false);
            this.grpbStatistic.ResumeLayout(false);
            this.grpbStatistic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnMaxNumMatch)).EndInit();
            this.grpbRecogRate.ResumeLayout(false);
            this.grpbRecogRate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        protected internal System.Windows.Forms.Button btnCreateModel;
        protected internal System.Windows.Forms.Button btnSaveModel;
        protected internal System.Windows.Forms.Button btnLoadModel;
        protected internal System.Windows.Forms.Button btnLoadImage;
        protected internal System.Windows.Forms.RadioButton rdbtnContourShape;
        protected internal System.Windows.Forms.RadioButton rdbtnRegionShape;
        protected internal System.Windows.Forms.RadioButton rdbtnAnnulus;
        protected internal System.Windows.Forms.RadioButton rdbtnCircle;
        protected internal System.Windows.Forms.RadioButton rdbtnRectangle2;
        protected internal System.Windows.Forms.RadioButton rdbtnCircleArc;
        protected internal System.Windows.Forms.RadioButton rdbtnRectangle1;
        protected internal System.Windows.Forms.RadioButton rdbtnLine;
        private System.Windows.Forms.GroupBox grpbShapeOption;
        private System.Windows.Forms.GroupBox grpbBrushOption;
        private System.Windows.Forms.CheckBox chkbFillErase;
        protected internal System.Windows.Forms.GroupBox grpbViewOption;
        protected internal System.Windows.Forms.RadioButton rdbtnMagnifyImage;
        protected internal System.Windows.Forms.RadioButton rdbtnZoomImage;
        protected internal System.Windows.Forms.RadioButton rdbtnMoveImage;
        protected internal System.Windows.Forms.RadioButton rdbtnNone;
        private System.Windows.Forms.SplitContainer splitContainerView;
        protected internal System.Windows.Forms.Label lblCoordinateGrayValue;
        protected internal HalconDotNet.HWindowControl hWindowControl;
        private System.Windows.Forms.TabControl tabControlModel;
        protected internal System.Windows.Forms.TabPage tbpCreateModelPara;
        private System.Windows.Forms.Panel pnlCreateModelPara;
        protected internal System.Windows.Forms.Label label12;
        protected internal System.Windows.Forms.Label label11;
        protected internal System.Windows.Forms.Label label8;
        protected internal System.Windows.Forms.Label label7;
        protected internal System.Windows.Forms.Label label10;
        protected internal System.Windows.Forms.Label label4;
        protected internal System.Windows.Forms.Label label6;
        protected internal System.Windows.Forms.Label label9;
        protected internal System.Windows.Forms.Label label3;
        protected internal System.Windows.Forms.Label label5;
        protected internal System.Windows.Forms.Label label2;
        protected internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tbpFindModelPara;
        private System.Windows.Forms.TabPage tbpInspectModel;
        private System.Windows.Forms.TabPage tbpOptimize;
        private System.Windows.Forms.TabPage tbpStatistic;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnMinContrast;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnAngleStep;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnAngleExtent;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnMaxScale;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnStartAngle;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnNumLevels;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnMinScale;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnScaleStep;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnContrast;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnDisplayLevel;
        protected internal System.Windows.Forms.TrackBar trkbDisplayLevel;
        protected internal System.Windows.Forms.ComboBox cmbOptimization;
        protected internal System.Windows.Forms.ComboBox cmbMetric;
        protected internal System.Windows.Forms.TrackBar trkbMinContrast;
        protected internal System.Windows.Forms.TrackBar trkbStartAngle;
        protected internal System.Windows.Forms.TrackBar trkbScaleStep;
        protected internal System.Windows.Forms.TrackBar trkbNumLevels;
        protected internal System.Windows.Forms.TrackBar trkbMaxScale;
        protected internal System.Windows.Forms.TrackBar trkbAngleStep;
        protected internal System.Windows.Forms.TrackBar trkbMinScale;
        protected internal System.Windows.Forms.TrackBar trkbAngleExtent;
        protected internal System.Windows.Forms.TrackBar trkbContrast;
        protected internal System.Windows.Forms.Panel pnlFindModelPara;
        protected internal System.Windows.Forms.ComboBox cmbSubPixel;
        protected internal System.Windows.Forms.Label label18;
        protected internal System.Windows.Forms.TrackBar trkbLastLevel;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnLastLevel;
        protected internal System.Windows.Forms.TrackBar trkbMaxOverlap;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnMaxOverlap;
        protected internal System.Windows.Forms.TrackBar trkbGreediness;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnGreediness;
        protected internal System.Windows.Forms.Label label17;
        protected internal System.Windows.Forms.TrackBar trkbNumToMatch;
        protected internal System.Windows.Forms.Label label16;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnNumToMatch;
        protected internal System.Windows.Forms.Label label15;
        protected internal System.Windows.Forms.TrackBar trkbMinScore;
        protected internal System.Windows.Forms.Label label14;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnMinScore;
        protected internal System.Windows.Forms.Label label13;
        private System.Windows.Forms.SplitContainer splitContainerInspect;
        protected internal System.Windows.Forms.Panel pnlOperation;
        protected internal System.Windows.Forms.ListBox lstbTestImages;
        private System.Windows.Forms.DataGridView dgvMatchResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColScore;
        private System.Windows.Forms.CheckBox chkbAlwaysFind;
        private System.Windows.Forms.Button btnFindModel;
        private System.Windows.Forms.Button btnDisplayImage;
        private System.Windows.Forms.Button btnClearImageList;
        private System.Windows.Forms.Button btnDeleteImage;
        private System.Windows.Forms.Button btnLoadImageList;
        protected internal System.Windows.Forms.Panel pnlOptimization;
        private System.Windows.Forms.GroupBox grpbMatchModel;
        protected internal System.Windows.Forms.RadioButton rdbtnMaxNum;
        protected internal System.Windows.Forms.RadioButton rdbtnAtLeastOne;
        protected internal System.Windows.Forms.RadioButton rdbtnSpecifiedNum;
        protected internal System.Windows.Forms.Label label20;
        protected internal System.Windows.Forms.Label label19;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnSpecifiedNum;
        private System.Windows.Forms.TrackBar trkbRecogRate;
        protected internal System.Windows.Forms.ComboBox cmbRecogRateOption;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnRecogRate;
        protected internal System.Windows.Forms.Label lblOptimizationStatus;
        protected internal System.Windows.Forms.Label label23;
        protected internal System.Windows.Forms.Label label22;
        protected internal System.Windows.Forms.Label label27;
        protected internal System.Windows.Forms.Label label26;
        protected internal System.Windows.Forms.Label label25;
        protected internal System.Windows.Forms.Label label24;
        protected internal System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnOptimize;
        protected internal System.Windows.Forms.Label lblOptElapse;
        protected internal System.Windows.Forms.Label lblLastElapse;
        protected internal System.Windows.Forms.Label lblOptRecogRate;
        protected internal System.Windows.Forms.Label lblLastRecogRate;
        protected internal System.Windows.Forms.Label lblOptGreediness;
        protected internal System.Windows.Forms.Label lblLastGreediness;
        protected internal System.Windows.Forms.Label lblOptMinScore;
        protected internal System.Windows.Forms.Label lblLastMinScore;
        protected internal System.Windows.Forms.GroupBox grpbRecogRate;
        protected internal System.Windows.Forms.Label label29;
        protected internal System.Windows.Forms.Label label28;
        protected internal System.Windows.Forms.GroupBox grpbStatistic;
        protected internal System.Windows.Forms.Label lblToMaxNum;
        protected internal System.Windows.Forms.Label lblToSpecifiedNum;
        protected internal System.Windows.Forms.Label lblMaxNum;
        protected internal System.Windows.Forms.Label lblSpecifiedNum;
        protected internal System.Windows.Forms.Label lblAtleastOne;
        protected internal System.Windows.Forms.Label label34;
        protected internal System.Windows.Forms.Label label33;
        protected internal System.Windows.Forms.Label label32;
        protected internal System.Windows.Forms.Label label31;
        protected internal System.Windows.Forms.Label label30;
        protected internal System.Windows.Forms.Label label38;
        protected internal System.Windows.Forms.Label label37;
        protected internal System.Windows.Forms.Label label36;
        protected internal System.Windows.Forms.Label label47;
        protected internal System.Windows.Forms.Label label46;
        protected internal System.Windows.Forms.Label label44;
        protected internal System.Windows.Forms.Label label43;
        protected internal System.Windows.Forms.Label label45;
        protected internal System.Windows.Forms.Label label41;
        protected internal System.Windows.Forms.Label label42;
        protected internal System.Windows.Forms.Label label40;
        protected internal System.Windows.Forms.Label label39;
        protected internal System.Windows.Forms.Label label35;
        private System.Windows.Forms.Button btnStatistic;
        protected internal System.Windows.Forms.NumericUpDown numUpDwnMaxNumMatch;
        protected internal System.Windows.Forms.Label lblInspectRangeColScale;
        protected internal System.Windows.Forms.Label lblInspectRangeRowScale;
        protected internal System.Windows.Forms.Label lblInspectMaxColScale;
        protected internal System.Windows.Forms.Label lblInspectMaxRowScale;
        protected internal System.Windows.Forms.Label lblInspectRangeAngle;
        protected internal System.Windows.Forms.Label lblInspectMinColScale;
        protected internal System.Windows.Forms.Label lblInspectMaxAngle;
        protected internal System.Windows.Forms.Label lblInspectRangeCol;
        protected internal System.Windows.Forms.Label lblInspectMinRowScale;
        protected internal System.Windows.Forms.Label lblInspectMaxCol;
        protected internal System.Windows.Forms.Label lblInspectRangeRow;
        protected internal System.Windows.Forms.Label lblInspectMinAngle;
        protected internal System.Windows.Forms.Label lblInspectMaxRow;
        protected internal System.Windows.Forms.Label lblInspectMinCol;
        protected internal System.Windows.Forms.Label lblInspectRangeElapse;
        protected internal System.Windows.Forms.Label lblInspectMinRow;
        protected internal System.Windows.Forms.Label lblInspectMaxElapse;
        protected internal System.Windows.Forms.Label lblInspectRangeScore;
        protected internal System.Windows.Forms.Label lblInspectMaxScore;
        protected internal System.Windows.Forms.Label lblInspectMinElapse;
        protected internal System.Windows.Forms.Label lblInspectMinScore;
        protected internal System.Windows.Forms.Button btnClearROI;
        private System.Windows.Forms.CheckBox chkbBrushShape;
        private System.Windows.Forms.CheckBox chkbBrushSize;
        protected internal System.Windows.Forms.RadioButton rdbtnFreeDraw;
        private System.Windows.Forms.CheckBox chkbMinContrast;
        private System.Windows.Forms.CheckBox chkbOption;
        private System.Windows.Forms.CheckBox chkbNumLevel;
        private System.Windows.Forms.CheckBox chkbAngleStep;
        private System.Windows.Forms.CheckBox chkbScaleStep;
        private System.Windows.Forms.CheckBox chkbContrast;
        private System.Windows.Forms.CheckBox chkbBrushOnOff;
        protected internal System.Windows.Forms.ComboBox cmbOpLevel;
        protected internal System.Windows.Forms.RadioButton rdbtnModelExtract;
        protected internal System.Windows.Forms.RadioButton rdbtnModelSearch;
        private System.Windows.Forms.Panel pRegionFunction;
        private System.Windows.Forms.Panel pModelType;
        protected internal System.Windows.Forms.GroupBox grpOperation;
        protected internal System.Windows.Forms.SplitContainer splitContainerModel;
    }
}