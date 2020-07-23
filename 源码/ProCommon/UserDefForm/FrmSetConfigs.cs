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
    public partial class FrmSetConfigs : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 软件语言版本
        /// </summary>
        public ProCommon.Communal.Language LanguageVersion { protected set; get; }

        protected internal bool _isChs;
        protected internal string _txt, _caption;
        protected internal int _cameraSelectedIdx, _boardSelectedIdx, _serialPortSelectedIdx, _socketSelectedIdx, _webSelectedIdx;
        protected internal int _cameraBrandIdx, _serialPortIdx;


        private FrmSetConfigs()
        {
            InitializeComponent();
        }

        public FrmSetConfigs(ProCommon.Communal.Language lan):this()
        {
            LanguageVersion = lan;
            _isChs = LanguageVersion == ProCommon.Communal.Language.Chinese;
            this.Load += FrmSetConfigs_Load;
        }


        #region 可能覆写的成员

        protected internal virtual void FrmSetConfigs_Load(object sender, EventArgs e)
        {
            InitFieldAndProperty();
            UpdateControl();
        }

        protected internal virtual void Sbtn_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if (sbtn != null
                && sbtn.Tag != null)
            {
                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_CONFIRM":
                        ConfirmClick();
                        break;
                    case "SBTN_EXIT":
                        ExitClick();
                        break;
                    case "SBTN_ENUMERATESERIALPORT":
                         EnumerateSerialPort();
                        break;
                    case "SBTN_CAMERACONFIRMUPDATE":
                        ConfirmUpdateCamera();
                        break;
                    case "SBTN_SERIALPORTCONFIRMUPDATE":
                        ConfirmUpdateSerialPort();
                        break;
                    case "SBTN_ADDAXES":
                        AddAxes();
                        break;
                    case "SBTN_DELETEAXES":
                        DeleteAxes();
                        break;
                    case "SBTN_UPDATEAXES":
                        UpdateAxes();
                        break;
                    case "SBTN_UPLOADPARAMETER":
                        UpLoadParameter();
                        break;
                    case "SBTN_DOWNLOADPARAMETER":
                        DownLoadParameter();
                        break;
                    case "SBTN_ADDINPUT":
                        AddInput();
                        break;
                    case "SBTN_DELETEINPUT":
                        DeleteInput();
                        break;
                    case "SBTN_UPDATEINPUT":
                        UpdateInput();
                        break;
                    case "SBTN_ADDOUTPUT":
                        AddOutput();
                        break;
                    case "SBTN_DELETEOUTPUT":
                        DeleteOutput();
                        break;
                    case "SBTN_UPDATEOUTPUT":
                        UpdateOutput();
                        break;
                    case "SBTN_CAMERACALIBRATE":
                        CalibrateCamera();
                        break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// 枚举串口单击事件
        /// </summary>      
        protected internal virtual void EnumerateSerialPort()
        {
            string[] names=System.IO.Ports.SerialPort.GetPortNames();
            if(names!=null
                && names.Count()>0)
            {
                this.cmbeSerialPorts.Properties.Items.Clear();
                this.cmbeSerialPorts.Properties.Items.AddRange(names);
            }
            else
            {
                _txt = _isChs ? "未发现串口!" : "No serialport detected !";
                _caption = _isChs ? "提示信息" : "Information Message";
                ProCommon.UserDefForm.FrmMsgBox.Show(_txt, _caption,
                             ProCommon.UserDefForm.MyButtons.YesNo, ProCommon.UserDefForm.MyIcon.Warning, _isChs);
            }

        }

        /// <summary>
        /// 确认单击事件
        /// </summary>      
        protected internal virtual void ConfirmClick()
        {
            SaveConfig();
            this.Close();
        }

        /// <summary>
        /// 退出单击事件
        /// </summary>     
        protected internal virtual void ExitClick()
        {
            _txt = _isChs ? "是否保存配置,然后退出?" : "Save the config ,and then exit ?";
            _caption = _isChs ? "询问信息" : "Question Message";
            if (ProCommon.UserDefForm.FrmMsgBox.Show(_txt, _caption,
                            ProCommon.UserDefForm.MyButtons.YesNo, ProCommon.UserDefForm.MyIcon.Warning, _isChs) != DialogResult.No)
            {
                SaveConfig();
            }

            this.Close();
        }

        protected internal virtual bool SaveConfig()
        {
            return false;
        }

        /// <summary>
        /// 串口删除
        /// </summary>
        protected internal virtual void DeleteSerialPort()
        {

        }

        /// <summary>
        /// 串口修改
        /// </summary>
        protected internal virtual void ModifySerialPort()
        {

        }

        /// <summary>
        /// 串口新增
        /// </summary>
        protected internal virtual void AddSerialPort()
        {

        }

        /// <summary>
        /// 串口确认更新
        /// </summary>
        protected internal virtual void ConfirmUpdateSerialPort()
        {

        }

        /// <summary>
        /// 标定相机
        /// </summary>
        protected internal virtual void CalibrateCamera()
        {

        }

        /// <summary>
        /// 相机确认更新
        /// </summary>
        protected internal virtual void ConfirmUpdateCamera()
        {

        }

        /// <summary>
        /// 相机删除
        /// </summary>
        protected internal virtual void DeleteCamera()
        {

        }

        /// <summary>
        /// 相机修改
        /// </summary>
        protected internal virtual void ModifyCamera()
        {
            
        }

        /// <summary>
        /// 相机新增
        /// </summary>
        protected internal virtual void AddCamera()
        {

        }

        protected internal virtual void Lstb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 更新输出变量
        /// </summary>
        protected internal virtual void UpdateOutput()
        {

        }

        /// <summary>
        /// 删除输出变量
        /// </summary>
        protected internal virtual void DeleteOutput()
        {

        }

        /// <summary>
        /// 添加输出变量
        /// </summary>
        protected internal virtual void AddOutput()
        {

        }

        /// <summary>
        /// 更新输入变量
        /// </summary>
        protected internal virtual void UpdateInput()
        {

        }

        /// <summary>
        /// 更新输入变量
        /// </summary>
        protected internal virtual void DeleteInput()
        {

        }

        /// <summary>
        /// 添加输入变量
        /// </summary>
        protected internal virtual void AddInput()
        {

        }

        /// <summary>
        /// 下载参数到控制器
        /// </summary>
        protected internal virtual void DownLoadParameter()
        {

        }

        /// <summary>
        /// 从控制器上载参数
        /// </summary>
        protected internal virtual void UpLoadParameter()
        {

        }

        /// <summary>
        /// 更新轴
        /// </summary>
        protected internal virtual void UpdateAxes()
        {

        }

        /// <summary>
        /// 删除轴
        /// </summary>
        protected internal virtual void DeleteAxes()
        {

        }

        /// <summary>
        /// 添加轴
        /// </summary>
        protected internal virtual void AddAxes()
        {

        }

        #endregion

        /// <summary>
        /// 初始化字段
        /// </summary>
        protected internal virtual void InitFieldAndProperty()
        {
            _cameraSelectedIdx =-1;
            _cameraBrandIdx = 0;
            _boardSelectedIdx = -1;
            _serialPortIdx = -1;
            _serialPortSelectedIdx = -1;
            _socketSelectedIdx = -1;
            _webSelectedIdx = -1;           
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        protected internal virtual void UpdateControl()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);
            string str = this.Tag.ToString();
            this.HtmlText = isChs ? ProCommon.Properties.Resources.ResourceManager.GetString("chs_" + str) : ProCommon.Properties.Resources.ResourceManager.GetString("en_" + str);

            this.cmbeCameraBrand.SelectedIndexChanged -= Cmbe_SelectedIndexChanged;
            this.cmbeCameraBrand.SelectedIndexChanged += Cmbe_SelectedIndexChanged;
            this.cmbeSerialPorts.SelectedIndexChanged -= Cmbe_SelectedIndexChanged;
            this.cmbeSerialPorts.SelectedIndexChanged += Cmbe_SelectedIndexChanged;

            this.lstbCameras.SelectedIndexChanged -= Lstb_SelectedIndexChanged;
            this.lstbCameras.SelectedIndexChanged += Lstb_SelectedIndexChanged;

            this.lstbBoard.SelectedIndexChanged -= Lstb_SelectedIndexChanged;
            this.lstbBoard.SelectedIndexChanged += Lstb_SelectedIndexChanged;                   

            this.lstbSerialport.SelectedIndexChanged -= Lstb_SelectedIndexChanged;
            this.lstbSerialport.SelectedIndexChanged += Lstb_SelectedIndexChanged;

            UpdateXtraTabControl();
            UpdateGoupControl();
            UpdateLabelControl();
            UpdateCheckEdit();
            UpdateSimpleButton();
        }      

        private void Cmbe_SelectedIndexChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.ComboBoxEdit cmbe=sender as DevExpress.XtraEditors.ComboBoxEdit;
            if(cmbe!=null
                && cmbe.Tag!=null)
            {
                switch(cmbe.Tag.ToString())
                {
                    case "CMBE_CAMERABRAND":
                        _cameraBrandIdx = this.cmbeCameraBrand.SelectedIndex;
                        break;
                    case "CMBE_SERIALPORT":
                        _serialPortIdx = this.cmbeSerialPorts.SelectedIndex;
                        break;
                    default:break;
                }
            }
            
        }

        /// <summary>
        /// 更新XtraTabControl控件
        /// </summary>
        protected internal virtual void UpdateXtraTabControl()
        {
            for (int i = 0; i < this.xtbcRoot.TabPages.Count; i++)
            {
                UpdateXtraTabPage(this.xtbcRoot.TabPages[i], LanguageVersion,ProCommon.Properties.Resources.ResourceManager);
            }

            for(int j=0;j<this.xtbcBoard.TabPages.Count;j++)
            {
                UpdateXtraTabPage(this.xtbcBoard.TabPages[j], LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            }
        }

        protected internal void UpdateXtraTabPage(DevExpress.XtraTab.XtraTabPage xtbp, ProCommon.Communal.Language lan,System.Resources.ResourceManager resourceManager)
        {
            if (xtbp != null
                && xtbp.Tag != null)
            {
                if(resourceManager!=null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = xtbp.Tag.ToString();
                    xtbp.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                }
            }
        }

        /// <summary>
        /// 更新GroupControl控件
        /// </summary>
        private void UpdateGoupControl()
        {
            UpdateGroupControl(this.grpcCfgSystem, LanguageVersion,ProCommon.Properties.Resources.ResourceManager);
            UpdateGroupControl(this.grpcCfgCmera, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateGroupControl(this.grpcCfgBoard, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateGroupControl(this.grpcCfgSerialPort, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateGroupControl(this.grpcCfgSocket, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateGroupControl(this.grpcCfgWeb, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
        }

        protected internal void UpdateGroupControl(DevExpress.XtraEditors.GroupControl grpc, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (grpc != null
               && grpc.Tag != null)
            {
                if(resourceManager!=null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = grpc.Tag.ToString();
                    grpc.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                }
              
            }
        }

        /// <summary>
        /// 更新LabelControl
        /// </summary>
        private void UpdateLabelControl()
        {
            UpdateLabelControl(this.lblCameraPrompt, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblCameraBrand, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblCameraSerialNumber, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblCameraName, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblCameraFPS, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblCameraExposure, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblCameraGain, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblCameraOpseration, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);

            UpdateLabelControl(this.lblSerialPortPrompt, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblSerialPortnSelection, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblBaudRate, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblDtatBits, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblParity, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblStopBits, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblSerialPortOperation, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
        }
        protected internal void UpdateLabelControl(DevExpress.XtraEditors.LabelControl lblc, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (lblc != null
              && lblc.Tag != null)
            {
                if(resourceManager!=null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = lblc.Tag.ToString();
                    string chs = resourceManager.GetString("chs_" + str);
                    string en = resourceManager.GetString("en_" + str);
                    lblc.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                }
            }
        }

        /// <summary>
        /// 更新CheckEdit控件
        /// </summary>
        private void UpdateCheckEdit()
        {
            UpdateCheckEdit(this.chkeUpdateCameraBrand, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeUpdateCameraSN, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeUpdateCameraName, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeUpdateCameraFPS, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeUpdateCameraExposure, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeUpdateCameraGain, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeCameraAdd, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeCameraModify, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeCameraDelete, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);

            UpdateCheckEdit(this.chkeUpdatePortSelection, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeUpdateBaudRate, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeUpdateDataBits, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeUpdateParity, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeUpdateStopBits, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeSerialPortAdd, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeSerialPortModify, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeSerialPortDelete, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);            

        }

        protected internal void UpdateCheckEdit(DevExpress.XtraEditors.CheckEdit chke, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (chke != null
             && chke.Tag != null)
            {
                if(resourceManager!=null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = chke.Tag.ToString();
                    chke.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                    chke.CheckedChanged += Chke_CheckedChanged;
                }
            }
        }

        protected internal virtual void Chke_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit chke = sender as DevExpress.XtraEditors.CheckEdit;
            if(chke!=null
                && chke.Properties.Tag!=null)
            {
                switch(chke.Properties.Tag.ToString())
                {
                    case "CHKE_CAMERAADD":
                        if(chke.Checked)
                            AddCamera();
                        break;
                    case "CHKE_CAMERAMODIFY":
                        if (chke.Checked)
                            ModifyCamera();
                        break;
                    case "CHKE_CAMERADELETE":
                        if (chke.Checked)
                            DeleteCamera();
                        break;
                    case "CHKE_SERIALPORTADD":
                        if (chke.Checked)
                            AddSerialPort();
                        break;
                    case "CHKE_SERIALPORTMODIFY":
                        if (chke.Checked)
                            ModifySerialPort();
                        break;
                    case "CHKE_SERIALPORTDELETE":
                        if (chke.Checked)
                            DeleteSerialPort();
                        break;
                    case "CHKE_UPDATECAMERABRAND":
                        if(this.chkeCameraModify.Checked)
                            this.cmbeCameraBrand.Enabled = chke.Checked;
                        break;
                    case "CHKE_UPDATECAMERASN":
                        if (this.chkeCameraModify.Checked)
                            this.txteCameraSNNew.Enabled = chke.Checked;
                        break;
                    case "CHKE_UPDATECAMERANAME":
                        if (this.chkeCameraModify.Checked)
                            this.txteCameraNameNew.Enabled = chke.Checked;
                        break;
                    case "CHKE_UPDATECAMERAFPS":
                        if (this.chkeCameraModify.Checked)
                            this.speCameraFPSNew.Enabled = chke.Checked;
                        break;
                    case "CHKE_UPDATECAMERAEXPOSURE":
                        if (this.chkeCameraModify.Checked)
                            this.speCameraExposure.Enabled = chke.Checked;
                        break;
                    case "CHKE_UPDATECAMERAGAIN":
                        if (this.chkeCameraModify.Checked)
                            this.speCameraGain.Enabled = chke.Checked;
                        break;
                    case "CHKE_UPDATESELECTION":
                        if (this.chkeSerialPortModify.Checked)
                            this.cmbeSerialPorts.Enabled = chke.Checked;
                        break;
                    case "CHKE_UPDATEBAUDRATE":
                        if (this.chkeSerialPortModify.Checked)
                            this.cmbeBaudRate.Enabled = chke.Checked;
                        break;
                    case "CHKE_UPDATEDATABITS":
                        if (this.chkeSerialPortModify.Checked)
                            this.cmbeDtatBits.Enabled = chke.Checked;
                        break;
                    case "CHKE_UPDATEPARITY":
                        if (this.chkeSerialPortModify.Checked)
                            this.cmbeParity.Enabled = chke.Checked;
                        break;
                    case "CHKE_UPDATESTOPBITS":
                        if (this.chkeSerialPortModify.Checked)
                            this.cmbeStopBits.Enabled = chke.Checked;
                        break;
                    default:break;
                }
            }
        }



        /// <summary>
        /// 更新SimpleButton控件
        /// </summary>
        private void UpdateSimpleButton()
        {
            UpdateSimpleButton(this.sbtnCameraCalibrate, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnCameraConfirmUpdate, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnEnumerateSerialPort, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnSerialPortConfirmUpdate, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);

            UpdateSimpleButton(this.sbtnAddAxes, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnDeleteAxes, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnUpdateAxes, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnUpLoadParameter, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnDownLoadParameter, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnAddInput, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnDeleteInput, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnUpdateInput, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnAddOutput, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnDeleteOutput, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnUpdateOutput, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);           

            UpdateSimpleButton(this.sbtnConfirm, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnExit, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
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
                if(resourceManager!=null)
                {
                    sbtn.Click -= Sbtn_Click;
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = sbtn.Tag.ToString();
                    sbtn.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                    sbtn.Click += Sbtn_Click;
                }
            }
        }
    }
}