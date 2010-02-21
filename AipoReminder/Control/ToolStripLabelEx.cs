using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AipoReminder.Control
{
    class ToolStripLabelEx : ToolStripLabel
    {
        /// <summary>
        /// タイマー
        /// </summary>
        private Timer timer;

        public ToolStripLabelEx() : base()
        {
            timer = new Timer();
            timer.Interval = 5000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = false;
        }

        /// <summary>
        /// ステータスバーに文字を表示する
        /// </summary>
        /// <param name="text">表示文字</param>
        public void DisplayMessage(string text)
        {
            this.Text = text;
//            timer.Enabled = true;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Text = "";
            timer.Stop();
        }
    }
}
