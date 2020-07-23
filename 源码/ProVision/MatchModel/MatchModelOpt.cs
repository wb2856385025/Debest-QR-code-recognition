using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       MatchModelOpt
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProVision.MatchModel
 * File      Name：       MatchModelOpt
 * Creating  Time：       10/9/2019 12:05:13 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProVision.MatchModel
{
    /// <summary>
    /// 委托:
    /// </summary>
    /// <param name="val"></param>
    public delegate void RecognizedAndStatisticedDelegate(int val);
    public delegate void ModelMatchedDelegate(int val);
    public delegate void AutoParameterizedDelegate(string val);

    /// <summary>
    /// This class and its derived classes 'MatchModelOptAccuracy' and 
    /// 'MatchModelOptStatistics' implement the optimization process for the 
    /// matching parameters in terms of the recognition speed and the 
    /// recognition rate. Similar to the processing in HDevelop, a timer 
    /// is used to be able to abort the processing during a run.
    /// 模板匹配模型优化：识别速度(耗时)和识别率
    /// </summary>
    public class MatchModelOpt
    {
        /// <summary>
        /// Constant describing a change in the status line
        /// 常量标记:更新识别优化和检查统计的状态
        /// </summary>
        public const int UPDATE_RECOG_STATISTICS_STATUS = 21;

        /// <summary>
        /// Constant describing a change in the statistics of 
        /// the last recognition run
        /// 常量标记:更新上次识别的更新参数值统计表
        /// </summary>
		public const int UPDATE_RECOG_UPDATED_VALS = 22;

        /// <summary>
        /// Constant describing a change in the statistics of 
        /// the optimal recognition run
        /// 常量标记:更新最优化识别的参数值统计表
        /// </summary>
		public const int UPDATE_RECOG_OPTIMAL_VALS = 23;

        /// <summary>
        /// Constant describing an error during the optimization 
        /// run, concerning the test image data or matching model
        /// 常量标记:更新优化参数时测试图像或匹配模板引发的异常
        /// </summary>
		public const int UPDATE_TEST_ERR = 24;

        /// <summary>
        /// Constant describing an error, which says that there is 
        /// no possible combination of matching parameters to obtain
        /// a detection result
        /// 常量标记:更新识别引发的异常
        /// </summary>
		public const int UPDATE_RECOG_ERR = 25;

        /// <summary>
        /// Constant describing a change in the statistics of
        /// the recognition rate
        /// 常量标记:更新检查统计时的识别率值的统计表
        /// </summary>
		public const int UPDATE_INSP_RECOGRATE = 26;

        /// <summary>
        /// Constant describing a change in the statics of
        /// the average recognition results 
        /// 常量标记:更新检查统计时的结果值的统计表
        /// </summary>
		public const int UPDATE_INSP_RESULTS = 27;

        /// <summary>
        /// Constant describing an update of the 
        /// detection results
        /// 常量标记:更新测试图像的识别结果
        /// </summary>
		public const int UPDATE_TESTVIEW = 28;

        /// <summary>
        /// Constant describing the success of the optimization 
        /// process and triggering the adjustment of the GUI
        /// components to the optimal parameter setting
        /// 常量标记:优化成功
        /// </summary>
		public const int RUN_SUCCESSFUL = 29;

        /// <summary>
        /// Constant describing the failure of the optimization
        /// process and reseting the matching parameters to the 
        /// initial setup
        /// 常量标记:优化识别
        /// </summary>
		public const int RUN_FAILED = 30;

        /// <summary>
        /// Delegate to notify about the state of  the optimization process
        /// </summary>
        public RecognizedAndStatisticedDelegate RecoginzedAndStatisticedDel;

        /// <summary>
        /// Information about the optimization process
        /// (e.g. Success or Failure) to be displayed in the GUI
        /// </summary>
        public string OptimizationStatus;

        /// <summary>
        /// Statistics for the parameter optimization
        /// 识别参数优化结果的统计
        /// </summary>
        public string[] RecogTabOpimizationData;

        /// <summary>
        /// Statistics for the recognition rate
        /// 检查模板时的识别率统计
        /// </summary>
        public string[] InspectTabRecogRateData;

        /// <summary>
        ///  Statistics of detection results for the optimal
        ///  检测模板时的结果统计
        /// </summary>
        public string[] InspectTabResultsData;

        /// <summary>
        /// Reference to instance of 'ModelMatchAssistant',which triggers the optimization performance.
        /// 模板匹配模型助手
        /// </summary>
        public MatchModel.MatchModelAssistant MatchAssistant;

        /// <summary>
        /// Set of matching parameters
        /// 模板匹配模型参数集
        /// </summary>
        public Communal.MatchModelParameter Parameter;

        /// <summary>
        /// Result of detection
        /// 模板匹配模型结果
        /// </summary>
        public Communal.MatchModelResult Result;

        /// <summary>
        /// Number of all test images to be inspected
        ///需要检测测试的图像数量
        /// </summary>
        public int ImageCount;

        /// <summary>
        ///  Index of test image, being inspected currently
        ///  当前正在检测图像的索引
        /// </summary>
        public int CurrentImageIndex;

        /// <summary>
        /// Flag, indicating success or failure of optimization process
        /// 标记:优化参数过程是否成功
        /// </summary>
        public bool OptSuccess;

        /// <summary>
        /// 迭代器
        /// </summary>
        public System.Collections.IEnumerator Iterator;

        public MatchModelOpt()
        {
            RecogTabOpimizationData = new string[8];
            InspectTabRecogRateData = new string[5];
            InspectTabResultsData = new string[21];
        }

        /// <summary>
        /// Performs an optimization step
        /// 执行一次优化
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteStep() { return true; }

        /// <summary>
        /// Resets all parameters for evaluating the performance to their initial values.
        /// 使评估优化过程的参数恢复到初始值
        /// </summary>
        public virtual void Reset() { }

        public virtual void Stop() { }

        public void Dummy(int val) { }

    }
}
