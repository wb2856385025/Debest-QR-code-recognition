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

namespace Pro2DBarcode.UI
{
    public partial class FrmSetRoutinePara : DevExpress.XtraEditors.XtraForm
    {
        public ProCommon.Communal.Language LanguageVersion { protected set; get; }
        public ProCommon.Communal.Camera SpecifiedCamera { set; get; }

        private Pro2DBarcode.Device.Device_Camera _devCam;
        private HalconDotNet.HTuple _imgFiles, _imgIndex;
        private HalconDotNet.HObject _tstImage, _inspectArea;

        private Pro2DBarcode.Algorithm.ImageProcess_InspectDataCode2D _imgProcess2DBarcode;
        private Pro2DBarcode.Config.VisionPara.VisionPara2DBarcode _para2DBarcode;
        public Pro2DBarcode.Config.VisionPara.VisionPara2DBarcode Para2DBarcode
        {
            set
            {
                if (value != null)
                {
                    _para2DBarcode = value;
                    this.visionPara2DBarcodeBindingSource.DataSource = _para2DBarcode;
                                       
                    if (!string.IsNullOrEmpty(_para2DBarcode.PathForInspectArea))
                    {
                        if (System.IO.File.Exists(_para2DBarcode.PathForInspectArea))
                        {
                            if (_inspectArea != null
                                && _inspectArea.IsInitialized())
                                _inspectArea.Dispose();
                            HalconDotNet.HOperatorSet.ReadRegion(out _inspectArea, new HalconDotNet.HTuple(_para2DBarcode.PathForInspectArea));
                        }
                    }
                }
            }
            get
            {
                return _para2DBarcode;
            }
        }

        public string RoutineName { set; get; }
        public string RoutineDirectory { set; get; }

        private FrmSetRoutinePara()
        {
            InitializeComponent();
        }
        public FrmSetRoutinePara(ProCommon.Communal.Language lan) : this()
        {
            LanguageVersion = lan;
            this.Load += FrmSetRoutinePara_Load;
            this.FormClosing += FrmSetRoutinePara_FormClosing;
        }

        protected internal virtual void FrmSetRoutinePara_Load(object sender, EventArgs e)
        {
            Init();
        }

        protected internal void FrmSetRoutinePara_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        protected internal void Init()
        {
            InitFieldAndProperty();
            InitControl();
        }

        protected internal virtual void InitFieldAndProperty()
        {
            
        }

        protected internal virtual void InitControl()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);
            string str = this.Tag.ToString();
            this.Text = (isChs ? Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str))
                + "--["+RoutineName+"]";
            UpdateSimpleButton(this.sbtnGrabContinue, LanguageVersion);
            UpdateSimpleButton(this.sbtnGrabStop, LanguageVersion);
            UpdateSimpleButton(this.sbtnGrabContinue, LanguageVersion);
            UpdateSimpleButton(this.sbtnCameraSet, LanguageVersion);
            UpdateSimpleButton(this.sbtnGrabOnceAndTest, LanguageVersion);
            UpdateSimpleButton(this.sbtnLoadImage, LanguageVersion);
            UpdateSimpleButton(this.sbtnTstOffLine, LanguageVersion);
            UpdateSimpleButton(this.sbtnLstImage, LanguageVersion);
            UpdateSimpleButton(this.sbtnNxtImage, LanguageVersion);
            UpdateSimpleButton(this.sbtnInspectArea, LanguageVersion);
            UpdateSimpleButton(this.sbtnApplyParameter, LanguageVersion);         

            UpdateCheckEdit(this.chkeSaveImageAll, LanguageVersion);
            UpdateCheckEdit(this.chkeSaveImageOK, LanguageVersion);
            UpdateCheckEdit(this.chkeSaveImageNG, LanguageVersion);
            UpdateCheckEdit(this.chkeEnableAlgorithm, LanguageVersion);

            UpdateLabelControl(this.lblCoordinate, LanguageVersion,null);
            UpdateLabelControl(this.lblGrayValue, LanguageVersion,null);
            UpdateLabelControl(this.lblDirectoryPrompt, LanguageVersion,null);
            UpdateLabelControl(this.lblDirectoryText, LanguageVersion,null);
            UpdateLabelControl(this.lblImageName, LanguageVersion,null);

            UpdateLabelControl(this.lblToBeFoundNum, LanguageVersion, null);
            UpdateLabelControl(this.lblTimeOut, LanguageVersion, null);
            UpdateLabelControl(this.lblCodeType, LanguageVersion, null);
            UpdateLabelControl(this.lblNotFoundColor, LanguageVersion, null);
            UpdateLabelControl(this.lblFoundColor, LanguageVersion, null);          
                     
            UpdateLabelControl(this.lblGain, LanguageVersion, null);
            UpdateLabelControl(this.lblExposureTime, LanguageVersion, null);
            UpdateGroupControl(this.grpPreProcessParameter, LanguageVersion);
            UpdateGroupControl(this.grpAlgorithmParameter, LanguageVersion);
            UpdateGroupControl(this.grpProcessParameter, LanguageVersion);

            UpdateSpinEdit(this.speGain, LanguageVersion);
            UpdateSpinEdit(this.speExposureTime, LanguageVersion);

            this.hwndDisplay.HMouseMove += HWindowControlDisplay_HMouseMove;

            UpdateSimpleButton(this.sbtnSaveParameter, LanguageVersion);

            if (_devCam == null)
                _devCam = (Pro2DBarcode.Device.Device_Camera)Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Camera];
            _devCam.EnableAlgorithm = false;
        }

        private void HWindowControlDisplay_HMouseMove(object sender, HalconDotNet.HMouseEventArgs e)
        {
            if (_tstImage != null
                && _tstImage.IsInitialized())
            {
                try
                {
                    HalconDotNet.HTuple gryV, chnlCount;
                    int y, x;
                    y = Convert.ToInt32(e.Y);
                    x = Convert.ToInt32(e.X);
                    HalconDotNet.HOperatorSet.GetGrayval(_tstImage, y, x, out gryV);
                    this.lblCoordinate.Text = "Y:" + y.ToString() + ",X:" + x.ToString();
                    chnlCount = gryV.TupleLength();
                    if (chnlCount.TupleEqual(new HalconDotNet.HTuple(1)))
                    {
                        this.lblGrayValue.Text = gryV.I.ToString("00");
                    }
                    else if (chnlCount.TupleEqual(new HalconDotNet.HTuple(3)))
                    {
                        this.lblGrayValue.Text = "R:" + gryV[0].I.ToString("00")
                                               + ",G:" + gryV[1].I.ToString("00")
                                              + ",B:" + gryV[2].I.ToString("00");
                    }
                }
                catch (HalconDotNet.HOperatorException hex)
                {

                }

            }
        }

        protected internal virtual void UpdateSimpleButton(DevExpress.XtraEditors.SimpleButton sbtn, ProCommon.Communal.Language lan)
        {
            if (sbtn != null
             && sbtn.Tag != null)
            {
                sbtn.Click -= Sbtn_Click;
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = sbtn.Tag.ToString();
                sbtn.Text = isChs ? Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str);
                sbtn.Click += Sbtn_Click;
            }
        }

        protected internal virtual void UpdateCheckEdit(DevExpress.XtraEditors.CheckEdit chke, ProCommon.Communal.Language lan)
        {
            if (chke != null
             && chke.Tag != null)
            {
                chke.CheckedChanged -= Chke_CheckedChanged;
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = chke.Tag.ToString();
                chke.Text = isChs ? Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str);
                chke.Checked = false;
                chke.CheckedChanged += Chke_CheckedChanged;
            }
        }

        protected internal virtual void UpdateLabelControl(DevExpress.XtraEditors.LabelControl lbl, ProCommon.Communal.Language lan,string prefix)
        {
            if (lbl != null
             && lbl.Tag != null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = lbl.Tag.ToString();
                lbl.Text = (isChs ? Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str))+ prefix;
            }
        }

        protected internal virtual void UpdateGroupControl(DevExpress.XtraEditors.GroupControl grp, ProCommon.Communal.Language lan)
        {
            if (grp != null
             && grp.Tag != null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = grp.Tag.ToString();
                grp.Text = (isChs ? Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str));
            }
        }

        protected internal virtual void UpdateSpinEdit(DevExpress.XtraEditors.SpinEdit spe, ProCommon.Communal.Language lan)
        {
            if (spe != null
             && spe.Tag != null)
            {
                spe.ValueChanged -= Spe_ValueChanged;              
                spe.ValueChanged += Spe_ValueChanged;
            }
        }

        protected internal virtual void Spe_ValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit spe = sender as DevExpress.XtraEditors.SpinEdit;
            if (spe != null)
            {
                switch (spe.Tag.ToString())
                {
                    case "SPE_CAMERAEXPOSURE":
                        if (_devCam == null)
                            _devCam = (Pro2DBarcode.Device.Device_Camera)Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Camera];
                        _devCam.CamHandleListNew[SpecifiedCamera.ID].SetExposureTime(Convert.ToSingle(this.speExposureTime.EditValue));
                        break;
                    case "SPE_CAMERAGAIN":
                        if (_devCam == null)
                            _devCam = (Pro2DBarcode.Device.Device_Camera)Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Camera];
                        _devCam.CamHandleListNew[SpecifiedCamera.ID].SetGain(Convert.ToSingle(this.speGain.EditValue));
                        break;
                    default:
                        break;
                }
            }
        }

        protected internal virtual void Chke_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit chke = sender as DevExpress.XtraEditors.CheckEdit;
            if (chke != null
                && chke.Tag != null)
            {
                switch (chke.Tag.ToString())
                {
                    case "CHKE_SAVEALL":
                        _para2DBarcode.IsSaveImageAll = chke.Checked;
                        break;
                    case "CHKE_SAVEOK":
                        _para2DBarcode.IsSaveImageOK = chke.Checked;
                        break;
                    case "CHKE_SAVENG":
                        _para2DBarcode.IsSaveImageNG = chke.Checked;
                        break;
                    case "CHKE_ENABLEALGORITHM":
                        if (_devCam == null)
                            _devCam = (Pro2DBarcode.Device.Device_Camera)Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Camera];
                        _devCam.EnableAlgorithm = chke.Checked;                        
                        break;
                    default: break;
                }
            }
        }

        protected internal virtual void Sbtn_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if (sbtn != null
                && sbtn.Tag != null)
            {
                bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);
                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_GRABCONTINUE":
                        {
                            if (SpecifiedCamera != null)
                            {
                                this.sbtnGrabContinue.Enabled = false;
                                this.sbtnGrabStop.Enabled = true;
                                this.sbtnCameraSet.Enabled = true;
                                this.sbtnGrabOnceAndTest.Enabled = false;
                                this.sbtnLoadImage.Enabled = false;
                                this.sbtnTstOffLine.Enabled = false;
                                this.sbtnLstImage.Enabled = false;
                                this.sbtnNxtImage.Enabled = false;                              
                                this.sbtnSaveParameter.Enabled = false;

                                if (_devCam == null)
                                    _devCam = (Pro2DBarcode.Device.Device_Camera)Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Camera];
                                _devCam.IsDebug = true;
                                _devCam.CamHandleListNew[SpecifiedCamera.ID].SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.Continue, 1);
                            }
                        }
                        break;
                    case "SBTN_GRABSTOP":
                        {
                            this.sbtnGrabContinue.Enabled = true;
                            this.sbtnGrabStop.Enabled = true;
                            this.sbtnCameraSet.Enabled = true;
                            this.sbtnGrabOnceAndTest.Enabled = true;
                            this.sbtnLoadImage.Enabled = true;
                            this.sbtnTstOffLine.Enabled = true;
                            this.sbtnLstImage.Enabled = true;
                            this.sbtnNxtImage.Enabled = true;                         
                            this.sbtnSaveParameter.Enabled = true;

                            if (SpecifiedCamera != null)
                            {
                                if (_devCam == null)
                                    _devCam = (Pro2DBarcode.Device.Device_Camera)Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Camera];
                                _devCam.IsDebug = true;
                                _devCam.CamHandleListNew[SpecifiedCamera.ID].SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.SoftTrigger, 1);
                            }
                        }
                        break;
                    case "SBTN_CAMERASET":
                        if (SpecifiedCamera != null)
                        {
                            if (_devCam == null)
                                _devCam = (Pro2DBarcode.Device.Device_Camera)Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Camera];
                            _devCam.CamHandleListNew[SpecifiedCamera.ID].CreateCameraSetPage(this.Handle, "");
                            _devCam.CamHandleListNew[SpecifiedCamera.ID].ShowCameraSetPage();
                        }
                        break;
                    case "SBTN_TESTONLINE":
                        {
                            this.sbtnGrabContinue.Enabled = false;
                            this.sbtnGrabStop.Enabled = true;
                            this.sbtnCameraSet.Enabled = true;
                            this.sbtnGrabOnceAndTest.Enabled = true;
                            this.sbtnLoadImage.Enabled = false;
                            this.sbtnTstOffLine.Enabled = false;
                            this.sbtnLstImage.Enabled = false;
                            this.sbtnNxtImage.Enabled = false;                          
                            this.sbtnSaveParameter.Enabled = true;
                            if (SpecifiedCamera != null)
                            {
                                if (_devCam == null)
                                    _devCam = (Pro2DBarcode.Device.Device_Camera)Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Camera];
                                _devCam.IsDebug = true;
                                _devCam.IsInitProcess = true;
                                _devCam.CamHandleListNew[SpecifiedCamera.ID].IsImageGrabbed = false;
                                _devCam.CamHandleListNew[SpecifiedCamera.ID].SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.SoftTrigger, 1);
                                _devCam.CamHandleListNew[SpecifiedCamera.ID].SoftTriggerOnce();

                                int cnt = 0; _tstImage = null; bool istimeout = false;
                                while (!_devCam.CamHandleListNew[SpecifiedCamera.ID].IsImageGrabbed)
                                {
                                    if (cnt > 500)
                                    {
                                        istimeout = true;
                                        break;
                                    }

                                    System.Threading.Thread.Sleep(20);
                                    cnt++;
                                }

                                if (!istimeout)
                                    _tstImage = _devCam.CamHandleListNew[SpecifiedCamera.ID].HoImage;
                                else
                                {
                                    string txt = isChs ? "相机采图超时!" : "Timeout when acquire image!";
                                    string caption = isChs ? "错误信息" : "Error Message";                                 
                                    ProCommon.UserDefForm.FrmMsgBox.Show(txt, caption,
                                    ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Error, isChs);
                                }
                            }
                        }
                        break;
                    case "SBTN_LOADIMAGE":
                        {
                            this.sbtnGrabContinue.Enabled = false;
                            this.sbtnGrabStop.Enabled = true;
                            this.sbtnCameraSet.Enabled = false;
                            this.sbtnGrabOnceAndTest.Enabled = false;
                            this.sbtnLoadImage.Enabled = true;
                            this.sbtnTstOffLine.Enabled = true;
                            this.sbtnLstImage.Enabled = true;
                            this.sbtnNxtImage.Enabled = true;                         
                            this.sbtnSaveParameter.Enabled = true;

                            System.Windows.Forms.FolderBrowserDialog fbd = new FolderBrowserDialog();
                            fbd.SelectedPath = Manager.ConfigManager.DirectoryBase;
                            if (fbd.ShowDialog() == DialogResult.OK)
                            {
                                try
                                {
                                    _imgFiles = new HalconDotNet.HTuple();
                                    if (ProVision.Communal.Functions.ListImageFile(fbd.SelectedPath, new HalconDotNet.HTuple(), new HalconDotNet.HTuple(), out _imgFiles))
                                    {
                                        if (_tstImage != null
                                            && _tstImage.IsInitialized())
                                            _tstImage.Dispose();
                                        if (_imgFiles.TupleNotEqual(new HalconDotNet.HTuple()))
                                        {
                                            _imgIndex = 0;
                                            HalconDotNet.HOperatorSet.ReadImage(out _tstImage, _imgFiles.TupleSelect(_imgIndex));
                                            HalconDotNet.HTuple w, h;
                                            HalconDotNet.HOperatorSet.GetImageSize(_tstImage, out w, out h);
                                            HalconDotNet.HOperatorSet.SetPart(this.hwndDisplay.HalconWindow, new HalconDotNet.HTuple(0), new HalconDotNet.HTuple(0), h, w);
                                            HalconDotNet.HOperatorSet.DispObj(_tstImage, this.hwndDisplay.HalconWindow);
                                        }
                                    }
                                }
                                catch (HalconDotNet.HalconException hex)
                                {
                                    string txt = isChs ? "加载图像异常!\r\n" : "An error occured while loading an image!";
                                    string caption = isChs ? "错误信息" : "Error Message";                                 
                                    ProCommon.UserDefForm.FrmMsgBox.Show(txt + hex.GetErrorMessage(), caption,
                                    ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Error, isChs);
                                }
                            }

                        }
                        break;
                    case "SBTN_TESTOFFLINE":
                        {
                            if (_imgProcess2DBarcode == null)
                                _imgProcess2DBarcode = new Algorithm.ImageProcess_InspectDataCode2D();

                            _imgProcess2DBarcode.IsChinese = (LanguageVersion == ProCommon.Communal.Language.Chinese);

                            if (_tstImage != null
                                && _tstImage.IsInitialized())
                            {
                                _imgProcess2DBarcode.Para2DBarcode = Para2DBarcode;
                                _imgProcess2DBarcode.InitProcess();
                                _imgProcess2DBarcode.SetHwndIndex(-1);
                                _imgProcess2DBarcode.SetCameraIndex(0);
                                _imgProcess2DBarcode.SetEnableAlgorithm(true);//强制为启用图像处理
                                _imgProcess2DBarcode.Process(_tstImage);                              
                            }
                        }
                        break;
                    case "SBTN_LAST":
                        {
                            if (_imgFiles.TupleNotEqual(new HalconDotNet.HTuple()))
                            {
                                if (_imgIndex.TupleLessEqual(0))
                                    _imgIndex = _imgFiles.TupleLength() - 1;
                                else
                                    _imgIndex -= 1;

                                HalconDotNet.HOperatorSet.ReadImage(out _tstImage, _imgFiles.TupleSelect(_imgIndex));
                                HalconDotNet.HTuple w, h;
                                HalconDotNet.HOperatorSet.GetImageSize(_tstImage, out w, out h);
                                HalconDotNet.HOperatorSet.SetPart(this.hwndDisplay.HalconWindow, new HalconDotNet.HTuple(0), new HalconDotNet.HTuple(0), h, w);
                                HalconDotNet.HOperatorSet.DispObj(_tstImage, this.hwndDisplay.HalconWindow);
                            }
                        }
                        break;
                    case "SBTN_NEXT":
                        {
                            if (_imgFiles.TupleNotEqual(new HalconDotNet.HTuple()))
                            {
                                if (_imgIndex.TupleLess(_imgFiles.TupleLength() - 1))
                                    _imgIndex += 1;
                                else
                                    _imgIndex = 0;

                                HalconDotNet.HOperatorSet.ReadImage(out _tstImage, _imgFiles.TupleSelect(_imgIndex));
                                HalconDotNet.HTuple w, h;
                                HalconDotNet.HOperatorSet.GetImageSize(_tstImage, out w, out h);
                                HalconDotNet.HOperatorSet.SetPart(this.hwndDisplay.HalconWindow, new HalconDotNet.HTuple(0), new HalconDotNet.HTuple(0), h, w);
                                HalconDotNet.HOperatorSet.DispObj(_tstImage, this.hwndDisplay.HalconWindow);
                            }
                        }
                        break;
                    case "SBTN_INSPECTAREA":
                        {
                            if (_tstImage != null
                               && _tstImage.IsInitialized())
                            {
                                UI.UI_ModifyRegion uiModiftRegion = new UI_ModifyRegion(LanguageVersion,_tstImage);
                                uiModiftRegion.ShowDialog(this);

                                //注意:此处获取多个检测区域
                                _inspectArea = uiModiftRegion.GetResultRegion();

                                if (_inspectArea != null
                                   && _inspectArea.IsInitialized())
                                {
                                    HalconDotNet.HOperatorSet.SetColor(this.hwndDisplay.HalconWindow, new HalconDotNet.HTuple("blue"));
                                    HalconDotNet.HOperatorSet.SetDraw(this.hwndDisplay.HalconWindow, new HalconDotNet.HTuple("margin"));
                                    HalconDotNet.HOperatorSet.DispObj(_inspectArea, this.hwndDisplay.HalconWindow);
                                }
                            }
                            else
                            {
                                string txt = isChs ? "未加载或采集图像!" : "Invalid image!";
                                string caption = isChs ? "警告信息" : "Warning Message";                              
                                ProCommon.UserDefForm.FrmMsgBox.Show(txt, caption,
                                  ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                            }
                        }
                        break;
                    case "SBTN_APPLYPARAMETER":
                        {
                            if (_devCam == null)
                                _devCam = (Pro2DBarcode.Device.Device_Camera)Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Camera];
                            _devCam.IsInitProcess = true;
                        }
                        break;
                    case "SBTN_CALCULATEJUDGE":
                        break;
                    case "SBTN_SAVEPARAMETER":
                        {
                            if (Para2DBarcode!= null)
                            {
                                if (!string.IsNullOrEmpty(RoutineDirectory))
                                {
                                    if (!System.IO.Directory.Exists(RoutineDirectory))
                                    {
                                        System.IO.Directory.CreateDirectory(RoutineDirectory);
                                    }

                                    string[] files = System.IO.Directory.GetFiles(RoutineDirectory);
                                    if (files != null
                                        && files.Length > 0)
                                    {
                                        string fileName = System.IO.Path.GetFileNameWithoutExtension(RoutineDirectory + "\\" + RoutineName);
                                        string extention = System.IO.Path.GetExtension(RoutineDirectory + "\\" + RoutineName);

                                        System.IO.File.Delete(RoutineDirectory + "\\" + fileName + extention);
                                    }
                                    try
                                    {
                                        if (_inspectArea != null
                                           && _inspectArea.IsInitialized())
                                        {
                                            HalconDotNet.HOperatorSet.WriteRegion(_inspectArea, new HalconDotNet.HTuple(RoutineDirectory + "\\" + RoutineName + "_trackArea.hobj"));

                                            Para2DBarcode.PathForInspectArea = RoutineDirectory + "\\" + RoutineName + "_trackArea.hobj";

                                            using (var fs = new System.IO.FileStream(RoutineDirectory + "\\" + RoutineName + ".pro", System.IO.FileMode.Create))
                                            {
                                                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                                                bf.Serialize(fs, Para2DBarcode);
                                            }
                                            string txt = isChs ? "程式参数保存成功!" : "Save parameter file successfully !";                                          
                                            ProCommon.UserDefForm.FrmMsgBox.Show(txt,isChs);                                          
                                        }
                                        else
                                        {
                                            string txt = isChs ? "程式参数保存失败!\r\n未定义检测区域!" : "An error occured while saving parameter!\r\n No inspect area.";
                                            string caption = isChs ? "警告信息" : "Warning Message";
                                            System.Windows.Forms.MessageBox.Show(txt, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    catch (System.Exception ex)
                                    {
                                        string txt = isChs ? "程式参数保存失败!\r\n" : "An error occured while saving parameter!\r\n";
                                        string caption = isChs ? "错误信息" : "Error Message";                                      
                                        ProCommon.UserDefForm.FrmMsgBox.Show(txt, caption,
                                        ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Error, isChs);
                                    }
                                }
                                else
                                {
                                    System.Windows.Forms.MessageBox.Show("未选择程式!");
                                    string txt = isChs ? "未选择程式!" : "No routine selected !";
                                    string caption = isChs ? "警告信息" : "Warning Message";                                   
                                    ProCommon.UserDefForm.FrmMsgBox.Show(txt, caption,
                                      ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }       
    }
}