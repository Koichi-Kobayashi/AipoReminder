using System;
using System.Text;

namespace WinFramework.Exceptions
{
    /// <summary>
    /// DB関連の例外クラス
    /// </summary>
    public class DBException : Exception
    {
        string msg;
        Exception ex;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="msg">エラー内容</param>
        public DBException(string msg)
        {
            this.msg = msg;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="msg">エラー内容</param>
        /// <param name="ex">InnerException</param>
        public DBException(string msg, Exception ex)
        {
            this.msg = msg;
            this.ex = ex;
        }

        /// <summary>
        /// エラー内容を出力
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            StringBuilder sb = new StringBuilder();

            if (!String.IsNullOrEmpty(this.msg))
            {
                sb.AppendLine(this.msg);
            }

            if (this.ex != null)
            {
                sb.AppendLine(this.ex.ToString());
            }

            return sb.ToString();
        }
    }
}
