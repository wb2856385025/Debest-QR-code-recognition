using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProCommon.Communal;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CameraDriver_DaHeng
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProDriver.Driver
 * File      Name：       CameraDriver_DaHeng
 * Creating  Time：       12/26/2019 11:03:02 AM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProDriver.Driver
{
    public class CameraDriver_DaHengUSB : CamDriver
    {
#pragma warning disable CS0067 // The event 'CameraDriver_DaHengUSB.CameraImageGrabbedEvt' is never used
        public override event CameraImageGrabbedDel CameraImageGrabbedEvt; //图像抓取到事件(统一事件)
#pragma warning restore CS0067 // The event 'CameraDriver_DaHengUSB.CameraImageGrabbedEvt' is never used

        protected const USBCamera.HV_RESOLUTION m_kResolotion = USBCamera.HV_RESOLUTION.RES_MODE0;                             ///< 分辨率                      
        protected const int m_kGain = 9;                                                                                       ///< 增益
        protected const int m_kADCLevel = (int)USBCamera.HV_ADC_LEVEL.ADC_LEVEL2; 

        protected const int m_kHBlanking = 0;                                                                                   ///< 水平消隐
        protected const int m_kVBlanking = 0;                                                                                   ///< 垂直消隐
        protected const USBCamera.HV_SNAP_SPEED m_kSnapSpeed = USBCamera.HV_SNAP_SPEED.HIGH_SPEED;                              ///< 采集速度
        protected const int m_kUpperET = 60;                                                                                    ///< 曝光时间分子
        protected const int m_kLowerET = 1000;                                                                                  ///< 曝光时间分母
        protected const double m_kZeorInDouble = 0.000000001;                                                                   ///<double类型的0
        protected const USBCamera.HV_BAYER_CONVERT_TYPE m_kConvertType = USBCamera.HV_BAYER_CONVERT_TYPE.BAYER2RGB_NEIGHBOUR;   ///< 转换类型
        protected const USBCamera.HV_BAYER_LAYOUT m_kBayerType = USBCamera.HV_BAYER_LAYOUT.BAYER_GR;                            ///< Bayer类型
        protected const System.Drawing.Imaging.ImageLockMode m_kLockMode = System.Drawing.Imaging.ImageLockMode.WriteOnly;      ///< 图像锁定模式
        protected const System.Drawing.Imaging.PixelFormat m_kBMPFormat = System.Drawing.Imaging.PixelFormat.Format24bppRgb;    ///< 图像像素格式

        private int _deviceNums = 0;        //设备数量
        private System.Int32 _deviceIdx;    //当前设备的索引
        private System.IntPtr _deviceRef;   //当前设备资源句柄

        protected string m_strCameraType = string.Empty; ///< 相机类型
        protected byte[] m_RawBuffer;                       ///< Raw图像缓存
        protected IntPtr m_pRawBuffer;                      ///< Raw图像缓存指针
        protected byte[] m_ImageBuffer;                     ///< RGB图像缓存
        protected byte[] m_LutR = new byte[256];            ///< 颜色查询表R分量
        protected byte[] m_LutG = new byte[256];            ///< 颜色查询表G分量
        protected byte[] m_LutB = new byte[256];            ///< 颜色查询表B分量
        protected System.Drawing.Bitmap m_bmpCurrent;       ///< 当前位图
        protected System.Drawing.Rectangle m_OutPutWindow;  ///< 表征源图像大小的矩形_rectSource
        private USBCamera.HV_SNAP_PROC _SDKImageGrabbedDel;   //图像抓取到委托(当前品牌驱动)

        private CameraDriver_DaHengUSB()
        {
            _deviceIdx = 0;
            _deviceRef = System.IntPtr.Zero;
            m_OutPutWindow = new System.Drawing.Rectangle(0, 0, 800, 600);          
            _SDKImageGrabbedDel = new USBCamera.HV_SNAP_PROC(OnCameraImageGrabbed);
        }       

        public CameraDriver_DaHengUSB(ProCommon.Communal.Camera cam):this()
        {
            Camera = cam;
        }

        /// <summary>
        /// 大恒相机USB2.0采集到图像的回调处理函数
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        private bool OnCameraImageGrabbed(ref USBCamera.HV_SNAP_INFO Info)
        {
            bool rt = false;
            try
            {

            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (System.Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {

            }
            return rt;
        }

        /// <summary>
        /// 枚举在线相机
        /// </summary>
        /// <returns></returns>
        public override bool DoEnumerateCameraList()
        {
            bool rt = false;
            USBCamera.HVSTATUS status;            
            try
            {
                System.GC.Collect();
                status = USBCamera.API.HVGetDeviceTotal(ref _deviceNums);
                USBCamera.API.HV_VERIFY(status);
                rt = (USBCamera.HVSTATUS.STATUS_OK == status) ? true : false;
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大恒USB2.0相机枚举设备失败!\n错误代码:{0:X8}", System.Convert.ToInt32(status)));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 计算在线相机数量
        /// </summary>
        /// <returns></returns>
        public override int DoGetCameraListCount()
        {

            if (_deviceNums == 0)
                DoEnumerateCameraList();
            return _deviceNums;
        }

        /// <summary>
        /// 根据相机索引获取相机
        /// [相机索引号由其上电顺序得来，非固定]
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public override bool DoGetCameraByIdx(int index)
        {
            bool rt = false;
            USBCamera.HVSTATUS status=USBCamera.HVSTATUS.STATUS_OK;
            try
            {
                if(_deviceNums>0)
                {
                    if(index<=_deviceNums)
                    {
                        if (_deviceRef != System.IntPtr.Zero)
                        {
                            status = USBCamera.API.EndHVDevice(_deviceRef);
                            USBCamera.API.HV_VERIFY(status);
                            _deviceRef = System.IntPtr.Zero;
                            _deviceIdx = 0;
                        }

                        if (USBCamera.HVSTATUS.STATUS_OK == status)
                        {
                            _deviceIdx = index;
                            status = USBCamera.API.BeginHVDevice(_deviceIdx, ref _deviceRef);
                            USBCamera.API.HV_VERIFY(status);
                            rt = (USBCamera.HVSTATUS.STATUS_OK == status) ? true : false;
                            if (!rt)
                            {
                               
                                if (DriverExceptionDel != null)
                                    DriverExceptionDel(string.Format("错误：大恒USB2.0相机获取索引对应设备失败!\n索引:{0}\n错误代码:{1:X8}",
                                        index, System.Convert.ToInt32(status)));
                            }
                        }
                    }
                    else
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大恒USB2.0相机获取索引对应设备失败!\n索引:{0}\n超出范围", index));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大恒USB2.0相机获取索引对应设备失败!\n:{0}", "设备列表为空"));
                }
            }
            catch
            {
            }
            finally
            {
            }

            return rt;
        }

        /// <summary>
        /// 获取索引指定相机的名称
        /// </summary>
        /// <param name="index">相机索引</param>
        /// <returns></returns>
        public override string DoGetCameraSN(int index)
        {
            if (DoGetCameraByIdx(index))
            {
                USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
                int size = sizeof(USBCamera.HVTYPE);
                IntPtr buffer = new IntPtr();               
                buffer = System.Runtime.InteropServices.Marshal.AllocHGlobal(size);
                status = USBCamera.API.HVGetDeviceInfo(_deviceRef, USBCamera.HV_DEVICE_INFO.DESC_DEVICE_SERIESNUM, buffer, ref size);
                USBCamera.API.HV_VERIFY(status);

                int[] sn = new int[size/4];
                System.Runtime.InteropServices.Marshal.Copy(buffer, sn, 0, size / 4);

                StringBuilder str = new StringBuilder();
                //for (int i=0;i<size/4;i++)

                string serialNum = str.ToString();
                str.Remove(0, str.Length);
                System.Runtime.InteropServices.Marshal.FreeHGlobal(buffer);

                return serialNum;
            }

            return string.Empty;
        }

        /// <summary>
        /// 根据相机名称获取相机
        /// </summary>
        /// <param name="camName"></param>
        /// <returns></returns>
        public override bool DoGetCameraByName(string camName)
        {
            bool rt = false;
#pragma warning disable CS0219 // The variable 'status' is assigned but its value is never used
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
#pragma warning restore CS0219 // The variable 'status' is assigned but its value is never used
            try
            {
                if (_deviceNums > 0)
                {
                    rt = false; //大恒USB2.0相机未找到根据名称获取相机的方法
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大恒USB2.0相机获取名称对应设备失败!\n:{0}", "设备列表为空"));
                }
            }
            catch
            {
            }
            finally
            {
            }

            return rt;
        }

        /// <summary>
        /// 根据相机SN地址获取相机
        /// </summary>
        /// <param name="camSN"></param>
        /// <returns></returns>
        public override bool DoGetCameraBySN(string camSN)
        {
            bool rt = false;
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
            try
            {
                if (_deviceNums > 0)
                {
                    int j = 1;
                    string sn = string.Empty;
                    for(int i=1;i<=_deviceNums;i++)
                    {
                        sn = DoGetCameraSN(i);
                        if(sn==camSN)
                        {
                            _deviceIdx = i;
                        }
                        j++;
                    }
                    if(j> _deviceNums)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大恒USB2.0相机获取设备失败!\n设备SN:{0}\n异常描述{1}",
                                 camSN, "指定SN不匹配"));
                    }
                    else
                    {
                        if(_deviceRef!=System.IntPtr.Zero)
                            status = USBCamera.API.EndHVDevice(_deviceRef);
                        status = USBCamera.API.BeginHVDevice(_deviceIdx, ref _deviceRef);
                        USBCamera.API.HV_VERIFY(status);
                        rt = (USBCamera.HVSTATUS.STATUS_OK == status) ? true : false;
                        if (!rt)
                        {

                            if (DriverExceptionDel != null)
                                DriverExceptionDel(string.Format("错误：大恒USB2.0相机获取序列号对应设备失败!\n设备SN:{0}\n错误代码:{1:X8}",
                                    camSN, System.Convert.ToInt32(status)));
                        }
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大恒USB2.0相机获取序列号对应设备失败!\n:{0}", "设备列表为空"));
                }
            }
            catch
            {
            }
            finally
            {
            }

            return rt;
        }

        public override bool DoOpen()
        {
            bool rt = false;
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
            try
            {
                if(_deviceRef!=System.IntPtr.Zero)
                {
                    status = USBCamera.API.HVOpenSnap(_deviceRef, _SDKImageGrabbedDel, new IntPtr());
                    USBCamera.API.HV_VERIFY(status);
                    rt = (USBCamera.HVSTATUS.STATUS_OK == status) ? true : false;
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大恒USB2.0相机打开设备失败!\n错误代码:{0:X8}", System.Convert.ToInt32(status)));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大恒USB2.0相机打开失败!\n错误描述:{0}", "设备未连接"));
                }               
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }

        public override bool DoClose()
        {
            bool rt = false;
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
            try
            {
                if (_deviceRef != System.IntPtr.Zero)
                {
                    status = USBCamera.API.HVCloseSnap(_deviceRef);
                    USBCamera.API.HV_VERIFY(status);
                    rt = (USBCamera.HVSTATUS.STATUS_OK == status) ? true : false;
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大恒USB2.0相机关闭设备失败!\n错误代码:{0:X8}", System.Convert.ToInt32(status)));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大恒USB2.0相机关闭失败!\n错误描述:{0}", "设备未连接"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 方法：设置采集模式
        /// </summary>
        /// <param name="acqmode"></param>
        /// <param name="frameNum"></param>
        /// <returns></returns>
        public override bool DoSetAcquisitionMode(ProCommon.Communal.AcquisitionMode acqmode, uint frameNum)
        {
            bool rt = false;
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
            try
            {
                if (_deviceRef != System.IntPtr.Zero)
                {
                    DoStartGrab();
                    switch (acqmode)
                    {
                        case ProCommon.Communal.AcquisitionMode.Continue:
                            status = USBCamera.API.HVSetSnapMode(_deviceRef, USBCamera.HV_SNAP_MODE.CONTINUATION);
                            USBCamera.API.HV_VERIFY(status);
                            break;
                        case ProCommon.Communal.AcquisitionMode.SoftTrigger:
                            break;
                        case ProCommon.Communal.AcquisitionMode.ExternalTrigger:
                            status = USBCamera.API.HVSetSnapMode(_deviceRef, USBCamera.HV_SNAP_MODE.TRIGGER);
                            USBCamera.API.HV_VERIFY(status);
                            break;
                        default: break;
                    }

                    rt = (USBCamera.HVSTATUS.STATUS_OK == status) ? true : false;
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大恒USB2.0相机设置采集模式失败!\n错误代码:{0:X8}", System.Convert.ToInt32(status)));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大恒USB2.0相机设置设备采集模式失败!\n错误描述:{0}", "设备未连接"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 方法:设置触发信号边缘
        /// [注:用于触发源为硬触发
        /// 并非大恒USB2.0相机支持边缘信号
        /// 故不再设置触发信号边缘]
        /// </summary>
        /// <param name="dege">边缘信号</param>
        /// <returns></returns>
        public override bool DoSetTriggerActivation(ProCommon.Communal.TriggerLogic edge)
        {
            bool rt = false;
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
            try
            {
                if (_deviceRef != System.IntPtr.Zero)
                {
                    switch (edge)
                    {
                        case ProCommon.Communal.TriggerLogic.FallEdge:                           
                            break;
                        case ProCommon.Communal.TriggerLogic.RaiseEdge:                           
                            break;
                        default:
                            break;
                    }

                    rt = (USBCamera.HVSTATUS.STATUS_OK == status) ? true : false;
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大恒USB2.0相机设置触发信号边沿失败!\n错误代码:{0:X8}", System.Convert.ToInt32(status)));
                    }
                }
                else
                {

                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大恒USB2.0相机设置设备采集模式失败!\n错误描述:{0}", "设备未连接"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }
        public override bool DoStartGrab()
        {
            bool rt = false;
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
            try
            {
                if (_deviceRef != System.IntPtr.Zero)
                {
                    IntPtr[] pBuffers = new IntPtr[1];
                    pBuffers[0] = m_pRawBuffer;
                    status = USBCamera.API.HVStartSnap(_deviceRef, pBuffers,1);
                    USBCamera.API.HV_VERIFY(status);
                    rt = (USBCamera.HVSTATUS.STATUS_OK == status) ? true : false;
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大恒USB2.0相机开启采集失败!\n错误代码:{0:X8}", System.Convert.ToInt32(status)));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大恒USB2.0相机设置开启采集失败!\n错误描述:{0}", "设备未连接"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }
        public override bool DoPauseGrab()
        {
            bool rt = false;
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
            try
            {
                if (_deviceRef != System.IntPtr.Zero)
                {                  
                    rt = (USBCamera.HVSTATUS.STATUS_OK == status) ? true : false;
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大恒USB2.0相机暂停采集失败!\n错误代码:{0:X8}", System.Convert.ToInt32(status)));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大恒USB2.0相机设置暂停采集失败!\n错误描述:{0}", "设备未连接"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }
        public override bool DoStopGrab()
        {
            bool rt = false;
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
            try
            {
                if (_deviceRef != System.IntPtr.Zero)
                {
                    status = USBCamera.API.HVStopSnap(_deviceRef);
                    USBCamera.API.HV_VERIFY(status);
                    rt = (USBCamera.HVSTATUS.STATUS_OK == status) ? true : false;
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大恒USB2.0相机停止采集失败!\n错误代码:{0:X8}", System.Convert.ToInt32(status)));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大恒USB2.0相机设置停止采集失败!\n错误描述:{0}", "设备未连接"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }
        public override bool DoSoftTriggerOnce()
        {
            bool rt = false;
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
            try
            {
                if (_deviceRef != System.IntPtr.Zero)
                {
                    status = USBCamera.API.HVTriggerShot(_deviceRef);
                    USBCamera.API.HV_VERIFY(status);
                    rt = (USBCamera.HVSTATUS.STATUS_OK == status) ? true : false;
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大恒USB2.0相机软触发采集失败!\n错误代码:{0:X8}", System.Convert.ToInt32(status)));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大恒USB2.0相机单次软触发采集失败!\n错误描述:{0}", "设备未连接"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }
        public override bool DoRegisterExceptionCallBack()
        {
            bool rt = false;
#pragma warning disable CS0219 // The variable 'status' is assigned but its value is never used
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
#pragma warning restore CS0219 // The variable 'status' is assigned but its value is never used
            try
            {
                rt = true;
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }
        public override bool DoRegisterImageGrabbedCallBack()
        {
            bool rt = false;
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
            try
            {
                if (_deviceRef != System.IntPtr.Zero)
                {
                    //已经在打开相机设备时注册
                    rt = (USBCamera.HVSTATUS.STATUS_OK == status) ? true : false;
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大恒USB2.0相机注册图像采集回调失败!\n错误代码:{0:X8}", System.Convert.ToInt32(status)));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大恒USB2.0相机注册采集回调函数失败!\n错误描述:{0}", "设备未连接"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }
        public override bool DoSetExposureTime(float exposuretime)
        {
            bool rt = false;
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
            try
            {
                if (_deviceRef != System.IntPtr.Zero)
                {
                    status = SetExposureTime(m_OutPutWindow.Width, m_kUpperET, m_kLowerET,
                                                m_kHBlanking, m_kSnapSpeed, m_kResolotion);
                    USBCamera.API.HV_VERIFY(status);
                    rt = (USBCamera.HVSTATUS.STATUS_OK == status) ? true : false;
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大恒USB2.0相机设置曝光时间失败!\n错误代码:{0:X8}", System.Convert.ToInt32(status)));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大恒USB2.0相机设置曝光时间失败!\n错误描述:{0}", "设备未连接"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }
        public override bool DoSetFrameRate(float fps)
        {
            bool rt = false;
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
            try
            {
                if (_deviceRef != System.IntPtr.Zero)
                {
                    //未发现设置采集帧率方法                   
                    USBCamera.API.HV_VERIFY(status);
                    rt = (USBCamera.HVSTATUS.STATUS_OK == status) ? true : false;
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大恒USB2.0相机设置采集帧率失败!\n错误代码:{0:X8}", System.Convert.ToInt32(status)));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大恒USB2.0相机设置采集帧率失败!\n错误描述:{0}", "设备未连接"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }
        public override bool DoSetGain(float gain)
        {
            bool rt = false;
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
            try
            {
                if (_deviceRef != System.IntPtr.Zero)
                {
                    int gn = Convert.ToInt32(gain);
                    for (int i = 0; i < 4; i++)
                    {
                        status = USBCamera.API.HVAGCControl(_deviceRef, (byte)(USBCamera.HV_CHANNEL.RED_CHANNEL + i),gn);
                        USBCamera.API.HV_VERIFY(status);
                    }
                    rt = (USBCamera.HVSTATUS.STATUS_OK == status) ? true : false;
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大恒USB2.0相机设置模拟增益失败!\n错误代码:{0:X8}", System.Convert.ToInt32(status)));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大恒USB2.0相机设置增益失败!\n错误描述:{0}", "设备未连接"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }
        public override bool DoSetTriggerDelay(float trigdelay)
        {
            bool rt = false;
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
            try
            {
                if (_deviceRef != System.IntPtr.Zero)
                {
                    //未发现设置触发延迟方法
                    USBCamera.API.HV_VERIFY(status);
                    rt = (USBCamera.HVSTATUS.STATUS_OK == status) ? true : false;
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：迈德威视相机设置触发延迟失败!\n错误代码:{0:X8}", System.Convert.ToInt32(status)));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：迈德威视相机设置触发延迟失败!\n错误描述:{0}", "设备未连接"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 设置相机输出信号
        /// </summary>
        /// <param name="onOff"></param>
        /// <returns></returns>
        public override bool DoSetOutPut(bool onOff)
        {
            bool rt = false;
#pragma warning disable CS0219 // The variable 'status' is assigned but its value is never used
            USBCamera.HVSTATUS status = USBCamera.HVSTATUS.STATUS_OK;
#pragma warning restore CS0219 // The variable 'status' is assigned but its value is never used
            try
            {

            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：迈德威视相机设置输出信号失败!\n错误描述:{0}", ex.Message));
            }
            finally
            {
            }
            return rt;
        }

        /// <summary>
        /// 创建参数设置窗口
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="promption"></param>
        /// <returns></returns>
        public override bool DoCreateCameraSetPage(System.IntPtr windowHandle, string promption)
        {
            bool rt = false;
            try
            {

            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (System.Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {

            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 显示参数设置窗口
        /// </summary>
        /// <returns></returns>
        public override bool DoShowCameraSetPage()
        {
            bool rt = false;
            try
            {

            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (System.Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {

            }
            finally
            {

            }

            return rt;
        }

        public override string ToString()
        {
            return "CameraDriver[DaHengUSB2.0]";
        }

        #region 大恒USB2.0相机官方函数
        //----------------------------------------------------------------------------------
        /**
            设置曝光时间
            \param      int                 [in] 窗口宽度
            \param      int                 [in] 分子
            \param      int                 [in] 分母
            \param      int                 [in] 消隐控制
            \param      HV_SNAP_SPEED       [in] 采集速度
            \param      HV_RESOLUTION       [in] 分辨率
            \return		HVSTATUS            [out]状态
        */

        //----------------------------------------------------------------------------------
        protected USBCamera.HVSTATUS SetExposureTime(int nWindWidth, int nUpper, int nLower, int nHBlanking,
                                            USBCamera.HV_SNAP_SPEED SnapSpeed, USBCamera.HV_RESOLUTION Resolution)
        {
            double clockFreq = 0.0;
            int tB = nHBlanking;
            int outPut = nWindWidth;
            double exposure = 0.0;
            System.Diagnostics.Debug.Assert(nLower != 0);
            double temp = (double)nUpper / (double)nLower;
            double tInt = (temp > m_kZeorInDouble) ? temp : m_kZeorInDouble;

            if (IsGV400())
            {
                tB += 0x5e;
                clockFreq = (SnapSpeed == USBCamera.HV_SNAP_SPEED.HIGH_SPEED) ? 26600000.0 : 13300000.0;
                int rate = 0;

                switch (Resolution)
                {
                    case USBCamera.HV_RESOLUTION.RES_MODE0:
                        rate = 1;
                        break;

                    case USBCamera.HV_RESOLUTION.RES_MODE1:
                        rate = 2;
                        break;

                    default:
                        return USBCamera.HVSTATUS.STATUS_PARAMETER_OUT_OF_BOUND;
                }

                outPut = outPut * rate;

                if ((tInt * clockFreq) <= (outPut + tB - 255))
                {
                    exposure = 1;
                }
                else
                {
                    System.Diagnostics.Debug.Assert((outPut + tB) != 0);
                    exposure = ((tInt * clockFreq) - (outPut + tB - 255)) / (outPut + tB);
                }

                if (exposure < 3)
                {
                    exposure = 3;
                }
                else if (exposure > 32766)
                {
                    exposure = 32766;
                }
            }
            else if (IsHV300())
            {
                clockFreq = (SnapSpeed == USBCamera.HV_SNAP_SPEED.HIGH_SPEED) ? 24000000 : 12000000;
                tB += 142;
                if (tB < 21)
                {
                    tB = 21;
                }
                int param1 = 331;
                int param2 = 38;
                int param3 = 316;
                if (Resolution == USBCamera.HV_RESOLUTION.RES_MODE1)
                {
                    param1 = 673;
                    param2 = 22;
                    param3 = 316 * 2;
                }
                int AQ = outPut + param1 + param2 + tB;
                int tmp = param1 + param3;
                int trow = (AQ > tmp) ? AQ : tmp;

                System.Diagnostics.Debug.Assert(trow != 0);
                exposure = ((tInt * clockFreq) + param1 - 132.0) / trow;

                if ((exposure - (int)exposure) > 0.5)
                {
                    exposure += 1.0;
                }
                if (exposure <= 0)
                {
                    exposure = 1;
                }
                else if (exposure > 1048575)
                {
                    exposure = 1048575;
                }
            }
            else if (IsHV200())
            {
                clockFreq = (SnapSpeed == USBCamera.HV_SNAP_SPEED.HIGH_SPEED) ? 24000000 : 12000000;
                tB += 53;
                if (tB < 19)
                {
                    tB = 19;
                }
                int AQ = outPut + 305 + tB;
                int trow = (617 > AQ) ? 617 : AQ;
                System.Diagnostics.Debug.Assert((trow + 1) != 0);
                exposure = (tInt * clockFreq + 180.0) / trow + 1;

                if ((exposure - (int)exposure) > 0.5)
                {
                    exposure += 1.0;
                }
                if (exposure <= 0)
                {
                    exposure = 1;
                }
                else if (exposure > 16383)
                {
                    exposure = 16383;
                }
            }
            else if (IsHV5051())
            {
                USBCamera.SHUTTER_UNIT_VALUE unit = USBCamera.SHUTTER_UNIT_VALUE.SHUTTER_MS;

                if (nLower == 1000000)
                {
                    unit = USBCamera.SHUTTER_UNIT_VALUE.SHUTTER_US;
                }

                //设置曝光时间单位
                USBCamera.HVSTATUS status = USBCamera.API.HVAECControl(_deviceRef, (byte)USBCamera.HV_AEC_CONTROL.AEC_SHUTTER_UNIT, (int)unit);
                if (!USBCamera.API.HV_SUCCESS(status))
                {
                    return status;
                }

                //设置曝光时间
                return USBCamera.API.HVAECControl(_deviceRef, (byte)USBCamera.HV_AEC_CONTROL.AEC_SHUTTER_SPEED, nUpper);
            }
            else
            {
                clockFreq = (SnapSpeed == USBCamera.HV_SNAP_SPEED.HIGH_SPEED) ? 24000000 : 12000000;
                tB += 9;
                tB -= 19;
                if (tB <= 0)
                {
                    tB = 0;
                }
                if ((outPut + 244.0 + tB) > 552)
                {
                    exposure = (tInt * clockFreq + 180.0) / ((double)outPut + 244.0 + tB);
                }
                else
                {
                    exposure = ((tInt * clockFreq) + 180.0) / 552;
                }

                if ((exposure - (int)exposure) > 0.5)
                {
                    exposure += 1.0;
                }
                if (exposure <= 0)
                {
                    exposure = 1;
                }
                else if (exposure > 16383)
                {
                    exposure = 16383;
                }
            }

            return USBCamera.API.HVAECControl(_deviceRef, (byte)USBCamera.HV_AEC_CONTROL.AEC_EXPOSURE_TIME, (int)exposure);
        }

        public bool IsHV130()
        {
            if ((m_strCameraType == "HV1300UCTYPE") || (m_strCameraType == "HV1300UMTYPE") ||
                (m_strCameraType == "HV1301UCTYPE") || (m_strCameraType == "HV1302UMTYPE") ||
                (m_strCameraType == "HV1302UCTYPE") || (m_strCameraType == "HV1303UMTYPE") ||
                (m_strCameraType == "HV1303UCTYPE") || (m_strCameraType == "HV1350UMTYPE") ||
                (m_strCameraType == "HV1350UCTYPE") || (m_strCameraType == "HV1351UMTYPE") ||
                (m_strCameraType == "HV1351UCTYPE"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsHV200()
        {
            if ((m_strCameraType == "HV2000UCTYPE") || (m_strCameraType == "HV2001UCTYPE") ||
                (m_strCameraType == "HV2002UCTYPE") || (m_strCameraType == "HV2003UCTYPE") ||
                (m_strCameraType == "HV2050UCTYPE") || (m_strCameraType == "HV2051UCTYPE"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsHV300()
        {
            if ((m_strCameraType == "HV3000UCTYPE") || (m_strCameraType == "HV3102UCTYPE") ||
                (m_strCameraType == "HV3103UCTYPE") || (m_strCameraType == "HV3150UCTYPE") ||
                (m_strCameraType == "HV3151UCTYPE"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsGV400()
        {
            if ((m_strCameraType == "GV400UCTYPE") || (m_strCameraType == "GV400UMTYPE"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsHV5051()
        {
            if ((m_strCameraType == "HV5051UCTYPE") || (m_strCameraType == "HV5051UMTYPE"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 辅助函数

        /// <summary>
        /// 获取相机类型
        /// </summary>
        private void GetCameraType()
        {
            if(_deviceRef != IntPtr.Zero)
            {
                IntPtr buffer = new IntPtr();
                int size = sizeof(USBCamera.HVTYPE);
                StringBuilder str = new StringBuilder();

                buffer = System.Runtime.InteropServices.Marshal.AllocHGlobal(size);
                USBCamera.HVSTATUS status = USBCamera.API.HVGetDeviceInfo(_deviceRef, USBCamera.HV_DEVICE_INFO.DESC_DEVICE_TYPE, buffer, ref size);
                USBCamera.API.HV_VERIFY(status);
                int[] type = new int[size / 4];
                System.Runtime.InteropServices.Marshal.Copy(buffer, type, 0, size / 4);
                for (int i = 0; i < size / 4; i++)
                {
                    str.Append(((USBCamera.HVTYPE)type[i]).ToString().Substring(0, 8));
                }
                m_strCameraType = str.ToString();
                str.Remove(0, str.Length);
                System.Runtime.InteropServices.Marshal.FreeHGlobal(buffer);
            }
        }

        /// <summary>
        /// 设置分辨率
        /// </summary>
        private void SetResolution()
        {
            System.Diagnostics.Debug.Assert(_deviceRef!= IntPtr.Zero);
            USBCamera.HVSTATUS status = USBCamera.API.HVSetResolution(_deviceRef, m_kResolotion);
            USBCamera.API.HV_VERIFY(status);
        }

        private void SetADC()
        {
            System.Diagnostics.Debug.Assert(_deviceRef != IntPtr.Zero);

            USBCamera.HVSTATUS status = USBCamera.API.HVADCControl(_deviceRef, (byte)USBCamera.HV_ADC_CONTROL.ADC_BITS, m_kADCLevel);
            USBCamera.API.HV_VERIFY(status);
        }

        /// <summary>
        /// 设置表征源图像大小的矩形
        /// </summary>
        private void SetOutPutWindow()
        {
            System.Diagnostics.Debug.Assert(_deviceRef != IntPtr.Zero);

            IntPtr buffer = new IntPtr();
            int size = 0;
            USBCamera.HVSTATUS status = USBCamera.API.HVGetDeviceInfo(_deviceRef, USBCamera.HV_DEVICE_INFO.DESC_RESOLUTION, buffer, ref size);

            buffer = System.Runtime.InteropServices.Marshal.AllocHGlobal(size);
            status = USBCamera.API.HVGetDeviceInfo(_deviceRef, USBCamera.HV_DEVICE_INFO.DESC_RESOLUTION, buffer, ref size);
            USBCamera.API.HV_VERIFY(status);
            int[] type = new int[64];
            System.Runtime.InteropServices.Marshal.Copy(buffer, type, 0, 64);

            System.Runtime.InteropServices.Marshal.FreeHGlobal(buffer);

            m_OutPutWindow.Width = type[(int)m_kResolotion * 2];
            m_OutPutWindow.Height = type[(int)m_kResolotion * 2 + 1];

            status = USBCamera.API.HVSetOutputWindow(_deviceRef, m_OutPutWindow.X, m_OutPutWindow.Y, m_OutPutWindow.Width, m_OutPutWindow.Height);
            USBCamera.API.HV_VERIFY(status);
        }

        /// <summary>
        /// 设置相机消隐
        /// </summary>
        public void SetBlanking()
        {
            System.Diagnostics.Debug.Assert(_deviceRef != IntPtr.Zero);

            USBCamera.HVSTATUS status = USBCamera.API.HVSetBlanking(_deviceRef, m_kHBlanking, m_kVBlanking);
            USBCamera.API.HV_VERIFY(status);
        }

        /// <summary>
        /// 设置相机采集速度
        /// </summary>
        private void SetSnapSpeed()
        {
            System.Diagnostics.Debug.Assert(_deviceRef != IntPtr.Zero);

            USBCamera.HVSTATUS status = USBCamera.API.HVSetSnapSpeed(_deviceRef, m_kSnapSpeed);
            USBCamera.API.HV_VERIFY(status);
        }

        #endregion
    }
}
