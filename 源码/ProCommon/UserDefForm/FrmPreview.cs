using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;


namespace ProCommon.UserDefForm
{
    public partial class FrmPreview : DevExpress.XtraEditors.XtraForm
    {
        protected internal  BackgroundWorker _bkgrdwker;
        protected internal HalconDotNet.HTuple _hvWndHandle;

        public FrmPreview()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 预览图片
        /// </summary>
        /// <param name="imgPath"></param>
        protected internal virtual void PreviewImage(string imgPath)
        {
            try
            {
                string[] strArr = imgPath.Split('\\');
                string fileName = strArr[strArr.Length - 1];
                this.HtmlText = "预览图片["+ fileName + "]";
                pePreview.Image = System.Drawing.Image.FromFile(imgPath);
            }
            catch(Exception ex)
            {
                this.HtmlText = "预览图片[异常]";
                System.Windows.Forms.MessageBox.Show("预览失败:\\n"+ex.Message, "错误信息", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
           
        }

        /// <summary>
        /// 实时视频
        /// </summary>
        protected internal virtual void PreviewVideo()
        {
            try
            {

            }
            catch (Exception ex)
            {
                this.HtmlText = "预览视频[异常]";
                System.Windows.Forms.MessageBox.Show("预览失败:\\n" + ex.Message, "错误信息", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected internal virtual void InitAcquisition()
        {
            if(_hvWndHandle==null)
            {
                HalconDotNet.HOperatorSet.OpenWindow(0, 0, this.pePreview.Width, this.pePreview.Height, this.pePreview.Handle, "visible", "", out _hvWndHandle);
            }

            HalconDotNet.HOperatorSet.ClearWindow(_hvWndHandle);

            _bkgrdwker = new BackgroundWorker();
            _bkgrdwker.WorkerSupportsCancellation = true;
            _bkgrdwker.WorkerReportsProgress = true;

            _bkgrdwker.ProgressChanged += Bkgrdwker_ProgressChanged;
            _bkgrdwker.RunWorkerCompleted += Bkgrdwker_RunWorkerCompleted;
            _bkgrdwker.DoWork += Bkgrdwker_DoWork;

            _bkgrdwker.RunWorkerAsync();
        }


        /// <summary>
        /// 后台工作:处理函数(实时采集图像)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void Bkgrdwker_DoWork(object sender, DoWorkEventArgs e)
        {
           
        }


        /// <summary>
        /// 后台完成:处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void Bkgrdwker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }


        /// <summary>
        /// 后台进度更新:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void Bkgrdwker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           
        }
    }
}