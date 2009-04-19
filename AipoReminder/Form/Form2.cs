using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AipoReminder
{
    public partial class Form2 : Form
    {
        /// <summary>
        /// スケジュールの内容を取得、設定するためのプロパティ
        /// </summary>
        public string TextBoxScheduleInfoText
        {
            get
            {
                return textBoxScheduleInfo.Text;
            }
            set
            {
                textBoxScheduleInfo.Text = value;
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
