using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       Device_SerialPort
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Device
 * File      Name：       Device_SerialPort
 * Creating  Time：       3/4/2020 2:15:42 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Device
{
    public class Device_SerialPort : Device
    {
        public Device_SerialPort(Pro2DBarcode.Config.ConfigSerialPort cfgSerialPort, Pro2DBarcode.Config.ConfigSystem cfgSystem)
        {
            _cfgSystem = cfgSystem;
            _serialPortList = cfgSerialPort.ComSerialPortList;
            IsChinese = cfgSystem.LanguageVersion == ProCommon.Communal.Language.Chinese;
            if (_serialPortList != null
              && _serialPortList.Count > 0)
            {
                InitTimer();   //初始化定时器               
                SerialPortHandleList = new SortedList<string, ProDriver.APIHandle.SerialSyncAPIHandle>();
                for (int i = 0; i < _serialPortList.Count; i++)
                {
                    _serialPortList[i].PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Camera_PropertyChanged);
                }
            }

            _packHeadStr = _cfgSystem.SerialPortPackHeadStr;
            _packEndStr = _cfgSystem.SerialPortPackEndStr;
          
            if (_cameraFor2DCode==null)
                _cameraFor2DCode = Pro2DBarcode.Manager.SystemManager.Instance.CameraFor2DCode;          
            
        }

        private void Camera_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
            {
                ProCommon.Communal.ComWrappedSerialPort comSerialPort = sender as ProCommon.Communal.ComWrappedSerialPort;

                if (comSerialPort != null
                    && (!comSerialPort.IsConnected)
                    && (!_isSystemStop))
                {
                    if (!_timer.Enabled)
                        StartTimer();
                }
            }
        }

        private ProCommon.Communal.ComWrappedSerialPortList _serialPortList;
        private bool _isSerialPortInitialized;      
        public System.Collections.Generic.SortedList<string, ProDriver.APIHandle.SerialSyncAPIHandle> SerialPortHandleList;

        private Pro2DBarcode.Config.ConfigSystem _cfgSystem;
        private ProCommon.Communal.Camera _cameraFor2DCode;
        private Pro2DBarcode.Device.Device_Camera _devCamera;

        /// <summary>
        /// 异步执行功能的委托
        /// </summary>
        private AsynComSerialPortDataReceivedHandler _delserialPortFor2DCode;

        /// <summary>
        /// 解析字符串中触发相机拍照信号
        /// </summary>
        private readonly string _packHeadStr;

        /// <summary>
        /// 解析字符串中结束符
        /// </summary>
        private readonly string _packEndStr;
      
        private int _dataLength;
        private string _tmpStr;       
        private System.Text.StringBuilder _receivedData;

        public override void DoIni()
        {
            try
            {
                if (_serialPortList != null
                    && _serialPortList.Count > 0)
                {
                    for (int i = 0; i < _serialPortList.Count; i++)
                    {
                        ProDriver.APIHandle.SerialSyncAPIHandle spAPI = new ProDriver.APIHandle.SerialSyncAPIHandle(_serialPortList[i]);
                        spAPI.ConnectedEvt += Device_SerialPort_ConnectedEvt;
                        spAPI.ReceivedEvt += Device_SerialPort_ReceivedEvt;
                        spAPI.ClosedEvt += Device_SerialPort_ClosedEvt;

                        SerialPortHandleList.Add(_serialPortList[i].ID, spAPI);

                        if (!_serialPortList[i].IsConnected)
                        {
                            SerialPortHandleList[_serialPortList[i].ID].EnumerateSerialPortList(); //枚举串口列表
                            if (SerialPortHandleList[_serialPortList[i].ID].GetSerialPortByName(_serialPortList[i].Name))
                            {
                                if (!SerialPortHandleList[_serialPortList[i].ID].Connect())
                                {
                                    if (!_timer.Enabled)
                                        StartTimer();  //第一连接失败，需要启动定时器，因为串口连接状态更新的值与原来的值一样，不会触发属性值改变事件
                                    return;
                                }
                                else
                                {
                                    _serialPortList[i].IsConnected = true;
                                }
                            }
                            else { continue; }
                        }
                    }

                    if (!_isSerialPortInitialized)
                    {
                        InitSerialPort();
                        _isSerialPortInitialized = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ProCommon.Communal.LogWriter.WriteException(_exLogFilePath, ex);
                ProCommon.Communal.LogWriter.WriteLog(_sysLogFilePath, string.Format("错误：初始化串口设备失败!\n异常描述:{0}", ex.Message));
                throw new ProCommon.Communal.InitException(ToString(), "初始化异常\n" + ex.Message);
            }
        }

        /// <summary>
        /// 初始化串口
        /// </summary>
        private void InitSerialPort()
        {
            _delserialPortFor2DCode = new AsynComSerialPortDataReceivedHandler(OnComSerialPortForGunDataReceived);          
           
        }       

        /// <summary>
        /// 串口关闭事件
        /// </summary>
        /// <param name="iErrorCode"></param>
        /// <param name="strErrorMsg"></param>
        private void Device_SerialPort_ClosedEvt(ProCommon.Communal.ComWrappedSerialPort comSerialPort,int iErrorCode, string strErrorMsg)
        {
            
        }

        /// <summary>
        /// 串口接收到数据事件
        /// </summary>
        /// <param name="comSerialPort"></param>
        /// <param name="msgStr"></param>
        /// <param name="msgByte"></param>
        private void Device_SerialPort_ReceivedEvt(ProCommon.Communal.ComWrappedSerialPort comSerialPort, string msgStr, byte[] msgByte)
        {
            if (Manager.SystemManager.Instance.IsRunning)
            {
                //接收数据信号,解析数据包,根据不同情形执行动作 
                if (msgStr != null
                && msgStr.Length > 0)
                {
                    //用串口名称区别，异步执行不同功能
                    if (_cfgSystem.SerialPortNameFor2DCode == comSerialPort.Name)
                    {
                        if (_receivedData == null)
                            _receivedData = new StringBuilder();

                        _receivedData.Append(msgStr);
                        _dataLength = _receivedData.Length;

                        if (_dataLength >= 2)
                        {
                            //判断包头是否收到包头
                            _tmpStr = _receivedData.ToString().Substring(0, 1);
                            if (_tmpStr == _packHeadStr)
                            {
                                //判断包尾是否收到包尾
                                _tmpStr = _receivedData.ToString().Substring(_dataLength - 1, 1);
                                if (_tmpStr == _packEndStr)
                                {
                                    System.Threading.Thread.Sleep(150);
                                    byte[] dataArr = null;
                                    string dataStr = _receivedData.ToString();
                                    _receivedData.Clear();
                                    //异步完成其他动作
                                    if (_delserialPortFor2DCode != null)
                                        _delserialPortFor2DCode.BeginInvoke(comSerialPort, dataStr, dataArr, null, _delserialPortFor2DCode);
                                }
                            }
                            else { _receivedData.Clear(); }//异常数据清空
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 接收到数据信号
        /// </summary>
        /// <param name="comSerialPort"></param>
        /// <param name="msgStr"></param>
        /// <param name="msgByte"></param>
        private void OnComSerialPortForGunDataReceived(ProCommon.Communal.ComWrappedSerialPort comSerialPort, string msgStr, byte[] msgByte)
        {
            if(msgStr.Substring(0,1)== _packHeadStr)
            {
                if (_devCamera == null)
                    _devCamera = (Pro2DBarcode.Device.Device_Camera)Pro2DBarcode.Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Camera];
                _devCamera.CamHandleListNew[_cameraFor2DCode.ID].SoftTriggerOnce();
            }     
        }

        /// <summary>
        /// 串口连接事件
        /// </summary>
        /// <param name="comSerialPort"></param>
        /// <param name="iErrorCode"></param>
        /// <param name="strErrorMsg"></param>
        private void Device_SerialPort_ConnectedEvt(ProCommon.Communal.ComWrappedSerialPort comSerialPort, int iErrorCode, string strErrorMsg)
        {
            
        }

        public override void DoRelease()
        {
            try
            {
                if(SerialPortHandleList!=null
                    && SerialPortHandleList.Count>0)
                {
                    for(int i=0;i< _serialPortList.Count;i++)
                    {
                        if (!_serialPortList[i].IsConnected)
                            SerialPortHandleList[_serialPortList[i].ID].Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ProCommon.Communal.LogWriter.WriteException(_exLogFilePath, ex);
                ProCommon.Communal.LogWriter.WriteLog(_sysLogFilePath, string.Format("错误：释放串口设备失败!\n异常描述:{0}", ex.Message));
                throw new ProCommon.Communal.ReleaseException(this.ToString(), "释放异常\n" + ex.Message);
            }
        }

        public override void DoStart()
        {
            try
            {
                if (_serialPortList != null
                   && _serialPortList.Count > 0)
                {
                    string serialPortName = string.Empty;

                    for (int i = 0; i < _serialPortList.Count; i++)
                    {
                        if (!_serialPortList[i].IsConnected)
                        {
                            serialPortName += "\n" + _serialPortList[i].Name;
                        }
                    }

                    if (!string.IsNullOrEmpty(serialPortName)
                        && (!_isShowing))
                    {
                        _isShowing = true;
                                               
                        string txt1 = IsChinese ? "串口:" : "SerialPort:";
                        string txt2 = IsChinese ? "\n连接失败 !" : "\nConnect falied !";
                        string caption = IsChinese ? "警告信息" : "Warning Message";

                        ProCommon.UserDefForm.FrmMsgBox.Show(txt1 + serialPortName + txt2, caption,
                           ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, IsChinese);
                    }
                }
            }
            catch (Exception ex)
            {
                ProCommon.Communal.LogWriter.WriteException(_exLogFilePath, ex);
                ProCommon.Communal.LogWriter.WriteLog(_sysLogFilePath, string.Format("错误：启动串口设备失败!\n异常描述:{0}", ex.Message));
                throw new ProCommon.Communal.StartException(ToString(), "启动异常\n" + ex.Message);
            }
        }

        public override void DoStop()
        {
            try
            {
                _isSystemStop = true; //系统退出标记
                StopTimer();
                if (SerialPortHandleList != null
                     && SerialPortHandleList.Count > 0)
                {
                    for (int i = 0; i < _serialPortList.Count; i++)
                    {
                        if (!_serialPortList[i].IsConnected)
                            SerialPortHandleList[_serialPortList[i].ID].Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ProCommon.Communal.LogWriter.WriteException(_exLogFilePath, ex);
                ProCommon.Communal.LogWriter.WriteLog(_sysLogFilePath, string.Format("错误：停止串口设备失败!\n异常描述:{0}", ex.Message));
                throw new ProCommon.Communal.StopException(this.ToString(), "停止异常\n" + ex.Message);
            }
        }

        public override string ToString()
        {
            return "DEVICE_SERIALPORT";
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
                if (_serialPortList != null
                  && _serialPortList.Count > 0)
                {
                    for (int i = 0; i < _serialPortList.Count; i++)
                    {
                        if (!_serialPortList[i].IsConnected)
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

    public delegate void AsynComSerialPortDataReceivedHandler(ProCommon.Communal.ComWrappedSerialPort comSerialPort, string msgStr, byte[] msgByte);
}
