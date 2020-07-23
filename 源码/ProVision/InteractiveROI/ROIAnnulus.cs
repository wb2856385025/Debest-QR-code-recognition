using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ROIAnnulus
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProVision.InteractiveROI
 * File      Name：       ROIAnnulus
 * Creating  Time：       10/8/2019 5:04:14 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProVision.InteractiveROI
{
    public class ROIAnnulus : ROI
    {
        private double _radiusExternal, _radiusInternal;
        private double _locateRow, _locateCol;             //圆环操作柄--圆心--坐标(该点用来定位圆)-0
        private double _sizeRowInternal, _sizeColInternal;  //圆环操作柄--边点1--坐标(该点用来控制圆环的大小)-1 
        private double _sizeRowExternal, _sizeColExternal; //圆环操作柄--边点2--坐标(该点用来控制圆环的大小)-2 

        public ROIAnnulus()
        {
            NumHandles = 3;
            ActiveHandleIdx = 0;  //活动操作柄在中点，以便于移动位置
            ROIShape = ROIShape.ROI_SHAPE_ANNULUS;
        }

        public override void CreateROI(double row, double col)
        {
            _locateRow = row;
            _locateCol = col;

            _radiusInternal = 40;
            _radiusExternal = 80;

            _sizeRowInternal = row;
            _sizeColInternal = col + _radiusInternal;

            _sizeRowExternal = row;
            _sizeColExternal = col + _radiusExternal;

        }

        public override void Draw(HalconDotNet.HTuple hwndHandle)
        {
            HalconDotNet.HObject rgInternal = new HalconDotNet.HObject();
            rgInternal.Dispose();
            HalconDotNet.HOperatorSet.GenCircle(out rgInternal, new HalconDotNet.HTuple(_locateRow), new HalconDotNet.HTuple(_locateCol), new HalconDotNet.HTuple(_radiusInternal));
            HalconDotNet.HOperatorSet.DispObj(rgInternal, hwndHandle);

            HalconDotNet.HObject rgExternal = new HalconDotNet.HObject();
            rgExternal.Dispose();
            HalconDotNet.HOperatorSet.GenCircle(out rgExternal, new HalconDotNet.HTuple(_locateRow), new HalconDotNet.HTuple(_locateCol), new HalconDotNet.HTuple(_radiusExternal));
            HalconDotNet.HOperatorSet.DispObj(rgExternal, hwndHandle);

            HalconDotNet.HObject rec1 = new HalconDotNet.HObject(), rec2 = new HalconDotNet.HObject(), rec3 = new HalconDotNet.HObject();
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
                new HalconDotNet.HTuple(_sizeRowInternal),
                new HalconDotNet.HTuple(_sizeColInternal),
                new HalconDotNet.HTuple(0),
                new HalconDotNet.HTuple(5),
                new HalconDotNet.HTuple(5));
            HalconDotNet.HOperatorSet.DispObj(rec2, hwndHandle);

            rec3.Dispose();
            HalconDotNet.HOperatorSet.GenRectangle2(out rec3,
                new HalconDotNet.HTuple(_sizeRowExternal),
                new HalconDotNet.HTuple(_sizeColExternal),
                new HalconDotNet.HTuple(0),
                new HalconDotNet.HTuple(5),
                new HalconDotNet.HTuple(5));
            HalconDotNet.HOperatorSet.DispObj(rec3, hwndHandle);
        }


        public override double GetDistanceFromStartPoint(double row, double col)
        {
            return base.GetDistanceFromStartPoint(row, col);
        }


        public override double DistanceToClosestHandle(double row, double col)
        {
            double[] val = new double[NumHandles];
            HalconDotNet.HTuple dist;
            HalconDotNet.HOperatorSet.DistancePp(new HalconDotNet.HTuple(new double[] { row, row, row }),
                new HalconDotNet.HTuple(new double[] { col, col, col }),
                new HalconDotNet.HTuple(new double[] { _locateRow, _sizeRowInternal, _sizeRowExternal }),
                new HalconDotNet.HTuple(new double[] { _locateCol, _sizeColInternal, _sizeColExternal }),
                out dist);
            val[0] = dist[0].D;
            val[1] = dist[1].D;
            val[2] = dist[2].D;

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
                         new HalconDotNet.HTuple(_sizeRowInternal),
                         new HalconDotNet.HTuple(_sizeColInternal),
                         new HalconDotNet.HTuple(0),
                         new HalconDotNet.HTuple(5),
                         new HalconDotNet.HTuple(5));
                        HalconDotNet.HOperatorSet.DispObj(rec, hwndHandle);
                    }
                    break;
                case 2:
                    {
                        HalconDotNet.HOperatorSet.GenRectangle2(out rec,
                         new HalconDotNet.HTuple(_sizeRowExternal),
                         new HalconDotNet.HTuple(_sizeColExternal),
                         new HalconDotNet.HTuple(0),
                         new HalconDotNet.HTuple(5),
                         new HalconDotNet.HTuple(5));
                        HalconDotNet.HOperatorSet.DispObj(rec, hwndHandle);
                    }
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

                    _sizeRowInternal += shiftR;
                    _sizeColInternal += shiftC;

                    _sizeRowExternal += shiftR;
                    _sizeColExternal += shiftC;

                    break;
                // handle at circle internal border(scale ROI):缩放
                case 1:
                    _sizeRowInternal = row;
                    _sizeColInternal = col;
                    HalconDotNet.HOperatorSet.DistancePp(new HalconDotNet.HTuple(row), new HalconDotNet.HTuple(col), new HalconDotNet.HTuple(_locateRow), new HalconDotNet.HTuple(_locateCol), out distance);
                    _radiusInternal = distance[0].D;
                    break;
                // handle at circle external border(scale ROI):缩放
                case 2:
                    _sizeRowExternal = row;
                    _sizeColExternal = col;
                    HalconDotNet.HOperatorSet.DistancePp(new HalconDotNet.HTuple(row), new HalconDotNet.HTuple(col), new HalconDotNet.HTuple(_locateRow), new HalconDotNet.HTuple(_locateCol), out distance);
                    _radiusExternal = distance[0].D;
                    break;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override HalconDotNet.HObject GetModelRegion()
        {

            HalconDotNet.HObject rgInternal = new HalconDotNet.HObject();
            rgInternal.Dispose();
            HalconDotNet.HOperatorSet.GenCircle(out rgInternal, new HalconDotNet.HTuple(_locateRow), new HalconDotNet.HTuple(_locateCol), new HalconDotNet.HTuple(_radiusInternal));

            HalconDotNet.HObject rgExternal = new HalconDotNet.HObject();
            rgExternal.Dispose();
            HalconDotNet.HOperatorSet.GenCircle(out rgExternal, new HalconDotNet.HTuple(_locateRow), new HalconDotNet.HTuple(_locateCol), new HalconDotNet.HTuple(_radiusExternal));

            HalconDotNet.HObject rg = new HalconDotNet.HObject();
            rg.Dispose();

            //计算圆环区域:半径大的圆与半径小的圆之间的差集
            if (_radiusExternal > _radiusInternal)
            {
                HalconDotNet.HOperatorSet.Difference(rgExternal, rgInternal, out rg);
            }
            else
            {
                HalconDotNet.HOperatorSet.Difference(rgInternal, rgExternal, out rg);
            }

            return rg;
        }


        public override HalconDotNet.HTuple GetModelData()
        {
            return new HalconDotNet.HTuple(new double[] { _locateRow, _locateCol, _radiusInternal, _radiusExternal });
        }
    }
}
