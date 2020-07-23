using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pro2DBarcode.UI
{
    public partial class UI_CheckProgress : ProCommon.UserDefForm.FrmCheckProgress
    {
        public UI_CheckProgress():base()
        {
            InitializeComponent();
        }

        #region 必须覆写的成员

        protected override void _backgrdWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Pro2DBarcode.Manager.SystemManager.Instance.ChkCount = 1;
            _backgrdWorker.ReportProgress(Pro2DBarcode.Manager.SystemManager.Instance.ChkCount, "System check launch");
            System.Threading.Thread.Sleep(500);

            Pro2DBarcode.Manager.SystemManager.Instance.ChkCount = 2;
            _backgrdWorker.ReportProgress(Pro2DBarcode.Manager.SystemManager.Instance.ChkCount, "Check system config");
            System.Threading.Thread.Sleep(500);
            CheckSystem(_backgrdWorker);

            if (Pro2DBarcode.Manager.SystemManager.Instance.ChkOK)
            {
                Pro2DBarcode.Manager.ConfigManager.Instance.Load();
                Pro2DBarcode.Manager.SystemManager.Instance.InitExternal();               
                ((FrmMain)this.Owner).LanguageVersion = Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.LanguageVersion;
                ((FrmMain)this.Owner).ViewNumber = Pro2DBarcode.Manager.ConfigManager.Instance.CfgSystem.ViewNumber;

                System.Threading.Thread.Sleep(500);
                _backgrdWorker.ReportProgress(Pro2DBarcode.Manager.SystemManager.Instance.ChkCount + 1, "Check system config successfully");
                System.Threading.Thread.Sleep(500);
                _backgrdWorker.ReportProgress(Pro2DBarcode.Manager.SystemManager.Instance.ChkCount + 2, "System run allowed");
            }
            else
            {
                System.Threading.Thread.Sleep(1000);
                _backgrdWorker.ReportProgress(Pro2DBarcode.Manager.SystemManager.Instance.ChkCount + 1, "Check system config failed");

                System.Threading.Thread.Sleep(1000);
                _backgrdWorker.ReportProgress(Pro2DBarcode.Manager.SystemManager.Instance.ChkCount + 2, "System run forbbiden");
            }
        }

        protected override void _backgrdWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _backgrdWorker.Dispose();
            _backgrdWorker = null;

            this._timer.Stop();
            this._timer.Enabled = false;

            this.Close();
            this.Dispose();
        }

        protected override void _backgrdWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.mqpbCtrlProcess.Text = e.ProgressPercentage.ToString() + ":" + e.UserState.ToString();
        }

        #endregion

        /// <summary>
        /// 开始系统检测
        /// </summary>
        internal void StartCheck()
        {
            if (_backgrdWorker != null)
                _backgrdWorker.RunWorkerAsync();
        }

        /// <summary>
        /// 系统检测
        /// </summary>
        private void CheckSystem(System.ComponentModel.BackgroundWorker bkgrdWorker)
        {
            try
            {
                Pro2DBarcode.Manager.SystemManager.Instance.CheckDirectoryAndFiles(bkgrdWorker);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
