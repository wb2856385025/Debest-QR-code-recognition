using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProCommon.UserDefControl
{
    public partial class UsrCtrlLocationDebug : UserControl
    {
        public UsrCtrlLocationDebug()
        {
            InitializeComponent();

            this.Load += UsrCtrlLocationDebug_Load;
        }

     

        /// <summary>
        /// 相机
        /// </summary>
        public ProCommon.Communal.Camera Camera { set; get; }

        public ProCommon.Communal.Language _isChsLanguageVersion;
        public ProCommon.Communal.Language LanguageVersion
        {
            set
            {
                _isChsLanguageVersion = value;
                if (_isChsLanguageVersion!=value)
                {
                    switch(_isChsLanguageVersion)
                    {
                        case Communal.Language.Chinese:
                            ProCommon.Communal.Functions.UpdateControlsText(new Communal.UpdateCtrlWithLanguageHandler(this.UpdateControlTextChineseLanguage), this.Controls);
                            break;
                        case Communal.Language.English:
                            ProCommon.Communal.Functions.UpdateControlsText(new Communal.UpdateCtrlWithLanguageHandler(this.UpdateControlTextEnglishLanguage), this.Controls);
                            break;
                    }                   
                }
            }
            get
            {
                return _isChsLanguageVersion;
            }
        }

        protected internal virtual void InitField()
        {

        }

        protected internal virtual void InitControl()
        {
            this.tlstrpbtnAcquireOnce.Click += TlstrpbtnAquireOnce_Click;
            this.tlstrpbtnAcquireContinue.Click += TlstrpbtnAcquireContinue_Click;
            this.tlstrpbtnSetCamera.Click += TlstrpbtnSetCamera_Click;
            this.tlstrpmitmCalibratePlate.Click += TlstrpmitmCalibratePlate_Click;
            this.tlstrpmitmCalibrate9Point.Click += TlstrpmitmCalibrate9Point_Click;
            this.tlstrpbtnMatchModel.Click += TlstrpbtnMatchModel_Click;
            this.tlstrpbtnRotateCenter.Click += TlstrpbtnRotateCenter_Click;
            this.tlstrptxtbExposureTime.MouseEnter += TlstrptxtbExposureTime_MouseEnter;
            this.tlstrptxtbMatchScore.MouseEnter += TlstrptxtbMatchScore_MouseEnter;
        }

        /// <summary>
        /// 根据控件Tag更新其简体中文文本
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        protected internal virtual bool UpdateControlTextChineseLanguage(System.Windows.Forms.Control ctrl)
        {
            bool rt = false;
            try
            {
                if (ctrl != null
                    && ctrl != null)
                {
                    ctrl.Text = Properties.Resources.ResourceManager.GetString("chs_" + ctrl.Tag.ToString());
                    rt = true;
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (System.Exception ex) { }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            return rt;
        }

        /// <summary>
        /// 根据控件Tag更新其英文文本
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        protected internal virtual bool UpdateControlTextEnglishLanguage(System.Windows.Forms.Control ctrl)
        {
            bool rt = false;
            try
            {
                if (ctrl != null
                    && ctrl != null)
                {
                    ctrl.Text = Properties.Resources.ResourceManager.GetString("en_" + ctrl.Tag.ToString());
                    rt = true;
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (System.Exception ex) { }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            return rt;
        }

        #region 需要被覆写的函数

        /// <summary>
        /// 控件加载完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void UsrCtrlLocationDebug_Load(object sender, EventArgs e)
        {
            InitField();
            InitControl();
        }

        /// <summary>
        /// '匹配分数'控件'Enter'键按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void TlstrptxtbMatchScore_MouseEnter(object sender, EventArgs e)
        {
           
        }


        /// <summary>
        /// '曝光时间'控件'Enter'键按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void TlstrptxtbExposureTime_MouseEnter(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// '旋转中心'控件单击事件
        /// [注:计算旋转中心]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void TlstrpbtnRotateCenter_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// '创建模板'控件单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void TlstrpbtnMatchModel_Click(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// '九点标定'控件单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void TlstrpmitmCalibrate9Point_Click(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// '标定板标定'控件单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void TlstrpmitmCalibratePlate_Click(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// '相机设置'控件单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void TlstrpbtnSetCamera_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// '连续采集'控件单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void TlstrpbtnAcquireContinue_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// '单张采集'控件单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void TlstrpbtnAquireOnce_Click(object sender, EventArgs e)
        {
            
        }

        #endregion

    }
}
