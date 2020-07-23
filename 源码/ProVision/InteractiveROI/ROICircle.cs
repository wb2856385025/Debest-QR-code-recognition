using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ROICircle
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProVision.InteractiveROI
 * File      Name：       ROICircle
 * Creating  Time：       10/8/2019 5:05:35 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProVision.InteractiveROI
{
    public class ROICircle : ROI
    {
        private double _radius;
        private double _locateRow, _locateCol; //圆操作柄--圆心--坐标(该点用来定位圆)-0
        private double _sizeRow, _sizeCol;     //圆操作柄--边点--坐标(该点用来控制圆的大小)-1     

        /// <summary>
        /// 构造函数
        /// 圆的操作柄数量：2，活动操作柄索引：1
        /// </summary>
        public ROICircle()
        {
            NumHandles = 2;       //一个定位中心点+一个缩放角点(在圆形边上)
            ActiveHandleIdx = 0;  //活动操作柄在中点，以便于移动位置
            ROIShape = ROIShape.ROI_SHAPE_CIRCLE;
        }

        public override void CreateROI(double row, double col)
        {
            _locateRow = row;
            _locateCol = col;
            _radius = 60;

            _sizeRow = _locateRow;
            _sizeCol = _locateCol + _radius;
        }

        public override void Draw(HalconDotNet.HTuple hwndHandle)
        {
            HalconDotNet.HObject rgCircle = new HalconDotNet.HObject();
            rgCircle.Dispose();
            HalconDotNet.HOperatorSet.GenCircle(out rgCircle, new HalconDotNet.HTuple(_locateRow), new HalconDotNet.HTuple(_locateCol), new HalconDotNet.HTuple(_radius));
            HalconDotNet.HOperatorSet.DispObj(rgCircle, hwndHandle);

            HalconDotNet.HObject rec1 = new HalconDotNet.HObject(), rec2 = new HalconDotNet.HObject();
            rec1.Dispose();
            HalconDotNet.HOperatorSet.GenRectangle2(out rec1,
                new HalconDotNet.HTuple(_locateRow),
                new HalconDotNet.HTuple(_locateCol),
                new HalconDotNet.HTuple(0),
                new HalconDotNet.HTuple(5),
                new HalconDotNet.HTuple(5));
            HalconDotNet.HOperatorSet.DispObj(rec1, hwndHandle);

            rec2.Dispose();
            HalconDotNet.HOperatorSet.GenRectangle2(out rec2,
                new HalconDotNet.HTuple(_sizeRow),
                new HalconDotNet.HTuple(_sizeCol),
                new HalconDotNet.HTuple(0),
                new HalconDotNet.HTuple(5),
                new HalconDotNet.HTuple(5));
            HalconDotNet.HOperatorSet.DispObj(rec2, hwndHandle);
        }

        /// <summary>
        /// 计算从ROI起始点，逆时针沿着ROI
        /// 到达ROI中点与指定点连线交ROI于某点时的曲线距离
        /// [弧长]
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public override double GetDistanceFromStartPoint(double row, double col)
        {
            //计算向量A到向量B的角度，角度范围[-π,π];计算弧长时，圆心角范围[0,2π]          
            HalconDotNet.HTuple hvAngle = new HalconDotNet.HTuple();
            HalconDotNet.HOperatorSet.AngleLl(new HalconDotNet.HTuple(_locateRow),
                new HalconDotNet.HTuple(_locateCol),
                new HalconDotNet.HTuple(_sizeRow),
                new HalconDotNet.HTuple(_sizeCol),
                 new HalconDotNet.HTuple(_locateRow),
                new HalconDotNet.HTuple(_locateCol),
                new HalconDotNet.HTuple(row),
                 new HalconDotNet.HTuple(col), out hvAngle);

            if (hvAngle.TupleNotEqual(new HalconDotNet.HTuple()))
            {
                if (hvAngle[0].D < 0)
                {
                    hvAngle[0] += 2 * Math.PI;
                }
            }

            return (_radius * hvAngle[0].D);
        }

        public override double DistanceToClosestHandle(double row, double col)
        {
            double[] val = new double[NumHandles];
            HalconDotNet.HTuple dist;
            HalconDotNet.HOperatorSet.DistancePp(new HalconDotNet.HTuple(new double[] { row, row }),
                new HalconDotNet.HTuple(new double[] { col, col }),
                new HalconDotNet.HTuple(new double[] { _locateRow, _sizeRow }),
                new HalconDotNet.HTuple(new double[] { _locateCol, _sizeCol }),
                out dist);
            val[0] = dist[0].D;
            val[1] = dist[1].D;

            double minvalue = 0.0;
            int idx = 0;
            if (Communal.Functions.MinValueAndIndex(val, out minvalue, out idx))
            {
                ActiveHandleIdx = idx;
            }

            return minvalue;
        }

        public override void DisplayActiveHandle(HalconDotNet.HTuple hwndHandle)
        {
            HalconDotNet.HObject rec = new HalconDotNet.HObject();
            rec.Dispose();
            switch (ActiveHandleIdx)
            {
                case 0:
                    {
                        HalconDotNet.HOperatorSet.GenRectangle2(out rec,
                         new HalconDotNet.HTuple(_locateRow),
                         new HalconDotNet.HTuple(_locateCol),
                         new HalconDotNet.HTuple(0),
                         new HalconDotNet.HTuple(5),
                         new HalconDotNet.HTuple(5));
                        HalconDotNet.HOperatorSet.DispObj(rec, hwndHandle);
                    }
                    break;
                case 1:
                    {
                        HalconDotNet.HOperatorSet.GenRectangle2(out rec,
                         new HalconDotNet.HTuple(_sizeRow),
                         new HalconDotNet.HTuple(_sizeCol),
                         new HalconDotNet.HTuple(0),
                         new HalconDotNet.HTuple(5),
                         new HalconDotNet.HTuple(5));
                        HalconDotNet.HOperatorSet.DispObj(rec, hwndHandle);
                    }
                    break;
                default:
                    break;
            }

        }

        public override void MoveByHandle(double row, double col)
        {
            HalconDotNet.HTuple distance;
            double shiftR, shiftC;
            switch (ActiveHandleIdx)
            {
                // handle at circle center(translate ROI):平移
                case 0:
                    shiftR = row - _locateRow;
                    shiftC = col - _locateCol;

                    _locateRow = row;
                    _locateCol = col;

                    _sizeRow += shiftR;
                    _sizeCol += shiftC;
                    break;
                // handle at circle border(scale ROI):缩放
                case 1:
                    _sizeRow = row;
                    _sizeCol = col;
                    HalconDotNet.HOperatorSet.DistancePp(new HalconDotNet.HTuple(row), new HalconDotNet.HTuple(col), new HalconDotNet.HTuple(_locateRow), new HalconDotNet.HTuple(_locateCol), out distance);
                    _radius = distance[0].D;
                    break;
                default:
                    break;
            }
        }

        public override HalconDotNet.HObject GetModelRegion()
        {
            HalconDotNet.HObject rg = new HalconDotNet.HObject(); rg.Dispose();
            HalconDotNet.HOperatorSet.GenCircle(out rg, new HalconDotNet.HTuple(_locateRow), new HalconDotNet.HTuple(_locateCol), new HalconDotNet.HTuple(_radius));
            return rg;
        }

        public override HalconDotNet.HTuple GetModelData()
        {
            return new HalconDotNet.HTuple(new double[] { _locateRow, _locateCol, _radius });
        }

    }
}
