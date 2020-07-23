using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       Defination
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProVision.Communal
 * File      Name：       Defination
 * Creating  Time：       12/20/2019 6:28:43 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProVision.Communal
{
    /// <summary>
    /// 像素坐标
    /// </summary>
    public struct PixelCoordinate
    {
        public double Row;
        public double Col;
    }

    /// <summary>
    /// 世界坐标
    /// </summary>
    public struct WorldCoordinate
    {
        public double AXIS1Value;
        public double AXIS2Value;
        public double AXIS3Value;
    }

    /// <summary>
    /// 标定点对
    /// [注:2D标定]
    /// </summary>
    [System.Serializable]
    public struct CalibrationPointPair
    {
        public PixelCoordinate PC { set; get; }
        public WorldCoordinate WC { set; get; }
    }

    #region 标定方案
    [Serializable]
    public class CalibrationSolution : System.ComponentModel.INotifyPropertyChanged
    {
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private CalibrationSolution() { this.CalibrationPointPairBList = new System.ComponentModel.BindingList<ProVision.Communal.CalibrationPointPair>(); }

        /// <summary>
        /// 创建标定方案
        /// [注:同属相机的标定方案的编号不允许相同]
        /// </summary>
        /// <param name="calType">方案类型
        /// 基于标定板，基于九点</param>
        /// <param name="cameraName">所属相机名称</param>
        /// <param name="calIndex">方案的编号</param>
        public CalibrationSolution(ProVision.Communal.CalibrationType calType, string cameraName, int calIndex) : this()
        {
            this.CalibrationType = calType;
            CreateTime = DateTime.Now;
            this.ID = "Calibration_" + calIndex.ToString("00");
            this.CameraName = cameraName;
        }

        /// <summary>
        /// 标定类型
        /// </summary>
        public ProVision.Communal.CalibrationType CalibrationType
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:标定方案编号
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 属性：标定方案ID
        /// </summary>
        public string ID
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：标定方案创建者
        /// </summary>
        public string Creator
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：标定方案创建日期
        /// </summary>
        public DateTime? CreateTime
        {
            set;
            get;
        }

        public string CameraName
        {
            set; get;
        }

        /// <summary>
        /// 属性：方案是否激活
        /// [注:在备选方案列表中被选中,即被激活]
        /// </summary>
        public bool IsActive
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:方案是否有效
        /// [注：根据标定点对计算转换矩阵后有效]
        /// </summary>
        public bool IsEffective { set; get; }

        /// <summary>
        /// 属性:标定点对列表
        /// </summary>
        public System.ComponentModel.BindingList<ProVision.Communal.CalibrationPointPair> CalibrationPointPairBList { set; get; }

        /// <summary>
        /// 属性:基于特征点的标定结果
        /// </summary>
        public ProVision.Communal.ResultOfCalibrationPoint ResultOfCaliPoint { set; get; }
      
    }

    [Serializable]
    public class CalibrationSolutionList : System.Collections.ICollection
    {
        public CalibrationSolutionList()
        {
            _list = new System.Collections.SortedList();
        }

        private System.Collections.SortedList _list;

        /// <summary>
        /// 方法:添加标定方案实体
        /// </summary>
        /// <param name="cal"></param>
        public void Add(CalibrationSolution cal)
        {
            if (!_list.ContainsKey(cal.ID))
            {
                _list.Add(cal.ID, cal);
            }
        }

        /// <summary>
        /// 方法：删除标定方案实体
        /// </summary>
        /// <param name="cal"></param>
        public void Delete(CalibrationSolution cal)
        {
            if (_list.ContainsKey(cal.ID))
            {
                _list.Remove(cal.ID);
            }
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        /// <summary>
        /// 索引器：返回标定列表中的标定方案实体
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public CalibrationSolution this[int indx]
        {
            get
            {
                CalibrationSolution cal = null;
                if (_list.Count > 0 && indx < _list.Count)
                {
                    cal = (CalibrationSolution)_list.GetByIndex(indx);
                }
                return cal;
            }
        }

        /// <summary>
        /// 索引器：返回标定列表中的标定方案实体
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public CalibrationSolution this[string id]
        {
            get
            {
                CalibrationSolution cal = null;
                if (_list.ContainsKey(id))
                {
                    cal = (CalibrationSolution)_list[id];
                }
                return cal;
            }
        }

        /// <summary>
        /// 方法：标定列表从指定索引开始复制标定实体到给定的一维数组
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="indx"></param>
        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        /// <summary>
        /// 属性：返回标定列表中实体的数量
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
    /// 基于特征点的标定结果
    /// [注:2D标定]
    /// </summary>
    public class ResultOfCalibrationPoint/*:System.Xml.Serialization.IXmlSerializable*/
    {
        /// <summary>
        /// 像素坐标系到世界坐标系的齐次转换矩阵
        /// </summary>
        public double[] PC2WCHomMat2D { set; get; }

        /// <summary>
        /// 世界坐标系到像素坐标系的齐次转换矩阵
        /// </summary>
        public double[] WC2PCHomMat2D { set; get; }

        /// <summary>
        /// 第一轴缩放系数
        /// </summary>
        public double Sx { set; get; }

        /// <summary>
        /// 第二轴缩放系数
        /// </summary>
        public double Sy { set; get; }

        /// <summary>
        /// 旋转角
        /// </summary>
        public double Phi { set; get; }

        /// <summary>
        /// 斜切角
        /// </summary>
        public double Theta { set; get; }

        /// <summary>
        /// 第一轴平移量
        /// </summary>
        public double Tx { set; get; }

        /// <summary>
        /// 第二轴平移量
        /// </summary>
        public double Ty { set; get; }

        /// <summary>
        /// 标定结果的均值误差
        /// [像素坐标均值误差]
        /// </summary>
        public double CalibrationPixelError { set; get; }

        /// <summary>
        /// 标定结果的均值误差
        /// [物理坐标均值误差]
        /// </summary>
        public double CalibrationPhysicalError { set; get; }
       

        #region 实现IXmlSerializable--读取暂时有问题,待完善

        /*

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            bool isEmpty = reader.IsEmptyElement;
            if (isEmpty) return;

            while(reader.NodeType!=System.Xml.XmlNodeType.EndElement)
            {
                System.Xml.Serialization.XmlSerializer xmls = new System.Xml.Serialization.XmlSerializer(typeof(double[]));
                reader.ReadStartElement("PC2WCHomMat2D");
                
                double[] tmpArr = xmls.Deserialize(reader) as double[];
                reader.ReadEndElement();
                PC2WCHomMat2D = new HalconDotNet.HTuple(tmpArr);

                reader.ReadStartElement("WC2PCHomMat2D");
                tmpArr = xmls.Deserialize(reader) as double[];
                reader.ReadEndElement();
                WC2PCHomMat2D = new HalconDotNet.HTuple(tmpArr);

                reader.MoveToContent();
            }

            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
           // System.Xml.Serialization.XmlSerializerNamespaces xmlNmSpc = new System.Xml.Serialization.XmlSerializerNamespaces(new System.Xml.XmlQualifiedName[] { new System.Xml.XmlQualifiedName() });

            double[] tmpArr = PC2WCHomMat2D.ToDArr();
            System.Xml.Serialization.XmlSerializer xmls = new System.Xml.Serialization.XmlSerializer(typeof(double[]));
            writer.WriteStartElement("PC2WCHomMat2D");
            xmls.Serialize(writer, tmpArr);
            writer.WriteEndElement();

            tmpArr = WC2PCHomMat2D.ToDArr();          
            writer.WriteStartElement("WC2PCHomMat2D");
            xmls.Serialize(writer, tmpArr);
            writer.WriteEndElement();
        }


        */

        #endregion

        
    }

    #endregion



    /// <summary>
    /// 类:模板匹配模型参数
    /// </summary>
    [System.Serializable]
    public class MatchModelParameter
    {
        // ---------------- Create model -------------------

        /// <summary>
        /// Measure for local gray value differences between
        /// the object and the background and between
        /// different parts of the object
        /// 对比度
        /// </summary>
        public int Contrast;

        /// <summary>
        /// Minimum scale of the model
        /// 模板的最小缩放系数
        /// </summary>
        public double ScaleMin;

        /// <summary>
        /// Maximum scale of the model
        /// 模板的最大缩放系数
        /// </summary>
        public double ScaleMax;

        /// <summary>
        /// Step length within the selected range of scales
        /// 选择的缩放系数范围内的增长步长
        /// </summary>
        public double ScaleStep;

        /// <summary>
        /// Smallest rotation of the model
        /// 匹配模板的实例的最小旋转角(即起始角)
        /// </summary>
        public double StartAngle;

        /// <summary>
        /// Extent of the rotation angles
        /// 匹配模板的实例的旋转角范围
        /// </summary>
        public double AngleExtent;

        /// <summary>
        /// Step length within the selected range of angles
        /// 选择的旋转角范围内的增长角步长
        /// </summary>
        public double AngleStep;

        /// <summary>
        ///  Defines the maximum number of pyramid levels
        ///  定义金字塔等级最大数
        /// </summary>
        public int NumLevels;


        /// <summary>
        /// Conditions determining how the model is supposed to be 
        /// recognized in the image
        /// 度量条件,决定模板匹配的方式
        /// [注:'use_polarity','ignore_global_polarity'
        /// 'ignore_local_polarity','ignore_color_polarity']
        /// </summary>
        public string Metric;

        /// <summary>
        /// Defines the kind of optimization and optionally the
        /// kind of method used for generating the model
        /// 定义优化类型以及可选的方法，用于创建模板匹配模型
        /// [注:'none','point_reduction_low',
        /// 'point_reduction_medium','point_reduction_high']
        /// </summary>
        public string Optimization;

        /// <summary>
        /// Used to separate the model from the noise in the image
        /// 最小对比度[注:用于将模板从噪声图像中分割出来]
        /// </summary>
        public int MinContrast;


        // ---------------- Find model -------------------

        /// <summary>
        /// Defines the score a potential match must have at least
        /// to be accepted as an instance of the model in the image
        /// 匹配模板的实例的最小匹配得分
        /// </summary>
        public double MinScore;

        /// <summary>
        /// Number of instances of the model to be found
        /// 可能的匹配模板的实例数
        /// </summary>
        public int NumToMatch;

        /// <summary>
        /// 'Greediness' of the search heuristic (0 means safe but slow
        /// 1 means fast but matches may be missed).
        /// 贪婪度
        /// </summary>
        public double Greediness;

        /// <summary>
        ///  Defines fraction two instances may at most overlap in order
        ///  to consider them as different instances, and hence
        ///  to be returned separately
        ///  最大重叠比
        /// </summary>
        public double MaxOverLap;

        /// <summary>
        /// Determines whether the instances should be extracted with
        /// subpixel accuracy
        /// 亚像素等级
        /// [注:"interpolation","least_squares","least_squares_high",
        /// "least_squares_very_high","max_deformation 1","max_deformation 2",
        /// "max_deformation 3","max_deformation 4","max_deformation 5",
        /// "max_deformation 6","none"]
        /// </summary>
        public string SubPixel;

        /// <summary>
        /// Determines the lowest pyramid level to which the
        /// found matches are tracked. Mechanism is used to
        /// speed up the matching
        /// 可以找到匹配模板的实例的最低金字塔等级
        /// </summary>
        public int LastNumLevel;

        // ---------------- Optimize recognition speed -------------------

        /// <summary>
        /// Defines heuristic for evaluating the detection results
        /// 评估搜索结果的启发式选项
        /// '0'means'=','1'means'>='
        /// [注:指定匹配模板的实例数后,
        /// 以识别率作为参数优化的启发项]
        /// </summary>
        public int RecogRateOpt;

        /// <summary>
        /// Defines rate for the model recognition
        /// 模板识别率
        /// </summary>
        public int RecogRate;

        /// <summary>
        /// Defines mode to determine accuracy of recognition
        /// 识别精度的模式
        /// [注:指定匹配模板的实例满足的数量条件,
        /// 在此条件下得到的识别精度]
        /// </summary>
        public string RecogAccuracyMode;

        /// <summary>
        /// Manual selection of number of matches to be found.
        /// 用户指定的匹配模板的实例数
        /// [注:优化识别时指定数量]
        /// </summary>
        public int RecogSpecifiedMatchNum;

        // ---------------- Inspect values -------------------

        /// <summary>
        /// Is equal to the value of the parameter 'NumToMatch'
        /// 等于'可能的匹配模板的实例数'
        /// [注:检查统计时的最大数量]
        /// </summary>
        public int InspectMaxNoMatch;

        /// <summary>
        /// List of parameters to be determined automatically
        /// 自动优化参数列表
        /// </summary>
        public System.Collections.ArrayList AutoParamList;

        /// <summary>
        /// Constant defining the auto-mode for the parameter 'PyramidLevels'
        /// 常量标记:图层金字塔最大等级数(可自动计算的参数)
        /// </summary>
        public const string AUTO_NUM_LEVELS = "num_levels";

        /// <summary>
        /// Constant defining the auto-mode for the parameter 'Contrast'
        /// 常量标记:对比度(可自动计算的参数)
        /// </summary>
		public const string AUTO_CONTRAST = "contrast";

        /// <summary>
        /// Constant defining the auto-mode for the parameter 'ScaleStep'
        /// 常量标记:缩放系数增长步长(可自动计算的参数)
        /// </summary>
        public const string AUTO_SCALE_STEP = "scale_step";

        /// <summary>
        /// Constant defining the auto-mode for the parameter 'AngleStep'
        /// 常量标记:旋转角增长步长(可自动计算的参数)
        /// </summary>
		public const string AUTO_ANGLE_STEP = "angle_step";

        /// <summary>
        /// Constant defining the auto-mode for the parameter 'MinContrast'
        /// 常量标记:最小对比度(可自动计算的参数)
        /// </summary>
		public const string AUTO_MIN_CONTRAST = "min_contrast";

        /// <summary>
        /// Constant defining the auto-mode for the parameter 'Optimization'
        /// 常量标记:优化选项(可自动计算的参数)
        /// </summary>
		public const string AUTO_OPTIMIZATION = "optimization";

        /// <summary>
        /// Constant indicating an update for the button representation of
        /// 'AngleStart'.
        /// 常量标记:旋转角的起始角(关联该参数的控件)
        /// </summary>
        public const string CONTROL_ANGLE_START = "angle_start";

        /// <summary>
        /// Constant indicating an update for the button representation of
        /// 'AngleExtent'.
        /// 常量标记:旋转角范围(关联该参数的控件)
        /// </summary>
		public const string CONTROL_ANGLE_EXTENT = "angle_extent";

        /// <summary>
        /// Constant indicating an update for the button representation of
        /// 'ScaleMin'.
        /// 常量标记:最小缩放系数(关联该参数的控件)
        /// </summary>
		public const string CONTROL_SCALE_MIN = "scale_min";

        /// <summary>
        /// Constant indicating an update for the button representation of
        /// 'ScaleMax'.
        /// 常量标记:最大缩放系数(关联该参数的控件)
        /// </summary>
		public const string CONTROL_SCALE_MAX = "scale_max";

        /// <summary>
        /// Constant indicating an update for the button representation of
        /// 'Metric'.
        /// 常量标记:度量条件(关联该参数的控件)
        /// </summary>
		public const string CONTROL_METRIC = "metric";

        /// <summary>
        /// Constant indicating an update for the button representation of
        /// 'MinScore'.
        /// 常量标记:最小匹配分数(关联该参数的控件)
        /// </summary>
		public const string CONTROL_MINSCORE = "min_score";

        /// <summary>
        /// Constant indicating an update for the button representation of
        /// 'Greediness'.
        /// 常量标记:贪婪度(关联该参数的控件)
        /// </summary>
		public const string CONTROL_GREEDINESS = "greediness";

        /// <summary>
        /// Constant defining the number of instances to be detected:
        /// Find number of models specified by the user
        /// 常量标记:搜索用户指定数量的匹配模板的实例(识别精度的模式)
        /// </summary>
		public const string RECOG_MODE_SPECIFIEDNUM = "RecognFindSpecifiedNumber";

        /// <summary>
        /// Constant defining the number of instances to be detected:
        /// Find at least one model instance per image
        /// 常量标记:搜索至少一个匹配模板的实例(识别精度的模式)
        /// </summary>
		public const string RECOG_MODE_ATLEASTONE = "RecognAtLeast";

        /// <summary>
        /// Constant defining the number of instances to be detected:
        /// Find maximum number of model instances per image
        /// 常量标记:搜索最大数量的匹配模板的实例(识别精度的模式)
        /// </summary>
		public const string RECOG_MODE_MAXIMUMNUM = "RecognFindMaximum";

        /// <summary>
        /// Constant indicating a range of ScaleStep for
        /// its GUI component representation 
        /// 常量标记:对应缩放系数增长步长的范围
        /// </summary>
        public const string RANGE_SCALE_STEP = "RangeScaleStep";


        /// <summary>
        /// Constant indicating a range of AngleStep for
        /// its GUI component representation 
        /// 常量标记:对应旋转角度增长步长的范围
        /// </summary>
		public const string RANGE_ANGLE_STEP = "RangeAngleStep";

        /// <summary>
        /// Constant indicating an error regarding the parameter set. 
        /// It is forwarded for HALCON errors that occur during the
        /// creation of the shape-based model or detection of instances 
        /// of the model
        /// 常量标记:异常(HalconException)
        /// </summary>
        public const string H_ERR_MESSAGE = "Halcon Error";

        public MatchModelParameter()
        {
            AutoParamList = new System.Collections.ArrayList(10);

            NumLevels = (int)5;
            Optimization = "none";
            Metric = "use_polarity";
            Contrast = (int)30;
            MinContrast = (int)10;
            NumToMatch = (int)1;
            MinScore = (double)50 / 100.0;
            Greediness = (double)75 / 100.0;
            MaxOverLap = (double)50 / 100.0;
            SubPixel = "least_squares";
            LastNumLevel = (int)1;

            RecogRateOpt = (int)0;
            RecogRate = (int)100;
            RecogAccuracyMode = MatchModelParameter.RECOG_MODE_ATLEASTONE;
            RecogSpecifiedMatchNum = (int)1;
            InspectMaxNoMatch = (int)1;

            AngleExtent = ((double)360) * System.Math.PI / 180.0;
            AngleStep = ((double)1.0) * System.Math.PI / 180.0;
            StartAngle = (double)(-180) * System.Math.PI / 180.0;

            ScaleMax = (double)1 / 1.0;
            ScaleMin = (double)1 / 1.0;
            ScaleStep = (double)1 / 100.0;

        }

        //******************************************************************/
        /*    Setter-methods for the set of values, that can be determined 
         *    automatically. If a parameter gets assigned a new value
         *    it can be only caused by user interaction, which means, the
         *    auto-modus for these particular parameters needs to be 
         *    canceled, to avoid further automatic adjustment              
		/*******************************************************************/

        /// <summary>
        /// Sets the parameter <c>Optimization</c> to the supplied value;
        /// if the parameter has been in auto-mode, cancel this option
        /// 设置优化选项
        /// 若自动模式参数列表包含该参数，则取消掉该参数
        /// </summary>
        /// <param name="val"></param>
        public void SetOptimization(string val)
        {
            Optimization = val;
            if (AutoParamList.Contains(AUTO_OPTIMIZATION))
                AutoParamList.Remove(AUTO_OPTIMIZATION);
        }

        /// <summary>
        /// Sets the parameter <c>PyramidLevels</c> to the supplied value;
        /// if the parameter has been in auto-mode, cancel this option
        /// 设置图层金字塔最大等级数
        /// 若自动模式参数列表包含该参数，则取消掉该参数
        /// </summary>
        /// <param name="val"></param>
        public void SetNumLevels(int val)
        {
            NumLevels = val;
            if (AutoParamList.Contains(AUTO_NUM_LEVELS))
                AutoParamList.Remove(AUTO_NUM_LEVELS);
        }

        /// <summary>
        /// Sets the parameter <c>Contrast</c> to the supplied value;
        /// if the parameter has been in auto-mode, cancel this option
        /// 设置对比度
        /// 若自动模式参数列表包含该参数，则取消掉该参数
        /// </summary>
        /// <param name="val"></param>
        public void SetContrast(int val)
        {
            Contrast = val;
            if (AutoParamList.Contains(AUTO_CONTRAST))
                AutoParamList.Remove(AUTO_CONTRAST);
        }

        /// <summary>
        /// Sets the parameter <c>MinContrast</c> to the supplied value;
        /// if the parameter has been in auto-mode, cancel this option
        /// 设置最小对比度
        /// 若自动模式参数列表包含该参数，则取消掉该参数
        /// </summary>
        /// <param name="val"></param>
        public void SetMinContrast(int val)
        {
            MinContrast = val;
            if (AutoParamList.Contains(AUTO_MIN_CONTRAST))
                AutoParamList.Remove(AUTO_MIN_CONTRAST);
        }

        /// <summary>
        /// Sets the parameter <c>ScaleStep</c> to the supplied value;
        /// if the parameter has been in auto-mode, cancel this option
        /// 设置自动模式下缩放系数的增长步长
        /// 若自动模式参数列表包含该参数，则取消掉该参数
        /// </summary>
        public void SetScaleStep(double val)
        {
            ScaleStep = val;
            if (AutoParamList.Contains(AUTO_SCALE_STEP))
                AutoParamList.Remove(AUTO_SCALE_STEP);
        }

        /// <summary>
        /// Sets the parameter <c>AngleStep</c> to the supplied value;
        /// if the parameter has been in auto-mode, cancel this option
        /// 设置自动模式下旋转角的增长步长
        /// 若自动模式参数列表包含该参数，则取消掉该参数
        /// </summary>
        public void SetAngleStep(double val)
        {
            AngleStep = val;
            if (AutoParamList.Contains(AUTO_ANGLE_STEP))
                AutoParamList.Remove(AUTO_ANGLE_STEP);
        }

        //******************************************************************/
        /*              Setter-methods for other values                    */
        /*******************************************************************/

        /// <summary>
        /// Sets the parameter <c>MinScale</c> to the supplied value;     
        /// 设置最小缩放系数     
        /// </summary>
        /// <param name="val"></param>
        public void SetMinScale(double val)
        {
            ScaleMin = val;
        }

        /// <summary>
        /// Sets the parameter <c>MaxScale</c> to the supplied value;       
        /// 设置最大缩放系数     
        /// </summary>
        /// <param name="val"></param>
        public void SetMaxScale(double val)
        {
            ScaleMax = val;
        }

        /// <summary>
        ///  Sets the parameter <c>StartingAngle</c> to the supplied value
        ///  设置起始角
        /// </summary>
        /// <param name="val"></param>
        public void SetStartAngle(double val)
        {
            StartAngle = val;
        }

        /// <summary>
     	/// Sets the parameter <c>AngleExtent</c> to the supplied value
        /// 设置角范围
        /// </summary>
        /// <param name="val"></param>
        public void SetAngleExtent(double val)
        {
            AngleExtent = val;
        }

        /// <summary>
        ///  Sets the parameter <c>Metric</c> to the supplied value
        ///  设置度量条件
        /// </summary>
        /// <param name="val"></param>
        public void SetMetric(string val)
        {
            Metric = val;
        }

        /// <summary>
        /// Sets the parameter <c>MinScore</c> to the supplied value
        /// 设置最小匹配分数
        /// </summary>
        /// <param name="val"></param>
        public void SetMinScore(double val)
        {
            MinScore = val;
        }

        /// <summary>
        /// Sets the parameter <c>MumToMatch</c> to the supplied value
        /// 设置可能的匹配数量
        /// </summary>
        /// <param name="val"></param>
        public void SetNumToMatch(int val)
        {
            NumToMatch = val;
        }

        /// <summary>
        /// Sets the parameter <c>Greediness</c> to the supplied value
        /// 设置贪婪度
        /// </summary>
        public void SetGreediness(double val)
        {
            Greediness = val;
        }

        /// <summary>
		/// Sets the parameter <c>MaxOverlap</c> to the supplied value
        /// 设置最大重叠比
		/// </summary>
        public void SetMaxOverlap(double val)
        {
            MaxOverLap = val;
        }

        /// <summary>
        /// Sets the parameter <c>Subpixel</c> to the supplied value
        /// 设置亚像素等级
        /// </summary>
        public void SetSubPixel(string val)
        {
            SubPixel = val;
        }

        /// <summary>
        /// Sets the parameter <c>LastPyramidLevel</c> to the supplied value
        /// 设置可以找到匹配模板的实例的最低金字塔等级
        /// </summary>
        /// <param name="val"></param>
        public void SetLastPyramidLevel(int val)
        {
            LastNumLevel = val;
        }

        /// <summary>
        /// Sets the parameter defining the options for the recognition rate
        /// to the supplied value
        /// 设置评估搜索结果的启发式选项
        /// [注:'0'--'==',
        /// '1'--'>=']
        /// </summary>
        public void SetRecogRateOption(int val)
        {
            RecogRateOpt = val;
        }

        /// <summary>
        /// Sets the parameter defining the rate for the recognition to the
        /// supplied value
        /// 设置识别率
        /// </summary>
        /// <param name="val"></param>
        public void SetRecognitionRate(int val)
        {
            RecogRate = val;
        }

        /// <summary>
        ///  Sets the parameter defining the mode to determine accuracy of recognition
        ///  设置模板识别精度的模式
        /// </summary>
        /// <param name="val"></param>
        public void SetRecogAccuracyMode(string val)
        {
            RecogAccuracyMode = val;
        }

        /// <summary>
        /// Sets the number of matches to be recognized to the supplied value.
        /// 设置指定的匹配模板的实例数
        /// </summary>
        public void SetRecogSpecifiedMatchNum(int val)
        {
            RecogSpecifiedMatchNum = val;
        }

        /// <summary>
        /// 设置检查统计时的最大匹配数
        /// </summary>
        /// <param name="val"></param>
        public void SetInspectMaxNoMatch(int val)
        {
            InspectMaxNoMatch = val;
        }



        /// <summary>
        /// Checks if the parameter referenced by <c>mode</c> is 
        /// in the auto-mode list, i.e., that it is determined automatically
        /// 判断参数'mode'所指定的参数是否在自动参数列表
        /// 若在，则其值自动设定;否则，用户设定
        /// </summary>
        /// <param name="mode">
        /// Constant starting with AUTO_*, describing one of the parameters
        /// for the auto-mode.
        /// </param>
        public bool IsAutoParameter(string mode)
        {
            bool rt = false;
            switch (mode) //其实:rt = this.AutoParamList.Contains(mode)
            {
                case AUTO_OPTIMIZATION:
                    rt = AutoParamList.Contains(AUTO_OPTIMIZATION);
                    break;
                case AUTO_NUM_LEVELS:
                    rt = AutoParamList.Contains(AUTO_NUM_LEVELS);
                    break;
                case AUTO_CONTRAST:
                    rt = AutoParamList.Contains(AUTO_CONTRAST);
                    break;
                case AUTO_MIN_CONTRAST:
                    rt = AutoParamList.Contains(AUTO_MIN_CONTRAST);
                    break;
                case AUTO_SCALE_STEP:
                    rt = AutoParamList.Contains(AUTO_SCALE_STEP);
                    break;
                case AUTO_ANGLE_STEP:
                    rt = AutoParamList.Contains(AUTO_ANGLE_STEP);
                    break;
                default: break;
            }
            return rt;
        }

        /// <summary>
		/// Checks if any parameter is registered for automatic 
		/// determination. If not, the call for automatic
		/// determination can be skipped
        /// 判断自动参数列表是否有自动参数,若无则忽略自动设置
		/// </summary>
        public bool IsOnAuto()
        {
            if (AutoParamList.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Adds the parameter <c>val</c> to the list of parameters that 
        /// will be determined automatically before the application.
        /// 添加自动设置的参数
		/// </summary>
		/// <param name="val">
		/// Constant starting with AUTO_*, describing one of the parameters
        /// for the auto-mode.
		/// </param>
		/// <returns>
		/// Indicates whether the variable is already in auto-mode or
		/// was added to the auto-list successfully.
		/// </returns>
        public bool AddAutoParameter(string val)
        {
            string mode = string.Empty;
            switch (val)
            {
                case AUTO_OPTIMIZATION:
                    if (AutoParamList != null
                        && (!AutoParamList.Contains(AUTO_OPTIMIZATION)))
                        mode = AUTO_OPTIMIZATION;
                    break;
                case AUTO_NUM_LEVELS:
                    if (AutoParamList != null
                        && (!AutoParamList.Contains(AUTO_NUM_LEVELS)))
                        mode = AUTO_NUM_LEVELS;
                    break;
                case AUTO_CONTRAST:
                    if (AutoParamList != null
                        && (!AutoParamList.Contains(AUTO_CONTRAST)))
                        mode = AUTO_CONTRAST;
                    break;
                case AUTO_MIN_CONTRAST:
                    if (AutoParamList != null
                        && (!AutoParamList.Contains(AUTO_MIN_CONTRAST)))
                        mode = AUTO_MIN_CONTRAST;
                    break;
                case AUTO_SCALE_STEP:
                    if (AutoParamList != null
                        && (!AutoParamList.Contains(AUTO_SCALE_STEP)))
                        mode = AUTO_SCALE_STEP;
                    break;
                case AUTO_ANGLE_STEP:
                    if (AutoParamList != null
                        && (!AutoParamList.Contains(AUTO_ANGLE_STEP)))
                        mode = AUTO_ANGLE_STEP;
                    break;
                default: break;
            }

            if (string.IsNullOrEmpty(mode))
                return false;
            else
            {
                AutoParamList.Add(mode);
                return true;
            }

        }

        /// <summary>
        /// Removes the parameter <c>val</c> from the list of parameters that 
        /// will be determined automatically.
        /// 移除自动设置的参数
        /// </summary>
        /// <param name="val">
        /// Constant starting with AUTO_*, describing one of the parameters for
        /// the auto-mode.
        /// </param>
        /// <returns>
        /// Indicates if the variable was removed from the 
        /// auto-list successfully.
        /// </returns>
        public bool RemoveAutoParameter(string val)
        {
            if (AutoParamList != null
                && AutoParamList.Contains(val))
            {
                AutoParamList.Remove(val);
                return true;
            }
            else { return false; }
        }

        /// <summary>
		/// Gets the names of the parameters to be determined
		/// automatically
        /// 获取自动设置值的参数名列表
		/// </summary>
		/// <returns>
		/// List of parameter names being in auto-mode.
		/// </returns>
        public string[] GetAutoParameterList()
        {
            string[] pl = null;
            if (this.AutoParamList != null
                && this.AutoParamList.Count > 0)
            {
                int num = this.AutoParamList.Count;
                pl = new string[num];
                for (int i = 0; i < num; i++)
                {
                    pl[i] = (string)this.AutoParamList[i];
                }
            }

            return pl;
        }
    }

    /// <summary>
    /// 类:模板匹配模型结果
    /// </summary>
    [System.Serializable]
    public class MatchModelResult
    {
        /// <summary>
        /// Model contour applied for model detection
        /// 属性:模板轮廓
        /// </summary>      
        public HalconDotNet.HObject ModelContour;

        /// <summary>
        /// Row coordinate of the found instances of the model
        /// 属性:匹配模板的实例坐标(行)
        /// </summary>
        public HalconDotNet.HTuple Row;

        /// <summary>
        ///  Column coordinate of the found instances of the model
        /// 属性:匹配模板的实例坐标(列)
        /// </summary>
        public HalconDotNet.HTuple Col;

        /// <summary>
        /// Rotation angle of the found instances of the model
        /// 匹配模板的实例相对模板的旋转角
        /// </summary>
        public HalconDotNet.HTuple Angle;

        /// <summary>
        /// Scale of the found instances of the model in the row direction
        /// 匹配模板的实例相对模板的行缩放系数
        /// </summary>
        public HalconDotNet.HTuple ScaleRow;

        /// <summary>
        /// Scale of the found instances of the model in the column direction
        /// 匹配模板的实例相对模板的列缩放系数
        /// </summary>
        public HalconDotNet.HTuple ScaleCol;

        /// <summary>
        /// Score of the found instances of the model
        /// 匹配模板的实例的匹配得分
        /// </summary>
        public HalconDotNet.HTuple Score;

        /// <summary>
        /// Time needed to detect <c>MatchCount</c> numbers of model instances
        /// 搜索时间
        /// </summary>
        public double ElapseTime;

        /// <summary>
        /// Number of model instances found
        /// 匹配模板的实例数
        /// [实际找到的]
        /// </summary>
        public int MatchCount;

        public HalconDotNet.HTuple HMat2D;

        public MatchModelResult()
        {
            HMat2D = new HalconDotNet.HTuple();
        }

        /// <summary>
        /// Resets the detection results and 
        /// sets count to 0.
        /// </summary>
        public void Reset() { MatchCount = 0; }

        /// <summary>
        /// Gets the detected contour.
        /// 获取匹配模板的实例轮廓
        /// </summary>
        /// <returns>Detected contour</returns>
        public HalconDotNet.HObject GetInstanceContour()
        {
            HalconDotNet.HObject tmp = new HalconDotNet.HObject(), rContour = new HalconDotNet.HObject();
            rContour.GenEmptyObj();

            if (MatchCount > 0)
            {
                for (int i = 0; i < MatchCount; i++)
                {
                    HalconDotNet.HOperatorSet.VectorAngleToRigid(0, 0, 0, Row[i], Col[i], Angle[i], out HMat2D);
                    tmp.Dispose();
                    HalconDotNet.HOperatorSet.AffineTransContourXld(ModelContour, out tmp, HMat2D);
                    rContour = rContour.ConcatObj(tmp);
                }
                return rContour;
            }
            else { return null; }

        }
    }

    public enum MatchModelType : uint
    {
        ShapeRegionModel = 0,
        ShapeContourModel = 1
    }

    public abstract class ImageProcess
    {
        /// <summary>
        /// 是否汉语语言标记
        /// </summary>
        public bool IsChinese { set; get; }

        /// <summary>
        /// 图像处理结果是否达标标记
        /// true-达到设计要求,false-未达到设计要求
        /// </summary>
        public bool ResultOK { get; protected set; }

        /// <summary>
        /// 图像处理
        /// </summary>
        /// <param name="hobjRaw"></param>
        /// <returns></returns>
        public abstract bool Process(HalconDotNet.HObject hobjRaw);

        /// <summary>
        /// 算法初始化
        /// </summary>
        public abstract void InitProcess();

        /// <summary>
        /// 根据算法参数更新算法变量
        /// </summary>
        protected abstract void UpdateParameter();

        /// <summary>
        /// 加载算法文件
        /// </summary>
        /// <returns></returns>
        protected abstract bool LoadAlgorithmFile();

        /// <summary>
        /// 设置显示窗口索引
        /// </summary>
        /// <param name="wndIdx"></param>
        public abstract void SetHwndIndex(int wndIdx);

        /// <summary>
        /// 设置相机索引
        /// </summary>
        /// <param name="camIdx"></param>
        public abstract void SetCameraIndex(int camIdx);

        /// <summary>
        /// 设置图像启用算法标记
        /// [enable=true,表示算启用法处理;
        /// enable=false,表示忽略算法处理;]
        /// </summary>
        /// <param name="enable"></param>
        public abstract void SetEnableAlgorithm(bool enable);

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { set; get; }

        /// <summary>
        /// 是否允许允许标记
        /// </summary>
        protected internal bool _IsLaunchAllowed;

        /// <summary>
        /// 图像启用算法标记
        /// [注:
        /// _IsEnableAlgorithm=true,表示启用算法处理;
        /// _IsEnableAlgorithm=false,表示忽略算法处理;]
        /// </summary>
        protected internal bool _IsEnableAlgorithm;

        /// <summary>
        /// 原始图像
        /// </summary>
        protected internal HalconDotNet.HObject _RawImage;

        /// <summary>
        /// 显示窗口索引
        /// </summary>
        protected internal int _HwndIndex;

        /// <summary>
        /// 相机索引
        /// </summary>
        protected internal int _CameraIndex;

        /// <summary>
        /// 基于阈值方法的缺陷计算
        /// </summary>
        /// <param name="img"></param>
        /// <param name="defctRegion"></param>
        /// <param name="grayValue"></param>
        /// <param name="opnCR"></param>
        /// <param name="clsCR"></param>
        /// <param name="defctAreaRange"></param>
        /// <param name="resltAreaRange"></param>
        /// <returns></returns>
        protected internal bool CalculateDefectArea(HalconDotNet.HObject img, out HalconDotNet.HObject defctRegion,
         HalconDotNet.HTuple grayValue, HalconDotNet.HTuple opnCR, HalconDotNet.HTuple clsCR, HalconDotNet.HTuple defctAreaRange, out HalconDotNet.HTuple resltAreaRange)
        {
            bool rt = false; defctRegion = new HalconDotNet.HObject(); resltAreaRange = new HalconDotNet.HTuple();

            HalconDotNet.HObject border = new HalconDotNet.HObject();
            HalconDotNet.HObject region = new HalconDotNet.HObject();
            HalconDotNet.HObject regionOpnC = new HalconDotNet.HObject();
            HalconDotNet.HObject regionUnion = new HalconDotNet.HObject();
            HalconDotNet.HObject regionClsC = new HalconDotNet.HObject();
            HalconDotNet.HObject regionCon = new HalconDotNet.HObject();
            HalconDotNet.HObject regionSel = new HalconDotNet.HObject();

            try
            {
                if (img != null && img.IsInitialized())
                {
                    HalconDotNet.HTuple area, r, c, tmp;
                    HalconDotNet.HOperatorSet.ThresholdSubPix(img, out border, grayValue);
                    HalconDotNet.HOperatorSet.GenRegionContourXld(border, out region, new HalconDotNet.HTuple("filled"));
                    HalconDotNet.HOperatorSet.OpeningCircle(region, out regionOpnC, opnCR);
                    HalconDotNet.HOperatorSet.Union1(regionOpnC, out regionUnion);
                    HalconDotNet.HOperatorSet.ClosingCircle(regionUnion, out regionClsC, clsCR);
                    HalconDotNet.HOperatorSet.Connection(regionClsC, out regionCon);
                    HalconDotNet.HOperatorSet.SelectShape(regionCon, out regionSel, new HalconDotNet.HTuple("area"), new HalconDotNet.HTuple("and"), defctAreaRange[0], defctAreaRange[1]);
                    HalconDotNet.HOperatorSet.AreaCenter(regionSel, out area, out r, out c);
                    if (area.TupleLength() > 0)
                    {
                        HalconDotNet.HOperatorSet.TupleMin(area, out tmp);
                        resltAreaRange[0] = tmp;
                        HalconDotNet.HOperatorSet.TupleMax(area, out tmp);
                        resltAreaRange[1] = tmp;
                        rt = true;
                    }
                }
            }
#pragma warning disable CS0168 // The variable 'hex' is declared but never used
            catch (HalconDotNet.HalconException hex)
#pragma warning restore CS0168 // The variable 'hex' is declared but never used
            {
            }
            finally
            {
                border.Dispose();
                region.Dispose();
                regionOpnC.Dispose();
                regionUnion.Dispose();
                regionClsC.Dispose();
                regionCon.Dispose();
                regionSel.Dispose();
            }
            return rt;
        }

        /// <summary>
        /// 分割字符检测区域
        /// </summary>
        /// <param name="img"></param>
        /// <param name="ocrRegion"></param>
        /// <param name="resultImg"></param>
        /// <param name="resultOCRRegion"></param>
        /// <param name="scaleFactor"></param>
        /// <param name="characterSize"></param>
        /// <param name="characterAreaRange"></param>
        /// <param name="textOrientationRange"></param>
        /// <param name="lghtChrOnBlkgrd"></param>
        /// <param name="clsRadius"></param>
        /// <returns></returns>
        protected internal bool SegmentCharacterRegion(HalconDotNet.HObject img, HalconDotNet.HObject ocrRegion, out HalconDotNet.HObject resultImg, out HalconDotNet.HObject resultOCRRegion,
            HalconDotNet.HTuple scaleFactor, HalconDotNet.HTuple characterSize, HalconDotNet.HTuple characterAreaRange, HalconDotNet.HTuple textOrientationRange, HalconDotNet.HTuple lghtChrOnBlkgrd, HalconDotNet.HTuple clsRadius)
        {
            bool rt = false;
            resultImg = new HalconDotNet.HObject();
            resultOCRRegion = new HalconDotNet.HObject();

            HalconDotNet.HObject tmpRegion = new HalconDotNet.HObject();
            HalconDotNet.HObject imgReduced = new HalconDotNet.HObject();
            HalconDotNet.HObject imgScaled = new HalconDotNet.HObject();
            HalconDotNet.HObject binaryRegion = new HalconDotNet.HObject();
            HalconDotNet.HObject closingRegion = new HalconDotNet.HObject();
            HalconDotNet.HObject connectedRegion = new HalconDotNet.HObject();
            HalconDotNet.HObject transRegion = new HalconDotNet.HObject();
            HalconDotNet.HObject partitionedRegion = new HalconDotNet.HObject();
            HalconDotNet.HObject selectedRegion = new HalconDotNet.HObject();
            HalconDotNet.HObject selectedObj = new HalconDotNet.HObject();
            HalconDotNet.HObject intersectedRegion = new HalconDotNet.HObject();
            try
            {
                resultOCRRegion.GenEmptyObj();

                HalconDotNet.HTuple w, h, orienAngle, homMat2D, useThre, num = new HalconDotNet.HTuple(0);
                HalconDotNet.HOperatorSet.GetImageSize(img, out w, out h);
                HalconDotNet.HOperatorSet.TextLineOrientation(ocrRegion, img, characterSize[1], textOrientationRange[0], textOrientationRange[1], out orienAngle);
                HalconDotNet.HOperatorSet.VectorAngleToRigid(h / 2, w / 2, -orienAngle, h / 2, w / 2, 0, out homMat2D);
                HalconDotNet.HOperatorSet.AffineTransImage(img, out resultImg, homMat2D, new HalconDotNet.HTuple("constant"), new HalconDotNet.HTuple("false"));
                HalconDotNet.HOperatorSet.AffineTransRegion(ocrRegion, out tmpRegion, homMat2D, new HalconDotNet.HTuple("nearest_neighbor"));
                HalconDotNet.HOperatorSet.ReduceDomain(resultImg, tmpRegion, out imgReduced);
                HalconDotNet.HOperatorSet.ScaleImage(imgReduced, out imgScaled, scaleFactor[0], scaleFactor[1]);

                //若是亮文本暗背景
                if (lghtChrOnBlkgrd.TupleNotEqual(new HalconDotNet.HTuple(0)))
                {
                    HalconDotNet.HOperatorSet.BinaryThreshold(imgScaled, out binaryRegion, new HalconDotNet.HTuple("max_separability"), new HalconDotNet.HTuple("light"), out useThre);
                }
                else
                {
                    //若是暗文本亮背景
                    HalconDotNet.HOperatorSet.BinaryThreshold(imgScaled, out binaryRegion, new HalconDotNet.HTuple("max_separability"), new HalconDotNet.HTuple("dark"), out useThre);
                }

                //使字符区域独立整体部分连接(如i,j等字符,连接为整体)
                HalconDotNet.HOperatorSet.ClosingCircle(binaryRegion, out closingRegion, clsRadius);
                HalconDotNet.HOperatorSet.Connection(closingRegion, out connectedRegion);
                HalconDotNet.HOperatorSet.ShapeTrans(connectedRegion, out transRegion, new HalconDotNet.HTuple("rectangle1"));
                //分割开字符区域之间粘连的部分
                HalconDotNet.HOperatorSet.PartitionRectangle(transRegion, out partitionedRegion, characterSize[0], characterSize[1]);
                HalconDotNet.HOperatorSet.SelectShape(partitionedRegion, out selectedRegion, new HalconDotNet.HTuple("area"), new HalconDotNet.HTuple("and"), characterAreaRange[0], characterAreaRange[1]);
                HalconDotNet.HOperatorSet.CountObj(selectedRegion, out num);
                tmpRegion.Dispose();
                if (num.TupleNotEqual(new HalconDotNet.HTuple(0)))
                {
                    tmpRegion.GenEmptyObj();
                    for (int i = 0; i < num[0].I; i++)
                    {
                        HalconDotNet.HOperatorSet.SelectObj(selectedRegion, out selectedObj, (i + 1));
                        HalconDotNet.HOperatorSet.Intersection(binaryRegion, selectedObj, out intersectedRegion);
                        HalconDotNet.HOperatorSet.ConcatObj(tmpRegion, intersectedRegion, out tmpRegion);
                    }

                    HalconDotNet.HOperatorSet.SortRegion(tmpRegion, out resultOCRRegion, new HalconDotNet.HTuple("character"), new HalconDotNet.HTuple("true"), new HalconDotNet.HTuple("row"));
                    rt = true;
                }
            }
#pragma warning disable CS0168 // The variable 'hex' is declared but never used
            catch (HalconDotNet.HalconException hex)
#pragma warning restore CS0168 // The variable 'hex' is declared but never used
            {
            }
            finally
            {
                tmpRegion.Dispose();
                imgReduced.Dispose();
                imgScaled.Dispose();
                binaryRegion.Dispose();
                closingRegion.Dispose();
                connectedRegion.Dispose();
                transRegion.Dispose();
                partitionedRegion.Dispose();
                selectedRegion.Dispose();
                selectedObj.Dispose();
                intersectedRegion.Dispose();
            }
            return rt;
        }

    }

    /// <summary>
    /// 网点掩膜
    /// </summary>
    public class DotMatriceMask
    {
        /// <summary>
        /// 掩膜区域
        /// </summary>
        public HalconDotNet.HObject MaskRegion
        {
            set;
            get;
        }

        private HalconDotNet.HTuple _row1, _col1, _row2, _col2;

        public DotMatriceMask()
        {
            _row1 = 0;
            _col1 = 0;
            _row2 = 0;
            _col2 = 0;
        }

        /// <summary>
        /// 区域合并
        /// </summary>
        /// <param name="region"></param>
        public void UnionRegion(HalconDotNet.HObject region)
        {
            if(MaskRegion!=null
                && MaskRegion.IsInitialized())
            {
                HalconDotNet.HObject tmpRegion = new HalconDotNet.HObject();
                HalconDotNet.HOperatorSet.Union2(MaskRegion, region, out tmpRegion);
                MaskRegion.Dispose();
                MaskRegion = tmpRegion;
            }
            else
            {
                MaskRegion = region.Clone();
            }
        }

        /// <summary>
        /// 区域做差
        /// </summary>
        /// <param name="region"></param>
        public void DifferenceRegion(HalconDotNet.HObject region)
        {
            if (MaskRegion != null
                && MaskRegion.IsInitialized())
            {
                HalconDotNet.HObject tmpRegion = new HalconDotNet.HObject();
                HalconDotNet.HOperatorSet.Difference(MaskRegion, region, out tmpRegion);
                MaskRegion.Dispose();
                MaskRegion = tmpRegion;
            }
        }

        public HalconDotNet.HObject DotMatriceMaskRegion
        {
            get
            {
                if (MaskRegion == null 
                    || !MaskRegion.IsInitialized()
                    || new HalconDotNet.HRegion(MaskRegion).Area < 10)
                    return null;
                HalconDotNet.HOperatorSet.SmallestRectangle1(MaskRegion, out _row1, out _col1, out _row2, out _col2);
                HalconDotNet.HObject gridRegion = new HalconDotNet.HObject();
                HalconDotNet.HOperatorSet.GenGridRegion(out gridRegion, 10, 10, "points", _col2 - _col1, _row2 - _row1);
                HalconDotNet.HObject movedRegion = new HalconDotNet.HObject();
                HalconDotNet.HOperatorSet.MoveRegion(gridRegion, out movedRegion, _row1, _col1);
                gridRegion.Dispose();
                HalconDotNet.HObject gridMaskRegion = new HalconDotNet.HObject();
                HalconDotNet.HOperatorSet.Intersection(movedRegion, MaskRegion, out gridMaskRegion);
                movedRegion.Dispose();
                return gridMaskRegion;
            }
        }
       
    }

    /// <summary>
    /// 绘制模式
    /// </summary>
    public enum DrawMode 
    {
        //填充模式
        Fill=0,
        //擦除模式
        Erase=1
    }

    /// <summary>
    /// ROI形状类型定义
    /// </summary>
    public enum ROIShape : uint
    {
        ROI_SHAPE_NONE = 0,
        ROI_SHAPE_LINE = 1,
        ROI_SHAPE_RECTANGLE1 = 2,
        ROI_SHAPE_RECTANGLE2 = 3,
        ROI_SHAPE_CIRCULARARC = 4,
        ROI_SHAPE_CIRCLE = 5,
        ROI_SHAPE_ANNULUS = 6,
        ROI_SHAPE_POLYGON = 7
    }

    /// <summary>
    /// 标定类型   
    /// </summary>
    public enum CalibrationType : uint
    {
        //摄像机标定(相机+标定板)
        CALIBRATION_CAMERABOARD = 0,
        //手眼标定(相机移动+标定板)
        CALIBRATION_HANDINEYE = 1,
        //手眼标定(相机固定+标定板)
        CALIBRATION_HANDTOEYE = 2,
        //手眼标定(相机固定+九点)
        CALIBRATION_POINTS = 3
    }
}
