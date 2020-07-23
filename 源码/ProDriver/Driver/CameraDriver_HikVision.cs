using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*************************************************************************************
    * CLR    Version：       4.0.30319.42000
    * Class     Name：       CameraDriver_HikVision
    * Machine   Name：       
    * Name     Space：       ProDriver.Driver
    * File      Name：       CameraDriver_HikVision
    * Creating  Time：       4/29/2019 2:47:51 PM
    * Author    Name：       xYz_Albert
    * Description   ：       HiVision相机操作封装类
    * Modifying Time：
    * Modifier  Name：
*************************************************************************************/

namespace ProDriver.Driver
{
    public class CameraDriver_HikVision : CamDriver
    {
        //----------------------------自定义错误代码-----------------------------------//
        //99-图像处理类未实例化
        //98-设备列表为空
        //97-指定的触发源未定义
        //96-内存分配异常
        //95-未获取到图像帧

        public override event CameraImageGrabbedDel CameraImageGrabbedEvt; //图像抓取到事件(统一事件)

        private MvCamCtrl.NET.MyCamera.MV_CC_DEVICE_INFO_LIST _deviceInfoList;  //设备描述信息列表
        private MvCamCtrl.NET.MyCamera.MV_CC_DEVICE_INFO _deviceInfo;           //当前设备描述信息
        private MvCamCtrl.NET.MyCamera _deviceRef;                              //当前设备的资源引用
        private UInt32 _payloadSize = 0;                                 //网络包大小

        private byte[] _pDataForRed = new byte[1024 * 1024 * 20];        //20MB 红色通道存储空间
        private byte[] _pDataForGreen = new byte[1024 * 1024 * 20];      //20MB 绿色通道存储空间
        private byte[] _pDataForBlue = new byte[1024 * 1024 * 20];       //20MB 蓝色通道存储空间
        uint nDataSize;
        IntPtr RedPtr = IntPtr.Zero;
        IntPtr GreenPtr = IntPtr.Zero;
        IntPtr BluePtr = IntPtr.Zero;
        IntPtr pTemp = IntPtr.Zero;
        IntPtr pImageBuffer = System.IntPtr.Zero;

        private MvCamCtrl.NET.MyCamera.cbOutputExdelegate _SDKImageGrabbedDel; //采集更新委托
        private MvCamCtrl.NET.MyCamera.cbExceptiondelegate _SDKCameraLostDel;  //相机异常委托

        private CameraDriver_HikVision()
        {
            _SDKImageGrabbedDel = new MvCamCtrl.NET.MyCamera.cbOutputExdelegate(OnCameraImageGrabbed);
            _SDKCameraLostDel = new MvCamCtrl.NET.MyCamera.cbExceptiondelegate(OnCameraConnectionLost);
            _deviceRef = new MvCamCtrl.NET.MyCamera();
        }

        public CameraDriver_HikVision(ProCommon.Communal.Camera cam) : this()
        {
            Camera = cam;
        }

        /// <summary>
        /// 海康威视相机采集到图像的回调处理函数
        /// </summary>
        /// <param name="pData"></param>
        /// <param name="pFrameInfo"></param>
        /// <param name="pUser"></param>
        private void OnCameraImageGrabbed(IntPtr pData, ref MvCamCtrl.NET.MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            if (HoImage != null
               && HoImage.IsInitialized())
            {
                HoImage.Dispose();
                IsImageGrabbed = false;
            }

            #region 海康相机SDK内部像素格式转换
            int nRet = MvCamCtrl.NET.MyCamera.MV_OK;
            nDataSize = _payloadSize * 3;
            if(pImageBuffer == System.IntPtr.Zero)
                pImageBuffer = System.Runtime.InteropServices.Marshal.AllocHGlobal((int)_payloadSize * 3);

            if (pImageBuffer == IntPtr.Zero)
            {
                return;
            }

            #region 彩色图像
            if (IsColorPixelFormat(pFrameInfo.enPixelType))
            {
                if (pFrameInfo.enPixelType == MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed)
                {
                    pTemp = pData;
                }
                else
                {
                    nRet = ConvertToRGB(_deviceRef, pData, pFrameInfo.nHeight, pFrameInfo.nWidth, pFrameInfo.enPixelType, pImageBuffer);
                    if (MvCamCtrl.NET.MyCamera.MV_OK != nRet)
                    {
                        return;
                    }
                    pTemp = pImageBuffer;
                }

                unsafe
                {
                    byte* pBufForSaveImage = (byte*)pTemp;

                    UInt32 nSupWidth = (pFrameInfo.nWidth + (UInt32)3) & 0xfffffffc;

                    for (int nRow = 0; nRow < pFrameInfo.nHeight; nRow++)
                    {
                        for (int col = 0; col < pFrameInfo.nWidth; col++)
                        {
                            _pDataForRed[nRow * nSupWidth + col] = pBufForSaveImage[nRow * pFrameInfo.nWidth * 3 + (3 * col)];
                            _pDataForGreen[nRow * nSupWidth + col] = pBufForSaveImage[nRow * pFrameInfo.nWidth * 3 + (3 * col + 1)];
                            _pDataForBlue[nRow * nSupWidth + col] = pBufForSaveImage[nRow * pFrameInfo.nWidth * 3 + (3 * col + 2)];
                        }
                    }
                }

                RedPtr = System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(_pDataForRed, 0);
                GreenPtr = System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(_pDataForGreen, 0);
                BluePtr = System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(_pDataForBlue, 0);

                try
                {
                    HalconDotNet.HOperatorSet.GenImage3Extern(out HoImage, (HalconDotNet.HTuple)"byte", pFrameInfo.nWidth, pFrameInfo.nHeight,
                                        (new HalconDotNet.HTuple(RedPtr)), (new HalconDotNet.HTuple(GreenPtr)), (new HalconDotNet.HTuple(BluePtr)), IntPtr.Zero);
                }
                catch (HalconDotNet.HalconException hex1)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机转换RGB图像失败!\n错误信息:{0}", hex1.GetErrorMessage()));
                    return;
                }
            }
            #endregion

            #region 黑白图像
            else if(IsMonoPixelFormat(pFrameInfo.enPixelType))
            {
                if (pFrameInfo.enPixelType == MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                {
                    pTemp = pData;
                }
                else
                {
                    nRet = ConvertToMono8(_deviceRef, pData, pImageBuffer, pFrameInfo.nHeight, pFrameInfo.nWidth, pFrameInfo.enPixelType);
                    if (MvCamCtrl.NET.MyCamera.MV_OK != nRet)
                    {
                        return;
                    }
                    pTemp = pImageBuffer;
                }

                try
                {
                    HalconDotNet.HOperatorSet.GenImage1Extern(out HoImage, "byte", pFrameInfo.nWidth, pFrameInfo.nHeight, pTemp, IntPtr.Zero);
                }
                catch (HalconDotNet.HalconException hex2)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机转换Mono8图像失败!\n错误信息:{0}", hex2.GetErrorMessage()));
                    return;
                }
            }
            #endregion

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
        private void OnCameraConnectionLost(uint nMsgType, IntPtr pUser)
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                System.GC.Collect();
                nRet = MvCamCtrl.NET.MyCamera.MV_CC_EnumDevices_NET(MvCamCtrl.NET.MyCamera.MV_GIGE_DEVICE | MvCamCtrl.NET.MyCamera.MV_USB_DEVICE, ref _deviceInfoList);

                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机枚举设备失败!\n错误代码:{0:X8}",nRet));
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
           return (int)_deviceInfoList.nDeviceNum;
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceInfoList.nDeviceNum > 0)
                {
                    if(index >= 0 && (index <_deviceInfoList.nDeviceNum))
                    {
                        _deviceInfo = (MvCamCtrl.NET.MyCamera.MV_CC_DEVICE_INFO)System.Runtime.InteropServices.Marshal.PtrToStructure(_deviceInfoList.pDeviceInfo[index], typeof(MvCamCtrl.NET.MyCamera.MV_CC_DEVICE_INFO));

                        nRet = _deviceRef.MV_CC_CreateDevice_NET(ref _deviceInfo);
                        rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                        if (!rt)
                        {
                            _deviceRef.MV_CC_DestroyDevice_NET();
                            _deviceRef = null;
                        }
                    }
                    else
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：海康相机获取设备失败!\n索引:{0}\n异常描述:{1}",index, "超出设备索引范围"));
                    }                   
                }
                else
                {                   
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机获取设备失败!\n索引:{0}\n异常描述:{1}",index, "设备列表空"));
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
            if (DoGetCameraByIdx(index))
            {
                IntPtr buffer;
                switch (_deviceInfo.nTLayerType)
                {
                    case MvCamCtrl.NET.MyCamera.MV_GIGE_DEVICE:
                        buffer = System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(this._deviceInfo.SpecialInfo.stGigEInfo, 0);
                        MvCamCtrl.NET.MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MvCamCtrl.NET.MyCamera.MV_GIGE_DEVICE_INFO)System.Runtime.InteropServices.Marshal.PtrToStructure(buffer, typeof(MvCamCtrl.NET.MyCamera.MV_GIGE_DEVICE_INFO));
                        strRT= gigeInfo.chSerialNumber;
                        break;
                    case MvCamCtrl.NET.MyCamera.MV_USB_DEVICE:
                        buffer = System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(this._deviceInfo.SpecialInfo.stUsb3VInfo, 0);
                        MvCamCtrl.NET.MyCamera.MV_USB3_DEVICE_INFO usbInfo=(MvCamCtrl.NET.MyCamera.MV_USB3_DEVICE_INFO)System.Runtime.InteropServices.Marshal.PtrToStructure(buffer, typeof(MvCamCtrl.NET.MyCamera.MV_USB3_DEVICE_INFO));
                        strRT = usbInfo.chSerialNumber;
                        break;
                }               
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceInfoList.nDeviceNum > 0)
                {
                    MvCamCtrl.NET.MyCamera.MV_CC_DEVICE_INFO tmpdevice;
                    for (int i = 0; i < _deviceInfoList.nDeviceNum; i++)
                    {
                        tmpdevice = (MvCamCtrl.NET.MyCamera.MV_CC_DEVICE_INFO)System.Runtime.InteropServices.Marshal.PtrToStructure(_deviceInfoList.pDeviceInfo[i], typeof(MvCamCtrl.NET.MyCamera.MV_CC_DEVICE_INFO));
                        if (tmpdevice.nTLayerType == MvCamCtrl.NET.MyCamera.MV_GIGE_DEVICE)
                        {
                            IntPtr buffer = System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(tmpdevice.SpecialInfo.stGigEInfo, 0);
                            MvCamCtrl.NET.MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MvCamCtrl.NET.MyCamera.MV_GIGE_DEVICE_INFO)System.Runtime.InteropServices.Marshal.PtrToStructure(buffer, typeof(MvCamCtrl.NET.MyCamera.MV_GIGE_DEVICE_INFO));
                            if (gigeInfo.chUserDefinedName == camName)
                            {
                                _deviceInfo = tmpdevice;
                                nRet = _deviceRef.MV_CC_CreateDevice_NET(ref _deviceInfo);
                                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                                if (!rt)
                                {
                                    _deviceRef.MV_CC_DestroyDevice_NET();
                                    _deviceRef = null;
                                }
                                break;
                            }
                        }
                        else if (tmpdevice.nTLayerType == MvCamCtrl.NET.MyCamera.MV_USB_DEVICE)
                        {
                            IntPtr buffer = System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(tmpdevice.SpecialInfo.stUsb3VInfo, 0);
                            MvCamCtrl.NET.MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MvCamCtrl.NET.MyCamera.MV_USB3_DEVICE_INFO)System.Runtime.InteropServices.Marshal.PtrToStructure(buffer, typeof(MvCamCtrl.NET.MyCamera.MV_USB3_DEVICE_INFO));
                            if (usbInfo.chFamilyName == camName)
                            {
                                _deviceInfo = tmpdevice;
                                nRet = _deviceRef.MV_CC_CreateDevice_NET(ref _deviceInfo);
                                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                                if (!rt)
                                {
                                    _deviceRef.MV_CC_DestroyDevice_NET();
                                    _deviceRef = null;
                                }
                                break;
                            }
                        }
                    }

                    if(!rt)
                    {                      
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：海康相机获取设备失败!\n设备名称:{0}\n异常描述:{1}",camName, "指定名称不匹配"));
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceInfoList.nDeviceNum > 0)
                {
                    MvCamCtrl.NET.MyCamera.MV_CC_DEVICE_INFO tmpdevice;
                    for (int i = 0; i < _deviceInfoList.nDeviceNum; i++)
                    {
                        tmpdevice = (MvCamCtrl.NET.MyCamera.MV_CC_DEVICE_INFO)System.Runtime.InteropServices.Marshal.PtrToStructure(_deviceInfoList.pDeviceInfo[i], typeof(MvCamCtrl.NET.MyCamera.MV_CC_DEVICE_INFO));
                        if (tmpdevice.nTLayerType == MvCamCtrl.NET.MyCamera.MV_GIGE_DEVICE)
                        {
                            IntPtr buffer = System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(tmpdevice.SpecialInfo.stGigEInfo, 0);
                            MvCamCtrl.NET.MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MvCamCtrl.NET.MyCamera.MV_GIGE_DEVICE_INFO)System.Runtime.InteropServices.Marshal.PtrToStructure(buffer, typeof(MvCamCtrl.NET.MyCamera.MV_GIGE_DEVICE_INFO));
                            if (gigeInfo.chSerialNumber == camSN)
                            {
                                _deviceInfo = tmpdevice;
                                nRet = _deviceRef.MV_CC_CreateDevice_NET(ref _deviceInfo);
                                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                                if (!rt)
                                {
                                    _deviceRef.MV_CC_DestroyDevice_NET();
                                    _deviceRef = null;
                                }
                                break;
                            }
                        }
                        else if (tmpdevice.nTLayerType == MvCamCtrl.NET.MyCamera.MV_USB_DEVICE)
                        {
                            IntPtr buffer = System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(tmpdevice.SpecialInfo.stUsb3VInfo, 0);
                            MvCamCtrl.NET.MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MvCamCtrl.NET.MyCamera.MV_USB3_DEVICE_INFO)System.Runtime.InteropServices.Marshal.PtrToStructure(buffer, typeof(MvCamCtrl.NET.MyCamera.MV_USB3_DEVICE_INFO));
                            if (usbInfo.chSerialNumber == camSN)
                            {
                                _deviceInfo = tmpdevice;
                                nRet = _deviceRef.MV_CC_CreateDevice_NET(ref _deviceInfo);
                                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                                if (!rt)
                                {
                                    _deviceRef.MV_CC_DestroyDevice_NET();
                                    _deviceRef = null;
                                }
                                break;
                            }
                        }
                    }

                    if (!rt)
                    {                       
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：海康相机获取设备失败!\n设备SN:{0}\n异常描述:{1}",camSN, "指定SN不匹配"));
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

        public override bool DoOpen()
        {
            bool rt = false;
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceRef != null)
                {
                    nRet = _deviceRef.MV_CC_OpenDevice_NET();
                    rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;

                    if (!rt)
                    {
                        _deviceRef.MV_CC_DestroyDevice_NET();                      
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：海康相机打开失败!\n错误代码:{0:X8}",nRet));
                    }
                    else
                    {
                        //网口相机需要设置网络包大小
                        if (_deviceInfo.nTLayerType == MvCamCtrl.NET.MyCamera.MV_GIGE_DEVICE)
                        {
                            int nPacketSize = this._deviceRef.MV_CC_GetOptimalPacketSize_NET();
                            if (nPacketSize > 0)
                            {
                                nRet = this._deviceRef.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;

                                if (!rt)
                                {                            
                                  
                                    if (DriverExceptionDel != null)
                                        DriverExceptionDel(string.Format("错误：海康相机设置GigE数据包大小失败!\n错误代码:{0:X8}",nRet));
                                }
                            }
                            else
                            { 
                                if (DriverExceptionDel != null)
                                    DriverExceptionDel(string.Format("错误：海康相机获取GigE数据包大小失败!\n错误代码:{0:X8}", nRet));
                            }
                        }

                        // ch:获取包大小 || en: Get Payload Size
                        MvCamCtrl.NET.MyCamera.MVCC_INTVALUE stParam = new MvCamCtrl.NET.MyCamera.MVCC_INTVALUE();
                        nRet = this._deviceRef.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
                        rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                        if (!rt)
                        {
                            if (DriverExceptionDel != null)
                                DriverExceptionDel(string.Format("错误：海康相机获取PayloadSize失败!\n错误代码:{0:X8}", nRet));
                            return rt;
                        }
                        _payloadSize = stParam.nCurValue;

                        // ch:获取高 || en: Get Height
                        nRet = this._deviceRef.MV_CC_GetIntValue_NET("Height", ref stParam);
                        rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                        if (!rt)
                        {
                            if (DriverExceptionDel != null)
                                DriverExceptionDel(string.Format("错误：海康相机获取图像高度失败!\n错误代码:{0:X8}", nRet));
                            return rt;
                        }
                        uint nHeight = stParam.nCurValue;

                        // ch:获取宽 || en: Get Width
                        nRet = this._deviceRef.MV_CC_GetIntValue_NET("Width", ref stParam);
                        rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                        if (!rt)
                        {                          
                            if (DriverExceptionDel != null)
                                DriverExceptionDel(string.Format("错误：海康相机获取图像宽度失败!\n错误代码:{0:X8}", nRet));
                            return rt;
                        }
                        uint nWidth = stParam.nCurValue;

                        _pDataForRed = new byte[nWidth * nHeight];
                        _pDataForGreen = new byte[nWidth * nHeight];
                        _pDataForBlue = new byte[nWidth * nHeight];

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

        public override bool DoClose()
        {
            bool rt = false;
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (this._deviceRef != null)
                {
                    nRet = this._deviceRef.MV_CC_CloseDevice_NET();                                  
                }

                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                if (!rt)
                {                  
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机关闭设备失败!\n错误代码:{0:X8}", nRet));
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
            int nRet= MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
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
                            //if (SetExternalTrigger())
                            //    rt = SetFrameNumber(frameNum);
                            rt = SetExternalTrigger();
                            break;
                        case ProCommon.Communal.AcquisitionMode.SoftTrigger:
                            //if (SetInternalTrigger())
                            //    rt = SetFrameNumber(frameNum);
                            rt = SetInternalTrigger();
                            break;
                        default: break;
                    }

                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：海康相机采集模式设置失败!\n错误代码:{0:X8}", nRet));
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceRef != null)
                {

                    nRet = _deviceRef.MV_CC_SetEnumValue_NET("TriggerMode", 0);//关闭触发采集
                    rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;                  
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机设置自由采集失败!\n:{0}", "设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：海康相机设置自由采集失败!\n错误描述:{0}", ex.Message));
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceRef != null)
                {
                    nRet = this._deviceRef.MV_CC_SetEnumValue_NET("AcquisitionMode", 2); //海康官方驱动设置:0-单帧模式,1-多帧模式，2-连续模式
                    rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机设置连续采集失败!\n:{0}", "设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：海康相机设置连续采集失败!\n错误描述:{0}", ex.Message));
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceRef != null)
                {
                    nRet=_deviceRef.MV_CC_SetEnumValue_NET("TriggerMode", 1);//开启触发采集
                    rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                    if(rt)
                    {
                        nRet = this._deviceRef.MV_CC_SetEnumValue_NET("TriggerSource", 7);
                        rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                        if (!rt)
                        {
                            if (DriverExceptionDel != null)
                                DriverExceptionDel(string.Format("错误：海康相机内触发源(Software)设置失败!\n错误代码:{0:X8}", nRet));
                        }
                    }
                    else
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：海康相机开启触发模式失败!\n错误代码:{0:X8}", nRet));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机设置内触发采集失败!\n:{0}", "设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：海康相机设置内触发采集失败!\n错误描述:{0}", ex.Message));
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceRef != null)
                {
                    if (frameNum == 1)
                    {
                        nRet = _deviceRef.MV_CC_SetEnumValue_NET("AcquisitionMode", 0); //海康官方驱动设置:0-单帧模式,1-多帧模式，2-连续模式
                    }
                    else
                    {
                        nRet = _deviceRef.MV_CC_SetEnumValue_NET("AcquisitionMode", 1); //海康官方驱动设置:0-单帧模式,1-多帧模式，2-连续模式 
                    }
                    rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机设置采集帧数失败!\n:{0}", "设备未连接"));
                }
            }
            catch (Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：海康相机设置采集帧数失败!\n错误描述:{0}", ex.Message));
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceRef != null)
                {
                    nRet = _deviceRef.MV_CC_SetEnumValue_NET("TriggerMode", 1);//开启触发采集 
                    rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                    if(rt)
                    {
                        nRet = this._deviceRef.MV_CC_SetEnumValue_NET("TriggerSource", 0);
                        rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                        if (!rt)
                        {
                            if (DriverExceptionDel != null)
                                DriverExceptionDel(string.Format("错误：海康相机外触发源(Line0)设置失败!\n错误代码:{0:X8}", nRet));
                        }
                    }
                    else
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：海康相机开启触发模式失败!\n错误代码:{0:X8}", nRet));
                    }                   
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机设置外触发采集失败!\n:{0}", "设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：海康相机设置外触发采集失败!\n错误描述:{0}", ex.Message));
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_OK;
            try
            {
                if (this._deviceRef != null)
                {
                    switch (edge)
                    {
                        case ProCommon.Communal.TriggerLogic.FallEdge:
                            break;
                        case ProCommon.Communal.TriggerLogic.RaiseEdge:
                            break;                      
                        case ProCommon.Communal.TriggerLogic.NONE:
                        default:
                            break;
                    }
                }

                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                rt = true;
                if (!rt)
                {                   
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机触信号边沿设置失败!\n错误代码:{0:X8}", nRet));
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (this._deviceRef != null)
                {
                    nRet = this._deviceRef.MV_CC_StartGrabbing_NET();                   
                }

                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                if (!rt)
                {                  
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机开启采集失败!\n错误代码:{0:X8}", nRet));
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
                if (this._deviceRef != null)
                {
                    nRet = this._deviceRef.MV_CC_StopGrabbing_NET();                                     
                }

                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                if (!rt)
                {                   
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机停止采集失败!\n错误代码:{0:X8}", nRet));
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
                if (this._deviceRef != null)
                {
                    nRet = this._deviceRef.MV_CC_SetCommandValue_NET("TriggerSoftware");                                    
                }

                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                if (!rt)
                {                  
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机触发失败!\n错误代码:{0:X8}", nRet));
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (this._deviceRef != null)
                {
                    nRet = this._deviceRef.MV_CC_SetFloatValue_NET("ExposureTime", exposuretime);                   
                }

                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                if (!rt)
                {                   
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机设置曝光失败!\n错误代码:{0:X8}", nRet));
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (this._deviceRef != null)
                {
                    nRet = this._deviceRef.MV_CC_SetFloatValue_NET("Gain", gain);                   
                }

                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机设置增益失败!\n错误代码:{0:X8}", nRet));
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (this._deviceRef != null)
                {
                    nRet = this._deviceRef.MV_CC_SetFloatValue_NET("AcquisitionFrameRate", fps);                  
                }

                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                if (!rt)
                {                   
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机设置帧率失败!\n错误代码:{0:X8}", nRet));
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (this._deviceRef != null)
                {
                    nRet = this._deviceRef.MV_CC_SetFloatValue_NET("ExposureTime", trigdelay);                   
                }

                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                if (!rt)
                {                  
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：海康相机设置触发延时失败!\n错误代码:{0:X8}", nRet));
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
        /// 方法：注册异常回调函数(海康)
        /// </summary>
        /// <returns></returns>
        public override bool DoRegisterExceptionCallBack()
        {
            bool rt = false;
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;          
            if (this._deviceRef != null)
            {
                nRet = this._deviceRef.MV_CC_RegisterExceptionCallBack_NET(_SDKCameraLostDel, new System.IntPtr());              
            }

            rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
            if (!rt)
            {               
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：海康相机注册异常回调失败!\n错误代码:{0:X8}", nRet));
            }

            return rt;
        }

        /// <summary>
        /// 方法:注册采集数据更新回调(海康)
        /// </summary>
        /// <returns></returns>
        public override bool DoRegisterImageGrabbedCallBack()
        {
            bool rt = false;
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;

            if (this._deviceRef != null)
            {
                nRet = this._deviceRef.MV_CC_RegisterImageCallBackEx_NET(_SDKImageGrabbedDel, new System.IntPtr());
            }

            rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
            if (!rt)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：海康相机注册采集回调失败!\n错误代码:{0:X8}", nRet));
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
                    DriverExceptionDel(string.Format("错误：海康相机设置输出信号失败!\n错误描述:{0}", ex.Message));
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

        /// <summary>
        /// 方法：显示错误信息
        /// </summary>
        /// <param name="csMessage"></param>
        /// <param name="nErrorNum"></param>
        public void ShowErrorMsg(string csMessage, int nErrorNum)
        {
            string errorMsg;
            if (nErrorNum == 0)
            {
                errorMsg = csMessage;
            }
            else
            {
                errorMsg = csMessage + ": Error =" + String.Format("{0:X}", nErrorNum);
            }

            switch (nErrorNum)
            {
                case MvCamCtrl.NET.MyCamera.MV_E_HANDLE: errorMsg += " Error or invalid handle "; break;
                case MvCamCtrl.NET.MyCamera.MV_E_SUPPORT: errorMsg += " Not supported function "; break;
                case MvCamCtrl.NET.MyCamera.MV_E_BUFOVER: errorMsg += " Cache is full "; break;
                case MvCamCtrl.NET.MyCamera.MV_E_CALLORDER: errorMsg += " Function calling order error "; break;
                case MvCamCtrl.NET.MyCamera.MV_E_PARAMETER: errorMsg += " Incorrect parameter "; break;
                case MvCamCtrl.NET.MyCamera.MV_E_RESOURCE: errorMsg += " Applying resource failed "; break;
                case MvCamCtrl.NET.MyCamera.MV_E_NODATA: errorMsg += " No data "; break;
                case MvCamCtrl.NET.MyCamera.MV_E_PRECONDITION: errorMsg += " Precondition error, or running environment changed "; break;
                case MvCamCtrl.NET.MyCamera.MV_E_VERSION: errorMsg += " Version mismatches "; break;
                case MvCamCtrl.NET.MyCamera.MV_E_NOENOUGH_BUF: errorMsg += " Insufficient memory "; break;
                case MvCamCtrl.NET.MyCamera.MV_E_UNKNOW: errorMsg += " Unknown error "; break;
                case MvCamCtrl.NET.MyCamera.MV_E_GC_GENERIC: errorMsg += " General error "; break;
                case MvCamCtrl.NET.MyCamera.MV_E_GC_ACCESS: errorMsg += " Node accessing condition error "; break;
                case MvCamCtrl.NET.MyCamera.MV_E_ACCESS_DENIED: errorMsg += " No permission "; break;
                case MvCamCtrl.NET.MyCamera.MV_E_BUSY: errorMsg += " Device is busy, or network disconnected "; break;
                case MvCamCtrl.NET.MyCamera.MV_E_NETER: errorMsg += " Network error "; break;
            }

            System.Windows.Forms.MessageBox.Show(errorMsg, "错误信息", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }            

        #region 海康相机SDK官方函数
        /// <summary>
        /// 判断是否黑白图像
        /// </summary>
        /// <param name="enType"></param>
        /// <returns></returns>
        private bool IsMonoPixelFormat(MvCamCtrl.NET.MyCamera.MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 判断是否彩色图像
        /// </summary>
        /// <param name="enType"></param>
        /// <returns></returns>
        private bool IsColorPixelFormat(MvCamCtrl.NET.MyCamera.MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BGR8_Packed:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_RGBA8_Packed:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BGRA8_Packed:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 转换为黑白图像
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pInData"></param>
        /// <param name="pOutData"></param>
        /// <param name="nHeight"></param>
        /// <param name="nWidth"></param>
        /// <param name="nPixelType"></param>
        /// <returns></returns>
        public Int32 ConvertToMono8(object obj, IntPtr pInData, IntPtr pOutData, ushort nHeight, ushort nWidth, MvCamCtrl.NET.MyCamera.MvGvspPixelType nPixelType)
        {
            if (IntPtr.Zero == pInData || IntPtr.Zero == pOutData)
            {
                return MvCamCtrl.NET.MyCamera.MV_E_PARAMETER;
            }

            int nRet = MvCamCtrl.NET.MyCamera.MV_OK;
            MvCamCtrl.NET.MyCamera device = obj as MvCamCtrl.NET.MyCamera;
            MvCamCtrl.NET.MyCamera.MV_PIXEL_CONVERT_PARAM stPixelConvertParam = new MvCamCtrl.NET.MyCamera.MV_PIXEL_CONVERT_PARAM();

            stPixelConvertParam.pSrcData = pInData;//源数据
            if (IntPtr.Zero == stPixelConvertParam.pSrcData)
            {
                return -1;
            }

            stPixelConvertParam.nWidth = nWidth;//图像宽度
            stPixelConvertParam.nHeight = nHeight;//图像高度
            stPixelConvertParam.enSrcPixelType = nPixelType;//源数据的格式
            stPixelConvertParam.nSrcDataLen = (uint)(nWidth * nHeight * ((((uint)nPixelType) >> 16) & 0x00ff) >> 3);

            stPixelConvertParam.nDstBufferSize = (uint)(nWidth * nHeight * ((((uint)MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed) >> 16) & 0x00ff) >> 3);
            stPixelConvertParam.pDstBuffer = pOutData;//转换后的数据
            stPixelConvertParam.enDstPixelType = MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
            stPixelConvertParam.nDstBufferSize = (uint)(nWidth * nHeight * 3);

            nRet = device.MV_CC_ConvertPixelType_NET(ref stPixelConvertParam);//格式转换
            if (MvCamCtrl.NET.MyCamera.MV_OK != nRet)
            {
                return -1;
            }

            return nRet;
        }

        /// <summary>
        /// 转换为彩色图像
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pSrc"></param>
        /// <param name="nHeight"></param>
        /// <param name="nWidth"></param>
        /// <param name="nPixelType"></param>
        /// <param name="pDst"></param>
        /// <returns></returns>
        public Int32 ConvertToRGB(object obj, IntPtr pSrc, ushort nHeight, ushort nWidth, MvCamCtrl.NET.MyCamera.MvGvspPixelType nPixelType, IntPtr pDst)
        {
            if (IntPtr.Zero == pSrc || IntPtr.Zero == pDst)
            {
                return MvCamCtrl.NET.MyCamera.MV_E_PARAMETER;
            }

            int nRet = MvCamCtrl.NET.MyCamera.MV_OK;
            MvCamCtrl.NET.MyCamera device = obj as MvCamCtrl.NET.MyCamera;
            MvCamCtrl.NET.MyCamera.MV_PIXEL_CONVERT_PARAM stPixelConvertParam = new MvCamCtrl.NET.MyCamera.MV_PIXEL_CONVERT_PARAM();

            stPixelConvertParam.pSrcData = pSrc;//源数据
            if (IntPtr.Zero == stPixelConvertParam.pSrcData)
            {
                return -1;
            }

            stPixelConvertParam.nWidth = nWidth;//图像宽度
            stPixelConvertParam.nHeight = nHeight;//图像高度
            stPixelConvertParam.enSrcPixelType = nPixelType;//源数据的格式
            stPixelConvertParam.nSrcDataLen = (uint)(nWidth * nHeight * ((((uint)nPixelType) >> 16) & 0x00ff) >> 3);

            stPixelConvertParam.nDstBufferSize = (uint)(nWidth * nHeight * ((((uint)MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed) >> 16) & 0x00ff) >> 3);
            stPixelConvertParam.pDstBuffer = pDst;//转换后的数据
            stPixelConvertParam.enDstPixelType = MvCamCtrl.NET.MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
            stPixelConvertParam.nDstBufferSize = (uint)nWidth * nHeight * 3;

            nRet = device.MV_CC_ConvertPixelType_NET(ref stPixelConvertParam);//格式转换
            if (MvCamCtrl.NET.MyCamera.MV_OK != nRet)
            {
                return -1;
            }

            return MvCamCtrl.NET.MyCamera.MV_OK;
        }

        #endregion
    }
}
