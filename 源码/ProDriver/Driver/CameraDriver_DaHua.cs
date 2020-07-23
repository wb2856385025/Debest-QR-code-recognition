using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CameraDriver_DaHua
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProDriver.Driver
 * File      Name：       CameraDriver_DaHua
 * Creating  Time：       2/22/2020 11:52:11 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProDriver.Driver
{
    class CameraDriver_DaHua : CamDriver
    {
        public override event CameraImageGrabbedDel CameraImageGrabbedEvt; //图像抓取到事件(统一事件)

        System.Collections.Generic.List<ThridLibray.IDeviceInfo> _deviceInfoList;  //设备描述信息列表
        private ThridLibray.IDeviceInfo _deviceInfo;                               //当前设备描述信息
        private ThridLibray.IDevice _deviceRef;                                    //当前设备的资源引用      

        private CameraDriver_DaHua()
        {

        }

        public CameraDriver_DaHua(ProCommon.Communal.Camera cam) : this()
        {
            Camera = cam;
        }

        /// <summary>
        /// 图像采集到事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StreamGrabber_ImageGrabbed(object sender, ThridLibray.GrabbedEventArgs e)
        {
            if (HoImage != null
              && HoImage.IsInitialized())
            {
                HoImage.Dispose();
                IsImageGrabbed = false;
            }

            #region 大华相机SDK内部像素格式转换
            ThridLibray.IGrabbedRawData frame = e.GrabResult.Clone();
            if(frame!=null)
            {
                System.Drawing.Bitmap btmp=null;
                if (frame.PixelFmt==ThridLibray.GvspPixelFormatType.gvspPixelMono8
                    || frame.PixelFmt == ThridLibray.GvspPixelFormatType.gvspPixelMono10)
                    btmp = frame.ToBitmap(false);
                else if(frame.PixelFmt == ThridLibray.GvspPixelFormatType.gvspPixelRGB8
                    || frame.PixelFmt == ThridLibray.GvspPixelFormatType.gvspPixelRGB10)
                {
                    btmp = frame.ToBitmap(true);
                }

                if(btmp!=null)
                {
                    ProVision.Communal.Functions.BitmapToHObject(btmp, out HoImage);
                    btmp.Dispose();
                }
            }

            #endregion

            if (HoImage != null
              && HoImage.IsInitialized())
            {
                System.Threading.Thread.Sleep(10);
                IsImageGrabbed = true;
                if (CameraImageGrabbedEvt != null)
                    CameraImageGrabbedEvt(Camera, HoImage);
            }
        }

        /// <summary>
        /// 相机断连
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _deviceRef_ConnectionLost(object sender, EventArgs e)
        {

        }

        #region 实现抽象函数

        /// <summary>
        /// 枚举在线相机
        /// </summary>
        /// <returns></returns>
        public override bool DoEnumerateCameraList()
        {
            bool rt = false;          
            try
            {
                System.GC.Collect();
                _deviceInfoList = ThridLibray.Enumerator.EnumerateDevices();

                if(_deviceInfoList!=null)
                    rt = true;

                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机枚举设备失败!\n错误代码:{0:X8}",99));
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
            return (int)_deviceInfoList.Count;
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
            try
            {
                if (_deviceInfoList.Count > 0)
                {
                    if (index < _deviceInfoList.Count)
                    {
                        _deviceRef = ThridLibray.Enumerator.GetDeviceByIndex(index);

                        if (_deviceRef!=null)
                            rt = true;
                    }
                    else
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大华相机获取设备失败!\n索引:{0}\n异常描述:{1}", index, "超出设备索引范围"));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机获取设备失败!\n索引:{0}\n异常描述:{1}", index, "设备列表空"));
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
            string strRT = string.Empty;
            try
            {
                if (_deviceInfoList.Count > 0)
                {
                    if (index>=0
                        && (index < _deviceInfoList.Count))
                    {
                        strRT=_deviceInfoList[index].SerialNumber;
                    }
                    else
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大华相机获取设备失败!\n索引:{0}\n异常描述:{1}", index, "超出设备索引范围"));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机获取设备失败!\n索引:{0}\n异常描述:{1}", index, "设备列表空"));
                }
            }
            catch
            {

            }
            finally
            {
            }

            return strRT;
        }

        /// <summary>
        /// 根据相机名称获取相机
        /// </summary>
        /// <param name="camName"></param>
        /// <returns></returns>
        public override bool DoGetCameraByName(string camName)
        {
            bool rt = false;          
            try
            {
                if (_deviceInfoList.Count > 0)
                {
                    ThridLibray.IDeviceInfo tmpdevice;
                    for (int i = 0; i < _deviceInfoList.Count; i++)
                    {
                        tmpdevice = _deviceInfoList[i];
                        if(tmpdevice.Name== camName)
                        {
                            _deviceInfo = tmpdevice;
                            if (DoGetCameraByIdx(i))
                            {
                                rt = true;
                                break;
                            }
                        }
                    }

                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大华相机获取设备失败!\n设备名称:{0}\n异常描述:{1}", camName, "指定名称不匹配"));
                    }
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
            try
            {
                if (_deviceInfoList.Count > 0)
                {
                    ThridLibray.IDeviceInfo tmpdevice;
                    for (int i = 0; i < _deviceInfoList.Count; i++)
                    {
                        tmpdevice = _deviceInfoList[i];
                        if (tmpdevice.SerialNumber == camSN)
                        {
                            _deviceInfo = tmpdevice;
                            if (DoGetCameraByIdx(i))
                            {
                                rt = true;
                                break;
                            }
                        }
                    }

                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大华相机获取设备失败!\n设备SN:{0}\n异常描述:{1}", camSN, "指定SN不匹配"));
                    }
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
        /// 打开设备
        /// </summary>
        /// <returns></returns>
        public override bool DoOpen()
        {
            bool rt = false;          
            try
            {
                if (_deviceRef != null)
                {
                    if(!_deviceRef.IsOpen)
                    {
                        rt=_deviceRef.Open();
                    }

                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大华相机打开失败!\n错误代码:{0:X8}", 99));
                    }
                    else
                    {
                        /* 设置缓存个数为8（默认值为16） */
                        /* set buffer count to 8 (default 16) */
                        _deviceRef.StreamGrabber.SetBufferCount(8);

                        /* 设置图像格式 */
                        /* set PixelFormat */
                        using (ThridLibray.IEnumParameter p = _deviceRef.ParameterCollection[ThridLibray.ParametrizeNameSet.ImagePixelFormat])
                        {
                            p.SetValue("Mono8");
                        }
                    }
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
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public override bool DoClose()
        {
            bool rt = false;
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceRef != null)
                {
                    _deviceRef.StreamGrabber.ImageGrabbed -= StreamGrabber_ImageGrabbed; /* 反注册回调 | unregister grab event callback */
                    _deviceRef.ShutdownGrab();                                           /* 停止码流 | stop grabbing */
                    _deviceRef.Close();                                                  /* 关闭相机 | close camera */
                    rt = true;
                }
                               
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机关闭设备失败!\n错误代码:{0:X8}", nRet));
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceRef != null)
                {
                    switch (acqmode)
                    {
                        case ProCommon.Communal.AcquisitionMode.Continue:
                            if (SetFreeRun())
                                rt = SetContinueRun();
                            break;
                        case ProCommon.Communal.AcquisitionMode.ExternalTrigger:
                            if (SetExternalTrigger())
                                rt = SetFrameNumber(frameNum);                         
                            break;
                        case ProCommon.Communal.AcquisitionMode.SoftTrigger:
                            if (SetInternalTrigger())
                                rt = SetFrameNumber(frameNum);                        
                            break;
                        default: break;
                    }

                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大华相机采集模式设置失败!\n错误代码:{0:X8}", nRet));
                    }
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
        /// 自由采集模式
        /// [不采集]
        /// </summary>
        /// <returns></returns>
        private bool SetFreeRun()
        {
            bool rt = false;          
            try
            {
                if (_deviceRef != null)
                {
                    rt = true;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置自由采集失败!\n:{0}", "设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：大华相机设置自由采集失败!\n错误描述:{0}", ex.Message));
            }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        /// 连续采集模式
        /// </summary>
        /// <returns></returns>
        private bool SetContinueRun()
        {
            bool rt = false;           
            try
            {
                if (_deviceRef != null)
                {
                    rt = _deviceRef.TriggerSet.Close();//关闭触发采集(开启连续采集)
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置连续采集失败!\n:{0}", "设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：大华相机设置连续采集失败!\n错误描述:{0}", ex.Message));
            }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        /// 设置内部触发采集(软触发)
        /// 0-Line0,1-Line1,2-Line2,3-Line3,4-Counter,7-Software
        /// </summary>
        /// <returns></returns>
        private bool SetInternalTrigger()
        {
            bool rt = false;          
            try
            {
                if (_deviceRef != null)
                {
                    rt=_deviceRef.TriggerSet.Open(ThridLibray.TriggerSourceEnum.Software); //开启触发模式:软触发
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大华相机内触发源(Software)设置失败!\n错误代码:{0:X8}", 99));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置内触发采集失败!\n:{0}", "设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：大华相机设置内触发采集失败!\n错误描述:{0}", ex.Message));
            }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        /// 设置触发采集时的帧数
        /// </summary>
        /// <param name="frameNum"></param>
        /// <returns></returns>
        private bool SetFrameNumber(uint frameNum)
        {
            bool rt = false;          
            try
            {
                if (_deviceRef != null)
                {
                    using (ThridLibray.IIntegraParameter p = _deviceRef.ParameterCollection[new ThridLibray.IntegerName("AcquisitionFrameCount")])
                    {
                        rt=p.SetValue(frameNum);
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置采集帧数失败!\n:{0}", "设备未连接"));
                }
            }
            catch (Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：大华相机设置采集帧数失败!\n错误描述:{0}", ex.Message));
            }

            return rt;
        }


        /// <summary>
        /// 设置外部触发采集(硬触发)
        /// 0-Line0,1-Line1,2-Line2,3-Line3,4-Counter,7-Software
        /// </summary>
        /// <returns></returns>
        private bool SetExternalTrigger()
        {
            bool rt = false;          
            try
            {
                if (_deviceRef != null)
                {
                    rt = _deviceRef.TriggerSet.Open(ThridLibray.TriggerSourceEnum.Line1);//开启触发模式:外触发
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大华相机外触发源(Line0)设置失败!\n错误代码:{0:X8}", 99));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置外触发采集失败!\n:{0}", "设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：大华相机设置外触发采集失败!\n错误描述:{0}", ex.Message));
            }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        /// 方法:设置触发信号边缘
        /// [注:用于触发源为硬触发]
        /// </summary>
        /// <param name="dege">边缘信号</param>
        /// <returns></returns>
        public override bool DoSetTriggerActivation(ProCommon.Communal.TriggerLogic edge)
        {
            bool rt = false;          
            try
            {
                if (_deviceRef != null)
                {
                    switch (edge)
                    {
                        case ProCommon.Communal.TriggerLogic.FallEdge:
                            {
                                using (ThridLibray.IEnumParameter p = _deviceRef.ParameterCollection[ThridLibray.ParametrizeNameSet.TriggerActivation])
                                {
                                    rt = p.SetValue("FallingEdge");
                                }
                            }
                            break;
                        case ProCommon.Communal.TriggerLogic.RaiseEdge:
                            {
                                using (ThridLibray.IEnumParameter p = _deviceRef.ParameterCollection[ThridLibray.ParametrizeNameSet.TriggerActivation])
                                {
                                    rt = p.SetValue("RisingEdge");
                                }
                            }
                            break;
                        case ProCommon.Communal.TriggerLogic.NONE:
                        default:
                            break;
                    }
                }

                rt = true;
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机触信号边沿设置失败!\n错误代码:{0:X8}", 99));
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
            try
            {
                if (_deviceRef != null)
                    rt = _deviceRef.GrabUsingGrabLoopThread();

                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机开启采集失败!\n错误代码:{0:X8}", 99));
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
            try
            {

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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceRef != null)
                {
                    rt= _deviceRef.ShutdownGrab();
                }

                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机停止采集失败!\n错误代码:{0:X8}", nRet));
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceRef != null)
                {
                    rt = _deviceRef.ExecuteSoftwareTrigger();
                }

                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机触发失败!\n错误代码:{0:X8}", nRet));
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
            try
            {
                if (_deviceRef != null)
                {
                    using (ThridLibray.IFloatParameter p = _deviceRef.ParameterCollection[ThridLibray.ParametrizeNameSet.ExposureTime])
                    {
                       rt=p.SetValue(exposuretime);                       
                    }
                }
                             
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置曝光失败!\n错误代码:{0:X8}", 99));
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
            try
            {
                if (_deviceRef != null)
                {
                    using (ThridLibray.IFloatParameter p = _deviceRef.ParameterCollection[ThridLibray.ParametrizeNameSet.GainRaw])
                    {
                        rt = p.SetValue(gain);
                    }
                }
                              
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置增益失败!\n错误代码:{0:X8}", 99));
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
            try
            {
                if (_deviceRef != null)
                {
                    using (ThridLibray.IFloatParameter p = _deviceRef.ParameterCollection[ThridLibray.ParametrizeNameSet.AcquisitionFrameRate])
                    {
                        rt = p.SetValue(fps);
                    }
                }
                            
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置帧率失败!\n错误代码:{0:X8}", 99));
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
        /// 设置相机触发延时
        /// </summary>
        /// <param name="trigdelay"></param>
        /// <returns></returns>
        public override bool DoSetTriggerDelay(float trigdelay)
        {
            bool rt = false;         
            try
            {
                if (_deviceRef != null)
                {
                    using (ThridLibray.IFloatParameter p = _deviceRef.ParameterCollection[ThridLibray.ParametrizeNameSet.TriggerDelay])
                    {
                        rt = p.SetValue(trigdelay);
                    }
                }
                             
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置触发延时失败!\n错误代码:{0:X8}", 99));
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
        /// 方法：注册异常回调函数(大华)
        /// </summary>
        /// <returns></returns>
        public override bool DoRegisterExceptionCallBack()
        {
            bool rt = false;          
            if (_deviceRef != null)
            {
                _deviceRef.ConnectionLost += _deviceRef_ConnectionLost;
                rt = true;
            }
         
            if (!rt)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：大华相机注册异常回调失败!\n错误代码:{0:X8}", 99));
            }

            return rt;
        }       

        /// <summary>
        /// 方法:注册采集数据更新回调(大华)
        /// </summary>
        /// <returns></returns>
        public override bool DoRegisterImageGrabbedCallBack()
        {
            bool rt = false;          

            if (_deviceRef != null)
            {
                _deviceRef.StreamGrabber.ImageGrabbed += StreamGrabber_ImageGrabbed;
                rt = true;
            }
          
            if (!rt)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：大华相机注册采集回调失败!\n错误代码:{0:X8}", 99));
            }

            return rt;
        }

        public override bool DoSetOutPut(bool onOff)
        {
            bool rt = false;
            try
            {

            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：大华相机设置输出信号失败!\n错误描述:{0}", ex.Message));
            }
            finally
            {
            }
            return rt;
        }

        public override bool DoCreateCameraSetPage(System.IntPtr windowHandle, string promption)
        {
            return false;
        }

        public override bool DoShowCameraSetPage()
        {
            return false;
        }

        public override string ToString()
        {
            return "CameraDriver[HikVision]";
        }

        #endregion

        #region 大华相机SDK官方函数

        #endregion

    }
}
