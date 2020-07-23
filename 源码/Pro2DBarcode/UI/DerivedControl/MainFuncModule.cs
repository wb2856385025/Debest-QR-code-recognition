using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pro2DBarcode.UI.DerivedControl
{
    public partial class MainFuncModule : Pro2DBarcode.UI.DerivedControl.BaseModule
    {

        /// <summary>
        /// 分屏数
        /// </summary>
        protected internal int ViewNumber
        {
            private set;get;
        }

        /// <summary>
        /// 是否窗体加载完成
        /// [注:防止窗体未加载完成前,可引用其其尺寸]
        /// </summary>
        private bool _isLoadFinished;      

        /// <summary>
        /// 图像结果视图
        /// </summary>
        private ImageResultViewer[] _imgResultViewerArr;

        /// <summary>
        /// 射手详情视图
        /// </summary>
        private ShooterDetailViewer _shooterDetailViewer;

        private MainFuncModule()
        {
            InitializeComponent();
            this.Load += MainFuncModule_Load;
            this.tblltpViewRoot.SizeChanged += TblltpViewRoot_SizeChanged; //先注册事件,其尺寸变化后触发事件      
            InitFieldAndProperty();           
        }      

        public MainFuncModule(ProCommon.Communal.Language lan, int viewNum) :this()
        {
            LanguageVersion = lan;
            ViewNumber = (viewNum > 0) ? viewNum:1;
            InitControls();
        }

        private void InitFieldAndProperty()
        {
            _isLoadFinished = false;           
            ViewNumber = 0;
            _imgResultViewerArr = null;
            _shooterDetailViewer =null;
        }
       
        private void InitControls()
        {
            _shooterDetailViewer = new ShooterDetailViewer(LanguageVersion);
            _shooterDetailViewer.Parent = this.pcViewRoot;
            _shooterDetailViewer.Dock = DockStyle.Fill;           
        }

        private void MainFuncModule_Load(object sender, EventArgs e)
        {
            SplitViewers(ViewNumber);
            _isLoadFinished = true;           
        }

        private void TblltpViewRoot_SizeChanged(object sender, EventArgs e)
        {
            if (_isLoadFinished)
                SplitViewers(ViewNumber);
            ShowMultiViewers();
        }


        /// <summary>
        /// 显示多屏界面
        /// [注:多个图形结果显示窗口]
        /// </summary>
        public void ShowMultiViewers()
        {
            if (_shooterDetailViewer != null)
                _shooterDetailViewer.Hide();
                       
            this.tblltpViewRoot.Show();
            this.tblltpViewRoot.BringToFront();           
        }

        /// <summary>
        /// 显示单屏界面
        /// [注:射手详情显示窗口]
        /// </summary>
        public void ShowSingleViewer(int viewIdx)
        {
            this.tblltpViewRoot.Hide();           

            if (_shooterDetailViewer != null)
            {
                _shooterDetailViewer.Show();
                _shooterDetailViewer.BringToFront();

                UpdateShooterDetailViewer(viewIdx);
            }
        }

        /// <summary>
        /// 更射手详情窗口
        /// </summary>
        public void UpdateShooterDetailViewer(int viewIdx)
        {
            if (_shooterDetailViewer != null)
            {
                //1-图形信息
                //2-文本信息
                //3-数据表信息
            }
        }

        /// <summary>
        /// 分屏
        /// </summary>
        private void SplitViewers(int viewNum)
        {
            if(!_isLoadFinished)
            {
                this.tblltpViewRoot.ColumnStyles.Clear();
                this.tblltpViewRoot.RowStyles.Clear();
                this.tblltpViewRoot.Controls.Clear();
                this.tblltpViewRoot.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
            }
            switch (viewNum)
            {
                case 2:                    
                    SplitTwoViews();
                    break;
                case 3:
                case 4:                    
                    SplitFourViews();
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:                   
                    SplitNineViews();
                    break;
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:                   
                    SplitSixteenViews(); 
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:                   
                    SplitTwentyFiveViews();
                    break;
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:                   
                    SplitThirtySixViews();
                    break;
                default:
                    SplitOneView();
                    break;
            }

        }

        #region 分视图

        /// <summary>
        /// 单视图
        /// </summary>
        private void SplitOneView()
        {
            int tmpViewNum = 1,rowNum=1,colNum=1;
            if(_imgResultViewerArr==null)
                _imgResultViewerArr = new ImageResultViewer[tmpViewNum];

            this.tblltpViewRoot.RowCount = rowNum;
            this.tblltpViewRoot.ColumnCount = colNum;
            this.tblltpViewRoot.Refresh();

            //ImageResultViewer的Margin设置为3(默认),因此需要空格6*列数,6*行数
            int viewerWidth = (this.tblltpViewRoot.Width - 6 * colNum) / colNum;
            int viewerHeight= (this.tblltpViewRoot.Height - 6 * rowNum) / colNum;

            int rowIdx, colIdx;//单元格的行列索引
            for(int i=0;i< tmpViewNum; i++)
            {
                // 按先行后列的顺序排
                rowIdx = i / rowNum; //控件所在行索引为视图个数的商数
                colIdx = i % colNum;//控件所在列索引为视图个数的余数

                if (_imgResultViewerArr[i] == null)
                    _imgResultViewerArr[i] = new ImageResultViewer(LanguageVersion, i);

                _imgResultViewerArr[i].Size = new Size(viewerWidth, viewerHeight);
                //更新图形结果窗口
                UpdateImgRsltViewer(_imgResultViewerArr[i]);

                if (_imgResultViewerArr[i] != null
                    && !_isLoadFinished)
                    this.tblltpViewRoot.Controls.Add(_imgResultViewerArr[i], colIdx, rowIdx);
            }
        }      

        /// <summary>
        /// 双视图
        /// </summary>
        private void SplitTwoViews()
        {
            int tmpViewNum = 2, rowNum = 1, colNum = 2;
            if (_imgResultViewerArr == null)
                _imgResultViewerArr = new ImageResultViewer[tmpViewNum];

            this.tblltpViewRoot.RowCount = rowNum;
            this.tblltpViewRoot.ColumnCount = colNum;
            this.tblltpViewRoot.Refresh();
           

            //ImageResultViewer的Margin设置为3(默认),因此需要空格6*列数,6*行数
            int viewerWidth = (this.tblltpViewRoot.Width - 6 * colNum) / colNum;
            int viewerHeight = (this.tblltpViewRoot.Height - 6 * rowNum) / rowNum;

            int rowIdx, colIdx;//单元格的行列索引
            for (int i = 0; i < tmpViewNum; i++)
            {
                // 按先行后列的顺序排              
                switch (i)
                {
                    case 0:
                        rowIdx = 0;
                        colIdx = 0;
                        break;
                    case 1:
                        rowIdx = 0;
                        colIdx = 1;
                        break;
                    default:
                        rowIdx = 0;
                        colIdx = 0;
                        break;
                }

                if (_imgResultViewerArr[i] == null)
                {
                    _imgResultViewerArr[i] = new ImageResultViewer(LanguageVersion, i);
                    //_imgResultViewerArr[i].hwndcImgRsltView.BackColor = System.Drawing.Color.White;
                }

                _imgResultViewerArr[i].Size = new Size(viewerWidth, viewerHeight);

                //更新图形结果窗口
                UpdateImgRsltViewer(_imgResultViewerArr[i]);

                if (_imgResultViewerArr[i] != null
                    && !_isLoadFinished)
                    this.tblltpViewRoot.Controls.Add(_imgResultViewerArr[i], colIdx, rowIdx);
            }
        }

        /// <summary>
        /// 四视图
        /// </summary>
        private void SplitFourViews()
        {
            int tmpViewNum = 4, rowNum = 2, colNum = 2; //视图个数的平方根
            if (_imgResultViewerArr == null)
                _imgResultViewerArr = new ImageResultViewer[tmpViewNum];

            this.tblltpViewRoot.RowCount = rowNum;
            this.tblltpViewRoot.ColumnCount = colNum;
            this.tblltpViewRoot.Refresh();

            //ImageResultViewer的Margin设置为3(默认),因此需要空格6*列数,6*行数
            int viewerWidth = (this.tblltpViewRoot.Width - 6 * colNum) / colNum;
            int viewerHeight = (this.tblltpViewRoot.Height - 6 * rowNum) / colNum;

            int rowIdx, colIdx;//单元格的行列索引
            for (int i = 0; i < tmpViewNum; i++)
            {
                // 按先行后列的顺序排
                rowIdx = i / rowNum; //控件所在行索引为视图个数的商数
                colIdx = i % colNum;//控件所在列索引为视图个数的余数

                if (_imgResultViewerArr[i] == null)
                    _imgResultViewerArr[i] = new ImageResultViewer(LanguageVersion, i);

                _imgResultViewerArr[i].Size = new Size(viewerWidth, viewerHeight);
                //更新图形结果窗口
                UpdateImgRsltViewer(_imgResultViewerArr[i]);

                if (_imgResultViewerArr[i] != null
                    && !_isLoadFinished)
                    this.tblltpViewRoot.Controls.Add(_imgResultViewerArr[i], colIdx, rowIdx);
            }
        }

        /// <summary>
        /// 九视图
        /// </summary>
        private void SplitNineViews()
        {
            int tmpViewNum = 9, rowNum = 3, colNum = 3; //视图个数的平方根
            if (_imgResultViewerArr == null)
                _imgResultViewerArr = new ImageResultViewer[tmpViewNum];

            this.tblltpViewRoot.RowCount = rowNum;
            this.tblltpViewRoot.ColumnCount = colNum;
            this.tblltpViewRoot.Refresh();

            //ImageResultViewer的Margin设置为3(默认),因此需要空格6*列数,6*行数
            int viewerWidth = (this.tblltpViewRoot.Width - 6 * colNum) / colNum;
            int viewerHeight = (this.tblltpViewRoot.Height - 6 * rowNum) / colNum;

            int rowIdx, colIdx;//单元格的行列索引
            for (int i = 0; i < tmpViewNum; i++)
            {
                // 按先行后列的顺序排
                rowIdx = i / rowNum; //控件所在行索引为视图个数的商数
                colIdx = i % colNum;//控件所在列索引为视图个数的余数

                if (_imgResultViewerArr[i] == null)
                    _imgResultViewerArr[i] = new ImageResultViewer(LanguageVersion, i);

                _imgResultViewerArr[i].Size = new Size(viewerWidth, viewerHeight);
                //更新图形结果窗口
                UpdateImgRsltViewer(_imgResultViewerArr[i]);

                if (_imgResultViewerArr[i] != null
                    && !_isLoadFinished)
                    this.tblltpViewRoot.Controls.Add(_imgResultViewerArr[i], colIdx, rowIdx);
            }
        }

        /// <summary>
        /// 十六视图
        /// </summary>
        private void SplitSixteenViews()
        {
            int tmpViewNum = 16, rowNum = 4, colNum = 4; //视图个数的平方根
            if (_imgResultViewerArr == null)
                _imgResultViewerArr = new ImageResultViewer[tmpViewNum];

            this.tblltpViewRoot.RowCount = rowNum;
            this.tblltpViewRoot.ColumnCount = colNum;
            this.tblltpViewRoot.Refresh();

            //ImageResultViewer的Margin设置为3(默认),因此需要空格6*列数,6*行数
            int viewerWidth = (this.tblltpViewRoot.Width - 6 * colNum) / colNum;
            int viewerHeight = (this.tblltpViewRoot.Height - 6 * rowNum) / colNum;

            int rowIdx, colIdx;//单元格的行列索引
            for (int i = 0; i < tmpViewNum; i++)
            {
                // 按先行后列的顺序排
                rowIdx = i / rowNum; //控件所在行索引为视图个数的商数
                colIdx = i % colNum;//控件所在列索引为视图个数的余数

                if (_imgResultViewerArr[i] == null)
                    _imgResultViewerArr[i] = new ImageResultViewer(LanguageVersion, i);

                _imgResultViewerArr[i].Size = new Size(viewerWidth, viewerHeight);
                //更新图形结果窗口
                UpdateImgRsltViewer(_imgResultViewerArr[i]);

                if (_imgResultViewerArr[i] != null
                    && !_isLoadFinished)
                    this.tblltpViewRoot.Controls.Add(_imgResultViewerArr[i], colIdx, rowIdx);
            }
        }

        /// <summary>
        /// 二十五视图
        /// </summary>
        private void SplitTwentyFiveViews()
        {
            int tmpViewNum = 25, rowNum = 5, colNum = 5; //视图个数的平方根
            if (_imgResultViewerArr == null)
                _imgResultViewerArr = new ImageResultViewer[tmpViewNum];

            this.tblltpViewRoot.RowCount = rowNum;
            this.tblltpViewRoot.ColumnCount = colNum;
            this.tblltpViewRoot.Refresh();

            //ImageResultViewer的Margin设置为3(默认),因此需要空格6*列数,6*行数
            int viewerWidth = (this.tblltpViewRoot.Width - 6 * colNum) / colNum;
            int viewerHeight = (this.tblltpViewRoot.Height - 6 * rowNum) / colNum;

            int rowIdx, colIdx;//单元格的行列索引
            for (int i = 0; i < tmpViewNum; i++)
            {
                // 按先行后列的顺序排
                rowIdx = i / rowNum; //控件所在行索引为视图个数的商数
                colIdx = i % colNum;//控件所在列索引为视图个数的余数
                if (_imgResultViewerArr[i] == null)
                    _imgResultViewerArr[i] = new ImageResultViewer(LanguageVersion, i);

                _imgResultViewerArr[i].Size = new Size(viewerWidth, viewerHeight);
                //更新图形结果窗口
                UpdateImgRsltViewer(_imgResultViewerArr[i]);

                if (_imgResultViewerArr[i] != null
                    && !_isLoadFinished)
                    this.tblltpViewRoot.Controls.Add(_imgResultViewerArr[i], colIdx, rowIdx);
            }
        }

        /// <summary>
        /// 三十六视图
        /// </summary>
        private void SplitThirtySixViews()
        {
            int tmpViewNum = 36, rowNum = 6, colNum = 6; //视图个数的平方根
            if (_imgResultViewerArr == null)
                _imgResultViewerArr = new ImageResultViewer[tmpViewNum];

            this.tblltpViewRoot.RowCount = rowNum;
            this.tblltpViewRoot.ColumnCount = colNum;
            this.tblltpViewRoot.Refresh();

            //ImageResultViewer的Margin设置为3(默认),因此需要空格6*列数,6*行数
            int viewerWidth = (this.tblltpViewRoot.Width - 6 * colNum) / colNum;
            int viewerHeight = (this.tblltpViewRoot.Height - 6 * rowNum) / colNum;

            int rowIdx, colIdx;//单元格的行列索引
            for (int i = 0; i < tmpViewNum; i++)
            {
                // 按先行后列的顺序排
                rowIdx = i / rowNum; //控件所在行索引为视图个数的商数
                colIdx = i % colNum;//控件所在列索引为视图个数的余数
                if(_imgResultViewerArr[i]==null)
                    _imgResultViewerArr[i] = new ImageResultViewer(LanguageVersion, i);

                _imgResultViewerArr[i].Size = new Size(viewerWidth, viewerHeight);
                //更新图形结果窗口
                UpdateImgRsltViewer(_imgResultViewerArr[i]);

                if (_imgResultViewerArr[i] != null
                    && !_isLoadFinished)
                    this.tblltpViewRoot.Controls.Add(_imgResultViewerArr[i], colIdx, rowIdx);
            }
        }

        private void UpdateImgRsltViewer(ImageResultViewer imgRsltView)
        {

        }

        private void UpdateViewers()
        {

        }

        #endregion
    }
}
