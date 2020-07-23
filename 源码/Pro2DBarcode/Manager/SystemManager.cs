using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProCommon.Communal;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       SystemManager
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Manager
 * File      Name：       SystemManager
 * Creating  Time：       1/15/2020 5:52:35 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Manager
{
    /// <summary>
    /// 系统管理器
    /// [管理系统设备事件]
    /// </summary>
    public class SystemManager
    {
        #region 静态单例

        static object _syncObj = new object();
        static SystemManager _instance;
        public static SystemManager Instance
        {
            get
            {
                lock (_syncObj)
                {
                    if (_instance == null)
                    { _instance = new SystemManager(); }
                }

                return _instance;
            }
        }

        private SystemManager()
        {
            InitInternal();
        }

        #endregion

        public bool IsRunning, IsStopped, IsScram, IsStandBy;//是否自动运行，是否自动停止,是否急停，是否待机
        public bool IsResetting, IsReseted;//是否正在复位,是否完成复位      
        public bool IsRunAllowed; //false-运行不允许,true-运行允许
        public bool IsAlarmed; //是否有轴报警
        public bool IsRunOnce;//是否单次运行

        private Pro2DBarcode.Config.ConfigSystem _cfgSystem;            //系统配置      
        private Pro2DBarcode.Config.ConfigCamera _cfgCamera;            //相机配置
        private Pro2DBarcode.Config.ConfigBoardCtrller _cfgBoardCtrller;//板卡配置
        private Pro2DBarcode.Config.ConfigSerialPort _cfgSerialPort;    //串口配置

        private Pro2DBarcode.Config.ConfigSystem _tmpCfgSystem = null;//系统配置[产生系统配置文件时,为方便更新系统定义的特殊信号,故而定义为类属字段]

        //private ProDriver.APIHandle.CameraAPIHandle _camHandle;
        //private ProDriver.APIHandle.BoardCtrllerAPIHandle _mainBoardHandle;
        private string _sysLogFilePath, _exLogFilePath; //系统日志文件路径,异常日志文件路径 
        public int ChkCount; //自检执行步骤标号

        /// <summary>
        /// 系统自检结果标记
        /// </summary>
        public bool ChkOK { private set; get; }

        /// <summary>
        /// 系统自检异常信息
        /// </summary>
        public string ChkErr { private set; get; }

        /// <summary>
        /// 二维码识别的采图相机
        /// </summary>
        public ProCommon.Communal.Camera CameraFor2DCode { get;set; }

        /// <summary>
        /// 二维码识别的运控板卡
        /// </summary>
        public ProCommon.Communal.BoardCtrller BoardCtrllerFor2DCode { set; get; }

        /// <summary>
        /// 二维码识别的通信串口
        /// </summary>
        public ProCommon.Communal.ComWrappedSerialPort ComSerialPortFor2DCode { set; get; }

        /// <summary>
        /// 初始化字段
        /// </summary>
        private void InitField()
        {
            IsRunAllowed = false;
            IsRunning = false;
            IsStopped = false;
            IsScram = false;
            IsStandBy = false;
            IsResetting = false;
            IsReseted = false;
            IsAlarmed = false;
            IsRunOnce = true;
            ChkOK = true;
        }

        private void InitInternal()
        {
            InitField();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitExternal()
        {            
            _cfgSystem = Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem;

            //可以在此处分配板卡--按照其名称映射绑定
            _cfgBoardCtrller = Pro2DBarcode.Manager.ConfigManager.Instance.CfgBoardCtrller;
            for (int i = 0; i < _cfgBoardCtrller.BoardCtrllerList.Count; i++)
            {
                if (_cfgSystem.BoardCtrllerNamerFor2DCode == _cfgBoardCtrller.BoardCtrllerList[i].Name)
                    BoardCtrllerFor2DCode = _cfgBoardCtrller.BoardCtrllerList[i];
            }          

            //可以在此处分配相机--按照其名称映射绑定
            _cfgCamera = Pro2DBarcode.Manager.ConfigManager.Instance.CfgCamera;
            for (int i=0;i< _cfgCamera.CameraList.Count;i++)
            {
                if(_cfgSystem.CameraNameFor2DCode==_cfgCamera.CameraList[i].Name)
                    CameraFor2DCode = _cfgCamera.CameraList[i];
            }

            //可以在此处分配串口--按照其名称映射绑定
            _cfgSerialPort = Pro2DBarcode.Manager.ConfigManager.Instance.CfgSerialPort;
            for (int i = 0; i < _cfgSerialPort.ComSerialPortList.Count; i++)
            {
                if (_cfgSystem.SerialPortNameFor2DCode == _cfgSerialPort.ComSerialPortList[i].Name)
                    ComSerialPortFor2DCode = _cfgSerialPort.ComSerialPortList[i];
            }

            _sysLogFilePath = Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.SystemLogFilePath;
            _exLogFilePath = Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ExceptionLogFilePath;
        }        

        public void WriteProcessLog(string varName, bool isOn)
        {
            Pro2DBarcode.Manager.UIManager.Instance.AddRunLog("", varName, isOn ? "触发" : "释放");
        }

        public void WriteProcessLog(string varName, string description)
        {
            Pro2DBarcode.Manager.UIManager.Instance.AddRunLog("序列号", varName, description);
        }

        /// <summary>
        /// 计算匹配相机外触发信号口的变量
        /// </summary>
        /// <param name="inVar"></param>
        /// <param name="cam"></param>
        /// <returns></returns>
        public bool GetMatchCamera(ProCommon.Communal.InVar inVar, out ProCommon.Communal.Camera cam)
        {
            bool rt = false;
            cam = null;
            try
            {
                for (int i = 0; i < _cfgCamera.CameraList.Count; i++)
                {
                    if (_cfgCamera.CameraList[i].IN_GrabTrigger.InVar == inVar)
                    {
                        cam = _cfgCamera.CameraList[i];
                        rt = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ProCommon.Communal.LogWriter.WriteException(_exLogFilePath, ex);
                ProCommon.Communal.LogWriter.WriteLog(_sysLogFilePath, string.Format("错误：获取以输入口[{0}]为外触发拍照信号的相机失败!\n异常描述:{1}", inVar.ToString(), ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 控制变量状态改变处理函数
        /// [注:输入控制变量]
        /// </summary>
        /// <param name="inVar"></param>
        public void InVarObjPropertyChangedHandler(ProCommon.Communal.InVarObj inVarObj)
        {
            //if (_mainBoardHandle == null)
            //    _mainBoardHandle = ((Pro2DBarcode.Device.Device_BoardCtrller)Pro2DBarcode.Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Board]).BoardHandleList[MainBoardCtrller.Number];
            bool lastvalue, nowvalue;
            lastvalue = (bool)inVarObj.LastValue;
            nowvalue = (bool)inVarObj.NewValue;
            ProCommon.Communal.TriggerLogic edge = ProCommon.Communal.Functions.JudgeEdge(lastvalue, nowvalue);

            #region 相机触发拍照信号[通用可选输入:触发中转的时候使用]

            /*
            ProCommon.Communal.Camera cam;
            if (GetMatchCamera(inVarObj.InVar, out cam))
            {
                bool enable = false;
                switch (cam.IN_GrabTrigger.EffectiveLogic)
                {
                    case ProCommon.Communal.TriggerLogic.FallEdge:
                        if (edge == ProCommon.Communal.TriggerLogic.FallEdge)
                        {
                            enable = true;
                        }
                        break;
                    case ProCommon.Communal.TriggerLogic.LogicTrue:
                        if (nowvalue)
                            enable = true;
                        break;
                    case ProCommon.Communal.TriggerLogic.LogicFalse:
                        if (!nowvalue)
                            enable = true;
                        break;
                    case ProCommon.Communal.TriggerLogic.RaiseEdge:
                        if (edge == ProCommon.Communal.TriggerLogic.RaiseEdge)
                        {
                            enable = true;
                        }
                        break;
                    default: break;
                }
                if (enable)
                {
                    WriteProcessLog(inVarObj.VarName, nowvalue);
                    _camHandle = ((Pro2DBarcode.Device.Device_Camera)Pro2DBarcode.Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Camera]).CamHandleList[cam.Number];

                    //光源环境1下采集
                    int timecnt = 0, acquirecnt = 0, bitNo = 0, stV = 0;
                    bool reAcquire = true;
                    for (int i = 0; i < cam.AcquireConditionList.Count; i++)
                    {

                        _camHandle.SetExposureTime(cam.AcquireConditionList[i].EnvrExposureTime);
                        _camHandle.SetGain(cam.AcquireConditionList[i].EnvrGain);

                        for (int j = 0; j < cam.AcquireConditionList[i].LighterList.Count; j++)
                        {
                            ProCommon.Communal.OutVarObj outV = _mainBoardHandle.GetOutVarObj(cam.AcquireConditionList[i].LighterList[j].OUT_Light.OutVar);
                            bitNo = ProCommon.Communal.Functions.GetIndexByOutVar(cam.AcquireConditionList[i].LighterList[j].OUT_Light.OutVar);

                            switch (cam.AcquireConditionList[i].LighterList[j].OUT_Light.EffectiveLogic)
                            {
                                case ProCommon.Communal.TriggerLogic.FallEdge:
                                    _mainBoardHandle.SetOutBitLogicValue(bitNo, true);
                                    _mainBoardHandle.SetOutBitLogicValue(bitNo, false);
                                    break;
                                case ProCommon.Communal.TriggerLogic.RaiseEdge:
                                    _mainBoardHandle.SetOutBitLogicValue(bitNo, false);
                                    _mainBoardHandle.SetOutBitLogicValue(bitNo, true);
                                    break;
                                case ProCommon.Communal.TriggerLogic.LogicFalse:
                                    _mainBoardHandle.SetOutBitLogicValue(bitNo, false);
                                    break;
                                case ProCommon.Communal.TriggerLogic.LogicTrue:
                                    _mainBoardHandle.SetOutBitLogicValue(bitNo, true);
                                    break;
                                default: break;
                            }
                        }

                        acquirecnt++;
                        while (true)
                        {
                            if (!_camHandle.IsImageGrabbed)
                            {
                                if (cam.AcquireConditionList[i].AcquireTimeout < 5 * timecnt)
                                {
                                    if (acquirecnt > cam.AcquireConditionList[i].ReAcquireCount)
                                        reAcquire = false;
                                    break;
                                }
                                System.Threading.Thread.Sleep(10);
                                timecnt++;
                            }
                            else
                            {
                                reAcquire = false;
                                break;
                            }
                        }

                        if (reAcquire) //重采
                            i--;
                    }
                }
            }
            */
            #endregion

            #region 系统专用功能[通用可选输入]

            #endregion

            #region 其他功能

            switch (inVarObj.InVar)
            {
                case ProCommon.Communal.InVar.ALM0:
                case ProCommon.Communal.InVar.ALM1:
                case ProCommon.Communal.InVar.ALM2:
                case ProCommon.Communal.InVar.ALM3:
                case ProCommon.Communal.InVar.ALM4:
                case ProCommon.Communal.InVar.ALM5:
                case ProCommon.Communal.InVar.ALM6:
                case ProCommon.Communal.InVar.ALM7:
                    if (nowvalue)
                    {
                        //_mainBoardHandle.RapidStop(2);
                        //_mainBoardHandle.SwitchBoardCtrllerServo(false);
                        //三色灯的亮灭:红亮,绿灭，黄闪，鸣静---同停止
                        IsScram = false;
                        IsStopped = true;
                        IsReseted = false;
                        IsRunning = false;
                        IsStandBy = false;
                        IsAlarmed = true;

                        WriteProcessLog(inVarObj.VarName + "[报警信号]", nowvalue);                      
                        //2伺服报警--更新界面的运行数据
                    }
                    break;
                case ProCommon.Communal.InVar.DTM0:
                    break;
                case ProCommon.Communal.InVar.DTM1:
                    break;
                case ProCommon.Communal.InVar.DTM2:
                    break;
                case ProCommon.Communal.InVar.DTM3:
                    break;
                case ProCommon.Communal.InVar.FWD0:
                    break;
                case ProCommon.Communal.InVar.FWD1:
                    break;
                case ProCommon.Communal.InVar.FWD2:
                    break;
                case ProCommon.Communal.InVar.FWD3:
                    break;
                case ProCommon.Communal.InVar.IN00:
                    break;
                case ProCommon.Communal.InVar.IN01:
                    break;
                case ProCommon.Communal.InVar.IN02:
                    break;
                case ProCommon.Communal.InVar.IN03:
                    break;
                case ProCommon.Communal.InVar.IN04:
                    break;
                case ProCommon.Communal.InVar.IN05:
                    break;
                case ProCommon.Communal.InVar.IN06:
                    break;
                case ProCommon.Communal.InVar.IN07:
                    break;
                case ProCommon.Communal.InVar.IN08:
                    break;
                case ProCommon.Communal.InVar.IN09:
                    break;
                case ProCommon.Communal.InVar.IN10:
                    break;
                case ProCommon.Communal.InVar.IN11:
                    break;
                case ProCommon.Communal.InVar.IN12:
                    break;
                case ProCommon.Communal.InVar.IN13:
                    break;
                case ProCommon.Communal.InVar.IN14:
                    break;
                case ProCommon.Communal.InVar.IN15:
                    break;
                case ProCommon.Communal.InVar.IN16:
                    break;
                case ProCommon.Communal.InVar.IN17:
                    break;
                case ProCommon.Communal.InVar.IN18:
                    break;
                case ProCommon.Communal.InVar.IN19:
                    break;
                case ProCommon.Communal.InVar.IN20:
                    break;
                case ProCommon.Communal.InVar.IN21:
                    break;
                case ProCommon.Communal.InVar.IN22:
                    break;
                case ProCommon.Communal.InVar.IN23:
                    break;
                case ProCommon.Communal.InVar.IN24:
                    break;
                case ProCommon.Communal.InVar.IN25:
                    break;
                case ProCommon.Communal.InVar.IN26:
                    break;
                case ProCommon.Communal.InVar.IN27:
                    break;
                case ProCommon.Communal.InVar.IN28:
                    break;
                case ProCommon.Communal.InVar.IN29:
                    break;
                case ProCommon.Communal.InVar.IN30:
                    break;
                case ProCommon.Communal.InVar.IN31:
                    break;
                case ProCommon.Communal.InVar.IN32:
                    break;
                case ProCommon.Communal.InVar.REV0:
                    break;
                case ProCommon.Communal.InVar.REV1:
                    break;
                case ProCommon.Communal.InVar.REV2:
                    break;
                case ProCommon.Communal.InVar.REV3:
                    break;
                case ProCommon.Communal.InVar.NONE:
                    break;
                default:
                    break;
            }

            #endregion
        }

        /// <summary>
        /// 控制变量状态改变处理函数
        /// [注:输出控制变量]
        /// </summary>
        /// <param name="outVar"></param>
        public void OutVarObjPropertyChangedHandler(ProCommon.Communal.OutVarObj outVarObj)
        {

        }

        /// <summary>
        /// 方法：控制变量处理函数
        /// [注：轴变量]
        /// </summary>
        /// <param name="axis">控制变量</param>        
        public void AxisObjPropertyChangedHandler(ProCommon.Communal.Axis axis)
        {

        }

        #region 系统恢复
        /// <summary>
        /// 检测系统文件夹和文件夹下文件
        /// </summary>
        public void CheckDirectoryAndFiles(System.ComponentModel.BackgroundWorker bkgrdWorker)
        {
            foreach (var itm in Pro2DBarcode.Manager.ConfigManager.DirectoryNames)
            {
                if (!ChkOK)
                    break;

                CheckDirectory(Pro2DBarcode.Manager.ConfigManager.DirectoryBase + itm, bkgrdWorker);
                System.Threading.Thread.Sleep(500);
                switch (itm)
                {
                    case Pro2DBarcode.Manager.ConfigManager.DIRECTORY_NAME_FOR_CONFIG:
                        {
                            foreach (var itm1 in Pro2DBarcode.Manager.ConfigManager.ConfigFileNames)
                            {
                                if (!CheckConfigFile((Pro2DBarcode.Manager.ConfigManager.DirectoryBase + itm), itm1, bkgrdWorker))
                                {
                                    ChkOK = false;
                                    break;
                                }
                                System.Threading.Thread.Sleep(200);
                            }
                        }
                        break;
                    case Pro2DBarcode.Manager.ConfigManager.DIRECTORY_NAME_FOR_DATABASE:
                        {
                            //需要创建数据库文件及数据表
                        }
                        break;
                    case Pro2DBarcode.Manager.ConfigManager.DIRECTORY_NAME_FOR_LOG:
                        {
                            foreach (var itm2 in Pro2DBarcode.Manager.ConfigManager.LogFileNames)
                            {
                                if (!CheckLogFile(Pro2DBarcode.Manager.ConfigManager.DirectoryBase + itm + "\\" + itm2, bkgrdWorker))
                                {
                                    ChkOK = false;
                                    break;
                                }
                                System.Threading.Thread.Sleep(200);
                            }
                        }
                        break;
                    case Pro2DBarcode.Manager.ConfigManager.DIRECTORY_NAME_FOR_ROUTINE:
                        break;
                    case Pro2DBarcode.Manager.ConfigManager.DIRECTORY_NAME_FOR_RESOURCES:
                        break;
                }
            }
        }

        /// <summary>
        /// 检测配置文件夹
        /// </summary>
        /// <param name="directroy"></param>
        /// <param name="bkgrdWorker"></param>
        private void CheckDirectory(string directroy,System.ComponentModel.BackgroundWorker bkgrdWorker)
        {
            ChkCount += 1;
            int startIdx = directroy.LastIndexOf('\\');
            string directoryName = directroy.Substring(startIdx+1, directroy.Length- startIdx-1);

            if (!System.IO.Directory.Exists(directroy))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(directroy);
                    if (bkgrdWorker != null)
                        bkgrdWorker.ReportProgress(ChkCount, "Create folder[" + directoryName + "]successfully !");
                }
                catch (System.Exception ex)
                {
                    if (bkgrdWorker != null)
                        bkgrdWorker.ReportProgress(ChkCount, "Create folder[" + directoryName + "]failed !\r\n" + ex.Message);
                }
            }
            else
            {
                if (bkgrdWorker != null)
                    bkgrdWorker.ReportProgress(ChkCount, "[" + directoryName + "]found !");
            }
        }

        /// <summary>
        /// 检测日志文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="bkgrdWorker"></param>
        private bool CheckLogFile(string filePath,System.ComponentModel.BackgroundWorker bkgrdWorker)
        {
            bool rt = false;
            ChkCount +=1;
            string fileNameEx = System.IO.Path.GetFileName(filePath); //包含扩展名的文件名
            string fileName = fileNameEx.Substring(0, fileNameEx.LastIndexOf('.'));

            if (!System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Create(filePath);
                   
                    if (bkgrdWorker != null)
                        bkgrdWorker.ReportProgress(ChkCount, "Create file[" + fileName + "]successfully !");
                    rt=true;
                }
                catch (System.Exception ex)
                {
                    ChkErr = "Create file[" + fileName + "]failed!Description:\r\n" + ex.Message;
                    if (bkgrdWorker != null)
                        bkgrdWorker.ReportProgress(ChkCount, "Create file[" + fileName + "]failed!\r\n" + ex.Message);
                }
                
            }
            else
            {
                if (bkgrdWorker != null)
                    bkgrdWorker.ReportProgress(ChkCount, "[" + fileName + "]found !");
                rt = true;
            }

            return rt;
        }

        /// <summary>
        /// 检测配置文件
        /// </summary>
        /// <param name="fileDirectory"></param>
        /// <param name="fileName"></param>
        /// <param name="bkgrdWorker"></param>
        private bool CheckConfigFile(string fileDirectory, string fileName, System.ComponentModel.BackgroundWorker bkgrdWorker)
        {
            ChkCount+=1;
            bool rt = false;
            if (!System.IO.File.Exists(fileDirectory + "\\" + fileName))
            {
                try
                {
                    switch (fileName)
                    {
                        #region 系统配置
                        case Pro2DBarcode.Manager.ConfigManager.SYSTEM_CONFIG_FILE_NAME:
                            {
                                _tmpCfgSystem = new Config.ConfigSystem();
                                _tmpCfgSystem.LanguageVersion = ProCommon.Communal.Language.Chinese;
                                _tmpCfgSystem.ViewNumber = 1;
                                _tmpCfgSystem.EnableAutoLaunch = false;
                                _tmpCfgSystem.CameraNameFor2DCode = "CameraFor2DCode";

                                _tmpCfgSystem.SerialPortNameFor2DCode = "COM3";
                                _tmpCfgSystem.SerialPortPackHeadStr = "T";
                                _tmpCfgSystem.SerialPortPackEndStr = "&";
                                _tmpCfgSystem.ResultNGStr = "NOREAD";
                                _tmpCfgSystem.ResultSplitStr = ";";

                                _tmpCfgSystem.ClientName = "深圳市联合创新实业有限公司";
                                _tmpCfgSystem.CtrllerCategoryArray = new ProCommon.Communal.CtrllerCategory[] {
                                    ProCommon.Communal.CtrllerCategory.Camera,
                                    //ProCommon.Communal.CtrllerCategory.Board,
                                    ProCommon.Communal.CtrllerCategory.SerialPort
                                    /*ProCommon.Communal.CtrllerCategory.Socket,
                                    ProCommon.Communal.CtrllerCategory.Web */};

                                _tmpCfgSystem.EnableLastRoutine = false; //运行软件时不启用上次生产程式
                                _tmpCfgSystem.LastRoutinePath = null;    //上次生产程式(默认为空)
                                _tmpCfgSystem.RoutineDirectory =null;//生产程式目录(转到默认)
                                _tmpCfgSystem.DataBaseDirectory = null;//数据和日志目录(数据库,转到默认)

                                _tmpCfgSystem.RunDataSheetName = null;//流水结果表名称(转到默认)
                                _tmpCfgSystem.EnableSaveRunData = false;
                                _tmpCfgSystem.SaveRunDataDays = 7;
                                _tmpCfgSystem.DisplayRunDataCount = 10;

                                _tmpCfgSystem.RunLogSheetName = null;//流水日志表名称(转到默认)
                                _tmpCfgSystem.EnableSaveRunLog = false;
                                _tmpCfgSystem.SaveRunLogDays = 7;
                                _tmpCfgSystem.DisplayRunLogCount = 15;

                                _tmpCfgSystem.AlarmLogSheetName = null;//报警日志表名称(转到默认)
                                _tmpCfgSystem.EnableSaveAlarmLog = false;
                                _tmpCfgSystem.SaveAlarmLogDays = 7;

                                _tmpCfgSystem.SystemLogFilePath =null;//系统日志文件路径(转到默认)
                                _tmpCfgSystem.ExceptionLogFilePath = null;//异常日志文件路径(转到默认)                                

                                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigSystem>(_tmpCfgSystem,
                                    Pro2DBarcode.Manager.ConfigManager.ConfigDirectory+"\\" + Pro2DBarcode.Manager.ConfigManager.SYSTEM_CONFIG_FILE_NAME);

                            }
                            break;
                        #endregion

                        #region 用户配置
                        case Pro2DBarcode.Manager.ConfigManager.ACCOUNT_CONFIG_FILE_NAME:
                            {
                                ProCommon.Communal.Account user1 = new ProCommon.Communal.Account("Administrator", 0);
                                user1.Authority = ProCommon.Communal.AccountAuthority.Administrator;
                                user1.PassWord = ProCommon.Communal.DESEncrypt.Encrypt("88888888");

                                ProCommon.Communal.Account user2 = new ProCommon.Communal.Account("Operator", 1);
                                user2.Authority = ProCommon.Communal.AccountAuthority.Junior;
                                user2.PassWord = ProCommon.Communal.DESEncrypt.Encrypt("123");

                                Pro2DBarcode.Config.ConfigAccount configUser = new Config.ConfigAccount();
                                configUser.UserList.Add(user1);
                                configUser.UserList.Add(user2);
                                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigAccount>(configUser,
                                    Pro2DBarcode.Manager.ConfigManager.ConfigDirectory + "\\" + Pro2DBarcode.Manager.ConfigManager.ACCOUNT_CONFIG_FILE_NAME);
                            }
                            break;
                        #endregion

                        #region 报警配置
                        case Pro2DBarcode.Manager.ConfigManager.ALARM_CONFIG_FILE_NAME:
                            break;
                        #endregion

                        #region 相机配置
                        case Pro2DBarcode.Manager.ConfigManager.CAMERA_CONFIG_FILE_NAME:
                            {
                                Pro2DBarcode.Config.ConfigCamera cfgCamera = new Config.ConfigCamera();
                                if (_tmpCfgSystem != null
                                    && _tmpCfgSystem.CtrllerCategoryArray != null)
                                {
                                    bool isCreate = false;
                                    foreach (ProCommon.Communal.CtrllerCategory cc in _tmpCfgSystem.CtrllerCategoryArray)
                                    {
                                        if (cc == ProCommon.Communal.CtrllerCategory.Camera)
                                            isCreate = true;
                                    }

                                    if (isCreate)
                                    {
                                        // 根据项目需要,创建默认恢复的相机配置文件
                                        ProCommon.Communal.Camera cam = new ProCommon.Communal.Camera(ProCommon.Communal.CtrllerBrand.MindVision, ProCommon.Communal.CtrllerType.Camera_AreaScan, 0, _tmpCfgSystem.CameraNameFor2DCode);
                                        cam.ExposureTime = 90000;
                                        cam.FPS = 14;
                                        cam.Gain = 1.5f;
                                        cam.ReconnectInterval = 500;
                                        cam.SerialNo = "058021910040";
                                        cfgCamera.CameraList.Add(cam);
                                    }                                   
                                }

                                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigCamera>(cfgCamera,
                                    Pro2DBarcode.Manager.ConfigManager.ConfigDirectory + "\\" + Pro2DBarcode.Manager.ConfigManager.CAMERA_CONFIG_FILE_NAME);
                            }                            
                            break;
                        #endregion

                        #region 项目选用变量配置
                        case Pro2DBarcode.Manager.ConfigManager.USERCONTROLS_CONFIG_FILE_NAME:
                            {
                                Config.ConfigUserControls cfgUserCtrls = new Config.ConfigUserControls();

                                #region 轴Axes
                                ProCommon.Communal.AxisList axesList = new ProCommon.Communal.AxisList();
                                CreateAxesInstance(ref axesList);
                                cfgUserCtrls.AxisImportX = axesList[0].Clone();
                                cfgUserCtrls.AxisImportX.Name = "进料X轴";
                                #endregion

                                #region 输入InPut  
                                cfgUserCtrls.IN_ScramTrigger = new ProCommon.Communal.InVarObj();
                                cfgUserCtrls.IN_ScramTrigger.InVar = ProCommon.Communal.InVar.IN05;
                                cfgUserCtrls.IN_ScramTrigger.EffectiveLevel = ProCommon.Communal.ElectricalLevel.High;
                                cfgUserCtrls.IN_ScramTrigger.EffectiveLogic = ProCommon.Communal.TriggerLogic.RaiseEdge;

                                cfgUserCtrls.IN_StopTrigger = new ProCommon.Communal.InVarObj();
                                cfgUserCtrls.IN_StopTrigger.VarName = "停止按钮";
                                cfgUserCtrls.IN_StopTrigger.InVar = ProCommon.Communal.InVar.IN03;
                                cfgUserCtrls.IN_StopTrigger.EffectiveLogic = ProCommon.Communal.TriggerLogic.RaiseEdge; 
                                                             
                                cfgUserCtrls.IN_ResetTrigger = new ProCommon.Communal.InVarObj();
                                cfgUserCtrls.IN_ResetTrigger.VarName = "复位按钮";
                                cfgUserCtrls.IN_ResetTrigger.InVar = ProCommon.Communal.InVar.IN04;
                                cfgUserCtrls.IN_ResetTrigger.EffectiveLogic = ProCommon.Communal.TriggerLogic.RaiseEdge;

                                cfgUserCtrls.IN_RunTrigger = new ProCommon.Communal.InVarObj();
                                cfgUserCtrls.IN_RunTrigger.VarName = "启动按钮";
                                cfgUserCtrls.IN_RunTrigger.InVar = ProCommon.Communal.InVar.IN02;
                                cfgUserCtrls.IN_RunTrigger.EffectiveLogic = ProCommon.Communal.TriggerLogic.RaiseEdge;
                                #endregion

                                #region 输出OutPut
                                                               
                                cfgUserCtrls.OUT_RunLight = new ProCommon.Communal.OutVarObj();
                                cfgUserCtrls.OUT_RunLight.VarName = "启动灯开关";
                                cfgUserCtrls.OUT_RunLight.OutVar = ProCommon.Communal.OutVar.OUT00;
                                cfgUserCtrls.OUT_RunLight.EffectiveLogic = ProCommon.Communal.TriggerLogic.LogicTrue;
                                cfgUserCtrls.OUT_StopLight = new ProCommon.Communal.OutVarObj();
                                cfgUserCtrls.OUT_StopLight.VarName = "停止灯开关";
                                cfgUserCtrls.OUT_StopLight.OutVar = ProCommon.Communal.OutVar.OUT01;
                                cfgUserCtrls.OUT_StopLight.EffectiveLogic = ProCommon.Communal.TriggerLogic.LogicTrue;
                                cfgUserCtrls.OUT_ResetLight = new ProCommon.Communal.OutVarObj();
                                cfgUserCtrls.OUT_ResetLight.VarName = "复位灯开关";
                                cfgUserCtrls.OUT_ResetLight.OutVar = ProCommon.Communal.OutVar.OUT02;
                                cfgUserCtrls.OUT_ResetLight.EffectiveLogic = ProCommon.Communal.TriggerLogic.LogicTrue;

                                #endregion

                                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigUserControls>(cfgUserCtrls,
                                   Pro2DBarcode.Manager.ConfigManager.ConfigDirectory + "\\" + Pro2DBarcode.Manager.ConfigManager.USERCONTROLS_CONFIG_FILE_NAME);

                            }
                            break;
                        #endregion

                        #region 板卡配置
                        case Pro2DBarcode.Manager.ConfigManager.BOARD_CONFIG_FILE_NAME:
                            {
                                Pro2DBarcode.Config.ConfigBoardCtrller cfgBoard = new Config.ConfigBoardCtrller();

                                if (_tmpCfgSystem != null
                                    && _tmpCfgSystem.CtrllerCategoryArray != null)
                                {
                                    bool isCreate = false;
                                    foreach (ProCommon.Communal.CtrllerCategory cc in _tmpCfgSystem.CtrllerCategoryArray)
                                    {
                                        if (cc == ProCommon.Communal.CtrllerCategory.Board)
                                            isCreate = true;
                                    }

                                    if (isCreate)
                                    {
                                        // 根据项目需要,创建默认恢复的板卡配置文件
                                        ProCommon.Communal.BoardCtrller boardCtrller = new ProCommon.Communal.BoardCtrller(ProCommon.Communal.CtrllerBrand.ZMotion, ProCommon.Communal.CtrllerType.Board_EtherNet, "MainCtrller", 0);
                                        boardCtrller.IsConnected = false;
                                        boardCtrller.ReconnectInterval = 500;
                                        boardCtrller.CtrllerEthernetIP = "192.168.0.11";
                                        boardCtrller.AcquireInterval = 50;

                                        //创建硬件配置时的板卡轴配置
                                        ProCommon.Communal.AxisList axisList = new ProCommon.Communal.AxisList();
                                        CreateAxesInstance(ref axisList);
                                        for (int i = 0; i < axisList.Count; i++)
                                            boardCtrller.AxisList.Add(axisList[i]);


                                        foreach (ProCommon.Communal.InVar itm in Enum.GetValues(typeof(ProCommon.Communal.InVar)))
                                        {
                                            if (itm != ProCommon.Communal.InVar.NONE)
                                            {
                                                ProCommon.Communal.InVarObj inVarObj = new ProCommon.Communal.InVarObj();
                                                inVarObj.VarName = ((ProCommon.Communal.InVar)itm).ToString();
                                                inVarObj.VarAddress = ((ProCommon.Communal.InVar)itm).ToString();
                                                inVarObj.VarType = "IN";
                                                inVarObj.InVar = (ProCommon.Communal.InVar)itm;
                                                inVarObj.EffectiveLevel = ProCommon.Communal.ElectricalLevel.Low;
                                                inVarObj.EffectiveLogic = ProCommon.Communal.TriggerLogic.LogicTrue;
                                                inVarObj.Description = "输入口" + itm.ToString();
                                                boardCtrller.InVarObjList.Add(inVarObj);
                                                inVarObj = null;
                                            }
                                        }

                                        //根据轴列表对应的专用报警信号端口位,专用原点硬限信号端口位,专用正向硬限信号端口位,专用负向硬限信号端口位更新其有效电平以及触发逻辑
                                        if (axisList != null)
                                        {
                                            for (int i = 0; i < axisList.Count; i++)
                                            {
                                                ProCommon.Communal.InVar inV = ProCommon.Communal.Functions.GetInVarByIdx(axisList[i].ALMIn);
                                                if (inV != ProCommon.Communal.InVar.NONE)
                                                {
                                                    boardCtrller.InVarObjList[inV].EffectiveLevel = axisList[i].ALMInLevel;
                                                }

                                                inV = ProCommon.Communal.Functions.GetInVarByIdx(axisList[i].HRevIn);
                                                if (inV != ProCommon.Communal.InVar.NONE)
                                                {
                                                    boardCtrller.InVarObjList[inV].EffectiveLevel = axisList[i].HRevInLevel;
                                                }

                                                inV = ProCommon.Communal.Functions.GetInVarByIdx(axisList[i].DatumIn);
                                                if (inV != ProCommon.Communal.InVar.NONE)
                                                {
                                                    boardCtrller.InVarObjList[inV].EffectiveLevel = axisList[i].DatumInLevel;
                                                }

                                                inV = ProCommon.Communal.Functions.GetInVarByIdx(axisList[i].HFwdIn);
                                                if (inV != ProCommon.Communal.InVar.NONE)
                                                {
                                                    boardCtrller.InVarObjList[inV].EffectiveLevel = axisList[i].HFwdInLevel;
                                                }
                                            }
                                        }

                                        foreach (ProCommon.Communal.OutVar itm in Enum.GetValues(typeof(ProCommon.Communal.OutVar)))
                                        {
                                            if (itm != ProCommon.Communal.OutVar.NONE)
                                            {
                                                ProCommon.Communal.OutVarObj outVarObj = new ProCommon.Communal.OutVarObj();
                                                outVarObj.VarName = itm.ToString();
                                                outVarObj.VarAddress = itm.ToString();
                                                outVarObj.VarType = "OUT";
                                                outVarObj.OutVar = itm;
                                                outVarObj.EffectiveLevel = ProCommon.Communal.ElectricalLevel.Low;
                                                outVarObj.Description = "输出口" + itm.ToString();
                                                boardCtrller.OutVarObjList.Add(outVarObj);
                                                outVarObj = null;
                                            }
                                        }

                                        cfgBoard.BoardCtrllerList.Add(boardCtrller);
                                    }
                                                                       
                                    Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigBoardCtrller>(cfgBoard,
                                           Pro2DBarcode.Manager.ConfigManager.ConfigDirectory + "\\" + Pro2DBarcode.Manager.ConfigManager.BOARD_CONFIG_FILE_NAME);
                                }
                            }                           
                            break;
                        #endregion

                        #region 通信Socket配置
                        case Pro2DBarcode.Manager.ConfigManager.COMSOCKET_CONFIG_FILE_NAME:
                            {
                                Pro2DBarcode.Config.ConfigSocket cfgSocket = new Config.ConfigSocket();
                                if (_tmpCfgSystem != null
                                   && _tmpCfgSystem.CtrllerCategoryArray != null)
                                {
                                    bool isCreate = false;
                                    foreach (ProCommon.Communal.CtrllerCategory cc in _tmpCfgSystem.CtrllerCategoryArray)
                                    {
                                        if (cc == ProCommon.Communal.CtrllerCategory.Socket)
                                            isCreate = true;
                                    }

                                    if (isCreate)
                                    {
                                        // 根据项目需要,创建默认恢复的Socket配置文件
                                        ProCommon.Communal.ComWrappedSocket comWrpSocket = new ProCommon.Communal.ComWrappedSocket();
                                        comWrpSocket.IP = "192.168.1.11";
                                        comWrpSocket.Name = "测试网口";
                                        cfgSocket.ComSocketList.Add(comWrpSocket);
                                    }                                   
                                }
                                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigSocket>(cfgSocket,
                                     Pro2DBarcode.Manager.ConfigManager.ConfigDirectory + "\\" + Pro2DBarcode.Manager.ConfigManager.COMSOCKET_CONFIG_FILE_NAME);
                            }
                            break;
                        #endregion

                        #region 通信Serial配置
                        case Pro2DBarcode.Manager.ConfigManager.COMSERIAL_CONFIG_FILE_NAME:
                            {
                                Pro2DBarcode.Config.ConfigSerialPort cfgSerial = new Config.ConfigSerialPort();
                                if (_tmpCfgSystem != null
                                  && _tmpCfgSystem.CtrllerCategoryArray != null)
                                {
                                    bool isCreate = false;
                                    foreach (ProCommon.Communal.CtrllerCategory cc in _tmpCfgSystem.CtrllerCategoryArray)
                                    {
                                        if (cc == ProCommon.Communal.CtrllerCategory.SerialPort)
                                            isCreate = true;
                                    }

                                    if (isCreate)
                                    {
                                        // 根据项目需要,创建默认恢复的SerialPort配置文件
                                        ProCommon.Communal.ComWrappedSerialPort comSerialPort1 = new ProCommon.Communal.ComWrappedSerialPort(_tmpCfgSystem.SerialPortNameFor2DCode, 0);
                                        comSerialPort1.AcquireInterval = 500;
                                        comSerialPort1.CtrllerBrand = ProCommon.Communal.CtrllerBrand.Microsoft;
                                        comSerialPort1.CtrllerCategory = ProCommon.Communal.CtrllerCategory.PC;
                                        comSerialPort1.ProcessInterval = 1000;
                                        comSerialPort1.ReceiveTimeOut = 500;
                                        comSerialPort1.ReconnectInterval = 1000;
                                        comSerialPort1.SendTimeOut = 500;
                                        comSerialPort1.HandShake = System.IO.Ports.Handshake.None;                                      
                                        comSerialPort1.BaudRate =115200;
                                        comSerialPort1.DataBits = 8;
                                        comSerialPort1.StopBits = System.IO.Ports.StopBits.One;
                                        comSerialPort1.Parity = System.IO.Ports.Parity.None;                                       
                                        comSerialPort1.DtrEnable = true;
                                        comSerialPort1.RtsEnable = true;
                                        comSerialPort1.ReceivedBytesThreshold = 8;
                                        comSerialPort1.NewLine = "/r/n";
                                        cfgSerial.ComSerialPortList.Add(comSerialPort1);                                                                            
                                    }
                                }
                                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigSerialPort>(cfgSerial,
                                   Pro2DBarcode.Manager.ConfigManager.ConfigDirectory + "\\" + Pro2DBarcode.Manager.ConfigManager.COMSERIAL_CONFIG_FILE_NAME);
                            }
                            break;
                        #endregion

                        #region 通信WebService配置
                        case Pro2DBarcode.Manager.ConfigManager.COMWEB_CONFIG_FILE_NAME:
                            {
                                Pro2DBarcode.Config.ConfigWeb cfgWeb = new Config.ConfigWeb();
                                if (_tmpCfgSystem != null
                                  && _tmpCfgSystem.CtrllerCategoryArray != null)
                                {
                                    bool isCreate = false;
                                    foreach (ProCommon.Communal.CtrllerCategory cc in _tmpCfgSystem.CtrllerCategoryArray)
                                    {
                                        if (cc == ProCommon.Communal.CtrllerCategory.Web)
                                            isCreate = true;
                                    }

                                    if (isCreate)
                                    {
                                        // 根据项目需要,创建默认恢复的Socket配置文件
                                        ProCommon.Communal.ComWrappedWeb comWeb = new ProCommon.Communal.ComWrappedWeb();
                                        comWeb.Name = "测试网服";
                                        comWeb.IsConnected = false;
                                        cfgWeb.ComWebList.Add(comWeb);
                                    }
                                }
                                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigWeb>(cfgWeb,
                                     Pro2DBarcode.Manager.ConfigManager.ConfigDirectory + "\\" + Pro2DBarcode.Manager.ConfigManager.COMWEB_CONFIG_FILE_NAME);
                            }
                            break;
                        #endregion

                        #region 视觉参数配置
                        case Pro2DBarcode.Manager.ConfigManager.VISIONPARA_CONFIG_FILE_NAME:
                            {
                                Pro2DBarcode.Config.ConfigVisionPara cfgVisionPara = new Pro2DBarcode.Config.ConfigVisionPara();
                                #region 项目用视觉参数 
                                                              
                                cfgVisionPara.Para2DBarcode = new Pro2DBarcode.Config.VisionPara.VisionPara2DBarcode() {
                                    CameraGain = 1.5f,
                                    CameraExposure = 99000.0f,
                                    CodeType= "Data Matrix ECC 200",
                                    GenParameterNamesForCreate=new string[] { "default_parameters" },
                                    GenParameterValuesForCreate=new string[] { "maximum_recognition" },
                                    TimeOut=500,
                                    ToBeFoundNumber=1,
                                    ColorStrForResultNotFound="red",
                                    ColorStrForResultFound="green"                                   
                                };//类似的处理,需要根据项目实现初始默认值
                                #endregion

                                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigVisionPara>(cfgVisionPara,
                                   Pro2DBarcode.Manager.ConfigManager.ConfigDirectory + "\\" + Pro2DBarcode.Manager.ConfigManager.VISIONPARA_CONFIG_FILE_NAME);
                            }
                            break;
                        #endregion

                        #region 委托运控参数配置
                        case Pro2DBarcode.Manager.ConfigManager.MOTIONPARA_CONFIG_FILE_NAME:
                            {
                                Pro2DBarcode.Config.ConfigMotionPara cfgMotionPara = new Pro2DBarcode.Config.ConfigMotionPara();
                                #region 项目用运控参数                              
                                #endregion

                                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigMotionPara>(cfgMotionPara,
                                 Pro2DBarcode.Manager.ConfigManager.ConfigDirectory + "\\" + Pro2DBarcode.Manager.ConfigManager.MOTIONPARA_CONFIG_FILE_NAME);
                            }
                            break;
                        #endregion
                        default:
                            break;
                    }

                    if (bkgrdWorker != null)
                        bkgrdWorker.ReportProgress(ChkCount, "Create file[" + fileName + "]successfully !");
                    rt = true;
                }
                catch(System.Exception ex)
                {
                    ChkErr = "Create file[" + fileName+ "]failed!Description:\r\n" + ex.Message;
                    if (bkgrdWorker != null)
                        bkgrdWorker.ReportProgress(ChkCount, ChkErr);
                }             
            }
            else
            {
                //若系统配置文件存在,加载系统配置文件，为其他依赖系统配置的配置做准备
                if (fileName == Pro2DBarcode.Manager.ConfigManager.SYSTEM_CONFIG_FILE_NAME)
                {
                    _tmpCfgSystem = Pro2DBarcode.Config.ConfigAPIHandle.Load<Pro2DBarcode.Config.ConfigSystem>(Pro2DBarcode.Manager.ConfigManager.ConfigDirectory + "\\" + Pro2DBarcode.Manager.ConfigManager.SYSTEM_CONFIG_FILE_NAME);
                }

                if (bkgrdWorker != null)
                    bkgrdWorker.ReportProgress(ChkCount, "[" + fileName + "]found !");
                rt = true;
            }
            return rt;
        }

        /// <summary>
        /// 创建硬件配置时的板卡轴配置
        /// </summary>
        /// <param name="axesList"></param>
        /// <returns></returns>
        private bool CreateAxesInstance(ref ProCommon.Communal.AxisList axesList)
        {
            bool rt = false;
            if (axesList != null)
            {
                axesList.Clear();
                int tmpServoOnPort = 0, tmpALMIn = 0, tmpDatumIn = 0, tmpHFwdIn = 0, tmpHRevIn = 0, tmpALMCLROut = 0;
                ProCommon.Communal.Axis axis = null;
                for (int i = 0; i < 4; i++)
                {
                    axis = new ProCommon.Communal.Axis(i, "轴");
                    axis.Type = 1;
                    axis.PulseOutMode = 0;
                    axis.ServoOnLevel = ProCommon.Communal.ElectricalLevel.Low;
                    axis.PulseUnit = 100;
                    switch (i)
                    {
                        case 0:
                            {
                                axis.Name = "X";
                                tmpServoOnPort = Convert.ToInt32(ProCommon.Communal.OutVar.OUT16);
                                tmpALMIn = Convert.ToInt32(ProCommon.Communal.InVar.IN40);
                                tmpDatumIn = Convert.ToInt32(ProCommon.Communal.InVar.IN16);
                                tmpHFwdIn = Convert.ToInt32(ProCommon.Communal.InVar.IN20);
                                tmpHRevIn = Convert.ToInt32(ProCommon.Communal.InVar.IN21);
                                tmpALMCLROut = Convert.ToInt32(ProCommon.Communal.OutVar.OUT17);
                            }
                            break;
                        case 1:
                            {
                                axis.Name = "Y";
                                tmpServoOnPort = Convert.ToInt32(ProCommon.Communal.OutVar.OUT18);
                                tmpALMIn = Convert.ToInt32(ProCommon.Communal.InVar.IN41);
                                tmpDatumIn = Convert.ToInt32(ProCommon.Communal.InVar.IN17);
                                tmpHFwdIn = Convert.ToInt32(ProCommon.Communal.InVar.IN22);
                                tmpHRevIn = Convert.ToInt32(ProCommon.Communal.InVar.IN23);
                                tmpALMCLROut = Convert.ToInt32(ProCommon.Communal.OutVar.OUT19);
                            }
                            break;
                        case 2:
                            {
                                axis.Name = "R";
                                tmpServoOnPort = Convert.ToInt32(ProCommon.Communal.OutVar.OUT20);
                                tmpALMIn = Convert.ToInt32(ProCommon.Communal.InVar.IN42);
                                tmpDatumIn = Convert.ToInt32(ProCommon.Communal.InVar.IN18);
                                tmpHFwdIn = Convert.ToInt32(ProCommon.Communal.InVar.IN24);
                                tmpHRevIn = Convert.ToInt32(ProCommon.Communal.InVar.IN25);
                                tmpALMCLROut = Convert.ToInt32(ProCommon.Communal.OutVar.OUT21);
                            }
                            break;
                        case 3:
                            {
                                axis.Name = "U";
                                tmpServoOnPort = Convert.ToInt32(ProCommon.Communal.OutVar.OUT22);
                                tmpALMIn = Convert.ToInt32(ProCommon.Communal.InVar.IN43);
                                tmpDatumIn = Convert.ToInt32(ProCommon.Communal.InVar.IN19);
                                tmpHFwdIn = Convert.ToInt32(ProCommon.Communal.InVar.IN26);
                                tmpHRevIn = Convert.ToInt32(ProCommon.Communal.InVar.IN27);
                                tmpALMCLROut = Convert.ToInt32(ProCommon.Communal.OutVar.OUT23);
                            }
                            break;
                        case 4:
                            {
                                tmpServoOnPort = Convert.ToInt32(ProCommon.Communal.OutVar.OUT24);
                                tmpALMIn = Convert.ToInt32(ProCommon.Communal.InVar.IN44);
                                tmpDatumIn = Convert.ToInt32(ProCommon.Communal.InVar.IN28);
                                tmpHFwdIn = Convert.ToInt32(ProCommon.Communal.InVar.IN32);
                                tmpHRevIn = Convert.ToInt32(ProCommon.Communal.InVar.IN33);
                                tmpALMCLROut = Convert.ToInt32(ProCommon.Communal.OutVar.OUT25);
                            }
                            break;
                        case 5:
                            {
                                tmpServoOnPort = Convert.ToInt32(ProCommon.Communal.OutVar.OUT26);
                                tmpALMIn = Convert.ToInt32(ProCommon.Communal.InVar.IN45);
                                tmpDatumIn = Convert.ToInt32(ProCommon.Communal.InVar.IN29);
                                tmpHFwdIn = Convert.ToInt32(ProCommon.Communal.InVar.IN34);
                                tmpHRevIn = Convert.ToInt32(ProCommon.Communal.InVar.IN35);
                                tmpALMCLROut = Convert.ToInt32(ProCommon.Communal.OutVar.OUT27);
                            }
                            break;
                        case 6:
                            {
                                tmpServoOnPort = Convert.ToInt32(ProCommon.Communal.OutVar.OUT28);
                                tmpALMIn = Convert.ToInt32(ProCommon.Communal.InVar.IN46);
                                tmpDatumIn = Convert.ToInt32(ProCommon.Communal.InVar.IN30);
                                tmpHFwdIn = Convert.ToInt32(ProCommon.Communal.InVar.IN36);
                                tmpHRevIn = Convert.ToInt32(ProCommon.Communal.InVar.IN37);
                                tmpALMCLROut = Convert.ToInt32(ProCommon.Communal.OutVar.OUT29);
                            }
                            break;
                        case 7:
                            {
                                tmpServoOnPort = Convert.ToInt32(ProCommon.Communal.OutVar.OUT30);
                                tmpALMIn = Convert.ToInt32(ProCommon.Communal.InVar.IN47);
                                tmpDatumIn = Convert.ToInt32(ProCommon.Communal.InVar.IN31);
                                tmpHFwdIn = Convert.ToInt32(ProCommon.Communal.InVar.IN38);
                                tmpHRevIn = Convert.ToInt32(ProCommon.Communal.InVar.IN39);
                                tmpALMCLROut = Convert.ToInt32(ProCommon.Communal.OutVar.OUT31);
                            }
                            break;
                    }

                    //通用输入或输出口做专用信号
                    axis.ServoONPort = tmpServoOnPort;
                    axis.ALMIn = tmpALMIn;
                    axis.ALMInLevel = ProCommon.Communal.ElectricalLevel.High;
                    axis.ALMCLROut = tmpALMCLROut;
                    axis.ALMCLROutLevel = 0;
                    axis.StatusALM = false;
                    axis.DatumIn = tmpDatumIn;
                    axis.DatumInLevel = ProCommon.Communal.ElectricalLevel.Low;
                    axis.StatusDatum = false;
                    axis.HFwdIn = tmpHFwdIn;
                    axis.HFwdInLevel = ProCommon.Communal.ElectricalLevel.Low;
                    axis.StatusFwd = false;
                    axis.HRevIn = tmpHRevIn;
                    axis.HRevInLevel = ProCommon.Communal.ElectricalLevel.Low;
                    axis.StatusRev = false;
                    axis.DatumDir = "负向";
                    axis.DatumMode = 14;

                    axis.StartSpeed = 10.0f;
                    axis.RunSpeed = 100.0f;
                    axis.Accel = 50.0f;
                    axis.Decel = 50.0f;
                    axis.DatumCreepSpeed = 10.0f;

                    axis.CurrentPos = 0.0f;
                    axis.FirstPos = 10.0f;
                    axis.SecondPos = 50.0f;
                    axis.ThirdPos = 80.0f;

                    axesList.Add(axis);
                }

                rt = true;
            }
            return rt;
        }

        #endregion
    }
}
