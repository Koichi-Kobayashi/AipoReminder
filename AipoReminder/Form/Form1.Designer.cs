﻿namespace AipoReminder
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonDataReset = new System.Windows.Forms.Button();
            this.buttonDataSave = new System.Windows.Forms.Button();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDbName = new System.Windows.Forms.TextBox();
            this.textBoxDbPassword = new System.Windows.Forms.TextBox();
            this.textBoxServerPort = new System.Windows.Forms.TextBox();
            this.textBoxDbUserId = new System.Windows.Forms.TextBox();
            this.textBoxServerIP = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBoxVersionInfo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxSystemInfo5 = new System.Windows.Forms.TextBox();
            this.textBoxSystemInfo4 = new System.Windows.Forms.TextBox();
            this.textBoxSystemInfo3 = new System.Windows.Forms.TextBox();
            this.textBoxSystemInfo2 = new System.Windows.Forms.TextBox();
            this.textBoxSystemInfo1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.timerWhatsnew = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerTasktrayIcon = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxMemo = new System.Windows.Forms.CheckBox();
            this.checkBoxWorkflow = new System.Windows.Forms.CheckBox();
            this.checkBoxSchedule = new System.Windows.Forms.CheckBox();
            this.checkBoxMsgboard = new System.Windows.Forms.CheckBox();
            this.checkBoxBlogComment = new System.Windows.Forms.CheckBox();
            this.checkBoxBlog = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxCheckTime = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBoxAutoRun = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoLogin = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonInfoSettings = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(378, 220);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FloralWhite;
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.buttonDataReset);
            this.tabPage1.Controls.Add(this.buttonDataSave);
            this.tabPage1.Controls.Add(this.textBoxUserName);
            this.tabPage1.Controls.Add(this.textBoxURL);
            this.tabPage1.Controls.Add(this.textBoxPassword);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(370, 195);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "アカウント設定";
            // 
            // buttonDataReset
            // 
            this.buttonDataReset.Location = new System.Drawing.Point(187, 157);
            this.buttonDataReset.Name = "buttonDataReset";
            this.buttonDataReset.Size = new System.Drawing.Size(75, 23);
            this.buttonDataReset.TabIndex = 8;
            this.buttonDataReset.Text = "リセット";
            this.buttonDataReset.UseVisualStyleBackColor = true;
            this.buttonDataReset.Click += new System.EventHandler(this.buttonDataReset_Click);
            // 
            // buttonDataSave
            // 
            this.buttonDataSave.Location = new System.Drawing.Point(108, 157);
            this.buttonDataSave.Name = "buttonDataSave";
            this.buttonDataSave.Size = new System.Drawing.Size(75, 23);
            this.buttonDataSave.TabIndex = 7;
            this.buttonDataSave.Text = "設定";
            this.buttonDataSave.UseVisualStyleBackColor = true;
            this.buttonDataSave.Click += new System.EventHandler(this.buttonDataSave_Click);
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(108, 56);
            this.textBoxUserName.MaxLength = 64;
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(121, 19);
            this.textBoxUserName.TabIndex = 2;
            // 
            // textBoxURL
            // 
            this.textBoxURL.Location = new System.Drawing.Point(108, 106);
            this.textBoxURL.MaxLength = 2083;
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.Size = new System.Drawing.Size(202, 19);
            this.textBoxURL.TabIndex = 6;
            this.textBoxURL.Leave += new System.EventHandler(this.textBoxURL_Leave);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(108, 81);
            this.textBoxPassword.MaxLength = 64;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(121, 19);
            this.textBoxPassword.TabIndex = 4;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "ログイン名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "URL：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "パスワード：";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FloralWhite;
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.textBoxDbName);
            this.tabPage2.Controls.Add(this.textBoxDbPassword);
            this.tabPage2.Controls.Add(this.textBoxServerPort);
            this.tabPage2.Controls.Add(this.textBoxDbUserId);
            this.tabPage2.Controls.Add(this.textBoxServerIP);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(370, 195);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "DB設定";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "データベース：";
            // 
            // textBoxDbName
            // 
            this.textBoxDbName.Location = new System.Drawing.Point(108, 154);
            this.textBoxDbName.Name = "textBoxDbName";
            this.textBoxDbName.Size = new System.Drawing.Size(121, 19);
            this.textBoxDbName.TabIndex = 10;
            // 
            // textBoxDbPassword
            // 
            this.textBoxDbPassword.Location = new System.Drawing.Point(108, 129);
            this.textBoxDbPassword.Name = "textBoxDbPassword";
            this.textBoxDbPassword.PasswordChar = '*';
            this.textBoxDbPassword.Size = new System.Drawing.Size(121, 19);
            this.textBoxDbPassword.TabIndex = 8;
            // 
            // textBoxServerPort
            // 
            this.textBoxServerPort.Location = new System.Drawing.Point(108, 79);
            this.textBoxServerPort.Name = "textBoxServerPort";
            this.textBoxServerPort.Size = new System.Drawing.Size(121, 19);
            this.textBoxServerPort.TabIndex = 4;
            // 
            // textBoxDbUserId
            // 
            this.textBoxDbUserId.Location = new System.Drawing.Point(108, 104);
            this.textBoxDbUserId.Name = "textBoxDbUserId";
            this.textBoxDbUserId.Size = new System.Drawing.Size(121, 19);
            this.textBoxDbUserId.TabIndex = 6;
            // 
            // textBoxServerIP
            // 
            this.textBoxServerIP.Location = new System.Drawing.Point(108, 54);
            this.textBoxServerIP.Name = "textBoxServerIP";
            this.textBoxServerIP.Size = new System.Drawing.Size(121, 19);
            this.textBoxServerIP.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(61, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "ポート：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "パスワード：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "ユーザID：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "サーバIP：";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FloralWhite;
            this.tabPage3.Controls.Add(this.textBoxVersionInfo);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.pictureBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(370, 195);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "バージョン情報";
            // 
            // textBoxVersionInfo
            // 
            this.textBoxVersionInfo.BackColor = System.Drawing.Color.FloralWhite;
            this.textBoxVersionInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxVersionInfo.Location = new System.Drawing.Point(79, 27);
            this.textBoxVersionInfo.Name = "textBoxVersionInfo";
            this.textBoxVersionInfo.ReadOnly = true;
            this.textBoxVersionInfo.Size = new System.Drawing.Size(271, 12);
            this.textBoxVersionInfo.TabIndex = 0;
            this.textBoxVersionInfo.Text = "Aipoリマインダー";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxSystemInfo5);
            this.groupBox1.Controls.Add(this.textBoxSystemInfo4);
            this.groupBox1.Controls.Add(this.textBoxSystemInfo3);
            this.groupBox1.Controls.Add(this.textBoxSystemInfo2);
            this.groupBox1.Controls.Add(this.textBoxSystemInfo1);
            this.groupBox1.Location = new System.Drawing.Point(14, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 120);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "システム情報";
            // 
            // textBoxSystemInfo5
            // 
            this.textBoxSystemInfo5.BackColor = System.Drawing.Color.FloralWhite;
            this.textBoxSystemInfo5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSystemInfo5.Location = new System.Drawing.Point(20, 88);
            this.textBoxSystemInfo5.Name = "textBoxSystemInfo5";
            this.textBoxSystemInfo5.ReadOnly = true;
            this.textBoxSystemInfo5.Size = new System.Drawing.Size(310, 12);
            this.textBoxSystemInfo5.TabIndex = 2;
            this.textBoxSystemInfo5.Text = "Aipoリマインダー";
            // 
            // textBoxSystemInfo4
            // 
            this.textBoxSystemInfo4.BackColor = System.Drawing.Color.FloralWhite;
            this.textBoxSystemInfo4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSystemInfo4.Location = new System.Drawing.Point(20, 61);
            this.textBoxSystemInfo4.Name = "textBoxSystemInfo4";
            this.textBoxSystemInfo4.ReadOnly = true;
            this.textBoxSystemInfo4.Size = new System.Drawing.Size(310, 12);
            this.textBoxSystemInfo4.TabIndex = 2;
            this.textBoxSystemInfo4.Text = "Aipoリマインダー";
            // 
            // textBoxSystemInfo3
            // 
            this.textBoxSystemInfo3.BackColor = System.Drawing.Color.FloralWhite;
            this.textBoxSystemInfo3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSystemInfo3.Location = new System.Drawing.Point(44, 48);
            this.textBoxSystemInfo3.Name = "textBoxSystemInfo3";
            this.textBoxSystemInfo3.ReadOnly = true;
            this.textBoxSystemInfo3.Size = new System.Drawing.Size(286, 12);
            this.textBoxSystemInfo3.TabIndex = 2;
            this.textBoxSystemInfo3.Text = "Aipoリマインダー";
            // 
            // textBoxSystemInfo2
            // 
            this.textBoxSystemInfo2.BackColor = System.Drawing.Color.FloralWhite;
            this.textBoxSystemInfo2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSystemInfo2.Location = new System.Drawing.Point(20, 35);
            this.textBoxSystemInfo2.Name = "textBoxSystemInfo2";
            this.textBoxSystemInfo2.ReadOnly = true;
            this.textBoxSystemInfo2.Size = new System.Drawing.Size(310, 12);
            this.textBoxSystemInfo2.TabIndex = 1;
            this.textBoxSystemInfo2.Text = "Aipoリマインダー";
            // 
            // textBoxSystemInfo1
            // 
            this.textBoxSystemInfo1.BackColor = System.Drawing.Color.FloralWhite;
            this.textBoxSystemInfo1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSystemInfo1.Location = new System.Drawing.Point(20, 22);
            this.textBoxSystemInfo1.Name = "textBoxSystemInfo1";
            this.textBoxSystemInfo1.ReadOnly = true;
            this.textBoxSystemInfo1.Size = new System.Drawing.Size(310, 12);
            this.textBoxSystemInfo1.TabIndex = 0;
            this.textBoxSystemInfo1.Text = "Aipoリマインダー";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(41, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FloralWhite;
            this.tabPage4.Controls.Add(this.buttonInfoSettings);
            this.tabPage4.Controls.Add(this.checkBoxAutoLogin);
            this.tabPage4.Controls.Add(this.checkBoxAutoRun);
            this.tabPage4.Controls.Add(this.comboBoxCheckTime);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.groupBox2);
            this.tabPage4.Location = new System.Drawing.Point(4, 21);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(370, 195);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "お知らせ設定";
            // 
            // timerWhatsnew
            // 
            this.timerWhatsnew.Enabled = true;
            this.timerWhatsnew.Interval = 60000;
            this.timerWhatsnew.Tick += new System.EventHandler(this.timerWhatsnew_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Aipo リマインダー";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripSeparator1,
            this.終了ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(156, 98);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.CheckOnClick = true;
            this.toolStripMenuItem1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
            this.toolStripMenuItem1.Text = "メイン画面を起動(&O)";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(155, 22);
            this.toolStripMenuItem2.Text = "トップページを開く(&B)";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(155, 22);
            this.toolStripMenuItem3.Text = "今すぐチェックする(&S)";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // 終了ToolStripMenuItem
            // 
            this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
            this.終了ToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.終了ToolStripMenuItem.Text = "終了(&X)";
            this.終了ToolStripMenuItem.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
            // 
            // timerTasktrayIcon
            // 
            this.timerTasktrayIcon.Tick += new System.EventHandler(this.timerTasktrayIcon_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxMemo);
            this.groupBox2.Controls.Add(this.checkBoxWorkflow);
            this.groupBox2.Controls.Add(this.checkBoxSchedule);
            this.groupBox2.Controls.Add(this.checkBoxMsgboard);
            this.groupBox2.Controls.Add(this.checkBoxBlogComment);
            this.groupBox2.Controls.Add(this.checkBoxBlog);
            this.groupBox2.Location = new System.Drawing.Point(151, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 148);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "新着情報";
            // 
            // checkBoxMemo
            // 
            this.checkBoxMemo.AutoSize = true;
            this.checkBoxMemo.Location = new System.Drawing.Point(6, 128);
            this.checkBoxMemo.Name = "checkBoxMemo";
            this.checkBoxMemo.Size = new System.Drawing.Size(175, 16);
            this.checkBoxMemo.TabIndex = 5;
            this.checkBoxMemo.Text = "伝言メモの新着メモをチェックする";
            this.checkBoxMemo.UseVisualStyleBackColor = true;
            // 
            // checkBoxWorkflow
            // 
            this.checkBoxWorkflow.AutoSize = true;
            this.checkBoxWorkflow.Location = new System.Drawing.Point(6, 106);
            this.checkBoxWorkflow.Name = "checkBoxWorkflow";
            this.checkBoxWorkflow.Size = new System.Drawing.Size(195, 16);
            this.checkBoxWorkflow.TabIndex = 4;
            this.checkBoxWorkflow.Text = "ワークフローの新着依頼をチェックする";
            this.checkBoxWorkflow.UseVisualStyleBackColor = true;
            // 
            // checkBoxSchedule
            // 
            this.checkBoxSchedule.AutoSize = true;
            this.checkBoxSchedule.Location = new System.Drawing.Point(6, 84);
            this.checkBoxSchedule.Name = "checkBoxSchedule";
            this.checkBoxSchedule.Size = new System.Drawing.Size(197, 16);
            this.checkBoxSchedule.TabIndex = 3;
            this.checkBoxSchedule.Text = "スケジュールの新着予定をチェックする";
            this.checkBoxSchedule.UseVisualStyleBackColor = true;
            // 
            // checkBoxMsgboard
            // 
            this.checkBoxMsgboard.AutoSize = true;
            this.checkBoxMsgboard.Location = new System.Drawing.Point(6, 62);
            this.checkBoxMsgboard.Name = "checkBoxMsgboard";
            this.checkBoxMsgboard.Size = new System.Drawing.Size(204, 16);
            this.checkBoxMsgboard.TabIndex = 2;
            this.checkBoxMsgboard.Text = "掲示板の新しい書き込みをチェックする";
            this.checkBoxMsgboard.UseVisualStyleBackColor = true;
            // 
            // checkBoxBlogComment
            // 
            this.checkBoxBlogComment.AutoSize = true;
            this.checkBoxBlogComment.Location = new System.Drawing.Point(6, 40);
            this.checkBoxBlogComment.Name = "checkBoxBlogComment";
            this.checkBoxBlogComment.Size = new System.Drawing.Size(177, 16);
            this.checkBoxBlogComment.TabIndex = 1;
            this.checkBoxBlogComment.Text = "ブログの新着コメントをチェックする";
            this.checkBoxBlogComment.UseVisualStyleBackColor = true;
            // 
            // checkBoxBlog
            // 
            this.checkBoxBlog.AutoSize = true;
            this.checkBoxBlog.Location = new System.Drawing.Point(6, 18);
            this.checkBoxBlog.Name = "checkBoxBlog";
            this.checkBoxBlog.Size = new System.Drawing.Size(168, 16);
            this.checkBoxBlog.TabIndex = 0;
            this.checkBoxBlog.Text = "ブログの新着記事をチェックする";
            this.checkBoxBlog.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "スケジュール確認：";
            // 
            // comboBoxCheckTime
            // 
            this.comboBoxCheckTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCheckTime.FormattingEnabled = true;
            this.comboBoxCheckTime.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "60"});
            this.comboBoxCheckTime.Location = new System.Drawing.Point(56, 40);
            this.comboBoxCheckTime.Name = "comboBoxCheckTime";
            this.comboBoxCheckTime.Size = new System.Drawing.Size(49, 20);
            this.comboBoxCheckTime.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(111, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "分前";
            // 
            // checkBoxAutoRun
            // 
            this.checkBoxAutoRun.Location = new System.Drawing.Point(7, 66);
            this.checkBoxAutoRun.Name = "checkBoxAutoRun";
            this.checkBoxAutoRun.Size = new System.Drawing.Size(138, 32);
            this.checkBoxAutoRun.TabIndex = 3;
            this.checkBoxAutoRun.Text = "Windows起動時にAipoリマインダーを起動する";
            this.checkBoxAutoRun.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoLogin
            // 
            this.checkBoxAutoLogin.Location = new System.Drawing.Point(7, 104);
            this.checkBoxAutoLogin.Name = "checkBoxAutoLogin";
            this.checkBoxAutoLogin.Size = new System.Drawing.Size(133, 32);
            this.checkBoxAutoLogin.TabIndex = 4;
            this.checkBoxAutoLogin.Text = "ブラウザ起動時に自動ログインする";
            this.checkBoxAutoLogin.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Moccasin;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(15, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(340, 31);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "※ほとんどの場合変更する必要はありませんが、\r\n   変更するときは、アカウント設定タブでリセットボタンを押して下さい。";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Moccasin;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Location = new System.Drawing.Point(15, 12);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(340, 31);
            this.textBox2.TabIndex = 0;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "※チェックする項目などの設定は、お知らせ設定タブで行えます。\r\n※保存ボタンを押すと、DB設定も確定されます。";
            // 
            // buttonInfoSettings
            // 
            this.buttonInfoSettings.Location = new System.Drawing.Point(137, 157);
            this.buttonInfoSettings.Name = "buttonInfoSettings";
            this.buttonInfoSettings.Size = new System.Drawing.Size(75, 23);
            this.buttonInfoSettings.TabIndex = 6;
            this.buttonInfoSettings.Text = "設定";
            this.buttonInfoSettings.UseVisualStyleBackColor = true;
            this.buttonInfoSettings.Click += new System.EventHandler(this.buttonInfoSettings_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 229);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "Aipoリマインダー";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDbPassword;
        private System.Windows.Forms.TextBox textBoxDbUserId;
        private System.Windows.Forms.TextBox textBoxServerIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxDbName;
        private System.Windows.Forms.Timer timerWhatsnew;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
        private System.Windows.Forms.Button buttonDataReset;
        private System.Windows.Forms.Button buttonDataSave;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TextBox textBoxServerPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxVersionInfo;
        private System.Windows.Forms.TextBox textBoxSystemInfo3;
        private System.Windows.Forms.TextBox textBoxSystemInfo2;
        private System.Windows.Forms.TextBox textBoxSystemInfo1;
        private System.Windows.Forms.TextBox textBoxSystemInfo5;
        private System.Windows.Forms.TextBox textBoxSystemInfo4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.Timer timerTasktrayIcon;
        private System.Windows.Forms.ComboBox comboBoxCheckTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxMemo;
        private System.Windows.Forms.CheckBox checkBoxWorkflow;
        private System.Windows.Forms.CheckBox checkBoxSchedule;
        private System.Windows.Forms.CheckBox checkBoxMsgboard;
        private System.Windows.Forms.CheckBox checkBoxBlogComment;
        private System.Windows.Forms.CheckBox checkBoxBlog;
        private System.Windows.Forms.CheckBox checkBoxAutoRun;
        private System.Windows.Forms.CheckBox checkBoxAutoLogin;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonInfoSettings;
    }
}
