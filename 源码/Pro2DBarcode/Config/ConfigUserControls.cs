using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ConfigUserControls
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Config
 * File      Name：       ConfigUserControls
 * Creating  Time：       2/9/2020 2:11:12 AM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Config
{
    /// <summary>
    /// 用户定义的控制变量
    /// [注:项目中有所选择的用户控制的变量]
    /// </summary>
    public class ConfigUserControls:Config
    {
        #region 选用轴

        /// <summary>
        /// 进料X轴
        /// </summary>
        public ProCommon.Communal.Axis AxisImportX { set; get; }

        #endregion

        #region 选用InPut

        /// <summary>
        /// 属性:系统急停信号端口对象
        /// </summary>       
        public ProCommon.Communal.InVarObj IN_ScramTrigger { set; get; }

        /// <summary>
        /// 属性:系统停止信号端口对象
        /// </summary>
        public ProCommon.Communal.InVarObj IN_StopTrigger { set; get; }

        /// <summary>
        /// 属性:系统复位信号端口对象
        /// </summary>
        public ProCommon.Communal.InVarObj IN_ResetTrigger { set; get; }

        /// <summary>
        /// 属性:系统启动信号端口对象
        /// </summary>       
        public ProCommon.Communal.InVarObj IN_RunTrigger { set; get; }

        #endregion

        #region 选用OutPut

        /// <summary>
        /// 启动灯输出信号端口对象
        /// </summary>
        public ProCommon.Communal.OutVarObj OUT_RunLight { set; get; }

        /// <summary>
        /// 复位灯输出信号端口对象
        /// </summary>
        public ProCommon.Communal.OutVarObj OUT_ResetLight { set; get; }

        /// <summary>
        /// 停止灯输出信号端口对象
        /// </summary>
        public ProCommon.Communal.OutVarObj OUT_StopLight { set; get; }

        #endregion       
    }
}
