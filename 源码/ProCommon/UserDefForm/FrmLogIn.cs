using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;

namespace ProCommon.UserDefForm
{
    public partial class FrmLogIn : DevExpress.XtraEditors.XtraForm
    {
        protected internal ProCommon.Communal.Account _currentUser; //选择的用户      
        protected internal int _selectedIndex = -1;      

        /// <summary>
        /// 软件语言版本s
        /// </summary>
        public ProCommon.Communal.Language LanguageVersion { private set; get; }

        #region 必须覆写的成员 

        /// <summary>
        /// 向调用者传递用户信息
        /// </summary>
        /// <param name="us"></param>
        protected internal virtual void TransmitAccountInfor(ProCommon.Communal.Account us)
        {

        }
      
        #endregion

        private FrmLogIn()
        {
            InitializeComponent();
        }

        protected internal FrmLogIn(ProCommon.Communal.Language lan):this()
        {
            LanguageVersion = lan;         
            this.Load += FrmLogIn_Load;
        }

        /// <summary>
        /// 初始化字段
        /// </summary>
        protected internal virtual void InitFieldAndProperty()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmbeUserList.SelectedIndex = -1;
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        protected internal virtual void UpdateControl()
        {
            string str = this.Tag.ToString();
            bool isChs = LanguageVersion == ProCommon.Communal.Language.Chinese;
            this.HtmlText = isChs ? ProCommon.Properties.Resources.ResourceManager.GetString("chs_"+str): ProCommon.Properties.Resources.ResourceManager.GetString("en_" + str);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            UpdateGroupControl();
            UpdateLabelControl();
            UpdateSimpleButton();
        }

        /// <summary>
        /// 更新GroupControl控件
        /// </summary>
        private void UpdateGroupControl()
        {
            UpdateGroupControl(this.grpcAccountAndPassWord, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateGroupControl(this.grpcOperationPromotion, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
        }

        protected internal void UpdateGroupControl(DevExpress.XtraEditors.GroupControl grpc, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
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
        /// 更新LabelControl控件
        /// </summary>
        private void UpdateLabelControl()
        {
            UpdateLabelControl(this.lblAccount,LanguageVersion,ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblPassWord, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblErrorPromotion, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
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

        private void UpdateSimpleButton()
        {
            UpdateSimpleButton(this.sbtnLogIn, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnCancel, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
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

        private void Sbtn_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if(sbtn!=null)
            {
                bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);
                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_LOGIN":
                        {
                            if (this.cmbeUserList.SelectedIndex != -1)
                            {
                                string psw = ProCommon.Communal.DESEncrypt.Encrypt(this.txtePassWord.Text);
                                if (psw.Length != _currentUser.PassWord.Length)
                                {
                                    this.lblErrorPromotion.Appearance.BackColor = Color.Red;
                                    this.lblErrorPromotion.Text = isChs? " 提示:密码错误!":"Promotion:Wrong PassWord !";
                                }
                                else
                                {
                                    if (psw == _currentUser.PassWord)
                                    {
                                        this.lblErrorPromotion.Appearance.BackColor = Color.Green;
                                        this.lblErrorPromotion.Text = isChs ? " 提示:登录成功!":"Promotion:Login Successfully !";
                                        this.DialogResult = System.Windows.Forms.DialogResult.OK;                                      
                                        TransmitAccountInfor(_currentUser);
                                        this.cmbeUserList.Properties.Items.Clear();
                                        this.txtePassWord.EditValue = null;
                                        this.Close();
                                    }
                                    else
                                    {
                                        this.lblErrorPromotion.Appearance.BackColor = Color.Red;
                                        this.lblErrorPromotion.Text = isChs ? " 提示:密码错误!" : "Promotion:Wrong PassWord !";                                      
                                    }
                                }
                            }
                            else
                            {
                                this.lblErrorPromotion.Appearance.BackColor = Color.Yellow;
                                this.lblErrorPromotion.Text = isChs ? " 提示:未选择账户!":"Promotion:No Account Selected !";
                            }
                        }
                        break;
                    case "SBTN_CANCEL":
                        this.Close();
                        break;
                }
            }           
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void FrmLogIn_Load(object sender, EventArgs e)
        {
            InitFieldAndProperty();
            UpdateControl();
        }
    }
}