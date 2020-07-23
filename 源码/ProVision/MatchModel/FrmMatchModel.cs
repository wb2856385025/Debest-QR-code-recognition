using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProVision.MatchModel
{
    public partial class FrmMatchModel : Form
    {
        protected internal HalconDotNet.HObject _currentImage;
        protected internal HalconDotNet.HObject _modelRegion;      //模板区域
        protected internal HalconDotNet.HObject _modelContour;     //模板轮廓
        protected internal HalconDotNet.HObject _instanceContour;  //匹配模板的实例轮廓
        protected internal System.Drawing.Color _createModelColor, _trainModelColor; //显示训练图像时窗体的背景颜色,显示测试图像时的窗体背景颜色

        protected internal bool _locked, _specifiedModelPara; //锁定标记,是否指定模型参数标记
        protected internal System.Windows.Forms.OpenFileDialog _ofdTrainImg, _ofdTestImgs, _ofdModelFile; //训练图像对话框,测试图像对话框,模板匹配模型文件对话框
        protected internal System.Windows.Forms.SaveFileDialog _sfdModelFile; //模板匹配模型文件
        protected internal System.Windows.Forms.FolderBrowserDialog _fbdModelFolder;
        protected internal string _imgExtention;
        protected internal System.Windows.Forms.Timer _timer;

        protected internal ProVision.MatchModel.MatchModelOpt _matchModelOpt;
        protected internal ProVision.MatchModel.MatchModelOptAccuracy _matchModelOptAccuracy;
        protected internal ProVision.MatchModel.MatchModelOptResults _matchModelOptResults;
        protected internal ProVision.MatchModel.MatchModelAssistant _matchModelAssistant;
        protected internal string _modelRegionColor, _modelContourColor, _instanceContourColor, _activeInstanceContourColor;
        protected internal string _extractRegionColor, _searchRegionColor;
        protected internal int _modelRegionLineWidth,_modelContourLineWidth,_instanceContourLineWidth,_activeInsContourIdx;

        protected internal ProVision.Communal.DrawMode _drawMode;
        protected internal int _brushSize;
        protected internal bool _isRegionForExtractModel;

        public ProVision.InteractiveROI.HWndCtrller HWndCtrller;
        public ProVision.InteractiveROI.ROIManager ROICtrller;
        public ProVision.Communal.DotMatriceMask GrdMsk;

        /// <summary>
        /// 模板提取区域
        /// [注:通过ROI交互定义]
        /// </summary>
        public HalconDotNet.HObject ExtractRegion { private set; get; } 

        /// <summary>
        /// 模板匹配模型句柄
        /// </summary>
        public HalconDotNet.HTuple ModelID { private set; get; }

        /// <summary>
        /// 模板的位姿
        /// [注:ModelPose[0]-模板行坐标Row,ModelPose[1]-模板列坐标Column,
        /// ModelPose[2]-模板参考角,ModelPose[3]-模板类型(0-区域形状,1-轮廓形状)]
        /// </summary>
        public HalconDotNet.HTuple ModelPose { private set; get; }

        /// <summary>
        /// 模板搜索区域
        /// </summary>
        public HalconDotNet.HObject SearchRegion { set; get; }

        public FrmMatchModel()
        {
            InitializeComponent();
            InitField();
            this.Load += FrmMatchModel_Load;
        }

        public FrmMatchModel(HalconDotNet.HObject trainImg):this()
        {
            TransmitTrainImg(trainImg);
        }

        /// <summary>
        /// 传入训练图像
        /// </summary>
        /// <param name="trainImg"></param>
        private void TransmitTrainImg(HalconDotNet.HObject trainImg)
        {
            _locked = true;
            if (_instanceContour != null
               && _instanceContour.IsInitialized())
            {
                _instanceContour.Dispose();
                _instanceContour = null;
            }

            trkbDisplayLevel.Enabled = false;
            trkbDisplayLevel.Value = 1;
            numUpDwnDisplayLevel.Enabled = false;
            numUpDwnDisplayLevel.Value = 1;
            _locked = false;

            this.chkbAlwaysFind.Checked = false;

            //设置选择的图像为训练图像
            if (!_matchModelAssistant.SetTrainImage(trainImg))      
                return;

            //显示窗口显示区域复位且显示内容清空
            HWndCtrller.ResetAll();  

            if (_matchModelAssistant.OnExternalModelID)
                _modelContour = _matchModelAssistant.GetTrainInstanceContour(); //此时获取的是匹配模板的实例轮廓(已经加载过模板匹配模型)

            if (this.tabControlModel.SelectedIndex != 0)
            {
                this.tabControlModel.SelectedIndex = 0;
            }

            DisplayTrainImage();
        }

        /// <summary>
        /// 显示训练图像
        /// </summary>
        private void DisplayTrainImage()
        {
            ChangePromptionPanelColor(ProVision.InteractiveROI.HWndCtrller.PAINT_MODE_INCLUDE_ROI);
            SetModelGraphics();
            HWndCtrller.Repaint();//模型匹配--显示训练状态,重新绘图
        }

        /// <summary>
        /// 根据指定的模式,切换当前图像并
        /// 改变提示容器的颜色
        /// </summary>
        /// <param name="roiPaintMode">
        /// 绘图模式
        /// 1-包含ROI,2-不包含ROI</param>
        private void ChangePromptionPanelColor(int roiPaintMode)
        {
            HWndCtrller.SetROIPaintMode(roiPaintMode);
            switch (roiPaintMode)
            {
                case ProVision.InteractiveROI.HWndCtrller.PAINT_MODE_INCLUDE_ROI:
                    {
                        _currentImage = _matchModelAssistant.GetPyramidImage();
                        if (this._matchModelAssistant.OnExternalModelID)
                        {
                            //外部加载的模板
                            this.splitContainerView.Panel1.BackColor = System.Drawing.SystemColors.ControlDark;
                            
                        }
                        else if (this._currentImage == null)
                        {
                            //显示训练图像的窗口背景(未创建模板图像，弹窗提示?)
                            this.splitContainerView.Panel1.BackColor = _createModelColor;
                        }
                        else
                        {
                            //显示训练图像的窗口背景(已创建模板图像，允许修改)
                            this.splitContainerView.Panel1.BackColor = _createModelColor;
                        }
                    }
                    break;
                case ProVision.InteractiveROI.HWndCtrller.PAINT_MODE_EXCLUDE_ROI:
                    {
                        _currentImage = _matchModelAssistant.GetCurrentTestImage();
                        //显示测试图像的窗口背景
                        this.splitContainerView.Panel1.BackColor = _trainModelColor;
                    }
                    break;
                default: break;
            }
        }

        /// <summary>
        /// 模板的图形上下文设置
        /// [包含图像,模板区域,
        /// 模板轮廓的图形上下文设置]
        /// </summary>
        private void SetModelGraphics()
        {
            if (_currentImage != null
                && _currentImage.IsInitialized())
            {
                HWndCtrller.ClearEntities();
                HWndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_LINESTYLE, new HalconDotNet.HTuple());
                HWndCtrller.AddHobjEntity(_currentImage);
            }

            //模板提取区域
            if (ExtractRegion != null
                && ExtractRegion.IsInitialized())
            {
                HWndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_COLOR, _extractRegionColor);
                HWndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_LINEWIDTH, _modelRegionLineWidth);
                HWndCtrller.AddHobjEntity(ExtractRegion);
            }

            //模板区域(显示的收按照margin显示的，故而不需要显示模板区域)
            //if (_modelRegion != null
            //    && _modelRegion.IsInitialized())
            //{
            //    HWndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_COLOR, _modelRegionColor);
            //    HWndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_LINEWIDTH, _modelRegionLineWidth);
            //    HWndCtrller.AddHobjEntity(_modelRegion);
            //}

          
            //模板轮廓
            if (_modelContour != null
                && _modelContour.IsInitialized()) //训练图像中的匹配模板的实例轮廓
            {
                HWndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_COLOR, _modelContourColor);
                HWndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_LINEWIDTH, _modelContourLineWidth);
                HWndCtrller.AddHobjEntity(_modelContour);
            }

            //模板搜索区域
            if (SearchRegion != null
                && SearchRegion.IsInitialized())
            {
                HWndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_COLOR, _searchRegionColor);
                HWndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_LINEWIDTH, _modelRegionLineWidth);
                HWndCtrller.AddHobjEntity(SearchRegion);
            }
        }

        /// <summary>
        /// 匹配实例的图形上下文设置
        /// </summary>
        private void SetInstanceGraphic()
        {
            if (_currentImage != null
                && _currentImage.IsInitialized())
            {
                HWndCtrller.ClearEntities();
                HWndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_LINESTYLE, new HalconDotNet.HTuple());
                HWndCtrller.AddHobjEntity(_currentImage);
            }

            if (_instanceContour != null
                && _instanceContour.IsInitialized())
            {
                HWndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_COLOR, _instanceContourColor);
                HWndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_LINEWIDTH, _instanceContourLineWidth);
                HWndCtrller.AddHobjEntity(_instanceContour);

                int cnt = _instanceContour.CountObj(); //匹配实例的轮廓对象数可能很多
                if (_activeInsContourIdx >= 0
                    && _activeInsContourIdx < cnt)
                {
                    //待处理----多个匹配实例选择其中一个
                    this.HWndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_COLOR, _activeInstanceContourColor);
                    this.HWndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_LINEWIDTH, _instanceContourLineWidth);
                    this.HWndCtrller.AddHobjEntity(_instanceContour.SelectObj(_activeInsContourIdx + 1));
                }
            }

        }

        /// <summary>
        /// 初始化字段
        /// </summary>
        /// <param name="matchModelPara"></param>
        protected internal virtual void InitField(ProVision.Communal.MatchModelParameter matchModelPara=null)
        {
            string imgPathDefault = (string)HalconDotNet.HSystem.GetSystem("image_dir").TupleSplit(";");
            _ofdTrainImg = new OpenFileDialog();
            _ofdTrainImg.InitialDirectory = imgPathDefault;
            _ofdTestImgs = new OpenFileDialog();
            _ofdTestImgs.InitialDirectory = imgPathDefault;
            _ofdModelFile = new OpenFileDialog();
            _ofdModelFile.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            _fbdModelFolder = new FolderBrowserDialog();
            _fbdModelFolder.RootFolder = System.Environment.SpecialFolder.MyComputer;
            _fbdModelFolder.ShowNewFolderButton = true;
            _sfdModelFile = new SaveFileDialog();
            _sfdModelFile.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            _createModelColor = Color.RoyalBlue;
            _trainModelColor = Color.Chartreuse;

            _timer = new Timer();
            _timer.Interval = 50;
            _timer.Tick += Timer_Tick;

            HalconDotNet.HOperatorSet.SetSystem("tsp_height", 10000);
            HalconDotNet.HOperatorSet.SetSystem("tsp_width", 10000);

            ModelID = new HalconDotNet.HTuple(-1);
            ModelPose = new HalconDotNet.HTuple();          
                                       
            ROICtrller = new ProVision.InteractiveROI.ROIManager();
            ROICtrller.SetActiveROISign(ProVision.InteractiveROI.ROIManager.ROI_MODE_POS);
            ROICtrller.IconicUpdatedDel += new ProVision.InteractiveROI.IconicUpdatedDelegate(OnIconicUpdated);

            HWndCtrller = new ProVision.InteractiveROI.HWndCtrller(this.hWindowControl);
            HWndCtrller.RegisterROICtroller(ROICtrller);
            HWndCtrller.IconicUpdatedEvt += new ProVision.InteractiveROI.IconicUpdatedDelegate(OnIconicUpdated);
            HWndCtrller.RegisterHwndCtrlMouseEvents();                          //控件的鼠标事件(单击按下，单击抬起，移动，滑动)
            this.hWindowControl.HMouseMove += HWindowControl_HMouseMove; //控件的鼠标移动事件再注册
            this.hWindowControl.HMouseDown += HWindowControl_HMouseDown; //控件的鼠标按下事件再注册
            this.hWindowControl.HMouseUp += HWindowControl_HMouseUp;     //控件的鼠标抬起事件再注册
            this.hWindowControl.SizeChanged += HWindowControl_SizeChanged;
            HWndCtrller.SetViewMode(ProVision.InteractiveROI.HWndCtrller.VIEW_MODE_NONE);

            GrdMsk = new Communal.DotMatriceMask();
            _drawMode = ProVision.Communal.DrawMode.Fill;

            if (matchModelPara==null)
            {
                _matchModelAssistant = new ProVision.MatchModel.MatchModelAssistant(new Communal.MatchModelParameter());
                _specifiedModelPara = false;
            }
            else
            {
                _matchModelAssistant = new ProVision.MatchModel.MatchModelAssistant(matchModelPara);
                _specifiedModelPara = true;
            }          

            _matchModelAssistant.ModelMatchedDel = new ProVision.MatchModel.ModelMatchedDelegate(OnModelMatched);
            _matchModelAssistant.AutoParameterizedDel = new ProVision.MatchModel.AutoParameterizedDelegate(OnAutoParameterized);

            _matchModelOptAccuracy = new ProVision.MatchModel.MatchModelOptAccuracy(_matchModelAssistant);
            _matchModelOptAccuracy.RecoginzedAndStatisticedDel = new ProVision.MatchModel.RecognizedAndStatisticedDelegate(OnRecognizedAndStatisticed);

            _matchModelOptResults = new ProVision.MatchModel.MatchModelOptResults(_matchModelAssistant);
            _matchModelOptResults.RecoginzedAndStatisticedDel = new ProVision.MatchModel.RecognizedAndStatisticedDelegate(OnRecognizedAndStatisticed);

            _extractRegionColor = "blue";
            _modelRegionColor = "red";
            _modelContourColor = "red";
            _instanceContourColor = "yellow";
            _activeInstanceContourColor = "green";           
            _searchRegionColor="magenta";

            _modelRegionLineWidth = 3;
            _modelContourLineWidth = 1;
            _instanceContourLineWidth = 3;
            _activeInsContourIdx = -1;

            _brushSize = 10;
            _isRegionForExtractModel = true;
        }

        private void HWindowControl_SizeChanged(object sender, EventArgs e)
        {
            if(ROICtrller!=null
                && HWndCtrller!=null)
            {
                HWndCtrller.ResetAll();
                HWndCtrller.Repaint();
            }
        }

        private void HWindowControl_HMouseUp(object sender, HalconDotNet.HMouseEventArgs e)
        {
            //if(ExtractRegion!=null
            //    && ExtractRegion.IsInitialized())
            //{
               
            //}

            if ((ExtractRegion != null
               && ExtractRegion.IsInitialized())
               || (SearchRegion != null
               && SearchRegion.IsInitialized()))
            {
                //更新模板提取区域后重新提取模板信息
                ROICtrller.IconicUpdatedDel(ProVision.InteractiveROI.ROIManager.EVENT_UPDATE_ROI);
            }
        }

        /// <summary>
        /// HWindowControl控件上鼠标按下事件
        /// []
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HWindowControl_HMouseDown(object sender, HalconDotNet.HMouseEventArgs e)
        {
            //若是绘制模板提取区域:按下左键并移动时绘制一个区域并与掩膜区域进行并或差操作
            //若是绘制模板搜索区域:按下左键并移动时按照ROI的管理方式进行(平移和拖放)
            DrawMaskAndDisplay(e, _brushSize);
        }


        /// <summary>
        /// 模板提取区域情形,绘制掩膜区域并显示
        /// 模板搜索区域情形,修选搜索区域并显示
        /// </summary>
        private void DrawMaskAndDisplay(HalconDotNet.HMouseEventArgs e,int radius)
        {
            //绘制模板提取区域情形
            if (this.rdbtnModelExtract.Checked)
            {
                //自由绘制且笔刷启用
                if (rdbtnFreeDraw.Checked
                    && chkbBrushOnOff.Checked)
                {
                    HalconDotNet.HRegion hRegion;
                    if (this.chkbBrushShape.Checked)
                    {
                        //方形笔刷
                        hRegion = new HalconDotNet.HRegion(e.Y - radius / 2, e.X - radius / 2, e.Y + radius / 2, e.X + radius / 2);

                        //HalconDotNet.HObject region = new HalconDotNet.HObject();
                        //region.Dispose();                  
                        //HalconDotNet.HOperatorSet.GenRectangle1(out region, e.Y - radius / 2, e.X - radius / 2, e.Y + radius / 2, e.X + radius / 2);
                    }
                    else
                    {
                        //圆形笔刷              
                        hRegion = new HalconDotNet.HRegion(e.Y, e.X, radius);

                        //HalconDotNet.HObject region = new HalconDotNet.HObject();
                        //region.Dispose();
                        //HalconDotNet.HOperatorSet.GenCircle(out region, new HalconDotNet.HTuple(e.Y), new HalconDotNet.HTuple(e.Y), new HalconDotNet.HTuple(radius));
                    }

                    HalconDotNet.HObject hObj = new HalconDotNet.HObject(hRegion);
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        if (_drawMode == ProVision.Communal.DrawMode.Fill)
                        {
                            if (GrdMsk != null)
                            {
                                GrdMsk.UnionRegion(hObj);
                            }
                        }
                        else if (_drawMode == ProVision.Communal.DrawMode.Erase)
                        {
                            if (GrdMsk != null)
                            {
                                GrdMsk.DifferenceRegion(hObj);
                            }
                        }

                        ExtractRegion = GrdMsk.MaskRegion;
                        DisplayMaskAndSearchRegion(hObj,SearchRegion);
                    }
                    hRegion.Dispose();
                }
                //基础形状绘制
                else if (!rdbtnFreeDraw.Checked)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        if (this.chkbBrushOnOff.Checked)
                        {
                            //已经定义基本形状(固定位置)
                            if (ExtractRegion != null
                                && ExtractRegion.IsInitialized())
                            {
                                if (GrdMsk != null)
                                    GrdMsk.MaskRegion = ExtractRegion;

                                HalconDotNet.HRegion hRegion;
                                if (this.chkbBrushShape.Checked)
                                {
                                    //方形笔刷
                                    hRegion = new HalconDotNet.HRegion(e.Y - radius / 2, e.X - radius / 2, e.Y + radius / 2, e.X + radius / 2);
                                }
                                else
                                {
                                    //圆形笔刷
                                    hRegion = new HalconDotNet.HRegion(e.Y, e.X, radius);
                                }

                                HalconDotNet.HObject hObj = new HalconDotNet.HObject(hRegion);

                                if (_drawMode == ProVision.Communal.DrawMode.Fill)
                                {
                                    if (GrdMsk != null)
                                    {
                                        GrdMsk.UnionRegion(hObj);
                                    }
                                }
                                else if (_drawMode == ProVision.Communal.DrawMode.Erase)
                                {
                                    if (GrdMsk != null)
                                    {
                                        GrdMsk.DifferenceRegion(hObj);
                                    }
                                }

                                //ExtractRegion作为引用变量在进行并或差操作时，已经被释放掉,需要重新指向内存
                                ExtractRegion = GrdMsk.MaskRegion;
                                DisplayMaskAndSearchRegion(hObj, SearchRegion);
                                hRegion.Dispose();
                            }
                        }
                        else
                        {
                            //获取模板提取区域
                            if (ROICtrller.CalculateSyntheticalRegion())
                                ExtractRegion = ROICtrller.GetSyntheticalRegion().Clone();
                        }
                    }
                }
            }
            //绘制搜索区域情形
            else if(this.rdbtnModelSearch.Checked)
            {
                if (ROICtrller.CalculateSyntheticalRegion())
                    SearchRegion = ROICtrller.GetSyntheticalRegion().Clone();
            }
         
        }

        /// <summary>
        /// 显示掩膜区域和搜索区域
        /// [注:掩膜区域包括模板提取区域]
        /// </summary>
        private void DisplayMaskAndSearchRegion(HalconDotNet.HObject brushRegion, HalconDotNet.HObject searchRegion)
        {
            HWndCtrller.ClearEntities();
            if (_currentImage != null
                && _currentImage.IsInitialized())
                HWndCtrller.AddHobjEntity(_currentImage);

            HWndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_COLOR, _extractRegionColor);
            HWndCtrller.AddHobjEntity(brushRegion);
            HWndCtrller.AddHobjEntity(GrdMsk.MaskRegion);
            HWndCtrller.AddHobjEntity(GrdMsk.DotMatriceMaskRegion);
            HWndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_COLOR, _searchRegionColor);
            HWndCtrller.AddHobjEntity(searchRegion);
            HWndCtrller.Repaint();

            if (brushRegion != null
                && brushRegion.IsInitialized())
                brushRegion.Dispose();
        }

        /// <summary>
        /// HWindowControl控件上鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HWindowControl_HMouseMove(object sender, HalconDotNet.HMouseEventArgs e)
        {
            //若是绘制模板提取区域:按下左键并移动时绘制一个区域并与掩膜区域进行并或差操作
            //若是绘制模板搜索区域:按下左键并移动时按照ROI的管理方式进行(平移和拖放)
            DrawMaskAndDisplay(e, _brushSize);
            UpdateMouseCoordinateAndGrayValue(e);
        }

        /// <summary>
        /// 更新鼠标坐标及像素灰度值
        /// </summary>
        /// <param name="e"></param>
        private void UpdateMouseCoordinateAndGrayValue(HalconDotNet.HMouseEventArgs e)
        {
            HalconDotNet.HTuple gv = new HalconDotNet.HTuple();
            if (this._currentImage != null
                && this._currentImage.IsInitialized())
            {
                HalconDotNet.HObject domain;
                HalconDotNet.HTuple isInside = new HalconDotNet.HTuple();
                HalconDotNet.HOperatorSet.GetDomain(this._currentImage, out domain);
                HalconDotNet.HOperatorSet.TestRegionPoint(domain, new HalconDotNet.HTuple(e.Y), new HalconDotNet.HTuple(e.X), out isInside);

                if (isInside.TupleNotEqual(new HalconDotNet.HTuple())
                    && isInside.Length == 1
                    && isInside[0] == 1)
                {
                    try
                    {
                        HalconDotNet.HOperatorSet.GetGrayval(this._currentImage, new HalconDotNet.HTuple(e.Y), new HalconDotNet.HTuple(e.X), out gv);
                    }
#pragma warning disable CS0168 // The variable 'hex' is declared but never used
                    catch (HalconDotNet.HalconException hex)
#pragma warning restore CS0168 // The variable 'hex' is declared but never used
                    {

                    }
                }
            }

            string gs = (gv.TupleEqual(new HalconDotNet.HTuple())) ? "-" : gv.I.ToString();
            this.lblCoordinateGrayValue.Text = "行:" + e.Y.ToString("00.00") + ",列:" + e.X.ToString("00.00") + ",像素:" + gs;
        }

        /// <summary>
        /// This method is invoked if changes occur in the HWndCtrller instance
        /// or the ROICtrller. In either case, the HALCON 
        /// window needs to be updated/repainted.
        /// </summary>
        private void OnIconicUpdated(int val)
        {
            if (_matchModelAssistant == null)
                return;
            switch (val)
            {
                //ROI创建和更新执行一次即可
                case ProVision.InteractiveROI.ROIManager.EVENT_CHANGED_ROI_SIGN:
                case ProVision.InteractiveROI.ROIManager.EVENT_DELETED_ACTROI:
                case ProVision.InteractiveROI.ROIManager.EVENT_DELETED_ALL_ROIS: //清空模板时
                case ProVision.InteractiveROI.ROIManager.EVENT_UPDATE_ROI:      //创建交互ROI定义模板提取区域时或加载模板(包含加载模板提取区域)
                    {
                        if(this.rdbtnModelExtract.Checked)
                        {
                            //此处仅用来在更改交互获得的ROI模板区域后，提取金字塔图层和ROI区域层
                            if (ExtractRegion != null
                                && ExtractRegion.IsInitialized())
                            {
                                if (_modelRegion != null)
                                {
                                    _modelRegion.Dispose();
                                }

                                if (_modelContour != null
                                     && _modelContour.IsInitialized())
                                {
                                    _modelContour.Dispose();
                                    _modelContour = null;
                                }

                                if (_instanceContour != null)
                                {
                                    _instanceContour.Dispose();
                                    _instanceContour = null;
                                }

                                _matchModelAssistant.SetModelROI(ExtractRegion);
                            }
                        }else if(this.rdbtnModelSearch.Checked)
                        {
                            if(SearchRegion!=null
                                && SearchRegion.IsInitialized())
                            {
                                OnModelMatched(ProVision.MatchModel.MatchModelAssistant.UPDATE_MODEL_XLD);
                            }
                        }
                    }
                    break;
                case ProVision.InteractiveROI.HWndCtrller.ERROR_READING_IMAGE:
                    {
                        System.Windows.Forms.MessageBox.Show("读取文件时发生异常!\r\n" + this.HWndCtrller.ExceptionMsg,
                            "警告信息-[匹配向导]", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
                default: break;
            }
        }

        /// <summary>
        /// This method is invoked for any changes in the 
        /// 'MatchModel', concerning the model creation and
        /// the model finding. Also changes in the display mode 
        /// (e.g., pyramid level) are mapped here.
        /// </summary>
        private void OnModelMatched(int val)
        {
            bool isrepaint = false; //是否重绘
            switch (val)
            {
                case ProVision.MatchModel.MatchModelAssistant.UPDATE_MODEL_XLD:
                case ProVision.MatchModel.MatchModelAssistant.UPDATE_DISP_LEVEL:
                    {
                        //注意：_currentImage仅仅是引用选择的图像(即作为主变量的别名),不能释放其资源,否则主变量会被释放,造成引用异常
                        _currentImage = _matchModelAssistant.GetPyramidImage();                       
                        _modelContour = _matchModelAssistant.GetPyramidContour();
                        SetModelGraphics();
                        isrepaint = true;
                    }
                    break;
                case ProVision.MatchModel.MatchModelAssistant.UPDATE_PYRAMID_LEVELS:
                    {
                        this.numUpDwnDisplayLevel.Enabled = true;
                        this.trkbDisplayLevel.Enabled = true;
                    }
                    break;
                case ProVision.MatchModel.MatchModelAssistant.UPDATE_DETECTION_RESULT:
                    {
                        _instanceContour = _matchModelAssistant.Result.GetInstanceContour();
                        SetInstanceGraphic();
                        isrepaint = true;
                    }
                    break;
                case ProVision.MatchModel.MatchModelAssistant.UPDATE_TEST_VIEW:
                    {
                        _currentImage = _matchModelAssistant.GetCurrentTestImage();
                        SetInstanceGraphic();
                        isrepaint = true;
                    }
                    break;
                case ProVision.MatchModel.MatchModelAssistant.ERR_WRITE_SHAPEMODEL:
                    {
                        System.Windows.Forms.MessageBox.Show("写入文件时发生异常!\r\n" + _matchModelAssistant.ExceptionMsg,
                           "错误信息-[匹配向导]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case ProVision.MatchModel.MatchModelAssistant.ERR_READ_SHAPEMODEL:
                    {
                        System.Windows.Forms.MessageBox.Show("读取文件时发生异常!\r\n" + _matchModelAssistant.ExceptionMsg,
                          "错误信息-[匹配向导]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case ProVision.MatchModel.MatchModelAssistant.ERR_NO_MODEL_DEFINED:
                    {
                        System.Windows.Forms.MessageBox.Show("未能创建模板匹配模型!\r\n请先定义模板区域!",
                          "提示信息-[匹配向导]", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case ProVision.MatchModel.MatchModelAssistant.ERR_NO_IMAGE:
                    {
                        //System.Windows.Forms.MessageBox.Show("未加载模板图像!",
                        // "警示信息-[匹配向导]", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    break;
                case ProVision.MatchModel.MatchModelAssistant.ERR_NO_TESTIMAGE:
                    {
                        System.Windows.Forms.MessageBox.Show("未加载测试图像!",
                         "警示信息-[匹配向导]", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    break;
                case ProVision.MatchModel.MatchModelAssistant.ERR_NO_VALID_FILE:
                    {
                        System.Windows.Forms.MessageBox.Show("已加载的文件非Halcon定义的模板匹配模型文件(*.shm)!",
                         "警示信息-[匹配向导]", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    break;
                case ProVision.MatchModel.MatchModelAssistant.ERR_READING_IMG:
                    OnIconicUpdated(ProVision.InteractiveROI.HWndCtrller.ERROR_READING_IMAGE);
                    break;
                default: break;
            }
            if (isrepaint)
                HWndCtrller.Repaint();
        }

        /// <summary>
        /// Calculates new optimal values for a parameter, if the parameter is
        /// in the auto-mode list. The new settings are forwarded to the GUI
        /// components to update the display.
        /// </summary>
        private void OnAutoParameterized(string val)
        {
            int[] r;
            this._locked = true;
            switch (val)
            {
                case ProVision.Communal.MatchModelParameter.AUTO_ANGLE_STEP:
                    this.numUpDwnAngleStep.Value = (int)(_matchModelAssistant.Parameter.AngleStep * 10.0 * 180.0 / System.Math.PI);
                    break;
                case ProVision.Communal.MatchModelParameter.AUTO_CONTRAST:
                    this.numUpDwnContrast.Value = _matchModelAssistant.Parameter.Contrast;
                    break;
                case ProVision.Communal.MatchModelParameter.AUTO_MIN_CONTRAST:
                    this.numUpDwnMinContrast.Value = _matchModelAssistant.Parameter.MinContrast;
                    break;
                case ProVision.Communal.MatchModelParameter.AUTO_NUM_LEVELS:
                    this.numUpDwnNumLevels.Value = _matchModelAssistant.Parameter.NumLevels;
                    break;
                case ProVision.Communal.MatchModelParameter.AUTO_OPTIMIZATION:
                    this.cmbOptimization.Text = _matchModelAssistant.Parameter.Optimization;
                    break;
                case ProVision.Communal.MatchModelParameter.AUTO_SCALE_STEP:
                    this.numUpDwnScaleStep.Value = (int)(_matchModelAssistant.Parameter.ScaleStep * 1000.0);
                    break;
                case ProVision.Communal.MatchModelParameter.CONTROL_ANGLE_EXTENT:
                    this.numUpDwnAngleExtent.Value = (int)(_matchModelAssistant.Parameter.AngleExtent * 180.0 / System.Math.PI);
                    break;
                case ProVision.Communal.MatchModelParameter.CONTROL_ANGLE_START:
                    this.numUpDwnStartAngle.Value = (int)(_matchModelAssistant.Parameter.StartAngle * 180.0 / System.Math.PI);
                    break;
                case ProVision.Communal.MatchModelParameter.CONTROL_GREEDINESS:
                    this.numUpDwnGreediness.Value = (int)(_matchModelAssistant.Parameter.Greediness * 100.0);
                    break;
                case ProVision.Communal.MatchModelParameter.CONTROL_METRIC:
                    this.cmbMetric.Text = _matchModelAssistant.Parameter.Metric;
                    break;
                case ProVision.Communal.MatchModelParameter.CONTROL_MINSCORE:
                    this.numUpDwnMinScore.Value = (int)(_matchModelAssistant.Parameter.MinScore * 100.0);
                    break;
                case ProVision.Communal.MatchModelParameter.CONTROL_SCALE_MAX:
                    this.numUpDwnMaxScale.Value = (int)(_matchModelAssistant.Parameter.ScaleMax * 100.0);
                    break;
                case ProVision.Communal.MatchModelParameter.CONTROL_SCALE_MIN:
                    this.numUpDwnMinScale.Value = (int)(_matchModelAssistant.Parameter.ScaleMin * 100.0);
                    break;
                case ProVision.Communal.MatchModelParameter.H_ERR_MESSAGE:
                    {
                        System.Windows.Forms.MessageBox.Show("参数异常!\r\n" + this._matchModelAssistant.ExceptionMsg,
                           "警示信息-[匹配向导]", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    break;
                case ProVision.Communal.MatchModelParameter.RANGE_ANGLE_STEP:
                    {
                        r = this._matchModelAssistant.GetStepRange(ProVision.Communal.MatchModelParameter.RANGE_ANGLE_STEP);
                        this.numUpDwnAngleStep.Minimum = (decimal)r[0];
                        this.trkbAngleStep.Minimum = r[0];
                        this.numUpDwnAngleStep.Maximum = (decimal)r[1];
                        this.trkbAngleStep.Maximum = r[1];

                        this.numUpDwnAngleStep.Value = (int)(_matchModelAssistant.Parameter.AngleStep * 10.0 * 180.0 / System.Math.PI);
                    }
                    break;
                case ProVision.Communal.MatchModelParameter.RANGE_SCALE_STEP:
                    {
                        r = this._matchModelAssistant.GetStepRange(ProVision.Communal.MatchModelParameter.RANGE_SCALE_STEP);
                        this.numUpDwnScaleStep.Minimum = (decimal)r[0];
                        this.trkbScaleStep.Minimum = r[0];
                        this.numUpDwnScaleStep.Maximum = (decimal)r[1];
                        this.trkbScaleStep.Maximum = r[1];

                        this.numUpDwnScaleStep.Value = (int)(_matchModelAssistant.Parameter.ScaleStep * 1000.0);
                    }
                    break;
                default: break;
            }
            this._locked = false;
        }

        /// <summary>
        /// This method is invoked when the inspection tab or the 
        /// recognition tab are triggered to compute the optimized values
        /// and to forward the results to the display.
        /// </summary>
        private void OnRecognizedAndStatisticed(int val)
        {
            switch (val)
            {
                case ProVision.MatchModel.MatchModelOpt.UPDATE_RECOG_STATISTICS_STATUS:
                    this.lblOptimizationStatus.Text = _matchModelOpt.OptimizationStatus;
                    break;
                case ProVision.MatchModel.MatchModelOpt.UPDATE_RECOG_UPDATED_VALS:
                    {
                        //更新:识别更新后的参数
                        this.lblLastMinScore.Text = _matchModelOpt.RecogTabOpimizationData[0];
                        this.lblLastGreediness.Text = _matchModelOpt.RecogTabOpimizationData[1];
                        this.lblLastRecogRate.Text = _matchModelOpt.RecogTabOpimizationData[2];
                        this.lblLastElapse.Text = _matchModelOpt.RecogTabOpimizationData[3];
                    }
                    break;
                case ProVision.MatchModel.MatchModelOpt.UPDATE_RECOG_OPTIMAL_VALS:
                    {
                        //更新:优化后的参数
                        this.lblOptMinScore.Text = _matchModelOpt.RecogTabOpimizationData[4];
                        this.lblOptGreediness.Text = _matchModelOpt.RecogTabOpimizationData[5];
                        this.lblOptRecogRate.Text = _matchModelOpt.RecogTabOpimizationData[6];
                        this.lblOptElapse.Text = _matchModelOpt.RecogTabOpimizationData[7];
                    }
                    break;
                case ProVision.MatchModel.MatchModelOpt.UPDATE_INSP_RECOGRATE:
                    {
                        //更新:检测模板识别率
                        this.lblAtleastOne.Text = _matchModelOpt.InspectTabRecogRateData[0];
                        this.lblSpecifiedNum.Text = _matchModelOpt.InspectTabRecogRateData[1];
                        this.lblMaxNum.Text = _matchModelOpt.InspectTabRecogRateData[2];
                        this.lblToSpecifiedNum.Text = _matchModelOpt.InspectTabRecogRateData[3];
                        this.lblToMaxNum.Text = _matchModelOpt.InspectTabRecogRateData[4];
                    }
                    break;
                case ProVision.MatchModel.MatchModelOpt.UPDATE_INSP_RESULTS:
                    {
                        //更新:检测模板统计
                        this.lblInspectMinScore.Text = _matchModelOpt.InspectTabResultsData[0];
                        this.lblInspectMaxScore.Text = _matchModelOpt.InspectTabResultsData[1];
                        this.lblInspectRangeScore.Text = _matchModelOpt.InspectTabResultsData[2];

                        this.lblInspectMinElapse.Text = _matchModelOpt.InspectTabResultsData[3];
                        this.lblInspectMaxElapse.Text = _matchModelOpt.InspectTabResultsData[4];
                        this.lblInspectRangeElapse.Text = _matchModelOpt.InspectTabResultsData[5];

                        this.lblInspectMinRow.Text = _matchModelOpt.InspectTabResultsData[6];
                        this.lblInspectMaxRow.Text = _matchModelOpt.InspectTabResultsData[7];
                        this.lblInspectRangeRow.Text = _matchModelOpt.InspectTabResultsData[8];

                        this.lblInspectMinCol.Text = _matchModelOpt.InspectTabResultsData[9];
                        this.lblInspectMaxCol.Text = _matchModelOpt.InspectTabResultsData[10];
                        this.lblInspectRangeCol.Text =_matchModelOpt.InspectTabResultsData[11];

                        this.lblInspectMinAngle.Text = _matchModelOpt.InspectTabResultsData[12];
                        this.lblInspectMaxAngle.Text = _matchModelOpt.InspectTabResultsData[13];
                        this.lblInspectRangeAngle.Text = _matchModelOpt.InspectTabResultsData[14];

                        this.lblInspectMinRowScale.Text = _matchModelOpt.InspectTabResultsData[15];
                        this.lblInspectMaxRowScale.Text = _matchModelOpt.InspectTabResultsData[16];
                        this.lblInspectRangeRowScale.Text = _matchModelOpt.InspectTabResultsData[17];

                        this.lblInspectMinColScale.Text = _matchModelOpt.InspectTabResultsData[18];
                        this.lblInspectMaxColScale.Text = _matchModelOpt.InspectTabResultsData[19];
                        this.lblInspectRangeColScale.Text = _matchModelOpt.InspectTabResultsData[20];
                    }
                    break;
                case ProVision.MatchModel.MatchModelOpt.UPDATE_TEST_ERR:
                    {
                        System.Windows.Forms.MessageBox.Show("优化异常!\r\n请检查是否定义模板匹配模型!",
                           "警示信息-[匹配向导]", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    break;
                case ProVision.MatchModel.MatchModelOpt.UPDATE_RECOG_ERR:
                    {
                        System.Windows.Forms.MessageBox.Show("识别异常!\r\n无法匹配到对应模型的参数设置的实例\r\n请检查是否定义模板匹配模型且适当的参数设置",
                           "错误信息-[匹配向导]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case ProVision.MatchModel.MatchModelOpt.RUN_SUCCESSFUL:
                    OnAutoParameterized(ProVision.Communal.MatchModelParameter.CONTROL_GREEDINESS);
                    OnAutoParameterized(ProVision.Communal.MatchModelParameter.CONTROL_MINSCORE);
                    break;
                case ProVision.MatchModel.MatchModelOpt.RUN_FAILED:
                    {
                        if (this.lstbTestImages.Items.Count != 0
                                   && this.chkbAlwaysFind.Checked)
                            ChangePromptionPanelColor(ProVision.InteractiveROI.HWndCtrller.PAINT_MODE_EXCLUDE_ROI);

                        _matchModelAssistant.SetMinScore((double)this.numUpDwnMinScore.Value / 100.0); 
                        _matchModelAssistant.SetGreediness((double)this.numUpDwnGreediness.Value / 100.0);
                    }
                    break;
                case ProVision.MatchModel.MatchModelAssistant.ERR_NO_TESTIMAGE:
                    OnModelMatched(ProVision.MatchModel.MatchModelAssistant.ERR_NO_TESTIMAGE);
                    break;
                default: break;
            }
        }

        protected internal virtual void InitControls()
        {
            this.numUpDwnNumLevels.Value = 6; //金字塔最大等级数
            this.numUpDwnMinContrast.Maximum = this.numUpDwnContrast.Value;
            this.trkbMinContrast.Maximum = (int)this.numUpDwnContrast.Value;
            this.cmbMetric.SelectedIndex = 0;
            this.cmbOptimization.SelectedIndex = 0;
            this.cmbSubPixel.SelectedIndex = 2;
            this.cmbRecogRateOption.SelectedIndex = 1;
            this.cmbOpLevel.SelectedIndex = 0;
            OnOperationLevelChanged(this.cmbOpLevel.SelectedIndex);
        }

        private void FrmMatchModel_Load(object sender, EventArgs e)
        {
            if (_specifiedModelPara)
                InitControlValueWithMatchModelParameter();
            else
                InitMatchModelParameterWithControlValue();
        }

        protected internal virtual void InitMatchModelParameterWithControlValue()
        {
            InitControls();
            _locked = true;           
            InitMatchModelParameterWithControlValue(_matchModelAssistant.Parameter);
            _locked = false;
        }

        protected internal virtual void InitControlValueWithMatchModelParameter()
        {
            InitControls();
            _locked = true;
            InitControlValueWithMatchModelParameter(_matchModelAssistant.Parameter);
            _locked = false;
        }

        /// <summary>
        /// 用控件值初始化匹配参数
        /// </summary>
        /// <param name="param"></param>
        private void InitMatchModelParameterWithControlValue(ProVision.Communal.MatchModelParameter param)
        {
            param.NumLevels = (int)this.numUpDwnNumLevels.Value;
            param.Metric = (string)this.cmbMetric.Items[this.cmbMetric.SelectedIndex];
            param.Optimization = (string)this.cmbOptimization.Items[this.cmbOptimization.SelectedIndex];
            param.Contrast = (int)this.numUpDwnContrast.Value;
            param.MinContrast = (int)this.numUpDwnMinContrast.Value;

            param.StartAngle = (double)this.numUpDwnStartAngle.Value * System.Math.PI / 180.0;
            param.AngleExtent = (double)(this.numUpDwnAngleExtent.Value) * System.Math.PI / 180.0;
            param.AngleStep = ((double)this.numUpDwnAngleStep.Value / 10.0) * System.Math.PI / 180.0;

            param.ScaleMin = (double)this.numUpDwnMinScale.Value / 100.0;
            param.ScaleMax = (double)this.numUpDwnMaxScale.Value / 100.0;
            param.ScaleStep = (double)this.numUpDwnScaleStep.Value / 1000.0;

            param.MinScore = (double)this.numUpDwnMinScore.Value / 100.0;
            param.NumToMatch = (int)this.numUpDwnNumToMatch.Value;
            param.Greediness = (double)this.numUpDwnGreediness.Value / 100.0;
            param.MaxOverLap = (double)this.numUpDwnMaxOverlap.Value / 100.0;
            param.SubPixel = (string)this.cmbSubPixel.Items[this.cmbSubPixel.SelectedIndex];
            param.LastNumLevel = (int)this.numUpDwnLastLevel.Value;

            param.RecogRateOpt = (int)this.cmbRecogRateOption.SelectedIndex;
            param.RecogRate = (int)this.numUpDwnRecogRate.Value;
            param.RecogAccuracyMode = ProVision.Communal.MatchModelParameter.RECOG_MODE_SPECIFIEDNUM;
            param.RecogSpecifiedMatchNum = (int)this.numUpDwnSpecifiedNum.Value;
            param.InspectMaxNoMatch = (int)this.numUpDwnMaxNumMatch.Value;
        }

        /// <summary>
        /// 用匹配参数初始化控件值
        /// </summary>
        /// <param name="param"></param>
        private void InitControlValueWithMatchModelParameter(ProVision.Communal.MatchModelParameter param)
        {
            this.numUpDwnNumLevels.Value = param.NumLevels;
            this.cmbMetric.SelectedIndex = this.cmbMetric.Items.IndexOf(param.Metric);
            this.cmbOptimization.SelectedIndex = this.cmbOptimization.Items.IndexOf(param.Optimization);
            this.numUpDwnContrast.Value = (decimal)param.Contrast;
            this.numUpDwnMinContrast.Value = (decimal)param.MinContrast;

            this.numUpDwnStartAngle.Value = (decimal)(param.StartAngle * 180.0 / System.Math.PI);
            this.numUpDwnAngleExtent.Value = (decimal)(param.AngleExtent * 180.0 / System.Math.PI);
            this.numUpDwnAngleStep.Value = (decimal)(param.AngleStep * 180.0 / System.Math.PI * 10.0);

            this.numUpDwnMinScale.Value = (decimal)(param.ScaleMin * 100.0);
            this.numUpDwnMaxScale.Value = (decimal)(param.ScaleMax * 100.0);
            this.numUpDwnScaleStep.Value = (decimal)(param.ScaleStep * 1000.0);

            this.numUpDwnMinScore.Value = (decimal)(param.MinScore * 100.0);
            this.numUpDwnNumToMatch.Value = (decimal)(param.NumToMatch);
            this.numUpDwnGreediness.Value = (decimal)(param.Greediness * 100.0);
            this.numUpDwnMaxOverlap.Value = (decimal)(param.MaxOverLap * 100.0);
            this.cmbSubPixel.SelectedIndex = this.cmbSubPixel.Items.IndexOf(param.SubPixel);
            this.numUpDwnLastLevel.Value = (decimal)(param.LastNumLevel);

            this.cmbRecogRateOption.SelectedIndex = param.RecogRateOpt;
            this.numUpDwnRecogRate.Value = (decimal)(param.RecogRate);
            this.numUpDwnSpecifiedNum.Value = (decimal)(param.RecogSpecifiedMatchNum);
            this.numUpDwnMaxNumMatch.Value = (decimal)(param.InspectMaxNoMatch);
            this.numUpDwnRecogRate.Value = (decimal)(param.RecogRate);
        }


        /// <summary>
        /// Button单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void Btn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            if(btn!=null
                && btn.Tag!=null)
            {
                switch(btn.Tag.ToString())
                {
                    case "LOADTRAINIMAGE":
                        LoadTrainImage();
                        break;
                    case "LOADMODEL":
                        LoadModel();
                        break;
                    case "CREATEMODEL":
                        CreateModel();
                        break;
                    case "SAVEMODEL":
                        SaveModel();
                        break;
                    case "LOADTESTIMAGES":
                        LoadTestImages();
                        break;
                    case "DELETETESTIMAGE":
                        DeleteTestImage();
                        break;
                    case "CLEARTESTIMAGES":
                        ClearTestImages();
                        break;
                    case "DISPLAYTESTIMAGE":
                        DisplayTestImage();
                        break;
                    case "FINDMODEL":
                        FindModel();
                        break;
                    case "OPTIMIZE":
                        RunOptimize();
                        break;
                    case "STATISTIC":
                        RunStatistic();
                        break;
                    case "CLEARROI":
                        {
                            if (_currentImage != null
                               && _currentImage.IsInitialized())
                            {
                                HWndCtrller.ResetAll();
                                HWndCtrller.ClearEntities();
                                if (GrdMsk.MaskRegion != null
                                && GrdMsk.MaskRegion.IsInitialized())
                                {
                                    GrdMsk.MaskRegion.Dispose();
                                    GrdMsk.MaskRegion = null;
                                }
                                HWndCtrller.AddHobjEntity(_currentImage);
                                HWndCtrller.Repaint();
                            }
                        }
                        break;
                    case "CONFIRMROI":
                        {
                            if (_currentImage != null
                              && _currentImage.IsInitialized())
                            {
                                if (ExtractRegion != null
                                   && ExtractRegion.IsInitialized())
                                {
                                    ExtractRegion.Dispose();
                                }
                            }
                        }
                        break;                                
                    default:break;
                }
            }                
        }

        #region Button单击事件函数

        /// <summary>
        /// 创建模板匹配模型
        /// </summary>
        protected internal virtual void CreateModel()
        {
            if (this.rdbtnContourShape.Checked)
            {
                if (ExtractRegion != null
                    && ExtractRegion.IsInitialized())
                {
                    if (_matchModelAssistant.ModelID.TupleEqual(new HalconDotNet.HTuple(-1)))
                    {
                        _matchModelAssistant.IsDetectInTrainImage = true;
                        _matchModelAssistant.ModelSearchRegion = SearchRegion;
                        _matchModelAssistant.DetectShapeModel();
                        ModelID = _matchModelAssistant.ModelID;
                        ModelPose = _matchModelAssistant.ModelPose;

                        if (ModelPose.TupleNotEqual(new HalconDotNet.HTuple()))
                            MessageBox.Show("获取模板模型参数成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else { MessageBox.Show("获取模板模型参数失败!\r\n模板位姿获取失败!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    }
                    else
                    {
                        if (MessageBox.Show("已创建模板匹配模型!\r\n是否重新创建模板匹配模型?", "询问信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            _matchModelAssistant.CreateShapeModel();
                            _matchModelAssistant.IsDetectInTrainImage = true;
                            _matchModelAssistant.DetectShapeModel();                          
                            ModelID = _matchModelAssistant.ModelID;
                            ModelPose = _matchModelAssistant.ModelPose;
                            if (ModelPose.TupleNotEqual(new HalconDotNet.HTuple()))
                                MessageBox.Show("获取模板模型参数成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else { MessageBox.Show("获取模板模型参数失败!\r\n模板位姿获取失败!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        }
                    }
                }
                else { MessageBox.Show("未定义模板提取区域!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            else if (this.rdbtnRegionShape.Checked)
            {
                if (ExtractRegion != null
                   && ExtractRegion.IsInitialized())
                {
                    if (_matchModelAssistant.ModelID.TupleEqual(new HalconDotNet.HTuple(-1)))
                    {
                        _matchModelAssistant.IsDetectInTrainImage = true;
                        _matchModelAssistant.ModelSearchRegion = SearchRegion;
                        _matchModelAssistant.DetectShapeModel();
                        ModelID = _matchModelAssistant.ModelID;
                        ModelPose = _matchModelAssistant.ModelPose;

                        if (ModelPose.TupleNotEqual(new HalconDotNet.HTuple()))
                            MessageBox.Show("获取模板模型参数成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else { MessageBox.Show("获取模板模型参数失败!\r\n模板位姿获取失败!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    }
                    else
                    {
                        if(MessageBox.Show("已创建模板匹配模型!\r\n是否重新创建模板匹配模型?", "询问信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            _matchModelAssistant.CreateShapeModel();
                            _matchModelAssistant.IsDetectInTrainImage = true;
                            _matchModelAssistant.DetectShapeModel();
                            ModelID = _matchModelAssistant.ModelID;
                            ModelPose = _matchModelAssistant.ModelPose;

                            if (ModelPose.TupleNotEqual(new HalconDotNet.HTuple()))
                                MessageBox.Show("获取模板模型参数成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else { MessageBox.Show("获取模板模型参数失败!\r\n模板位姿获取失败!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        }
                    }
                }
                else { MessageBox.Show("未定义模板提取区域!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
        }

        /// <summary>
        /// 保存模板
        /// </summary>
        protected internal virtual void SaveModel()
        {
            if (_currentImage != null
                && _currentImage.IsInitialized())
            {
                if (ExtractRegion != null
                    && ExtractRegion.IsInitialized())
                {
                    if (_matchModelAssistant != null
                        && _matchModelAssistant.ModelID.TupleNotEqual(new HalconDotNet.HTuple(-1)))
                    {
                        if (SearchRegion != null
                           && SearchRegion.IsInitialized())
                        {
                            _fbdModelFolder.Description = "请选择或者创建一个文件夹";
                            if (_fbdModelFolder.ShowDialog() == DialogResult.OK)
                            {
                                string folderPath = _fbdModelFolder.SelectedPath;
                                if (_imgExtention == "tif")
                                    _imgExtention = "tiff";
                                HalconDotNet.HOperatorSet.WriteImage(_currentImage, _imgExtention, new HalconDotNet.HTuple(0), folderPath + "\\TrainImage." + _imgExtention);
                                HalconDotNet.HOperatorSet.WriteRegion(ExtractRegion, folderPath + "\\ExtrackRegion.hobj");
                                HalconDotNet.HOperatorSet.WriteRegion(SearchRegion, folderPath + "\\SearchRegion.hobj");
                                ModelID = _matchModelAssistant.ModelID;
                                ModelPose = _matchModelAssistant.ModelPose;
                                HalconDotNet.HOperatorSet.WriteTuple(ModelPose, folderPath + "\\ModelPose.tup");
                                _matchModelAssistant.SaveShapeModel(folderPath + "\\ModelFile.shm");
                            }
                        }
                        else { MessageBox.Show("未创建模板搜索区域!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }                       
                    }
                    else { MessageBox.Show("未创建模板匹配模型!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                }
                else { MessageBox.Show("未创建模板提取区域!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            else { MessageBox.Show("未加载训练图像!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }       

        /// <summary>
        /// 清空模板
        /// </summary>
        protected internal virtual void ClearModel()
        {
            _matchModelAssistant.OnExternalModelID = false;
            this.chkbAlwaysFind.Checked = false;

            if (ExtractRegion != null
                && ExtractRegion.IsInitialized())
            {
                ExtractRegion.Dispose();
                ExtractRegion = null;
            }

            if (_modelContour != null)
            {
                _modelContour.Dispose();
                _modelContour = null;
            }

            if (_instanceContour != null
                && _instanceContour.IsInitialized())
            {
                _instanceContour.Dispose();
                _instanceContour = null;
            }

            _matchModelOptAccuracy.Reset();
            _matchModelOpt = _matchModelOptAccuracy;
            OnRecognizedAndStatisticed(ProVision.MatchModel.MatchModelOpt.UPDATE_RECOG_UPDATED_VALS);
            OnRecognizedAndStatisticed(ProVision.MatchModel.MatchModelOpt.UPDATE_RECOG_OPTIMAL_VALS);
            OnRecognizedAndStatisticed(ProVision.MatchModel.MatchModelOpt.UPDATE_RECOG_STATISTICS_STATUS);

            _matchModelOptResults.Reset();
            _matchModelOpt = _matchModelOptResults;
            OnRecognizedAndStatisticed(ProVision.MatchModel.MatchModelOpt.UPDATE_INSP_RECOGRATE);
            OnRecognizedAndStatisticed(ProVision.MatchModel.MatchModelOpt.UPDATE_INSP_RESULTS);

            _matchModelAssistant.Reset();
            //恢复缓存的模型参数
            _matchModelAssistant.RetrieveModelParameters();

            if (this.tabControlModel.SelectedIndex != 0)
            {
                this.tabControlModel.SelectedIndex = 0;
            }

            DisplayTrainImage();
        }

        /// <summary>
        /// 加载模板
        /// </summary>
        protected internal virtual void LoadModel()
        {
            _fbdModelFolder.Description = "请选择包含模板文件的文件夹";
            if (_fbdModelFolder.ShowDialog() == DialogResult.OK)
            {
                HalconDotNet.HTuple exist;
                string folderPath = _fbdModelFolder.SelectedPath;
                string extension = string.Empty;
                string name = string.Empty;
                int idx;
                System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(folderPath);
                if (dirInfo.GetFiles().Length + dirInfo.GetDirectories().Length == 0)
                {
                    MessageBox.Show("文件夹下无文件!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    System.IO.FileInfo[] windImgFiles = dirInfo.GetFiles();
                    foreach (System.IO.FileInfo itm in windImgFiles)
                    {
                        idx = itm.Name.LastIndexOf(".");
                        name = itm.Name.Substring(0, idx);
                        if (name == "TrainImage")
                        {
                            extension = itm.Extension;
                            break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(extension))
                {
                    HalconDotNet.HOperatorSet.FileExists(folderPath + "\\ExtrackRegion.hobj", out exist);
                    if ((int)exist != 0)
                    {
                        HalconDotNet.HOperatorSet.FileExists(folderPath + "\\SearchRegion.hobj", out exist);
                        if ((int)exist != 0)
                        {
                            HalconDotNet.HOperatorSet.FileExists(folderPath + "\\ModelPose.tup", out exist);
                            if ((int)exist != 0)
                            {
                                HalconDotNet.HOperatorSet.FileExists(folderPath + "\\ModelFile.shm", out exist);
                                if ((int)exist != 0)
                                {
                                    _matchModelAssistant.Reset();
                                    this.chkbAlwaysFind.Checked = false;

                                    HalconDotNet.HObject Imgtmp, extmp; HalconDotNet.HTuple mdlP;
                                    HalconDotNet.HOperatorSet.ReadImage(out Imgtmp, folderPath + "\\TrainImage" + extension);
                                    _matchModelAssistant.SetTrainImage(Imgtmp);
                                    HalconDotNet.HOperatorSet.ReadRegion(out extmp, folderPath + "\\ExtrackRegion.hobj");
                                    ExtractRegion = extmp;
                                    extmp.Dispose();
                                    HalconDotNet.HOperatorSet.ReadRegion(out extmp, folderPath + "\\SearchRegion.hobj");
                                    SearchRegion = extmp;
                                    HalconDotNet.HOperatorSet.ReadTuple(folderPath + "\\ModelPose.tup", out mdlP);
                                    ModelPose = mdlP;
                                    _matchModelAssistant.ModelPose = ModelPose;

                                    if (ModelPose[3].I == 0)
                                        this.rdbtnRegionShape.Checked = true;
                                    else if (ModelPose[3].I == 1)
                                        this.rdbtnContourShape.Checked = true;

                                    ROICtrller.Reset();
                                    if (_instanceContour != null
                                    && _instanceContour.IsInitialized())
                                    {
                                        _instanceContour.Dispose();
                                        _instanceContour = null;
                                    }

                                    //加载模板匹配模型文件
                                    if (_matchModelAssistant.LoadShapeModel(folderPath + "\\ModelFile.shm"))
                                    {
                                        ModelID = _matchModelAssistant.ModelID;
                                        _modelContour = _matchModelAssistant.GetTrainInstanceContour(); //此时获取的是匹配模板的实例轮廓(已经加载过模板匹配模型)

                                        if (this.tabControlModel.SelectedIndex != 0)
                                        {
                                            this.tabControlModel.SelectedIndex = 0;
                                        }

                                        DisplayTrainImage();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("未找到模板匹配模型!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    OnModelMatched(ProVision.MatchModel.MatchModelAssistant.ERR_NO_VALID_FILE);
                                }
                            }
                            else { MessageBox.Show("未找到模板位姿文件!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        }
                        else { MessageBox.Show("未找到模板搜索区域文件!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }                      
                    }
                    else { MessageBox.Show("未找到模板提取区域文件!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                }
                else { MessageBox.Show("未找到训练图像!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
        }

        /// <summary>
        /// 加载训练图像
        /// </summary>
        protected internal virtual void LoadTrainImage()
        {
            _ofdTrainImg.Multiselect = false;
            _ofdTrainImg.Filter = "图像文件(*.BMP,*.JPG,*.JPEG,*.TIF)|*.bmp;*.jpg;*.jpeg;*.tif";
            _ofdTrainImg.FilterIndex = 0;
            _ofdTrainImg.Title = "请选择一张图像文件";
            if (_ofdTrainImg.ShowDialog() == DialogResult.OK)
            {
                _locked = true;
                if (_instanceContour != null
                   && _instanceContour.IsInitialized())
                {
                    _instanceContour.Dispose();
                    _instanceContour = null;
                }

                trkbDisplayLevel.Enabled = false;
                trkbDisplayLevel.Value = 1;
                numUpDwnDisplayLevel.Enabled = false;
                numUpDwnDisplayLevel.Value = 1;
                _locked = false;

                this.chkbAlwaysFind.Checked = false;

                //设置选择的图像为训练图像
                if (!_matchModelAssistant.SetTrainImage(_ofdTrainImg.FileName)) //1-触发清空模板信息,更新模板轮廓
                    return;

                //显示窗口显示区域复位且显示内容清空
                HWndCtrller.ResetAll();                                          

                if (_matchModelAssistant.OnExternalModelID)
                    _modelContour = _matchModelAssistant.GetTrainInstanceContour(); //此时获取的是匹配模板的实例轮廓(已经加载过模板匹配模型)

                if (this.tabControlModel.SelectedIndex != 0)
                {
                    this.tabControlModel.SelectedIndex = 0;
                }                
                DisplayTrainImage();
                int startIndex = _ofdTrainImg.FileName.LastIndexOf(".");
                _imgExtention = _ofdTrainImg.FileName.Substring(startIndex + 1, (_ofdTrainImg.FileName.Length - startIndex - 1));
            }
        }

        /// <summary>
        /// 查找模板
        /// </summary>
        protected internal virtual void FindModel()
        {
            if (this.lstbTestImages.Items.Count != 0)
                ChangePromptionPanelColor(ProVision.InteractiveROI.HWndCtrller.PAINT_MODE_EXCLUDE_ROI);

            _matchModelAssistant.ModelSearchRegion = SearchRegion;
            _matchModelAssistant.DetectShapeModel();
            UpdateResultListBox();
        }

        /// <summary>
        /// 显示测试图像
        /// </summary>
        protected internal virtual void DisplayTestImage()
        {
            string filePath;
            if (this.lstbTestImages.Items.Count == 0)
            {
                ChangePromptionPanelColor(ProVision.InteractiveROI.HWndCtrller.PAINT_MODE_INCLUDE_ROI);
                SetModelGraphics();
                HWndCtrller.Repaint();
                OnModelMatched(ProVision.MatchModel.MatchModelAssistant.ERR_NO_TESTIMAGE);
                return;
            }

            filePath = (string)this.lstbTestImages.SelectedItem;
            _matchModelAssistant.GetTestImage(filePath);
            ChangePromptionPanelColor(ProVision.InteractiveROI.HWndCtrller.PAINT_MODE_EXCLUDE_ROI);

            if (_modelContour != null
                && _modelContour.IsInitialized()
                && this.chkbAlwaysFind.Checked)
            {
                _matchModelAssistant.DetectShapeModel();
            }
            else
            {
                SetInstanceGraphic();
                HWndCtrller.Repaint();
            }
        }

        /// <summary>
        /// 清空测试图像列表
        /// </summary>
        protected internal virtual void ClearTestImages()
        {
            if (this.lstbTestImages.Items.Count > 0)
            {
                _matchModelAssistant.ClearTestImage();
                this.lstbTestImages.Items.Clear();

                ChangePromptionPanelColor(ProVision.InteractiveROI.HWndCtrller.PAINT_MODE_EXCLUDE_ROI);

                if (_instanceContour != null
                && _instanceContour.IsInitialized())
                {
                    _instanceContour.Dispose();
                    _instanceContour = null;
                }

                SetInstanceGraphic(); 

                HWndCtrller.Repaint();
            }
        }

        /// <summary>
        /// 删除测试图像
        /// </summary>
        protected internal virtual void DeleteTestImage()
        {
            int count;
            if ((count = this.lstbTestImages.SelectedIndex) < 0)
                return;
            string filePath = (string)this.lstbTestImages.SelectedItem;

            if ((--count) < 0)
                count += 2;

            if (count < this.lstbTestImages.Items.Count)
            {
                this.lstbTestImages.SelectedIndex = count;
            }

            _matchModelAssistant.RemoveTestImage(filePath);
            this.lstbTestImages.Items.Remove(filePath);
        }

        /// <summary>
        /// 加载测试图像列表
        /// </summary>
        protected internal virtual void LoadTestImages()
        {
            _ofdTestImgs.Multiselect = true;
            _ofdTestImgs.Filter = "图像(*.BMP)|*.bmp|图像(*.JPG)|*.jpg|图像(*.JPEG)|*.jpeg|图像(*.TIF)|*.tif|所有|*.*";
            _ofdTestImgs.FilterIndex = 0;
            _ofdTestImgs.Title = "请选择一组图像(多选)";
            string[] filePaths;
            int count = 0;

            if (_ofdTestImgs.ShowDialog() == DialogResult.OK)
            {
                filePaths = _ofdTestImgs.FileNames;
                count = filePaths.Length;

                for (int i = 0; i < count; i++)
                {
                    if (_matchModelAssistant.AddTestImage(filePaths[i]))
                        this.lstbTestImages.Items.Add(filePaths[i]);
                }

                if (this.lstbTestImages.Items.Count != 0
                    && this.lstbTestImages.SelectedIndex < 0)
                {
                    _matchModelAssistant.GetTestImage((string)this.lstbTestImages.Items[0]); //此时已经在模型中更新了模型的当前测试图像,可以通过_matchShapeBasedModel.GetCurrentTestImage()获取
                    this.lstbTestImages.SelectedIndex = 0;
                    ChangePromptionPanelColor(ProVision.InteractiveROI.HWndCtrller.PAINT_MODE_EXCLUDE_ROI);//会更新UI中的当前测试图像

                    if (_instanceContour != null)
                    {
                        _instanceContour.Dispose();
                        _instanceContour = null;
                    }

                    SetInstanceGraphic();
                    HWndCtrller.Repaint();
                }
            }
        }

        /// <summary>
        /// 开启统计
        /// </summary>
        protected internal virtual void RunStatistic()
        {
            if (this.lstbTestImages.Items.Count != 0)
                ChangePromptionPanelColor(2);

            if (!this._timer.Enabled)
            {
                this.chkbAlwaysFind.Checked = false;
                _matchModelOpt = _matchModelOptResults;
                _matchModelOpt.Reset();//定时器未启动,则启动定时器并使评估优化过程的参数恢复到初始值[模板匹配模型的结果统计数据恢复到初始值]
                OnRecognizedAndStatisticed(ProVision.MatchModel.MatchModelOpt.UPDATE_INSP_RECOGRATE);
                OnRecognizedAndStatisticed(ProVision.MatchModel.MatchModelOpt.UPDATE_INSP_RESULTS);

                this.btnStatistic.Text = "停止统计";
                _matchModelAssistant.OnTimer = true;
                _timer.Enabled = true;

            }
            else//定时器启动,则停止计时器
            {
                this.btnStatistic.Text = "开启统计";
                _matchModelAssistant.OnTimer = false;
                _timer.Enabled = false;
            }
        }

        /// <summary>
        /// 开启优化
        /// </summary>
        protected internal virtual void RunOptimize()
        {
            if (this.lstbTestImages.Items.Count != 0)
                ChangePromptionPanelColor(ProVision.InteractiveROI.HWndCtrller.PAINT_MODE_EXCLUDE_ROI);

            if (!this._timer.Enabled)//定时器未启动,则启动定时器并使评估优化过程的参数恢复到初始值[模板匹配模型部分查找参数恢复到初始值]
            {
                this.chkbAlwaysFind.Checked = false;
                _matchModelOpt = _matchModelOptAccuracy;
                _matchModelOpt.Reset();

                OnRecognizedAndStatisticed(ProVision.MatchModel.MatchModelOpt.UPDATE_RECOG_UPDATED_VALS);
                OnRecognizedAndStatisticed(ProVision.MatchModel.MatchModelOpt.UPDATE_RECOG_OPTIMAL_VALS);
                OnRecognizedAndStatisticed(ProVision.MatchModel.MatchModelOpt.UPDATE_RECOG_STATISTICS_STATUS);

                this.btnOptimize.Text = "停止优化";
                _matchModelAssistant.OnTimer = true;
                _timer.Enabled = true;
            }
            else//定时器启动,则停止计时器
            {
                this.btnOptimize.Text = "开启优化";
                _matchModelAssistant.OnTimer = false;
                _timer.Enabled = false;
                //this.UpdateStatisticData(ProVision.Communal.MatchModelOptimization.RUN_FAILED);
            }
        }

        #endregion

        /// <summary>
        /// RadioButton选择状态改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void Rdbtn_CheckedChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.RadioButton rdbtn = sender as System.Windows.Forms.RadioButton;
            if(rdbtn!=null
                && rdbtn.Tag!=null
                && rdbtn.Checked)
            {
                switch(rdbtn.Tag.ToString())
                {
                    case "REGIONMODEL":
                        {
                            _matchModelAssistant.ModelType = ProVision.Communal.MatchModelType.ShapeRegionModel;
                            ChangeMetricItems(ProVision.Communal.MatchModelType.ShapeRegionModel);
                        }
                        break;
                    case "CONTOURMODEL":
                        {
                            _matchModelAssistant.ModelType = ProVision.Communal.MatchModelType.ShapeContourModel; 
                            ChangeMetricItems(ProVision.Communal.MatchModelType.ShapeContourModel);
                        }
                        break;                   
                    case "FREEDRAW":
                        {
                            if (_currentImage != null
                              && _currentImage.IsInitialized())
                            {
                                ResetExtractAndHWndCtrller();
                                HWndCtrller.AddHobjEntity(_currentImage);
                                HWndCtrller.Repaint();
                                this.chkbBrushOnOff.Checked = true;
                            }
                        }
                        break;
                    case "LINE":
                        {
                            if (_currentImage != null
                                && _currentImage.IsInitialized())
                            {                               
                                ResetExtractAndHWndCtrller();
                                HWndCtrller.AddHobjEntity(_currentImage);
                                ROICtrller.SetROIShape(new ProVision.InteractiveROI.ROILine());
                                HWndCtrller.Repaint();
                                this.chkbBrushOnOff.Checked = false;
                            }
                            else { System.Windows.Forms.MessageBox.Show("未加载图像!","警示信息",MessageBoxButtons.OK,MessageBoxIcon.Asterisk); }                           
                        }
                        break;
                    case "RECTANGLE1":
                        {
                            if (_currentImage != null
                               && _currentImage.IsInitialized())
                            {
                                ResetExtractAndHWndCtrller();
                                HWndCtrller.AddHobjEntity(_currentImage);
                                ROICtrller.SetROIShape(new ProVision.InteractiveROI.ROIRectangle1());
                                HWndCtrller.Repaint();
                                this.chkbBrushOnOff.Checked = false;
                            }
                            else { System.Windows.Forms.MessageBox.Show("未加载图像!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
                        }
                        break;
                    case "RECTANGLE2":
                        {
                            if (_currentImage != null
                               && _currentImage.IsInitialized())
                            {
                                ResetExtractAndHWndCtrller();
                                HWndCtrller.AddHobjEntity(_currentImage);
                                ROICtrller.SetROIShape(new ProVision.InteractiveROI.ROIRectangle2());
                                HWndCtrller.Repaint();
                                this.chkbBrushOnOff.Checked = false;
                            }
                            else { System.Windows.Forms.MessageBox.Show("未加载图像!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
                        }
                        break;
                    case "CIRCULARARC":
                        {
                            if (_currentImage != null
                               && _currentImage.IsInitialized())
                            {
                                ResetExtractAndHWndCtrller();
                                HWndCtrller.AddHobjEntity(_currentImage);
                                ROICtrller.SetROIShape(new ProVision.InteractiveROI.ROICircularArc());
                                HWndCtrller.Repaint();
                                this.chkbBrushOnOff.Checked = false;
                            }
                            else { System.Windows.Forms.MessageBox.Show("未加载图像!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
                        }
                        break;
                    case "CIRCLE":
                        {
                            if (_currentImage != null
                               && _currentImage.IsInitialized())
                            {
                                ResetExtractAndHWndCtrller();
                                HWndCtrller.AddHobjEntity(_currentImage);
                                ROICtrller.SetROIShape(new ProVision.InteractiveROI.ROICircle());
                                HWndCtrller.Repaint();
                                this.chkbBrushOnOff.Checked = false;
                            }
                            else { System.Windows.Forms.MessageBox.Show("未加载图像!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
                        }
                        break;
                    case "ANNULUS":
                        {
                            if (_currentImage != null
                               && _currentImage.IsInitialized())
                            {
                                ResetExtractAndHWndCtrller();
                                HWndCtrller.AddHobjEntity(_currentImage);
                                ROICtrller.SetROIShape(new ProVision.InteractiveROI.ROIAnnulus());
                                HWndCtrller.Repaint();
                                this.chkbBrushOnOff.Checked = false;
                            }
                            else { System.Windows.Forms.MessageBox.Show("未加载图像!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
                        }
                        break;
                    case "BRUSHSIZE":
                        break;
                    case "BRUSHSHAPE":
                        break;
                    case "NONE":
                        {
                            HWndCtrller.SetViewMode(ProVision.InteractiveROI.HWndCtrller.VIEW_MODE_NONE);
                            HWndCtrller.ResetWindow();
                            HWndCtrller.Repaint();
                        }
                        break;
                    case "MOVEIMAGE":
                        {
                            if (_currentImage != null
                              && _currentImage.IsInitialized())
                            {
                                HWndCtrller.SetViewMode(ProVision.InteractiveROI.HWndCtrller.VIEW_MODE_MOVE);
                            }
                            else
                            {
                                this.rdbtnNone.Checked = true;
                                System.Windows.Forms.MessageBox.Show("未加载图像!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                        break;
                    case "ZOOMIMAGE":
                        {
                            if (_currentImage != null
                              && _currentImage.IsInitialized())
                            {
                                HWndCtrller.SetViewMode(ProVision.InteractiveROI.HWndCtrller.VIEW_MODE_ZOOM);
                            }
                            else
                            {
                                this.rdbtnNone.Checked = true;
                                System.Windows.Forms.MessageBox.Show("未加载图像!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                        break;
                    case "MAGNIFYIMAGE":
                        {
                            if (_currentImage != null
                              && _currentImage.IsInitialized())
                            {
                                HWndCtrller.SetViewMode(ProVision.InteractiveROI.HWndCtrller.VIEW_MODE_MAGNIFY);
                            }
                            else
                            {
                                this.rdbtnNone.Checked = true;
                                System.Windows.Forms.MessageBox.Show("未加载图像!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                        break;
                    case "FINDSPECIFIEDNUM":
                        break;
                    case "FINDATLEASTONE":
                        break;
                    case "FINDMAXNUM":
                        break;
                    case "MODELEXTRACTREGION":
                        {
                            ROICtrller.ROIList.Clear();
                            this.rdbtnFreeDraw.Enabled = true;
                            this.chkbBrushSize.Enabled = true;
                            this.chkbBrushShape.Enabled = true;
                            this.chkbBrushOnOff.Enabled = true;
                            this.chkbFillErase.Enabled = true;
                        }
                        break;
                    case "MODELSEARCHRESGION":
                        {
                            ROICtrller.ROIList.Clear();
                            this.rdbtnFreeDraw.Enabled = false;
                            this.chkbBrushSize.Enabled = false;
                            this.chkbBrushShape.Enabled = false;
                            this.chkbBrushOnOff.Enabled = false;
                            this.chkbFillErase.Enabled = false;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 重置模板提取区域和窗体管理器
        /// </summary>
        private void ResetExtractAndHWndCtrller()
        {
            if(this.rdbtnModelExtract.Checked)
            {
                if (ExtractRegion != null
                && ExtractRegion.IsInitialized())
                    ExtractRegion.Dispose();
                ExtractRegion = null;
                HWndCtrller.ResetAll();
                HWndCtrller.ClearEntities();
            } 
        }

        private void ChangeMetricItems(ProVision.Communal.MatchModelType mt)
        {
            switch (mt)
            {
                case ProVision.Communal.MatchModelType.ShapeContourModel:
                    {
                        this.cmbMetric.Items.Clear();
                        this.cmbMetric.Items.Add("ignore_global_polarity");
                        this.cmbMetric.Items.Add("ignore_local_polarity");
                        this.cmbMetric.Items.Add("ignore_color_polarity");
                        this.cmbMetric.SelectedIndex = 0;
                        this.cmbMetric.Text = "ignore_global_polarity";
                    }
                    break;
                case ProVision.Communal.MatchModelType.ShapeRegionModel:
                    {
                        this.cmbMetric.Items.Clear();
                        this.cmbMetric.Items.Add("use_polarity");
                        this.cmbMetric.Items.Add("ignore_global_polarity");
                        this.cmbMetric.Items.Add("ignore_local_polarity");
                        this.cmbMetric.Items.Add("ignore_color_polarity");
                        this.cmbMetric.SelectedIndex = 0;
                        this.cmbMetric.Text = "use_polarity";
                    }
                    break;
                default: break;
            }
        }

        /// <summary>
        /// CheckBox选择状态改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void Chkb_CheckedChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.CheckBox chkb = sender as System.Windows.Forms.CheckBox;
            if (chkb != null
               && chkb.Tag != null)
            {
                switch(chkb.Tag.ToString())
                {
                    case "FILLERASE":
                        {
                            if (chkb.Checked)
                            {
                                _drawMode = ProVision.Communal.DrawMode.Erase;
                                chkb.Text = "擦除模式";
                            }
                            else
                            {
                                _drawMode = ProVision.Communal.DrawMode.Fill;
                                chkb.Text = "填充模式";
                            }
                        }
                        break;
                    case "ALWAYSFIND":
                        {

                        }
                        break;
               case "CONTRAST":
                   {
                        if (chkb.Checked)
                        {
                            if (_matchModelAssistant != null)
                                _matchModelAssistant.AddAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_CONTRAST);
                                this.numUpDwnContrast.Enabled = false;
                                this.trkbContrast.Enabled = false;
                                chkb.Text = "自动适配";
                        }
                        else
                        {
                            if (_matchModelAssistant != null)
                                _matchModelAssistant.RemoveAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_CONTRAST);
                                this.numUpDwnContrast.Enabled = true;
                                this.trkbContrast.Enabled = true;
                                chkb.Text = "手动调整";
                        }
                   }
                   break;
               case "SCALESTEP":
                   {
                        if (chkb.Checked)
                        {
                            if (_matchModelAssistant != null)
                                _matchModelAssistant.AddAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_SCALE_STEP);
                                this.numUpDwnScaleStep.Enabled = false;
                                this.trkbScaleStep.Enabled = false;
                                chkb.Text = "自动适配";
                        }
                        else
                        {
                            if (_matchModelAssistant != null)
                                _matchModelAssistant.RemoveAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_SCALE_STEP);
                                this.numUpDwnScaleStep.Enabled = true;
                                this.trkbScaleStep.Enabled = true;
                                chkb.Text = "手动调整";
                        }
                   }
                   break;
               case "ANGLESTEP":
                   {
                        if (chkb.Checked)
                        {
                            if (_matchModelAssistant != null)
                                _matchModelAssistant.AddAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_ANGLE_STEP);
                                this.numUpDwnAngleStep.Enabled = false;
                                this.trkbAngleStep.Enabled = false;
                                chkb.Text = "自动适配";
                        }
                        else
                        {
                            if (_matchModelAssistant != null)
                                _matchModelAssistant.RemoveAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_ANGLE_STEP);
                                this.numUpDwnAngleStep.Enabled = true;
                                this.trkbAngleStep.Enabled = true;
                                chkb.Text = "手动调整";
                        }
                   }
                   break;
               case "NUMLEVELS":
                   {
                        if (chkb.Checked)
                        {
                            if (_matchModelAssistant != null)
                                _matchModelAssistant.AddAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_NUM_LEVELS);
                                this.numUpDwnNumLevels.Enabled = false;
                                this.trkbNumLevels.Enabled = false;
                                chkb.Text = "自动适配";
                        }
                        else
                        {
                            if (_matchModelAssistant != null)
                                _matchModelAssistant.RemoveAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_NUM_LEVELS);
                                this.numUpDwnNumLevels.Enabled = true;
                                this.trkbNumLevels.Enabled = true;
                                chkb.Text = "手动调整";
                        }
                   }
                   break;
               case "OPTIMIZATION":
                   {
                        if (chkb.Checked)
                        {
                            if (_matchModelAssistant != null)
                                _matchModelAssistant.AddAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_OPTIMIZATION);
                                this.cmbOptimization.Enabled = false;
                                chkb.Text = "自动适配";
                        }
                        else
                        {
                            if (_matchModelAssistant != null)
                                _matchModelAssistant.RemoveAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_OPTIMIZATION);
                                this.cmbOptimization.Enabled = true;
                                chkb.Text = "手动调整";
                        }
                   }
                   break;
               case "MINCONTRAST":
                   {
                        if (chkb.Checked)
                        {
                            if (_matchModelAssistant != null)
                                _matchModelAssistant.AddAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_MIN_CONTRAST);
                                this.numUpDwnMinContrast.Enabled = false;
                                this.trkbMinContrast.Enabled = false;
                                chkb.Text = "自动适配";
                        }
                        else
                        {
                            if (_matchModelAssistant != null)
                                _matchModelAssistant.RemoveAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_MIN_CONTRAST);
                                this.numUpDwnMinContrast.Enabled = true;
                                this.trkbMinContrast.Enabled = true;
                                chkb.Text = "手动调整";
                        }
                   }
                   break;
                    case "BRUSHONOFF":
                        {
                            if (chkb.Checked)
                            {
                                chkb.Text = "笔刷启用"; 
                                //非自由绘制情形下,启用笔刷时则清空ROIList,即不再允许改变基本形状(移动或缩放)
                                if(!this.rdbtnFreeDraw.Checked)
                                {
                                    //ROIList非空(有基本形状),获取基本形状,作为当前的模板提取区域,然后清空ROIList
                                    if(ROICtrller.CalculateSyntheticalRegion())
                                    {
                                        ExtractRegion = ROICtrller.GetSyntheticalRegion();
                                        ROICtrller.ROIList.Clear();
                                    }
                                }                             
                            }
                            else
                            {
                                chkb.Text = "笔刷禁用";                               
                            }
                        }
                        break;
                    case "BRUSHSHAPE":
                        {
                            if (chkb.Checked)
                            {
                                chkb.Text = "方形笔刷";
                            }
                            else
                            {
                                chkb.Text = "圆形笔刷";
                            }
                        }
                        break;
                    case "BRUSHSIZE":
                        {
                            if (chkb.Checked)
                            {
                                chkb.Text = "大号笔刷";
                                _brushSize = 30;
                            }
                            else
                            {
                                chkb.Text = "小号笔刷";
                                _brushSize = 10;
                            }
                        }
                        break;
                   default:break;
                }
            }
        }

        /// <summary>
        /// ComboBox选择项改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void Cmb_SelectedIdxChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox cmb = sender as System.Windows.Forms.ComboBox;
            if(cmb!=null
                && cmb.Tag!=null)
            {
                switch(cmb.Tag.ToString())
                {
                    case "OPERATIONLEVEL":
                        {
                            OnOperationLevelChanged(cmb.SelectedIndex);
                        }
                        break;
                    case "METRIC":
                        {

                        }
                        break;
                    case "OPTIMIZATION":
                        {

                        }
                        break;
                    case "SUBPIXEL":
                        {

                        }
                        break;
                    case "RECOGRATEOPTION":
                        {

                        }
                        break;
                    default:break;
                }
            }
        }

        /// <summary>
        /// 操作等级改变
        /// </summary>
        /// <param name="selectedIndex"></param>
        private void OnOperationLevelChanged(int selectedIndex)
        {
           switch(selectedIndex)
            {
                case 0:
                    {
                        this.numUpDwnDisplayLevel.Enabled = false;
                        this.trkbDisplayLevel.Enabled = false;
                        this.numUpDwnDisplayLevel.Value = 1;

                        this.numUpDwnContrast.Enabled = false;
                        this.trkbContrast.Enabled = false;
                        this.chkbContrast.Checked = true;
                        this.chkbContrast.Enabled = false;
                        if(_matchModelAssistant!=null)
                            _matchModelAssistant.AddAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_MIN_CONTRAST);

                        this.numUpDwnMinScale.Enabled = false;                       
                        this.numUpDwnMinScale.Value = (decimal)100;
                        this.trkbMinScale.Enabled = false;
                        this.trkbMinScale.Value= 1;

                        this.numUpDwnMaxScale.Enabled = false;
                        this.numUpDwnMaxScale.Value = (decimal)100;
                        this.trkbMaxScale.Enabled = false;
                        this.trkbMaxScale.Value = 1;

                        this.numUpDwnScaleStep.Enabled = false;
                        this.trkbScaleStep.Enabled = false;
                        this.chkbScaleStep.Checked = true;
                        this.chkbScaleStep.Enabled = false;
                        if (_matchModelAssistant != null)
                            _matchModelAssistant.AddAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_SCALE_STEP);

                        this.numUpDwnStartAngle.Enabled = false;
                        this.numUpDwnStartAngle.Value = -(decimal)180;
                        this.trkbStartAngle.Enabled = false;
                        this.trkbStartAngle.Value = -180;

                        this.numUpDwnAngleExtent.Enabled = false;
                        this.numUpDwnAngleExtent.Value = (decimal)360;
                        this.trkbAngleExtent.Enabled = false;
                        this.trkbAngleExtent.Value = 360;
                      
                        this.numUpDwnAngleStep.Enabled = false;
                        this.trkbAngleStep.Enabled = false;
                        this.chkbAngleStep.Checked = true;
                        this.chkbAngleStep.Enabled = false;
                        if (_matchModelAssistant != null)
                            _matchModelAssistant.AddAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_ANGLE_STEP);

                        this.numUpDwnNumLevels.Enabled = false;
                        this.trkbNumLevels.Enabled = false;
                        this.chkbNumLevel.Checked = true;
                        this.chkbNumLevel.Enabled = false;
                        if (_matchModelAssistant != null)
                            _matchModelAssistant.AddAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_NUM_LEVELS);

                        this.cmbMetric.Enabled = false;
                        this.cmbMetric.SelectedIndex = 0;
                        this.cmbMetric.Text = "use_polarity";

                        this.cmbOptimization.Enabled = false;
                        this.chkbOption.Checked = true;
                        this.chkbOption.Enabled = false;
                        if (_matchModelAssistant != null)
                            _matchModelAssistant.AddAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_OPTIMIZATION);

                        this.numUpDwnMinContrast.Enabled = false;
                        this.trkbMinContrast.Enabled = false;
                        this.chkbMinContrast.Checked = true;
                        this.chkbMinContrast.Enabled = false;
                        if (_matchModelAssistant != null)
                            _matchModelAssistant.AddAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_MIN_CONTRAST);

                        this.numUpDwnGreediness.Enabled = false;
                        this.numUpDwnGreediness.Value = (decimal)80;
                        this.trkbGreediness.Enabled = false;
                        this.trkbGreediness.Value = 80;

                        this.numUpDwnMaxOverlap.Enabled = false;
                        this.numUpDwnMaxOverlap.Value = 50;
                        this.trkbMaxOverlap.Enabled = false;
                        this.trkbMaxOverlap.Value = 50;

                        this.cmbSubPixel.Enabled = false;
                        this.cmbSubPixel.SelectedIndex = 2;
                        this.cmbSubPixel.Text = "least_squares";

                        this.numUpDwnLastLevel.Enabled = false;
                        this.numUpDwnLastLevel.Value = (decimal)1;
                        this.trkbLastLevel.Enabled = false;
                        this.trkbLastLevel.Value = 1;

                        this.tbpInspectModel.Parent = null;
                        this.tbpOptimize.Parent = null;
                        this.tbpStatistic.Parent = null;
                    }
                    break;
                case 1:
                    {
                        this.numUpDwnDisplayLevel.Enabled = true;
                        this.trkbDisplayLevel.Enabled = true;
                        this.numUpDwnDisplayLevel.Value = 1;

                        this.numUpDwnContrast.Enabled = true;
                        this.trkbContrast.Enabled = true;
                        this.chkbContrast.Checked = false;
                        this.chkbContrast.Enabled = true;
                        if (_matchModelAssistant != null)
                            _matchModelAssistant.RemoveAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_MIN_CONTRAST);

                        this.numUpDwnMinScale.Enabled = true;
                        this.numUpDwnMinScale.Value = (decimal)100;
                        this.trkbMinScale.Enabled = true;
                        this.trkbMinScale.Value = 1;

                        this.numUpDwnMaxScale.Enabled = true;
                        this.numUpDwnMaxScale.Value = (decimal)100;
                        this.trkbMaxScale.Enabled = true;
                        this.trkbMaxScale.Value = 1;

                        this.numUpDwnScaleStep.Enabled = true;
                        this.trkbScaleStep.Enabled = true;
                        this.chkbScaleStep.Checked = false;
                        this.chkbScaleStep.Enabled = true;
                        if (_matchModelAssistant != null)
                            _matchModelAssistant.RemoveAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_SCALE_STEP);

                        this.numUpDwnStartAngle.Enabled = true;
                        this.numUpDwnStartAngle.Value = -(decimal)180;
                        this.trkbStartAngle.Enabled = true;
                        this.trkbStartAngle.Value = -180;

                        this.numUpDwnAngleExtent.Enabled = true;
                        this.numUpDwnAngleExtent.Value = (decimal)360;
                        this.trkbAngleExtent.Enabled = true;
                        this.trkbAngleExtent.Value = 360;

                        this.numUpDwnAngleStep.Enabled = true;
                        this.trkbAngleStep.Enabled = true;
                        this.chkbAngleStep.Checked = false;
                        this.chkbAngleStep.Enabled = true;
                        if (_matchModelAssistant != null)
                            _matchModelAssistant.RemoveAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_ANGLE_STEP);

                        this.numUpDwnNumLevels.Enabled = true;
                        this.trkbNumLevels.Enabled = true;
                        this.chkbNumLevel.Checked = false;
                        this.chkbNumLevel.Enabled = true;
                        if (_matchModelAssistant != null)
                            _matchModelAssistant.RemoveAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_NUM_LEVELS);

                        this.cmbMetric.Enabled = true;
                        this.cmbMetric.SelectedIndex = 0;
                        this.cmbMetric.Text = "use_polarity";

                        this.cmbOptimization.Enabled = true;
                        this.chkbOption.Checked = false;
                        this.chkbOption.Enabled = true;
                        if (_matchModelAssistant != null)
                            _matchModelAssistant.RemoveAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_OPTIMIZATION);

                        this.numUpDwnMinContrast.Enabled = true;
                        this.trkbMinContrast.Enabled = true;
                        this.chkbMinContrast.Checked = false;
                        this.chkbMinContrast.Enabled = true;
                        if (_matchModelAssistant != null)
                            _matchModelAssistant.RemoveAutoParameter(ProVision.Communal.MatchModelParameter.AUTO_MIN_CONTRAST);

                        this.numUpDwnGreediness.Enabled = true;
                        this.numUpDwnGreediness.Value = (decimal)80;
                        this.trkbGreediness.Enabled = true;
                        this.trkbGreediness.Value = 80;

                        this.numUpDwnMaxOverlap.Enabled = true;
                        this.numUpDwnMaxOverlap.Value = 50;
                        this.trkbMaxOverlap.Enabled = true;
                        this.trkbMaxOverlap.Value = 50;

                        this.cmbSubPixel.Enabled = true;
                        this.cmbSubPixel.SelectedIndex = 2;
                        this.cmbSubPixel.Text = "least_squares";

                        this.numUpDwnLastLevel.Enabled = true;
                        this.numUpDwnLastLevel.Value = (decimal)1;
                        this.trkbLastLevel.Enabled = true;
                        this.trkbLastLevel.Value = 1;

                        this.tbpInspectModel.Parent =this.tabControlModel;
                        this.tbpOptimize.Parent = this.tabControlModel;
                        this.tbpStatistic.Parent = this.tabControlModel;
                    }
                    break;
                default:break;
            }
        }

        /// <summary>
        /// NumberUpDown值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void NumUpDwn_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numUpDwn = sender as NumericUpDown;
            if (numUpDwn != null)
            {
                int val = (int)numUpDwn.Value;
                switch (numUpDwn.Tag.ToString())
                {
                    case "DISPLAYLEVEL":
                        {
                            trkbDisplayLevel.Value = val;
                            HWndCtrller.SetROIPaintMode(ProVision.InteractiveROI.HWndCtrller.PAINT_MODE_INCLUDE_ROI);

                            if (!_locked)
                                _matchModelAssistant.SetDisplayLevel(val);//显示指定的金字塔图层
                        }
                        break;
                    case "CONTRAST":
                        {
                            this.trkbContrast.Value = val;
                            this.trkbMinContrast.Maximum = val;
                            this.numUpDwnMinContrast.Maximum = val;

                            if (!_locked)
                                _matchModelAssistant.SetContrast(val);
                        }
                        break;
                    case "MINSCALE":
                        {
                            this.trkbMinScale.Value = val;
                            if (val > (int)this.numUpDwnMaxScale.Value)
                                this.numUpDwnMaxScale.Value = val;

                            if (!_locked)
                                _matchModelAssistant.SetMinScale((double)val / 100.0);
                        }
                        break;
                    case "MAXSCALE":
                        {
                            this.trkbMaxScale.Value = val;
                            if (val < (int)this.numUpDwnMinScale.Value)
                                this.numUpDwnMinScale.Value = val;

                            if (!_locked)
                                _matchModelAssistant.SetMaxScale((double)val / 100.0);
                        }
                        break;
                    case "SCALESTEP":
                        {
                            this.trkbScaleStep.Value = val;

                            if (!_locked)
                                _matchModelAssistant.SetScaleStep((double)val / 1000.0);
                        }
                        break;
                    case "STARTANGLE":
                        {
                            this.trkbStartAngle.Value = val;

                            if (!_locked)
                                _matchModelAssistant.SetStartAngle((double)val * System.Math.PI / 180.0);
                        }
                        break;
                    case "ANGLEEXTENT":
                        {
                            this.trkbAngleExtent.Value = val;

                            if (!this._locked)
                                this._matchModelAssistant.SetAngleExtent((double)val * System.Math.PI / 180.0);
                        }
                        break;
                    case "ANGLESTEP":
                        {
                            this.trkbAngleStep.Value = val;

                            if (!_locked)
                                _matchModelAssistant.SetAngleStep((double)val / 10.0 * System.Math.PI / 180.0);                          
                        }
                        break;
                    case "NUMLEVELS":
                        {
                            this.trkbNumLevels.Value = val;

                            if (!_locked)
                                _matchModelAssistant.SetNumLevels(val);                           
                        }
                        break;
                    case "MINCONTRAST":
                        {
                            this.trkbMinContrast.Value = val;

                            if (!_locked)
                                _matchModelAssistant.SetMinContrast(val);
                        }
                        break;
                    case "MINSCORE":
                        {
                            this.trkbMinScore.Value = val;
                            if (!_locked)
                            {
                                if (this.lstbTestImages.Items.Count != 0
                                    && this.chkbAlwaysFind.Checked)
                                    ChangePromptionPanelColor(ProVision.InteractiveROI.HWndCtrller.PAINT_MODE_EXCLUDE_ROI);
                                _matchModelAssistant.SetMinScore((double)val / 100.0);
                            }
                        }
                        break;
                    case "NUMTOMATCH":
                        {
                            this.trkbNumToMatch.Value = val;
                            this.numUpDwnMaxNumMatch.Value = val;
                            if (!this._locked)
                            {
                                if (this.lstbTestImages.Items.Count != 0
                                   && this.chkbAlwaysFind.Checked)
                                    ChangePromptionPanelColor(ProVision.InteractiveROI.HWndCtrller.PAINT_MODE_EXCLUDE_ROI);

                                if (val == 0)
                                {
                                    if (this.rdbtnMaxNum.Checked)
                                        this.rdbtnAtLeastOne.Checked = true;

                                    this.rdbtnMaxNum.Enabled = false;
                                }
                                else { this.rdbtnMaxNum.Enabled = true; }

                                _matchModelAssistant.SetNumToMatch(val);
                            }
                        }
                        break;
                    case "GREEDINESS":
                        {
                            this.trkbGreediness.Value = val;
                            if (!this._locked)
                            {
                                if (this.lstbTestImages.Items.Count != 0
                                  && this.chkbAlwaysFind.Checked)
                                    ChangePromptionPanelColor(ProVision.InteractiveROI.HWndCtrller.PAINT_MODE_EXCLUDE_ROI);

                                _matchModelAssistant.SetGreediness((double)val / 100.0);
                            }
                        }
                        break;
                    case "MAXOVERLAP":
                        {
                            this.trkbMaxOverlap.Value = val;
                            if (!this._locked)
                            {
                                if (this.lstbTestImages.Items.Count != 0
                                  && this.chkbAlwaysFind.Checked)
                                    ChangePromptionPanelColor(ProVision.InteractiveROI.HWndCtrller.PAINT_MODE_EXCLUDE_ROI);

                                _matchModelAssistant.SetMaxOverlap((double)val / 100.0);
                            }
                        }
                        break;
                    case "LASTLEVEL":
                        {
                            this.trkbLastLevel.Value = val;
                            if (!this._locked)
                            {
                                if (this.lstbTestImages.Items.Count != 0
                                  && this.chkbAlwaysFind.Checked)
                                    ChangePromptionPanelColor(ProVision.InteractiveROI.HWndCtrller.PAINT_MODE_EXCLUDE_ROI);

                                _matchModelAssistant.SetLastPyramidLevel(val);
                            }
                        }
                        break;
                    case "RECOGRATE":
                        {
                            this.trkbRecogRate.Value = val;
                            _matchModelAssistant.SetRecogRate(val);
                        }
                        break;
                    case "SPECIFIEDNUM":
                        {
                            if (!_locked)
                                _matchModelAssistant.SetRecogSpecifiedMatchNum(val);
                        }
                        break;
                    case "MAXNUMTOMATCH":
                        {
                            if (val != (int)this.numUpDwnNumToMatch.Value)
                                this.numUpDwnNumToMatch.Value = val;
                        }
                        break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// TrackBar拖动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void Trkb_Scroll(object sender, EventArgs e)
        {
            TrackBar trkb = sender as TrackBar;
            if (trkb != null
                && trkb.Tag!=null)
            {
                int val = (int)trkb.Value;
                switch (trkb.Tag.ToString())
                {
                    case "DISPLAYLEVEL":
                        {
                            this.numUpDwnDisplayLevel.Value = val;
                            this.numUpDwnDisplayLevel.Refresh();
                        }
                        break;
                    case "CONTRAST":
                        {
                            this.numUpDwnContrast.Value = val;
                            this.numUpDwnContrast.Refresh();
                        }
                        break;
                    case "MINSCALE":
                        {
                            this.numUpDwnMinScale.Value = val;
                            this.numUpDwnMinScale.Refresh();
                        }
                        break;
                    case "MAXSCALE":
                        {
                            this.numUpDwnMaxScale.Value = val;
                            this.numUpDwnMaxScale.Refresh();
                        }
                        break;
                    case "SCALESTEP":
                        {
                            this.numUpDwnScaleStep.Value = val;
                            this.numUpDwnScaleStep.Refresh();
                        }
                        break;
                    case "STARTANGLE":
                        {
                            this.numUpDwnStartAngle.Value = val;
                            this.numUpDwnStartAngle.Refresh();
                        }
                        break;
                    case "ANGLEEXTENT":
                        {
                            this.numUpDwnAngleExtent.Value = val;
                            this.numUpDwnAngleExtent.Refresh();
                        }
                        break;
                    case "ANGLESTEP":
                        {
                            this.numUpDwnAngleStep.Value = val;
                            this.numUpDwnAngleStep.Refresh();
                        }
                        break;
                    case "NUMLEVELS":
                        {
                            this.numUpDwnNumLevels.Value = val;
                            this.numUpDwnNumLevels.Refresh();
                        }
                        break;
                    case "MINCONTRAST":
                        {
                            this.numUpDwnMinContrast.Value = val;
                            this.numUpDwnMinContrast.Refresh();
                        }
                        break;
                    case "MINSCORE":
                        {
                            this.numUpDwnMinScore.Value = val;
                            this.numUpDwnMinScore.Refresh();
                        }
                        break;
                    case "NUMTOMATCH":
                        {
                            this.numUpDwnNumToMatch.Value = val;
                            this.numUpDwnNumToMatch.Refresh();
                        }
                        break;
                    case "GREEDINESS":
                        {
                            this.numUpDwnGreediness.Value = val;
                            this.numUpDwnGreediness.Refresh();
                        }
                        break;
                    case "MAXOVERLAP":
                        {
                            this.numUpDwnMaxOverlap.Value = val;
                            this.numUpDwnMaxOverlap.Refresh();
                        }
                        break;
                    case "LASTLEVEL":
                        {
                            this.numUpDwnLastLevel.Value = val;
                            this.numUpDwnLastLevel.Refresh();
                        }
                        break;
                    case "RECOGRATE":
                        {
                            this.numUpDwnRecogRate.Value = val;
                            this.numUpDwnRecogRate.Refresh();
                        }
                        break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// ListBox选择项改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void Lstb_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayTestImage();
            UpdateResultListBox();
        }

        /// <summary>
        /// DataGridView行键入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void Dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            UpdateActiveResultObject(e.RowIndex);
        }

        protected internal virtual void UpdateActiveResultObject(int rowIdx)
        {
            _activeInsContourIdx = rowIdx;
            SetInstanceGraphic();
            HWndCtrller.Repaint();
        }

        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void Timer_Tick(object sender, EventArgs e)
        {
            bool rt;
            rt = _matchModelOpt.ExecuteStep();
            if (!rt)
            {
                this._timer.Enabled = false;
                _matchModelOpt.Stop();
                _matchModelAssistant.OnTimer = false;

                this.btnOptimize.Text = "开启优化";
                this.btnStatistic.Text = "开启统计";
            }
        }     

        /// <summary>
        /// 更新匹配结果列表
        /// </summary>
        protected internal virtual void UpdateResultListBox()
        {
            if (_matchModelAssistant != null
               && _matchModelAssistant.Result != null)
            {
                int count = _matchModelAssistant.Result.MatchCount;
                if (this.dgvMatchResult != null)
                {
                    this.dgvMatchResult.Rows.Clear();
                    this.dgvMatchResult.DefaultCellStyle.Font = new Font("宋体", 14);
                }

                if (count > 0)
                {
                    this.dgvMatchResult.Rows.Add(count);
                    for (int i = 0; i < count; i++)
                    {
                        this.dgvMatchResult.Rows[i].Cells[0].Value = (i + 1).ToString("00");
                        this.dgvMatchResult.Rows[i].Cells[1].Value = System.Math.Round(_matchModelAssistant.Result.Score[i].D, 2).ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 保存模板到指定目录下
        /// </summary>
        /// <param name="modelDirectory"></param>
        /// <returns></returns>
        public bool SaveModel(string modelDirectory)
        {
            bool rt = false;
            try
            {
                if (_currentImage != null
               && _currentImage.IsInitialized())
                {
                    if (ExtractRegion != null
                        && ExtractRegion.IsInitialized())
                    {
                        if (_matchModelAssistant != null
                            && _matchModelAssistant.ModelID.TupleNotEqual(new HalconDotNet.HTuple(-1)))
                        {
                            if (SearchRegion != null
                               && SearchRegion.IsInitialized())
                            {
                                if (!System.IO.Directory.Exists(modelDirectory))
                                    System.IO.Directory.CreateDirectory(modelDirectory);
                                string[] fileNames = System.IO.Directory.GetFiles(modelDirectory);
                                for (int i = 0; i < fileNames.Count(); i++)
                                {
                                    System.IO.File.Delete(fileNames[i]);
                                }

                                if (_imgExtention == "tif")
                                    _imgExtention = "tiff";
                                HalconDotNet.HOperatorSet.WriteImage(_currentImage, _imgExtention, new HalconDotNet.HTuple(0), modelDirectory + "\\TrainImage." + _imgExtention);
                                HalconDotNet.HOperatorSet.WriteRegion(ExtractRegion, modelDirectory + "\\ExtrackRegion.hobj");
                                HalconDotNet.HOperatorSet.WriteRegion(SearchRegion, modelDirectory + "\\SearchRegion.hobj");
                                ModelID = _matchModelAssistant.ModelID;
                                ModelPose = _matchModelAssistant.ModelPose;
                                HalconDotNet.HOperatorSet.WriteTuple(ModelPose, modelDirectory + "\\ModelPose.tup");
                                _matchModelAssistant.SaveShapeModel(modelDirectory + "\\ModelFile.shm");
                                rt = true;
                            }
                            else { MessageBox.Show("未创建模板搜索区域!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        }
                        else { MessageBox.Show("未创建模板匹配模型!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    }
                    else { MessageBox.Show("未创建模板提取区域!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                }
                else { MessageBox.Show("未加载训练图像!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

            }
#pragma warning disable CS0168 // The variable 'hex' is declared but never used
            catch (HalconDotNet.HalconException hex) { }
#pragma warning restore CS0168 // The variable 'hex' is declared but never used
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (System.Exception ex) { }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used

            return rt;
        }

        /// <summary>
        /// 加载指定目录下的模板文件
        /// </summary>
        /// <param name="modelDirectory"></param>
        /// <returns></returns>
        public bool LoadModel(string modelDirectory)
        {
            bool rt = false;
            try
            {
                if (System.IO.Directory.Exists(modelDirectory))
                {
                    HalconDotNet.HTuple exist;
                    string extension = string.Empty;
                    string name = string.Empty;
                    int idx;
                    System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(modelDirectory);
                    if (dirInfo.GetFiles().Length + dirInfo.GetDirectories().Length == 0)
                    {
                        MessageBox.Show("文件夹下无文件!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        System.IO.FileInfo[] windImgFiles = dirInfo.GetFiles();
                        foreach (System.IO.FileInfo itm in windImgFiles)
                        {
                            idx = itm.Name.LastIndexOf(".");
                            name = itm.Name.Substring(0, idx);
                            if (name == "TrainImage")
                            {
                                extension = itm.Extension;
                                break;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(extension))
                    {
                        HalconDotNet.HOperatorSet.FileExists(modelDirectory + "\\ExtrackRegion.hobj", out exist);
                        if ((int)exist != 0)
                        {
                            HalconDotNet.HOperatorSet.FileExists(modelDirectory + "\\SearchRegion.hobj", out exist);
                            if ((int)exist != 0)
                            {
                                HalconDotNet.HOperatorSet.FileExists(modelDirectory + "\\ModelPose.tup", out exist);
                                if ((int)exist != 0)
                                {
                                    HalconDotNet.HOperatorSet.FileExists(modelDirectory + "\\ModelFile.shm", out exist);
                                    if ((int)exist != 0)
                                    {
                                        _matchModelAssistant.Reset();
                                        this.chkbAlwaysFind.Checked = false;

                                        HalconDotNet.HObject Imgtmp, extmp; HalconDotNet.HTuple mdlP;
                                        HalconDotNet.HOperatorSet.ReadImage(out Imgtmp, modelDirectory + "\\TrainImage" + extension);
                                        _matchModelAssistant.SetTrainImage(Imgtmp);
                                        HalconDotNet.HOperatorSet.ReadRegion(out extmp, modelDirectory + "\\ExtrackRegion.hobj");
                                        ExtractRegion = extmp;
                                        extmp.Dispose();
                                        HalconDotNet.HOperatorSet.ReadRegion(out extmp, modelDirectory + "\\SearchRegion.hobj");
                                        SearchRegion = extmp;
                                        HalconDotNet.HOperatorSet.ReadTuple(modelDirectory + "\\ModelPose.tup", out mdlP);
                                        ModelPose = mdlP;
                                        _matchModelAssistant.ModelPose = ModelPose;

                                        if (ModelPose[3].I == 0)
                                            this.rdbtnRegionShape.Checked = true;
                                        else if (ModelPose[3].I == 1)
                                            this.rdbtnContourShape.Checked = true;

                                        ROICtrller.Reset();
                                        if (_instanceContour != null
                                        && _instanceContour.IsInitialized())
                                        {
                                            _instanceContour.Dispose();
                                            _instanceContour = null;
                                        }

                                        //加载模板匹配模型文件
                                        if (_matchModelAssistant.LoadShapeModel(modelDirectory + "\\ModelFile.shm"))
                                        {
                                            ModelID = _matchModelAssistant.ModelID;
                                            _modelContour = _matchModelAssistant.GetTrainInstanceContour(); //此时获取的是匹配模板的实例轮廓(已经加载过模板匹配模型)

                                            if (this.tabControlModel.SelectedIndex != 0)
                                            {
                                                this.tabControlModel.SelectedIndex = 0;
                                            }

                                            DisplayTrainImage();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("未找到模板匹配模型!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        OnModelMatched(ProVision.MatchModel.MatchModelAssistant.ERR_NO_VALID_FILE);
                                    }
                                }
                                else { MessageBox.Show("未找到模板位姿文件!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                            }
                            else { MessageBox.Show("未找到模板搜索区域文件!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }                           
                        }
                        else { MessageBox.Show("未找到模板提取区域文件!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    }
                    else { MessageBox.Show("未找到训练图像!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                }
                else { MessageBox.Show("指定目录不存在!", "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            catch (HalconDotNet.HalconException hex) { }
            catch (System.Exception ex) { }
            return rt;
        }

        public ProVision.MatchModel.MatchModelAssistant MatchAssistant
        {
            get { return _matchModelAssistant; }
        }

    }
}
