using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       HWndCtrller
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProVision.InteractiveROI
 * File      Name：       HWndCtrller
 * Creating  Time：       10/8/2019 4:58:57 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProVision.InteractiveROI
{
    /// <summary>
    /// This class works as a wrapper class for the HALCON window HWindow.
    /// HWndCtrl is in charge of the visualization.
    /// You can move and zoom the visible image part by using GUI component 
    /// inputs or with the mouse. The class HWndCtrller uses a graphics stack 
    /// to manage the iconic objects for the display. Each object is linked 
    /// to a graphical context, which determines how the object is to be drawn.
    /// The context can be changed by calling ChangeGraphicSettings().
    /// The graphical "modes" are defined by the class GraphicsContext and 
    /// map most of the dev_set_* operators provided in HDevelop.
    /// 本类用作HALCON窗体类HWindow的封装类,HWndCtrller控制窗体内变量的可视化.
    /// 用户通过使用GUI组件或鼠标可以移动、缩放窗体内可见的图形变量.
    /// HWndCtrl类使用了图形栈来管理图形对象以便于显示,每一个图形对象都链接到一个图表上下文
    /// 图表上下文决定如何去画对象，可以通过调用ChangeGraphicSettings修改图表上下文
    /// 图形相关的模式由GraphicContext类定义,对应多数HDevelop类提供的'dev_set_*'算子
    /// </summary>
    public class HWndCtrller
    {
        //-------------------------↓Private parameters↓---------------------------------------//
        /// <summary>
        /// Maximum number of HALCON objects that can be put on the graphics
        /// stack without loss. For each additional object, the first entry
        /// is removed from the stack again.
        /// </summary>
        private const int _MaxNum = 50;

        private int _viewMode;                //视图模式:None,拖动,图像缩放,局部图像放大[NONE,MOVE,ZOOM,MAGNIFY]
        private bool _mousePressed;           //鼠标是否按下
        private double _startRow, _startCol;  //鼠标点击起始坐标 

        /// <summary>HALCON Window </summary>
        private HalconDotNet.HWindowControl _hWndCtrl;

        /// <summary> Instance of ROIController, which manages ROI interaction </summary>
        private InteractiveROI.ROIManager _ROICtrller;

        private HalconDotNet.HWindow _zoomWindow;  //局部放大窗口
        public double ZoomAddOn
        {
            private set;
            get;
        }
        public double ZoomWndFactor
        {
            private set;get;
        }

        private int _zoomWndSize;

        /// <summary>
        /// List of HALCON objects to be drawn into the HALCON window.
        /// The list shouldn't contain more than 'mMaxNum' objects,
        /// otherwise the first entry is removed from the list.
        /// </summary>
        private System.Collections.ArrayList _hObjEntityList;  //图形对象列表

        /// <summary>
        /// Instance that describes the graphical context for the HALCON window.
        /// According on the graphical settings attached to each HALCON object, 
        /// this graphical context list is updated constantly.
        /// 图形上下文(用于控制各个图形变量的显示)
        /// </summary>
        private InteractiveROI.GraphicContext _grpCntx;

        /// <summary>
        /// _ROIPaintMode is a flag to know when to add the ROI models to the 
	    /// paint routine and whether or not to respond to mouse events for 
		/// ROI objects 标记:何时添加ROI到绘制程式，是否响应鼠标事件
        /// </summary>
        private int _ROIPaintMode;

        /// <summary>
        /// 显示控件的宽高,图像的宽高
        /// </summary>
        private int _windowWidth, _windowHeight, _imageWidth, _imageHeight;

        /// <summary>
        /// Image coordinates, which describe the image part 
        /// that is displayed in the HALCON window 
        /// 显示区域的范围：左上角，右下角
        /// </summary>
        private double _imgRow1, _imgCol1, _imgRow2, _imgCol2;


        //通过GUI绘制显示窗口
        private int[] _compRangeX, _compRangeY;
        private int _prevCompX, _prevCompY;
        private double _stepSizeX, _stepSizeY;


        /// <summary> 
        /// No action is performed on mouse events 
        /// 常量标记:视图模式--None(无操作)
        /// </summary>
        public const int VIEW_MODE_NONE = 10;

        /// <summary> 
        /// Zoom is performed on mouse events
        /// 常量标记:视图模式--Zoom(缩放)
        /// </summary>
        public const int VIEW_MODE_ZOOM = 11;

        /// <summary> 
        /// Move is performed on mouse events
        /// 常量标记:视图模式--Move(移动)
        /// </summary>
        public const int VIEW_MODE_MOVE = 12;

        /// <summary> 
        /// Maganification is performed on mouse events 
        /// 常量标记:视图模式--Magnify(局部放大)
        /// </summary>
        public const int VIEW_MODE_MAGNIFY = 13;

        /// <summary>
        /// 常量标记:绘图模式--包含ROI
        /// </summary>
        public const int PAINT_MODE_INCLUDE_ROI = 1;

        /// <summary>
        /// 常量标记:绘图模式--不包含ROI
        /// </summary>
        public const int PAINT_MODE_EXCLUDE_ROI = 2;

        /// <summary> 
        /// Constant describes delegate message to signal new image
        /// 常量标记:更新图像
        /// </summary>
        public const int EVENT_UPDATE_IMAGE = 31;

        /// <summary> 
        /// Constant describes delegate message to signal error
        /// when reading an image from file 
        /// 常量标记:读取图像出错
        ///  </summary>
        public const int ERROR_READING_IMAGE = 32;

        /// <summary> 
        /// Constant describes delegate message to signal error
        /// when defining a graphical context 
        /// 常量标记:定义图形变量上下文出错
        /// </summary>
        public const int ERROR_DEFINING_GC = 33;

        /// <summary> 
        /// Error message when an exception is thrown
        /// 异常信息
        /// </summary>
        public string ExceptionMsg = "";

        /// <summary> 
        /// Delegate to add information to the HALCON window after 
        /// the paint routine has finished 
        /// 绘图程序完成后在显示窗体更新信息的委托
        /// </summary>
        public InformationUpdatedDelegate NotifyInfoObserver;

        /// <summary>
        /// Delegate to notify about failed tasks of the HWndCtrl instance
        /// 显示窗体管理类任务执行委托回调
        /// </summary>
        public event IconicUpdatedDelegate IconicUpdatedEvt;

        public bool IsInitDataFromInner;

        public bool IsCtrlKeyPressed, IsAltKeyPressed;


        /// <summary>
        /// Initializes the image dimension, mouse delegation, 
        /// and the graphical context setup of the instance.
        /// </summary>
        /// <param name="hwndctrl"></param>
        public HWndCtrller(HalconDotNet.HWindowControl hwndctrl)
        {
            _hWndCtrl = hwndctrl;
            _viewMode = HWndCtrller.VIEW_MODE_NONE;
            _windowWidth = _hWndCtrl.Size.Width;
            _windowHeight = _hWndCtrl.Size.Height;

            ZoomWndFactor = (double)_imageWidth / _hWndCtrl.Width;
            ZoomAddOn = System.Math.Pow(0.9, 5);
            _zoomWndSize = 150;

            _ROIPaintMode = HWndCtrller.PAINT_MODE_INCLUDE_ROI;

            NotifyInfoObserver = new InformationUpdatedDelegate(DummyS);
            IconicUpdatedEvt = new IconicUpdatedDelegate(OnIconicUpdated);

            /* Graphical Stack */
            _hObjEntityList = new System.Collections.ArrayList(20);
            _grpCntx = new GraphicContext();
            _grpCntx.NotifyGraphicContext = new GraphicContextDelegate(ExceptionGC);

            /*GUI绘制窗口用参数的初始值*/
            _compRangeX = new int[] { 0, 100 };
            _compRangeY = new int[] { 0, 100 };
            _prevCompX = _prevCompY = 0;
        }

        public void RegisterHwndCtrlMouseEvents()
        {
            _hWndCtrl.HMouseUp += new HalconDotNet.HMouseEventHandler(MouseUp);
            _hWndCtrl.HMouseDown += new HalconDotNet.HMouseEventHandler(MouseDown);
            _hWndCtrl.HMouseMove += new HalconDotNet.HMouseEventHandler(MouseMove);
            _hWndCtrl.HMouseWheel += new HalconDotNet.HMouseEventHandler(MouseWheel);
        }

        /// <summary>
        ///  Registers an instance of a ROICtrller with this halcon window
        ///  controller (and vice versa).
        ///  注册ROI管理类(ROICtrller)实例到(HWndCtrller)窗体管理类实例,反之亦然
        /// </summary>
        /// <param name="roiCtrller"></param>
        public void RegisterROICtroller(InteractiveROI.ROIManager roiCtrller)
        {
            _ROICtrller = roiCtrller;
            roiCtrller.RegisterHWndCtrller(this);          
        }

        /// <summary>
        /// Read dimensions of the image to adjust own window settings
        /// </summary>
        /// <param name="himage"></param>
        private void SetImagePart(HalconDotNet.HObject himage)
        {
            HalconDotNet.HTuple width, height;
            HalconDotNet.HOperatorSet.GetImageSize(himage, out width, out height);
            SetImagePart(0, 0, height[0].I, width[0].I);
        }

        /// <summary>
        /// Adjust window settings by the values supplied for the left
        /// upper corner and the right lower corner
        /// 根据提供的左上角和右下角值调整窗体设置
        /// </summary>
        /// <param name="r1">row coordinate of left upper point</param>
        /// <param name="c1">column coordinate of left upper point</param>
        /// <param name="r2">row coordinate of right bottom point</param>
        /// <param name="c2">column coordinate of right bottom point</param>
        private void SetImagePart(int r1, int c1, int r2, int c2)
        {
            _imgRow1 = r1;
            _imgCol1 = c1;
            _imgRow2 = _imageHeight = r2;
            _imgCol2 = _imageWidth = c2;

            System.Drawing.Rectangle rect = _hWndCtrl.ImagePart;
            rect.X = (int)_imgCol1;
            rect.Y = (int)_imgRow1;
            rect.Height = (int)_imageHeight;
            rect.Width = (int)_imageWidth;
            _hWndCtrl.ImagePart = rect;
        }

        /// <summary>
        /// Sets the view mode for mouse events in the HALCON window
        /// (zoom, move, magnify or none).
        /// 设置显示控件的视图模式
        /// </summary>
        /// <param name="mode">One of the MODE_VIEW_* constants</param>
        public void SetViewMode(int mode)
        {
            switch (mode)
            {
                case HWndCtrller.VIEW_MODE_NONE:
                case HWndCtrller.VIEW_MODE_ZOOM:
                case HWndCtrller.VIEW_MODE_MOVE:
                case HWndCtrller.VIEW_MODE_MAGNIFY:
                    _viewMode = mode;
                    break;
                default:
                    _viewMode = HWndCtrller.VIEW_MODE_NONE;
                    break;
            }

            //ROI管理器非空,重置即时ROI
            if (_ROICtrller != null)
                _ROICtrller.ResetInstantROI();

        }

        /// <summary>
        /// Paint or don't paint the ROIs into the HALCON window by
        /// defining the parameter to be equal to 1 or not equal to 1.
        /// 设置绘图模式(是否包含ROI)
        /// </summary>
        /// <param name="mode"></param>
        public void SetROIPaintMode(int mode)
        {
            switch (mode)
            {
                case HWndCtrller.PAINT_MODE_INCLUDE_ROI:
                case HWndCtrller.PAINT_MODE_EXCLUDE_ROI:
                    _ROIPaintMode = mode;
                    break;
                default:
                    _ROIPaintMode = HWndCtrller.PAINT_MODE_EXCLUDE_ROI;
                    break;
            }
        }

        /// <summary>
        /// 图形变量上下文操作异常处理函数
        /// </summary>
        /// <param name="msg"></param>
        private void ExceptionGC(string msg)
        {
            ExceptionMsg = msg;
            IconicUpdatedEvt(HWndCtrller.ERROR_DEFINING_GC);
        }

        /// <summary>
        /// 图形变量信息相关的委托的处理函数
        /// </summary>
        public void DummyS()
        {
        }

        /// <summary>
        /// 图形变量任务相关的委托的处理函数
        /// </summary>
        /// <param name="val"></param>
        public void OnIconicUpdated(int val)
        {

        }

        #region Graphical Element图形元素

        /// <summary>
        /// 以指定点缩放图形
        /// </summary>
        /// <param name="y">指定行坐标</param>
        /// <param name="x">指定列坐标</param>
        /// <param name="scale"></param>
        private void ZoomImage(double y, double x, double scale)
        {
            //-----------待完善,不能无限制缩放_2019-06-21----------

            double dlthC, dlthR;        //宽度，高度(double 类型)
            double percentC, percentR;  //相对坐标
            int ilthC, ilthR;           //宽度，高度(int 类型)

            //缩放前指定点相对图像区域起始点的变化量
            percentC = (x - _imgCol1) / (_imgCol2 - _imgCol1);
            percentR = (y - _imgRow1) / (_imgRow2 - _imgRow1);

            //缩放后的宽度,高度
            dlthC = (_imgCol2 - _imgCol1) * scale;
            dlthR = (_imgRow2 - _imgRow1) * scale;

            //缩放前后相对量之比等于缩放比，缩放后的图像区域起始坐标
            _imgCol1 = x - dlthC * percentC;
            _imgCol2 = x + dlthC * (1 - percentC);

            _imgRow1 = y - dlthR * percentR;
            _imgRow2 = y + dlthR * (1 - percentR);

            //取整型值的宽度和高度
            ilthC = (int)System.Math.Round(dlthC);
            ilthR = (int)System.Math.Round(dlthR);

            System.Drawing.Rectangle rect = _hWndCtrl.ImagePart;
            rect.X = (int)System.Math.Round(_imgCol1);
            rect.Y = (int)System.Math.Round(_imgRow1);

            //更新图像的显示区域：缩放后整型值为零时，则取值为1
            rect.Width = (ilthC > 0) ? ilthC : 1;
            rect.Height = (ilthR > 0) ? ilthR : 1;
            _hWndCtrl.ImagePart = rect;
            ZoomWndFactor *= scale;

            Repaint(); //在指定位置缩放图像,重新渲染图形
        }

        /// <summary>
        /// Scales the image in the HALCON window according to
        /// the value scale factor.
        /// [以图像起始点为指定点进行缩放]
        /// </summary>
        /// <param name="scalefactor"></param>
        public void ZoomImage(double scalefactor)
        {
            double midPointX, midPointY;
            if (((_imgRow2 - _imgRow1) == _imageHeight * scalefactor) &&
                ((_imgCol2 - _imgCol1) == _imageWidth * scalefactor))
            {
                Repaint(); //按系数缩放图像,重新渲染图形
                return;
            }

            _imgRow2 = _imgRow1 + _imageHeight;
            _imgCol2 = _imgCol1 + _imageWidth;

            midPointX = _imgCol1;
            midPointY = _imgRow1;
            ZoomWndFactor = (double)_imageWidth / _hWndCtrl.Width;

            ZoomImage(midPointX, midPointY, scalefactor);
        }

        /// <summary>
        /// 移动图形
        /// </summary>
        /// <param name="motionY">垂直方向移动量:目标点至起始点的偏移</param>
        /// <param name="motionX">水平方向移动量:目标点至起始点的偏移</param>
        private void MoveImage(double motionY, double motionX)
        {
            this._imgRow1 -= motionY;
            this._imgRow2 -= motionY;

            this._imgCol1 -= motionX;
            this._imgCol2 -= motionX;

            System.Drawing.Rectangle rect = _hWndCtrl.ImagePart;
            rect.X = (int)System.Math.Round(_imgCol1);
            rect.Y = (int)System.Math.Round(_imgRow1);
            _hWndCtrl.ImagePart = rect;
            Repaint(); //移动图像,重新渲染图形
        }

        /// <summary>
        /// Scales the HALCON window according to the value scale
        /// 缩放显示控件
        /// </summary>
        /// <param name="scale"></param>
        public void ScaleWindow(double scale)
        {
            _imgRow1 = 0;
            _imgCol1 = 0;
            _imgRow2 = _imageHeight;
            _imgCol2 = _imageWidth;

            _hWndCtrl.Width = (int)(_imgCol2 * scale);
            _hWndCtrl.Height = (int)(_imgRow2 * scale);
            ZoomWndFactor = ((double)_imageWidth / _hWndCtrl.Width);
        }

        /// <summary>
        /// Recalculates the image-window-factor, which needs to be added to
        /// the scale factor for zooming an image. This way the zoom gets
        /// adjusted to the window-image relation, expressed by the equation
        /// mImageWidth/mHWindowControl.Width.
        /// </summary>
        public void SetZoomWndFactor()
        {
            ZoomWndFactor = ((double)_imageWidth / _hWndCtrl.Width);
        }

        /// <summary>
        /// Sets the image-window-factor to the value zoomfc
        /// </summary>
        /// <param name="zoomfc"></param>
        public void SetZoomWndFactor(double zoomfc)
        {
            ZoomWndFactor = zoomfc;
        }

        /// <summary>
        /// Resets all parameters that concern the HALCON window display
        /// setup to their initial values and clears the ROI list.
        /// </summary>
        public void ResetAll()
        {
            ResetWindow();
            if (this._ROICtrller != null)
                this._ROICtrller.Reset();
        }

        /// <summary>
        /// Resets all parameters that concern the HALCON window display
        /// setup to their initial values.
        /// [重置显示控件的显示区域]
        /// </summary>
        public void ResetWindow()
        {
            _imgRow1 = 0;
            _imgCol1 = 0;
            _imgRow2 = _imageHeight;
            _imgCol2 = _imageWidth;
            ZoomWndFactor = (double)_imageWidth / _hWndCtrl.Width;

            System.Drawing.Rectangle rect = _hWndCtrl.ImagePart;
            rect.X = (int)System.Math.Round(_imgCol1);
            rect.Y = (int)System.Math.Round(_imgRow1);

            rect.Width = (int)_imageWidth;
            rect.Height = (int)_imageHeight;
            _hWndCtrl.ImagePart = rect;
        }


        /// <summary>
        /// Triggers a repaint of the HALCON window
        /// </summary>
        public void Repaint()
        {
            Repaint(_hWndCtrl.HalconWindow);
        }

        public void Repaint(HalconDotNet.HTuple hwndHandle)
        {
            int count = _hObjEntityList.Count;
            InteractiveROI.HObjectEntity hobjentry;

            HalconDotNet.HSystem.SetSystem("flush_graphic", "false"); //不更新图形变量
            HalconDotNet.HOperatorSet.ClearWindow(hwndHandle);

            _grpCntx.LastGCSettings.Clear();
            //显示对应图形上下文的图形对象(图像)
            for (int i = 0; i < count; i++)
            {
                hobjentry = (InteractiveROI.HObjectEntity)_hObjEntityList[i];
                if(hobjentry.HObj!=null
                    && hobjentry.HObj.IsInitialized())
                {
                    _grpCntx.ApplyGraphicSettings(hwndHandle, hobjentry.GCSettings);

                    HalconDotNet.HOperatorSet.DispObj(hobjentry.HObj, hwndHandle);
                }
            }

            NotifyInfoObserver();

            if ((_ROICtrller != null)
                && (_ROIPaintMode == HWndCtrller.PAINT_MODE_INCLUDE_ROI))
                _ROICtrller.PaintData(hwndHandle);

            HalconDotNet.HSystem.SetSystem("flush_graphic", "true"); //更新图形变量
            HalconDotNet.HOperatorSet.SetColor(hwndHandle, new HalconDotNet.HTuple("black"));
            HalconDotNet.HOperatorSet.DispLine(hwndHandle, -100.0, -100.0, -101.0, -101.0);//不知何用   
        }

        /// <summary>
        /// To initialize the move function using a GUI component, the HWndCtrller
        /// first needs to know the range supplied by the GUI component. 
        /// For the x direction it is specified by 'xrge', which is 
        /// calculated as follows: GuiComponentX.Max()-GuiComponentX.Min().
        /// The starting value of the GUI component has to be supplied 
        /// by the parameter 'initval'
        /// </summary>
        public void SetGUICompRangeX(int[] xrge, int initval)
        {
            int cmprgeX;

            _compRangeX = xrge;
            cmprgeX = xrge[1] - xrge[0];
            _prevCompX = initval;
            _stepSizeX = ((double)_imageWidth / cmprgeX) * (_imageWidth / _windowWidth);

        }

        /// <summary>
        /// To initialize the move function using a GUI component, the HWndCtrller
        /// first needs to know the range supplied by the GUI component. 
        /// For the y direction it is specified by 'yrge', which is 
        /// calculated as follows: GuiComponentY.Max()-GuiComponentY.Min().
        /// The starting value of the GUI component has to be supplied 
        /// by the parameter 'initval'
        /// </summary>
        public void SetGUICompRangeY(int[] yrge, int initval)
        {
            int cmprgeY;

            _compRangeX = yrge;
            cmprgeY = yrge[1] - yrge[0];
            _prevCompY = initval;
            _stepSizeY = ((double)_imageHeight / cmprgeY) * (_imageHeight / _windowHeight);

        }

        /// <summary>
		/// Resets to the starting value of the GUI component.
		/// </summary>
        public void ResetGUIInitValues(int initVY, int initVX)
        {
            _prevCompY = initVY;
            _prevCompX = initVX;
        }

        /// <summary>
		/// Moves the image by the value initVY supplied by the GUI component
		/// </summary>
        public void MoveYByGUIHandle(int initVY)
        {
            double deltaY = (initVY - _prevCompY) * _stepSizeY;

            if (deltaY == 0.0)
                return;
            MoveImage(deltaY, 0.0);
            _prevCompY = initVY;

        }

        /// <summary>
        /// Moves the image by the value initVX supplied by the GUI component
        /// </summary>
        public void MoveXByGUIHandle(int initVX)
        {
            double deltaX = (initVX - _prevCompX) * _stepSizeX;

            if (deltaX == 0.0)
                return;
            MoveImage(0.0, deltaX);
            _prevCompX = initVX;

        }

        /// <summary>
		/// Zooms the image by the value valF supplied by the GUI component
		/// </summary>
        public void ZoomByGUIHandle(double valF)
        {
            double y, x, scale;
            double prescale;

            y = (_imgRow2 + _imgRow1) / 2;
            x = (_imgCol2 + _imgCol1) / 2;

            prescale = (double)((_imgCol2 - _imgCol1) / _imageWidth);
            scale = ((double)1.0 / prescale * (100.0 / valF));

            ZoomImage(y, x, scale);
        }

        #endregion    

        #region Event handling for mouse鼠标事件处理函数

        /// <summary>
        /// 鼠标滚轮事件的处理函数
        /// [满足条件下缩放图像]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MouseWheel(object sender, HalconDotNet.HMouseEventArgs e)
        {
            int activeROIIdx = -1;
            double scale = 0;

            //在ROI管理器非空且ROI是否添加绘制的标记为MODE_INCLUDE_ROI
            if (_ROICtrller != null && _ROIPaintMode == PAINT_MODE_INCLUDE_ROI)
            {
                //执行ROI管理器对于鼠标按下的处理，并返回活动ROI的索引(有ROI选中时,无法操作[缩放]图像)
                activeROIIdx = _ROICtrller.MouseDownAction(e.Y, e.X);
            }

            //活动ROI索引为-1，即没有选中ROI，则可以响应滚轮事件[在指定鼠标位置缩放图像]
            if (activeROIIdx == -1)
            {
                switch (_viewMode)
                {
                    case HWndCtrller.VIEW_MODE_ZOOM:
                        {
                            //设置固定的缩放比例
                            if (e.Delta > 0)
                                scale = 0.8;
                            else
                                scale = 1 / 0.8;

                            ZoomImage(e.Y, e.X, scale);
                        }
                        break;
                    case HWndCtrller.VIEW_MODE_NONE:
                        break;
                    case HWndCtrller.VIEW_MODE_MOVE:
                        break;
                    case HWndCtrller.VIEW_MODE_MAGNIFY:
                        break;
                }
            }
        }

        /// <summary>
        /// 鼠标拖动事件的处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MouseMove(object sender, HalconDotNet.HMouseEventArgs e)
        {
            double deltaX, deltaY, posX, posY;
            double zoomZone;

            //由鼠标按下到抬起(即前次的状态是按下)
            if (!_mousePressed)
                return;

            //在ROI管理器非空且ROI是否添加绘制的标记为MODE_INCLUDE_ROI,且有活动ROI
            if (_ROICtrller != null 
                && _ROIPaintMode == PAINT_MODE_INCLUDE_ROI
                && _ROICtrller.GetActiveROIIdx() != -1)
            {
                //执行ROI管理器对于鼠标移动的处理，并返回活动ROI的索引(有ROI选中时,无法操作[移动]图像)
                _ROICtrller.MouseMoveAction(e.Y, e.X);
            }
            else if (_viewMode == VIEW_MODE_MOVE)
            {
                //显示控件的视图状态为MODE_VIEW_MOVE(移动图像)
                deltaY = (e.Y - _startRow);
                deltaX = (e.X - _startCol);

                if ((int)deltaY != 0 || (int)deltaX != 0)
                {
                    //位置发生变化，移动图像
                    MoveImage(deltaY, deltaX);
                }

            }
            else if (_viewMode == HWndCtrller.VIEW_MODE_MAGNIFY)
            {
                //显示控件的视图状态为MODE_VIEW_MAGNIFY(在鼠标当前位置创建放大窗口)

                HalconDotNet.HSystem.SetSystem("flush_graphic", "false");
                _zoomWindow.ClearWindow();

                posY = ((e.Y - _imgRow1) / (_imgRow2 - _imgRow1)) * _hWndCtrl.Height;
                posX = ((e.X - _imgCol1) / (_imgCol2 - _imgCol1)) * _hWndCtrl.Width;
                zoomZone = (_zoomWndSize / 2) * ZoomWndFactor * ZoomAddOn;

                _zoomWindow.SetWindowExtents((int)posY - (_zoomWndSize / 2),
                                                  (int)posX - (_zoomWndSize / 2),
                                                  _zoomWndSize, _zoomWndSize);

                _zoomWindow.SetPart((int)(e.Y - zoomZone), (int)(e.X - zoomZone), (int)(e.Y + zoomZone), (int)(e.X + zoomZone));
                Repaint(_zoomWindow);
                HalconDotNet.HSystem.SetSystem("flush_graphic", "true");
                _zoomWindow.DispLine(-100.0, -100.0, -100.0, -100.0);
            }

        }


        /// <summary>
        /// 鼠标按下事件的处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MouseDown(object sender, HalconDotNet.HMouseEventArgs e)
        {
            this._mousePressed = true;
            int activeROIIdx = -1;
#pragma warning disable CS0219 // The variable 'scale' is assigned but its value is never used
            double scale = 0;
#pragma warning restore CS0219 // The variable 'scale' is assigned but its value is never used

            //在ROI管理器非空且ROI是否添加绘制的标记为MODE_INCLUDE_ROI
            if (_ROICtrller != null
                && _ROIPaintMode == HWndCtrller.PAINT_MODE_INCLUDE_ROI)
            {
                //执行ROI管理器对于鼠标按下的处理，并返回活动ROI的索引(有ROI选中时,无法操作[移动]图像)
                activeROIIdx = _ROICtrller.MouseDownAction(e.Y, e.X);
            }

            //活动ROI索引为-1，即没有选中ROI
            if (activeROIIdx == -1)
            {
                switch (_viewMode)
                {
                    case HWndCtrller.VIEW_MODE_ZOOM:
                        //{                            
                        //    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                        //        scale = 0.8;
                        //    else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                        //        scale = 1 / 0.8;

                        //    this.ZoomImage(e.Y, e.X, scale);
                        //}
                        break;
                    case HWndCtrller.VIEW_MODE_NONE:
                        break;
                    case HWndCtrller.VIEW_MODE_MOVE:
                        //与鼠标移动事件互助，此时记录鼠标的点击位置，在鼠标移动事件时，平移图像
                        _startRow = e.Y;
                        _startCol = e.X;
                        break;
                    case HWndCtrller.VIEW_MODE_MAGNIFY:
                        //在鼠标点击位置，创建显示窗口,局部放大图像[放大部分待定]
                        CreateZoomWindow(e.Y, e.X);
                        break;
                }
            }
        }

        /// <summary>
        /// 鼠标抬起事件的处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MouseUp(object sender, HalconDotNet.HMouseEventArgs e)
        {
            this._mousePressed = false;

            //在ROI管理器非空且ROI是否添加绘制的标记为MODE_INCLUDE_ROI,且有活动ROI
            if (_ROICtrller != null
                && _ROIPaintMode == HWndCtrller.PAINT_MODE_INCLUDE_ROI
                && _ROICtrller.GetActiveROIIdx() != -1)
            {
                //若活动ROI索引非-1,则表示更新ROI(或创建或选择)
                _ROICtrller.IconicUpdatedDel(ROIManager.EVENT_UPDATE_ROI);
            }
            else if (_viewMode == HWndCtrller.VIEW_MODE_MAGNIFY)
            {
                if (_zoomWindow != null)
                    _zoomWindow.Dispose();
            }
            else if (_viewMode == HWndCtrller.VIEW_MODE_MOVE)
            {
                //主要时测试鼠标抬起和移动谁先触发
#pragma warning disable CS0219 // The variable 'i' is assigned but its value is never used
                double i = 0.0;
#pragma warning restore CS0219 // The variable 'i' is assigned but its value is never used
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="y">行坐标</param>
        /// <param name="x">列坐标</param>
        private void CreateZoomWindow(double y, double x)
        {
            double posY, posX;
            int zoomZone;

            int iY = (int)y;
            int iX = (int)x;

            if (this._zoomWindow != null)
                this._zoomWindow.Dispose();

            HalconDotNet.HOperatorSet.SetSystem("border_width", 10);
            this._zoomWindow = new HalconDotNet.HWindow();
            posY = ((iY - _imgRow1) / (_imgRow2 - _imgRow1)) * _hWndCtrl.Height;
            posX = ((iX - _imgCol1) / (_imgCol2 - _imgCol1)) * _hWndCtrl.Width;

            zoomZone = (int)((_zoomWndSize / 2) * ZoomWndFactor * ZoomAddOn);

            _zoomWindow.OpenWindow((int)posY - (_zoomWndSize / 2), (int)posX - (_zoomWndSize / 2),
                _zoomWndSize, _zoomWndSize, _hWndCtrl.HalconID, "visible", "");
            _zoomWindow.SetPart(iY - zoomZone, iX - zoomZone, iY + zoomZone, iX + zoomZone);

            Repaint(_zoomWindow);
            _zoomWindow.SetColor("black");
        }

        #endregion

        #region GraphicsStack

        public void AddHobjEntity(HalconDotNet.HObject hobj)
        {
            InteractiveROI.HObjectEntity entry;
            if (hobj == null)
                return;
            HalconDotNet.HObject obj = hobj as HalconDotNet.HObject;

            if (obj != null
                && obj.IsInitialized())
            {
                HalconDotNet.HTuple cls = new HalconDotNet.HTuple();
                HalconDotNet.HOperatorSet.GetObjClass(obj, out cls);

                if (cls.TupleNotEqual(new HalconDotNet.HTuple()))
                {
                    switch (cls[0].S)
                    {
                        case "image":
                            {
                                HalconDotNet.HTuple r, c, h = new HalconDotNet.HTuple(), w = new HalconDotNet.HTuple(), area = new HalconDotNet.HTuple();
                                HalconDotNet.HObject domain;
                                HalconDotNet.HOperatorSet.GetDomain(obj, out domain);
                                if (domain != null
                                    && domain.IsInitialized())
                                {
                                    HalconDotNet.HOperatorSet.AreaCenter(domain, out area, out r, out c);
                                }

                                if (area != null
                                    && area.TupleNotEqual(new HalconDotNet.HTuple()))
                                {
                                    HalconDotNet.HOperatorSet.GetImageSize(obj, out w, out h);
                                }

                                if (w != null
                                    && w.TupleNotEqual(new HalconDotNet.HTuple()))
                                {
                                    if (area[0].I == (w * h)[0].I)//有效图像(即定义域面积等于其宽高乘积)
                                    {
                                        ClearEntities();

                                        if ((h[0].I != _imageHeight) || (w[0].I != _imageWidth))
                                        {
                                            _imageHeight = h;
                                            _imageWidth = w;
                                            ZoomWndFactor = (double)_imageWidth / _hWndCtrl.Width;
                                            SetImagePart(0, 0, h[0].I, w[0].I);
                                        }
                                    }
                                }

                                entry = new HObjectEntity(hobj, _grpCntx.CopyGraphicSettings());
                                _hObjEntityList.Add(entry);
                            }
                            break;
                        case "region":
                        case "xld_cont":
                            entry = new HObjectEntity(hobj, _grpCntx.CopyGraphicSettings());
                            _hObjEntityList.Add(entry);
                            break;
                        case "xld_poly":
                            break;
                        case "xld_parallel":
                            break;
                        default: break;
                    }
                }

                if (_hObjEntityList.Count > _MaxNum)
                    _hObjEntityList.RemoveAt(1);
            }
        }

        /// <summary>
        /// Clears all entries from the graphics stack
        /// </summary>
        public void ClearEntities()
        {
            _hObjEntityList.Clear();
        }

        public int GetEntitiesCount()
        {
            return _hObjEntityList.Count;
        }

        /// <summary>
        /// 设置图形上下文
        /// [字符串型参数]
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="val"></param>
        public void ChangeGraphicSettings(string mode, string val)
        {
            switch (mode)
            {
                case GraphicContext.GC_COLOR:
                    _grpCntx.SetColorAttribute(val);
                    break;
                case GraphicContext.GC_DRAWMODE:
                    _grpCntx.SetDrawModeAttribute(val);
                    break;
                case GraphicContext.GC_LUT:
                    _grpCntx.SetLutAttribute(val);
                    break;
                case GraphicContext.GC_PAINT:
                    _grpCntx.SetPaintAttribute(val);
                    break;
                case GraphicContext.GC_SHAPE:
                    _grpCntx.SetShapeAttribute(val);
                    break;
            }
        }

        /// <summary>
        /// 设置图形上下文
        /// [整型参数]
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="val"></param>
        public void ChangeGraphicSettings(string mode, int val)
        {
            switch (mode)
            {
                case GraphicContext.GC_COLORED:
                    _grpCntx.SetColoredAttribute(val);
                    break;
                case GraphicContext.GC_LINEWIDTH:
                    _grpCntx.SetLineWidthAttribute(val);
                    break;
            }
        }

        /// <summary>
        /// 设置图形上下文
        /// [HTuple型参数]
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="val"></param>
        public void ChangeGraphicSettings(string mode, HalconDotNet.HTuple val)
        {
            switch (mode)
            {
                case GraphicContext.GC_LINESTYLE:
                    _grpCntx.SetLineStyleAttribute(val);
                    break;
            }
        }

        /// <summary>
        /// 获取图形上下文设置
        /// </summary>
        /// <returns></returns>
        public System.Collections.Hashtable GetGraphicContext()
        {
            return _grpCntx.CopyGraphicSettings();
        }

        public void ClearGraphicSettings()
        {
            _grpCntx.ClearGraphicSettings();
        }

        #endregion

    }

    public delegate void InformationUpdatedDelegate();   //图形变量信息的相关委托

    public delegate void IconicUpdatedDelegate(int val); //图形变量任务相关的委托
}
