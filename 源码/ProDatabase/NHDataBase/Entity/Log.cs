using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       Log
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProDatabase.NHDataBase.Entity
 * File      Name：       Log
 * Creating  Time：       10/9/2019 2:44:04 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProDatabase.NHDataBase.Entity
{
    /// <summary>
    /// 日志类
    /// </summary>
    [Serializable]
    public class Log
    {
        public Log() { }
        public Log(string serialNo, string itemName, string description)
        {
            this.SerialNo = serialNo;
            this.ItemName = itemName;
            this.Description = description;
            this.DoTime = DateTime.Now;
        }

        /// <summary>
        /// 属性：日志Id
        /// </summary>
        public virtual int Id
        {
            set;
            get;
        }


        /// <summary>
        /// 属性：日志流水号
        /// </summary>
        public virtual string SerialNo
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：项名称
        /// </summary>
        public virtual string ItemName
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:描述
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 属性：日期
        /// </summary>
        public virtual DateTime? DoTime
        {
            set;
            get;
        }
    }
}
