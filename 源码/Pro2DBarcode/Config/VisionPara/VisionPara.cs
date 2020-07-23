using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       VisionPara
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Config.VisionPara
 * File      Name：       VisionPara
 * Creating  Time：       2/20/2020 11:58:12 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Config.VisionPara
{
    [Serializable]
    public abstract class VisionProcessPara
    {
        /// <summary>
        /// 是否保存所有图像
        /// </summary>
        public bool IsSaveImageAll { set; get; }

        /// <summary>
        /// 是否保存OK图像
        /// </summary>
        public bool IsSaveImageOK { set; get; }

        /// <summary>
        /// 是否保存NG图像
        /// </summary>
        public bool IsSaveImageNG { set; get; }

        /// <summary>
        /// 保存所有图像的目录路径
        /// </summary>
        public string PathForSaveImageAll { set; get; }

        /// <summary>
        /// 保存OK图像的目录路径
        /// </summary>
        public string PathForSaveImageOK { set; get; }

        /// <summary>
        /// 保存NG图像的目录路径
        /// </summary>
        public string PathForSaveImageNG { set; get; }
    }

    /// <summary>
    /// 二维码识别参数
    /// [注:联合创新]
    /// </summary>
    [Serializable]
    public class VisionPara2DBarcode : Pro2DBarcode.Config.VisionPara.VisionProcessPara
    {
        public float CameraExposure { get; set; }
        public float CameraGain { get; set; }
        public string PathForInspectArea { get; set; }

        /// <summary>
        /// 识别二维码的类型
        /// </summary>
        public string CodeType { set; get; }

        /// <summary>
        /// 查找指定个数时的超时
        /// </summary>
        public int TimeOut { set; get; }

        /// <summary>
        /// 预期找到的二维码个数
        /// </summary>
        public int ToBeFoundNumber { set; get; }

        /// <summary>
        /// 通用参数名数组
        /// [注:创建二维码识别模型]
        /// </summary>
        public string[] GenParameterNamesForCreate { set; get; }

        /// <summary>
        /// 与通用参数名数组对应的参数值数组
        /// [注:创建二维码识别模型]
        /// </summary>
        public string[] GenParameterValuesForCreate { set; get; } 
        
        /// <summary>
        /// 未识别二维码的区域颜色
        /// </summary>
        public string ColorStrForResultNotFound { set; get; }

        /// <summary>
        /// 识别到二维码区域的颜色
        /// </summary>
        public string ColorStrForResultFound { set; get; }      
    }
}
