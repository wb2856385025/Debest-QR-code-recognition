namespace Pro2DBarcode.UI.DerivedControl
{
    partial class AlarmLogModule
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
            this.grdcAlarmLog = new DevExpress.XtraGrid.GridControl();
            this.grdvAlarmLog = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdcAlarmLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvAlarmLog)).BeginInit();
            this.SuspendLayout();
            // 
            // grdcAlarmLog
            // 
            this.grdcAlarmLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdcAlarmLog.Location = new System.Drawing.Point(0, 0);
            this.grdcAlarmLog.MainView = this.grdvAlarmLog;
            this.grdcAlarmLog.Name = "grdcAlarmLog";
            this.grdcAlarmLog.Size = new System.Drawing.Size(1086, 687);
            this.grdcAlarmLog.TabIndex = 0;
            this.grdcAlarmLog.Tag = "GRDC_ALARMLOG";
            this.grdcAlarmLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdvAlarmLog});
            // 
            // grdvAlarmLog
            // 
            this.grdvAlarmLog.GridControl = this.grdcAlarmLog;
            this.grdvAlarmLog.Name = "grdvAlarmLog";
            this.grdvAlarmLog.OptionsView.ShowGroupPanel = false;
            this.grdvAlarmLog.Tag = "GRDV_ALARMLOG";
            // 
            // AlarmLogModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.Controls.Add(this.grdcAlarmLog);
            this.Name = "AlarmLogModule";
            this.Size = new System.Drawing.Size(1086, 687);
            ((System.ComponentModel.ISupportInitialize)(this.grdcAlarmLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvAlarmLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected internal DevExpress.XtraGrid.GridControl grdcAlarmLog;
        private DevExpress.XtraGrid.Views.Grid.GridView grdvAlarmLog;
    }
}
