using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       MatchModelOptResults
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProVision.MatchModel
 * File      Name：       MatchModelOptResults
 * Creating  Time：       10/9/2019 12:40:34 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProVision.MatchModel
{
    /// <summary>
    /// To determine the performance of a shape-based model, given
    /// a parameter setup for model creation and detection, 
    /// this class applies a model detection for the whole set
    /// of test images loaded and computes an all-over statistics.
    /// </summary>
    public class MatchModelOptResults : MatchModelOpt
    {
        // Recognize -group
        private int _probMathcNum;     //可能的匹配数
        private bool _modelFound;      //是否找到模板
        private int _specifiedMatchNum;//指定的匹配数
        private int _maxMatchNum;      //最大匹配数
        private int _foundMatchNum;    //实际匹配数

        private int _imageWithOneMatchNum;        //至少匹配一个模板模式计数
        private int _imageWithSpecifiedMatchNum;  //指定匹配数个模板模式计数
        private int _imageWithMaxMatchNum;        //最大匹配数个模板模式计数


        // Statistic -group
        private double _scoreMin, _scoreMax;
        private double _elapseTimeMin, _elapseTimeMax;
        private double _rowMin, _rowMax;
        private double _colMin, _colMax;
        private double _angleMin, _angleMax;
        private double _scaleRowMin, _scaleRowMax;
        private double _scaleColMin, _scaleColMax;

        public MatchModelOptResults(MatchModel.MatchModelAssistant assistant)
        {
            MatchAssistant = assistant;
            Parameter = assistant.Parameter;
            RecoginzedAndStatisticedDel = new RecognizedAndStatisticedDelegate(Dummy);

            Reset();
        }

        /// <summary>
        /// With each execution step the shape-based model is searched in
        /// the current test image. The detection result is then compared 
        /// with the previous results and the overall statistics is adjusted.
        /// </summary>
        public override bool ExecuteStep()
        {
            string fileKey, imgNumStr;
            int val, i;
            string matchFormatStr;
            int actualMatchNum;
            int specifiedMatchNum;
            int maxMatchNum;
            double score, time, row, col, angleA, angleB, scaleR, scaleC;

            //----------------------第一步:检测是否有测试图像-------------------------//
            if (!Iterator.MoveNext()) //未有测试图像,停止优化
                return false;

            fileKey = (string)Iterator.Current;
            MatchAssistant.SetTestImage(fileKey);

            if (!(OptSuccess = MatchAssistant.DetectShapeModel())) ////查找匹配模板的实例失败,停止统计
                return false;



            Result = MatchAssistant.Result;
            actualMatchNum = Result.MatchCount;

            // --------------determine recognition rate ----------------
            specifiedMatchNum = Parameter.RecogSpecifiedMatchNum;
            maxMatchNum = Parameter.NumToMatch;

            _specifiedMatchNum += specifiedMatchNum;
            _maxMatchNum += maxMatchNum;
            _foundMatchNum += actualMatchNum;

            if (actualMatchNum > 0) //若找到匹配模板的实例数大于零,则'至少匹配1个数量的实例'的标记数量更新
                _imageWithOneMatchNum++;

            if (actualMatchNum >= specifiedMatchNum) //若找到匹配模板的实例数大于预期(指定)的数量,则'匹配指定数量的实例'的标记数量更新
                _imageWithSpecifiedMatchNum++;

            if (actualMatchNum == maxMatchNum)  //若找到匹配模板的实例数等于'可能的(最大)数量',则'匹配最大数量的实例'的标记数量更新
                _imageWithMaxMatchNum++;

            CurrentImageIndex++;          //图像索引递增
            InspectTabRecogRateData[2] = "-";
            InspectTabRecogRateData[3] = "-";
            InspectTabRecogRateData[4] = "-";

            imgNumStr = " of " + CurrentImageIndex + "图像)";

            val = _imageWithOneMatchNum; //'识别率'% ('至少匹配一个模板模式计数' of '当前图像索引' 图像 )
            InspectTabRecogRateData[0] = System.Math.Round(100.0 * ((double)val / CurrentImageIndex), 2) + " % (" + val + imgNumStr;

            val = _imageWithSpecifiedMatchNum; //'识别率'% ('指定匹配数个模板模式计数' of '当前图像索引' 图像 )
            InspectTabRecogRateData[1] = System.Math.Round(100.0 * ((double)val / CurrentImageIndex), 2) + " % (" + val + imgNumStr;

            if (_maxMatchNum > 0)
            {
                matchFormatStr = " of " + _maxMatchNum + "模板)";
                val = _imageWithMaxMatchNum;  //'识别率'% ('最大匹配数个模板模式计数' of '当前图像索引' 图像 )
                this.InspectTabRecogRateData[2] = System.Math.Round(100.0 * ((double)val / CurrentImageIndex), 2) + " % (" + val + imgNumStr;

                val = _foundMatchNum;        //'识别率'% ('实际匹配模板个数' of '可能匹配个数' 模板 )
                InspectTabRecogRateData[3] = System.Math.Round(100.0 * ((double)val / _maxMatchNum), 2) + " % (" + val + matchFormatStr;
            }

            if (_specifiedMatchNum > 0)
            {
                matchFormatStr = " of " + _specifiedMatchNum + "模板)";
                val = _foundMatchNum;      //'识别率'% ('实际匹配模板个数' of '指定匹配个数' 模板 )
                InspectTabRecogRateData[4] = System.Math.Round(100.0 * ((double)val / _specifiedMatchNum), 2) + " % (" + val + matchFormatStr;
            }

            RecoginzedAndStatisticedDel(MatchModelOpt.UPDATE_INSP_RECOGRATE);

            // ---------------- determine statistics data ------------ 
            if (actualMatchNum > 0) //实际匹配数大于零
            {
                i = 0;
                if (!_modelFound) //初始赋值
                {
                    _scoreMin = _scoreMax = Result.Score[0].D;
                    _elapseTimeMin = _elapseTimeMax = Result.ElapseTime;
                    _rowMin = _rowMax = Result.Row[0].D;
                    _colMin = _colMax = Result.Col[0].D;
                    _scaleRowMin = _scaleRowMax = Result.ScaleRow[0].D;
                    _scaleColMin = _scaleColMax = Result.ScaleCol[0].D;
                    _angleMin = _angleMax = Result.Angle[0].D;
                    _modelFound = true;
                    i++;
                }

                for (; i < actualMatchNum; i++)
                {
                    score = Result.Score[i].D;
                    if (score < _scoreMin)
                        _scoreMin = score;
                    else if (score > _scoreMax)
                        _scoreMax = score;

                    row = Result.Row[0].D;
                    if (row < _rowMin)
                        _rowMin = row;
                    else if (row > _rowMax)
                        _rowMax = row;

                    col = Result.Col[0].D;
                    if (col < _colMin)
                        _colMin = col;
                    else if (col > _colMax)
                        _colMax = col;

                    angleA = Result.Angle[0].D;
                    if (angleA < _angleMin)
                        _angleMin = angleA;
                    else if (angleA > _angleMax)
                        _angleMax = angleA;

                    scaleR = Result.ScaleRow[0].D;
                    if (scaleR < _scaleRowMin)
                        _scaleRowMin = scaleR;
                    else if (scaleR > _scaleRowMax)
                        _scaleRowMax = scaleR;

                    scaleC = Result.ScaleCol[0].D;
                    if (scaleC < _scaleColMin)
                        _scaleColMin = scaleC;
                    else if (scaleC > _scaleColMax)
                        _scaleColMax = scaleC;
                }

                time = Result.ElapseTime;
                if (time < _elapseTimeMin)
                    _elapseTimeMin = time;
                else if (time > _elapseTimeMax)
                    _elapseTimeMax = time;
            }

            if (_modelFound)
            {
                InspectTabResultsData[0] = "" + System.Math.Round(_scoreMin, 2);
                InspectTabResultsData[1] = "" + System.Math.Round(_scoreMax, 2);
                InspectTabResultsData[2] = "" + System.Math.Round((_scoreMax - _scoreMin), 2);

                InspectTabResultsData[3] = "" + System.Math.Round(_elapseTimeMin, 2);
                InspectTabResultsData[4] = "" + System.Math.Round(_elapseTimeMax, 2);
                InspectTabResultsData[5] = "" + System.Math.Round((_elapseTimeMax - _elapseTimeMin), 2);

                InspectTabResultsData[6] = "" + System.Math.Round(_rowMin, 2);
                InspectTabResultsData[7] = "" + System.Math.Round(_rowMax, 2);
                InspectTabResultsData[8] = "" + System.Math.Round((_rowMax - _rowMin), 2);

                InspectTabResultsData[9] = "" + System.Math.Round(_colMin, 2);
                InspectTabResultsData[10] = "" + System.Math.Round(_colMax, 2);
                InspectTabResultsData[11] = "" + System.Math.Round((_colMax - _colMin), 2);

                angleA = (double)_angleMin * 180.0 / System.Math.PI;
                angleB = (double)_angleMax * 180.0 / System.Math.PI;
                InspectTabResultsData[12] = "" + System.Math.Round(angleA, 2) + "°";
                InspectTabResultsData[13] = "" + System.Math.Round(angleB, 2) + "°";
                InspectTabResultsData[14] = "" + System.Math.Round((angleB - angleA), 2) + "°";

                InspectTabResultsData[15] = "" + System.Math.Round(_scaleRowMin, 2);
                InspectTabResultsData[16] = "" + System.Math.Round(_scaleRowMax, 2);
                InspectTabResultsData[17] = "" + System.Math.Round((_scaleRowMax - _scaleRowMin), 2);

                InspectTabResultsData[18] = "" + System.Math.Round(_scaleColMin, 2);
                InspectTabResultsData[19] = "" + System.Math.Round(_scaleColMax, 2);
                InspectTabResultsData[20] = "" + System.Math.Round((_scaleColMax - _scaleColMin), 2);

                RecoginzedAndStatisticedDel(MatchModelOpt.UPDATE_INSP_RESULTS);
            }
            return (CurrentImageIndex < ImageCount);
        }

        /// <summary>
        /// Resets all parameters for evaluating the performance to their initial values.
        /// [模板匹配模型的结果统计数据恢复到初始值]
        /// </summary>
        public override void Reset()
        {
            _probMathcNum = Parameter.NumToMatch;
            _modelFound = false;
            _specifiedMatchNum = 0;
            _maxMatchNum = 0;
            _foundMatchNum = 0;
            _imageWithOneMatchNum = 0;
            _imageWithSpecifiedMatchNum = 0;
            _imageWithMaxMatchNum = 0;
            OptSuccess = false;

            for (int i = 0; i < 21; i++)
                InspectTabResultsData[i] = "-";

            InspectTabRecogRateData[0] = "100.00 % (1 of 1 图像)";
            InspectTabRecogRateData[1] = "100.00 % (1 of 1 图像)";
            InspectTabRecogRateData[2] = "100.00 % (1 of 1 图像)";

            InspectTabRecogRateData[3] = "100.00 % (1 of 1 模板)";
            InspectTabRecogRateData[4] = "100.00 % (1 of 1 模板)";

            ImageCount = MatchAssistant.TestImageList.Count;
            Iterator = MatchAssistant.TestImageList.Keys.GetEnumerator();
            CurrentImageIndex = 0;
        }

        /// <summary>
        /// If the optimization has stopped, then check whether it was
        /// completed successfully or whether it was aborted 
        /// due to errors or to user interaction.
        /// Depending on the failure or success of the run, the GUI is notified
        /// for visual update of the results and obtained statistics.
        /// </summary>
        public override void Stop()
        {
            if (ImageCount == 0)
                RecoginzedAndStatisticedDel(MatchModel.MatchModelAssistant.ERR_NO_TESTIMAGE);
            else if (!OptSuccess)
            {
                RecoginzedAndStatisticedDel(MatchModelOpt.UPDATE_TEST_ERR);
            }
        }
    }
}
