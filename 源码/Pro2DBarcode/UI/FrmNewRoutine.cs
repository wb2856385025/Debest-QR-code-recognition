using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Pro2DBarcode.UI
{
    public partial class FrmNewRoutine : DevExpress.XtraEditors.XtraForm
    {
        public bool IsConfirmed;
        public string NewRoutineName; 

        public ProCommon.Communal.Language LanguageVersion { protected set; get; }
        private FrmNewRoutine()
        {
            InitializeComponent();
        }

        public FrmNewRoutine(ProCommon.Communal.Language lan):this()
        {
            LanguageVersion = lan;
            this.Load += FrmNewRoutine_Load;
        }

        protected internal virtual void FrmNewRoutine_Load(object sender, EventArgs e)
        {
            Init();
        }

        protected internal void Init()
        {
            InitFieldAndProperty();
            InitControl();
        }

        protected internal virtual void InitFieldAndProperty()
        {
            IsConfirmed = false;
            NewRoutineName = null;
        }

        protected internal virtual void InitControl()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);
            string str = this.Tag.ToString();           
            this.Text = isChs ? Properties.Resources.ResourceManager.GetString("chs_"+str) : Properties.Resources.ResourceManager.GetString("en_" + str);
            str = this.lblPromotion.Tag.ToString();
            this.lblPromotion.Text = isChs ? Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str);
            UpdateSimpleButton(this.sbtnConfirm, LanguageVersion);
            UpdateSimpleButton(this.sbtnCancel, LanguageVersion);
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

        protected internal virtual void Sbtn_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if(sbtn!=null
                && sbtn.Tag!=null)
            {
                bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);
                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_CONFIRM":
                        {
                            if(!string.IsNullOrEmpty(this.txteNewRoutineName.Text))
                            {
                                NewRoutineName = this.txteNewRoutineName.Text;
                                IsConfirmed = true;
                                this.Close();
                            }
                            else
                            {
                                IsConfirmed = false;
                                string txt = isChs ? "输入新程式的名称为空!" : "Empty name for new routine !";
                                string caption = isChs ? "警示信息" : "Warning Message";                              
                                ProCommon.UserDefForm.FrmMsgBox.Show(txt, caption, 
                                    ProCommon.UserDefForm.MyButtons.OK, ProCommon.UserDefForm.MyIcon.Warning, isChs);
                            }                            
                        }
                        break;
                    case "SBTN_CANCEL":
                        {
                            IsConfirmed = false;
                            this.Close();
                        }
                        break;
                    default:break;
                }
            }
        }
    }
}