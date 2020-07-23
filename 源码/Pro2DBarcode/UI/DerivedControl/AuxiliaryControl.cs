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
    public partial class AuxiliaryControl : DevExpress.XtraEditors.XtraUserControl
    {
        public AuxiliaryControl()
        {
            InitializeComponent();

            if (!DevExpress.Utils.Design.DesignTimeTools.IsDesignMode)
                LookAndFeel.ActiveLookAndFeel.StyleChanged += new EventHandler(ActiveLookAndFeel_StyleChanged);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DevExpress.Utils.Design.DesignTimeTools.IsDesignMode)
                LookAndFeelStyleChanged();
        }

        void ActiveLookAndFeel_StyleChanged(object sender, EventArgs e)
        {
            LookAndFeelStyleChanged();
        }

        protected virtual void LookAndFeelStyleChanged() { }
    }
}
