using System;
using System.Security.Cryptography;
using System.Text;

namespace WinFramework.Utility
{
    public static class Security
    {
        //暗号化で使用する追加のバイト配列
        public static byte[] entropy;

        /// <summary>
        /// 初期化
        /// </summary>
        static Security()
        {
            Security.entropy = new byte[] { 0x72, 0xa2, 0x12, 0x04 };
        }

        /// <summary>
        /// パスワード文字列のSAH1によるダイジェストをBase64エンコードする
        /// </summary>
        /// <param name="password">パスワード</param>
        /// <returns></returns>
        public static string EncryptPassword(string password)
        {

            if (String.IsNullOrEmpty(password))
            {
                return null;
            }

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] source = Encoding.UTF8.GetBytes(password);
            byte[] result = sha1.ComputeHash(source);
            string digest = Convert.ToBase64String(result);

            return digest;

        }

        /// <summary>
        /// ProtectedDataを使って暗号化する
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EncryptData(string value)
        {
            //文字列をバイト型配列に変換
            byte[] userData = Encoding.UTF8.GetBytes(value);

            //暗号化する
            byte[] encryptedData = ProtectedData.Protect(
                userData, Security.entropy,
                DataProtectionScope.CurrentUser);

            //暗号化されたデータを文字列に変換
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// ProtectedDataを使って暗号化されたデータを復元する
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnEncryptData(string value)
        {
            //文字列を暗号化されたデータに戻す
            byte[] encryptedData = Convert.FromBase64String(value);

            //復号化する
            byte[] userData = ProtectedData.Unprotect(
                encryptedData, Security.entropy,
                DataProtectionScope.CurrentUser);

            //復号化されたデータを文字列に変換
            return Encoding.UTF8.GetString(userData);
        }
    }
}
