using System;
using System.Text;
using System.Text.RegularExpressions;

namespace WinFramework.Utility
{
    public static class Validator
    {
        public static int UNINPUT = 1;
        public static int LENGTH = 10;
        public static int HALF_ALPHA = 100;
        public static int HALF_ALPHA_NUM_SYMBOL = 200;
        public static int HALF_NUM = 300;
        public static int URL = 400;

        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <param name="value">チェック対象</param>
        /// <param name="isMust">必須かどうか　(true:必須)</param>
        /// <param name="length">桁数</param>
        /// <param name="type">文字種　(ALPHA、ALPHA_NUM、NUM)</param>
        /// <returns></returns>
        public static int InputCheck(string value, bool isMust, int length, int type)
        {
            // 必須かどうか
            if (isMust)
            {
                // 必須
                if (String.IsNullOrEmpty(value))
                {
                    // 未入力
                    return UNINPUT;
                }
            }

            // 桁数チェック
            if (value.Length > length)
            {
                return LENGTH;
            }

            Regex regex = null;
            if (type == HALF_ALPHA) 
            {
                // a-zA-Z のみの文字列
                regex = new Regex(@"^[a-zA-Z]+$");

                if (!regex.IsMatch(value))
                {
                    return HALF_ALPHA;
                }
            }
            else if (type == HALF_ALPHA_NUM_SYMBOL)
            {
                // ! から ~ までの文字列
                // http://e-words.jp/p/r-ascii.html
                regex = new Regex(@"^[!-~]+$");
                if (!regex.IsMatch(value))
                {
                    return HALF_ALPHA_NUM_SYMBOL;
                }
            }
            else if (type == HALF_NUM)
            {
                // 0-9 のみの文字列
                regex = new Regex(@"^[0-9]+$");
                if (!regex.IsMatch(value))
                {
                    return HALF_NUM;
                }
            }
            else if (type == URL)
            {
                regex = new Regex(@"^http");
                if (!regex.IsMatch(value))
                {
                    return URL;
                }
            }

            return 0;
        }
    }
}
