using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProCommon.UserDefForm
{
    public partial class FrmToolCenterOffset : DevExpress.XtraEditors.XtraForm
    {
        public ProCommon.Communal.Language LanguageVersion { private set; get; }

        public float ToolCenterOffsetX { private set; get; }

        public float ToolCenterOffsetY { private set; get; }

        private FrmToolCenterOffset()
        {
            InitializeComponent();
        }

        public FrmToolCenterOffset(ProCommon.Communal.Language lan):this()
        {
            LanguageVersion = lan;
            this.Load += FrmToolCenterOffset_Load;
        }

        #region 可能覆写的函数
        protected internal virtual void FrmToolCenterOffset_Load(object sender, EventArgs e)
        {
            InitFieldAndProperty();
            UpdateControl();
        }

        protected internal virtual void InitFieldAndProperty()
        {

        }

        protected internal virtual void UpdateControl()
        {
            string str = this.Tag.ToString();
            bool isChs = LanguageVersion == ProCommon.Communal.Language.Chinese;
            this.HtmlText = isChs ? ProCommon.Properties.Resources.ResourceManager.GetString("chs_" + str) : ProCommon.Properties.Resources.ResourceManager.GetString("en_" + str);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            UpdateLabelControl(this.lblPointX1, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblPointY1, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblPointR1, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLinkLabel(this.lklblGetPosition1, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLinkLabel(this.lklblGoToPosition1, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);

            UpdateLabelControl(this.lblPointX2, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblPointY2, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblPointR2, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLinkLabel(this.lklblGetPosition2, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLinkLabel(this.lklblGoToPosition2, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);

            UpdateLinkLabel(this.lklblCalculateToolCenterOffset, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
        }

        /// <summary>
        /// 获取坐标
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        protected internal virtual void GetPosition(out float x, out float y, out float r)
        {
            x = 0.0f;
            y = 0.0f;
            r = 0.0f;
        }

        /// <summary>
        /// 定位坐标
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        protected internal virtual void GoToPosition(float x, float y, float r)
        {

        }

        /// <summary>
        /// 更新工具坐标偏移
        /// </summary>
        /// <param name="offsetx"></param>
        /// <param name="offsety"></param>
        protected internal virtual void UpdateToolCenterOffset(float offsetx, float offsety)
        {

        }

        #endregion

        protected internal void UpdateLabelControl(DevExpress.XtraEditors.LabelControl lblc, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (lblc != null
              && lblc.Tag != null)
            {
                if (resourceManager != null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = lblc.Tag.ToString();
                    lblc.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                }
            }
        }

        protected internal void UpdateLinkLabel(System.Windows.Forms.LinkLabel lklbl, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (lklbl != null
             && lklbl.Tag != null)
            {
                if (resourceManager != null)
                {
                    lklbl.Click -= Lklbl_Click;
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = lklbl.Tag.ToString();
                    lklbl.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                    lklbl.Click += Lklbl_Click;
                }
            }
        }

        private void Lklbl_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.LinkLabel lklbl = sender as System.Windows.Forms.LinkLabel;
            if(lklbl!=null
                && lklbl.Tag!=null)
            {
                float x, y, r;
                switch (lklbl.Tag.ToString())
                {
                    case "LKLBL_GETPOSITION1":
                        GetPosition(out x, out y, out r);
                        this.spePointX1.EditValue = x;
                        this.spePointY1.EditValue = y;
                        this.spePointR1.EditValue = r;
                        break;
                    case "LKLBL_GOTOPOSITION1":
                        x = Convert.ToSingle(this.spePointX1.EditValue);
                        y = Convert.ToSingle(this.spePointY1.EditValue);
                        r = Convert.ToSingle(this.spePointR1.EditValue);
                        GoToPosition(x, y, r);
                        break;
                    case "LKLBL_GETPOSITION2":
                        GetPosition(out x, out y, out r);
                        this.spePointX2.EditValue = x;
                        this.spePointY2.EditValue = y;
                        this.spePointR2.EditValue = r;
                        break;
                    case "LKLBL_GOTOPOSITION2":
                        x = Convert.ToSingle(this.spePointX2.EditValue);
                        y = Convert.ToSingle(this.spePointY2.EditValue);
                        r = Convert.ToSingle(this.spePointR2.EditValue);
                        GoToPosition(x, y, r);
                        break;
                    case "LKLBL_CALCULATETOOLCENTEROFFSET":
                        ToolCenterOffsetX = 0.0f;
                        ToolCenterOffsetY = 0.0f;
                        float tmpx, tmpy;
                        x = Convert.ToSingle(this.spePointX2.EditValue);
                        y = Convert.ToSingle(this.spePointY2.EditValue);
                        tmpx = Convert.ToSingle(this.spePointX1.EditValue);
                        tmpy = Convert.ToSingle(this.spePointY1.EditValue);
                        ToolCenterOffsetX = (x - tmpx)/2.0f;
                        ToolCenterOffsetY = (y - tmpy)/2.0f;
                        this.speToolCenterOffsetX.EditValue = ToolCenterOffsetX;
                        this.speToolCenterOffsetY.EditValue = ToolCenterOffsetY;
                        UpdateToolCenterOffset(ToolCenterOffsetX, ToolCenterOffsetY);
                        break;
                    default:break;
                }
            }
        }

       
    }
}