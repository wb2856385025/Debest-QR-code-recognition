using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       Device_Camera
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Device
 * File      Name：       Device_Camera
 * Creating  Time：       1/15/2020 6:43:27 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Device
{
    /// <summary>
    /// 相机设备
    /// [管理所有相机设备的逻辑处理]
    /// </summary>
    public class Device_Camera : Device
    {
        public Device_Camera(Pro2DBarcode.Config.ConfigCamera cfgCamera,Pro2DBarcode.Config.ConfigSystem cfgSystem)
        {
            _cfgSystem = cfgSystem;
            _cameraList = cfgCamera.CameraList;
            IsChinese = cfgSystem.LanguageVersion == ProCommon.Communal.Language.Chinese;
            if (_cameraList != null
                && _cameraList.Count > 0)
            {
                //--------------------相机驱动若无断线重连功能,则启用定时器重新连接-----------------------------//
                InitTimer();   //初始化定时器               
                CamHandleListNew = new SortedList<string, ProDriver.APIHandle.CameraAPIHandle>();
                _camGrabbedList = new SortedList<string, bool>();
                for (int i = 0; i < _cameraList.Count; i++)
                {
                    _cameraList[i].PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Camera_PropertyChanged);
                }

                if(_comSerialFor2DCode==null)
                    _comSerialFor2DCode = Pro2DBarcode.Manager.SystemManager.Instance.ComSerialPortFor2DCode;               
            }

            _resultNGStr = _cfgSystem.ResultNGStr;
            _resultSeparator = _cfgSystem.ResultSplitStr;
            _serialPortPackEndStr = _cfgSystem.SerialPortPackEndStr;
        }
        private void Camera_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
            {
                ProCommon.Communal.Camera cam = sender as ProCommon.Communal.Camera;

                if (cam != null
                    && (!cam.IsConnected)
                    && (!_isSystemStop))
                {
                    if (!_timer.Enabled)
                        StartTimer();
                }
            }
        }
               
        public System.Collections.Generic.SortedList<string, ProDriver.APIHandle.CameraAPIHandle> CamHandleListNew;
        private System.Collections.Generic.SortedList<string, bool> _camGrabbedList;
        private bool _isCameraInitialized;       
        private ProCommon.Communal.CameraList _cameraList { set; get; }

        private ProCommon.Communal.ComWrappedSerialPort _comSerialFor2DCode;
        private Pro2DBarcode.Device.Device_SerialPort _devSerialPort;
        private Pro2DBarcode.Config.ConfigSystem _cfgSystem;

        private Pro2DBarcode.Algorithm.ImageProcess_InspectDataCode2D _imgProcess2DBarcode;
        private HalconDotNet.HObject _imgFor2DBarcode;    

        /// <summary>
        /// 二维码识别结果
        /// </summary>
        private Pro2DBarcode.Data.BarcodeResult _barcodeResult;

        /// <summary>
        /// 用户自定义结束符
        /// </summary>
        private readonly string _serialPortPackEndStr;

        /// <summary>
        /// 识别NG时对应的用户定义字符串
        /// </summary>
        private readonly string _resultNGStr;

        /// <summary>
        /// 多识别结果分隔符
        /// </summary>
        private readonly string _resultSeparator;

        /// <summary>
        /// 相机设备是否调试模式标记
        /// [IsDebug=true,表示相机接收到的图像显示在调试窗口;
        /// IsDebug=false,表示相机接收到的图像显示在分屏窗口]
        /// </summary>
        public bool IsDebug { set; get; }

        /// <summary>
        /// 是否重新加载参数
        /// </summary>
        public bool IsInitProcess { set; get; }

        /// <summary>
        /// 方法:初始化(覆写实现)
        /// </summary>
        public override void DoIni()
        {
            try
            {
                _barcodeResult = new Data.BarcodeResult();

                if (_cameraList != null
                    && _cameraList.Count > 0)
                {
                    for (int i = 0; i < _cameraList.Count; i++)
                    {
                        ProDriver.APIHandle.CameraAPIHandle camAPI = new ProDriver.APIHandle.CameraAPIHandle(_cameraList[i]);
                        camAPI.ImageGrabbedEvt += Device_Camera_ImageGrabbedEvt;
                        CamHandleListNew.Add(_cameraList[i].ID,camAPI);
                        _camGrabbedList.Add(_cameraList[i].ID, false);

                        if (!_cameraList[i].IsConnected)
                        {
                            CamHandleListNew[_cameraList[i].ID].EnumerateCameraList(); //枚举相机列表
                            if (CamHandleListNew[_cameraList[i].ID].GetCameraBySN(_cameraList[i].SerialNo))
                            {
                                if (!CamHandleListNew[_cameraList[i].ID].Open())
                                {
                                    if (!_timer.Enabled)
                                        StartTimer();  //第一连接失败，需要启动定时器，因为相机连接状态更新的值与原来的值一样，不会触发属性值改变事件
                                    return;
                                }
                                else
                                {
                                    _cameraList[i].IsConnected = true;
                                }                               
                            }
                            else { continue; }
                        }
                    }

                    if (!_isCameraInitialized)
                    {
                        InitCamera();
                        _isCameraInitialized = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ProCommon.Communal.LogWriter.WriteException(_exLogFilePath, ex);
                ProCommon.Communal.LogWriter.WriteLog(_sysLogFilePath, string.Format("错误：初始化相机设备失败!\n异常描述:{0}", ex.Message));
                throw new ProCommon.Communal.InitException(ToString(), "初始化异常\n" + ex.Message);
            }
        }
        private void Device_Camera_ImageGrabbedEvt(ProCommon.Communal.Camera cam, HalconDotNet.HObject hobj)
        {
            if(Manager.SystemManager.Instance.IsRunning)
            {
                _camGrabbedList[cam.ID] = true;

                if (IsDebug)
                {
                    ImageProcessUnderDebugging(cam.Number, hobj);
                }
                else
                {
                    bool rt = true;
                    for (int i = 0; i < _cameraList.Count; i++)
                        rt &= _camGrabbedList[_cameraList[i].ID];
                    if (rt)
                    {
                        ImageProcessUnderRunning();
                        for (int i = 0; i < _cameraList.Count; i++)
                            _camGrabbedList[_cameraList[i].ID] = false;
                    }
                }

                if (Manager.SystemManager.Instance.IsRunOnce)
                    Manager.SystemManager.Instance.IsRunning = false;
            }
        }

        /// <summary>
        /// 方法:开启(覆写实现)
        /// </summary>
        public override void DoStart()
        {
            try
            {
                if (_cameraList != null
                   && _cameraList.Count > 0)
                {
                    string camName = string.Empty;

                    for (int i = 0; i < _cameraList.Count; i++)
                    {
                        if (!_cameraList[i].IsConnected)
                        {
                            camName += "\n" + _cameraList[i].Name;
                        }
                    }

                    if (!string.IsNullOrEmpty(camName)
                        && (!_isShowing))
                    {
                        _isShowing = true;
                        string txt1 = IsChinese ? "相机:" : "Camera:";
                        string txt2= IsChinese ? "\n连接失败 !" : "\nConnect falied !";
                        string caption = IsChinese ? "警告信息" : "Warning Message";
                      
                        ProCommon.UserDefForm.FrmMsgBox.Show(txt1 + camName + txt2, caption, 
                            ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, IsChinese);
                    }
                }
            }
            catch (Exception ex)
            {
                ProCommon.Communal.LogWriter.WriteException(_exLogFilePath, ex);
                ProCommon.Communal.LogWriter.WriteLog(_sysLogFilePath, string.Format("错误：启动相机设备失败!\n异常描述:{0}", ex.Message));
                throw new ProCommon.Communal.StartException(ToString(), "启动异常\n" + ex.Message);
            }
        }

        /// <summary>
        /// 图像处理
        /// [注:运行模式下]
        /// </summary>
        private void ImageProcessUnderRunning()
        {
            //处理结果复位          
            _barcodeResult.ImgProcessOK = false;
            _barcodeResult.BarcodeString = null;
            _barcodeResult.ElapseTime = 0.0;

            if (_imgProcess2DBarcode == null)
            {
                _imgProcess2DBarcode=new Algorithm.ImageProcess_InspectDataCode2D();
                _imgProcess2DBarcode.IsChinese = _cfgSystem.LanguageVersion == ProCommon.Communal.Language.Chinese;
                _imgProcess2DBarcode.Para2DBarcode = Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode;
                _imgProcess2DBarcode.InitProcess();
            }

            if (IsInitProcess)
            {
                _imgProcess2DBarcode.InitProcess();
                IsInitProcess = false;
            }

            //当前只有一台相机,故而只有一组图像数据;若是多组,则根据逻辑要求进行处理
            _imgFor2DBarcode = CamHandleListNew[_cameraList[0].ID].HoImage;

            if (_imgFor2DBarcode != null
             && _imgFor2DBarcode.IsInitialized())
            {
                _imgProcess2DBarcode.SetHwndIndex(0);
                _imgProcess2DBarcode.SetCameraIndex(0);
                _imgProcess2DBarcode.SetEnableAlgorithm(true);//强制为启用图像处理
                _barcodeResult.ImgProcessOK = _imgProcess2DBarcode.Process(_imgFor2DBarcode);

                int decodeCount = 0;

                if (_barcodeResult.ImgProcessOK)
                {
                    decodeCount = _imgProcess2DBarcode.DecodeStrings.Length;

                    for (int i = 0; i < decodeCount; i++)
                    {
                        if (i != (decodeCount - 1))
                        {
                            if (_imgProcess2DBarcode.DecodeStrings[i].S == "")
                            {
                                _barcodeResult.BarcodeString += _resultNGStr + _resultSeparator;
                            }
                            else
                            {
                                _barcodeResult.BarcodeString += _imgProcess2DBarcode.DecodeStrings[i].S + _resultSeparator;
                            }
                        }
                        else
                        {
                            if (_imgProcess2DBarcode.DecodeStrings[i].S == "")
                            {
                                _barcodeResult.BarcodeString += _resultNGStr;
                            }
                            else
                            {
                                _barcodeResult.BarcodeString += _imgProcess2DBarcode.DecodeStrings[i].S;
                            }
                        }
                    }

                    _barcodeResult.ImgResultOK = _imgProcess2DBarcode.ResultOK;  
                                  
                    if (_barcodeResult.ImgResultOK)
                    {
                        if (_imgProcess2DBarcode.Para2DBarcode.IsSaveImageOK)
                        {
                            if (!System.IO.Directory.Exists(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageOK))
                                System.IO.Directory.CreateDirectory(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageOK);
                            HalconDotNet.HOperatorSet.WriteImage(_imgFor2DBarcode, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                                new HalconDotNet.HTuple(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageOK + "\\ImgOK_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                        }
                    }
                    else
                    {
                        if (_imgProcess2DBarcode.Para2DBarcode.IsSaveImageNG)
                        {
                            if (!System.IO.Directory.Exists(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageNG))
                                System.IO.Directory.CreateDirectory(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageNG);
                            HalconDotNet.HOperatorSet.WriteImage(_imgFor2DBarcode, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                                new HalconDotNet.HTuple(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageNG + "\\ImgNG_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                        }
                    }                  
                }
                else
                {
                    for(int i=0;i<_imgProcess2DBarcode.InspectAreaCount;i++)
                    {
                        if(i!= (_imgProcess2DBarcode.InspectAreaCount-1))
                        {
                            _barcodeResult.BarcodeString += _resultNGStr + _resultSeparator;
                        }
                        else
                        {
                            _barcodeResult.BarcodeString += _resultNGStr;
                        }
                    }
                       
                     
                    if (_imgProcess2DBarcode.Para2DBarcode.IsSaveImageNG)
                    {
                        if (!System.IO.Directory.Exists(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageNG))
                            System.IO.Directory.CreateDirectory(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageNG);
                        HalconDotNet.HOperatorSet.WriteImage(_imgFor2DBarcode, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                            new HalconDotNet.HTuple(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageNG + "\\ImgNG_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                    }
                }

                _barcodeResult.ElapseTime = _imgProcess2DBarcode.DecodeTime.D;
                Pro2DBarcode.Manager.UIManager.Instance.UpdateResult(0, _barcodeResult);

                //追加结束符
                _barcodeResult.BarcodeString += _serialPortPackEndStr;

                if (_devSerialPort == null)
                    _devSerialPort = (Pro2DBarcode.Device.Device_SerialPort)Pro2DBarcode.Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.SerialPort];
                //返回字符串进行包装后通过串口发送              
                _devSerialPort.SerialPortHandleList[_comSerialFor2DCode.ID].Send(_barcodeResult.BarcodeString);

                if (_imgProcess2DBarcode.Para2DBarcode.IsSaveImageAll)
                {
                    if (!System.IO.Directory.Exists(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageAll))
                        System.IO.Directory.CreateDirectory(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageAll);
                    HalconDotNet.HOperatorSet.WriteImage(_imgFor2DBarcode, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                        new HalconDotNet.HTuple(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageAll + "\\ImgAll_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                }
            }
        }

        /// <summary>
        /// 图像处理
        /// [注:调试模式下]
        /// </summary>
        /// <param name="camIdx"></param>
        /// <param name="hobj"></param>
        private void ImageProcessUnderDebugging(int camIdx, HalconDotNet.HObject hobj)
        {
            //处理结果复位
            _barcodeResult.ImgProcessOK = false;
            _barcodeResult.BarcodeString = null;
            _barcodeResult.ElapseTime = 0.0;

            if (_imgProcess2DBarcode == null)
            {
                _imgProcess2DBarcode = new Algorithm.ImageProcess_InspectDataCode2D();
                _imgProcess2DBarcode.IsChinese = _cfgSystem.LanguageVersion == ProCommon.Communal.Language.Chinese;
                _imgProcess2DBarcode.Para2DBarcode = Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode;
                _imgProcess2DBarcode.InitProcess();
            }

            if (IsInitProcess)
            {
                _imgProcess2DBarcode.InitProcess();
                IsInitProcess = false;
            }

            if (hobj != null
             && hobj.IsInitialized())
            {
                _imgProcess2DBarcode.SetHwndIndex(-1);
                _imgProcess2DBarcode.SetCameraIndex(camIdx);
                _imgProcess2DBarcode.SetEnableAlgorithm(EnableAlgorithm);
                _barcodeResult.ImgProcessOK = _imgProcess2DBarcode.Process(hobj);

                if(_barcodeResult.ImgProcessOK)
                {
                    _barcodeResult.ImgResultOK = _imgProcess2DBarcode.ResultOK;

                    if (_barcodeResult.ImgResultOK)
                    {
                        //图像处理结果为OK,获取识别到的字符串  
                        int decodeCount = _imgProcess2DBarcode.DecodeStrings.Length;
                        for (int i = 0; i < decodeCount; i++)
                        {
                            if(i!= (decodeCount-1))
                            {
                                if (_imgProcess2DBarcode.DecodeStrings[i].S == "")
                                {
                                    _barcodeResult.BarcodeString += _resultNGStr + _resultSeparator;
                                }
                                else
                                {
                                    _barcodeResult.BarcodeString += _imgProcess2DBarcode.DecodeStrings[i].S + _resultSeparator;
                                }
                            }
                            else
                            {
                                if (_imgProcess2DBarcode.DecodeStrings[i].S == "")
                                {
                                    _barcodeResult.BarcodeString += _resultNGStr;
                                }
                                else
                                {
                                    _barcodeResult.BarcodeString += _imgProcess2DBarcode.DecodeStrings[i].S;
                                }
                            }                           
                        }

                        if (_imgProcess2DBarcode.Para2DBarcode.IsSaveImageOK)
                        {
                            if (!System.IO.Directory.Exists(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageOK))
                                System.IO.Directory.CreateDirectory(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageOK);
                            HalconDotNet.HOperatorSet.WriteImage(hobj, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                                new HalconDotNet.HTuple(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageOK + "\\ImgOK_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                        }
                    }
                    else
                    {
                        _barcodeResult.BarcodeString = _resultNGStr;
                        if (_imgProcess2DBarcode.Para2DBarcode.IsSaveImageNG)
                        {
                            if (!System.IO.Directory.Exists(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageNG))
                                System.IO.Directory.CreateDirectory(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageNG);
                            HalconDotNet.HOperatorSet.WriteImage(hobj, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                                new HalconDotNet.HTuple(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageNG + "\\ImgNG_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                        }
                    }
                }
                else
                {
                    _barcodeResult.BarcodeString = _resultNGStr;
                    if (_imgProcess2DBarcode.Para2DBarcode.IsSaveImageNG)
                    {
                        if (!System.IO.Directory.Exists(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageNG))
                            System.IO.Directory.CreateDirectory(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageNG);
                        HalconDotNet.HOperatorSet.WriteImage(hobj, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                            new HalconDotNet.HTuple(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageNG + "\\ImgNG_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                    }
                }

                //追加结束符
                _barcodeResult.BarcodeString += _serialPortPackEndStr;
                if (_devSerialPort == null)
                    _devSerialPort = (Pro2DBarcode.Device.Device_SerialPort)Pro2DBarcode.Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.SerialPort];
                //返回字符串进行包装后通过串口发送
                _devSerialPort.SerialPortHandleList[_comSerialFor2DCode.ID].Send(_barcodeResult.BarcodeString);

                if (_imgProcess2DBarcode.Para2DBarcode.IsSaveImageAll)
                {
                    if (!System.IO.Directory.Exists(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageAll))
                        System.IO.Directory.CreateDirectory(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageAll);
                    HalconDotNet.HOperatorSet.WriteImage(hobj, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                        new HalconDotNet.HTuple(_imgProcess2DBarcode.Para2DBarcode.PathForSaveImageAll + "\\ImgAll_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                }               
            }
        }

        /// <summary>
        /// 方法:停止(覆写实现)
        /// </summary>
        public override void DoStop()
        {
            try
            {
                _isSystemStop = true; //系统退出标记
                StopTimer();
                if (_cameraList != null
                  && _cameraList.Count > 0)
                {
                    for (int i = 0; i < _cameraList.Count; i++)
                    {
                        if (_cameraList[i].IsConnected)
                        {
                            CamHandleListNew[_cameraList[i].ID].StopGrab();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProCommon.Communal.LogWriter.WriteException(_exLogFilePath, ex);
                ProCommon.Communal.LogWriter.WriteLog(_sysLogFilePath, string.Format("错误：停止相机设备失败!\n异常描述:{0}", ex.Message));
                throw new ProCommon.Communal.StopException(this.ToString(), "停止异常\n" + ex.Message);
            }
        }

        /// <summary>
        /// 方法:释放(覆写实现)
        /// </summary>
        public override void DoRelease()
        {
            try
            {
                if (_cameraList != null
                  && _cameraList.Count > 0)
                {
                    for (int i = 0; i < _cameraList.Count; i++)
                    {
                        if (_cameraList[i].IsConnected)
                        {
                            CamHandleListNew[_cameraList[i].ID].Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProCommon.Communal.LogWriter.WriteException(_exLogFilePath, ex);
                ProCommon.Communal.LogWriter.WriteLog(_sysLogFilePath, string.Format("错误：释放相机设备失败!\n异常描述:{0}", ex.Message));
                throw new ProCommon.Communal.ReleaseException(this.ToString(), "释放异常\n" + ex.Message);
            }
        }
        public override string ToString()
        {
            return "DEVICE_CAMERA";
        }

        /// <summary>
        /// 方法：初始化相机资源
        /// </summary>
        private void InitCamera()
        {
            if (_cameraList != null
                   && _cameraList.Count > 0)
            {
                for (int i = 0; i < _cameraList.Count; i++)
                {
                    //设置采集模式:触发采集,软触发每次1帧-OK
                    if (!CamHandleListNew[_cameraList[i].ID].SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.SoftTrigger, 1))
                        continue;

                    //设置触发信号边缘:上升沿-OK,硬触发时启用
                    //if (!CamHandleListNew[_cameraList[i].ID].SetTriggerActivation(ProCommon.Communal.TriggerLogic.RaiseEdge))
                    //    continue;

                    //设置采集帧率:-OK
                    if (!CamHandleListNew[_cameraList[i].ID].SetFrameRate(_cameraList[i].FPS))
                        continue;
                    //设置相机曝光时间:-OK
                    if (!CamHandleListNew[_cameraList[i].ID].SetExposureTime(_cameraList[i].ExposureTime))
                        continue;
                    //设置相机增益:-OK
                    if (!CamHandleListNew[_cameraList[i].ID].SetGain(_cameraList[i].Gain))
                        continue;
                    //设置相机触发采集延时：
                    //if (!CamHandleListNew[_cameraList[i].ID].SetTriggerDelay(100.0f))
                    //    continue;

                    //注册相机图像采集到事件回调函数
                    if (!CamHandleListNew[_cameraList[i].ID].RegisterImageGrabbedCallBack())
                        continue;
                    //注意:HikVision相机提供断线重连功能,Baumer相机暂无.因此,HikVision相机不需要定时器重连相机
                    if (!CamHandleListNew[_cameraList[i].ID].RegisterExceptionCallBack())
                        continue;
                    //设置相机开始采集
                    if (!CamHandleListNew[_cameraList[i].ID].StartGrab())
                        continue;
                }
            }
        }

        #region  定时器
        private System.Timers.Timer _timer = new System.Timers.Timer();

        /// <summary>
        /// 初始化定时器
        /// </summary>
        public void InitTimer()
        {
            this._timer.Interval = 1000;
            this._timer.Elapsed += new System.Timers.ElapsedEventHandler(mTimer_Elapsed);
            this._timer.Enabled = false;
        }

        /// <summary>
        /// 定时器间隔到达事件处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                StopTimer();
                if (_cameraList != null
                  && _cameraList.Count > 0)
                {
                    for (int i = 0; i < _cameraList.Count; i++)
                    {
                        if (!_cameraList[i].IsConnected)
                        {
                            DoIni();
                            StartTimer();
                            break;
                        }
                    }
                }

            }
            catch { }
        }

        /// <summary>
        /// 启用定时器
        /// </summary>
        public void StartTimer()
        {
            this._timer.Enabled = false;
            this._timer.Enabled = true;
        }

        /// <summary>
        /// 停用定时器
        /// </summary>
        public void StopTimer()
        {
            this._timer.Enabled = false;
        }

        #endregion
    }
}
