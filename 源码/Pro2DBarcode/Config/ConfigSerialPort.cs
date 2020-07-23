using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ConfigSerialPort
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Config
 * File      Name：       ConfigSerialPort
 * Creating  Time：       1/15/2020 3:48:53 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Config
{   
    public class ConfigSerialPort : Config
    {
        private ProCommon.Communal.ComWrappedSerialPortList _comSerialPortList;

        public ConfigSerialPort()
        {
            _comSerialPortList = new ProCommon.Communal.ComWrappedSerialPortList();
        }

        /// <summary>
        /// 属性:ComSerialPort列表实体(用于实体删减+查询)
        /// </summary>       
        public ProCommon.Communal.ComWrappedSerialPortList ComSerialPortList
        {
            set { _comSerialPortList = value; }
            get { return _comSerialPortList; }
        }

        /// <summary>
        /// 属性:ComSerialPort实体的列表(用于数据绑定+查询)
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<ProCommon.Communal.ComWrappedSerialPort> ComSerialPortBList
        {
            get
            {
                System.ComponentModel.BindingList<ProCommon.Communal.ComWrappedSerialPort> bl = new System.ComponentModel.BindingList<ProCommon.Communal.ComWrappedSerialPort>();
                for (int i = 0; i < _comSerialPortList.Count; i++)
                {
                    bl.Add(_comSerialPortList[i]);
                }
                return bl;
            }
        }
    }
}
