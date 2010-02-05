using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AipoReminder.ValueObject;

namespace AipoReminder
{
    public partial class Form2 : Form
    {
        private List<ScheduleItem> oneDayScheduleList = new List<ScheduleItem>();
        private List<ScheduleItem> scheduleList = new List<ScheduleItem>();

        private StringBuilder sbOneDaySchedule = new StringBuilder();
        private StringBuilder sbSchedule = new StringBuilder(); 

        /// <summary>
        /// 終日スケジュールの内容を取得、設定するためのプロパティ
        /// </summary>
        public List<ScheduleItem> OneDayScheduleList
        {
            get
            {
                return oneDayScheduleList;
            }
            set
            {
                oneDayScheduleList = value;
                StringBuilder sb = new StringBuilder();
                foreach (ScheduleItem item in oneDayScheduleList)
                {
                    if (!String.IsNullOrEmpty(sb.ToString()))
                    {
                        sb.AppendLine("");
                    }
                    sb.Append(item.ToString());
                }
                sbOneDaySchedule = sb;
            }
        }

        /// <summary>
        /// スケジュールの内容を取得、設定するためのプロパティ
        /// </summary>
        public List<ScheduleItem> ScheduleList
        {
            get
            {
                return scheduleList;
            }
            set
            {
                scheduleList = value;
                StringBuilder sb = new StringBuilder();
                foreach (ScheduleItem item in scheduleList)
                {
                    if (!String.IsNullOrEmpty(sb.ToString()))
                    {
                        sb.AppendLine("");
                    }
                    sb.Append(item.ToString());
                }
                sbSchedule = sb;
            }
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (sbOneDaySchedule.Length != 0)
            {
                string str = "■■□  終日スケジュール  □■■";
                richTextBoxScheduleInfo.AppendText(str);
                richTextBoxScheduleInfo.Select(0, str.Length);
//                richTextBoxScheduleInfo.SelectionBackColor = Color.BurlyWood;
                richTextBoxScheduleInfo.AppendText("\n");
                richTextBoxScheduleInfo.AppendText(sbOneDaySchedule.ToString());
                richTextBoxScheduleInfo.AppendText("\n");
            }

            if (sbSchedule.Length != 0)
            {
                int len = richTextBoxScheduleInfo.TextLength;
                string str = "■■□  もうすぐ始まるスケジュール  □■■";
                richTextBoxScheduleInfo.AppendText(str);
                richTextBoxScheduleInfo.Select(len, str.Length);
//                richTextBoxScheduleInfo.SelectionBackColor = Color.AliceBlue;
                richTextBoxScheduleInfo.AppendText("\n");
                richTextBoxScheduleInfo.AppendText(sbSchedule.ToString());
            }

            this.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
