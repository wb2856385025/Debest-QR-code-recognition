using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ROILine
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProVision.InteractiveROI
 * File      Name：       ROILine
 * Creating  Time：       10/8/2019 5:07:14 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProVision.InteractiveROI
{
    public class ROILine : ROI
    {
        private double _locateRow, _locateCol; //线段操作柄--中点--坐标(该点用来定位线段)-0
        private double _startRow, _startCol;   //线段操作柄--起点--坐标-1
        private double _extentRow, _extentCol; //线段操作柄--终点--坐标-2
        private HalconDotNet.HObject _arrowHandle;

        public ROILine()
        {
            NumHandles = 3;        //两个个端点+一个定位中心点
            ActiveHandleIdx = 0;   //活动操作柄在中点，以便于移动位置
            ROIShape = ROIShape.ROI_SHAPE_LINE;
            _arrowHandle = new HalconDotNet.HObject();
            _arrowHandle.GenEmptyObj();
        }

        public override void CreateROI(double row, double col)
        {
            _locateRow = row;
            _locateCol = col;

            _startRow = _locateRow;
            _startCol = _locateCol - 20;

            _extentRow = _locateRow;
            _extentCol = _locateCol + 20;

            DetermineArrowHandle();
        }

        public override void Draw(HalconDotNet.HTuple hwndHandle)
        {
            HalconDotNet.HObject line = new HalconDotNet.HObject(), rec1 = new HalconDotNet.HObject(), rec2 = new HalconDotNet.HObject();
            line.Dispose();
            HalconDotNet.HOperatorSet.GenRegionLine(out line,
                new HalconDotNet.HTuple(_startRow),
                new HalconDotNet.HTuple(_startCol),
                new HalconDotNet.HTuple(_extentRow),
                new HalconDotNet.HTuple(_extentCol));
            HalconDotNet.HOperatorSet.DispObj(line, hwndHandle);

            rec1.Dispose();
            HalconDotNet.HOperatorSet.GenRectangle2(out rec1,
                new HalconDotNet.HTuple(_startRow),
                new HalconDotNet.HTuple(_startCol),
                new HalconDotNet.HTuple(0),
                new HalconDotNet.HTuple(5),
                new HalconDotNet.HTuple(5));
            HalconDotNet.HOperatorSet.DispObj(rec1, hwndHandle);

            rec2.Dispose();
            HalconDotNet.HOperatorSet.GenRectangle2(out rec2,
                new HalconDotNet.HTuple(_locateRow),
                new HalconDotNet.HTuple(_locateCol),
                new HalconDotNet.HTuple(0),
                new HalconDotNet.HTuple(5),
                new HalconDotNet.HTuple(5));
            HalconDotNet.HOperatorSet.DispObj(rec2, hwndHandle);
            if (_arrowHandle != null
                && _arrowHandle.IsInitialized())
                HalconDotNet.HOperatorSet.DispObj(_arrowHandle, hwndHandle);
        }


        public override double GetDistanceFromStartPoint(double row, double col)
        {
            HalconDotNet.HTuple dist;
            HalconDotNet.HOperatorSet.DistancePp(new HalconDotNet.HTuple(row),
                new HalconDotNet.HTuple(col),
                new HalconDotNet.HTuple(_locateRow),
                new HalconDotNet.HTuple(_locateCol),
                out dist);
            return dist[0].D;
        }


        public override double DistanceToClosestHandle(double row, double col)
        {
            double[] val = new double[NumHandles];
            HalconDotNet.HTuple dist;
            HalconDotNet.HOperatorSet.DistancePp(new HalconDotNet.HTuple(new double[] { row, row, row }),
                new HalconDotNet.HTuple(new double[] { col, col, col }),
                new HalconDotNet.HTuple(new double[] { _locateRow, _startRow, _extentRow }),
                new HalconDotNet.HTuple(new double[] { _locateCol, _startCol, _extentCol }),
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
                //定位中心
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
                //线段起点
                case 1:
                    {
                        HalconDotNet.HOperatorSet.GenRectangle2(out rec,
                         new HalconDotNet.HTuple(_startRow),
                         new HalconDotNet.HTuple(_startCol),
                         new HalconDotNet.HTuple(0),
                         new HalconDotNet.HTuple(5),
                         new HalconDotNet.HTuple(5));
                        HalconDotNet.HOperatorSet.DispObj(rec, hwndHandle);
                    }
                    break;
                //线段终点箭头
                case 2:
                    if (_arrowHandle != null
                        && _arrowHandle.IsInitialized())
                        HalconDotNet.HOperatorSet.DispObj(_arrowHandle, hwndHandle);
                    break;
                default:
                    break;
            }
        }

        public override void MoveByHandle(double row, double col)
        {
            switch (ActiveHandleIdx)
            {
                // handle at center of line segment (translate ROI):平移
                case 0:
                    _startRow = _startRow + (row - _locateRow);
                    _startCol = _startCol + (col - _locateCol);

                    _extentRow = _extentRow + (row - _locateRow);
                    _extentCol = _extentCol + (col - _locateCol);

                    _locateRow = row;
                    _locateCol = col;
                    break;
                // handle at start of line segment (translate ROI):改变线段长短
                case 1:
                    _startRow = row;
                    _startCol = col;

                    _locateRow = (_startRow + _extentRow) / 2.0;
                    _locateCol = (_startCol + _extentCol) / 2.0;
                    break;
                // handle at end of line segment (translate ROI):改变线段长短
                case 2:
                    _extentRow = row;
                    _extentCol = col;

                    _locateRow = (_startRow + _extentRow) / 2.0;
                    _locateCol = (_startCol + _extentCol) / 2.0;
                    break;
                default:
                    break;
            }

            DetermineArrowHandle();
        }

        public override HalconDotNet.HObject GetModelRegion()
        {
            HalconDotNet.HObject rg = new HalconDotNet.HObject();
            rg.Dispose();
            HalconDotNet.HOperatorSet.GenRegionLine(out rg,
               new HalconDotNet.HTuple(_startRow),
               new HalconDotNet.HTuple(_startCol),
               new HalconDotNet.HTuple(_extentRow),
               new HalconDotNet.HTuple(_extentCol));
            return rg;
        }


        public override HalconDotNet.HTuple GetModelData()
        {
            return new HalconDotNet.HTuple(new double[] { _locateRow, _locateCol, _extentRow, _extentCol });
        }

        #region 辅助函数

        private void DetermineArrowHandle()
        {
            double lth, dr, dc, halfHW;
            double rrow, ccol, rowP1, colP1, rowP2, colP2;
            double headlth = 15, headwth = 15;

            //取线段向量的比例值构成的向量
            rrow = _locateRow + (_extentRow - _locateRow) * 0.8;
            ccol = _locateCol + (_extentCol - _locateCol) * 0.8;

            lth = HalconDotNet.HMisc.DistancePp(rrow, ccol, _extentRow, _extentCol);
            if (lth == 0)
                lth = -1;
            dr = (_extentRow - rrow) / lth;
            dc = (_extentCol - ccol) / lth;
            halfHW = headwth / 2.0;

            rowP1 = rrow + (lth - headlth) * dr + halfHW * dc;
            colP1 = ccol + (lth - headlth) * dc - halfHW * dr;

            rowP2 = rrow + (lth - headlth) * dr - halfHW * dc;
            colP2 = ccol + (lth - headlth) * dc + halfHW * dr;

            if (_arrowHandle != null
                   && _arrowHandle.IsInitialized())
                _arrowHandle.Dispose();

            if (lth == -1)
            {
                //产生点
                HalconDotNet.HOperatorSet.GenContourPolygonXld(out _arrowHandle, new HalconDotNet.HTuple(rrow), new HalconDotNet.HTuple(ccol));
            }
            else
            {
                //产生箭头
                HalconDotNet.HOperatorSet.GenContourPolygonXld(out _arrowHandle,
                    new HalconDotNet.HTuple(new double[] { rrow, _extentRow, rowP1, _extentRow, rowP2 }),
                    new HalconDotNet.HTuple(new double[] { ccol, _extentCol, colP1, _extentCol, colP2 }));
            }
        }

        #endregion
    }
}
