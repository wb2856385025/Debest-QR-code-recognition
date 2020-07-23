using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       DeviceManager
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Manager
 * File      Name：       DeviceManager
 * Creating  Time：       1/15/2020 5:48:26 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Manager
{
    public delegate void AsyncPropertyChangedEventHandler(object sender, System.ComponentModel.PropertyChangedEventArgs e);

    /// <summary>
    /// 设备管理器
    /// [管理系统配置的所有设备]
    /// </summary>
    public class DeviceManager
    {
        #region 静态单例

        static object _syncObj = new object();
        static DeviceManager _instance;
        public static DeviceManager Instance
        {
            get
            {
                lock (_syncObj)
                {
                    if (_instance == null)
                    {
                        _instance = new DeviceManager();
                    }
                }

                return _instance;
            }
        }

        private DeviceManager()
        {
            DeviceList = new System.Collections.SortedList();
            _sysLogFilePath =Manager.ConfigManager.Instance.CfgSystem.SystemLogFilePath;
            _exLogFilePath = Manager.ConfigManager.Instance.CfgSystem.ExceptionLogFilePath;
            CreateDeviceList();
        }

        #endregion

        //字段:设备列表
        public System.Collections.SortedList DeviceList;
        private string _sysLogFilePath, _exLogFilePath; //系统日志文件路径,异常日志文件路径

        /// <summary>
        /// 方法:创建设备列表
        /// </summary>
        private void CreateDeviceList()
        {
            try
            {
                int ctrllerCount = Manager.ConfigManager.Instance.CfgSystem.CtrllerCategoryArray.Count();

                foreach (ProCommon.Communal.CtrllerCategory cc in Manager.ConfigManager.Instance.CfgSystem.CtrllerCategoryArray)
                {
                    switch (cc)
                    {
                        case ProCommon.Communal.CtrllerCategory.Board:
                            {
                                #region 可以使用统一驱动接口的控制器设备
                                /*
                                Device.Device_BoardCtrller devBoard = new Device.Device_BoardCtrller(Pro2DBarcode.Manager.ConfigManager.Instance.CfgBoardCtrller, Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem);
                                */
                                #endregion                             
                            }
                            break;
                        case ProCommon.Communal.CtrllerCategory.Camera:
                            {
                                Device.Device_Camera devCam = new Device.Device_Camera(Manager.ConfigManager.Instance.CfgCamera, Manager.ConfigManager.Instance.CfgSystem);
                                if (devCam != null)
                                {
                                    if (!DeviceList.ContainsKey(ProCommon.Communal.CtrllerCategory.Camera))
                                        DeviceList.Add(ProCommon.Communal.CtrllerCategory.Camera, devCam);
                                }
                            }
                            break;
                        case ProCommon.Communal.CtrllerCategory.PLC:
                            break;
                        case ProCommon.Communal.CtrllerCategory.Socket:
                            break;
                        case ProCommon.Communal.CtrllerCategory.SerialPort:
                            {
                                Device.Device_SerialPort devSerialPort = new Device.Device_SerialPort(Manager.ConfigManager.Instance.CfgSerialPort, Manager.ConfigManager.Instance.CfgSystem);
                                if (devSerialPort != null)
                                {
                                    if (!DeviceList.ContainsKey(ProCommon.Communal.CtrllerCategory.SerialPort))
                                        DeviceList.Add(ProCommon.Communal.CtrllerCategory.SerialPort, devSerialPort);
                                }
                            }                           
                            break;
                        case ProCommon.Communal.CtrllerCategory.Web:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                ProCommon.Communal.LogWriter.WriteException(_exLogFilePath, ex);
                ProCommon.Communal.LogWriter.WriteLog(_sysLogFilePath, string.Format("错误：创建设备列表失败!\n异常描述:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 方法：初始化设备列表中的设备
        /// </summary>
        public void DeviceListInit()
        {

            System.Collections.IDictionaryEnumerator mDicEnumerator = DeviceList.GetEnumerator();
            while (mDicEnumerator.MoveNext())
            {
                Device.IDevice mIDevice = mDicEnumerator.Value as Device.IDevice;//调用实现该接口的类
                try
                {
                    mIDevice.Init();
                }
                catch (ProCommon.Communal.InitException initex)
                {
                    ProCommon.Communal.LogWriter.WriteException(_exLogFilePath, initex);
                    ProCommon.Communal.LogWriter.WriteLog(_sysLogFilePath, string.Format("错误：初始化设备列表失败!\n异常描述:{0}", initex.Message));
                }
            }
        }

        /// <summary>
        /// 方法：开启设备列表中的设备
        /// </summary>
        public void DeviceListStart()
        {
            System.Collections.IDictionaryEnumerator mDicEnumerator = this.DeviceList.GetEnumerator();

            while (mDicEnumerator.MoveNext())
            {
                Device.IDevice mIDevice = mDicEnumerator.Value as Device.IDevice; //强制转换到实现该接口的类的接口引用
                try
                {
                    mIDevice.Start();
                }
                catch (ProCommon.Communal.StartException startex)
                {
                    ProCommon.Communal.LogWriter.WriteException(_exLogFilePath, startex);
                    ProCommon.Communal.LogWriter.WriteLog(_sysLogFilePath, string.Format("错误：启动设备列表失败!\n异常描述:{0}", startex.Message));
                }
            }
        }

        /// <summary>
        /// 方法：停止设备列表中的设备
        /// </summary>
        public void DeviceListStop()
        {
            System.Collections.IDictionaryEnumerator mDicEnumerator = this.DeviceList.GetEnumerator();

            while (mDicEnumerator.MoveNext())
            {
                Device.IDevice mIDevice = mDicEnumerator.Value as Device.IDevice; //强制转换到实现该接口的类的接口引用
                try
                {
                    mIDevice.Stop();
                }
                catch (ProCommon.Communal.StopException stopex)
                {
                    ProCommon.Communal.LogWriter.WriteException(_exLogFilePath, stopex);
                    ProCommon.Communal.LogWriter.WriteLog(_sysLogFilePath, string.Format("错误：停止设备列表失败!\n异常描述:{0}", stopex.Message));
                }
            }
        }

        /// <summary>
        /// 方法：释放设备列表的设备
        /// </summary>
        public void DeviceListRelease()
        {
            System.Collections.IDictionaryEnumerator mDicEnumerator = this.DeviceList.GetEnumerator();

            while (mDicEnumerator.MoveNext())
            {
                Device.IDevice mIDevice = mDicEnumerator.Value as Device.IDevice; //强制转换到实现该接口的类的接口引用
                try
                {
                    mIDevice.Release();
                }
                catch (ProCommon.Communal.ReleaseException releaseex)
                {
                    ProCommon.Communal.LogWriter.WriteException(_exLogFilePath, releaseex);
                    ProCommon.Communal.LogWriter.WriteLog(_sysLogFilePath, string.Format("错误：释放设备列表失败!\n异常描述:{0}", releaseex.Message));
                }
            }
        }
    }
}
