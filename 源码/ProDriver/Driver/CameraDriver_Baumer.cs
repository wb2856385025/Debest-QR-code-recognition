using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGAPI2.Events;


/*************************************************************************************
    * CLR    Version：       4.0.30319.42000
    * Class     Name：       CameraDriver_Baumer
    * Machine   Name：       
    * Name     Space：       ProDriver.Driver
    * File      Name：       CameraDriver_Baumer
    * Creating  Time：       4/29/2019 2:40:12 PM
    * Author    Name：       xYz_Albert
    * Description   ：       Baumer相机操作封装类
    * Modifying Time：
    * Modifier  Name：
*************************************************************************************/

namespace ProDriver.Driver
{
    public class CameraDriver_Baumer : CamDriver
    {
        public override event CameraImageGrabbedDel CameraImageGrabbedEvt; //图像抓取到事件(统一事件)
        private bool _FirstFrame;        //首帧图像标记
        private System.Byte[] ImgBits;    //图像数据字节数组
        private System.Drawing.Bitmap Bitmap;          //BitMap格式图像
        private System.Drawing.Rectangle _rectSource; //表征源图像大小的矩形
        private bool FlagSaveImg = false;  //保存图像标记
#pragma warning disable CS0649 // Field 'CameraDriver_Baumer.ImgFileDir' is never assigned to, and will always have its default value null
        private string ImgFileDir;         //图像保存路径
#pragma warning restore CS0649 // Field 'CameraDriver_Baumer.ImgFileDir' is never assigned to, and will always have its default value null
        private  BGAPI2.DataStream.NewBufferEventHandler _SDKImageGrabbedDel; //采集更新委托 

        #region Variables for SystemList and System
        private BGAPI2.SystemList _bsystemList = null;
        private BGAPI2.System _bsystem = null;
        private string _bsystemID = "";

        #endregion

        #region Variables for InterfaceList and Interface
        private BGAPI2.InterfaceList _binterfaceList = null;
        private BGAPI2.Interface _binterface = null;
        private string _binterfaceID = "";

        #endregion

        #region Variables for DeviceList and Device
        private BGAPI2.DeviceList _bdeviceList = null;
        private BGAPI2.Device _bdevice = null;
        private string _bdeviceID = "";

        #endregion

        #region Variables for DataStreamList and DataStream
        private BGAPI2.DataStreamList _bdataStreamList = null;
        private BGAPI2.DataStream _bdataStream = null;
        private string _bdataStreamID = "";

        #endregion

        #region Variables for BufferList and Buffer
        private BGAPI2.BufferList BufferList = null;
#pragma warning disable CS0414 // The field 'CameraDriver_Baumer.Buffer' is assigned but its value is never used
        private BGAPI2.Buffer Buffer = null;
#pragma warning restore CS0414 // The field 'CameraDriver_Baumer.Buffer' is assigned but its value is never used

        #endregion

        #region Variables for ImageProcessor
        private BGAPI2.ImageProcessor _bimageProcessor = null;
        #endregion

        private CameraDriver_Baumer()
        {
            _SDKImageGrabbedDel = new BGAPI2.Events.DataStreamEventControl.NewBufferEventHandler(OnCameraImageGrabbed);
        }

        public CameraDriver_Baumer(ProCommon.Communal.Camera cam) : this()
        {
            Camera = cam;
        }

        /// <summary>
        /// 堡盟相机图像采集到回调处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCameraImageGrabbed(object sender, NewBufferEventArgs e)
        {
            if (HoImage != null
              && HoImage.IsInitialized())
             {
                HoImage.Dispose();
                IsImageGrabbed = false;
            }

            #region 堡盟相机SDK内部像素格式转换
            try
            {
                BGAPI2.Buffer mBufferFilled = null;
                mBufferFilled = e.BufferObj;
                if (mBufferFilled == null)
                {
                    //MessageBox.Show("Error: Buffer Timeout after 1000 ms!");
                }
                else if (mBufferFilled.IsIncomplete == true)
                {
                    //MessageBox.Show("Error: Image is incomplete!");
                    mBufferFilled.QueueBuffer();  // queue buffer again 
                }
                else
                {
                    IntPtr imagebuffer = new IntPtr();
                    BGAPI2.ImageProcessor mImageProcessor = BGAPI2.ImageProcessor.Instance;
                    BGAPI2.Image pImage = mImageProcessor.CreateImage((uint)mBufferFilled.Width, (uint)mBufferFilled.Height, mBufferFilled.PixelFormat, mBufferFilled.MemPtr, mBufferFilled.MemSize);
                    BGAPI2.Image pTranImage = null;
                    int w=0, h=0;
                    System.Drawing.Imaging.BitmapData bmpdata;

                    if (mBufferFilled.PixelFormat=="BGR8")
                    {
                        pTranImage = pImage.TransformImage("BGR8");
                        //测试--BGR8
                        w = (int)pTranImage.Width;
                        h = (int)pTranImage.Height;
                        imagebuffer = pTranImage.Buffer; //得到图像指针
                        ImgBits = new byte[w * h * 3];
                        Bitmap = new System.Drawing.Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                        if (_FirstFrame)
                        {
                            _rectSource.X = 0;
                            _rectSource.Y = 0;
                            _rectSource.Width = w;
                            _rectSource.Height = h;
                            _FirstFrame = false;
                        }

                        bmpdata = Bitmap.LockBits(_rectSource, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                        System.Runtime.InteropServices.Marshal.Copy(imagebuffer, ImgBits, 0, w * h * 3);   //图像数据字节数组
                        System.Runtime.InteropServices.Marshal.Copy(ImgBits, 0, bmpdata.Scan0, w * h * 3); //图像数据                   
                        Bitmap.UnlockBits(bmpdata);
                    }
                    else if(mBufferFilled.PixelFormat == "MONO8")
                    {
                        //测试--Mono8
                        w = (int)pImage.Width;
                        h = (int)pImage.Height;
                        imagebuffer = pImage.Buffer; //得到图像指针
                        ImgBits = new byte[w * h];
                        Bitmap = new System.Drawing.Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

                        if (_FirstFrame)
                        {
                            _rectSource.X = 0;
                            _rectSource.Y = 0;
                            _rectSource.Width = w;
                            _rectSource.Height = h;
                            _FirstFrame = false;
                        }

                        bmpdata = Bitmap.LockBits(_rectSource, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                        System.Runtime.InteropServices.Marshal.Copy(imagebuffer, ImgBits, 0, w * h);   //图像数据字节数组
                        System.Runtime.InteropServices.Marshal.Copy(ImgBits, 0, bmpdata.Scan0, w * h); //图像数据
                        Bitmap.UnlockBits(bmpdata);
                    }

                    ProVision.Communal.Functions.BitmapToHObject(Bitmap, out HoImage);//Bitmap转换为HObject
                    //queue buffer again
                    pImage.Release();
                    pTranImage.Release();
                    //imagebuffer.
                    mBufferFilled.QueueBuffer();
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (BGAPI2.Exceptions.IException ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                //string str25 = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                //System.Windows.Forms.MessageBox.Show(str25, "错误提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region 实现抽象函数

        /// <summary>
        /// 枚举在线相机
        /// </summary>
        /// <returns></returns>
        public override bool DoEnumerateCameraList()
        {
            bool rt = false;
            if (this.InitSystemList(ref _bimageProcessor, ref _bsystemList))
            {
                try
                {
                    foreach (KeyValuePair<string, BGAPI2.System> sys_pair in _bsystemList)
                    {
                        try
                        {
                            sys_pair.Value.Open();
                            _bsystemID = sys_pair.Key;
                            if (this.InitInterfaceList(sys_pair.Value, _binterfaceList))
                            {
                                foreach (KeyValuePair<string, BGAPI2.Interface> ifc_pair in _binterfaceList)
                                {
                                    try
                                    {
                                        ifc_pair.Value.Open();
                                        if (this.InitDeviceList(ifc_pair.Value, _bdeviceList))
                                        {
                                            if (_bdeviceList.Count == 0)
                                            {
                                                ifc_pair.Value.Close();
                                            }
                                            else
                                            {
                                                //获取到首个连接设备的接口后，退出接口循环
                                                _binterfaceID = ifc_pair.Key;
                                                break;
                                            }
                                        }
                                    }
                                    catch (BGAPI2.Exceptions.ResourceInUseException ex)
                                    {
                                        if (DriverExceptionDel != null)
                                            DriverExceptionDel(string.Format("错误：堡盟系统{0}已经打开!\n异常类型:{1}\n异常描述:{2}", ifc_pair, ex.GetType(), ex.GetErrorDescription()));                                     
                                    }
                                }

                                //获取到首个连接设备的接口后，退出系统循环
                                if (_binterfaceID != "")
                                {
                                    rt = true;
                                    break;
                                }
                            }
                        }
                        catch (BGAPI2.Exceptions.ResourceInUseException ex)
                        {
                            if (DriverExceptionDel != null)
                                DriverExceptionDel(string.Format("错误：堡盟系统{0}已经打开!\n异常类型:{1}\n异常描述:{2}",sys_pair, ex.GetType(), ex.GetErrorDescription()));
                        }
                    }
                }
                catch (BGAPI2.Exceptions.IException ex)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：堡盟相机获取系统失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                }

            }

            return rt;
        }

        /// <summary>
        /// 计算在线相机数量
        /// </summary>
        /// <returns></returns>
        public override int DoGetCameraListCount()
        {
            if (_bdeviceList != null)
                return _bdeviceList.Count;
            return 0;
        }

        /// <summary>
        /// 根据相机索引获取相机
        /// [相机索引号由其上电顺序得来，非固定]
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public override bool DoGetCameraByIdx(int index)
        {
            return false;
        }

        /// <summary>
        /// 获取索引指定相机的名称
        /// </summary>
        /// <param name="index">相机索引</param>
        /// <returns></returns>
        public override string DoGetCameraSN(int index)
        {
            if (DoGetCameraByIdx(index))
                return this._bdevice.SerialNumber;
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
            _bdevice = null;
            if (_bdeviceList != null)
            {
                try
                {
                    _bdevice = _bdeviceList[camName];
                    rt = true;
                }
                catch (BGAPI2.Exceptions.IException ex)
                {
                    //string str = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                    //MessageBox.Show(str, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);                 
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：堡盟相机获取设备失败!\n设备名称:{0}\n异常描述:{1}",camName, ex.GetErrorDescription()));
                }
                finally
                {
                }
            }
            return rt;
        }

        /// <summary>
        /// 根据相机SN获取相机
        /// </summary>
        /// <param name="camSN"></param>
        /// <returns></returns>
        public override bool DoGetCameraBySN(string camSN)
        {
            bool rt = false;
            _bdevice = null;
            if (_bdeviceList != null)
            {
                try
                {
                    //Device = DeviceList[camName];
                    rt = true;
                }
                catch (BGAPI2.Exceptions.IException ex)
                {
                    //string str = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                    //MessageBox.Show(str, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);                 
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：堡盟相机获取设备失败!\n设备SN:{0}\n异常描述:{1}\n时间：{2}",camSN, ex.GetErrorDescription()));
                }
                finally
                {
                }
            }
            return rt;
        }

        public override bool DoOpen()
        {
            bool rt = false;
            try
            {
                if (_bsystemID != "")
                {
                    return rt;
                }
                else
                {
                    _bsystem = _bsystemList[_bsystemID];
                }

                if (_binterfaceID != "")
                {
                    return rt;
                }
                else
                {
                    _binterface = _binterfaceList[_binterfaceID];
                }

                if (this.InitDeviceList(_binterface, _bdeviceList))
                {
                    foreach (KeyValuePair<string, BGAPI2.Device> dev_pair in _bdeviceList)
                    {
                        try
                        {
                            dev_pair.Value.Open();
                            _bdeviceID = dev_pair.Key;
                            if (_bdeviceID != "")
                            {
                                break;
                            }
                        }
                        catch (BGAPI2.Exceptions.ResourceInUseException ex)
                        {
                            if (DriverExceptionDel != null)
                                DriverExceptionDel(string.Format("错误：堡盟设备{0}已经打开!\n异常类型:{1}\n异常描述:{2}",dev_pair.Key, ex.GetType(), ex.GetErrorDescription()));
                        }
                    }

                    if (_bdeviceID == "")
                    {
                        _binterface.Close();
                        _bsystem.Close();
                        return rt;
                    }
                    else
                    {
                        _bdevice = _bdeviceList[_bdeviceID];
                    }

                    if (this.InitDataStreamList(_bdevice, _bdataStreamList))
                    {
                        try
                        {
                            foreach (KeyValuePair<string, BGAPI2.DataStream> ds_pair in _bdataStreamList)
                            {
                                ds_pair.Value.Open();
                                _bdataStreamID = ds_pair.Key;
                                if (_bdataStreamID != "")
                                {
                                    break;
                                }
                            }

                            //判断是否获取到数据流
                            if (_bdataStreamID == "")
                            {
                                _bdevice.Close();
                                _binterface.Close();
                                _bsystem.Close();
                                return rt;
                            }
                            else
                            {
                                _bdataStream = _bdataStreamList[_bdataStreamID];
                            }

                            if (this.InitBufferList(_bdataStream, BufferList))
                            {
                                if (this.QueueBufferList(BufferList))
                                {
                                    try
                                    {
                                        _bdataStream.StartAcquisition();
                                    }
                                    catch (BGAPI2.Exceptions.IException ex)
                                    {
                                        //string str18 = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                                        //MessageBox.Show(str18, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);                                      
                                        if (DriverExceptionDel != null)
                                            DriverExceptionDel(string.Format("错误：堡盟相机数据流开启采集失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                                        return rt;
                                    }
                                    rt = true;
                                }
                            }
                        }
                        catch (BGAPI2.Exceptions.IException ex)
                        {
                            //string str = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                            //MessageBox.Show(str, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);                           
                            if (DriverExceptionDel != null)
                                DriverExceptionDel(string.Format("错误：堡盟相机打开数据流失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                        }

                    }//初始化数据流
                }//初始化设备
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
            try
            {
                if (_bdevice.RemoteNodeList.GetNodePresent("AcquisitionAbort"))
                    _bdevice.RemoteNodeList["AcquisitionAbort"].Execute();
                _bdevice.RemoteNodeList["AcquisitionStop"].Execute();
            }
            catch (BGAPI2.Exceptions.IException ex)
            {
                //string str26 = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                //MessageBox.Show(str26, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);              
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：堡盟相机采集停止失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                return rt;
            }

            try
            {
                _bdataStream.StopAcquisition();
                BufferList.DiscardAllBuffers();
            }
            catch (BGAPI2.Exceptions.IException ex)
            {
                //string str27 = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                //MessageBox.Show(str27, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);              
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：堡盟相机数据流停止失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                return rt;
            }

            try
            {
                _bdataStream.UnregisterNewBufferEvent();
                _bdataStream.RegisterNewBufferEvent(BGAPI2.Events.EventMode.UNREGISTERED);
                BGAPI2.Events.EventMode currentEventMode = _bdataStream.EventMode;
            }
            catch (BGAPI2.Exceptions.IException ex)
            {
                //string str28 = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                //MessageBox.Show(str28, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);              
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：堡盟相机注销缓存更新事件失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                return rt;
            }

            try
            {
                BufferList.DiscardAllBuffers();
                _bdataStream.Close();
                _bdevice.Close();
                _binterface.Close();
                _bsystem.Close();
                rt = true;
            }
            catch (BGAPI2.Exceptions.IException ex)
            {
                //string str29 = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                //MessageBox.Show(str29, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);              
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：堡盟相机关闭数据流等失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
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
            try
            {
                if (_bdevice != null)
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
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：堡盟相机设置设备采集模式失败!\n:{0}", "设备未连接"));
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
                if (_bdevice != null)
                {
                    _bdevice.RemoteNodeList["TriggerMode"].Value = "Off";
                    _bdevice.RemoteNodeList["TriggerSource"].Value = "Off";
                    //未知自由采集的设置
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：堡盟相机设置设备采集模式失败!\n:{0}", "设备未连接"));
                }
            }
            catch
            {
            }
            return rt;
        }

        /// <summary>
        /// 连续采集模式
        /// TriggerSource:外触发--"Line0",软触发--"Software"
        /// 全部触发--"All",关闭触发--"Off",时间触发--"Action1" 
        /// </summary>
        /// <returns></returns>
        private bool SetContinueRun()
        {
            bool rt = false;
            try
            {
                if (_bdevice != null)
                {
                    _bdevice.RemoteNodeList["TriggerMode"].Value = "Off";
                    _bdevice.RemoteNodeList["TriggerSource"].Value = "Action1";
                   //未知连续采集的设置
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：堡盟相机设置设备采集模式失败!\n:{0}", "设备未连接"));
                }
            }
            catch
            {
            }
            return rt;
        }

        /// <summary>
        /// 设置内部触发采集(软触发)
        /// TriggerSource:外触发--"Line0",软触发--"Software"
        /// 全部触发--"All",关闭触发--"Off",时间触发--"Action1" 
        /// </summary>
        /// <returns></returns>
        private bool SetInternalTrigger()
        {
            bool rt = false;
            try
            {
                if (_bdevice != null)
                {
                    _bdevice.RemoteNodeList["TriggerMode"].Value = "On";
                    _bdevice.RemoteNodeList["TriggerSource"].Value = "Software";
                    rt = true;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：堡盟相机设置设备采集模式失败!\n:{0}", "设备未连接"));
                }
            }
            catch
            {
            }
            return rt;
        }

        /// <summary>
        /// 设置外部触发采集(硬触发)
        /// TriggerSource:外触发--"Line0",软触发--"Software"
        /// 全部触发--"All",关闭触发--"Off",时间触发--"Action1"
        /// </summary>
        /// <returns></returns>
        private bool SetExternalTrigger()
        {
            bool rt = false;
            try
            {
                if (_bdevice != null)
                {
                    _bdevice.RemoteNodeList["TriggerMode"].Value = "On";
                    _bdevice.RemoteNodeList["TriggerSource"].Value = "Line0";
                    rt = true;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：堡盟相机设置设备采集模式失败!\n:{0}", "设备未连接"));
                }
            }
            catch
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
                if (_bdevice != null)
                {

                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：堡盟相机设置设备采集模式失败!\n:{0}", "设备未连接"));
                }
            }
            catch
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
                if (_bdevice != null)
                {
                    switch (edge)
                    {
                        case ProCommon.Communal.TriggerLogic.FallEdge:
                            {
                                _bdevice.RemoteNodeList["TriggerActivation"].Value = "FallingEdge";
                                rt = true;
                            }
                            break;
                        case ProCommon.Communal.TriggerLogic.RaiseEdge:
                            {
                                _bdevice.RemoteNodeList["TriggerActivation"].Value = "RisingEdge";
                                rt = true;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch
            {
            }
            return rt;
        }

        public override bool DoStartGrab()
        {
            bool rt = false;
            try
            {
                if (_bdevice != null)
                {
                    _bdevice.RemoteNodeList["AcquisitionStart"].Execute();
                    rt = true;
                }
            }
            catch
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
            try
            {
                if (_bdevice != null)
                {
                    _bdevice.RemoteNodeList["AcquisitionStop"].Execute();
                    rt = true;
                }
            }
            catch
            {
            }
            return rt;
        }

        public override bool DoSoftTriggerOnce()
        {
            bool rt = false;
            try
            {
                if (_bdevice != null)
                {
                    if ("Software" == (string)_bdevice.RemoteNodeList["TriggerSource"].Value)
                    {
                        _bdevice.RemoteNodeList["TriggerSoftware"].Execute();
                        rt = true;
                    }
                }
            }
            catch
            {
            }
            return rt;
        }

        public override bool DoSetExposureTime(float exposuretime)
        {
            bool rt = false;
            try
            {
                if (_bdevice != null)
                {
                    _bdevice.RemoteNodeList["ExposureTime"].Value = (double)exposuretime;
                    rt = true;
                }
            }
            catch (BGAPI2.Exceptions.IException ex)
            {
                //string str18 = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                //MessageBox.Show(str18, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);             
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：堡盟相机设置曝光时间失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                return rt;
            }
            return rt;
        }

        public override bool DoSetGain(float gain)
        {
            bool rt = false;
            try
            {
                if (_bdevice != null)
                {
                    _bdevice.RemoteNodeList["Gain"].Value = (double)gain;
                    rt = true;
                }
            }
            catch (BGAPI2.Exceptions.IException ex)
            {
                //string str18 = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                //MessageBox.Show(str18, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);             
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：堡盟相机设置增益失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                return rt;
            }
            return rt;
        }

        /// <summary>
        /// 设置相机帧率
        /// </summary>
        /// <param name="fps">帧率</param>
        /// <returns></returns>
        public override bool DoSetFrameRate(float fps)
        {
            bool rt = false;
            try
            {
                if (_bdevice != null)
                {
                    _bdevice.RemoteNodeList["ExposureTime"].Value = (double)fps;
                    rt = true;
                }
            }
            catch (BGAPI2.Exceptions.IException ex)
            {
                //string str18 = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                //MessageBox.Show(str18, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);              
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：堡盟相机设置帧率失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                return rt;
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
                if (_bdevice != null)
                {
                    _bdevice.RemoteNodeList["TriggerDelay"].Value = (double)trigdelay;
                    rt = true;
                }
            }
            catch (BGAPI2.Exceptions.IException ex)
            {
                //string str18 = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                //MessageBox.Show(str18, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);             
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：堡盟相机设置触发延时失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                return rt;
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
                    DriverExceptionDel(string.Format("错误：堡盟相机设置输出信号失败!\n错误描述:{0}",ex.Message));
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

        /// <summary>
        /// 注册采集异常回调
        /// </summary>
        /// <returns></returns>
        public override bool DoRegisterExceptionCallBack()
        {
            bool rt = false;
            try
            {
                if (this._bdataStream != null)
                {

                }
            }
            catch
            {
            }
            return rt;
        }

        /// <summary>
        /// 注册采集数据更新回调
        /// </summary>
        /// <returns></returns>
        public override bool DoRegisterImageGrabbedCallBack()
        {
            bool rt = false;
            try
            {
                if (this._bdataStream != null)
                {
                    this._bdataStream.RegisterNewBufferEvent(BGAPI2.Events.EventMode.EVENT_HANDLER);
                    this._bdataStream.NewBufferEvent += _SDKImageGrabbedDel;
                    rt = true;
                }
            }
            catch
            {
            }
            return rt;
        }

        public override string ToString()
        {
            return "CameraDriver[Baumer]";
        }

        #endregion       

        #region 封装堡盟相机官方函数
        /// <summary>
        /// 初始化系统列表(堡盟相机定义的系统)
        /// </summary>
        /// <param name="imgProcessor">堡盟定义的过程</param>
        /// <param name="systemList">堡盟定义的系统列表</param>
        /// <returns></returns>
        private bool InitSystemList(ref BGAPI2.ImageProcessor imgProcessor, ref BGAPI2.SystemList systemList)
        {
            bool rt = false;
            try
            {
                try
                {
                    imgProcessor = BGAPI2.ImageProcessor.Instance;
                }
                catch (BGAPI2.Exceptions.IException ex)
                {
                    //string str1 = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                    //MessageBox.Show(str1, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);                   
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：堡盟相机初始化过程失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                    return rt;
                }

                //获取系统列表
                try
                {
                    systemList = BGAPI2.SystemList.Instance;
                    systemList.Refresh();
                    //int sysCount = mSystemList.Count;
                }
                catch (BGAPI2.Exceptions.IException ex)
                {
                    //string str2 = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                    //MessageBox.Show(str2, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);                  
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：堡盟相机初始化系统列表失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                    return rt;
                }

                rt = true;
            }
            catch
            {
            }
            return rt;
        }

        /// <summary>
        /// 初始化接口列表(堡盟相机定义的接口)
        /// </summary>
        /// <param name="system">堡盟定义的系统</param>
        /// <param name="intfList">堡盟定义的接口列表</param>
        /// <returns></returns>
        private bool InitInterfaceList(BGAPI2.System system, BGAPI2.InterfaceList intfList)
        {
            bool rt = false;
            if (system != null)
            {
                try
                {
                    intfList = system.Interfaces;
                    intfList.Refresh(1000);
                    rt = true;
                }
                catch (BGAPI2.Exceptions.IException ex)
                {
                    //string str = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                    //MessageBox.Show(str, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);                  
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：堡盟相机初始化接口列表失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                }
                finally
                {
                }
            }
            return rt;
        }

        /// <summary>
        /// 初始化设备列表(堡盟相机定义的设备)
        /// </summary>
        /// <param name="intface">堡盟定义的接口</param>
        /// <param name="deviceList">堡盟定义的设备列表</param>
        /// <returns></returns>
        private bool InitDeviceList(BGAPI2.Interface intface, BGAPI2.DeviceList deviceList)
        {
            bool rt = false;
            if (intface != null)
            {
                try
                {
                    deviceList = intface.Devices;
                    deviceList.Refresh(1000);
                    rt = true;
                }
                catch (BGAPI2.Exceptions.IException ex)
                {
                    //string str = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                    //MessageBox.Show(str, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);                  
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：堡盟相机初始化设备列表失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                }
                finally
                {
                }
            }

            return rt;
        }

        /// <summary>
        /// 初始化数据流列表(堡盟相机定义的数据流)
        /// </summary>
        /// <param name="device">设备(堡盟相机定义的设备)</param>
        /// <param name="dataStreamList">数据流列表</param>
        /// <returns></returns>
        private bool InitDataStreamList(BGAPI2.Device device, BGAPI2.DataStreamList dataStreamList)
        {
            bool rt = false;
            //dataStreamList = null;
            if (device != null)
            {
                try
                {
                    dataStreamList = device.DataStreams;
                    dataStreamList.Refresh();
                    rt = true;
                }
                catch (BGAPI2.Exceptions.IException ex)
                {
                    //string str = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                    //MessageBox.Show(str, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);                  
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：堡盟相机初始化数据流列表失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                }
                finally
                {
                }
            }
            return rt;
        }

        /// <summary>
        /// 初始化缓存列表
        /// </summary>
        /// <param name="dataStream">数据流(堡盟相机定义的数据流)</param>
        /// <param name="bufferList">缓存列表</param>
        /// <returns></returns>
        private bool InitBufferList(BGAPI2.DataStream dataStream, BGAPI2.BufferList bufferList)
        {
            bool rt = false;
            //bufferList = null;
            if (bufferList != null)
            {
                try
                {
                    bufferList = dataStream.BufferList;
                    BGAPI2.Buffer buffer = null;

                    //内部Buffer模式设定为4个Buffer
                    for (int i = 0; i < 4; i++)
                    {
                        buffer = new BGAPI2.Buffer();
                        buffer.QueueBuffer();
                        bufferList.Add(buffer);
                    }

                    rt = true;
                }
                catch (BGAPI2.Exceptions.IException ex)
                {
                    //string str = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                    //MessageBox.Show(str, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);                   
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：堡盟相机初始化缓存区列表失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                }
                finally
                {
                }
            }
            return rt;
        }

        private bool QueueBufferList(BGAPI2.BufferList bufferList)
        {
            bool rt = false;
            if (bufferList != null)
            {
                try
                {
                    foreach (KeyValuePair<string, BGAPI2.Buffer> buf_pair in bufferList)
                    {
                        buf_pair.Value.QueueBuffer();
                    }
                    rt = true;
                }
                catch (BGAPI2.Exceptions.IException ex)
                {
                    //string str = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                    //MessageBox.Show(str, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);                  
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：堡盟相机缓存加入队列失败!\n异常类型:{0}\n异常描述:{1}",ex.GetType(), ex.GetErrorDescription()));
                }
                finally
                {
                }
            }
            return rt;
        }
        #endregion        

        #region 堡盟相机图像采集回调函数参考

        /// <summary>
        /// 相机采集到图像的回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mDataStream_NewBufferEvent(object sender, BGAPI2.Events.NewBufferEventArgs e)
        {
            try
            {
                BGAPI2.Buffer mBufferFilled = null;
                mBufferFilled = e.BufferObj;
                if (mBufferFilled == null)
                {
                    //MessageBox.Show("Error: Buffer Timeout after 1000 ms!");
                }
                else if (mBufferFilled.IsIncomplete == true)
                {
                    //MessageBox.Show("Error: Image is incomplete!");
                    mBufferFilled.QueueBuffer();  // queue buffer again 
                }
                else
                {
                    IntPtr imagebuffer = new IntPtr();
                    BGAPI2.ImageProcessor mImageProcessor = BGAPI2.ImageProcessor.Instance;
                    BGAPI2.Image pImage = mImageProcessor.CreateImage((uint)mBufferFilled.Width, (uint)mBufferFilled.Height, mBufferFilled.PixelFormat, mBufferFilled.MemPtr, mBufferFilled.MemSize);

                    BGAPI2.Image pTranImage = null;
                    pTranImage = pImage.TransformImage("BGR8");

                    //测试--BGR8
                    int w = (int)pTranImage.Width;
                    int h = (int)pTranImage.Height;
                    imagebuffer = pTranImage.Buffer; //得到图像指针

                    //测试--Mono8
                    //int w = (int)pImage.Width;
                    //int h = (int)pImage.Height;
                    //imagebuffer = pImage.Buffer; //得到图像指针

                    //显示图像，第一次需要根据显示控件大小来调整
                    if (_FirstFrame)
                    {
                        //首帧时，初始化数据

                        //测试--BGR8
                        ImgBits = new byte[w * h * 3];
                        Bitmap = new System.Drawing.Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                        //测试--Mono8
                        //mImgBits = new byte[w * h];
                        //mBitmap = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

                        _rectSource.X = 0;
                        _rectSource.Y = 0;
                        _rectSource.Width = w;
                        _rectSource.Height = h;
                        _FirstFrame = false;
                    }

                    System.Drawing.Imaging.BitmapData bmpdata;

                    //测试--BGR8
                    bmpdata = Bitmap.LockBits(_rectSource, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    System.Runtime.InteropServices.Marshal.Copy(imagebuffer, ImgBits, 0, w * h * 3);   //图像数据字节数组
                    System.Runtime.InteropServices.Marshal.Copy(ImgBits, 0, bmpdata.Scan0, w * h * 3); //图像数据                   
                    Bitmap.UnlockBits(bmpdata);

                    //测试--Mono8
                    //bmpdata = mBitmap.LockBits(mRecSourece, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    //System.Runtime.InteropServices.Marshal.Copy(imagebuffer, mImgBits, 0, w * h);   //图像数据字节数组
                    //System.Runtime.InteropServices.Marshal.Copy(mImgBits, 0, bmpdata.Scan0, w * h); //图像数据
                    //mBitmap.UnlockBits(bmpdata);

                    if (FlagSaveImg)
                    {
                        Bitmap.Save(ImgFileDir + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".bmp");
                        FlagSaveImg = false;
                    }

                    //queue buffer again
                    pImage.Release();
                    pTranImage.Release();
                    //imagebuffer.
                    mBufferFilled.QueueBuffer();

                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (BGAPI2.Exceptions.IException ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                //string str25 = string.Format("ExceptionType:{0}\nExceptionDescription:{1}\nIn function:{2}", ex.GetType(), ex.GetErrorDescription(), ex.GetFunctionName());
                //System.Windows.Forms.MessageBox.Show(str25, "错误提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }            
        }

        #endregion 
    }
}
