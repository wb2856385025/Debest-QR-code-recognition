using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       UIManager
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Manager
 * File      Name：       UIManager
 * Creating  Time：       1/15/2020 5:52:49 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Manager
{
    public delegate void SwitchWindowDel(int wndIdx);

    /// <summary>
    /// 界面管理器
    /// [管理系统界面交互]
    /// </summary>
    public class UIManager
    {
        #region 静态单例

        static object _syncObj = new object();
        static UIManager _instance;
        public static UIManager Instance
        {
            get
            {
                lock (_syncObj)
                {
                    if (_instance == null)
                    { _instance = new UIManager(); }
                }

                return _instance;
            }
        }

        private UIManager()
        {
            Init();
        }

        #endregion

        public SwitchWindowDel SwitchMainAndSubWindow;          
        protected string _sysLogFilePath, _exLogFilePath; //系统日志文件路径,异常日志文件路径   
        protected bool _isChineseLanguage;
        private char _barcodeSeparatorChar;
        private System.ComponentModel.BindingList<Pro2DBarcode.Data.BarcodeResult> _barcodeResultBList;

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            InitFieldAndProperty();
            InitVisionControl();
        }

        /// <summary>
        /// 初始化字段和属性
        /// </summary>
        private void InitFieldAndProperty()
        {           
            _sysLogFilePath = Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.SystemLogFilePath;
            _exLogFilePath = Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ExceptionLogFilePath;
            _isChineseLanguage = (Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.LanguageVersion == ProCommon.Communal.Language.Chinese);
            _barcodeSeparatorChar = Convert.ToChar(Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ResultSplitStr);
            _barcodeResultBList = new System.ComponentModel.BindingList<Data.BarcodeResult>();
            UI.FrmMain.Instance.barcodeResultBindingSource.DataSource = _barcodeResultBList;
        }

        /// <summary>
        /// 视觉控件相关的初始化
        /// </summary>
        private void InitVisionControl()
        {
            //预设Halcon窗体的分辨率
            HalconDotNet.HOperatorSet.SetSystem(new HalconDotNet.HTuple("width"), new HalconDotNet.HTuple(5000));
            HalconDotNet.HOperatorSet.SetSystem(new HalconDotNet.HTuple("height"), new HalconDotNet.HTuple(5000));
        }

        /// <summary>
        /// 控制变量对象属性值改变的回调函数
        /// [注:表示控制器连接状态或运行模式的控制变量的属性]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Connected_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                string str1 = _isChineseLanguage ? "连接成功" : "OnConnection";
                string str2= _isChineseLanguage ? "连接失败" : "OffConnection";

                #region 连接属性或输入控制变量属性值改变事件
                if (e.PropertyName == "IsConnected" || e.PropertyName == "InNewValue")
                {
                    //异步跨线程委托[MethodInvoker/EventHandler]
                    UI.FrmMain.Instance.BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate {
                        #region 相机连接属性
                        if (sender.GetType() == typeof(ProCommon.Communal.Camera))
                        {
                            ProCommon.Communal.Camera camera = sender as ProCommon.Communal.Camera;
                            if (camera != null)
                            {
                                for(int i=0;i< UI.FrmMain.Instance.bsbiCameraState.Manager.Items.Count; i++)
                                {
                                    DevExpress.XtraBars.BarEditItem bei = UI.FrmMain.Instance.bsbiCameraState.Manager.Items[i] as DevExpress.XtraBars.BarEditItem;
                                    if(bei!=null
                                    && bei.Tag!=null)
                                    {
                                        if(bei.Tag.ToString()==camera.ID)
                                        {
                                            bei.EditValue = camera.IsConnected;
                                            break;
                                        }
                                    }
                                }

                                if (camera.IsConnected)
                                {
                                    //Logic.LogHandle.Instance.Add(camera.Name, "连接成功");
                                    
                                }
                                else
                                {
                                    //Logic.LogHandle.Instance.Add(camera.Name, "连接失败");
                                }
                            }
                        }
                        #endregion

                        #region 板卡连接属性
                        else if (sender.GetType() == typeof(ProCommon.Communal.BoardCtrller))
                        {
                            ProCommon.Communal.BoardCtrller motionBoard = sender as ProCommon.Communal.BoardCtrller;
                            if (motionBoard != null)
                            {
                                for (int i = 0; i < UI.FrmMain.Instance.bsbiBoardState.Manager.Items.Count; i++)
                                {
                                    DevExpress.XtraBars.BarEditItem bei = UI.FrmMain.Instance.bsbiBoardState.Manager.Items[i] as DevExpress.XtraBars.BarEditItem;
                                    if (bei != null
                                        && bei.Tag != null)
                                    {
                                        if (bei.Tag.ToString() == motionBoard.ID)
                                        {
                                            bei.EditValue = motionBoard.IsConnected;
                                            break;
                                        }
                                    }
                                }

                                if (motionBoard.IsConnected)
                                {
                                    //Logic.LogHandle.Instance.Add(motionBoard.Name, "连接成功");
                                }
                                else
                                {
                                    //Logic.LogHandle.Instance.Add(motionBoard.Name, "连接失败");
                                }
                            }
                        }
                        #endregion

                        #region Socket连接属性
                        else if (sender.GetType() == typeof(ProCommon.Communal.ComWrappedSocket))
                        {
                            ProCommon.Communal.ComWrappedSocket comSocket = sender as ProCommon.Communal.ComWrappedSocket;
                            if (comSocket != null)
                            {
                               
                            }
                        }
                        #endregion

                        #region SerialPort连接属性
                        else if (sender.GetType() == typeof(ProCommon.Communal.ComWrappedSerialPort))
                        {
                            ProCommon.Communal.ComWrappedSerialPort comSerialPort = sender as ProCommon.Communal.ComWrappedSerialPort;
                            if (comSerialPort != null)
                            {
                                for (int i = 0; i < UI.FrmMain.Instance.bsbiCameraState.Manager.Items.Count; i++)
                                {
                                    DevExpress.XtraBars.BarEditItem bei = UI.FrmMain.Instance.bsbiCameraState.Manager.Items[i] as DevExpress.XtraBars.BarEditItem;
                                    if (bei != null
                                    && bei.Tag != null)
                                    {
                                        if (bei.Tag.ToString() == comSerialPort.ID)
                                        {
                                            bei.EditValue = comSerialPort.IsConnected;
                                            break;
                                        }
                                    }
                                }

                                if (comSerialPort.IsConnected)
                                {
                                    //Logic.LogHandle.Instance.Add(camera.Name, "连接成功");

                                }
                                else
                                {
                                    //Logic.LogHandle.Instance.Add(camera.Name, "连接失败");
                                }
                            }
                        }
                        #endregion

                        #region Web连接属性
                        else if (sender.GetType() == typeof(ProCommon.Communal.ComWrappedWeb))
                        {
                            ProCommon.Communal.ComWrappedWeb comWeb = sender as ProCommon.Communal.ComWrappedWeb;
                            if (comWeb != null)
                            {
                                if (comWeb.IsConnected)
                                {
                                    //Logic.LogHandle.Instance.Add(comWeb.Name, "连接成功");
                                }
                                else
                                {
                                    //Logic.LogHandle.Instance.Add(comWeb.Name, "连接失败");
                                }
                            }
                        }
                        #endregion

                        #region 输入控制变量[运行模式]属性
                        else if (sender.GetType() == typeof(ProCommon.Communal.InVarObj))
                        {
                            ProCommon.Communal.InVarObj ctrlInVarObj = (ProCommon.Communal.InVarObj)sender;
                            if (ctrlInVarObj != null)
                            {
                               
                            }
                        }
                        #endregion
                    }));
                }
                #endregion
            }
            catch (Exception ex)
            {
                ProCommon.Communal.LogWriter.WriteException(_exLogFilePath, ex);
                ProCommon.Communal.LogWriter.WriteLog(_sysLogFilePath, string.Format("错误：UI连接状态更新失败!\n异常描述:{0}", ex.Message));
            }
        }
      
        /// <summary>
        /// 添加运行日志
        /// </summary>
        /// <param name="itemname"></param>
        /// <param name="description"></param>
        public void AddRunLog(string itemname, string description)
        {
            AddRunLog("", itemname, description);
        }

        /// <summary>
        /// 添加运行日志
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="itemname"></param>
        /// <param name="description"></param>
        public void AddRunLog(string sn, string itemname, string description)
        {

        }

        public void DisplayObject(int viewerIdx, HalconDotNet.HObject hObj)
        {
            if (UI.FrmMain.Instance == null
                || hObj==null
                || (!hObj.IsInitialized())) return;
            UI.FrmMain.Instance.Invoke(new System.Windows.Forms.MethodInvoker(delegate
            {
                HalconDotNet.HTuple objClass;
                HalconDotNet.HTuple w = new HalconDotNet.HTuple(), h = new HalconDotNet.HTuple();
                HalconDotNet.HOperatorSet.GetObjClass(hObj, out objClass);
                if (objClass.TupleEqual(new HalconDotNet.HTuple("image")))
                {
                    HalconDotNet.HOperatorSet.GetImageSize(hObj, out w, out h);
                }

                switch (viewerIdx)
                {
                    case -1:
                        {
                            try
                            {
                                if (objClass.TupleEqual(new HalconDotNet.HTuple("image")))
                                    HalconDotNet.HOperatorSet.SetPart(UI.FrmMain.Instance.FrmSetRoutinePara.hwndDisplay.HalconWindow, 0, 0, h - 1, w - 1);

                                HalconDotNet.HOperatorSet.DispObj(hObj, UI.FrmMain.Instance.FrmSetRoutinePara.hwndDisplay.HalconWindow);
                            }
                            catch (HalconDotNet.HalconException hex) { }

                        }
                        break;
                    case 0:
                        {
                            try
                            {
                                if (objClass.TupleEqual(new HalconDotNet.HTuple("image")))
                                    HalconDotNet.HOperatorSet.SetPart(UI.FrmMain.Instance.hwndDisplay.HalconWindow, 0, 0, h - 1, w - 1);

                                HalconDotNet.HOperatorSet.DispObj(hObj, UI.FrmMain.Instance.hwndDisplay.HalconWindow);
                            }
                            catch (HalconDotNet.HalconException hex) { }

                        }
                        break;
                    default:
                        break;
                }
            }));
        }

        /// <summary>
        /// 在指定窗口显示信息
        /// [注:同步处理]
        /// </summary>
        /// <param name="viewerIdx"></param>
        /// <param name="hv_String"></param>
        /// <param name="hv_CoordSystem"></param>
        /// <param name="hv_Row"></param>
        /// <param name="hv_Column"></param>
        /// <param name="hv_Color"></param>
        /// <param name="hv_Box"></param>
        public void DispMessage(int viewerIdx, HalconDotNet.HTuple hv_String, HalconDotNet.HTuple hv_CoordSystem, HalconDotNet.HTuple hv_Row, HalconDotNet.HTuple hv_Column, HalconDotNet.HTuple hv_Color, HalconDotNet.HTuple hv_Box)
        {
            if (UI.FrmMain.Instance == null) return;
            UI.FrmMain.Instance.Invoke(new System.Windows.Forms.MethodInvoker(delegate
            {
                switch (viewerIdx)
                {
                    case -1:
                        ProVision.Communal.Functions.DispMessage(UI.FrmMain.Instance.FrmSetRoutinePara.hwndDisplay.HalconWindow,
                                                                hv_String, hv_CoordSystem, hv_Row, hv_Column, hv_Color, hv_Box);
                        break;
                    case 0:
                        ProVision.Communal.Functions.DispMessage(UI.FrmMain.Instance.hwndDisplay.HalconWindow,
                                                                 hv_String, hv_CoordSystem, hv_Row, hv_Column, hv_Color, hv_Box);
                        break;
                    default:
                        break;
                }

            }));
        }

        /// <summary>
        /// 更新界面上的处理结果
        /// [注:同步处理]
        /// </summary>
        /// <param name="viewerIdx"></param>
        /// <param name="imgresultOK"></param>
        /// <param name="elapseTime"></param>
        /// <param name="barcodeString"></param>
        internal void UpdateResult(int viewerIdx, Pro2DBarcode.Data.BarcodeResult barcodeResult)
        {
            if (UI.FrmMain.Instance == null) return;
            UI.FrmMain.Instance.Invoke(new System.Windows.Forms.MethodInvoker(delegate
            {
                UI.FrmMain.Instance.lblInspectResult.ForeColor = barcodeResult.ImgResultOK ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                if (barcodeResult.ImgResultOK)
                    Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProOK += 1;
                else
                    Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProNG += 1;

                Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProTotal = Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProOK 
                + Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProNG;
                Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProYieldRatio = ((float)Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProOK) 
                / ((float)Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProTotal);
               
                UI.FrmMain.Instance.lblInspectResult.Text = barcodeResult.ImgResultOK ? "OK" : "NG";
                UI.FrmMain.Instance.btneProOK.EditValue = Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProOK;
                UI.FrmMain.Instance.btneProNG.EditValue = Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProNG;
                UI.FrmMain.Instance.btneProTotal.EditValue = Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProTotal;
                UI.FrmMain.Instance.btneProYieldRatio.EditValue = (Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProYieldRatio * 100).ToString("f2");
                UI.FrmMain.Instance.btneElapse.EditValue = System.Math.Round(barcodeResult.ElapseTime, 2);
              
                string[] strArr = barcodeResult.BarcodeString.Split(_barcodeSeparatorChar);
                if(strArr!=null)
                {
                    _barcodeResultBList.Clear();
                    for (int i = 0; i < strArr.Length; i++)
                    {
                        Pro2DBarcode.Data.BarcodeResult bcr = new Data.BarcodeResult();
                        bcr.ImgProcessOK = true;
                        bcr.Number = i+1;
                        bcr.BarcodeString = strArr[i];
                        bcr.ElapseTime = System.Math.Round(barcodeResult.ElapseTime,2);
                        _barcodeResultBList.Add(bcr);
                    }
                }               
            }));
        }
        /// <summary>
        /// 更新界面上的处理结果
        /// [注:同步处理]
        /// </summary>
        /// <param name="viewerIdx"></param>
        /// <param name="imgresultOK"></param>
        /// <param name="elapseTime"></param>
        /// <param name="barcodeString"></param>
        internal void Update(int viewerIdx, Pro2DBarcode.Data.BarcodeResult barcodeResult)
        {
            if (UI.FrmMain.Instance == null) return;
            UI.FrmMain.Instance.Invoke(new System.Windows.Forms.MethodInvoker(delegate
            {
                UI.FrmMain.Instance.lblInspectResult.ForeColor = barcodeResult.ImgResultOK ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                if (barcodeResult.ImgResultOK)
                    Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProOK += 1;
                else
                    Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProNG += 1;

                Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProTotal = Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProOK
                + Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProNG;
                Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProYieldRatio = ((float)Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProOK)
                / ((float)Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProTotal);

                UI.FrmMain.Instance.lblInspectResult.Text = barcodeResult.ImgResultOK ? "OK" : "NG";
                UI.FrmMain.Instance.btneProOK.EditValue = Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProOK;
                UI.FrmMain.Instance.btneProNG.EditValue = Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProNG;
                UI.FrmMain.Instance.btneProTotal.EditValue = Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProTotal;
                UI.FrmMain.Instance.btneProYieldRatio.EditValue = (Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProYieldRatio * 100).ToString("f2");
                UI.FrmMain.Instance.btneElapse.EditValue = System.Math.Round(barcodeResult.ElapseTime, 2);

                string[] strArr = barcodeResult.BarcodeString.Split(_barcodeSeparatorChar);
                if (strArr != null)
                {
                    _barcodeResultBList.Clear();
                    for (int i = 0; i < strArr.Length; i++)
                    {
                        Pro2DBarcode.Data.BarcodeResult bcr = new Data.BarcodeResult();
                        bcr.ImgProcessOK = true;
                        bcr.Number = i + 1;
                        bcr.BarcodeString = strArr[i];
                        bcr.ElapseTime = System.Math.Round(barcodeResult.ElapseTime, 2);
                        _barcodeResultBList.Add(bcr);
                    }
                }
            }));
        }

        internal void ResetResult()
        {
            Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProOK = 0;
            Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProNG = 0;
            Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProTotal = 0;
            Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ProYieldRatio = 0.0f;
            if(_barcodeResultBList!=null)
                _barcodeResultBList.Clear();
        }

        /// <summary>
        /// 设置窗口的画模式
        /// [注:同步处理]
        /// </summary>
        /// <param name="viewerIdx"></param>
        /// <param name="mode"></param>
        public void SetDraw(int viewerIdx, HalconDotNet.HTuple mode)
        {
            if (UI.FrmMain.Instance == null) return;
            UI.FrmMain.Instance.Invoke(new System.Windows.Forms.MethodInvoker(delegate
            {
                switch (viewerIdx)
                {
                    case -1:
                        HalconDotNet.HOperatorSet.SetDraw(UI.FrmMain.Instance.FrmSetRoutinePara.hwndDisplay.HalconWindow, mode);
                        break;
                    case 0:
                        HalconDotNet.HOperatorSet.SetDraw(UI.FrmMain.Instance.hwndDisplay.HalconWindow, mode);
                        break;
                    default:
                        break;
                }

            }));
        }

        /// <summary>
        /// 设置窗口画的颜色
        /// [注:同步处理]
        /// </summary>
        /// <param name="viewerIdx"></param>
        /// <param name="colr"></param>
        public void SetColor(int viewerIdx, HalconDotNet.HTuple colr)
        {
            if (UI.FrmMain.Instance == null) return;
            UI.FrmMain.Instance.Invoke(new System.Windows.Forms.MethodInvoker(delegate
            {
                switch (viewerIdx)
                {
                    case -1:
                        HalconDotNet.HOperatorSet.SetColor(UI.FrmMain.Instance.FrmSetRoutinePara.hwndDisplay.HalconWindow, colr);
                        break;
                    case 0:
                        HalconDotNet.HOperatorSet.SetColor(UI.FrmMain.Instance.hwndDisplay.HalconWindow, colr);
                        break;
                    default:
                        break;
                }
            }));
        }

        /// <summary>
        /// 清空图像窗口
        /// [注:同步处理]
        /// </summary>
        /// <param name="viewerIdx"></param>
        public void ClearWindow(int viewerIdx)
        {
            if (UI.FrmMain.Instance == null) return;
            UI.FrmMain.Instance.Invoke(new System.Windows.Forms.MethodInvoker(delegate
            {
                switch (viewerIdx)
                {
                    case -1:
                        HalconDotNet.HOperatorSet.ClearWindow(UI.FrmMain.Instance.FrmSetRoutinePara.hwndDisplay.HalconWindow);
                        break;
                    case 0:
                        HalconDotNet.HOperatorSet.ClearWindow(UI.FrmMain.Instance.hwndDisplay.HalconWindow);
                        break;
                    default:
                        break;
                }

            }));
        }

    }
}
