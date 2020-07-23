using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ROICtrller
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProVision.InteractiveROI
 * File      Name：       ROICtrller
 * Creating  Time：       10/8/2019 4:57:46 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProVision.InteractiveROI
{ 
  /// <summary>
  /// This class creates and manages ROI objects. It responds 
  /// to  mouse device inputs using the methods mouseDownAction and 
  /// mouseMoveAction. You don't have to know this class in detail when you 
  /// build your own C# project. But you must consider a few things if 
  /// you want to use interactive ROIs in your application: There is a
  /// quite close connection between the ROIController and the HWndCtrl 
  /// class, which means that you must 'register' the ROIController
  /// with the HWndCtrl, so the HWndCtrl knows it has to forward user input
  /// (like mouse events) to the ROIController class.  
  /// The visualization and manipulation of the ROI objects is done 
  /// by the ROIController.
  /// This class provides special support for the matching
  /// applications by calculating a model region from the list of ROIs. For
  /// this, ROIs are added and subtracted according to their sign.
  /// 
  /// 该类创建并管理ROI对象，且响应鼠标设备的鼠标事件：鼠标按下、拖动等事件。
  /// 当你创建自己的C#项目时，无需了解该类的详细实现，但确实需要考虑一些细节：
  /// ROIController类与HWndCtrl类关联密切，即需要将ROIController类注册到HWndCtrl类，
  /// 以便于HWndCtrl类可以转发用户输入到ROIController类。
  /// ROIController类完成对ROI对象的可视化以及控制操作。
  /// 该类通过计算ROI对象列表中的模型区域可以对模板匹配应用提供很好支持。
  ///
  /// </summary>
    public class ROIManager
    {
        #region ROI模式标记
        /// <summary>
        /// Constant for setting the ROI mode: positive ROI sign.
        /// ROI操作极性标记：正向极性(Union)
        /// </summary>
        public const int ROI_MODE_POS = 21;

        /// <summary>
        /// Constant for setting the ROI mode: negative ROI sign.
        /// ROI操作极性标记：负向极性(Difference)
        /// </summary>
        public const int ROI_MODE_NEG = 22;

        /// <summary>
        /// Constant for setting the ROI mode: no model region is computed as
        /// the sum of all ROI objects.
        /// ROI操作极性标记：不计算ROI对象模型区域
        /// </summary>
        public const int ROI_MODE_NONE = 23;

        #endregion

        #region ROI事件标记

        /// <summary>Constant describing an update of the model region
        /// ROI事件标记：更新ROI
        /// </summary>
        public const int EVENT_UPDATE_ROI = 50;

        /// <summary>
        /// ROI事件标记：改变ROI操作极性
        /// </summary>
        public const int EVENT_CHANGED_ROI_SIGN = 51;

        /// <summary>Constant describing a moving of the model region
        /// ROI事件标记：移动ROI
        /// </summary>
        public const int EVENT_MOVING_ROI = 52;

        /// <summary>
        /// ROI事件标记：删除活动ROI
        /// </summary>
        public const int EVENT_DELETED_ACTROI = 53;

        /// <summary>
        /// ROI事件标记：删除所有ROI
        /// </summary>
        public const int EVENT_DELETED_ALL_ROIS = 54;

        /// <summary>
        /// ROI事件标记：激活ROI
        /// </summary>
        public const int EVENT_ACTIVATED_ROI = 55;

        /// <summary>
        /// ROI事件标记：创建ROI
        /// </summary>
        public const int EVENT_CREATED_ROI = 56;

        /// <summary>
        /// ROI事件标记:更新联合ROI
        /// </summary>
        public const int EVENT_UPDATE_UNION = 57;

        #endregion

        /// <summary>
        /// ROI形状
        /// [即直线段，圆，圆弧，圆环,矩形，多边形等]
        /// </summary>
        private InteractiveROI.ROI _ROIShape;

        /// <summary>
        /// ROI操作极性标记
        /// 注['SIGN_ROI_NONE','SIGN_ROI_NEG','SIGN_ROI_POS']
        /// </summary>
        private int _operationFlag;

        /// <summary>
        /// 
        /// </summary>
        private double _currY, _currX;

        /// <summary> List containing all created ROI objects so far </summary>
        public System.Collections.ArrayList ROIList;

        /// <summary> Index of the active ROI object </summary>
        private int _activeROIIdx;

        /// <summary> Index of the deleted ROI object </summary>
        private int _deletedROIIdx;

        private double _epsilon = 35.0;    //maximal shortest distance to one of the handles，距离最近操作柄最大距离值，超出该值表示未选中

        /// <summary> SyntheticalRegion obtained by summing up all negative and
        /// positive ROI objects from the ROIList
        /// 综合区域
        /// </summary>
        private HalconDotNet.HObject _syntheticalRegion;

        /// <summary>
        /// NonUnionPositiveRegion obtained by summing up all positive ROI objects from the ROIList
        /// </summary>
        private HalconDotNet.HObject _nonUnionPositiveRegion;
        private string _activeColor = "green";
        private string _activeHandleColor = "red";
        private string _inactiveColor = "yellow";

        /// <summary>
        /// HWindowControl显示控件的管理类
        /// </summary>
        public InteractiveROI.HWndCtrller HWndCtrller;

        /// <summary>
        /// Delegate that notifies about changes made in the model region
        /// 模型区域发生变化的委托
        /// </summary>
        public IconicUpdatedDelegate IconicUpdatedDel;       

        public ROIManager()
        {
            _operationFlag = ROI_MODE_NONE;
            ROIList = new System.Collections.ArrayList();
            _activeROIIdx = -1;
            _deletedROIIdx = -1;
            _syntheticalRegion = new HalconDotNet.HRegion();
            IconicUpdatedDel = new IconicUpdatedDelegate(OnIconicUpdated);
            _currX = _currY = -1;           
        }

        /// <summary>
        /// ROI改变事件的回调函数
        /// </summary>
        /// <param name="val"></param>
        private void OnIconicUpdated(int val)
        {

        }

        /// <summary>
        ///  Registers an instance of a HWndCtrller with this roi
        ///  controller (and vice versa).
        ///  注册显示控件的管理类HWndCtrller实例到ROI管理类ROICtrller实例,反之亦然      
        /// </summary>
        /// <param name="hwndctrller"></param>
        public void RegisterHWndCtrller(InteractiveROI.HWndCtrller hwndctrller)
        {
            HWndCtrller = hwndctrller;           
        }

        /// <summary>
        /// 获取提取区域
        /// [注:即所有带符号ROI的联合区域]
        /// </summary>
        /// <returns></returns>
        public HalconDotNet.HObject GetSyntheticalRegion()
        {
            return _syntheticalRegion;
        }

        /// <summary>
        /// 获取ROI列表
        /// </summary>
        /// <returns></returns>
        public System.Collections.ArrayList GetROIList()
        {
            return ROIList;
        }

        /// <summary>
        /// 获取活动ROI
        /// </summary>
        /// <returns></returns>
        public ROI GetActiveROI()
        {
            if (_activeROIIdx != -1
                && ROIList != null)
            {
                return (ROI)ROIList[_activeROIIdx];
            }
            return null;
        }

        /// <summary>
        /// 获取活动ROI的索引
        /// </summary>
        /// <returns></returns>
        public int GetActiveROIIdx()
        {
            return _activeROIIdx;
        }

        /// <summary>
        /// 设置活动ROI的索引
        /// </summary>
        /// <param name="idx"></param>
        public void SetActiveROIIdx(int idx)
        {
            _activeROIIdx = idx;
        }

        /// <summary>
        /// 获取删除ROI的索引
        /// </summary>
        /// <returns></returns>
        public int GetDelROIIdx()
        {
            return _deletedROIIdx;
        }

        /// <summary>
        /// To create a new ROI object the application class initializes a
        /// 'seed' ROI instance and passes it to the ROICtrller.
        /// The ROICtrller now responds by manipulating this new ROI instance.
        /// </summary>
        /// <param name="r">
        /// 'Seed' ROI object forwarded by the application forms class.</param>
        public void SetROIShape(ROI r)
        {
            _ROIShape = r;
            _ROIShape.SetOperationFlag(_operationFlag);
        }

        /// <summary>
		/// Sets the operation flag of a ROI object to the value 'operationFlag' (MODE_ROI_NONE,
		/// MODE_ROI_POS,MODE_ROI_NEG)
		/// </summary>
        public void SetActiveROISign(int operationFlag)
        {
            switch (operationFlag)
            {
                case ROI_MODE_NEG:
                case ROI_MODE_POS:
                    _operationFlag = operationFlag;
                    break;
                case ROI_MODE_NONE:
                default:
                    _operationFlag = ROI_MODE_NONE;
                    break;
            }

            if (_activeROIIdx != -1)
            {
                ((ROI)ROIList[_activeROIIdx]).SetOperationFlag(_operationFlag);
                HWndCtrller.Repaint(); //ROI--设置活动ROI模式,重新绘图
                IconicUpdatedDel(ROIManager.EVENT_CHANGED_ROI_SIGN);
            }
        }

        /// <summary>
        /// Removes the ROI object that is marked as active.
        /// If no ROI object is active, then nothing happens. 
        /// </summary>
        public void RemoveActiveROI()
        {
            if (_activeROIIdx != -1
                && ROIList != null)
            {
                ROIList.RemoveAt(_activeROIIdx);
                _deletedROIIdx = _activeROIIdx;
                _activeROIIdx = -1;
                HWndCtrller.Repaint();  //ROI--移除活动ROI,重新渲染图形
                IconicUpdatedDel(ROIManager.EVENT_DELETED_ACTROI);
            }
        }

        /// <summary>
        /// Calculates the ModelROI region for all objects contained 
        /// in ROIList, by adding and subtracting the positive and 
        /// negative ROI objects.
        /// 计算合成区域(联合结果)       
        /// </summary>
        public bool CalculateSyntheticalRegion()
        {
            HalconDotNet.HObject tmpAdd, tmpDiff, tmp;
            HalconDotNet.HTuple area = new HalconDotNet.HTuple(), row = new HalconDotNet.HTuple(), col = new HalconDotNet.HTuple();

            if (_operationFlag == ROI_MODE_NONE)
                return true;

            tmpAdd = new HalconDotNet.HObject();
            tmpAdd.GenEmptyObj();
            tmpDiff = new HalconDotNet.HObject();
            tmpDiff.GenEmptyObj();
            tmp = new HalconDotNet.HObject();
            tmp.GenEmptyObj();

            for (int i = 0; i < ROIList.Count; i++)
            {
                switch (((ROI)ROIList[i]).GetOperationFlag())
                {
                    case ROI.MODE_NEGATIVE:
                        HalconDotNet.HOperatorSet.Union2(tmpDiff, ((ROI)ROIList[i]).GetModelRegion(), out tmpDiff);
                        break;
                    case ROI.MODE_POSITIVE:
                        HalconDotNet.HOperatorSet.Union2(tmpAdd, ((ROI)ROIList[i]).GetModelRegion(), out tmpAdd);
                        break;
                }
            }

            if (_syntheticalRegion != null
                && _syntheticalRegion.IsInitialized())
                _syntheticalRegion.Dispose();
            HalconDotNet.HOperatorSet.AreaCenter(tmpAdd, out area, out row, out col);

            if (area.TupleNotEqual(new HalconDotNet.HTuple())
                && area[0].D > 0)
            {
                if (tmp != null && tmp.IsInitialized()) tmp.Dispose();
                HalconDotNet.HOperatorSet.Difference(tmpAdd, tmpDiff, out tmp);

                HalconDotNet.HOperatorSet.AreaCenter(tmp, out area, out row, out col);
                if (area.TupleNotEqual(new HalconDotNet.HTuple())
                 && area[0].D > 0)
                {
                    _syntheticalRegion = tmp;
                }
            }

            if (_syntheticalRegion == null || ROIList.Count == 0)
                return false;

            return true;

        }

        /// <summary>
        /// 计算提取区域(数组结果)
        /// </summary>
        /// <returns></returns>
        public bool CalculateNonUnionPositiveRegion()
        {
            if (_operationFlag == ROI_MODE_NONE)
                return true;
            HalconDotNet.HObject tmp = new HalconDotNet.HObject();
            tmp.GenEmptyObj();

            for (int i = 0; i < ROIList.Count; i++)
            {
                switch (((ROI)ROIList[i]).GetOperationFlag())
                {
                    case ROI.MODE_NEGATIVE:
                        break;
                    case ROI.MODE_POSITIVE:
                        tmp = tmp.ConcatObj(((ROI)ROIList[i]).GetModelRegion());
                        break;
                }
            }

            _nonUnionPositiveRegion = tmp;

            if (ROIList.Count == 0)
                return false;

            return true;
        }

        /// <summary>
        /// Deletes this ROI instance if a 'seed' ROI object has been passed
        /// to the ROICtrller by the application class.
        /// 删除即时ROI
        /// </summary>
        public void ResetInstantROI()
        {
            _activeROIIdx = -1;
            _ROIShape = null;
        }

        /// <summary>
        /// Clears all variables managing ROI objects
        /// 删除ROI列表
        /// </summary>
        public void Reset()
        {
            ROIList.Clear();
            ResetInstantROI();
            if (_syntheticalRegion != null
                && _syntheticalRegion.IsInitialized())
                _syntheticalRegion.Dispose();
            _syntheticalRegion = null;

            if (_nonUnionPositiveRegion != null
                && _nonUnionPositiveRegion.IsInitialized())
                _nonUnionPositiveRegion.Dispose();
            _nonUnionPositiveRegion = null;

            IconicUpdatedDel(EVENT_DELETED_ALL_ROIS);
        }

        /// <summary>
        /// Defines the colors for the ROI objects
        /// </summary>
        /// <param name="aColor">Color for the active ROI object</param>
        /// <param name="inaColor">Color for the inactive ROI objects</param>
        /// <param name="aHdlColor">Color for the active handle of the active ROI object</param>
        public void SetDrawColor(string aColor, string inaColor, string aHdlColor)
        {
            if (aColor != "")
                _activeColor = aColor;
            if (inaColor != "")
                _inactiveColor = inaColor;
            if (aHdlColor != "")
                _activeHandleColor = aHdlColor;
        }       

        /// <summary>
        /// 绘制ROI图形
        /// </summary>
        /// <param name="hwndHandle">窗口句柄</param>
        public void PaintData(HalconDotNet.HTuple hwndHandle)
        {
            HalconDotNet.HOperatorSet.SetDraw(hwndHandle, new HalconDotNet.HTuple("margin"));
            HalconDotNet.HOperatorSet.SetLineWidth(hwndHandle, new HalconDotNet.HTuple(1));
            if (ROIList.Count > 0)
            {
                HalconDotNet.HOperatorSet.SetColor(hwndHandle, new HalconDotNet.HTuple(_inactiveColor));
                for (int i = 0; i < ROIList.Count; i++)
                {
                    HalconDotNet.HOperatorSet.SetLineStyle(hwndHandle, ((ROI)ROIList[i]).LineStyle);
                    ((ROI)ROIList[i]).Draw(hwndHandle);
                }

                if (_activeROIIdx != -1)
                {
                    HalconDotNet.HOperatorSet.SetColor(hwndHandle, new HalconDotNet.HTuple(_activeColor));
                    HalconDotNet.HOperatorSet.SetLineStyle(hwndHandle, ((ROI)ROIList[_activeROIIdx]).LineStyle);
                    ((ROI)ROIList[_activeROIIdx]).Draw(hwndHandle);

                    HalconDotNet.HOperatorSet.SetColor(hwndHandle, new HalconDotNet.HTuple(_activeHandleColor));
                    ((ROI)ROIList[_activeROIIdx]).DisplayActiveHandle(hwndHandle);
                }
            }
        }


        /// <summary>
        /// 鼠标按下时的处理函数
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public int MouseDownAction(double y, double x)
        {          
            int candidateidx = -1;
            _activeROIIdx = -1;
            //_epsilon *= HWndCtrller.ZoomWndFactor;

            if (_ROIShape != null)                         //either a new ROI object is created，创建新的ROI
            {
                _ROIShape.CreateROI(y, x);
                ROIList.Add(_ROIShape);
                _ROIShape = null;
                _activeROIIdx = ROIList.Count - 1;
                HWndCtrller.Repaint();  //ROI--创建ROI,重新渲染图形

                IconicUpdatedDel(EVENT_CREATED_ROI);
            }
            else if (ROIList.Count > 0)                  //an existing ROI object is manipulated,已有ROI
            {
                double[] distArr = new double[ROIList.Count];
                double minvalue = 0;
                int tmpIdx = -1;

                for (int i = 0; i < ROIList.Count; i++)
                {
                    distArr[i] = ((ROI)ROIList[i]).DistanceToClosestHandle(y, x);
                }

                Communal.Functions.MinValueAndIndex(distArr, out minvalue, out tmpIdx);

                if (minvalue < _epsilon)
                    candidateidx = tmpIdx;

                if (candidateidx >= 0)
                {
                    _activeROIIdx = candidateidx;
                    IconicUpdatedDel(EVENT_ACTIVATED_ROI);
                }

                HWndCtrller.Repaint(); //ROI--激活ROI,重新渲染图形
            } //没有ROI,也未创建ROI，则什么都不做

            return _activeROIIdx;
        }

        /// <summary>
        /// 鼠标移动时的处理函数
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        public void MouseMoveAction(double y, double x)
        {
            if (_currY == y && _currX == x)
                return;
            ((ROI)ROIList[_activeROIIdx]).MoveByHandle(y, x);
            HWndCtrller.Repaint();//ROI--移动ROI,重新渲染图形
            _currY = y;
            _currX = x;
            IconicUpdatedDel(EVENT_MOVING_ROI);
        }
    }
}
