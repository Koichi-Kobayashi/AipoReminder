using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using AipoReminder.ValueObject;
using System.Diagnostics;

namespace AipoReminder.Utility
{
    class RegistryUtility
    {
        private static string Sleipnir = "Sleipnir";
        private static string Firefox = "Firefox";
        private static string Lunascape = "Lunascape";
        private static string Opera = "Opera";
        private static string Safari = "Safari";

        public List<ComboBoxBrowserItem> getBrowserComboItem()
        {
            List<ComboBoxBrowserItem> listSoftware = new List<ComboBoxBrowserItem>();
            string baseKey = @"Software\Microsoft\Windows\CurrentVersion\Uninstall\";
            string rGetValueName = "DisplayName";
            string insPath = "InstallLocation";

            RegistryKey rKey = Registry.LocalMachine.OpenSubKey(baseKey);

            foreach (string keyName in rKey.GetSubKeyNames())
            {
                RegistryKey rKey2 = Registry.LocalMachine.OpenSubKey(baseKey + keyName);
                if (rKey2.GetValue(rGetValueName) != null)
                {
                    string name = rKey2.GetValue(rGetValueName).ToString();
                    if (rKey2.GetValue(insPath) != null)
                    {
                        string rName = rKey2.GetValue(rGetValueName).ToString();
                        if (rName.IndexOf(Sleipnir) != -1)
                        {
                            string path = rKey2.GetValue(insPath).ToString();
                            path += @"\bin\Sleipnir.exe";
                            FileVersionInfo vi = FileVersionInfo.GetVersionInfo(path);
                            ComboBoxBrowserItem item = new ComboBoxBrowserItem(vi.ProductName + " " + vi.ProductVersion, path);
                            listSoftware.Add(item);
                            continue;
                        }

                        if (rName.IndexOf(Firefox) != -1)
                        {
                            string path = rKey2.GetValue(insPath).ToString();
                            path += @"\firefox.exe";
                            FileVersionInfo vi = FileVersionInfo.GetVersionInfo(path);
                            ComboBoxBrowserItem item = new ComboBoxBrowserItem(vi.ProductName + " " + vi.ProductVersion, path);
                            listSoftware.Add(item);
                            continue;
                        }

                        if (rName.IndexOf(Lunascape) != -1)
                        {
                            string path = rKey2.GetValue(insPath).ToString();
                            path += @"\Luna.exe";
                            FileVersionInfo vi = FileVersionInfo.GetVersionInfo(path);
                            ComboBoxBrowserItem item = new ComboBoxBrowserItem(vi.ProductName + " " + vi.ProductVersion, path);
                            listSoftware.Add(item);
                            continue;
                        }

                        if (rName.IndexOf(Opera) != -1)
                        {
                            string path = rKey2.GetValue(insPath).ToString();
                            path += @"\Opera.exe";
                            FileVersionInfo vi = FileVersionInfo.GetVersionInfo(path);
                            ComboBoxBrowserItem item = new ComboBoxBrowserItem(vi.ProductName + " " + vi.ProductVersion, path);
                            listSoftware.Add(item);
                            continue;
                        }

                        //if (rName.IndexOf(Safari) != -1)
                        //{
                        //    string path = rKey2.GetValue(insPath).ToString();
                        //    path += @"\Safari.exe";
                        //    ComboBoxBrowserItem item = new ComboBoxBrowserItem(Safari, path);
                        //    listSoftware.Add(item);
                        //    continue;
                        //}
                    }
                }
                rKey2.Close();
            }
            rKey.Close();
            listSoftware.Sort();
            return listSoftware;
        }
    }
}
