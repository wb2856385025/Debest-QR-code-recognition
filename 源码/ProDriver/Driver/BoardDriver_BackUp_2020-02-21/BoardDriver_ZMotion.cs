using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*************************************************************************************
    * CLR    Version：       4.0.30319.42000
    * Class     Name：       BoardDriver_ZMotion_ECI1408
    * Machine   Name：       LAPTOP-KFCLDVVH
    * Name     Space：       ProDriver.Driver
    * File      Name：       BoardDriver_ZMotion_ECI1408
    * Creating  Time：       4/29/2019 3:29:15 PM
    * Author    Name：       xYz_Albert
    * Description   ：       正运动运动控制板卡ECI1408操作封装类
    * Modifying Time：
    * Modifier  Name：
*************************************************************************************/

namespace ProDriver.Driver
{
    /// <summary>
    /// 正运动运动控制板卡ECI1408操作
    /// </summary>
    public class BoardDriver_ZMotion : BoardDriver
    {
        private IntPtr _ghandle;

        public BoardDriver_ZMotion()
        {
            _ghandle = (IntPtr)0;
        }       

        #region 实现抽象函数
        public override bool DoConnectCtrller(string ip,int port=8089)
        {
            bool rt = false;
            try
            {
                if (cszmcaux.zmcaux.ZAux_OpenEth(ip, out _ghandle) == 0)
                {
                    if (_ghandle.ToInt64()!= 0)
                        rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡链接失败!\n异常描述:{0}", ex.Message));
            }

            return rt;
        }     

        public override bool DoDisconnectCtrller()
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Close(_ghandle) == 0)
                    {
                        _ghandle = (IntPtr)0;
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡关闭失败!\n异常描述:{0}", ex.Message));
            }

            return rt;
        }

        public override bool DoInitCtrllerSys()
        {
            bool rt = false;
            try
            {
                rt = true;
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡初始化失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置主轴及联动轴列表
        /// </summary>
        /// <param name="axisNum">主轴</param>
        /// <param name="piAxislist">轴列表</param>
        /// <returns></returns>
        public override bool DoSetBaseAxes(int axisNum, int[] piAxislist)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_Base(_ghandle, axisNum, piAxislist) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置主轴失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的轴类型
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetAtype(_ghandle, naxis, type) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {              
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置轴类型失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的脉冲输出模式
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetInvertStep(_ghandle, naxis, mode) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置脉冲输出模式失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的限位模式
        /// [注:无此功能]
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public override bool DoSetAxisLimitMode(int naxis, int mode)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置轴限位模式失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetAlmIn(_ghandle, naxis, inputno) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置轴报警端口失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定轴的报警信号端口位
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="inputno">输入端口位号</param>
        /// <returns></returns>
        public override bool DoGetAxisALMIn(int naxis, ref int inputno)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_GetAlmIn(_ghandle, naxis, ref inputno) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡获取轴报警端口失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的负向硬限位信号及有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public override bool DoSetAxisHRevIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetRevIn(_ghandle, naxis, inputno) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置硬负限位端口失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定轴的负向硬限位信号
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="inputno">输入端口位号</param>
        /// <returns></returns>
        public override bool DoGetAxisHRevIn(int naxis, ref int inputno)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_GetRevIn(_ghandle, naxis, ref inputno) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡获取硬负限位端口失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetRsLimit(_ghandle, naxis, fvalue) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置软负限位值失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的原点硬限位信号端口位及有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public override bool DoSetAxisDatumIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetDatumIn(_ghandle, naxis, inputno) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置硬原点限位端口失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定轴的原点硬限位信号端口位
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="inputno">输入端口位号</param>
        /// <returns></returns>
        public override bool DoGetAxisDatumIn(int naxis, ref int inputno)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_GetDatumIn(_ghandle, naxis, ref inputno) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡获取硬原点限位端口失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的正向硬限位信号端口位及有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public override bool DoSetAxisHFwdIn(int naxis, int inputno, ProCommon.Communal.ElectricalLevel level)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetFwdIn(_ghandle, naxis, inputno) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置硬正限位端口失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定轴的正向硬限位信号端口位
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="inputno">输入端口位号</param>
        /// <returns></returns>
        public override bool DoGetAxisHFwdIn(int naxis, ref int inputno)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_GetFwdIn(_ghandle, naxis, ref inputno) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡获取硬正限位端口失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetFsLimit(_ghandle, naxis, fvalue) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置软正限位值失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定输入端口位的有效电平
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetInvertIn(_ghandle, inputno, lv) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置端口有效电平失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetUnits(_ghandle, naxis, units) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置轴脉冲当量失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的爬行速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="creep">爬行速度</param>
        /// <returns></returns>
        public override bool DoSetAxisCreep(int naxis, float creep)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetCreep(_ghandle, naxis, creep) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置轴爬行速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置轴的T形运控参数
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
        /// </summary>
        /// <param name="ionum"></param>
        /// <param name="fValue"></param>
        /// <returns></returns>
        public override bool DoSetDA(int ionum, float fValue)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetDA(_ghandle, ionum, fValue) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置轴主轴速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置轴的S形运控参数
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
        private bool SetAxisInitspeed(int naxis, float lspeed)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetLspeed(_ghandle, naxis, lspeed) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置轴初始速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的运动速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="speed">运动速度</param>
        /// <returns></returns>
        private bool SetAxisSteadySpeed(int naxis, float speed)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetSpeed(_ghandle, naxis, speed) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置轴运行速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的加速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="accel">加速度</param>
        /// <returns></returns>
        private bool SetAxisAccel(int naxis, float accel)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetAccel(_ghandle, naxis, accel) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置轴加速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的减速度
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="decel">减速度</param>
        /// <returns></returns>
        private bool SetAxisDecel(int naxis, float decel)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetDecel(_ghandle, naxis, decel) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置轴减速度失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定轴的运行速度曲线
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="sramp">速度曲线</param>
        /// <returns></returns>
        private bool SetAxisSramp(int naxis, float sramp)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetSramp(_ghandle, naxis, sramp) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {              
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置轴速度曲线失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 指定轴根据设定的方向和模式回零
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="moveDir">回零方向</param>
        /// <param name="mode">回零模式
        /// 0,清除所有轴的错误状态
        /// 1,以回零速度Creep正向运行直到编码器Z信号出现,DPOS重置为0
        /// 2,以回零速度Creep反向运行直到编码器Z信号出现,DPOS重置为0
        /// 3,以运行速度Speed正向运行,碰到原点信号,以回零速度Creep反向运行,直到离开原点,DPOS重置为0
        /// 4,以运行速度Speed反向运行,碰到原点信号,以回零速度Creep正向运行,直到离开原点,DPOS重置为0
        /// 5,以运行速度Speed正向运行,碰到原点信号,以回零速度Creep反向运行,直到离开原点且编码器Z信号出现,DPOS重置为0
        /// 6,以运行速度Speed反向运行,碰到原点信号,以回零速度Creep正向运行,直到离开原点且编码器Z信号出现,DPOS重置为0
        /// 7,保留
        /// 8,以运行速度Speed正向运行,碰到原点信号,DPOS重置为0
        /// 9,以运行速度Speed反向运行,碰到原点信号,DPOS重置为0
        /// 对于原点在正负限位中间的情况,在各个模式上加10,表示回零过程中碰到限位不取消运动,继续反向找原点
        /// </param>
        /// <returns></returns>
        public override bool DoFindDatum(int naxis, ProCommon.Communal.MoveDir moveDir, int mode)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    switch (moveDir)
                    {
                        case ProCommon.Communal.MoveDir.BackWard:
                            {
                                if ((mode == 2 || mode == 4 || mode == 6 || mode == 9
                                    || mode == 12 || mode == 14 || mode == 16 || mode == 19))
                                {
                                    if (cszmcaux.zmcaux.ZAux_Direct_Singl_Datum(_ghandle, naxis, mode) == 0)
                                    {
                                        rt = true;
                                    }
                                }
                            }
                            break;
                        case ProCommon.Communal.MoveDir.ForWard:
                            {
                                if ((mode == 1 || mode == 3 || mode == 5 || mode == 8
                                    || mode == 11 || mode == 13 || mode == 15 || mode == 18))
                                {
                                    if (cszmcaux.zmcaux.ZAux_Direct_Singl_Datum(_ghandle, naxis, mode) == 0)
                                    {
                                        rt = true;
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡轴回零失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_Singl_Vmove(_ghandle, naxis, dir) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡轴连续运动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 指定轴相对运动
        /// [注:正运动卡此处速度忽略]
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_Singl_Move(_ghandle, naxis, pos) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡轴相对运动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 指定轴绝对运动
        /// [注:正运动卡此处速度忽略]
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="pos"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public override bool DoSingleAbsMove(int naxis, float pos, float speed = 0.0f)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_Singl_MoveAbs(_ghandle, naxis, pos) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡轴绝对运动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 轴列表运动按指定模式取消
        /// </summary>
        /// <param name="axes"></param>
        /// <returns></returns>
        public override bool DoCancelAxisList(int[] axes,int mode)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_CancelAxisList(_ghandle,axes.Count(), axes, mode) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡取消轴列表运动指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 指定轴按指定模式停止
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="mode">停止模式：0-当前运动，1-缓存运动，2-当前运动和缓存运动</param>
        /// <returns></returns>
        public override bool DoSingleCancelMove(int naxis, int mode)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_Singl_Cancel(_ghandle, naxis, mode) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡轴停止运动指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetDpos(_ghandle, naxis, curpos) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置轴当前位置指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_GetDpos(_ghandle, naxis, ref curpos) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡获取轴当前位置指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_GetVpSpeed(_ghandle, naxis, ref curspeed) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {              
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡获取轴当前运行速度指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 检查指定轴是否停止
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="stopped">是否停止</param>
        /// <returns></returns>
        public override bool DoChekcAxisIfStop(int naxis, ref bool stopped)
        {
            bool rt = false;
            int idle = 0;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_GetIfIdle(_ghandle, naxis, ref idle) == 0)
                    {
                        stopped = (idle != 0) ? true : false;
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡检查轴是否停止指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 获取指定轴的轴状态
        /// </summary>
        /// <param name="naxis">轴号</param>
        /// <param name="axisStatus">轴状态(ZMotionAxisStatus)</param>
        /// <returns></returns>
        public override bool DoGetAxisStatus(int naxis, ref int axisStatus)
        {
            bool rt = false;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_GetAxisStatus(_ghandle, naxis, ref axisStatus) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡获取轴状态指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 检查指定轴是否正常
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
                    ExceptionOccuredDel(string.Format("错误：正运动板卡检查轴是否正常指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_Rapidstop(_ghandle, mode) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡列表轴紧急停止指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    //相对位置运动
                    if (mode == 0)
                    {
                        if (cszmcaux.zmcaux.ZAux_Direct_Move(_ghandle, 2, axespos) == 0)
                            rt = true;
                    }
                    else  //绝对位置运动
                    {
                        if (cszmcaux.zmcaux.ZAux_Direct_MoveAbs(_ghandle, 2, axespos) == 0)
                            rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡列表轴2轴直线插补运动指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    //相对位置运动
                    if (mode == 0)
                    {
                        if (cszmcaux.zmcaux.ZAux_Direct_MoveCirc2(_ghandle, midpos[0], midpos[1], dstpos[0], dstpos[1]) == 0)
                            rt = true;
                    }
                    else  //绝对位置运动
                    {
                        if (cszmcaux.zmcaux.ZAux_Direct_MoveCirc2Abs(_ghandle, midpos[0], midpos[1], dstpos[0], dstpos[1]) == 0)
                            rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡列表轴三点定圆2轴圆弧插补运动指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    //相对位置运动
                    if (mode == 0)
                    {
                        switch(dir)
                        {
                            case 0:
                                if (cszmcaux.zmcaux.ZAux_Direct_MoveCirc(_ghandle, dstpos[0], dstpos[1], cenpos[0], cenpos[1], 1) == 0)
                                    rt = true;
                                break;
                            default:
                                if (cszmcaux.zmcaux.ZAux_Direct_MoveCirc(_ghandle, dstpos[0], dstpos[1], cenpos[0], cenpos[1], 0) == 0)
                                    rt = true;
                                break;
                        }
                        
                    }
                    else  //绝对位置运动
                    {
                        switch (dir)
                        {
                            case 0:
                                if (cszmcaux.zmcaux.ZAux_Direct_MoveCircAbs(_ghandle, dstpos[0], dstpos[1], cenpos[0], cenpos[1], 1) == 0)
                                    rt = true;
                                break;
                            default:
                                if (cszmcaux.zmcaux.ZAux_Direct_MoveCircAbs(_ghandle, dstpos[0], dstpos[1], cenpos[0], cenpos[1], 0) == 0)
                                    rt = true;
                                break;
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡列表轴圆心定圆2轴圆弧插补运动指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetCornerMode(_ghandle, axisNum, deceleratemode) == 0)
                        rt = true;
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置指定轴减速模式指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetDecelAngle(_ghandle, axisNum, decelanglerange[0]) == 0)
                    {
                        if (cszmcaux.zmcaux.ZAux_Direct_SetStopAngle(_ghandle, axisNum, decelanglerange[1]) == 0)
                            rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置指定轴减速角范围指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetFullSpRadius(_ghandle, axisNum, decelradius) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置指定轴减速半径指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetZsmooth(_ghandle, axisNum, smoothradius) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置指定轴倒角半径指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }


        /// <summary>
        /// 指定输出端口位输出逻辑值
        /// </summary>
        /// <param name="nbit">输出端口位</param>
        /// <param name="onoff"></param>
        /// <returns></returns>
        public override bool DoSetOutBitLogicValue(int nbit, bool onoff)
        {
            bool rt = false;           
            uint v=(onoff)?(uint)1:0; //此处逻辑的定义由正运动控制卡定义:0-false,1-true
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_SetOp(_ghandle, nbit, v) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置输出位指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 设置指定起始输出口开始的指定数量个输出口的当前值(待与单个端口位的形式统一)
        /// </summary>
        /// <param name="nportstart">指定起始输出口</param>
        /// <param name="portnum">指定数量</param>
        /// <param name="values">当前值(十进制表示)</param>
        /// <returns></returns>
        public bool SetOutBitXValues(ushort nportstart, ushort portnum, uint values)
        {
            bool rt = false;
            byte[] iostate = BitConverter.GetBytes(values);

            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Modbus_Set0x(_ghandle, nportstart, portnum, iostate) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {             
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置多输出位指令失败!\n异常描述:{0}", ex.Message));
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
            uint v = 0;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_GetOp(_ghandle, nbit, ref v) == 0)
                    {
                        ///此处逻辑值的定义由正运动控制卡定义:0-false,1-true
                        onoff = (v == 1) ? true :false;
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡获取输出位指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }



        /// <summary>
        /// 获取指定起始输出口开始的指定数量个输出口的当前值(待与单个端口位的形式统一)
        /// 最多四个字节，即32 Bit位
        /// </summary>
        /// <param name="nportstart">指定起始输出口</param>
        /// <param name="portnum">指定数量</param>
        /// <param name="bytearrValue">当前值(字节数组表示)</param>
        /// <returns></returns>
        public bool GetOutBitXValues(ushort nportstart, ushort portnum, out byte[] bytearrValue)
        {
            bool rt = false;
            bytearrValue = new byte[4];
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Modbus_Get0x(_ghandle, nportstart, portnum, bytearrValue) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡获取多输出位指令失败!\n异常描述:{0}", ex.Message));
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
            uint v = 0;
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_Direct_GetIn(_ghandle, nbit, ref v) == 0)
                    {
                        ///此处逻辑值的定义由正运动控制卡定义:0-false,1-true
                        onoff = (v == 1) ? true : false;
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡获取输入位指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        /// <summary>
        ///  获取指定起始输入口开始的指定数量个输入口的当前值(待与单个端口位的形式统一)
        ///  最多四个字节，即32 Bit位
        /// </summary>
        /// <param name="nportstart">指定起始输入口</param>
        /// <param name="portnum">指定数量</param>
        /// <param name="bytearrValue">当前值(字节数组表示)</param>
        /// <returns></returns>
        public bool GetInBitXValues(int nportstart, int portnum, out byte[] bytearrValue)
        {
            bool rt = false;
            bytearrValue = new byte[4];
            try
            {
                if (_ghandle.ToInt64() != 0)
                {
                    if (cszmcaux.zmcaux.ZAux_GetModbusIn(_ghandle, nportstart, portnum, bytearrValue) == 0)
                    {
                        rt = true;
                    }
                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡获取多输入位指令失败!\n异常描述:{0}", ex.Message));
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
                    if(!axisInportGetted)
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
                if (_ghandle.ToInt64() != 0)
                {
                   
                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置指定轴的减速信号有效电平指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置指定轴的位置改变信号有效电平指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置指定轴的到位信号有效电平指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置指定轴的误差清除信号有效电平指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置指定轴的误差信号指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {

                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置指定轴的报警信号有效电平指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {

                }
            }
            catch (Exception ex)
            {               
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置指定轴的编码器复位信号逻辑电平指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置指定轴的锁存信号有效电平指令失败!\n异常描述:{0}", ex.Message));             
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
                if (_ghandle.ToInt64() != 0)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置指定轴的限位信号有效电平及制动方式指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置指定轴的原点限位信号有效电平指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {
                    switch (naxis)
                    {
                        case 0:
                            if (cszmcaux.zmcaux.ZAux_Direct_SetOp(_ghandle, 16, v) == 0)
                            {
                                rt = true;
                            }
                            break;
                        case 1:
                            if (cszmcaux.zmcaux.ZAux_Direct_SetOp(_ghandle, 18, v) == 0)
                            {
                                rt = true;
                            }
                            break;
                        case 2:
                            if (cszmcaux.zmcaux.ZAux_Direct_SetOp(_ghandle, 20, v) == 0)
                            {
                                rt = true;
                            }
                            break;
                        case 3:
                            if (cszmcaux.zmcaux.ZAux_Direct_SetOp(_ghandle, 22, v) == 0)
                            {
                                rt = true;
                            }
                            break;
                        case 4:
                            if (cszmcaux.zmcaux.ZAux_Direct_SetOp(_ghandle, 24, v) == 0)
                            {
                                rt = true;
                            }
                            break;
                        case 5:
                            if (cszmcaux.zmcaux.ZAux_Direct_SetOp(_ghandle, 26, v) == 0)
                            {
                                rt = true;
                            }
                            break;
                        case 6:
                            if (cszmcaux.zmcaux.ZAux_Direct_SetOp(_ghandle, 28, v) == 0)
                            {
                                rt = true;
                            }
                            break;
                        case 7:
                            if (cszmcaux.zmcaux.ZAux_Direct_SetOp(_ghandle, 30, v) == 0)
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
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置指定轴的伺服使能信号指令失败!\n异常描述:{0}", ex.Message));
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
                if (_ghandle.ToInt64() != 0)
                {

                }
            }
            catch (Exception ex)
            {
                if (ExceptionOccuredDel != null)
                    ExceptionOccuredDel(string.Format("错误：正运动板卡设置指定轴的紧急停止信号有效电平指令失败!\n异常描述:{0}", ex.Message));
            }
            return rt;
        }

        //--------------👆驱动器专用接口信号👆------------------//

        #endregion

        #region 编码器控制或设置
        #endregion

        #region 计数器控制或设置
        #endregion
    }
}
