using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pro2DBarcode.UI
{
    public partial class UI_LogIn : ProCommon.UserDefForm.FrmLogIn
    {
        protected internal Pro2DBarcode.Config.ConfigAccount _cfgAccount;

        #region 覆写的成员

        protected override void UpdateControl()
        {
            base.UpdateControl();

            if (_cfgAccount != null
                && _cfgAccount.UserBList.Count > 0)
            {
                this.cmbeUserList.Properties.Items.Clear();

                for (int i = 0; i < _cfgAccount.UserBList.Count; i++)
                {
                    this.cmbeUserList.Properties.Items.Add(_cfgAccount.UserBList[i].Name);
                }

                this.cmbeUserList.EditValue = _cfgAccount.UserBList[0].Name;
                this.cmbeUserList.SelectedIndex = 0;
                _currentUser = _cfgAccount.UserList[0];
                this.cmbeUserList.SelectedIndexChanged += cmbeUser_SelectedIndexChanged;
            }
        }     

        #endregion

        private UI_LogIn(ProCommon.Communal.Language lan) :base(lan)
        {
            InitializeComponent();
        }

        protected internal UI_LogIn(ProCommon.Communal.Language lan,Pro2DBarcode.Config.ConfigAccount cfgUser):this(lan)
        {
            _cfgAccount = cfgUser;
        }

        protected override void TransmitAccountInfor(ProCommon.Communal.Account us)
        {
            if(us!=null)
            {
                if(((Pro2DBarcode.UI.FrmMain)this.Owner)!=null)
                {
                    ((Pro2DBarcode.UI.FrmMain)this.Owner).CurrentAccount.Authority = us.Authority;
                    ((Pro2DBarcode.UI.FrmMain)this.Owner).CurrentAccount.Name = us.Name;
                    ((Pro2DBarcode.UI.FrmMain)this.Owner).CurrentAccount.ID = us.ID;
                    ((Pro2DBarcode.UI.FrmMain)this.Owner).CurrentAccount.PassWord = us.PassWord;
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        protected virtual void cmbeUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbeUserList.SelectedIndex != -1)
            {
                _selectedIndex = this.cmbeUserList.SelectedIndex;
                _currentUser = _cfgAccount.UserBList[_selectedIndex];
            }
        }
    }
}
