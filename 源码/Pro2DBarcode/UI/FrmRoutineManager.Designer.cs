namespace Pro2DBarcode.UI
{
    partial class FrmRoutineManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRoutineManager));
            this.lblSerachByName = new DevExpress.XtraEditors.LabelControl();
            this.txteRoutineName = new DevExpress.XtraEditors.TextEdit();
            this.sbtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnClear = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.lstbRoutine = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.txteRoutineName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstbRoutine)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSerachByName
            // 
            this.lblSerachByName.Appearance.Font = new System.Drawing.Font("DengXian", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerachByName.Appearance.Options.UseFont = true;
            this.lblSerachByName.Location = new System.Drawing.Point(34, 29);
            this.lblSerachByName.Name = "lblSerachByName";
            this.lblSerachByName.Size = new System.Drawing.Size(100, 19);
            this.lblSerachByName.TabIndex = 0;
            this.lblSerachByName.Tag = "LBLC_SEARCHBYNAMES";
            this.lblSerachByName.Text = "按名称搜索:";
            // 
            // txteRoutineName
            // 
            this.txteRoutineName.Location = new System.Drawing.Point(159, 26);
            this.txteRoutineName.Name = "txteRoutineName";
            this.txteRoutineName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txteRoutineName.Size = new System.Drawing.Size(288, 26);
            this.txteRoutineName.TabIndex = 1;
            // 
            // sbtnSearch
            // 
            this.sbtnSearch.Appearance.Font = new System.Drawing.Font("DengXian", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbtnSearch.Appearance.Options.UseFont = true;
            this.sbtnSearch.Location = new System.Drawing.Point(488, 17);
            this.sbtnSearch.Name = "sbtnSearch";
            this.sbtnSearch.Size = new System.Drawing.Size(96, 35);
            this.sbtnSearch.TabIndex = 2;
            this.sbtnSearch.Tag = "SBTN_SEARCH";
            this.sbtnSearch.Text = "搜索";
            // 
            // sbtnNew
            // 
            this.sbtnNew.Appearance.Font = new System.Drawing.Font("DengXian", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbtnNew.Appearance.Options.UseFont = true;
            this.sbtnNew.Location = new System.Drawing.Point(34, 418);
            this.sbtnNew.Name = "sbtnNew";
            this.sbtnNew.Size = new System.Drawing.Size(67, 36);
            this.sbtnNew.TabIndex = 4;
            this.sbtnNew.Tag = "SBTN_NEWROUTINE";
            this.sbtnNew.Text = "新建";
            // 
            // sbtnEdit
            // 
            this.sbtnEdit.Appearance.Font = new System.Drawing.Font("DengXian", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbtnEdit.Appearance.Options.UseFont = true;
            this.sbtnEdit.Location = new System.Drawing.Point(132, 418);
            this.sbtnEdit.Name = "sbtnEdit";
            this.sbtnEdit.Size = new System.Drawing.Size(67, 36);
            this.sbtnEdit.TabIndex = 4;
            this.sbtnEdit.Tag = "SBTN_EDITROUTINE";
            this.sbtnEdit.Text = "编辑";
            // 
            // sbtnLoad
            // 
            this.sbtnLoad.Appearance.Font = new System.Drawing.Font("DengXian", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbtnLoad.Appearance.Options.UseFont = true;
            this.sbtnLoad.Location = new System.Drawing.Point(230, 418);
            this.sbtnLoad.Name = "sbtnLoad";
            this.sbtnLoad.Size = new System.Drawing.Size(67, 36);
            this.sbtnLoad.TabIndex = 4;
            this.sbtnLoad.Tag = "SBTN_LOADROUTINE";
            this.sbtnLoad.Text = "加载";
            // 
            // sbtnDelete
            // 
            this.sbtnDelete.Appearance.Font = new System.Drawing.Font("DengXian", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbtnDelete.Appearance.Options.UseFont = true;
            this.sbtnDelete.Location = new System.Drawing.Point(328, 418);
            this.sbtnDelete.Name = "sbtnDelete";
            this.sbtnDelete.Size = new System.Drawing.Size(67, 36);
            this.sbtnDelete.TabIndex = 4;
            this.sbtnDelete.Tag = "SBTN_DELETEROUTINE";
            this.sbtnDelete.Text = "删除";
            // 
            // sbtnClear
            // 
            this.sbtnClear.Appearance.Font = new System.Drawing.Font("DengXian", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbtnClear.Appearance.Options.UseFont = true;
            this.sbtnClear.Location = new System.Drawing.Point(426, 418);
            this.sbtnClear.Name = "sbtnClear";
            this.sbtnClear.Size = new System.Drawing.Size(67, 36);
            this.sbtnClear.TabIndex = 4;
            this.sbtnClear.Tag = "SBTN_CLEARROUTINE";
            this.sbtnClear.Text = "清空";
            // 
            // sbtnExit
            // 
            this.sbtnExit.Appearance.Font = new System.Drawing.Font("DengXian", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbtnExit.Appearance.Options.UseFont = true;
            this.sbtnExit.Location = new System.Drawing.Point(524, 418);
            this.sbtnExit.Name = "sbtnExit";
            this.sbtnExit.Size = new System.Drawing.Size(67, 36);
            this.sbtnExit.TabIndex = 4;
            this.sbtnExit.Tag = "SBTN_EXIT";
            this.sbtnExit.Text = "退出";
            // 
            // lstbRoutine
            // 
            this.lstbRoutine.Location = new System.Drawing.Point(34, 54);
            this.lstbRoutine.Name = "lstbRoutine";
            this.lstbRoutine.Size = new System.Drawing.Size(557, 358);
            this.lstbRoutine.TabIndex = 5;
            // 
            // FrmRoutineManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 466);
            this.ControlBox = false;
            this.Controls.Add(this.lstbRoutine);
            this.Controls.Add(this.sbtnExit);
            this.Controls.Add(this.sbtnClear);
            this.Controls.Add(this.sbtnDelete);
            this.Controls.Add(this.sbtnLoad);
            this.Controls.Add(this.sbtnEdit);
            this.Controls.Add(this.sbtnNew);
            this.Controls.Add(this.sbtnSearch);
            this.Controls.Add(this.txteRoutineName);
            this.Controls.Add(this.lblSerachByName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRoutineManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "FRM_ROUTINEMANAGER";
            this.Text = "程式管理对话框";
            ((System.ComponentModel.ISupportInitialize)(this.txteRoutineName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstbRoutine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected internal DevExpress.XtraEditors.TextEdit txteRoutineName;
        protected internal DevExpress.XtraEditors.LabelControl lblSerachByName;
        protected internal DevExpress.XtraEditors.SimpleButton sbtnSearch;
        protected internal DevExpress.XtraEditors.SimpleButton sbtnNew;
        protected internal DevExpress.XtraEditors.SimpleButton sbtnEdit;
        protected internal DevExpress.XtraEditors.SimpleButton sbtnLoad;
        protected internal DevExpress.XtraEditors.SimpleButton sbtnDelete;
        protected internal DevExpress.XtraEditors.SimpleButton sbtnClear;
        protected internal DevExpress.XtraEditors.SimpleButton sbtnExit;
        protected internal DevExpress.XtraEditors.ListBoxControl lstbRoutine;
    }
}