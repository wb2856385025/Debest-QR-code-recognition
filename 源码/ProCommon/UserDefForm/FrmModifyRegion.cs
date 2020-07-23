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
    public partial class FrmModifyRegion : DevExpress.XtraEditors.XtraForm
    {
        protected internal HalconDotNet.HObject _hoImage, _hoBrushRegion, _hoRawRegion, _hoResultRegion, _selectedRegn;
        protected internal string _brushColor, _resultRegionColor,_selectedRegionColor;
        protected internal HalconDotNet.HTuple _imgWidth, _imgHeight;
        public ProCommon.Communal.Language LanguageVersion { protected set; get; }

        public FrmModifyRegion()
        {
            InitializeComponent();           
        }

        public FrmModifyRegion(ProCommon.Communal.Language lan,HalconDotNet.HObject img):this()
        {
            LanguageVersion = lan;
            _hoImage = img;           
            this.sbtnLoadImage.Enabled = false;
        }

        public FrmModifyRegion(ProCommon.Communal.Language lan,HalconDotNet.HObject img, HalconDotNet.HObject region) : this()
        {
            LanguageVersion = lan;
            _hoImage = img;
            _hoResultRegion = region;
            this.sbtnLoadImage.Enabled = false;
            this.sbtnDefineRegion.Enabled = false;
        }


        #region 必须覆写的成员

        protected internal virtual void Sbtn_Click(object sender, EventArgs e)
        {
           
        }

        protected internal virtual void FrmRegionModify_Load(object sender, EventArgs e)
        {
            Init();
        }

        protected internal virtual void FrmRegionModify_SizeChanged(object sender, EventArgs e)
        {

        }

        protected internal virtual void CmbeDefineAreaList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

        protected internal virtual void Init()
        {
            InitFieldAndProperty();
            UpdateControl();
        }
        protected internal virtual void InitFieldAndProperty()
        {

        }
        protected internal virtual void UpdateControl()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);
            string str = this.Tag.ToString();
            this.Text = isChs ?ProCommon.Properties.Resources.ResourceManager.GetString("chs_" + str) :ProCommon.Properties.Resources.ResourceManager.GetString("en_" + str);

            UpdateSimpleButton(this.sbtnDefineRegion, LanguageVersion,ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnEraseRegion, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnExit, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnLoadImage, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);

            UpdateLabelControl(this.lblPrompt, LanguageVersion, ProCommon.Properties.Resources.ResourceManager, null);
            UpdateLabelControl(this.lblAreasList, LanguageVersion, ProCommon.Properties.Resources.ResourceManager, null);
            UpdateLabelControl(this.lblRemark, LanguageVersion, ProCommon.Properties.Resources.ResourceManager, null);

            this.cmbeDefineAreaList.SelectedIndexChanged += CmbeDefineAreaList_SelectedIndexChanged;
        }
        protected internal virtual void FrmRegionModify_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        protected void LoadImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            bool isChs = LanguageVersion == ProCommon.Communal.Language.Chinese;
            string txtFilter = isChs ? "图像文件(*.BMP,*.JPG,*.JPEG,*.TIF)|*.bmp;*.jpg;*.jpeg;*.tif" : "ImageFile(*.BMP,*.JPG,*.JPEG,*.TIF)|*.bmp;*.jpg;*.jpeg;*.tif";
            ofd.Filter = txtFilter;
            ofd.FilterIndex = 0;
            string txtTitle = isChs ? "请选择一张图像文件" : "Select an image file";
            ofd.Title = txtTitle;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    HalconDotNet.HOperatorSet.ReadImage(out _hoImage, ofd.FileName);
                    DisplayData();
                }
                catch (HalconDotNet.HalconException hex)
                {
                    string txt = isChs ? "加载图像失败!\r\n" : "Load image failed !\r\n";
                    string caption=isChs?"错误信息":"Error Message";
                    ProCommon.UserDefForm.FrmMsgBox.Show(txt+hex.GetErrorMessage(),caption,ProCommon.UserDefForm.MyButtons.OK,ProCommon.UserDefForm.MyIcon.Error,isChs);
                }
            }
        }
        protected void GetImageSize(HalconDotNet.HObject img,out HalconDotNet.HTuple wdth,out HalconDotNet.HTuple hgh)
        {
            wdth = null;
            hgh = null;
            if(img!=null
                && img.IsInitialized())
            {
                HalconDotNet.HOperatorSet.GetImageSize(img, out wdth, out hgh);
            }
        }
        protected void DisplayData()
        {
            if (_hoImage != null
               && _hoImage.IsInitialized())
            {
                HalconDotNet.HTuple wdth,hgh;
                GetImageSize(_hoImage, out wdth, out hgh);

                if (_imgWidth ==null
                  ||_imgHeight == null
                  || _imgWidth != wdth
                  || _imgHeight != hgh)
                {
                    _imgWidth = wdth;
                    _imgHeight = hgh;
                    this.hWndCtrlDisplay.HalconWindow.SetPart(0, 0, _imgHeight, _imgWidth);
                    this.hWndCtrlDisplay.HalconWindow.SetDraw("margin");
                }              
                HalconDotNet.HOperatorSet.DispObj(_hoImage, this.hWndCtrlDisplay.HalconID);
            }               

            if (_hoResultRegion != null
                && _hoResultRegion.IsInitialized())
            {
                this.hWndCtrlDisplay.HalconWindow.SetColor(_resultRegionColor);               
                HalconDotNet.HOperatorSet.DispObj(_hoResultRegion, this.hWndCtrlDisplay.HalconID);
            }

          
            if (_selectedRegn != null
               && _selectedRegn.IsInitialized())
            {
                this.hWndCtrlDisplay.HalconWindow.SetColor(_selectedRegionColor);
                HalconDotNet.HOperatorSet.DispObj(_selectedRegn, this.hWndCtrlDisplay.HalconID);
            }

            if (_hoBrushRegion != null
               && _hoBrushRegion.IsInitialized())
            {
                this.hWndCtrlDisplay.HalconWindow.SetColor(_brushColor);             
                HalconDotNet.HOperatorSet.DispObj(_hoBrushRegion, this.hWndCtrlDisplay.HalconID);
            }

        }

        /// <summary>
        /// 更新SimpleButton控件
        /// </summary>
        /// <param name="sbtn"></param>
        /// <param name="lan"></param>
        protected internal void UpdateSimpleButton(DevExpress.XtraEditors.SimpleButton sbtn, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (sbtn != null
                && sbtn.Tag != null)
            {
                if (resourceManager != null)
                {
                    sbtn.Click -= Sbtn_Click;
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = sbtn.Tag.ToString();
                    sbtn.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                    sbtn.Click += Sbtn_Click;
                }
            }
        }

        protected internal void UpdateLabelControl(DevExpress.XtraEditors.LabelControl lblc, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager, string prefix)
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
    }
}