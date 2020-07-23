using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ConfigSocket
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Config
 * File      Name：       ConfigSocket
 * Creating  Time：       1/15/2020 3:51:25 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Config
{
    /// <summary>
    /// 通信Socket配置
    /// </summary>
    [Serializable]
    public class ConfigSocket : Config
    {
        public ConfigSocket() { _comSocketList = new ProCommon.Communal.ComWrappedSocketList(); }

        private ProCommon.Communal.ComWrappedSocketList _comSocketList;

        /// <summary>
        /// 属性:ComSocket列表实体(用于实体删减+查询)
        /// </summary>
        public ProCommon.Communal.ComWrappedSocketList ComSocketList
        {
            set { _comSocketList = value; }
            get { return _comSocketList; }
        }

        /// <summary>
        /// 属性:ComSocket实体的列表(用于数据绑定+查询)
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<ProCommon.Communal.ComWrappedSocket> ComSocketBList
        {
            get
            {
                System.ComponentModel.BindingList<ProCommon.Communal.ComWrappedSocket> bl = new System.ComponentModel.BindingList<ProCommon.Communal.ComWrappedSocket>();
                for (int i = 0; i < _comSocketList.Count; i++)
                {
                    bl.Add(_comSocketList[i]);
                }
                return bl;
            }
        }
    }
}
