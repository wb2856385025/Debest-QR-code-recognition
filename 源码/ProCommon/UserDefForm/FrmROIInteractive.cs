using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProCommon.UserDefForm
{
    public partial class FrmROIInteractive : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        protected internal bool _isCtrlKeyPressed,_isAltKeyPressed;

        public ProCommon.Communal.Language LanguageVersion { protected set; get; }

        private FrmROIInteractive()
        {
            InitializeComponent();           
        }

        protected internal FrmROIInteractive(ProCommon.Communal.Language lan):this()
        {
            LanguageVersion = lan;
        }

        #region 必须覆写的成员

        protected internal virtual void InitFieldAndProperty()
        {

        }

        protected internal virtual void UpdateControl()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);
            string str = this.Tag.ToString();
            this.Text = isChs ? ProCommon.Properties.Resources.ResourceManager.GetString("chs_" + str) : ProCommon.Properties.Resources.ResourceManager.GetString("en_" + str);

            str = this.rbpROIInteractive.Tag.ToString();
            this.rbpROIInteractive.Text=isChs? ProCommon.Properties.Resources.ResourceManager.GetString("chs_" + str) : ProCommon.Properties.Resources.ResourceManager.GetString("en_" + str);

            UpdateRabbinPageGroup(this.rbpgrpROIFile, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateRabbinPageGroup(this.rbpgrpROIShape, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateRabbinPageGroup(this.rbpgrpViewOption, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateRabbinPageGroup(this.rbpgrpOperation, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);

            UpdateBarButtonItem(this.bbiLoadImage, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiLine, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiRectangle1, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiRectangle2, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiCircularArc, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiCircle, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiAnnulus, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);

            UpdateBarCheckItem(this.bchkiNone, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarCheckItem(this.bchkiMove, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarCheckItem(this.bchkiZoom, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarCheckItem(this.bchkiMagnify, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);

            UpdateBarButtonItem(this.bbiDeleteActiveROI, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiDeleteAllROI, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiResetWindow, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
           
        }

        protected internal virtual void FrmROIInteractive_Load(object sender, EventArgs e)
        {

        }

        protected internal virtual void Bbi_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        protected internal virtual void Bchki_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        protected internal virtual void FrmROIInteractive_SizeChanged(object sender, EventArgs e)
        {

        }

        protected internal virtual void FrmROIInteractive_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        #endregion          

        protected internal virtual void FrmROIInteractive_KeyDown(object sender, KeyEventArgs e)
        {
        }

        protected internal virtual void FrmROIInteractive_KeyUp(object sender, KeyEventArgs e)
        {

        }

        protected void UpdateRabbinPageGroup(DevExpress.XtraBars.Ribbon.RibbonPageGroup rbpgrp,ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (rbpgrp != null
                && rbpgrp.Tag != null)
            {
                if (resourceManager != null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = rbpgrp.Tag.ToString();
                    rbpgrp.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);  
                }
            }

        }

        protected void UpdateBarCheckItem(DevExpress.XtraBars.BarCheckItem bchki, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (bchki != null
              && bchki.Tag != null)
            {
                if (resourceManager != null)
                {
                    bchki.CheckedChanged -= Bchki_CheckedChanged;
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = bchki.Tag.ToString();
                    bchki.Caption = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                    bchki.CheckedChanged += Bchki_CheckedChanged;
                }
            }
        }

        protected void UpdateBarButtonItem(DevExpress.XtraBars.BarBaseButtonItem bbi,ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (bbi != null
               && bbi.Tag != null)
            {
                if (resourceManager != null)
                {
                    bbi.ItemClick -= Bbi_Click;
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = bbi.Tag.ToString();
                    bbi.Caption = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                    bbi.ItemClick += Bbi_Click;
                }
            }
        }
    }
}