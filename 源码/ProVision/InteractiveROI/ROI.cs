using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ROI
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProVision.InteractiveROI
 * File      Name：       ROI
 * Creating  Time：       10/3/2019 9:35:06 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProVision.InteractiveROI
{
    /// <summary>
    /// This class is a base class containing virtual methods for handling ROIs.
    /// Therefore, an inheriting class needs to define/override these methods to 
    /// provide the ROIController with the necessary information on its (= the ROIs) 
    /// shape and position.The example project provides derived ROI shapes for rectangles,
    /// lines, circles, and circular arcs.To use other shapes you must derive a new class 
    /// from the base class ROI and implement its methods.
    /// 该类是基类,定义了处理ROI的虚方法.因此,继承类需要定义或者覆写这些虚方法,
    /// 为ROIController类提供ROI本身形状和位置的信息.例程项目提供了扩展ROI类,
    /// 包括矩形,直线,圆和圆弧.若使用其他形状,需要继承该基类并且实现它的方法.
    /// </summary>
    public class ROI
    {

        /// <summary>
        /// ROI形状类型
        /// </summary>
        public ROIShape ROIShape = ROIShape.ROI_SHAPE_NONE;

        /// <summary>
        /// ROI操作柄的个数
        /// </summary>
        protected int NumHandles;

        /// <summary>
        /// ROI活动操作柄索引
        /// </summary>
        protected int ActiveHandleIdx;

        /// <summary>
        /// Flag to define the ROI to be 'positive' or 'negative' 
        /// 操作极性标记:正向极性+负向极性
        /// </summary>
        public int OperationFlag;

        /// <summary>Constant for a positive ROI flag.
        /// ROI操作正极性常量</summary>
        public const int MODE_POSITIVE = ROIManager.ROI_MODE_POS;

        /// <summary>Constant for a negative ROI flag.
        /// ROI操作负极性常量</summary>
        public const int MODE_NEGATIVE = ROIManager.ROI_MODE_NEG;

        protected HalconDotNet.HTuple PosOperation;
        protected HalconDotNet.HTuple NegOperation;

        /// <summary>Parameter to define the line style of the ROI.
        /// ROI线型类型
        /// </summary>
        public HalconDotNet.HTuple LineStyle;

        public ROI()
        {
            PosOperation = new HalconDotNet.HTuple();
            NegOperation = new HalconDotNet.HTuple(new int[] { 2, 2 });
        }

        #region 继承类需要覆写的函数

        /// <summary>Creates a new ROI instance at the mouse position.
        /// 在指定的位置创建ROI实例
        /// </summary>
        /// <param name="row">row coordinate for ROI</param>
        /// <param name="col">column coordinate for ROI</param>
        public virtual void CreateROI(double row, double col) { }

        /// <summary>Paints the ROI into the supplied window.
        /// 在提供的窗口画ROI       
        /// </summary>
        /// <param name="window">HALCON window</param>
        public virtual void Draw(HalconDotNet.HTuple hwndHandle) { }

        /// <summary>
        /// 获取ROI起始点距离指定点的距离值
        /// </summary>
        /// <param name="row">row coordinate</param>
        /// <param name="col">column coordinate</param>
        /// <returns></returns>
        public virtual double GetDistanceFromStartPoint(double row, double col)
        {
            return 0.0;
        }

        /// <summary> 
        /// Returns the distance of the ROI handle being
        /// closest to the image point(row,col)
        /// 获取ROI最靠近指定点的操作柄与该指定点的距离值
        /// </summary>
        /// <param name="row">row coordinate</param>
        /// <param name="col">column coordinate</param>
        /// <returns> 
        /// Distance of the closest ROI handle.
        /// </returns>
        public virtual double DistanceToClosestHandle(double row, double col)
        {
            return 0.0;
        }

        /// <summary> 
        /// Paints the active handle of the ROI object into the supplied window. 
        /// 在提供的窗口显示ROI的活动操作柄
        /// </summary>
        /// <param name="window">HALCON window</param>
        public virtual void DisplayActiveHandle(HalconDotNet.HTuple hwndHandle) { }

        /// <summary> 
        /// Recalculates the shape of the ROI. Translation is 
        /// performed at the active handle of the ROI object 
        /// for the image coordinate (x,y).
        /// 平移或缩放ROI对象
        /// </summary>
        /// <param name="row">row coordinate</param>
        /// <param name="col">column coordinate</param>
        public virtual void MoveByHandle(double row, double col) { }

        /// <summary>Gets the HALCON region described by the ROI.
        /// 获取ROI模型的区域
        /// </summary>
        public virtual HalconDotNet.HObject GetModelRegion()
        {
            return null;
        }

        /// <summary>
        /// Gets the model information described by the ROI. 
        /// 获取ROI模型的信息
        /// </summary> 
        public virtual HalconDotNet.HTuple GetModelData()
        {
            return null;
        }

        #endregion

        #region 继承类继承的函数

        /// <summary>Number of handles defined for the ROI.
        /// 获取ROI操作柄数量
        /// </summary>
        /// <returns>Number of handles</returns>
        public int GetNumHandles()
        {
            return NumHandles;
        }

        /// <summary>Gets the active handle of the ROI.
        /// 获取ROI活动的操作柄
        /// </summary>
        /// <returns>Index of the active handle (from the handle list)</returns>
        public int GetActHandleIdx()
        {
            return ActiveHandleIdx;
        }

        /// <summary>
        /// Gets the sign of the ROI object, being either 
        /// 'positive' or 'negative'. This sign is used when creating a model
        /// region for matching applications from a list of ROIs.
        /// 获取ROI操作极性
        /// </summary>
        public int GetOperationFlag()
        {
            return OperationFlag;
        }

        /// <summary>
        /// Sets the sign of a ROI object to be positive or negative. 
        /// The sign is used when creating a model region for matching
        /// applications by summing up all positive and negative ROI models
        /// created so far.
        /// 设置ROI标记
        /// </summary>
        /// <param name="flag">Sign of ROI object</param>
        public void SetOperationFlag(int flag)
        {
            OperationFlag = flag;
            switch (OperationFlag)
            {
                case ROI.MODE_POSITIVE:
                    LineStyle = PosOperation;
                    break;
                case ROI.MODE_NEGATIVE:
                    LineStyle = NegOperation;
                    break;
                default:
                    LineStyle = PosOperation;
                    break;
            }
        }

        #endregion 
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
}
