using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pro2DBarcode.Config;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ConfigManager
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Manager
 * File      Name：       ConfigManager
 * Creating  Time：       1/15/2020 4:01:18 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Manager
{
    /// <summary>
    /// 配置文件管理器
    /// [管理系统配置文件]
    /// </summary>
    public class ConfigManager
    {
        #region 静态单例

        static object _syncObj = new object();
        static ConfigManager _instance;
        public static ConfigManager Instance
        {
            get
            {
                lock (_syncObj)
                {
                    if (_instance == null)
                    { _instance = new ConfigManager(); }
                }

                return _instance;
            }
        }

        private ConfigManager()
        {
            _isEventRegistered = false;
        }

        #endregion

        /// <summary>
        /// 参考根目录
        /// [应用程序所在目录,包括"\"]
        /// </summary>
        public static string DirectoryBase { get { return System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase; } }

        public static string ConfigDirectory { get { return DirectoryBase+DIRECTORY_NAME_FOR_CONFIG; } }

        #region 目录及目录下的文件
        public const string DIRECTORY_NAME_FOR_CONFIG = "Config";
        public const string DIRECTORY_NAME_FOR_LOG = "Log";
        public const string DIRECTORY_NAME_FOR_DATABASE = "DataBase";
        public const string DIRECTORY_NAME_FOR_ROUTINE = "Routine";
        public const string DIRECTORY_NAME_FOR_RESOURCES = "Resource";

        /// <summary>
        /// 获取基础目录名称
        /// </summary>
        public static string[] DirectoryNames
        {
            get
            {
                return new string[]
                {   DIRECTORY_NAME_FOR_CONFIG,
                    DIRECTORY_NAME_FOR_DATABASE,
                    DIRECTORY_NAME_FOR_LOG,
                    DIRECTORY_NAME_FOR_ROUTINE,
                    DIRECTORY_NAME_FOR_RESOURCES
                };
            }
        }

        public const string SYSTEM_CONFIG_FILE_NAME = "System.config";
        public const string ACCOUNT_CONFIG_FILE_NAME = "Account.config";
        public const string ALARM_CONFIG_FILE_NAME = "Alarm.config";
        public const string CAMERA_CONFIG_FILE_NAME = "Camera.config";
        public const string BOARD_CONFIG_FILE_NAME = "Board.config";
        public const string USERCONTROLS_CONFIG_FILE_NAME = "UserControls.config";
        public const string COMSERIAL_CONFIG_FILE_NAME = "SerialPort.config";
        public const string COMSOCKET_CONFIG_FILE_NAME = "Socket.config";       
        public const string COMWEB_CONFIG_FILE_NAME = "Web.config"; 
        public const string VISIONPARA_CONFIG_FILE_NAME = "VisionPara.config";
        public const string MOTIONPARA_CONFIG_FILE_NAME = "MotionPara.config";

        /// <summary>
        /// 获取配置文件名称组
        /// </summary>
        public static string[] ConfigFileNames
        {
            get
            {
                return new string[]
                {
                  SYSTEM_CONFIG_FILE_NAME,
                  ACCOUNT_CONFIG_FILE_NAME,
                  ALARM_CONFIG_FILE_NAME,
                  CAMERA_CONFIG_FILE_NAME,
                  BOARD_CONFIG_FILE_NAME,
                  USERCONTROLS_CONFIG_FILE_NAME,
                  COMSERIAL_CONFIG_FILE_NAME,
                  COMSOCKET_CONFIG_FILE_NAME,
                  COMWEB_CONFIG_FILE_NAME,
                  VISIONPARA_CONFIG_FILE_NAME,
                  MOTIONPARA_CONFIG_FILE_NAME
                };
            }
        }

        public const string SYSTEM_LOG_FILE_NAME = "LogSystem.txt";
        public const string EXCEPTION_LOG_FILE_NAME = "LogException.txt";

        /// <summary>
        /// 获取日志文件名称组
        /// </summary>
        public static string[] LogFileNames
        {
            get { return new string[] { SYSTEM_LOG_FILE_NAME, EXCEPTION_LOG_FILE_NAME }; }
        }


        public const string DATA_AND_LOG_FILE_NAME = "DataAndLog.accdb";
        /// <summary>
        /// 获取数据库文件名称组
        /// [注:存储流水结果,流水日志,报警日志的数据库文件]
        /// </summary>
        public static string DataAndLogFileName
        {
            get { return DATA_AND_LOG_FILE_NAME; }
        }

        public const string RUNDATA_SHEET_NAME = "RunData";
        public const string RUNLOG_SHEET_NAME = "RunLog";
        public const string ALARMLOG_SHEET_NAME = "AlarmLog";

        /// <summary>
        /// 数据表名称组
        /// [注:流水结果表名称,流水日志表名称,报警日志表名称,存储于数据库]
        /// </summary>
        public static string[] DataAndLogSheetNames
        {
            get { return new string[] { RUNDATA_SHEET_NAME, RUNLOG_SHEET_NAME, ALARMLOG_SHEET_NAME }; }
        }     

        #endregion     

        /// <summary>
        /// 系统配置
        /// </summary>
        public Pro2DBarcode.Config.ConfigSystem CfgSystem { set; get; }       

        /// <summary>
        /// 账户配置
        /// </summary>
        public Pro2DBarcode.Config.ConfigAccount CfgAccount { set; get; }  

        /// <summary>
        /// 相机配置
        /// </summary>
        public Pro2DBarcode.Config.ConfigCamera CfgCamera { set; get; }
       
        /// <summary>
        /// 板卡配置
        /// </summary>
        public Pro2DBarcode.Config.ConfigBoardCtrller CfgBoardCtrller { set; get; }      

        /// <summary>
        /// 串口配置
        /// </summary>
        public Pro2DBarcode.Config.ConfigSerialPort CfgSerialPort { set; get; }
      

        /// <summary>
        /// Socket配置
        /// </summary>
        public Pro2DBarcode.Config.ConfigSocket CfgSocket { set; get; }      

        /// <summary>
        /// Web配置
        /// </summary>
        public Pro2DBarcode.Config.ConfigWeb CfgWeb { set; get; }      

        /// <summary>
        /// 图像处理参数配置
        /// </summary>
        public Pro2DBarcode.Config.ConfigVisionPara CfgVisionPara { set; get; }

        /// <summary>
        /// 运控参数配置
        /// </summary>
        public Pro2DBarcode.Config.ConfigMotionPara CfgMotionPara { get; internal set; }

        private bool _isEventRegistered;

        public void Load()
        {
            CfgSystem = Pro2DBarcode.Config.ConfigAPIHandle.Load<Pro2DBarcode.Config.ConfigSystem>(ConfigDirectory+"\\"+ SYSTEM_CONFIG_FILE_NAME);
            CfgAccount = Pro2DBarcode.Config.ConfigAPIHandle.Load<Pro2DBarcode.Config.ConfigAccount>(ConfigDirectory + "\\"+ ACCOUNT_CONFIG_FILE_NAME);
            CfgCamera = Pro2DBarcode.Config.ConfigAPIHandle.Load<Pro2DBarcode.Config.ConfigCamera>(ConfigDirectory + "\\"+ CAMERA_CONFIG_FILE_NAME);
            CfgBoardCtrller = Pro2DBarcode.Config.ConfigAPIHandle.Load<Pro2DBarcode.Config.ConfigBoardCtrller>(ConfigDirectory + "\\"+ BOARD_CONFIG_FILE_NAME);
            CfgSerialPort = Pro2DBarcode.Config.ConfigAPIHandle.Load<Pro2DBarcode.Config.ConfigSerialPort>(ConfigDirectory + "\\"+ COMSERIAL_CONFIG_FILE_NAME);
            CfgSocket = Pro2DBarcode.Config.ConfigAPIHandle.Load<Pro2DBarcode.Config.ConfigSocket>(ConfigDirectory + "\\"+ COMSOCKET_CONFIG_FILE_NAME);
            CfgWeb = Pro2DBarcode.Config.ConfigAPIHandle.Load<Pro2DBarcode.Config.ConfigWeb>(ConfigDirectory + "\\"+ COMWEB_CONFIG_FILE_NAME);
            CfgVisionPara = Pro2DBarcode.Config.ConfigAPIHandle.Load<Pro2DBarcode.Config.ConfigVisionPara>(ConfigDirectory + "\\"+ VISIONPARA_CONFIG_FILE_NAME);
            CfgMotionPara= Pro2DBarcode.Config.ConfigAPIHandle.Load<Pro2DBarcode.Config.ConfigMotionPara>(ConfigDirectory + "\\" + MOTIONPARA_CONFIG_FILE_NAME);
            
            if (!_isEventRegistered)
            {
                //相机连接状态改变(更新UI界面)
                if (CfgCamera != null && CfgCamera.CameraList != null)
                {
                    //foreach (ProCommon.Communal.Camera camera in this.CfgCamera.CameraBList)
                    //{
                    //    camera.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Manager.UIManager.Instance.Connected_PropertyChanged);
                    //}

                    for (int i = 0; i < this.CfgCamera.CameraList.Count; i++)
                    {
                        this.CfgCamera.CameraList[i].PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Manager.UIManager.Instance.Connected_PropertyChanged);
                    }
                }

                //控制器连接状态改变(更新UI界面)
                if (CfgBoardCtrller != null && CfgBoardCtrller.BoardCtrllerList != null)
                {
                    for (int i = 0; i < this.CfgBoardCtrller.BoardCtrllerList.Count; i++)
                    {
                        CfgBoardCtrller.BoardCtrllerList[i].PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Manager.UIManager.Instance.Connected_PropertyChanged);
                    }
                }

                //SerialPort连接状态改变(更新UI界面)
                if (CfgSerialPort != null && CfgSerialPort.ComSerialPortBList != null)
                {
                    foreach (ProCommon.Communal.ComWrappedSerialPort comserialport in CfgSerialPort.ComSerialPortBList)
                    {
                        comserialport.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Manager.UIManager.Instance.Connected_PropertyChanged);
                    }
                }

                //Socket连接状态改变(更新UI界面)
                if (CfgSocket != null && CfgSocket.ComSocketList != null)
                {
                    foreach (ProCommon.Communal.ComWrappedSocket comsocket in CfgSocket.ComSocketBList)
                    {
                        comsocket.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Manager.UIManager.Instance.Connected_PropertyChanged);
                    }
                }

                //Web连接状态改变(更新UI界面)
                if (CfgWeb != null && CfgWeb.ComWebList != null)
                {
                    foreach (ProCommon.Communal.ComWrappedWeb comweb in CfgWeb.ComWebBList)
                    {
                        comweb.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Manager.UIManager.Instance.Connected_PropertyChanged);
                    }
                }

                //控制器的控制变量对象的属性值改变事件注册回调函数
                if (CfgBoardCtrller != null && CfgBoardCtrller.BoardCtrllerList != null)
                {
                    for (int i = 0; i < CfgBoardCtrller.BoardCtrllerList.Count; i++)
                    {
                        //输入控制变量对象的属性值改变事件注册回调函数
                        if (CfgBoardCtrller.BoardCtrllerList[i].InVarObjList != null)
                        {
                            for (int j = 0; j <CfgBoardCtrller.BoardCtrllerList[i].InVarObjList.Count; j++)
                            {
                               CfgBoardCtrller.BoardCtrllerList[i].InVarObjList[j].PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(AsyncVarObjPropertyChanged);
                            }
                        }

                        //输出控制变量对象的属性值改变事件注册回调函数
                        if (CfgBoardCtrller.BoardCtrllerList[i].OutVarObjList != null)
                        {
                            for (int j = 0; j <CfgBoardCtrller.BoardCtrllerList[i].OutVarObjList.Count; j++)
                            {
                                CfgBoardCtrller.BoardCtrllerList[i].OutVarObjList[j].PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(AsyncVarObjPropertyChanged);
                            }
                        }

                        //轴变量对象的属性值改变注册回调函数
                        if (CfgBoardCtrller.BoardCtrllerList[i].AxisList != null)
                        {
                            for (int j = 0; j < this.CfgBoardCtrller.BoardCtrllerList[i].AxisList.Count; j++)
                            {
                                CfgBoardCtrller.BoardCtrllerList[i].AxisList[j].PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(AsyncVarObjPropertyChanged);
                            }
                        }
                    }
                }
                _isEventRegistered = true;
            }          
        }

        public void Save()
        {
            if(CfgSystem!=null)
                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigSystem>(CfgSystem, ConfigDirectory + "\\"+ SYSTEM_CONFIG_FILE_NAME);

            if(CfgAccount!=null)
                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigAccount>(CfgAccount, ConfigDirectory + "\\"+ ACCOUNT_CONFIG_FILE_NAME);

            if (CfgCamera!=null)
                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigCamera>(CfgCamera, ConfigDirectory + "\\"+ CAMERA_CONFIG_FILE_NAME);

            if (CfgBoardCtrller != null)
                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigBoardCtrller>(CfgBoardCtrller, ConfigDirectory + "\\"+ BOARD_CONFIG_FILE_NAME);

            if (CfgSerialPort != null)
                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigSerialPort>(CfgSerialPort, ConfigDirectory + "\\"+ COMSERIAL_CONFIG_FILE_NAME);

            if (CfgSocket != null)
                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigSocket>(CfgSocket, ConfigDirectory + "\\"+ COMSOCKET_CONFIG_FILE_NAME);

            if (CfgWeb != null)
                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigWeb>(CfgWeb, ConfigDirectory + "\\"+ COMWEB_CONFIG_FILE_NAME);

            if (CfgVisionPara != null)
                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigVisionPara>(CfgVisionPara, ConfigDirectory + "\\"+ VISIONPARA_CONFIG_FILE_NAME);

            if (CfgMotionPara != null)
                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigMotionPara>(CfgMotionPara, ConfigDirectory + "\\" + MOTIONPARA_CONFIG_FILE_NAME);
        }

        /// <summary>
        /// 异步调用属性值改变对应的处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyncVarObjPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Pro2DBarcode.Manager.AsyncPropertyChangedEventHandler d = new Pro2DBarcode.Manager.AsyncPropertyChangedEventHandler(VarObjPropertyChanged);
            d.BeginInvoke(sender, e, null, d);
        }

        /// <summary>
        /// 调用属性值改变对应的处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VarObjPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {

                if (e.PropertyName == "InNewValue")
                {
                    ProCommon.Communal.InVarObj ctrlInVarObj = sender as ProCommon.Communal.InVarObj;
                    if (ctrlInVarObj != null)
                        Pro2DBarcode.Manager.SystemManager.Instance.InVarObjPropertyChangedHandler(ctrlInVarObj);
                }
                else if (e.PropertyName == "OutNewValue")
                {
                    ProCommon.Communal.OutVarObj ctrlOutVarObj = sender as ProCommon.Communal.OutVarObj;
                    if (ctrlOutVarObj != null)
                        Pro2DBarcode.Manager.SystemManager.Instance.OutVarObjPropertyChangedHandler(ctrlOutVarObj);
                }
                else if (e.PropertyName == "AxisCurrentPos")
                {
                    ProCommon.Communal.Axis axis = sender as ProCommon.Communal.Axis;
                    if (axis != null)
                        Pro2DBarcode.Manager.SystemManager.Instance.AxisObjPropertyChangedHandler(axis);
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
            }
        }
    }
}
