using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       MatchModelOptAccuracy
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProVision.MatchModel
 * File      Name：       MatchModelOptAccuracy
 * Creating  Time：       10/9/2019 12:39:14 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProVision.MatchModel
{
    /// <summary>
    /// This class optimizes the performance of a defined shape-based model
    /// for a given set of test images.
    /// To perform an optimization of the detection parameters, the instance
    /// has to know the used set of matching parameters and the calling 
    /// 'MatchModelAssistant', to retrieve the set of test images and to call 
    /// the methods for finding the model. 
    /// The optimization is performed in the sense that the two detection 
    /// parameters ScoreMin and Greediness are iteratively increased and
    /// decreased, respectively, and every new parameter combination is used
    /// to detect the model in the set of test images. Each performance is
    /// then measured and compared with the best performance so far.
    /// The single execution steps are triggered by a timer from the
    /// class 'MatchModelAssistant', so that you can stop the optimization anytime
    /// during the run.
    /// </summary>
    public class MatchModelOptAccuracy : MatchModelOpt
    {
        private int _currentScoreMin;
        private int _currentGreediness;
        private double _currentElapseTime;
        private int _scoreMinStep;
        private int _greedinessStep;

        private int _optScoreMin;
        private int _optGreediness;
        private double _optElapseTime;

        private int _matchNum;          //实际的匹配数
        private int _specifiedMathcNum; //指定的匹配数(即预期的匹配数)     

        public MatchModelOptAccuracy(MatchModel.MatchModelAssistant assistant)
        {
            MatchAssistant = assistant;
            Parameter = assistant.Parameter;
            RecoginzedAndStatisticedDel = new RecognizedAndStatisticedDelegate(Dummy);

            _scoreMinStep = -10;
            _greedinessStep = 10;

            Reset();
        }

        /// <summary>
        /// In each execution step a certain parameter set is applied 
        /// to the whole set of test images and the performance is then
        /// evaluated.
        /// </summary>
        public override bool ExecuteStep()
        {
            double cScoreMin, cGreediness, cRecogRate;
            string fileKey;
            int actualMatchNum, specifiedMatchNum;
            bool satisfiedRate;

            //----------------------第一步:检测是否有测试图像-------------------------//

            if (!Iterator.MoveNext()) //未有测试图像,停止优化
                return false;

            //----------------------第二步:当前检测参数下图像处理---------------------//
            cScoreMin = _currentScoreMin / 100.0;
            cGreediness = _currentGreediness / 100.0;

            OptimizationStatus = "测试图像 " + (CurrentImageIndex + 1).ToString() +
                              "-最小得分:" + cScoreMin +
                              "-贪婪度:" + cGreediness;
            RecoginzedAndStatisticedDel(MatchModelOpt.UPDATE_RECOG_STATISTICS_STATUS);

            fileKey = (string)Iterator.Current;
            MatchAssistant.SetTestImage(fileKey);
            MatchAssistant.SetMinScore(cScoreMin);
            MatchAssistant.SetGreediness(cGreediness);

            if (!MatchAssistant.DetectShapeModel()) //查找匹配模板的实例失败,停止优化
                return false;


            Result = MatchAssistant.Result;
            actualMatchNum = Result.MatchCount;
            specifiedMatchNum = 0;                             //每张图像中的指定匹配实例数

            switch (Parameter.RecogAccuracyMode)
            {
                case Communal.MatchModelParameter.RECOG_MODE_SPECIFIEDNUM:
                    specifiedMatchNum = Parameter.RecogSpecifiedMatchNum;
                    break;
                case Communal.MatchModelParameter.RECOG_MODE_ATLEASTONE:
                    specifiedMatchNum = 1;
                    if (actualMatchNum > 1) actualMatchNum = 1;
                    break;
                case Communal.MatchModelParameter.RECOG_MODE_MAXIMUMNUM:
                    specifiedMatchNum = Parameter.NumToMatch;
                    break;
                default: break;
            }

            _matchNum += actualMatchNum;             //实际匹配数
            _specifiedMathcNum += specifiedMatchNum; //指定匹配数总数(所有图像中指定匹配实例数之和)

            cRecogRate = (_specifiedMathcNum > 0) ? (100.0 * _matchNum / _specifiedMathcNum) : 0.0;
            _currentElapseTime = _currentElapseTime * CurrentImageIndex + Result.ElapseTime;
            _currentElapseTime /= ++CurrentImageIndex;

            //Write data into strings and call for update
            RecogTabOpimizationData[0] = "" + System.Math.Round(cScoreMin, 2);
            RecogTabOpimizationData[1] = "" + System.Math.Round(cGreediness, 2);
            RecogTabOpimizationData[2] = "" + System.Math.Round(cRecogRate, 2) + "%";

            if (_currentElapseTime < 1000)
                RecogTabOpimizationData[3] = "" + System.Math.Round(_currentElapseTime, 2) + " ms";
            else
                RecogTabOpimizationData[3] = "" + System.Math.Round(_currentElapseTime / 1000.0, 2) + " s";

            RecoginzedAndStatisticedDel(MatchModelOpt.UPDATE_RECOG_UPDATED_VALS); //更新:识别更新后的参数值

            if (CurrentImageIndex < ImageCount)  //未达到最后图像,返回True(继续按当前参数处理图像:识别并显示)
                return true;

            //----------------------第三步:当前参数条件下，判断识别率-------------------------//
            Iterator.Reset();
            CurrentImageIndex = 0;
            _matchNum = 0;
            _specifiedMathcNum = 0;

            satisfiedRate = (Parameter.RecogRateOpt == 0) ?  //评估搜索的启发式选项:==识别率,>=识别率
                (System.Math.Abs((double)cRecogRate - Parameter.RecogRate) < 0.001) : (cRecogRate >= Parameter.RecogRate);

            //----------------------第三步A:识别率满足设定,递增贪婪度-------------------------//
            if (satisfiedRate) //当前参数条件下,满足识别率条件
            {
                OptSuccess = true;
                if (_currentElapseTime < _optElapseTime)//识别平均时间是否比上次短,短则更新优化后的参数
                {
                    _optScoreMin = _currentScoreMin;
                    _optGreediness = _currentGreediness;
                    _optElapseTime = _currentElapseTime;

                    RecogTabOpimizationData[4] = "" + System.Math.Round(_optScoreMin / 100.0, 2);
                    RecogTabOpimizationData[5] = "" + System.Math.Round(_optGreediness / 100.0, 2);
                    RecogTabOpimizationData[6] = "" + System.Math.Round(cRecogRate, 2) + " %";

                    RecogTabOpimizationData[7] = RecogTabOpimizationData[3];
                    RecoginzedAndStatisticedDel(MatchModelOpt.UPDATE_RECOG_OPTIMAL_VALS); //更新:最优化参数值
                }

                _currentGreediness += _greedinessStep;
                return (_currentGreediness <= 100);       //贪婪度是否超出限定值:未超出--返回True(继续优化贪婪度),超出--返回False(停止优化)
            }


            //----------------------第三步B:识别率不满足设定,递减匹配得分-------------------------//
            _currentScoreMin += _scoreMinStep;           //未满足识别率条件,递减最小匹配得分,准备再优化   

            if (OptSuccess)                              //[优化参数成功],若当前最小匹配得分:大于10,则返回True(继续优化);否则返回False(停止优化)
                return (_currentScoreMin >= 10);         //即优化贪婪度到极限前,出现某个贪婪度下的识别率不满足条件,则再递减匹配得分,若可以满足识别率,则因贪婪度到底极限而停止优化;否则只能到最低匹配得分而停止优化

            //----------------------第四步B:判断匹配得分是否超出底线-------------------------//
            return (_currentScoreMin > 0);              //[优化参数未成功]若当前最小匹配得分:大于0,则返回True(继续优化);否则返回False(停止优化)
        }

        /// <summary>
        /// Resets all parameters for evaluating the performance to their initial values.
        /// 使评估优化过程的参数恢复到初始值
        /// [模板匹配模型部分查找参数恢复到初始值]
        /// </summary>
        public override void Reset()
        {
            OptSuccess = false;
            for (int i = 0; i < 8; i++)
                RecogTabOpimizationData[i] = "-";

            OptimizationStatus = "参数优化:复位";
            _currentScoreMin = 100;
            _currentGreediness = 0;
            _currentElapseTime = 0.0;

            _optScoreMin = 100;
            _optGreediness = 0;
            _optElapseTime = double.MaxValue;

            _matchNum = 0;
            _specifiedMathcNum = 0;

            ImageCount = MatchAssistant.TestImageList.Count;
            Iterator = MatchAssistant.TestImageList.Keys.GetEnumerator();
            CurrentImageIndex = 0;
        }

        /// <summary>
        /// If the optimization has stopped, then check whether it was
        /// completed successfully or whether it was aborted due to errors or
        /// to user interaction.
        /// Depending on the failure or success of the run, the GUI is notified
        /// for visual update of the results and obtained statistics.
        /// </summary>
        public override void Stop()
        {
            if (ImageCount == 0)
            {
                RecoginzedAndStatisticedDel(MatchModel.MatchModelAssistant.ERR_NO_TESTIMAGE);
                RecoginzedAndStatisticedDel(MatchModelOpt.RUN_FAILED);
            }
            else if (!OptSuccess && (_currentScoreMin <= 0.0))
            {
                RecoginzedAndStatisticedDel(MatchModelOpt.UPDATE_RECOG_ERR);
                RecoginzedAndStatisticedDel(MatchModelOpt.RUN_FAILED);
            }
            else if (!OptSuccess)
            {
                RecoginzedAndStatisticedDel(MatchModelOpt.UPDATE_TEST_ERR);
                RecoginzedAndStatisticedDel(MatchModelOpt.RUN_FAILED);
            }
            else
            {
                OptimizationStatus = "参数优化成功完成!";
                RecoginzedAndStatisticedDel(MatchModelOpt.UPDATE_RECOG_STATISTICS_STATUS);
                MatchAssistant.SetMinScore(_optScoreMin / 100.0);
                MatchAssistant.SetGreediness(_optGreediness / 100.0);
                RecoginzedAndStatisticedDel(MatchModelOpt.RUN_SUCCESSFUL);
            }
        }

    }
}
