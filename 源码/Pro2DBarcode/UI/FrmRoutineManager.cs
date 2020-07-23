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
    public partial class FrmRoutineManager : DevExpress.XtraEditors.XtraForm
    {
        private string _routineBaseDirectory;          
        private Config.ConfigSystem _cfgSystem;

        public string RoutineName, RoutineDirectorySelected; //程式名称,程式目录路径    
        public bool IsNewCommand, IsEditCommand, IsLoadCommand, IsDeleteCommand, IsClearCommand, IsExitCommand;
        public ProCommon.Communal.Language LanguageVersion { protected set; get; }

        /// <summary>
        /// 文件夹全路径和文件夹名
        /// [注意:文件夹全路径与文件夹名一一对应,文件夹名对应程式名，方便用于显示]
        /// </summary>
        private System.Collections.Generic.SortedList<string,string> _routineNamesAndDirectories;

        private FrmRoutineManager()
        {
            InitializeComponent();
            this.Load += FrmRoutineManager_Load;
        }

        public FrmRoutineManager(ProCommon.Communal.Language lan,Pro2DBarcode.Config.ConfigSystem cfgSys):this()
        {
            LanguageVersion = lan;
            _cfgSystem = cfgSys;
        }

        private void FrmRoutineManager_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            InitFieldAndProperty();
            InitControls();
        }

        private void InitFieldAndProperty()
        {
            if(_cfgSystem!=null)
            {
                _routineBaseDirectory =_cfgSystem.RoutineDirectory;
            }
            RoutineName = null;
            RoutineDirectorySelected = null;
            IsNewCommand = false;
            IsEditCommand = false;
            IsLoadCommand = false;
            IsDeleteCommand = false;
            IsClearCommand = false;
            IsExitCommand = false;
            _routineNamesAndDirectories = new SortedList<string, string>();
        }

        private void InitControls()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);
            string str = this.Tag.ToString();
            this.Text = isChs ? Pro2DBarcode.Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str);
            UpdateSimpleButton(this.sbtnNew, LanguageVersion);
            UpdateSimpleButton(this.sbtnEdit, LanguageVersion);
            UpdateSimpleButton(this.sbtnLoad, LanguageVersion);
            UpdateSimpleButton(this.sbtnDelete, LanguageVersion);
            UpdateSimpleButton(this.sbtnClear, LanguageVersion);
            UpdateSimpleButton(this.sbtnExit, LanguageVersion);
            UpdateSimpleButton(this.sbtnSearch, LanguageVersion);
            UpdateLabelControl(this.lblSerachByName, LanguageVersion, Pro2DBarcode.Properties.Resources.ResourceManager);
            LoadLocalRoutines();
            UpdateListBoxForRoutine();
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

        private void LoadLocalRoutines()
        {
            if (System.IO.Directory.Exists(_routineBaseDirectory))
            {
                string[] directories = System.IO.Directory.GetDirectories(_routineBaseDirectory);
                if (directories != null
                    && directories.Length > 0)
                {
                    int count = directories.Length;
                    string routineName;
                    for (int i = 0; i < count; i++)
                    {
                        routineName = System.IO.Path.GetFileNameWithoutExtension(directories[i]);
                        _routineNamesAndDirectories.Add(routineName, directories[i]);
                    }
                }
            }
        }

        private void UpdateListBoxForRoutine()
        {
            this.lstbRoutine.Items.Clear();
            string[] rtNames=null;
            if(_routineNamesAndDirectories!=null
                && _routineNamesAndDirectories.Count>0)
            {
                rtNames=_routineNamesAndDirectories.Keys.ToArray();
            }

            if(rtNames!=null
                && rtNames.Length>0)
            {
                int cnt = rtNames.Length;
                for(int i=0;i<cnt;i++)
                    this.lstbRoutine.Items.Add(rtNames[i]);
            }          
        }

        private void Sbtn_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if (sbtn != null
                && sbtn.Tag != null)
            {
                IsNewCommand = false;
                IsEditCommand = false;
                IsLoadCommand = false;
                IsDeleteCommand = false;
                IsClearCommand = false;
                IsExitCommand = false;
                bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);

                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_NEWROUTINE":
                        CreateNewRoutine();
                        this.Close();
                        break;
                    case "SBTN_EDITROUTINE":
                        {
                            //当前程式更新为选中程式，然后进入编辑状态
                            if(this.lstbRoutine.SelectedItem!=null)
                            {
                                RoutineName = this.lstbRoutine.SelectedItem.ToString();
                                RoutineDirectorySelected = _routineNamesAndDirectories[RoutineName];                            
                                IsEditCommand = true;                              
                                this.Close();
                            }
                        }
                        break;
                    case "SBTN_LOADROUTINE":
                        {
                            //当前程式更新为选中程式,加载程式参数
                            if (this.lstbRoutine.SelectedItem != null)
                            {
                                RoutineName = this.lstbRoutine.SelectedItem.ToString();
                                RoutineDirectorySelected = _routineNamesAndDirectories[RoutineName];
                                IsLoadCommand = true;                             
                                this.Close();
                            }
                        }
                        break;
                    case "SBTN_DELETEROUTINE":
                        {
                            //当前程式更新为空,且选中程式对应的文件(或文件夹)被删除
                            if (this.lstbRoutine.SelectedItem != null)
                            {
                                RoutineName = this.lstbRoutine.SelectedItem.ToString();
                                RoutineDirectorySelected = _routineNamesAndDirectories[RoutineName];
                                if (System.IO.Directory.Exists(RoutineDirectorySelected))
                                {
                                    try
                                    {
                                        System.IO.Directory.Delete(RoutineDirectorySelected, true);                                      
                                        _routineNamesAndDirectories.Remove(RoutineName);
                                        UpdateListBoxForRoutine();
                                        IsDeleteCommand = true;
                                    }
                                    catch { }
                                }                               
                            }
                        }
                        break;
                    case "SBTN_CLEARROUTINE":
                        ClearRoutines();
                        break;
                    case "SBTN_EXIT":
                        {
                            //退出当前管理对话框                          
                            string txt = isChs ? "是否退出程式管理?" : "Cancel the dialog of routine manager ?";
                            string caption = isChs ? "询问信息" : "Question Message";
                            if (MessageBox.Show(txt, caption, MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.OK)
                            {
                                this.Close();
                            }
                        }
                        break;
                    case "SBTN_LOOKUP":
                        {
                            //获取搜索框提供的程式名称,非空程式名时在已有程式列表中搜索同名程式
                            //弹窗提示搜索结果,搜索到匹配名称的程式时，关闭提示窗后再在列表匹配名称处高亮显示(鼠标焦点)
                        }
                        break;
                    default:break;
                }
            }
        }       

        /// <summary>
        /// 创建新程式
        /// </summary>
        private void CreateNewRoutine()
        {
            FrmNewRoutine frm = new FrmNewRoutine(LanguageVersion);
            frm.ShowDialog(this);
            if(frm.IsConfirmed)
            {
                RoutineName = frm.NewRoutineName;
                RoutineDirectorySelected = _routineBaseDirectory + "\\"+RoutineName;
                bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);
                if (System.IO.Directory.Exists(RoutineDirectorySelected))
                {
                    string txt = isChs ? "系统已经存在相同名称项目!\r\n是否覆盖[" : "Exist a same name project!\r\n Would it cover [";
                    string caption = isChs ? "询问信息" : "Question Message";

                    if (System.Windows.Forms.MessageBox.Show(txt + RoutineName+"]?",
                        caption, MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.OK)
                    {
                        System.IO.Directory.Delete(RoutineDirectorySelected,true);
                    }else { return; }
                }

                try
                {
                    System.IO.Directory.CreateDirectory(RoutineDirectorySelected); //程式名称命名的文件夹
                    System.IO.Directory.CreateDirectory(RoutineDirectorySelected + "\\Image_All"); //程式名称命名文件夹保存全部图像文件夹
                    System.IO.Directory.CreateDirectory(RoutineDirectorySelected + "\\Image_OK");//程式名称命名文件夹下保存OK图像的文件夹
                    System.IO.Directory.CreateDirectory(RoutineDirectorySelected + "\\Image_NG");//程式名称命名文件夹下保存NG图像的文件夹
                    _routineNamesAndDirectories.Add(RoutineName, RoutineDirectorySelected);
                    UpdateListBoxForRoutine();
                    IsNewCommand = true;
                }
                catch(Exception ex)
                {
                    string txt1 = isChs ? "创建程式[" : "Create routine [";
                    string txt2 = isChs ? "]失败!\r\n异常描述:\r\n" : "] failed !\r\n Error description:\r\n";
                    string caption = isChs ? "错误信息" : "Error Message";                    
                    System.Windows.Forms.MessageBox.Show(txt1 + RoutineName+ txt2 + ex.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    
                }                            
            }
        }

        /// <summary>
        /// 清空程式
        /// </summary>
        private void ClearRoutines()
        {
            if (System.IO.Directory.Exists(_routineBaseDirectory))
            {
                bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);

                string[] rtNames = null;
                if (_routineNamesAndDirectories != null
                    && _routineNamesAndDirectories.Count > 0)
                {
                    rtNames = _routineNamesAndDirectories.Keys.ToArray();
                }

                if (rtNames != null
                    && rtNames.Length > 0)
                {
                    int cnt = rtNames.Length;
                    string routineName, routineDirectory;

                    for (int i = 0; i < cnt; i++)
                    {
                        routineName = rtNames[i];
                        routineDirectory = _routineNamesAndDirectories[routineName];

                        try
                        {
                            System.IO.Directory.Delete(routineDirectory, true);
                            _routineNamesAndDirectories.Remove(routineName);
                        }
                        catch (Exception ex)
                        {
                            string txt = isChs ? "删除程式文件夹异常!异常描述:\r\n" : "An error occured while deleting file !\r\n Error description:\r\n";
                            string caption = isChs ? "错误信息" : "Error Message";
                            System.Windows.Forms.MessageBox.Show(txt + ex.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        finally { }
                    }

                    UpdateListBoxForRoutine();
                    IsClearCommand = true;
                }              
            }
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
    }
}