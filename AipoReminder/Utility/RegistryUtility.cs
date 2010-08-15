using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using AipoReminder.ValueObject;

namespace AipoReminder.Utility
{
    class RegistryUtility
    {
        private static string Sleipnir = "Sleipnir";
        private static string Firefox = "Firefox";
        private static string Lunascape = "Lunascape";

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
                            ComboBoxBrowserItem item = new ComboBoxBrowserItem(Sleipnir, path);
                            listSoftware.Add(item);
                            continue;
                        }

                        if (rName.IndexOf(Firefox) != -1)
                        {
                            string path = rKey2.GetValue(insPath).ToString();
                            path += @"\firefox.exe";
                            ComboBoxBrowserItem item = new ComboBoxBrowserItem(Firefox, path);
                            listSoftware.Add(item);
                            continue;
                        }

                        if (rName.IndexOf(Lunascape) != -1)
                        {
                            string path = rKey2.GetValue(insPath).ToString();
                            path += @"\Luna.exe";
                            ComboBoxBrowserItem item = new ComboBoxBrowserItem(Lunascape, path);
                            listSoftware.Add(item);
                            continue;
                        }
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
