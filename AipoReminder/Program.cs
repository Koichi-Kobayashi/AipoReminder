using System;
using System.Windows.Forms;

namespace AipoReminder
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Mutexクラスの作成
            System.Threading.Mutex m = new System.Threading.Mutex(false, "AipoReminder");
            // ミューテックスの所有権を要求する
            if (m.WaitOne(0, false) == false)
            {
                //すでに起動していると判断する
                MessageBox.Show("多重起動はできません。");
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }

            // アプリケーションが終わるまでmへの参照を維持するようにする
            GC.KeepAlive(m);
        }
    }
}
