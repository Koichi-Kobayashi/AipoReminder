using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using AipoReminder.Utility;
using WinFramework.Utility;
using System.Reflection;

namespace AipoReminder.Manager
{
    class ThreadingManager
    {
        private string path = "";

        public ThreadingManager()
        {
        }

        public void setBrowserPath(string path)
        {
            this.path = path;
        }

        public void Run()
        {
            if (SettingManager.AutoLogin)
            {
                // 自動ログイン用のファイルを作成
                string fullPath = Path.GetTempPath();
                fullPath += @"AipoReminder\";
                string fileName = Path.GetRandomFileName();
                fullPath += fileName;
                string changeFileName = Path.ChangeExtension(fullPath, ".html");

                // Shift-Jisでファイルを作成
                StreamWriter sw = new StreamWriter(changeFileName,
                                                   true,
                                                   Encoding.GetEncoding("Shift_Jis"));

                sw.WriteLine(SettingManager.AutoLoginHtml);

                // ストリームを閉じる
                sw.Close();

                // ブラウザで表示
                this.OpenUrl("\"" + changeFileName + "\"");
            }
            else
            {
                // ブラウザで表示
                this.OpenUrl("");
            }
        }

        /// <summary>
        /// 指定のURLを標準ブラウザで開く
        /// </summary>
        /// <param name="targetUrl"></param>
        private void OpenUrl(String targetUrl)
        {
            string arg = "";

            if (String.IsNullOrEmpty(targetUrl))
            {
                arg = SettingManager.Url;
            }
            else
            {
                arg = targetUrl;
            }

            ProcessStartInfo info = new ProcessStartInfo();
            Process p = null;

            if ("".Equals(path))
            {
                info.FileName = WinFramework.Utility.RegistryUtility.GetDefaultBrowserExePath();
                info.WorkingDirectory = Path.GetDirectoryName(WinFramework.Utility.RegistryUtility.GetDefaultBrowserExePath());
            }
            else
            {
                info.FileName = path;
                info.WorkingDirectory = Path.GetDirectoryName(path);
            }
            info.Arguments = arg;
            p = Process.Start(info);
            p.WaitForInputIdle(60000);
        }
    }
}
