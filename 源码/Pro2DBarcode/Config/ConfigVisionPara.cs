using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ConfigVisionPara
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.Config
 * File      Name：       ConfigVisionPara
 * Creating  Time：       1/15/2020 3:59:34 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.Config
{
    /// <summary>
    /// 视觉参数配置
    /// </summary>
    [Serializable]
    public class ConfigVisionPara : Pro2DBarcode.Config.Config
    {
        /// <summary>
        /// 二维码识别参数
        /// </summary>
        public Pro2DBarcode.Config.VisionPara.VisionPara2DBarcode Para2DBarcode { set; get; }
       
    }
}
