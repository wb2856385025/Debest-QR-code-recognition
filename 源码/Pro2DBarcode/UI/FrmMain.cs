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
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
    {
        #region 静态单例
        static object _syncObj = new object();
        static FrmMain _instance;
        public static FrmMain Instance
        {
            get
            {
                lock (_syncObj)
                {
                    if (_instance == null)
                    { _instance = new FrmMain(); }
                }

                return _instance;
            }
            set { _instance = value; }
        }

        private FrmMain()
        {
            InitializeComponent();
            this.Load += FrmMain_Load;           
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Init();
            try
            {              
                this.bstiCurrentTime.ItemAppearance.Normal.BackColor = Color.YellowGreen;
                _timer.Start();
            }
            catch (ProCommon.Communal.InitException initEx)
            {
                Manager.SystemManager.Instance.IsRunAllowed = false;

                System.Windows.Forms.MessageBox.Show("系统设备初始化异常!\r\n"
                   + initEx.Message, "错误信息",
                   System.Windows.Forms.MessageBoxButtons.OK,
                   System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (ProCommon.Communal.StartException startEx)
            {
                Manager.SystemManager.Instance.IsRunAllowed = false;

                System.Windows.Forms.MessageBox.Show("系统设备启动异常!\r\n"
                   + startEx.Message, "错误信息",
                   System.Windows.Forms.MessageBoxButtons.OK,
                   System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (System.Exception ex)
            {
                Manager.SystemManager.Instance.IsRunAllowed = false;

                System.Windows.Forms.MessageBox.Show("系统运行异常!\r\n"
                    + ex.Message, "错误信息",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        #endregion

        /// <summary>
        /// 软件语言版本s
        /// </summary>
        public ProCommon.Communal.Language LanguageVersion { set; get; }

        public int ViewNumber { set; get; }

        /// <summary>
        /// 系统软件名称
        /// </summary>
        public string AppName
        {
            get
            {
                bool isChs = LanguageVersion == ProCommon.Communal.Language.Chinese;
                return isChs ? Properties.Resources.ResourceManager.GetString("chs_APPNAME") : Properties.Resources.ResourceManager.GetString("en_APPNAME");
            }
        }

        /// <summary>
        /// 开发商信息
        /// </summary>
        public string VendorInfo
        {
            get
            {
                bool isChs = LanguageVersion == ProCommon.Communal.Language.Chinese;
                return isChs ? Properties.Resources.ResourceManager.GetString("chs_VENDORINFO") : Properties.Resources.ResourceManager.GetString("en_VENDORINFO");
            }
        }

        /// <summary>
        /// 当前登录账户
        /// </summary>
        public ProCommon.Communal.Account CurrentAccount { set; get; }

        /// <summary>
        /// 金属识别相机
        /// [注:标识多个相机时,建议通过其实现功能来命名]
        /// </summary>
        public ProCommon.Communal.Camera CameraFor2DBarcode;
        public Pro2DBarcode.Device.Device_Camera DevCam;

        protected internal System.Windows.Forms.Timer _timer; //定时器
        protected internal ProCommon.Communal.AutoLaunch _autoLaunch; //自启动
        private Pro2DBarcode.Config.VisionPara.VisionPara2DBarcode _para2DBarcode;
        private string _warningText, _warningCaption;
        private string _routineName, _routineDirectory;
        public UI.FrmSetRoutinePara FrmSetRoutinePara;

        internal protected void ShowInfo()
        {
            bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);
            HtmlText = isChs ? ("[" + Properties.Resources.chs_APPNAME + "]--" + Properties.Resources.chs_VENDORINFO) : ("[" + Properties.Resources.en_APPNAME + "]--" + Properties.Resources.en_VENDORINFO);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            InitFieldAndProperty();
            InitSkinGallery();
            InitControl();
            ShowInfo();
            UpdateControlWhenAccountChanged(CurrentAccount);
            LoadInitRoutine(Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.EnableLastRoutine,
              Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.LastRoutinePath);
        }

      
        /// <summary>
        /// 初始化字段和属性
        /// </summary>
        private void InitFieldAndProperty()
        {
            string accName = (LanguageVersion == ProCommon.Communal.Language.Chinese) ? "匿名" : "Annoymous";

            CurrentAccount = new ProCommon.Communal.Account(accName, 0);
            CurrentAccount.Authority = ProCommon.Communal.AccountAuthority.Junior;
            CurrentAccount.PassWord = string.Empty;

            CameraFor2DBarcode = Manager.SystemManager.Instance.CameraFor2DCode;

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 500;
            _timer.Tick += Timer_Tick;
            _timer.Enabled = true;

            _autoLaunch = new ProCommon.Communal.AutoLaunch();
            _autoLaunch.QuickName = Properties.Resources.ResourceManager.GetString("chs_APPNAME");
            _autoLaunch.SetAutoStart(Manager.ConfigManager.Instance.CfgSystem.EnableAutoLaunch);
            _warningText = (LanguageVersion == ProCommon.Communal.Language.Chinese) ? 
                "运行状态！请停止运行后访问" : "Running!Acess to it after stop";
            _warningCaption = (LanguageVersion == ProCommon.Communal.Language.Chinese) ?
                "警告信息" : "Warning Message";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateBarStaticItem(this.bstiCurrentTime, LanguageVersion, DateTime.Now.ToString());
        }

        /// <summary>
        /// 初始化皮肤
        /// </summary>
        private void InitSkinGallery()
        {
            
        }      

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Manager.UIManager.Instance.SwitchMainAndSubWindow = new Manager.SwitchWindowDel(SwitchSingleAndMultiWindow);
            UpdateControl();
        }

        private void SwitchSingleAndMultiWindow(int wndIdx)
        {
           
        }

        /// <summary>
        /// 更新控件
        /// </summary>
        private void UpdateControl()
        {
            ShowInfo();
            UpdateMenuBarItem(LanguageVersion);
            UpdateResultIetm(LanguageVersion);
            UpdateStatusBarItem(LanguageVersion);
        }

        /// <summary>
        /// 更新菜单栏控件
        /// </summary>
        /// <param name="lan"></param>
        private void UpdateMenuBarItem(ProCommon.Communal.Language lan)
        {
            UpdateBarSubItem(this.bsbiRoutine, lan);
            UpdateBarButtonItem(this.bbiRoutineNew, lan);
            UpdateBarButtonItem(this.bbiRoutineLoad, lan);
            UpdateBarButtonItem(this.bbiRoutineManager, lan);           

            UpdateBarSubItem(this.bsbiSet, lan);
            UpdateBarButtonItem(this.bbiSystemSet, lan);
            UpdateBarButtonItem(this.bbiRoutineSet, lan);

            UpdateBarSubItem(this.bsbiAccount, lan);
            UpdateBarButtonItem(this.bbiLogIn, lan);
            UpdateBarButtonItem(this.bbiAccountManager, lan);
            UpdateBarButtonItem(this.bbiLogOut, lan);

            UpdateBarSubItem(this.bsbiHelp, lan);
            UpdateBarButtonItem(this.bbiVendorInfo, lan);
            UpdateBarButtonItem(this.bbiAppInfo, lan);

            UpdateBarButtonItem(this.bbiRunOnce, lan);
            UpdateBarButtonItem(this.bbiRunContinue, lan);
            UpdateBarButtonItem(this.bbiRunStop, lan);
            UpdateBarButtonItem(this.bbiProductStatistic, lan);
            UpdateBarButtonItem(this.bbiExit, lan);
           
            UpdateBarEditItem(this.beiCurrentRoutine, lan);
           
        }

        /// <summary>
        /// 更新结果栏控件
        /// [注:只是更新提示,非结果]
        /// </summary>
        /// <param name="lan"></param>
        private void UpdateResultIetm(ProCommon.Communal.Language lan)
        {
            UpdateLabelControl(this.lblProOK, lan);
            UpdateLabelControl(this.lblProNG, lan);
            UpdateLabelControl(this.lblProTotal, lan);
            UpdateLabelControl(this.lblProYieldRatio, lan);
            UpdateLabelControl(this.lblRunState, lan);
            UpdateLabelControl(this.lblElapseTime, lan);
            UpdateSimpleButton(this.sbtnCountClear, lan);
            UpdateGridControlForResult(lan);
        }

        /// <summary>
        /// 更新状态栏控件
        /// </summary>
        /// <param name="lan"></param>
        private void UpdateStatusBarItem(ProCommon.Communal.Language lan)
        {
            UpdateBarStaticItem(this.bstiClientName, lan, Manager.ConfigManager.Instance.CfgSystem.ClientName);
            UpdateBarStaticItem(this.bstiCurrentAccount, lan, CurrentAccount.Name);
            UpdateBarStaticItem(this.bstiCurrentTime, lan, null);

            #region 相机状态组
            UpdateBarSubItem(this.bsbiCameraState, lan, ProCommon.Communal.CtrllerCategory.Camera, Manager.ConfigManager.Instance);
            #endregion

            #region 板卡状态组          
            UpdateBarSubItem(this.bsbiBoardState, lan, ProCommon.Communal.CtrllerCategory.Board, Manager.ConfigManager.Instance);
            #endregion

            #region 串口状态组
            UpdateBarSubItem(this.bsbiSerialPortState, lan, ProCommon.Communal.CtrllerCategory.SerialPort, Manager.ConfigManager.Instance);
            #endregion
        }


        /// <summary>
        /// 更新BarButtonItem控件
        /// </summary>
        /// <param name="bbi"></param>
        /// <param name="lan"></param>
        private void UpdateBarButtonItem(DevExpress.XtraBars.BarButtonItem bbi, ProCommon.Communal.Language lan)
        {
            if (bbi != null
              && bbi.Tag != null)
            {
                bbi.ItemClick -= Bbi_ItemClick;
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = bbi.Tag.ToString();
                bbi.Caption = isChs ? Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str);
                bbi.ItemClick += Bbi_ItemClick;
            }
        }

        /// <summary>
        /// BarButton单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bbi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool isChs = LanguageVersion == ProCommon.Communal.Language.Chinese;
            string txt1,txt2, caption;
            switch (e.Item.Tag.ToString())
            {
                #region 新建程式
                case "BBI_NEWROUTINE":
                    {
                        if (!Manager.SystemManager.Instance.IsRunning)
                        {
                            if (CurrentAccount != null
                                && CurrentAccount.Authority == ProCommon.Communal.AccountAuthority.Administrator)
                            {
                                UI.FrmNewRoutine frmNewRoutine = new FrmNewRoutine(LanguageVersion);
                                frmNewRoutine.ShowDialog();
                                if (frmNewRoutine.IsConfirmed)
                                {
                                    _routineName = frmNewRoutine.NewRoutineName;
                                    _routineDirectory = Manager.ConfigManager.Instance.CfgSystem.RoutineDirectory +"\\"+ _routineName;
                                    if (System.IO.Directory.Exists(_routineDirectory))
                                    {
                                        txt1 = isChs ? "系统已经存在相同名称项目\r\n是否覆盖[" : "Exist the same project!Would you recover [";
                                        txt2 = isChs ? "]?" : "]?";
                                        caption = isChs ? "询问信息" : "Question Message";

                                        if(ProCommon.UserDefForm.FrmMsgBox.Show(txt1 + _routineName + txt2,caption,
                                            ProCommon.UserDefForm.MyButtons.OKCancel,ProCommon.UserDefForm.MyIcon.Question,isChs)==DialogResult.OK)
                                        {
                                            System.IO.Directory.Delete(_routineDirectory, true);
                                        }
                                        else { return; }                                      
                                    }

                                    _para2DBarcode = null;
                                    try
                                    {
                                        System.IO.Directory.CreateDirectory(_routineDirectory); //程式名称命名的文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_All"); //程式名称命名文件夹保存全部图像文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_OK");//程式名称命名文件夹下保存OK图像的文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_NG");//程式名称命名文件夹下保存NG图像的文件夹
                                        this.beiCurrentRoutine.EditValue =_routineName;

                                        //创建一个视觉参数类实例
                                        if (_para2DBarcode == null)
                                            _para2DBarcode = new Config.VisionPara.VisionPara2DBarcode();

                                        _para2DBarcode.IsSaveImageAll = false;
                                        _para2DBarcode.IsSaveImageNG = false;
                                        _para2DBarcode.IsSaveImageOK = false;
                                        _para2DBarcode.PathForSaveImageAll = _routineDirectory + "\\Image_All";
                                        _para2DBarcode.PathForSaveImageNG = _routineDirectory + "\\Image_NG";
                                        _para2DBarcode.PathForSaveImageOK = _routineDirectory + "\\Image_OK";
                                        _para2DBarcode.PathForInspectArea = null;
                                        _para2DBarcode.CameraExposure = 99000.0f;
                                        _para2DBarcode.CameraGain = 1.5f;
                                        _para2DBarcode.CodeType = "Data Matrix ECC 200";
                                        _para2DBarcode.TimeOut = 500;
                                        _para2DBarcode.ToBeFoundNumber = 1;
                                        _para2DBarcode.GenParameterNamesForCreate = new string[] { "default_parameters" };
                                        _para2DBarcode.GenParameterValuesForCreate = new string[] { "maximum_recognition" };
                                        _para2DBarcode.ColorStrForResultNotFound ="red";
                                        _para2DBarcode.ColorStrForResultFound = "green"; 

                                        Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode = _para2DBarcode;
                                    }
                                    catch (Exception ex)
                                    {
                                        _para2DBarcode = null;
                                        txt1 = isChs ? "创建程式[" : "Create routine[";
                                        txt2 = isChs ? "]失败!\r\n异常描述:\r\n" : "]failed!\r\n Error description:\r\n";
                                        caption = isChs ? "错误信息" : "Error Message";                                      
                                        ProCommon.UserDefForm.FrmMsgBox.Show(txt1 + _routineName + txt2 + ex.Message, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Error, isChs);
                                    }
                                    finally
                                    {
                                    }
                                }
                            }
                            else
                            {                              
                                txt1 = isChs ? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                                caption = isChs ? "警告信息" : "Warning Message";                              
                                ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                            }
                        }
                        else
                        {
                            ProCommon.UserDefForm.FrmMsgBox.Show(_warningText, _warningCaption, 
                                ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                        }
                    }
                    break;
                #endregion

                #region 加载程式
                case "BBI_LOADROUTINE":
                    {
                        if (!Manager.SystemManager.Instance.IsRunning)
                        {
                            if (CurrentAccount != null
                                && CurrentAccount.Authority != ProCommon.Communal.AccountAuthority.None)
                            {

                                System.Windows.Forms.OpenFileDialog ofd = new OpenFileDialog();
                                ofd.Title = isChs? "选择运行程式":"Select the routine";
                                ofd.InitialDirectory = Manager.ConfigManager.Instance.CfgSystem.RoutineDirectory;
                                ofd.Filter = isChs ? "运行程式(*.pro)|*.pro":"Run routine(*.pro)|*.pro";
                                ofd.Multiselect = false;
                                if (ofd.ShowDialog() == DialogResult.OK)
                                {
                                    _routineName = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                                    _routineDirectory = System.IO.Path.GetDirectoryName(ofd.FileName);
                                    this.beiCurrentRoutine.EditValue = _routineName;

                                    _para2DBarcode = null;
                                    //加载指定文件并反序列化为视觉参数类实例                                   
                                    try
                                    {
                                        using (var fs = new System.IO.FileStream(_routineDirectory + "\\" + _routineName + ".pro", System.IO.FileMode.Open))
                                        {
                                            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                                            _para2DBarcode = (Pro2DBarcode.Config.VisionPara.VisionPara2DBarcode)bf.Deserialize(fs);
                                        }
                                        Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode = _para2DBarcode;
                                    }
                                    catch (System.Exception ex)
                                    {
                                        txt1 = isChs ? "加载程式参数失败!\r\n" : "Load routine parameter failed !\r\n";                                       
                                        caption = isChs ? "错误信息" : "Error Message";
                                        ProCommon.UserDefForm.FrmMsgBox.Show(txt1 + ex.Message, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Error, isChs);                                     
                                    }
                                }
                            }
                            else
                            {
                                txt1 = isChs ? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                                caption = isChs ? "警告信息" : "Warning Message";
                                ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning,isChs);
                            }
                        }
                        else
                        {
                            ProCommon.UserDefForm.FrmMsgBox.Show(_warningText, _warningCaption,
                               ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                        }
                    }
                    break;
                #endregion

                #region 程式管理
                case "BBI_ROUTINEMANAGER":
                    {
                        if (!Manager.SystemManager.Instance.IsRunning)
                        {
                            if (CurrentAccount != null
                                && CurrentAccount.Authority == ProCommon.Communal.AccountAuthority.Administrator)
                            {
                                this.Hide();
                                UI.FrmRoutineManager frm = new FrmRoutineManager(LanguageVersion,Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem);
                                frm.ShowDialog();

                                if (frm.IsNewCommand)
                                {
                                    _routineName = frm.RoutineName;
                                    _routineDirectory = frm.RoutineDirectorySelected;
                                    this.beiCurrentRoutine.EditValue = _routineName;

                                    //创建一个视觉参数类实例
                                    if (_para2DBarcode == null)
                                        _para2DBarcode = new Config.VisionPara.VisionPara2DBarcode();

                                    _para2DBarcode.IsSaveImageAll = false;
                                    _para2DBarcode.IsSaveImageNG = false;
                                    _para2DBarcode.IsSaveImageOK = false;
                                    _para2DBarcode.PathForSaveImageAll = _routineDirectory + "\\Image_All";
                                    _para2DBarcode.PathForSaveImageNG = _routineDirectory + "\\Image_NG";
                                    _para2DBarcode.PathForSaveImageOK = _routineDirectory + "\\Image_OK";
                                    _para2DBarcode.PathForInspectArea = null;
                                    _para2DBarcode.CameraExposure = 99000.0f;
                                    _para2DBarcode.CameraGain = 1.5f;                                  
                                    _para2DBarcode.CodeType = "Data Matrix ECC 200";
                                    _para2DBarcode.TimeOut = 500;
                                    _para2DBarcode.ToBeFoundNumber = 1;
                                    _para2DBarcode.GenParameterNamesForCreate = new string[] { "default_parameters" };
                                    _para2DBarcode.GenParameterValuesForCreate = new string[] { "maximum_recognition" };
                                    _para2DBarcode.ColorStrForResultNotFound = "red";
                                    _para2DBarcode.ColorStrForResultFound = "green";

                                    Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode = _para2DBarcode;
                                }
                                else if (frm.IsEditCommand)
                                {
                                    //加载指定文件并反序列化为视觉参数类实例
                                    _routineName = frm.RoutineName;
                                    _routineDirectory = frm.RoutineDirectorySelected;
                                    this.beiCurrentRoutine.EditValue = _routineName;
                                    _para2DBarcode = null;
                                    if (System.IO.File.Exists(_routineDirectory + "\\" + _routineName + ".pro"))
                                    {
                                        try
                                        {
                                            using (var fs = new System.IO.FileStream(_routineDirectory + "\\" + _routineName + ".pro", System.IO.FileMode.OpenOrCreate))
                                            {
                                                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                                                _para2DBarcode = (Pro2DBarcode.Config.VisionPara.VisionPara2DBarcode)bf.Deserialize(fs);
                                                Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode = _para2DBarcode;
                                            }

                                        }
                                        catch (System.Exception ex)
                                        {
                                            txt1 = isChs ? "加载程式参数失败!\r\n" : "Load routine parameter failed !\r\n";
                                            caption = isChs ? "错误信息" : "Error Message";
                                            ProCommon.UserDefForm.FrmMsgBox.Show(txt1 + ex.Message, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Error, isChs);
                                        }
                                    }
                                    else
                                    {
                                        txt1 = isChs ? "选择程式未创建参数文件!是否现在创建?" : "Routine selected has no parameter file ! Create it now ?";
                                        caption = isChs ? "询问信息" : "Question Message";
                                        if(ProCommon.UserDefForm.FrmMsgBox.Show(txt1,caption,ProCommon.UserDefForm.MyButtons.YesNo,ProCommon.UserDefForm.MyIcon.Question,isChs) == DialogResult.Yes)                                      
                                        {
                                            if (FrmSetRoutinePara == null)
                                                FrmSetRoutinePara = new FrmSetRoutinePara(LanguageVersion);
                                            Manager.SystemManager.Instance.IsRunning = true;
                                            Manager.SystemManager.Instance.IsRunOnce = false;
                                            this.Hide();
                                            //用上次程式的参数作为初始参数进行创建
                                            Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode.PathForSaveImageAll = _routineDirectory + "\\Image_All";
                                            Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode.PathForSaveImageNG = _routineDirectory + "\\Image_NG";
                                            Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode.PathForSaveImageOK = _routineDirectory + "\\Image_OK";
                                            FrmSetRoutinePara.Para2DBarcode = Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode;
                                            _para2DBarcode = Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode;
                                            FrmSetRoutinePara.RoutineDirectory = _routineDirectory;
                                            FrmSetRoutinePara.RoutineName = _routineName;                                           
                                            FrmSetRoutinePara.SpecifiedCamera = CameraFor2DBarcode;
                                            FrmSetRoutinePara.ShowDialog();
                                            Manager.SystemManager.Instance.IsRunning = false;
                                            Manager.SystemManager.Instance.IsRunOnce = true;
                                            this.Show();
                                        }
                                    }
                                }
                                else if (frm.IsLoadCommand)
                                {
                                    //加载指定文件并反序列化为视觉参数类实例
                                    _routineName = frm.RoutineName;
                                    _routineDirectory = frm.RoutineDirectorySelected;
                                    this.beiCurrentRoutine.EditValue = _routineName;
                                    _para2DBarcode = null;
                                    if (System.IO.File.Exists(_routineDirectory + "\\" + _routineName + ".pro"))
                                    {
                                        try
                                        {
                                            using (var fs = new System.IO.FileStream(_routineDirectory + "\\" + _routineName + ".pro", System.IO.FileMode.OpenOrCreate))
                                            {
                                                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                                                _para2DBarcode = (Pro2DBarcode.Config.VisionPara.VisionPara2DBarcode)bf.Deserialize(fs);
                                                Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode = _para2DBarcode;
                                            }

                                        }
                                        catch (System.Exception ex)
                                        {
                                            txt1 = isChs ? "加载程式参数失败!\r\n" : "Load routine parameter failed !\r\n";
                                            caption = isChs ? "错误信息" : "Error Message";
                                            ProCommon.UserDefForm.FrmMsgBox.Show(txt1 + ex.Message, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Error, isChs);
                                        }
                                    }
                                    else
                                    {
                                        txt1 = isChs ? "选择程式未创建参数文件!是否现在创建?" : "Routine selected has no parameter file ! Create it now ?";
                                        caption = isChs ? "询问信息" : "Question Message";
                                        if (ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.YesNo, ProCommon.UserDefForm.MyIcon.Question, isChs) == DialogResult.Yes)
                                        {
                                            if (FrmSetRoutinePara == null)
                                                FrmSetRoutinePara = new FrmSetRoutinePara(LanguageVersion);
                                            Manager.SystemManager.Instance.IsRunning = true;
                                            Manager.SystemManager.Instance.IsRunOnce = false;
                                            this.Hide();
                                            //用上次程式的参数作为初始参数进行创建
                                            Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode.PathForSaveImageAll = _routineDirectory + "\\Image_All";
                                            Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode.PathForSaveImageNG = _routineDirectory + "\\Image_NG";
                                            Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode.PathForSaveImageOK = _routineDirectory + "\\Image_OK";
                                            FrmSetRoutinePara.Para2DBarcode = Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode;
                                            _para2DBarcode = Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode;
                                            FrmSetRoutinePara.RoutineDirectory = _routineDirectory;
                                            FrmSetRoutinePara.RoutineName = _routineName;
                                            FrmSetRoutinePara.SpecifiedCamera = CameraFor2DBarcode;
                                            FrmSetRoutinePara.ShowDialog();
                                            Manager.SystemManager.Instance.IsRunning = false;
                                            Manager.SystemManager.Instance.IsRunOnce = true;
                                            this.Show();
                                        }
                                    }
                                }
                                else if (frm.IsDeleteCommand)
                                {
                                    //判断删除的程式是否当前显示的程式，若是,则当前显示的程式为空
                                    this.beiCurrentRoutine.EditValue = null;
                                    _para2DBarcode = null;
                                    try
                                    {
                                        if (System.IO.File.Exists(_routineDirectory + "\\" + _routineName + ".pro"))
                                        {
                                            System.IO.File.Delete(_routineDirectory + "\\" + _routineName + ".pro");
                                        }
                                    }
                                    catch (System.Exception ex)
                                    {
                                        txt1 = isChs ? "删除程式参数失败!\r\n" : "Delete routine parameter failed !\r\n";
                                        caption = isChs ? "错误信息" : "Error Message";
                                        ProCommon.UserDefForm.FrmMsgBox.Show(txt1 + ex.Message, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Error, isChs);
                                    }
                                }
                                else if (frm.IsClearCommand)
                                {
                                    this.beiCurrentRoutine.EditValue = null;
                                    _para2DBarcode = null;
                                    try
                                    {
                                        if (System.IO.Directory.Exists(_routineDirectory))
                                        {
                                            System.IO.Directory.Delete(_routineDirectory, true);
                                        }
                                    }
                                    catch (System.Exception ex)
                                    {
                                        txt1 = isChs ? "清空程式参数失败!\r\n" : "Clear routine parameter failed !\r\n";
                                        caption = isChs ? "错误信息" : "Error Message";
                                        ProCommon.UserDefForm.FrmMsgBox.Show(txt1 + ex.Message, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Error, isChs);
                                    }
                                }
                                this.Show();
                            }
                            else
                            {
                                txt1 = isChs ? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                                caption = isChs ? "警告信息" : "Warning Message";
                                ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                            }
                        }
                        else
                        {
                            ProCommon.UserDefForm.FrmMsgBox.Show(_warningText, _warningCaption,
                               ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                        }
                    }
                    break;
                #endregion

                #region 系统设置
                case "BBI_SYSTEMSET":
                    {
                        if (!Manager.SystemManager.Instance.IsRunning)
                        {
                            this.Hide();                          
                            Pro2DBarcode.UI.UI_SetConfigs uiSetConfig = new UI_SetConfigs(CurrentAccount,LanguageVersion, Manager.ConfigManager.Instance);
                            uiSetConfig.ShowDialog(this);
                            this.Show();                 
                        }
                        else
                        {
                            ProCommon.UserDefForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                        }
                    }
                    break;
                #endregion

                #region 程式设置
                case "BBI_ROUTINESET":
                    {
                        if (!Manager.SystemManager.Instance.IsRunning)
                        {
                            if (CurrentAccount != null
                                && CurrentAccount.Authority != ProCommon.Communal.AccountAuthority.None)
                            {
                                //此处需要加载程式，更新视觉参数配置
                                if (_para2DBarcode != null)
                                {
                                    if (FrmSetRoutinePara == null)
                                        FrmSetRoutinePara = new FrmSetRoutinePara(LanguageVersion);
                                    this.Hide();
                                    Manager.SystemManager.Instance.IsRunning = true;
                                    Manager.SystemManager.Instance.IsRunOnce = false;

                                    FrmSetRoutinePara.Para2DBarcode = Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode;
                                    _para2DBarcode = Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode;
                                    FrmSetRoutinePara.RoutineDirectory = _routineDirectory;
                                    FrmSetRoutinePara.RoutineName = _routineName;
                                    FrmSetRoutinePara.SpecifiedCamera = CameraFor2DBarcode;
                                    FrmSetRoutinePara.ShowDialog();
                                    Manager.SystemManager.Instance.IsRunning = false;
                                    Manager.SystemManager.Instance.IsRunOnce = true;
                                    this.Show();
                                }
                                else
                                {
                                    txt1 = isChs ? "无法操作!\r\n未创建或加载程式参数。" : "Denied!\r\n No routine parameter file created .";
                                    caption = isChs ? "警告信息" : "Warning Message";
                                    ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                                }
                            }
                            else
                            {
                                txt1 = isChs ? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                                caption = isChs ? "警告信息" : "Warning Message";
                                ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                            }
                        }
                        else
                        {
                            ProCommon.UserDefForm.FrmMsgBox.Show(_warningText, _warningCaption,
                               ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                        }
                    }
                    break;
                #endregion

                #region 登录
                case "BBI_LOGIN":
                    {
                        if (!Manager.SystemManager.Instance.IsRunning)
                        {
                            //根据当前语言版本,显示登录窗体,隐藏主窗体,登录成功后,显示主窗体,根据登录账户更新显示控件(账户操作权限分配待做)
                            UI.UI_LogIn uiLogin = new UI_LogIn(LanguageVersion, Manager.ConfigManager.Instance.CfgAccount);
                            if (uiLogin.ShowDialog(this) != DialogResult.OK)
                            {
                                if (CurrentAccount != null)
                                {
                                    CurrentAccount.Authority = ProCommon.Communal.AccountAuthority.None;
                                    CurrentAccount.Name = "匿名";
                                    CurrentAccount.PassWord = string.Empty;
                                }
                            }
                                                        
                            UpdateControlWhenAccountChanged(CurrentAccount);
                        }
                        else
                        {
                            ProCommon.UserDefForm.FrmMsgBox.Show(_warningText, _warningCaption,
                               ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                        }
                    }
                    break;
                #endregion

                #region 账户管理
                case "BBI_ACCOUNTMANAGER":
                    {
                        if (!Manager.SystemManager.Instance.IsRunning)
                        {
                            //根据当前语言版本,显示账户管理窗体,隐藏主窗体,账户管理完成后,显示主窗体
                            if(CurrentAccount!=null)
                            {
                                UI.UI_AccountManager uiAccountManager = new UI_AccountManager(LanguageVersion, CurrentAccount, Manager.ConfigManager.Instance.CfgAccount);
                                uiAccountManager.ShowDialog(this);
                            }                          
                        }
                        else
                        {
                            ProCommon.UserDefForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                        }
                    }
                    break;
                #endregion

                #region 登出
                case "BBI_LOGOUT":
                    {
                        if (!Manager.SystemManager.Instance.IsRunning)
                        {

                            //账户恢复默认账户,根据登录账户更新显示控件(账户操作权限分配待做)
                            if (CurrentAccount != null)
                            {
                                CurrentAccount.Authority = ProCommon.Communal.AccountAuthority.None;
                                CurrentAccount.Name = "匿名";
                                CurrentAccount.PassWord = string.Empty;
                            }

                            UpdateControlWhenAccountChanged(CurrentAccount);
                        }
                        else
                        {
                            ProCommon.UserDefForm.FrmMsgBox.Show(_warningText, _warningCaption,
                               ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                        }
                    }
                    break;
                #endregion

                #region 厂商信息
                case "BBI_VENDORINFO":
                    {
                        if (!Manager.SystemManager.Instance.IsRunning)
                        {
                        }
                        else
                        {
                            ProCommon.UserDefForm.FrmMsgBox.Show(_warningText, _warningCaption,
                               ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                        }
                    }
                    break;
                #endregion

                #region 软件信息
                case "BBI_APPINFO":
                    {
                        if (!Manager.SystemManager.Instance.IsRunning)
                        {

                            //根据当前语言版本,显示软件帮助窗体
                         
                        }
                        else
                        {
                            ProCommon.UserDefForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                        }
                    }
                    break;
                #endregion

                #region 单次运行
                case "BBI_RUNONCE":
                    {
                        if (!Manager.SystemManager.Instance.IsRunning)
                        {
                            if (CurrentAccount != null
                                && CurrentAccount.Authority != ProCommon.Communal.AccountAuthority.None)
                            {
                                if (!string.IsNullOrEmpty(_routineName))
                                {
                                    if (_para2DBarcode != null)
                                    {
                                        if (!string.IsNullOrEmpty(_para2DBarcode.PathForInspectArea))
                                        {
                                            try
                                            {
                                                Manager.SystemManager.Instance.IsRunning = true;
                                                Manager.SystemManager.Instance.IsRunOnce = true;

                                                if (DevCam == null)
                                                    DevCam = (Pro2DBarcode.Device.Device_Camera)Pro2DBarcode.Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Camera];
                                                DevCam.IsDebug = false;
                                                DevCam.IsInitProcess = true;
                                                DevCam.CamHandleListNew[CameraFor2DBarcode.ID].SetExposureTime(Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode.CameraExposure);
                                                DevCam.CamHandleListNew[CameraFor2DBarcode.ID].SetGain(Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode.CameraGain);
                                                DevCam.CamHandleListNew[CameraFor2DBarcode.ID].SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.SoftTrigger, 1);
                                                DevCam.CamHandleListNew[CameraFor2DBarcode.ID].SoftTriggerOnce();

                                                this.lblRunState.Text = isChs ? "单次" : "ONCE";
                                                this.bsbiRoutine.Enabled = true;
                                                this.bsbiSet.Enabled = true;
                                                this.bsbiAccount.Enabled = true;
                                                this.bsbiHelp.Enabled = true;
                                                this.bbiRunOnce.Enabled = true;
                                                this.bbiRunContinue.Enabled = true;
                                                this.bbiRunStop.Enabled = true;
                                                this.bbiProductStatistic.Enabled = true;
                                                this.bbiExit.Enabled = true;                                               
                                            }
                                            catch (System.Exception ex)
                                            {
                                                txt1 = isChs ? "加载程式参数失败!\r\n" : "Load routine parameter failed !\r\n";
                                                caption = isChs ? "错误信息" : "Error Message";
                                                ProCommon.UserDefForm.FrmMsgBox.Show(txt1+ex.Message, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Error, isChs);
                                            }
                                        }
                                        else
                                        {
                                            txt1 = isChs ? "无法操作!\r\n新建程式未设置检测区域。" : "Denied!\r\n No inspect area created .";
                                            caption = isChs ? "警告信息" : "Warning Message";                                          
                                            ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                                        }
                                    }
                                    else
                                    {
                                        txt1 = isChs ? "无法操作!\r\n未选择程式。" : "Denied!\r\n No routine selected .";
                                        caption = isChs ? "警告信息" : "Warning Message";
                                        ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                                    }
                                }
                                else
                                {
                                    txt1 = isChs ? "未加载程式!\r\n" : "No routine loaded !\r\n";
                                    caption = isChs ? "错误信息" : "Error Message";
                                    ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Error, isChs);
                                }
                            }
                            else
                            {
                                txt1 = isChs ? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                                caption = isChs ? "警告信息" : "Warning Message";
                                ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                            }
                        }
                        else
                        {
                            ProCommon.UserDefForm.FrmMsgBox.Show(_warningText, _warningCaption,
                               ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                        }
                    }
                    break;
                #endregion

                #region 连续运行
                case "BBI_RUNCONTINUE":
                    {
                        if (!Manager.SystemManager.Instance.IsRunning)
                        {
                            if (CurrentAccount != null
                                && CurrentAccount.Authority != ProCommon.Communal.AccountAuthority.None)
                            {
                                if (!string.IsNullOrEmpty(_routineName))
                                {
                                    if (_para2DBarcode != null)
                                    {
                                        if (!string.IsNullOrEmpty(_para2DBarcode.PathForInspectArea))
                                        {
                                            try
                                            {
                                                Manager.SystemManager.Instance.IsRunning = true;
                                                Manager.SystemManager.Instance.IsRunOnce = false;

                                                Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.LastRoutinePath = _routineDirectory + "\\" + _routineName + ".pro";
                                                if (DevCam == null)
                                                    DevCam = (Pro2DBarcode.Device.Device_Camera)Pro2DBarcode.Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Camera];
                                                DevCam.IsDebug = false;
                                                DevCam.IsInitProcess = true;
                                                DevCam.CamHandleListNew[CameraFor2DBarcode.ID].SetExposureTime(Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode.CameraExposure);
                                                DevCam.CamHandleListNew[CameraFor2DBarcode.ID].SetGain(Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode.CameraGain);
                                                DevCam.CamHandleListNew[CameraFor2DBarcode.ID].SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.ExternalTrigger, 1);
                                                this.lblRunState.Text = isChs ? "连续" : "CONTINUE";
                                                this.bsbiRoutine.Enabled = false;
                                                this.bsbiSet.Enabled = false;
                                                this.bsbiAccount.Enabled = false;
                                                this.bsbiHelp.Enabled = false;
                                                this.bbiRunOnce.Enabled = false;
                                                this.bbiRunContinue.Enabled = true;
                                                this.bbiRunStop.Enabled = true;
                                                this.bbiProductStatistic.Enabled = false;
                                                this.bbiExit.Enabled = false;
                                            }
                                            catch (System.Exception ex)
                                            {
                                                txt1 = isChs ? "加载程式参数失败!\r\n" : "Load routine parameter failed !\r\n";
                                                caption = isChs ? "错误信息" : "Error Message";
                                                ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Error, isChs);
                                            }
                                        }
                                        else
                                        {
                                            txt1 = isChs ? "无法操作!\r\n新建程式未设置检测区域。" : "Denied!\r\n No inspect area created .";
                                            caption = isChs ? "警告信息" : "Warning Message";
                                            ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                                        }
                                    }
                                    else
                                    {
                                        txt1 = isChs ? "无法操作!\r\n未选择程式。" : "Denied!\r\n No routine selected .";
                                        caption = isChs ? "警告信息" : "Warning Message";
                                        ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                                    }
                                }
                                else
                                {
                                    txt1 = isChs ? "未加载程式!\r\n" : "No routine loaded !\r\n";
                                    caption = isChs ? "错误信息" : "Error Message";
                                    ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Error, isChs);
                                }
                            }
                            else
                            {
                                txt1 = isChs ? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                                caption = isChs ? "警告信息" : "Warning Message";
                                ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                            }
                        }
                        else
                        {
                            ProCommon.UserDefForm.FrmMsgBox.Show(_warningText, _warningCaption,
                               ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                        }
                    }
                    break;

                #endregion

                #region 停止运行
                case "BBI_RUNSTOP":
                    {
                        if (CurrentAccount != null
                               && CurrentAccount.Authority != ProCommon.Communal.AccountAuthority.None)
                        {
                            Manager.SystemManager.Instance.IsRunning = false;
                            Manager.SystemManager.Instance.IsRunOnce = true;

                            if (DevCam == null)
                                DevCam = (Pro2DBarcode.Device.Device_Camera)Pro2DBarcode.Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Camera];

                            //停止,使相机暂时不工作
                            //迈德威视相机停止相机会释放相机资源,故只需要切换采集模式即可(非程序工作时的采集模式)
                            if (!DevCam.CamHandleListNew[CameraFor2DBarcode.ID].SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.ExternalTrigger, 1))
                            {
                                txt1 = isChs ? "相机连接异常!" : "Camera connection lost !";
                                caption = isChs ? "警告信息" : "Warning Message";
                                ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                            }

                            this.lblRunState.Text = isChs ? "停止" : "STOP";
                            this.bsbiRoutine.Enabled = true;
                            this.bsbiSet.Enabled = true;
                            this.bsbiAccount.Enabled = true;
                            this.bsbiHelp.Enabled = true;
                            this.bbiRunOnce.Enabled = true;
                            this.bbiRunContinue.Enabled = true;
                            this.bbiRunStop.Enabled = true;
                            this.bbiProductStatistic.Enabled = true;
                            this.bbiExit.Enabled = true;
                        }
                        else
                        {
                            txt1 = isChs ? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                            caption = isChs ? "警告信息" : "Warning Message";
                            ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                        }
                    }
                    break;
                #endregion

                #region 产品统计
                case "BBI_PRODUCTSTATISTIC":
                    {
                        if (!Manager.SystemManager.Instance.IsRunning)
                        {
                            
                        }
                        else
                        {
                            ProCommon.UserDefForm.FrmMsgBox.Show(_warningText, _warningCaption,
                               ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                        }
                    }
                    break;
                #endregion

                #region 系统退出
                case "BBI_EXIT":
                    {
                        if (!Manager.SystemManager.Instance.IsRunning)
                        {
                            txt1 = isChs ? "确认退出系统吗?" : "Exit the application ?";
                            caption = isChs ? "询问信息" : "Question Message";
                            if(ProCommon.UserDefForm.FrmMsgBox.Show(txt1, caption, ProCommon.UserDefForm.MyButtons.YesNo, ProCommon.UserDefForm.MyIcon.Question, isChs) == DialogResult.Yes)
                            {
                                Pro2DBarcode.Manager.ConfigManager.Instance.Save();
                                Pro2DBarcode.Manager.DeviceManager.Instance.DeviceListStop();
                                Pro2DBarcode.Manager.DeviceManager.Instance.DeviceListRelease();
                                this.Close();
                            }
                        }
                        else
                        {
                            ProCommon.UserDefForm.FrmMsgBox.Show(_warningText, _warningCaption,
                               ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                        }
                    }
                    break;
                #endregion

                #region 相机设置
                case "BBI_CAMERASET":
                    {
                        if (!Manager.SystemManager.Instance.IsRunning)
                        {
                            //根据当前语言版本,显示相机设置窗体,隐藏主窗体,相机设置完成后,显示主窗体
                           
                        }
                        else
                        {
                            ProCommon.UserDefForm.FrmMsgBox.Show(_warningText, _warningCaption,
                               ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                        }
                    }
                    break;
                #endregion

                default:
                    break;
            }
        }

        /// <summary>
        /// 更新BarStaticItem控件
        /// </summary>
        /// <param name="bsti"></param>
        /// <param name="lan"></param>
        /// <param name="prefix"></param>
        private void UpdateBarStaticItem(DevExpress.XtraBars.BarStaticItem bsti, ProCommon.Communal.Language lan, string prefix)
        {
            if (bsti != null
              && bsti.Tag != null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = bsti.Tag.ToString();
                bsti.Caption = (isChs ? Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str)) + ":" + prefix;
            }
        }

        /// <summary>
        /// 更新BarSubItem控件
        /// </summary>
        /// <param name="bsbi"></param>
        /// <param name="lan"></param>
        private void UpdateBarSubItem(DevExpress.XtraBars.BarSubItem bsbi, ProCommon.Communal.Language lan)
        {
            if (bsbi != null
              && bsbi.Tag != null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = bsbi.Tag.ToString();
                bsbi.Caption = isChs ? Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str);
            }
        }

        private void UpdateBarSubItem(DevExpress.XtraBars.BarSubItem bsbi, ProCommon.Communal.Language lan, ProCommon.Communal.CtrllerCategory ctrlCategory, Manager.ConfigManager cfgManager)
        {
            UpdateBarSubItem(bsbi, lan);
            if (bsbi != null
                && cfgManager != null)
            {
                int ctrlCount = 0;
                bsbi.ItemLinks.Clear();
                switch (ctrlCategory)
                {
                    #region 相机
                    case ProCommon.Communal.CtrllerCategory.Camera:
                        {
                            ctrlCount = cfgManager.CfgCamera.CameraList.Count;
                            if (ctrlCount > 0)
                            {
                                for (int i = 0; i < ctrlCount; i++)
                                {
                                    DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rpiImgCmbe = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
                                    UpdateRepositoryItemImgCmbe(rpiImgCmbe, lan);
                                    DevExpress.XtraBars.BarEditItem bei = new DevExpress.XtraBars.BarEditItem(this.bmMain, rpiImgCmbe);
                                    bei.Tag = cfgManager.CfgCamera.CameraList[i].ID;
                                    bei.EditValue = false;
                                    bei.EditWidth = 80;
                                    //bei.Caption = (lan == ProCommon.Communal.Language.Chinese ? "相机" : "Camera") + i.ToString("00") + "#";
                                    bei.Caption = cfgManager.CfgCamera.CameraList[i].Name;
                                    bsbi.AddItem(bei);
                                }
                            }
                            else
                            {
                                bsbi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                        }
                        break;
                    #endregion

                    #region 板卡
                    case ProCommon.Communal.CtrllerCategory.Board:
                        {
                            ctrlCount = cfgManager.CfgBoardCtrller.BoardCtrllerList.Count;
                            if (ctrlCount > 0)
                            {
                                for (int i = 0; i < ctrlCount; i++)
                                {
                                    DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rpiImgCmbe = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
                                    UpdateRepositoryItemImgCmbe(rpiImgCmbe, lan);
                                    DevExpress.XtraBars.BarEditItem bei = new DevExpress.XtraBars.BarEditItem(this.bmMain, rpiImgCmbe);
                                    bei.Tag = cfgManager.CfgBoardCtrller.BoardCtrllerList[i].ID;
                                    bei.EditValue = false;
                                    bei.EditWidth = 80;
                                    //bei.Caption = (lan == ProCommon.Communal.Language.Chinese ? "板卡" : "Board") + i.ToString("00") + "#";
                                    bei.Caption = cfgManager.CfgBoardCtrller.BoardCtrllerList[i].Name;
                                    bsbi.AddItem(bei);
                                }
                            }
                            else
                            {
                                bsbi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                        }
                        break;
                    #endregion

                    #region Socket
                    case ProCommon.Communal.CtrllerCategory.Socket:
                        {
                            ctrlCount = cfgManager.CfgSocket.ComSocketList.Count;
                            if (ctrlCount > 0)
                            {
                                for (int i = 0; i < ctrlCount; i++)
                                {
                                    DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rpiImgCmbe = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
                                    UpdateRepositoryItemImgCmbe(rpiImgCmbe, lan);
                                    DevExpress.XtraBars.BarEditItem bei = new DevExpress.XtraBars.BarEditItem(this.bmMain, rpiImgCmbe);
                                    bei.Tag = cfgManager.CfgSocket.ComSocketList[i].ID;
                                    bei.EditValue = false;
                                    bei.EditWidth = 120;
                                    //bei.Caption = (lan == ProCommon.Communal.Language.Chinese ? "网络端口" : "Socket") + i.ToString("00") + "#";
                                    bei.Caption = cfgManager.CfgSocket.ComSocketList[i].Name;
                                    bsbi.AddItem(bei);
                                }
                            }
                            else
                            {
                                bsbi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                        }
                        break;
                    #endregion

                    #region WebService
                    case ProCommon.Communal.CtrllerCategory.Web:
                        {
                            ctrlCount = cfgManager.CfgWeb.ComWebList.Count;
                            if (ctrlCount > 0)
                            {
                                for (int i = 0; i < ctrlCount; i++)
                                {
                                    DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rpiImgCmbe = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
                                    UpdateRepositoryItemImgCmbe(rpiImgCmbe, lan);
                                    DevExpress.XtraBars.BarEditItem bei = new DevExpress.XtraBars.BarEditItem(this.bmMain, rpiImgCmbe);
                                    bei.Tag = cfgManager.CfgWeb.ComWebList[i].ID;
                                    bei.EditValue = false;
                                    bei.EditWidth = 120;
                                    //bei.Caption = (lan == ProCommon.Communal.Language.Chinese ? "网端客服" : "Web") + i.ToString("00") + "#";
                                    bei.Caption = cfgManager.CfgWeb.ComWebList[i].Name;
                                    bsbi.AddItem(bei);
                                }
                            }
                            else
                            {
                                bsbi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                        }
                        break;
                    #endregion

                    #region PLC
                    case ProCommon.Communal.CtrllerCategory.PLC:
                        break;
                    #endregion

                    #region SerialPort
                    case ProCommon.Communal.CtrllerCategory.SerialPort:
                        {
                            ctrlCount = cfgManager.CfgSerialPort.ComSerialPortList.Count;
                            if (ctrlCount > 0)
                            {

                                for (int i = 0; i < ctrlCount; i++)
                                {
                                    DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rpiImgCmbe = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
                                    UpdateRepositoryItemImgCmbe(rpiImgCmbe, lan);
                                    DevExpress.XtraBars.BarEditItem bei = new DevExpress.XtraBars.BarEditItem(this.bmMain, rpiImgCmbe);
                                    bei.Tag = cfgManager.CfgSerialPort.ComSerialPortList[i].ID;
                                    bei.EditValue = false;
                                    bei.EditWidth = 80;
                                    //bei.Caption = (lan == ProCommon.Communal.Language.Chinese ? "串口" : "COM") + i.ToString("00") + "#";
                                    bei.Caption = cfgManager.CfgSerialPort.ComSerialPortList[i].Name;
                                    bsbi.AddItem(bei);
                                }
                            }
                            else
                            {
                                bsbi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                        }
                        break;
                    #endregion

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 更新RepositoryItemImageComboBoxEdit
        /// </summary>
        /// <param name="rpiImgCmbe"></param>
        /// <param name="lan"></param>
        private void UpdateRepositoryItemImgCmbe(DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rpiImgCmbe, ProCommon.Communal.Language lan)
        {
            if (rpiImgCmbe != null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                rpiImgCmbe.Items.Clear();
                rpiImgCmbe.SmallImages = this.imgColSmall;
                rpiImgCmbe.LargeImages = this.imgColLarge;

                string onStr = isChs ? Properties.Resources.ResourceManager.GetString("chs_ONLINE") : Properties.Resources.ResourceManager.GetString("en_ONLINE");
                string offStr = isChs ? Properties.Resources.ResourceManager.GetString("chs_OFFLINE") : Properties.Resources.ResourceManager.GetString("en_OFFLINE");
                DevExpress.XtraEditors.Controls.ImageComboBoxItem imgCmbi1 = new DevExpress.XtraEditors.Controls.ImageComboBoxItem(offStr, false, 2);
                DevExpress.XtraEditors.Controls.ImageComboBoxItem imgCmbi2 = new DevExpress.XtraEditors.Controls.ImageComboBoxItem(onStr, true, 3);

                rpiImgCmbe.Items.Add(imgCmbi1);
                rpiImgCmbe.Items.Add(imgCmbi2);
                rpiImgCmbe.ReadOnly = true;
            }
        }

        private void UpdateSimpleButton(DevExpress.XtraEditors.SimpleButton sbtn, ProCommon.Communal.Language lan)
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

        /// <summary>
        /// SimpleButton单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sbtn_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if(sbtn!=null)
            {
                switch(sbtn.Tag.ToString())
                {
                    case "SBTN_COUNTCLEAR":
                        {
                            if (!Manager.SystemManager.Instance.IsRunning)
                            {
                                if (CurrentAccount != null
                                    && CurrentAccount.Authority == ProCommon.Communal.AccountAuthority.Administrator)
                                {
                                    this.btneProOK.Text = "0";
                                    this.btneProNG.Text = "0";
                                    this.btneProTotal.Text = "0";
                                    this.btneProYieldRatio.Text = "0.00";
                                    this.btneElapse.Text = "0.00";
                                    Manager.UIManager.Instance.ResetResult();                                                                   
                                }
                                else
                                {
                                    bool isChs = LanguageVersion == ProCommon.Communal.Language.Chinese;
                                    string txt = isChs? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                                    string caption = isChs ? "警告信息" : "Warning Message";
                                    MessageBox.Show(txt, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show(_warningText, _warningCaption,
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                            }
                        }
                        break;
                    default:break;
                }
            }
        }

        private void UpdateBarEditItem(DevExpress.XtraBars.BarEditItem bei, ProCommon.Communal.Language lan)
        {
            if (bei != null
              && bei.Tag != null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = bei.Tag.ToString();
                bei.Caption = isChs ? Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str);
            }
        }

        private void UpdateLabelControl(DevExpress.XtraEditors.LabelControl lbl, ProCommon.Communal.Language lan)
        {
            if (lbl != null
             && lbl.Tag != null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = lbl.Tag.ToString();
                lbl.Text = isChs ? Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str);
            }
        }

        /// <summary>
        /// 根据当前账户的操作权限控制控件的显隐或使能
        /// </summary>
        /// <param name="acc"></param>
        private void UpdateControlWhenAccountChanged(ProCommon.Communal.Account acc)
        {
            if(acc!=null)
            {
                switch (acc.Authority)
                {
                    case ProCommon.Communal.AccountAuthority.Junior:
                        {
                            this.bsbiAccount.Enabled = true;
                            this.bbiAccountManager.Enabled = false;
                        }
                        break;
                    case ProCommon.Communal.AccountAuthority.Senior:
                        {
                            this.bsbiAccount.Enabled = true;
                            this.bbiAccountManager.Enabled = false;
                        }
                        break;
                    case ProCommon.Communal.AccountAuthority.Administrator:
                        {
                            this.bbiAccountManager.Enabled = true;
                        }
                        break;
                    case ProCommon.Communal.AccountAuthority.None:
                    default:
                        {
                            this.bbiAccountManager.Enabled = false;
                        }
                        break;
                }
                this.bstiCurrentAccount.Caption = CurrentAccount.Name;
            }           
        }

        /// <summary>
        /// 加载上次程式
        /// </summary>
        /// <param name="enbaleLastRoutine"></param>
        /// <param name="pathForLastRoutine"></param>
        private void LoadInitRoutine(bool enbaleLastRoutine, string pathForLastRoutine)
        {
            bool isChs = LanguageVersion == ProCommon.Communal.Language.Chinese;
            string txt1, caption;          
            if (enbaleLastRoutine)
            {
                if (!string.IsNullOrEmpty(pathForLastRoutine))
                {
                    if (System.IO.File.Exists(pathForLastRoutine))
                    {
                        _routineName = System.IO.Path.GetFileNameWithoutExtension(pathForLastRoutine);
                        _routineDirectory = System.IO.Path.GetDirectoryName(pathForLastRoutine);
                        this.beiCurrentRoutine.EditValue = _routineName;

                        _para2DBarcode = null;
                        //加载指定文件并反序列化为视觉参数类实例                                   
                        try
                        {
                            using (var fs = new System.IO.FileStream(_routineDirectory + "\\" + _routineName + ".pro", System.IO.FileMode.Open))
                            {
                                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                                _para2DBarcode = (Pro2DBarcode.Config.VisionPara.VisionPara2DBarcode)bf.Deserialize(fs);
                            }
                            Pro2DBarcode.Manager.ConfigManager.Instance.CfgVisionPara.Para2DBarcode = _para2DBarcode;
                        }
                        catch (System.Exception ex)
                        {
                            txt1 = isChs ? "加载程式参数失败!\r\n" : "Load routine parameter failed !\r\n";
                            caption = isChs ? "错误信息" : "Error Message";
                            System.Windows.Forms.MessageBox.Show(txt1 + ex.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        txt1 = isChs ? "加载程式参数失败!\r\n上次程式文件不存在!" : "Load routine parameter failed !\r\n No last routine file exists !";
                        caption = isChs ? "错误信息" : "Error Message";
                        System.Windows.Forms.MessageBox.Show(txt1, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void UpdateGridControlForResult(ProCommon.Communal.Language lan)
        {
            bool isChs = (lan == ProCommon.Communal.Language.Chinese);           
            this.colNumber.Caption = isChs?"编号":"No.";
            this.colBarcodeString.Caption = isChs ? "字符" : "Decode";
            this.colElapseTime.Caption = isChs ? "耗时" : "Elapse";
        }
    }
}