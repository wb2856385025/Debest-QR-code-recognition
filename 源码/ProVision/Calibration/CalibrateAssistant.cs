using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CalibrateAssistant
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProVision.Calibration
 * File      Name：       CalibrateAssistant
 * Creating  Time：       3/27/2020 10:49:59 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProVision.Calibration
{
    public class CalibrateAssistant
    {

        public CalibrateAssistant(ProVision.Communal.CalibrationSolution cal)
        {
            CaliSolution = cal;
        }
        /// <summary>
        /// 标定方案
        /// </summary>
        public ProVision.Communal.CalibrationSolution CaliSolution { private set; get; }

        private HalconDotNet.HTuple _p2whomMat2D, _w2phomMat2D;

        /// <summary>
        /// 计算基于特征点的标定转换矩阵
        /// </summary>
        public void CalculateCaliPointHomMatrix()
        {
            if (CaliSolution != null
                && CaliSolution.CalibrationPointPairBList.Count>3)
            {
                                
                HalconDotNet.HTuple row, col, axis1, axis2;
                HalconDotNet.HTuple sx, sy, phi, theta, tx, ty;
                HalconDotNet.HTuple tmp1, tmp2, pixelError, physicalError;
                CaliSolution.IsEffective = false;

                try
                {
                    row = new HalconDotNet.HTuple(CaliSolution.CalibrationPointPairBList[0].PC.Row);
                    col = new HalconDotNet.HTuple(CaliSolution.CalibrationPointPairBList[0].PC.Col);
                    axis1 = new HalconDotNet.HTuple(CaliSolution.CalibrationPointPairBList[0].WC.AXIS1Value);
                    axis2 = new HalconDotNet.HTuple(CaliSolution.CalibrationPointPairBList[0].WC.AXIS2Value);

                    int count = CaliSolution.CalibrationPointPairBList.Count;
                    for (int i = 1; i < count; i++)
                    {
                        row = row.TupleConcat(new HalconDotNet.HTuple(CaliSolution.CalibrationPointPairBList[i].PC.Row));
                        col = col.TupleConcat(new HalconDotNet.HTuple(CaliSolution.CalibrationPointPairBList[i].PC.Col));

                        axis1 = axis1.TupleConcat(new HalconDotNet.HTuple(CaliSolution.CalibrationPointPairBList[i].WC.AXIS1Value));
                        axis2 = axis2.TupleConcat(new HalconDotNet.HTuple(CaliSolution.CalibrationPointPairBList[i].WC.AXIS2Value));
                    }

                    HalconDotNet.HOperatorSet.VectorToHomMat2d(row, col, axis1, axis2,out _p2whomMat2D);
                    HalconDotNet.HOperatorSet.VectorToHomMat2d(axis1, axis2, row, col,out _w2phomMat2D);

                    //1-根据转换关系,物理坐标系点转换为像素点
                    HalconDotNet.HOperatorSet.AffineTransPoint2d(_w2phomMat2D,axis1, axis2, out tmp1, out tmp2);
                    pixelError = ((row - tmp1) * (row - tmp1) + (col - tmp2) * (col - tmp2)).TupleMean();

                    //2-根据转换关系,像素坐标系点转换为物理点
                    HalconDotNet.HOperatorSet.AffineTransPoint2d(_p2whomMat2D, row, col, out tmp1, out tmp2);
                    physicalError = ((axis1 - tmp1) * (axis1 - tmp1) + (axis2 - tmp2) * (axis2 - tmp2)).TupleMean();

                    HalconDotNet.HOperatorSet.HomMat2dToAffinePar(_p2whomMat2D, out sx, out sy, out phi, out theta, out tx, out ty);
                   
                    CaliSolution.ResultOfCaliPoint = new Communal.ResultOfCalibrationPoint();                  
                    CaliSolution.ResultOfCaliPoint.PC2WCHomMat2D = _p2whomMat2D.ToDArr();
                    CaliSolution.ResultOfCaliPoint.WC2PCHomMat2D = _w2phomMat2D.ToDArr();
                    CaliSolution.ResultOfCaliPoint.Sx = sx.D;
                    CaliSolution.ResultOfCaliPoint.Sy = sy.D;
                    CaliSolution.ResultOfCaliPoint.Phi = phi.D;
                    CaliSolution.ResultOfCaliPoint.Theta = theta.D;
                    CaliSolution.ResultOfCaliPoint.Tx = tx.D;
                    CaliSolution.ResultOfCaliPoint.Ty = ty.D;
                    CaliSolution.ResultOfCaliPoint.CalibrationPhysicalError = physicalError;
                    CaliSolution.ResultOfCaliPoint.CalibrationPixelError = pixelError;
                    CaliSolution.IsEffective = true;
                }
                catch { }
            }
        }

        /// <summary>
        /// 像素坐标转换为物理坐标
        /// </summary>
        /// <param name="pixelP"></param>
        /// <param name="physicalP"></param>
        public void TransferPixelToPhysical(ProVision.Communal.PixelCoordinate pixelP,out ProVision.Communal.WorldCoordinate physicalP)
        {
            physicalP = new Communal.WorldCoordinate() { AXIS1Value=pixelP.Row,AXIS2Value=pixelP.Col,AXIS3Value=0};
            if(CaliSolution!=null
                && CaliSolution.IsEffective)
            {
                HalconDotNet.HTuple row, col, x, y;
                row = new HalconDotNet.HTuple(pixelP.Row);
                col = new HalconDotNet.HTuple(pixelP.Col);
                HalconDotNet.HOperatorSet.AffineTransPoint2d(_p2whomMat2D, row, col, out x, out y);
                physicalP.AXIS1Value = x.D;
                physicalP.AXIS2Value = y.D;
            }
        }

        /// <summary>
        /// 物理坐标转换为像素坐标
        /// </summary>
        /// <param name="physicalP"></param>
        /// <param name="pixelP"></param>
        public void TransferPhysicalToPixel(ProVision.Communal.WorldCoordinate physicalP,out ProVision.Communal.PixelCoordinate pixelP)
        {
            pixelP = new Communal.PixelCoordinate() { Row=0,Col=0};
            if (CaliSolution != null
              && CaliSolution.IsEffective)
            {
                HalconDotNet.HTuple row, col, x, y;
                x= new HalconDotNet.HTuple(physicalP.AXIS1Value);
                y = new HalconDotNet.HTuple(physicalP.AXIS2Value);
                HalconDotNet.HOperatorSet.AffineTransPoint2d(_w2phomMat2D, x, y, out row, out col);              
                pixelP.Row = row.D;
                pixelP.Col = col.D;
            }
        }
    }
}
