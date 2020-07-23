using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ModuleAuxiliary
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       Pro2DBarcode.UI.DerivedControl
 * File      Name：       ModuleAuxiliary
 * Creating  Time：       2/7/2020 3:32:41 AM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace Pro2DBarcode.UI.DerivedControl
{
    /// <summary>
    /// 模块
    /// [注:基类]
    /// </summary>
    public class BaseModule : AuxiliaryControl
    {
        /// <summary>
        /// 语言版本
        /// </summary>
        protected internal ProCommon.Communal.Language LanguageVersion { protected set; get; }

        /// <summary>
        /// 表格控件
        /// </summary>
        protected internal virtual DevExpress.XtraGrid.GridControl GridCtrl { get; set; }

        /// <summary>
        /// 打印组件
        /// </summary>
        protected internal virtual DevExpress.XtraPrinting.IPrintable PrintableComponent { get; set; }

        /// <summary>
        /// 导出组件
        /// </summary>
        protected internal virtual DevExpress.XtraPrinting.IPrintable ExportComponent { get; set; }

        private string _moduleName;

        /// <summary>
        /// 模块名称
        /// </summary>
        protected internal virtual string ModuleName { get { return _moduleName; } }

        public BaseModule()
        {
            _moduleName = string.Empty;
        }

        /// <summary>
        /// 当前类所属窗体(一般定义为UI界面主窗体)
        /// </summary>
        internal UI.FrmMain OwnerForm { get { return this.FindForm() as UI.FrmMain; } }

        /// <summary>
        /// 显示模块
        /// </summary>
        /// <param name="isFirstShow"></param>
        protected internal virtual void ShowModule(bool isFirstShow)
        {
            if (OwnerForm == null) return;
            ShowInfo();
        }

        /// <summary>
        /// 显示模块名称
        /// </summary>
        protected internal virtual void ShowInfo()
        {
            if (OwnerForm == null) return;
            else
            {
                OwnerForm.ShowInfo();
            }

        }

        /// <summary>
        /// 隐藏模块
        /// </summary>
        protected internal virtual void HideModule()
        {

        }

        /// <summary>
        /// 模块初始化
        /// [注:为模块内的控件添加管理器]
        /// </summary>
        protected internal virtual void InitModule(DevExpress.Utils.Menu.IDXMenuManager manager, object data)
        {
            SetMenuManager(this.Controls, manager);

            if (GridCtrl != null
                && GridCtrl.MainView is DevExpress.XtraGrid.Views.Base.ColumnView)
                ((DevExpress.XtraGrid.Views.Base.ColumnView)GridCtrl.MainView).ColumnFilterChanged += GridCtrl_ColumnFilterChanged;
        }

        /// <summary>
        /// 表格控件的列筛选条件改变的回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridCtrl_ColumnFilterChanged(object sender, EventArgs e)
        {
            ShowInfo(sender as DevExpress.XtraGrid.Views.Base.ColumnView);
        }

        /// <summary>
        /// 为控件添加管理器
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="manager"></param>
        private void SetMenuManager(ControlCollection controls, DevExpress.Utils.Menu.IDXMenuManager manager)
        {
            foreach (System.Windows.Forms.Control ctrl in controls)
            {
                //当前控件满足属于某种类型的控件,则为其添加管理器

                //当前控件的子控件,添加管理器
                SetMenuManager(ctrl.Controls, manager);
            }
        }

        protected internal virtual void ShowInfo(DevExpress.XtraGrid.Views.Base.ColumnView colView)
        {
            if (OwnerForm == null) return;
        }

        protected internal virtual void ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {

        }
    }

    /// <summary>
    /// 模块对象的包装类
    /// [存储于导航栏控件的Tag]
    /// </summary>
    public class ModuleWrappedObj
    {
        /// <summary>
        /// 语言版本
        /// </summary>
        protected internal ProCommon.Communal.Language LanguageVersion { set; get; }

        /// <summary>
        /// 视图数量
        /// </summary>
        protected internal int ViewNumber { set; get; }

        /// <summary>
        /// 视图索引
        /// </summary>
        protected internal int ViewIndex { set; get; }

        private string _moduleName = string.Empty;
        /// <summary>
        /// 模块对象的名称
        /// </summary>
        public string ModuleName { get { return _moduleName; } }

        private string _moduleTag = string.Empty;
        /// <summary>
        /// 模块对象的Tag标记
        /// [注:用于控制相应的RibbonPage显隐]
        /// </summary>
        public string ModuleTag { get { return _moduleTag; } }

        /// <summary>
        /// 模块对象的类型
        /// </summary>
        private System.Type _modelType;
        public System.Type ModuleType { get { return _modelType; } }

        /// <summary>
        /// 模块对象
        /// </summary>
        private BaseModule _module;
        public BaseModule Module { get { return _module; } set { _module = value; } }

        protected ModuleWrappedObj()
        {
            _moduleName = string.Empty;
            _modelType = null;
            _module = null;
        }

        public ModuleWrappedObj(string modNam,string modTag, System.Type modType) : this()
        {
            _moduleName = modNam;
            _moduleTag = modTag;
            _modelType = modType;
        }
    }

    /// <summary>
    /// 导航窗格
    /// </summary>
    public class NavPanePanel : AuxiliaryControl
    {

        DevExpress.XtraNavBar.NavPaneState _state = DevExpress.XtraNavBar.NavPaneState.Collapsed;

        /// <summary>
        /// 导航窗格状态
        /// </summary>
        public DevExpress.XtraNavBar.NavPaneState State
        {
            get { return _state; }
            set
            {
                _state = value;
                RefreshBackColor();
            }
        }

        /// <summary>
        /// 方法:覆写界面外观改变事件回调函数
        /// </summary>
        protected override void LookAndFeelStyleChanged()
        {
            base.LookAndFeelStyleChanged();
            RefreshBackColor();
        }


        /// <summary>
        /// 刷新背景颜色
        /// </summary>
        private void RefreshBackColor()
        {
            this.BackColor = new DevExpress.Skins.SkinElementInfo(DevExpress.Skins.CommonSkins.GetSkin(this.LookAndFeel)[DevExpress.Skins.CommonSkins.SkinTextBorder]).Element.Border.Bottom;
            this.Padding = new System.Windows.Forms.Padding(BorderByState(_state), 1, 1, IsOfficeStyle ? BorderByState(_state) : 1);
        }

        /// <summary>
        /// 方法:获取边界的状态值
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private static int BorderByState(DevExpress.XtraNavBar.NavPaneState state)
        {
            return state == DevExpress.XtraNavBar.NavPaneState.Collapsed ? 0 : 1;
        }

        /// <summary>
        /// 属性:是否"OfficeStyle"
        /// </summary>
        private bool IsOfficeStyle
        {
            get
            {
                return this.LookAndFeel.ActiveSkinName.IndexOf("Office") > -1;
            }
        }
    }

    /// <summary>
    /// 模块导航器
    /// [注:管理各个模块]
    /// </summary>
    public class ModuleNavigator
    {
        DevExpress.XtraBars.Ribbon.RibbonControl _ribbon;
        DevExpress.XtraEditors.PanelControl _panel;
        private ModuleNavigator()
        {

        }

        public ModuleNavigator(DevExpress.XtraBars.Ribbon.RibbonControl rb, DevExpress.XtraEditors.PanelControl pn) : this()
        {
            _ribbon = rb;
            _panel = pn;
        }

        public void ChangeGroupAndShowRibbonPage(DevExpress.XtraNavBar.NavBarGroup grp, object moduleData)
        {
            bool allowVisible = true;
            ModuleWrappedObj mdlWrappedObj = moduleData as ModuleWrappedObj;
            if (mdlWrappedObj == null) return;

            //待显示的RibbonPage
            List<DevExpress.XtraBars.Ribbon.RibbonPage> deferredPagesToShow = new List<DevExpress.XtraBars.Ribbon.RibbonPage>();

            //Ribbon控件里的RibbonPage
            foreach (DevExpress.XtraBars.Ribbon.RibbonPage page in _ribbon.Pages)
            {
                //Tag内容非空的RibbonPage进行显隐处理,(对于不需要显隐的RibbonPage,其Tag置为Null)
                if (!string.IsNullOrEmpty(page.Tag.ToString()))
                {
                    //模块对象的ModuleName内容与Ribbon的Page的Tag内容一致,则该Page的显示标记为true
                    string tmpStr= mdlWrappedObj.ModuleTag;
                    int idx = tmpStr.LastIndexOf('_');
                    string moduleNamKey = tmpStr.Substring(idx+1,tmpStr.Length-idx-1);

                    tmpStr = page.Tag.ToString();
                    idx= tmpStr.LastIndexOf('_');

                    string pageTagKey = tmpStr.Substring(idx+1, tmpStr.Length -idx-1);
                    bool isPageVisible = moduleNamKey.Equals(pageTagKey);

                    //Page显示标记为True,且其当前显示状态为False,则将其添加到待显示Page列表
                    //注:第一次运行时,所有的RibbonPage的显示状态都为True,故而第一个NavBarGroup对应的RibbonPage显示,其他RibbonPage隐藏
                    //当然，受显隐控制的RibbonPage其Tag非空
                    if (isPageVisible != page.Visible
                        && isPageVisible)
                        deferredPagesToShow.Add(page);
                    else
                        page.Visible = isPageVisible; //Page的显示标记False,或者Page的显示标记与当前的显示状态一致，仅仅更新Page的显示标记
                }

                //RibbonPage显示状态为True,且允许可见标记为True,则RibbonControl的活动RibbonPage为当前RibbonPage,允许可见标记复位

                //注:第一次运行时,所有Tag非空的RibbonPage的显示状态都为True,故而第一个NavBarGroup对应的RibbonPage作为RibbonControl的活动RibbonPage,其他RibbonPage隐藏
                //非第一次运行时,所有Tag非空的RibbonPage的显示状态都为False,只有首个Tag为空的RibbonPage作为RibbonControl的活动RibbonPage
                if (page.Visible && allowVisible)
                {
                    _ribbon.SelectedPage = page;
                    allowVisible = false;
                }
            }

            bool isFirstShow = mdlWrappedObj.Module == null;
            if (isFirstShow)
            {
                //界面管理器的默认窗体为空
                if (DevExpress.XtraSplashScreen.SplashScreenManager.Default == null)
                    //显示等候窗体(CommonProcess.Form.FrmWait),效果为非淡入,淡出
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(_ribbon.FindForm(), typeof(ProCommon.UserDefForm.FrmWait), false, true);

                //获取指定参数类型的构造函数信息(这里需要传递两个参数:ProCommon.Communal.Language+int两个类型)
                System.Type[] types = new Type[] { typeof(ProCommon.Communal.Language),typeof(int)};
                System.Reflection.ConstructorInfo conStruInfo = mdlWrappedObj.ModuleType.GetConstructor(types);

                //调用构造函数信息创建实例
                if (conStruInfo != null)
                    mdlWrappedObj.Module = conStruInfo.Invoke(new object[] { mdlWrappedObj.LanguageVersion, mdlWrappedObj .ViewNumber}) as BaseModule;

                //界面管理器非空
                if (DevExpress.XtraSplashScreen.SplashScreenManager.Default != null)
                {
                    System.Windows.Forms.Form frm = moduleData as System.Windows.Forms.Form;

                    //非空窗体
                    if (frm != null)
                    {
                        if (DevExpress.XtraSplashScreen.SplashScreenManager.FormInPendingState) //是否有创建但未显示的窗体
                            DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
                        else
                            DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false, 500, frm); //SplashScreenManager中窗体A显示指定时间后,关闭窗体A;然后激活frm
                    }
                    else
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
                }
            }

            //待显示的Page,设置为可见
            foreach (DevExpress.XtraBars.Ribbon.RibbonPage page in deferredPagesToShow)
            {
                page.Visible = true;
            }

            //Tag非空的RibbonPage中首个可见的Page作为选择的Page
            foreach (DevExpress.XtraBars.Ribbon.RibbonPage page in _ribbon.Pages)
            {
                if (!string.IsNullOrEmpty(page.Tag.ToString())
                    && page.Visible)
                {
                    _ribbon.SelectedPage = page;
                    break;
                }
            }

            //模块非空
            if (mdlWrappedObj.Module != null)
            {
                //容器里有其他模块
                if (_panel.Controls.Count > 0)
                {
                    BaseModule currentModule = _panel.Controls[0] as BaseModule;
                    if (currentModule != null)
                        currentModule.HideModule();
                }

                _panel.Controls.Clear();
                _panel.Controls.Add(mdlWrappedObj.Module);
                mdlWrappedObj.Module.Dock = System.Windows.Forms.DockStyle.Fill;
                mdlWrappedObj.Module.ShowModule(isFirstShow);
            }
        }

      

        /// <summary>
        /// 当前模块
        /// </summary>
        public BaseModule CurrentModule
        {
            get
            {
                if (_panel.Controls.Count == 0) return null;
                return _panel.Controls[0] as BaseModule;
            }
        }
    }
}
