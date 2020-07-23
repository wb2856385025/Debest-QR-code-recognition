using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       Functions
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProCommon.Communal
 * File      Name：       Functions
 * Creating  Time：       12/20/2019 3:49:19 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProCommon.Communal
{
    /// <summary>
    /// DES加/解密类
    /// </summary>
    public class DESEncrypt
    {
        #region +++++++++加密+++++++++
        public static string Encrypt(string txt)
        {
            return Encrypt(txt, "xYz_Albert");
        }

        public static string Encrypt(string txt, string sKey)
        {
            System.Security.Cryptography.DESCryptoServiceProvider dESCryptoServiceProvider = new System.Security.Cryptography.DESCryptoServiceProvider();
            byte[] inputByteArr = Encoding.Default.GetBytes(txt);
#pragma warning disable CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
            dESCryptoServiceProvider.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
#pragma warning restore CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
#pragma warning disable CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
            dESCryptoServiceProvider.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
#pragma warning restore CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            //加密器
            System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
            cryptoStream.Write(inputByteArr, 0, inputByteArr.Length);
            cryptoStream.FlushFinalBlock();
            StringBuilder strBuilder = new StringBuilder();
            foreach (byte b in memoryStream.ToArray())
            {
                strBuilder.AppendFormat("{0:X2}", b);
            }
            return strBuilder.ToString();
        }
        #endregion

        #region ---------解密---------
        public static string Decrypt(string txt)
        {
            return Decrypt(txt, "xYz_Albert");
        }

        public static string Decrypt(string txt, string sKey)
        {
            System.Security.Cryptography.DESCryptoServiceProvider dESCryptoServiceProvider = new System.Security.Cryptography.DESCryptoServiceProvider();
            int len = txt.Length / 2;
            byte[] inputByteArr = new byte[len];
            int i, j;
            for (i = 0; i < len; i++)
            {
                j = Convert.ToInt32(txt.Substring(i * 2, 2), 16);
                inputByteArr[i] = (byte)j;
            }

#pragma warning disable CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
            dESCryptoServiceProvider.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
#pragma warning restore CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
#pragma warning disable CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
            dESCryptoServiceProvider.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
#pragma warning restore CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            //解密器
            System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
            cryptoStream.Write(inputByteArr, 0, inputByteArr.Length);
            cryptoStream.FlushFinalBlock();
            return Encoding.Default.GetString(memoryStream.ToArray());
        }
        #endregion
    }

    /// <summary>
    /// 序列化类
    /// </summary>
    public class Serialization
    {
        /// <summary>
        /// 私有构造
        /// </summary>
        private Serialization() { }

        /// <summary>
        /// 从文件加载对象(反序列化XML格式为该类类型)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static object LoadFromFile(System.Type type, string file)
        {
            object obj = new object();

            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type);

            if (System.IO.File.Exists(file))
            {
                System.IO.FileStream fs = null;

                try
                {
                    fs = System.IO.File.Open(file, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                }
                catch
                {
                    obj = type.InvokeMember(null, System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.CreateInstance, null, null, null);
                }

                try
                {
                    obj = xs.Deserialize(fs);
                }
                catch (Exception e)
                {
                    string s = e.Message;
                    obj = type.InvokeMember(null, System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.CreateInstance, null, null, null);
                }
                finally
                {
                    fs.Close();
                }
            }
            else
            {
                try
                {
                    obj = type.InvokeMember(null, System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.CreateInstance, null, null, null);
                    SaveToFile(obj, file);
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch(System.Exception ex) { }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            }

            return obj;
        }

        /// <summary>
        /// 将对象保存到文件(序列化为XML格式)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="file"></param>
        public static void SaveToFile(object obj, string file)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            System.IO.FileStream fs = System.IO.File.Open(file, System.IO.FileMode.Create, System.IO.FileAccess.Write);

            try
            {
                xs.Serialize(fs, obj);
            }
            catch(System.Exception ex) { }
            finally
            {
                fs.Close();
            }
        }
    }

    /// <summary>
    /// 软件日志写操作类
    /// 对系统执行过程以及异常进行记录
    /// 写入到本地TXT文档
    /// 文件容量超出2MB字节,删除文件并重新创建
    /// </summary>
    public class LogWriter
    {
        const uint txtContentCapacity = 1; //日志文件的容量,单位MB  

        /// <summary>
        /// 根据指定的格式写日志到指定的文件
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void WriteLog(string filePath, string format, params object[] args)
        {
            Write(filePath, string.Format(format, args));
        }

        public static void WriteException(string filePath, System.Exception ex)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(filePath);
                    if (fi.Length > txtContentCapacity * 1024 * 1024)
                    {
                        System.IO.File.Delete(filePath);
                        System.IO.File.Create(filePath);
                    }
                }

                System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, true, new System.Text.UnicodeEncoding());
                sw.WriteLine(DateTime.Now.ToString("[yy-MM-dd HH:mm:ss]"));
                sw.WriteLine(ex.TargetSite);
                sw.WriteLine(ex.StackTrace);
                sw.WriteLine(ex.Message);
                sw.Flush();
                sw.Close();
            }
            catch { }
        }


        /// <summary>
        /// 方法：指定路径文件写入内容并检查是否超容量(2*1024*1024)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        private static void Write(string filePath, string content)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(filePath);
                    if (fi.Length > txtContentCapacity * 1024 * 1024)
                    {
                        System.IO.File.Delete(filePath);
                        System.IO.File.Create(filePath);
                    }
                }

                System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, true, new System.Text.UnicodeEncoding());
                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
            }
            catch { }

        }
    }

    public class AutoLaunch
    {
        #region 程序开机启动的方法一:将程序的快捷方式创建到计算机的自动启动目录下(不需要管理员权限)

        /// <summary>
        /// 快捷方式的名称--任意自定义
        /// </summary>
        private string _quickName;
        public string QuickName
        {
            set { _quickName = value; }
            get
            {
                if (!string.IsNullOrEmpty(_quickName))
                    return _quickName;
                else
                    return "自启动用户程序";
            }
        }

        /// <summary>
        ///获取系统开机自启动目录
        ///[注:电脑登录账户]
        /// </summary>
        private string systemUserStartPath { get { return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Startup); } }

        /// <summary>
        /// 获取系统开机自启动目录
        /// [注意:所有账户,此文件夹下的操作会被系统拒绝]
        /// </summary>
        private string systemCommonStartPath { get { return System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonStartup); } }


        /// <summary>
        /// 目标程序完整路径
        /// [注:WinForm应用程序*.exe]
        /// </summary>
        private string destinationFilePath;
        public string DestinationFilePath
        {
            set { destinationFilePath = value; }
            get
            {
                if(System.IO.File.Exists(destinationFilePath))
                    return destinationFilePath;
                else
                {
                    return System.Windows.Forms.Application.ExecutablePath; 
                }
            }
        }

        /// <summary>
        /// 获取桌面目录
        /// </summary>
        private string desktopPath { get { return System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory); } }

        /// <summary>
        /// 设置开启自动启动
        /// [注:默认是开启]
        /// </summary>
        /// <param name="onOff"></param>
        public void SetAutoStart(bool onOff = true)
        {
            System.Collections.Generic.List<string> shortcutPaths = GetShortCutFromFolder(systemUserStartPath, DestinationFilePath);

            if (onOff)
            {
                if (shortcutPaths.Count >= 2)
                {
                    for (int i = 1; i < shortcutPaths.Count; i++)
                        DeleteFile(shortcutPaths[i]);
                }
                else if (shortcutPaths.Count == 0)
                {
                    CreateShortcut(systemUserStartPath, QuickName, DestinationFilePath, "自定义快捷方式");
                }
            }
            else
            {
                if (shortcutPaths.Count > 0)
                {
                    for (int i = 0; i < shortcutPaths.Count; i++)
                        DeleteFile(shortcutPaths[i]);
                }
            }
        }

        /// <summary>
        /// 向目标路径创建指定文件的快捷方式
        /// </summary>
        /// <param name="directory">快捷方式的目标目录</param>
        /// <param name="shortcutName">快捷方式名称</param>
        /// <param name="targetPath">快捷方式的指向文件路径</param>
        /// <param name="description">快捷方式的备注描述</param>
        /// <param name="iconLocation">快捷方式的图标路径</param>
        /// <returns></returns>
        private bool CreateShortcut(string directory, string shortcutName, string targetPath, string description = null, string iconLocation = null)
        {
            bool rt = false;
            try
            {
                //目录不存在，则创建目录
                if (!System.IO.Directory.Exists(directory)) System.IO.Directory.CreateDirectory(directory);

                string shortcutPath = System.IO.Path.Combine(directory, string.Format("{0}.lnk", shortcutName));

                //添加:Com 中搜索Windows Script Host Object Model
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);

                //快捷方式的目标路径
                shortcut.TargetPath = targetPath;
                //快捷方式的起始位置
                shortcut.WorkingDirectory = System.IO.Path.GetDirectoryName(targetPath);
                //快捷方式的运行窗口,默认为常规窗口
                shortcut.WindowStyle = 1;
                //快捷方式的备注描述
                shortcut.Description = description;
                //快捷方式的图标路径
                shortcut.IconLocation = string.IsNullOrEmpty(iconLocation) ? targetPath : iconLocation;

                shortcut.Save();
                rt = true;

            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (System.Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {

            }
            return rt;
        }

        /// <summary>
        /// 获取指定目录下指向目标路径的快捷方式
        /// </summary>
        /// <param name="directory">指定目录</param>
        /// <param name="targetPath">目标路径</param>
        /// <returns>指向目标文件路径的快捷方式集合</returns>
        private System.Collections.Generic.List<string> GetShortCutFromFolder(string directory, string targetPath)
        {
            System.Collections.Generic.List<string> shortcutList = new List<string>();
            shortcutList.Clear();

            string tempStr = string.Empty;
            string[] files = System.IO.Directory.GetFiles(directory, "*.lnk");
            if (files != null
                && files.Length >= 1)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    tempStr = GetAppPathFromShortCut(files[i]);
                    if (tempStr.Equals(targetPath))
                        shortcutList.Add(files[i]);
                }
            }
            return shortcutList;
        }

        /// <summary>
        /// 获取快捷方式指向的目标文件路径
        /// </summary>
        /// <param name="shortcutPath">快捷方式路径</param>
        /// <returns>快捷方式指向的目标文件路径</returns>
        private string GetAppPathFromShortCut(string shortcutPath)
        {
            if (System.IO.File.Exists(shortcutPath))
            {
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);

                return shortcut.TargetPath;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除文件
        /// [注:用于取消开机自动启动时，从计算机自动启动目录中删除应用程序的快捷方式]
        /// </summary>
        /// <param name="filePath"></param>
        private void DeleteFile(string filePath)
        {
            System.IO.FileAttributes attr = System.IO.File.GetAttributes(filePath);
            if (attr == System.IO.FileAttributes.Directory)
            {
                System.IO.Directory.Delete(filePath, true);
            }
            else { System.IO.File.Delete(filePath); }
        }      
        #endregion
    }

    /// <summary>
    /// 通用方法集合类
    /// </summary>
    public class Functions
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = true)]
        internal static extern void CopyMemory(int Destination, int Source, int Length);

        /// <summary>
        /// 初始化控件的Text属性
        /// </summary>
        /// <param name="control"></param>
        /// <param name="caption"></param>
        public static void InitControl(System.Windows.Forms.Control control, string caption)
        {
            if (control != null && caption != null)
            {
                control.Text = caption;
            }
            return;
        }

        /// <summary>
        /// 根据使用语言初始化控件
        /// </summary>
        /// <param name="Controls"></param>
        /// <param name="lang"></param>
        public static void InitControls(System.Windows.Forms.Control.ControlCollection Controls, Language lang)
        {
            bool isChs = (lang == ProCommon.Communal.Language.Chinese);

            foreach (System.Windows.Forms.Control control in Controls)
            {
                InitControl(control, isChs ? Properties.Resources.ResourceManager.GetString("chs_" + control.Tag) : Properties.Resources.ResourceManager.GetString("en_" + control.Tag));
                if (control.Controls.Count > 0) InitControls(control.Controls, lang);                
            }
            return;
        }

        /// <summary>
        /// 根据语言版本更新控件文本
        /// </summary>
        /// <param name="updateCtrlHdl"></param>
        /// <param name="ctrls"></param>
        /// <returns></returns>
        public static bool UpdateControlsText(ProCommon.Communal.UpdateCtrlWithLanguageHandler updateCtrlHdl, System.Windows.Forms.Control.ControlCollection ctrls)
        {
            bool rt = false;
            try
            {
                if(updateCtrlHdl!=null)
                {
                    foreach(System.Windows.Forms.Control ctrl in ctrls)
                    {
                        rt = updateCtrlHdl(ctrl);
                        if (!rt)
                            break;
                    }
                }
            }catch(System.Exception ex) { }
            return rt;
        }

        /// <summary>
        /// 根据语言版本更新控件文本
        /// </summary>
        /// <param name="updateCtrlHdl"></param>
        /// <param name="ctrls"></param>
        /// <returns></returns>
        public static bool UpdateControlsCaption(ProCommon.Communal.UpdateCtrlWithLanguageHandler updateCtrlHdl, System.Windows.Forms.Control.ControlCollection ctrls)
        {
            bool rt = false;
            try
            {
                if (updateCtrlHdl != null)
                {
                    foreach (System.Windows.Forms.Control ctrl in ctrls)
                    {
                        rt = updateCtrlHdl(ctrl);
                        if (!rt)
                            break;
                    }
                }
            }
            catch (System.Exception ex) { }
            return rt;
        }

        /// <summary>
        /// 方法：字节数组转换为整型指针
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static System.IntPtr BytesToIntptr(byte[] bytes)
        {
            int size = bytes.Length;
            System.IntPtr bfIntPtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(size);
            System.Runtime.InteropServices.Marshal.Copy(bytes, 0, bfIntPtr, size);
            return bfIntPtr;
        }

        /// <summary>
        /// 方法:十六进制字符串转十进制数
        /// </summary>
        /// <param name="hexstr"></param>
        /// <returns></returns>
        public static double HexToDec(string hexstr)
        {
            double rt = 0;
            string HEXSTR = hexstr.ToUpper();
            for (int i = 0; i < HEXSTR.Length; i++)
            {
                string temp = HEXSTR.Substring(HEXSTR.Length - i - 1, 1);//从右往左，低位到高位
                switch (temp)
                {
                    case "0":
                        rt += Math.Pow(16, i) * 0;
                        break;
                    case "1":
                        rt += Math.Pow(16, i) * 1;
                        break;
                    case "2":
                        rt += Math.Pow(16, i) * 2;
                        break;
                    case "3":
                        rt += Math.Pow(16, i) * 3;
                        break;
                    case "4":
                        rt += Math.Pow(16, i) * 4;
                        break;
                    case "5":
                        rt += Math.Pow(16, i) * 5;
                        break;
                    case "6":
                        rt += Math.Pow(16, i) * 6;
                        break;
                    case "7":
                        rt += Math.Pow(16, i) * 7;
                        break;
                    case "8":
                        rt += Math.Pow(16, i) * 8;
                        break;
                    case "9":
                        rt += Math.Pow(16, i) * 9;
                        break;
                    case "A":
                        rt += Math.Pow(16, i) * 10;
                        break;
                    case "B":
                        rt += Math.Pow(16, i) * 11;
                        break;
                    case "C":
                        rt += Math.Pow(16, i) * 12;
                        break;
                    case "D":
                        rt += Math.Pow(16, i) * 13;
                        break;
                    case "E":
                        rt += Math.Pow(16, i) * 14;
                        break;
                    case "F":
                        rt += Math.Pow(16, i) * 15;
                        break;
                }
            }
            return rt;
        }

        /// <summary>
        /// 方法:数据字符串高低位互换
        /// 注：4个字符作为一个有效数据
        /// </summary>
        /// <param name="str">数据字符串</param>
        /// <returns></returns>
        public static string[] ReverseHighLow(string str)
        {
            string[] rt = null;
            if (!string.IsNullOrEmpty(str))
            {
                int num = str.Length;
                if (num % 4 == 0)                                          //字符串字符个数是4的整数倍
                {
                    string[] tempstr = new string[(int)(num / 4)];
                    int k = 0;
                    for (int i = 0; i < (str.Length); i += 4)              //每四个字符作为一个数据
                    {
                        string H8 = str.Substring(2 + i, 2);               //提取返回字符串指定位置字符（高低位互换）高位
                        string L8 = str.Substring(i, 2);                   //提取返回字符串指定位置字符（高低位互换）低位
                        tempstr[k] = H8 + L8;                              //临时结果字符串
                        k += 1;
                    }

                    rt = tempstr;
                }
            }
            return rt;
        }

        /// <summary>
        /// 方法:字符串添加校验码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AddCheckCode(string str)
        {
            string r = "**";
            try
            {
                byte[] arrbyte = System.Text.ASCIIEncoding.ASCII.GetBytes(str);
                byte temp = arrbyte[0];
                for (int i = 1; i < arrbyte.Length; i++)
                {
                    temp ^= arrbyte[i];
                }

                r = Convert.ToString(temp, 16).PadLeft(2, '0').ToUpper();
            }
            catch { }
            return str + r;
        }

        /// <summary>
        /// 方法：边沿信号判断
        /// </summary>
        /// <param name="last"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public static TriggerLogic JudgeEdge(bool last, bool now)
        {
            TriggerLogic edge = TriggerLogic.NONE;
            if (!last)
            {
                if (now)
                    edge = TriggerLogic.RaiseEdge;
                else
                    edge = TriggerLogic.LogicFalse;
            }
            else
            {
                if (!now)
                    edge = TriggerLogic.FallEdge;
                else
                    edge = TriggerLogic.LogicTrue;
            }

            return edge;
        }

        /// <summary>
        /// 方法：获取byte[]中指定起始和长度的字节段
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GetSubData(byte[] data, int startIndex, int length)
        {
            byte[] ret = new byte[length];
            System.Array.Copy(data, startIndex, ret, 0, length);
            return ret;
        }

        /// <summary>
        /// 根据对话框指定的目录加载所有文件
        /// </summary>
        /// <returns></returns>
        public static System.IO.FileInfo[] GetFilesFromFolderWithDialog()
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folderPath = fbd.SelectedPath;
                System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(folderPath);
                if (dirInfo.GetFiles().Length + dirInfo.GetDirectories().Length == 0)
                {
                    System.Windows.Forms.MessageBox.Show("指定目录中无文件，需重新加载！", "提示信息", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return null;
                }
                else
                {
                    return dirInfo.GetFiles();
                }
            }
            return null;
        }

        /// <summary>
        /// 根据指定的目录加载所有文件
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static System.IO.FileInfo[] GetFilesFromDirectoryPath(string directoryPath)
        {
            System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(directoryPath);
            if (dirInfo.GetFiles().Length + dirInfo.GetDirectories().Length == 0)
            {
                return null;
            }
            else
            {
                return dirInfo.GetFiles();
            }
        }      

        /// <summary>
        /// 根据Int类型的值,返回用1或0(对应true或false)填充的数组
        /// 注:从右侧开始向左索引(0~31)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static System.Collections.Generic.IEnumerable<bool> GetBitList(int value)
        {
            var list = new System.Collections.Generic.List<bool>(32);
            for (var i = 0; i <= 31; i++)
            {
                var val = 1 << i;
                list.Add((value & val) == val);
            }

            return list;
        }

        /// <summary>
        /// 返回Int数据中某一位是否为1
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index">
        /// 32位数据的从右向左的偏移位索引(0~31)</param>
        /// <returns>位置值
        /// true,1
        /// false,0</returns>
        public static bool GetBitValue(int value, ushort index)
        {
            if (index > 31) throw new ArgumentOutOfRangeException("index");

            var val = 1 << index;
            return ((value & val) == val);
        }

        /// <summary>
        /// 设置Int数据中的某一位的值
        /// </summary>
        /// <param name="value">位设置前的值</param>
        /// <param name="index">
        /// 32位数据的从右向左的偏移位索引(0~31)</param>
        /// <param name="bitValue">设置值
        /// true,设置1
        /// false,设置0</param>
        /// <returns>返回位设置后的值</returns>
        public static int SetBitValue(int value, ushort index, bool bitValue)
        {
            if (index > 31) throw new ArgumentOutOfRangeException("index");
            var val = 1 << index;
            return bitValue ? (value | val) : (value & ~val);
        }

        /// <summary>
        /// 根据索引获得输入变量
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public static ProCommon.Communal.InVar GetInVarByIdx(int idx)
        {
            if(idx==-1)
            {
                return ProCommon.Communal.InVar.NONE;
            }
            else
            {
                foreach (ProCommon.Communal.InVar itm in Enum.GetValues(typeof(ProCommon.Communal.InVar)))
                {
                    int i = Convert.ToInt32(itm);
                    if (i == idx)
                        return itm;
                }
                return ProCommon.Communal.InVar.NONE;
            }            
        }

        /// <summary>
        /// 根据输入变量获取索引
        /// </summary>
        /// <param name="invar"></param>       
        /// <returns></returns>
        public static int GetIndexByInVar(ProCommon.Communal.InVar invar)
        {
            if (ProCommon.Communal.InVar.NONE == invar)
            {
                return -1;
            }
            else { return Convert.ToInt32(invar); }          
        }

        /// <summary>
        /// 根据索引获取输出变量
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public static ProCommon.Communal.OutVar GetOutVarByIdx(int idx)
        {                    
            foreach(ProCommon.Communal.OutVar itm in Enum.GetValues(typeof(ProCommon.Communal.OutVar)))
            {
                int i = Convert.ToInt32(itm);
                if (i == idx)
                    return itm;
            }

            return ProCommon.Communal.OutVar.NONE;
        }

        /// <summary>
        /// 根据输出变量获取索引
        /// </summary>
        /// <param name="outVar"></param>       
        /// <returns></returns>
        public static int GetIndexByOutVar(ProCommon.Communal.OutVar outVar)
        {
            if (ProCommon.Communal.OutVar.NONE == outVar)
            {
                return -1;
            }
            else { return Convert.ToInt32(outVar); }
        }

        /// <summary>
        /// 根据索引获取信号触发逻辑
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public static ProCommon.Communal.TriggerLogic GetTriggerLogicByIdx(int idx)
        {
            if(idx==-1)
            {
                return ProCommon.Communal.TriggerLogic.NONE;
            }
            else
            {
                foreach(ProCommon.Communal.TriggerLogic itm in Enum.GetValues(typeof(ProCommon.Communal.TriggerLogic)))
                {
                    int i = Convert.ToInt32(itm);
                    if (i == idx)
                        return itm;
                }
                return ProCommon.Communal.TriggerLogic.NONE;
            }
        }

        /// <summary>
        /// 根据有效逻辑获取索引
        /// </summary>
        /// <param name="statusEdge"></param>
        /// <param name="staEdgeNm"></param>
        /// <returns></returns>
        public static int GetIndexByTriggerLogic(ProCommon.Communal.TriggerLogic triggerLogic)
        {
           if(ProCommon.Communal.TriggerLogic.NONE== triggerLogic)
            {
                return -1;
            }
            else
            {
                return Convert.ToInt32(triggerLogic);
            }
        }

       public static System.Reflection.PropertyInfo[] GetProperties<T>(T t)
        {
            System.Reflection.PropertyInfo[] properties =
                t.GetType().GetProperties(System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.Public);
            return properties;
        }

        /// <summary>
        /// 轴物理位置转换为脉冲数
        /// </summary>
        /// <param name="axis">轴</param>
        /// <param name="position">轴的物理位置</param>
        /// <returns></returns>
        public static int TransferPhysicalPositionToPulse(ProCommon.Communal.Axis axis,float position)
        {
            int pls = 0;
            if(axis!=null)
            {
                axis.PulseUnit = axis.PulsePerRound / axis.HelicalPitch;
                pls = Convert.ToInt32(axis.PulseUnit * position);
            }
            return pls;
        }

        /// <summary>
        /// 轴脉冲位数转换为物理位置
        /// </summary>
        /// <param name="axis">轴</param>
        /// <param name="pulse">轴脉冲数</param>
        /// <returns></returns>
        public static float TransferPulseToPhysicalPosition(ProCommon.Communal.Axis axis, int pulse)
        {
            float pos = 0.0f;
            if (axis != null)
            {
                axis.PulseUnit = axis.PulsePerRound / axis.HelicalPitch;
                pos = Convert.ToSingle((float)pulse / axis.PulseUnit);
            }
            return pos;
        }

        /// <summary>
        /// 轴角位置转换为脉冲数
        /// </summary>
        /// <param name="axis">轴</param>
        /// <param name="angle">轴角位置(度)</param>
        /// <returns></returns>
        public static int TransferAnglePositionToPulse(ProCommon.Communal.Axis axis, float angle)
        {
            int pls = 0;
            if (axis != null)
            {
                axis.PulseAngleUnit = axis.PulsePerRound / 360.0f;
                pls = Convert.ToInt32(axis.PulseAngleUnit*angle);
            }
            return pls;
        }

        public static float TransferPulseToAnglePosition(ProCommon.Communal.Axis axis, int pulse)
        {
            float pos = 0.0f;
            if (axis != null)
            {
                axis.PulseAngleUnit = axis.PulsePerRound / 360.0f;
                pos = Convert.ToSingle((float)pulse / axis.PulseAngleUnit);
            }
            return pos;
        }
    }
       
}
