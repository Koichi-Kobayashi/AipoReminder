using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using AipoReminder.Utility;
using WinFramework.Utility;

namespace AipoReminder.Manager
{
    class ThreadingManager
    {
        public ThreadingManager()
        {
        }

        public void Run()
        {
            if (SettingManager.AutoLogin)
            {
                // 自動ログイン用のファイルを作成
                string fullPath = Path.GetTempPath();
                string fileName = Path.GetRandomFileName();
                fullPath = fullPath + fileName;
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

                // 一時停止
                Thread.Sleep(20000);

                // 後始末
                File.Delete(changeFileName);
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

            info.FileName = RegistryUtility.GetDefaultBrowserExePath();
            info.WorkingDirectory = Path.GetDirectoryName(RegistryUtility.GetDefaultBrowserExePath());
            info.Arguments = arg;
            p = Process.Start(info);
        }
    }
}
