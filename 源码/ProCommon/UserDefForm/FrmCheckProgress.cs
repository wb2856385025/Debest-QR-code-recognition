using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

namespace ProCommon.UserDefForm
{
    public partial class FrmCheckProgress : DevExpress.XtraSplashScreen.SplashScreen
    {
        protected internal int _dotCount = 0;
        protected internal System.Windows.Forms.Timer _timer;
        protected internal System.ComponentModel.BackgroundWorker _backgrdWorker;       

        #region 必须覆写的成员

        protected internal virtual void _backgrdWorker_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        protected internal virtual void _backgrdWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        protected internal virtual void _backgrdWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }



        #endregion

        public FrmCheckProgress()
        {
            InitializeComponent();
            Init();
        }

        protected internal virtual void Init()
        {
            InitField();
        }

        protected internal virtual void InitField()
        {
            this.mqpbCtrlProcess.Properties.ShowTitle = true; 
            _backgrdWorker = new BackgroundWorker();
            _backgrdWorker.WorkerSupportsCancellation = true;
            _backgrdWorker.WorkerReportsProgress = true;
            _backgrdWorker.ProgressChanged += _backgrdWorker_ProgressChanged;
            _backgrdWorker.RunWorkerCompleted += _backgrdWorker_RunWorkerCompleted;
            _backgrdWorker.DoWork += _backgrdWorker_DoWork;

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 200;
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Enabled = true;
            _timer.Start();
        }      

        protected internal virtual void _timer_Tick(object sender, EventArgs e)
        {
            if (_dotCount++ > 3) _dotCount = 0;
            this.lblCtrlPrompt.Text = string.Format("{0}{1}", "Starting", GetDot(_dotCount));
            this.lblCtrlRight.Text = string.Format("{0}{1}", "Copyright © 2015-", GetYear());
        }

        /// <summary>
        /// 获取当前年份
        /// </summary>
        /// <returns></returns>
        protected internal virtual int GetYear()
        {
            int rt = System.DateTime.Now.Year;
            return (rt < 2010 ? 2010 : rt);
        }

        /// <summary>
        /// 获取字符串(符号:点)
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        protected internal virtual string GetDot(int count)
        {
            string rt = string.Empty;
            for (int i = 0; i < count; i++)
            {
                rt += ".";
            }
            return rt;
        }


    }
}