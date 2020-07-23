using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace ProCommon.UserDefForm
{
    public partial class FrmAccountManager : DevExpress.XtraEditors.XtraForm
    {
        protected internal ProCommon.Communal.Account _currentAccount, _selectedAccount; //登录的账户，选择的账户
        protected internal int _accountSelectedIndex;
        protected internal bool _isAccountListEventRegistered; //账户列表控件选择项索引值改变事件是否注册过回调函数标记
        protected internal ProCommon.Communal.AccountOperation _accountOperation; //账户操作选择:新增,删除,修改

        /// <summary>
        /// 软件语言版本
        /// </summary>
        public ProCommon.Communal.Language LanguageVersion { protected set; get; }


        #region 可能覆写的成员

        /// <summary>
        /// 初始化ComboBoxEdit账户列表控件
        /// </summary>
        protected internal virtual void InitComboBoxEditAccount()
        {

        }

        /// <summary>
        /// 账户索引值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void CmbeAccountList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected internal virtual void Sbtn_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if (sbtn != null
                && sbtn.Tag != null)
            {
                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_CONFIRMA":
                        AdminConfirmClick(sbtn);
                        break;
                    case "SBTN_CONFIRMG":
                        GeneralConfirmClick(sbtn);
                        break;
                    case "SBTN_CANCEL":
                        CancelClick(sbtn);
                        break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// 管理权限确认单击事件
        /// </summary>
        /// <param name="sbtn"></param>
        protected internal virtual void AdminConfirmClick(DevExpress.XtraEditors.SimpleButton sbtn)
        {

        }

        /// <summary>
        /// 一般权限确认单击事件
        /// </summary>
        /// <param name="sbtn"></param>
        protected internal virtual void GeneralConfirmClick(DevExpress.XtraEditors.SimpleButton sbtn)
        {

        }

        /// <summary>
        /// 取消单击事件
        /// </summary>
        /// <param name="sbtn"></param>
        protected internal virtual void CancelClick(DevExpress.XtraEditors.SimpleButton sbtn)
        {

        }

        /// <summary>
        /// 检测是否有重名账户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected internal virtual bool CheckExistSameAccount(string name)
        {
            return false;
        }

        /// <summary>
        /// 检测新账户的索引
        /// </summary>
        /// <returns></returns>
        protected internal virtual int CheckNewAccountIndex()
        {
            return 0;
        }

        protected internal virtual void FrmAccountManager_Load(object sender, EventArgs e)
        {
            InitFieldAndProperty();
            UpdateControl();
        }

        #endregion

        private FrmAccountManager()
        {
            InitializeComponent();
        }

        protected internal FrmAccountManager(ProCommon.Communal.Language lan,Communal.Account us) : this()
        {
            _currentAccount = us;
            _selectedAccount = us;
            LanguageVersion = lan;
            this.Load += FrmAccountManager_Load;
        }

        /// <summary>
        /// 初始化字段
        /// </summary>
        protected internal virtual void InitFieldAndProperty()
        {

        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        protected internal virtual void UpdateControl()
        {
            UpdateXtraTabControl();
            UpdateGroupControl();
            UpdateLabelControl();
            InitComboBoxEditAccount();
            UpdateComboBoxEditAuthority();
            UpdateSimpleButton();
            UpdateRadioButton();
        }

        /// <summary>
        /// 初始化XtraTabPage控件
        /// </summary>
        private void UpdateXtraTabControl()
        {
            for (int i = 0; i < this.xtbcAccountManager.TabPages.Count; i++)
            {
                UpdateXtraTabPage(this.xtbcAccountManager.TabPages[i], LanguageVersion,ProCommon.Properties.Resources.ResourceManager);
            }
        }

        protected internal void UpdateXtraTabPage(DevExpress.XtraTab.XtraTabPage xtbp,ProCommon.Communal.Language lan,System.Resources.ResourceManager resourceManager)
        {
            if(xtbp!=null
                && xtbp.Tag!=null)
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
        /// 初始化GroupControl控件
        /// </summary>
        private void UpdateGroupControl()
        {
            UpdateGroupControl(this.grpcOperationOption, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateGroupControl(this.grpcAccountsDetail, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateGroupControl(this.grpcAccountDetail, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
        }

        protected internal void UpdateGroupControl(DevExpress.XtraEditors.GroupControl grpc,ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (grpc != null
               && grpc.Tag != null)
            {
                if (resourceManager != null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = grpc.Tag.ToString();
                    grpc.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                }
            }
        }

        /// <summary>
        /// 初始化LabelControl控件
        /// </summary>
        private void UpdateLabelControl()
        {
            UpdateLabelControl(this.lblAccountList, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblAccountNameA, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblAccountAuthorityA, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblAccountPassWordA, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblAccountNameG, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblAccountAuthorityG, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblAccountPassWordG, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
        }

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

        /// <summary>
        /// 初始化ComboBoxEdit控件
        /// </summary>
        private void UpdateComboBoxEditAuthority()
        {
            bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);
            string str = isChs ? "无权" : "None";

            //按枚举定义值的升序进行添加
            this.cmbeUserAuthority.Properties.Items.Clear();          
            this.cmbeUserAuthority.Properties.Items.Add(str);
            str = isChs ? "初级" : "Junior";
            this.cmbeUserAuthority.Properties.Items.Add(str);
            str = isChs ? "高级" : "Senior";
            this.cmbeUserAuthority.Properties.Items.Add(str);
            str = isChs ? "管理" : "Administrator";
            this.cmbeUserAuthority.Properties.Items.Add(str);
        }

        /// <summary>
        /// 初始化SimpleButton控件
        /// </summary>
        private void UpdateSimpleButton()
        {
            UpdateSimpleButton(this.sbtnConfirmA, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnCancelA, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);

            UpdateSimpleButton(this.sbtnConfirmG, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnCancelG, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
        }

        /// <summary>
        /// 更新SimpleButton控件
        /// </summary>
        /// <param name="sbtn"></param>
        /// <param name="lan"></param>
        protected internal void UpdateSimpleButton(DevExpress.XtraEditors.SimpleButton sbtn,ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if(sbtn!=null
                && sbtn.Tag!=null)
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

        /// <summary>
        /// 初始化RadioButton控件
        /// </summary>
        private void UpdateRadioButton()
        {
            UpdateRadioButton(this.rbtnAdd, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            this.rbtnAdd.Checked = false;

            UpdateRadioButton(this.rbtnDelete, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            this.rbtnDelete.Checked = false;

            UpdateRadioButton(this.rbtnModify, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            this.rbtnModify.Checked = true;

            UpdateControlStatus(ProCommon.Communal.AccountOperation.Modify);
        }

        protected internal void UpdateRadioButton(System.Windows.Forms.RadioButton rdbtn, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (rdbtn != null
               && rdbtn.Tag != null)
            {
                if (resourceManager != null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = rdbtn.Tag.ToString();
                    rdbtn.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                    rdbtn.CheckedChanged += Rbtn_CheckedChanged;
                }
            }
        }
        private void UpdateControlStatus(ProCommon.Communal.AccountOperation uo)
        {
            switch (uo)
            {
                case ProCommon.Communal.AccountOperation.Add:
                    {
                        this.txteUserName.ReadOnly = false;
                        this.cmbeUserAuthority.ReadOnly = false;
                        this.txteUserPsWd.ReadOnly = false;
                    }
                    break;
                case ProCommon.Communal.AccountOperation.Delete:
                    {
                        this.txteUserName.ReadOnly = true;
                        this.cmbeUserAuthority.ReadOnly = true;
                        this.txteUserPsWd.ReadOnly = true;
                    }
                    break;
                case ProCommon.Communal.AccountOperation.Modify:
                    {
                        this.txteUserName.ReadOnly = true;
                        this.cmbeUserAuthority.ReadOnly = (_currentAccount.Name == _selectedAccount.Name) ? false : true;
                        this.txteUserPsWd.ReadOnly = (_currentAccount.Name == _selectedAccount.Name) ? false : true;
                    }
                    break;
                case ProCommon.Communal.AccountOperation.NONE:
                default:
                    break;
            }

            this.txteUserAuthorityG.ReadOnly = true;
            this.txteUserNameG.ReadOnly = true;
        }
        protected internal virtual void Rbtn_CheckedChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.RadioButton rbtn = sender as System.Windows.Forms.RadioButton;

            if (rbtn!=null)
            {
                switch(rbtn.Tag.ToString())
                {
                    case "RBTN_ADD":
                        {
                            if (rbtn.Checked)
                                _accountOperation = ProCommon.Communal.AccountOperation.Add;
                        }
                        break;
                    case "RBTN_DELETE":
                        {
                            if (rbtn.Checked)
                                _accountOperation = ProCommon.Communal.AccountOperation.Delete;
                        }
                        break;
                    case "RBTN_MODIFY":                                              
                    default:
                        {
                            if (rbtn.Checked)
                                _accountOperation = ProCommon.Communal.AccountOperation.Modify;
                        }
                        break;
                }
                UpdateControlStatus(_accountOperation);
            }
        }      
        protected internal virtual void SwitchPage(ProCommon.Communal.Account us)
        {
            switch(us.Authority)
            {
                case ProCommon.Communal.AccountAuthority.Administrator:
                    {
                        this.xtbpAdministrator.PageVisible = true;
                        this.xtbpGeneral.PageVisible = false;
                    }
                    break;
                default:
                    {
                        this.xtbpAdministrator.PageVisible = false;
                        this.xtbpGeneral.PageVisible = true;
                    }
                    break;
            }
        }

        #region 辅助方法

        /// <summary>
        /// 账户权限转为换字符串
        /// </summary>
        /// <param name="au"></param>
        /// <returns></returns>
        protected internal string AuthorityToString(ProCommon.Communal.AccountAuthority au)
        {
            string s = "无权";
            switch (au)
            {
                case ProCommon.Communal.AccountAuthority.Administrator:
                    s = "管理";
                    break;
                case ProCommon.Communal.AccountAuthority.Junior:
                    s = "初级";
                    break;
                case ProCommon.Communal.AccountAuthority.Senior:
                    s = "高级";
                    break;
                default:
                    break;
            }
            return s;
        }

        /// <summary>
        /// 字符串转换为账户权限
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        protected internal ProCommon.Communal.AccountAuthority StringToAuthority(string s)
        {
            ProCommon.Communal.AccountAuthority a = ProCommon.Communal.AccountAuthority.None;
            if (s != null)
            {
                switch (s)
                {
                    case "初级":
                        a = ProCommon.Communal.AccountAuthority.Junior;
                        break;
                    case "高级":
                        a = ProCommon.Communal.AccountAuthority.Senior;
                        break;
                    case "管理":
                        a = ProCommon.Communal.AccountAuthority.Administrator;
                        break;
                    case "无权":
                    default:
                        break;
                }
            }
            return a;
        }

        protected internal virtual int AuthorityToInt(ProCommon.Communal.AccountAuthority au)
        {
            int i = 0;
            switch (au)
            {
                case ProCommon.Communal.AccountAuthority.Administrator:
                    i = 3;
                    break;
                case ProCommon.Communal.AccountAuthority.Junior:
                    i = 1;
                    break;
                case ProCommon.Communal.AccountAuthority.Senior:
                    i = 2;
                    break;
                default:
                    break;
            }
            return i;
        }
        protected internal virtual ProCommon.Communal.AccountAuthority IntToAuthority(int i)
        {
            ProCommon.Communal.AccountAuthority a = ProCommon.Communal.AccountAuthority.None;
            switch (i)
            {
                case 1:
                    a = ProCommon.Communal.AccountAuthority.Junior;
                    break;
                case 2:
                    a = ProCommon.Communal.AccountAuthority.Senior;
                    break;
                case 3:
                    a = ProCommon.Communal.AccountAuthority.Administrator;
                    break;
                default:
                    break;
            }
            return a;
        }

        #endregion
    }
}