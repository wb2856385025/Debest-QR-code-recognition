using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ROIRectangle2
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProVision.InteractiveROI
 * File      Name：       ROIRectangle2
 * Creating  Time：       10/8/2019 5:09:02 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProVision.InteractiveROI
{
    public class ROIRectangle2 : ROI
    {
        private double _locateRow, _locateCol;     //仿射矩形操作柄--中点--坐标(该点用来定位仿射矩形)-0
        private double _length1;                   //仿射矩形主轴长度半值;(平行角度向量)
        private double _length2;                   //仿射矩形次轴长度半值;(垂直角度向量)
        private double _phi;                       //仿射矩形角(弧度) 注--角度参考:角度是以水平方向为零,逆时针为正，反之为负,角度范围[-Π,Π]

        //Auxiliary variables
        private HalconDotNet.HTuple rowsInit, colsInit, rows, cols; //初始矩形的坐标和绘制的矩形坐标      
        private HalconDotNet.HTuple homMat2D, tmp2D;//初始矩形的属性坐标系到绘制矩形的属性坐标系的转换关系；逆转换关系

        public ROIRectangle2()
        {
            NumHandles = 6;          //四个角点+一个定位中心点+一个旋转角点(在矩形边上)
            ActiveHandleIdx = 0;     //活动操作柄在中点，以便于移动位置
            this.ROIShape = ROIShape.ROI_SHAPE_RECTANGLE2;
        }

        public override void CreateROI(double row, double col)
        {
            _locateRow = row;
            _locateCol = col;

            _length1 = 100;
            _length2 = 50;
            _phi = 0.0;

            //角点order :midpoint,upperright,upperleft,lowerleft,lowerright,arrowmidpoint
            rowsInit = new HalconDotNet.HTuple(new double[] { 0.0, -1.0, -1.0, 1.0, 1.0, 0.0 });
            colsInit = new HalconDotNet.HTuple(new double[] { 0.0, 1.0, -1.0, -1.0, 1.0, 0.8 });

            homMat2D = new HalconDotNet.HTuple();
            tmp2D = new HalconDotNet.HTuple();

            DetermineHandlePos();

        }

        /// <summary>
        /// Auxiliary method to recalculate the contour points of 
        /// the rectangle by transforming the initial row and column
        /// coordinates (rowsInit, colsInit) by the updated homography
        /// hom2D.注：基于右手坐标系描述法
        /// </summary>
        private void DetermineHandlePos()
        {
            HalconDotNet.HOperatorSet.HomMat2dIdentity(out homMat2D);
            HalconDotNet.HOperatorSet.HomMat2dTranslateLocal(homMat2D, _locateRow, _locateCol, out homMat2D);//基于基础坐标系的平移
            HalconDotNet.HOperatorSet.HomMat2dRotateLocal(homMat2D, _phi, out homMat2D); //基于平移后坐标系的旋转
                            
            HalconDotNet.HOperatorSet.HomMat2dScaleLocal(homMat2D,
                new HalconDotNet.HTuple(_length2),
                new HalconDotNet.HTuple(_length1), out tmp2D);          //基于旋转后坐标系的缩放

            HalconDotNet.HOperatorSet.AffineTransPoint2d(tmp2D, rowsInit, colsInit, out rows, out cols);//仿射变换得到:点在变换后坐标系相对初始坐标系的坐标
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        public override void Draw(HalconDotNet.HTuple hwndHandle)
        {
            //ROI矩形          
            HalconDotNet.HObject rec;
            HalconDotNet.HOperatorSet.GenRectangle2(out rec,
                        new HalconDotNet.HTuple(_locateRow),
                        new HalconDotNet.HTuple(_locateCol),
                        new HalconDotNet.HTuple(_phi),
                        new HalconDotNet.HTuple(_length1),
                        new HalconDotNet.HTuple(_length2));
            HalconDotNet.HOperatorSet.DispObj(rec, hwndHandle);

            for (int i = 0; i < NumHandles; i++)
            {
                //ROI矩形的操作柄           
                HalconDotNet.HObject rec2;
                HalconDotNet.HOperatorSet.GenRectangle2(out rec2,
                      new HalconDotNet.HTuple(rows[i].D),
                      new HalconDotNet.HTuple(cols[i].D),
                      new HalconDotNet.HTuple(_phi),
                      new HalconDotNet.HTuple(5),
                      new HalconDotNet.HTuple(5));
                HalconDotNet.HOperatorSet.DispObj(rec2, hwndHandle);
            }

            //注：矩形右边的中点坐标(_locateRow-_length1*Sine(angle),_locateCol+_length2*Cosine(angle)),为使箭头超出一点，故而用系数1.3修正          
            HalconDotNet.HOperatorSet.DispArrow(hwndHandle,
                new HalconDotNet.HTuple(_locateRow),
                new HalconDotNet.HTuple(_locateCol),
                new HalconDotNet.HTuple(_locateRow - (1.3 * _length1 * Math.Sin(_phi))),
                new HalconDotNet.HTuple(_locateCol + 1.3 * (_length1 * Math.Cos(_phi))),
                new HalconDotNet.HTuple(2));

        }

        public override double GetDistanceFromStartPoint(double row, double col)
        {
            return base.GetDistanceFromStartPoint(row, col);
        }

        public override double DistanceToClosestHandle(double row, double col)
        {
            double[] val = new double[NumHandles];
            val[0] = HalconDotNet.HMisc.DistancePp(row, col, _locateRow, _locateCol);

            for (int i = 1; i < NumHandles; i++)
            {
                val[i] = HalconDotNet.HMisc.DistancePp(row, col, rows[i], cols[i]);
            }

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
            //显示矩形        
            HalconDotNet.HObject rec;
            HalconDotNet.HOperatorSet.GenRectangle2(out rec,
                        rows[ActiveHandleIdx],
                        cols[ActiveHandleIdx],
                        new HalconDotNet.HTuple(_phi),
                        new HalconDotNet.HTuple(5),
                        new HalconDotNet.HTuple(5));
            HalconDotNet.HOperatorSet.DispObj(rec, hwndHandle);

            //显示箭头
            if (ActiveHandleIdx == 5)
            {
                HalconDotNet.HOperatorSet.DispArrow(hwndHandle,
                  new HalconDotNet.HTuple(_locateRow),
                  new HalconDotNet.HTuple(_locateCol),
                  new HalconDotNet.HTuple(_locateRow - (1.3 * _length1 * Math.Sin(_phi))),
                  new HalconDotNet.HTuple(_locateCol + 1.3 * (_length1 * Math.Cos(_phi))),
                  new HalconDotNet.HTuple(2));
            }

        }

        public override void MoveByHandle(double row, double col)
        {
            HalconDotNet.HTuple vX, vY, x = 0, y = 0;
            switch (ActiveHandleIdx)
            {
                //定位点(平移)
                case 0:
                    _locateRow = row;
                    _locateCol = col;
                    break;
                //四个角点(缩放)
                case 1:
                case 2:
                case 3:
                case 4:
                    HalconDotNet.HOperatorSet.HomMat2dInvert(homMat2D, out tmp2D);
                    HalconDotNet.HOperatorSet.AffineTransPoint2d(tmp2D, row, col, out y, out x);
                    _length1 = Math.Abs(x[0].D);
                    _length2 = Math.Abs(y[0].D);

                    //计算并判断
                    CheckForRange(y, x);
                    break;
                //箭头
                case 5:
                    //旋角向量
                    vY = row - rows[0].D;
                    vX = col - cols[0].D;
                    //角度参考:角度是以水平方向为零,逆时针为正，反之为负,角度范围[-Π,Π]
                    HalconDotNet.HTuple rad = new HalconDotNet.HTuple();
                    HalconDotNet.HOperatorSet.AngleLx((HalconDotNet.HTuple)0, (HalconDotNet.HTuple)0, (HalconDotNet.HTuple)vY, (HalconDotNet.HTuple)vX, out rad);
                    //_phi = Math.Atan2(vX, vY);   
                    _phi = rad[0].D;
                    break;
            }

            DetermineHandlePos();
        }

        /// <summary>
        /// This auxiliary method checks the half lengths
        /// (length1, length2) using the coordinates (x,y) of the four 
        /// rectangle corners (handles 0 to 3) to avoid 'bending' of 
        /// the rectangular ROI at its midpoint, when it comes to a
        /// 'collapse' of the rectangle for length1=length2=0.
        /// 计算并判断(待梳理)
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        private void CheckForRange(double y, double x)
        {
            switch (ActiveHandleIdx)
            {
                //UpperRight
                case 1:
                    if (y < 0 && x > 0)
                        return;
                    if (y >= 0) _length2 = 0.01;
                    if (x <= 0) _length1 = 0.01;
                    break;
                //UpperLeft
                case 2:
                    if (y < 0 && x < 0)
                        return;
                    if (y >= 0) _length2 = 0.01;
                    if (x >= 0) _length1 = 0.01;
                    break;
                //BotLeft
                case 3:
                    if (y > 0 && x < 0)
                        return;
                    if (y <= 0) _length2 = 0.01;
                    if (x >= 0) _length1 = 0.01;
                    break;
                //BotRight
                case 4:
                    if (y > 0 && x > 0)
                        return;
                    if (y <= 0) _length2 = 0.01;
                    if (x <= 0) _length1 = 0.01;
                    break;
            }
        }

        public override HalconDotNet.HObject GetModelRegion()
        {
            HalconDotNet.HObject rec = new HalconDotNet.HObject();
            HalconDotNet.HOperatorSet.GenRectangle2(out rec,
                       new HalconDotNet.HTuple(_locateRow),
                       new HalconDotNet.HTuple(_locateCol),
                       new HalconDotNet.HTuple(_phi),
                       new HalconDotNet.HTuple(_length1),
                       new HalconDotNet.HTuple(_length2));
            return rec;
        }

        public override HalconDotNet.HTuple GetModelData()
        {
            return new HalconDotNet.HTuple(new double[] { _locateRow, _locateCol, _phi, _length1, _length2 });
        }
    }
}
