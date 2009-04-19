namespace AipoReminder
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.textBoxScheduleInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxScheduleInfo
            // 
            this.textBoxScheduleInfo.BackColor = System.Drawing.Color.White;
            this.textBoxScheduleInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxScheduleInfo.Location = new System.Drawing.Point(12, 24);
            this.textBoxScheduleInfo.Multiline = true;
            this.textBoxScheduleInfo.Name = "textBoxScheduleInfo";
            this.textBoxScheduleInfo.ReadOnly = true;
            this.textBoxScheduleInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxScheduleInfo.Size = new System.Drawing.Size(251, 105);
            this.textBoxScheduleInfo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "もうすぐ開始する予定です。";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(100, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "閉じる(&C)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 170);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxScheduleInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.Text = "予定の通知";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxScheduleInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}