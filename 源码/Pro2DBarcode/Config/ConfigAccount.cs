using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ConfigAccount
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Config
 * File      Name：       ConfigAccount
 * Creating  Time：       1/15/2020 3:56:29 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Config
{
    [Serializable]
    public class ConfigAccount : Config
    {
        public ConfigAccount()
        {
            _userList = new ProCommon.Communal.AccountList();
        }

        private ProCommon.Communal.AccountList _userList;
        /// <summary>
        /// 属性：用户列表实体(用于实体删减+查询)
        /// </summary>
        public ProCommon.Communal.AccountList UserList
        {
            set { _userList = value; }
            get { return _userList; }
        }

        /// <summary>
        /// 属性：用户实体的列表(用于数据绑定+查询)
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<ProCommon.Communal.Account> UserBList
        {
            get
            {
                System.ComponentModel.BindingList<ProCommon.Communal.Account> userlist = new System.ComponentModel.BindingList<ProCommon.Communal.Account>();
                for (int i = 0; i < this.UserList.Count; i++)
                {
                    userlist.Add(this._userList[i]);
                }
                return userlist;
            }
        }
    }
}
