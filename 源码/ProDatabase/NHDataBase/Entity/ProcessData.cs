using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ProcessData
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProDatabase.NHDataBase.Entity
 * File      Name：       ProcessData
 * Creating  Time：       10/9/2019 2:45:20 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProDatabase.NHDataBase.Entity
{
    /// <summary>
    /// 过程数据类
    /// </summary>
    [Serializable]
    public class ProcessData : ICloneable, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// 事件：属性值变化
        /// </summary>
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 回调函数：属性值改变事件
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }


        /// <summary>
        /// 属性：序列号
        /// </summary>
        public virtual string SerialNo
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:处理时间
        /// </summary>
        public virtual DateTime DoTime
        {
            set;
            get;
        }

        #region 顶部相机
        /// <summary>
        /// 属性：顶部相机识别标记(成功/失败)
        /// </summary>
        public virtual bool IdtTopIsOK
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：顶部相机校验标记(成功/失败)
        /// </summary>
        public virtual bool VerTopIsOK
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：顶部相机识别图像保存路径
        /// </summary>
        public virtual string ImagePathIdtTop
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：顶部相机校验图像保存路径
        /// </summary>
        public virtual string ImagePathVerTop
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:顶部相机识别耗时
        /// </summary>
        public virtual int ElapseIdtTop
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:顶部相机校验耗时
        /// </summary>
        public virtual int ElapseVerTop
        {
            set;
            get;
        }

        #region 顶部相机图像处理结果数据(位置数据或其他数据)

        public virtual bool IdtTop_IsDefected { set; get; }

        public virtual double IdtTop_X { set; get; }

        public virtual double IdtTop_Y { set; get; }

        public virtual double IdtTop_Z { set; get; }

        public virtual double IdtTop_RZ { set; get; }

        public virtual double IdtTop_Length { set; get; }

        public virtual double IdtTop_Width { set; get; }
        #endregion

        #endregion

        #region 底部相机
        /// <summary>
        /// 属性：底部相机识别标记(成功/失败)
        /// </summary>
        public virtual bool IdtBotIsOK { set; get; }

        /// <summary>
        /// 属性：底部相机图像保存路径
        /// </summary>
        public virtual string ImagePathIdtBot { set; get; }

        /// <summary>
        /// 属性:底部相机识别耗时
        /// </summary>
        public virtual int ElapseIdtBot { set; get; }

        #region 底部相机图像处理结果数据(位置数据或其他数据)
        public virtual bool IdtBot_IsDefected { set; get; }
        public virtual double IdtBot_X { set; get; }

        public virtual double IdtBot_Y { set; get; }

        public virtual double IdtBot_Z { set; get; }

        public virtual double IdtBot_RZ { set; get; }

        public virtual double IdtBot_Length { set; get; }

        public virtual double IdtBot_Width { set; get; }
        #endregion

        #endregion

        /// <summary>
        /// 属性：图像处理输出标记(识别成功/失败+处理结果值：识别失败-NK,识别成功且图像结果非标准范围-NG，识别成功且图像结果在标准范围-OK)
        /// </summary>
        public virtual string Result
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:备注
        /// </summary>
        public virtual string Remark
        {
            set;
            get;
        }



        /// <summary>
        /// 方法:实现接口(ICloneable:Clone)
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            ProcessData pd = new ProcessData();
            pd.SerialNo = this.SerialNo;
            pd.DoTime = this.DoTime;

            pd.IdtTopIsOK = this.IdtTopIsOK;
            pd.VerTopIsOK = this.VerTopIsOK;
            pd.ImagePathIdtTop = this.ImagePathIdtTop;
            pd.ImagePathVerTop = this.ImagePathVerTop;
            pd.ElapseIdtTop = this.ElapseIdtTop;
            pd.ElapseVerTop = this.ElapseVerTop;
            pd.IdtTop_IsDefected = this.IdtTop_IsDefected;
            pd.IdtTop_X = this.IdtTop_X;
            pd.IdtTop_Y = this.IdtTop_Y;
            pd.IdtTop_Z = this.IdtTop_Z;
            pd.IdtTop_RZ = this.IdtTop_RZ;
            pd.IdtTop_Length = this.IdtTop_Length;
            pd.IdtTop_Width = this.IdtTop_Width;

            pd.IdtBotIsOK = this.IdtBotIsOK;
            pd.ImagePathIdtBot = this.ImagePathIdtBot;
            pd.ElapseIdtBot = this.ElapseIdtBot;
            pd.IdtBot_IsDefected = this.IdtBot_IsDefected;
            pd.IdtBot_X = this.IdtBot_X;
            pd.IdtBot_Y = this.IdtBot_Y;
            pd.IdtBot_Z = this.IdtBot_Z;
            pd.IdtBot_RZ = this.IdtBot_RZ;
            pd.IdtBot_Length = this.IdtBot_Length;
            pd.IdtBot_Width = this.IdtBot_Width;

            pd.Result = this.Result;
            pd.Remark = this.Remark;

            return pd;
        }
    }
}
