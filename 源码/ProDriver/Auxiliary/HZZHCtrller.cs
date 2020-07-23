using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       HZZHCtrller
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProDriver.Auxiliary
 * File      Name：       HZZHCtrller
 * Creating  Time：       3/19/2020 12:42:58 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProDriver.Auxiliary
{

    internal class HZZHCtrllerAddress
    {

        #region 系统地址
        public static int HardWare_Ver = 0;		//0-19	硬件版本
        public static int SoftWare_Ver = 20;		//20-39	软件版本
        public static int Reserve = 40;			//40-99	保留
        public static int AxisEncoder = 100; //100-199	轴编码器值
        public static int AxisPosition = 200;//200-299	轴当前位置
        public static int AxisStatus = 300;			//300-309	轴当前状态
        public static int InputStatus = 310;		//310-389	输入口状态
        public static int ErrorCode = 390;			//390-	错误码
        public static int ErrorLevel = 430;				//430-	错误等级

        public static int RunStatus = 500;			//500
        public static int res = 1000;			//1000-1019 保留
        public static int OutputStatus = 1020;	//1020-1099	输出口状态
        public static int FlashOperate = 1100;		//1100-	Flash操作
        public static int FilesOperate = 1102;		//1102-	文件操作
        public static int FramOperate = 1104;		//1104-	铁电操作
        public static int JogMode = 1106;			//1106-	点动模式
        public static int JogPositionLevel = 1108;	//1108-	点动行程级别
        public static int JogPosition = 1110;		//1110-	点动设定行程
        public static int JogForward = 1112;		//1112-1115	正向点动
        public static int JogBackward = 1116;		//1116-1119	反向点动
        public static int JogGohome = 1120;		//1120-1123	点动回零
        public static int JogStop = 1124;			//1124-1127	点动立即停止
        public static int jogSpd = 1128;				//1128  轴点动百分比速度
        public static int ClearAlarm = 1130;			//1130-	清除错误	
        public static int AxisData = 2000;			//2000-2999	50个轴，每个占20个寄存器

        public static int AxisIOConfig = 3200;			//IO配置
        public static int AxisPulse = 3000;		//脉冲
        public static int AxisLeader = 3002;//导程


        #endregion


        #region 活动地址

        /// <summary>
        /// 平台移动  en;	//[6]
        ///  point;//[6]
        ///  start;[1]
        /// </summary>
        public static int DeskRun = 1500;
        /// <summary>
        ///  execute;        //启动
        ///  u32 type;       //类型 0点焊 1拖焊起点 2托焊终点
        ///  u32 ymode;      //0 y1 1 y2 坐标
        ///  u32 backmode;   //0 原来返回， 1 直线上升
        ///  PointDef6 point;//坐标[5]
        ///  u32 detax;      //角度计算得出偏差
        ///  u32 detay;
        ///  u32 SlowZ;      //慢速接近高度
        ///  u32 Workz;      //焊锡高度
        ///  u32 uPara[20];  //u轴参数，包括速度等
        /// </summary>

        /// <summary>
        ///  execute;        //启动
        ///  u32 ymode;      //0 y1 1 y2 坐标
        ///  PointDef6 point;//坐标[4]  x y z r
        ///  u32 Workz;      //焊锡高度
        ///  u32 solderr;    //焊锡角度
        ///  u32 speed;               //焊锡速度
        //  u32 pointmode;            //0普通焊点  1.拖焊起点 2.拖焊终点
        //  feedPara feed[3];         //送锡
        //  u32 heatTime[3];          //加热时间
        //  s32 raiseheight;            //提起高度
        //  u32 mode;        //抖动模式
        //  u32 times;       //次数
        //  u32 len;         //抖动距离
        //  s32 height;      //抖动高度
        //  u32 speed;       //抖动速度
        //  u32 ufeedlen;     //抖动送锡长度
        //  u32 feedspeed;    //抖动送锡速度
        //  u32 dwmode;        //气缸下降是否开启
        //  u32 dwdelay;       //下降气缸到位延时
        //  u32 upmode;        //气缸上升是否开启
        //  u32 updelay;       //上升到位延时
        //  u32 mode;           //返回模式
        //  s32 backheight;    //返回高度
        /// </summary>
        public static int solderPoint = 1520;
        /// <summary>
        /// TeachSpd[6];	//示教速度
        /// Runspd[5];  //快速运行速度 不带u轴的送锡
        /// slowspd[5];	//
        /// </summary>
        public static int Speed = 4000;
        /// <summary>
        /// reference;  基准高度
        /// moveheight; 移动高度
        /// </summary>
        public static int Zpos = 4022;

        /// <summary>
        /// execute; //启动标识
        /// ymode;  //y坐标
        /// PointDef6 point; //坐标
        /// ufeedlength; //	
        /// ufeedspeed;
        /// times;
        /// </summary>
        public static int Clean = 1720;

        /// <summary>
        /// 输入输出配置
        /// </summary>
        public static int Q_Clean = 4100;
        public static int Q_Red = 4102;
        public static int Q_Yellow = 4104;
        public static int Q_Green = 4106;
        public static int Q_Bee = 4108;
        public static int Q_Done = 4110;
        public static int Q_WorkDone = 4112;
        public static int Q_RestDone = 4114;
        public static int Q_BackDone = 4116;


        public static int I_Y1start = 4120;
        public static int I_Y2start = 4122;
        public static int I_LackTin = 4124;
        public static int I_JammedTin = 4126;
        public static int I_Stop = 4128;
        public static int I_Rest = 4130;
        public static int I_Clean = 4132;
        public static int I_Tinfeeding = 4134;
        public static int I_Back = 4136;
        public static int I_Y1Rdy = 4138;
        public static int I_Y2Rdy = 4140;
        public static int I_Y1raster = 4142;
        public static int I_Y2raster = 4144;


        #endregion

    }

    public class HZZHCtrller
    {
        /// <summary>
        /// 与控制器通信类
        /// [注:暂时采用Modbus通信]
        /// </summary>
        private Modbus.Common _comNet;

        #region 静态单例

        static object _syncObj = new object();
        static HZZHCtrller _instance;
        public static HZZHCtrller Instance
        {
            get
            {
                lock (_syncObj)
                {
                    if (_instance == null)
                    { _instance = new HZZHCtrller(); }
                }

                return _instance;
            }
        }

        private bool _netWorkState,_netConnected;//板卡工作标记,板卡通信连接成功标记
        private System.Collections.Concurrent.ConcurrentQueue<ProDriver.Auxiliary.HZZHComBaseData> _netQueue;
        private System.ComponentModel.BackgroundWorker _bkgrdwkForGeneralBaseData,_bkgrdwkForSpecialBaseData;

        /// <summary>
        /// 控制器专用数据单元
        /// [注:包括控制的轴当前位置单元,轴状态单元,输入端口状态单元,输出端口状态单元
        /// ,错误代码单元,报警状态单元,板卡状态单元,]
        /// </summary>
        private System.Collections.Generic.List<ProDriver.Auxiliary.HZZHComBaseData> _specialBaseDataList;

        #region 板卡专用数据单元
        /// <summary>
        /// 轴位置数据单元
        /// </summary>     
        public ProDriver.Auxiliary.HZZHComBaseData AxesPositionBaseData { private set; get; }

        /// <summary>
        /// 轴状态数据单元
        /// </summary>      
        public ProDriver.Auxiliary.HZZHComBaseData AxesStatusBaseData { private set; get; }

        /// <summary>
        /// 输入端口状态数据单元
        /// </summary>       
        public ProDriver.Auxiliary.HZZHComBaseData InputStatusBaseData { private set; get; }

        /// <summary>
        /// 输出端口状态数据单元
        /// </summary>    
        public ProDriver.Auxiliary.HZZHComBaseData OutputStatusBaseData { private set; get; }

        /// <summary>
        /// 错误代码数据单元
        /// </summary>      
        public ProDriver.Auxiliary.HZZHComBaseData ErrorCodeBaseData { private set; get; }

        /// <summary>
        /// 报警状态数据单元
        /// </summary>      
        public ProDriver.Auxiliary.HZZHComBaseData AlarmOnBaseData { private set; get; }

        /// <summary>
        /// 板卡运行状态数据单元
        /// </summary>     
        public ProDriver.Auxiliary.HZZHComBaseData BoardStatusBaseData { private set; get; }
        #endregion

        private HZZHCtrller()
        {
            _comNet = new Modbus.Common();
            _netWorkState = false;
            _netConnected = false;
            _netQueue = new System.Collections.Concurrent.ConcurrentQueue<ProDriver.Auxiliary.HZZHComBaseData>();

            //轴位置:从指定地址开始的，30个整型变量
            AxesPositionBaseData = new ProDriver.Auxiliary.HZZHComBaseData();
            AxesPositionBaseData.AssignValue<int>((ushort)ProDriver.Auxiliary.HZZHCtrllerAddress.AxisPosition,30);

            //轴状态:从指定地址开始的,5个整型变量
            AxesStatusBaseData = new ProDriver.Auxiliary.HZZHComBaseData();
            AxesStatusBaseData.AssignValue<int>((ushort)ProDriver.Auxiliary.HZZHCtrllerAddress.AxisStatus, 5);

            //输入端口状态:从指定地址开始的,40个整型变量
            InputStatusBaseData = new ProDriver.Auxiliary.HZZHComBaseData();
            InputStatusBaseData.AssignValue<int>((ushort)ProDriver.Auxiliary.HZZHCtrllerAddress.InputStatus, 40);

            //输出端口状态:从指定地址开始的,40个整型变量
            OutputStatusBaseData = new ProDriver.Auxiliary.HZZHComBaseData();
            OutputStatusBaseData.AssignValue<int>((ushort)ProDriver.Auxiliary.HZZHCtrllerAddress.OutputStatus, 40);

            //错误代码:从指定地址开始的,20个整型变量
            ErrorCodeBaseData = new ProDriver.Auxiliary.HZZHComBaseData();
            ErrorCodeBaseData.AssignValue<int>((ushort)ProDriver.Auxiliary.HZZHCtrllerAddress.ErrorCode, 20);

            //报警状态:从指定地址开始的,1个整型变量
            AlarmOnBaseData = new ProDriver.Auxiliary.HZZHComBaseData();
            AlarmOnBaseData.AssignValue<int>((ushort)ProDriver.Auxiliary.HZZHCtrllerAddress.ErrorLevel, 1);

            //板卡运行状态:从指定地址开始的,1个整型变量
            BoardStatusBaseData = new ProDriver.Auxiliary.HZZHComBaseData();
            BoardStatusBaseData.AssignValue<int>((ushort)ProDriver.Auxiliary.HZZHCtrllerAddress.RunStatus, 1);

        }

        #endregion

        /// <summary>
        /// 访问成功标记
        /// </summary>
        public bool IsConnected { get { return _netConnected; }set { _netConnected = value; } }

        /// <summary>
        /// 专用数据单元更新耗时
        /// </summary>
        public double SpecailBaseDataElapse { private set; get; }

        /// <summary>
        /// 添加需要访问的数据单元
        /// </summary>
        /// <param name="bsData"></param>
        public void AddNetList(ProDriver.Auxiliary.HZZHComBaseData bsData)
        {
            lock(_syncObj)
            {
                _netQueue.Enqueue(bsData);
            }
        }

        public bool ConnectCtrller(string ip,int port)
        {
            bool rt = false;
            if(_comNet!=null)
            {
                try
                {
                    _comNet.InitializeUdp(ip, port);
                    ResetSpecialBaseData();
                    _netConnected = true;                   
                }catch
                {
                    _netConnected = false;
                }
            }

            if (_netConnected)
            {
                _netWorkState = true;

                _bkgrdwkForGeneralBaseData = new System.ComponentModel.BackgroundWorker();
                _bkgrdwkForGeneralBaseData.WorkerSupportsCancellation = true;
                _bkgrdwkForGeneralBaseData.DoWork += _bkgrdwkForGeneralBaseData_DoWork;
                _bkgrdwkForGeneralBaseData.RunWorkerCompleted += _bkgrdwkForGeneralBaseData_RunWorkerCompleted;
                _bkgrdwkForGeneralBaseData.RunWorkerAsync();

                _bkgrdwkForSpecialBaseData = new System.ComponentModel.BackgroundWorker();
                _bkgrdwkForSpecialBaseData.WorkerSupportsCancellation = true;
                _bkgrdwkForSpecialBaseData.DoWork += _bkgrdwkForSpecialBaseData_DoWork;
                _bkgrdwkForSpecialBaseData.RunWorkerCompleted += _bkgrdwkForSpecialBaseData_RunWorkerCompleted;
                _bkgrdwkForSpecialBaseData.RunWorkerAsync();
            }
            else { _netWorkState = false; }

            return rt;
        }

        /// <summary>
        /// 板卡专用工作完成回调
        /// [注:释放内建变量]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _bkgrdwkForSpecialBaseData_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
           
        }

        /// <summary>
        /// 板卡专用工作回调
        /// [注:读写专用变量值]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _bkgrdwkForSpecialBaseData_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            uint[] uintData;
            ushort[] ushortData;
            short[] shortData;
            int[] intData;
            float[] floatData;
            byte[] byteData;          

            ushort address = 0, num = 1;
            int lpcnt = 0;
            ProDriver.Auxiliary.HZZHComDataType dt;
            System.DateTime startTime = System.DateTime.Now;
            while(_netWorkState)
            {
                try
                {
                    if(_netConnected)
                    {
                        //通用访问数据单元为空且专用数据单元非空
                        if((_netQueue.Count==0)
                            &&(_specialBaseDataList.Count>0))
                        {
                            address = _specialBaseDataList[lpcnt].Address;
                            num = _specialBaseDataList[lpcnt].RegisterNumber;
                            dt = _specialBaseDataList[lpcnt].DataType;
                            switch (dt)
                            {
                                case ProDriver.Auxiliary.HZZHComDataType.Short:
                                    _comNet.Read(1, address, num, out shortData);
                                    System.Buffer.BlockCopy(shortData, 0, _specialBaseDataList[lpcnt].ShortValue, 0, shortData.Length * 2);
                                    break;
                                case ProDriver.Auxiliary.HZZHComDataType.Uint:
                                    _comNet.Read(1, address, num, out uintData);
                                    System.Buffer.BlockCopy(uintData, 0, _specialBaseDataList[lpcnt].UintValue, 0, uintData.Length * 4);
                                    break;
                                case ProDriver.Auxiliary.HZZHComDataType.Ushort:
                                    _comNet.Read(1, address, num, out ushortData);
                                    System.Buffer.BlockCopy(ushortData, 0, _specialBaseDataList[lpcnt].UshortValue, 0, ushortData.Length * 2);
                                    break;
                                case ProDriver.Auxiliary.HZZHComDataType.Float:
                                    _comNet.Read(1, address, num, out floatData);
                                    System.Buffer.BlockCopy(floatData, 0, _specialBaseDataList[lpcnt].FloatValue, 0, floatData.Length * 4);
                                    break;
                                case ProDriver.Auxiliary.HZZHComDataType.Byte:
                                    _comNet.Read(1, address, num, out byteData);

                                    byte[] btArr = new byte[byteData.Length];
                                    for (int i = 0; i < byteData.Length; i++)
                                    {
                                        //高低位互换
                                        if (i % 2 != 0)
                                        {
                                            btArr[i - 1] = byteData[i];
                                            btArr[i] = byteData[i - 1];
                                        }
                                    }
                                    System.Buffer.BlockCopy(btArr, 0, _specialBaseDataList[lpcnt].ByteValue, 0, byteData.Length);
                                    break;
                                case ProDriver.Auxiliary.HZZHComDataType.Int:
                                default:
                                    _comNet.Read(1, address, num, out intData);
                                    System.Buffer.BlockCopy(intData, 0, _specialBaseDataList[lpcnt].IntValue, 0, intData.Length * 4);
                                    break;
                            }

                            lpcnt++;
                            if(lpcnt>=_specialBaseDataList.Count)
                            {
                                System.Threading.Thread.Sleep(2);                               
                                System.TimeSpan elapse = System.DateTime.Now.Subtract(startTime);
                                SpecailBaseDataElapse = elapse.TotalMilliseconds;
                                lpcnt = 0;
                                startTime = System.DateTime.Now;
                            }
                        }
                    }
                }
                catch
                {
                    _netWorkState = false;
                    _netConnected = false;
                }
            }
        }

        private void ResetSpecialBaseData()
        {
            if (_specialBaseDataList == null)
                _specialBaseDataList = new List<HZZHComBaseData>();

            _specialBaseDataList.Clear();
            _specialBaseDataList.Add(AxesPositionBaseData);
            _specialBaseDataList.Add(AxesStatusBaseData);
            _specialBaseDataList.Add(InputStatusBaseData);
            _specialBaseDataList.Add(OutputStatusBaseData);
            _specialBaseDataList.Add(ErrorCodeBaseData);
            _specialBaseDataList.Add(AlarmOnBaseData);
            _specialBaseDataList.Add(BoardStatusBaseData);
        }

        public void DisconnectCtrller()
        {
            _netWorkState = false;
            _netConnected = false;
            _bkgrdwkForGeneralBaseData.CancelAsync();           
        }

        /// <summary>
        /// 板卡通用工作回调
        /// [注:读写通用变量值]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _bkgrdwkForGeneralBaseData_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            uint[] uintData;
            ushort[] ushortData;
            short[] shortData;
            int[] intData;
            float[] floatData;
            byte[] byteData;
            bool queueState;          

            //板卡工作状态
            while(_netWorkState)
            {
                try
                {
                    //板卡连接状态
                    if(_netConnected)
                    {
                        //数据单元队列非空(并发队列)
                        if(_netQueue.Count>0)
                        {
                            ProDriver.Auxiliary.HZZHComBaseData bsData;
                            queueState = _netQueue.TryPeek(out bsData);
                            //获取队列元素成功
                            if (queueState)
                            {
                                //数据单元读取状态
                                if (bsData.InitiativeRead)
                                {
                                    try
                                    {
                                        switch (bsData.DataType)
                                        {
                                            case ProDriver.Auxiliary.HZZHComDataType.Short:
                                                _comNet.Read(1, bsData.Address, bsData.RegisterNumber, out shortData);
                                                bsData.ShortValue = (short[])shortData.Clone();
                                                bsData.ComSucceed = true;
                                                break;
                                            case ProDriver.Auxiliary.HZZHComDataType.Uint:
                                                _comNet.Read(1, bsData.Address, bsData.RegisterNumber, out uintData);
                                                bsData.UintValue = (uint[])uintData.Clone();
                                                bsData.ComSucceed = true;
                                                break;
                                            case ProDriver.Auxiliary.HZZHComDataType.Ushort:
                                                _comNet.Read(1, bsData.Address, bsData.RegisterNumber, out ushortData);
                                                bsData.UshortValue = (ushort[])ushortData.Clone();
                                                bsData.ComSucceed = true;
                                                break;
                                            case ProDriver.Auxiliary.HZZHComDataType.Float:
                                                _comNet.Read(1, bsData.Address, bsData.RegisterNumber, out floatData);
                                                bsData.FloatValue = (float[])floatData.Clone();
                                                bsData.ComSucceed = true;
                                                break;
                                            case ProDriver.Auxiliary.HZZHComDataType.Byte:
                                                _comNet.Read(1, bsData.Address, bsData.RegisterNumber, out byteData);

                                                byte[] btArr = new byte[byteData.Length];
                                                for (int i = 0; i < byteData.Length; i++)
                                                {
                                                    //高低位互换
                                                    if (i % 2 != 0)
                                                    {
                                                        btArr[i - 1] = byteData[i];
                                                        btArr[i] = byteData[i - 1];
                                                    }
                                                }

                                                bsData.ByteValue = (byte[])btArr.Clone();
                                                bsData.ComSucceed = true;
                                                break;
                                            case ProDriver.Auxiliary.HZZHComDataType.Int:
                                            default:
                                                _comNet.Read(1, bsData.Address, bsData.RegisterNumber, out intData);
                                                bsData.IntValue = (int[])intData.Clone();
                                                bsData.ComSucceed = true;
                                                break;
                                        }
                                    }
                                    catch (System.Exception ex)
                                    { throw new Exception("ReadError:" + ex.Message); }
                                }
                                else //数据单元写入状态
                                {
                                    try
                                    {
                                        switch (bsData.DataType)
                                        {
                                            case ProDriver.Auxiliary.HZZHComDataType.Int:
                                                _comNet.Write(1, bsData.Address, bsData.IntValue);
                                                bsData.ComSucceed = true;
                                                break;
                                            case ProDriver.Auxiliary.HZZHComDataType.Short:
                                                _comNet.Write(1, bsData.Address, bsData.ShortValue);
                                                bsData.ComSucceed = true;
                                                break;
                                            case ProDriver.Auxiliary.HZZHComDataType.Uint:
                                                _comNet.Write(1, bsData.Address, bsData.UintValue);
                                                bsData.ComSucceed = true;
                                                break;
                                            case ProDriver.Auxiliary.HZZHComDataType.Ushort:
                                                _comNet.Write(1, bsData.Address, bsData.UshortValue);
                                                bsData.ComSucceed = true;
                                                break;
                                            case ProDriver.Auxiliary.HZZHComDataType.Float:
                                                _comNet.Write(1, bsData.Address, bsData.FloatValue);
                                                bsData.ComSucceed = true;
                                                break;
                                            case ProDriver.Auxiliary.HZZHComDataType.Byte:
                                                _comNet.Write(1, bsData.Address, bsData.ByteValue);
                                                bsData.ComSucceed = true;
                                                break;
                                            default: break;
                                        }
                                    }
                                    catch (System.Exception ex)
                                    { throw new Exception("WriteError:" + ex.Message); }
                                }
                            }
                            else { throw new Exception("Obtain base data failed !"); }
                            queueState = _netQueue.TryDequeue(out bsData);
                        }
                    }
                }
                catch
                {
                    _netWorkState = false;
                    _netConnected = false;
                }
            }

        }

        /// <summary>
        /// 板卡通用工作完成回调
        /// [注:释放内建变量]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _bkgrdwkForGeneralBaseData_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

        }

        public ProDriver.Auxiliary.HZZHComBaseData ReadRegister(ushort address,int registerNumber,ProDriver.Auxiliary.HZZHComDataType dataType,int blockTime=1000)
        {
            ProDriver.Auxiliary.HZZHComBaseData bsData = new HZZHComBaseData();
            switch(dataType)
            {
                case ProDriver.Auxiliary.HZZHComDataType.Byte:
                    bsData.AssignValue(address, new byte[registerNumber]);
                    break;
                case ProDriver.Auxiliary.HZZHComDataType.Float:
                    bsData.AssignValue(address, new float[registerNumber]);
                    break;
                case ProDriver.Auxiliary.HZZHComDataType.Int:
                    bsData.AssignValue(address, new int[registerNumber]);
                    break;
                case ProDriver.Auxiliary.HZZHComDataType.Short:
                    bsData.AssignValue(address, new short[registerNumber]);
                    break;
                case ProDriver.Auxiliary.HZZHComDataType.Uint:
                    bsData.AssignValue(address, new uint[registerNumber]);
                    break;
                case ProDriver.Auxiliary.HZZHComDataType.Ushort:
                    bsData.AssignValue(address, new ushort[registerNumber]);
                    break;
                default:
                    break;
            }
            int cnt = 0;
            //在板卡连接成功的情形下，一直等到数据源访问成功
            while(!bsData.ComSucceed)
            {
                //板卡连接不成功,退出等待
                if(!HZZHCtrller.Instance.IsConnected)
                {
                    break;
                }

                System.Threading.Thread.Sleep(1);
                cnt++;
                //超时退出
                if(cnt> blockTime)
                {
                    bsData.IntValue[0] = -1;
                    break;
                }
            }

            return bsData;
        }
    }

    public enum HZZHComDataType : uint
    {
        Int,
        Float,
        Ushort,
        Short,
        Uint,
        Byte
    }

    /// <summary>
    /// 通信数据单元
    /// [注:普通类]
    /// </summary>
    public class HZZHComBaseData
    {
        /// <summary>
        /// 变量对应的寄存器地址
        /// </summary>
        public ushort Address { set; get; }

        /// <summary>
        /// 整型变量值数组
        /// </summary>
        public int[] IntValue { set; get; }

        /// <summary>
        /// 无符号整型变量值数组
        /// </summary>
        public uint[] UintValue { set; get; }

        /// <summary>
        /// 浮点型变量值数组
        /// </summary>
        public float[] FloatValue { set; get; }

        /// <summary>
        /// 短整型变量值数组
        /// </summary>
        public short[] ShortValue { set; get; }

        /// <summary>
        /// 无符号短整型变量值数组
        /// </summary>
        public ushort[] UshortValue { set; get; }

        /// <summary>
        /// 字节型变量值数组
        /// </summary>
        public byte[] ByteValue { set; get; }

        /// <summary>
        /// 寄存器数量
        /// </summary>
        public ushort RegisterNumber { set; get; }

        /// <summary>
        /// 通信成功标记
        /// </summary>
        public bool ComSucceed { set; get; }

        public ProDriver.Auxiliary.HZZHComDataType DataType { private set; get; }

        /// <summary>
        /// 主动读取标记
        /// </summary>
        public bool InitiativeRead { set; get; }

        public HZZHComBaseData()
        {
            this.ComSucceed = false;
            this.InitiativeRead = false;
        }

        public void AssignValue<T>(ushort address,T[] arrValue)where T:struct
        {
            this.Address = address;
            {
                ProDriver.Auxiliary.HZZHComDataType dtTp = ProDriver.Auxiliary.HZZHComDataType.Int;
                System.Type tp = typeof(T);
                if (tp.Name == "Int32")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Int;
                    this.IntValue = (int[])arrValue.Clone();
                }
                else if (tp.Name == "Single")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Float;
                    this.FloatValue = (float[])arrValue.Clone();
                }
                else if (tp.Name == "UInt16")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Ushort;
                    this.UshortValue = (ushort[])arrValue.Clone();
                }
                else if (tp.Name == "Int16")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Short;
                    this.ShortValue = (short[])arrValue.Clone();
                }
                else if (tp.Name == "UInt32")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Uint;
                    this.UintValue = (uint[])arrValue.Clone();
                }
                else if (tp.Name == "Byte")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Byte;
                    this.ByteValue=(byte[])arrValue.Clone();
                }

                this.DataType = dtTp;
            }  
            this.ComSucceed = false;
            this.InitiativeRead = false;
        }

        public void AssignValue<T>(ushort address,int number) where T : struct
        {
            this.Address = address;
            {
                ProDriver.Auxiliary.HZZHComDataType dtTp = ProDriver.Auxiliary.HZZHComDataType.Int;
                System.Type tp = typeof(T);
                if (tp.Name == "Int32")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Int;
                    this.IntValue = new int[number];
                }
                else if (tp.Name == "Single")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Float;
                    this.FloatValue = new float[number];
                }
                else if (tp.Name == "UInt16")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Ushort;
                    this.UshortValue = new ushort[number];
                }
                else if (tp.Name == "Int16")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Short;
                    this.ShortValue = new short[number];
                }
                else if (tp.Name == "UInt32")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Uint;
                    this.UintValue = new uint[number];
                }
                else if (tp.Name == "Byte")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Byte;
                    this.ByteValue = new byte[number*2];
                }

                this.DataType = dtTp;
            }

            this.RegisterNumber=(ushort)number;
            this.ComSucceed = false;
            this.InitiativeRead = false;
        }

    }

    /// <summary>
    /// 通信数据单元
    /// [注:泛型类]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HZZHComBaseData<T>where T:struct
    {
        /// <summary>
        /// 变量对应的寄存器地址
        /// </summary>
        public ushort Address { set; get; }

        /// <summary>
        /// 寄存器数量
        /// </summary>
        public ushort RegisterNumber { set; get; }

        /// <summary>
        /// 通信成功标记
        /// </summary>
        public bool ComSucceed { set; get; }

        /// <summary>
        /// 主动读取标记
        /// </summary>
        public bool InitiativeRead { set; get; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public ProDriver.Auxiliary.HZZHComDataType DataType
        {           
            get
            {
                ProDriver.Auxiliary.HZZHComDataType dtTp = ProDriver.Auxiliary.HZZHComDataType.Int;
                System.Type tp = typeof(T);
                if(tp.Name=="Int32")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Int;
                } 
                else if(tp.Name == "Single")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Float;
                }
                else if (tp.Name == "UInt16")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Ushort;
                }
                else if (tp.Name == "Int16")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Short;
                }
                else if (tp.Name == "UInt32")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Uint;
                }
                else if (tp.Name == "Byte")
                {
                    dtTp = ProDriver.Auxiliary.HZZHComDataType.Byte;
                }
                return dtTp;
            }
        }

        /// <summary>
        /// 指定类型值的数组
        /// </summary>
        public T[] TValue { set; get; }

        public HZZHComBaseData(ushort address, T[] arrValue)
        {
            this.Address = address;
            this.TValue = (T[])arrValue.Clone();            
            this.ComSucceed = false;
            this.InitiativeRead = false;
        }
    }
}
