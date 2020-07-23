namespace ProCommon.UserDefForm
{
    partial class FrmROIInteractive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmROIInteractive));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbiLoadImage = new DevExpress.XtraBars.BarButtonItem();
            this.bchkiNone = new DevExpress.XtraBars.BarCheckItem();
            this.bchkiMove = new DevExpress.XtraBars.BarCheckItem();
            this.bchkiZoom = new DevExpress.XtraBars.BarCheckItem();
            this.bchkiMagnify = new DevExpress.XtraBars.BarCheckItem();
            this.bbiLine = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRectangle1 = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRectangle2 = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCircle = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCircularArc = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAnnulus = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDeleteActiveROI = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDeleteAllROI = new DevExpress.XtraBars.BarButtonItem();
            this.bbiResetWindow = new DevExpress.XtraBars.BarButtonItem();
            this.rbpROIInteractive = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rbpgrpROIFile = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rbpgrpROIShape = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rbpgrpViewOption = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rbpgrpOperation = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.hWndCtrlDisplay = new HalconDotNet.HWindowControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.bbiLoadImage,
            this.bchkiNone,
            this.bchkiMove,
            this.bchkiZoom,
            this.bchkiMagnify,
            this.bbiLine,
            this.bbiRectangle1,
            this.bbiRectangle2,
            this.bbiCircle,
            this.bbiCircularArc,
            this.bbiAnnulus,
            this.bbiDeleteActiveROI,
            this.bbiDeleteAllROI,
            this.bbiResetWindow});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 21;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rbpROIInteractive});
            this.ribbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.MacOffice;
            this.ribbon.Size = new System.Drawing.Size(979, 166);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // bbiLoadImage
            // 
            this.bbiLoadImage.Caption = "加载图像";
            this.bbiLoadImage.Hint = "加载本地图像";
            this.bbiLoadImage.Id = 2;
            this.bbiLoadImage.ImageOptions.ImageUri.Uri = "Open;Size16x16;Office2013";
            this.bbiLoadImage.Name = "bbiLoadImage";
            this.bbiLoadImage.Tag = "BBI_LOADIMAGE";
            // 
            // bchkiNone
            // 
            this.bchkiNone.Caption = "无操作";
            this.bchkiNone.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.bchkiNone.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
            this.bchkiNone.GroupIndex = 1;
            this.bchkiNone.Id = 5;
            this.bchkiNone.Name = "bchkiNone";
            this.bchkiNone.Tag = "BCHKI_NONE";          
            // 
            // bchkiMove
            // 
            this.bchkiMove.Caption = "移动图像";
            this.bchkiMove.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.bchkiMove.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
            this.bchkiMove.GroupIndex = 1;
            this.bchkiMove.Id = 6;
            this.bchkiMove.Name = "bchkiMove";
            this.bchkiMove.Tag = "BCHKI_MOVE";          
            // 
            // bchkiZoom
            // 
            this.bchkiZoom.Caption = "缩放图像";
            this.bchkiZoom.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.bchkiZoom.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
            this.bchkiZoom.GroupIndex = 1;
            this.bchkiZoom.Id = 7;
            this.bchkiZoom.Name = "bchkiZoom";
            this.bchkiZoom.Tag = "BCHKI_ZOOM";          
            // 
            // bchkiMagnify
            // 
            this.bchkiMagnify.Caption = "局部放大";
            this.bchkiMagnify.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.bchkiMagnify.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
            this.bchkiMagnify.GroupIndex = 1;
            this.bchkiMagnify.Id = 8;
            this.bchkiMagnify.Name = "bchkiMagnify";
            this.bchkiMagnify.Tag = "BCHKI_MAGNIFY";        
            // 
            // bbiLine
            // 
            this.bbiLine.Caption = "矢量线段";
            this.bbiLine.Id = 9;
            this.bbiLine.Name = "bbiLine";
            this.bbiLine.Tag = "BBI_LINE";
            // 
            // bbiRectangle1
            // 
            this.bbiRectangle1.Caption = "齐轴矩形";
            this.bbiRectangle1.Id = 10;
            this.bbiRectangle1.Name = "bbiRectangle1";
            this.bbiRectangle1.Tag = "BBI_RECTANGLE1";
            // 
            // bbiRectangle2
            // 
            this.bbiRectangle2.Caption = "仿射矩形";
            this.bbiRectangle2.Id = 11;
            this.bbiRectangle2.Name = "bbiRectangle2";
            this.bbiRectangle2.Tag = "BBI_RECTANGLE2";
            // 
            // bbiCircle
            // 
            this.bbiCircle.Caption = "圆形";
            this.bbiCircle.Id = 12;
            this.bbiCircle.Name = "bbiCircle";
            this.bbiCircle.Tag = "BBI_CIRCLE";
            // 
            // bbiCircularArc
            // 
            this.bbiCircularArc.Caption = "有向圆弧";
            this.bbiCircularArc.Id = 13;
            this.bbiCircularArc.Name = "bbiCircularArc";
            this.bbiCircularArc.Tag = "BBI_CIRCULARARC";
            // 
            // bbiAnnulus
            // 
            this.bbiAnnulus.Caption = "圆环";
            this.bbiAnnulus.Id = 14;
            this.bbiAnnulus.Name = "bbiAnnulus";
            this.bbiAnnulus.Tag = "BBI_ANNULUS";
            // 
            // bbiDeleteActiveROI
            // 
            this.bbiDeleteActiveROI.Caption = "删除活动ROI";
            this.bbiDeleteActiveROI.Id = 15;
            this.bbiDeleteActiveROI.Name = "bbiDeleteActiveROI";
            this.bbiDeleteActiveROI.Tag = "BBI_DELETEACTIVE";
            // 
            // bbiDeleteAllROI
            // 
            this.bbiDeleteAllROI.Caption = "删除所有ROI";
            this.bbiDeleteAllROI.Id = 16;
            this.bbiDeleteAllROI.Name = "bbiDeleteAllROI";
            this.bbiDeleteAllROI.Tag = "BBI_DELETEALL";
            // 
            // bbiResetWindow
            // 
            this.bbiResetWindow.Caption = "窗口重置";
            this.bbiResetWindow.Id = 20;
            this.bbiResetWindow.Name = "bbiResetWindow";
            this.bbiResetWindow.Tag = "BBI_RESETWINDOW";
            // 
            // rbpROIInteractive
            // 
            this.rbpROIInteractive.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rbpgrpROIFile,
            this.rbpgrpROIShape,
            this.rbpgrpViewOption,
            this.rbpgrpOperation});
            this.rbpROIInteractive.Name = "rbpROIInteractive";
            this.rbpROIInteractive.Tag = "RBP_ROIINTERACTIVE";
            this.rbpROIInteractive.Text = "关注区域";
            // 
            // rbpgrpROIFile
            // 
            this.rbpgrpROIFile.ItemLinks.Add(this.bbiLoadImage);
            this.rbpgrpROIFile.Name = "rbpgrpROIFile";
            this.rbpgrpROIFile.Tag = "RBPGRP_IMAGEFUNCTION";
            this.rbpgrpROIFile.Text = "图像功能组";
            // 
            // rbpgrpROIShape
            // 
            this.rbpgrpROIShape.ItemLinks.Add(this.bbiLine);
            this.rbpgrpROIShape.ItemLinks.Add(this.bbiRectangle1);
            this.rbpgrpROIShape.ItemLinks.Add(this.bbiRectangle2);
            this.rbpgrpROIShape.ItemLinks.Add(this.bbiCircularArc);
            this.rbpgrpROIShape.ItemLinks.Add(this.bbiCircle);
            this.rbpgrpROIShape.ItemLinks.Add(this.bbiAnnulus);
            this.rbpgrpROIShape.Name = "rbpgrpROIShape";
            this.rbpgrpROIShape.Tag = "RBPGRP_ROISHAPE";
            this.rbpgrpROIShape.Text = "区域形状组";
            // 
            // rbpgrpViewOption
            // 
            this.rbpgrpViewOption.ItemLinks.Add(this.bchkiNone);
            this.rbpgrpViewOption.ItemLinks.Add(this.bchkiMove);
            this.rbpgrpViewOption.ItemLinks.Add(this.bchkiZoom);
            this.rbpgrpViewOption.ItemLinks.Add(this.bchkiMagnify);
            this.rbpgrpViewOption.Name = "rbpgrpViewOption";
            this.rbpgrpViewOption.Tag = "RBPGRP_VIEWOPTION";
            this.rbpgrpViewOption.Text = "视图选项组";
            // 
            // rbpgrpOperation
            // 
            this.rbpgrpOperation.ItemLinks.Add(this.bbiDeleteActiveROI);
            this.rbpgrpOperation.ItemLinks.Add(this.bbiDeleteAllROI);
            this.rbpgrpOperation.ItemLinks.Add(this.bbiResetWindow);
            this.rbpgrpOperation.Name = "rbpgrpOperation";
            this.rbpgrpOperation.Tag = "RBPGRP_OPERATION";
            this.rbpgrpOperation.Text = "交互操作组";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 733);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(979, 40);
            // 
            // hWndCtrlDisplay
            // 
            this.hWndCtrlDisplay.BackColor = System.Drawing.Color.Black;
            this.hWndCtrlDisplay.BorderColor = System.Drawing.Color.Black;
            this.hWndCtrlDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWndCtrlDisplay.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWndCtrlDisplay.Location = new System.Drawing.Point(0, 166);
            this.hWndCtrlDisplay.Name = "hWndCtrlDisplay";
            this.hWndCtrlDisplay.Size = new System.Drawing.Size(979, 567);
            this.hWndCtrlDisplay.TabIndex = 2;
            this.hWndCtrlDisplay.WindowSize = new System.Drawing.Size(979, 567);
            // 
            // FrmROIInteractive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 773);
            this.Controls.Add(this.hWndCtrlDisplay);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmROIInteractive";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Tag = "FRM_ROIINTERACTIVE";
            this.Text = "关注区域交互窗口-[ROI]";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmROIInteractive_FormClosed);
            this.Load += new System.EventHandler(this.FrmROIInteractive_Load);
            this.SizeChanged += new System.EventHandler(this.FrmROIInteractive_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmROIInteractive_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmROIInteractive_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonPage rbpROIInteractive;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rbpgrpROIFile;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rbpgrpROIShape;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rbpgrpViewOption;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rbpgrpOperation;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        protected internal DevExpress.XtraBars.BarButtonItem bbiLoadImage;
        protected internal DevExpress.XtraBars.BarCheckItem bchkiNone;
        protected internal DevExpress.XtraBars.BarCheckItem bchkiMove;
        protected internal DevExpress.XtraBars.BarCheckItem bchkiZoom;
        protected internal DevExpress.XtraBars.BarCheckItem bchkiMagnify;
        protected internal DevExpress.XtraBars.BarButtonItem bbiLine;
        protected internal DevExpress.XtraBars.BarButtonItem bbiRectangle1;
        protected internal DevExpress.XtraBars.BarButtonItem bbiRectangle2;
        protected internal DevExpress.XtraBars.BarButtonItem bbiCircle;
        protected internal DevExpress.XtraBars.BarButtonItem bbiCircularArc;
        protected internal DevExpress.XtraBars.BarButtonItem bbiAnnulus;
        protected internal DevExpress.XtraBars.BarButtonItem bbiDeleteActiveROI;
        protected internal DevExpress.XtraBars.BarButtonItem bbiDeleteAllROI;
        protected internal DevExpress.XtraBars.BarButtonItem bbiResetWindow;
        protected internal HalconDotNet.HWindowControl hWndCtrlDisplay;
        protected internal DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
    }
}