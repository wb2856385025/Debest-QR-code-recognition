namespace Pro2DBarcode.UI.DerivedControl
{
    partial class RunLogModule
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
            this.grdcRunLog = new DevExpress.XtraGrid.GridControl();
            this.grdvRunLog = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdcRunLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvRunLog)).BeginInit();
            this.SuspendLayout();
            // 
            // grdcRunLog
            // 
            this.grdcRunLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdcRunLog.Location = new System.Drawing.Point(0, 0);
            this.grdcRunLog.MainView = this.grdvRunLog;
            this.grdcRunLog.Name = "grdcRunLog";
            this.grdcRunLog.Size = new System.Drawing.Size(1023, 495);
            this.grdcRunLog.TabIndex = 0;
            this.grdcRunLog.Tag = "GRDC_RUNLOG";
            this.grdcRunLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdvRunLog});
            // 
            // grdvRunLog
            // 
            this.grdvRunLog.GridControl = this.grdcRunLog;
            this.grdvRunLog.Name = "grdvRunLog";
            this.grdvRunLog.OptionsView.ShowGroupPanel = false;
            this.grdvRunLog.Tag = "GRDV_RUNLOG";
            // 
            // RunLogModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.Controls.Add(this.grdcRunLog);
            this.Name = "RunLogModule";
            ((System.ComponentModel.ISupportInitialize)(this.grdcRunLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvRunLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Views.Grid.GridView grdvRunLog;
        protected internal DevExpress.XtraGrid.GridControl grdcRunLog;
    }
}
