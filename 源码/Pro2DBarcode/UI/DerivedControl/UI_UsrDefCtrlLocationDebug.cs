using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pro2DBarcode.UI.DerivedControl
{
    public partial class UI_UsrDefCtrlLocationDebug : ProCommon.UserDefControl.UsrCtrlLocationDebug
    {
        public UI_UsrDefCtrlLocationDebug()
        {
            InitializeComponent();
        }

        #region 可能被覆写的函数

        /// <summary>
        /// '匹配分数'控件'Enter'键按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void TlstrptxtbMatchScore_MouseEnter(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripTextBox tlstrptxtb = sender as System.Windows.Forms.ToolStripTextBox;
            if(tlstrptxtb!=null)
            {
                //1-判断文本框内的值是否有效数值
                //2-数值有效,判断匹配模型是否为空
                //3-匹配模型非空,设置匹配模型的匹配分数
                //4-匹配模型执行一次查找模板并更新匹配结果
            }
        }


        /// <summary>
        /// '曝光时间'控件'Enter'键按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void TlstrptxtbExposureTime_MouseEnter(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripTextBox tlstrptxtb = sender as System.Windows.Forms.ToolStripTextBox;
            if (tlstrptxtb != null)
            {
                //1-判断文本框内的值是否有效数值
                //2-数值有效,判断相机设备是否为空
                //3-相机设备非空,设置相机的曝光时间
                if (true)
                {
                    if (DeviceCamera != null)
                    {
                        float fvalue;
                        float.TryParse(tlstrptxtb.Text, out fvalue);
                        DeviceCamera.CamHandleListNew[Camera.ID].SetExposureTime(fvalue);
                    }
                }
            }
        }

        /// <summary>
        /// '旋转中心'控件单击事件
        /// [注:计算旋转中心]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void TlstrpbtnRotateCenter_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripButton tlstrpbtn = sender as System.Windows.Forms.ToolStripButton;
            if(tlstrpbtn!=null)
            {
                //1-判断计算旋转中心所需数据是否有效
                //2-数据有效，计算旋转中心
            }
        }

        /// <summary>
        /// '创建模板'控件单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void TlstrpbtnMatchModel_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripButton tlstrpbtn = sender as System.Windows.Forms.ToolStripButton;
            if (tlstrpbtn != null)
            {
                //1-实例化建模板匹配模型工具,显示模板匹配模型工具
                //2-更新模板匹配模型参数
                ProVision.MatchModel.FrmMatchModel frmMatchModel = new ProVision.MatchModel.FrmMatchModel();
                frmMatchModel.ShowDialog(this);

                //3-获取模板匹配模型参数
            }
        }

        /// <summary>
        /// '九点标定'控件单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void TlstrpmitmCalibrate9Point_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripMenuItem tlstrpmi = sender as System.Windows.Forms.ToolStripMenuItem;
            if (tlstrpmi != null)
            {
                //1-实例化基于九点标定的标定工具,显示标定工具
                //2-创建或更新标定参数
            }
        }

        /// <summary>
        /// '标定板标定'控件单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void TlstrpmitmCalibratePlate_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripMenuItem tlstrpmi = sender as System.Windows.Forms.ToolStripMenuItem;
            if(tlstrpmi!=null)
            {
                //1-实例化基于标定板标定的标定工具,显示标定工具
                //2-创建或更新标定参数
            }
        }

        /// <summary>
        /// '相机设置'控件单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void TlstrpbtnSetCamera_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripButton tlstrpbtn = sender as System.Windows.Forms.ToolStripButton;
            if (tlstrpbtn != null)
            {
                //1-判断相机设备是否为空
                //2-相机设备非空,打开设置相机参数窗口
            }
        }

        /// <summary>
        /// '连续采集'控件单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void TlstrpbtnAcquireContinue_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripButton tlstrpbtn = sender as System.Windows.Forms.ToolStripButton;
            if (tlstrpbtn != null)
            {
                //1-判断相机设备是否为空
                //2-相机设备非空,设置相机采集模式为连续采集
                if(DeviceCamera!=null
                    && Camera!=null)
                {
                    DeviceCamera.CamHandleListNew[Camera.ID].SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.Continue, 1);
                }
            }
        }

        /// <summary>
        /// '单张采集'控件单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void TlstrpbtnAquireOnce_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripButton tlstrpbtn = sender as System.Windows.Forms.ToolStripButton;
            if (tlstrpbtn != null)
            {
                //1-判断相机设备是否为空
                //2-相机设备非空,设置相机采集模式为触发采集(软)
                //3-执行软触发一次
                if (DeviceCamera != null
                    && Camera != null)
                {
                    DeviceCamera.CamHandleListNew[Camera.ID].SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.SoftTrigger, 1);
                    System.Threading.Thread.Sleep(50);
                    DeviceCamera.CamHandleListNew[Camera.ID].SoftTriggerOnce();
                }
            }
        }

        #endregion

        /// <summary>
        /// 相机设备
        /// </summary>
        public Device.Device_Camera DeviceCamera { set; get; }



    }
}
