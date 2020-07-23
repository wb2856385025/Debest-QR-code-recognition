using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ConfigSystem
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Config
 * File      Name：       ConfigSystem
 * Creating  Time：       1/15/2020 3:54:45 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Config
{
    [Serializable]
    public class ConfigSystem : Config
    {
        #region 系统配置  

        /// <summary>
        /// 系统语言版本
        /// </summary>
        public ProCommon.Communal.Language LanguageVersion { set; get; }
      

        /// <summary>
        /// 客户名称
        /// </summary>
        public string ClientName { set; get; }

        /// <summary>
        /// 视图数量
        /// </summary>
        public int ViewNumber { set; get; }

        /// <summary>
        /// 自启动
        /// </summary>
        public bool EnableAutoLaunch { set; get; }

        /// <summary>
        /// 当前系统控制器种类列表
        /// </summary>
        public ProCommon.Communal.CtrllerCategory[] CtrllerCategoryArray { set; get; }

        /// <summary>
        /// 二维码识别的运控板卡名称
        /// </summary>
        public string BoardCtrllerNamerFor2DCode { set; get; }

        /// <summary>
        /// 二维码识别通信的串口名称
        /// </summary>
        public string SerialPortNameFor2DCode { set; get; }

        /// <summary>
        /// 串口通信字符串有效开始符
        /// </summary>
        public string SerialPortPackHeadStr { set; get; }

        /// <summary>
        /// 串口通信字符串结束符
        /// </summary>
        public string SerialPortPackEndStr { set; get; }

        /// <summary>
        /// 二维码识别的相机名称
        /// </summary>
        public string CameraNameFor2DCode { set; get; }

        /// <summary>
        /// 识别错误用户定义字符串
        /// </summary>
        public string ResultNGStr { get; set; }

        /// <summary>
        /// 识别结果分隔字符
        /// </summary>
        public string ResultSplitStr { set; get; }

        private string _routineDirectory;
        private static string _defaultRoutineDirectory = Manager.ConfigManager.DirectoryBase
                                                   + Manager.ConfigManager.DIRECTORY_NAME_FOR_ROUTINE;
        /// <summary>
        /// 程式目录
        /// </summary>
        public string RoutineDirectory
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _routineDirectory = value;
            }
            get
            {
                return string.IsNullOrEmpty(_routineDirectory) ? _defaultRoutineDirectory : _routineDirectory;
            }
        }

        #region 流水结果,流水日志,报警日志的数据库存储

        private string _dataBaseDirectory;
        private static string _defaultDataBaseDirectory= Manager.ConfigManager.DirectoryBase
                                                   + Manager.ConfigManager.DIRECTORY_NAME_FOR_DATABASE;

        /// <summary>
        /// 数据和日志目录
        /// [注:包括流水结果,流水日志,报警日志的数据库]
        /// </summary>
        public string DataBaseDirectory
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _dataBaseDirectory = value;
            }
            get
            {
                return string.IsNullOrEmpty(_dataBaseDirectory) ? _defaultDataBaseDirectory : _dataBaseDirectory;
            }
        }

        //---------------------------------------------------------------------------------------

        private string _runDataSheetName;
        private static string _defaultRunDataSheetName = Manager.ConfigManager.RUNDATA_SHEET_NAME;

        /// <summary>
        /// 流水结果表名称
        /// </summary>
        public string RunDataSheetName
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _runDataSheetName = value;
            }
            get
            {
                return string.IsNullOrEmpty(_runDataSheetName) ? _defaultRunDataSheetName : _runDataSheetName;
            }
        }

        /// <summary>
        /// 是否保存结果数据
        /// </summary>
        public bool EnableSaveRunData { set; get; }

        /// <summary>
        /// 保存数据结果的天数
        /// </summary>
        public int SaveRunDataDays { set; get; }

        /// <summary>
        /// 显示数据结果的记录条行数
        /// </summary>
        public int DisplayRunDataCount { set; get; }

        //--------------------------------------------------------------------------------------

        private string _runLogSheetName;
        private static string _defaultRunLogSheetName = Manager.ConfigManager.RUNLOG_SHEET_NAME;

        /// <summary>
        /// 流水日志表名称
        /// </summary>
        public string RunLogSheetName
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _runLogSheetName = value;
            }
            get
            {
                return string.IsNullOrEmpty(_runLogSheetName) ? _defaultRunLogSheetName : _runLogSheetName;
            }
        }

        /// <summary>
        /// 是否保存流水日志
        /// [注:生产工艺的记录]
        /// </summary>
        public bool EnableSaveRunLog { set; get; }

        /// <summary>
        /// 保存流水日志的天数
        /// </summary>
        public int SaveRunLogDays { set; get; }

        /// <summary>
        /// 显示流水日志的记录条行数
        /// </summary>
        public int DisplayRunLogCount { set; get; }

        //------------------------------------------------------------------------------------

        private string _alarmLogSheetName;
        private static string _defaultAlarmLogSheetName = Manager.ConfigManager.ALARMLOG_SHEET_NAME;

        /// <summary>
        /// 流水日志表名称
        /// </summary>
        public string AlarmLogSheetName
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _alarmLogSheetName = value;
            }
            get
            {
                return string.IsNullOrEmpty(_alarmLogSheetName) ? _defaultAlarmLogSheetName : _alarmLogSheetName;
            }
        }

        /// <summary>
        /// 是否保存报警日志
        /// </summary>
        public bool EnableSaveAlarmLog { set; get; }

        /// <summary>
        /// 保存报警日志的天数
        /// </summary>
        public int SaveAlarmLogDays { set; get; }

        /// <summary>
        /// 显示报警日志的记录条行数
        /// </summary>
        public int DisplayAlarmLogCount { set; get; }

        #endregion

        private string _sysLogFilePath; //系统日志文件路径
        private static string _defaultSysLogFilePath = Manager.ConfigManager.DirectoryBase
                                                    + Manager.ConfigManager.DIRECTORY_NAME_FOR_LOG
                                                    + "\\" + Manager.ConfigManager.SYSTEM_LOG_FILE_NAME;     

        /// <summary>
        /// 系统日志文件路径
        /// </summary>
        public string SystemLogFilePath
        {
            set
            {
                if (value != null)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        int idx = value.LastIndexOf('.');
                        if (idx < 0)
                        {
                            _sysLogFilePath = value+ ".txt";
                        }
                        else
                        {
                            _sysLogFilePath = value;
                        }
                    }
                    else
                    {
                        _sysLogFilePath = _defaultSysLogFilePath;
                    }
                }                   
            }
            get
            {
                return string.IsNullOrEmpty(_sysLogFilePath) ? _defaultSysLogFilePath : _sysLogFilePath;
            }
        }

        private string _exLogFilePath; //异常日志文件路径       
        private static string _defaultExLogFilePath =Manager.ConfigManager.DirectoryBase
                                                    + Manager.ConfigManager.DIRECTORY_NAME_FOR_LOG
                                                    +"\\"+ Manager.ConfigManager.EXCEPTION_LOG_FILE_NAME;
       
        /// <summary>
        /// 异常日志文件路径
        /// </summary>
        public string ExceptionLogFilePath
        {
            set
            {
                if (value != null)
                {
                    if(!string.IsNullOrEmpty(value))
                    {
                        int idx = value.LastIndexOf('.');
                        if (idx < 0)
                        {
                            _exLogFilePath = value + ".txt";
                        }
                        else
                        {
                            _exLogFilePath = value;
                        }
                    }
                    else
                    {
                        _exLogFilePath = _defaultExLogFilePath;
                    }
                }
            }
            get
            {
                return string.IsNullOrEmpty(_exLogFilePath) ? _defaultExLogFilePath : _exLogFilePath;
            }
        }

        /// <summary>
        /// 启用上次生产程式
        /// [注:]
        /// </summary>
        public bool EnableLastRoutine { set; get; }

        /// <summary>
        /// 上次生产程式路径
        /// </summary>
        public string LastRoutinePath { set; get; }     

        /// <summary>
        /// 产品OK数量
        /// </summary>
        public int ProOK { get; set; }

        /// <summary>
        /// 产品NG数量
        /// </summary>
        public int ProNG { get; set; }

        /// <summary>
        /// 产品总数
        /// </summary>
        public int ProTotal { get; set; }

        /// <summary>
        /// 产品良率
        /// </summary>
        public float ProYieldRatio { get; set; }

        #endregion
    }
}
