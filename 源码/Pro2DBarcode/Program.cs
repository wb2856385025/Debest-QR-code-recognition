using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;

namespace Pro2DBarcode
{
   
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("User32.dll", EntryPoint = "ShowWindow", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //hWnd窗口句柄，cmdShow指示窗口如何被显示(如果窗口可见，则返回非零，如果窗口被隐藏，则返回零)
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        //找到某个窗口与给出的类别名和窗口名相同窗口
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [System.Runtime.InteropServices.DllImport("User32.dll", SetLastError = true)]
        //切换到窗口并把窗口置入前台,fAltTab代表窗口正在通过Alt+Tab被切换
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        private const int WS_SHOWNORMAL = 1;
        public static IntPtr formhwnd;
        private const int WS_RESTORE = 9;
        public static bool _isChs;
        public static string _txt, _caption;
        public static ProCommon.Communal.Language LanguageVersion {  set; get; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            _isChs = LanguageVersion == ProCommon.Communal.Language.English;
            System.Diagnostics.Process currentpro = System.Diagnostics.Process.GetCurrentProcess();                             //获取当前运行进程实例
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(currentpro.ProcessName);     //根据当前进程的进程名获取进程资源集合
            //程序已经运行
            if (processes.Length > 1)
            {
                foreach (System.Diagnostics.Process pro in processes)
                {
                    if (pro.Id != currentpro.Id)
                    {
                        //进程关联的主窗口的句柄为0，代表没有找到该窗体，即该窗体被隐藏的情况
                        if (pro.MainWindowHandle.ToInt32() == 0)
                        {
                            _txt = _isChs ? "程序已经运行在托盘!" : "The program has run on the tray!";
                            _caption = _isChs ? "提示信息" : "Prompt Information";
                            MessageBox.Show(_txt, _caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //获取窗体句柄
                            formhwnd = FindWindow(null, "FrmMain");
                            //显示指定窗口
                            ShowWindow(formhwnd, WS_RESTORE);
                            //重新显示该窗体并切换置入前台
                            SwitchToThisWindow(formhwnd, true);
                        }
                        else
                        {
                            //进程句柄非0，窗体未隐藏，则切换到该窗体并置入前台
                            _txt = _isChs ? "程序已经运行!" : "The program is running!";
                            _caption = _isChs ? "提示信息" : "Prompt Information";
                            MessageBox.Show(_txt, _caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowWindow(formhwnd, WS_RESTORE);
                            SwitchToThisWindow(pro.MainWindowHandle, true);
                        }
                    }
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                DevExpress.UserSkins.BonusSkins.Register();
                DevExpress.Skins.SkinManager.EnableFormSkins();
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2016 Colorful");

                Pro2DBarcode.UI.UI_CheckProgress checkProcess = new Pro2DBarcode.UI.UI_CheckProgress();
                checkProcess.Shown += (sender, e) => { checkProcess.StartCheck(); };
                checkProcess.Disposed += (sender, e) =>
                {                   
                    if (Pro2DBarcode.Manager.SystemManager.Instance.ChkOK)
                    {
                        Manager.DeviceManager.Instance.DeviceListInit();
                        Manager.DeviceManager.Instance.DeviceListStart();

                        Pro2DBarcode.UI.FrmMain.Instance.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("系统自检异常!\r\n"+Pro2DBarcode.Manager.SystemManager.Instance.ChkErr, "警示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Environment.Exit(-1);
                    }
                };
                checkProcess.ShowDialog(UI.FrmMain.Instance);
            }
        }
    }
}
