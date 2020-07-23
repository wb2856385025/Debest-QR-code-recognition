using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProCommon.UserDefForm
{
    public partial class FrmMsgBox : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool MessageBeep(uint type);

        [DllImport("Shell32.dll")]
        public extern static int ExtractIconEx(string libName, int iconIndex, IntPtr[] largeIcon, IntPtr[] smallIcon, int nIcons);

        static private IntPtr[] largeIcon;
        static private IntPtr[] smallIcon;

        static private FrmMsgBox newMessageBox;
        static private Label frmTitle;
        static private Label frmMessage;
        static private PictureBox pIcon;
        static private FlowLayoutPanel flpButtons;
        static private Icon frmIcon;

        static private Button btnOK;
        static private Button btnAbort;
        static private Button btnRetry;
        static private Button btnIgnore;
        static private Button btnCancel;
        static private Button btnYes;
        static private Button btnNo;

        static private DialogResult CYReturnButton;

        private static object olock;

        static private void BuildMessageBox(string title)
        {
            lock (olock)
            {
                newMessageBox = new FrmMsgBox();
                newMessageBox.Text = title;
                newMessageBox.Size = new System.Drawing.Size(400, 220);
                newMessageBox.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                newMessageBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                newMessageBox.Paint += new PaintEventHandler(newMessageBox_Paint);
                newMessageBox.BackColor = System.Drawing.Color.White;

                TableLayoutPanel tlp = new TableLayoutPanel();
                tlp.RowCount = 3;
                tlp.ColumnCount = 0;
                tlp.Dock = System.Windows.Forms.DockStyle.Fill;
                tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50));
                tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
                tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50));
                tlp.BackColor = System.Drawing.Color.Transparent;
                tlp.Padding = new Padding(2, 5, 2, 2);

                frmTitle = new Label();
                frmTitle.Dock = System.Windows.Forms.DockStyle.Fill;
                frmTitle.BackColor = System.Drawing.Color.Transparent;
                frmTitle.ForeColor = System.Drawing.Color.White;
                frmTitle.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular);

                frmMessage = new Label();
                frmMessage.Dock = System.Windows.Forms.DockStyle.Fill;
                frmMessage.BackColor = System.Drawing.Color.White;
                frmMessage.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular);
                frmMessage.Text = "hiii";

                pIcon = new PictureBox();
                ExtractIconEx("shell32.dll", 0, largeIcon, smallIcon, 250);
                flpButtons = new FlowLayoutPanel();
                flpButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
                flpButtons.Padding = new Padding(0, 5, 5, 0);
                flpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
                flpButtons.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

                TableLayoutPanel tlpMessagePanel = new TableLayoutPanel();
                tlpMessagePanel.BackColor = System.Drawing.Color.White;
                tlpMessagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
                tlpMessagePanel.ColumnCount = 2;
                tlpMessagePanel.RowCount = 0;
                tlpMessagePanel.Padding = new Padding(4, 5, 4, 4);
                tlpMessagePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50));
                tlpMessagePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
                tlpMessagePanel.Controls.Add(pIcon);
                tlpMessagePanel.Controls.Add(frmMessage);

                tlp.Controls.Add(frmTitle);
                tlp.Controls.Add(tlpMessagePanel);
                tlp.Controls.Add(flpButtons);
                newMessageBox.Controls.Add(tlp);
            }
        }

        /// <summary>
        /// Message: Text to display in the message box.
        /// </summary>
        static public DialogResult Show(string Message, bool isChsLanguage)
        {
            BuildMessageBox("");
            frmMessage.Text = Message;
            ShowOKButton(isChsLanguage);
            newMessageBox.ShowDialog();
            return CYReturnButton;
        }

        /// <summary>
        /// Title: Text to display in the title bar of the messagebox.
        /// </summary>
        static public DialogResult Show(string Message, string Title, bool isChsLanguage)
        {
            BuildMessageBox(Title);
            frmTitle.Text = Title;
            frmMessage.Text = Message;
            ShowOKButton(isChsLanguage);
            newMessageBox.ShowDialog();
            return CYReturnButton;
        }

        /// <summary>
        /// MButtons: Display MyButtons on the message box.
        /// </summary>
        static public DialogResult Show(string Message, string Title, MyButtons MButtons, bool isChsLanguage)
        {
            BuildMessageBox(Title); // BuildMessageBox method, responsible for creating the MessageBox
            frmTitle.Text = Title; // Set the title of the MessageBox
            frmMessage.Text = Message; //Set the text of the MessageBox
            ButtonStatements(MButtons, isChsLanguage); // ButtonStatements method is responsible for showing the appropreiate buttons
            newMessageBox.ShowDialog(); // Show the MessageBox as a Dialog.
            return CYReturnButton; // Return the button click as an Enumerator
        }

        /// <summary>
        /// MIcon: Display MyIcon on the message box.
        /// </summary>
        static public DialogResult Show(string Message, string Title, MyButtons MButtons, MyIcon MIcon, bool isChsLanguage)
        {
            lock (olock)
            {
                BuildMessageBox(Title);
                frmTitle.Text = Title;
                frmMessage.Text = Message;
                ButtonStatements(MButtons, isChsLanguage);
                IconStatements(MIcon);
                Image imageIcon = new Bitmap(frmIcon.ToBitmap(), 38, 38);
                pIcon.Image = imageIcon;
                newMessageBox.ShowDialog();
                return CYReturnButton;
            }

        }

        static void btnOK_Click(object sender, EventArgs e)
        {
            CYReturnButton = DialogResult.OK;
            newMessageBox.Dispose();
        }

        static void btnAbort_Click(object sender, EventArgs e)
        {
            CYReturnButton = DialogResult.Abort;
            newMessageBox.Dispose();
        }

        static void btnRetry_Click(object sender, EventArgs e)
        {
            CYReturnButton = DialogResult.Retry;
            newMessageBox.Dispose();
        }

        static void btnIgnore_Click(object sender, EventArgs e)
        {
            CYReturnButton = DialogResult.Ignore;
            newMessageBox.Dispose();
        }

        static void btnCancel_Click(object sender, EventArgs e)
        {
            CYReturnButton = DialogResult.Cancel;
            newMessageBox.Dispose();
        }

        static void btnYes_Click(object sender, EventArgs e)
        {
            CYReturnButton = DialogResult.Yes;
            newMessageBox.Dispose();
        }

        static void btnNo_Click(object sender, EventArgs e)
        {
            CYReturnButton = DialogResult.No;
            newMessageBox.Dispose();
        }

        static private void ShowOKButton(bool isChsLanguage)
        {
            btnOK = new Button();
            btnOK.Text = isChsLanguage ? "确认" : "Ok";
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.Size = new System.Drawing.Size(100, 35);
            btnOK.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            btnOK.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular);
            btnOK.Click += new EventHandler(btnOK_Click);
            flpButtons.Controls.Add(btnOK);
        }

        static private void ShowAbortButton(bool isChsLanguage)
        {
            btnAbort = new Button();
            btnAbort.Text = isChsLanguage ? "关于" : "Abort";
            btnAbort.FlatStyle = FlatStyle.Flat;
            btnAbort.Size = new System.Drawing.Size(100, 35);
            btnAbort.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            btnAbort.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular);
            btnAbort.Click += new EventHandler(btnAbort_Click);
            flpButtons.Controls.Add(btnAbort);
        }

        static private void ShowRetryButton(bool isChsLanguage)
        {
            btnRetry = new Button();
            btnRetry.Text = isChsLanguage ? "重试" : "Retry";
            btnRetry.FlatStyle = FlatStyle.Flat;
            btnRetry.Size = new System.Drawing.Size(100, 35);
            btnRetry.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            btnRetry.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular);
            btnRetry.Click += new EventHandler(btnRetry_Click);
            flpButtons.Controls.Add(btnRetry);
        }

        static private void ShowIgnoreButton(bool isChsLanguage)
        {
            btnIgnore = new Button();
            btnIgnore.Text = isChsLanguage ? "忽略" : "Ignore";
            btnIgnore.FlatStyle = FlatStyle.Flat;
            btnIgnore.Size = new System.Drawing.Size(100, 35);
            btnIgnore.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            btnIgnore.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular);
            btnIgnore.Click += new EventHandler(btnIgnore_Click);
            flpButtons.Controls.Add(btnIgnore);
        }

        static private void ShowCancelButton(bool isChsLanguage)
        {
            btnCancel = new Button();
            btnCancel.Text = isChsLanguage ? "取消" : "Cancel";
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Size = new System.Drawing.Size(100, 35);
            btnCancel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            btnCancel.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            flpButtons.Controls.Add(btnCancel);
        }

        static private void ShowYesButton(bool isChsLanguage)
        {
            btnYes = new Button();
            btnYes.Text = isChsLanguage ? "是" : "Yes";
            btnYes.FlatStyle = FlatStyle.Flat;
            btnYes.Size = new System.Drawing.Size(100, 35);
            btnYes.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            btnYes.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular);
            btnYes.Click += new EventHandler(btnYes_Click);
            flpButtons.Controls.Add(btnYes);
        }

        static private void ShowNoButton(bool isChsLanguage)
        {
            btnNo = new Button();
            btnNo.Text = isChsLanguage ? "否" : "No";
            btnNo.FlatStyle = FlatStyle.Flat;
            btnNo.Size = new System.Drawing.Size(100, 35);
            btnNo.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            btnNo.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular);
            btnNo.Click += new EventHandler(btnNo_Click);
            flpButtons.Controls.Add(btnNo);
        }

        static private void ButtonStatements(MyButtons MButtons, bool isChsLanguage)
        {
            lock (olock)
            {
                if (MButtons == MyButtons.AbortRetryIgnore)
                {
                    ShowIgnoreButton(isChsLanguage);
                    ShowRetryButton(isChsLanguage);
                    ShowAbortButton(isChsLanguage);
                }

                if (MButtons == MyButtons.OK)
                {
                    ShowOKButton(isChsLanguage);
                }

                if (MButtons == MyButtons.OKCancel)
                {
                    ShowCancelButton(isChsLanguage);
                    ShowOKButton(isChsLanguage);
                }

                if (MButtons == MyButtons.RetryCancel)
                {
                    ShowCancelButton(isChsLanguage);
                    ShowRetryButton(isChsLanguage);
                }

                if (MButtons == MyButtons.YesNo)
                {
                    ShowNoButton(isChsLanguage);
                    ShowYesButton(isChsLanguage);
                }

                if (MButtons == MyButtons.YesNoCancel)
                {
                    ShowCancelButton(isChsLanguage);
                    ShowNoButton(isChsLanguage);
                    ShowYesButton(isChsLanguage);
                }
            }
        }


      

        static private void IconStatements(MyIcon MIcon)
        {
            lock (olock)
            {
                if (MIcon == MyIcon.Error)
                {
                    MessageBeep(30);
                    frmIcon = Icon.FromHandle(largeIcon[109]);
                }

                if (MIcon == MyIcon.Explorer)
                {
                    MessageBeep(0);
                    frmIcon = Icon.FromHandle(largeIcon[220]);
                }

                if (MIcon == MyIcon.Find)
                {
                    MessageBeep(0);
                    frmIcon = Icon.FromHandle(largeIcon[22]);
                }

                if (MIcon == MyIcon.Information)
                {
                    MessageBeep(0);
                    frmIcon = Icon.FromHandle(largeIcon[221]);
                }

                if (MIcon == MyIcon.Mail)
                {
                    MessageBeep(0);
                    frmIcon = Icon.FromHandle(largeIcon[156]);
                }

                if (MIcon == MyIcon.Media)
                {
                    MessageBeep(0);
                    frmIcon = Icon.FromHandle(largeIcon[116]);
                }

                if (MIcon == MyIcon.Print)
                {
                    MessageBeep(0);
                    frmIcon = Icon.FromHandle(largeIcon[136]);
                }

                if (MIcon == MyIcon.Question)
                {
                    MessageBeep(0);
                    frmIcon = Icon.FromHandle(largeIcon[23]);
                }

                if (MIcon == MyIcon.RecycleBinEmpty)
                {
                    MessageBeep(0);
                    frmIcon = Icon.FromHandle(largeIcon[31]);
                }

                if (MIcon == MyIcon.RecycleBinFull)
                {
                    MessageBeep(0);
                    frmIcon = Icon.FromHandle(largeIcon[32]);
                }

                if (MIcon == MyIcon.Stop)
                {
                    MessageBeep(0);
                    frmIcon = Icon.FromHandle(largeIcon[27]);
                }

                if (MIcon == MyIcon.User)
                {
                    MessageBeep(0);
                    frmIcon = Icon.FromHandle(largeIcon[170]);
                }

                if (MIcon == MyIcon.Warning)
                {
                    MessageBeep(30);
                    frmIcon = Icon.FromHandle(largeIcon[217]);
                }
            }
        }

        static void newMessageBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle frmTitleL = new Rectangle(0, 0, (newMessageBox.Width / 2 + 2), 35);
            Rectangle frmTitleR = new Rectangle((newMessageBox.Width / 2 + 2), 0, (newMessageBox.Width / 2 + 2), 35);
            Rectangle frmMessageBox = new Rectangle(0, 0, (newMessageBox.Width - 2), (newMessageBox.Height - 2));
            //LinearGradientBrush frmLGBL = new LinearGradientBrush(frmTitleL, Color.FromArgb(87, 148, 160), Color.FromArgb(209, 230, 243), LinearGradientMode.Horizontal);
            //LinearGradientBrush frmLGBR = new LinearGradientBrush(frmTitleR, Color.FromArgb(209, 230, 243), Color.FromArgb(87, 148, 160), LinearGradientMode.Horizontal);
            LinearGradientBrush frmLGBL = new LinearGradientBrush(frmTitleL, SystemColors.InactiveCaptionText, SystemColors.InactiveCaptionText, LinearGradientMode.Horizontal);
            LinearGradientBrush frmLGBR = new LinearGradientBrush(frmTitleR, SystemColors.InactiveCaptionText, SystemColors.InactiveCaptionText, LinearGradientMode.Horizontal);
            Pen frmPen = new Pen(Color.FromArgb(63, 119, 143), 1);
            g.FillRectangle(frmLGBL, frmTitleL);
            g.FillRectangle(frmLGBR, frmTitleR);
            g.DrawRectangle(frmPen, frmMessageBox);
        }

        private FrmMsgBox()
        {
            InitializeComponent();
        }

        static FrmMsgBox()
        {
            largeIcon = new IntPtr[250];
            smallIcon = new IntPtr[250];
            olock = new object();
        }
    }

    public enum MyIcon
    {
        Error,
        Explorer,
        Find,
        Information,
        Mail,
        Media,
        Print,
        Question,
        RecycleBinEmpty,
        RecycleBinFull,
        Stop,
        User,
        Warning
    }

    public enum MyButtons
    {
        AbortRetryIgnore,
        OK,
        OKCancel,
        RetryCancel,
        YesNo,
        YesNoCancel
    }
}
