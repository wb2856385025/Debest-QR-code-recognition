using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pro2DBarcode.UI
{
    public partial class UI_ROIInteractive : ProCommon.UserDefForm.FrmROIInteractive
    {
        ProVision.InteractiveROI.ROIManager _ROIManager;
        ProVision.InteractiveROI.HWndCtrller _HwndCtrller;
        HalconDotNet.HObject _hoImage;

        protected internal UI_ROIInteractive(ProCommon.Communal.Language lan) : base(lan)
        {
            InitializeComponent();          
        }

        protected internal UI_ROIInteractive(ProCommon.Communal.Language lan, HalconDotNet.HObject img) : this(lan)
        {
            _hoImage = img;
            this.bbiLoadImage.Enabled = false;               
        }

        #region 必须覆写的成员
        protected override void Bbi_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            {
                case "BBI_LOADIMAGE":
                    this.LoadImage();
                    break;
                case "BBI_LINE":
                    _ROIManager.SetROIShape(new ProVision.InteractiveROI.ROILine()); //定义区域时,矢量线段不响应;主要用在定义测量
                    break;
                case "BBI_RECTANGLE1":
                    _ROIManager.SetROIShape(new ProVision.InteractiveROI.ROIRectangle1());
                    break;
                case "BBI_RECTANGLE2":
                    _ROIManager.SetROIShape(new ProVision.InteractiveROI.ROIRectangle2());
                    break;
                case "BBI_CIRCULARARC":
                    _ROIManager.SetROIShape(new ProVision.InteractiveROI.ROICircularArc()); //定义区域时,有向圆弧不响应;主要用在定义测量
                    break;
                case "BBI_CIRCLE":
                    _ROIManager.SetROIShape(new ProVision.InteractiveROI.ROICircle());
                    break;
                case "BBI_ANNULUS":
                    _ROIManager.SetROIShape(new ProVision.InteractiveROI.ROIAnnulus());
                    break;
                case "BBI_DELETEACTIVE":
                    _ROIManager.RemoveActiveROI();
                    break;
                case "BBI_DELETEALL":
                    _HwndCtrller.ResetAll();
                    _HwndCtrller.Repaint();//ROI交互--删除全部ROI,重新绘图
                    break;
                case "BBI_RESETWINDOW":
                    _HwndCtrller.ResetWindow();
                    _HwndCtrller.Repaint();//ROI交互--重置窗口,重新绘图
                    break;
                default: break;
            }
        }

        protected override void Bchki_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraBars.BarCheckItem bchki = sender as DevExpress.XtraBars.BarCheckItem;
            if (bchki != null
                && bchki.Checked)
            {
                switch (e.Item.Tag.ToString())
                {
                    case "BCHKI_NONE":
                        _HwndCtrller.SetViewMode(ProVision.InteractiveROI.HWndCtrller.VIEW_MODE_NONE);
                        break;
                    case "BCHKI_MOVE":
                        _HwndCtrller.SetViewMode(ProVision.InteractiveROI.HWndCtrller.VIEW_MODE_MOVE);
                        break;
                    case "BCHKI_ZOOM":
                        _HwndCtrller.SetViewMode(ProVision.InteractiveROI.HWndCtrller.VIEW_MODE_ZOOM);
                        break;
                    case "BCHKI_MAGNIFY":
                        _HwndCtrller.SetViewMode(ProVision.InteractiveROI.HWndCtrller.VIEW_MODE_MAGNIFY);
                        break;
                    default: break;
                }
            }
        }

        protected override void FrmROIInteractive_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                switch (this.WindowState)
                {
                    case FormWindowState.Maximized:
                    case FormWindowState.Normal:
                        if (_hoImage != null
                            && _hoImage.IsInitialized())
                        {
                            if(_HwndCtrller!=null)
                                _HwndCtrller.Repaint();//ROI交互--窗体尺寸改变,重新绘图
                        }
                        break;
                }

            }
            catch (HalconDotNet.HalconException hex)
            {

            }
        }

        protected override void FrmROIInteractive_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        protected override void FrmROIInteractive_Load(object sender, EventArgs e)
        {
            InitFieldAndProperty();
            UpdateControl();

            _HwndCtrller.AddHobjEntity(_hoImage);
            _HwndCtrller.Repaint();//ROI--传递图像,重新渲染图形          
        }

        #endregion


        protected override void FrmROIInteractive_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Modifiers)
            {
                case Keys.Alt:
                    _isAltKeyPressed = true;
                    this.bchkiMove.Checked = _isAltKeyPressed;
                    break;
                case Keys.Control:
                    _isCtrlKeyPressed = true;
                    this.bchkiZoom.Checked = _isCtrlKeyPressed;
                    break;
                default:
                    break;
            }
        }

        protected override void FrmROIInteractive_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Modifiers)
            {
                case Keys.Alt:
                    _isAltKeyPressed = false;
                    this.bchkiMove.Checked = _isAltKeyPressed;
                    break;
                case Keys.Control:
                    _isCtrlKeyPressed = false;
                    this.bchkiZoom.Checked = _isCtrlKeyPressed;
                    break;
                default:
                    break;
            }
        }      

        protected override void InitFieldAndProperty()
        {
            base.InitFieldAndProperty();

            _isCtrlKeyPressed = false;
            _isAltKeyPressed = false;

            _ROIManager = new ProVision.InteractiveROI.ROIManager();
            _HwndCtrller = new ProVision.InteractiveROI.HWndCtrller(this.hWndCtrlDisplay);
            _HwndCtrller.RegisterHwndCtrlMouseEvents();
            _HwndCtrller.SetViewMode(ProVision.InteractiveROI.HWndCtrller.VIEW_MODE_NONE);
            _HwndCtrller.RegisterROICtroller(_ROIManager);
            _ROIManager.SetActiveROISign(ProVision.InteractiveROI.ROIManager.ROI_MODE_POS);
        }

        protected override void UpdateControl()
        {
            base.UpdateControl();

            this.bchkiNone.Checked = true;
            this.KeyPreview = true; //窗体响应按键

            try
            {
                HalconDotNet.HOperatorSet.SetSystem("tsp_height", 10000);
                HalconDotNet.HOperatorSet.SetSystem("tsp_width", 10000);
                HalconDotNet.HOperatorSet.SetDraw(this.hWndCtrlDisplay.HalconWindow, "margin");
                HalconDotNet.HOperatorSet.SetColor(this.hWndCtrlDisplay.HalconWindow, "blue");  //为加载的区域显示效果
                HalconDotNet.HOperatorSet.SetLineWidth(this.hWndCtrlDisplay.HalconWindow, 1.5);
            }
            catch (HalconDotNet.HalconException hex)
            {

            }
        }        

        public System.Collections.ArrayList GetROIList()
        {
            return _ROIManager.ROIList;
        }

        protected void LoadImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "图像文件(*.BMP,*.JPG,*.JPEG,*.TIF)|*.bmp;*.jpg;*.jpeg;*.tif";
            ofd.FilterIndex = 0;
            ofd.Title = "请选择一张图像文件";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!(_hoImage != null
                    && _hoImage.IsInitialized()))
                {
                    _hoImage = new HalconDotNet.HImage();
                }

                _hoImage.Dispose();
                try
                {
                    _hoImage = new HalconDotNet.HImage(ofd.FileName);
                    _HwndCtrller.AddHobjEntity(_hoImage);
                    _HwndCtrller.Repaint();//ROI--加载图像,重新渲染图形
                }
                catch (HalconDotNet.HalconException hex)
                {
                    MessageBox.Show("加载图像失败!\r\n" + hex.GetErrorMessage(), "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
