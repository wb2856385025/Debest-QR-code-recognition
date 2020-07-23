using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Pro2DBarcode.UI.DerivedControl
{
    public partial class ShooterDetailViewer : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 语言版本
        /// </summary>
        protected internal ProCommon.Communal.Language LanguageVersion { private set; get; }

        private ShooterDetailViewer()
        {
            InitializeComponent();
            InitFieldAndProperty();
        }

        /// <summary>
        /// 构造函数
        /// [注:根据语言版本更新控件显示文本]
        /// </summary>
        /// <param name="lan"></param>
        protected internal ShooterDetailViewer(ProCommon.Communal.Language lan) : this()
        {
            LanguageVersion = lan;
            InitControls();
        }


        private void InitFieldAndProperty()
        {
            LanguageVersion = ProCommon.Communal.Language.Chinese;
        }

        private void InitControls()
        {

        }
    }
}
