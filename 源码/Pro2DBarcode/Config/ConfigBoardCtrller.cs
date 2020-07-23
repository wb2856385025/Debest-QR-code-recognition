using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ConfigBoardCtrller
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Config
 * File      Name：       ConfigBoardCtrller
 * Creating  Time：       1/15/2020 3:46:38 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Config
{
    /// <summary>
    /// 运动控制器配置
    /// </summary>
    [Serializable]
    public class ConfigBoardCtrller : Config
    {
        private ProCommon.Communal.BoardCtrllerList _boardCtrllerList;

        public ConfigBoardCtrller() { _boardCtrllerList = new ProCommon.Communal.BoardCtrllerList(); }

        /// <summary>
        /// 属性:运控板列表实体(用于实体删减+查询)
        /// </summary>
        public ProCommon.Communal.BoardCtrllerList BoardCtrllerList
        {
            set { _boardCtrllerList = value; }
            get { return _boardCtrllerList; }
        }

        /// <summary>
        /// 属性:运控板实体的列表(用于数据绑定+查询)
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<ProCommon.Communal.BoardCtrller> BoardCtrllerBList
        {
            get
            {
                System.ComponentModel.BindingList<ProCommon.Communal.BoardCtrller> movectrllerlist = new System.ComponentModel.BindingList<ProCommon.Communal.BoardCtrller>();
                for (int i = 0; i < this.BoardCtrllerList.Count; i++)
                {
                    movectrllerlist.Add(this.BoardCtrllerList[i]);
                }
                return movectrllerlist;
            }
        }
    }
}
