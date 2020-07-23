using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pro2DBarcode.UI
{
    public partial class UI_AccountManager : ProCommon.UserDefForm.FrmAccountManager
    {
        protected internal Pro2DBarcode.Config.ConfigAccount _cfgUser;

        protected internal UI_AccountManager(ProCommon.Communal.Language lan,ProCommon.Communal.Account curUser, Pro2DBarcode.Config.ConfigAccount cfgUser) :base(lan,curUser)
        {
            InitializeComponent();
            _cfgUser = cfgUser;
        }

        #region 覆写的函成员

        /// <summary>
        /// 初始化ComboBoxEdit账户列表控件
        /// </summary>
        protected override void InitComboBoxEditAccount()
        {
            if (_cfgUser != null
                && _cfgUser.UserList != null
              && _cfgUser.UserList.Count > 0)
            {
                this.cmbeUserList.Properties.Items.Clear();

                for (int i = 0; i < _cfgUser.UserList.Count; i++)
                {
                    this.cmbeUserList.Properties.Items.Add(_cfgUser.UserList[i].Name);
                    if (_cfgUser.UserList[i].Name == _currentAccount.Name)
                    {
                        _accountSelectedIndex = i;
                    }
                }

                if(_accountSelectedIndex>=0)
                {
                    this.cmbeUserList.EditValue = _cfgUser.UserList[_accountSelectedIndex].Name;
                    this.cmbeUserList.SelectedIndex = _accountSelectedIndex;
                    this.txteUserName.Text = _currentAccount.Name;
                    this.cmbeUserAuthority.EditValue = AuthorityToString(_currentAccount.Authority);
                    this.cmbeUserAuthority.SelectedIndex = AuthorityToInt(_currentAccount.Authority);
                    this.txteUserPsWd.Text = ProCommon.Communal.DESEncrypt.Decrypt(_currentAccount.PassWord);

                    this.txteUserNameG.Text = _currentAccount.Name;
                    this.txteUserAuthorityG.Text = AuthorityToString(_currentAccount.Authority);
                    this.txteUserPsWdG.Text = ProCommon.Communal.DESEncrypt.Decrypt(_currentAccount.PassWord);
                }               
            }
            if (!_isAccountListEventRegistered)
            {
                this.cmbeUserList.SelectedIndexChanged += CmbeAccountList_SelectedIndexChanged;
                _isAccountListEventRegistered = true;
            }
        }

        /// <summary>
        /// 账户索引值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void CmbeAccountList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbeUserList.SelectedIndex != -1)
            {
                _accountSelectedIndex = this.cmbeUserList.SelectedIndex;
                _selectedAccount = _cfgUser.UserList[_accountSelectedIndex];

                this.txteUserName.Text = _selectedAccount.Name;
                this.cmbeUserAuthority.SelectedItem = _selectedAccount.Authority;
                this.txteUserPsWd.Text = ProCommon.Communal.DESEncrypt.Decrypt(_selectedAccount.PassWord);
            }
        }
       
        /// <summary>
        ///管理权限确认单击事件
        /// </summary>
        /// <param name="sbtn"></param>
        protected override void AdminConfirmClick(DevExpress.XtraEditors.SimpleButton sbtn)
        {
            bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese) ? true : false;
            string txt = string.Empty;
            string caption =string.Empty;

            switch (_accountOperation)
            {
                case ProCommon.Communal.AccountOperation.Add:
                    {
                        txt = isChs ? "是否新增账户？" : "Would you like to add new account?";
                        caption = isChs ? "询问信息" : "Question Information";

                        if (MessageBox.Show(txt, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (!string.IsNullOrEmpty(this.txteUserName.Text.Trim()))
                            {
                                //是否有重名账户?
                                if (!CheckExistSameAccount(this.txteUserName.Text.Trim()))
                                {
                                    ProCommon.Communal.Account us = new ProCommon.Communal.Account(this.txteUserName.Text.Trim(), CheckNewAccountIndex());
                                    us.Authority = StringToAuthority(this.cmbeUserAuthority.SelectedItem.ToString());
                                    us.PassWord = ProCommon.Communal.DESEncrypt.Encrypt(this.txteUserPsWd.Text.Trim());
                                    _cfgUser.UserList.Add(us);
                                    Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigAccount>(_cfgUser, Pro2DBarcode.Manager.ConfigManager.ConfigDirectory + Pro2DBarcode.Manager.ConfigManager.ACCOUNT_CONFIG_FILE_NAME);
                                    InitComboBoxEditAccount();
                                }
                                else
                                {
                                    txt = isChs ? "已有同名账户！" : "There was already the same name account!";
                                    caption = isChs ? "警告信息" : "Warning Information";

                                    MessageBox.Show(txt, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                txt = isChs ? "账户为空！" : "An empty account!";
                                caption = isChs ? "警告信息" : "Warning Information";
                                MessageBox.Show(txt, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    break;
                case ProCommon.Communal.AccountOperation.Delete:
                    {
                        txt = isChs ? "是否删除账户？" : "Would you like to delete the selected account?";
                        caption = isChs ? "询问信息" : "Question Information";

                        if (MessageBox.Show(txt,caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //是否删除的是自身
                            if (_selectedAccount.Name != _currentAccount.Name)
                            {
                                _cfgUser.UserList.Delete(_selectedAccount);
                                Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigAccount>(_cfgUser, Pro2DBarcode.Manager.ConfigManager.ConfigDirectory + Pro2DBarcode.Manager.ConfigManager.ACCOUNT_CONFIG_FILE_NAME);
                                _accountSelectedIndex = 0;
                                InitComboBoxEditAccount();
                            }
                            else
                            {
                                txt = isChs ? "无法删除自身账户！" : "Can not delete the account which was logged in !";
                                caption = isChs ? "询问信息" : "Question Information";

                                MessageBox.Show(txt, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    break;
                case ProCommon.Communal.AccountOperation.Modify:
                    {
                        txt = isChs ? "是否修改账户？" : "Would you like to modify the selected account?";
                        caption = isChs ? "询问信息" : "Question Information";

                        if (MessageBox.Show(txt, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //是否修改的是自身
                            if (_selectedAccount.Name != _currentAccount.Name)
                            {
                                if (!string.IsNullOrEmpty(this.txteUserName.Text.Trim()))
                                {
                                    //是否有重名用户
                                    if (!CheckExistSameAccount(this.txteUserName.Text.Trim()))
                                    {
                                        //删除原来的用户
                                        _cfgUser.UserList.Delete(_selectedAccount);
                                        //修改后的用户
                                        _selectedAccount.Name = this.txteUserName.Text.Trim();
                                        _selectedAccount.Authority = StringToAuthority(this.cmbeUserAuthority.SelectedItem.ToString());
                                        _selectedAccount.PassWord = ProCommon.Communal.DESEncrypt.Encrypt(this.txteUserPsWd.Text.Trim());
                                        //新增修改后的用户
                                        _cfgUser.UserList.Add(_selectedAccount);
                                        Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigAccount>(_cfgUser, Pro2DBarcode.Manager.ConfigManager.ConfigDirectory + Pro2DBarcode.Manager.ConfigManager.ACCOUNT_CONFIG_FILE_NAME);
                                    }
                                    else
                                    {
                                        txt = isChs ? "已有重名账户！" : "There was already the same name account!";
                                        caption = isChs ? "询问信息" : "Question Information";

                                        MessageBox.Show(txt, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    txt = isChs ? "账户为空！" : "An empty account!";
                                    caption = isChs ? "警告信息" : "Warning Information";
                                    MessageBox.Show(txt, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(this.txteUserName.Text.Trim()))
                                {
                                    _currentAccount.Authority = StringToAuthority(this.cmbeUserAuthority.SelectedItem.ToString());
                                    _currentAccount.PassWord = ProCommon.Communal.DESEncrypt.Encrypt(this.txteUserPsWd.Text.Trim());
                                    _cfgUser.UserList[_currentAccount.ID].Authority = StringToAuthority(this.cmbeUserAuthority.SelectedItem.ToString());
                                    _cfgUser.UserList[_currentAccount.ID].PassWord = ProCommon.Communal.DESEncrypt.Encrypt(this.txteUserPsWd.Text.Trim());
                                    Pro2DBarcode.Config.ConfigAPIHandle.Save<Pro2DBarcode.Config.ConfigAccount>(_cfgUser, Pro2DBarcode.Manager.ConfigManager.ConfigDirectory + Pro2DBarcode.Manager.ConfigManager.ACCOUNT_CONFIG_FILE_NAME);
                                }
                                else
                                {
                                    txt = isChs ? "账户为空！" : "An empty account!";
                                    caption = isChs ? "警告信息" : "Warning Information";
                                    MessageBox.Show(txt, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 一般权限确认单击事件
        /// </summary>
        /// <param name="sbtn"></param>
        protected override void GeneralConfirmClick(DevExpress.XtraEditors.SimpleButton sbtn)
        {
            bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese) ? true : false;
            string txt = isChs ? "是否修改账户？" : "Would you like to modify the selected account?";
            string caption = isChs ? "询问信息" : "Question Information";

            if (MessageBox.Show(txt, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(!string.IsNullOrEmpty(this.txteUserPsWdG.Text.Trim()))
                    _cfgUser.UserList[_currentAccount.Name].PassWord = ProCommon.Communal.DESEncrypt.Encrypt(this.txteUserPsWdG.Text.Trim());
                else
                {
                    txt = isChs ? "密码不能为空！" : "An empty pass word!";
                    caption = isChs ? "警告信息" : "Warning Information";
                    MessageBox.Show(txt,caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        /// <summary>
        /// 取消单击事件
        /// </summary>
        /// <param name="sbtn"></param>
        protected override void CancelClick(DevExpress.XtraEditors.SimpleButton sbtn)
        {
            this.Close();
        }

        /// <summary>
        /// 检测是否有重名账户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected override bool CheckExistSameAccount(string name)
        {
            if (_cfgUser != null
                && _cfgUser.UserList != null)
            {
                for (int i = 0; i < _cfgUser.UserList.Count; i++)
                {
                    if (_cfgUser.UserList[i].Name == name)
                        return true;
                }
                return false;
            }
            else
            {
                return true; //用户列表为空，标记有重名用户
            }
        }

        /// <summary>
        /// 检测新账户的索引
        /// </summary>
        /// <returns></returns>
        protected override int CheckNewAccountIndex()
        {
            if (_cfgUser != null
                && _cfgUser.UserList != null)
            {
                for (int i = 0; i < _cfgUser.UserList.Count; i++)
                {
                    if (_cfgUser.UserList[i].Num != i)
                        return i;       //中间最早插入型索引(有某个用户被删除)
                }
                return _cfgUser.UserList.Count; //追加型索引
            }
            else
            {
                return 0; //用户列表为空，返回初始索引(需要创建用户列表)
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void FrmAccountManager_Load(object sender, EventArgs e)
        {
            base.FrmAccountManager_Load(sender,e);
            SwitchPage(_currentAccount);
        }

        #endregion

        protected override void InitFieldAndProperty()
        {
            base.InitFieldAndProperty();
            this.DialogResult = System.Windows.Forms.DialogResult.None;
            _accountSelectedIndex = -1;
            _isAccountListEventRegistered = false;
            _accountOperation = ProCommon.Communal.AccountOperation.Modify;
        }

        protected override void UpdateControl()
        {
            base.UpdateControl();
            bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese) ? true : false;
            string authority = isChs? "无权":"None";
            switch (this._currentAccount.Authority)
            {
                case ProCommon.Communal.AccountAuthority.Administrator:                   
                    authority = isChs ? "管理" : "Administrator";
                    break;
                case ProCommon.Communal.AccountAuthority.Junior:                  
                    authority = isChs ? "初级" : "Junior";
                    break;
                case ProCommon.Communal.AccountAuthority.Senior:                   
                    authority = isChs ? "高级" : "Senior";
                    break;
                default:
                    break;
            }

            this.HtmlText = (isChs ? ("当前账户:" + this._currentAccount.Name + "\\权限：") : ("CurrentAccount:" + this._currentAccount.Name + "\\Authority:")) + authority; 
        }
    }
}
