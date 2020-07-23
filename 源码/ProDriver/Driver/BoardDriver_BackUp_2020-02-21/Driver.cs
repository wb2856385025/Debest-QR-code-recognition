using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*************************************************************************************
    * CLR    Version：       4.0.30319.42000
    * Class     Name：       Driver
    * Machine   Name：       LAPTOP-KFCLDVVH
    * Name     Space：       ProDriver
    * File      Name：       Driver
    * Creating  Time：       4/29/2019 11:06:18 AM
    * Author    Name：       xYz_Albert
    * Description   ：       驱动封装类
    * Modifying Time：
    * Modifier  Name：
*************************************************************************************/

namespace ProDriver.Driver
{
    public delegate void DriverExceptionOccuredDel(string err);

    public delegate void CameraImageGrabbedDel(ProCommon.Communal.Camera cam, HalconDotNet.HObject hoImage);

    #region 相机相关
   
    /// <summary>
    /// 相机操作接口
    /// </summary>
    public interface ICamDriver
    {
        event CameraImageGrabbedDel CameraImageGrabbedEvt;
        bool IsImageGrabbed { set; get; }
        bool EnumerateCameraList();
        int GetCameraListCount();
        bool GetCameraByIdx(int index);
        string GetCameraSN(int index);
        bool GetCameraByName(string camName);
        bool GetCameraBySN(string camSN);       
        bool Open();
        bool Close();
        bool SetAcquisitionMode(ProCommon.Communal.AcquisitionMode acqmode, uint frameNum); 
        bool SetTriggerActivation(ProCommon.Communal.TriggerLogic edge);
        bool StartGrab();
        bool PauseGrab();
        bool StopGrab();
        bool SoftTriggerOnce();
        bool SetExposureTime(float exposuretime);
        bool SetGain(float gain);
        bool SetFrameRate(float fps);
        bool SetTriggerDelay(float trigdelay);
        bool SetOutPut(bool onOff);
        bool CreateCameraSetPage(System.IntPtr windowHandle, string promption);
        bool ShowCameraSetPage();
        bool RegisterExceptionCallBack();
        bool RegisterImageGrabbedCallBack();
    }

    /// <summary>
    /// 相机操作类
    /// </summary>
    public abstract class CamDriver : ICamDriver
    {
        public abstract event CameraImageGrabbedDel CameraImageGrabbedEvt;
        public DriverExceptionOccuredDel DriverExceptionDel;
        public HalconDotNet.HObject HoImage;     
        public CamDriver()
        {
            HoImage = new HalconDotNet.HObject();          
            DriverExceptionDel = new DriverExceptionOccuredDel(OnDriverExceptionOccured);          
        }

        private void OnDriverExceptionOccured(string err)
        {
            //什么都不做
        }

        #region 抽象成员(钩子函数)
        public abstract bool DoEnumerateCameraList();       
        public abstract int DoGetCameraListCount();
        public abstract bool DoGetCameraByIdx(int index);
        public abstract string DoGetCameraSN(int index);
        public abstract bool DoGetCameraByName(string camName);
        public abstract bool DoGetCameraBySN(string camSN);
        public abstract bool DoOpen();
        public abstract bool DoClose();
        public abstract bool DoSetAcquisitionMode(ProCommon.Communal.AcquisitionMode acqmode, uint frameNum); 
        public abstract bool DoSetTriggerActivation(ProCommon.Communal.TriggerLogic edge);
        public abstract bool DoStartGrab();
        public abstract bool DoPauseGrab();
        public abstract bool DoStopGrab();
        public abstract bool DoSoftTriggerOnce();
        public abstract bool DoSetExposureTime(float exposuretime);
        public abstract bool DoSetGain(float gain);
        public abstract bool DoSetFrameRate(float fps);
        public abstract bool DoSetTriggerDelay(float trigdelay);
        public abstract bool DoSetOutPut(bool onOff);
        public abstract bool DoCreateCameraSetPage(System.IntPtr windowHandle, string promption);
        public abstract bool DoShowCameraSetPage();
        public abstract bool DoRegisterExceptionCallBack();
        public abstract bool DoRegisterImageGrabbedCallBack();
        #endregion

        #region 实现接口

        public bool IsImageGrabbed { set; get; }

        public bool EnumerateCameraList()
        {
            return DoEnumerateCameraList();
        }

        public int GetCameraListCount()
        {
            return  DoGetCameraListCount();
        }

        public bool GetCameraByIdx(int index)
        {
            return DoGetCameraByIdx(index);
        }

        public string GetCameraSN(int index)
        {
            return DoGetCameraSN(index);
        }

        public bool GetCameraByName(string camName)
        {
            return DoGetCameraByName(camName);
        }

        public bool GetCameraBySN(string camSN)
        {
            return DoGetCameraBySN(camSN);
        }

        public bool Open()
        {
            return DoOpen();
        }

        public bool Close()
        {
            return DoClose();
        }

        public bool SetAcquisitionMode(ProCommon.Communal.AcquisitionMode acqmode, uint frameNum)
        {
            return DoSetAcquisitionMode(acqmode,frameNum);
        }  
        public bool SetTriggerActivation(ProCommon.Communal.TriggerLogic edge)
        {
            return DoSetTriggerActivation(edge);
        }

        public bool StartGrab()
        {
            return DoStartGrab();
        }

        public bool PauseGrab()
        {
            return DoPauseGrab();
        }

        public bool StopGrab()
        {
            return DoStopGrab();
        }

        public bool SoftTriggerOnce()
        {
            return DoSoftTriggerOnce();
        }

        public bool SetExposureTime(float exposuretime)
        {
            return DoSetExposureTime(exposuretime);
        }

        public bool SetGain(float gain)
        {
            return DoSetGain(gain);
        }

        public bool SetFrameRate(float fps)
        {
            return DoSetFrameRate(fps);
        }

        public bool SetTriggerDelay(float trigdelay)
        {
            return DoSetTriggerDelay(trigdelay);
        }

        public bool SetOutPut(bool onOff)
        {
            return DoSetOutPut(onOff);
        }

        public bool CreateCameraSetPage(System.IntPtr windowHandle, string promption)
        {
            return DoCreateCameraSetPage(windowHandle, promption);
        }

        public bool ShowCameraSetPage()
        {
            return DoShowCameraSetPage();
        }

        public bool RegisterExceptionCallBack()
        {
            return DoRegisterExceptionCallBack();
        }

        public bool RegisterImageGrabbedCallBack()
        {
            return DoRegisterImageGrabbedCallBack();
        }

        #endregion

        #region 覆写并抽象化Object类的ToString()
        public abstract override string ToString();
        #endregion

        public ProCommon.Communal.Camera Camera
        {
            set;
            get;
        }

    }

    #endregion

    #region 控制器相关

    /// <summary>
    /// 极限位
    /// </summary>
    public enum LimitType : uint
    {
        NegaLimit = 0,
        FwdLimit = 1
    }

    /// <summary>
    /// 正运动板卡轴外部信号
    /// </summary>
    public enum ZMotionAxisStatus : uint
    {
        NOERROR=0,                               //无异常(隐藏定义)
        FOLLOWUPERROR_EXTENDALM = 2,             //随动误差超限报警
        REMOTEAXISCOMMU_ERROR = 4,               //远程轴通信错误
        REMOTEDRIVER_ERROR = 8,                  //远程驱动器错误
        HARD_FWDLIMIT = 16,                      //正向硬限位
        HARD_NEGALIMIT = 32,                     //反向硬限位
        HARD_DATUMING = 64,                      //找原点中
        HOLDSPEED_KEEPIN = 128,                  //HOLD速度保持信号输入
        FOLLOWUPERROR_EXTENDERR = 256,           //随动误差超限错误
        SOFT_EXTENDFWDLIMIT = 512,               //超出正向软限位
        SOFT_EXTENDNEGLIMIT = 1024,              //超出反向软限位
        CANCEL = 2048,                           //执行Cancel
        EXTENDMAX_SPEED = 4096,                  //脉冲频率超限
        MACHANICALROBOT_COORDINATEERR = 16384,   //机械手坐标错误
        POWER_ERR = 262144,                      //电源异常
        ALARM_IN = 4194304,                      //告警信号输入
        AXIS_PAUSE = 8388608                     //轴进入暂停状态
    }

    /// <summary>
    /// 雷泰板卡轴运动状态
    /// </summary>
    public enum LeadShineAxisStatus : uint
    {
        Reserved0 = 0,
        Reserved1 = 1,
        Reserved2 = 2,
        Reserved3 = 3,
        Reserved4 = 4,
        Reserved5 = 5,
        Reserved6 = 6,
        Reserved7 = 7,
        FU = 8,         //正在加速
        FD = 9,         //正在减速
        FC = 10,        //正在低速运动
        ALM = 11,       //告警信号输入
        PEL = 12,       //正向硬限位信号输入
        MEL = 13,       //负向硬限位信号输入
        ORG = 14,       //原点硬限位信号输入
        SD = 15         //减速硬限位信号输入
    }

    /// <summary>
    /// 雷泰板卡轴外部信号
    /// </summary>
    public enum LeadShineAxisExStatus : uint
    {
        Reserved0 = 0,
        Reserved1 = 1,
        Reserved2 = 2,
        Reserved3 = 3,
        CSD = 4,         //同时减速信号,1=ON,0=OFF
        STA = 5,         //同时启动信号,1=ON,0=OFF
        STP = 6,         //同时停止信号,1=ON,0=OFF
        EMG = 7,         //紧急停止信号,1=ON,0=OFF
        PCS = 8,         //位置改变信号,1=ON,0=OFF
        ERC = 9,         //误差清除信号,1=ON,0=OFF
        EZ = 10,         //索引信号,1=ON,0=OFF
        DRPA = 11,       //+DR(PA)信号,1=ON,0=OFF
        DRPB = 12,       //-DR(PB)信号,1=ON,0=OFF
        Reserved13 = 13,
        SD = 14,         //减速信号,1=ON,0=OFF
        INP = 15,        //轴到位信号,1=ON,0=OFF
        DIR = 16,        //脉冲输出方向,1=负方向,0=正方向
        Reserved17 = 17,
        Reserved18 = 18,
        Reserved19 = 19,
        Reserved20 = 20,
        Reserved21 = 21,
        Reserved22 = 22,
        Reserved23 = 23,
        Reserved24 = 24,
        Reserved25 = 25,
        Reserved26 = 26,
        Reserved27 = 27,
        Reserved28 = 28,
        Reserved29 = 29,
        Reserved30 = 30,
        Reserved31 = 31
    }

    /// <summary>
    /// 板卡操作接口
    /// </summary>
    public interface IBoardDriver
    {

        /// <summary>
        /// 启连控制器
        /// [注:对于EtherNet控制器会用得到]
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        bool ConnectCtrller(string ip,int port=8089);

        /// <summary>
        /// 断连控制器
        /// </summary>
        /// <returns></returns>
        bool DisconnectCtrller();

        /// <summary>
        /// 初始化控制器资源
        /// [注:对于PCI,PCIE插槽的控制器会用得到]
        /// </summary>
        /// <returns></returns>
        bool InitCtrllerSys();

        /// <summary>
        /// 设置主轴及联动轴列表
        /// [注:多轴联动插补时]
        /// </summary>
        /// <param name="axisNum"></param>
        /// <param name="piAxislist"></param>
        /// <returns></returns>
        bool SetBaseAxes(int axisNum, int[] piAxislist);

        /// <summary>
        /// 设置轴类型
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool SetAxisType(int naxis, int type);

        /// <summary>
        /// 设置指定轴的脉冲输出模式
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool SetAxisPulseOutMode(int naxis, int mode);

        /// <summary>
        /// 设置指定轴的限位类型
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool SetAxisLimitMode(int naxis, int mode);

        /// <summary>
        /// 设置指定轴的软负限位值
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="fvalue"></param>
        /// <returns></returns>
        bool SetAxisSRevValue(int naxis, float fvalue);

        /// <summary>
        /// 设置指定轴软正限位值
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="fvalue"></param>
        /// <returns></returns>
        bool SetAxisSFwdValue(int naxis, float fvalue);

        /// <summary>
        /// 设置指定轴的脉冲当量
        /// [注:这里的描述为运行单位用户单位时的脉冲数;
        /// 例如运行单位mm需要的脉冲数]
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        bool SetAxisUnits(int naxis, float units);

        bool SetAxisCreep(int naxis, float creep);

        /// <summary>
        /// 设置指定轴T型曲线运动参数
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="
        /// "></param>
        /// <param name="maxspeed"></param>
        /// <param name="tacc"></param>
        /// <param name="tdec"></param>
        /// <returns></returns>
        bool SetAxisTrapeziumPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec);

        /// <summary>
        /// 设置指定轴S型曲线运动参数
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="lspeed"></param>
        /// <param name="maxspeed"></param>
        /// <param name="tacc"></param>
        /// <param name="tdec"></param>
        /// <param name="sacc"></param>
        /// <param name="sdec"></param>
        /// <returns></returns>
        bool SetAxisSigmoidPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec, int sacc, int sdec);

        /// <summary>
        /// 设置指定端口模拟量
        /// </summary>
        /// <param name="ionum"></param>
        /// <param name="fValue"></param>
        /// <returns></returns>
        bool SetDA(int ionum, float fValue);

        //专用信号口设置函数

        /// <summary>
        /// 设置指定轴的轴减速信号有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="enable"></param>
        /// <param name="sdlevel"></param>
        /// <param name="sdmode"></param>
        /// <returns></returns>
        bool SetAxisSDEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel sdlevel, uint sdmode);

        /// <summary>
        /// 设置指定轴位置改变信号有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="enable"></param>
        /// <param name="pcslevel"></param>
        /// <returns></returns>
        bool SetAxisPCSEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel pcslevel);

        /// <summary>
        /// 设置指定轴位置到达信号的有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="enable"></param>
        /// <param name="inplevel"></param>
        /// <returns></returns>
        bool SetAxisINPEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel inplevel);

        /// <summary>
        /// 指定轴的误差清除信号有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="enable"></param>
        /// <param name="erclevel"></param>
        /// <param name="ercwidth"></param>
        /// <param name="ercofftime"></param>
        /// <returns></returns>
        bool SetAxisERCEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel erclevel, uint ercwidth, uint ercofftime);

        /// <summary>
        /// 设置指定轴的ERC信号状态
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="sel"></param>
        /// <returns></returns>
        bool SetAxisERC(int naxis, uint sel);

        /// <summary>
        /// 设置指定轴的报警信号有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="almlevel"></param>
        /// <param name="actionmode"></param>
        /// <returns></returns>
        bool SetAxisALMEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel almlevel, uint actionmode);

        /// <summary>
        /// 设置指定轴编码器复位信号有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="ezlevel"></param>
        /// <param name="actionmode"></param>
        /// <returns></returns>
        bool SetAxisEZEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ezlevel, uint actionmode);

        /// <summary>
        /// 设置指定轴锁存器的有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="ltclevel"></param>
        /// <param name="actionmode"></param>
        /// <returns></returns>
        bool SetAxisLTCEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ltclevel, uint actionmode);

        bool SetAxisELEffectiveLevel(int naxis, uint actionmode);

        bool SetAxisDatumEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel datumlevel, uint filter);

        bool SetAxisServo(int naxis, bool onoff);

        bool SetAxisEMGEffectiveLevel(uint enable, ProCommon.Communal.ElectricalLevel emglevel);

        /// <summary>
        /// 设置指定轴的报警信号输入端口位及有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        bool SetAxisALMIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level);

        /// <summary>
        /// 获取指定轴的报警信号输入端口位
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <returns></returns>
        bool GetAxisALMIn(int naxis, ref int inputno);

        /// <summary>
        /// 设置指定轴的硬负限位信号端口位及有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        bool SetAxisHRevIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level);

        /// <summary>
        /// 获取指定轴的硬负限位信号端口位
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <returns></returns>
        bool GetAxisHRevIn(int naxis, ref int inputno);

        /// <summary>
        /// 设置指定轴的硬原点信号端口位及有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        bool SetAxisDatumIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level);

        /// <summary>
        /// 获取指定轴的硬原点限位信号端口位
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <returns></returns>
        bool GetAxisDatumIn(int naxis, ref int inputno);

        /// <summary>
        /// 设置指定轴的硬正限位信号端口位及有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        bool SetAxisHFwdIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level);

        /// <summary>
        /// 获取指定轴的硬正限位信号端口位
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <returns></returns>
        bool GetAxisHFwdIn(int naxis, ref int inputno);

        /// <summary>
        /// 设置指定端口位的有效逻辑电平
        /// </summary>
        /// <param name="inputno"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        bool SetPortInEffectiveLevel(int inputno, ProCommon.Communal.ElectricalLevel level);

        /// <summary>
        /// 指定轴单轴找原点
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="moveDir"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool FindDatum(int naxis, ProCommon.Communal.MoveDir moveDir, int mode);

        /// <summary>
        /// 指定轴单轴连续运动
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="movedir"></param>
        /// <returns></returns>
        bool SingleContinueMove(int naxis, ProCommon.Communal.MoveDir movedir);

        /// <summary>
        /// 指定轴单轴相对运动
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="pos"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        bool SingleRelMove(int naxis, float pos, float speed = 0.0f);

        /// <summary>
        /// 指定轴单轴绝对运动
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        bool SingleAbsMove(int naxis, float pos,float speed=0.0f);

        /// <summary>
        /// 轴列表运动按指定模式取消
        /// </summary>
        /// <param name="axes"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool CancelAxisList(int[] axes,int mode);

        /// <summary>
        /// 指定轴单轴停止运动
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool SingleCancelMove(int naxis, int mode);

        /// <summary>
        /// 设置指定轴的当前位置
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="curpos"></param>
        /// <returns></returns>
        bool SetAxisCurPos(int naxis, float curpos);

        /// <summary>
        /// 获取指定轴的当前位置
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="curpos"></param>
        /// <returns></returns>
        bool GetAxisCurPos(int naxis, ref float curpos);

        /// <summary>
        /// 获取指定轴的当前速度
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="curspeed"></param>
        /// <returns></returns>
        bool GetAxisCurspeed(int naxis, ref float curspeed);

        /// <summary>
        /// 检测指定轴是否停止
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="stopped"></param>
        /// <returns></returns>
        bool ChekcAxisIfStop(int naxis, ref bool stopped);

        /// <summary>
        /// 获取指定轴的轴状态
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="axisStatus"></param>
        /// <returns></returns>
        bool GetAxisStatus(int naxis, ref int axisStatus);

        /// <summary>
        /// 检测指定轴是否正常
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="normal"></param>
        /// <returns></returns>
        bool CheckAxisIfNormal(int naxis, ref bool normal);

        /// <summary>
        /// 立即停止(轴列表指定的所有轴)
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool RapidStop(int mode);

        /// <summary>
        /// 两轴直线插补运动
        /// </summary>
        /// <param name="axespos"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool Line2Move(float[] axespos, int mode);

        /// <summary>
        /// 基于三点的两轴圆弧插补运动
        /// </summary>
        /// <param name="midpos"></param>
        /// <param name="dstpos"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool PointsBasedArc2Move(float[] midpos, float[] dstpos, int mode);

        /// <summary>
        /// 基于圆心的两轴圆弧插补运动
        /// </summary>
        /// <param name="dstpos"></param>
        /// <param name="cenpos"></param>
        /// <param name="dir"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool CenterBasedArc2Move(float[] dstpos, float[] cenpos, int dir, int mode);

        /// <summary>
        /// 设置指定轴的减速运动模式
        /// </summary>
        /// <param name="axisNum"></param>
        /// <param name="deceleratemode"></param>
        /// <returns></returns>
        bool SetCornerDecelerateMode(int axisNum, int deceleratemode);

        /// <summary>
        /// 设置指定轴减速运动模式的减速角范围
        /// </summary>
        /// <param name="axisNum"></param>
        /// <param name="decelanglerange"></param>
        /// <returns></returns>
        bool SetCornerDecelerateAngleRange(int axisNum, float[] decelanglerange);

        /// <summary>
        /// 设置指定轴减速运动模式的减速半径
        /// </summary>
        /// <param name="axisNum"></param>
        /// <param name="decelradius"></param>
        /// <returns></returns>
        bool SetCornerDecelerateRadius(int axisNum, float decelradius);

        /// <summary>
        /// 设置指定轴减速运动模式的倒角半径
        /// </summary>
        /// <param name="axisNum"></param>
        /// <param name="soomthradius"></param>
        /// <returns></returns>
        bool SetCornerSoomthRadius(int axisNum, float soomthradius);

        /// <summary>
        /// 设置指定输出端口输出逻辑值
        /// </summary>
        /// <param name="nbit"></param>
        /// <param name="onoff"></param>
        /// <returns></returns>
        bool SetOutBitLogicValue(int nbit, bool onoff);

        /// <summary>
        /// 获取指定输出端口位的电平
        /// </summary>
        /// <param name="nbit"></param>
        /// <param name="onoff"></param>
        /// <returns></returns>
        bool GetOutBitLogicValue(int nbit, ref bool onoff);

        /// <summary>
        /// 获取指定输入端口位的电平
        /// </summary>
        /// <param name="nbit"></param>
        /// <param name="onoff"></param>
        /// <returns></returns>
        bool GetInBitLogicValue(int nbit, ref bool onoff);
       
        /// <summary>
        /// 等待指定轴找原点
        /// [注：找原点+轴停止+轴到指定位置]
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="waitfordatum"></param>
        /// <param name="sleepsecond"></param>
        /// <param name="timeout"></param>
        /// <param name="enablepause"></param>
        /// <param name="limitdistance"></param>
        /// <param name="specifiedpos"></param>
        /// <returns></returns>
        bool WaitForAxisFindDatum(int naxis, bool waitfordatum = true, double sleepsecond = 0.01,
                                  double timeout = 20, bool enablepause = true, float limitdistance = -1, float specifiedpos = 0.0f);

        /// <summary>
        /// 等待指定轴停止
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="sleepsecond"></param>
        /// <param name="timeout"></param>
        /// <param name="enablepause"></param>
        /// <returns></returns>
        bool WaitForAxisStop(int naxis, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true);

        /// <summary>
        /// 等待指定轴到指定限位
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="limittype"></param>
        /// <param name="waitforstatus"></param>
        /// <param name="sleepsecond"></param>
        /// <param name="timeout"></param>
        /// <param name="enablepause"></param>
        /// <returns></returns>
        bool WaitForAxisLimit(int naxis, LimitType limittype, bool waitforstatus = true, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true);
    }

    /// <summary>
    /// 板卡操作类
    /// </summary>
    public abstract class BoardDriver : IBoardDriver
    {
        public DriverExceptionOccuredDel ExceptionOccuredDel;

        public BoardDriver()
        {
            ExceptionOccuredDel = new DriverExceptionOccuredDel(OnDirverExceptionOccured);
        }
        private void OnDirverExceptionOccured(string err)
        {
            //什么都不做
        }

        #region 抽象成员(钩子函数)

        public abstract bool DoConnectCtrller(string ip,int port=8089);     
        public abstract bool DoDisconnectCtrller();
        public abstract bool DoInitCtrllerSys();
        public abstract bool DoSetBaseAxes(int axisNum, int[] piAxislist);
        public abstract bool DoSetAxisType(int naxis, int type);
        public abstract bool DoSetAxisPulseOutMode(int naxis, int mode);
        public abstract bool DoSetAxisLimitMode(int naxis, int mode);
        public abstract bool DoSetAxisALMIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level);
        public abstract bool DoGetAxisALMIn(int naxis, ref int inputno); 
        public abstract bool DoSetAxisHRevIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level);
        public abstract bool DoGetAxisHRevIn(int naxis, ref int inputno);       
        public abstract bool DoSetAxisSRevValue(int naxis, float fvalue);
        public abstract bool DoSetAxisDatumIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level);
        public abstract bool DoGetAxisDatumIn(int naxis, ref int inputno);    
        public abstract bool DoSetAxisHFwdIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level);
        public abstract bool DoGetAxisHFwdIn(int naxis, ref int inputno);
        public abstract bool DoSetAxisSFwdValue(int naxis, float fvalue);
        public abstract bool DoSetPortInEffectiveLevel(int inputno, ProCommon.Communal.ElectricalLevel level);
        public abstract bool DoSetAxisUnits(int naxis, float units);
        public abstract bool DoSetAxisCreep(int naxis, float creep);
        public abstract bool DoSetAxisTrapeziumPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec);

        public abstract bool DoSetDA(int ionum, float fValue);
        public abstract bool DoSetAxisSigmoidPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec, int sacc, int sdec);


        public abstract bool DoSetAxisSDEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel sdlevel, uint sdmode);
        public abstract bool DoSetAxisPCSEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel pcslevel);
        public abstract bool DoSetAxisINPEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel inplevel);
        public abstract bool DoSetAxisERCEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel erclevel, uint ercwidth, uint ercofftime);
        public abstract bool DoSetAxisERC(int naxis, uint sel);
        public abstract bool DoSetAxisALMEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel almlevel, uint actionmode);
        public abstract bool DoSetAxisEZEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ezlevel, uint actionmode);
        public abstract bool DoSetAxisLTCEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ltclevel, uint actionmode);
        public abstract bool DoSetAxisELEffectiveLevel(int naxis, uint actionmode);
        public abstract bool DoSetAxisDatumEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel datumlevel, uint filter);
        public abstract bool DoSetAxisServo(int naxis, bool onoff);
        public abstract bool DoSetAxisEMGEffectiveLevel(uint enable, ProCommon.Communal.ElectricalLevel emglevel);


        public abstract bool DoFindDatum(int naxis, ProCommon.Communal.MoveDir moveDir, int mode);
        public abstract bool DoSingleContinueMove(int naxis, ProCommon.Communal.MoveDir movedir);
        public abstract bool DoSingleRelMove(int naxis, float pos, float speed = 0.0f);
        public abstract bool DoSingleAbsMove(int naxis, float pos, float speed = 0.0f);
        public abstract bool DoCancelAxisList(int[] axes,int mode);
        public abstract bool DoSingleCancelMove(int naxis, int mode);
        public abstract bool DoSetAxisCurPos(int naxis, float curpos);
        public abstract bool DoGetAxisCurPos(int naxis, ref float curpos);
        public abstract bool DoGetAxisCurspeed(int naxis, ref float curspeed);
        public abstract bool DoChekcAxisIfStop(int naxis, ref bool stopped);
        public abstract bool DoGetAxisStatus(int naxis, ref int axisStatus);
        public abstract bool DoCheckAxisIfNormal(int naxis, ref bool normal);
        public abstract bool DoRapidStop(int mode);
        public abstract bool DoLine2Move(float[] axespos, int mode);
        public abstract bool DoPointsBasedArc2Move(float[] midpos, float[] dstpos, int mode);
        public abstract bool DoCenterBasedArc2Move(float[] dstpos, float[] cenpos, int dir, int mode);

        public abstract bool DoSetCornerDecelerateMode(int axisNum, int deceleratemode);
        public abstract bool DoSetCornerDecelerateAngleRange(int axisNum, float[] decelanglerange);
        public abstract bool DoSetCornerDecelerateRadius(int axisNum, float decelradius);
        public abstract bool DoSetCornerSoomthRadius(int axisNum, float soomthradius);

        public abstract bool DoSetOutBitLogicValue(int nbit, bool onoff);
        public abstract bool DoGetOutBitLogicValue(int nbit, ref bool onoff);
        public abstract bool DoGetInBitLogicValue(int nbit, ref bool onoff);

        public abstract bool DoWaitForAxisFindDatum(int naxis, bool waitfordatum = true, double sleepsecond = 0.01,
             double timeout = 20, bool enablepause = true, float limitdistance = -1, float specifiedpos = 0.0f);
        public abstract bool DoWaitForAxisStop(int naxis, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true);
        public abstract bool DoWaitForAxisLimit(int naxis, LimitType limittype, bool waitforstatus = true, double sleepsecond = 0.01,
             double timeout = 20, bool enablepause = true);
        #endregion

        #region 实现接口
        public bool ConnectCtrller(string ip,int port=8089)
        {
            return DoConnectCtrller(ip, port);
        }

        public bool DisconnectCtrller()
        {
            return DoDisconnectCtrller();
        }
        public bool InitCtrllerSys()
        {
            return DoInitCtrllerSys();
        }
        public bool SetBaseAxes(int axisNum, int[] piAxislist)
        {
            return DoSetBaseAxes(axisNum, piAxislist);
        }
        public bool SetAxisType(int naxis, int type)
        {
            return DoSetAxisType(naxis, type);
        }
        public bool SetAxisPulseOutMode(int naxis, int mode)
        {
            return DoSetAxisPulseOutMode(naxis, mode);
        }

        public bool SetAxisLimitMode(int naxis, int mode)
        {
            return DoSetAxisLimitMode(naxis,mode);
        }

        public bool SetAxisALMIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level)
        {
            return DoSetAxisALMIn(naxis, inputno,level);
        }

        public bool GetAxisALMIn(int naxis, ref int inputno)
        {
           return DoGetAxisALMIn(naxis, ref inputno);
        }

        public bool GetAxisHRevIn(int naxis, ref int inputno)
        {
            return DoGetAxisHRevIn(naxis, ref inputno);
        }

        public bool GetAxisDatumIn(int naxis, ref int inputno)
        {
            return DoGetAxisDatumIn(naxis, ref inputno);
        }

        public bool GetAxisHFwdIn(int naxis, ref int inputno)
        {
            return DoGetAxisHFwdIn(naxis, ref inputno);
        }

        public bool SetAxisHRevIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level)
        {
            return DoSetAxisHRevIn(naxis, inputno,level);
        }
        public bool SetAxisSRevValue(int naxis, float fvalue)
        {
            return DoSetAxisSRevValue(naxis, fvalue);
        }
        public bool SetAxisDatumIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level)
        {
            return DoSetAxisDatumIn(naxis, inputno,level);
        }
        public bool SetAxisHFwdIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level)
        {
            return DoSetAxisHFwdIn(naxis, inputno,level);
        }
        public bool SetAxisSFwdValue(int naxis, float fvalue)
        {
            return DoSetAxisSFwdValue(naxis, fvalue);
        }
        public bool SetPortInEffectiveLevel(int inputno, ProCommon.Communal.ElectricalLevel level)
        {
            return DoSetPortInEffectiveLevel(inputno, level);
        }
        public bool SetAxisUnits(int naxis, float units)
        {
            return DoSetAxisUnits(naxis, units);
        }

        public bool SetAxisCreep(int naxis, float creep)
        {
            return DoSetAxisCreep(naxis, creep);
        }
        public bool SetAxisTrapeziumPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec)
        {
            return DoSetAxisTrapeziumPara(naxis, lspeed, maxspeed, tacc, tdec);
        }

        public  bool SetDA(int ionum, float fValue)
        {
            return DoSetDA(ionum, fValue);
        }
        public bool SetAxisSigmoidPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec, int sacc, int sdec)
        {
            return DoSetAxisSigmoidPara(naxis, lspeed, maxspeed, tacc, tdec, sacc, sdec);
        }
        public bool FindDatum(int naxis, ProCommon.Communal.MoveDir moveDir, int mode)
        {
            return DoFindDatum(naxis, moveDir, mode);
        }
        public bool SingleContinueMove(int naxis, ProCommon.Communal.MoveDir movedir)
        {
            return DoSingleContinueMove(naxis, movedir);
        }
        public bool SingleRelMove(int naxis, float pos, float speed = 0.0f)
        {
            return DoSingleRelMove(naxis, pos,speed);
        }
        public bool SingleAbsMove(int naxis, float pos, float speed = 0.0f)
        {
            return DoSingleAbsMove(naxis, pos,speed);
        }

        public bool CancelAxisList(int[] axes,int mode)
        {
            return DoCancelAxisList(axes,mode);
        }
        public bool SingleCancelMove(int naxis, int mode)
        {
            return DoSingleCancelMove(naxis, mode);
        }
        public bool SetAxisCurPos(int naxis, float curpos)
        {
            return DoSetAxisCurPos(naxis, curpos);
        }
        public bool GetAxisCurPos(int naxis, ref float curpos)
        {
            return DoGetAxisCurPos(naxis, ref curpos);
        }
        public bool GetAxisCurspeed(int naxis, ref float curspeed)
        {
            return DoGetAxisCurspeed(naxis, ref curspeed);
        }
        public bool ChekcAxisIfStop(int naxis, ref bool stopped)
        {
            return DoChekcAxisIfStop(naxis, ref stopped);
        }
        public bool GetAxisStatus(int naxis, ref int axisStatus)
        {
            return DoGetAxisStatus(naxis, ref axisStatus);
        }
        public bool CheckAxisIfNormal(int naxis, ref bool normal)
        {
            return DoCheckAxisIfNormal(naxis, ref normal);
        }
        public bool RapidStop(int mode)
        {
            return DoRapidStop(mode);
        }
        public bool Line2Move(float[] axespos, int mode)
        {
            return DoLine2Move(axespos, mode);
        }
        public bool PointsBasedArc2Move(float[] midpos, float[] dstpos, int mode)
        {
            return DoPointsBasedArc2Move(midpos, dstpos, mode);
        }

        public bool CenterBasedArc2Move(float[] dstpos, float[] cenpos, int dir, int mode)
        {
            return DoCenterBasedArc2Move(dstpos, cenpos, dir, mode);
        }
        public bool SetCornerDecelerateMode(int axisNum, int deceleratemode)
        {
            return DoSetCornerDecelerateMode(axisNum, deceleratemode);
        }
        public bool SetCornerDecelerateAngleRange(int axisNum, float[] decelanglerange)
        {
            return SetCornerDecelerateAngleRange(axisNum, decelanglerange);
        }
        public bool SetCornerDecelerateRadius(int axisNum, float decelradius)
        {
            return SetCornerDecelerateRadius(axisNum, decelradius);
        }
        public bool SetCornerSoomthRadius(int axisNum, float smoothradius)
        {
            return SetCornerSoomthRadius(axisNum, smoothradius);
        }

        public bool SetOutBitLogicValue(int nbit, bool onoff)
        {
            return DoSetOutBitLogicValue(nbit, onoff);
        }
        public bool GetOutBitLogicValue(int nbit, ref bool onoff)
        {
            return DoGetOutBitLogicValue(nbit, ref onoff);
        }
        public bool GetInBitLogicValue(int nbit, ref bool onoff)
        {
            return DoGetInBitLogicValue(nbit, ref onoff);
        }
        public bool WaitForAxisFindDatum(int naxis, bool waitfordatum = true, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true, float limitdistance = -1, float specifiedpos = 0.0f)
        {
            return DoWaitForAxisFindDatum(naxis, waitfordatum, sleepsecond, timeout, enablepause, limitdistance, specifiedpos);
        }
        public bool WaitForAxisStop(int naxis, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true)
        {
            return DoWaitForAxisStop(naxis, sleepsecond, timeout, enablepause);
        }
        public bool WaitForAxisLimit(int naxis, LimitType limittype, bool waitforstatus = true, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true)
        {
            return DoWaitForAxisLimit(naxis, limittype, waitforstatus, sleepsecond, timeout, enablepause);
        }


        public bool SetAxisSDEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel sdlevel, uint sdmode)
        {
            return DoSetAxisSDEffectiveLevel(naxis, enable, sdlevel, sdmode);
        }
        public bool SetAxisPCSEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel pcslevel)
        {
            return DoSetAxisPCSEffectiveLevel(naxis, enable, pcslevel);
        }
        public bool SetAxisINPEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel inplevel)
        {
            return DoSetAxisINPEffectiveLevel(naxis, enable, inplevel);
        }
        public bool SetAxisERCEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel erclevel, uint ercwidth, uint ercofftime)
        {
            return DoSetAxisERCEffectiveLevel(naxis, enable, erclevel, ercwidth, ercofftime);
        }
        public bool SetAxisERC(int naxis, uint sel)
        {
            return DoSetAxisERC(naxis, sel);
        }
        public bool SetAxisALMEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel almlevel, uint actionmode)
        {
            return DoSetAxisALMEffectiveLevel(naxis, almlevel, actionmode);
        }
        public bool SetAxisEZEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ezlevel, uint actionmode)
        {
            return DoSetAxisEZEffectiveLevel(naxis, ezlevel, actionmode);
        }
        public bool SetAxisLTCEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ltclevel, uint actionmode)
        {
            return DoSetAxisLTCEffectiveLevel(naxis, ltclevel, actionmode);
        }
        public bool SetAxisELEffectiveLevel(int naxis, uint actionmode)
        {
            return DoSetAxisELEffectiveLevel(naxis, actionmode);
        }
        public bool SetAxisDatumEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel datumlevel, uint filter)
        {
            return DoSetAxisDatumEffectiveLevel(naxis, datumlevel, filter);
        }
        public bool SetAxisServo(int naxis, bool onoff)
        {
            return DoSetAxisServo(naxis, onoff);
        }
        public bool SetAxisEMGEffectiveLevel(uint enable, ProCommon.Communal.ElectricalLevel emglevel)
        {
            return DoSetAxisEMGEffectiveLevel(enable, emglevel);
        }

        #endregion

        #region 覆写并抽象化Object类的ToString()
        public abstract override string ToString();       
        #endregion
    }


    /// <summary>
    /// PLC操作接口
    /// </summary>
    public interface IPLCDriver
    {

    }

    /// <summary>
    /// PLC操作类
    /// </summary>
    public abstract class PLCDriver : IPLCDriver
    {

    }

    #endregion
}
