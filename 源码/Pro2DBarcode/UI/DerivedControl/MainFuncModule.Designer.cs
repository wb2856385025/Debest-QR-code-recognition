namespace Pro2DBarcode.UI.DerivedControl
{
    partial class MainFuncModule
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pcViewRoot = new DevExpress.XtraEditors.PanelControl();
            this.tblltpViewRoot = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pcViewRoot)).BeginInit();
            this.pcViewRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // pcViewRoot
            // 
            this.pcViewRoot.Controls.Add(this.tblltpViewRoot);
            this.pcViewRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcViewRoot.Location = new System.Drawing.Point(0, 0);
            this.pcViewRoot.Name = "pcViewRoot";
            this.pcViewRoot.Size = new System.Drawing.Size(1075, 681);
            this.pcViewRoot.TabIndex = 0;
            this.pcViewRoot.Tag = "PC_VIEWROOT";
            // 
            // tblltpViewRoot
            // 
            this.tblltpViewRoot.ColumnCount = 2;
            this.tblltpViewRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltpViewRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltpViewRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltpViewRoot.Location = new System.Drawing.Point(2, 2);
            this.tblltpViewRoot.Name = "tblltpViewRoot";
            this.tblltpViewRoot.RowCount = 2;
            this.tblltpViewRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltpViewRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltpViewRoot.Size = new System.Drawing.Size(1071, 677);
            this.tblltpViewRoot.TabIndex = 0;
            this.tblltpViewRoot.Tag = "TBLLTP_VIEWROOT";
            // 
            // MainFuncModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.Controls.Add(this.pcViewRoot);
            this.Name = "MainFuncModule";
            this.Size = new System.Drawing.Size(1075, 681);
            ((System.ComponentModel.ISupportInitialize)(this.pcViewRoot)).EndInit();
            this.pcViewRoot.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected internal DevExpress.XtraEditors.PanelControl pcViewRoot;
        private System.Windows.Forms.TableLayoutPanel tblltpViewRoot;
    }
}
