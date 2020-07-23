using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProCommon.Communal;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       BoardDriver_HZZH
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProDriver.Driver
 * File      Name：       BoardDriver_HZZH
 * Creating  Time：       2/19/2020 9:56:58 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProDriver.Driver
{
    #region 待修改完善的汇众智慧控制器驱动
    /*
    public class BoardDriver_HZZH : BoardDriver
    {

        HZZH.Card.Controller hzaux;
        public BoardDriver_HZZH()
        {
            hzaux = new HZZH.Card.Controller();
        }

        #region 实现抽象函数

        /// <summary>
        /// 启连控制器
        /// [注:对于EtherNet控制器会用得到]
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public override bool DoConnectCtrller(string ip, int port = 8089)
        {
            bool rt = false;
            try
            {
                if (hzaux == null)
                    hzaux = new HZZH.Card.Controller();

                hzaux.HZ_InitializeUdp(ip, port);
                rt = hzaux.Succeed;
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡链接失败!\n异常描述:{0}", ex.Message));
            }

            return rt;
        }

        /// <summary>
        /// 断连控制器
        /// </summary>
        /// <returns></returns>
        public override bool DoDisconnectCtrller()
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    hzaux.Dispose();
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡关闭失败!\n异常描述:{0}", ex.Message));
            }

            return rt;
        }

        /// <summary>
        /// 初始化控制器资源
        /// [注:对于PCI,PCIE插槽的控制器会用得到]
        /// </summary>
        /// <returns></returns>
        public override bool DoInitCtrllerSys()
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                    rt = true;
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡初始化失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置主轴及联动轴列表
        /// [注:无此功能]
        /// </summary>
        /// <param name="axisNum">主轴</param>
        /// <param name="piAxislist">轴列表</param>
        /// <returns></returns>
        public override bool DoSetBaseAxes(int axisNum, int[] piAxislist)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                    rt = true;
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置主轴失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的轴类型
        /// [注:无此功能]
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="type">轴类型
        /// 0,虚拟轴;1,脉冲方向方式的步进或伺服
        /// 2,虚拟信号控制方式的伺服;3,正交编码器
        /// 4,步进+编码器;5,保留
        /// 6,脉冲方向方式的编码器,可用于手轮输入
        /// 7,脉冲方向方式步进或伺服+EZ信号输入
        /// 8,ZCAN扩展脉冲方向方式步进或伺服
        /// 9,ZCAN扩展正交编码器
        /// 10,ZCAN扩展脉冲方向方式的编码器
        /// 11-64,保留
        /// 65,ECAT脉冲类型
        /// 66,ECAT速度闭环
        /// 67,ECAT力矩闭环
        /// 68-69,保留
        /// 70,ECAT自定义操作,只读取编码器</param>
        /// <returns></returns>
        public override bool DoSetAxisType(int naxis, int type)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                    rt = true;
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴类型失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的脉冲输出模式
        /// [注:无此功能]
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="mode">脉冲输出模式
        /// 0,脉冲+方向,方向信号高电平正向,脉冲常态信号低电平
        /// 1,脉冲+方向,方向信号高电平正向,脉冲常态信号高电平
        /// 2,脉冲+方向,方向信号高电平负向,脉冲常态信号低电平
        /// 3,脉冲+方向,方向信号高电平负向,脉冲常态信号高电平
        /// 4,双脉冲,方向信号高电平,脉冲线接脉冲,脉冲常态信号高电平,正向
        /// 5,双脉冲,方向信号高电平,脉冲线接方向,脉冲常态信号高电平,正向
        /// 6,双脉冲,方向信号低电平,脉冲线接脉冲,脉冲常态信号低电平,正向
        /// 7,双脉冲,方向信号低电平,脉冲线接方向,脉冲常态信号低电平,正向</param>
        public override bool DoSetAxisPulseOutMode(int naxis, int mode)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                    rt = true;
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置脉冲输出模式失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的限位模式
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="mode">
        /// 0-无限位,1-软限位,2-硬限位,3-软硬都限位</param>
        /// <returns></returns>
        public override bool DoSetAxisLimitMode(int naxis, int mode)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxSetLimitMode((ushort)mode, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴限位模式失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的报警信号端口位及有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public override bool DoSetAxisALMIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    //根据汇众智慧板卡InPut电平高低、对应数字以及逻辑定义:低电平-数字1-逻辑true,高电平-数字0-逻辑false
                    ushort almlev = (level == ProCommon.Communal.ElectricalLevel.Low) ? (ushort)0 : (ushort)1;
                    if (hzaux.HZ_AxSetAlarmlevel(almlev, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴报警端口失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定轴的报警信号端口位
        /// [注:无此功能]
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="inputno">输入端口位号</param>
        /// <returns></returns>
        public override bool DoGetAxisALMIn(int naxis, ref int inputno)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                   
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取轴报警端口失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的负向硬限位信号及有效电平
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="inputno">输入端口位号</param>
        /// <returns></returns>
        public override bool DoSetAxisHRevIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    //根据汇众智慧板卡InPut电平高低、对应数字以及逻辑定义:低电平-数字1-逻辑true,高电平-数字0-逻辑false
                    ushort neglimitlev = (level == ProCommon.Communal.ElectricalLevel.Low) ? (ushort)0 : (ushort)1;
                    if (hzaux.HZ_AxSetNegsLimitlevel((ushort)inputno, neglimitlev, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置硬负限位端口失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定轴的负向硬限位信号
        /// [注:无此功能]
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="inputno">输入端口位号</param>
        /// <returns></returns>
        public override bool DoGetAxisHRevIn(int naxis, ref int inputno)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                   
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取硬负限位端口失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的负向软限位值
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="fvalue">负限位值</param>
        /// <returns></returns>
        public override bool DoSetAxisSRevValue(int naxis, float fvalue)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxSetSoftMinLimit(fvalue, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置软负限位值失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的原点硬限位信号端口位
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="inputno">输入端口位号</param>
        /// <returns></returns>
        public override bool DoSetAxisDatumIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    //根据汇众智慧板卡InPut电平高低、对应数字以及逻辑定义:低电平-数字1-逻辑true,高电平-数字0-逻辑false
                    ushort orglimitlev = (level == ProCommon.Communal.ElectricalLevel.Low) ? (ushort)0 : (ushort)1;
                    if (hzaux.HZ_AxSetOrgLimitlevel((ushort)inputno, orglimitlev, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置硬原点限位端口失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定轴的原点硬限位信号端口位
        /// [注:无此功能]
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="inputno">输入端口位号</param>
        /// <returns></returns>
        public override bool DoGetAxisDatumIn(int naxis, ref int inputno)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取硬原点限位端口失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的正向硬限位信号端口位及有效电平
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="inputno">输入端口位号</param>
        /// <returns></returns>
        public override bool DoSetAxisHFwdIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    //根据汇众智慧板卡InPut电平高低、对应数字以及逻辑定义:低电平-数字1-逻辑true,高电平-数字0-逻辑false
                    ushort poslimitlev = (level==ProCommon.Communal.ElectricalLevel.Low)?(ushort)0: (ushort)1;
                    if (hzaux.HZ_AxSetPosLimitlevel((ushort)inputno, poslimitlev, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置硬正限位端口失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定轴的正向硬限位信号端口位
        /// [注:无此功能]
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="inputno">输入端口位号</param>
        /// <returns></returns>
        public override bool DoGetAxisHFwdIn(int naxis, ref int inputno)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                   
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取硬正限位端口失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的正向软限位值
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="fvalue">正限位值</param>
        /// <returns></returns>
        public override bool DoSetAxisSFwdValue(int naxis, float fvalue)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxSetSoftMaxLimit(fvalue, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置软正限位值失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定输入端口位的有效电平
        /// [注:无此功能]
        /// </summary>
        /// <param name="inputno">输入位</param>
        /// <param name="level"></param>
        /// <returns></returns>
        public override bool DoSetPortInEffectiveLevel(int inputno, ProCommon.Communal.ElectricalLevel level)
        {
            bool rt = false;
            int lv = (level == ProCommon.Communal.ElectricalLevel.High) ? 1 : 0;
            try
            {
                if (hzaux != null)
                {
                   
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置端口有效电平失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的脉冲当量
        /// 脉冲当量:移动单位用户定义量所对应的脉冲数，
        /// 比如移动单位m(dm,cm,mm)位移所对应的脉冲数
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="units">脉冲当量</param>
        /// <returns></returns>
        public override bool DoSetAxisUnits(int naxis, float units)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxSetUnits(units, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴脉冲当量失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的爬行速度
        /// [注:无此功能]
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="creep">爬行速度</param>
        /// <returns></returns>
        public override bool DoSetAxisCreep(int naxis, float creep)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                   
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴爬行速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置轴的T形运控参数
        /// [注:无此功能]
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="lspeed">初始速度</param>
        /// <param name="maxspeed">最大速度</param>
        /// <param name="tacc">加速时间(秒)</param>
        /// <param name="tdec">减速时间(秒)</param>
        /// <returns></returns>
        public override bool DoSetAxisTrapeziumPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec)
        {
            bool rt = false;
            if (this.SetAxisSramp(naxis, 0.0f))  //设置运控曲线0--Trapezium,1.0--Sigmoid
            {
                if (this.SetAxisInitspeed(naxis, lspeed))
                {
                    if (this.SetAxisSteadySpeed(naxis, maxspeed))
                    {
                        float accel = (maxspeed - lspeed) / tacc;
                        if (this.SetAxisAccel(naxis, accel))
                        {
                            if (this.SetAxisDecel(naxis, accel))
                                rt = true;
                        }
                    }
                }
            }

            return rt;
        }

        /// <summary>
        /// 设置指定端口模拟量
        /// [注:无此功能]
        /// </summary>
        /// <param name="ionum"></param>
        /// <param name="fValue"></param>
        /// <returns></returns>
        public override bool DoSetDA(int ionum, float fValue)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                   
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴主轴速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置轴的S形运控参数
        /// [注:无此功能]
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="lspeed">初始速度</param>
        /// <param name="maxspeed">最大速度</param>
        /// <param name="tacc">T段加速时间(秒)</param>
        /// <param name="tdec">T段减速时间(秒)</param>
        /// <param name="sacc">S段加速时间(秒)</param>
        /// <param name="sdec">S段减速时间(秒)</param>
        /// <returns></returns>
        public override bool DoSetAxisSigmoidPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec, int sacc, int sdec)
        {
            bool rt = false;
            if (this.SetAxisSramp(naxis, 1.0f))  //设置运控曲线0--Trapezium,1.0--Sigmoid
            {
                if (this.SetAxisInitspeed(naxis, lspeed))
                {
                    if (this.SetAxisSteadySpeed(naxis, maxspeed))
                    {
                        float accel = (maxspeed - lspeed) / tacc;
                        if (this.SetAxisAccel(naxis, accel))
                        {
                            if (this.SetAxisDecel(naxis, accel))
                                rt = true;
                        }
                    }
                }
            }

            return rt;
        }

        /// <summary>
        /// 设置指定轴的起始速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="lspeed">起始速度</param>
        /// <returns></returns>
        public bool SetAxisInitspeed(int naxis, float lspeed)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxSetStartSpeed(lspeed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴初始速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的起始速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="lspeed">起始速度</param>
        /// <returns></returns>
        public bool SetAxisInitspeed(int naxis, int lspeed)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxSetStartSpeed(lspeed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴初始速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的运动速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="speed">运动速度</param>
        /// <returns></returns>
        public bool SetAxisSteadySpeed(int naxis, float speed)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxSetSpeed(speed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴运行速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的运动速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="speed">运动速度</param>
        /// <returns></returns>
        public bool SetAxisSteadySpeed(int naxis, int speed)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxSetSpeed(speed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴运行速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的加速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="accel">加速度</param>
        /// <returns></returns>
        public bool SetAxisAccel(int naxis, float accel)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    int acc = Convert.ToInt32(accel);                   
                    if (hzaux.HZ_AxSetAcecl(acc, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴加速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的减速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="decel">减速度</param>
        /// <returns></returns>
        public bool SetAxisDecel(int naxis, float decel)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    int dec = Convert.ToInt32(decel);
                    if (hzaux.HZ_AxSetDecel(dec, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴减速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的末速度
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SetAxisEndSpeed(int naxis, float speed)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxSetEndSpeed(speed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴末速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的末速度
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SetAxisEndSpeed(int naxis, int speed)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxSetEndSpeed(speed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴末速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的运行速度曲线
        /// [注:无此功能]
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="sramp">速度曲线</param>
        /// <returns></returns>
        public bool SetAxisSramp(int naxis, float sramp)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                   
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴速度曲线失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置回零运动的速度
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SetAxisFindDatumSpeed(int naxis, float speed)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxHome(speed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴回零速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置回零运动的速度
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SetAxisFindDatumSpeed(int naxis, int speed)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxHome(speed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴回零速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置快速回零运动的速度
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SetAxisFindDatumFastSpeed(int naxis, float speed)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxSetHomeFastSpeed(speed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴快速回零速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置快速回零运动的速度
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SetAxisFindDatumFastSpeed(int naxis, int speed)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxSetHomeFastSpeed(speed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴快速回零速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置慢速回零运动速度
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SetAxisFindDatumLowSpeed(int naxis, float speed)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxSetHomeSlowSpeed(speed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴慢速回零速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置慢速回零运动速度
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SetAxisFindDatumLowSpeed(int naxis, int speed)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxSetHomeSlowSpeed(speed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴慢速回零速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的回零模式
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public bool SetAxisFindDatumMode(int naxis, int mode)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxSetHomMod((ushort)mode, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴回零模式失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴在指定速度下回零
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool AxisFindDatum(int naxis,int speed)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxHome(speed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴回零失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴在指定速度下回零
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool AxisFindDatum(int naxis, float speed)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxHome(speed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴回零失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 指定轴根据设定的方向和模式回零
        /// [注:无此功能]
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="moveDir"></param>
        /// <param name="mode">
        /// 0-负方向找原点;
        /// 1-先正方向运行到达正限位,然后反方向运行直到原点限位;
        /// 2-先负方向运行到达负限位,然后反方向运行直到原点限位;</param>
        /// <returns></returns>
        public override bool DoFindDatum(int naxis, ProCommon.Communal.MoveDir moveDir, int mode)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡轴回零失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴回零偏移量
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool SetAxisFindDatumOffSet(int naxis, float offset)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxSetHomeOffset(offset, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴回零位移偏移量失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 指定轴在指定方向连续运动
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="movedir">方向</param>
        /// <returns></returns>
        public override bool DoSingleContinueMove(int naxis, ProCommon.Communal.MoveDir movedir)
        {
            bool rt = false;
            int dir = (movedir == ProCommon.Communal.MoveDir.BackWard) ? -1 : 1;
            try
            {
                if (hzaux != null)
                {
                   
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡轴连续运动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 指定轴相对运动
        /// [注:汇众智慧卡此处速度应非零]
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="pos"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public override bool DoSingleRelMove(int naxis, float pos, float speed = 0.0f)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxMoveRel(pos, speed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡轴相对运动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 指定轴绝对运动
        /// [注:汇众智慧卡此处速度应非零]
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="pos">绝对位置</param>
        /// <returns></returns>
        public override bool DoSingleAbsMove(int naxis, float pos,float speed = 0.0f)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxMoveAbs(pos, speed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡轴绝对运动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 指定轴在指定速度下移动指定位移
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="pos"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SingleConstantMove(int naxis, float pos, float speed)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxMoveVelocity(pos, speed, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡轴指定速度运动指定位移指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 轴列表运动按指定模式取消
        /// </summary>
        /// <param name="axes"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public override bool DoCancelAxisList(int[] axes,int mode)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡取消轴列表运动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 指定轴按指定模式停止
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="mode">停止模式：0-立即停止，1-减速停止</param>
        /// <returns></returns>
        public override bool DoSingleCancelMove(int naxis, int mode)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    switch (mode)
                    {
                        case 0:
                            if (hzaux.HZ_AxStop((ushort)naxis) == 0)
                            {
                                rt = true;
                            }
                            break;
                        case 1:
                            if (hzaux.HZ_AxStopDec((ushort)naxis) == 0)
                            {
                                rt = true;
                            }
                            break;
                        default:break;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡轴停止运动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的位置
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="curpos">当前位置</param>
        /// <returns></returns>
        public override bool DoSetAxisCurPos(int naxis, float curpos)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                   
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴当前位置指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定轴的位置
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="curpos">当前位置</param>
        /// <returns></returns>
        public override bool DoGetAxisCurPos(int naxis, ref float curpos)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_AxGetCurPos(ref curpos, (ushort)naxis) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取轴当前位置指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定轴当前的运动速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="curspeed">当前的运动速度</param>
        /// <returns></returns>
        public override bool DoGetAxisCurspeed(int naxis, ref float curspeed)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                   
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取轴当前运行速度指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 检查指定轴是否停止
        /// [注:汇众智慧卡无此功能]
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="stopped">是否停止</param>
        /// <returns></returns>
        public override bool DoChekcAxisIfStop(int naxis, ref bool stopped)
        {
            bool rt = false;           
            try
            {
                if (hzaux != null)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡检查轴是否停止指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定轴的轴状态
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="axisStatus">轴状态
        /// 0-就绪,1-停止,2-减速停,3-普通运动,4-连续运动
        /// 5-正在回原点,6-未激活状态,7-错误停止,8-轴同步状态</param>
        /// <returns></returns>
        public override bool DoGetAxisStatus(int naxis, ref int axisStatus)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    short sta = (short)axisStatus;
                    if (hzaux.HZ_AxGetStatus(ref sta, (ushort)naxis) == 0)
                    {
                        axisStatus = sta;
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取轴状态指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取硬件版本
        /// </summary>
        /// <param name="HardWareVer"></param>
        /// <returns></returns>
        public bool GetHardWareVer(ref int[] HardWareVer)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_GetHardWareVer(ref HardWareVer) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取硬件版本指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取软件版本
        /// </summary>
        /// <param name="softVer"></param>
        /// <returns></returns>
        public bool GetSoftWareVer(ref int[] softVer)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_GetSoftWareVer(ref softVer) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取软件版本指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 检查指定轴是否正常
        /// [注:汇众智慧卡无此功能]
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="normal">是否正常</param>
        /// <returns></returns>
        public override bool DoCheckAxisIfNormal(int naxis, ref bool normal)
        {
            bool rt = false;
            try
            {
                int axisStatus = 0;
                if (GetAxisStatus(naxis, ref axisStatus))
                {
                    normal = (axisStatus == 0) ? true : false;
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡检查轴是否正常指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 轴列表运动按指定模式立即停止
        /// </summary>
        /// <param name="mode">停止模式，0-当前运动，1-缓存运动，2-当前运动和缓存运动</param>
        /// <returns></returns>
        public override bool DoRapidStop(int mode)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡列表轴紧急停止指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 2轴直线插补运动
        /// </summary>      
        /// <param name="axespos">目标位置</param>
        /// <param name="mode">运行模式
        /// 0-相对位置运动,1-绝对位置运动</param>
        /// <returns></returns>
        public override bool DoLine2Move(float[] axespos, int mode)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    //相对位置运动
                    if (mode == 0)
                    {
                       
                    }
                    else  //绝对位置运动
                    {
                       
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡列表轴2轴直线插补运动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 三点定圆的2轴圆弧插补运动
        /// </summary>      
        /// <param name="midpos">中间点坐标</param>
        /// <param name="dstpos">目标点坐标</param>
        /// <param name="mode">运行模式
        /// 0-相对位置运动,1-绝对位置运动</param>
        /// <returns></returns>
        public override bool DoPointsBasedArc2Move(float[] midpos, float[] dstpos, int mode)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    //相对位置运动
                    if (mode == 0)
                    {
                        
                    }
                    else  //绝对位置运动
                    {
                       
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡列表轴三点定圆2轴圆弧插补运动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 圆心定圆的2轴圆弧插补运动
        /// </summary>     
        /// <param name="dstpos">终点相对起始点坐标</param>
        /// <param name="cenpos">圆心相对起始点坐标</param>
        /// <param name="dir">方向
        /// 0-顺时针,1-逆时针</param>
        /// <param name="mode">运行模式
        /// 0-相对位置模式,1-绝对位置模式</param>
        /// <returns></returns>
        public override bool DoCenterBasedArc2Move(float[] dstpos, float[] cenpos, int dir, int mode)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    //相对位置运动
                    if (mode == 0)
                    {
                        switch (dir)
                        {
                            case 0:                               
                                break;
                            default:
                               
                                break;
                        }

                    }
                    else  //绝对位置运动
                    {
                        switch (dir)
                        {
                            case 0:                               
                                break;
                            default:                               
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡列表轴圆心定圆2轴圆弧插补运动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的减速运动模式
        /// [插补运动时，只需要设置主轴]
        /// </summary>
        /// <param name="axisNum">轴号</param>
        /// <param name="deceleratemode">减速模式
        /// 2-拐角减速
        /// 4-小圆减速
        /// 32-倒角</param>
        /// <returns></returns>
        public override bool DoSetCornerDecelerateMode(int axisNum, int deceleratemode)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                   
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置指定轴减速模式指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的减速角范围
        /// [小于减速角下限时:不减速
        /// 在减速角范围内:由指定速度开始线性减速到指定低速
        /// 大于减速角上限时:完全限速为指定低速]
        /// </summary>
        /// <param name="axisNum"></param>
        /// <param name="decelanglerange"></param>
        /// <returns></returns>
        public override bool DoSetCornerDecelerateAngleRange(int axisNum, float[] decelanglerange)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                   
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置指定轴减速角范围指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }


        /// <summary>
        /// 设置指定轴的减速半径
        /// [当小于该半径时,线性减速;插补运动时，设置主轴]
        /// </summary>
        /// <param name="axisNum"></param>
        /// <param name="decelradius"></param>
        /// <returns></returns>
        public override bool DoSetCornerDecelerateRadius(int axisNum, float decelradius)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                   
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置指定轴减速半径指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的倒角半径
        /// [插补运动时，设置主轴]
        /// </summary>
        /// <param name="axisNum"></param>
        /// <param name="smoothradius"></param>
        /// <returns></returns>
        public override bool DoSetCornerSoomthRadius(int axisNum, float smoothradius)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                   
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置指定轴倒角半径指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }


        /// <summary>
        /// 指定输出端口位输出逻辑值
        /// [注:汇众智慧卡待处理]
        /// </summary>
        /// <param name="nbit">输出端口位</param>
        /// <param name="onoff"></param>
        /// <returns></returns>
        public override bool DoSetOutBitLogicValue(int nbit, bool onoff)
        {
            bool rt = false;
            uint v = (onoff) ? (uint)1 : 0; //此处逻辑的定义由汇众智慧控制卡定义:0-false,1-true
            try
            {
                if (hzaux.HZ_SetOutputStata((ushort)nbit) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置输出位指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }       

        /// <summary>
        /// 获取指定输出位的逻辑值
        /// </summary>
        /// <param name="nbit">输出端口位</param>
        /// <param name="onoff">逻辑值</param>
        /// <returns></returns>
        public override bool DoGetOutBitLogicValue(int nbit, ref bool onoff)
        {
            bool rt = false;
            onoff = false;
            int v = 0;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_GetOutputStata(ref v, (ushort)nbit) == 0)
                    {
                        ///此处逻辑值的定义由汇众智慧控制卡定义:0-false,1-true
                        onoff = (v == 1) ? true : false;
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取输出位指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }
      

        /// <summary>
        /// 获取指定输入位的电平
        /// </summary>
        /// <param name="nbit">输入位号</param>
        /// <param name="onoff">当前值</param>
        /// <returns></returns>
        public override bool DoGetInBitLogicValue(int nbit, ref bool onoff)
        {
            bool rt = false;
            onoff = false;
            int v = 0;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_GetInputStata(ref v, (ushort)nbit) == 0)
                    {
                        //此处逻辑值的定义由汇众智慧控制卡定义:0-false,1-true
                        onoff = (v == 1) ? true : false;
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取输入位指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取错误码
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public bool GetErrorCode(ref int[] errorCode)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_GetErrorCode(ref errorCode) == 0)
                    {
                        rt = true;
                    }
                }               
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取错误码指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }
        
        /// <summary>
        /// 获取错误等级
        /// </summary>
        /// <param name="errorLevel"></param>
        /// <returns></returns>
        public bool GetErrorLevel(ref int errorLevel)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_GetErrorLevel(ref errorLevel) == 0)
                    {
                        rt = true;
                    }
                }               
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取错误等级指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取机器时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool GetMachineTime(ref byte[] time)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_GetMachineTime(ref time) == 0)
                    {
                        rt = true;
                    }
                }               
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取机器时间指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取机器日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool GetMachineDate(ref byte[] date)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_GetMachineData(ref date) == 0)
                    {
                        rt = true;
                    }
                }                
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取机器日期指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取机器ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GetMachineID(ref int[] id)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_GetMachineCID(ref id) == 0)
                    {
                        rt = true;
                    }
                }               
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取机器ID指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取运行状态
        /// </summary>
        /// <param name="state">
        /// 0-初始,1-停止,2-运行,3-复位,4-急停,5-暂停</param>
        /// <returns></returns>
        public bool GetRunStatus(ref int state)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_GetRunStatus(ref state) == 0)
                    {
                        rt = true;
                    }
                }               
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取运行状态指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 板卡写闪存Flash
        /// </summary>
        /// <returns></returns>
        public bool WriteFlash()
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_WriteFlash() == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡写闪存指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置点动模式
        /// </summary>
        /// <param name="mode">
        /// 0-数量级,2-指定脉冲数,3-连续模式</param>
        /// <returns></returns>
        public bool SetJogMode(int mode)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    if (hzaux.HZ_SetJogMode(mode) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置点动模式指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }
       

        /// <summary>
        /// 等待指定轴找原点
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="waitfordatum">是否等待轴回原点的限位信号</param>
        /// <param name="sleepsecond">轮询间隔</param>
        /// <param name="timeout">超时</param>
        /// <param name="enablepause">允许暂停</param>
        /// <param name="limitdistance">限制距离</param>
        /// <param name="specifiedpos">指定位置</param>
        /// <returns></returns>
        public override bool DoWaitForAxisFindDatum(int naxis, bool waitfordatum = true, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true, float limitdistance = -1, float specifiedpos = 0.0f)
        {
            bool rt = false;    //正常执行:true,否则false
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Reset();
            stopWatch.Start();
            int inputno = 0;
            bool onoff = false;
            bool axisInportGetted = false;

            while (true)
            {
                int axisStatus = 0;
                if (!DoGetAxisStatus(naxis, ref axisStatus))
                {
                    break; //获取轴状态异常，则退出
                }
                else
                {
                    if (axisStatus == 4194304)
                    {
                        break; //告警输入，则退出
                    }
                }

                //等待硬原点信号
                if (waitfordatum)
                {
                    if (!axisInportGetted)
                        if (!DoGetAxisDatumIn(naxis, ref inputno))
                            break;

                    if (!DoGetInBitLogicValue(inputno, ref onoff))
                    {
                        break;
                    }
                    else
                    {
                        if (onoff)
                        { rt = true; break; }
                    }
                }
                else //不等待硬原点信号
                {
                    rt = true;
                    break;
                }

                //启用限制距离，超时时间内，未找到硬原点信号时
                //若当前位置与指定的预期位置偏差超出限制距离，则是出现异常
                if (limitdistance > 0)
                {
                    float currpos = 0;
                    if (this.GetAxisCurPos(naxis, ref currpos))
                    {
                        if (Math.Abs(currpos - specifiedpos) > limitdistance)
                        {
                            SingleCancelMove(naxis, 2);
                            System.Windows.Forms.MessageBox.Show("当前轴[" + naxis + "]复位异常", "错误提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        }
                    }
                    else
                    {
                        //获取轴当前位置异常，则退出
                        break;
                    }
                }

                if (stopWatch.ElapsedMilliseconds >= (timeout * 1000))
                {
                    break; //超时，退出等待
                }

                System.Threading.Thread.Sleep((int)(sleepsecond * 1000));
            }

            stopWatch.Stop();
            //返回
            return rt;
        }

        /// <summary>
        /// 等待指定轴停止
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="sleepsecond">轮询间隔</param>
        /// <param name="timeout">超时</param>
        /// <param name="enablepause">允许暂停</param>
        /// <returns></returns>
        public override bool DoWaitForAxisStop(int naxis, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true)
        {
            bool rt = false;    //正常执行:true,否则false
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Reset();
            stopWatch.Start();
            while (true)
            {
                bool stopped = false;
                if (DoChekcAxisIfStop(naxis, ref stopped))
                {
                    if (stopped)
                    {
                        rt = true;
                        break;
                    }
                }
                else
                {
                    break; //查看轴是否停止异常，则退出
                }

                if (stopWatch.ElapsedMilliseconds >= (timeout * 1000))
                {
                    break; //超时，退出等待
                }
                System.Threading.Thread.Sleep((int)(sleepsecond * 1000));
            }
            stopWatch.Stop();
            //返回
            return rt;
        }

        /// <summary>
        /// 等待指定轴到达指定类型的限位
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="limittype">限位类型</param>
        /// <param name="waitforstatus">是否等待轴到达指定类型的限位信号</param>
        /// <param name="sleepsecond">轮询间隔</param>
        /// <param name="timeout">超时</param>
        /// <param name="enablepause">允许暂停</param>
        /// <returns></returns>
        public override bool DoWaitForAxisLimit(int naxis, LimitType limittype, bool waitforstatus = true, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true)
        {
            bool rt = false;    //正常执行:true,否则false
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Reset();
            stopWatch.Start();
            int inputno = 0;
            bool onoff = false;
            bool axisInportGetted = false;

            while (true)
            {
                int curraxisstatus = 0;
                if (!this.GetAxisStatus(naxis, ref curraxisstatus))
                {
                    break; //获取轴状态异常，退出
                }
                else
                {
                    if (curraxisstatus != 0)
                    {
                        break; //轴状态异常，则退出
                    }
                }

                if (waitforstatus)
                {
                    if (!axisInportGetted)
                    {
                        switch (limittype)
                        {
                            case LimitType.FwdLimit:
                                if (!DoGetAxisHFwdIn(naxis, ref inputno))
                                    goto LBrk;
                                break;
                            case LimitType.NegaLimit:
                                if (!DoGetAxisHRevIn(naxis, ref inputno))
                                    goto LBrk;
                                break;
                        }
                    }

                    if (!DoGetInBitLogicValue(inputno, ref onoff))
                    {
                        break;
                    }
                    else
                    {
                        if (onoff)
                        { rt = true; break; }
                    }
                }
                else
                {
                    rt = true;
                    break;
                }

                if (stopWatch.ElapsedMilliseconds >= (timeout * 1000))
                {
                    break; //超时，退出等待
                }

                System.Threading.Thread.Sleep((int)(sleepsecond * 1000));
            }
            stopWatch.Stop();
        //返回
        LBrk: return rt;
        }

        public override string ToString()
        {
            return "BoardDriver[ZMotion]";
        }

        //--------------👇驱动器专用接口信号👇------------------//
        //----------------******待完善*****-------------------//       

        /// <summary>
        /// 设置指定轴的减速信号有效电平
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="enable">轴减速信号使能
        /// 0,禁止
        /// 1,使能</param>
        /// <param name="sdlevel">电平
        /// 0,低电平
        /// 1,高电平</param>
        /// <param name="sdmode">减速停止模式
        /// 0,减速到起始速度,如果SD信号丢失,则又加速
        /// 1,减速到起始速度,并停止;如果SD信号丢失,则又加速
        /// 2,锁存SD信号,并减速到起始速度
        /// 3,锁存SD信号,并减速到起始速度后停止</param>
        /// <returns></returns>
        public override bool DoSetAxisSDEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel sdlevel, uint sdmode)
        {
            bool rt = false;
            uint v = (sdlevel == ProCommon.Communal.ElectricalLevel.High) ? (uint)1 : 0; //此处有效电平的高低的定义由正运动控制卡定义:0-低电平,1-高电平
            try
            {
                if (hzaux != null)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置指定轴的减速信号有效电平指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴位置改变信号有效电平
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="enable">位置改变信号使能
        /// 0,禁止
        /// 1,使能</param>
        /// <param name="pcslevel">
        /// 有效逻辑电平
        /// 0,低电平
        /// 1,高电平</param>
        /// <returns></returns>
        public override bool DoSetAxisPCSEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel pcslevel)
        {
            bool rt = false;
            uint v = (pcslevel == ProCommon.Communal.ElectricalLevel.High) ? (uint)1 : 0; //此处有效电平的高低的定义由正运动控制卡定义:0-低电平,1-高电平
            try
            {
                if (hzaux != null)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置指定轴的位置改变信号有效电平指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的到位信号有效电平
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="enable">使能轴到位信号
        /// 0,禁止
        /// 1,使能</param>
        /// <param name="inplevel">有效电平
        /// 0，低电平
        /// 1，高电平</param>
        /// <returns></returns>
        public override bool DoSetAxisINPEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel inplevel)
        {
            bool rt = false;
            uint v = (inplevel == ProCommon.Communal.ElectricalLevel.High) ? (uint)1 : 0; //此处有效电平的高低的定义由正运动控制卡定义:0-低电平,1-高电平
            try
            {
                if (hzaux != null)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置指定轴的到位信号有效电平指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的误差清除信号有效电平
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="enable">清除误差信号使能
        /// 0,不自动输出ERC信号
        /// 1,接收EL,ALM,CEMG等信号停止时,自动输出ERC信号
        /// 2,接收ORG信号时,自动输出ERC信号
        /// 3,满足第1或2亮相条件时,均自动输出ERC信号</param>
        /// <param name="erclevel">有效电平
        /// 0,低电平
        /// 1,高电平</param>
        /// <param name="ercwidth">误差清除信号有效输出宽度
        /// 0,12us
        /// 1,102us
        /// 2,409us
        /// 3,1.6ms
        /// 4,13ms
        /// 5,52ms
        /// 6,104ms
        /// 7,电平输出</param>
        /// <param name="ercofftime">ERC信号的关断时间
        /// 0,0us
        /// 1,12us
        /// 2,1.6ms
        /// 3,104ms</param>
        /// <returns></returns>
        public override bool DoSetAxisERCEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel erclevel, uint ercwidth, uint ercofftime)
        {
            bool rt = false;
            uint v = (erclevel == ProCommon.Communal.ElectricalLevel.High) ? (uint)1 : 0; //此处有效电平的高低的定义由正运动控制卡定义:0-低电平,1-高电平
            try
            {
                if (hzaux != null)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置指定轴的误差清除信号有效电平指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的ERC信号状态
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="sel">ERC信号状态
        /// 0,复位ERC信号
        /// 1,输出ERC信号</param>
        /// <returns></returns>
        public override bool DoSetAxisERC(int naxis, uint sel)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置指定轴的误差信号指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的报警信号有效电平
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="almlevel">有效电平
        /// 0，低电平
        /// 1，高电平</param>
        /// <param name="actionmode">轴报警后制动方式
        /// 0,立即停止
        /// 1,减速停止</param>
        /// <returns></returns>
        public override bool DoSetAxisALMEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel almlevel, uint actionmode)
        {
            bool rt = false;
            uint v = (almlevel == ProCommon.Communal.ElectricalLevel.High) ? (uint)1 : 0; //此处有效电平的高低的定义由正运动控制卡定义:0-低电平,1-高电平
            try
            {
                if (hzaux != null)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置指定轴的报警信号有效电平指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的EZ信号有效电平
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="ezlevel">有效电平
        /// 0,低电平
        /// 1,高电平</param>
        /// <param name="actionmode">EZ信号有效的工作方式
        /// 0,EZ信号无效
        /// 1,EZ是计数器复位信号
        /// 2,EZ是原点信号，且不复位计数器
        /// 3,EZ是原点信号，且复位计数器</param>
        /// <returns></returns>
        public override bool DoSetAxisEZEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ezlevel, uint actionmode)
        {
            bool rt = false;
            uint v = (ezlevel == ProCommon.Communal.ElectricalLevel.High) ? (uint)1 : 0; //此处有效电平的高低的定义由正运动控制卡定义:0-低电平,1-高电平
            try
            {
                if (hzaux != null)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置指定轴的编码器复位信号逻辑电平指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的轴锁存信号有效电平
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="ltclevel">有效电平
        /// 0,低电平
        /// 1,高电平</param>
        /// <param name="actionmode">保留,可以为任意值</param>
        /// <returns></returns>
        public override bool DoSetAxisLTCEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ltclevel, uint actionmode)
        {
            bool rt = false;
            uint v = (ltclevel == ProCommon.Communal.ElectricalLevel.High) ? (uint)1 : 0; //此处有效电平的高低的定义由正运动控制卡定义:0-低电平,1-高电平
            try
            {
                if (hzaux != null)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置指定轴的锁存信号有效电平指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的限位信号有效电平和制动方式
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="actionmode">有效电平和制动方式
        /// 0,立即停止,低电平有效
        /// 1,减速停止,低电平有效
        /// 2,立即停止,高电平有效
        /// 3,减速停止,高电平有效</param>
        /// <returns></returns>
        public override bool DoSetAxisELEffectiveLevel(int naxis, uint actionmode)
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置指定轴的限位信号有效电平及制动方式指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴原点限位信号有效电平
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="datumlevel">有效逻辑电平
        /// 0,低电平
        /// 1,高电平</param>
        /// <param name="filter">滤波功能使能信号
        /// 0,禁止
        /// 1,使能</param>
        /// <returns></returns>
        public override bool DoSetAxisDatumEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel datumlevel, uint filter)
        {
            bool rt = false;
            uint v = (datumlevel == ProCommon.Communal.ElectricalLevel.High) ? (uint)1 : 0; //此处有效电平的高低的定义由正运动控制卡定义:0-低电平,1-高电平
            try
            {
                if (hzaux != null)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置指定轴的原点限位信号有效电平指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的伺服使能信号
        /// [注:各个轴的伺服使能信号由内部定义的输出口提供]
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="value">
        /// 0-低电平,1-高电平</param>
        /// <returns></returns>
        public override bool DoSetAxisServo(int naxis, bool onoff)
        {
            bool rt = false;
            uint v = onoff ? (uint)1 : 0; //此处逻辑值的定义由正运动控制卡定义:0-false,1-true
            try
            {
                if (hzaux != null)
                {
                    switch (naxis)
                    {
                        case 0:                           
                            break;
                        case 1:                          
                            break;
                        case 2:                           
                            break;
                        case 3:                           
                            break;
                        case 4:                           
                            break;
                        case 5:                           
                            break;
                        case 6:                           
                            break;
                        case 7:                           
                            break;
                        default: break;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置指定轴的伺服使能信号指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的紧急停止信号有效电平
        /// </summary>       
        /// <param name="enable">紧急停止信号使能
        /// 0,禁止
        /// 1,使能</param>
        /// <param name="emglevel">有效电平
        /// 0,低电平
        /// 1,高电平</param>
        /// <returns></returns>
        public override bool DoSetAxisEMGEffectiveLevel(uint enable, ProCommon.Communal.ElectricalLevel emglevel)
        {
            bool rt = false;
            uint v = (emglevel == ProCommon.Communal.ElectricalLevel.High) ? (uint)1 : 0; //此处有效电平的高低的定义由正运动控制卡定义:0-低电平,1-高电平
            try
            {
                if (hzaux != null)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置指定轴的紧急停止信号有效电平指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        //--------------👆驱动器专用接口信号👆------------------//

        #endregion

        #region 编码器控制或设置
        /// <summary>
        /// 获取编码器状态
        /// </summary>
        /// <param name="code">编码器值，长度5</param>
        /// <param name="num">编码器编号</param>
        /// <returns></returns>
        public bool AxGetEncoder(ref int[] code, ushort num)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxGetEncoder(ref code, num) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取编码器状态指令失败!\n异常描述:{0}", ex.Message));              
            }
            return rt;
        }
        #endregion

        #region 计数器控制或设置
        #endregion
    }

    */

    #endregion

    public class BoardDriver_HZZH
    {
        public DriverExceptionOccuredDel ExceptionOccuredDel;
        HZZH.Card.Controller hzaux;

        public BoardDriver_HZZH()
        {
            hzaux = new HZZH.Card.Controller();
            ExceptionOccuredDel = new DriverExceptionOccuredDel(OnDirverExceptionOccured);
        }

        private void OnDirverExceptionOccured(string err)
        {
            //什么都不做
        }

        #region 实现抽象函数

        /// <summary>
        /// 连接控制器
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool DoConnectCtrller(string ip, int port)
        {
            bool rt = false;
            try
            {
                hzaux.HZ_InitializeUdp(ip, port);
                rt = true;
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡启连失败!\n异常描述:{0}", ex.Message));
            }

            return rt;
        }

        /// <summary>
        /// 控制器的连接状态
        /// [注:状态是如何更新的？]
        /// </summary>
        /// <returns></returns>
        public bool DoIsConnected()
        {
            return hzaux.Succeed;
        }

        /// <summary>
        /// 断连控制器
        /// </summary>
        /// <returns></returns>
        public bool DoDisconnectCtrller()
        {
            bool rt = false;
            try
            {
                if (hzaux != null)
                {
                    hzaux.Dispose();
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡断连失败!\n异常描述:{0}", ex.Message));
            }

            return rt;
        }

        /// <summary>
        /// 设定轴限位模式
        /// </summary>
        /// <param name="limitMode">限位模式参数：0无限位，1软限位，2硬限位，3软硬都限位</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool DoSetLimitMode(ushort limitMode, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetLimitMode(limitMode, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设定轴限位模式失败!\n异常描述:{0}", ex.Message));
            }
            return rt;

        }

        /// <summary>
        /// 设置轴正限位信号和电平
        /// </summary>
        /// <param name="poslimit">正限位信号：配置限位端口，0、1、2..........等</param>
        /// <param name="poslimitlev">正限位信号电平：0低电平，1高电平</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool DoSetPosLimitlevel(ushort poslimit, ushort poslimitlev, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetPosLimitlevel(poslimit, poslimitlev, naxis) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴正限位信号和电平失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置轴负限位信号和电平
        /// </summary>
        /// <param name="neglimit">负限位信号：配置限位端口，0、1、2..........等</param>
        /// <param name="neglimitlev">负限位信号电平：0低电平，1高电平</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool DoSetNegsLimitlevel(ushort neglimit, ushort neglimitlev, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetNegsLimitlevel(neglimit, neglimitlev, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴负限位信号和电平失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置轴原点信号和电平
        /// </summary>
        /// <param name="orgNum">原点信号：配置限位端口，0、1、2..........等</param>
        /// <param name="orglev">原点信号电平：0低电平，1高电平</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool DoSetOrgLimitlevel(ushort orgNum, ushort orglev, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetOrgLimitlevel(orgNum, orglev, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴原点限位信号和电平失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置轴回零模式
        /// </summary>
        /// <param name="homeMode">回零模式：0：反向找原点；1：先正向找上限位，再反向找原点；2：先反向找下限位，再正向找原点</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool DoSetHomMod(ushort homeMode, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetHomMod(homeMode, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴回零模式失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置轴报警模式
        /// </summary>
        /// <param name="alarmmode">轴报警电平：</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool DoSetAlarmlevel(ushort alarmmode, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetAlarmlevel(alarmmode, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴报警模式失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置轴回零
        /// </summary>
        /// <param name="speed">回零速度，脉冲量</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool DoHome(int speed, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxHome(speed, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴回零失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置轴回零
        /// </summary>
        /// <param name="speed">回零速度，毫米量</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool DoHome(float speed, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxHome(speed, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴回零失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }




        /// <summary>
        /// 设置指定轴的脉冲当量
        /// 脉冲当量:移动单位用户定义量所对应的脉冲数，
        /// 比如移动单位m(dm,cm,mm)位移所对应的脉冲数
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="units">脉冲当量</param>
        /// <returns></returns>
        public bool DoSetnaxisUnits(float units, int naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetUnits(units, (ushort)naxis) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴脉冲当量失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }


        /// <summary>
        /// 设置指定轴的起始速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="lspeed">起始速度：脉冲量</param>
        /// <returns></returns>
        private bool SetnaxisInitspeed(int lspeed, int naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetStartSpeed(lspeed, (ushort)naxis) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴初始速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的起始速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="lspeed">起始速度:毫米量</param>
        /// <returns></returns>
        private bool SetnaxisInitspeed(float lspeed, int naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetStartSpeed(lspeed, (ushort)naxis) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴初始速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的运动速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="speed">运动速度：脉冲量</param>
        /// <returns></returns>
        private bool SetnaxisSteadySpeed(int speed, int naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetSpeed(speed, (ushort)naxis) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴运行速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的运动速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="speed">运动速度：毫米量</param>
        /// <returns></returns>
        private bool SetnaxisSteadySpeed(float speed, int naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetSpeed(speed, (ushort)naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴运行速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的加速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="accel">加速度</param>
        /// <returns></returns>
        private bool SetnaxisAccel(int naxis, float accel)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetAcecl((int)accel, (ushort)naxis) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴加速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的减速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="decel">减速度</param>
        /// <returns></returns>
        private bool SetnaxisDecel(int naxis, float decel)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetDecel((int)decel, (ushort)naxis) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴减速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }


        /// <summary>
        /// 设置轴末速度
        /// </summary>
        /// <param name="End">末速度,脉冲量</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetAxEndSpeed(int End, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetEndSpeed(End, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴末速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置轴末速度
        /// </summary>
        /// <param name="End">末速度,毫米量</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetAxEndSpeed(float End, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetEndSpeed(End, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴末速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置轴回原点快速度
        /// </summary>
        /// <param name="HomFast">回零快速,脉冲量</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetAxHomeFastSpeed(int HomFast, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetHomeFastSpeed(HomFast, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴回原点快速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置轴回原点快速度
        /// </summary>
        /// <param name="HomFast">回零快速,毫米量</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetAxHomeFastSpeed(float HomFast, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetHomeFastSpeed(HomFast, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴回原点快速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置轴回原点慢速度
        /// </summary>
        /// <param name="HomSlow">回零慢速度,脉冲量</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetAxHomeSlowSpeed(int HomSlow, ushort naxis)
        {

            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetHomeSlowSpeed(HomSlow, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴回原点慢速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置轴回原点慢速度
        /// </summary>
        /// <param name="HomSlow">回零慢速度,毫米量</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetAxHomeSlowSpeed(float HomSlow, ushort naxis)
        {

            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetHomeSlowSpeed(HomSlow, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴回原点慢速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置轴原点偏移量
        /// </summary>
        /// <param name="HomOffset">回原点偏移坐标,脉冲量</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetAxHomeOffset(int HomOffset, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetHomeOffset(HomOffset, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴原点偏移量失败!\n异常描述:{0}", ex.Message));
            }
            return rt;

        }

        /// <summary>
        /// 设置轴原点偏移量
        /// </summary>
        /// <param name="HomOffset">回原点偏移坐标,毫米量</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetAxHomeOffset(float HomOffset, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetHomeOffset(HomOffset, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴原点偏移量失败!\n异常描述:{0}", ex.Message));
            }
            return rt;

        }

        /// <summary>
        /// 设置指定轴的负向软限位值
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="fvalue">负限位值：脉冲量</param>
        /// <returns></returns>
        public bool DoSetnaxisSRevValue(int fvalue, int naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetSoftMinLimit(fvalue, (ushort)naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴软负限位值失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的负向软限位值
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="fvalue">负限位值：毫米量</param>
        /// <returns></returns>
        public bool DoSetnaxisSRevValue(float fvalue, int naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetSoftMinLimit(fvalue, (ushort)naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴软负限位值失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }



        /// <summary>
        /// 设置指定轴的正向软限位值
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="fvalue">正限位值：脉冲量</param>
        /// <returns></returns>
        public bool DoSetnaxisSFwdValue(int fvalue, int naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetSoftMaxLimit(fvalue, (ushort)naxis) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴软正限位值失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的正向软限位值
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="fvalue">正限位值:毫米量</param>
        /// <returns></returns>
        public bool DoSetnaxisSFwdValue(float fvalue, int naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxSetSoftMaxLimit(fvalue, (ushort)naxis) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴软正限位值失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }



        /// <summary>
        /// 指定轴运动绝对位置
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="speed">速度，脉冲量</param>
        /// <param name="pos">绝对位置，脉冲量</param>
        /// <returns></returns>
        public bool DoSingleAbsMove(int naxis, int speed, int pos)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxMoveAbs(pos, speed, (ushort)naxis) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴绝对运动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 指定轴运动绝对位置
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="speed">速度，毫米量</param>
        /// <param name="pos">绝对位置，毫米量</param>
        /// <returns></returns>
        public bool DoSingleAbsMove(int naxis, float speed, float pos)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxMoveAbs(pos, speed, (ushort)naxis) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴绝对运动失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }


        /// <summary>
        /// 指定轴运动相对位置
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="speed">速度，脉冲量</param>
        /// <param name="pos">相对位置，脉冲量</param>
        /// <returns></returns>
        public bool DoSingleRelMove(int naxis, float speed, float pos)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxMoveRel(pos, speed, (ushort)naxis) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴相对运动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 指定轴运动相对位置
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="speed">速度，毫米量</param>
        /// <param name="pos">相对位置，毫米量</param>
        /// <returns></returns>
        public bool DoSingleRelMove(int naxis, int speed, int pos)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxMoveRel(pos, speed, (ushort)naxis) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴相对运动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }



        /// <summary>
        /// 指定轴口恒定速度运行
        /// </summary>
        /// <param name="distance">位移,脉冲量</param>
        /// <param name="speed">速度,脉冲量</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetMoveVelocity(int distance, int speed, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxMoveVelocity(distance, speed, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴口恒定速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 指定轴口恒定速度运行
        /// </summary>
        /// <param name="distance">位移,毫米量</param>
        /// <param name="speed">速度,毫米量</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetMoveVelocity(float distance, float speed, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxMoveVelocity(distance, speed, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴口恒定速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 指定轴立即停止运动
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetAxStop(ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxStop(naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴立即停止运动失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 指定轴减速停止运动
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetAxStopDec(ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxStopDec(naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置轴减速停止运动失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定轴的轴状态
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="naxisStatus">轴状态： 0就绪，1停止，2减速停，3普通运动，4连续运动，5正在回原点，6未激活状态，7错误停，8轴同步状态</param>
        /// <returns></returns>
        public bool DoGetnaxisStatus(int naxis, ref short naxisStatus)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxGetStatus(ref naxisStatus, (ushort)naxis) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取轴状态指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取硬件版本
        /// </summary>
        /// <param name="HardWareVer">硬件版本：长度10</param>
        /// <returns></returns>
        public bool GetHardWareVer(ref int[] HardWareVer)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_GetHardWareVer(ref HardWareVer) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取硬件版本指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取软件版本
        /// </summary>
        /// <param name="softVer">软件版本：长度10</param>
        /// <returns></returns>
        public bool GetSoftWareVer(ref int[] softVer)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_GetSoftWareVer(ref softVer) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取软件版本指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定轴的位置
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="curpos">当前位置，脉冲量</param>
        /// <returns></returns>
        public bool DoGetnaxisCurPos(int naxis, ref int curpos)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxGetCurPos(ref curpos, (ushort)naxis) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取轴当前位置指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定轴的位置
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="curpos">当前位置，毫米量</param>
        /// <returns></returns>
        public bool DoGetnaxisCurPos(int naxis, ref float curpos)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxGetCurPos(ref curpos, (ushort)naxis) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取轴当前位置指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取编码器状态
        /// </summary>
        /// <param name="code">编码器值，长度5</param>
        /// <param name="num">编码器编号</param>
        /// <returns></returns>
        public bool AxGetEncoder(ref int[] code, ushort num)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_AxGetEncoder(ref code, num) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取轴限位模式失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定输入口的当前值
        /// </summary>
        /// <param name="nport">输入口号</param>
        /// <param name="value">当前值</param>
        /// <returns></returns>
        public bool DoGetInBitValue(int nport, ref int value)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_GetInputStata(ref value, (ushort)nport) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取输入位指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定输出口的当前值
        /// </summary>
        /// <param name="nport">输出口号</param>
        /// <param name="value">当前值</param>
        /// <returns></returns>
        public bool DoGetOutBitValue(int nport, ref int value)
        {
            bool rt = false;
            value = 0;
            try
            {
                if (hzaux.HZ_GetOutputStata(ref value, (ushort)nport) == 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取输出位指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置输出状态
        /// </summary>
        /// <param name="nport">输出口位置，从0开始</param>
        /// <returns></returns>
        public bool SetOutputStata(ushort nport)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_SetOutputStata(nport) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置输出状态指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取错误代码，获取的是数字，错误内容参考说明书
        /// </summary>
        /// <param name="errorCode">报警码，长度20</param>
        /// <returns></returns>
        public bool GetErrorCode(ref int[] errorCode)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_GetErrorCode(ref errorCode) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取错误代码指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }


        /// <summary>
        /// 获取设备错误等级
        /// </summary>
        /// <param name="errorLevel">报警等级</param>
        /// <returns></returns>
        public bool GetErrorLevel(ref int errorLevel)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_GetErrorLevel(ref errorLevel) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡取设备错误等级失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }




        /// <summary>
        /// 获取时间
        /// </summary>
        /// <param name="time">时间，长度4</param>
        /// <returns></returns>
        public bool GetMachineTime(ref byte[] time)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_GetMachineTime(ref time) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡取时间失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取日期
        /// </summary>
        /// <param name="data">日期，长度4</param>
        /// <returns></returns>
        public bool GetMachineData(ref byte[] data)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_GetMachineData(ref data) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设定轴限位模式失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取客户随机码
        /// </summary>
        /// <param name="id">客户随机码,长度2</param>
        /// <returns></returns>
        public bool GetMachineCID(ref int[] id)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_GetMachineCID(ref id) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设定轴限位模式失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取设备运行状态
        /// </summary>
        /// <param name="state">设备状态：0初始，1停止，2运行，3复位，4急停，5暂停</param>
        /// <returns></returns>
        public bool GetRunStatus(ref int state)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_GetRunStatus(ref state) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡获取设备运行状态失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 操作控制卡写Flash
        /// </summary>
        /// <returns></returns>
        public bool WriteFlash()
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_WriteFlash() == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡操作控制卡写Flash失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置点动模式  0：数量级 1：指定脉冲数 2：连续模式
        /// </summary>
        /// <param name="mode">0,1,2</param>
        /// <returns></returns>
        public bool SetJogMode(int mode)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_SetJogMode(mode) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置点动模式失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

      

        /// <summary>
        /// 设置点动等级，在0点动模式下有效
        /// </summary>
        /// <param name="level">0：一个脉冲，1：:10个脉冲，2:100个脉冲，3:1000个脉冲，4：10000个脉冲，5:100000个脉冲</param>
        /// <returns></returns>
        public bool SetJogLevel(int level)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_SetJogLevel(level) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置点动等级失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置固定距离(脉冲数)，在1点动模式下有效
        /// </summary>
        /// <param name="pluse">脉冲量</param>
        /// <returns></returns>
        public bool SetJogPosition(int pluse)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_SetJogPosition(pluse) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置固定距离失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置固定距离，在1点动模式下有效
        /// </summary>
        /// <param name="pluse">毫米量</param>
        /// <returns></returns>
        public bool SetJogPosition(float pluse, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_SetJogPosition(pluse, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置固定距离失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 正向点动
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetJogForward(ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_SetJogForward(naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡正向点动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 反向点动
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetJogBackward(ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_SetJogBackward(naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {              
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡反向点动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }
        /// <summary>
        /// 点动清零
        /// </summary>
        /// <returns></returns>
        public bool SetJogClear()
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_SetJogClear() == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡点动清零指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 回零
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetJogGohome(ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_SetJogGohome(naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡回零指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 点动立即停止
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetJogStop(ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_SetJogStop(naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡点动立即停止指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置点动速度
        /// </summary>
        /// <param name="speed">速度</param>
        /// <returns></returns>
        public bool SetJogSpeedPercent(int speed)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_SetJogSpeedPercent(speed) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置点动速度指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置点动速度
        /// </summary>
        /// <param name="speed">速度</param>
        /// <param name="naxis">轴号</param>
        /// <returns></returns>
        public bool SetJogSpeedPercent(float speed, ushort naxis)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_SetJogSpeedPercent(speed, naxis) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡设置点动速度指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 清除报警
        /// </summary>
        /// <returns></returns>
        public bool SetClearAlarm()
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_SetClearAlarm() == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡清除报警指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 读取一位寄存器，返回两个字节
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="number">短整型个数，返回字节</param>
        /// <param name="pValue">字节数组</param>
        /// <returns></returns>
        public bool ReadRegister(ushort startAddr, ushort number, out byte[] pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_ReadRegister(startAddr, number, out pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡读取一位寄存器指令失败!\n异常描述:{0}", ex.Message));
                pValue = new byte[] { 0 };
            }

            return rt;
        }

        /// <summary>
        /// 读取一位寄存器，返回一个无符号短整型
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="number">无符号短整型个数</param>
        /// <param name="pValue">无符号短整型数组</param>
        /// <returns></returns>
        public bool ReadRegister(ushort startAddr, ushort number, out ushort[] pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_ReadRegister(startAddr, number, out pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {              
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡读取一位寄存器指令失败!\n异常描述:{0}", ex.Message));
                pValue = new ushort[] { 0 };
            }

            return rt;
        }

        /// <summary>
        /// 读取一位寄存器，返回一个短整型
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="number">短整型个数</param>
        /// <param name="pValue">短整型数组</param>
        /// <returns></returns>
        public bool ReadRegister(ushort startAddr, ushort number, out short[] pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_ReadRegister(startAddr, number, out pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡读取一位寄存器指令失败!\n异常描述:{0}", ex.Message));
                pValue = new short[] { 0 };
            }

            return rt;
        }

        /// <summary>
        /// 读取两位寄存器，返回一个无符号整型
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="number">无符号整型个数</param>
        /// <param name="pValue">无符号整型数组</param>
        /// <returns></returns>
        public bool ReadRegister(ushort startAddr, ushort number, out uint[] pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_ReadRegister(startAddr, number, out pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡读取两位寄存器指令失败!\n异常描述:{0}", ex.Message));
                pValue = new uint[] { 0 };
            }

            return rt;
        }

        /// <summary>
        /// 读取两位寄存器，返回一个整型
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="number">整型个数</param>
        /// <param name="pValue">整型数组</param>
        /// <returns></returns>
        public bool ReadRegister(ushort startAddr, ushort number, out int[] pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_ReadRegister(startAddr, number, out pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡读取两位寄存器指令失败!\n异常描述:{0}", ex.Message));
                pValue = new int[] { 0 };
            }

            return rt;
        }

        /// <summary>
        /// 读取两位寄存器，返回一个浮点型
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="number">浮点型个数</param>
        /// <param name="pValue">浮点型数组</param>
        /// <returns></returns>
        public bool ReadRegister(ushort startAddr, ushort number, out float[] pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_ReadRegister(startAddr, number, out pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡读取两位寄存器指令失败!\n异常描述:{0}", ex.Message));
                pValue = new float[] { 0 };
            }

            return rt;
        }

        /// <summary>
        /// 写字节
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="pValue">写入数据</param>
        /// <returns></returns>
        public bool WriteRegister(ushort startAddr, byte[] pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_WriteRegister(startAddr, pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡写字节指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 写无符号短整型
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="pValue">写入数据</param>
        /// <returns></returns>
        public bool WriteRegister(ushort startAddr, ushort[] pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_WriteRegister(startAddr, pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡写无符号短整型指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 写短整型
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="pValue">写入数据</param>
        /// <returns></returns>
        public bool WriteRegister(ushort startAddr, short[] pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_WriteRegister(startAddr, pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡写短整型指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 写无符号整型
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="pValue">写入数据</param>
        /// <returns></returns>
        public bool WriteRegister(ushort startAddr, uint[] pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_WriteRegister(startAddr, pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡写无符号整型指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 写整型
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="pValue">写入数据</param>
        /// <returns></returns>
        public bool WriteRegister(ushort startAddr, int[] pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_WriteRegister(startAddr, pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡写整型指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 写浮点型
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="pValue">写入数据</param>
        /// <returns></returns>
        public bool WriteRegister(ushort startAddr, float[] pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_WriteRegister(startAddr, pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡写浮点型指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }


        /// <summary>
        /// 写无符号短整型
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="pValue">写入数据</param>
        /// <returns></returns>
        public bool WriteRegister(ushort startAddr, ushort pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_WriteRegister(startAddr, pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡写无符号短整型指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 写短整型
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="pValue">写入数据</param>
        /// <returns></returns>
        public bool WriteRegister(ushort startAddr, short pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_WriteRegister(startAddr, pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡写短整型指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 写无符号整型
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="pValue">写入数据</param>
        /// <returns></returns>
        public bool WriteRegister(ushort startAddr, uint pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_WriteRegister(startAddr, pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡写无符号整型指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 写整型
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="pValue">写入数据</param>
        /// <returns></returns>
        public bool WriteRegister(ushort startAddr, int pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_WriteRegister(startAddr, pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡写整型指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 写浮点型
        /// </summary>
        /// <param name="startAddr">地址</param>
        /// <param name="pValue">写入数据</param>
        /// <returns></returns>
        public bool WriteRegister(ushort startAddr, float pValue)
        {
            bool rt = false;
            try
            {
                if (hzaux.HZ_WriteRegister(startAddr, pValue) == 0)
                {
                    rt = true;
                }

            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：汇众智慧板卡写浮点型指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }


        public new string ToString()
        {
            return "BoardDriver[HZZH]";
        }

        //--------------👇驱动器专用接口信号👇------------------//
        //----------------******待完善*****-------------------//       











        //--------------👆驱动器专用接口信号👆------------------//

        #endregion

        #region 编码器控制或设置
        #endregion

        #region 计数器控制或设置
        #endregion
    }
}
