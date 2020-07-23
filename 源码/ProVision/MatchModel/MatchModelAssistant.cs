using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       MatchModelAssistant
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProVision.MatchModel
 * File      Name：       MatchModelAssistant
 * Creating  Time：       10/9/2019 11:47:18 AM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProVision.MatchModel
{
    /// <summary>
    /// This is a controller class, which receives the
    /// GUI component changes made by the user and forwards 
    /// the actions according to the internal settings.
    /// To notify the GUI about semantic changes, we use two delegates. One
    /// notifies about changes regarding the display objects
    /// (like a change of image or change in model or contour). It is also used
    /// to forward errors to the user, which might occur during a processing step.
    /// The second delegate notifies the GUI to correct its sliders and trackbars
    /// according to the new parameter setting, computed along with the 
    /// 'auto'-mechanism. 
    /// 类:模板匹配模型助手
    /// </summary>
    public class MatchModelAssistant
    {
        /// <summary>
        /// Constant indicating that the model contour display should be
        /// updated.
        /// 常量标记:更新模板轮廓
        /// </summary>
        public const int UPDATE_MODEL_XLD = 0;

        /// <summary>
        /// Constant indicating that the pyramid (display-)level should be
        /// updated.
        /// 常量标记:更新图层金字塔显示的等级序数
        /// </summary>
		public const int UPDATE_DISP_LEVEL = 1;

        /// <summary>
        /// Constant indicating that the pyramid should be updated.
        /// 常量标记:更新图层金字塔的等级总数
        /// </summary>
		public const int UPDATE_PYRAMID_LEVELS = 2;

        /// <summary>
        /// Constant indicating that the detection results should be updated.
        /// 常量标记:更新检测结果
        /// </summary>
		public const int UPDATE_DETECTION_RESULT = 3;

        /// <summary>
        /// Constant indicating that the test image display should be updated.
        /// 常量标记:更新测试图像
        /// </summary>
		public const int UPDATE_TEST_VIEW = 4;

        /// <summary>
        /// Constant indicating an error if a wrong file extension is used for
        /// a model file.
        /// 常量标记:出错[模板文件的扩展名异常]
        /// </summary>
		public const int ERR_NO_VALID_FILE = 5;

        /// <summary>
        /// Constant indicating an error when writing a model file 
        /// 常量标记:出错[写操作模板文件]
        /// </summary>
        public const int ERR_WRITE_SHAPEMODEL = 6;

        /// <summary>
        /// Constant indicating an error when reading from model file
        /// 常量标记:出错[读操作模板文件]
        /// </summary>
        public const int ERR_READ_SHAPEMODEL = 7;

        /// <summary>
        /// Constant indicating an error if operations are performed that 
        /// need a shape-based model, though no model has been created, yet
        /// 常量标记:出错[未创建模板匹配模型]
        /// </summary>
        public const int ERR_NO_MODEL_DEFINED = 8;

        /// <summary>
        /// Constant indicating an error if operations are performed that 
        /// need a model image, though no model image has been loaded, yet
        /// 常量标记:出错[未加载模板图像]
        /// </summary>
        public const int ERR_NO_IMAGE = 9;

        /// <summary> 
        /// Constant indicating an error if operations are performed that
        /// need test images, though no test image has been loaded, yet
        /// 常量标记:出错[未加载测试图像]
        /// </summary>
        public const int ERR_NO_TESTIMAGE = 10;

        /// <summary>
        /// Constant indicating an error when reading an image file.
        /// 常量标记:出错[读操作图像文件]
        /// </summary>
        public const int ERR_READING_IMG = 11;

        /// <summary>
        /// List of test images
        /// 测试图像列表
        /// </summary>
        public System.Collections.Hashtable TestImageList;
        public Communal.MatchModelType ModelType { set; get; }
        //Flags to control the inspection and recognition processing
        public bool OnExternalModelID; //是否外部模板匹配模型
        public bool OnTimer;           //是否计时器开启
        public string ExceptionMsg = string.Empty;

        /// <summary>
        /// Training image with full domain
        /// 训练图像
        /// </summary>
        private HalconDotNet.HObject _trainImage;

        /// <summary>
        /// The model image is the training image with 
        /// a domain reduced by the region '_modelRegionROI'
        /// 模板提取图像
        /// [注:模板金字塔图层的首层]
        /// </summary>
        private HalconDotNet.HObject _modelExtrackImage;

        /// <summary>
        /// Region of interest defined by the sum of
        /// the positive and negative ROIs
        /// 模板提取区域(ROI集合结果)
        /// </summary>
        private HalconDotNet.HObject _modelExtrackRegion;

        /// <summary>
        /// 模板位姿
        /// [匹配模板的实例在训练图像中的坐标]
        /// </summary>
        public HalconDotNet.HTuple ModelPose { set; get; }

        /// <summary>
        /// Test image in which the model is searched.
        /// 当前测试图像
        /// </summary>
        private HalconDotNet.HObject _testImage;

        /// <summary>
        /// 模板匹配模型参数
        /// </summary>      
        public Communal.MatchModelParameter Parameter
        {
            private set;
            get;
        }

        /// <summary>
        /// 模板匹配模型结果
        /// </summary>       
        public Communal.MatchModelResult Result
        {
            private set;
            get;
        }

        /// <summary>
        /// 模板匹配模型句柄
        /// </summary>      
        public HalconDotNet.HTuple ModelID
        {
            private set;
            get;
        }

        /// <summary>
        /// 是否在训练图像中查找模板
        /// </summary>
        public bool IsDetectInTrainImage { set; get; }

        /// <summary>
        /// 模板搜索区域
        /// </summary>
        public HalconDotNet.HObject ModelSearchRegion { set; get; }

        /// <summary>
        /// 过程控制标记
        /// </summary>
        private bool _findAlways;          //是否总是查找
        private bool _createNewModelID;   //是否需要创建模板匹配模型

        //Display purposes
        private HalconDotNet.HTuple _trainImgWidth, _trainImgHeight;
        private double _scaleWidth = 1.0, _scaleHeight = 1.0;

        /// <summary>
        /// 金字塔图层
        /// [图像元组]
        /// </summary>
        private HalconDotNet.HObject _pyramidImages;  //模板金字塔图层
        private HalconDotNet.HObject _pyramidRegions; //模板金字塔区域
        //private HalconDotNet.HHomMat2D _homMat2D;
        private HalconDotNet.HTuple _hMat2D;
        private int _currentImgLevel = 1; //当前金字塔等级
        private int _numLevels = 6;        //金字塔等级数

        //Upper and lower range(Boundary)
        private int _contrastLowB, _contrastUpB;
        private int _minContrastLowB, _minContrastUpB;
        private double _scaleStepLowB, _scaleStepUpB;
        private double _angleStepLowB, _angleStepUpB;
        private int _numLevelLowB, _numLevelUpB;

        //Auxiliary value table to store intermediate states
        private int _cachedNumLevel;
        private double _cachedStartAngle;
        private double _cachedAngleExtent;
        private double _cachedAngleStep;
        private double _cachedMinScale;
        private double _cachedMaxScale;
        private double _cachedScaleStep;
        private string _cachedMetric;
        private int _cachedMinContrast;


        /// <summary>
        /// Delegate to forward changes for the display, which means changes 
        /// in the model contour, image level etc.
        /// 委托变量:响应图形变量改变的委托变量
        /// </summary>
        public ModelMatchedDelegate ModelMatchedDel;

        /// <summary>
        /// Delegate to forward changes in the matching parameters determined 
        /// with the 'auto-' mechanism
        /// 委托变量:响应模板匹配模型参数改变的委托变量
        /// </summary>
        public AutoParameterizedDelegate AutoParameterizedDel;

        /// <summary>
        /// 构造函数
        /// [默认模板匹配参数]
        /// </summary>
        public MatchModelAssistant() : this(new Communal.MatchModelParameter()) { }

        /// <summary>
        /// 构造函数
        /// [传入指定模板匹配参数]
        /// </summary>
        /// <param name="param"></param>
        public MatchModelAssistant(Communal.MatchModelParameter param)
        {
            Parameter = param;
            CacheModelParameters();
            Init();
        }

        private void Init()
        {
            InitField();
        }
        private void InitField()
        {
            ModelType = Communal.MatchModelType.ShapeRegionModel;
            Result = new Communal.MatchModelResult();
            ModelMatchedDel = new ModelMatchedDelegate(OnModelMatched);
            AutoParameterizedDel = new AutoParameterizedDelegate(OnAutoParamerized);          
            _hMat2D = new HalconDotNet.HTuple();
            TestImageList = new System.Collections.Hashtable(10);

            _contrastLowB = 0;
            _contrastUpB = 255;
            _scaleStepLowB = 0.0;
            _scaleStepUpB = (double)19.0 / 1000.0;
            _angleStepLowB = 0.0;
            _angleStepUpB = (double)(11.2) * System.Math.PI / 180.0;
            _numLevelLowB = 1;
            _numLevelUpB = 6;
            _minContrastLowB = 0;
            _minContrastUpB = 30;

            _findAlways = false;
            _createNewModelID = true;
            OnExternalModelID = false;
            OnTimer = false;
            IsDetectInTrainImage = false;
        }

        private void OnAutoParamerized(string val) { }

        private void OnModelMatched(int val) { }

        /// <summary>
        /// Set 'image' to be the training image(_trainImage)
        /// </summary>
        /// <param name="image"></param>
        public bool SetTrainImage(HalconDotNet.HObject image)
        {
            if (image != null
                && image.IsInitialized())
            {
                HalconDotNet.HOperatorSet.GetImageSize(image, out _trainImgWidth, out _trainImgHeight);
                _trainImage = image;
                Reset();
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// Loads the image file from the path suppliedby 'fileName'
        /// and set it to be the training image.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool SetTrainImage(string fileName)
        {
            bool rt = false;
            HalconDotNet.HObject himage;
            try
            {
                HalconDotNet.HOperatorSet.ReadImage(out himage, fileName);
                rt = SetTrainImage(himage);
            }
            catch (HalconDotNet.HOperatorException hex)
            {
                ExceptionMsg = hex.GetErrorMessage();
                ModelMatchedDel(MatchModelAssistant.ERR_READING_IMG);
            }
            return rt;
        }

        /// <summary>
        /// 根据提供的模板提取区域
        /// 提取模板图像,计算参数步长和自动参数值
        /// 提取模板图层金字塔和模板区域金字塔
        /// </summary>
        public void SetModelROI(HalconDotNet.HObject roi)
        {
            _modelExtrackRegion = roi;
            _pyramidRegions = null;

            if (!(_modelExtrackRegion != null && _modelExtrackRegion.IsInitialized()))
            {
                _modelExtrackImage = null;
                return;
            }

            HalconDotNet.HOperatorSet.ReduceDomain(_trainImage, _modelExtrackRegion, out _modelExtrackImage);
            DetermineStepRanges();

            if (Parameter.IsOnAuto())
                DetermineShapeAutoParameter();

            InspectShapeModel();
        }


        /*-----------------------------------------------------------------------*/
        /*                    Compute Parameter Values                           */
        /*-----------------------------------------------------------------------*/

        /// <summary>
        /// Determines the values for the matching parameters
        /// contained in the auto-list automatically.
        /// 根据自动参数列表，计算自动参数的值
        /// </summary>
        public void DetermineShapeAutoParameter()
        {
            double vald = 0.0;
            int vali, count;
            HalconDotNet.HTuple paramValues = new HalconDotNet.HTuple(), paramNames = new HalconDotNet.HTuple();

            if (!(_modelExtrackImage != null && _modelExtrackImage.IsInitialized()))
            {
                ModelMatchedDel(MatchModelAssistant.ERR_NO_IMAGE);         //无模板图像
                return;
            }

            try
            {
                HalconDotNet.HOperatorSet.DetermineShapeModelParams(_modelExtrackImage, Parameter.NumLevels, Parameter.StartAngle, Parameter.AngleExtent, Parameter.ScaleMin, Parameter.ScaleMax, Parameter.Optimization, Parameter.Metric, Parameter.Contrast, Parameter.MinContrast, Parameter.GetAutoParameterList(),
                                                                   out paramNames, out paramValues);

            }
            catch (HalconDotNet.HOperatorException hex)
            {
                ExceptionMsg = hex.GetErrorMessage();
                AutoParameterizedDel(Communal.MatchModelParameter.H_ERR_MESSAGE);
            }

            count = paramNames.Length;
            for (int i = 0; i < count; i++)
            {
                switch ((string)paramNames[i])
                {
                    case Communal.MatchModelParameter.AUTO_ANGLE_STEP:
                        {
                            vald = paramValues[i].D;

                            //保证优化值在优化范围内
                            if (vald > _angleStepUpB)
                                vald = _angleStepUpB;
                            else if (vald < _angleStepLowB)
                                vald = _angleStepLowB;

                            Parameter.AngleStep = vald;
                        }
                        break;
                    case Communal.MatchModelParameter.AUTO_CONTRAST:
                        {
                            vali = paramValues[i].I;

                            //保证优化值在优化范围内
                            if (vali > _contrastUpB)
                                vali = _contrastUpB;
                            else if (vali < _contrastLowB)
                                vali = _contrastLowB;

                            _minContrastUpB = vali;
                            Parameter.Contrast = vali;
                        }
                        break;
                    case Communal.MatchModelParameter.AUTO_MIN_CONTRAST:
                        {
                            vali = paramValues[i].I;

                            //保证优化值在优化范围内
                            if (vali > _minContrastUpB)
                                vali = _minContrastUpB;
                            else if (vali < _minContrastLowB)
                                vali = _minContrastLowB;

                            Parameter.MinContrast = vali;
                        }
                        break;
                    case Communal.MatchModelParameter.AUTO_NUM_LEVELS:
                        {
                            vali = paramValues[i].I;

                            //保证优化值在优化范围内
                            if (vali > _numLevelUpB)
                                vali = _numLevelUpB;
                            else if (vali < _numLevelLowB)
                                vali = _numLevelLowB;

                            Parameter.NumLevels = vali;
                        }
                        break;
                    case Communal.MatchModelParameter.AUTO_OPTIMIZATION:
                        {
                            Parameter.Optimization = paramValues[i].S;
                        }
                        break;
                    case Communal.MatchModelParameter.AUTO_SCALE_STEP:
                        {
                            vald = paramValues[i].D;

                            //保证优化值在优化范围内
                            if (vald > _scaleStepUpB)
                                vald = _scaleStepUpB;
                            else if (vald < _scaleStepLowB)
                                vald = _scaleStepLowB;

                            Parameter.ScaleStep = vald;
                        }
                        break;
                }

                AutoParameterizedDel(paramNames[i].S);
            }
            if (count != 0)
                _createNewModelID = true;
        }

        /// <summary>
        /// Adjusts the range of ScaleStep and AngleStep according to the
        /// current set of matching parameters.
        /// 根据当前匹配模型参数，调整旋转角增长步长及缩放系数增长步长
        /// </summary>
        public void DetermineStepRanges()
        {
            double vald = 0.0;
            HalconDotNet.HTuple paramValues = new HalconDotNet.HTuple(), paramNames = new HalconDotNet.HTuple();
            string[] strNames = new string[] { "scale_step", "angle_step" };

            if (!(_modelExtrackImage != null && _modelExtrackImage.IsInitialized()))
            {
                ModelMatchedDel(MatchModelAssistant.ERR_NO_IMAGE);         //无模板图像
                return;
            }

            try
            {
                HalconDotNet.HOperatorSet.DetermineShapeModelParams(_modelExtrackImage, Parameter.NumLevels, Parameter.StartAngle, Parameter.AngleExtent, Parameter.ScaleMin, Parameter.ScaleMax, Parameter.Optimization, Parameter.Metric, Parameter.Contrast, Parameter.MinContrast, strNames,
                                                                  out paramNames, out paramValues);
            }
            catch (HalconDotNet.HOperatorException hex)
            {
                ExceptionMsg = hex.GetErrorMessage();
                AutoParameterizedDel(Communal.MatchModelParameter.H_ERR_MESSAGE);
            }

            for (int i = 0; i < paramNames.Length; i++)
            {
                switch ((string)paramNames[i])
                {
                    case Communal.MatchModelParameter.AUTO_ANGLE_STEP:
                        {
                            vald = paramValues[i].D;
                            _angleStepLowB = vald / 3.0;
                            _angleStepUpB = vald * 3.0;
                            Parameter.AngleStep = vald;
                            AutoParameterizedDel(Communal.MatchModelParameter.RANGE_ANGLE_STEP);
                        }
                        break;
                    case Communal.MatchModelParameter.AUTO_SCALE_STEP:
                        {
                            vald = paramValues[i].D;
                            _scaleStepLowB = vald / 3.0;
                            _scaleStepUpB = vald * 3.0;
                            Parameter.ScaleStep = vald;
                            AutoParameterizedDel(Communal.MatchModelParameter.RANGE_SCALE_STEP);
                        }
                        break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// Creates the model contour.
        /// 提取金字塔图层和模板图像的金字塔ROI区域
        /// [改变金字塔等级或者对比度
        /// 就需要更新金字塔图层和金字塔区域层]
        /// </summary>
        private void InspectShapeModel()
        {
            if (_modelExtrackImage == null || (!_modelExtrackImage.IsInitialized()))
            {
                ModelMatchedDel(MatchModelAssistant.ERR_NO_IMAGE);   //无模板图像
                return;
            }
            HalconDotNet.HObject tmp;
            HalconDotNet.HOperatorSet.InspectShapeModel(_trainImage, out _pyramidImages, out tmp, _numLevels, Parameter.Contrast);

            if (tmp != null
                && tmp.IsInitialized())
                tmp.Dispose();

            HalconDotNet.HOperatorSet.InspectShapeModel(_modelExtrackImage, out tmp, out _pyramidRegions, _numLevels, Parameter.Contrast);

            ModelMatchedDel(MatchModelAssistant.UPDATE_MODEL_XLD);
            ModelMatchedDel(MatchModelAssistant.UPDATE_PYRAMID_LEVELS);
        }

        /// <summary>
        /// Gets the range of either one of the parameters  
        /// AngleStep and ScaleStep, defined by <c>param</c>
        /// </summary>
        public int[] GetStepRange(string param)
        {
            int[] range = new int[2];
            if (!string.IsNullOrEmpty(param))
            {
                switch (param)
                {
                    case Communal.MatchModelParameter.RANGE_ANGLE_STEP:
                        {
                            range[0] = (int)(this._angleStepLowB * 10.0 * 180.0 / System.Math.PI);
                            range[1] = (int)(this._angleStepUpB * 10.0 * 180.0 / System.Math.PI);
                        }
                        break;
                    case Communal.MatchModelParameter.RANGE_SCALE_STEP:
                        {
                            range[0] = (int)(this._scaleStepLowB * 1000.0);
                            range[1] = (int)(this._scaleStepUpB * 1000.0);
                        }
                        break;
                    default: break;
                }
            }
            return range;
        }

        /*-----------------------------------------------------------------------*/
        /*                    Methods for test images                            */
        /*-----------------------------------------------------------------------*/

        /// <summary>
        /// Loads and adds test image files to the list of test images
        /// </summary>
        public bool AddTestImage(string fileKey)
        {
            if (TestImageList != null
                && (!TestImageList.ContainsKey(fileKey)))
            {
                try
                {
                    HalconDotNet.HObject image;
                    HalconDotNet.HOperatorSet.ReadImage(out image, new HalconDotNet.HTuple(fileKey));
                    if (image != null
                       && image.IsInitialized())
                    {
                        TestImageList.Add(fileKey, image);
                        return true;
                    }
                    else { return false; }

                }
                catch (HalconDotNet.HOperatorException hex)
                {
                    ExceptionMsg = hex.GetErrorMessage();
                    ModelMatchedDel(MatchModelAssistant.ERR_READING_IMG);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sets an image from the list of test images to be the current
        /// test image. The filename is given by the value 'fileKey'.
        /// </summary>
        public void SetTestImage(string fileKey)
        {
            if (TestImageList != null
                 && TestImageList.ContainsKey(fileKey))
            {
                _testImage = TestImageList[fileKey] as HalconDotNet.HObject;
                if (_testImage != null)
                    ModelMatchedDel(MatchModelAssistant.UPDATE_TEST_VIEW);
                else
                {
                    ModelMatchedDel(MatchModelAssistant.ERR_NO_TESTIMAGE);
                }
            }
        }

        public void SetTestImage(HalconDotNet.HObject hobj)
        {
            if (hobj != null && hobj.IsInitialized())
            {
                _testImage = hobj;
                if (_testImage != null)
                    ModelMatchedDel(MatchModelAssistant.UPDATE_TEST_VIEW);
                else
                {
                    ModelMatchedDel(MatchModelAssistant.ERR_NO_TESTIMAGE);
                }
            }
        }

        /// <summary>
        /// Removes an image from the list of test images, 
        /// using the file name
        /// </summary>
        public void RemoveTestImage(string fileKey)
        {
            if (TestImageList != null)
            {
                if (TestImageList.ContainsKey(fileKey))
                    TestImageList.Remove(fileKey);

                if (TestImageList.Count == 0)
                    _testImage = null;
            }
        }

        /// <summary>
        /// Removes all images from the list of test images
        /// </summary>
        public void ClearTestImage()
        {
            if (TestImageList != null)
                TestImageList.Clear();

            if (_testImage != null)
                _testImage.Dispose();

            _testImage = null;
        }

        /// <summary>
        /// Sets the current test image to the image defined by 
        /// <c>fileName</c> and return it
        /// </summary>
        public HalconDotNet.HObject GetTestImage(string fileKey)
        {
            if (TestImageList != null
                && TestImageList.ContainsKey(fileKey))
                return _testImage = TestImageList[fileKey] as HalconDotNet.HObject;
            return _testImage;
        }

        /// <summary>
        /// Returns the current test image
        /// </summary>
        public HalconDotNet.HObject GetCurrentTestImage()
        {
            return _testImage;
        }

        /*-----------------------------------------------------------------------*/
        /*                    Methods for auto options                           */
        /*-----------------------------------------------------------------------*/

        /// <summary>
        /// Adds the parameter name given by <c>mode</c> 
        /// to the list of matching parameters that are 
        /// set to be determined automatically
        /// </summary>
        public void AddAutoParameter(string mode)
        {
            if (Parameter.AddAutoParameter(mode))
                DetermineShapeAutoParameter();
        }

        /// <summary>
        /// Removes the parameter name given by <c>mode</c> from the
        /// list of matching parameters that are determined 
        /// automatically
        /// </summary>
        public bool RemoveAutoParameter(string mode)
        {
            return Parameter.RemoveAutoParameter(mode);
        }

        /*-----------------------------------------------------------------------*/
        /*                    Methods for matching parameters                    */
        /*-----------------------------------------------------------------------*/

        /// <summary>
        /// Sets Contrast to the input value <c>val</c> 
        /// and recreates the model.
        /// </summary>
        public void SetContrast(int val)
        {
            Parameter.SetContrast(val);
            _minContrastUpB = val;

            InspectShapeModel();

            if (Parameter.IsOnAuto())
                DetermineShapeAutoParameter();

            _createNewModelID = true;
        }

        /// <summary>
        /// Sets ScaleStep to the input value <c>val</c> 
        /// </summary>
        public void SetScaleStep(double val)
        {
            Parameter.SetScaleStep(val);

            if (Parameter.IsOnAuto())
                DetermineShapeAutoParameter();

            _createNewModelID = true;
        }

        /// <summary>
        /// Sets AngleStep to the input value <c>val</c> 
        /// </summary>
        public void SetAngleStep(double val)
        {
            Parameter.SetAngleStep(val);

            if (Parameter.IsOnAuto())
                DetermineShapeAutoParameter();

            _createNewModelID = true;
        }

        /// <summary>
        /// Sets NumLevels to the input value <c>val</c> 
        /// </summary>
        public void SetNumLevels(int val)
        {
            Parameter.SetNumLevels(val);

            if (Parameter.IsOnAuto())
                DetermineShapeAutoParameter();

            _createNewModelID = true;
        }

        /// <summary>
        /// Sets Optimization to the input value <c>val</c> 
        /// </summary>
        public void SetOptimization(string val)
        {
            Parameter.SetOptimization(val);

            if (Parameter.IsOnAuto())
                DetermineShapeAutoParameter();

            _createNewModelID = true;
        }

        /// <summary>
        /// Sets MinContrast to the input value <c>val</c> 
        /// </summary>
        public void SetMinContrast(int val)
        {
            Parameter.SetMinContrast(val);

            if (Parameter.IsOnAuto())
                DetermineShapeAutoParameter();

            _createNewModelID = true;
        }

        /// <summary>
        /// Sets Metric to the input value <c>val</c> 
        /// </summary>
        public void SetMetric(string val)
        {
            Parameter.SetMetric(val);

            _createNewModelID = true;
        }

        /// <summary>
        /// Sets MinScale to the input value <c>val</c> 
        /// </summary>
        public void SetMinScale(double val)
        {
            Parameter.SetMinScale(val);

            _createNewModelID = true;
        }

        /// <summary>
        /// Sets MaxScale to the input value <c>val</c> 
        /// </summary>
        public void SetMaxScale(double val)
        {
            Parameter.SetMaxScale(val);

            _createNewModelID = true;
        }

        /// <summary>
        /// Sets StartingAngle to the input value <c>val</c> 
        /// </summary>
        public void SetStartAngle(double val)
        {
            Parameter.SetStartAngle(val);

            _createNewModelID = true;
        }

        /// <summary>
        /// Sets AngleExtent to the input value <c>val</c> 
        /// </summary>
        public void SetAngleExtent(double val)
        {
            Parameter.SetAngleExtent(val);

            _createNewModelID = true;
        }

        /// <summary>
        /// 设置当前金字塔等级
        /// [注:显示等级]
        /// </summary>
        public void SetDisplayLevel(int val)
        {
            _currentImgLevel = val;
            ModelMatchedDel(MatchModelAssistant.UPDATE_DISP_LEVEL);
        }



        /*-----------------------------------------------------------------------*/
        /*                    Methods for finding parameters                     */
        /*-----------------------------------------------------------------------*/

        /// <summary>
        /// Sets MinScore to the input value and starts model detection
        /// if the flag <c>findAlways</c> is checked
        /// </summary>
        public void SetMinScore(double val)
        {
            Parameter.SetMinScore(val);

            if (_findAlways
                && _testImage != null
                 && _testImage.IsInitialized())
                DetectShapeModel();
        }

        /// <summary>
        /// Sets NumToMatch to the input value and starts model detection
        /// if the flag <c>findAlways</c> is checked
        /// </summary>
        public void SetNumToMatch(int val)
        {
            Parameter.SetNumToMatch(val);

            if (_findAlways
               && _testImage != null
                && _testImage.IsInitialized())
                DetectShapeModel();
        }


        /// <summary>
        /// Sets Greediness to the input value and starts model detection
        /// if the flag <c>findAlways</c> is checked
        /// </summary>
        public void SetGreediness(double val)
        {
            Parameter.SetGreediness(val);

            if (_findAlways
               && _testImage != null
                && _testImage.IsInitialized())
                DetectShapeModel();
        }

        /// <summary>
        /// Sets MaxOverlap to the input value and starts model detection
        /// if the flag <c>findAlways</c> is checked
        /// </summary>
        public void SetMaxOverlap(double val)
        {
            Parameter.SetMaxOverlap(val);

            if (_findAlways
               && _testImage != null
                && _testImage.IsInitialized())
                DetectShapeModel();
        }

        /// <summary>
        /// Sets SubPixel to the input value and starts model detection
        /// if the flag <c>findAlways</c> is checked
        /// </summary>
        public void SetSubPixel(string val)
        {
            Parameter.SetSubPixel(val);

            if (_findAlways
               && _testImage != null
                && _testImage.IsInitialized())
                DetectShapeModel();
        }

        /// <summary>
        /// Sets LastPyramLevel to the input value and starts model detection
        /// if the flag <c>findAlways</c> is checked
        /// </summary>
        public void SetLastPyramidLevel(int val)
        {
            Parameter.SetLastPyramidLevel(val);

            if (_findAlways
                && _testImage != null
                 && _testImage.IsInitialized())
                DetectShapeModel();
        }

        /// <summary>
        /// Sets the flag <c>findAlways</c> to the input value <c>flag</c>
        /// </summary>
        public void SetFindAlways(bool val)
        {
            _findAlways = val;
            if (_findAlways
                && _testImage != null
                 && _testImage.IsInitialized())
                DetectShapeModel();
        }

        /// <summary>
        /// Gets detected model contours
        /// 获取非训练图像中的实例轮廓
        /// </summary>
        public HalconDotNet.HObject GetInstanceContour()
        {
            return Result.GetInstanceContour();
        }

        /*-----------------------------------------------------------------------*/
        /*                    Optimize recognition speed                         */
        /*-----------------------------------------------------------------------*/

        /// <summary>
        /// Sets the RecognitionRateOption to the input  
        /// value <c>val</c>
        /// </summary>
        public void SetRecogRateOption(int val)
        {
            Parameter.SetRecogRateOption(val);
        }

        /// <summary>
        /// Sets the RecognitionRate to the input value <c>val</c>
        /// </summary>
        public void SetRecogRate(int val)
        {
            Parameter.SetRecognitionRate(val);
        }

        /// <summary>
        /// Sets the RecogAccuracyMode to the input 
        /// value <c>val</c>
        /// </summary>
        public void SetRecogAccuracyMode(string val)
        {
            Parameter.SetRecogAccuracyMode(val);
        }

        /// <summary>
        /// Sets the RecognitionManualSelection to the input.
        /// 设置指定的匹配模板的实例数
        /// value <c>val</c>
        /// </summary>
        public void SetRecogSpecifiedMatchNum(int val)
        {
            Parameter.SetRecogSpecifiedMatchNum(val);
        }

        /*-----------------------------------------------------------------------*/
        /*                    Getter methods                                     */
        /*-----------------------------------------------------------------------*/

        /// <summary>
        /// Gets the model for the current image level
        /// 获取金字塔指定等级的区域(ROI)层对应的轮廓
        /// </summary>
        public HalconDotNet.HObject GetPyramidContour()
        {
            if (_pyramidRegions == null
                || (!_pyramidRegions.IsInitialized()))
                return null;

            HalconDotNet.HOperatorSet.HomMat2dIdentity(out _hMat2D);
            HalconDotNet.HOperatorSet.HomMat2dScaleLocal(_hMat2D, _scaleWidth, _scaleHeight, out _hMat2D);
            HalconDotNet.HObject tmpRegion, tmpContour, affinedContour;
            HalconDotNet.HOperatorSet.SelectObj(_pyramidRegions, out tmpRegion, _currentImgLevel);
            HalconDotNet.HOperatorSet.GenContourRegionXld(tmpRegion, out tmpContour, "center");
            HalconDotNet.HOperatorSet.AffineTransContourXld(tmpContour, out affinedContour, _hMat2D);
            return affinedContour;
        }

        /// <summary>
        /// Gets the model region for the current image level.
        /// 获取金字塔指定等级的区域(ROI)层
        /// </summary>
        public HalconDotNet.HObject GetPyramidRegion()
        {
            if (_pyramidRegions == null
              || (!_pyramidRegions.IsInitialized()))
                return null;
            HalconDotNet.HObject rg, rgzoomed;
            HalconDotNet.HOperatorSet.SelectObj(_pyramidRegions, out rg, _currentImgLevel);
            HalconDotNet.HOperatorSet.ZoomRegion(rg, out rgzoomed, _scaleWidth, _scaleHeight);
            return rgzoomed;
        }

        /// <summary>
        /// Gets the model image for the current image level.
        /// 获取金字塔指定等级的图层
        /// </summary>
        public HalconDotNet.HObject GetPyramidImage()
        {
            HalconDotNet.HTuple fW, fH;//图层图像的宽和高
            if (_pyramidImages == null
                || (!_pyramidImages.IsInitialized())) //未生成金字塔图层，则返回训练图像(原图)
                return _trainImage;

            HalconDotNet.HObject img, imgzoomed;
            HalconDotNet.HOperatorSet.SelectObj(_pyramidImages, out img, _currentImgLevel);
            HalconDotNet.HOperatorSet.GetImageSize(img, out fW, out fH);

            _scaleWidth = (_trainImgWidth / fW)[0].D;
            _scaleHeight = (_trainImgHeight / fH)[0].D;
            HalconDotNet.HOperatorSet.ZoomImageFactor(img, out imgzoomed, _scaleWidth, _scaleHeight, "nearest_neighbor");
            return imgzoomed;
        }


        /// <summary>
        /// Gets the model supplied by a loaded shapebased model file (.shm)
        /// 获取训练图像中的实例轮廓
        /// [注意:匹配模板位置的轮廓,非原点位置]
        /// </summary>
        public HalconDotNet.HObject GetTrainInstanceContour()
        {
            HalconDotNet.HTuple hmat2D = new HalconDotNet.HTuple();
            HalconDotNet.HObject contour;

            try
            {
                HalconDotNet.HOperatorSet.VectorAngleToRigid(0, 0, 0, ModelPose[0], ModelPose[1], ModelPose[2], out hmat2D);
                HalconDotNet.HOperatorSet.AffineTransContourXld(Result.ModelContour, out contour, hmat2D);
                return contour;
            }
            catch (HalconDotNet.HOperatorException hex)
            {
                ExceptionMsg = hex.GetErrorMessage();
                ModelMatchedDel(MatchModelAssistant.ERR_READ_SHAPEMODEL);
                return null;
            }
        }


        /// <summary>
        /// 清空模型参数和匹配结果
        /// </summary>
        public void Reset()
        {
            _currentImgLevel = 1;
            _pyramidRegions = null;
            _modelExtrackRegion = null;
            _pyramidImages = null;
            _modelExtrackImage = null;
            ModelPose = new HalconDotNet.HTuple();
            ModelID = new HalconDotNet.HTuple(-1);

            _scaleWidth = 1.0;
            _scaleHeight = 1.0;
            _createNewModelID = true;
            Result.Reset();
            ModelMatchedDel(MatchModelAssistant.UPDATE_MODEL_XLD);
        }

        /*-----------------------------------------------------------------------*/
        /*                    Other methods                                      */
        /*-----------------------------------------------------------------------*/

        /// <summary>
        /// Writes model to the file specified by <c>filePath</c>.
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveShapeModel(string filePath)
        {
            if (_modelExtrackImage == null)
            {
                ModelMatchedDel(MatchModelAssistant.ERR_NO_IMAGE); //无模板图像               
                return;
            }

            if (_createNewModelID)
                if (!CreateShapeModel())
                    return;

            try
            {
                HalconDotNet.HOperatorSet.WriteShapeModel(ModelID, filePath);
            }
            catch (HalconDotNet.HOperatorException hex)
            {
                ExceptionMsg = hex.GetErrorMessage();
                ModelMatchedDel(MatchModelAssistant.ERR_WRITE_SHAPEMODEL);
            }
        }

        /// <summary>
        /// Loads model from the file specified by <c>fileName</c>
        /// </summary>
        public bool LoadShapeModel(string filePath)
        {
            OnExternalModelID = false;
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    HalconDotNet.HTuple fileExist = new HalconDotNet.HTuple(0);
                    HalconDotNet.HOperatorSet.FileExists(new HalconDotNet.HTuple(filePath), out fileExist);
                    if (fileExist.TupleNotEqual(new HalconDotNet.HTuple(0)))
                    {
                        HalconDotNet.HTuple mdl;
                        HalconDotNet.HOperatorSet.ReadShapeModel(filePath, out mdl);
                        ModelID = mdl;
                    }                       
                    else { return false; }
                }
                else { return false; }
            }
            catch (HalconDotNet.HOperatorException hex)
            {
                ExceptionMsg = hex.GetErrorMessage();
                ModelMatchedDel(MatchModelAssistant.ERR_READ_SHAPEMODEL);
                return false;
            }

            OnExternalModelID = true;
            HalconDotNet.HOperatorSet.GetShapeModelContours(out Result.ModelContour, ModelID, 1);

            CacheModelParameters();
            HalconDotNet.HTuple numlevels, startangle, angleextent, anglestep, minscale, maxscale, scalestep, metric, mincontrast;
            HalconDotNet.HOperatorSet.GetShapeModelParams(ModelID, out numlevels, out startangle, out angleextent, out anglestep, out minscale, out maxscale, out scalestep, out metric, out mincontrast);

            Parameter.NumLevels = numlevels[0].I;
            Parameter.StartAngle = startangle[0].D;
            Parameter.AngleExtent = angleextent[0].D;
            Parameter.AngleStep = anglestep[0].D;
            Parameter.ScaleMin = minscale[0].D;
            Parameter.ScaleMax = maxscale[0].D;
            Parameter.ScaleStep = scalestep[0].D;
            Parameter.Metric = metric[0].S;
            Parameter.MinContrast = mincontrast[0].I;

            return true;
        }

        /// <summary>
        /// Cache part of the parameters
        /// 缓存模板匹配模型参数
        /// </summary>
        private void CacheModelParameters()
        {
            _cachedNumLevel = Parameter.NumLevels;
            _cachedStartAngle = Parameter.StartAngle;
            _cachedAngleExtent = Parameter.AngleExtent;
            _cachedAngleStep = Parameter.AngleStep;
            _cachedMinScale = Parameter.ScaleMin;
            _cachedMaxScale = Parameter.ScaleMax;
            _cachedScaleStep = Parameter.ScaleStep;
            _cachedMetric = Parameter.Metric;
            _cachedMinContrast = Parameter.MinContrast;
        }

        /// <summary>
        /// Reset part of the parameters
        /// 将缓存的参数赋值给模板匹配模型参数
        /// </summary>
        public void RetrieveModelParameters()
        {
            Parameter.NumLevels = _cachedNumLevel;
            Parameter.StartAngle = _cachedStartAngle;
            Parameter.AngleExtent = _cachedAngleExtent;
            Parameter.AngleStep = _cachedAngleStep;
            Parameter.ScaleMin = _cachedMinScale;
            Parameter.ScaleMax = _cachedMaxScale;
            Parameter.ScaleStep = _cachedScaleStep;
            Parameter.Metric = _cachedMetric;
            Parameter.MinContrast = _cachedMinContrast;

            _createNewModelID = true;
        }

        /// <summary>
        /// Creates the shape-based model. If the region of interest 
        /// <c>_plateROI</c> is missing or not well defined using the 
        /// interactive ROIs, then an error message is returned.
        /// 创建模板匹配模型
        /// </summary>
        public bool CreateShapeModel()
        {
            switch (ModelType)
            {
                case Communal.MatchModelType.ShapeRegionModel:
                    {
                        if (_modelExtrackImage == null
                            || !(_modelExtrackImage.IsInitialized()))
                        {
                            ModelMatchedDel(MatchModelAssistant.ERR_NO_MODEL_DEFINED);
                            return false;
                        }

                        try
                        {
                            HalconDotNet.HTuple mdl;
                            HalconDotNet.HOperatorSet.CreateScaledShapeModel(_modelExtrackImage,
                                Parameter.NumLevels,
                                Parameter.StartAngle,
                                Parameter.AngleExtent,
                                Parameter.AngleStep,
                                Parameter.ScaleMin,
                                Parameter.ScaleMax,
                                Parameter.ScaleStep,
                                Parameter.Optimization,
                                Parameter.Metric,
                                Parameter.Contrast,
                                Parameter.MinContrast,
                                out mdl);
                            ModelID = mdl;
                        }
                        catch (HalconDotNet.HOperatorException hex)
                        {
                            if (!OnTimer)
                            {
                                ExceptionMsg = hex.GetErrorMessage();
                                AutoParameterizedDel(Communal.MatchModelParameter.H_ERR_MESSAGE);
                            }
                            return false;
                        }
                    }
                    break;
                case Communal.MatchModelType.ShapeContourModel:
                    {
                        try
                        {
                            if (GetPyramidContour() == null)
                            {
                                ModelMatchedDel(MatchModelAssistant.ERR_NO_MODEL_DEFINED);
                                return false;
                            }
                            HalconDotNet.HTuple mdl;
                            HalconDotNet.HOperatorSet.CreateScaledShapeModelXld(GetPyramidContour(),
                                "auto"/*_parameter.NumLevels*/,
                                Parameter.StartAngle,
                                Parameter.AngleExtent,
                                "auto"/*_parameter.AngleStep*/,
                                Parameter.ScaleMin,
                                Parameter.ScaleMax,
                                "auto"/*_parameter.ScaleStep*/,
                                "auto"/*_parameter.Optimization*/,
                                "ignore_local_polarity"/*_parameter.Metric*/,
                                Parameter.Contrast,
                                out mdl);
                            ModelID = mdl;
                        }
                        catch (HalconDotNet.HOperatorException hex)
                        {
                            if (!this.OnTimer)
                            {
                                ExceptionMsg = hex.GetErrorMessage();
                                AutoParameterizedDel(Communal.MatchModelParameter.H_ERR_MESSAGE);
                            }
                            return false;
                        }
                    }
                    break;
                default: break;
            }
            HalconDotNet.HOperatorSet.GetShapeModelContours(out Result.ModelContour, ModelID, 1);
            _createNewModelID = false;
            return true;
        }

        /// <summary>
        /// Finds the model in the test image. If the model
        /// hasn't been created or needs to be recreated (due to 
        /// user changes made to the GUI components), 
        /// then the model is created first.
        /// 检测测试图像中的匹配实例
        /// </summary>
        public bool DetectShapeModel()
        {
            HalconDotNet.HTuple levels;
            double t2, t1;
            if(IsDetectInTrainImage)
            {
                if (!(_trainImage != null && _trainImage.IsInitialized()))
                    return false;

                SetTestImage(_trainImage);
            }

            if (!(_testImage != null && _testImage.IsInitialized()))
                return false;

            if (_createNewModelID && !OnExternalModelID)//若需要创建模板匹配模型且未有外载模板匹配模型,则创建模板匹配模型
                if (!CreateShapeModel())
                    return false;

            try
            {
                levels = new HalconDotNet.HTuple(new int[]
                {Parameter.NumLevels,Parameter.LastNumLevel });

                t1 = HalconDotNet.HSystem.CountSeconds();
                HalconDotNet.HObject reduceImage = null;
                if(ModelSearchRegion!=null
                    && ModelSearchRegion.IsInitialized())
                {
                    HalconDotNet.HOperatorSet.ReduceDomain(_testImage, ModelSearchRegion, out reduceImage);
                }
                else
                {
                    reduceImage = _testImage;
                }              

                HalconDotNet.HOperatorSet.FindScaledShapeModel(reduceImage,
                    ModelID,
                    Parameter.StartAngle,
                    Parameter.AngleExtent,
                    Parameter.ScaleMin,
                    Parameter.ScaleMax,
                    Parameter.MinScore,
                    Parameter.NumToMatch,
                    Parameter.MaxOverLap,
                    new HalconDotNet.HTuple(Parameter.SubPixel),
                    levels,
                    Parameter.Greediness,
                     out Result.Row,
                     out Result.Col,
                     out Result.Angle,
                     out Result.ScaleRow,
                     out Result.Score);

                t2 = HalconDotNet.HSystem.CountSeconds();
                Result.ElapseTime = 1000.0 * (t2 - t1);
                Result.MatchCount = Result.Row.Length; //执行匹配成功,不代表一定能有匹配实例
                Result.ScaleCol = Result.ScaleRow;

                if (IsDetectInTrainImage)
                {
                    ModelPose = new HalconDotNet.HTuple();
                    ModelPose = ModelPose.TupleConcat(Result.Row).TupleConcat(Result.Col).TupleConcat(Result.Angle);
                    if (ModelType == ProVision.Communal.MatchModelType.ShapeRegionModel)
                        ModelPose = ModelPose.TupleConcat(new HalconDotNet.HTuple(0));
                    else if (ModelType == ProVision.Communal.MatchModelType.ShapeContourModel)
                        ModelPose = ModelPose.TupleConcat(new HalconDotNet.HTuple(1));
                    IsDetectInTrainImage = false;
                }
            }
            catch (HalconDotNet.HOperatorException hex)
            {
                if (!OnTimer)
                {
                    ExceptionMsg = hex.GetErrorMessage();
                    AutoParameterizedDel(Communal.MatchModelParameter.H_ERR_MESSAGE);
                }
                return false;
            }

            ModelMatchedDel(MatchModelAssistant.UPDATE_DETECTION_RESULT);
            return true;
        }
      
    }
}
