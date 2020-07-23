using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       Defination
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProCommon.Communal
 * File      Name：       Defination
 * Creating  Time：       12/20/2019 3:15:27 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProCommon.Communal
{
    public delegate bool UpdateCtrlWithLanguageHandler(System.Windows.Forms.Control ctl);

    #region 语言
    public enum Language
    {
        Chinese = 0,
        English = 1
    }
    #endregion

    #region 控制器
    /// <summary>
    /// 控制对象类别
    /// </summary>
    public enum CtrllerCategory : uint
    {
        None = 0x0000,
        PC = 0x0001,
        Board = 0x0002,
        PLC = 0x0003,
        Camera = 0x0004,
        Socket = 0x0005,
        Web = 0x0006,
        SerialPort=0x0007
    }

    /// <summary>
    /// 控制器品牌
    /// </summary>
    public enum CtrllerBrand : uint
    {
        None = 0x0000,
        Microsoft = 0x0001,
        LeadShine = 0x0002,
        ZMotion = 0x0003,
        HikVision = 0x0004,
        Dalsa = 0x0005,
        Baumer = 0x0006,
        Computar = 0x0007,
        Imaging = 0x0008,
        Mitsubishi = 0x0009,
        Panasonic = 0x000A,
        Delta = 0x000B,
        MindVision = 0x000C,
        Halcon = 0x000D,
        ICPDAS = 0x000E,
        Basler = 0x000F,
        DaHua  = 0x0010,
        HZZH = 0x0011
    }

    /// <summary>
    /// 控制器类型
    /// </summary>
    public enum CtrllerType : uint
    {
        None = 0,
        Camera_AreaScan = 1,
        Camera_LineScan = 2,
        Board_EtherNet = 3,
        Borad_PCI = 4
    }

    /// <summary>
    /// 控制对象
    /// </summary>
    public abstract class CtrllerObj
    {
        /// <summary>
        /// 属性:控制器的类别
        /// </summary>
        public CtrllerCategory CtrllerCategory
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:控制器的品牌
        /// </summary>
        public CtrllerBrand CtrllerBrand
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:控制器的编号
        /// 区别多台控制器(包括同型和异型控制器)
        /// </summary>      
        public int Number
        {
            set;get;
        }

        /// <summary>
        /// 属性：控制器的ID
        /// 唯一标识控制器的类别,品牌,类型及编号
        /// [格式:类别-品牌-类型-名称(含编号)]
        /// [注:同品牌同类型的控制器的编号不允许相同]
        /// </summary>   
        public string ID { set; get; }

        /// <summary>
        /// 属性：控制器的名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 属性：运控器的类型
        /// 控制器类别下的具体类型
        /// </summary>
        public string Type { set; get; }

        /// <summary>
        /// 属性：运控器重链间隔(毫秒)
        /// </summary>
        public int ReconnectInterval { set; get; }

        /// <summary>
        /// 属性：线程访问运控器时间间隔(毫秒)
        /// </summary>
        public int AcquireInterval { set; get; }

        /// <summary>
        /// 属性:线程处理时间间隔(毫秒)
        /// </summary>
        public int ProcessInterval { set; get; }

        /// <summary>
        /// 控制对象是否激活
        /// </summary>
        public bool IsActive { set; get; }      
    }

    #endregion

    #region 运动控制
    /// <summary>
    /// 运动方向
    /// </summary>
    public enum MoveDir : int
    {
        BackWard = -1,
        None = 0,
        ForWard = 1
    }

    /// <summary>
    /// 元器件逻辑电平
    /// </summary>
    public enum ElectricalLevel : int
    {
        Low = 0x00,
        High = 0x01
    }

    /// <summary>
    /// 元器件逻辑电平产生的有效触发逻辑
    /// [注:由元器件逻辑电平或电平变化边沿
    /// 产生的有效触发逻辑]
    /// </summary>
    public enum TriggerLogic : uint
    {
        NONE = 0xFFFF,
        LogicFalse = 0x0000,
        LogicTrue = 0x0001,
        FallEdge = 0x0002,
        RaiseEdge = 0x0003
    }

    public enum LimitMode : uint
    {
        None=0,
        SoftLimit=1,
        HardLimit=2,
        BothLimit=3
    }

    /// <summary>
    /// 输入型控制变量
    /// 注:包含专用输入和通用输入
    /// </summary>
    public enum InVar : uint
    {
        NONE = 0xFFFF,
        IN00 = 0x0000,
        IN01 = 0x0001,
        IN02 = 0x0002,
        IN03 = 0x0003,
        IN04 = 0x0004,
        IN05 = 0x0005,
        IN06 = 0x0006,
        IN07 = 0x0007,
        IN08 = 0x0008,
        IN09 = 0x0009,
        IN10 = 0x000A,
        IN11 = 0x000B,
        IN12 = 0x000C,
        IN13 = 0x000D,
        IN14 = 0x000E,
        IN15 = 0x000F,
        IN16 = 0x0010,
        IN17 = 0x0011,
        IN18 = 0x0012,
        IN19 = 0x0013,
        IN20 = 0x0014,
        IN21 = 0x0015,
        IN22 = 0x0016,
        IN23 = 0x0017,
        IN24 = 0x0018,
        IN25 = 0x0019,
        IN26 = 0x001A,
        IN27 = 0x001B,
        IN28 = 0x001C,
        IN29 = 0x001D,
        IN30 = 0x001E,
        IN31 = 0x001F,
        IN32 = 0x0020,
        IN33 = 0x0021,
        IN34 = 0x0022,
        IN35 = 0x0023,
        IN36 = 0x0024,
        IN37 = 0x0025,
        IN38 = 0x0026,
        IN39 = 0x0027,
        IN40 = 0x0028,
        IN41 = 0x0029,
        IN42 = 0x002A,
        IN43 = 0x002B,
        IN44 = 0x002C,
        IN45 = 0x002D,
        IN46 = 0x002E,
        IN47 = 0x002F,
        IN48 = 0x0030,
        IN49 = 0x0031,
        IN50 = 0x0032,
        IN51 = 0x0033,
        IN52 = 0x0034,
        IN53 = 0x0035,
        IN54 = 0x0036,
        IN55 = 0x0037,
        IN56 = 0x0038,
        IN57 = 0x0039,
        IN58 = 0x003A,
        IN59 = 0x003B,
        IN60 = 0x003C,
        IN61 = 0x003D,
        IN62 = 0x003E,
        IN63 = 0x003F,
        IN64 = 0x0040,
        IN65 = 0x0041,
        IN66 = 0x0042,
        IN67 = 0x0043,
        IN68 = 0x0044,
        IN69 = 0x0045,
        IN70 = 0x0046,
        IN71 = 0x0047,
        IN72 = 0x0048,
        IN73 = 0x0049,
        IN74 = 0x004A,
        IN75 = 0x004B,
        IN76 = 0x004C,
        IN77 = 0x004D,
        IN78 = 0x004E,
        IN79 = 0x004F,
        IN80 = 0x0050,
        IN81 = 0x0051,
        IN82 = 0x0052,
        IN83 = 0x0053,
        IN84 = 0x0054,
        IN85 = 0x0055,
        IN86 = 0x0056,
        IN87 = 0x0057,
        IN88 = 0x0058,
        IN89 = 0x0059,
        IN90 = 0x005A,

        ALM0 = 0x0100,
        FWD0 = 0x0101,
        DTM0 = 0x0102,
        REV0 = 0x0103,
        ALM1 = 0x0104,
        FWD1 = 0x0105,
        DTM1 = 0x0106,
        REV1 = 0x0107,
        ALM2 = 0x0108,
        FWD2 = 0x0109,
        DTM2 = 0x010A,
        REV2 = 0x010B,
        ALM3 = 0x010C,
        FWD3 = 0x010D,
        DTM3 = 0x010E,
        REV3 = 0x010F,
        ALM4 = 0x0110,
        FWD4 = 0x0111,
        DTM4 = 0x0112,
        REV4 = 0x0113,
        ALM5 = 0x0114,
        FWD5 = 0x0115,
        DTM5 = 0x0116,
        REV5 = 0x0117,
        ALM6 = 0x0118,
        FWD6 = 0x0119,
        DTM6 = 0x011A,
        REV6 = 0x011B,
        ALM7 = 0x011C,
        FWD7 = 0x011D,
        DTM7 = 0x011E,
        REV7 = 0x011F,

    }

    /// <summary>
    /// 输出型控制变量
    /// 注:通用输出
    /// </summary>
    public enum OutVar : uint
    {
        NONE = 0xFFFF,
        OUT00 = 0x0000,
        OUT01 = 0x0001,
        OUT02 = 0x0002,
        OUT03 = 0x0003,
        OUT04 = 0x0004,
        OUT05 = 0x0005,
        OUT06 = 0x0006,
        OUT07 = 0x0007,
        OUT08 = 0x0008,
        OUT09 = 0x0009,
        OUT10 = 0x000A,
        OUT11 = 0x000B,
        OUT12 = 0x000C,
        OUT13 = 0x000D,
        OUT14 = 0x000E,
        OUT15 = 0x000F,
        OUT16 = 0x0010,
        OUT17 = 0x0011,
        OUT18 = 0x0012,
        OUT19 = 0x0013,
        OUT20 = 0x0014,
        OUT21 = 0x0015,
        OUT22 = 0x0016,
        OUT23 = 0x0017,
        OUT24 = 0x0018,
        OUT25 = 0x0019,
        OUT26 = 0x001A,
        OUT27 = 0x001B,
        OUT28 = 0x001C,
        OUT29 = 0x001D,
        OUT30 = 0x001E,
        OUT31 = 0x001F,
        OUT32 = 0x0020,
        OUT33 = 0x0021,
        OUT34 = 0x0022,
        OUT35 = 0x0023,
        OUT36 = 0x0024,
        OUT37 = 0x0025,
        OUT38 = 0x0026,
        OUT39 = 0x0027,
        OUT40 = 0x0028,
        OUT41 = 0x0029,
        OUT42 = 0x002A,
        OUT43 = 0x002B,
        OUT44 = 0x002C,
        OUT45 = 0x002D,
        OUT46 = 0x002E,
        OUT47 = 0x002F,
        OUT48 = 0x0030,
        OUT49 = 0x0031,
        OUT50 = 0x0032,
        OUT51 = 0x0033,
        OUT52 = 0x0034,
        OUT53 = 0x0035,
        OUT54 = 0x0036,
        OUT55 = 0x0037,
        OUT56 = 0x0038,
        OUT57 = 0x0039,
        OUT58 = 0x003A,
        OUT59 = 0x003B,
        OUT60 = 0x003C,
        OUT61 = 0x003D,
        OUT62 = 0x003E,
        OUT63 = 0x003F,
        OUT64 = 0x0040,
        OUT65 = 0x0041,
        OUT66 = 0x0042,
        OUT67 = 0x0043,
        OUT68 = 0x0044,
        OUT69 = 0x0045,
        OUT70 = 0x0046,
        OUT71 = 0x0047,
        OUT72 = 0x0048,
        OUT73 = 0x0049,
        OUT74 = 0x004A,
        OUT75 = 0x004B,
        OUT76 = 0x004C,
        OUT77 = 0x004D,
        OUT78 = 0x004E,
        OUT79 = 0x004F,
    }

    #region 输入控制变量对象
    [Serializable]
    public class InVarObj : System.ComponentModel.INotifyPropertyChanged
    {
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        public InVarObj() { }

        /// <summary>
        /// 属性:输入控制变量
        /// </summary>
        public InVar InVar
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:控制变量的名称
        /// </summary>
        public string VarName
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:控制变量的类型
        /// </summary>
        public string VarType
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:控制变量的站号(PLC时用到)
        /// </summary>
        public string StationNum
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:控制变量的地址
        /// </summary>
        public string VarAddress
        {
            set;
            get;
        }

        /// <summary>
        /// 有效电平       
        /// </summary>
        public ProCommon.Communal.ElectricalLevel EffectiveLevel
        {
            set; get;
        }

        /// <summary>
        /// 有效触发信号
        /// </summary>
        public ProCommon.Communal.TriggerLogic EffectiveLogic
        {
            set; get;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set;
            get;
        }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdateTime
        {
            set;
            get;
        }

        private object _NewValue;
        /// <summary>
        /// 属性:更新值(控制变量所在实体本身值)
        /// </summary>
        [System.ComponentModel.Bindable(true)]
        public object NewValue
        {
            get { return _NewValue; }
            set
            {
                if (value != null)//避免与控件绑定时的空异常
                {
                    if (value.GetType() == typeof(bool))
                    {
                        //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                        if (this._NewValue == null || (bool)this._NewValue != (bool)value)
                        {
                            this.LastValue = this._NewValue == null ? false : this._NewValue;
                            this._NewValue = value;
                            NotifyPropertyChanged("InNewValue");  //最新值更新
                        }
                    }
                    else if (value.GetType() == typeof(float))
                    {
                        //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                        if (this._NewValue == null || (float)this._NewValue != (float)value)
                        {
                            this.LastValue = this._NewValue == null ? (float)0 : this._NewValue;
                            this._NewValue = value;
                            NotifyPropertyChanged("InNewValue");  //最新值更新
                        }
                    }
                    else
                    {
                        //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                        if (this._NewValue == null || this._NewValue != value)
                        {
                            this.LastValue = this._NewValue;
                            this._NewValue = value;
                            NotifyPropertyChanged("InNewValue");   //最新值更新
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 属性:上次值
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public object LastValue
        {
            get;
            set;
        }

        [System.Xml.Serialization.XmlIgnore]
        private object _EditValue;

        /// <summary>
        /// 属性:编辑值(控制变量对象绑定控件的编辑值)
        /// </summary>
        public object EditValue
        {
            get
            {
                return _EditValue;
            }
            set
            {
                _EditValue = value;
                NotifyPropertyChanged("InEditValue"); //编辑值更新
            }
        }

        /// <summary>
        /// 地址位索引
        /// </summary>
        public int AddressIndex { get; set; }

        /// <summary>
        /// 控制变量对象复制
        /// </summary>
        /// <returns></returns>
        public InVarObj Clone()
        {
            InVarObj ctrlVarObj = new InVarObj();
            return ctrlVarObj;
        }
    }

    [Serializable]
    public class InVarObjList : System.Collections.ICollection
    {
        private System.Collections.SortedList _list;

        public InVarObjList() { _list = new System.Collections.SortedList(); }

        /// <summary>
        /// 方法：增加控制变量实体
        /// </summary>
        /// <param name="inVarObj"></param>
        public void Add(InVarObj inVarObj)
        {
            if (!_list.ContainsKey(inVarObj.InVar))
            {
                _list.Add(inVarObj.InVar, inVarObj);
            }
        }

        /// <summary>
        /// 方法：删除指定控制变量实体
        /// </summary>
        /// <param name="inVarObj"></param>
        public void Delete(InVarObj inVarObj)
        {
            if (!_list.ContainsKey(inVarObj.InVar))
            {
                _list.Remove(inVarObj.InVar);
            }
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        /// <summary>
        /// 索引器：返回控制变量对象列表中的实体
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public InVarObj this[int indx]
        {
            get
            {
                InVarObj ctrlVarObj = null;
                if (_list.Count > 0 && indx < _list.Count)
                {
                    ctrlVarObj = (InVarObj)_list.GetByIndex(indx);
                }
                return ctrlVarObj;
            }
        }

        /// <summary>
        /// 索引器：返回控制变量对象列表中的实体
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public InVarObj this[InVar inVar]
        {
            get
            {
                InVarObj ctrlVarObj = null;
                if (_list.ContainsKey(inVar))
                {
                    ctrlVarObj = (InVarObj)_list[inVar];
                }
                return ctrlVarObj;
            }
        }

        /// <summary>
        /// 方法：控制变量对象列表从指定索引开始复制控制变量对象到给定的一维数组
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="indx"></param>
        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        /// <summary>
        /// 属性：返回控制变量对象列表中实体的数量
        /// </summary>
        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        /// <summary>
        /// 属性：是否同步
        /// </summary>
        public bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// 属性：SyncRoot
        /// </summary>
        public object SyncRoot
        {
            get { return this; }

        }

        /// <summary>
        /// 方法：获取枚举器
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
    #endregion

    #region 输出变量对象
    [Serializable]
    public class OutVarObj : System.ComponentModel.INotifyPropertyChanged
    {
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        public OutVarObj() { }

        /// <summary>
        /// 属性:输出控制变量
        /// </summary>
        public OutVar OutVar
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:控制变量的名称
        /// </summary>
        public string VarName
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:控制变量的类型
        /// </summary>
        public string VarType
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:控制变量的地址
        /// </summary>
        public string VarAddress
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:站号
        /// </summary>
        public string StationNum
        {
            set;
            get;
        }

        /// <summary>
        /// 有效电平
        /// </summary>
        public ProCommon.Communal.ElectricalLevel EffectiveLevel
        {
            set; get;
        }

        /// <summary>
        /// 触发逻辑
        /// </summary>
        public ProCommon.Communal.TriggerLogic EffectiveLogic { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set;
            get;
        }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdateTime
        {
            set;
            get;
        }

        private object _NewValue;
        /// <summary>
        /// 属性:更新值(控制变量所在实体本身值)
        /// </summary>
        [System.ComponentModel.Bindable(true)]
        public object NewValue
        {
            get { return _NewValue; }
            set
            {
                if (value != null) //避免与控件绑定时的空异常
                {
                    if (value.GetType() == typeof(bool))
                    {
                        //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                        if (this._NewValue == null || (bool)this._NewValue != (bool)value)
                        {
                            this.LastValue = this._NewValue == null ? false : this._NewValue;
                            this._NewValue = value;
                            NotifyPropertyChanged("OutNewValue");  //最新值更新
                        }
                    }
                    else if (value.GetType() == typeof(float))
                    {
                        //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                        if (this._NewValue == null || (float)this._NewValue != (float)value)
                        {
                            this.LastValue = this._NewValue == null ? (float)0 : this._NewValue;
                            this._NewValue = value;
                            NotifyPropertyChanged("OutNewValue");  //最新值更新
                        }
                    }
                    else
                    {
                        //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                        if (this._NewValue == null || this._NewValue != value)
                        {
                            this.LastValue = this._NewValue;
                            this._NewValue = value;
                            NotifyPropertyChanged("OutNewValue");   //最新值更新
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 属性:上次值
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public object LastValue
        {
            get;
            set;
        }

        [System.Xml.Serialization.XmlIgnore]
        private object _EditValue;

        /// <summary>
        /// 属性:编辑值(控制变量对象绑定控件的编辑值)
        /// </summary>
        public object EditValue
        {
            get
            {
                return _EditValue;
            }
            set
            {
                _EditValue = value;
                NotifyPropertyChanged("OutEditValue"); //编辑值更新
            }
        }

        /// <summary>
        /// 地址位索引
        /// </summary>
        public int AddressIndex { get; set; }

        /// <summary>
        /// 控制变量对象复制
        /// </summary>
        /// <returns></returns>
        public OutVarObj Clone()
        {
            OutVarObj ctrlVarObj = new OutVarObj();
            return ctrlVarObj;
        }
    }

    [Serializable]
    public class OutVarObjList : System.Collections.ICollection
    {
        private System.Collections.SortedList _list;

        public OutVarObjList() { _list = new System.Collections.SortedList(); }

        /// <summary>
        /// 方法：增加控制变量实体
        /// </summary>
        /// <param name="outVarObj"></param>
        public void Add(OutVarObj outVarObj)
        {
            if (!_list.ContainsKey(outVarObj.OutVar))
            {
                _list.Add(outVarObj.OutVar, outVarObj);
            }
        }

        /// <summary>
        /// 方法：删除指定控制变量实体
        /// </summary>
        /// <param name="outVarObj"></param>
        public void Delete(OutVarObj outVarObj)
        {
            if (!_list.ContainsKey(outVarObj.OutVar))
            {
                _list.Remove(outVarObj.OutVar);
            }
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        /// <summary>
        /// 索引器：返回控制变量对象列表中的实体
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public OutVarObj this[int indx]
        {
            get
            {
                OutVarObj ctrlVarObj = null;
                if (_list.Count > 0 && indx < _list.Count)
                {
                    ctrlVarObj = (OutVarObj)_list.GetByIndex(indx);
                }
                return ctrlVarObj;
            }
        }

        /// <summary>
        /// 索引器：返回控制变量对象列表中的实体
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public OutVarObj this[OutVar outVar]
        {
            get
            {
                OutVarObj ctrlVarObj = null;
                if (_list.ContainsKey(outVar))
                {
                    ctrlVarObj = (OutVarObj)_list[outVar];
                }
                return ctrlVarObj;
            }
        }

        /// <summary>
        /// 方法：控制变量对象列表从指定索引开始复制控制变量对象到给定的一维数组
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="indx"></param>
        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        /// <summary>
        /// 属性：返回控制变量对象列表中实体的数量
        /// </summary>
        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        /// <summary>
        /// 属性：是否同步
        /// </summary>
        public bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// 属性：SyncRoot
        /// </summary>
        public object SyncRoot
        {
            get { return this; }

        }

        /// <summary>
        /// 方法：获取枚举器
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
    #endregion

    #region 运控轴
    [Serializable] 
    public class Axis
    {
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
            }
        }

        private Axis()
        {
        }

        /// <summary>
        /// 创建运控轴
        /// [注:轴的编号不允许相同]
        /// </summary>
        /// <param name="axisNum">轴名称</param>
        /// <param name="axisName">轴编号</param>
        public Axis(int axisNum, string axisName)
            : this()
        {
            this.Number = axisNum;
            this.Name = axisName;
            this.ID = "Axis_" + axisNum.ToString("00");
        }

        /// <summary>
        /// 轴唯一标识符
        /// </summary>
        public string ID
        {
            set; get;
        }       

        /// <summary>
        /// 属性：轴号
        /// </summary>
        public int Number { set; get; }

        /// <summary>
        /// 属性：轴名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 属性：轴类型
        /// </summary>
        public int Type { set; get; }

        /// <summary>
        /// 属性：轴脉冲输出模式
        /// </summary>
        public int PulseOutMode { set; get; }

        /// <summary>
        /// 属性：轴伺服使能端口
        /// </summary>
        public int ServoONPort { set; get; }

        /// <summary>
        /// 属性：轴伺服使能电平
        /// </summary>
        /// 
        public ProCommon.Communal.ElectricalLevel ServoOnLevel { set; get; }


        /// <summary>
        /// 属性：轴报警输入端口号
        /// </summary>
        public int ALMIn { set; get; }

        /// <summary>
        /// 属性：轴报警输入端口有效电平
        /// </summary>
        public ProCommon.Communal.ElectricalLevel ALMInLevel { set; get; }

        /// <summary>
        /// 属性:报警清除输出端口
        /// </summary>
        public int ALMCLROut { set; get; }

        /// <summary>
        /// 属性:报警清除输出端口有效电平
        /// </summary>
        public int ALMCLROutLevel { set; get; }

        /// <summary>
        /// 轴报警状态
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public bool StatusALM { set; get; }

        /// <summary>
        /// 限位模式
        /// </summary>
        public ProCommon.Communal.LimitMode LmtMode { set; get; }       

        /// <summary>
        /// 属性：轴负限位输入端口号
        /// </summary>
        public int HRevIn { set; get; }

        /// <summary>
        /// 属性：轴负限位输入端口有效电平
        /// </summary>
        public ProCommon.Communal.ElectricalLevel HRevInLevel { set; get; }

        /// <summary>
        /// 轴负限位状态
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public bool StatusRev { set; get; }

        /// <summary>
        /// 属性：轴原点输入端口号
        /// </summary>
        public int DatumIn { set; get; }

        /// <summary>
        /// 属性：轴原点输入端口有效电平
        /// </summary>
        public ProCommon.Communal.ElectricalLevel DatumInLevel { set; get; }

        /// <summary>
        /// 轴原点限位状态
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public bool StatusDatum { set; get; }

        /// <summary>
        /// 属性：轴正限位输入端口号
        /// </summary>
        public int HFwdIn { set; get; }

        /// <summary>
        /// 属性：轴正限位输入端口有效电平
        /// </summary>
        public ProCommon.Communal.ElectricalLevel HFwdInLevel { set; get; }

        /// <summary>
        /// 轴正限位状态
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public bool StatusFwd { set; get; }

        /// <summary>
        /// 导程
        /// [注:螺距]
        /// </summary>
        public float HelicalPitch { set; get; }

        /// <summary>
        /// 每转脉冲数
        /// </summary>
        public float PulsePerRound { set; get; }

        /// <summary>
        /// 属性：脉冲当量
        /// [注:一般定义:每转脉冲数/导程,即每转一个用户单位所需的脉冲数]
        /// </summary>
        public float PulseUnit { set; get; }

        /// <summary>
        /// 脉冲角当量
        /// [注:一般定义:每转脉冲数/360度,即每转一个角度所需的脉冲数]
        /// </summary>
        public float PulseAngleUnit { set; get; }

        /// <summary>
        /// 回零方向
        /// </summary>
        private MoveDir _datumDir;
        public string DatumDir
        {
            set
            {
                if (value == "负向"
                    || value=="NEGATIVE")
                    this._datumDir = MoveDir.BackWard;
                else if(value=="正向"
                    || value=="POSITIVE")
                    this._datumDir = MoveDir.ForWard;

            }
            get
            {
                if (this._datumDir == MoveDir.BackWard)
                    return "负向";
                else
                    return "正向";
            }
        }

        /// <summary>
        /// 属性：负向软限位
        /// </summary>
        public float RsLimit { set; get; }

        /// <summary>
        /// 属性：正向软限位
        /// </summary>
        public float FsLimit { set; get; }

        /// <summary>
        /// 属性：初始速度
        /// </summary>
        public float StartSpeed { set; get; }

        /// <summary>
        /// 属性：运行速度
        /// </summary>
        public float RunSpeed { set; get; }

        /// <summary>
        /// 属性:末速度
        /// </summary>
        public float EndSpeed { set; get; }

        /// <summary>
        /// 属性：加速度
        /// </summary>
        public float Accel { set; get; }

        /// <summary>
        /// 属性：减速度
        /// </summary>
        public float Decel { set; get; }

        /// <summary>
        /// 属性：S曲线速度
        /// </summary>
        public float Sramp { set; get; }

        /// <summary>
        /// 属性：回零爬行速度
        /// [注:二次回零时的反向速度]
        /// </summary>
        public float DatumCreepSpeed { set; get; }

        /// <summary>
        /// 属性:回零速度
        /// </summary>
        public float DatumSpeed { set; get; }

        /// <summary>
        /// 属性:回零偏移
        /// </summary>
        public float DatumOffset { set; get; }

        /// <summary>
        /// 属性：回零模式
        /// </summary>
        public int DatumMode { set; get; }

        /// <summary>
        /// 属性：轴位置1
        /// [注:用于设置轴的待机位1或安全位1]
        /// </summary>
        public float FirstPos { set; get; }

        /// <summary>
        /// 属性：位置2
        /// [注:用于设置轴的待机位2或安全位2]
        /// </summary>
        public float SecondPos { set; get; }

        /// <summary>
        /// 属性：位置3
        /// [注:用于设置轴的待机位3或安全位3]
        /// </summary>
        public float ThirdPos { set; get; }

        private float _currentPos;
        /// <summary>
        /// 属性：轴当前位置
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public float CurrentPos
        {
            set
            {
                //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                if (value != this._currentPos)
                {
                    _currentPos = value;
                    NotifyPropertyChanged("AxisCurrentPos");  //最新值更新
                }
            }
            get
            {
                return this._currentPos;
            }
        }

        public ProCommon.Communal.Axis Clone()
        {
            ProCommon.Communal.Axis destAxis = new ProCommon.Communal.Axis();
            destAxis.Accel = this.Accel;
            destAxis.ALMCLROut = this.ALMCLROut;
            destAxis.ALMCLROutLevel = this.ALMCLROutLevel;
            destAxis.ALMIn = this.ALMIn;
            destAxis.ALMInLevel = this.ALMInLevel;
            destAxis.DatumCreepSpeed = this.DatumCreepSpeed;
            destAxis.CurrentPos = this.CurrentPos;
            destAxis.DatumDir = this.DatumDir;
            destAxis.DatumIn = this.DatumIn;
            destAxis.DatumInLevel = this.DatumInLevel;
            destAxis.DatumMode = this.DatumMode;
            destAxis.Decel = this.Decel;
            destAxis.FirstPos = this.FirstPos;
            destAxis.HFwdIn = this.HFwdIn;
            destAxis.HFwdInLevel = this.HFwdInLevel;
            destAxis.HRevIn = this.HRevIn;
            destAxis.HRevInLevel = this.HRevInLevel;
            destAxis.ID = this.ID;
            destAxis.FsLimit = this.FsLimit;
            destAxis.RsLimit = this.RsLimit;
            destAxis.StartSpeed = this.StartSpeed;
            destAxis.Name = this.Name;
            destAxis.Number = this.Number;
            destAxis.PulseOutMode = this.PulseOutMode;
            destAxis.PulseUnit = this.PulseUnit;
            destAxis.SecondPos = this.SecondPos;
            destAxis.ServoOnLevel = this.ServoOnLevel;
            destAxis.ServoONPort = this.ServoONPort;
            destAxis.RunSpeed = this.RunSpeed;
            destAxis.Sramp = this.Sramp;
            destAxis.StatusALM = this.StatusALM;
            destAxis.StatusDatum = this.StatusDatum;
            destAxis.StatusFwd = this.StatusFwd;
            destAxis.StatusRev = this.StatusRev;
            destAxis.ThirdPos = this.ThirdPos;
            destAxis.Type = this.Type;
            destAxis.HelicalPitch = this.HelicalPitch;
            destAxis.PulsePerRound = this.PulsePerRound;
            destAxis.DatumSpeed = this.DatumSpeed;
            destAxis.DatumOffset = this.DatumOffset;
            destAxis.LmtMode = this.LmtMode;
            return destAxis;
        }
    }

    [Serializable]
    public class AxisList : System.Collections.ICollection
    {
        private System.Collections.SortedList _list;

        public AxisList() { _list = new System.Collections.SortedList(); }

        /// <summary>
        /// 方法：添加轴实体
        /// </summary>
        /// <param name="axis"></param>
        public void Add(Axis axis)
        {
            if (!_list.ContainsKey(axis.ID))
            {
                _list.Add(axis.ID, axis);
            }
        }

        /// <summary>
        /// 方法：删除轴实体
        /// </summary>
        /// <param name="axis"></param>
        public void Delete(Axis axis)
        {
            if (_list.ContainsKey(axis.ID))
            {
                _list.Remove(axis.ID);
            }
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        /// <summary>
        /// 索引器：返回轴列表中的实体
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Axis this[int index]
        {
            get
            {
                Axis axis = null;
                if (_list.Count > 0 && index < _list.Count)
                {
                    axis = (Axis)_list.GetByIndex(index);
                }
                return axis;
            }
        }

        public Axis this[string axisID]
        {
            get
            {
                Axis axis = null;
                if (_list.ContainsKey(axisID))
                {
                    axis = (Axis)_list[axisID];
                }
                return axis;
            }
        }

        /// <summary>
        /// 方法：轴列表从指定索引开始复制轴实体到给定的一维数组
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="indx"></param>
        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        /// <summary>
        /// 属性：返回轴列表中实体的数量
        /// </summary>
        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        /// <summary>
        /// 属性：是否同步
        /// </summary>
        public bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// 属性：SyncRoot
        /// </summary>
        public object SyncRoot
        {
            get { return this; }

        }

        /// <summary>
        /// 方法：获取枚举器
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }

    #endregion

    #endregion

    #region 账户操作
    /// <summary>
    /// 账户权限
    /// </summary>
    public enum AccountAuthority : uint
    {
        None = 0,
        Junior = 1, //初级权限,仅打开、浏览,及运行默认参数下的程式
        Senior = 2, //高级权限,打开、浏览,及运行修改参数下的程式(允许修改参数)
        Administrator = 3 //管理员权限,打开、浏览及运行修改参数后的程式,另外允许增减用户
    }

    /// <summary>
    /// 账户操作
    /// </summary>
    public enum AccountOperation : uint
    {
        NONE = 0,
        Add = 1,
        Delete = 2,
        Modify = 3
    }

    /// <summary>
    /// 账户
    /// </summary>
    [Serializable]
    public class Account : System.ComponentModel.INotifyPropertyChanged
    {
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private Account() { }

        /// <summary>
        /// 创建用户
        /// [注:用户的编号不允许相同]
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userIndex"></param>
        public Account(string userName, int userIndex)
        {
            this.Name = userName;
            this.Num = userIndex;
            this.ID = "User_" + userIndex.ToString("00");
        }


        /// <summary>
        /// 属性:ID
        /// [唯一标识]
        /// </summary>
        public string ID
        {
            set; get;
        }

        public int Num
        {
            set; get;
        }

        /// <summary>
        /// 属性:用户名称
        /// </summary>
        public string Name { set; get; }
        public AccountAuthority Authority { set; get; }
        public string PassWord { set; get; }
    }

    [Serializable]
    public class AccountList : System.Collections.ICollection
    {
        private System.Collections.SortedList _list;
        public AccountList() { _list = new System.Collections.SortedList(); }

        /// <summary>
        /// 方法：添加用户实体
        /// </summary>
        /// <param name="user"></param>
        public void Add(Account user)
        {
            if (!_list.ContainsKey(user.ID))
            {
                _list.Add(user.ID, user);
            }
        }

        /// <summary>
        /// 方法：删除用户实体
        /// </summary>
        /// <param name="cam"></param>
        public void Delete(Account cam)
        {
            if (_list.ContainsKey(cam.ID))
            {
                _list.Remove(cam.ID);
            }
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        /// <summary>
        /// 索引器：返回用户列表中的实体
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public Account this[int indx]
        {
            get
            {
                Account cam = null;
                if (_list.Count > 0 
                    && indx>=0
                    && indx < _list.Count)
                {
                    cam = (Account)_list.GetByIndex(indx);
                }
                return cam;
            }
        }

        /// <summary>
        /// 索引器：返回用户列表中的实体
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Account this[string userID]
        {
            get
            {
                Account acc = null;
                if (_list.ContainsKey(userID))
                {
                    acc = (Account)_list[userID];
                }
                return acc;
            }
        }

        /// <summary>
        /// 方法：用户列表从指定索引开始复制用户实体到给定的一维数组
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="indx"></param>
        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        /// <summary>
        /// 属性：返回用户列表中实体的数量
        /// </summary>
        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        /// <summary>
        /// 属性：是否同步
        /// </summary>
        public bool IsSynchronized
        {
            get { return false; }
        }


        /// <summary>
        /// 属性：SyncRoot
        /// </summary>
        public object SyncRoot
        {
            get { return this; }

        }

        /// <summary>
        /// 方法：获取枚举器
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }


    }
    #endregion

    #region 相机

    [Serializable]
    public class Camera : CtrllerObj, System.ComponentModel.INotifyPropertyChanged
    {
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
            }
        }

        private Camera()
        {
            MothballCalibrationSolutionList = new ProVision.Communal.CalibrationSolutionList();
            AcquireConditionList = new CameraAcquireConditionList();
        }

        /// <summary>
        /// 创建相机
        /// [注:同品牌同类型下的相机编号不允许相同]
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="ctrllerType"></param>
        /// <param name="camIndex"></param>
        /// <param name="camName"></param>
        public Camera(CtrllerBrand brand, CtrllerType ctrllerType, int camIndex, string camName) : this()
        {
            this.CtrllerCategory = ProCommon.Communal.CtrllerCategory.Camera;
            this.CtrllerBrand = brand;
            this.Number = camIndex;
            this.Name = camName;
            this.Type = ctrllerType.ToString();
            this.ID = brand.ToString() + "-" + ctrllerType.ToString() + "_" + camIndex.ToString("00");
        }

        /// <summary>
        /// 属性：相机使用标定方案的ID
        /// </summary>
        public string CalibrationID
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:标定方案
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public ProVision.Communal.CalibrationSolution CalibrationSolution
        {
            get
            {
                try
                {
                    if (MothballCalibrationSolutionList.Count > 0)
                    {
                        for (int i = 0; i < MothballCalibrationSolutionList.Count; i++)
                        {
                            var itm = MothballCalibrationSolutionList[i];
                            if (itm.IsActive) //选择激活方案
                            {
                                return itm;
                            }
                        }

                        //未有激活方案，默认第一个方案
                        return this.MothballCalibrationSolutionList[0];

                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 属性：标定方案列表实体(用于实体删减+查询)
        /// [备用的标定方案]
        /// </summary>      
        public ProVision.Communal.CalibrationSolutionList MothballCalibrationSolutionList
        {
            set; get;
        }

        /// <summary>
        /// 属性:标定方案实体列表(用于数据绑定+查询)
        /// [所有标定方案实体的列表]
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<ProVision.Communal.CalibrationSolution> CalibrationSolutionBList
        {
            get
            {
                System.ComponentModel.BindingList<ProVision.Communal.CalibrationSolution> calList = new System.ComponentModel.BindingList<ProVision.Communal.CalibrationSolution>();
                for (int i = 0; i < this.MothballCalibrationSolutionList.Count; i++)
                {
                    calList.Add(this.MothballCalibrationSolutionList[i]);
                }
                return calList;
            }
        }

        /// <summary>
        /// 相机采集条件列表
        /// </summary>
        public ProCommon.Communal.CameraAcquireConditionList AcquireConditionList { set; get; }

        /// <summary>
        /// 相机拍照外触发信号对象
        /// </summary>
        public ProCommon.Communal.InVarObj IN_GrabTrigger { set; get; }

        /// <summary>
        /// 属性：相机序列号
        /// </summary>
        public string SerialNo
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：相机的视频格式
        /// </summary>
        public string VideoFormat
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：相机帧率(Frame Per Second)
        /// </summary>
        public float FPS
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：曝光时间(微秒)
        /// </summary>
        public float ExposureTime
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：增益
        /// </summary>
        public float Gain { set; get; }

        /// <summary>
        /// 属性:相机是否连接
        /// </summary>
        private bool _isConnected;
        [System.Xml.Serialization.XmlIgnore]
        public bool IsConnected
        {
            set
            {
                //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                if (this._isConnected != value)
                {
                    this._isConnected = value;
                    //调用方法：通知属性值改变
                    this.NotifyPropertyChanged("IsConnected");
                }
            }
            get
            {
                return _isConnected;
            }
        }
    }

    [Serializable]
    public class CameraList : System.Collections.ICollection
    {
        public CameraList()
        {
            _list = new System.Collections.SortedList();
        }

        private System.Collections.SortedList _list;

        /// <summary>
        /// 方法：添加相机实体
        /// </summary>
        /// <param name="cam"></param>
        public void Add(Camera cam)
        {
            if (!_list.ContainsKey(cam.ID))
            {
                _list.Add(cam.ID, cam);
            }
        }

        /// <summary>
        /// 方法：删除相机实体
        /// </summary>
        /// <param name="cam"></param>
        public void Delete(Camera cam)
        {
            if (_list.ContainsKey(cam.ID))
            {
                _list.Remove(cam.ID);
            }
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        /// <summary>
        /// 索引器：返回相机列表中的实体
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public Camera this[int indx]
        {
            get
            {
                Camera cam = null;
                if (_list.Count > 0 && indx < _list.Count)
                {
                    cam = (Camera)_list.GetByIndex(indx);
                }
                return cam;
            }
        }

        /// <summary>
        /// 索引器：返回相机列表中的实体
        /// </summary>
        /// <param name="camID"></param>
        /// <returns></returns>
        public Camera this[string camID]
        {
            get
            {
                Camera cam = null;
                if (_list.ContainsKey(camID))
                {
                    cam = (Camera)_list[camID];
                }
                return cam;
            }
        }

        /// <summary>
        /// 方法：相机列表从指定索引开始复制相机实体到给定的一维数组
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="indx"></param>
        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        /// <summary>
        /// 属性：返回相机列表中实体的数量
        /// </summary>
        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        /// <summary>
        /// 属性：是否同步
        /// </summary>
        public bool IsSynchronized
        {
            get { return false; }
        }


        /// <summary>
        /// 属性：SyncRoot
        /// </summary>
        public object SyncRoot
        {
            get { return this; }

        }

        /// <summary>
        /// 方法：获取枚举器
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }

    }

    /// <summary>
    /// 相机采集模式
    /// </summary>
    public enum AcquisitionMode : uint
    {
        Continue = 0,
        SoftTrigger = 1,
        ExternalTrigger = 2
    }

    #region 光源信号

    public class LightSource
    {
        /// <summary>
        /// 光源ID
        /// </summary>
        public int ID { set; get; }

        public ProCommon.Communal.OutVarObj OUT_Light { set; get; }
    }

    public class LightSourceList : System.Collections.ICollection
    {
        private System.Collections.SortedList _list;

        public LightSourceList()
        {
            _list = new System.Collections.SortedList();
        }

        /// <summary>
        /// 方法：添加光源信号实体
        /// </summary>
        /// <param name="lgtSrc"></param>
        public void Add(LightSource lgtSrc)
        {
            if (!_list.ContainsKey(lgtSrc.ID))
            {
                _list.Add(lgtSrc.ID, lgtSrc);
            }
        }

        /// <summary>
        /// 方法：删除相机采集条件实体
        /// </summary>
        /// <param name="lgtSrc"></param>
        public void Delete(LightSource lgtSrc)
        {
            if (_list.ContainsKey(lgtSrc.ID))
            {
                _list.Remove(lgtSrc.ID);
            }
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        /// <summary>
        /// 索引器：返回相机采集条件列表中的实体
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public LightSource this[int indx]
        {
            get
            {
                LightSource lgtSigIntf = null;
                if (_list.Count > 0 && indx < _list.Count)
                {
                    lgtSigIntf = (LightSource)_list.GetByIndex(indx);
                }
                return lgtSigIntf;
            }
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public void CopyTo(Array array, int index)
        {
            _list.CopyTo(array, index);
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }

    #endregion

    #region 相机采集条件

    /// <summary>
    /// 相机拍照采集条件
    /// </summary>
    [Serializable]
    public class CameraAcquireCondition
    {
        public CameraAcquireCondition()
        {
            LighterList = new LightSourceList();
        }

        /// <summary>
        /// 相机拍照的条件标号
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 相机拍照在当前条件下的曝光时间
        /// </summary>
        public float EnvrExposureTime { set; get; }

        /// <summary>
        /// 相机拍照在当前条件下的增益
        /// </summary>
        public float EnvrGain { set; get; }

        /// <summary>
        /// 相机拍照在当前条件下的采集超时
        /// [注:获取图像超时]
        /// </summary>
        public float AcquireTimeout { set; get; }

        /// <summary>
        /// 相机拍照在当前条件下的重采次数
        /// [注:在重采次数内,前次采集超时后,重采]
        /// </summary>
        public int ReAcquireCount { set; get; }

        /// <summary>
        /// 光源信号列表
        /// </summary>
        public ProCommon.Communal.LightSourceList LighterList { set; get; }

    }

    /// <summary>
    /// 相机拍照采集条件列表
    /// </summary>
    [Serializable]
    public class CameraAcquireConditionList : System.Collections.ICollection
    {
        private System.Collections.SortedList _list;

        public CameraAcquireConditionList()
        {
            _list = new System.Collections.SortedList();
        }

        /// <summary>
        /// 方法：添加相机采集条件实体
        /// </summary>
        /// <param name="camAcquireCdt"></param>
        public void Add(CameraAcquireCondition camAcquireCdt)
        {
            if (!_list.ContainsKey(camAcquireCdt.ID))
            {
                _list.Add(camAcquireCdt.ID, camAcquireCdt);
            }
        }

        /// <summary>
        /// 方法：删除相机采集条件实体
        /// </summary>
        /// <param name="camAcquireCdt"></param>
        public void Delete(CameraAcquireCondition camAcquireCdt)
        {
            if (_list.ContainsKey(camAcquireCdt.ID))
            {
                _list.Remove(camAcquireCdt.ID);
            }
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        /// <summary>
        /// 索引器：返回相机采集条件列表中的实体
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public CameraAcquireCondition this[int indx]
        {
            get
            {
                CameraAcquireCondition camAcquireCdt = null;
                if (_list.Count > 0 && indx < _list.Count)
                {
                    camAcquireCdt = (CameraAcquireCondition)_list.GetByIndex(indx);
                }
                return camAcquireCdt;
            }
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public void CopyTo(Array array, int index)
        {
            _list.CopyTo(array, index);
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
    #endregion

    #endregion

    #region 运控器[板卡]
    [Serializable]
    public class BoardCtrller : CtrllerObj, System.ComponentModel.INotifyPropertyChanged
    {
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private AxisList _axisList;
        private InVarObjList _inVarObjList;
        private OutVarObjList _outVarObjList;

        /// <summary>
        /// 控制器错误代码
        /// [注:报警信号组]
        /// </summary>
        public int[] ErrorCode { set; get; }

        /// <summary>
        /// 控制器错误等级
        /// [注:报警信号等级]
        /// </summary>
        public int ErrorLevel { set; get; }

        /// <summary>
        /// 设备状态
        /// [注:设备的运行状态记录在控制器内]
        /// </summary>
        public int MachineStatus { set; get; }

        private BoardCtrller()
        {
            this._axisList = new AxisList();
            this._inVarObjList = new InVarObjList();
            this._outVarObjList = new OutVarObjList();
        }

        /// <summary>
        /// 创建运控器
        /// [注:同品牌同类型的控制器编号不允许相同]
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="ctrllerType"></param>
        /// <param name="ctrllerName"></param>
        /// <param name="ctrllerIndex"></param>
        public BoardCtrller(CtrllerBrand brand, CtrllerType ctrllerType, string ctrllerName, int ctrllerIndex) : this()
        {
            this.CtrllerCategory = CtrllerCategory.Board;
            this.CtrllerBrand = brand;
            this.Name = ctrllerName;
            this.Number = ctrllerIndex;
            this.Type = ctrllerType.ToString();
            this.ID = brand.ToString() + "-" + ctrllerType.ToString() + "_" + ctrllerIndex.ToString("00");
        }


        /// <summary>
        /// 属性：轴列表实体(用于实体删减+查询)
        /// </summary>
        public AxisList AxisList
        {
            set { this._axisList = value; }
            get { return this._axisList; }
        }

        /// <summary>
        /// 属性：轴实体的列表(用于数据绑定+查询)
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<Axis> AxisBList
        {
            get
            {
                System.ComponentModel.BindingList<Axis> axislist = new System.ComponentModel.BindingList<Axis>();
                for (int i = 0; i < this._axisList.Count; i++)
                {
                    axislist.Add(this._axisList[i]);
                }
                return axislist;
            }
        }

        /// <summary>
        /// 属性：控制变量列表实体（用于实体删减+查询）
        /// </summary>
        public InVarObjList InVarObjList
        {
            set { this._inVarObjList = value; }
            get { return this._inVarObjList; }
        }

        private System.ComponentModel.BindingList<InVarObj> _ctrlnVarObjList;

        /// <summary>
        /// 属性：控制变量实体的列表(用于数据绑定+查询)
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<InVarObj> InVarObjBList
        {
            get
            {
                if (_ctrlnVarObjList == null)
                {
                    _ctrlnVarObjList = new System.ComponentModel.BindingList<InVarObj>();
                    _ctrlnVarObjList.Clear();

                    for (int i = 0; i < this._inVarObjList.Count; i++)
                    {
                        _ctrlnVarObjList.Add(this._inVarObjList[i]);
                    }
                }

                return _ctrlnVarObjList;
            }
        }

        /// <summary>
        /// 属性：控制变量列表实体（用于实体删减+查询）
        /// </summary>
        public OutVarObjList OutVarObjList
        {
            set { this._outVarObjList = value; }
            get { return this._outVarObjList; }
        }

        public System.ComponentModel.BindingList<OutVarObj> _ctrOutVarObjList;

        /// <summary>
        /// 属性：控制变量实体的列表(用于数据绑定+查询)
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<OutVarObj> OutVarObjBList
        {
            get
            {
                if (_ctrOutVarObjList.Count() == 0)
                {
                    _ctrOutVarObjList = new System.ComponentModel.BindingList<OutVarObj>();
                    _ctrOutVarObjList.Clear();

                    for (int i = 0; i < this._outVarObjList.Count; i++)
                    {
                        _ctrOutVarObjList.Add(this._outVarObjList[i]);
                    }
                }

                return _ctrOutVarObjList;
            }
        }       

        private bool _isConnected;     

        /// <summary>
        /// 属性:是否连接正常
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public bool IsConnected
        {
            get
            {
                return this._isConnected;
            }
            set
            {
                //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                if (this._isConnected != value)
                {
                    this._isConnected = value;
                    this.NotifyPropertyChanged("IsConnected");
                }
            }
        }

        /// <summary>
        /// 属性：运控器IP地址
        /// </summary>
        public string CtrllerEthernetIP { set; get; }

        /// <summary>
        /// 属性:控制器端口号
        /// </summary>
        public int CtrllerEthernetPort { set; get; }

    }

    [Serializable]
    public class BoardCtrllerList : System.Collections.ICollection
    {
        //列表
        private System.Collections.SortedList _list;
        public BoardCtrllerList()
        {
            _list = new System.Collections.SortedList();
        }

        /// <summary>
        /// 方法:添加运控器实体
        /// </summary>
        /// <param name="mctrller"></param>
        public void Add(BoardCtrller mctrller)
        {
            if (!_list.ContainsKey(mctrller.ID))
            {
                _list.Add(mctrller.ID, mctrller);
            }
        }

        /// <summary>
        /// 方法:删除运控器实体
        /// </summary>
        /// <param name="mctrller"></param>
        public void Delete(BoardCtrller mctrller)
        {
            if (_list.ContainsKey(mctrller.ID))
            {
                _list.Remove(mctrller.ID);
            }
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        /// <summary>
        /// 索引器:返回运控器列表中的实体
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public BoardCtrller this[int indx]
        {
            get
            {
                if (_list.Count > 0 && indx < _list.Count)
                {
                    return (BoardCtrller)_list.GetByIndex(indx);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 索引器:返回运控器列表中的实体
        /// </summary>
        /// <param name="mctrllerID"></param>
        /// <returns></returns>
        public BoardCtrller this[string mctrllerID]
        {
            get
            {
                if (_list.ContainsKey(mctrllerID))
                {
                    return (BoardCtrller)_list[mctrllerID];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 方法：运控器列表从指定索引开始复制运控器实体到给定的一维数组
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="indx"></param>
        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        /// <summary>
        /// 属性：返回运控板列表中实体的数量
        /// </summary>
        public int Count
        {
            get { return _list.Count; }
        }

        /// <summary>
        /// 属性:是否同步
        /// </summary>
        public bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// 属性：SyncRoot
        /// </summary>
        public object SyncRoot
        {
            get { return this; }
        }

        /// <summary>
        /// 方法：获取枚举器
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
    #endregion

    #region 通信Socket
    [Serializable]
    public class ComWrappedSocket : CtrllerObj, System.ComponentModel.INotifyPropertyChanged
    {
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        public ComWrappedSocket() { }

        /// <summary>
        /// Socket对象
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.Net.Sockets.Socket Socket { set; get; }

        /// <summary>
        /// 属性:协议类型
        /// </summary>
        public System.Net.Sockets.ProtocolType ProtocolType
        {
            set;
            get;
        }

        public string IP
        {
            set;
            get;
        }

        public int Port
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：发送超时(毫秒)
        /// </summary>
        public int SendTimeOut
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：接收超时(毫秒)
        /// </summary>
        public int ReceiveTimeOut
        {
            set;
            get;
        }

        private bool _isConnected;
        /// <summary>
        /// 属性:是否连接正常
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public bool IsConnected
        {
            get
            {
                return this._isConnected;
            }
            set
            {
                //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                if (this._isConnected != value)
                {
                    this._isConnected = value;
                    this.NotifyPropertyChanged("IsConnected");
                }
            }
        }

    }

    [Serializable]
    public class ComWrappedSocketList : System.Collections.ICollection
    {
        //列表
        private System.Collections.SortedList _list;

        public ComWrappedSocketList() { _list = new System.Collections.SortedList(); }

        /// <summary>
        /// 方法:添加ComSocket实体
        /// </summary>
        /// <param name="comSocket"></param>
        public void Add(ComWrappedSocket comSocket)
        {
            if (!_list.ContainsKey(comSocket.ID))
            {
                _list.Add(comSocket.ID, comSocket);
            }
        }

        /// <summary>
        /// 方法:删除ComSocket实体
        /// </summary>
        /// <param name="comSocket"></param>
        public void Delete(ComWrappedSocket comSocket)
        {
            if (_list.ContainsKey(comSocket.ID))
            {
                _list.Remove(comSocket.ID);
            }
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        /// <summary>
        /// 索引器:返回ComSocketList列表中的实体
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public ComWrappedSocket this[int indx]
        {
            get
            {
                if (_list.Count > 0 && indx < _list.Count)
                {
                    return (ComWrappedSocket)_list.GetByIndex(indx);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 索引器:返回ComSocketList列表中的实体
        /// </summary>
        /// <param name="tcpSocketID"></param>
        /// <returns></returns>
        public ComWrappedSocket this[string tcpSocketID]
        {
            get
            {
                if (_list.ContainsKey(tcpSocketID))
                {
                    return (ComWrappedSocket)_list[tcpSocketID];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 方法：通信对象列表从指定索引开始复制通信对象到给定的一维数组
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="indx"></param>
        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        /// <summary>
        /// 属性：返回ComSocket列表中实体的数量
        /// </summary>
        public int Count
        {
            get { return _list.Count; }
        }

        /// <summary>
        /// 属性:是否同步
        /// </summary>
        public bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// 属性：SyncRoot
        /// </summary>
        public object SyncRoot
        {
            get { return this; }
        }

        /// <summary>
        /// 方法：获取枚举器
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
    #endregion

    #region 通信SerialPort
    [Serializable]
    public class ComWrappedSerialPort : CtrllerObj, System.ComponentModel.INotifyPropertyChanged
    {
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private ComWrappedSerialPort() { }

        public ComWrappedSerialPort(string serialName, int serialIdex)
        {
            this.Name = serialName;
            this.Number = serialIdex;
            this.ID = "SerialPort_" + serialIdex.ToString("00");
        }

        [System.Xml.Serialization.XmlIgnore]
        public System.IO.Ports.SerialPort SerialPort
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：发送超时(毫秒)
        /// </summary>
        public int SendTimeOut
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：接收超时(毫秒)
        /// </summary>
        public int ReceiveTimeOut
        {
            set;
            get;
        }

        private bool _isConnected;
        /// <summary>
        /// 属性:是否连接正常
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public bool IsConnected
        {
            get
            {
                return this._isConnected;
            }
            set
            {
                //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                if (this._isConnected != value)
                {
                    this._isConnected = value;
                    this.NotifyPropertyChanged("IsConnected");
                }
            }
        }

        public bool IsEffective { set; get; }
        public System.IO.Ports.Handshake HandShake { set; get; }
        public int BaudRate { set; get; }
        public int DataBits { set; get; }
        public System.IO.Ports.StopBits StopBits { set; get; }
        public System.IO.Ports.Parity Parity { set; get; }
        public string NewLine { get; set; }
        public int ReceivedBytesThreshold { set; get; }
        public bool DtrEnable { get; set; }
        public bool RtsEnable { get; set; }
    }

    [Serializable]
    public class ComWrappedSerialPortList : System.Collections.ICollection
    {
        //列表
        private System.Collections.SortedList _list;
        public ComWrappedSerialPortList() { _list = new System.Collections.SortedList(); }


        /// <summary>
        /// 方法:添加ComSerialPort实体
        /// </summary>
        /// <param name="comSerialPort"></param>
        public void Add(ComWrappedSerialPort comSerialPort)
        {
            if (!_list.ContainsKey(comSerialPort.ID))
            {
                _list.Add(comSerialPort.ID, comSerialPort);
            }
        }

        /// <summary>
        /// 方法:删除ComSerialPort实体
        /// </summary>
        /// <param name="comSerialPort"></param>
        public void Delete(ComWrappedSerialPort comSerialPort)
        {
            if (_list.ContainsKey(comSerialPort.ID))
            {
                _list.Remove(comSerialPort.ID);
            }
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        /// <summary>
        /// 索引器:返回ComSerialPortList列表中的实体
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public ComWrappedSerialPort this[int indx]
        {
            get
            {
                if (_list.Count > 0 && indx < _list.Count)
                {
                    return (ComWrappedSerialPort)_list.GetByIndex(indx);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 索引器:返回ComSerialPortList列表中的实体
        /// </summary>
        /// <param name="comSerialPortID"></param>
        /// <returns></returns>
        public ComWrappedSerialPort this[string comSerialPortID]
        {
            get
            {
                if (_list.ContainsKey(comSerialPortID))
                {
                    return (ComWrappedSerialPort)_list[comSerialPortID];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 方法：通信对象列表从指定索引开始复制通信对象到给定的一维数组
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="indx"></param>
        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        /// <summary>
        /// 属性：返回ComSerialPort列表中实体的数量
        /// </summary>
        public int Count
        {
            get { return _list.Count; }
        }

        /// <summary>
        /// 属性:是否同步
        /// </summary>
        public bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// 属性：SyncRoot
        /// </summary>
        public object SyncRoot
        {
            get { return this; }
        }

        /// <summary>
        /// 方法：获取枚举器
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }

    }
    #endregion

    #region 通信Web
    [Serializable]
    public class ComWrappedWeb : CtrllerObj, System.ComponentModel.INotifyPropertyChanged
    {
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ComWrappedWeb() { }

        /// <summary>
        /// 属性：输出库文件路径
        /// </summary>
        public string OutDllFileName
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：代理类名称
        /// </summary>
        public string ProxyClassName
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：服务链接
        /// </summary>
        public string URL
        {
            set;
            get;
        }

        private bool _isConnected;
        /// <summary>
        /// 属性:是否连接正常
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public bool IsConnected
        {
            get
            {
                return this._isConnected;
            }
            set
            {
                if (this._isConnected != value)
                {
                    this._isConnected = value;
                    this.NotifyPropertyChanged("IsConnected");
                }
            }
        }
    }

    [Serializable]
    public class ComWrappedWebList : System.Collections.ICollection
    {
        //WEB列表
        public System.Collections.SortedList _list;

        public ComWrappedWebList() { _list = new System.Collections.SortedList(); }

        public void Add(ComWrappedWeb web)
        {
            if (!_list.ContainsKey(web.URL))
            {
                _list.Add(web.URL, web);
            }
        }

        /// <summary>
        /// 方法:删除WEB实体
        /// </summary>
        /// <param name="url"></param>
        public void Delete(string url)
        {
            if (_list.ContainsKey(url))
            {
                _list.Remove(url);
            }
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }


        /// <summary>
        /// 索引器:返回WEB列表中的实体
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public ComWrappedWeb this[int indx]
        {
            get
            {
                if (_list.Count > 0 && indx < _list.Count)
                {
                    return (ComWrappedWeb)_list.GetByIndex(indx);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 索引器:返回WEB列表中的实体
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public ComWrappedWeb this[string url]
        {
            get
            {
                if (_list.ContainsKey(url))
                {
                    return (ComWrappedWeb)_list[url];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 方法：WEB列表从指定索引开始复制WEB实体到给定的一维数组
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="indx"></param>
        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        /// <summary>
        /// 属性：返回WEB列表中实体的数量
        /// </summary>
        public int Count
        {
            get { return _list.Count; }
        }

        /// <summary>
        /// 属性：是否同步
        /// </summary>
        public bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// 属性：SyncRoot
        /// </summary>
        public object SyncRoot
        {
            get { return this; }
        }

        /// <summary>
        /// 方法：获取枚举器
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
    #endregion

    #region 异常类

    /// <summary>
    ///  初始化异常类
    /// </summary>
    public class InitException : System.Exception
    {
        /// <summary>
        /// 字段：设备
        /// </summary>
        private string _device;

        /// <summary>
        /// 字段：原因
        /// </summary>
        private string _reason;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="device"></param>
        /// <param name="reason"></param>
        public InitException(string device, string reason)
            : base(reason)
        {
            this._device = device;
            this._reason = reason;
        }
        /// <summary>
        /// 覆写：ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("设备[{0}]初始化时发生错误:{1}", _device, _reason);
        }

    }

    /// <summary>
    /// 启动异常类
    /// </summary>
    public class StartException : System.Exception
    {
        /// <summary>
        /// 字段：设备
        /// </summary>
        private string _device;

        /// <summary>
        /// 字段：原因
        /// </summary>
        private string _reason;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="device"></param>
        /// <param name="reason"></param>
        public StartException(string device, string reason)
            : base(reason)
        {
            this._device = device;
            this._reason = reason;
        }
        /// <summary>
        /// 覆写：ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("设备[{0}]启动时发生错误:{1}", _device, _reason);
        }

    }

    /// <summary>
    /// 停止异常类
    /// </summary>
    public class StopException : System.Exception
    {
        /// <summary>
        /// 字段：设备
        /// </summary>
        private string _device;

        /// <summary>
        /// 字段：原因
        /// </summary>
        private string _reason;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="device"></param>
        /// <param name="reason"></param>
        public StopException(string device, string reason)
            : base(reason)
        {
            this._device = device;
            this._reason = reason;
        }
        /// <summary>
        /// 覆写：ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("设备[{0}]停止时发生错误:{1}", _device, _reason);
        }

    }

    /// <summary>
    /// 释放异常类
    /// </summary>
    public class ReleaseException : System.Exception
    {
        /// <summary>
        /// 字段：设备
        /// </summary>
        private string _device;

        /// <summary>
        /// 字段：原因
        /// </summary>
        private string _reason;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="device"></param>
        /// <param name="reason"></param>
        public ReleaseException(string device, string reason)
            : base(reason)
        {
            this._device = device;
            this._reason = reason;
        }
        /// <summary>
        /// 覆写：ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("设备[{0}]释放时发生错误:{1}", _device, _reason);
        }

    }

    /// <summary>
    /// 加载异常类
    /// </summary>
    public class LoadException : Exception
    {
        private string _configName;
        private string _reason;

        public LoadException(string configName, string reason)
            : base(reason)
        {
            this._configName = configName;
            this._reason = reason;
        }

        /// <summary>
        /// 覆写方法：Exception的ToString()方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("配置[{0}]加载时发生错误:{1}", this._configName, this._reason);
        }
    }

    #endregion

    /// <summary>
    /// 操作状态
    /// </summary>
    public enum OperationState : int
    {
        View = 0,
        New = 1,
        Insert = 2,
        Modify = 3
    }   
}
