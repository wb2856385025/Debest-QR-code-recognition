using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pro2DBarcode.UI.DerivedControl
{
    public partial class ImageResultViewer : DevExpress.XtraEditors.XtraUserControl, IHWindowGraphicStack
    {
        /// <summary>
        /// 语言版本
        /// </summary>
        protected internal ProCommon.Communal.Language LanguageVersion { private set; get; }

        /// <summary>
        /// 是否显示中心十字
        /// </summary>
        protected internal bool ShowCrossEnable { set; get; }

        /// <summary>
        /// 是否显示图层信息
        /// </summary>
        protected internal bool ShowCoverInfoEnable { set; get; }

        /// <summary>
        /// 射击部分
        /// </summary>
        protected internal string ShotPart { set; get; }

        protected internal string ShotResult { set; get; }

        protected internal string RemarkInfo { set; get; }


        /// <summary>
        /// 当前视图索引
        /// [注:可以用于标记靶道]
        /// </summary>
        protected internal int CurrentViewIndex{ private set; get; }

        /// <summary>
        /// 显示控件的背景图像
        /// </summary>
        protected internal HalconDotNet.HObject HWndBkGrdImage;

        /// <summary>
        /// 实时图形结果
        /// [注:包括结果图像,结果图形(Region或者Contour)]
        /// </summary>
        protected internal HalconDotNet.HObject HWndRsltIconic;

        #region Obosolete Variable

        /*
        /// <summary>
        /// 用户图形显示控件
        /// </summary>
        protected internal HalconDotNet.HWindowControl CoverIconicView { private set; get; }

        /// <summary>
        /// 显示射击结果信息的控件
        /// </summary>
        protected internal DevExpress.XtraEditors.LabelControl CoverInfoView { private set; get; }

        */

        #endregion


        /// <summary>
        /// 图形对象动作
        /// </summary>
        private UserIconicAction _myIconicAction;

        /// <summary>
        /// 选择的绘制对象
        /// </summary>
        private HalconDotNet.HDrawingObject _selectedDrawingObject;

        /// <summary>
        /// 绘制对象列表
        /// </summary>
        private System.Collections.Generic.List<HalconDotNet.HDrawingObject> _drawObjectList;

        /// <summary>
        /// 图形对象栈
        /// </summary>
        private System.Collections.Generic.Stack<HalconDotNet.HObject> _graphicStack;

        /// <summary>
        /// 栈同步锁
        /// </summary>
        private object _stackLock;

        /// <summary>
        /// 是否单击标记
        /// </summary>
        private bool _isSingleClick;
        private string _hWndTag;

        /// <summary>
        /// 最近时刻
        /// </summary>
        private System.DateTime _dateTimeLast;

        private ImageResultViewer()
        {
            InitializeComponent();           
            InitFieldAndProperty();
        }       

        /// <summary>
        /// 构造函数
        /// [注:根据语言版本更新控件显示文本]
        /// </summary>
        /// <param name="lan"></param>
        protected internal ImageResultViewer(ProCommon.Communal.Language lan,int viewIndex):this()
        {
            LanguageVersion = lan;
            CurrentViewIndex = viewIndex;           
            InitControls();
        }

        /// <summary>
        /// 初始化字段和属性
        /// </summary>
        private void InitFieldAndProperty()
        {
            ShowCrossEnable = false;
            ShowCoverInfoEnable = true;

            ShotPart = "头部";
            ShotResult = "9.3";
            RemarkInfo = "晴,东南2级,24度";

            CurrentViewIndex = 0;
            HWndBkGrdImage = null;
           
            _myIconicAction = new UserIconicAction(this);           
            _selectedDrawingObject = null;
            _drawObjectList = new List<HalconDotNet.HDrawingObject>();
            _graphicStack = new Stack<HalconDotNet.HObject>();
            _stackLock = new object();
            _isSingleClick = false;
            LanguageVersion = ProCommon.Communal.Language.Chinese;
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            InitHWndControl();          
            InitLabelControl();
            InitBarSubItem();
            InitBarButtonItem();
            UpdateMemoEdit(this.meRemarkInfo, LanguageVersion, null);
        }

        #region HWindowControl
        private void InitHWndControl()
        {
            UpdateHWndControl(this.hwndcImgRsltView, LanguageVersion);
            RegisterEventsForHWndControl(this.hwndcImgRsltView);
            HalconDotNet.HOperatorSet.ReadImage(out HWndBkGrdImage, Manager.ConfigManager.DirectoryBase + "TargetSheet.jpg");
            AttachHWndCtrlBackGroundImage(this.hwndcImgRsltView, HWndBkGrdImage);           
        }

        private void UpdateHWndControl(HalconDotNet.HWindowControl hwndctrl,ProCommon.Communal.Language lan)
        {
            if(hwndctrl!=null
                && hwndctrl.Tag!=null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = hwndctrl.Tag.ToString();
                hwndctrl.Text = isChs ? Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str);
            }
        }

        /// <summary>
        ///  HWindowControl注册事件
        ///  [注:包含双击,移动,抬起事件]
        /// </summary>
        /// <param name="hwndctrl"></param>
        private void RegisterEventsForHWndControl(HalconDotNet.HWindowControl hwndctrl)
        {
            if (hwndctrl != null)
            {
                hwndctrl.HMouseDown += WrappedHMouseDown;
                hwndctrl.HMouseMove += HMouseMove;
                hwndctrl.HMouseUp += HMouseUp;
                hwndctrl.SizeChanged += Hwndctrl_SizeChanged;
            }
        }

        private void Hwndctrl_SizeChanged(object sender, EventArgs e)
        {
            DisplayGraphicStack();
            if (ShowCoverInfoEnable)
                UpdateInfoView(this.hwndcImgRsltView.HalconWindow, LanguageVersion, ShotPart, ShotResult, RemarkInfo);
        }

        /// <summary>
        ///  HWindowControl鼠标抬起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HMouseUp(object sender, HalconDotNet.HMouseEventArgs e)
        {
            
        }

        /// <summary>
        ///  HWindowControl鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HMouseMove(object sender, HalconDotNet.HMouseEventArgs e)
        {
            
        }

        /// <summary>
        /// HWindowControl鼠标单击事件
        /// [注:这里转换为双击效果]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WrappedHMouseDown(object sender, HalconDotNet.HMouseEventArgs e)
        {
            if(ConvertHWndCtrlMouseSingleToDoubleClick(sender,e))
            {
                //左键双击，则切换窗口
                Manager.UIManager.Instance.SwitchMainAndSubWindow(CurrentViewIndex);

            } else if(e.Button== System.Windows.Forms.MouseButtons.Right)
            {
                //右键，则弹出右键菜单
                this.ppmImgRsltView.ShowPopup(new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y));
            }
        }

        /// <summary>
        /// 鼠标左键双击判定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool ConvertHWndCtrlMouseSingleToDoubleClick(object sender, HalconDotNet.HMouseEventArgs e)
        {
            HalconDotNet.HWindowControl hwdctrl = sender as HalconDotNet.HWindowControl;
            if (hwdctrl != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (_isSingleClick)//左键单击,已记录单击一次
                    {
                        if (_hWndTag == hwdctrl.Tag.ToString()) //再次单击时，同一个窗体
                        {
                            TimeSpan span = DateTime.Now - _dateTimeLast;
                            if (span.Milliseconds+400 <= SystemInformation.DoubleClickTime) //与上次单击记录的时间间隔小于系统定义双击时间,则重置单击记录，判定为双击
                            {
                                //Console.WriteLine("Spn:"+ span.Milliseconds+",Sys:"+ SystemInformation.DoubleClickTime);
                                _isSingleClick = false; //左键单击记录复位
                                return true;
                            }
                            else
                            { return false; }
                        }
                        else { return false; }

                    } //左键单击，未记录单击一次，则当前记录单击一次，判定为非双击
                    else
                    {
                        _isSingleClick = true; //单击一次
                        _dateTimeLast = DateTime.Now;
                        _hWndTag = hwdctrl.Tag.ToString();
                        return false;
                    }
                }//非左键单击,判定为非双击
                else { return false; }
            }
            else { return false; }
        }        

        /// <summary>
        /// 添加背景图像
        /// </summary>
        protected virtual void AttachHWndCtrlBackGroundImage(HalconDotNet.HWindowControl hwndctrl,HalconDotNet.HObject bkgrdImg)
        {
            if(hwndctrl!=null
                && bkgrdImg!=null
                && bkgrdImg.IsInitialized())
            {
                HalconDotNet.HTuple h, w;
                HalconDotNet.HOperatorSet.GetImageSize(bkgrdImg, out w, out h);
                HalconDotNet.HOperatorSet.SetPart(hwndctrl.HalconWindow, 0, 0, h-1, w-1);
                HalconDotNet.HImage himg = new HalconDotNet.HImage();
                ProVision.Communal.Functions.HObjectToHimage(bkgrdImg, ref himg);
                hwndctrl.HalconWindow.AttachBackgroundToWindow(himg);
            }
        }

        private void UpdateInfoView(HalconDotNet.HWindow hwnd, ProCommon.Communal.Language lan, string shotPart, string shotRing, string remkInfo)
        {
            if (hwnd != null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string strInfo = isChs ?
                                       (Properties.Resources.ResourceManager.GetString("chs_LBLC_SHOTRESULTINFO") + "\r\n"
                                       + Properties.Resources.ResourceManager.GetString("chs_LBLC_SHOTPART") + shotPart + "\r\n"
                                       + Properties.Resources.ResourceManager.GetString("chs_LBLC_SHOTRINGRESULT") + shotRing + "\r\n"
                                       + Properties.Resources.ResourceManager.GetString("chs_ME_REMARKINFO") + remkInfo)
                                     : (Properties.Resources.ResourceManager.GetString("en_LBLC_SHOTRESULTINFO") + "\r\n"
                                       + Properties.Resources.ResourceManager.GetString("en_LBLC_SHOTPART") + shotPart + "\r\n"
                                       + Properties.Resources.ResourceManager.GetString("en_LBLC_SHOTRINGRESULT") + shotRing + "\r\n"
                                       + Properties.Resources.ResourceManager.GetString("en_ME_REMARKINFO") + "\r\n" + remkInfo);
                ProVision.Communal.Functions.DispMessage(hwnd, strInfo, new HalconDotNet.HTuple("image"), 20, 20, new HalconDotNet.HTuple("blue"), new HalconDotNet.HTuple("false"));
            }
        }

        #endregion

        #region LabelControl
        private void InitLabelControl()
        {
            UpdateLabelControl(this.lblcTargetNumber, LanguageVersion, CurrentViewIndex.ToString("00"));
            UpdateLabelControl(this.lblcShotPart, LanguageVersion, "头部");
            UpdateLabelControl(this.lblcShotRingResult, LanguageVersion, "9.3");
        }

        /// <summary>
        /// 更新图层信息控件
        /// </summary>
        /// <param name="lblc"></param>
        /// <param name="lan"></param>
        /// <param name="shotPart"></param>
        /// <param name="shotRing"></param>
        /// <param name="remkInfo"></param>
        private void UpdateLabelControlInfo(DevExpress.XtraEditors.LabelControl lblc,ProCommon.Communal.Language lan,string shotPart,string shotRing,string remkInfo)
        {
            if(lblc!=null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);               
                string strInfo = isChs ? 
                                       (Properties.Resources.ResourceManager.GetString("chs_LBLC_SHOTRESULTINFO")+"\r\n"
                                       + Properties.Resources.ResourceManager.GetString("chs_LBLC_SHOTPART")+ shotPart+"\r\n"
                                       + Properties.Resources.ResourceManager.GetString("chs_LBLC_SHOTRINGRESULT") + shotRing+ "\r\n"
                                       + Properties.Resources.ResourceManager.GetString("chs_ME_REMARKINFO")+ remkInfo)
                                     : (Properties.Resources.ResourceManager.GetString("en_LBLC_SHOTRESULTINFO") + "\r\n"
                                       + Properties.Resources.ResourceManager.GetString("en_LBLC_SHOTPART") + shotPart + "\r\n"
                                       + Properties.Resources.ResourceManager.GetString("en_LBLC_SHOTRINGRESULT") + shotRing + "\r\n"
                                       + Properties.Resources.ResourceManager.GetString("en_ME_REMARKINFO") + "\r\n"+remkInfo);
                lblc.Text = strInfo;
            }
        }


        /// <summary>
        /// 更新射击结果控件
        /// </summary>
        /// <param name="lblc"></param>
        /// <param name="lan"></param>
        /// <param name="str"></param>
        private void UpdateLabelControl(DevExpress.XtraEditors.LabelControl lblc, ProCommon.Communal.Language lan,string str)
        {
            if (lblc != null
                && lblc.Tag!=null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string strTag = lblc.Tag.ToString();
                lblc.Text= (isChs ?Properties.Resources.ResourceManager.GetString("chs_" + strTag):Properties.Resources.ResourceManager.GetString("en_" + strTag))+str;
            }
        }

        #region Obsolete Functions
        /*

        private void ResgisterEventsForLabelControl(DevExpress.XtraEditors.LabelControl lblc)
        {
            if (lblc != null)
            {
                lblc.MouseClick += Lblc_MouseClick;
                lblc.MouseDoubleClick += Lblc_MouseDoubleClick;
                lblc.MouseMove += Lblc_MouseMove;
                lblc.MouseUp += Lblc_MouseUp;
            }
        }

        private void Lblc_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                //右键，则弹出右键菜单
                this.ppmImgRsltView.ShowPopup(new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y));
        }

        /// <summary>
        /// LabelControl鼠标双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lblc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //左键双击，则切换窗口
            Manager.UIManager.Instance.SwitchMainAndSubWindow(CurrentViewIndex);
        }


        /// <summary>
        /// LabelControl控件鼠标抬起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lblc_MouseUp(object sender, MouseEventArgs e)
        {
           
        }

        /// <summary>
        /// LabelControl控件鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lblc_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        /// <summary>
        /// 鼠标左键双击判定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool ConvertMWndCtrlMouseSingleToDoubleClick(object sender, MouseEventArgs e)
        {
            DevExpress.XtraEditors.PanelControl hwdctrl = sender as DevExpress.XtraEditors.PanelControl;
            if (hwdctrl != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (_isSingleClick)//左键单击,已记录单击一次
                    {
                        if (_hWndTag == hwdctrl.Tag.ToString()) //再次单击时，同一个窗体
                        {
                            TimeSpan span = DateTime.Now - _dateTimeLast;
                            if (span.Milliseconds + 50 <= SystemInformation.DoubleClickTime) //与上次单击记录的时间间隔小于系统定义双击时间,则重置单击记录，判定为双击
                            {
                                _isSingleClick = false; //左键单击记录复位
                                return true;
                            }
                            else
                            { return false; }
                        }
                        else { return false; }

                    } //左键单击，未记录单击一次，则当前记录单击一次，判定为非双击
                    else
                    {
                        _isSingleClick = true; //单击一次
                        _dateTimeLast = DateTime.Now;
                        _hWndTag = hwdctrl.Tag.ToString();
                        return false;
                    }
                }//非左键单击,判定为非双击
                else { return false; }
            }
            else { return false; }
        }

        */
        #endregion

        #endregion

        #region BarSubItem
        private void InitBarSubItem()
        {
            UpdateBarSubItem(this.bsbiAddIconic,LanguageVersion);
            UpdateBarSubItem(this.bsbiSetColor, LanguageVersion);
            UpdateBarSubItem(this.bsbiSetLineType, LanguageVersion);
        }

        /// <summary>
        /// 更新Ribbon中BarSubItem控件
        /// </summary>
        /// <param name="bsbi"></param>
        /// <param name="lan"></param>
        private void UpdateBarSubItem(DevExpress.XtraBars.BarSubItem bsbi, ProCommon.Communal.Language lan)
        {
            if (bsbi != null
              && bsbi.Tag != null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = bsbi.Tag.ToString();
                bsbi.Caption = isChs ? Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str);
            }
        }

        #endregion

        #region BarButtonItem

        private void InitBarButtonItem()
        {
            UpdateBarButtonItem(this.bbiClearIconic, LanguageVersion);

            UpdateBarButtonItem(this.bbiRectangle1, LanguageVersion);
            UpdateBarButtonItem(this.bbiRectangle2, LanguageVersion);
            UpdateBarButtonItem(this.bbiCircle, LanguageVersion);
            UpdateBarButtonItem(this.bbiEllipse, LanguageVersion);
            UpdateBarButtonItem(this.bbiLine, LanguageVersion);

            UpdateBarButtonItem(this.bbiColorRed, LanguageVersion);
            UpdateBarButtonItem(this.bbiColorGreen, LanguageVersion);
            UpdateBarButtonItem(this.bbiColorBlue, LanguageVersion);
            UpdateBarButtonItem(this.bbiColorYellow, LanguageVersion);

            UpdateBarButtonItem(this.bbiContinuousLine, LanguageVersion);
            UpdateBarButtonItem(this.bbiDashedLine, LanguageVersion);
        }

        /// <summary>
        /// 更新Ribbon中BarButtonItem控件
        /// </summary>
        /// <param name="bbi"></param>
        /// <param name="lan"></param>
        private void UpdateBarButtonItem(DevExpress.XtraBars.BarButtonItem bbi, ProCommon.Communal.Language lan)
        {
            if (bbi != null
              && bbi.Tag != null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = bbi.Tag.ToString();
                bbi.Caption = isChs ? Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str);
                bbi.ItemClick += Bbi_ItemClick;
            }
        }

        /// <summary>
        /// BarButtonItem的单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bbi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            {
                case "BBI_CLEARICONIC":
                    {
                        lock (_stackLock)
                        {
                            foreach (HalconDotNet.HDrawingObject dobj in _drawObjectList)
                                dobj.Dispose();

                            _drawObjectList.Clear();
                            _graphicStack.Clear();
                            _selectedDrawingObject = null;
                        }
                        DisplayGraphicStack();
                    }
                    break;
                case "BBI_RECTANGLE1":
                    {
                        HalconDotNet.HDrawingObject rect1 = HalconDotNet.HDrawingObject.CreateDrawingObject(HalconDotNet.HDrawingObject.HDrawingObjectType.RECTANGLE1, 100, 100, 210, 210);
                        rect1.SetDrawingObjectParams(new HalconDotNet.HTuple("color"), new HalconDotNet.HTuple("red"));
                        AttachDrawObj(rect1);
                    }
                    break;
                case "BBI_RECTANGLE2":
                    {
                        HalconDotNet.HDrawingObject rect2 = HalconDotNet.HDrawingObject.CreateDrawingObject(HalconDotNet.HDrawingObject.HDrawingObjectType.RECTANGLE2, 100, 100, 0, 100, 50);
                        rect2.SetDrawingObjectParams(new HalconDotNet.HTuple("color"), new HalconDotNet.HTuple("green"));
                        AttachDrawObj(rect2);
                    }
                    break;
                case "BBI_CIRCLE":
                    {
                        HalconDotNet.HDrawingObject circle = HalconDotNet.HDrawingObject.CreateDrawingObject(HalconDotNet.HDrawingObject.HDrawingObjectType.CIRCLE, 200, 200, 50);
                        circle.SetDrawingObjectParams(new HalconDotNet.HTuple("color"), new HalconDotNet.HTuple("blue"));
                        AttachDrawObj(circle);
                    }
                    break;
                case "BBI_ELLIPSE":
                    {
                        HalconDotNet.HDrawingObject elipse = HalconDotNet.HDrawingObject.CreateDrawingObject(HalconDotNet.HDrawingObject.HDrawingObjectType.ELLIPSE, 50, 50, 0, 150, 50);
                        elipse.SetDrawingObjectParams(new HalconDotNet.HTuple("color"), new HalconDotNet.HTuple("yellow"));
                        AttachDrawObj(elipse);
                    }
                    break;
                case "BBI_LINE":
                    {
                        //HalconDotNet.HDrawingObject line = HalconDotNet.HDrawingObject.CreateDrawingObject(HalconDotNet.HDrawingObject.HDrawingObjectType.LINE, 50, 50, 150, 200);
                        //line.SetDrawingObjectParams(new HalconDotNet.HTuple("color"), new HalconDotNet.HTuple("magenta"));
                        //AttachDrawObj(line); //直线参数有些问题
                    }
                    break;
                case "BBI_COLORRED":
                    {
                        if (_selectedDrawingObject != null
                         && _selectedDrawingObject.IsInitialized())
                        {
                            _selectedDrawingObject.SetDrawingObjectParams(new HalconDotNet.HTuple("color"), new HalconDotNet.HTuple("red"));
                        }
                    }
                    break;
                case "BBI_COLORGREEN":
                    {
                        if (_selectedDrawingObject != null
                        && _selectedDrawingObject.IsInitialized())
                        {
                            _selectedDrawingObject.SetDrawingObjectParams(new HalconDotNet.HTuple("color"), new HalconDotNet.HTuple("green"));
                        }
                    }
                    break;
                case "BBI_COLORBLUE":
                    {
                        if (_selectedDrawingObject != null
                        && _selectedDrawingObject.IsInitialized())
                        {
                            _selectedDrawingObject.SetDrawingObjectParams(new HalconDotNet.HTuple("color"), new HalconDotNet.HTuple("blue"));
                        }
                    }
                    break;
                case "BBI_COLORYELLOW":
                    {
                        if (_selectedDrawingObject != null
                        && _selectedDrawingObject.IsInitialized())
                        {
                            _selectedDrawingObject.SetDrawingObjectParams(new HalconDotNet.HTuple("color"), new HalconDotNet.HTuple("yellow"));
                        }
                    }
                    break;
                case "BBI_CONTINUOUSLINE":
                    {
                        if (_selectedDrawingObject != null
                          && _selectedDrawingObject.IsInitialized())
                        {
                            _selectedDrawingObject.SetDrawingObjectParams(new HalconDotNet.HTuple("line_style"), new HalconDotNet.HTuple());
                        }
                    }
                    break;
                case "BBI_DASHEDLINE":
                    {
                        if (_selectedDrawingObject != null
                           && _selectedDrawingObject.IsInitialized())
                        {
                            _selectedDrawingObject.SetDrawingObjectParams(new HalconDotNet.HTuple("line_style"), new HalconDotNet.HTuple(20, 5));
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region MemoEdit

        /// <summary>
        /// 更新MemoEdit控件
        /// [注:备注信息控件]
        /// </summary>
        /// <param name="me"></param>
        /// <param name="lan"></param>
        /// <param name="info"></param>
        private void UpdateMemoEdit(DevExpress.XtraEditors.MemoEdit me, ProCommon.Communal.Language lan,string info)
        {
            if (me != null
               && me.Tag != null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = me.Tag.ToString();
                me.Text = (isChs ? Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str))+ info;
            }
        }

        #endregion

        #region 显示图层图形

        protected virtual void AttachDrawObj(HalconDotNet.HDrawingObject dobj)
        {
            _drawObjectList.Add(dobj);

            dobj.OnDrag(_myIconicAction.UserAction);
            dobj.OnResize(_myIconicAction.UserAction);
            dobj.OnSelect(OnSelectDrawingObject);
            dobj.OnAttach(_myIconicAction.UserAction);

            this.hwndcImgRsltView.HalconWindow.AttachDrawingObjectToWindow(dobj);
        }

        /// <summary>
        /// 添加图形对象到图形栈
        /// </summary>
        /// <param name="hobj"></param>
        public void AddToStack(HalconDotNet.HObject hobj)
        {
            lock (_stackLock)
            {
                _graphicStack.Push(hobj);
            }
        }

        /// <summary>
        /// 绘制对象选择事件
        /// </summary>
        /// <param name="dobj"></param>
        /// <param name="hwnd"></param>
        /// <param name="type"></param>
        private void OnSelectDrawingObject(HalconDotNet.HDrawingObject dobj, HalconDotNet.HWindow hwnd, string type)
        {
            _selectedDrawingObject = dobj;
            _myIconicAction.UserAction(dobj, hwnd, type);
        }

        /// <summary>
        /// 显示图形栈里的图形对象
        /// </summary>
        public void DisplayResult()
        {
            try
            {
                this.hwndcImgRsltView.BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate ()
                {
                    DisplayGraphicStack();                 
                }));
            }
            catch (HalconDotNet.HalconException hex)
            {
                System.Windows.Forms.MessageBox.Show("Halcon Error!\r\n" + hex.GetErrorMessage(), "错误信息",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Halcon Error!\r\n" + ex.ToString(), "错误信息",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 显示绘制对象
        /// </summary>
        private void DisplayGraphicStack()
        {
            lock (_stackLock)
            {
                HalconDotNet.HOperatorSet.SetSystem(new HalconDotNet.HTuple("flush_graphic"), new HalconDotNet.HTuple("false"));
                this.hwndcImgRsltView.HalconWindow.ClearWindow();
                if (HWndRsltIconic != null
                    && HWndRsltIconic.IsInitialized())
                    HalconDotNet.HOperatorSet.DispObj(HWndRsltIconic, this.hwndcImgRsltView.HalconWindow);
                while (_graphicStack.Count > 0)
                {
                    this.hwndcImgRsltView.HalconWindow.DispObj(_graphicStack.Pop());
                }

                HalconDotNet.HOperatorSet.SetSystem(new HalconDotNet.HTuple("flush_graphic"), new HalconDotNet.HTuple("true"));
            }

            this.hwndcImgRsltView.HalconWindow.DispCross(-10.0, -10.0, 3.0, 0.0);
        }       

        #endregion
    }

    /// <summary>
    /// 图形显示控件显示图形
    /// </summary>
    public interface IHWindowGraphicStack
    {
        void DisplayResult();

        void AddToStack(HalconDotNet.HObject hobj);
    }

    /// <summary>
    /// 图形对象动作
    /// </summary>
    public class UserIconicAction
    {
        protected internal ImageResultViewer HobjViewer { private set; get; }

        private UserIconicAction()
        {

        }

        protected internal UserIconicAction(ImageResultViewer viewer):this()
        {
            HobjViewer = viewer;
        }

        protected internal void UserAction(HalconDotNet.HDrawingObject hdrwobj,HalconDotNet.HWindow hwnd,string typ)
        {
            if(hdrwobj!=null)
            {
                try
                {
                    HalconDotNet.HTuple clsValue;
                    HalconDotNet.HOperatorSet.GetObjClass(hdrwobj.GetDrawingObjectIconic(), out clsValue);

                    //只显示轮廓
                    switch (clsValue.S)
                    {
                        case "region":
                            {
                                HalconDotNet.HObject hobj = hdrwobj.GetDrawingObjectIconic();
                                HalconDotNet.HObject contour;
                                HalconDotNet.HOperatorSet.GenContourRegionXld(hobj, out contour, new HalconDotNet.HTuple("border_holes"));
                                HobjViewer.AddToStack(contour);
                                HobjViewer.DisplayResult();
                            }
                            break;
                        case "xld_cont":
                            {
                                HobjViewer.AddToStack(hdrwobj.GetDrawingObjectIconic());
                                HobjViewer.DisplayResult();
                            }
                            break;
                        case "image":
                            break;
                        case "xld_poly":
                            break;
                        case "xld_parallel":
                            break;
                        default: break;
                    } 
                }
                catch(HalconDotNet.HalconException hex)
                {
                    System.Windows.Forms.MessageBox.Show("Halcon Error!\r\n" + hex.GetErrorMessage(), "错误信息",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }
    }
}
