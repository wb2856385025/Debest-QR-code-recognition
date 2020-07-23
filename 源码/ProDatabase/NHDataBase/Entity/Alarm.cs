using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       Alarm
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProDatabase.NHDataBase.Entity
 * File      Name：       Alarm
 * Creating  Time：       10/9/2019 2:43:16 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProDatabase.NHDataBase.Entity
{
    /// <summary>
    /// 报警类
    /// </summary>
    [Serializable]
    public class Alarm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Alarm() { }

        public Alarm(string serialNo, string address, string description)
        {
            this.SerialNo = serialNo;
            this.Address = address;
            this.Description = description;
        }

        /// <summary>
        /// 属性:报警信号Id
        /// </summary>
        public virtual int Id
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:报警流水号
        /// </summary>
        public virtual string SerialNo
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：报警信号地址
        /// </summary>
        public virtual string Address
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:起始时间
        /// </summary>
        public virtual DateTime? TimeBegin
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:结束时间
        /// </summary>
        public virtual DateTime? TimeEnd
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:报警描述
        /// </summary>
        public virtual string Description
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:是否有效
        /// </summary>
        public virtual bool IsOpen
        {
            set;
            get;
        }

    }
}
