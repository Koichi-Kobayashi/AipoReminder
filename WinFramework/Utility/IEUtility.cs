using System;

namespace WinFramework.Utility
{
    public class IEUtility
    {
        public static string GetInternetExplorerVersion()
        {
            string rKeyName = @"SOFTWARE\Microsoft\Internet Explorer";
            string rValueName = "Version";

            try
            {
                Microsoft.Win32.RegistryKey rKey =
                    Microsoft.Win32.Registry.LocalMachine.OpenSubKey(rKeyName);
                string sVersion = (string)rKey.GetValue(rValueName);
                rKey.Close();

                return sVersion;
            }
            catch (NullReferenceException)
            {
                return "Unknown";
            }
        }
    }
}
