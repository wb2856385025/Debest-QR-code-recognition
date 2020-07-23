using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ConfigWeb
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Config
 * File      Name：       ConfigWeb
 * Creating  Time：       1/15/2020 3:53:10 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Config
{
    /// <summary>
    /// 通信Web配置
    /// </summary>
    [Serializable]
    public class ConfigWeb : Config
    {
        private ProCommon.Communal.ComWrappedWebList _comWebList = new ProCommon.Communal.ComWrappedWebList();

        /// <summary>
        /// 属性:ComWeb列表(用于ComWeb实体删减+查询)
        /// </summary>
        public ProCommon.Communal.ComWrappedWebList ComWebList
        {
            set { this._comWebList = value; }
            get
            {
                return this._comWebList;
            }
        }

        /// <summary>
        /// 属性:ComWeb实体的列表(用于数据绑定+查询)
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<ProCommon.Communal.ComWrappedWeb> ComWebBList
        {
            get
            {
                System.ComponentModel.BindingList<ProCommon.Communal.ComWrappedWeb> comweblist = new System.ComponentModel.BindingList<ProCommon.Communal.ComWrappedWeb>();
                for (int i = 0; i < ComWebList.Count; i++)
                {
                    comweblist.Add(ComWebList[i]);
                }

                return comweblist;
            }
        }
    }
}
