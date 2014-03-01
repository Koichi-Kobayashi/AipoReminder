using System;
using Microsoft.Win32;

namespace WinFramework.Utility
{
    public class RegistryUtility
    {
        public static string GetDefaultBrowserExePath()
        {
            return _GetDefaultExePath(@"http\shell\open\command");
        }

        private static string _GetDefaultExePath(string keyPath)
        {
            string path = "";

            // レジストリ・キーを開く
            // 「HKEY_CLASSES_ROOT\xxxxx\shell\open\command」
            RegistryKey rKey = Registry.ClassesRoot.OpenSubKey(keyPath);
            if (rKey != null)
            {
                // レジストリの値を取得する
                string command = (string)rKey.GetValue(String.Empty);
                if (command == null)
                {
                    return path;
                }

                // 前後の余白を削る
                command = command.Trim();
                if (command.Length == 0)
                {
                    return path;
                }

                // 「"」で始まる長いパス形式かどうかで処理を分ける
                if (command[0] == '"')
                {
                    // 「"～"」間の文字列を抽出
                    int endIndex = command.IndexOf('"', 1);
                    if (endIndex != -1)
                    {
                        // 抽出開始を「1」ずらす分、長さも「1」引く
                        path = command.Substring(1, endIndex - 1);
                    }
                }
                else
                {
                    // 「（先頭）～（スペース）」間の文字列を抽出
                    int endIndex = command.IndexOf(' ');
                    if (endIndex != -1)
                    {
                        path = command.Substring(0, endIndex);
                    }
                    else
                    {
                        path = command;
                    }
                }
            }

            return path;
        }
    }
}
