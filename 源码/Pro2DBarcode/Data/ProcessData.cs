using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ProcessData
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Data
 * File      Name：       ProcessData
 * Creating  Time：       2/21/2020 12:16:33 AM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Data
{
    public abstract class ProcessData
    {
        /// <summary>
        /// 属性：序列号(整型)
        /// </summary>      
        public int SerialNo
        {
            private set;
            get;
        }

        /// <summary>
        /// 属性:流水日期
        /// </summary>
        public DateTime SerialDate
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:流水号(字符串)
        /// </summary>      
        public string GenSerialNo
        {
            get
            {
                if (DateTime.Compare(this.SerialDate.Date, DateTime.Now.Date) != 0)
                {
                    this.SerialDate = DateTime.Now.Date;
                    this.SerialNo = 0;
                }

                this.SerialNo++;
                return string.Format("{0}{1}",
                                     this.SerialDate.ToString("yyyy-MM-dd"),
                                     this.SerialNo.ToString("000000"));
            }
        }      

        /// <summary>
        /// 处理耗时
        /// </summary>
        public double ElapseTime { set; get; }

        /// <summary>
        /// 图像处理过程正常标记
        /// true-执行过程正常,false-执行过程异常
        /// </summary>
        public bool ImgProcessOK { set; get; }

        /// <summary>
        /// 图像处理结果是否达标标记
        /// true-达到设计要求,false-未达到设计要求
        /// </summary>
        public bool ImgResultOK { set; get; }

    }
    public class BarcodeResult: ProcessData
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Number { set; get; }

        /// <summary>
        /// 有效标记
        /// </summary>
        public bool Effective { set; get; }

        /// <summary>
        /// 条码内容
        /// </summary>
        public string BarcodeString { set; get; }       
    }
}
