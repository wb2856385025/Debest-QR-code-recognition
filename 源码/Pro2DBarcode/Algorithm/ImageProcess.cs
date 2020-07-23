using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ImageProcess_CableCoreIdentify
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Algorithm
 * File      Name：       ImageProcess_CableCoreIdentify
 * Creating  Time：       1/15/2020 6:47:48 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Algorithm
{

    /// <summary>
    /// 二维码识别
    /// </summary>
    public class ImageProcess_InspectDataCode2D : ProVision.Communal.ImageProcess
    {
        #region 字段和属性:算法变量以及算法参数
        private HalconDotNet.HTuple _codeType, _timeOut, _toBeFoundNumber, _genParaNamesForCreate, _genParaValuesForCreate,_colorForNotFound,_colorForFound;

        private HalconDotNet.HTuple _dataCodeHandle;

        private HalconDotNet.HObject _symbolXLDs;
        public HalconDotNet.HTuple DecodeStrings,DecodeTime;
        /// <summary>
        /// 算法参数
        /// </summary>
        public Pro2DBarcode.Config.VisionPara.VisionPara2DBarcode Para2DBarcode { set; get; }

        /// <summary>
        /// 图像处理结果是否达标标记
        /// true-达到设计要求,false-未达到设计要求
        /// </summary>
        public bool ResultOK { get; private set; }

        /// <summary>
        /// 是否汉语语言标记
        /// </summary>
        public bool IsChinese { set; get; }

        /// <summary>
        /// 检测区域
        /// </summary>
        private HalconDotNet.HObject _inspectArea;

        public int InspectAreaCount { private set; get; }

        #endregion

        /// <summary>
        /// 根据算法参数更新算法变量
        /// </summary>
        protected override void UpdateParameter()
        {
            if (Para2DBarcode != null)
            {
                //更新字段和属性变量 
                _codeType = new HalconDotNet.HTuple(Para2DBarcode.CodeType);
                _timeOut = new HalconDotNet.HTuple(Para2DBarcode.TimeOut);
                _toBeFoundNumber = new HalconDotNet.HTuple(Para2DBarcode.ToBeFoundNumber);
                string[] paraNames = Para2DBarcode.GenParameterNamesForCreate;
                string[] paraValues = Para2DBarcode.GenParameterValuesForCreate;
                if (paraNames.Length == paraValues.Length)
                {
                   
                    for (int i = 0; i < paraNames.Length; i++)
                    {
                        if(i==0)
                        {
                            _genParaNamesForCreate = new HalconDotNet.HTuple(paraNames[0]);
                            _genParaValuesForCreate = new HalconDotNet.HTuple(paraValues[0]);
                        }
                        else
                        {
                            _genParaNamesForCreate = _genParaNamesForCreate.TupleConcat(new HalconDotNet.HTuple(paraNames[i]));
                            _genParaValuesForCreate = _genParaValuesForCreate.TupleConcat(new HalconDotNet.HTuple(paraValues[i]));
                        }
                      
                    }
                }
                _colorForNotFound = new HalconDotNet.HTuple(Para2DBarcode.ColorStrForResultNotFound);
                _colorForFound = new HalconDotNet.HTuple(Para2DBarcode.ColorStrForResultFound);
            }
        }

        /// <summary>
        /// 加载算法文件
        /// </summary>
        /// <returns></returns>
        protected override bool LoadAlgorithmFile()
        {
            bool rt = false;
            try
            {
                if (!string.IsNullOrEmpty(Para2DBarcode.PathForInspectArea))
                {
                    if (_inspectArea != null
                    && _inspectArea.IsInitialized())
                        _inspectArea.Dispose();
                    HalconDotNet.HOperatorSet.ReadRegion(out _inspectArea, new HalconDotNet.HTuple(Para2DBarcode.PathForInspectArea));
                    rt = true;
                }
            }
            catch (HalconDotNet.HalconException hex) { }
            catch (System.Exception ex) { }
            finally { }
            return rt;
        }

        public override void InitProcess()
        {
            UpdateParameter();
            _IsLaunchAllowed = LoadAlgorithmFile();
            Pro2DBarcode.Manager.UIManager.Instance.SetDraw(_HwndIndex, new HalconDotNet.HTuple("margin"));
        }

        /// <summary>
        /// 图像处理
        /// </summary>
        /// <param name="hobjRaw"></param>
        /// <returns></returns>
        public override bool Process(HalconDotNet.HObject hobjRaw)
        {
            bool rt = false;
            ResultOK = false;
            DecodeStrings = new HalconDotNet.HTuple();
            if (_symbolXLDs != null
                && _symbolXLDs.IsInitialized())
            {
                _symbolXLDs.Dispose();
                _symbolXLDs = null;
            }

            //显示图像到指定窗口
            Pro2DBarcode.Manager.UIManager.Instance.DisplayObject(_HwndIndex, hobjRaw);
            try
            {
                if (_IsLaunchAllowed)
                {
                    _RawImage = hobjRaw;

                    //0-根据算法需要的参数是否有效，如图形变量的检测区域等，预设显示参数并显示
                    if (_inspectArea != null
                        && _inspectArea.IsInitialized())
                    {
                        //图形变量:检测区域有效,设置其显示颜色
                        Pro2DBarcode.Manager.UIManager.Instance.SetDraw(_HwndIndex, new HalconDotNet.HTuple("margin"));
                        HalconDotNet.HTuple color = (_colorForNotFound.TupleEqual(new HalconDotNet.HTuple())) ? new HalconDotNet.HTuple("red") : _colorForNotFound;
                        Pro2DBarcode.Manager.UIManager.Instance.SetColor(_HwndIndex, color);
                        Pro2DBarcode.Manager.UIManager.Instance.DisplayObject(_HwndIndex, _inspectArea);
                        InspectAreaCount = _inspectArea.CountObj();
                    }

                    if (_IsEnableAlgorithm)
                    {
                        //1-算法需要的参数是否有效，进行图像处理
                        if (_inspectArea != null
                            && _inspectArea.IsInitialized())
                        {
                            //2-进行算法处理,更新本函数返回值
                            rt = Inspect2DBarcode(_RawImage,_inspectArea,_codeType,_genParaNamesForCreate,_genParaValuesForCreate,_timeOut,_toBeFoundNumber,out _symbolXLDs,out DecodeStrings,out DecodeTime);
                        }


                        if (rt)
                        {
                            //3-更新图像处理结果标记:根据是否达标更新图像处理结果标记
                            if (_symbolXLDs != null
                                && _symbolXLDs.IsInitialized())
                            {
                                //4-显示图像处理结果图形变量，信息变量
                              
                                HalconDotNet.HObject tmpTrackAreaRegion = new HalconDotNet.HObject();
                                HalconDotNet.HTuple row, col, area,colr;
                                string showStr;int numOK = 0;
                                for (int i=1;i<= InspectAreaCount;i++)
                                {
                                    tmpTrackAreaRegion.Dispose();
                                    HalconDotNet.HOperatorSet.SelectObj(_inspectArea, out tmpTrackAreaRegion, i);

                                    //未识别到二维码的ROI
                                    if (DecodeStrings[i - 1].S == "")
                                    {
                                        colr = _colorForNotFound;                                      
                                        showStr = "NG";
                                    }
                                    else
                                    {
                                        colr = _colorForFound;
                                        showStr = "OK";
                                        numOK++;
                                    }

                                    Pro2DBarcode.Manager.UIManager.Instance.SetColor(_HwndIndex, colr);
                                    Pro2DBarcode.Manager.UIManager.Instance.DisplayObject(_HwndIndex, tmpTrackAreaRegion);                                  
                                    HalconDotNet.HOperatorSet.AreaCenter(tmpTrackAreaRegion, out area, out row, out col );
                                    Pro2DBarcode.Manager.UIManager.Instance.DispMessage(_HwndIndex,
                                    "#["+i+"]-"+ showStr, new HalconDotNet.HTuple("image"), row , col,
                                    colr, new HalconDotNet.HTuple("false"));

                                    System.Threading.Thread.Sleep(1);
                                }

                                ResultOK = numOK== InspectAreaCount;
                            }
                        }
                        else
                        {
                            //显示算法处理异常信息
                            string[] msgArr = ErrorMessage.Split(',');
                            for (int i = 0; i < msgArr.Length; i++)
                            {
                                Pro2DBarcode.Manager.UIManager.Instance.DispMessage(_HwndIndex, msgArr[i], new HalconDotNet.HTuple("image"), new HalconDotNet.HTuple(20 + 100 * i), new HalconDotNet.HTuple(20), new HalconDotNet.HTuple("red"), new HalconDotNet.HTuple("true"));
                            }
                        }
                    }
                }
                else
                {
                    string txt = IsChinese ? "参数文件丢失,无法识别!" : "Process denied , no parameter file found !";
                    Pro2DBarcode.Manager.UIManager.Instance.DispMessage(_HwndIndex, txt, new HalconDotNet.HTuple("image"), new HalconDotNet.HTuple(100), new HalconDotNet.HTuple(20), new HalconDotNet.HTuple("red"), new HalconDotNet.HTuple("true"));
                }
            }
            catch (HalconDotNet.HalconException hex) { }
            catch (System.Exception ex) { }
            finally { }
            return rt;
        }
        private bool Inspect2DBarcode(HalconDotNet.HObject rawImg, HalconDotNet.HObject trackArea, HalconDotNet.HTuple codeType, HalconDotNet.HTuple genParaNames, HalconDotNet.HTuple genParaValues, HalconDotNet.HTuple timeOut,HalconDotNet.HTuple toBeFoundNum, out HalconDotNet.HObject symbolXLDs, out HalconDotNet.HTuple decodeStrings, out HalconDotNet.HTuple decodeTime)
        {
            bool rt = false;
            HalconDotNet.HOperatorSet.GenEmptyObj(out symbolXLDs);
            decodeStrings = new HalconDotNet.HTuple();
            decodeTime = new HalconDotNet.HTuple(0);

            HalconDotNet.HObject imgReduce = new HalconDotNet.HObject();
            try
            {
                if (rawImg != null
                    && rawImg.IsInitialized())
                {
                    if (trackArea != null
                        && trackArea.IsInitialized())
                    {
                        if(_dataCodeHandle==null)
                        {
                            HalconDotNet.HTuple cdType = (codeType.TupleEqual(new HalconDotNet.HTuple())) ? new HalconDotNet.HTuple("Data Matrix ECC 200") : codeType;
                            HalconDotNet.HTuple gpNames = (genParaNames.TupleEqual(new HalconDotNet.HTuple())) ? new HalconDotNet.HTuple("default_parameters") : genParaNames;
                            HalconDotNet.HTuple gpValues = (genParaValues.TupleEqual(new HalconDotNet.HTuple())) ? new HalconDotNet.HTuple("maximum_recognition") : genParaValues;
                            HalconDotNet.HOperatorSet.CreateDataCode2dModel(codeType, gpNames, gpValues, out _dataCodeHandle);
                        }
                        HalconDotNet.HTuple tmOut = (timeOut.TupleEqual(new HalconDotNet.HTuple())) ? new HalconDotNet.HTuple(500) : timeOut;
                        HalconDotNet.HTuple tbfNum = (toBeFoundNum.TupleEqual(new HalconDotNet.HTuple())) ? new HalconDotNet.HTuple(1) : toBeFoundNum;
                        HalconDotNet.HOperatorSet.SetDataCode2dParam(_dataCodeHandle, new HalconDotNet.HTuple("timeout"), tmOut);
                        HalconDotNet.HTuple tStart, tEnd, resultHandle,decodeStr;
                        HalconDotNet.HOperatorSet.CountSeconds(out tStart);
                        int areaCount= trackArea.CountObj();
                        int symbolCount = 0;
                        HalconDotNet.HObject tmpRegion = new HalconDotNet.HObject();
                        HalconDotNet.HObject tmpSymbolXLDs = new HalconDotNet.HObject();
                        for (int i=1;i<=areaCount;i++)
                        {
                            tmpRegion.Dispose();
                            HalconDotNet.HOperatorSet.SelectObj(_inspectArea, out tmpRegion, i);
                            imgReduce.Dispose();
                            HalconDotNet.HOperatorSet.ReduceDomain(rawImg, tmpRegion, out imgReduce);

                            tmpSymbolXLDs.Dispose();
                            HalconDotNet.HOperatorSet.FindDataCode2d(imgReduce, out tmpSymbolXLDs, _dataCodeHandle, new HalconDotNet.HTuple("stop_after_result_num"), tbfNum, out resultHandle, out decodeStr);
                            symbolCount = tmpSymbolXLDs.CountObj();
                            //识别到二维码
                            if (symbolCount > 0)
                            {
                                HalconDotNet.HOperatorSet.ConcatObj(symbolXLDs, tmpSymbolXLDs, out symbolXLDs);
                                decodeStrings = decodeStrings.TupleConcat(decodeStr);
                            }
                            else {
                                HalconDotNet.HObject emptyObj;
                                HalconDotNet.HOperatorSet.GenEmptyObj(out emptyObj);
                                HalconDotNet.HOperatorSet.ConcatObj(symbolXLDs, emptyObj, out symbolXLDs);
                                decodeStrings = decodeStrings.TupleConcat("");
                                emptyObj.Dispose();
                            }
                        }
                        tmpRegion.Dispose();
                        tmpSymbolXLDs.Dispose();
                        HalconDotNet.HOperatorSet.CountSeconds(out tEnd);
                        decodeTime = (tEnd - tStart);
                        rt = true;
                    }
                    else { ErrorMessage = IsChinese ? "检测区域无效" : "Invalid track area !"; }
                }
                else { ErrorMessage = IsChinese ? "图像无效" : "Invalid image !"; }
            }
            catch (HalconDotNet.HalconException hex)
            {
                ErrorMessage = (IsChinese ? "识别异常!异常描述:," : "Process Error!Description:,") + hex.GetErrorMessage();
            }
            finally {  }
            return rt;
        }

        public override void SetCameraIndex(int camIdx)
        {
            _CameraIndex = camIdx;
        }

        /// <summary>
        /// 设置图像启用算法标记
        /// [true--启用算法,false--不启用算法]
        /// </summary>
        /// <param name="enable"></param>
        public override void SetEnableAlgorithm(bool enable)
        {
            _IsEnableAlgorithm = enable;
        }

        public override void SetHwndIndex(int wndIdx)
        {
            _HwndIndex = wndIdx;
        }
    }
}
