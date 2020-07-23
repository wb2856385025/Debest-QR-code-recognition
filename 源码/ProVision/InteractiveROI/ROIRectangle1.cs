using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ROIRectangle1
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProVision.InteractiveROI
 * File      Name：       ROIRectangle1
 * Creating  Time：       10/8/2019 5:08:20 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProVision.InteractiveROI
{
    public class ROIRectangle1 : ROI
    {
        private double _locateRow, _locateCol;     //齐轴矩形操作柄--中点--坐标(该点用来定位齐轴矩形)-0
        private double _upLeftRow, _upLeftCol;     //齐轴矩形操作柄--左上角点--坐标(该点用来缩放齐轴矩形)-2
        private double _botRightRow, _botRightCol; //齐轴矩形操作柄--右下角点--坐标(该点用来缩放齐轴矩形)-4

        public ROIRectangle1()
        {
            NumHandles = 5;          //四个角点+一个定位中心点
            ActiveHandleIdx = 0;     //活动操作柄在中点，以便于移动位置
            this.ROIShape = ROIShape.ROI_SHAPE_RECTANGLE1;
        }

        public override void CreateROI(double row, double col)
        {
            _locateRow = row;
            _locateCol = col;

            _upLeftRow = _locateRow - 50;
            _upLeftCol = _locateCol - 50;
            _botRightRow = _locateRow + 50;
            _botRightCol = _locateCol + 50;
        }

        public override void Draw(HalconDotNet.HTuple hwndHandle)
        {
            HalconDotNet.HObject rec1, rec2, rec3, rec4, rec5, rec;

            HalconDotNet.HOperatorSet.GenRectangle2(out rec1,
                new HalconDotNet.HTuple(_upLeftRow),
                new HalconDotNet.HTuple(_botRightCol),
                new HalconDotNet.HTuple(0),
                new HalconDotNet.HTuple(5),
                new HalconDotNet.HTuple(5));
            HalconDotNet.HOperatorSet.DispObj(rec1, hwndHandle);

            HalconDotNet.HOperatorSet.GenRectangle2(out rec2,
              new HalconDotNet.HTuple(_upLeftRow),
              new HalconDotNet.HTuple(_upLeftCol),
              new HalconDotNet.HTuple(0),
              new HalconDotNet.HTuple(5),
              new HalconDotNet.HTuple(5));
            HalconDotNet.HOperatorSet.DispObj(rec2, hwndHandle);

            HalconDotNet.HOperatorSet.GenRectangle2(out rec3,
             new HalconDotNet.HTuple(_botRightRow),
             new HalconDotNet.HTuple(_upLeftCol),
             new HalconDotNet.HTuple(0),
             new HalconDotNet.HTuple(5),
             new HalconDotNet.HTuple(5));
            HalconDotNet.HOperatorSet.DispObj(rec3, hwndHandle);

            HalconDotNet.HOperatorSet.GenRectangle2(out rec4,
            new HalconDotNet.HTuple(_botRightRow),
            new HalconDotNet.HTuple(_botRightCol),
            new HalconDotNet.HTuple(0),
            new HalconDotNet.HTuple(5),
            new HalconDotNet.HTuple(5));
            HalconDotNet.HOperatorSet.DispObj(rec4, hwndHandle);

            HalconDotNet.HOperatorSet.GenRectangle2(out rec5,
            new HalconDotNet.HTuple(_locateRow),
            new HalconDotNet.HTuple(_locateCol),
            new HalconDotNet.HTuple(0),
            new HalconDotNet.HTuple(5),
            new HalconDotNet.HTuple(5));
            HalconDotNet.HOperatorSet.DispObj(rec5, hwndHandle);

            HalconDotNet.HOperatorSet.GenRectangle1(out rec,
                new HalconDotNet.HTuple(_upLeftRow),
                new HalconDotNet.HTuple(_upLeftCol),
                new HalconDotNet.HTuple(_botRightRow),
                new HalconDotNet.HTuple(_botRightCol));
            HalconDotNet.HOperatorSet.DispObj(rec, hwndHandle);
        }

        public override double GetDistanceFromStartPoint(double row, double col)
        {
            return base.GetDistanceFromStartPoint(row, col);
        }

        public override double DistanceToClosestHandle(double row, double col)
        {
            double[] val = new double[NumHandles];
            HalconDotNet.HTuple dist;
            HalconDotNet.HOperatorSet.DistancePp(new HalconDotNet.HTuple(new double[] { row, row, row, row, row }),
            new HalconDotNet.HTuple(new double[] { col, col, col, col, col }),
            new HalconDotNet.HTuple(new double[] { _locateRow, _upLeftRow, _upLeftRow, _botRightRow, _botRightRow }),
            new HalconDotNet.HTuple(new double[] { _locateCol, _botRightCol, _upLeftCol, _upLeftCol, _botRightCol }),
            out dist);
            val[0] = dist[0].D;
            val[1] = dist[1].D;
            val[2] = dist[2].D;
            val[3] = dist[3].D;
            val[4] = dist[4].D;

            double minvalue = 0.0;
            int idx = 0;
            if (Communal.Functions.MinValueAndIndex(val, out minvalue, out idx))
            {
                this.ActiveHandleIdx = idx;
            }
            return minvalue;
        }

        public override void DisplayActiveHandle(HalconDotNet.HTuple hwndHandle)
        {
            HalconDotNet.HObject rec = new HalconDotNet.HObject();
            rec.Dispose();
            switch (ActiveHandleIdx)
            {
                //齐轴矩形定位中心
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
                //齐轴矩形右上角点
                case 1:
                    {
                        HalconDotNet.HOperatorSet.GenRectangle2(out rec,
                         new HalconDotNet.HTuple(_upLeftRow),
                         new HalconDotNet.HTuple(_botRightCol),
                         new HalconDotNet.HTuple(0),
                         new HalconDotNet.HTuple(5),
                         new HalconDotNet.HTuple(5));
                        HalconDotNet.HOperatorSet.DispObj(rec, hwndHandle);
                    }
                    break;
                //齐轴矩形左上角点
                case 2:
                    {
                        HalconDotNet.HOperatorSet.GenRectangle2(out rec,
                         new HalconDotNet.HTuple(_upLeftRow),
                         new HalconDotNet.HTuple(_upLeftCol),
                         new HalconDotNet.HTuple(0),
                         new HalconDotNet.HTuple(5),
                         new HalconDotNet.HTuple(5));
                        HalconDotNet.HOperatorSet.DispObj(rec, hwndHandle);
                    }
                    break;
                //齐轴矩形左下角点
                case 3:
                    {
                        HalconDotNet.HOperatorSet.GenRectangle2(out rec,
                         new HalconDotNet.HTuple(_botRightRow),
                         new HalconDotNet.HTuple(_upLeftCol),
                         new HalconDotNet.HTuple(0),
                         new HalconDotNet.HTuple(5),
                         new HalconDotNet.HTuple(5));
                        HalconDotNet.HOperatorSet.DispObj(rec, hwndHandle);
                    }
                    break;
                //齐轴矩形右下角点
                case 4:
                    {
                        HalconDotNet.HOperatorSet.GenRectangle2(out rec,
                         new HalconDotNet.HTuple(_botRightRow),
                         new HalconDotNet.HTuple(_botRightCol),
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
            double len1, len2, tmp;
            switch (ActiveHandleIdx)
            {
                //齐轴矩形定位中心
                case 0:
                    len1 = ((_botRightRow - _upLeftRow) / 2.0);
                    len2 = ((_botRightCol - _upLeftCol) / 2.0);

                    _upLeftRow = row - len1;
                    _upLeftCol = col - len2;

                    _botRightRow = row + len1;
                    _botRightCol = col + len2;
                    break;
                //齐轴矩形右上角点
                case 1:
                    _upLeftRow = row;
                    _botRightCol = col;
                    break;
                //齐轴矩形左上角点
                case 2:
                    _upLeftRow = row;
                    _upLeftCol = col;
                    break;
                //齐轴矩形左下角点
                case 3:
                    _botRightRow = row;
                    _upLeftCol = col;
                    break;
                //齐轴矩形右下角点
                case 4:
                    _botRightRow = row;
                    _botRightCol = col;
                    break;
                default:
                    break;
            }

            if (_botRightRow <= _upLeftRow)
            {
                tmp = _upLeftRow;
                _upLeftRow = _botRightRow;
                _botRightRow = tmp;
            }

            if (_botRightCol <= _upLeftCol)
            {
                tmp = _upLeftCol;
                _upLeftCol = _botRightCol;
                _botRightCol = tmp;
            }

            _locateRow = (_upLeftRow + _botRightRow) / 2.0;
            _locateCol = (_upLeftCol + _botRightCol) / 2.0;

        }

        public override HalconDotNet.HObject GetModelRegion()
        {
            HalconDotNet.HObject rg;
            HalconDotNet.HOperatorSet.GenRectangle1(out rg,
               new HalconDotNet.HTuple(_upLeftRow),
               new HalconDotNet.HTuple(_upLeftCol),
               new HalconDotNet.HTuple(_botRightRow),
               new HalconDotNet.HTuple(_botRightCol));

            return rg;
        }

        public override HalconDotNet.HTuple GetModelData()
        {
            return new HalconDotNet.HTuple(new double[] { _upLeftRow, _upLeftCol, _botRightRow, _botRightCol });
        }
    }
}
