using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pro2DBarcode.UI
{
    public partial class UI_ModifyRegion : ProCommon.UserDefForm.FrmModifyRegion
    {
        bool _isErasingRegion, _isObtainedResultRegion, _isTransmitRegion, _isErasedRegion;//是否在擦除区域,是否获取到待擦除区域,是否传入待擦除区域,是否完成擦除
        double _brushZoomFactor;
      

        public UI_ModifyRegion()
        {
            InitializeComponent();
        }

        public UI_ModifyRegion(ProCommon.Communal.Language lan,HalconDotNet.HObject img):base(lan,img)
        {
            InitializeComponent();
        }

        public UI_ModifyRegion(ProCommon.Communal.Language lan,HalconDotNet.HObject img, HalconDotNet.HObject region) : base(lan, img,region)
        {
            InitializeComponent();
            _isTransmitRegion = true;
        }


        #region 必须覆写的成员

        protected override void Sbtn_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if (sbtn != null)
            {
                bool isChs = LanguageVersion == ProCommon.Communal.Language.Chinese;
                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_LOAD":
                        LoadImage();
                        break;
                    case "SBTN_DEFINEREGION":
                        {
                            if (_hoImage != null
                                && _hoImage.IsInitialized())
                            {
                                UI.UI_ROIInteractive roiRg = new UI.UI_ROIInteractive(LanguageVersion,_hoImage);
                                roiRg.ShowDialog();

                                if (_hoRawRegion != null
                                    && _hoRawRegion.IsInitialized())
                                    _hoRawRegion.Dispose();

                                HalconDotNet.HOperatorSet.GenEmptyObj(out _hoRawRegion);

                                System.Collections.ArrayList roiList = roiRg.GetROIList();
                                if (roiList != null
                                    && roiList.Count>0)
                                {
                                    this.cmbeDefineAreaList.Properties.Items.Clear();
                                    for (int i = 0; i < roiList.Count; i++)
                                    {
                                        HalconDotNet.HOperatorSet.ConcatObj(_hoRawRegion, ((ProVision.InteractiveROI.ROI)roiList[i]).GetModelRegion(), out _hoRawRegion);
                                        this.cmbeDefineAreaList.Properties.Items.Add("Regn[" + i.ToString("00")+"]");
                                        
                                    }

                                    this.cmbeDefineAreaList.SelectedIndex = 0;
                                    this.cmbeDefineAreaList.EditValue = "Regn[00]";
                                    HalconDotNet.HOperatorSet.SelectObj(_hoRawRegion, out _selectedRegn, 1);
                                   
                                }


                                if (_hoResultRegion != null
                                    && _hoResultRegion.IsInitialized())
                                    _hoResultRegion.Dispose();
                                _hoResultRegion = _hoRawRegion;

                                if (_hoResultRegion != null
                                    && _hoResultRegion.IsInitialized())
                                    _isObtainedResultRegion = true;

                                roiRg.Dispose();
                                DisplayData();
                            }
                            else
                            {
                                string txt = isChs ? "未加载图像!" : "No valid image !";
                                string caption = isChs ? "警告信息" : "Warning Message";
                                ProCommon.UserDefForm.FrmMsgBox.Show(txt,caption,ProCommon.UserDefForm.MyButtons.OK,ProCommon.UserDefForm.MyIcon.Warning,isChs);
                            }
                        }
                        break;
                    case "SBTN_ERASEREGION":
                        {
                            if (_isObtainedResultRegion
                                || _isTransmitRegion)
                            {
                                if(_selectedRegn!=null
                                    && _selectedRegn.IsInitialized())
                                {
                                    _isErasingRegion = true;
                                    _isErasedRegion = false;
                                    EraseRegion();
                                }
                                else
                                {
                                    string txt = isChs ? "未选择修改区域!" : "No valid selected area !";
                                    string caption = isChs ? "警告信息" : "Warning Message";
                                    ProCommon.UserDefForm.FrmMsgBox.Show(txt, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                                }
                            }
                            else
                            {
                                string txt = isChs ? "未定义区域!" : "No valid area !";
                                string caption = isChs ? "警告信息" : "Warning Message";
                                ProCommon.UserDefForm.FrmMsgBox.Show(txt, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                            }
                        }
                        break;
                    case "SBTN_EXIT":
                        this.Close();
                        break;
                    default: break;
                }
            }
        }

        protected override void FrmRegionModify_Load(object sender, EventArgs e)
        {
            Init();
        }

        protected override void FrmRegionModify_SizeChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        protected override void CmbeDefineAreaList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_hoRawRegion != null
                && _hoRawRegion.IsInitialized())
            {
                if(this.cmbeDefineAreaList.SelectedIndex!=-1)
                {
                    if (_selectedRegn != null && _selectedRegn.IsInitialized()) _selectedRegn.Dispose();
                    HalconDotNet.HOperatorSet.SelectObj(_hoRawRegion, out _selectedRegn, this.cmbeDefineAreaList.SelectedIndex+1);
                    DisplayData();
                }               
            }
        }

        #endregion

        protected override void Init()
        {
            InitFieldAndProperty();
            UpdateControl();
        }

        protected override void InitFieldAndProperty()
        {
            base.InitFieldAndProperty();

            //引用变量复位           
            _hoRawRegion = null;
            _hoBrushRegion = null;
            _hoResultRegion = null;
            _imgWidth = null;
            _imgHeight = null;
            _brushColor = "white";
            _resultRegionColor = "yellow";
            _selectedRegionColor = "green";

            _isErasingRegion = false;
            _isErasedRegion = false;
            _isObtainedResultRegion = false;
            _brushZoomFactor = 15.0d;
        }

        protected override void UpdateControl()
        {
            base.UpdateControl();
        }

        protected override void FrmRegionModify_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool isChs = LanguageVersion == ProCommon.Communal.Language.Chinese;
            if (!_isErasedRegion)
            {
                //string txt = isChs ? "擦除区域未完成!\r\n是否离开?" : "Erasing area not finished!\r\n Exit now ?";
                //string caption = isChs ? "询问信息" : "Question Message";

                //if (!(ProCommon.UserDefForm.FrmMsgBox.Show(txt, caption, 
                //    ProCommon.UserDefForm.MyButtons.YesNo, ProCommon.UserDefForm.MyIcon.Question, isChs) == DialogResult.Yes))
                //    e.Cancel = true;
            }
            else
            {
                //string txt = isChs ? "擦除区域完成!" : "Erasing area finished!";
                //string caption = isChs ? "提示信息" : "Prompt Message";
                //ProCommon.UserDefForm.FrmMsgBox.Show(txt, caption,
                //    ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Information, isChs);
            }
        }

        /// <summary>
        /// 获取结果区域
        /// [原始区域擦除后的区域]
        /// </summary>
        /// <returns></returns>
        public HalconDotNet.HObject GetResultRegion()
        {
            return _hoResultRegion;
        }

        /// <summary>
        /// 擦除区域
        /// </summary>
        private void EraseRegion()
        {
            bool isChs = LanguageVersion == ProCommon.Communal.Language.Chinese;
            try
            {
                //擦除前的准备
                this.hWndCtrlDisplay.Focus();
                HalconDotNet.HOperatorSet.SetSystem("flush_graphic", "false");
                this.hWndCtrlDisplay.HalconWindow.ClearWindow();
                this.hWndCtrlDisplay.HalconWindow.SetDraw("margin");
                DisplayData();
                HalconDotNet.HTuple mr, mc, mbtntype;
                HalconDotNet.HOperatorSet.SetSystem("flush_graphic", "true");
                while (_isErasingRegion)
                {
                    try
                    {
                        HalconDotNet.HOperatorSet.GetMposition(this.hWndCtrlDisplay.HalconWindow, out mr, out mc, out mbtntype);
                        if (_hoBrushRegion != null
                            && _hoBrushRegion.IsInitialized())
                            _hoBrushRegion.Dispose();
                        HalconDotNet.HOperatorSet.GenRectangle1(out _hoBrushRegion, mr - _brushZoomFactor, mc - _brushZoomFactor, mr + _brushZoomFactor, mc + _brushZoomFactor);
                        DisplayData();

                        if (mbtntype.TupleEqual(new HalconDotNet.HTuple(1))) //按下鼠标左键
                        {
                            HalconDotNet.HObject tmp;

                            HalconDotNet.HOperatorSet.Difference(_selectedRegn, _hoBrushRegion, out tmp);

                            _selectedRegn.Dispose();
                            _selectedRegn = tmp;
                            int num = _hoResultRegion.CountObj();
                            HalconDotNet.HObject tmp2 = new HalconDotNet.HObject();
                            HalconDotNet.HObject tmp3 = new HalconDotNet.HObject();
                            for(int i=0;i<num;i++)
                            {
                                tmp2.Dispose();
                                if(i!=this.cmbeDefineAreaList.SelectedIndex)
                                {
                                    HalconDotNet.HOperatorSet.SelectObj(_hoResultRegion, out tmp2, i + 1);
                                }
                                else
                                {
                                    HalconDotNet.HOperatorSet.CopyObj(_selectedRegn, out tmp2, 1, 1);
                                }
                               
                                HalconDotNet.HOperatorSet.ConcatObj(tmp3, tmp2, out tmp3);
                            }

                            tmp2.Dispose();
                            _hoResultRegion.Dispose();
                            _hoResultRegion = tmp3;

                            HalconDotNet.HOperatorSet.WaitSeconds(0.001);
                        }
                        else if (mbtntype.TupleEqual(new HalconDotNet.HTuple(4))) //按下鼠标右键
                        {
                            _isErasingRegion = false;

                            if (_hoBrushRegion != null
                            && _hoBrushRegion.IsInitialized())
                                _hoBrushRegion.Dispose();

                            DisplayData();
                            _isErasedRegion = true;
                        }

                    }
                    catch (HalconDotNet.HalconException hex2)
                    {
                        //_isEraseRegion = false;
                        //MessageBox.Show("获取鼠标位置异常!\r\n"+hex2.GetErrorMessage(),"错误信息",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    }
                    System.Threading.Thread.Sleep(100);
                    Application.DoEvents();
                }

            }
            catch (HalconDotNet.HalconException hex1)
            {
                _isErasingRegion = false;
                string txt = isChs ? "擦除区域异常!\r\n" : "Erasing area failed!\r\n";
                string caption = isChs ? "错误信息" : "Error Message";
                ProCommon.UserDefForm.FrmMsgBox.Show(txt + hex1.GetErrorMessage(), caption,
                    ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Error, isChs);
            }
        }
    }
}
