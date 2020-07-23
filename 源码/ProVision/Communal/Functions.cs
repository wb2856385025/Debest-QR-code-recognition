using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       Functions
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProVision.Communal
 * File      Name：       Functions
 * Creating  Time：       12/20/2019 6:31:44 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProVision.Communal
{
    public class Functions
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = true)]
        internal static extern void CopyMemory(int Destination, int Source, int Length);

        /// <summary>
        /// 计算数值数组中的最小值和其索引
        /// 注意:首个最小值
        /// </summary>
        /// <param name="valuearr">数值数组</param>
        /// <param name="minvalue">最小值</param>
        /// <param name="idx">最小值索引</param>
        /// <returns></returns>
        public static bool MinValueAndIndex(double[] valuearr, out double minvalue, out int idx)
        {
            bool rt = false;
            idx = 0;
            minvalue = 0.0;
            try
            {
                if (valuearr != null)
                {
                    int tmpindex = 0;
                    minvalue = valuearr[0];
                    foreach (var v in valuearr)
                    {
                        if (tmpindex == 0)
                        {
                            ++tmpindex;
                            continue;
                        }
                        else
                        {
                            if (minvalue > valuearr[tmpindex])
                            {
                                minvalue = valuearr[tmpindex];
                                idx = tmpindex;
                            }
                        }
                        ++tmpindex;
                    }

                    rt = true;
                }
            }
            catch
            {
            }
            finally
            {
            }
            return rt;
        }

        /// <summary>
        /// HObject转Bitmap
        /// </summary>
        /// <param name="hobj">HObject</param>
        /// <param name="bmpbpp8">Bitmap,8位</param>
        /// <returns></returns>
        public static bool HObjectToBitmapBpp8(HalconDotNet.HObject hobj, out System.Drawing.Bitmap bmpbpp8)
        {
            bool rt = false;
            HalconDotNet.HTuple hpointer, type, width, height;

            const int Alpha = 255;
            int[] ptr = new int[2];
            HalconDotNet.HOperatorSet.GetImagePointer1(hobj, out hpointer, out type, out width, out height);
            bmpbpp8 = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            try
            {
                System.Drawing.Imaging.ColorPalette pal = bmpbpp8.Palette;
                for (int i = 0; i <= 255; i++)
                {
                    pal.Entries[i] = System.Drawing.Color.FromArgb(Alpha, i, i, i);
                }

                bmpbpp8.Palette = pal;
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, width, height);
                System.Drawing.Imaging.BitmapData bitmapData = bmpbpp8.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly,
                                                               System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                int PixelSize = System.Drawing.Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8;

                ptr[0] = bitmapData.Scan0.ToInt32();
                ptr[1] = hpointer.I;
                if (width % 4 == 0)
                {
                    CopyMemory(ptr[0], ptr[1], width * height * PixelSize);
                }
                else
                {
                    for (int i = 0; i < height - 1; i++)
                    {
                        ptr[1] += width;
                        CopyMemory(ptr[0], ptr[1], width * PixelSize);
                        ptr[0] += bitmapData.Stride;
                    }
                }
                bmpbpp8.UnlockBits(bitmapData);
                rt = true;
            }
            catch
            {
            }
            finally
            {
            }
            return rt;
        }

        /// <summary>
        /// HObject转Bitmap
        /// </summary>
        /// <param name="hobj">HObject</param>
        /// <param name="bmpbpp24">Bitmap,24位</param>
        /// <returns></returns>
        public static bool HObjectToBitmapBpp24(HalconDotNet.HObject hobj, out System.Drawing.Bitmap bmpbpp24)
        {
            bool rt = false;

            HalconDotNet.HTuple hred, hgreen, hblue, type, width, height;
            HalconDotNet.HOperatorSet.GetImagePointer3(hobj, out hred, out hgreen, out hblue, out type, out width, out height);
            bmpbpp24 = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            try
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, width, height);
                System.Drawing.Imaging.BitmapData bitmapData = bmpbpp24.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                unsafe
                {
                    byte* bptr = (byte*)bitmapData.Scan0;
                    byte* r = ((byte*)hred.I);
                    byte* g = ((byte*)hgreen.I);
                    byte* b = ((byte*)hblue.I);
                    for (int i = 0; i < width * height; i++)
                    {
                        bptr[i * 4] = (b)[i];
                        bptr[i * 4 + 1] = (g)[i];
                        bptr[i * 4 + 2] = (r)[i];
                        bptr[i * 4 + 3] = 255;
                    }
                }

                bmpbpp24.UnlockBits(bitmapData);
                rt = true;
            }
            catch
            {
            }
            finally
            {
            }
            return rt;
        }

        /// <summary>
        /// 方法：HObject对象转Himage对象
        /// </summary>
        /// <param name="hobject"></param>
        /// <param name="image"></param>
        public static void HObjectToHimage(HalconDotNet.HObject hobject, ref HalconDotNet.HImage image)
        {
            if (hobject != null
                && hobject.IsInitialized())
            {
                HalconDotNet.HTuple objclass, channelcnt;
                HalconDotNet.HOperatorSet.GetObjClass(hobject, out objclass);

                if (objclass.TupleEqual(new HalconDotNet.HTuple("image")))
                {
                    HalconDotNet.HOperatorSet.CountChannels(hobject, out channelcnt);
                    switch (channelcnt.I)
                    {
                        case 3:
                            {
                                HalconDotNet.HTuple pointerRed, pointerBlue, pointerGreen, type, width, height;
                                HalconDotNet.HOperatorSet.GetImagePointer3(hobject, out pointerRed, out pointerGreen, out pointerBlue, out type, out width, out height);
                                image.GenImage3(type, width, height, pointerRed, pointerGreen, pointerBlue);
                            }
                            break;
                        default:
                            {
                                HalconDotNet.HTuple pointer, type, width, height;
                                HalconDotNet.HOperatorSet.GetImagePointer1(hobject, out pointer, out type, out width, out height);
                                image.GenImage1(type, width, height, pointer);
                            }
                            break;
                    }
                }

            }

        }

        /// <summary>
        /// Bitmap转HObject
        /// </summary>
        /// <param name="bmp">24位Bitmap</param>
        /// <param name="hobj"></param>
        /// <returns></returns>
        public static bool BitmapBpp24ToHObject(System.Drawing.Bitmap bmp, out HalconDotNet.HObject hobj)
        {
            bool rt = false;
            HalconDotNet.HOperatorSet.GenEmptyObj(out hobj);

            try
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
                System.Drawing.Imaging.BitmapData srcBmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                HalconDotNet.HOperatorSet.GenImageInterleaved(out hobj, srcBmpData.Scan0, "bgr", bmp.Width, bmp.Height, 0, "byte", 0, 0, 0, 0, -1, 0);
                bmp.UnlockBits(srcBmpData);
                rt = true;
            }
            catch
            {
            }
            finally
            {
            }
            return rt;
        }

        /// <summary>
        /// Bitmap转HObject
        /// </summary>
        /// <param name="bmp">8位Bitmap</param>
        /// <param name="hobj"></param>
        /// <returns></returns>
        public static bool BitmapBpp8ToHObject(System.Drawing.Bitmap bmp, out HalconDotNet.HObject hobj)
        {
            bool rt = false;
            HalconDotNet.HOperatorSet.GenEmptyObj(out hobj);

            try
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
                System.Drawing.Imaging.BitmapData srcBmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                HalconDotNet.HOperatorSet.GenImage1(out hobj, "byte", bmp.Width, bmp.Height, srcBmpData.Scan0);
                bmp.UnlockBits(srcBmpData);
                rt = true;
            }
            catch
            {
            }
            finally
            {
            }
            return rt;
        }

        public static bool BitmapToHObject(System.Drawing.Bitmap bmp, out HalconDotNet.HObject hobj)
        {
            bool rt = false;
            HalconDotNet.HOperatorSet.GenEmptyObj(out hobj);

            try
            {
                switch (bmp.PixelFormat)
                {
                    case System.Drawing.Imaging.PixelFormat.Format16bppArgb1555:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format16bppGrayScale:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format16bppRgb555:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format16bppRgb565:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format1bppIndexed:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                        if (BitmapBpp24ToHObject(bmp, out hobj))
                            rt = true;
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format32bppPArgb:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format48bppRgb:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format4bppIndexed:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format64bppArgb:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format64bppPArgb:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                        if (BitmapBpp8ToHObject(bmp, out hobj))
                            rt = true;
                        break;
                }
            }
            catch
            {
            }
            finally
            {
            }
            return rt;
        }

        /// <summary>
        /// 获取矩形角点
        /// </summary>
        /// <param name="RowCen">仿射矩形中心坐标Row</param>
        /// <param name="ColCen">仿射矩形中心坐标Col</param>
        /// <param name="Phi">仿射矩形角度</param>
        /// <param name="Length1">仿射矩形主轴长度(半值)</param>
        /// <param name="Length2">仿射矩形次轴长度(半值)</param>
        /// <param name="RowVer">仿射矩形角点坐标Row</param>
        /// <param name="ColVer">仿射矩形角点坐标Col</param>
        /// <param name="ErrorCode">错误代码</param>
        /// <param name="ErrorMessage">错误信息</param>
        /// <returns></returns>
        public static bool GetRectangleVertex(HalconDotNet.HTuple RowCen, HalconDotNet.HTuple ColCen, HalconDotNet.HTuple Phi, HalconDotNet.HTuple Length1, HalconDotNet.HTuple Length2,
            out HalconDotNet.HTuple RowVer, out HalconDotNet.HTuple ColVer, out HalconDotNet.HTuple ErrorCode, out HalconDotNet.HTuple ErrorMessage)
        {
            bool rt = false;
            ErrorCode = new HalconDotNet.HTuple();
            ErrorMessage = new HalconDotNet.HTuple();
            RowVer = new HalconDotNet.HTuple();
            ColVer = new HalconDotNet.HTuple();

            HalconDotNet.HTuple rowtem = null, coltem = null, sin, cos, hException;
            try
            {
                if ((int)(new HalconDotNet.HTuple(Length1.TupleLess(0))) != 0 || (int)(new HalconDotNet.HTuple(Length2.TupleLess(0))) != 0)
                {
                    ErrorCode = 860300000;
                    ErrorMessage = "Error:IN ilegal parameter";
                    return rt;
                }

                HalconDotNet.HOperatorSet.TupleSin(Phi, out sin);
                HalconDotNet.HOperatorSet.TupleCos(Phi, out cos);

                //Up-Right Vertex
                rowtem = RowCen - Length1 * sin - Length2 * cos;
                coltem = ColCen + Length1 * cos - Length2 * sin;
                RowVer = RowVer.TupleConcat(rowtem);
                ColVer = ColVer.TupleConcat(coltem);

                //Up-Left Vertex
                rowtem = RowCen + Length1 * sin - Length2 * cos;
                coltem = ColCen - Length1 * cos - Length2 * sin;
                RowVer = RowVer.TupleConcat(rowtem);
                ColVer = ColVer.TupleConcat(coltem);

                //Lower-Left Vertex
                rowtem = RowCen + Length1 * sin + Length2 * cos;
                coltem = ColCen - Length1 * cos + Length2 * sin;
                RowVer = RowVer.TupleConcat(rowtem);
                ColVer = ColVer.TupleConcat(coltem);

                //Lower-Right Vertex
                rowtem = RowCen - Length1 * sin + Length2 * cos;
                coltem = ColCen + Length1 * cos + Length2 * sin;
                RowVer = RowVer.TupleConcat(rowtem);
                ColVer = ColVer.TupleConcat(coltem);
                rt = true;
            }
            catch (HalconDotNet.HalconException HException)
            {
                RowVer = null;
                ColVer = null;
                HException.ToHTuple(out hException);
                HalconDotNet.HalconException.GetExceptionData(hException, "error_code", out ErrorCode);
                HalconDotNet.HalconException.GetExceptionData(hException, "error_message", out ErrorMessage);
            }
            finally
            {
            }
            return rt;
        }

        /// <summary>
        /// 获取矩形直边中点
        /// </summary>
        /// <param name="RowCen">仿射矩形中心坐标Row</param>
        /// <param name="ColCen">仿射矩形中心坐标Col</param>
        /// <param name="Phi">仿射矩形角度</param>
        /// <param name="Length1">仿射矩形主轴长度(半值)</param>
        /// <param name="Length2">仿射矩形次轴长度(半值)</param>
        /// <param name="RowSideMid">仿射矩形直边中点坐标Row</param>
        /// <param name="ColSideMid">仿射矩形直边中点坐标Col</param>
        /// <param name="ErrorCode">错误代码</param>
        /// <param name="ErrorMessage">错误信息</param>
        /// <returns></returns>
        public static bool GetRectangleSideMidPoint(HalconDotNet.HTuple RowCen, HalconDotNet.HTuple ColCen, HalconDotNet.HTuple Phi, HalconDotNet.HTuple Length1, HalconDotNet.HTuple Length2,
            out HalconDotNet.HTuple RowSideMid, out HalconDotNet.HTuple ColSideMid, out HalconDotNet.HTuple ErrorCode, out HalconDotNet.HTuple ErrorMessage)
        {
            bool rt = false;
            ErrorCode = new HalconDotNet.HTuple();
            ErrorMessage = new HalconDotNet.HTuple();
            RowSideMid = new HalconDotNet.HTuple();
            ColSideMid = new HalconDotNet.HTuple();

            HalconDotNet.HTuple rowtem = null, coltem = null, sin, cos, hException;
            try
            {
                if ((int)(new HalconDotNet.HTuple(Length1.TupleLess(0))) != 0 || (int)(new HalconDotNet.HTuple(Length2.TupleLess(0))) != 0)
                {
                    ErrorCode = 860300000;
                    ErrorMessage = "Error:IN ilegal parameter";
                    return rt;
                }

                HalconDotNet.HOperatorSet.TupleSin(Phi, out sin);
                HalconDotNet.HOperatorSet.TupleCos(Phi, out cos);

                //Side combined by Up-Right And Up-Left
                rowtem = RowCen - Length2 * cos;
                coltem = ColCen - Length2 * sin;
                RowSideMid = RowSideMid.TupleConcat(rowtem);
                ColSideMid = ColSideMid.TupleConcat(coltem);

                //Side combined by Up-Left And Lower-Left
                rowtem = RowCen + Length1 * sin;
                coltem = ColCen - Length1 * cos;
                RowSideMid = RowSideMid.TupleConcat(rowtem);
                ColSideMid = ColSideMid.TupleConcat(coltem);

                //Side combined by Lower-Left And Lower-Right
                rowtem = RowCen + Length2 * cos;
                coltem = ColCen + Length2 * sin;
                RowSideMid = RowSideMid.TupleConcat(rowtem);
                ColSideMid = ColSideMid.TupleConcat(coltem);

                //Side combined by Lower-Right And Up-Right
                rowtem = RowCen - Length1 * sin;
                coltem = ColCen + Length1 * cos;
                RowSideMid = RowSideMid.TupleConcat(rowtem);
                ColSideMid = ColSideMid.TupleConcat(coltem);
                rt = true;
            }
            catch (HalconDotNet.HalconException HException)
            {
                RowSideMid = null;
                ColSideMid = null;
                HException.ToHTuple(out hException);
                HalconDotNet.HalconException.GetExceptionData(hException, "error_code", out ErrorCode);
                HalconDotNet.HalconException.GetExceptionData(hException, "error_message", out ErrorMessage);
            }
            finally
            {
            }
            return rt;
        }

        /// <summary>
        /// 计算高斯线条算子参数
        /// </summary>
        /// <param name="MaxLineWidth">线条像素宽度最大值</param>
        /// <param name="Contrast">线条对比值</param>
        /// <param name="Sigma">高斯平滑系数</param>
        /// <param name="HysteresisThresholdRange">高斯迟滞阈值范围</param>
        /// <returns></returns>
        public static bool CalculateLineGaussParameters(HalconDotNet.HTuple MaxLineWidth, HalconDotNet.HTuple Contrast,
            out HalconDotNet.HTuple Sigma, out HalconDotNet.HTuple HysteresisThresholdRange)
        {
            bool rt = false;
            Sigma = new HalconDotNet.HTuple();
            HysteresisThresholdRange = new HalconDotNet.HTuple();

            //local variable
            HalconDotNet.HTuple hv_MaxLineWidth_COPY_INP_TMP, hv_ContrastHigh, hv_ContrastLow, hv_HalfWidth = null, hv_Help = null, hv_High, hv_Low;
            hv_MaxLineWidth_COPY_INP_TMP = MaxLineWidth.Clone();

            try
            {
                //Check control parameters
                if ((int)(new HalconDotNet.HTuple((new HalconDotNet.HTuple(hv_MaxLineWidth_COPY_INP_TMP.TupleLength())).TupleNotEqual(
                    1))) != 0)
                {
                    throw new HalconDotNet.HalconException("Wrong number of values of control parameter: 1");
                }

                if ((int)(((hv_MaxLineWidth_COPY_INP_TMP.TupleIsNumber())).TupleNot()) != 0)
                {
                    throw new HalconDotNet.HalconException("Wrong type of control parameter: 1");
                }

                if ((int)(new HalconDotNet.HTuple(hv_MaxLineWidth_COPY_INP_TMP.TupleLessEqual(0))) != 0)
                {
                    throw new HalconDotNet.HalconException("Wrong value of control parameter: 1");
                }

                if ((int)((new HalconDotNet.HTuple((new HalconDotNet.HTuple(Contrast.TupleLength())).TupleNotEqual(1))).TupleAnd(
                    new HalconDotNet.HTuple((new HalconDotNet.HTuple(Contrast.TupleLength())).TupleNotEqual(2)))) != 0)
                {
                    throw new HalconDotNet.HalconException("Wrong number of values of control parameter: 2");
                }
                if ((int)(new HalconDotNet.HTuple(((((Contrast.TupleIsNumber())).TupleMin())).TupleEqual(
                    0))) != 0)
                {
                    throw new HalconDotNet.HalconException("Wrong type of control parameter: 2");
                }

                //Set and check ContrastHigh
                hv_ContrastHigh = Contrast[0];
                if ((int)(new HalconDotNet.HTuple(hv_ContrastHigh.TupleLess(0))) != 0)
                {
                    throw new HalconDotNet.HalconException("Wrong value of control parameter: 2");
                }

                //Set or derive ContrastLow
                if ((int)(new HalconDotNet.HTuple((new HalconDotNet.HTuple(Contrast.TupleLength())).TupleEqual(2))) != 0)
                {
                    hv_ContrastLow = Contrast[1];
                }
                else
                {
                    hv_ContrastLow = hv_ContrastHigh / 3.0;
                }

                //Check ContrastLow
                if ((int)(new HalconDotNet.HTuple(hv_ContrastLow.TupleLess(0))) != 0)
                {
                    throw new HalconDotNet.HalconException("Wrong value of control parameter: 2");
                }
                if ((int)(new HalconDotNet.HTuple(hv_ContrastLow.TupleGreater(hv_ContrastHigh))) != 0)
                {
                    throw new HalconDotNet.HalconException("Wrong value of control parameter: 2");
                }

                //Calculate the parameters Sigma, Low, and High for lines_gauss
                if ((int)(new HalconDotNet.HTuple(hv_MaxLineWidth_COPY_INP_TMP.TupleLess((new HalconDotNet.HTuple(3.0)).TupleSqrt()
                    ))) != 0)
                {
                    //Note that LineWidthMax < sqrt(3.0) would result in a Sigma < 0.5,
                    //which does not make any sense, because the corresponding smoothing
                    //filter mask would be of size 1x1.
                    //To avoid this, LineWidthMax is restricted to values greater or equal
                    //to sqrt(3.0) and the contrast values are adapted to reflect the fact
                    //that lines that are thinner than sqrt(3.0) pixels have a lower contrast
                    //in the smoothed image (compared to lines that are sqrt(3.0) pixels wide).
                    hv_ContrastLow = (hv_ContrastLow * hv_MaxLineWidth_COPY_INP_TMP) / ((new HalconDotNet.HTuple(3.0)).TupleSqrt()
                        );
                    hv_ContrastHigh = (hv_ContrastHigh * hv_MaxLineWidth_COPY_INP_TMP) / ((new HalconDotNet.HTuple(3.0)).TupleSqrt()
                        );
                    hv_MaxLineWidth_COPY_INP_TMP = (new HalconDotNet.HTuple(3.0)).TupleSqrt();
                }

                //Convert LineWidthMax and the given contrast values into the input parameters
                //Sigma, Low, and High required by lines_gauss
                hv_HalfWidth = hv_MaxLineWidth_COPY_INP_TMP / 2.0;
                Sigma = hv_HalfWidth / ((new HalconDotNet.HTuple(3.0)).TupleSqrt());

                hv_Help = ((-2.0 * hv_HalfWidth) / (((new HalconDotNet.HTuple(6.283185307178)).TupleSqrt()) * (Sigma.TuplePow(
                    3.0)))) * (((-0.5 * (((hv_HalfWidth / Sigma)).TuplePow(2.0)))).TupleExp());

                hv_High = ((hv_ContrastHigh * hv_Help)).TupleFabs();
                hv_Low = ((hv_ContrastLow * hv_Help)).TupleFabs();

                HysteresisThresholdRange[0] = hv_Low;
                HysteresisThresholdRange[1] = hv_High;
                rt = true;
            }
            catch { }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        /// 产生高斯滤波器
        /// </summary>
        /// <param name="imgGauss">频域高斯滤波器</param>
        /// <param name="gaussfiltersigmas">高斯滤波器偏差(主方向和次方向)</param>
        /// <param name="gaussphi">高斯滤波器主方向角</param>
        /// <param name="imgwidth">图像宽度</param>
        /// <param name="imgheight">图像高度</param>
        /// <returns></returns>
        public static bool GenerateGassFilter(out HalconDotNet.HObject imgGauss, HalconDotNet.HTuple gaussfiltersigmas, HalconDotNet.HTuple gaussphi, HalconDotNet.HTuple imgwidth, HalconDotNet.HTuple imgheight)
        {
            bool rt = false;
            HalconDotNet.HOperatorSet.GenEmptyObj(out imgGauss);
            try
            {
                imgGauss.Dispose();
                HalconDotNet.HOperatorSet.GenGaussFilter(out imgGauss, gaussfiltersigmas[0], gaussfiltersigmas[1], gaussphi, "n", "rft", imgwidth, imgheight);
                rt = true;
            }
            catch { }
            finally
            {
            }
            return rt;
        }

        /// <summary>
        /// 打开新的适应图像大小的窗口
        /// </summary>
        /// <param name="ho_Image">输入图像</param>
        /// <param name="hv_Row">窗口起始点行坐标</param>
        /// <param name="hv_Column">窗口起始点列坐标</param>
        /// <param name="hv_WidthLimit">窗口宽度范围</param>
        /// <param name="hv_HeightLimit">窗口高度范围</param>
        /// <param name="hv_WindowHandle">新窗口句柄</param>
        /// <returns></returns>
        public static bool OpenWindowFitImage(HalconDotNet.HObject ho_Image, HalconDotNet.HTuple hv_Row, HalconDotNet.HTuple hv_Column, HalconDotNet.HTuple hv_WidthLimit, HalconDotNet.HTuple hv_HeightLimit, out HalconDotNet.HTuple hv_WindowHandle)
        {
            bool rt = false;

            //This procedure opens a new graphics window and adjusts the size
            //such that it fits into the limits specified by WidthLimit
            //and HeightLimit, but also maintains the correct image aspect ratio.
            //
            //If it is impossible to match the minimum and maximum extent requirements
            //at the same time (f.e. if the image is very long but narrow),
            //the maximum value gets a higher priority,
            //

            // Initialize local and output iconic variables
            hv_WindowHandle = new HalconDotNet.HTuple();

            try
            {
                // Local iconic variables 
                // Local control variables

                HalconDotNet.HTuple hv_MinWidth = new HalconDotNet.HTuple(), hv_MaxWidth = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_MinHeight = new HalconDotNet.HTuple(), hv_MaxHeight = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_ResizeFactor = null, hv_ImageWidth = null, hv_ImageHeight = null;
                HalconDotNet.HTuple hv_TempWidth = null, hv_TempHeight = null, hv_WindowWidth = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_WindowHeight = null;

                //Parse input tuple WidthLimit
                if ((int)((new HalconDotNet.HTuple((new HalconDotNet.HTuple(hv_WidthLimit.TupleLength())).TupleEqual(0))).TupleOr(
                    new HalconDotNet.HTuple(hv_WidthLimit.TupleLess(0)))) != 0)
                {
                    hv_MinWidth = 500;
                    hv_MaxWidth = 800;
                }
                else if ((int)(new HalconDotNet.HTuple((new HalconDotNet.HTuple(hv_WidthLimit.TupleLength())).TupleEqual(
                    1))) != 0)
                {
                    hv_MinWidth = 0;
                    hv_MaxWidth = hv_WidthLimit.Clone();
                }
                else
                {
                    hv_MinWidth = hv_WidthLimit[0];
                    hv_MaxWidth = hv_WidthLimit[1];
                }

                //Parse input tuple HeightLimit
                if ((int)((new HalconDotNet.HTuple((new HalconDotNet.HTuple(hv_HeightLimit.TupleLength())).TupleEqual(0))).TupleOr(
                    new HalconDotNet.HTuple(hv_HeightLimit.TupleLess(0)))) != 0)
                {
                    hv_MinHeight = 400;
                    hv_MaxHeight = 600;
                }
                else if ((int)(new HalconDotNet.HTuple((new HalconDotNet.HTuple(hv_HeightLimit.TupleLength())).TupleEqual(
                    1))) != 0)
                {
                    hv_MinHeight = 0;
                    hv_MaxHeight = hv_HeightLimit.Clone();
                }
                else
                {
                    hv_MinHeight = hv_HeightLimit[0];
                    hv_MaxHeight = hv_HeightLimit[1];
                }

                //Test, if window size has to be changed.
                hv_ResizeFactor = 1;
                HalconDotNet.HOperatorSet.GetImageSize(ho_Image, out hv_ImageWidth, out hv_ImageHeight);
                //First, expand window to the minimum extents (if necessary).
                if ((int)((new HalconDotNet.HTuple(hv_MinWidth.TupleGreater(hv_ImageWidth))).TupleOr(new HalconDotNet.HTuple(hv_MinHeight.TupleGreater(
                    hv_ImageHeight)))) != 0)
                {
                    hv_ResizeFactor = (((((hv_MinWidth.TupleReal()) / hv_ImageWidth)).TupleConcat(
                        (hv_MinHeight.TupleReal()) / hv_ImageHeight))).TupleMax();
                }

                hv_TempWidth = hv_ImageWidth * hv_ResizeFactor;
                hv_TempHeight = hv_ImageHeight * hv_ResizeFactor;

                //Then, shrink window to maximum extents (if necessary).
                if ((int)((new HalconDotNet.HTuple(hv_MaxWidth.TupleLess(hv_TempWidth))).TupleOr(new HalconDotNet.HTuple(hv_MaxHeight.TupleLess(
                    hv_TempHeight)))) != 0)
                {
                    hv_ResizeFactor = hv_ResizeFactor * ((((((hv_MaxWidth.TupleReal()) / hv_TempWidth)).TupleConcat(
                        (hv_MaxHeight.TupleReal()) / hv_TempHeight))).TupleMin());
                }

                hv_WindowWidth = hv_ImageWidth * hv_ResizeFactor;
                hv_WindowHeight = hv_ImageHeight * hv_ResizeFactor;
                //Resize window
                HalconDotNet.HOperatorSet.SetWindowAttr("background_color", "black");
                HalconDotNet.HOperatorSet.OpenWindow(hv_Row, hv_Column, hv_WindowWidth, hv_WindowHeight, 0, "", "", out hv_WindowHandle);
                HalconDotNet.HDevWindowStack.Push(hv_WindowHandle);
                if (HalconDotNet.HDevWindowStack.IsOpen())
                {
                    HalconDotNet.HOperatorSet.SetPart(HalconDotNet.HDevWindowStack.GetActive(), 0, 0, hv_ImageHeight - 1, hv_ImageWidth - 1);
                }

                rt = true;
            }
            catch
            {
            }
            finally
            {
            }
            return rt;
        }

        /// <summary>
        /// 在指定窗口显示信息
        /// </summary>
        /// <param name="hv_WindowHandle">窗口句柄</param>
        /// <param name="hv_String">待显字符串</param>
        /// <param name="hv_CoordSystem">坐标系统</param>
        /// <param name="hv_Row">系统行坐标</param>
        /// <param name="hv_Column">系统列坐标</param>
        /// <param name="hv_Color">字符串颜色</param>
        /// <param name="hv_Box">字符串容器</param>
        /// <returns></returns>
        public static bool DispMessage(HalconDotNet.HTuple hv_WindowHandle, HalconDotNet.HTuple hv_String, HalconDotNet.HTuple hv_CoordSystem, HalconDotNet.HTuple hv_Row, HalconDotNet.HTuple hv_Column, HalconDotNet.HTuple hv_Color, HalconDotNet.HTuple hv_Box)
        {
            bool rt = false;
            try
            {
                HalconDotNet.HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
                HalconDotNet.HTuple hv_Row1Part = null, hv_Column1Part = null, hv_Row2Part = null;
                HalconDotNet.HTuple hv_Column2Part = null, hv_RowWin = null, hv_ColumnWin = null;
                HalconDotNet.HTuple hv_WidthWin = null, hv_HeightWin = null, hv_MaxAscent = null;
                HalconDotNet.HTuple hv_MaxDescent = null, hv_MaxWidth = null, hv_MaxHeight = null;
                HalconDotNet.HTuple hv_R1 = new HalconDotNet.HTuple(), hv_C1 = new HalconDotNet.HTuple(), hv_FactorRow = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_FactorColumn = new HalconDotNet.HTuple(), hv_UseShadow = null;
                HalconDotNet.HTuple hv_ShadowColor = null, hv_Exception = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_Width = new HalconDotNet.HTuple(), hv_Index = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_Ascent = new HalconDotNet.HTuple(), hv_Descent = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_W = new HalconDotNet.HTuple(), hv_H = new HalconDotNet.HTuple(), hv_FrameHeight = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_FrameWidth = new HalconDotNet.HTuple(), hv_R2 = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_C2 = new HalconDotNet.HTuple(), hv_DrawMode = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_CurrentColor = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_Box_COPY_INP_TMP = hv_Box.Clone();
                HalconDotNet.HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
                HalconDotNet.HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
                HalconDotNet.HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
                HalconDotNet.HTuple hv_String_COPY_INP_TMP = hv_String.Clone();

                HalconDotNet.HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
                HalconDotNet.HOperatorSet.GetPart(hv_WindowHandle, out hv_Row1Part, out hv_Column1Part, out hv_Row2Part, out hv_Column2Part);
                HalconDotNet.HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowWin, out hv_ColumnWin, out hv_WidthWin, out hv_HeightWin);
                HalconDotNet.HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
                if ((int)(new HalconDotNet.HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
                {
                    hv_Row_COPY_INP_TMP = 12;
                }
                if ((int)(new HalconDotNet.HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
                {
                    hv_Column_COPY_INP_TMP = 12;
                }
                if ((int)(new HalconDotNet.HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HalconDotNet.HTuple()))) != 0)
                {
                    hv_Color_COPY_INP_TMP = "";
                }

                hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
                HalconDotNet.HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent, out hv_MaxWidth, out hv_MaxHeight);
                if ((int)(new HalconDotNet.HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
                {
                    hv_R1 = hv_Row_COPY_INP_TMP.Clone();
                    hv_C1 = hv_Column_COPY_INP_TMP.Clone();
                }
                else
                {
                    //Transform image to window coordinates
                    hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
                    hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
                    hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
                    hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
                }
                hv_UseShadow = 1;
                hv_ShadowColor = "gray";
                if ((int)(new HalconDotNet.HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleEqual("true"))) != 0)
                {
                    if (hv_Box_COPY_INP_TMP == null)
                        hv_Box_COPY_INP_TMP = new HalconDotNet.HTuple();
                    hv_Box_COPY_INP_TMP[0] = "#fce9d4";
                    hv_ShadowColor = "#f28d26";
                }

                if ((int)(new HalconDotNet.HTuple((new HalconDotNet.HTuple(hv_Box_COPY_INP_TMP.TupleLength())).TupleGreater(1))) != 0)
                {
                    if ((int)(new HalconDotNet.HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual("true"))) != 0)
                    {
                        //Use default ShadowColor set above
                    }
                    else if ((int)(new HalconDotNet.HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual(
                        "false"))) != 0)
                    {
                        hv_UseShadow = 0;
                    }
                    else
                    {
                        hv_ShadowColor = hv_Box_COPY_INP_TMP[1];
                        //Valid color?
                        try
                        {
                            HalconDotNet.HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                                1));
                        }
                        // catch (Exception) 
                        catch (HalconDotNet.HalconException HDevExpDefaultException1)
                        {
                            HDevExpDefaultException1.ToHTuple(out hv_Exception);
                            hv_Exception = "Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)";
                            throw new HalconDotNet.HalconException(hv_Exception);
                        }
                    }
                }

                if ((int)(new HalconDotNet.HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleNotEqual("false"))) != 0)
                {
                    //Valid color?
                    try
                    {
                        HalconDotNet.HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                    }
                    // catch (Exception) 
                    catch (HalconDotNet.HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        hv_Exception = "Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)";
                        throw new HalconDotNet.HalconException(hv_Exception);
                    }
                    //Calculate box extents
                    hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
                    hv_Width = new HalconDotNet.HTuple();
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HalconDotNet.HTuple(hv_String_COPY_INP_TMP.TupleLength()
                        )) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        HalconDotNet.HOperatorSet.GetStringExtents(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                            hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                        hv_Width = hv_Width.TupleConcat(hv_W);
                    }
                    hv_FrameHeight = hv_MaxHeight * (new HalconDotNet.HTuple(hv_String_COPY_INP_TMP.TupleLength()
                        ));
                    hv_FrameWidth = (((new HalconDotNet.HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                    hv_R2 = hv_R1 + hv_FrameHeight;
                    hv_C2 = hv_C1 + hv_FrameWidth;
                    //Display rectangles
                    HalconDotNet.HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
                    HalconDotNet.HOperatorSet.SetDraw(hv_WindowHandle, "fill");
                    //Set shadow color
                    HalconDotNet.HOperatorSet.SetColor(hv_WindowHandle, hv_ShadowColor);
                    if ((int)(hv_UseShadow) != 0)
                    {
                        HalconDotNet.HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1 + 1, hv_C1 + 1, hv_R2 + 1, hv_C2 + 1);
                    }
                    //Set box color
                    HalconDotNet.HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                    HalconDotNet.HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                    HalconDotNet.HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
                }

                //Write text.
                for (hv_Index = 0; (int)hv_Index <= (int)((new HalconDotNet.HTuple(hv_String_COPY_INP_TMP.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                {
                    hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HalconDotNet.HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                        )));
                    if ((int)((new HalconDotNet.HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HalconDotNet.HTuple(hv_CurrentColor.TupleNotEqual(
                        "auto")))) != 0)
                    {
                        HalconDotNet.HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
                    }
                    else
                    {
                        HalconDotNet.HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                    }
                    hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
                    HalconDotNet.HOperatorSet.SetTposition(hv_WindowHandle, hv_Row_COPY_INP_TMP, hv_C1);
                    HalconDotNet.HOperatorSet.WriteString(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                        hv_Index));
                }

                //Reset changed window settings
                HalconDotNet.HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                HalconDotNet.HOperatorSet.SetPart(hv_WindowHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part, hv_Column2Part);
                rt = true;
            }
            catch { }
            finally
            {
            }
            return rt;
        }

        /// <summary>
        /// 在指定窗口显示匹配指定模型的轮廓
        /// </summary>
        /// <param name="hv_WindowHandle">显示窗口</param>
        /// <param name="hv_ModelID">模板句柄</param>
        /// <param name="hv_Color">指定颜色</param>
        /// <param name="hv_Row">匹配坐标Row</param>
        /// <param name="hv_Column">匹配坐标Column</param>
        /// <param name="hv_Angle">匹配角度</param>
        /// <param name="hv_ScaleR">Row向缩放系数</param>
        /// <param name="hv_ScaleC">Column向缩放系数</param>
        /// <param name="hv_Model">匹配模板的索引</param>
        /// <returns></returns>
        public static bool DispMatchModelResults(HalconDotNet.HTuple hv_WindowHandle, HalconDotNet.HTuple hv_ModelID, HalconDotNet.HTuple hv_Color, HalconDotNet.HTuple hv_Row, HalconDotNet.HTuple hv_Column, HalconDotNet.HTuple hv_Angle, HalconDotNet.HTuple hv_ScaleR, HalconDotNet.HTuple hv_ScaleC, HalconDotNet.HTuple hv_Model)
        {
            bool rt = false;

            try
            {
                // Local iconic variables 
                HalconDotNet.HObject ho_ModelContours = null, ho_ContoursAffinTrans = null;

                // Local control variables 
                HalconDotNet.HTuple hv_NumMatches = null, hv_Index = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_Match = new HalconDotNet.HTuple(), hv_HomMat2DIdentity = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_HomMat2DScale = new HalconDotNet.HTuple(), hv_HomMat2DRotate = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_HomMat2DTranslate = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_Model_COPY_INP_TMP = hv_Model.Clone();
                HalconDotNet.HTuple hv_ScaleC_COPY_INP_TMP = hv_ScaleC.Clone();
                HalconDotNet.HTuple hv_ScaleR_COPY_INP_TMP = hv_ScaleR.Clone();

                // Initialize local and output iconic variables 
                HalconDotNet.HOperatorSet.GenEmptyObj(out ho_ModelContours);
                HalconDotNet.HOperatorSet.GenEmptyObj(out ho_ContoursAffinTrans);

                //This procedure displays the results of Shape-Based Matching.
                hv_NumMatches = new HalconDotNet.HTuple(hv_Row.TupleLength());
                if ((int)(new HalconDotNet.HTuple(hv_NumMatches.TupleGreater(0))) != 0)
                {
                    if ((int)(new HalconDotNet.HTuple((new HalconDotNet.HTuple(hv_ScaleR_COPY_INP_TMP.TupleLength())).TupleEqual(
                        1))) != 0)
                    {
                        HalconDotNet.HOperatorSet.TupleGenConst(hv_NumMatches, hv_ScaleR_COPY_INP_TMP, out hv_ScaleR_COPY_INP_TMP);
                    }
                    if ((int)(new HalconDotNet.HTuple((new HalconDotNet.HTuple(hv_ScaleC_COPY_INP_TMP.TupleLength())).TupleEqual(
                        1))) != 0)
                    {
                        HalconDotNet.HOperatorSet.TupleGenConst(hv_NumMatches, hv_ScaleC_COPY_INP_TMP, out hv_ScaleC_COPY_INP_TMP);
                    }
                    if ((int)(new HalconDotNet.HTuple((new HalconDotNet.HTuple(hv_Model_COPY_INP_TMP.TupleLength())).TupleEqual(
                        0))) != 0)
                    {
                        HalconDotNet.HOperatorSet.TupleGenConst(hv_NumMatches, 0, out hv_Model_COPY_INP_TMP);
                    }
                    else if ((int)(new HalconDotNet.HTuple((new HalconDotNet.HTuple(hv_Model_COPY_INP_TMP.TupleLength()
                        )).TupleEqual(1))) != 0)
                    {
                        HalconDotNet.HOperatorSet.TupleGenConst(hv_NumMatches, hv_Model_COPY_INP_TMP, out hv_Model_COPY_INP_TMP);
                    }
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HalconDotNet.HTuple(hv_ModelID.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        ho_ModelContours.Dispose();
                        HalconDotNet.HOperatorSet.GetShapeModelContours(out ho_ModelContours, hv_ModelID.TupleSelect(
                            hv_Index), 1);
                        HalconDotNet.HOperatorSet.SetColor(hv_WindowHandle, hv_Color.TupleSelect(hv_Index % (new HalconDotNet.HTuple(hv_Color.TupleLength()
                            ))));
                        HalconDotNet.HTuple end_val18 = hv_NumMatches - 1;
                        HalconDotNet.HTuple step_val18 = 1;
                        for (hv_Match = 0; hv_Match.Continue(end_val18, step_val18); hv_Match = hv_Match.TupleAdd(step_val18))
                        {
                            if ((int)(new HalconDotNet.HTuple(hv_Index.TupleEqual(hv_Model_COPY_INP_TMP.TupleSelect(
                                hv_Match)))) != 0)
                            {
                                HalconDotNet.HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
                                HalconDotNet.HOperatorSet.HomMat2dScale(hv_HomMat2DIdentity, hv_ScaleR_COPY_INP_TMP.TupleSelect(
                                    hv_Match), hv_ScaleC_COPY_INP_TMP.TupleSelect(hv_Match), 0, 0, out hv_HomMat2DScale);
                                HalconDotNet.HOperatorSet.HomMat2dRotate(hv_HomMat2DScale, hv_Angle.TupleSelect(hv_Match),
                                    0, 0, out hv_HomMat2DRotate);
                                HalconDotNet.HOperatorSet.HomMat2dTranslate(hv_HomMat2DRotate, hv_Row.TupleSelect(
                                    hv_Match), hv_Column.TupleSelect(hv_Match), out hv_HomMat2DTranslate);
                                ho_ContoursAffinTrans.Dispose();
                                HalconDotNet.HOperatorSet.AffineTransContourXld(ho_ModelContours, out ho_ContoursAffinTrans,
                                    hv_HomMat2DTranslate);
                                HalconDotNet.HOperatorSet.DispObj(ho_ContoursAffinTrans, hv_WindowHandle);
                            }
                        }
                    }

                    rt = true;
                }
                ho_ModelContours.Dispose();
                ho_ContoursAffinTrans.Dispose();
            }
#pragma warning disable CS0168 // The variable 'hex' is declared but never used
            catch (HalconDotNet.HalconException hex)
#pragma warning restore CS0168 // The variable 'hex' is declared but never used
            {
            }
            finally { }
            return rt;
        }

        /// <summary>
        /// 设置显示字体
        /// </summary>
        /// <param name="hv_WindowHandle">窗口句柄</param>
        /// <param name="hv_Size">字体大小</param>
        /// <param name="hv_Font">字体名称</param>
        /// <param name="hv_Bold">字体加粗</param>
        /// <param name="hv_Slant">字体倾斜</param>
        /// <returns></returns>
        public static bool SetDisplayFont(HalconDotNet.HTuple hv_WindowHandle, HalconDotNet.HTuple hv_Size, HalconDotNet.HTuple hv_Font, HalconDotNet.HTuple hv_Bold, HalconDotNet.HTuple hv_Slant)
        {
            bool rt = false;
            try
            {
                HalconDotNet.HTuple hv_OS = null, hv_BufferWindowHandle = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_Ascent = new HalconDotNet.HTuple(), hv_Descent = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_Width = new HalconDotNet.HTuple(), hv_Height = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_Scale = new HalconDotNet.HTuple(), hv_Exception = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_SubFamily = new HalconDotNet.HTuple(), hv_Fonts = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_SystemFonts = new HalconDotNet.HTuple(), hv_Guess = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_I = new HalconDotNet.HTuple(), hv_Index = new HalconDotNet.HTuple(), hv_AllowedFontSizes = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_Distances = new HalconDotNet.HTuple(), hv_Indices = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_FontSelRegexp = new HalconDotNet.HTuple(), hv_FontsCourier = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_Bold_COPY_INP_TMP = hv_Bold.Clone();
                HalconDotNet.HTuple hv_Font_COPY_INP_TMP = hv_Font.Clone();
                HalconDotNet.HTuple hv_Size_COPY_INP_TMP = hv_Size.Clone();
                HalconDotNet.HTuple hv_Slant_COPY_INP_TMP = hv_Slant.Clone();

                HalconDotNet.HOperatorSet.GetSystem("operating_system", out hv_OS);
                if ((int)((new HalconDotNet.HTuple(hv_Size_COPY_INP_TMP.TupleEqual(new HalconDotNet.HTuple()))).TupleOr(new HalconDotNet.HTuple(hv_Size_COPY_INP_TMP.TupleEqual(-1)))) != 0)
                {
                    hv_Size_COPY_INP_TMP = 16;
                }

                if ((int)(new HalconDotNet.HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    //Set font on Windows systems
                    try
                    {
                        //Check, if font scaling is switched on
                        HalconDotNet.HOperatorSet.OpenWindow(0, 0, 256, 256, 0, "buffer", "", out hv_BufferWindowHandle);
                        HalconDotNet.HOperatorSet.SetFont(hv_BufferWindowHandle, "-Consolas-16-*-0-*-*-1-");
                        HalconDotNet.HOperatorSet.GetStringExtents(hv_BufferWindowHandle, "test_string", out hv_Ascent,
                            out hv_Descent, out hv_Width, out hv_Height);
                        //Expected width is 110
                        hv_Scale = 110.0 / hv_Width;
                        hv_Size_COPY_INP_TMP = ((hv_Size_COPY_INP_TMP * hv_Scale)).TupleInt();
                        HalconDotNet.HOperatorSet.CloseWindow(hv_BufferWindowHandle);
                    }
                    // catch (Exception) 
                    catch (HalconDotNet.HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        //throw (Exception)
                    }
                    if ((int)((new HalconDotNet.HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))).TupleOr(
                        new HalconDotNet.HTuple(hv_Font_COPY_INP_TMP.TupleEqual("courier")))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "Courier New";
                    }
                    else if ((int)(new HalconDotNet.HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "Consolas";
                    }
                    else if ((int)(new HalconDotNet.HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "Arial";
                    }
                    else if ((int)(new HalconDotNet.HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "Times New Roman";
                    }
                    if ((int)(new HalconDotNet.HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        hv_Bold_COPY_INP_TMP = 1;
                    }
                    else if ((int)(new HalconDotNet.HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("false"))) != 0)
                    {
                        hv_Bold_COPY_INP_TMP = 0;
                    }
                    else
                    {
                        hv_Exception = "Wrong value of control parameter Bold";
                        throw new HalconDotNet.HalconException(hv_Exception);
                    }
                    if ((int)(new HalconDotNet.HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        hv_Slant_COPY_INP_TMP = 1;
                    }
                    else if ((int)(new HalconDotNet.HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("false"))) != 0)
                    {
                        hv_Slant_COPY_INP_TMP = 0;
                    }
                    else
                    {
                        hv_Exception = "Wrong value of control parameter Slant";
                        throw new HalconDotNet.HalconException(hv_Exception);
                    }
                    try
                    {
                        HalconDotNet.HOperatorSet.SetFont(hv_WindowHandle, ((((((("-" + hv_Font_COPY_INP_TMP) + "-") + hv_Size_COPY_INP_TMP) + "-*-") + hv_Slant_COPY_INP_TMP) + "-*-*-") + hv_Bold_COPY_INP_TMP) + "-");
                    }
                    // catch (Exception) 
                    catch (HalconDotNet.HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        //throw (Exception)
                    }
                }
                else if ((int)(new HalconDotNet.HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Dar"))) != 0)
                {
                    //Set font on Mac OS X systems. Since OS X does not have a strict naming
                    //scheme for font attributes, we use tables to determine the correct font
                    //name.
                    hv_SubFamily = 0;
                    if ((int)(new HalconDotNet.HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        hv_SubFamily = hv_SubFamily.TupleBor(1);
                    }
                    else if ((int)(new HalconDotNet.HTuple(hv_Slant_COPY_INP_TMP.TupleNotEqual("false"))) != 0)
                    {
                        hv_Exception = "Wrong value of control parameter Slant";
                        throw new HalconDotNet.HalconException(hv_Exception);
                    }
                    if ((int)(new HalconDotNet.HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        hv_SubFamily = hv_SubFamily.TupleBor(2);
                    }
                    else if ((int)(new HalconDotNet.HTuple(hv_Bold_COPY_INP_TMP.TupleNotEqual("false"))) != 0)
                    {
                        hv_Exception = "Wrong value of control parameter Bold";
                        throw new HalconDotNet.HalconException(hv_Exception);
                    }
                    if ((int)(new HalconDotNet.HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
                    {
                        hv_Fonts = new HalconDotNet.HTuple();
                        hv_Fonts[0] = "Menlo-Regular";
                        hv_Fonts[1] = "Menlo-Italic";
                        hv_Fonts[2] = "Menlo-Bold";
                        hv_Fonts[3] = "Menlo-BoldItalic";
                    }
                    else if ((int)((new HalconDotNet.HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))).TupleOr(
                        new HalconDotNet.HTuple(hv_Font_COPY_INP_TMP.TupleEqual("courier")))) != 0)
                    {
                        hv_Fonts = new HalconDotNet.HTuple();
                        hv_Fonts[0] = "CourierNewPSMT";
                        hv_Fonts[1] = "CourierNewPS-ItalicMT";
                        hv_Fonts[2] = "CourierNewPS-BoldMT";
                        hv_Fonts[3] = "CourierNewPS-BoldItalicMT";
                    }
                    else if ((int)(new HalconDotNet.HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                    {
                        hv_Fonts = new HalconDotNet.HTuple();
                        hv_Fonts[0] = "ArialMT";
                        hv_Fonts[1] = "Arial-ItalicMT";
                        hv_Fonts[2] = "Arial-BoldMT";
                        hv_Fonts[3] = "Arial-BoldItalicMT";
                    }
                    else if ((int)(new HalconDotNet.HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                    {
                        hv_Fonts = new HalconDotNet.HTuple();
                        hv_Fonts[0] = "TimesNewRomanPSMT";
                        hv_Fonts[1] = "TimesNewRomanPS-ItalicMT";
                        hv_Fonts[2] = "TimesNewRomanPS-BoldMT";
                        hv_Fonts[3] = "TimesNewRomanPS-BoldItalicMT";
                    }
                    else
                    {
                        //Attempt to figure out which of the fonts installed on the system
                        //the user could have meant.
                        HalconDotNet.HOperatorSet.QueryFont(hv_WindowHandle, out hv_SystemFonts);
                        hv_Fonts = new HalconDotNet.HTuple();
                        hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                        hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                        hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                        hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                        hv_Guess = new HalconDotNet.HTuple();
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP);
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Regular");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "MT");
                        for (hv_I = 0; (int)hv_I <= (int)((new HalconDotNet.HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                        {
                            HalconDotNet.HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                            if ((int)(new HalconDotNet.HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                            {
                                if (hv_Fonts == null)
                                    hv_Fonts = new HalconDotNet.HTuple();
                                hv_Fonts[0] = hv_Guess.TupleSelect(hv_I);
                                break;
                            }
                        }
                        //Guess name of slanted font
                        hv_Guess = new HalconDotNet.HTuple();
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Italic");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-ItalicMT");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Oblique");
                        for (hv_I = 0; (int)hv_I <= (int)((new HalconDotNet.HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                        {
                            HalconDotNet.HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                            if ((int)(new HalconDotNet.HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                            {
                                if (hv_Fonts == null)
                                    hv_Fonts = new HalconDotNet.HTuple();
                                hv_Fonts[1] = hv_Guess.TupleSelect(hv_I);
                                break;
                            }
                        }
                        //Guess name of bold font
                        hv_Guess = new HalconDotNet.HTuple();
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Bold");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldMT");
                        for (hv_I = 0; (int)hv_I <= (int)((new HalconDotNet.HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                        {
                            HalconDotNet.HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                            if ((int)(new HalconDotNet.HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                            {
                                if (hv_Fonts == null)
                                    hv_Fonts = new HalconDotNet.HTuple();
                                hv_Fonts[2] = hv_Guess.TupleSelect(hv_I);
                                break;
                            }
                        }
                        //Guess name of bold slanted font
                        hv_Guess = new HalconDotNet.HTuple();
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldItalic");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldItalicMT");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldOblique");
                        for (hv_I = 0; (int)hv_I <= (int)((new HalconDotNet.HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                        {
                            HalconDotNet.HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                            if ((int)(new HalconDotNet.HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                            {
                                if (hv_Fonts == null)
                                    hv_Fonts = new HalconDotNet.HTuple();
                                hv_Fonts[3] = hv_Guess.TupleSelect(hv_I);
                                break;
                            }
                        }
                    }
                    hv_Font_COPY_INP_TMP = hv_Fonts.TupleSelect(hv_SubFamily);
                    try
                    {
                        HalconDotNet.HOperatorSet.SetFont(hv_WindowHandle, (hv_Font_COPY_INP_TMP + "-") + hv_Size_COPY_INP_TMP);
                    }
                    // catch (Exception) 
                    catch (HalconDotNet.HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        //throw (Exception)
                    }
                }
                else
                {
                    //Set font for UNIX systems
                    hv_Size_COPY_INP_TMP = hv_Size_COPY_INP_TMP * 1.25;
                    hv_AllowedFontSizes = new HalconDotNet.HTuple();
                    hv_AllowedFontSizes[0] = 11;
                    hv_AllowedFontSizes[1] = 14;
                    hv_AllowedFontSizes[2] = 17;
                    hv_AllowedFontSizes[3] = 20;
                    hv_AllowedFontSizes[4] = 25;
                    hv_AllowedFontSizes[5] = 34;
                    if ((int)(new HalconDotNet.HTuple(((hv_AllowedFontSizes.TupleFind(hv_Size_COPY_INP_TMP))).TupleEqual(
                        -1))) != 0)
                    {
                        hv_Distances = ((hv_AllowedFontSizes - hv_Size_COPY_INP_TMP)).TupleAbs();
                        HalconDotNet.HOperatorSet.TupleSortIndex(hv_Distances, out hv_Indices);
                        hv_Size_COPY_INP_TMP = hv_AllowedFontSizes.TupleSelect(hv_Indices.TupleSelect(
                            0));
                    }
                    if ((int)((new HalconDotNet.HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))).TupleOr(new HalconDotNet.HTuple(hv_Font_COPY_INP_TMP.TupleEqual(
                        "Courier")))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "courier";
                    }
                    else if ((int)(new HalconDotNet.HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "helvetica";
                    }
                    else if ((int)(new HalconDotNet.HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "times";
                    }
                    if ((int)(new HalconDotNet.HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        hv_Bold_COPY_INP_TMP = "bold";
                    }
                    else if ((int)(new HalconDotNet.HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("false"))) != 0)
                    {
                        hv_Bold_COPY_INP_TMP = "medium";
                    }
                    else
                    {
                        hv_Exception = "Wrong value of control parameter Bold";
                        throw new HalconDotNet.HalconException(hv_Exception);
                    }
                    if ((int)(new HalconDotNet.HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        if ((int)(new HalconDotNet.HTuple(hv_Font_COPY_INP_TMP.TupleEqual("times"))) != 0)
                        {
                            hv_Slant_COPY_INP_TMP = "i";
                        }
                        else
                        {
                            hv_Slant_COPY_INP_TMP = "o";
                        }
                    }
                    else if ((int)(new HalconDotNet.HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("false"))) != 0)
                    {
                        hv_Slant_COPY_INP_TMP = "r";
                    }
                    else
                    {
                        hv_Exception = "Wrong value of control parameter Slant";
                        throw new HalconDotNet.HalconException(hv_Exception);
                    }
                    try
                    {
                        HalconDotNet.HOperatorSet.SetFont(hv_WindowHandle, ((((((("-adobe-" + hv_Font_COPY_INP_TMP) + "-") + hv_Bold_COPY_INP_TMP) + "-") + hv_Slant_COPY_INP_TMP) + "-normal-*-") + hv_Size_COPY_INP_TMP) + "-*-*-*-*-*-*-*");
                    }
                    // catch (Exception) 
                    catch (HalconDotNet.HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        if ((int)((new HalconDotNet.HTuple(((hv_OS.TupleSubstr(0, 4))).TupleEqual("Linux"))).TupleAnd(
                            new HalconDotNet.HTuple(hv_Font_COPY_INP_TMP.TupleEqual("courier")))) != 0)
                        {
                            HalconDotNet.HOperatorSet.QueryFont(hv_WindowHandle, out hv_Fonts);
                            hv_FontSelRegexp = (("^-[^-]*-[^-]*[Cc]ourier[^-]*-" + hv_Bold_COPY_INP_TMP) + "-") + hv_Slant_COPY_INP_TMP;
                            hv_FontsCourier = ((hv_Fonts.TupleRegexpSelect(hv_FontSelRegexp))).TupleRegexpMatch(
                                hv_FontSelRegexp);
                            if ((int)(new HalconDotNet.HTuple((new HalconDotNet.HTuple(hv_FontsCourier.TupleLength())).TupleEqual(
                                0))) != 0)
                            {
                                hv_Exception = "Wrong font name";
                                //throw (Exception)
                            }
                            else
                            {
                                try
                                {
                                    HalconDotNet.HOperatorSet.SetFont(hv_WindowHandle, (((hv_FontsCourier.TupleSelect(
                                        0)) + "-normal-*-") + hv_Size_COPY_INP_TMP) + "-*-*-*-*-*-*-*");
                                }
                                // catch (Exception) 
                                catch (HalconDotNet.HalconException HDevExpDefaultException2)
                                {
                                    HDevExpDefaultException2.ToHTuple(out hv_Exception);
                                    //throw (Exception)
                                }
                            }
                        }
                        //throw (Exception)
                    }
                }

                rt = true;
            }
            catch { }
            finally
            {
            }
            return rt;
        }

        /// <summary>
        /// 产生箭头轮廓
        /// </summary>
        /// <param name="ho_Arrow">输出图形变量</param>
        /// <param name="hv_Row1">箭头起始点行坐标</param>
        /// <param name="hv_Column1">箭头起始点列坐标</param>
        /// <param name="hv_Row2">箭头终止点行坐标</param>
        /// <param name="hv_Column2">箭头终止点列坐标</param>
        /// <param name="hv_HeadLength">箭头长度</param>
        /// <param name="hv_HeadWidth">箭头宽度</param>
        /// <returns></returns>
        public static bool GenArrowContourXLD(out HalconDotNet.HObject ho_Arrow, HalconDotNet.HTuple hv_Row1, HalconDotNet.HTuple hv_Column1, HalconDotNet.HTuple hv_Row2, HalconDotNet.HTuple hv_Column2, HalconDotNet.HTuple hv_HeadLength, HalconDotNet.HTuple hv_HeadWidth)
        {
            bool rt = false;

            // Stack for temporary objects 
            HalconDotNet.HObject[] OTemp = new HalconDotNet.HObject[20];

            // Local iconic variables 
            HalconDotNet.HObject ho_TempArrow = null;

            // Local control variables 
            HalconDotNet.HTuple hv_Length = null, hv_ZeroLengthIndices = null;
            HalconDotNet.HTuple hv_DR = null, hv_DC = null, hv_HalfHeadWidth = null;
            HalconDotNet.HTuple hv_RowP1 = null, hv_ColP1 = null, hv_RowP2 = null;
            HalconDotNet.HTuple hv_ColP2 = null, hv_Index = null;

            // Initialize local and output iconic variables
            HalconDotNet.HOperatorSet.GenEmptyObj(out ho_Arrow);
            HalconDotNet.HOperatorSet.GenEmptyObj(out ho_TempArrow);

            try
            {
                //Init
                ho_Arrow.Dispose();
                HalconDotNet.HOperatorSet.GenEmptyObj(out ho_Arrow);

                //Calculate the arrow length
                HalconDotNet.HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Length);

                //Mark arrows with identical start and end point
                //(set Length to -1 to avoid division-by-zero exception)
                hv_ZeroLengthIndices = hv_Length.TupleFind(0);
                if ((int)(new HalconDotNet.HTuple(hv_ZeroLengthIndices.TupleNotEqual(-1))) != 0)
                {
                    if (hv_Length == null)
                        hv_Length = new HalconDotNet.HTuple();
                    hv_Length[hv_ZeroLengthIndices] = -1;
                }

                //Calculate auxiliary variables.
                hv_DR = (1.0 * (hv_Row2 - hv_Row1)) / hv_Length;
                hv_DC = (1.0 * (hv_Column2 - hv_Column1)) / hv_Length;
                hv_HalfHeadWidth = hv_HeadWidth / 2.0;

                //Calculate end points of the arrow head.
                hv_RowP1 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) + (hv_HalfHeadWidth * hv_DC);
                hv_ColP1 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) - (hv_HalfHeadWidth * hv_DR);
                hv_RowP2 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) - (hv_HalfHeadWidth * hv_DC);
                hv_ColP2 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) + (hv_HalfHeadWidth * hv_DR);

                //Finally create output XLD contour for each input point pair
                for (hv_Index = 0; (int)hv_Index <= (int)((new HalconDotNet.HTuple(hv_Length.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                {
                    if ((int)(new HalconDotNet.HTuple(((hv_Length.TupleSelect(hv_Index))).TupleEqual(-1))) != 0)
                    {
                        //Create_ single points for arrows with identical start and end point
                        ho_TempArrow.Dispose();
                        HalconDotNet.HOperatorSet.GenContourPolygonXld(out ho_TempArrow, hv_Row1.TupleSelect(
                            hv_Index), hv_Column1.TupleSelect(hv_Index));
                    }
                    else
                    {
                        //Create arrow contour
                        ho_TempArrow.Dispose();
                        HalconDotNet.HOperatorSet.GenContourPolygonXld(out ho_TempArrow, ((((((((((hv_Row1.TupleSelect(
                            hv_Index))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                            hv_RowP1.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                            hv_RowP2.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)),
                            ((((((((((hv_Column1.TupleSelect(hv_Index))).TupleConcat(hv_Column2.TupleSelect(
                            hv_Index)))).TupleConcat(hv_ColP1.TupleSelect(hv_Index)))).TupleConcat(
                            hv_Column2.TupleSelect(hv_Index)))).TupleConcat(hv_ColP2.TupleSelect(
                            hv_Index)))).TupleConcat(hv_Column2.TupleSelect(hv_Index)));
                    }
                    {
                        HalconDotNet.HObject ExpTmpOutVar_0;
                        HalconDotNet.HOperatorSet.ConcatObj(ho_Arrow, ho_TempArrow, out ExpTmpOutVar_0);
                        ho_Arrow.Dispose();
                        ho_Arrow = ExpTmpOutVar_0;
                    }
                }
                ho_TempArrow.Dispose();

                rt = true;
            }
            catch
            {
            }
            finally
            {
                ho_TempArrow.Dispose();
            }
            return rt;
        }

        /// <summary>
        /// 基于模板匹配获取匹配对象的像素坐标
        /// </summary>
        /// <param name="hobj">图像</param>
        /// <param name="mdlID">模板匹配模型ID</param>
        /// <param name="mmp">模板匹配参数</param>
        /// <returns></returns>
        public static bool MatchModelAndGetCoordinate(HalconDotNet.HObject hobj, HalconDotNet.HTuple mdlID, Communal.MatchModelParameter mmp,
            out HalconDotNet.HTuple row, out HalconDotNet.HTuple col, out HalconDotNet.HTuple angle, out HalconDotNet.HTuple scale, out HalconDotNet.HTuple score, out HalconDotNet.HTuple modellevel)
        {
            bool rt = false;
            row = new HalconDotNet.HTuple();
            col = new HalconDotNet.HTuple();
            angle = new HalconDotNet.HTuple();
            scale = new HalconDotNet.HTuple();
            score = new HalconDotNet.HTuple();
            modellevel = new HalconDotNet.HTuple();

            if (mdlID != -1 && mmp.NumToMatch > 0)
            {
                try
                {
                    HalconDotNet.HOperatorSet.FindShapeModel(hobj, mdlID, mmp.StartAngle, mmp.AngleExtent, mmp.MinScore, mmp.NumToMatch, mmp.MaxOverLap, mmp.SubPixel, mmp.NumLevels, mmp.Greediness,
                  out row, out col, out angle, out score);

                    HalconDotNet.HOperatorSet.FindScaledShapeModel(hobj, mdlID, mmp.StartAngle, mmp.AngleExtent, mmp.ScaleMin, mmp.ScaleMax, mmp.MinScore, mmp.NumToMatch, mmp.MaxOverLap, mmp.SubPixel, mmp.NumLevels, mmp.Greediness,
                        out row, out col, out angle, out scale, out score);

                    if (score.TupleIsInt()
                        && score.TupleLength() > 0)
                        rt = true;
                }
#pragma warning disable CS0168 // The variable 'hex' is declared but never used
                catch (HalconDotNet.HalconException hex) { }
#pragma warning restore CS0168 // The variable 'hex' is declared but never used
                finally { }
            }
            return rt;
        }

        /// <summary>
        /// 计算在指定模式下变化角与参考角之间重合时的旋转补偿值
        /// </summary>
        /// <param name="variationalAngle">变化角(弧度),取值范围[-Π,Π]</param>
        /// <param name="referenceAngle">参考角(弧度),取值范围[-Π,Π]</param>
        /// <param name="mode">
        /// 模式,即在坐标系中角向量的旋转方式
        /// 0--最少旋转，允许逆时针或顺时针,此时转角幅度最小(可正可负)
        /// 1--单向旋转，仅允许逆时针旋转,此时角为正值
        /// 2--单向旋转，仅允许顺时针旋转,此时角为负值</param>
        /// <param name="compensationAngle">指定模式下旋转补偿值(弧度),单向旋转时取值范围[-2Π,2Π],最少旋转时取值范围[-Π,Π]</param>
        /// <returns></returns>
        public static bool CalculateAngleCompensation(HalconDotNet.HTuple variationalAngle, HalconDotNet.HTuple referenceAngle, HalconDotNet.HTuple mode, out HalconDotNet.HTuple compensationAngle,out HalconDotNet.HTuple rotateDirection)
        {
            bool rt = false; compensationAngle = new HalconDotNet.HTuple(); rotateDirection = new HalconDotNet.HTuple(0);
            try
            {
                if (variationalAngle != null
                    && variationalAngle.TupleNotEqual(new HalconDotNet.HTuple()))
                {
                    if (referenceAngle != null
                    && referenceAngle.TupleNotEqual(new HalconDotNet.HTuple()))
                    {
                        if (mode == null
                            || (mode.TupleEqual(new HalconDotNet.HTuple())))
                        {
                            mode = new HalconDotNet.HTuple(0);
                        }

                        HalconDotNet.HTuple tmpOffset = referenceAngle - variationalAngle;
                        HalconDotNet.HTuple tmpCounterClk, tmpClk, rad2Pi, abs1, abs2;

                        //变化角超前参考角
                        if (variationalAngle[0].D >= referenceAngle[0].D)
                        {
                            HalconDotNet.HOperatorSet.TupleRad(new HalconDotNet.HTuple(360), out rad2Pi);
                            tmpCounterClk = tmpOffset + rad2Pi;
                            tmpClk = tmpOffset;

                            switch (mode[0].I)
                            {
                                case 1:
                                    compensationAngle = tmpCounterClk;
                                    rotateDirection = new HalconDotNet.HTuple(1);
                                    break;
                                case 2:
                                    compensationAngle = tmpClk;
                                    rotateDirection = new HalconDotNet.HTuple(2);
                                    break;
                                default:
                                    {
                                        HalconDotNet.HOperatorSet.TupleAbs(tmpCounterClk, out abs1);
                                        HalconDotNet.HOperatorSet.TupleAbs(tmpClk, out abs2);
                                        if (abs1[0].D <= abs2[0].D)
                                        {
                                            compensationAngle = tmpCounterClk; rotateDirection = new HalconDotNet.HTuple(1);
                                        }
                                        else { compensationAngle = tmpClk; rotateDirection = new HalconDotNet.HTuple(2); }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            HalconDotNet.HOperatorSet.TupleRad(new HalconDotNet.HTuple(360), out rad2Pi);
                            tmpCounterClk = tmpOffset;
                            tmpClk = tmpOffset - rad2Pi;

                            switch (mode[0].I)
                            {
                                case 1:
                                    compensationAngle = tmpCounterClk;
                                    rotateDirection = new HalconDotNet.HTuple(1);
                                    break;
                                case 2:
                                    compensationAngle = tmpClk;
                                    rotateDirection = new HalconDotNet.HTuple(2);
                                    break;
                                default:
                                    {
                                        HalconDotNet.HOperatorSet.TupleAbs(tmpCounterClk, out abs1);
                                        HalconDotNet.HOperatorSet.TupleAbs(tmpClk, out abs2);
                                        if (abs1[0].D <= abs2[0].D)
                                        {
                                            compensationAngle = tmpCounterClk;
                                            rotateDirection = new HalconDotNet.HTuple(1);
                                        }
                                        else { compensationAngle = tmpClk; rotateDirection = new HalconDotNet.HTuple(2); }
                                    }
                                    break;
                            }
                        }

                        rt = true;
                    }
                }
            }
            catch (HalconDotNet.HalconException hex) { }
            finally { }

            return rt;
        }

        /// <summary>
        /// 计算指定向量在指定模式下重合时的旋转补偿值
        /// </summary>
        /// <param name="variableX1">变化向量第一轴坐标</param>
        /// <param name="variableY1">变化向量第二轴坐标</param>
        /// <param name="variableX2">参考向量第一轴坐标</param>
        /// <param name="variableY2">参考向量第二轴坐标</param>
        /// <param name="mode">
        /// 模式,即在坐标系中角向量的旋转方式
        /// 0--最少旋转，允许逆时针或顺时针,此时转角幅度最小(可正可负)
        /// 1--单向旋转，仅允许逆时针旋转,此时角为正值
        /// 2--单向旋转，仅允许顺时针旋转,此时角为负值</param></param>
        /// <param name="compensationAngle">指定模式下旋转补偿值(弧度),单向旋转时取值范围[-2Π,2Π],最少旋转时取值范围[-Π,Π]</param>
        public static void CalculateAngleCompensation(HalconDotNet.HTuple variableX1, HalconDotNet.HTuple variableY1, HalconDotNet.HTuple variableX2, HalconDotNet.HTuple variableY2, HalconDotNet.HTuple mode, out HalconDotNet.HTuple compensationAngle,out HalconDotNet.HTuple rotateDirection)
        {
            compensationAngle = new HalconDotNet.HTuple(0);

            //注意计算出的向量角,其描述范围在[-Π/2,π/2],不一定适用以下角描述范围在[-π,π]的函数计算中
            HalconDotNet.HTuple variableAngle = Math.Atan(variableY1.D / variableX1.D); //适用一四象限
            if (variableX1.D < 0)
            {
                if (variableY1.D >= 0)
                {
                    //第二象限,补偿Π,使其在角描述范围[-π,π]
                    variableAngle = variableAngle+System.Math.PI;
                }
                else 
                {
                    //第三象限,补偿-Π,使其在角描述范围[-π,π]
                    variableAngle = variableAngle - System.Math.PI;
                }
            }

            HalconDotNet.HTuple refAngle = Math.Atan(variableY2.D / variableX2.D);
            if (variableX2.D < 0)
            {
                if (variableY2.D >=0)
                {
                    //第二象限,补偿Π,使其在角描述范围[-π,π]
                    refAngle = -refAngle + System.Math.PI;
                }
                else
                {
                    //第三象限,补偿-Π,使其在角描述范围[-π,π]
                    refAngle = refAngle - System.Math.PI;
                }
            }
            CalculateAngleCompensation(variableAngle, refAngle, mode, out compensationAngle,out rotateDirection);
        }

        /// <summary>
        /// 指定目录下列举图像文件
        /// </summary>
        /// <param name="hv_ImageDirectory"></param>
        /// <param name="hv_Extensions"></param>
        /// <param name="hv_Options"></param>
        /// <param name="hv_ImageFiles"></param>
        /// <returns></returns>
        public static bool ListImageFile(HalconDotNet.HTuple hv_ImageDirectory, HalconDotNet.HTuple hv_Extensions, HalconDotNet.HTuple hv_Options, out HalconDotNet.HTuple hv_ImageFiles)
        {
            bool rt = false;
            hv_ImageFiles = new HalconDotNet.HTuple();
            try
            {
                HalconDotNet.HTuple hv_HalconImages = null, hv_OS = null;
                HalconDotNet.HTuple hv_Directories = null, hv_Index = null, hv_Length = null;
                HalconDotNet.HTuple hv_network_drive = null, hv_Substring = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_FileExists = new HalconDotNet.HTuple(), hv_AllFiles = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_i = new HalconDotNet.HTuple(), hv_Selection = new HalconDotNet.HTuple();
                HalconDotNet.HTuple hv_Extensions_COPY_INP_TMP = hv_Extensions.Clone();
                HalconDotNet.HTuple hv_ImageDirectory_COPY_INP_TMP = hv_ImageDirectory.Clone();
                if ((int)((new HalconDotNet.HTuple((new HalconDotNet.HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(new HalconDotNet.HTuple()))).TupleOr(
                    new HalconDotNet.HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(""))))).TupleOr(new HalconDotNet.HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(
                    "default")))) != 0)
                {
                    hv_Extensions_COPY_INP_TMP = new HalconDotNet.HTuple();
                    hv_Extensions_COPY_INP_TMP[0] = "ima";
                    hv_Extensions_COPY_INP_TMP[1] = "tif";
                    hv_Extensions_COPY_INP_TMP[2] = "tiff";
                    hv_Extensions_COPY_INP_TMP[3] = "gif";
                    hv_Extensions_COPY_INP_TMP[4] = "bmp";
                    hv_Extensions_COPY_INP_TMP[5] = "jpg";
                    hv_Extensions_COPY_INP_TMP[6] = "jpeg";
                    hv_Extensions_COPY_INP_TMP[7] = "jp2";
                    hv_Extensions_COPY_INP_TMP[8] = "jxr";
                    hv_Extensions_COPY_INP_TMP[9] = "png";
                    hv_Extensions_COPY_INP_TMP[10] = "pcx";
                    hv_Extensions_COPY_INP_TMP[11] = "ras";
                    hv_Extensions_COPY_INP_TMP[12] = "xwd";
                    hv_Extensions_COPY_INP_TMP[13] = "pbm";
                    hv_Extensions_COPY_INP_TMP[14] = "pnm";
                    hv_Extensions_COPY_INP_TMP[15] = "pgm";
                    hv_Extensions_COPY_INP_TMP[16] = "ppm";
                }
                if ((int)(new HalconDotNet.HTuple(hv_ImageDirectory_COPY_INP_TMP.TupleEqual(""))) != 0)
                {
                    hv_ImageDirectory_COPY_INP_TMP = ".";
                }
                HalconDotNet.HOperatorSet.GetSystem("image_dir", out hv_HalconImages);
                HalconDotNet.HOperatorSet.GetSystem("operating_system", out hv_OS);
                if ((int)(new HalconDotNet.HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    hv_HalconImages = hv_HalconImages.TupleSplit(";");
                }
                else
                {
                    hv_HalconImages = hv_HalconImages.TupleSplit(":");
                }
                hv_Directories = hv_ImageDirectory_COPY_INP_TMP.Clone();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HalconDotNet.HTuple(hv_HalconImages.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    hv_Directories = hv_Directories.TupleConcat(((hv_HalconImages.TupleSelect(hv_Index)) + "/") + hv_ImageDirectory_COPY_INP_TMP);
                }
                HalconDotNet.HOperatorSet.TupleStrlen(hv_Directories, out hv_Length);
                HalconDotNet.HOperatorSet.TupleGenConst(new HalconDotNet.HTuple(hv_Length.TupleLength()), 0, out hv_network_drive);
                if ((int)(new HalconDotNet.HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HalconDotNet.HTuple(hv_Length.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        if ((int)(new HalconDotNet.HTuple(((((hv_Directories.TupleSelect(hv_Index))).TupleStrlen()
                            )).TupleGreater(1))) != 0)
                        {
                            HalconDotNet.HOperatorSet.TupleStrFirstN(hv_Directories.TupleSelect(hv_Index), 1, out hv_Substring);
                            if ((int)(new HalconDotNet.HTuple(hv_Substring.TupleEqual("//"))) != 0)
                            {
                                if (hv_network_drive == null)
                                    hv_network_drive = new HalconDotNet.HTuple();
                                hv_network_drive[hv_Index] = 1;
                            }
                        }
                    }
                }

                for (hv_Index = 0; (int)hv_Index <= (int)((new HalconDotNet.HTuple(hv_Directories.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    HalconDotNet.HOperatorSet.FileExists(hv_Directories.TupleSelect(hv_Index), out hv_FileExists);
                    if ((int)(hv_FileExists) != 0)
                    {
                        HalconDotNet.HOperatorSet.ListFiles(hv_Directories.TupleSelect(hv_Index), (new HalconDotNet.HTuple("files")).TupleConcat(
                            hv_Options), out hv_AllFiles);
                        hv_ImageFiles = new HalconDotNet.HTuple();
                        for (hv_i = 0; (int)hv_i <= (int)((new HalconDotNet.HTuple(hv_Extensions_COPY_INP_TMP.TupleLength()
                            )) - 1); hv_i = (int)hv_i + 1)
                        {
                            HalconDotNet.HOperatorSet.TupleRegexpSelect(hv_AllFiles, (((".*" + (hv_Extensions_COPY_INP_TMP.TupleSelect(
                                hv_i))) + "$")).TupleConcat("ignore_case"), out hv_Selection);
                            hv_ImageFiles = hv_ImageFiles.TupleConcat(hv_Selection);
                        }
                        HalconDotNet.HOperatorSet.TupleRegexpReplace(hv_ImageFiles, (new HalconDotNet.HTuple("\\\\")).TupleConcat(
                            "replace_all"), "/", out hv_ImageFiles);
                        if ((int)(hv_network_drive.TupleSelect(hv_Index)) != 0)
                        {
                            HalconDotNet.HOperatorSet.TupleRegexpReplace(hv_ImageFiles, (new HalconDotNet.HTuple("//")).TupleConcat(
                                "replace_all"), "/", out hv_ImageFiles);
                            hv_ImageFiles = "/" + hv_ImageFiles;
                        }
                        else
                        {
                            HalconDotNet.HOperatorSet.TupleRegexpReplace(hv_ImageFiles, (new HalconDotNet.HTuple("//")).TupleConcat(
                                "replace_all"), "/", out hv_ImageFiles);
                        }

                    }
                }

                rt = true;
            }
            catch (HalconDotNet.HalconException hex)
            {

            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 闭合多边形等间距分割插值采样
        /// </summary>
        /// <param name="contourEdgeAxis1Value">闭合轮廓第一轴物理坐标</param>
        /// <param name="contourEdgeAxis2Value">闭合轮廓第二轴物理坐标</param>
        /// <param name="sampleDist">轮廓采样间距(用户单位)</param>
        /// <param name="angleForCircleLineIntersetion">圆弧插值采样时的弧角偏移(弧度,正值)</param>
        /// <param name="distForCollinearLine">判断点是否在线段上时的距离容忍偏移(正值)</param>
        /// <param name="angleForCollinear">判断两线段共线时角度到角容忍偏移(弧度,正值)</param>
        /// <param name="smplEdgeAxis1Value">采样后轮廓第一轴物理坐标</param>
        /// <param name="smplEdgeAxis2Value">采样后轮廓第二轴物理坐标</param>
        /// <returns></returns>
        public static bool SamplePolygonWithEquidistantInterpolation(HalconDotNet.HTuple contourEdgeAxis1Value, HalconDotNet.HTuple contourEdgeAxis2Value, HalconDotNet.HTuple sampleDist, HalconDotNet.HTuple angleForCircleLineIntersetion, HalconDotNet.HTuple distForCollinearLine, HalconDotNet.HTuple angleForCollinear,
             out HalconDotNet.HTuple smplEdgeAxis1Value, out HalconDotNet.HTuple smplEdgeAxis2Value)
        {
            bool rt = false;
            smplEdgeAxis1Value = new HalconDotNet.HTuple();
            smplEdgeAxis2Value = new HalconDotNet.HTuple();
            HalconDotNet.HTuple rowStart, colStart; //采集起始点
            HalconDotNet.HTuple rowEnd, colEnd;     //采集终止点

            HalconDotNet.HTuple rowEndJuged, colEndJudge; //判断采样终止点是否异向分界点的辅助点
            HalconDotNet.HTuple rowNext1, colNext1;       //一次偏移点
            HalconDotNet.HTuple rowNext2, colNext2;       //二次偏移点
            HalconDotNet.HTuple rowCenter, colCenter;     //分割采样圆圆心
            HalconDotNet.HTuple rowIntersetion = new HalconDotNet.HTuple(), colIntersection = new HalconDotNet.HTuple(); //分割采样圆与矢量线段的交点
            HalconDotNet.HTuple idxNext2;                       //二次偏移点索引
            HalconDotNet.HTuple distStart, distEnd;             //分割交点与矢量线段起始点和矢量线段终止点的距离

            HalconDotNet.HTuple distPP, distSample;//点间距,采样间距
            HalconDotNet.HTuple idxStart, idxOffset; //采样起始点索引(相对于样本点集),采样终止点索引偏移(相对于采样起始点)

            HalconDotNet.HTuple segAngleOffset, segDistOffset; //圆弧分割交点时的弧角偏移容忍值,圆弧分割交点时判定点在线段的距离偏移容忍值
            HalconDotNet.HTuple angleLx;                       //矢量线段的弧角获异向分界矢量到角
            HalconDotNet.HTuple collinearAngleOffset;         //线段尾点判断时，判定两矢量共线的角偏移容忍值

            HalconDotNet.HTuple pointNum;                    //轮廓点总数        
            bool flagSampleInterpolation = false;            //执行过插值采样
            bool flagFirstPoint = true;                      //首个点标记
            HalconDotNet.HTuple distGreaterEuqal;            //距离大于等于
            HalconDotNet.HTuple iteration;                   //计算采样起始点索引/采样终止点索引偏移时的迭代次数

            try
            {
                //轮廓点数一致
                if (contourEdgeAxis1Value.Length== contourEdgeAxis2Value.Length)
                {
                    pointNum = contourEdgeAxis1Value.TupleLength();
                    //点数非零
                    if (pointNum > 0)
                    {
                        distSample = sampleDist.D > 0 ? sampleDist : new HalconDotNet.HTuple(1);
                        segAngleOffset = angleForCircleLineIntersetion.D> 0 ? angleForCircleLineIntersetion.TupleAbs() : new HalconDotNet.HTuple(0.1);
                        segDistOffset = distForCollinearLine.D > 0 ? distForCollinearLine : new HalconDotNet.HTuple(0.005);
                        collinearAngleOffset = angleForCollinear.D > 0 ? angleForCollinear: new HalconDotNet.HTuple(0.05);

                        idxStart = 0;
                        idxOffset = 1;

                        //分割轮廓点时,预留最后两个点(可能的一次偏移点，二次偏移点)
                        for (int I = 0; I < pointNum - 2; I++)
                        {
                            //采样起始点
                            rowStart = contourEdgeAxis1Value[I];
                            colStart = contourEdgeAxis2Value[I];

                            //采样终止点
                            rowEnd = contourEdgeAxis1Value[I + idxOffset];
                            colEnd = contourEdgeAxis2Value[I + idxOffset];

                            //二偏移点未超出点集终点(二次偏移点的位置索引小于等于点集最大索引,即小于点集数)
                            if (I + idxOffset + 1 < pointNum)
                            {
                                rowEndJuged = contourEdgeAxis1Value[I + idxOffset + 1];
                                colEndJudge = contourEdgeAxis2Value[I + idxOffset + 1];
                            }
                            else
                            {
                                rowEndJuged = contourEdgeAxis1Value[pointNum - 1]; //闭合轮廓首点与终点为同点
                                colEndJudge = contourEdgeAxis2Value[pointNum - 1];
                            }

                            //计算采样起始点与终止点点间距
                            HalconDotNet.HOperatorSet.DistancePp(rowStart, colStart, rowEnd, colEnd, out distPP);
                            HalconDotNet.HOperatorSet.TupleGreaterEqual(distPP, distSample, out distGreaterEuqal);

                            #region 点间距大于等于采样距离,可插值采样
                            if (distGreaterEuqal.I > 0)
                            {
                                rowCenter = rowStart;
                                colCenter = colStart;

                                //首次采样处理,(可插值采样时)添加采样起始点到采样点集
                                if (flagFirstPoint)
                                {
                                    smplEdgeAxis1Value = new HalconDotNet.HTuple(rowStart);
                                    smplEdgeAxis2Value = new HalconDotNet.HTuple(colStart);
                                    flagSampleInterpolation = true;  //执行过采样标记置位
                                    flagFirstPoint = false;          //首次采样处理标记复位
                                }

                                //点间距大于等于采样间距,循环插值采样
                                while (distGreaterEuqal.I > 0)
                                {
                                    //注：采样分割圆心与采样终止点构成的矢量线段,简称分割矢量线段
                                    //-1-计算采样圆与分割矢量线段交点(计算分割矢量线段的矢量角,采样圆弧的起始角和终止角以矢量线段的矢量角为基准,左右偏移弧角偏移容忍值)
                                    HalconDotNet.HOperatorSet.AngleLx(rowCenter, colCenter, rowEnd, colEnd, out angleLx);
                                    HalconDotNet.HOperatorSet.IntersectionLineCircle(rowCenter, colCenter, rowEnd, colEnd, rowCenter, colCenter, sampleDist, angleLx - angleForCircleLineIntersetion, angleLx + angleForCircleLineIntersetion, "positive", out rowIntersetion, out colIntersection);

                                    //-2-采样圆与分割矢量线段有交点
                                    if (rowIntersetion.Length > 0)
                                    {
                                        //-3-判断采样圆与分割矢量线段交点是否在分割矢量线段上
                                        HalconDotNet.HOperatorSet.DistancePp(rowIntersetion, colIntersection, rowCenter, colCenter, out distStart);//交点到分割矢量线段起点的距离
                                        HalconDotNet.HOperatorSet.DistancePp(rowIntersetion, colIntersection, rowEnd, colEnd, out distEnd);        //交点到分割矢量线段终点的距离
                                        HalconDotNet.HOperatorSet.DistancePp(rowCenter, colCenter, rowEnd, colEnd, out distPP);                  //分割矢量线段的长度

                                        //采样圆与分割矢量线段交点在分割矢量线段上,可插值采样
                                        if ((distStart + distEnd - distPP).TupleLessEqual(distForCollinearLine))
                                        {
                                            //二次偏移点,判断其是否超出样本点集终点(即闭合轮廓的首点)
                                            if (I + idxOffset.I + 1 < pointNum.I)
                                            {
                                                smplEdgeAxis1Value = smplEdgeAxis1Value.TupleConcat(rowIntersetion);
                                                smplEdgeAxis2Value = smplEdgeAxis2Value.TupleConcat(colIntersection);
                                            }
                                            else
                                            {
                                                HalconDotNet.HOperatorSet.DistancePp(rowIntersetion, colIntersection, rowEnd, colEnd, out distPP);

                                                //二次偏移点超出样本点集终点,且采样圆与分割矢量线段交点到分割矢量线段的终点(此时为一次偏移点)的距离小于等于共线距离偏移容忍值,不必重复添加分割点(点集终点即点集首点)
                                                if (distPP.TupleGreater(distForCollinearLine))
                                                {
                                                    smplEdgeAxis1Value = smplEdgeAxis1Value.TupleConcat(rowIntersetion);
                                                    smplEdgeAxis2Value = smplEdgeAxis2Value.TupleConcat(colIntersection);
                                                }
                                            }

                                            //更新采样圆心位置
                                            rowCenter = rowIntersetion;
                                            colCenter = colIntersection;
                                            //重新判断后续分割矢量线段是否可插值分割采样
                                            HalconDotNet.HOperatorSet.DistancePp(rowCenter, colCenter, rowEnd, colEnd, out distPP);
                                            HalconDotNet.HOperatorSet.TupleGreaterEqual(distPP, distSample, out distGreaterEuqal);
                                        }
                                        else
                                        {
                                            //采样圆与分割矢量线段交点不在分割矢量线段上,退出分割插值采样循环(采样圆的弧角范围异常导致)
                                            break;
                                        }
                                    }
                                    else
                                    { //采样圆与矢量线段无交点,退出分割插值采样循环(采样圆的弧角范围异常导致)
                                        break;
                                    }
                                }

                                //分割矢量线段尾点处理:采样分割点非采样终止点,则需要判断采样终止点是否异向分界点
                                HalconDotNet.HOperatorSet.DistancePp(rowIntersetion, colIntersection, rowEnd, colEnd, out distPP);
                                HalconDotNet.HOperatorSet.TupleGreaterEqual(distPP, distSample, out distGreaterEuqal);

                                //注:分割点与采样终止点构成的矢量线段,简称异向分界判断前矢量;采样终止点与异向分界判断辅助点构成的向量,简称异向分界判断后矢量
                                //采样分割插值点与采样终止点非同点
                                if (distGreaterEuqal.I > 0)
                                {
                                    //一次偏移点,判断其是否到达样本点集终点(即闭合轮廓的首点)
                                    if (I + idxOffset.I + 1 < pointNum.I)
                                    {
                                        //一次偏移点未到达样本点集终点(若一次偏移点到达样本点集终点,此时不必重复添加尾点)
                                        //异向分界判断前矢量与异向分界判断后矢量是否平行
                                        HalconDotNet.HOperatorSet.AngleLl(rowIntersetion, colIntersection, rowEnd, colEnd, rowEnd, colEnd, rowEndJuged, colEndJudge, out angleLx);

                                        //非平行,则采样终止点是异向分界点,保留到采样点集
                                        if (angleLx.TupleAbs().TupleLessEqual(angleForCollinear).I == 0)
                                        {
                                            smplEdgeAxis1Value = smplEdgeAxis1Value.TupleConcat(rowEnd);
                                            smplEdgeAxis2Value = smplEdgeAxis2Value.TupleConcat(colEnd);
                                        }
                                    }
                                }

                                //更新采样起始点索引以及采样终止点索引偏移
                                I += idxOffset - 1; //下次采样起始点索引加idxOffset,-1是因为循环自动+1,需要抵消
                                idxOffset = 1;//下次采样终止点索引偏移1
                            }
                            #endregion

                            #region  点间距小于采样间距,则计算采样起始点索引和采样终止点索引偏移
                            else
                            {
                                //迭代次数清零
                                iteration = 0;

                                #region  二次偏移点未超出点集终点(二次偏移点的位置索引小于等于点集最大索引,即小于点集数)
                                if (I + iteration + 1 < pointNum)
                                {
                                    //-1-更新一次偏移点和二次偏移点
                                    rowNext1 = contourEdgeAxis1Value[I + iteration + 1];
                                    colNext1 = contourEdgeAxis2Value[I + iteration + 1];

                                    rowNext2 = contourEdgeAxis1Value[I + iteration + 2];
                                    colNext2 = contourEdgeAxis2Value[I + iteration + 2];

                                    //注:采样起始点当前采样终止点构成共线判定前矢量,一次偏移点与二次偏移点构成共线判定后矢量

                                    //-2-判断共线判定前矢量和共线判定后矢量是否平行
                                    HalconDotNet.HOperatorSet.AngleLl(rowStart, colStart, rowEnd, colEnd, rowNext1, colNext1, rowNext2, colNext2, out angleLx);

                                    //共线判定前矢量和共线判定后矢量平行
                                    if (angleLx.TupleAbs().TupleLessEqual(angleForCollinear).I == 1)
                                    {
                                        //-3-计算采样起始点与二次偏移点点间距
                                        HalconDotNet.HOperatorSet.DistancePp(rowStart, colStart, rowNext2, colNext2, out distPP);
                                        HalconDotNet.HOperatorSet.TupleGreaterEqual(distPP, distSample, out distGreaterEuqal);

                                        //点间距小于采样间距
                                        while (distGreaterEuqal.I == 0)
                                        {
                                            iteration += 1;
                                            idxNext2 = I + iteration + 2;

                                            //二次偏移点索引不超出样本点集终点的邻近前一点(由外循环处理采样终止点在样本点集最后一点的情形,否则内循环无法直接退出外循环)
                                            if (idxNext2.TupleLess(pointNum - 1))
                                            {
                                                //-4-更新一次偏移点和二次偏移点
                                                rowNext1 = contourEdgeAxis1Value[I + iteration + 1];
                                                colNext1 = contourEdgeAxis2Value[I + iteration + 1];

                                                rowNext2 = contourEdgeAxis1Value[I + iteration + 2];
                                                colNext2 = contourEdgeAxis2Value[I + iteration + 2];

                                                //-5-判断共线判定前矢量和共线判定后矢量是否平行
                                                HalconDotNet.HOperatorSet.AngleLl(rowStart, colStart, rowEnd, colEnd, rowNext1, colNext1, rowNext2, colNext2, out angleLx);

                                                //共线判定前矢量和共线判定后矢量平行
                                                if (angleLx.TupleAbs().TupleLessEqual(angleForCollinear).I == 1)
                                                {
                                                    //-6-计算采样起始点与二次偏移点点间距
                                                    HalconDotNet.HOperatorSet.DistancePp(rowStart, colStart, rowNext2, colNext2, out distPP);
                                                    HalconDotNet.HOperatorSet.TupleGreaterEqual(distPP, distSample, out distGreaterEuqal);
                                                }
                                                else
                                                {
                                                    //(不可插值采样且一次偏移点为异向分界点)添加一次偏移点到采样点集
                                                    smplEdgeAxis1Value = smplEdgeAxis1Value.TupleConcat(rowNext1);
                                                    smplEdgeAxis2Value = smplEdgeAxis2Value.TupleConcat(colNext1);
                                                }
                                            }
                                            else
                                            {
                                                //内循环:二次偏移点到达样本点集终点的邻近前一点,退出内循环
                                                break;
                                            }
                                        }

                                        I += -1; //下次采样起始点的索引为当前索引,循环自动加1,故-1抵消
                                        idxOffset = iteration + 2;//下次采样终止点索引偏移为二次偏移
                                    }
                                    else
                                    {
                                        // //首次采样处理,(不可插值采样且采样终止点(与一次偏移点重合)为异向分界点)添加采样起始点和采样终止点到采样点集
                                        if (flagFirstPoint)
                                        {
                                            smplEdgeAxis1Value = new HalconDotNet.HTuple(rowStart);
                                            smplEdgeAxis2Value = new HalconDotNet.HTuple(colStart);
                                            flagSampleInterpolation = true;  //执行过采样标记置位
                                            flagFirstPoint = false;          //首次采样处理标记复位
                                        }

                                        //(不可插值采样且一次偏移点为异向分界点)添加一次偏移点到采样点集
                                        smplEdgeAxis1Value = smplEdgeAxis1Value.TupleConcat(rowNext1);
                                        smplEdgeAxis2Value = smplEdgeAxis2Value.TupleConcat(colNext1);
                                        I += iteration; //下次采样起始点的索引为一次偏移点的索引(iteration+1),再抵消一次循环自动+1;故只有iteration
                                        idxOffset = 1; //下次采样终止点索引偏移1
                                    }
                                }
                                #endregion

                                #region 二次偏移点超出点集终点
                                else
                                {
                                    //二次偏移点超出点集时,一次偏移点到达点集最后一个点;
                                    //此时,采样起始点到一次偏移点的点间距不足插值采样;
                                    //若没插值采样过,即点集首点到终点都不足插值采样,保留点集首点和终点(循环自动退出)
                                    //若插值采样过,则退出采集循环
                                    if (!flagSampleInterpolation)
                                    {
                                        smplEdgeAxis1Value = (new HalconDotNet.HTuple(rowStart)).TupleConcat(contourEdgeAxis1Value[pointNum - 1]);
                                        smplEdgeAxis2Value = (new HalconDotNet.HTuple(colStart)).TupleConcat(contourEdgeAxis2Value[pointNum - 1]);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                #endregion
                            }
                            #endregion
                        }
                        int l = smplEdgeAxis1Value.Length;
                        rt = true;
                    }
                }
            }
            catch (HalconDotNet.HalconException hex) { }
            catch (System.Exception ex) { }
            finally { }
            return rt;
        }

    }
}
