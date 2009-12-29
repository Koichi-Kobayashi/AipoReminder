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
        private List<ScheduleItem> _scheduleList = new List<ScheduleItem>();

        /// <summary>
        /// スケジュールの内容を取得、設定するためのプロパティ
        /// </summary>
        public List<ScheduleItem> ScheduleList
        {
            get
            {
                return _scheduleList;
            }
            set
            {
                _scheduleList = value;
                StringBuilder sb = new StringBuilder();
                foreach (ScheduleItem item in _scheduleList)
                {
                    if (!String.IsNullOrEmpty(textBoxScheduleInfo.Text))
                    {
                        sb.AppendLine("");
                    }
                    sb.Append(item.ToString());
                }
                textBoxScheduleInfo.Text = sb.ToString();
            }
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
