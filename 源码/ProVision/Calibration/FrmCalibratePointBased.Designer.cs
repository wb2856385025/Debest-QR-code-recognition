namespace ProVision.Calibration
{
    partial class FrmCalibratePointBased
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCalibratePointBased));
            this.tblpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.hWndCtrlDisplay = new HalconDotNet.HWindowControl();
            this.dtgrdvPointPairList = new System.Windows.Forms.DataGridView();
            this.ColNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAxis1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAxis2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pFeaturePointAndCalibrateResult = new System.Windows.Forms.Panel();
            this.grpbCalibrateResult = new System.Windows.Forms.GroupBox();
            this.tblpCalibrateResult = new System.Windows.Forms.TableLayoutPanel();
            this.lblCalibratePixelError = new System.Windows.Forms.Label();
            this.txtbHorizontalScale = new System.Windows.Forms.TextBox();
            this.lblHorizontalScale = new System.Windows.Forms.Label();
            this.lblRotateAngle = new System.Windows.Forms.Label();
            this.lblHorizontalTranslate = new System.Windows.Forms.Label();
            this.lblVerticalScale = new System.Windows.Forms.Label();
            this.lblChamferAngle = new System.Windows.Forms.Label();
            this.lblVerticalTranslate = new System.Windows.Forms.Label();
            this.txtbRotateAngle = new System.Windows.Forms.TextBox();
            this.txtbHorizontalTranslate = new System.Windows.Forms.TextBox();
            this.txtbVerticalTranslate = new System.Windows.Forms.TextBox();
            this.txtbChamferAngle = new System.Windows.Forms.TextBox();
            this.txtbVerticalScale = new System.Windows.Forms.TextBox();
            this.lblPhysicalError = new System.Windows.Forms.Label();
            this.txtbPixelError = new System.Windows.Forms.TextBox();
            this.txtbPhysicalError = new System.Windows.Forms.TextBox();
            this.grpbFeaturePoint = new System.Windows.Forms.GroupBox();
            this.chkbEnableVerifyCross = new System.Windows.Forms.CheckBox();
            this.chkbAcquisitionMode = new System.Windows.Forms.CheckBox();
            this.nmupdwnCrossCol = new System.Windows.Forms.NumericUpDown();
            this.nmupdwnCrossRow = new System.Windows.Forms.NumericUpDown();
            this.nmupdwnAxis2MoveStep = new System.Windows.Forms.NumericUpDown();
            this.lblVerifyCrossCol = new System.Windows.Forms.Label();
            this.nmupdwnAxis1MoveStep = new System.Windows.Forms.NumericUpDown();
            this.lblVerifyCrossRow = new System.Windows.Forms.Label();
            this.lblAxis2MoveStep = new System.Windows.Forms.Label();
            this.lblAxis1MoveStep = new System.Windows.Forms.Label();
            this.rdbtnMobileFeaturePoint = new System.Windows.Forms.RadioButton();
            this.rdbtnFixedFeaturePoint = new System.Windows.Forms.RadioButton();
            this.pCalibrateOperation = new System.Windows.Forms.Panel();
            this.btnExitCalibration = new System.Windows.Forms.Button();
            this.btnAddCalibrationSolution = new System.Windows.Forms.Button();
            this.btnVerifyCalibration = new System.Windows.Forms.Button();
            this.btnMatchLocation = new System.Windows.Forms.Button();
            this.btnCalculateCalibration = new System.Windows.Forms.Button();
            this.btnSetMatchModel = new System.Windows.Forms.Button();
            this.btnAcquireImage = new System.Windows.Forms.Button();
            this.ststrpOperation = new System.Windows.Forms.StatusStrip();
            this.tlstrpstlblOperation = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnToolOffset = new System.Windows.Forms.Button();
            this.tblpRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrdvPointPairList)).BeginInit();
            this.pFeaturePointAndCalibrateResult.SuspendLayout();
            this.grpbCalibrateResult.SuspendLayout();
            this.tblpCalibrateResult.SuspendLayout();
            this.grpbFeaturePoint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmupdwnCrossCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmupdwnCrossRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmupdwnAxis2MoveStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmupdwnAxis1MoveStep)).BeginInit();
            this.pCalibrateOperation.SuspendLayout();
            this.ststrpOperation.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblpRoot
            // 
            resources.ApplyResources(this.tblpRoot, "tblpRoot");
            this.tblpRoot.Controls.Add(this.hWndCtrlDisplay, 0, 0);
            this.tblpRoot.Controls.Add(this.dtgrdvPointPairList, 0, 1);
            this.tblpRoot.Controls.Add(this.pFeaturePointAndCalibrateResult, 1, 0);
            this.tblpRoot.Controls.Add(this.pCalibrateOperation, 1, 1);
            this.tblpRoot.Name = "tblpRoot";
            // 
            // hWndCtrlDisplay
            // 
            this.hWndCtrlDisplay.BackColor = System.Drawing.Color.Salmon;
            this.hWndCtrlDisplay.BorderColor = System.Drawing.Color.Salmon;
            resources.ApplyResources(this.hWndCtrlDisplay, "hWndCtrlDisplay");
            this.hWndCtrlDisplay.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWndCtrlDisplay.Name = "hWndCtrlDisplay";
            this.hWndCtrlDisplay.WindowSize = new System.Drawing.Size(535, 329);
            // 
            // dtgrdvPointPairList
            // 
            this.dtgrdvPointPairList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgrdvPointPairList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColNo,
            this.ColRow,
            this.ColCol,
            this.ColAxis1,
            this.ColAxis2});
            resources.ApplyResources(this.dtgrdvPointPairList, "dtgrdvPointPairList");
            this.dtgrdvPointPairList.Name = "dtgrdvPointPairList";
            this.dtgrdvPointPairList.RowTemplate.Height = 27;
            // 
            // ColNo
            // 
            resources.ApplyResources(this.ColNo, "ColNo");
            this.ColNo.Name = "ColNo";
            this.ColNo.ReadOnly = true;
            // 
            // ColRow
            // 
            resources.ApplyResources(this.ColRow, "ColRow");
            this.ColRow.Name = "ColRow";
            this.ColRow.ReadOnly = true;
            // 
            // ColCol
            // 
            resources.ApplyResources(this.ColCol, "ColCol");
            this.ColCol.Name = "ColCol";
            this.ColCol.ReadOnly = true;
            // 
            // ColAxis1
            // 
            resources.ApplyResources(this.ColAxis1, "ColAxis1");
            this.ColAxis1.Name = "ColAxis1";
            // 
            // ColAxis2
            // 
            resources.ApplyResources(this.ColAxis2, "ColAxis2");
            this.ColAxis2.Name = "ColAxis2";
            // 
            // pFeaturePointAndCalibrateResult
            // 
            this.pFeaturePointAndCalibrateResult.Controls.Add(this.grpbCalibrateResult);
            this.pFeaturePointAndCalibrateResult.Controls.Add(this.grpbFeaturePoint);
            resources.ApplyResources(this.pFeaturePointAndCalibrateResult, "pFeaturePointAndCalibrateResult");
            this.pFeaturePointAndCalibrateResult.Name = "pFeaturePointAndCalibrateResult";
            // 
            // grpbCalibrateResult
            // 
            this.grpbCalibrateResult.Controls.Add(this.tblpCalibrateResult);
            resources.ApplyResources(this.grpbCalibrateResult, "grpbCalibrateResult");
            this.grpbCalibrateResult.Name = "grpbCalibrateResult";
            this.grpbCalibrateResult.TabStop = false;
            this.grpbCalibrateResult.Tag = "LBL_CALIBRATERESULT";
            // 
            // tblpCalibrateResult
            // 
            resources.ApplyResources(this.tblpCalibrateResult, "tblpCalibrateResult");
            this.tblpCalibrateResult.Controls.Add(this.lblCalibratePixelError, 0, 3);
            this.tblpCalibrateResult.Controls.Add(this.txtbHorizontalScale, 1, 0);
            this.tblpCalibrateResult.Controls.Add(this.lblHorizontalScale, 0, 0);
            this.tblpCalibrateResult.Controls.Add(this.lblRotateAngle, 0, 1);
            this.tblpCalibrateResult.Controls.Add(this.lblHorizontalTranslate, 0, 2);
            this.tblpCalibrateResult.Controls.Add(this.lblVerticalScale, 2, 0);
            this.tblpCalibrateResult.Controls.Add(this.lblChamferAngle, 2, 1);
            this.tblpCalibrateResult.Controls.Add(this.lblVerticalTranslate, 2, 2);
            this.tblpCalibrateResult.Controls.Add(this.txtbRotateAngle, 1, 1);
            this.tblpCalibrateResult.Controls.Add(this.txtbHorizontalTranslate, 1, 2);
            this.tblpCalibrateResult.Controls.Add(this.txtbVerticalTranslate, 3, 2);
            this.tblpCalibrateResult.Controls.Add(this.txtbChamferAngle, 3, 1);
            this.tblpCalibrateResult.Controls.Add(this.txtbVerticalScale, 3, 0);
            this.tblpCalibrateResult.Controls.Add(this.lblPhysicalError, 2, 3);
            this.tblpCalibrateResult.Controls.Add(this.txtbPixelError, 1, 3);
            this.tblpCalibrateResult.Controls.Add(this.txtbPhysicalError, 3, 3);
            this.tblpCalibrateResult.Name = "tblpCalibrateResult";
            // 
            // lblCalibratePixelError
            // 
            resources.ApplyResources(this.lblCalibratePixelError, "lblCalibratePixelError");
            this.lblCalibratePixelError.Name = "lblCalibratePixelError";
            this.lblCalibratePixelError.Tag = "LBL_CALIBRATEPIXELERROR";
            // 
            // txtbHorizontalScale
            // 
            resources.ApplyResources(this.txtbHorizontalScale, "txtbHorizontalScale");
            this.txtbHorizontalScale.Name = "txtbHorizontalScale";
            this.txtbHorizontalScale.ReadOnly = true;
            // 
            // lblHorizontalScale
            // 
            resources.ApplyResources(this.lblHorizontalScale, "lblHorizontalScale");
            this.lblHorizontalScale.Name = "lblHorizontalScale";
            this.lblHorizontalScale.Tag = "LBL_SCALEX";
            // 
            // lblRotateAngle
            // 
            resources.ApplyResources(this.lblRotateAngle, "lblRotateAngle");
            this.lblRotateAngle.Name = "lblRotateAngle";
            this.lblRotateAngle.Tag = "LBL_ROTATEANGLE";
            // 
            // lblHorizontalTranslate
            // 
            resources.ApplyResources(this.lblHorizontalTranslate, "lblHorizontalTranslate");
            this.lblHorizontalTranslate.Name = "lblHorizontalTranslate";
            this.lblHorizontalTranslate.Tag = "LBL_TRANSLATEX";
            // 
            // lblVerticalScale
            // 
            resources.ApplyResources(this.lblVerticalScale, "lblVerticalScale");
            this.lblVerticalScale.Name = "lblVerticalScale";
            this.lblVerticalScale.Tag = "LBL_SCALEY";
            // 
            // lblChamferAngle
            // 
            resources.ApplyResources(this.lblChamferAngle, "lblChamferAngle");
            this.lblChamferAngle.Name = "lblChamferAngle";
            this.lblChamferAngle.Tag = "LBL_CHAMFERANGLE";
            // 
            // lblVerticalTranslate
            // 
            resources.ApplyResources(this.lblVerticalTranslate, "lblVerticalTranslate");
            this.lblVerticalTranslate.Name = "lblVerticalTranslate";
            this.lblVerticalTranslate.Tag = "LBL_TRANSLATEY";
            // 
            // txtbRotateAngle
            // 
            resources.ApplyResources(this.txtbRotateAngle, "txtbRotateAngle");
            this.txtbRotateAngle.Name = "txtbRotateAngle";
            this.txtbRotateAngle.ReadOnly = true;
            // 
            // txtbHorizontalTranslate
            // 
            resources.ApplyResources(this.txtbHorizontalTranslate, "txtbHorizontalTranslate");
            this.txtbHorizontalTranslate.Name = "txtbHorizontalTranslate";
            this.txtbHorizontalTranslate.ReadOnly = true;
            // 
            // txtbVerticalTranslate
            // 
            resources.ApplyResources(this.txtbVerticalTranslate, "txtbVerticalTranslate");
            this.txtbVerticalTranslate.Name = "txtbVerticalTranslate";
            this.txtbVerticalTranslate.ReadOnly = true;
            // 
            // txtbChamferAngle
            // 
            resources.ApplyResources(this.txtbChamferAngle, "txtbChamferAngle");
            this.txtbChamferAngle.Name = "txtbChamferAngle";
            this.txtbChamferAngle.ReadOnly = true;
            // 
            // txtbVerticalScale
            // 
            resources.ApplyResources(this.txtbVerticalScale, "txtbVerticalScale");
            this.txtbVerticalScale.Name = "txtbVerticalScale";
            this.txtbVerticalScale.ReadOnly = true;
            // 
            // lblPhysicalError
            // 
            resources.ApplyResources(this.lblPhysicalError, "lblPhysicalError");
            this.lblPhysicalError.Name = "lblPhysicalError";
            this.lblPhysicalError.Tag = "LBL_CALIBRATEPHYSICALERROR";
            // 
            // txtbPixelError
            // 
            resources.ApplyResources(this.txtbPixelError, "txtbPixelError");
            this.txtbPixelError.Name = "txtbPixelError";
            this.txtbPixelError.ReadOnly = true;
            // 
            // txtbPhysicalError
            // 
            resources.ApplyResources(this.txtbPhysicalError, "txtbPhysicalError");
            this.txtbPhysicalError.Name = "txtbPhysicalError";
            this.txtbPhysicalError.ReadOnly = true;
            // 
            // grpbFeaturePoint
            // 
            this.grpbFeaturePoint.Controls.Add(this.chkbEnableVerifyCross);
            this.grpbFeaturePoint.Controls.Add(this.chkbAcquisitionMode);
            this.grpbFeaturePoint.Controls.Add(this.nmupdwnCrossCol);
            this.grpbFeaturePoint.Controls.Add(this.nmupdwnCrossRow);
            this.grpbFeaturePoint.Controls.Add(this.nmupdwnAxis2MoveStep);
            this.grpbFeaturePoint.Controls.Add(this.lblVerifyCrossCol);
            this.grpbFeaturePoint.Controls.Add(this.nmupdwnAxis1MoveStep);
            this.grpbFeaturePoint.Controls.Add(this.lblVerifyCrossRow);
            this.grpbFeaturePoint.Controls.Add(this.lblAxis2MoveStep);
            this.grpbFeaturePoint.Controls.Add(this.lblAxis1MoveStep);
            this.grpbFeaturePoint.Controls.Add(this.rdbtnMobileFeaturePoint);
            this.grpbFeaturePoint.Controls.Add(this.rdbtnFixedFeaturePoint);
            resources.ApplyResources(this.grpbFeaturePoint, "grpbFeaturePoint");
            this.grpbFeaturePoint.Name = "grpbFeaturePoint";
            this.grpbFeaturePoint.TabStop = false;
            this.grpbFeaturePoint.Tag = "GRPB_FEATUREPOINTTYPE";
            // 
            // chkbEnableVerifyCross
            // 
            resources.ApplyResources(this.chkbEnableVerifyCross, "chkbEnableVerifyCross");
            this.chkbEnableVerifyCross.Name = "chkbEnableVerifyCross";
            this.chkbEnableVerifyCross.Tag = "CHKB_ENABLEVERIDYCROSS";
            this.chkbEnableVerifyCross.UseVisualStyleBackColor = true;
            // 
            // chkbAcquisitionMode
            // 
            resources.ApplyResources(this.chkbAcquisitionMode, "chkbAcquisitionMode");
            this.chkbAcquisitionMode.Name = "chkbAcquisitionMode";
            this.chkbAcquisitionMode.Tag = "CHKB_ACQUIRECONTINUE";
            this.chkbAcquisitionMode.UseVisualStyleBackColor = true;
            // 
            // nmupdwnCrossCol
            // 
            resources.ApplyResources(this.nmupdwnCrossCol, "nmupdwnCrossCol");
            this.nmupdwnCrossCol.Name = "nmupdwnCrossCol";
            // 
            // nmupdwnCrossRow
            // 
            resources.ApplyResources(this.nmupdwnCrossRow, "nmupdwnCrossRow");
            this.nmupdwnCrossRow.Name = "nmupdwnCrossRow";
            // 
            // nmupdwnAxis2MoveStep
            // 
            this.nmupdwnAxis2MoveStep.DecimalPlaces = 2;
            this.nmupdwnAxis2MoveStep.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            resources.ApplyResources(this.nmupdwnAxis2MoveStep, "nmupdwnAxis2MoveStep");
            this.nmupdwnAxis2MoveStep.Name = "nmupdwnAxis2MoveStep";
            // 
            // lblVerifyCrossCol
            // 
            resources.ApplyResources(this.lblVerifyCrossCol, "lblVerifyCrossCol");
            this.lblVerifyCrossCol.Name = "lblVerifyCrossCol";
            this.lblVerifyCrossCol.Tag = "LBL_VERIFYCROSSCOL";
            // 
            // nmupdwnAxis1MoveStep
            // 
            this.nmupdwnAxis1MoveStep.DecimalPlaces = 2;
            this.nmupdwnAxis1MoveStep.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            resources.ApplyResources(this.nmupdwnAxis1MoveStep, "nmupdwnAxis1MoveStep");
            this.nmupdwnAxis1MoveStep.Name = "nmupdwnAxis1MoveStep";
            // 
            // lblVerifyCrossRow
            // 
            resources.ApplyResources(this.lblVerifyCrossRow, "lblVerifyCrossRow");
            this.lblVerifyCrossRow.Name = "lblVerifyCrossRow";
            this.lblVerifyCrossRow.Tag = "LBL_VERIFYCROSSROW";
            // 
            // lblAxis2MoveStep
            // 
            resources.ApplyResources(this.lblAxis2MoveStep, "lblAxis2MoveStep");
            this.lblAxis2MoveStep.Name = "lblAxis2MoveStep";
            this.lblAxis2MoveStep.Tag = "LBL_AXIS2STEP";
            // 
            // lblAxis1MoveStep
            // 
            resources.ApplyResources(this.lblAxis1MoveStep, "lblAxis1MoveStep");
            this.lblAxis1MoveStep.Name = "lblAxis1MoveStep";
            this.lblAxis1MoveStep.Tag = "LBL_AXIS1STEP";
            // 
            // rdbtnMobileFeaturePoint
            // 
            resources.ApplyResources(this.rdbtnMobileFeaturePoint, "rdbtnMobileFeaturePoint");
            this.rdbtnMobileFeaturePoint.Name = "rdbtnMobileFeaturePoint";
            this.rdbtnMobileFeaturePoint.TabStop = true;
            this.rdbtnMobileFeaturePoint.Tag = "RDBTN_MOBILEPOINT";
            this.rdbtnMobileFeaturePoint.UseVisualStyleBackColor = true;
            // 
            // rdbtnFixedFeaturePoint
            // 
            resources.ApplyResources(this.rdbtnFixedFeaturePoint, "rdbtnFixedFeaturePoint");
            this.rdbtnFixedFeaturePoint.Name = "rdbtnFixedFeaturePoint";
            this.rdbtnFixedFeaturePoint.TabStop = true;
            this.rdbtnFixedFeaturePoint.Tag = "RDBTN_FIXEDPOINT";
            this.rdbtnFixedFeaturePoint.UseVisualStyleBackColor = true;
            // 
            // pCalibrateOperation
            // 
            this.pCalibrateOperation.Controls.Add(this.btnToolOffset);
            this.pCalibrateOperation.Controls.Add(this.btnExitCalibration);
            this.pCalibrateOperation.Controls.Add(this.btnAddCalibrationSolution);
            this.pCalibrateOperation.Controls.Add(this.btnVerifyCalibration);
            this.pCalibrateOperation.Controls.Add(this.btnMatchLocation);
            this.pCalibrateOperation.Controls.Add(this.btnCalculateCalibration);
            this.pCalibrateOperation.Controls.Add(this.btnSetMatchModel);
            this.pCalibrateOperation.Controls.Add(this.btnAcquireImage);
            resources.ApplyResources(this.pCalibrateOperation, "pCalibrateOperation");
            this.pCalibrateOperation.Name = "pCalibrateOperation";
            // 
            // btnExitCalibration
            // 
            resources.ApplyResources(this.btnExitCalibration, "btnExitCalibration");
            this.btnExitCalibration.Name = "btnExitCalibration";
            this.btnExitCalibration.Tag = "BTN_EXITCALIBRATION";
            this.btnExitCalibration.UseVisualStyleBackColor = true;
            // 
            // btnAddCalibrationSolution
            // 
            resources.ApplyResources(this.btnAddCalibrationSolution, "btnAddCalibrationSolution");
            this.btnAddCalibrationSolution.Name = "btnAddCalibrationSolution";
            this.btnAddCalibrationSolution.Tag = "BTN_ADDCALIBRATIONSOLUTION";
            this.btnAddCalibrationSolution.UseVisualStyleBackColor = true;
            // 
            // btnVerifyCalibration
            // 
            resources.ApplyResources(this.btnVerifyCalibration, "btnVerifyCalibration");
            this.btnVerifyCalibration.Name = "btnVerifyCalibration";
            this.btnVerifyCalibration.Tag = "BTN_VARIFYCALIBRATION";
            this.btnVerifyCalibration.UseVisualStyleBackColor = true;
            // 
            // btnMatchLocation
            // 
            resources.ApplyResources(this.btnMatchLocation, "btnMatchLocation");
            this.btnMatchLocation.Name = "btnMatchLocation";
            this.btnMatchLocation.Tag = "BTN_MATCHLOCATION";
            this.btnMatchLocation.UseVisualStyleBackColor = true;
            // 
            // btnCalculateCalibration
            // 
            resources.ApplyResources(this.btnCalculateCalibration, "btnCalculateCalibration");
            this.btnCalculateCalibration.Name = "btnCalculateCalibration";
            this.btnCalculateCalibration.Tag = "BTN_CALCULATECALIBRATION";
            this.btnCalculateCalibration.UseVisualStyleBackColor = true;
            // 
            // btnSetMatchModel
            // 
            resources.ApplyResources(this.btnSetMatchModel, "btnSetMatchModel");
            this.btnSetMatchModel.Name = "btnSetMatchModel";
            this.btnSetMatchModel.Tag = "BTN_SETMATCHMODEL";
            this.btnSetMatchModel.UseVisualStyleBackColor = true;
            // 
            // btnAcquireImage
            // 
            resources.ApplyResources(this.btnAcquireImage, "btnAcquireImage");
            this.btnAcquireImage.Name = "btnAcquireImage";
            this.btnAcquireImage.Tag = "BTN_ACQUIREIMAGE";
            this.btnAcquireImage.UseVisualStyleBackColor = true;
            // 
            // ststrpOperation
            // 
            this.ststrpOperation.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ststrpOperation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlstrpstlblOperation});
            resources.ApplyResources(this.ststrpOperation, "ststrpOperation");
            this.ststrpOperation.Name = "ststrpOperation";
            // 
            // tlstrpstlblOperation
            // 
            this.tlstrpstlblOperation.Name = "tlstrpstlblOperation";
            resources.ApplyResources(this.tlstrpstlblOperation, "tlstrpstlblOperation");
            // 
            // btnToolOffset
            // 
            resources.ApplyResources(this.btnToolOffset, "btnToolOffset");
            this.btnToolOffset.Name = "btnToolOffset";
            this.btnToolOffset.Tag = "BTN_TOOLOFFSET";
            this.btnToolOffset.UseVisualStyleBackColor = true;
            // 
            // FrmCalibratePointBased
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ststrpOperation);
            this.Controls.Add(this.tblpRoot);
            this.Name = "FrmCalibratePointBased";
            this.Tag = "FRM_CALIBRATEPOINTBASED";
            this.tblpRoot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgrdvPointPairList)).EndInit();
            this.pFeaturePointAndCalibrateResult.ResumeLayout(false);
            this.grpbCalibrateResult.ResumeLayout(false);
            this.tblpCalibrateResult.ResumeLayout(false);
            this.tblpCalibrateResult.PerformLayout();
            this.grpbFeaturePoint.ResumeLayout(false);
            this.grpbFeaturePoint.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmupdwnCrossCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmupdwnCrossRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmupdwnAxis2MoveStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmupdwnAxis1MoveStep)).EndInit();
            this.pCalibrateOperation.ResumeLayout(false);
            this.ststrpOperation.ResumeLayout(false);
            this.ststrpOperation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected internal System.Windows.Forms.TableLayoutPanel tblpRoot;
        protected internal System.Windows.Forms.DataGridView dtgrdvPointPairList;
        protected internal System.Windows.Forms.GroupBox grpbFeaturePoint;
        protected internal System.Windows.Forms.TableLayoutPanel tblpCalibrateResult;
        protected internal System.Windows.Forms.Panel pFeaturePointAndCalibrateResult;
        protected internal System.Windows.Forms.GroupBox grpbCalibrateResult;
        protected internal System.Windows.Forms.RadioButton rdbtnFixedFeaturePoint;
        protected internal System.Windows.Forms.RadioButton rdbtnMobileFeaturePoint;
        protected internal System.Windows.Forms.Label lblAxis2MoveStep;
        protected internal System.Windows.Forms.Label lblAxis1MoveStep;
        protected internal System.Windows.Forms.NumericUpDown nmupdwnAxis2MoveStep;
        protected internal System.Windows.Forms.NumericUpDown nmupdwnAxis1MoveStep;
        protected internal System.Windows.Forms.Label lblHorizontalScale;
        protected internal System.Windows.Forms.Label lblVerticalScale;
        protected internal System.Windows.Forms.Label lblHorizontalTranslate;
        protected internal System.Windows.Forms.Label lblRotateAngle;
        protected internal System.Windows.Forms.Label lblChamferAngle;
        protected internal System.Windows.Forms.Label lblVerticalTranslate;
        protected internal System.Windows.Forms.TextBox txtbHorizontalScale;
        protected internal System.Windows.Forms.TextBox txtbRotateAngle;
        protected internal System.Windows.Forms.TextBox txtbHorizontalTranslate;
        protected internal System.Windows.Forms.TextBox txtbVerticalTranslate;
        protected internal System.Windows.Forms.TextBox txtbChamferAngle;
        protected internal System.Windows.Forms.TextBox txtbVerticalScale;
        protected internal System.Windows.Forms.Panel pCalibrateOperation;
        protected internal System.Windows.Forms.Button btnAcquireImage;
        protected internal System.Windows.Forms.Button btnMatchLocation;
        protected internal System.Windows.Forms.Button btnSetMatchModel;
        protected internal System.Windows.Forms.Button btnAddCalibrationSolution;
        protected internal System.Windows.Forms.Button btnVerifyCalibration;
        protected internal System.Windows.Forms.Button btnCalculateCalibration;
        protected internal System.Windows.Forms.Button btnExitCalibration;
        protected internal System.Windows.Forms.Label lblCalibratePixelError;
        protected internal System.Windows.Forms.Label lblPhysicalError;
        protected internal System.Windows.Forms.TextBox txtbPixelError;
        protected internal System.Windows.Forms.TextBox txtbPhysicalError;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAxis1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAxis2;
        protected internal System.Windows.Forms.StatusStrip ststrpOperation;
        protected internal System.Windows.Forms.ToolStripStatusLabel tlstrpstlblOperation;
        protected internal HalconDotNet.HWindowControl hWndCtrlDisplay;
        protected internal System.Windows.Forms.CheckBox chkbAcquisitionMode;
        protected internal System.Windows.Forms.CheckBox chkbEnableVerifyCross;
        protected internal System.Windows.Forms.NumericUpDown nmupdwnCrossCol;
        protected internal System.Windows.Forms.NumericUpDown nmupdwnCrossRow;
        protected internal System.Windows.Forms.Label lblVerifyCrossCol;
        protected internal System.Windows.Forms.Label lblVerifyCrossRow;
        protected internal System.Windows.Forms.Button btnToolOffset;
    }
}