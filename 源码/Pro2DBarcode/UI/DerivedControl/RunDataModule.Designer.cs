namespace Pro2DBarcode.UI.DerivedControl
{
    partial class RunDataModule
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
            this.grdcRunData = new DevExpress.XtraGrid.GridControl();
            this.grdvRunData = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdcRunData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvRunData)).BeginInit();
            this.SuspendLayout();
            // 
            // grdcRunData
            // 
            this.grdcRunData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdcRunData.Location = new System.Drawing.Point(0, 0);
            this.grdcRunData.MainView = this.grdvRunData;
            this.grdcRunData.Name = "grdcRunData";
            this.grdcRunData.Size = new System.Drawing.Size(1081, 672);
            this.grdcRunData.TabIndex = 0;
            this.grdcRunData.Tag = "GRDC_RUNDATA";
            this.grdcRunData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdvRunData});
            // 
            // grdvRunData
            // 
            this.grdvRunData.GridControl = this.grdcRunData;
            this.grdvRunData.Name = "grdvRunData";
            this.grdvRunData.OptionsView.ShowGroupPanel = false;
            this.grdvRunData.Tag = "GRDV_RUNDATA";
            // 
            // RunDataModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.Controls.Add(this.grdcRunData);
            this.Name = "RunDataModule";
            this.Size = new System.Drawing.Size(1081, 672);
            ((System.ComponentModel.ISupportInitialize)(this.grdcRunData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvRunData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected internal DevExpress.XtraGrid.GridControl grdcRunData;
        private DevExpress.XtraGrid.Views.Grid.GridView grdvRunData;
    }
}
