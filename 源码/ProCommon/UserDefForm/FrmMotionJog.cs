using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProCommon.UserDefForm
{
    public partial class FrmMotionJog : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 软件语言版本s
        /// </summary>
        public ProCommon.Communal.Language LanguageVersion { private set; get; }

        private ProCommon.Communal.AxisList _axisList;
        private ProCommon.Communal.InVarObjList _inVarList;
        private ProCommon.Communal.OutVarObjList _outVarList;

        private System.ComponentModel.BindingList<ProCommon.Communal.InVarObj> _inVarBList;
        private System.ComponentModel.BindingList<ProCommon.Communal.OutVarObj> _outVarBList;

        protected internal float _speed;
        protected int _jogMode;
        protected internal int _distanceLevel;
        protected internal float _distanceInternal;

        private FrmMotionJog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 运控调试窗口构造函数
        /// </summary>
        /// <param name="lan"></param>
        /// <param name="axesList">轴变量(注意:最多四个轴，且依列表索引作为描述坐标系的顺序轴)</param>
        /// <param name="inVarList"></param>
        /// <param name="outVarList"></param>
        public FrmMotionJog(ProCommon.Communal.Language lan,
            ProCommon.Communal.AxisList axesList, ProCommon.Communal.InVarObjList inVarList,
            ProCommon.Communal.OutVarObjList outVarList) :this()
        {
            LanguageVersion = lan;          
            _axisList = axesList;
            _inVarList = inVarList;
            _outVarList = outVarList;

            _inVarList[0].NewValue = false;
            _inVarList[1].NewValue = false;
            _inVarList[2].NewValue = false;

            _inVarBList = new BindingList<Communal.InVarObj>();
            for (int i=0;i<_inVarList.Count;i++)
                _inVarBList.Add(_inVarList[i]);

            this.inVarObjBindingSource.DataSource = _inVarBList;

            _outVarBList = new BindingList<Communal.OutVarObj>();
            for (int j = 0; j < _outVarList.Count; j++)
                _outVarBList.Add(_outVarList[j]);

            this.outVarObjBindingSource.DataSource = _outVarBList;
            this.Load += FrmMotionJog_Load;
        }


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal void FrmMotionJog_Load(object sender, EventArgs e)
        {
            InitFieldAndProperty();
            UpdateControl();
        }
      

        #region 可能覆写代码

        protected internal virtual void InitFieldAndProperty()
        {
            _speed = 5.0f;
            _distanceLevel = 0;
        }

        protected internal virtual void UpdateControl()
        {
            string str = this.Tag.ToString();
            bool isChs = LanguageVersion == ProCommon.Communal.Language.Chinese;
            this.HtmlText = isChs? ProCommon.Properties.Resources.ResourceManager.GetString("chs_" + str) : ProCommon.Properties.Resources.ResourceManager.GetString("en_" + str);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            UpdateAxes();

            UpdateGroupControl(this.grpcModeStep, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblJogStep, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);

            UpdateCheckEdit(this.chkeModeContinue, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeModeJog, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeSpeedLow, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeSpeedMedium, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateCheckEdit(this.chkeSpeedHigh, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            this.cmbeDistanceOption.SelectedIndexChanged += CmbeDistanceOption_SelectedIndexChanged;
        }

        private void CmbeDistanceOption_SelectedIndexChanged(object sender, EventArgs e)
        {            
            _distanceLevel = this.cmbeDistanceOption.SelectedIndex;
            _distanceInternal = Convert.ToSingle(this.cmbeDistanceOption.SelectedItem);//控制器内部定义的行程等级,注意此时点动模式必须为0
        }

        /// <summary>
        /// 设置指定输出变量端口的值
        /// </summary>
        /// <param name="outVarObj"></param>
        /// <param name="onOff"></param>
        protected internal virtual void MotionSetOutput(ProCommon.Communal.OutVarObj outVarObj, bool onOff)
        {
           
        }

        /// <summary>
        /// 指定轴回零运动
        /// </summary>
        /// <param name="axis"></param>
        protected internal virtual void MotionFindDatum(ProCommon.Communal.Axis axis)
        {

        }

        /// <summary>
        /// 指定轴停止运动
        /// </summary>
        /// <param name="axis"></param>
        protected internal virtual void MotionMoveCancel(ProCommon.Communal.Axis axis)
        {

        }

        /// <summary>
        /// 指定轴在指定方向移动设定距离
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="mdir"></param>
        /// <param name="jogMode">点动模式
        /// 0-内部定义行程等级,1-用户定义行程等级,2-连续运动</param>
        /// <param name="distanceLevel">内部定义行程等级</param>
        /// <param name="distanceExternal">用户定义行程等级,且以用户单位计量</param>
        /// <param name="speed">速度,以用户单位计量</param>
        protected internal virtual void MotionSingleJogMove(ProCommon.Communal.Axis axis,ProCommon.Communal.MoveDir mdir,int jogMode,int distanceLevel,float distanceExternal,float speed)
        {

        }


        /// <summary>
        /// 指定轴在指定方向上连续移动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="mdir"></param>
        /// <param name="jogMode"></param>
        /// <param name="distanceLevel"></param>
        /// <param name="speed"></param>
        protected internal virtual void MotionSingleContinueMove(ProCommon.Communal.Axis axis, ProCommon.Communal.MoveDir mdir, int jogMode,int distanceLevel, float speed)
        {

        }

        /// <summary>
        /// 速度选项切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal virtual void Chke_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit chke = sender as DevExpress.XtraEditors.CheckEdit;
            if (chke != null
                && chke.Tag != null
                && chke.Checked)
            {
                switch (chke.Tag.ToString())
                {
                    case "CHKE_SPEEDLOW":
                        _speed = 5.0f;
                        break;
                    case "CHKE_SPEEDMEDIUM":
                        _speed = 20.0f;
                        break;
                    case "CHKE_SPEEDHIGH":
                        _speed = 50.0f;
                        break;
                    case "CHKE_MODECONTINUE":
                        _jogMode = 2;
                        break;
                    case "CHKE_MODEJOG":
                        _jogMode = 0;
                        break;
                    default: break;
                }
            }
        }

        #endregion

        protected internal void UpdateGroupControl(DevExpress.XtraEditors.GroupControl grpc, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (grpc != null
               && grpc.Tag != null)
            {
                if (resourceManager != null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = grpc.Tag.ToString();
                    grpc.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                }
            }
        }

        protected internal void UpdateLabelControl(DevExpress.XtraEditors.LabelControl lblc, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (lblc != null
              && lblc.Tag != null)
            {
                if (resourceManager != null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = lblc.Tag.ToString();
                    lblc.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                }
            }
        }

        protected internal void UpdateCheckEdit(DevExpress.XtraEditors.CheckEdit chke, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (chke != null
             && chke.Tag != null)
            {
                if (resourceManager != null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = chke.Tag.ToString();
                    chke.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                    chke.CheckedChanged += Chke_CheckedChanged;
                }
            }
        }


        /// <summary>
        /// 更新轴控件并绑定
        /// </summary>
        private void UpdateAxes()
        {
            if (_axisList != null
                && _axisList.Count > 0)
            {
                this.sbtnAxisFwd00.Enabled = false;
                this.sbtnAxisRev00.Enabled = false;
                this.sbtnAxisFwd01.Enabled = false;
                this.sbtnAxisRev01.Enabled = false;
                this.sbtnAxisFwd02.Enabled = false;
                this.sbtnAxisRev02.Enabled = false;
                this.sbtnAxisFwd03.Enabled = false;
                this.sbtnAxisRev03.Enabled = false;
                this.cmbeAxies01.Enabled = false;
                this.cmbeAxies23.Enabled = false;
                this.sbtnAxisStop01.Enabled = false;
                this.sbtnAxisDatum01.Enabled = false;
                this.sbtnAxisStop23.Enabled = false;
                this.sbtnAxisDatum23.Enabled = false;


                for (int i = 0; i < _axisList.Count; i++)
                {
                    if (i == 0)
                    {
                        this.sbtnAxisFwd00.Text = _axisList[0].Name + "+";
                        this.sbtnAxisFwd00.MouseDown -= SbtnAxis_MouseDown;
                        this.sbtnAxisFwd00.MouseDown += SbtnAxis_MouseDown;
                        this.sbtnAxisFwd00.MouseUp -= SbtnAxis_MouseUp;
                        this.sbtnAxisFwd00.MouseUp += SbtnAxis_MouseUp;

                        this.sbtnAxisRev00.Text = _axisList[0].Name + "-";
                        this.sbtnAxisRev00.MouseDown -= SbtnAxis_MouseDown;
                        this.sbtnAxisRev00.MouseDown += SbtnAxis_MouseDown;
                        this.sbtnAxisRev00.MouseUp -= SbtnAxis_MouseUp;
                        this.sbtnAxisRev00.MouseUp += SbtnAxis_MouseUp;

                        this.sbtnAxisDatum01.Click -= SbtnAxisDatum_Click;
                        this.sbtnAxisDatum01.Click += SbtnAxisDatum_Click;
                        this.sbtnAxisStop01.Click -= SbtnAxisStop_Click;
                        this.sbtnAxisStop01.Click += SbtnAxisStop_Click;

                        this.sbtnAxisFwd00.Enabled = true;
                        this.sbtnAxisRev00.Enabled = true;

                        this.sbtnAxisStop01.Enabled = true;
                        this.sbtnAxisDatum01.Enabled = true;

                        this.cmbeAxies01.Enabled = true;
                        this.cmbeAxies01.Properties.Items.Clear();
                        this.cmbeAxies01.Properties.Items.Add(_axisList[0].Name);
                        this.cmbeAxies01.SelectedIndex = 0;
                        this.cmbeAxies01.SelectedText = _axisList[0].Name;


                        this.lblAxisName00.Text = _axisList[0].Name;
                        this.txteAxisPosition00.DataBindings.Clear();
                        this.txteAxisPosition00.DataBindings.Add(new Binding("EditValue", _axisList[0], "CurrentPos", true));
                        this.imgcmbeAlarm00.DataBindings.Clear();
                        this.imgcmbeAlarm00.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", _axisList[0], "StatusALM", true));
                        this.imgcmbeRevLimit00.DataBindings.Clear();
                        this.imgcmbeRevLimit00.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", _axisList[0], "StatusRev", true));
                        this.imgcmbeDtmLimit00.DataBindings.Clear();
                        this.imgcmbeDtmLimit00.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", _axisList[0], "StatusDatum", true));
                        this.imgcmbeFwdLimit00.DataBindings.Clear();
                        this.imgcmbeFwdLimit00.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", _axisList[0], "StatusFwd", true));
                    }
                    else if (i == 1)
                    {
                        this.sbtnAxisFwd01.Text = _axisList[1].Name + "+";
                        this.sbtnAxisFwd01.MouseDown -= SbtnAxis_MouseDown;
                        this.sbtnAxisFwd01.MouseDown += SbtnAxis_MouseDown;
                        this.sbtnAxisFwd01.MouseUp -= SbtnAxis_MouseUp;
                        this.sbtnAxisFwd01.MouseUp += SbtnAxis_MouseUp;


                        this.sbtnAxisRev01.Text = _axisList[1].Name + "-";
                        this.sbtnAxisRev01.MouseDown -= SbtnAxis_MouseDown;
                        this.sbtnAxisRev01.MouseDown += SbtnAxis_MouseDown;
                        this.sbtnAxisRev01.MouseUp -= SbtnAxis_MouseUp;
                        this.sbtnAxisRev01.MouseUp += SbtnAxis_MouseUp;

                        this.sbtnAxisFwd01.Enabled = true;
                        this.sbtnAxisRev01.Enabled = true;

                        this.cmbeAxies01.Enabled = true;
                        this.cmbeAxies01.Properties.Items.Clear();
                        this.cmbeAxies01.Properties.Items.Add(_axisList[0].Name);
                        this.cmbeAxies01.Properties.Items.Add(_axisList[1].Name);
                        this.cmbeAxies01.SelectedIndex = 0;
                        this.cmbeAxies01.SelectedText = _axisList[0].Name;

                        this.lblAxisName01.Text = _axisList[1].Name;
                        this.txteAxisPosition01.DataBindings.Clear();
                        this.txteAxisPosition01.DataBindings.Add(new Binding("EditValue", _axisList[1], "CurrentPos", true));
                        this.imgcmbeAlarm01.DataBindings.Clear();
                        this.imgcmbeAlarm01.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", _axisList[1], "StatusALM", true));
                        this.imgcmbeRevLimit01.DataBindings.Clear();
                        this.imgcmbeRevLimit01.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", _axisList[1], "StatusRev", true));
                        this.imgcmbeDtmLimit01.DataBindings.Clear();
                        this.imgcmbeDtmLimit01.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", _axisList[1], "StatusDatum", true));
                        this.imgcmbeFwdLimit01.DataBindings.Clear();
                        this.imgcmbeFwdLimit01.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", _axisList[1], "StatusFwd", true));
                    }
                    else if (i == 2)
                    {
                        this.sbtnAxisFwd02.Text = _axisList[2].Name + "+";
                        this.sbtnAxisFwd02.MouseDown -= SbtnAxis_MouseDown;
                        this.sbtnAxisFwd02.MouseDown += SbtnAxis_MouseDown;
                        this.sbtnAxisFwd02.MouseUp -= SbtnAxis_MouseUp;
                        this.sbtnAxisFwd02.MouseUp += SbtnAxis_MouseUp;

                        this.sbtnAxisRev02.Text = _axisList[2].Name + "-";
                        this.sbtnAxisRev02.MouseDown -= SbtnAxis_MouseDown;
                        this.sbtnAxisRev02.MouseDown += SbtnAxis_MouseDown;
                        this.sbtnAxisRev02.MouseUp -= SbtnAxis_MouseUp;
                        this.sbtnAxisRev02.MouseUp += SbtnAxis_MouseUp;

                        this.sbtnAxisDatum23.Click -= SbtnAxisDatum_Click;
                        this.sbtnAxisDatum23.Click += SbtnAxisDatum_Click;
                        this.sbtnAxisStop23.Click -= SbtnAxisStop_Click;
                        this.sbtnAxisStop23.Click += SbtnAxisStop_Click;

                        this.sbtnAxisFwd02.Enabled = true;
                        this.sbtnAxisRev02.Enabled = true;

                        this.sbtnAxisStop23.Enabled = true;
                        this.sbtnAxisDatum23.Enabled = true;

                        this.cmbeAxies23.Enabled = true;
                        this.cmbeAxies23.Properties.Items.Clear();
                        this.cmbeAxies23.Properties.Items.Add(_axisList[2].Name);
                        this.cmbeAxies23.SelectedIndex = 0;
                        this.cmbeAxies23.SelectedText = _axisList[2].Name;

                        this.lblAxisName02.Text = _axisList[2].Name;
                        this.txteAxisPosition02.DataBindings.Clear();
                        this.txteAxisPosition02.DataBindings.Add(new Binding("EditValue", _axisList[2], "CurrentPos", true));
                        this.imgcmbeAlarm02.DataBindings.Clear();
                        this.imgcmbeAlarm02.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", _axisList[2], "StatusALM", true));
                        this.imgcmbeRevLimit02.DataBindings.Clear();
                        this.imgcmbeRevLimit02.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", _axisList[2], "StatusRev", true));
                        this.imgcmbeDtmLimit02.DataBindings.Clear();
                        this.imgcmbeDtmLimit02.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", _axisList[2], "StatusDatum", true));
                        this.imgcmbeFwdLimit02.DataBindings.Clear();
                        this.imgcmbeFwdLimit02.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", _axisList[2], "StatusFwd", true));

                    }
                    else if (i == 3)
                    {
                        this.sbtnAxisFwd03.Text = _axisList[3].Name + "+";
                        this.sbtnAxisFwd03.MouseDown -= SbtnAxis_MouseDown;
                        this.sbtnAxisFwd03.MouseDown += SbtnAxis_MouseDown;
                        this.sbtnAxisFwd03.MouseUp -= SbtnAxis_MouseUp;
                        this.sbtnAxisFwd03.MouseUp += SbtnAxis_MouseUp;

                        this.sbtnAxisRev03.Text = _axisList[3].Name + "-";
                        this.sbtnAxisRev03.MouseDown -= SbtnAxis_MouseDown;
                        this.sbtnAxisRev03.MouseDown += SbtnAxis_MouseDown;
                        this.sbtnAxisRev03.MouseUp -= SbtnAxis_MouseUp;
                        this.sbtnAxisRev03.MouseUp += SbtnAxis_MouseUp;

                        this.sbtnAxisFwd03.Enabled = true;
                        this.sbtnAxisRev03.Enabled = true;

                        this.cmbeAxies23.Enabled = true;
                        this.cmbeAxies23.Properties.Items.Clear();
                        this.cmbeAxies23.Properties.Items.Add(_axisList[2].Name);
                        this.cmbeAxies23.Properties.Items.Add(_axisList[3].Name);
                        this.cmbeAxies23.SelectedIndex = 0;
                        this.cmbeAxies23.SelectedText = _axisList[2].Name;

                        this.lblAxisName03.Text = _axisList[3].Name;
                        this.txteAxisPosition03.DataBindings.Clear();
                        this.txteAxisPosition03.DataBindings.Add(new Binding("EditValue", _axisList[3], "CurrentPos", true));
                        this.imgcmbeAlarm03.DataBindings.Clear();
                        this.imgcmbeAlarm03.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", _axisList[3], "StatusALM", true));
                        this.imgcmbeRevLimit03.DataBindings.Clear();
                        this.imgcmbeRevLimit03.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", _axisList[3], "StatusRev", true));
                        this.imgcmbeDtmLimit03.DataBindings.Clear();
                        this.imgcmbeDtmLimit03.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", _axisList[3], "StatusDatum", true));
                        this.imgcmbeFwdLimit03.DataBindings.Clear();
                        this.imgcmbeFwdLimit03.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", _axisList[3], "StatusFwd", true));
                    }
                }
            }
        }


        /// <summary>
        /// 运动按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnAxis_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if (sbtn != null
                && sbtn.Tag != null)
            {
                int Idx = -1; bool isOn = false;
                ProCommon.Communal.MoveDir dir = ProCommon.Communal.MoveDir.BackWard;
                float dist = Convert.ToSingle(this.cmbeDistanceOption.SelectedItem.ToString());

                if (this.chkeModeContinue.Checked)
                    isOn = true;

                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_AXISFWD00":
                        Idx = 0;
                        dir = ProCommon.Communal.MoveDir.ForWard;
                        break;
                    case "SBTN_AXISREV00":
                        Idx = 0;
                        dir = ProCommon.Communal.MoveDir.BackWard;
                        break;
                    case "SBTN_AXISFWD01":
                        Idx = 1;
                        dir = ProCommon.Communal.MoveDir.ForWard;
                        break;
                    case "SBTN_AXISREV01":
                        Idx = 1;
                        dir = ProCommon.Communal.MoveDir.BackWard;
                        break;
                    case "SBTN_AXISFWD02":
                        Idx = 2;
                        dir = ProCommon.Communal.MoveDir.ForWard;
                        break;
                    case "SBTN_AXISREV02":
                        Idx = 2;
                        dir = ProCommon.Communal.MoveDir.BackWard;
                        break;
                    case "SBTN_AXISFWD03":
                        Idx = 3;
                        dir = ProCommon.Communal.MoveDir.ForWard;
                        break;
                    case "SBTN_AXISREV03":
                        Idx = 3;
                        dir = ProCommon.Communal.MoveDir.BackWard;
                        break;
                    default: break;
                }

                if (Idx > -1)
                {
                    //if (isOn)
                    //    MotionSingleContinueMove(_axisList[Idx], dir, _jogMode, _distanceLevel, _speed);
                    //else
                    //    MotionSingleJogMove(_axisList[Idx], dir, _jogMode, _distanceLevel, dist, _speed);

                    MotionSingleJogMove(_axisList[Idx], dir,_jogMode,_distanceLevel, dist, _speed);
                }
            }
        }

        /// <summary>
        /// 运动按钮抬起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnAxis_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if (sbtn != null
                && sbtn.Tag != null)
            {
                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_AXISREV00":
                    case "SBTN_AXISFWD00":
                        MotionMoveCancel(_axisList[0]);
                        break;
                    case "SBTN_AXISREV01":
                    case "SBTN_AXISFWD01":
                        MotionMoveCancel(_axisList[1]);
                        break;
                    case "SBTN_AXISREV02":
                    case "SBTN_AXISFWD02":
                        MotionMoveCancel(_axisList[2]);
                        break;
                    case "SBTN_AXISREV03":
                    case "SBTN_AXISFWD03":
                        MotionMoveCancel(_axisList[3]);
                        break;
                    default: break;
                }
            }
        }


        /// <summary>
        /// 轴停止按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnAxisStop_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if (sbtn != null
                && sbtn.Tag != null)
            {
                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_AXISSTP01":
                        {
                            if (this.cmbeAxies01.SelectedIndex > -1)
                            {
                                //在索引为0,1的轴列表中选轴
                                MotionMoveCancel(_axisList[this.cmbeAxies01.SelectedIndex]);
                            }
                        }
                        break;
                    case "SBTN_AXISSTP23":
                        {
                            if (this.cmbeAxies23.SelectedIndex > -1)
                            {
                                //在索引为2,3的轴列表中选轴
                                MotionMoveCancel(_axisList[this.cmbeAxies23.SelectedIndex + 2]);
                            }
                        }
                        break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// 轴回零按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbtnAxisDatum_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if (sbtn != null
                && sbtn.Tag != null)
            {
                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_AXISDTM01":
                        {
                            if (this.cmbeAxies01.SelectedIndex > -1)
                            {
                                //在索引为0,1的轴列表中选轴
                                MotionFindDatum(_axisList[this.cmbeAxies01.SelectedIndex]);
                            }
                        }
                        break;
                    case "SBTN_AXISDTM23":
                        {
                            if (this.cmbeAxies23.SelectedIndex > -1)
                            {
                                //在索引为2,3的轴列表中选轴
                                MotionFindDatum(_axisList[this.cmbeAxies23.SelectedIndex + 2]);
                            }
                        }
                        break;
                    default: break;
                }
            }
        }       

        /// <summary>
        /// 单元格单击事件
        /// [实现反转对象状态效果_ColumnEdit=false]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdvOutput_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //判断是操作列的单元格单击
            if (e.Column.FieldName == "OUTOPERATE")
            {
                DevExpress.XtraGrid.Views.Grid.GridView grdv = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (grdv != null)
                {
                    //获取对应的输出变量以及输出变量的当前值
                    ProCommon.Communal.OutVarObj outVarObj = (ProCommon.Communal.OutVarObj)grdv.GetRow(e.RowHandle);
                    if (outVarObj != null)
                    {
                        if(outVarObj.NewValue!=null)
                        {
                            bool bv = (bool)outVarObj.NewValue;
                            MotionSetOutput(outVarObj, !bv);
                        }
                    }
                }
            }
        }       
    }
}