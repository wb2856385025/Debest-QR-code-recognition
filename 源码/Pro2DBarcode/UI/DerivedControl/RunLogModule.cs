using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pro2DBarcode.UI.DerivedControl
{
    public partial class RunLogModule : Pro2DBarcode.UI.DerivedControl.BaseModule
    {
        private RunLogModule()
        {
            InitializeComponent();
            Init();
        }

        public RunLogModule(ProCommon.Communal.Language lan, int viewNum) : this()
        {
            LanguageVersion = lan;
        }

        private void Init()
        {
            //1-设置表控件的数据源

            //2-设置表控件行号标识控件的宽度
            this.grdvRunLog.IndicatorWidth = 40;

            //3-注册显示行号事件
            this.grdvRunLog.CustomDrawRowIndicator += GrdvAlarmLog_CustomDrawRowIndicator;
        }

        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdvAlarmLog_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        protected internal override void InitModule(DevExpress.Utils.Menu.IDXMenuManager manager, object data)
        {
            base.InitModule(manager, data);
            DevExpress.XtraNavBar.NavBarGroup grp = data as DevExpress.XtraNavBar.NavBarGroup;
            if (grp != null)
                this.NavBarGroup = grp;
        }

        protected internal override void ShowModule(bool isFirstShow)
        {
            base.ShowModule(isFirstShow);
            if (isFirstShow)
                this.grdcRunLog.ForceInitialize();
        }

        /// <summary>
        /// 导航控件子控件
        /// </summary>
        protected internal DevExpress.XtraNavBar.NavBarGroup NavBarGroup
        {
            private set;
            get;
        }

        /// <summary>
        /// 数据表控件
        /// </summary>
        protected internal override DevExpress.XtraGrid.GridControl GridCtrl
        {
            get
            {
                return this.grdcRunLog;
            }
        }

        /// <summary>
        /// 数据表项改变事件
        /// [注:此处焦点于首行]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal override void ListChanged(object sender, ListChangedEventArgs e)
        {
            this.grdvRunLog.FocusedRowHandle = 0;
        }
    }
}
