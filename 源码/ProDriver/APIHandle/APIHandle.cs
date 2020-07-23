using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       OpHandle
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProDriver.OpHandle
 * File      Name：       APIHandle
 * Creating  Time：       1/15/2020 10:15:29 AM
 * Author    Name：       xYz_Albert
 * Description   ：       操作接口函数
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProDriver.APIHandle
{
    /// <summary>
    /// 相机操作接口函数
    /// </summary>
    public class CameraAPIHandle
    {
        public HalconDotNet.HObject HoImage;
        public event ProDriver.Driver.CameraImageGrabbedDel ImageGrabbedEvt;
        private ProCommon.Communal.Camera _cam;
        public CameraAPIHandle(ProCommon.Communal.Camera cam)
        {
            if (cam != null)
            {
                _cam = cam;
                switch (cam.CtrllerBrand)
                {
                    case ProCommon.Communal.CtrllerBrand.Baumer:
                        break;
                    case ProCommon.Communal.CtrllerBrand.Dalsa:
                        break;
                    case ProCommon.Communal.CtrllerBrand.Imaging:
                        break;
                    case ProCommon.Communal.CtrllerBrand.MindVision:
                        ProDriver.Driver.CameraDriver_MindVision camdriver_mindvision = new ProDriver.Driver.CameraDriver_MindVision(cam);
                        ICamDriverable = (camdriver_mindvision as ProDriver.Driver.ICamDriver);
                        break;
                    case ProCommon.Communal.CtrllerBrand.Basler:
                        ProDriver.Driver.CameraDriver_Basler camdriver_basler = new ProDriver.Driver.CameraDriver_Basler(cam);
                        ICamDriverable = (camdriver_basler as ProDriver.Driver.ICamDriver);
                        break;
                    case ProCommon.Communal.CtrllerBrand.HikVision:
                        ProDriver.Driver.CameraDriver_HikVision camdriver_hikvision = new ProDriver.Driver.CameraDriver_HikVision(cam);
                        ICamDriverable = (camdriver_hikvision as ProDriver.Driver.ICamDriver);
                        break;
                    default:
                        break;
                }
            }
        }
        private void OnCameraImageGrabbed(ProCommon.Communal.Camera cam, HalconDotNet.HObject hoImage)
        {
            if (hoImage != null
                && hoImage.IsInitialized())
            {
                if (HoImage != null
                    && HoImage.IsInitialized())
                    HoImage.Dispose();
                HoImage = hoImage;
                if(ImageGrabbedEvt!=null)
                    ImageGrabbedEvt(cam, HoImage);
            }
        }

        /// <summary>
        /// 属性:是否取到图像标记
        /// </summary>
        public bool IsImageGrabbed
        {
            set
            {
                if (ICamDriverable != null)
                {
                    ICamDriverable.IsImageGrabbed = value;
                }
            }
            get
            {
                if (ICamDriverable != null)
                    return ICamDriverable.IsImageGrabbed;
                return false;
            }
        }

        public ProDriver.Driver.ICamDriver ICamDriverable
        {
            private set;
            get;
        }

        /// <summary>
        /// 枚举在线相机
        /// </summary>
        /// <returns></returns>
        public bool EnumerateCameraList()
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.EnumerateCameraList();
            return rt;
        }

        /// <summary>
        /// 选择相机
        /// </summary>
        /// <param name="indx">相机索引</param>
        /// <returns></returns>
        public bool GetCameraByIdx(int indx)
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.GetCameraByIdx(indx);
            return rt;
        }

        /// <summary>
        /// 选择相机
        /// </summary>
        /// <param name="camNmae">相机名称</param>
        /// <returns></returns>
        public bool GetCameraByName(string camNmae)
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.GetCameraByName(camNmae);
            return rt;
        }

        /// <summary>
        /// 选择相机
        /// </summary>
        /// <param name="camSN">相机序列号</param>
        /// <returns></returns>
        public bool GetCameraBySN(string camSN)
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.GetCameraBySN(camSN);
            return rt;
        }

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.Open();
            return rt;
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.Close();
            return rt;
        }
        public bool SetAcquisitionMode(ProCommon.Communal.AcquisitionMode acqmode, uint frameNum)
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.SetAcquisitionMode(acqmode, frameNum);
            return rt;
        }

        /// <summary>
        /// 方法:设置相机输出信号
        /// </summary>
        /// <param name="triglogic">触发信号逻辑</param>
        /// <param name="delaytime">边沿信号时的延时,单位毫秒</param>
        /// <returns></returns>
        public bool SetOutPut(ProCommon.Communal.TriggerLogic triglogic, int delaytime)
        {
            bool rt = false;
            if (ICamDriverable != null)
            {
                switch (triglogic)
                {
                    case ProCommon.Communal.TriggerLogic.FallEdge:
                        rt = ICamDriverable.SetOutPut(true);
                        if (rt)
                        {
                            System.Threading.Thread.Sleep(delaytime);
                            rt = ICamDriverable.SetOutPut(false);
                        }
                        break;
                    case ProCommon.Communal.TriggerLogic.LogicFalse:
                        rt = ICamDriverable.SetOutPut(false);
                        break;
                    case ProCommon.Communal.TriggerLogic.LogicTrue:
                        rt = ICamDriverable.SetOutPut(true);
                        break;
                    case ProCommon.Communal.TriggerLogic.RaiseEdge:
                        rt = ICamDriverable.SetOutPut(false);
                        if (rt)
                        {
                            System.Threading.Thread.Sleep(delaytime);
                            rt = ICamDriverable.SetOutPut(true);
                        }
                        break;
                    default: break;
                }
            }
            return rt;
        }

        /// <summary>
        /// 方法：设置触发边沿信号模式
        /// </summary>
        /// <param name="edgemode">边缘模式</param>
        /// <returns></returns>
        public bool SetTriggerActivation(ProCommon.Communal.TriggerLogic edgemode)
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.SetTriggerActivation(edgemode);
            return rt;
        }

        /// <summary>
        /// 开启采集
        /// </summary>
        /// <returns></returns>
        public bool StartGrab()
        {
            bool rt = false;
            if (ICamDriverable != null)
            {
                rt = ICamDriverable.StartGrab();
            }
            return rt;
        }

        /// <summary>
        /// 停止采集
        /// </summary>
        /// <returns></returns>
        public bool StopGrab()
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.StopGrab();
            return rt;
        }

        /// <summary>
        /// 软件触发
        /// </summary>
        /// <returns></returns>
        public bool SoftTriggerOnce()
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.SoftTriggerOnce();
            return rt;
        }

        /// <summary>
        /// 设置曝光时间
        /// </summary>
        /// <param name="exposuretime"></param>
        /// <returns></returns>
        public bool SetExposureTime(float exposuretime)
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.SetExposureTime(exposuretime);
            return rt;
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="gain"></param>
        /// <returns></returns>
        public bool SetGain(float gain)
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.SetGain(gain);
            return rt;
        }

        /// <summary>
        /// 设置帧率
        /// </summary>
        /// <param name="fps"></param>
        /// <returns></returns>
        public bool SetFrameRate(float fps)
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.SetFrameRate(fps);
            return rt;
        }

        /// <summary>
        /// 设置触发延迟时间
        /// </summary>
        /// <param name="trigdelay"></param>
        /// <returns></returns>
        public bool SetTriggerDelay(float trigdelay)
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.SetTriggerDelay(trigdelay);
            return rt;
        }

        /// <summary>
        /// 创建相机参数设置窗口
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="promption"></param>
        /// <returns></returns>
        public bool CreateCameraSetPage(System.IntPtr windowHandle, string promption)
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.CreateCameraSetPage(windowHandle, promption);
            return rt;
        }

        /// <summary>
        /// 显示相机参数设置窗口
        /// </summary>
        /// <returns></returns>
        public bool ShowCameraSetPage()
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.ShowCameraSetPage();
            return rt;
        }

        /// <summary>
        /// 注册相机异常委托
        /// </summary>
        /// <returns></returns>
        public bool RegisterExceptionCallBack()
        {
            bool rt = false;
            if (ICamDriverable != null)
                rt = ICamDriverable.RegisterExceptionCallBack();
            return rt;
        }

        /// <summary>
        /// 注册相机采集到图像委托
        /// </summary>
        /// <returns></returns>
        public bool RegisterImageGrabbedCallBack()
        {
            bool rt = false;
            if (ICamDriverable != null)
            {
                ICamDriverable.CameraImageGrabbedEvt += new ProDriver.Driver.CameraImageGrabbedDel(OnCameraImageGrabbed); 
                rt = ICamDriverable.RegisterImageGrabbedCallBack();
            }
            return rt;
        }
    }   

    /// <summary>
    /// 运控板卡操作接口函数
    /// </summary>
    public class BoardCtrllerAPIHandle
    {
        public BoardCtrllerAPIHandle(ProCommon.Communal.BoardCtrller boardctrller)
        {
            switch (boardctrller.CtrllerBrand)
            {
                case ProCommon.Communal.CtrllerBrand.LeadShine:
                    {
                    }
                    break;
                case ProCommon.Communal.CtrllerBrand.ZMotion:
                    {
                        this.BoardCtrller = boardctrller;
                        ProDriver.Driver.BoardDriver_ZMotion boarddriverZm = new ProDriver.Driver.BoardDriver_ZMotion();
                        this.IBoardDriver = (boarddriverZm as ProDriver.Driver.IBoardDriver);
                    }
                    break;
                case ProCommon.Communal.CtrllerBrand.ICPDAS:
                    break;
            }
        }

        /// <summary>
        /// 属性:控制器
        /// </summary>
        public ProCommon.Communal.BoardCtrller BoardCtrller
        {
            private set;
            get;
        }

        /// <summary>
        /// 属性:控制器操控接口
        /// </summary>
        public ProDriver.Driver.IBoardDriver IBoardDriver
        {
            set;
            get;
        }

        #region 调用接口
        public bool ConnectCtrller(string ip,int port=8089)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.ConnectCtrller(ip, port);
            return rt;
        }
      
        public bool DisconnectCtrller()
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.DisconnectCtrller();
            return rt;
        }

        public bool InitCtrllerSys()
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.InitCtrllerSys();
            return rt;
        }

        public bool SetBaseAxes(int axisNum, int[] piAxislist)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetBaseAxes(axisNum, piAxislist);
            return rt;
        }

        public bool SetAxisType(int naxis, int type)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisType(naxis, type);
            return rt;
        }

        public bool SetAxisPulseOutMode(int naxis, int mode)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisPulseOutMode(naxis, mode);
            return rt;
        }

        public bool SetAxisALMIn(int naxis, int inputno)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisALMIn(naxis, inputno);
            return rt;
        }

        public bool SetAxisHRevIn(int naxis, int inputno)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisHRevIn(naxis, inputno);
            return rt;
        }

        public bool SetAxisSRevValue(int naxis, float fvalue)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisSRevValue(naxis, fvalue);
            return rt;
        }

        public bool SetAxisDatumIn(int naxis, int inputno)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisDatumIn(naxis, inputno);
            return rt;
        }

        public bool SetAxisHFwdIn(int naxis, int inputno)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisHFwdIn(naxis, inputno);
            return rt;
        }

        public bool SetAxisSFwdValue(int naxis, float fvalue)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisSFwdValue(naxis, fvalue);
            return rt;
        }

        public bool SetPortInEffectiveLevel(int inputno, ProCommon.Communal.ElectricalLevel level)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetPortInEffectiveLevel(inputno, level);
            return rt;
        }

        public bool SetAxisUnits(int naxis, float units)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisUnits(naxis, units);
            return rt;
        }

        public bool SetAxisTrapeziumPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisTrapeziumPara(naxis, lspeed, maxspeed, tacc, tdec);
            return rt;
        }

        public bool SetAxisSigmoidPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec, int sacc, int sdec)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisSigmoidPara(naxis, lspeed, maxspeed, tacc, tdec, sacc, sdec);
            return rt;
        }

        public bool SetAxisSDEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel sdlevel, uint sdmode)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisSDEffectiveLevel(naxis, enable, sdlevel, sdmode);
            return rt;
        }

        public bool SetAxisPCSEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel pcslevel)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisPCSEffectiveLevel(naxis, enable, pcslevel);
            return rt;
        }

        public bool SetAxisINPEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel inplevel)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisINPEffectiveLevel(naxis, enable, inplevel);
            return rt;
        }

        public bool SetAxisERCEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel erclevel, uint ercwidth, uint ercofftime)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisERCEffectiveLevel(naxis, enable, erclevel, ercwidth, ercofftime);
            return rt;
        }

        public bool SetAxisERC(int naxis, uint sel)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisERC(naxis, sel);
            return rt;
        }

        public bool SetAxisALMEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel almlevel, uint actionmode)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisALMEffectiveLevel(naxis, almlevel, actionmode);
            return rt;
        }

        public bool SetAxisEZEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ezlevel, uint actionmode)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisEZEffectiveLevel(naxis, ezlevel, actionmode);
            return rt;
        }

        public bool SetAxisLTCEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ltclevel, uint actionmode)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisLTCEffectiveLevel(naxis, ltclevel, actionmode);
            return rt;
        }

        public bool SetAxisELLevel(int naxis, uint actionmode)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisELEffectiveLevel(naxis, actionmode);
            return rt;
        }

        public bool SetAxisDatumEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel datumlevel, uint filter)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisDatumEffectiveLevel(naxis, datumlevel, filter);
            return rt;
        }

        public bool SetAxisServo(int naxis, bool onoff)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisServo(naxis, onoff);
            return rt;
        }

        public bool SetAxisEMGEffectiveLevel(uint enable, ProCommon.Communal.ElectricalLevel emglevel)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisEMGEffectiveLevel(enable, emglevel);
            return rt;
        }

        public bool FindSingleDatum(int naxis, ProCommon.Communal.MoveDir moveDir, int mode)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.FindDatum(naxis, moveDir, mode);
            return rt;
        }

        public bool SingleContinueMove(int naxis, ProCommon.Communal.MoveDir movedir)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SingleContinueMove(naxis, movedir);
            return rt;
        }

        public bool SingleRelMove(int naxis, float pos)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SingleRelMove(naxis, pos);
            return rt;
        }

        public bool SingleAbsMove(int naxis, float pos)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SingleAbsMove(naxis, pos);
            return rt;
        }

        public bool SingleCancelMove(int naxis, int mode)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SingleCancelMove(naxis, mode);
            return rt;
        }

        public bool Line2Move(float[] axespos, int mode)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.Line2Move(axespos, mode);
            return rt;
        }

        public bool CenterBasedArc2Move(float[] dstpos, float[] cenpos, int dir, int mode)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.CenterBasedArc2Move(dstpos, cenpos, dir, mode);
            return rt;
        }

        public bool PointsBasedArc2Move(float[] midpos, float[] dstpos, int mode)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.PointsBasedArc2Move(midpos, dstpos, mode);
            return rt;
        }

        public bool SetAxisCurPos(int naxis, float curpos)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetAxisCurPos(naxis, curpos);
            return rt;
        }

        public bool GetAxisCurPos(int naxis, ref float curpos)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.GetAxisCurPos(naxis, ref curpos);
            return rt;
        }

        public bool GetAxisCurspeed(int naxis, ref float curspeed)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.GetAxisCurspeed(naxis, ref curspeed);
            return rt;
        }

        public bool ChekcAxisIfStop(int naxis, ref bool stopped)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.ChekcAxisIfStop(naxis, ref stopped);
            return rt;
        }

        public bool GetAxisStatus(int naxis, ref int axisStatus)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.GetAxisStatus(naxis, ref axisStatus);
            return rt;
        }

        public bool CheckAxisIfNormal(int naxis, ref bool normal)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.CheckAxisIfNormal(naxis, ref normal);
            return rt;
        }

        public bool RapidStop(int mode)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.RapidStop(mode);
            return rt;
        }

        public bool SetOutBitLogicValue(int nbit, bool level)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.SetOutBitLogicValue(nbit, level);
            return rt;
        }

        public bool GetOutBitLogicValue(int nport, ref bool onoff)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.GetOutBitLogicValue(nport, ref onoff);
            return rt;
        }

        public bool GetInBitLogicValue(int nport, ref bool onoff)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.GetInBitLogicValue(nport, ref onoff);
            return rt;
        }

        public bool WaitForAxisFindDatum(int naxis, bool waitfordatum = true, double sleepsecond = 0.01, double timeout = 50, bool enablepause = true, float limitdistance = -1, float specifiedpos = 0.0f)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.WaitForAxisFindDatum(naxis, waitfordatum, sleepsecond, timeout, enablepause, limitdistance, specifiedpos);
            return rt;
        }

        public bool WaitForAxisStop(int naxis, double sleepsecond = 0.01, double timeout = 50, bool enablepause = true)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.WaitForAxisStop(naxis, sleepsecond, timeout, enablepause);
            return rt;
        }

        public bool WaitForAxisLimit(int naxis, ProDriver.Driver.LimitType limittype, bool waitforstatus = true, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true)
        {
            bool rt = false;
            if (this.IBoardDriver != null)
                rt = this.IBoardDriver.WaitForAxisLimit(naxis, limittype, waitforstatus, sleepsecond, timeout, enablepause);
            return rt;
        }
        #endregion      

        /// <summary>
        /// 控制器所有轴伺服使能或禁止
        /// </summary>
        /// <param name="on_off">
        /// true,使能
        /// false,禁止</param>
        public void SwitchBoardCtrllerServo(bool on_off)
        {
            if (this.BoardCtrller != null)
            {
                for (int i = 0; i < this.BoardCtrller.AxisList.Count; i++)
                {
                    SetAxisServo(this.BoardCtrller.AxisList[i].Number, on_off);
                }
            }
        }

        /// <summary>
        /// 初始化控制器输入口有效电平
        /// </summary>
        public void InitInPortEffectiveLevel()
        {
            if (this.BoardCtrller != null
                && this.BoardCtrller.InVarObjList != null)
            {
                for (int i = 0; i < this.BoardCtrller.InVarObjList.Count; i++)
                {
                    int nBit = ProCommon.Communal.Functions.GetIndexByInVar(this.BoardCtrller.InVarObjList[i].InVar);
                    SetPortInEffectiveLevel(nBit, this.BoardCtrller.InVarObjList[i].EffectiveLevel);
                }
            }
        }

        /// <summary>
        /// 初始化控制器输入输出变量
        /// </summary>
        /// <summary>
        /// 初始化输入输出变量对象
        /// </summary>
        public void InitInAndOutPutVarObj()
        {
            if (this.BoardCtrller != null
               && this.BoardCtrller.InVarObjList != null)
            {
                for (int i = 0; i < this.BoardCtrller.InVarObjList.Count; i++)
                {
                    //增加:设置输入端口逻辑状态
                    this.BoardCtrller.InVarObjList[i].NewValue = false;
                }
            }

            if (this.BoardCtrller != null
               && this.BoardCtrller.OutVarObjList != null)
            {
                int nport = -1;
                for (int i = 0; i < this.BoardCtrller.OutVarObjList.Count; i++)
                {
                    //增加:设置输出端口逻辑状态
                    nport = ProCommon.Communal.Functions.GetIndexByOutVar(this.BoardCtrller.OutVarObjList[i].OutVar);
                    SetOutBitLogicValue(nport, false); //注意轴伺服使能需要重新开启
                    System.Threading.Thread.Sleep(5);
                    this.BoardCtrller.OutVarObjList[i].NewValue = false;
                }
            }
        }

        /// <summary>
        /// 控制器所有轴参数更新
        /// </summary>
        /// <param name="runmode"></param>
        public void RefreshBoardCtrllerAxesPara(string runmode)
        {
            for (int i = 0; i < this.BoardCtrller.AxisList.Count; i++)
            {
                #region 轴基本参数
                this.SetAxisType(this.BoardCtrller.AxisList[i].Number, this.BoardCtrller.AxisList[i].Type);
                this.SetAxisUnits(this.BoardCtrller.AxisList[i].Number, this.BoardCtrller.AxisList[i].PulseUnit);
                this.SetAxisPulseOutMode(this.BoardCtrller.AxisList[i].Number, this.BoardCtrller.AxisList[i].PulseOutMode);
                #endregion

                #region 轴信号参数

                //通用输入口作轴信号口 ([注:]有些板卡有专用信号口,有的板卡利用通用输入输出口做信号口)
                this.SetAxisALMIn(this.BoardCtrller.AxisList[i].Number, this.BoardCtrller.AxisList[i].ALMIn);
                this.SetPortInEffectiveLevel(this.BoardCtrller.AxisList[i].ALMIn, this.BoardCtrller.AxisList[i].ALMInLevel);     //轴报警信号口及有效电平
                this.SetAxisHRevIn(this.BoardCtrller.AxisList[i].Number, this.BoardCtrller.AxisList[i].HRevIn);
                this.SetPortInEffectiveLevel(this.BoardCtrller.AxisList[i].HRevIn, this.BoardCtrller.AxisList[i].HRevInLevel);   //轴负向硬限位信号口及有效电平
                this.SetAxisDatumIn(this.BoardCtrller.AxisList[i].Number, this.BoardCtrller.AxisList[i].DatumIn);
                this.SetPortInEffectiveLevel(this.BoardCtrller.AxisList[i].DatumIn, this.BoardCtrller.AxisList[i].DatumInLevel); //轴原点限位信号口及有效电平
                this.SetAxisHFwdIn(this.BoardCtrller.AxisList[i].Number, this.BoardCtrller.AxisList[i].HFwdIn);
                this.SetPortInEffectiveLevel(this.BoardCtrller.AxisList[i].HFwdIn, this.BoardCtrller.AxisList[i].HFwdInLevel);   //轴正向硬限位信号口及有效电平 

                //专用输入口作轴信号口 ([注:]有些板卡有专用信号口,有的板卡利用通用输入输出口做信号口)
                //this.SetAxisALMLevel(this.BoardCtrller.AxisList[i].Num, 0, 0);       //设置指定轴的报警信号逻辑电平(低电平)
                //this.SetAxisELLevel(this.BoardCtrller.AxisList[i].Num, 0);           //设置指定轴的限位信号逻辑电平和制动方式(低电平+立即停止)
                //this.SetAxisDatumLevel(this.BoardCtrller.AxisList[i].Num, 0, 1);     //设置指定轴的原点信号逻辑电平(低电平+过滤)
                //this.SetAxisSDLevel(this.BoardCtrller.AxisList[i].Num, 0, 0, 1);     //设置指定轴的减速信号逻辑电平(低电平)
                //this.SetAxisPCSLevel(this.BoardCtrller.AxisList[i].Num, 0, 0);       //设置指定轴的位置改变信号逻辑电平(低电平)
                //this.SetAxisINPLevel(this.BoardCtrller.AxisList[i].Num, 0, 0);       //设置指定轴的到位信号逻辑电平(低电平)
                //this.SetAxisERCLevel(this.BoardCtrller.AxisList[i].Num, 3, 0, 1, 1); //设置指定轴的误差清除信号逻辑电平(低电平+有效输出宽度102us+关断时间12us)
                //this.SetAxisEZLevel(this.BoardCtrller.AxisList[i].Num, 0, 1);        //设置指定轴的编码器复位信号逻辑电平(低电平+计数器复位信号)
                //this.SetAxisLTCLevel(this.BoardCtrller.AxisList[i].Num, 0, 1);       //设置指定轴的锁存器锁存触发信号逻辑电平(低电平)
                #endregion

                #region 轴运行速度
                switch (runmode.ToUpper().Trim())
                {
                    case "RESET":
                        this.SetAxisTrapeziumPara(this.BoardCtrller.AxisList[i].Number, 10.0f, 50.0f, 0.5f, 0.5f);
                        break;
                    default:
                        float tacc = (this.BoardCtrller.AxisList[i].RunSpeed - this.BoardCtrller.AxisList[i].StartSpeed) / this.BoardCtrller.AxisList[i].Accel;
                        this.SetAxisTrapeziumPara(this.BoardCtrller.AxisList[i].Number, this.BoardCtrller.AxisList[i].StartSpeed, this.BoardCtrller.AxisList[i].RunSpeed, tacc, tacc);
                        break;
                }
                #endregion
            }
        }

        /// <summary>
        /// 等控制器所有轴停止
        /// </summary>
        public bool WaitAxesStop()
        {
            bool rt = false;

            if (this.BoardCtrller.IsConnected)
            {
                for (int i = 0; i < this.BoardCtrller.AxisList.Count; i++)
                {
                    if (!this.WaitForAxisStop(this.BoardCtrller.AxisList[i].Number))
                    {
                        System.Windows.Forms.MessageBox.Show("等轴[" + this.BoardCtrller.AxisList[i].Name + "]停止失败!", "错误信息",
                           System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        break;
                    }
                }
                rt = true;
            }

            return rt;
        }

        #region 获取输入控制变量对象

        /// <summary>
        /// 根据输入控制变量获取输入控制变量对象
        /// </summary>
        /// <param name="inVar">输入变量</param>
        /// <returns></returns>
        public ProCommon.Communal.InVarObj GetInVarObj(ProCommon.Communal.InVar inVar)
        {
            System.Collections.IEnumerable ie = from InVarObjs in this.BoardCtrller.InVarObjBList
                                                where InVarObjs.InVar == inVar
                                                select InVarObjs;
            System.Collections.Generic.List<ProCommon.Communal.InVarObj> inVarObjList = ie.Cast<ProCommon.Communal.InVarObj>().ToList();

            if (inVarObjList.Count > 0)
            {
                return inVarObjList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据输入控制变量获取输入控制变量对象
        /// 根据是否更新NewValue以及EditValue，执行相应操作
        /// </summary>
        /// <param name="inVar">输入变量</param>
        /// <param name="setNew">是否更新NewValue</param>
        /// <param name="setEdit">是否更新EditValue</param>
        /// <returns></returns>
        public ProCommon.Communal.InVarObj GetInVarObj(ProCommon.Communal.InVar inVar, bool setNew, bool setEdit)
        {
            if (setNew && this.BoardCtrller.IsConnected)
                this.GetInVarObjStatus(inVar);

            System.Collections.IEnumerable ie = from InVarObjs in this.BoardCtrller.InVarObjBList
                                                where InVarObjs.InVar == inVar
                                                select InVarObjs;
            System.Collections.Generic.List<ProCommon.Communal.InVarObj> inVarObjList = ie.Cast<ProCommon.Communal.InVarObj>().ToList();

            if (inVarObjList.Count > 0)
            {
                if (setEdit)
                    inVarObjList[0].EditValue = inVarObjList[0].NewValue;
                return inVarObjList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取单个输入控制变量对象的状态
        /// </summary>
        /// <param name="inVar"></param>
        /// <returns></returns>
        public bool GetInVarObjStatus(ProCommon.Communal.InVar inVar)
        {
            bool isOn = false;
            return GetInVarObjStatus(inVar, out isOn);
        }

        /// <summary>
        /// 获取单个输入控制变量对象的状态
        /// </summary>
        /// <param name="inVar">输入变量</param>
        /// <param name="isOn">输入变量对象是否有效</param>
        /// <returns>true,执行正常;false执行异常</returns>
        public bool GetInVarObjStatus(ProCommon.Communal.InVar inVar, out bool isOn)
        {
            bool rt = false;
            isOn = false;
            ProCommon.Communal.InVarObj inVarObj = this.GetInVarObj(inVar);

            //限制IN变量有效，其他无效
            if (!(inVarObj.VarType.Trim().ToUpper() == "IN"))
            {
                return rt;
            }
#pragma warning disable CS0168 // The variable 'inNm' is declared but never used
            string inNm;
#pragma warning restore CS0168 // The variable 'inNm' is declared but never used
            int port = ProCommon.Communal.Functions.GetIndexByInVar(inVar);
            if (port != -1)
            {
                rt = this.GetInBitLogicValue(port, ref isOn);
                if (rt)
                    inVarObj.NewValue = isOn;
            }
            else { rt = true; }
            return rt;
        }

        #endregion

        #region 获取输出控制变量对象

        /// <summary>
        /// 根据输出控制变量获取输出控制变量对象
        /// </summary>
        /// <param name="outVar">输出变量</param>
        /// <returns></returns>
        public ProCommon.Communal.OutVarObj GetOutVarObj(ProCommon.Communal.OutVar outVar)
        {
            System.Collections.IEnumerable ie = from OutVarObjs in this.BoardCtrller.OutVarObjBList
                                                where OutVarObjs.OutVar == outVar
                                                select OutVarObjs;
            System.Collections.Generic.List<ProCommon.Communal.OutVarObj> OutVarObjList = ie.Cast<ProCommon.Communal.OutVarObj>().ToList();

            if (OutVarObjList.Count > 0)
            {
                return OutVarObjList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据输出控制变量获取输出控制变量对象
        /// 根据是否更新NewValue以及EditValue，执行相应操作
        /// </summary>
        /// <param name="outVar">输出变量</param>
        /// <param name="setNew">是否更新NewValue</param>
        /// <param name="setEdit">是否更新EditValue</param>
        /// <returns></returns>
        public ProCommon.Communal.OutVarObj GetOutVarObj(ProCommon.Communal.OutVar outVar, bool setNew, bool setEdit)
        {
            if (setNew && this.BoardCtrller.IsConnected)
                GetOutVarObjStatus(outVar);

            System.Collections.IEnumerable ie = from OutVarObjs in this.BoardCtrller.OutVarObjBList
                                                where OutVarObjs.OutVar == outVar
                                                select OutVarObjs;
            System.Collections.Generic.List<ProCommon.Communal.OutVarObj> OutVarObjList = ie.Cast<ProCommon.Communal.OutVarObj>().ToList();

            if (OutVarObjList.Count > 0)
            {
                if (setEdit)
                    OutVarObjList[0].EditValue = OutVarObjList[0].NewValue;
                return OutVarObjList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取单个输出控制变量对象的状态
        /// </summary>
        /// <param name="outVar">输出变量</param>
        /// <returns></returns>
        public bool GetOutVarObjStatus(ProCommon.Communal.OutVar outVar)
        {
            bool isOn = false;
            return GetOutVarObjStatus(outVar, out isOn);
        }

        /// <summary>
        /// 获取单个输出控制变量对象的状态
        /// </summary>
        /// <param name="outVar">输出变量</param>
        /// <param name="isOn">输出变量对象是否有效</param>
        /// <returns></returns>
        public bool GetOutVarObjStatus(ProCommon.Communal.OutVar outVar, out bool isOn)
        {
            bool rt = false;
            isOn = false;
            ProCommon.Communal.OutVarObj outVarObj = this.GetOutVarObj(outVar);

            //限制OUT变量有效，其他无效
            if (!(outVarObj.VarType.Trim().ToUpper() == "OUT"))
            {
                return rt;
            }

#pragma warning disable CS0168 // The variable 'inNm' is declared but never used
            string inNm;
#pragma warning restore CS0168 // The variable 'inNm' is declared but never used
            int port = ProCommon.Communal.Functions.GetIndexByOutVar(outVar);
            if (port != -1)
            {
                rt = this.GetOutBitLogicValue(port, ref isOn);
                if (rt)
                    outVarObj.NewValue = isOn;
            }
            else { rt = true; }
            return rt;
        }

        #endregion

        #region 设置输出控制变量对象

        /// <summary>
        /// 设置输出控制变量对象的EditValue到输出口
        /// </summary>
        /// <param name="outVar">输出控制变量</param>
        /// <returns></returns>
        public bool SetOutVarObjStatus(ProCommon.Communal.OutVar outVar)
        {
            ProCommon.Communal.OutVarObj outVarObj = this.GetOutVarObj(outVar);
            bool isOn = false;
            try
            {
                isOn = bool.Parse(outVarObj.EditValue.ToString());
            }
            catch
            {
                outVarObj.EditValue = isOn;
            }

            return this.SetOutVarObjStatus(outVar, isOn);
        }

        /// <summary>
        /// 设置指定值到输出口
        /// </summary>
        /// <param name="outVar">输出控制变量</param>
        /// <param name="isOn">指定值</param>
        /// <returns></returns>
        public bool SetOutVarObjStatus(ProCommon.Communal.OutVar outVar, bool isOn)
        {
            bool rt = false;
            ProCommon.Communal.OutVarObj outVarObj = this.GetOutVarObj(outVar);
            if (outVarObj.VarType.Trim().ToUpper() != "OUT")
            {
                return rt;
            }

#pragma warning disable CS0168 // The variable 'outNm' is declared but never used
            string outNm;
#pragma warning restore CS0168 // The variable 'outNm' is declared but never used
            int port = ProCommon.Communal.Functions.GetIndexByOutVar(outVar);
            rt = this.SetOutBitLogicValue(port, isOn);
            return rt;
        }

        #endregion
    }

    /// <summary>
    /// 运控PLC操作接口函数
    /// [注:待完善_2020-01-15]
    /// </summary>
    public class PLCCtrllerAPIHandle
    {

    }  

    #region Socket操作接口函数

    //接收数据委托（字符串型 接收数据,字节数组型 接收数据）
    public delegate void ComWrappedSocketReceivedDel(ProCommon.Communal.ComWrappedSocket comSokect,string msgStr, byte[] msgByte);

    //连接委托（整型 错误代码，字符串型 错误信息）
    public delegate void ComWrappedSocketConnectedDel(ProCommon.Communal.ComWrappedSocket comSokect,int iErrorCode, string strErrorMsg);

    //关闭委托（整型 错误代码，字符串型 错误信息）  
    public delegate void ComWrappedSocketClosedDel(ProCommon.Communal.ComWrappedSocket comSokect,int iErrorCode, string strErrorMsg);

    /// <summary>
    /// Socket异步调用接口函数
    /// </summary>
    public class SocketAsyncAPIHandle
    {
        public SocketAsyncAPIHandle(ProCommon.Communal.ComWrappedSocket comSocket)
        {
            ReceivedData = new StringBuilder();
            _dataBuffer = new byte[1024 * 1024 * 10]; //预设接收字节10MB
            ComSocket = comSocket;
            ConnectedEvt = new ComWrappedSocketConnectedDel(OnConnected);
            ReceivedEvt = new ComWrappedSocketReceivedDel(OnReceived);
            ClosedEvt = new ComWrappedSocketClosedDel(OnClosed);
        }

        #region ComSocket的事件回调函数
        void OnClosed(ProCommon.Communal.ComWrappedSocket comSokect,int iErrorCode, string strErrorMsg)
        {

        }

        void OnReceived(ProCommon.Communal.ComWrappedSocket comSokect,string msgStr, byte[] msgByte)
        {

        }

        void OnConnected(ProCommon.Communal.ComWrappedSocket comSokect,int iErrorCode, string strErrorMsg)
        {

        }

        #endregion

        public event ComWrappedSocketConnectedDel ConnectedEvt;
        public event ComWrappedSocketReceivedDel ReceivedEvt;
        public event ComWrappedSocketClosedDel ClosedEvt;

        public ProCommon.Communal.ComWrappedSocket ComSocket { private set; get; }

        /// <summary>
        /// 接收字节转换成字符串
        /// </summary>
        public System.Text.StringBuilder ReceivedData
        {
            private set;
            get;
        }

        /// <summary>
        /// 接收字节缓存
        /// </summary>
        private byte[] _dataBuffer;

        #region Socket连接
        public bool Connect()
        {
            bool rt = false;           
            try
            {
                //若Socket已经打开，则关闭
                if (ComSocket.Socket != null && ComSocket.Socket.Connected)
                {
                    ComSocket.Socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                    System.Threading.Thread.Sleep(100);
                    ComSocket.Socket.Close();
                }

                //创建Socket对象(此处以TCP/IP协议创建对象,其他协议下创建对象的方法待完善)
                ComSocket.Socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork,
                    System.Net.Sockets.SocketType.Stream, ComSocket.ProtocolType);
                //定义服务器连接套接字
                System.Net.IPEndPoint iPEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(ComSocket.IP), ComSocket.Port);

                //非阻止模式连接到服务器端
                ComSocket.Socket.Blocking = false;
                System.AsyncCallback OnConnected = new System.AsyncCallback(ConnectedCallBack);

                //异步请求连接到服务器端，当完成连接后回调:OnConnected委托
                ComSocket.Socket.BeginConnect(iPEndPoint, OnConnected, ComSocket.Socket);
                rt = true;
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
            }
            finally
            {
            }
            return rt;
        }

        private void ConnectedCallBack(IAsyncResult ar)
        {
            int errorCode = 0;
            string errorMsg = "连接成功";
            System.Net.Sockets.Socket socket = (System.Net.Sockets.Socket)ar.AsyncState;

            // Check if socket was connected
            try
            {
                if (socket != null && socket.Connected)
                {
                    //结束异步连接请求（避免内存泄漏)
                    socket.EndConnect(ar);

                    errorMsg = string.Format("服务器:[{0}]{1}", socket.RemoteEndPoint.ToString(), errorMsg);
                    ComSocket.IsConnected = true; //改变连接属性
                }
                else
                {
                    errorCode = 1; errorMsg = "服务器:[" + ComSocket.IP + "]未连接";
                    ComSocket.IsConnected = false; //改变连接属性
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                Close();
                ComSocket.IsConnected = false; //改变连接属性
                errorCode = 1; errorMsg = "服务器:[" + ComSocket.IP + "]连接失败";
                if (socket != null && ConnectedEvt != null)
                {
                    System.Windows.Forms.Control target = ConnectedEvt.Target as System.Windows.Forms.Control;
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SocketConnected
                        target.Invoke(ConnectedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ConnectedEvt(ComSocket,errorCode, errorMsg);
                }
            }
            finally {}
        }
        #endregion

        #region Socket接收
        public bool Receive()
        {
            bool rt = false;
            try
            {
                //若Socket已经打开
                if (ComSocket.Socket != null && ComSocket.Socket.Connected)
                {
                    ComSocket.Socket.Blocking = false;
                    AsyncCallback OnReceived = new AsyncCallback(ReceivedCallBack);
                    //异步请求接收服务器数据，完成后回调：OnReceived委托
                    ComSocket.Socket.BeginReceive(_dataBuffer, 0, _dataBuffer.Length, System.Net.Sockets.SocketFlags.None, OnReceived, ComSocket.Socket);
                    rt = true;
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
            }
            finally
            {
            }
            return rt;
        }

        private void ReceivedCallBack(IAsyncResult ar)
        {
            int errorCode = 0;
            string errorMsg = "接收成功";
            System.Net.Sockets.Socket socket = (System.Net.Sockets.Socket)ar.AsyncState;

            // Check if socket was connected           
            if (socket != null && socket.Connected)
            {
                try
                {
                    //结束异步接收请求（避免内存泄漏)
                    int nBytesRec = socket.EndReceive(ar);

                    #region 接收到数据
                    if (nBytesRec > 0)
                    {
                        errorMsg = string.Format("服务器:[{0}]{1}", socket.RemoteEndPoint, errorMsg);

                        ReceivedData.Clear();
                        string strtemp = Encoding.Default.GetString(_dataBuffer, 0, nBytesRec);
                        ReceivedData.Append(strtemp);

                        if (ReceivedEvt != null)
                        {
                            System.Windows.Forms.Control target = ReceivedEvt.Target as System.Windows.Forms.Control;
                            if (target != null && target.InvokeRequired)
                                //非创建控件线程同步调用事件：SocketConnected
                                target.Invoke(ReceivedEvt, new object[] { ReceivedData.ToString(),
                                    ProCommon.Communal.Functions.GetSubData(_dataBuffer, 0, nBytesRec) });
                            else
                                //创建控件线程调用事件
                                ReceivedEvt(ComSocket,ReceivedData.ToString(),
                                    ProCommon.Communal.Functions.GetSubData(_dataBuffer, 0, nBytesRec));
                        }

                    }
                    else
                    {
                        errorCode = 1; errorMsg = "接收数据为空";
                        //Common.SocketClosedEventHandler d=new Common.SocketClosedEventHandler();
                        if (ClosedEvt != null)
                        {
                            System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                            if (target != null && target.InvokeRequired)
                                //非创建控件线程同步调用事件：SocketClosed
                                target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                            else
                                //创建控件线程调用事件
                                ClosedEvt(ComSocket, errorCode, errorMsg);
                        }
                    }
                    #endregion
                }
                catch
                {
                    try
                    {
                        if (ClosedEvt != null)
                        {
                            System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                            if (target != null && target.InvokeRequired)
                                //非创建控件线程同步调用事件：SocketClosed
                                target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                            else
                                //创建控件线程调用事件
                                ClosedEvt(ComSocket,errorCode, errorMsg);
                        }
                    }
                    catch { }
                }
            }
            else
            {
                ComSocket.IsConnected = false; //改变连接属性
            }
        }

        #endregion

        #region Socket发送
        public bool Send(string strcmd)
        {
            bool rt = false;
            int errorCode = 0; string errorMsg = "发送成功";
            try
            {
                if ((ComSocket.Socket == null) || (!ComSocket.Socket.Connected))
                {
                    errorCode = 1;
                    if (ComSocket.Socket == null)
                    {
                        errorMsg = "Socket对象为空";
                        errorMsg = string.Format("服务器:[{0}]{1}", ComSocket.Socket.RemoteEndPoint, errorMsg);
                    }
                    if (!this.ComSocket.Socket.Connected)
                    {
                        errorMsg = "Socket对象未连接";
                        errorMsg = string.Format("服务器:[{0}]{1}", ComSocket.IP + ":" + ComSocket.Port, errorMsg);
                    }
                    return rt;
                }
                // Convert to byte array and send.   
                byte[] cmdBuffer = Encoding.Default.GetBytes(strcmd);
                int rint = ComSocket.Socket.Send(cmdBuffer, cmdBuffer.Length, System.Net.Sockets.SocketFlags.None);
                rt = true;
            }
            catch (Exception ex)
            {
                Close();
                ComSocket.IsConnected = false; //改变连接属性
                if (ClosedEvt != null)
                {
                    System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                    errorCode = 1;
                    errorMsg = string.Format("服务器[{0}]{1}", ComSocket.Socket.RemoteEndPoint, ex.Message);
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SocketClosed
                        target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ClosedEvt(ComSocket,errorCode, errorMsg);
                }
            }
            finally
            {
            }
            return rt;
        }

        public bool Send(byte[] data)
        {
            bool rt = false;
            int errorCode = 0; string errorMsg = "发送成功";

            try
            {

                if ((ComSocket.Socket == null) || (!ComSocket.Socket.Connected))
                {
                    errorCode = 1;
                    if (this.ComSocket.Socket == null)
                    {
                        errorMsg = "Socket对象为空";
                        errorMsg = string.Format("服务器:[{0}]{1}", ComSocket.Socket.RemoteEndPoint, errorMsg);
                    }
                    if (!this.ComSocket.Socket.Connected)
                    {
                        errorMsg = "Socket对象未连接";
                        errorMsg = string.Format("服务器:[{0}]{1}", ComSocket.IP + ":" + ComSocket.Port, errorMsg);
                    }
                    return rt;
                }              
                            
                int rint = ComSocket.Socket.Send(data, data.Length, System.Net.Sockets.SocketFlags.None);
                rt = true;
            }
            catch (Exception ex)
            {
                Close();
                ComSocket.IsConnected = false; //改变连接属性

                errorCode = 1;
                errorMsg = string.Format("服务器[{0}]{1}", ComSocket.Socket.RemoteEndPoint, ex.Message);
                if (ClosedEvt != null)
                {
                    System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                    errorCode = 1;
                    errorMsg = string.Format("服务器[{0}]{1}", ComSocket.Socket.RemoteEndPoint, ex.Message);
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SocketClosed
                        target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ClosedEvt(ComSocket,errorCode, errorMsg);
                }
            }
            finally
            {

            }
            return rt;
        }
        #endregion

        #region Socket关闭
        public bool Close()
        {
            bool rt = false;
            int errorCode = 0; string errorMsg = "关闭成功";
            try
            {
                //若Socket已经打开，则关闭
                if (this.ComSocket.Socket != null && ComSocket.Socket.Connected)
                {
                    ComSocket.Socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                    System.Threading.Thread.Sleep(100);
                    ComSocket.Socket.Close();
                }
                rt = true;
            }
            catch (Exception ex)
            {
                errorCode = 1;
                errorMsg = string.Format("服务器[{0}]{1}", ComSocket.Socket.RemoteEndPoint, ex.Message);
                if (ClosedEvt != null)
                {
                    System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                    errorCode = 1;
                    errorMsg = string.Format("服务器[{0}]{1}", ComSocket.Socket.RemoteEndPoint, errorMsg);
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SocketClosed
                        target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ClosedEvt(ComSocket,errorCode, errorMsg);
                }
                ComSocket.Socket.Dispose();
                ComSocket.Socket = null;
            }
            finally { }
            return rt;
        }
        #endregion
    }

    /// <summary>
    /// Socket同步调用函数接口
    /// [注:待完善_2020-01-15]
    /// </summary>
    public class SocketSyncAPIHandle
    {
        public SocketSyncAPIHandle(ProCommon.Communal.ComWrappedSocket comSocket)
        {
            ReceivedData = new StringBuilder();
            _dataBuffer = new byte[1024 * 1024 * 10]; //预设接收字节10MB
            ComSocket = comSocket;
            ConnectedEvt = new ComWrappedSocketConnectedDel(OnConnected);
            ReceivedEvt = new ComWrappedSocketReceivedDel(OnReceived);
            ClosedEvt = new ComWrappedSocketClosedDel(OnClosed);
        }

        #region ComSocket的事件回调函数
        void OnClosed(ProCommon.Communal.ComWrappedSocket comSokect, int iErrorCode, string strErrorMsg)
        {

        }

        void OnReceived(ProCommon.Communal.ComWrappedSocket comSokect, string msgStr, byte[] msgByte)
        {

        }

        void OnConnected(ProCommon.Communal.ComWrappedSocket comSokect, int iErrorCode, string strErrorMsg)
        {

        }

        #endregion

        public event ComWrappedSocketConnectedDel ConnectedEvt;
        public event ComWrappedSocketReceivedDel ReceivedEvt;
        public event ComWrappedSocketClosedDel ClosedEvt;

        public ProCommon.Communal.ComWrappedSocket ComSocket { private set; get; }
        public System.Text.StringBuilder ReceivedData
        {
            private set;
            get;
        }

        /// <summary>
        /// 接收字节缓存
        /// </summary>
        private byte[] _dataBuffer;

        #region Socket连接
        public bool Connect()
        {
            bool rt = false;
            try
            {
                //若Socket已经打开，则关闭
                if (ComSocket.Socket != null && ComSocket.Socket.Connected)
                {
                    ComSocket.Socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                    System.Threading.Thread.Sleep(100);
                    ComSocket.Socket.Close();
                }

                //创建Socket对象(此处以TCP/IP协议创建对象,其他协议下创建对象的方法待完善)
                ComSocket.Socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork,
                    System.Net.Sockets.SocketType.Stream, ComSocket.ProtocolType);
                //定义服务器连接套接字
                System.Net.IPEndPoint iPEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(this.ComSocket.IP), this.ComSocket.Port);

                //非阻止模式连接到服务器端
                ComSocket.Socket.Blocking = false;
                System.AsyncCallback OnConnected = new System.AsyncCallback(ConnectedCallBack);

                //同步请求连接到服务器端
                ComSocket.Socket.Connect(iPEndPoint);
                rt = true;
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
            }
            finally
            {
            }
            return rt;
        }

        private void ConnectedCallBack(IAsyncResult ar)
        {
            int errorCode = 0;
            string errorMsg = "连接成功";
            System.Net.Sockets.Socket socket = (System.Net.Sockets.Socket)ar.AsyncState;

            // Check if socket was connected
            try
            {
                if (socket != null && socket.Connected)
                {
                    //结束异步连接请求（避免内存泄漏)
                    socket.EndConnect(ar);

                    errorMsg = string.Format("服务器:[{0}]{1}", socket.RemoteEndPoint.ToString(), errorMsg);
                    this.ComSocket.IsConnected = true; //改变连接属性
                }
                else
                {
                    errorCode = 1; errorMsg = "服务器:[" + this.ComSocket.IP + "]未连接";
                    ComSocket.IsConnected = false; //改变连接属性
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                errorCode = 1; errorMsg = "服务器:[" + this.ComSocket.IP + "]连接失败";
            }
            finally
            {
                if (socket != null && ConnectedEvt != null)
                {
                    System.Windows.Forms.Control target = ConnectedEvt.Target as System.Windows.Forms.Control;
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SocketConnected
                        target.Invoke(ConnectedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ConnectedEvt(ComSocket,errorCode, errorMsg);
                }
            }
        }
        #endregion

        #region Socket接收
        public bool Receive()
        {
            bool rt = false;
            try
            {
                //若Socket已经打开
                if (ComSocket.Socket != null && ComSocket.Socket.Connected)
                {
                    ComSocket.Socket.Blocking = false;
                    AsyncCallback OnReceived = new AsyncCallback(ReceivedCallBack);
                    //异步请求接收服务器数据，完成后回调：OnReceived委托
                    ComSocket.Socket.BeginReceive(_dataBuffer, 0, _dataBuffer.Length, System.Net.Sockets.SocketFlags.None, OnReceived, ComSocket.Socket);
                    rt = true;
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
            }
            finally
            {
            }
            return rt;
        }

        private void ReceivedCallBack(IAsyncResult ar)
        {
            int errorCode = 0;
            string errorMsg = "接收成功";
            System.Net.Sockets.Socket socket = (System.Net.Sockets.Socket)ar.AsyncState;

            // Check if socket was connected           
            if (socket != null && socket.Connected)
            {
                try
                {
                    //结束异步接收请求（避免内存泄漏)
                    int nBytesRec = socket.EndReceive(ar);

                    #region 接收到数据
                    if (nBytesRec > 0)
                    {
                        errorMsg = string.Format("服务器:[{0}]{1}", socket.RemoteEndPoint, errorMsg);

                        ReceivedData.Clear();
                        string strtemp = Encoding.Default.GetString(_dataBuffer, 0, nBytesRec);
                        ReceivedData.Append(strtemp);

                        if (ReceivedEvt != null)
                        {
                            System.Windows.Forms.Control target = ReceivedEvt.Target as System.Windows.Forms.Control;
                            if (target != null && target.InvokeRequired)
                                //非创建控件线程同步调用事件：SocketConnected
                                target.Invoke(ReceivedEvt, new object[] { ReceivedData.ToString(),
                                    ProCommon.Communal.Functions.GetSubData(_dataBuffer, 0, nBytesRec) });
                            else
                                //创建控件线程调用事件
                                ReceivedEvt(ComSocket, ReceivedData.ToString(),
                                    ProCommon.Communal.Functions.GetSubData(_dataBuffer, 0, nBytesRec));
                        }

                    }
                    else
                    {
                        errorCode = 1; errorMsg = "接收数据为空";
                        //Common.SocketClosedEventHandler d=new Common.SocketClosedEventHandler();
                        if (ClosedEvt != null)
                        {
                            System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                            if (target != null && target.InvokeRequired)
                                //非创建控件线程同步调用事件：SocketClosed
                                target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                            else
                                //创建控件线程调用事件
                                ClosedEvt(ComSocket, errorCode, errorMsg);
                        }
                    }
                    #endregion
                }
                catch
                {
                    try
                    {
                        if (ClosedEvt != null)
                        {
                            System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                            if (target != null && target.InvokeRequired)
                                //非创建控件线程同步调用事件：SocketClosed
                                target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                            else
                                //创建控件线程调用事件
                                ClosedEvt(ComSocket, errorCode, errorMsg);
                        }
                    }
                    catch { }
                }
            }
            else
            {
                ComSocket.IsConnected = false; //改变连接属性
            }
        }

        #endregion

        #region Socket发送
        public bool Send(string strcmd)
        {
            bool rt = false;
            int errorCode = 0; string errorMsg = "发送成功";
            try
            {
                if ((ComSocket.Socket == null) || (!ComSocket.Socket.Connected))
                {
                    errorCode = 1;
                    if (ComSocket.Socket == null)
                    {
                        errorMsg = "Socket对象为空";
                        errorMsg = string.Format("服务器:[{0}]{1}", ComSocket.Socket.RemoteEndPoint, errorMsg);
                    }
                    if (!this.ComSocket.Socket.Connected)
                    {
                        errorMsg = "Socket对象未连接";
                        errorMsg = string.Format("服务器:[{0}]{1}", ComSocket.IP + ":" + ComSocket.Port, errorMsg);
                    }
                    return rt;
                }

                Byte[] byteDateLine = Encoding.Default.GetBytes(strcmd);
                int rint = ComSocket.Socket.Send(byteDateLine, byteDateLine.Length, System.Net.Sockets.SocketFlags.None);
                rt = true;
            }
            catch (Exception ex)
            {
                if (ClosedEvt != null)
                {
                    System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                    errorCode = 1;
                    errorMsg = string.Format("服务器[{0}]{1}", ComSocket.Socket.RemoteEndPoint, ex.Message);
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SocketClosed
                        target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ClosedEvt(ComSocket, errorCode, errorMsg);
                }
            }
            finally
            {
            }
            return rt;
        }

        public bool Send(byte[] data)
        {
            bool rt = false;
            int errorCode = 0; string errorMsg = "发送成功";

            try
            {

                if ((ComSocket.Socket == null) || (!ComSocket.Socket.Connected))
                {
                    errorCode = 1;
                    if (this.ComSocket.Socket == null)
                    {
                        errorMsg = "Socket对象为空";
                        errorMsg = string.Format("服务器:[{0}]{1}", ComSocket.Socket.RemoteEndPoint, errorMsg);
                    }
                    if (!this.ComSocket.Socket.Connected)
                    {
                        errorMsg = "Socket对象未连接";
                        errorMsg = string.Format("服务器:[{0}]{1}", ComSocket.IP + ":" + ComSocket.Port, errorMsg);
                    }
                    return rt;
                }

                // Convert to byte array and send.                
                int rint = ComSocket.Socket.Send(data, data.Length, System.Net.Sockets.SocketFlags.None);
                rt = true;
            }
            catch (Exception ex)
            {
                errorCode = 1;
                errorMsg = string.Format("服务器[{0}]{1}", ComSocket.Socket.RemoteEndPoint, ex.Message);

                if (ClosedEvt != null)
                {
                    System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                    errorCode = 1;
                    errorMsg = string.Format("服务器[{0}]{1}", ComSocket.Socket.RemoteEndPoint, ex.Message);
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SocketClosed
                        target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ClosedEvt(ComSocket, errorCode, errorMsg);
                }
            }
            finally
            {

            }
            return rt;
        }
        #endregion

        #region Socket关闭
        public bool Close()
        {
            bool rt = false;
            int errorCode = 0; string errorMsg = "关闭成功";
            try
            {
                //若Socket已经打开，则关闭
                if (this.ComSocket.Socket != null && ComSocket.Socket.Connected)
                {
                    ComSocket.Socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                    System.Threading.Thread.Sleep(100);
                    ComSocket.Socket.Close();
                }
                rt = true;
            }
            catch (Exception ex)
            {
                errorCode = 1;
                errorMsg = string.Format("服务器[{0}]{1}", ComSocket.Socket.RemoteEndPoint, ex.Message);
            }
            finally
            {
                if (ClosedEvt != null)
                {
                    System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                    errorCode = 1;
                    errorMsg = string.Format("服务器[{0}]{1}", ComSocket.Socket.RemoteEndPoint, errorMsg);
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SocketClosed
                        target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ClosedEvt(ComSocket, errorCode, errorMsg);
                }
            }
            return rt;
        }
        #endregion
    }

    #endregion

    #region SerialPort操作接口函数

    //接收数据委托（字符串型 接收数据,字节数组型 接收数据）
    public delegate void ComWrappedSerialPortReceivedDel(ProCommon.Communal.ComWrappedSerialPort comSerialPort, string msgStr, byte[] msgByte);

    //连接委托（整型 错误代码，字符串型 错误信息）
    public delegate void ComWrappedSerialPortConnectedDel(ProCommon.Communal.ComWrappedSerialPort comSerialPort, int iErrorCode, string strErrorMsg);

    //关闭委托（整型 错误代码，字符串型 错误信息）  
    public delegate void ComWrappedSerialPortClosedDel(ProCommon.Communal.ComWrappedSerialPort comSerialPort, int iErrorCode, string strErrorMsg);

    /// <summary>
    /// SerialPort异步操作接口函数
    /// [注:待完善_2020-03-03]
    /// </summary>
    public class SerialAsyncAPIHandle
    {
        public SerialAsyncAPIHandle(ProCommon.Communal.ComWrappedSerialPort comWrappedserialPort)
        {
            ReceivedData = new StringBuilder();
            ComSerialPort = comWrappedserialPort;
            ConnectedEvt = new ComWrappedSerialPortConnectedDel(OnConnected);
            ReceivedEvt = new ComWrappedSerialPortReceivedDel(OnReceived);
            ClosedEvt = new ComWrappedSerialPortClosedDel(OnClosed);
        }

        #region ComSerialPort的事件回调函数
        void OnClosed(ProCommon.Communal.ComWrappedSerialPort comSerialPort, int iErrorCode, string strErrorMsg)
        {

        }

        void OnReceived(ProCommon.Communal.ComWrappedSerialPort comSerialPort, string msgStr, byte[] msgByte)
        {

        }

        void OnConnected(ProCommon.Communal.ComWrappedSerialPort comSerialPort, int iErrorCode, string strErrorMsg)
        {

        }

        #endregion

        public event ComWrappedSerialPortConnectedDel ConnectedEvt;
        public event ComWrappedSerialPortReceivedDel ReceivedEvt;
        public event ComWrappedSerialPortClosedDel ClosedEvt;

        public ProCommon.Communal.ComWrappedSerialPort ComSerialPort { private set; get; }
        public System.Text.StringBuilder ReceivedData
        {
            private set;
            get;
        }
    }

    /// <summary>
    /// SerialPort同步操作接口函数  
    /// </summary>
    public class SerialSyncAPIHandle
    {
        public SerialSyncAPIHandle(ProCommon.Communal.ComWrappedSerialPort comWrappedserialPort)
        {
            ReceivedData = new StringBuilder();
            ComSerialPort = comWrappedserialPort;
            ConnectedEvt = new ComWrappedSerialPortConnectedDel(OnConnected);
            ReceivedEvt = new ComWrappedSerialPortReceivedDel(OnReceived);
            ClosedEvt = new ComWrappedSerialPortClosedDel(OnClosed);
        }

        #region ComSerialPort的事件回调函数
        void OnClosed(ProCommon.Communal.ComWrappedSerialPort comSerialPort,int iErrorCode, string strErrorMsg)
        {

        }

        void OnReceived(ProCommon.Communal.ComWrappedSerialPort comSerialPort,string msgStr, byte[] msgByte)
        {

        }

        void OnConnected(ProCommon.Communal.ComWrappedSerialPort comSerialPort,int iErrorCode, string strErrorMsg)
        {

        }

        #endregion

        public event ComWrappedSerialPortConnectedDel ConnectedEvt;
        public event ComWrappedSerialPortReceivedDel ReceivedEvt;
        public event ComWrappedSerialPortClosedDel ClosedEvt;

        public ProCommon.Communal.ComWrappedSerialPort ComSerialPort { private set; get; }

        private System.IO.Ports.SerialPort _iSerialPort { set; get; }

        public System.Text.StringBuilder ReceivedData
        {
            private set;
            get;
        }

        public string[] SerialPortNameList { private set; get; }

        public bool EnumerateSerialPortList()
        {
            bool rt = false;
            try
            {
                SerialPortNameList= System.IO.Ports.SerialPort.GetPortNames();
                if(SerialPortNameList!=null
                    && SerialPortNameList.Length>0)
                    rt = true;
            }catch(System.Exception ex)
            {

            }
            return rt;
        }

        public bool GetSerialPortByName(string protName)
        {
            bool rt = false;
            try
            {
                if (SerialPortNameList != null
                    && SerialPortNameList.Length > 0)
                {
                    for (int i = 0; i < SerialPortNameList.Length; i++)
                    {
                        if(protName== SerialPortNameList[i])
                        {
                            _iSerialPort = new System.IO.Ports.SerialPort(protName);
                            rt = true;break;
                        }
                    }
                }

                if (!rt)
                {
                    if (_iSerialPort!=null)
                    {
                        if(_iSerialPort.IsOpen)
                            _iSerialPort.Close();

                        _iSerialPort.Dispose();
                        _iSerialPort = null;
                    }
                }                  
            }
            catch (System.Exception ex)
            {

            }
            return rt;
        }

        #region SerialPort连接
        public bool Connect()
        {
            bool rt = false;           
            try
            {
                if (_iSerialPort != null)
                {
                    ComSerialPort.SerialPort = _iSerialPort;
                    if (ComSerialPort.SerialPort.IsOpen)
                    {
                        ComSerialPort.SerialPort.DataReceived -= SerialPort_DataReceived;
                        ComSerialPort.SerialPort.Close();
                        System.Threading.Thread.Sleep(100);
                        ComSerialPort.SerialPort.Dispose();
                    } 
                                      
                    ComSerialPort.SerialPort.BaudRate = ComSerialPort.BaudRate;
                    ComSerialPort.SerialPort.Parity = ComSerialPort.Parity;
                    ComSerialPort.SerialPort.DataBits = ComSerialPort.DataBits;
                    ComSerialPort.SerialPort.StopBits = ComSerialPort.StopBits;
                    //ComSerialPort.SerialPort.ReceivedBytesThreshold = ComSerialPort.ReceivedBytesThreshold;
                    ComSerialPort.SerialPort.NewLine = ComSerialPort.NewLine;
                    ComSerialPort.SerialPort.ReadTimeout = ComSerialPort.ReceiveTimeOut;
                    ComSerialPort.SerialPort.DtrEnable = ComSerialPort.DtrEnable;
                    ComSerialPort.SerialPort.RtsEnable = ComSerialPort.RtsEnable;
                    ComSerialPort.SerialPort.DataReceived += SerialPort_DataReceived;
                    System.AsyncCallback OnConnected = new System.AsyncCallback(ConnectedCallBack);

                    //当完成连接后回调:OnConnected委托              
                    ComSerialPort.SerialPort.Open();
                    OnConnected(null);
                    rt = true;
                }
            }
            catch (Exception ex){ }
            finally { }
            return rt;
        }
        private void ConnectedCallBack(IAsyncResult ar)
        {
            int errorCode = 0;
            string errorMsg = "连接成功";

            // Check if SerialPort was connected
            try
            {
                if (ComSerialPort.SerialPort != null && ComSerialPort.SerialPort.IsOpen)
                {
                    errorMsg = string.Format("服务器:[{0}]{1}", ComSerialPort.SerialPort.PortName, errorMsg);
                    ComSerialPort.IsConnected = true; //改变连接属性
                }
                else
                {
                    errorCode = 1; errorMsg = "服务器:[" + ComSerialPort.SerialPort.PortName + "]未连接";
                    ComSerialPort.IsConnected = false; //改变连接属性
                }
            }
            catch (Exception ex)
            {
                errorCode = 1; errorMsg = "服务器:[" + ComSerialPort.SerialPort.PortName + "]连接失败";
                Close();
                ComSerialPort.IsConnected = false;               
            }
            finally
            {
                if (ComSerialPort.SerialPort != null && ConnectedEvt != null)
                {
                    System.Windows.Forms.Control target = ConnectedEvt.Target as System.Windows.Forms.Control;
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SerialPortConnectedEvt
                        target.Invoke(ConnectedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ConnectedEvt(ComSerialPort, errorCode, errorMsg);
                }
            }
        }

        /// <summary>
        /// 串口接收到数据回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            Receive();
        }
        #endregion

        #region SerialPort接收
        public bool Receive()
        {
            bool rt = false;
            try
            {
                if (_iSerialPort != null)
                {
                    ComSerialPort.SerialPort = _iSerialPort;

                }
                if (ComSerialPort.SerialPort.IsOpen)
                {
                     AsyncCallback OnReceived = new AsyncCallback(ReceivedCallBack);
                    //请求接收数据，完成后回调：OnReceived委托
                    OnReceived(null);
                    rt = true;
                }
            }
            catch (Exception ex) {  }
            finally {  }
            return rt;
        }

        private void ReceivedCallBack(IAsyncResult ar)
        {
            int errorCode = 0;
            string errorMsg = "接收成功";
            //若SerialPort已经打开
            if (ComSerialPort.SerialPort != null && ComSerialPort.SerialPort.IsOpen)
            {
                try
                {
                    #region 接收数据

                    int nBytesRec = ComSerialPort.SerialPort.BytesToRead;
                    byte[] _dataBuffer = new byte[nBytesRec];
                    ComSerialPort.SerialPort.Read(_dataBuffer, 0, nBytesRec);

                    if (nBytesRec > 0)
                    {
                        errorMsg = string.Format("串口:[{0}]{1}", ComSerialPort.SerialPort.PortName, errorMsg);

                        ReceivedData.Clear();
                        string strtemp = Encoding.Default.GetString(_dataBuffer, 0, nBytesRec);
                        ReceivedData.Append(strtemp);

                        if (ReceivedEvt != null)
                        {
                            System.Windows.Forms.Control target = ReceivedEvt.Target as System.Windows.Forms.Control;
                            if (target != null && target.InvokeRequired)
                                //非创建控件线程同步调用事件：SerialPortConnected
                                target.Invoke(ReceivedEvt, new object[] { ReceivedData.ToString(),
                                    ProCommon.Communal.Functions.GetSubData(_dataBuffer, 0, nBytesRec) });
                            else
                                //创建控件线程调用事件
                                ReceivedEvt(ComSerialPort, ReceivedData.ToString(),
                                    ProCommon.Communal.Functions.GetSubData(_dataBuffer, 0, nBytesRec));
                        }

                    }
                    else
                    {
                        errorCode = 1; errorMsg = "接收数据为空";
                        //Common.SocketClosedEventHandler d=new Common.SocketClosedEventHandler();
                        if (ClosedEvt != null)
                        {
                            System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                            if (target != null && target.InvokeRequired)
                                //非创建控件线程同步调用事件：SocketClosed
                                target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                            else
                                //创建控件线程调用事件
                                ClosedEvt(ComSerialPort, errorCode, errorMsg);
                        }
                    }
                    #endregion
                }
                catch
                {
                    try
                    {
                        if (ClosedEvt != null)
                        {
                            System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                            if (target != null && target.InvokeRequired)
                                //非创建控件线程同步调用事件：SocketClosed
                                target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                            else
                                //创建控件线程调用事件
                                ClosedEvt(ComSerialPort,errorCode, errorMsg);
                        }
                    }
                    catch { }
                }
            }
            else
            {
                ComSerialPort.IsConnected = false; //改变连接属性
            }
        }

        #endregion

        #region SerialPort发送
        public bool Send(string strcmd)
        {
            bool rt = false;
            int errorCode = 0; string errorMsg = "发送成功";
            try
            {
                if (_iSerialPort != null)
                {
                    ComSerialPort.SerialPort = _iSerialPort;

                }

                if (ComSerialPort.SerialPort == null || (!ComSerialPort.SerialPort.IsOpen))
                {
                    errorCode = 1;
                    if (ComSerialPort.SerialPort == null)
                    {
                        errorMsg = "SerialPort对象为空";
                        errorMsg = string.Format("串口:[{0}]{1}", ComSerialPort.SerialPort.PortName, errorMsg);
                    }
                    if (!ComSerialPort.SerialPort.IsOpen)
                    {
                        errorMsg = "SerialPort对象未连接";
                        errorMsg = string.Format("串口:[{0}]{1}", ComSerialPort.SerialPort.PortName, errorMsg);
                    }
                    return rt;
                }

                // Convert to byte array and send.   
                byte[] cmdBuffer = Encoding.Default.GetBytes(strcmd);
                ComSerialPort.SerialPort.Write(cmdBuffer, 0, cmdBuffer.Length);
                rt = true;
            }
            catch (Exception ex)
            {
                Close();
                ComSerialPort.IsConnected = false;
                if (ClosedEvt != null)
                {
                    System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                    errorCode = 1;
                    errorMsg = string.Format("串口[{0}]{1}", ComSerialPort.SerialPort.PortName, ex.Message);
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SerialPortClosed
                        target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ClosedEvt(ComSerialPort, errorCode, errorMsg);
                }
            }
            finally
            {
            }
            return rt;
        }

        public bool Send(byte[] byteArrCmd)
        {
            bool rt = false;
            int errorCode = 0; string errorMsg = "发送成功";
            try
            {
                if (_iSerialPort != null)
                {
                    ComSerialPort.SerialPort = _iSerialPort;
                    if (!ComSerialPort.SerialPort.IsOpen)
                    {
                        errorCode = 1;
                        if (ComSerialPort.SerialPort == null)
                        {
                            errorMsg = "SerialPort对象为空";
                            errorMsg = string.Format("串口:[{0}]{1}", ComSerialPort.SerialPort.PortName, errorMsg);
                        }
                        if (!ComSerialPort.SerialPort.IsOpen)
                        {
                            errorMsg = "SerialPort对象未连接";
                            errorMsg = string.Format("串口:[{0}]{1}", ComSerialPort.SerialPort.PortName, errorMsg);
                        }
                        return rt;
                    }

                    ComSerialPort.SerialPort.Write(byteArrCmd, 0, byteArrCmd.Length);
                    rt = true;
                }               
            }
            catch (Exception ex)
            {
                Close();
                ComSerialPort.IsConnected = false;
                if (ClosedEvt != null)
                {
                    System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                    errorCode = 1;
                    errorMsg = string.Format("串口[{0}]{1}", ComSerialPort.SerialPort.PortName, ex.Message);
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SerialPortClosed
                        target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ClosedEvt(ComSerialPort, errorCode, errorMsg);
                }
            }
            finally{}
            return rt;
        }
        #endregion

        #region SerialPort关闭
        public bool Close()
        {
            bool rt = false;
            int errorCode = 0; string errorMsg = "关闭成功";
            try
            {
                if (_iSerialPort != null)
                {
                    ComSerialPort.SerialPort = _iSerialPort;
                    if (ComSerialPort.SerialPort.IsOpen)
                    {
                        ComSerialPort.SerialPort.Close();
                        System.Threading.Thread.Sleep(100);
                    }
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                ComSerialPort.IsConnected = false;
                errorMsg = ex.Message;
            }
            finally
            {
                if (ClosedEvt != null)
                {
                    System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                    errorCode = 1;
                    errorMsg = string.Format("串口[{0}]{1}", ComSerialPort.SerialPort.PortName, errorMsg);
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SerialPortClosed
                        target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ClosedEvt(ComSerialPort, errorCode, errorMsg);
                }
                if (ComSerialPort.SerialPort != null)
                {
                    ComSerialPort.SerialPort.Dispose();
                    ComSerialPort.SerialPort = null;
                }
            }
            return rt;
        }
        #endregion

    }

    #endregion

    #region WebService操作接口函数

    /// <summary>
    /// Webservice异步操作接口函数
    /// [注:待完善_2020-01-15]
    /// </summary>
    public class WebAsyncAPIHandle
    {

    }

    /// <summary>
    /// Webservice同步操作接口函数
    /// [注:待完善_2020-01-15]
    /// </summary>
    public class WebSyncAPIHandle
    {

    }

    #endregion
  
}
