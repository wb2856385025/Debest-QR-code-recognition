using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pro2DBarcode.UI
{
    public partial class UI_SetConfigs : ProCommon.UserDefForm.FrmSetConfigs
    {
        private Pro2DBarcode.Manager.ConfigManager _cfgManager;
        private ProCommon.Communal.Account _currentAccount;
        protected Pro2DBarcode.Manager.ConfigManager CfgManager
        {
            get { return _cfgManager; }
            private set
            {
                if (value != null)
                {
                    _cfgManager = value;
                    configSystemBindingSource.DataSource = _cfgManager.CfgSystem;
                    ShowHideTabpage();
                                       
                    UpdateCameraListBox(this.lstbCameras, _cfgManager.CfgCamera.CameraList);
                    this.lstbCameras.SelectedIndex = _cfgManager.CfgCamera.CameraList.Count - 1;
                    Lstb_SelectedIndexChanged(this.lstbCameras, null);

                    UpdateBoardListBox(this.lstbBoard, _cfgManager.CfgBoardCtrller.BoardCtrllerList);
                    this.lstbBoard.SelectedIndex = _cfgManager.CfgBoardCtrller.BoardCtrllerList.Count - 1;
                    Lstb_SelectedIndexChanged(this.lstbBoard, null);

                    UpdateSerialPortListBox(this.lstbSerialport, _cfgManager.CfgSerialPort.ComSerialPortList);
                    this.lstbSerialport.SelectedIndex = _cfgManager.CfgSerialPort.ComSerialPortList.Count - 1;
                    Lstb_SelectedIndexChanged(this.lstbSerialport, null);

                    UpdateSocketListBox(this.lstbSocket, _cfgManager.CfgSocket.ComSocketList);
                    this.lstbSocket.SelectedIndex = _cfgManager.CfgSocket.ComSocketList.Count - 1;
                    Lstb_SelectedIndexChanged(this.lstbSocket, null);

                    UpdateWebListBox(this.lstbWeb, null);
                }
            }
        }        
        
        public UI_SetConfigs(ProCommon.Communal.Account acc,ProCommon.Communal.Language lan,Pro2DBarcode.Manager.ConfigManager cfgManager):base(lan)
        {
            InitializeComponent();
            _currentAccount = acc;
            _cfgManager = cfgManager;
        }

        #region 覆写基类成员
        protected override void FrmSetConfigs_Load(object sender, EventArgs e)
        {
            InitFieldAndProperty();
            UpdateControl();
        }       

        /// <summary>
        /// 初始化字段
        /// </summary>
        protected override void InitFieldAndProperty()
        {
            base.InitFieldAndProperty();
            CfgManager = _cfgManager;
        }

        /// <summary>
        /// 更新控件
        /// </summary>
        protected override void UpdateControl()
        {
            base.UpdateControl();
            UpdateLabelControl();
            UpdateCheckEdit();
            UpdateButtonEdit();
            UpdateComboBoxEditForCamera();
            UpdateComboBoxEditForSerialPort();
        }

        /// <summary>
        /// 更新系统配置页面串口的选项
        /// </summary>
        private void UpdateComboBoxEditForSerialPort()
        {
            int selectedIdx = -1;
            string name = string.Empty;

            this.cmbeSerialPortNameForBarcode.Properties.Items.Clear();
            if (_cfgManager != null
               && _cfgManager.CfgSerialPort != null
               && _cfgManager.CfgSerialPort.ComSerialPortList.Count > 0)
            {
                for (int i = 0; i < _cfgManager.CfgSerialPort.ComSerialPortList.Count; i++)
                {
                    name = _cfgManager.CfgSerialPort.ComSerialPortList[i].Name;
                    this.cmbeSerialPortNameForBarcode.Properties.Items.Add(name);
                    if (_cfgManager.CfgSystem.SerialPortNameFor2DCode == name)
                        selectedIdx = i;
                }

                //若无匹配项,则选择第一个项
                if (selectedIdx < 0)
                    selectedIdx = 0;
                this.cmbeSerialPortNameForBarcode.SelectedIndex = selectedIdx;
                this.cmbeSerialPortNameForBarcode.EditValue = this.cmbeSerialPortNameForBarcode.SelectedItem;
                _cfgManager.CfgSystem.SerialPortNameFor2DCode = this.cmbeSerialPortNameForBarcode.SelectedItem.ToString();
            }
            else
            {
                this.cmbeSerialPortNameForBarcode.SelectedIndex = selectedIdx;
                this.cmbeSerialPortNameForBarcode.EditValue = null;

                _txt = _isChs ? "未为系统指定使用的串口!" : "No valid serial port selected for system!";
                _caption = _isChs ? "警告信息" : "Warning Message";
                System.Windows.Forms.MessageBox.Show(_txt, _caption,
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 更新系统配置页面相机的选项
        /// </summary>
        private void UpdateComboBoxEditForCamera()
        {
            this.cmbeCameraNameForBarcode.Properties.Items.Clear();
            int selectedIdx = -1;
            string name = string.Empty;

            if (_cfgManager != null
                && _cfgManager.CfgCamera != null
                && _cfgManager.CfgCamera.CameraList.Count > 0)
            {
                for (int i = 0; i < _cfgManager.CfgCamera.CameraList.Count; i++)
                {
                    name = _cfgManager.CfgCamera.CameraList[i].Name;
                    this.cmbeCameraNameForBarcode.Properties.Items.Add(name);
                    if (_cfgManager.CfgSystem.CameraNameFor2DCode == name)
                        selectedIdx = i;
                }

                //若无匹配项,则选择第一个项
                if (selectedIdx < 0)
                    selectedIdx = 0;

                this.cmbeCameraNameForBarcode.SelectedIndex = selectedIdx;
                this.cmbeCameraNameForBarcode.EditValue = this.cmbeCameraNameForBarcode.SelectedItem;
                _cfgManager.CfgSystem.CameraNameFor2DCode = this.cmbeCameraNameForBarcode.SelectedItem.ToString();
            }
            else
            {
                this.cmbeCameraNameForBarcode.SelectedIndex = selectedIdx;
                this.cmbeCameraNameForBarcode.EditValue = null;

                _txt = _isChs ? "未为系统指定使用的相机!" : "No valid camera selected for system!";
                _caption = _isChs ? "警告信息" : "Warning Message";
                System.Windows.Forms.MessageBox.Show(_txt, _caption,
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 更新LabelControl控件
        /// [注:与基类同名,但各自都是私有访问]
        /// </summary>
        private void UpdateLabelControl()
        {
            UpdateLabelControl(this.lblClientName, LanguageVersion,Pro2DBarcode.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblRoutineDirectory, LanguageVersion, Pro2DBarcode.Properties.Resources.ResourceManager);          
            UpdateLabelControl(this.lblAppLanguage, LanguageVersion, Pro2DBarcode.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblCameraNameForBarcode, LanguageVersion, Pro2DBarcode.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblSerialPortNameForBarcode, LanguageVersion, Pro2DBarcode.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblPacketHeader, LanguageVersion, Pro2DBarcode.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblPacketEnd, LanguageVersion, Pro2DBarcode.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblResultSeparator, LanguageVersion, Pro2DBarcode.Properties.Resources.ResourceManager);
        }

        private void UpdateCheckEdit()
        {
            UpdateCheckEdit(this.chkeEnableLastRoutine, LanguageVersion, Pro2DBarcode.Properties.Resources.ResourceManager);
        }

        private void UpdateButtonEdit()
        {
            UpdateButtonEdit(this.btneRoutineDirectory, LanguageVersion, Pro2DBarcode.Properties.Resources.ResourceManager);
        }

        protected internal virtual void UpdateButtonEdit(DevExpress.XtraEditors.ButtonEdit btne,ProCommon.Communal.Language lan,System.Resources.ResourceManager resourceManager)
        {
            if(btne!=null)
            {
                if(resourceManager!=null)
                {
                    bool isChs = lan == ProCommon.Communal.Language.Chinese;
                    string str = string.Empty;
                    for (int i = 0; i < btne.Properties.Buttons.Count; i++)
                    {
                        if (btne.Properties.Buttons[i].Tag != null)
                        {
                            str = btne.Properties.Buttons[i].Tag.ToString();
                            btne.Properties.Buttons[i].Caption = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                        }
                    }
                    if (btne.Properties.Buttons.Count > 0)
                    {
                        btne.ButtonClick -= Btne_ButtonClick; //去掉以前注册的
                        btne.ButtonClick += Btne_ButtonClick; //添加当前注册的
                    }
                }                            
            }
        }

        protected internal virtual void Btne_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           if(e.Button.Tag!=null)
            {
                switch(e.Button.Tag.ToString())
                {
                    case "SELECT":
                        GetRoutineDirectory();
                        break;
                    default:break;
                }
            }
        }

        /// <summary>
        /// 获取程式目录
        /// </summary>
        private void GetRoutineDirectory()
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.RootFolder =Environment.SpecialFolder.DesktopDirectory;
            if(fbd.ShowDialog()==DialogResult.OK)
            {
                this.btneRoutineDirectory.Text = fbd.SelectedPath;
            }           
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <returns></returns>
        protected override bool SaveConfig()
        {
            if(_cfgManager!=null)
            {
                _cfgManager.Save();
                return true;
            }

            return false;
        }

        /// <summary>
        /// 串口删除
        /// </summary>
        protected override void DeleteSerialPort()
        {

        }

        /// <summary>
        /// 串口修改
        /// </summary>
        protected override void ModifySerialPort()
        {
            this.cmbeSerialPorts.Enabled = false;
            this.cmbeBaudRate.Enabled = false;
            this.cmbeDtatBits.Enabled = false;
            this.cmbeParity.Enabled = false;
            this.cmbeStopBits.Enabled = false;
        }

        /// <summary>
        /// 串口新增
        /// </summary>
        protected override void AddSerialPort()
        {
            this.cmbeSerialPorts.Enabled = true;
            this.cmbeBaudRate.Enabled = true;
            this.cmbeDtatBits.Enabled = true;
            this.cmbeParity.Enabled = true;
            this.cmbeStopBits.Enabled = true;
        }

        /// <summary>
        /// 串口确认更新
        /// </summary>
        protected override void ConfirmUpdateSerialPort()
        {
            if (this.chkeSerialPortAdd.Checked)
            {
                if (_cfgManager.CfgSerialPort != null
                   && _cfgManager.CfgSerialPort.ComSerialPortList != null)
                {
                    if (this.cmbeSerialPorts.Properties.Items.Count != 0)
                    {
                        if (_serialPortIdx > -1)
                        {
                            int count = _cfgManager.CfgSerialPort.ComSerialPortList.Count;
                            int serialNum = 0;
                            if (count != 0)
                                serialNum = _cfgManager.CfgSerialPort.ComSerialPortList[count - 1].Number + 1;

                            System.IO.Ports.Parity prty = System.IO.Ports.Parity.None;
                            switch (this.cmbeParity.SelectedIndex)
                            {
                                case 0:
                                    prty = System.IO.Ports.Parity.None;
                                    break;
                                case 1:
                                    prty = System.IO.Ports.Parity.Even;
                                    break;
                                case 2:
                                    prty = System.IO.Ports.Parity.Odd;
                                    break;
                                case 3:
                                    prty = System.IO.Ports.Parity.Mark;
                                    break;
                                case 4:
                                    prty = System.IO.Ports.Parity.Space;
                                    break;
                                default: break;
                            }

                            System.IO.Ports.StopBits stpBits = System.IO.Ports.StopBits.None;
                            switch (this.cmbeStopBits.SelectedIndex)
                            {
                                case 0:
                                    stpBits = System.IO.Ports.StopBits.One;
                                    break;
                                case 1:
                                    stpBits = System.IO.Ports.StopBits.OnePointFive;
                                    break;
                                case 2:
                                    stpBits = System.IO.Ports.StopBits.Two;
                                    break;
                                default: break;
                            }


                            bool isExist = false;
                            for (int i = 0; i < count; i++)
                            {
                                if (_cfgManager.CfgSerialPort.ComSerialPortList[i].Name == this.cmbeSerialPorts.SelectedItem.ToString())
                                    isExist = true;
                            }
                            if (!isExist)
                            {
                                ProCommon.Communal.ComWrappedSerialPort comWrappedSerialPort = new ProCommon.Communal.ComWrappedSerialPort(this.cmbeSerialPorts.SelectedItem.ToString(), serialNum);
                                comWrappedSerialPort.BaudRate = Convert.ToInt32(this.cmbeBaudRate.SelectedItem.ToString());
                                comWrappedSerialPort.DataBits = Convert.ToInt32(this.cmbeDtatBits.SelectedItem.ToString());
                                comWrappedSerialPort.Parity = prty;
                                comWrappedSerialPort.StopBits = stpBits;
                                comWrappedSerialPort.NewLine = "/r/n";
                                comWrappedSerialPort.ReceiveTimeOut = 500;
                                comWrappedSerialPort.SendTimeOut = 500;
                                comWrappedSerialPort.DtrEnable = true;
                                comWrappedSerialPort.RtsEnable = true;

                                _cfgManager.CfgSerialPort.ComSerialPortList.Add(comWrappedSerialPort);
                                UpdateSerialPortListBox(this.lstbSerialport, _cfgManager.CfgSerialPort.ComSerialPortList);
                                this.lstbSerialport.SelectedIndex = count;//增加前的数量
                                UpdateComboBoxEditForSerialPort();
                            }
                            else
                            {
                                _txt = _isChs ? "选择的串口已添加!" : "Serial port selected has been added!";
                                _caption = _isChs ? "警告信息" : "Warning Message";                              
                                ProCommon.UserDefForm.FrmMsgBox.Show(_txt, _caption, 
                                    ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning,_isChs);
                            }
                        }
                        else
                        {
                            _txt = _isChs ? "未选择串口 !" : "Invalid serial port !";
                            _caption = _isChs ? "警告信息" : "Warning Message";
                            ProCommon.UserDefForm.FrmMsgBox.Show(_txt, _caption,
                                    ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, _isChs);
                        }
                    }
                    else
                    {
                        _txt = _isChs ? "未发现串口 !" : "No serial port detected !";
                        _caption = _isChs ? "警告信息" : "Warning Message";
                        ProCommon.UserDefForm.FrmMsgBox.Show(_txt, _caption,
                                     ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, _isChs);
                    }
                }
            }
            else if (this.chkeSerialPortModify.Checked)
            {
                if (_cfgManager.CfgSerialPort != null
                 && _cfgManager.CfgSerialPort.ComSerialPortList != null)
                {
                    if (_serialPortSelectedIdx > -1)
                    {
                        ProCommon.Communal.ComWrappedSerialPort comSerialPort = _cfgManager.CfgSerialPort.ComSerialPortList[_serialPortSelectedIdx];
                        if (this.chkeUpdatePortSelection.Checked)
                        {
                            if (this.cmbeSerialPorts.Properties.Items.Count > 0)
                            {
                                comSerialPort.Name = this.cmbeSerialPorts.SelectedItem.ToString();
                                this.txteSerialPortSelection.Text = comSerialPort.Name;
                            }
                            else
                            {
                                _txt = _isChs ? "未发现串口 !" : "No serial port detected !";
                                _caption = _isChs ? "警告信息" : "Warning Message";
                                ProCommon.UserDefForm.FrmMsgBox.Show(_txt, _caption,
                                    ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, _isChs);
                            }
                        }

                        if (this.chkeUpdateBaudRate.Checked)
                        {
                            comSerialPort.BaudRate = Convert.ToInt32(this.cmbeBaudRate.SelectedItem.ToString());
                            this.txteBaudRate.Text = comSerialPort.BaudRate.ToString();
                        }

                        if (this.chkeUpdateDataBits.Checked)
                        {
                            comSerialPort.DataBits = Convert.ToInt32(this.cmbeDtatBits.SelectedItem.ToString());
                            this.txteDataBits.Text = comSerialPort.DataBits.ToString();
                        }

                        if (this.chkeUpdateParity.Checked)
                        {
                            System.IO.Ports.Parity prty = System.IO.Ports.Parity.None;
                            switch (this.cmbeParity.SelectedIndex)
                            {
                                case 0:
                                    prty = System.IO.Ports.Parity.None;
                                    break;
                                case 1:
                                    prty = System.IO.Ports.Parity.Even;
                                    break;
                                case 2:
                                    prty = System.IO.Ports.Parity.Odd;
                                    break;
                                case 3:
                                    prty = System.IO.Ports.Parity.Mark;
                                    break;
                                case 4:
                                    prty = System.IO.Ports.Parity.Space;
                                    break;
                                default: break;
                            }
                            comSerialPort.Parity = prty;
                            this.txteParity.Text = comSerialPort.Parity.ToString();
                        }

                        if (this.chkeUpdateStopBits.Checked)
                        {
                            System.IO.Ports.StopBits stpBits = System.IO.Ports.StopBits.None;
                            switch (this.cmbeStopBits.SelectedIndex)
                            {
                                case 0:
                                    stpBits = System.IO.Ports.StopBits.One;
                                    break;
                                case 1:
                                    stpBits = System.IO.Ports.StopBits.OnePointFive;
                                    break;
                                case 2:
                                    stpBits = System.IO.Ports.StopBits.Two;
                                    break;
                                default: break;
                            }
                            comSerialPort.StopBits = stpBits;
                            this.txteStopBits.Text = comSerialPort.StopBits.ToString();
                        }

                        UpdateSerialPortListBox(this.lstbSerialport, _cfgManager.CfgSerialPort.ComSerialPortList);
                        UpdateComboBoxEditForSerialPort();
                    }
                }
            }
            else if (this.chkeSerialPortDelete.Checked)
            {
                if (_cfgManager.CfgSerialPort != null
                  && _cfgManager.CfgSerialPort.ComSerialPortList != null
                  && _cfgManager.CfgSerialPort.ComSerialPortList.Count>0)
                {
                    if (_serialPortSelectedIdx > -1)
                    {
                        ProCommon.Communal.ComWrappedSerialPort comSerialPort = _cfgManager.CfgSerialPort.ComSerialPortList[_serialPortSelectedIdx];
                        _cfgManager.CfgSerialPort.ComSerialPortList.Delete(comSerialPort);
                        UpdateSerialPortListBox(this.lstbSerialport, _cfgManager.CfgSerialPort.ComSerialPortList);
                        if (_cfgManager.CfgSerialPort.ComSerialPortList.Count > 0)
                            this.lstbSerialport.SelectedIndex = _cfgManager.CfgSerialPort.ComSerialPortList.Count - 1;
                        UpdateComboBoxEditForSerialPort();
                    }
                }
            }
        }

        /// <summary>
        /// 相机确认更新
        /// </summary>
        protected override void ConfirmUpdateCamera()
        {
            if (this.chkeCameraAdd.Checked)
            {
                if (_cfgManager.CfgCamera != null
                    && _cfgManager.CfgCamera.CameraList != null)
                {
                    ProCommon.Communal.CtrllerBrand brand = ProCommon.Communal.CtrllerBrand.HikVision;
                    if (_cameraBrandIdx > -1)
                    {
                        switch (_cameraBrandIdx)
                        {
                            case 0:
                                brand = ProCommon.Communal.CtrllerBrand.HikVision;
                                break;
                            case 1:
                                brand = ProCommon.Communal.CtrllerBrand.Baumer;
                                break;
                            case 2:
                                brand = ProCommon.Communal.CtrllerBrand.MindVision;
                                break;
                            case 3:
                                brand = ProCommon.Communal.CtrllerBrand.Basler;
                                break;
                            case 4:
                                brand = ProCommon.Communal.CtrllerBrand.DaHua;
                                break;
                            default: break;
                        }

                        if (!string.IsNullOrEmpty(this.txteCameraSNNew.Text))
                        {
                            if (!string.IsNullOrEmpty(this.txteCameraNameNew.Text))
                            {
                                if (this.speCameraFPSNew.Value > 0)
                                {
                                    if (this.speCameraExposure.Value > 0)
                                    {
                                        if (this.speCameraGain.Value > 0)
                                        {
                                            //1-获取创建相机唯一标时ID时的索引号(以相机列表中最后一个相机对象的序号为基础,累计加一)
                                            //2-创建相机,配置相机其他参数
                                            int count = _cfgManager.CfgCamera.CameraList.Count;
                                            int camNumber = 0;
                                            if (count != 0)
                                                camNumber = _cfgManager.CfgCamera.CameraList[count - 1].Number + 1;

                                            ProCommon.Communal.Camera cam = new ProCommon.Communal.Camera(brand, ProCommon.Communal.CtrllerType.Camera_AreaScan, camNumber, this.txteCameraNameNew.Text);
                                            cam.SerialNo = this.txteCameraSNNew.Text;
                                            cam.FPS = (float)this.speCameraFPSNew.Value;
                                            cam.ExposureTime = (float)this.speCameraExposure.Value;
                                            cam.Gain = (float)this.speCameraGain.Value;
                                            _cfgManager.CfgCamera.CameraList.Add(cam);

                                            UpdateCameraListBox(this.lstbCameras, _cfgManager.CfgCamera.CameraList);
                                            this.lstbCameras.SelectedIndex = count;//增加前的数量
                                            UpdateComboBoxEditForCamera();
                                        }
                                        else
                                        {
                                            _txt = _isChs ? "相机增益为负!" : "Invalid camera gain !";
                                            _caption = _isChs ? "警告信息" : "Warning Message";
                                            ProCommon.UserDefForm.FrmMsgBox.Show(_txt, _caption,
                                            ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, _isChs);
                                        }
                                    }
                                    else
                                    {
                                        _txt = _isChs ? "相机曝光为负!" : "Invalid camera exposure time !";
                                        _caption = _isChs ? "警告信息" : "Warning Message";
                                        ProCommon.UserDefForm.FrmMsgBox.Show(_txt, _caption,
                                        ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, _isChs);
                                    }
                                }
                                else
                                {
                                    _txt = _isChs ? "相机帧率为负!" : "Invalid camera FPS !";
                                    _caption = _isChs ? "警告信息" : "Warning Message";
                                    System.Windows.Forms.MessageBox.Show(_txt, _caption,
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                _txt = _isChs ? "相机名称为空 !" : "Invalid camera name !";
                                _caption = _isChs ? "警告信息" : "Warning Message";
                                ProCommon.UserDefForm.FrmMsgBox.Show(_txt, _caption,
                                    ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, _isChs);
                            }
                        }
                        else
                        {
                            _txt = _isChs ? "相机序列号为空 !" : "Invalid camera serial number !";
                            _caption = _isChs ? "警告信息" : "Warning Message";
                            ProCommon.UserDefForm.FrmMsgBox.Show(_txt, _caption,
                                    ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, _isChs);
                        }
                    }
                    else
                    {
                        _txt = _isChs ? "未选择相机品牌 !" : "Invalid camera brand !";
                        _caption = _isChs ? "警告信息" : "Warning Message";
                        ProCommon.UserDefForm.FrmMsgBox.Show(_txt, _caption,
                                    ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, _isChs);
                    }
                }
            }
            else if (this.chkeCameraModify.Checked)
            {
                if (_cfgManager.CfgCamera != null
                  && _cfgManager.CfgCamera.CameraList != null)
                {
                    if (_cameraSelectedIdx > -1)
                    {
                        ProCommon.Communal.Camera cam = _cfgManager.CfgCamera.CameraList[_cameraSelectedIdx];
                        if (this.chkeUpdateCameraBrand.Checked)
                        {
                            ProCommon.Communal.CtrllerBrand brand = ProCommon.Communal.CtrllerBrand.HikVision;
                            switch (_cameraBrandIdx)
                            {
                                case 0:
                                    brand = ProCommon.Communal.CtrllerBrand.HikVision;
                                    break;
                                case 1:
                                    brand = ProCommon.Communal.CtrllerBrand.Baumer;
                                    break;
                                case 2:
                                    brand = ProCommon.Communal.CtrllerBrand.MindVision;
                                    break;
                                case 3:
                                    brand = ProCommon.Communal.CtrllerBrand.Basler;
                                    break;
                                case 4:
                                    brand = ProCommon.Communal.CtrllerBrand.DaHua;
                                    break;
                                default: break;
                            }

                            cam.CtrllerBrand = brand;
                            this.txtCameraBrandCurrent.Text = cam.CtrllerBrand.ToString();
                        }

                        if (this.chkeUpdateCameraSN.Checked)
                        {
                            if (!string.IsNullOrEmpty(this.txteCameraSNNew.Text))
                            {
                                cam.SerialNo = this.txteCameraSNNew.Text;
                                this.txteCameraSNCurrent.Text = cam.SerialNo;
                            }
                            else
                            {
                                _txt = _isChs ? "相机序列号为空 !" : "Invalid camera serial number !";
                                _caption = _isChs ? "警告信息" : "Warning Message";
                                ProCommon.UserDefForm.FrmMsgBox.Show(_txt, _caption,
                                   ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, _isChs);
                            }
                        }

                        if (this.chkeUpdateCameraName.Checked)
                        {
                            if (!string.IsNullOrEmpty(this.txteCameraNameNew.Text))
                            {
                                cam.Name = this.txteCameraNameNew.Text;
                                this.txteCameraNameCurrent.Text = cam.Name;
                            }
                            else
                            {
                                _txt = _isChs ? "相机名称为空 !" : "Invalid camera name !";
                                _caption = _isChs ? "警告信息" : "Warning Message";
                                ProCommon.UserDefForm.FrmMsgBox.Show(_txt, _caption,
                                   ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, _isChs);
                            }
                        }

                        if (this.chkeUpdateCameraFPS.Checked)
                        {
                            if (this.speCameraFPSNew.Value > 0)
                            {
                                cam.FPS = (float)this.speCameraFPSNew.Value;
                                this.txteCameraFPSCurrent.Text = cam.FPS.ToString();
                            }
                            else
                            {
                                _txt = _isChs ? "相机帧率为负!" : "Invalid camera FPS !";
                                _caption = _isChs ? "警告信息" : "Warning Message";
                                ProCommon.UserDefForm.FrmMsgBox.Show(_txt, _caption,
                                    ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, _isChs);
                            }
                        }

                        if (this.chkeUpdateCameraExposure.Checked)
                        {
                            if (this.speCameraExposure.Value > 0)
                            {
                                cam.ExposureTime = (float)this.speCameraExposure.Value;
                                this.txteCameraExposure.Text = cam.ExposureTime.ToString();
                            }
                            else
                            {
                                _txt = _isChs ? "相机曝光为负!" : "Invalid camera exposure time !";
                                _caption = _isChs ? "警告信息" : "Warning Message";
                                ProCommon.UserDefForm.FrmMsgBox.Show(_txt, _caption,
                                    ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, _isChs);
                            }
                        }

                        if (this.chkeUpdateCameraGain.Checked)
                        {
                            if (this.speCameraGain.Value > 0)
                            {
                                cam.Gain = (float)this.speCameraGain.Value;
                                this.txteCameraGain.Text = cam.Gain.ToString();
                            }
                            else
                            {
                                _txt = _isChs ? "相机增益为负!" : "Invalid camera gain !";
                                _caption = _isChs ? "警告信息" : "Warning Message";
                                ProCommon.UserDefForm.FrmMsgBox.Show(_txt, _caption,
                                    ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, _isChs);
                            }
                        }

                        UpdateCameraListBox(this.lstbCameras, _cfgManager.CfgCamera.CameraList);
                        UpdateComboBoxEditForCamera();
                    }
                }
            }
            else if (this.chkeCameraDelete.Checked)
            {
                if (_cfgManager.CfgCamera != null
                 && _cfgManager.CfgCamera.CameraList != null
                 && _cfgManager.CfgCamera.CameraList.Count>0)
                {
                    if (_cameraSelectedIdx > -1)
                    {
                        ProCommon.Communal.Camera cam = _cfgManager.CfgCamera.CameraList[_cameraSelectedIdx];
                        _cfgManager.CfgCamera.CameraList.Delete(cam);
                        UpdateCameraListBox(this.lstbCameras, _cfgManager.CfgCamera.CameraList);
                        if (_cfgManager.CfgCamera.CameraList.Count > 0)
                            this.lstbCameras.SelectedIndex = _cfgManager.CfgCamera.CameraList.Count - 1;
                        UpdateComboBoxEditForCamera();
                    }
                }
            }
        }

        /// <summary>
        /// 相机删除
        /// </summary>
        protected override void DeleteCamera()
        {
           
        }

        /// <summary>
        /// 相机修改
        /// </summary>
        protected override void ModifyCamera()
        {
            this.cmbeCameraBrand.Enabled = false;
            this.txteCameraSNNew.Enabled = false;
            this.txteCameraNameNew.Enabled = false;
            this.speCameraFPSNew.Enabled = false;
            this.speCameraExposure.Enabled = false;
            this.speCameraGain.Enabled = false;
        }

        /// <summary>
        /// 相机新增
        /// </summary>
        protected override void AddCamera()
        {
            this.cmbeCameraBrand.Enabled = true;
            this.txteCameraSNNew.Enabled = true;
            this.txteCameraNameNew.Enabled = true;
            this.speCameraFPSNew.Enabled = true;
            this.speCameraExposure.Enabled = true;
            this.speCameraGain.Enabled = true;
        }

        protected override void Lstb_SelectedIndexChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.ListBoxControl lstb = sender as DevExpress.XtraEditors.ListBoxControl;
            if(lstb!=null
                && lstb.Tag!=null)
            {
                switch(lstb.Tag.ToString())
                {
                    case "LSTB_CAMERA":
                        if (_cfgManager != null
                            && _cfgManager.CfgCamera != null
                            && _cfgManager.CfgCamera.CameraList != null
                            && _cfgManager.CfgCamera.CameraList.Count > 0)
                        {
                           //注意:ListBoxControl清空对象时会触发SelectedIndexChanged事件
                            _cameraSelectedIdx = lstb.SelectedIndex;
                            if (_cameraSelectedIdx > -1)
                                this.cameraBindingSource.DataSource = _cfgManager.CfgCamera.CameraList[_cameraSelectedIdx];
                        }                           
                        break;
                    case "LSTB_BOARD":                        
                        break;
                    case "LSTB_SERIALPORT":
                        if (_cfgManager != null
                           && _cfgManager.CfgSerialPort != null
                           && _cfgManager.CfgSerialPort.ComSerialPortList != null
                           && _cfgManager.CfgSerialPort.ComSerialPortList.Count > 0)
                        {
                            //注意:ListBoxControl清空对象时会触发SelectedIndexChanged事件
                            _serialPortSelectedIdx = lstb.SelectedIndex;
                            if(_serialPortSelectedIdx>-1)
                                this.comWrappedSerialPortBindingSource.DataSource = _cfgManager.CfgSerialPort.ComSerialPortList[_serialPortSelectedIdx];
                        }                            
                        break;
                    case "LSTB_SOCKET":
                        break;
                    case "LSTB_WEB":
                        break;
                    default:break;
                }
            }
        }

        #endregion

        /// <summary>
        /// 显隐选项页控件
        /// </summary>
        private void ShowHideTabpage()
        {
            if (_cfgManager != null
                && _cfgManager.CfgSystem != null)
            {
                for (int i = 0; i < this.xtbcRoot.TabPages.Count; i++)
                {
                    if (this.xtbcRoot.TabPages[i].Tag != null)
                    {
                        bool isShow = false;
                        switch (this.xtbcRoot.TabPages[i].Tag.ToString())
                        {
                            case "XTBP_CFGSYSTEM":
                                this.xtbcRoot.TabPages[i].PageVisible = true;
                                break;
                            case "XTBP_CFGCAMERA":
                                {
                                    if(_currentAccount!=null
                                        && _currentAccount.Authority==ProCommon.Communal.AccountAuthority.Administrator)
                                    {
                                        foreach (ProCommon.Communal.CtrllerCategory cc in _cfgManager.CfgSystem.CtrllerCategoryArray)
                                        {
                                            if (cc == ProCommon.Communal.CtrllerCategory.Camera)
                                            { isShow = true; break; }
                                        }
                                    }                                   
                                    this.xtbcRoot.TabPages[i].PageVisible = isShow;
                                }
                                break;
                            case "XTBP_CFGBOARD":
                                {
                                    if (_currentAccount != null
                                          && _currentAccount.Authority == ProCommon.Communal.AccountAuthority.Administrator)
                                    {
                                        foreach (ProCommon.Communal.CtrllerCategory cc in _cfgManager.CfgSystem.CtrllerCategoryArray)
                                        {
                                            if (cc == ProCommon.Communal.CtrllerCategory.Board)
                                            { isShow = true; break; }
                                        }
                                    }
                                                                       
                                    this.xtbcRoot.TabPages[i].PageVisible = isShow;
                                }
                                break;
                            case "XTBP_CFGSERIALPORT":
                                {
                                    if (_currentAccount != null
                                         && _currentAccount.Authority == ProCommon.Communal.AccountAuthority.Administrator)
                                    {
                                        foreach (ProCommon.Communal.CtrllerCategory cc in _cfgManager.CfgSystem.CtrllerCategoryArray)
                                        {
                                            if (cc == ProCommon.Communal.CtrllerCategory.SerialPort)
                                            { isShow = true; break; }
                                        }
                                    }
                                   
                                    this.xtbcRoot.TabPages[i].PageVisible = isShow;
                                }
                                break;
                            case "XTBP_CFGSOCKET":
                                {
                                    if (_currentAccount != null
                                        && _currentAccount.Authority == ProCommon.Communal.AccountAuthority.Administrator)
                                    {
                                        foreach (ProCommon.Communal.CtrllerCategory cc in _cfgManager.CfgSystem.CtrllerCategoryArray)
                                        {
                                            if (cc == ProCommon.Communal.CtrllerCategory.Socket)
                                            { isShow = true; break; }
                                        }
                                    }
                                    
                                    this.xtbcRoot.TabPages[i].PageVisible = isShow;
                                }
                                break;
                            case "XTBP_CFGWEB":
                                {
                                    if (_currentAccount != null
                                        && _currentAccount.Authority == ProCommon.Communal.AccountAuthority.Administrator)
                                    {
                                        foreach (ProCommon.Communal.CtrllerCategory cc in _cfgManager.CfgSystem.CtrllerCategoryArray)
                                        {
                                            if (cc == ProCommon.Communal.CtrllerCategory.Web)
                                            { isShow = true; break; }
                                        }
                                    }                                   
                                    this.xtbcRoot.TabPages[i].PageVisible = isShow;
                                }
                                break;
                            default: break;
                        }
                    }
                }


            }

        }

        protected internal void UpdateCameraListBox(DevExpress.XtraEditors.ListBoxControl lstb,ProCommon.Communal.CameraList camList)
        {
            if(lstb!=null)
            {
                lstb.Items.Clear();
                if (camList!=null
                    && camList.Count>0)
                {
                    int num = camList.Count;
                    for (int i=0;i<num;i++)
                        lstb.Items.Add(camList[i].Name);
                }
            }
        }
        protected internal void UpdateBoardListBox(DevExpress.XtraEditors.ListBoxControl lstb, ProCommon.Communal.BoardCtrllerList boardList)
        {
            if (lstb != null)
            {
                lstb.Items.Clear();
                if (boardList != null
                    && boardList.Count > 0)
                {                  
                    int num = boardList.Count;
                    for (int i = 0; i < num; i++)
                        lstb.Items.Add(boardList[i].Name);
                }
            }
        }
        protected internal void UpdateSerialPortListBox(DevExpress.XtraEditors.ListBoxControl lstb, ProCommon.Communal.ComWrappedSerialPortList serialPortList)
        {
            if (lstb != null)
            {
                lstb.Items.Clear();
                if (serialPortList != null
                    && serialPortList.Count > 0)
                {                   
                    int num = serialPortList.Count;
                    for (int i = 0; i < num; i++)
                        lstb.Items.Add(serialPortList[i].Name);
                }
            }
        }
        protected internal void UpdateSocketListBox(DevExpress.XtraEditors.ListBoxControl lstb, ProCommon.Communal.ComWrappedSocketList socketList)
        {
            if (lstb != null)
            {
                lstb.Items.Clear();
                if (socketList != null
                    && socketList.Count > 0)
                {
                    int num = socketList.Count;
                    for (int i = 0; i < num; i++)
                        lstb.Items.Add(socketList[i].Name);
                }
            }
        }
        protected internal void UpdateWebListBox(DevExpress.XtraEditors.ListBoxControl lstb, ProCommon.Communal.ComWrappedWebList webList)
        {
            if (lstb != null)
            {
                lstb.Items.Clear();
                if (webList != null
                    && webList.Count > 0)
                {                   
                    int num = webList.Count;
                    for (int i = 0; i < num; i++)
                        lstb.Items.Add(webList[i].Name);
                }
            }
        }

       
    }
}
