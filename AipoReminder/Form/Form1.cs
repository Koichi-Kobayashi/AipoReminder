﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AipoReminder.Constants;
using AipoReminder.Control;
using AipoReminder.DataSet;
using AipoReminder.Manager;
using AipoReminder.Model;
using AipoReminder.Utility;
using AipoReminder.ValueObject;
using Allison.AlertWindow;
using Microsoft.Win32;
using WinFramework.Exceptions;
using WinFramework.Utility;

namespace AipoReminder
{
    public partial class Form1 : Form
    {

#region クラス変数

        // ショートカット
        private string shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "Aipoリマインダー.lnk");

        // 通知リスト
        private Dictionary<string, BalloonItem> notificationDic;

        // タスクトレイにアニメで表示するアイコン
        private Icon[] tasktrayIcons;
        // アニメで現在表示しているアイコンのインデックス
        private int currentTasktrayIconIndex;

        // ログインフラグ(起動時にログインが成功したかどうか)
        private bool isLogin = false;
        // ログイン試行回数
        private int challengeLoginCount = 0;
        // ログイン試行回数の上限
        private static int CHALLENGE_LOGIN_MAX_COUNT = 6;

        // ユーザ選択フォームで選択したユーザをカンマ区切りで保持しておく変数
        private string groupUserId;
        // ユーザ選択フォームでOKボタンを押したかどうか
        private bool isSetGroupUserId;

#endregion

#region コンストラクタ

        public Form1()
        {
            InitializeComponent();
        }

#endregion

#region 初期化処理 終了処理

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // イベントをイベントハンドラに関連付ける
            // フォームコンストラクタなどの適当な位置に記述してもよい
            SystemEvents.SessionEnding +=
                new SessionEndingEventHandler(SystemEvents_SessionEnding);

            // タスクトレイアイコン用タイマーを無効にしておく（初めはアニメしない）
            this.timerTasktrayIcon.Enabled = false;
            // アニメ時は、1秒毎にアイコンを変更する
            this.timerTasktrayIcon.Interval = 1000;

            // タスクトレイにアニメで表示するアイコンを指定する
            this.currentTasktrayIconIndex = 0;
            this.tasktrayIcons = new Icon[] {
                Properties.Resources.favicon_16_red,
                Properties.Resources.favicon_16_blue};

            // ステータスバー
            this.statusStrip1.Items.Add(new ToolStripLabelEx());

            // 一時ディレクトリの作成と一時ファイルの削除
            this.initTempDir();

            // 設定を読み込む
            if (this.ReadUserData())
            {
                // ログイン試行のためのタイマーを起動
                this.timerLogin.Enabled = true;
            }
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;                        // 終了処理キャンセル
                this.Visible = false;                   // フォーム非表示
            }
            else
            {
                // タスクトレイからアイコンを取り除く
                this.notifyIcon1.Visible = false;
                // 一時ファイルの削除
                this.terminateTempDir();
            }
        }

        /// <summary>
        /// 終了後の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // イベントを開放する
            // フォームDisposeメソッド内の基本クラスのDisposeメソッド呼び出しの前に
            // 記述してもよい
            SystemEvents.SessionEnding -=
                new SessionEndingEventHandler(SystemEvents_SessionEnding);
        }

        /// <summary>
        /// ログオフ、シャットダウンしようとしているとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            if (e.Reason == SessionEndReasons.Logoff || e.Reason == SessionEndReasons.SystemShutdown)
            {
                this.shutdown();
            }
        }

        /// <summary>
        /// 設定情報からログイン名を取得し、ログイン名、パスワードを非活性にする
        /// </summary>
        private bool ReadUserData()
        {
            try
            {
                // ウィンドウ非表示
                this.Visible = false;

                // バージョン情報
                Assembly asm = Assembly.GetExecutingAssembly();
                Version ver = asm.GetName().Version;
                textBoxVersionInfo.Text = Application.ProductName + " Version " + Application.ProductVersion;
                textBoxSystemInfo1.Text = Application.ProductName + " Core Version " + Application.ProductVersion;
                textBoxSystemInfo2.Text = "Operating System Version ";
                textBoxSystemInfo3.Text = System.Environment.OSVersion.VersionString;
                textBoxSystemInfo4.Text = "Internet Explorer Version " + IEUtility.GetInternetExplorerVersion();
                // AssemblyCopyrightの取得
                System.Reflection.AssemblyCopyrightAttribute asmcpy =
                    (System.Reflection.AssemblyCopyrightAttribute)
                    Attribute.GetCustomAttribute(
                    System.Reflection.Assembly.GetExecutingAssembly(),
                    typeof(System.Reflection.AssemblyCopyrightAttribute));
                textBoxSystemInfo5.Text = asmcpy.Copyright;

                // サーバIP
                textBoxServerIP.Text = SettingManager.NpgsqlConnectionServer;
                // ポート
                textBoxServerPort.Text = SettingManager.NpgsqlConnectionPort;
                // ユーザID
                textBoxDbUserId.Text = SettingManager.NpgsqlConnectionUserId;
                // パスワード
                textBoxDbPassword.Text = SettingManager.NpgsqlConnectionPassword;
                // データベース名
                textBoxDbName.Text = SettingManager.NpgsqlConnectionDatabase;

                // v7(v6)対応バージョンの初回起動かどうか
                if (SettingManager.V7FirstTime)
                {
                    DialogResult result = MessageBox.Show(MessageConstants.MSG_CONFIRM_V7_FIRST_TIME, 
                                                          MessageConstants.MSG_CAPTION_004, 
                                                          MessageBoxButtons.OKCancel, 
                                                          MessageBoxIcon.Question);
                    if (result == DialogResult.OK)
                    {
                        SettingManager.AipoVersion = 7;
                    }

                    DialogResult result2 = MessageBox.Show(MessageConstants.MSG_CONFIRM_V7_BROWSER,
                                                           MessageConstants.MSG_CAPTION_005,
                                                           MessageBoxButtons.OK,
                                                           MessageBoxIcon.Information);


                    SettingManager.V7FirstTime = false;
                    SettingManager.V7FirstTimeSave();
                }

                // AipoVersionコンボボックスのSelectedIndexを設定
                switch (SettingManager.AipoVersion)
                {
                    case 4:
                        comboBoxAipoVersion.SelectedIndex = 0;
                        break;
                    case 5:
                        comboBoxAipoVersion.SelectedIndex = 1;
                        break;
                    case 6:
                        comboBoxAipoVersion.SelectedIndex = 2;
                        break;
                    case 7:
                        comboBoxAipoVersion.SelectedIndex = 3;
                        break;
                }

                // スケジュールのチェック間隔コンボボックスのSelectedIndexを設定
                for (int i = 0; i < 12; i++)
                {
                    // 選択
                    if (SettingManager.CheckTime == (i * 5 + 5))
                    {
                        comboBoxCheckTime.SelectedIndex = i;
                    }
                }

                // 指定ブラウザComboboxの初期設定
                //SetComboBoxBrowserItems();

                if (String.IsNullOrEmpty(SettingManager.UserId) ||
                    String.IsNullOrEmpty(SettingManager.LoginName) ||
                    String.IsNullOrEmpty(SettingManager.UserPassword))
                {
                    if (SettingManager.AipoVersion < 6)
                    {
                        // 新着情報はデフォルトですべてチェックしておく
                        SettingManager.CheckBlog = true;
                        SettingManager.CheckBlogComment = true;
                        SettingManager.CheckMsgboard = true;
                        SettingManager.CheckSchedule = true;
                        SettingManager.CheckWorkflow = true;
                        SettingManager.CheckMemo = true;
                        checkBoxBlog.Checked = true;
                        checkBoxBlogComment.Checked = true;
                        checkBoxMsgboard.Checked = true;
                        checkBoxSchedule.Checked = true;
                        checkBoxWorkflow.Checked = true;
                        checkBoxMemo.Checked = true;
                    }

                    this.ActiveWindow();
                    return false;
                }

                textBoxUserName.Text = SettingManager.LoginName;                        // ログイン名
                textBoxURL.Text = SettingManager.Url;                                   // URL
                comboBoxAipoVersion.SelectedItem = SettingManager.AipoVersion;          // AipoVersion
                comboBoxCheckTime.SelectedItem = SettingManager.CheckTime;              // スケジュールのチェック間隔
                checkBoxAutoRun.Checked = SettingManager.AutoRun;                       // 自動起動
                checkBoxAutoLogin.Checked = SettingManager.AutoLogin;                   // 自動ログイン
                checkBoxBlog.Checked = SettingManager.CheckBlog;                        // ブログの新着記事チェック
                checkBoxBlogComment.Checked = SettingManager.CheckBlogComment;          // ブログの新着コメントチェック
                checkBoxMsgboard.Checked = SettingManager.CheckMsgboard;                // 掲示板の新しい書き込みチェック
                checkBoxSchedule.Checked = SettingManager.CheckSchedule;                // スケジュールの新着予定チェック
                checkBoxWorkflow.Checked = SettingManager.CheckWorkflow;                // ワークフローの新着依頼チェック
                checkBoxMemo.Checked = SettingManager.CheckMemo;                        // 伝言メモの新着メモチェック
                checkBoxOtherSchedule.Checked = SettingManager.CheckOtherSchedule;      // 他のユーザのスケジュールをチェックするかどうか
                checkBoxInformation.Checked = SettingManager.CheckInformation;          // お知らせを吹き出しからウィンドウタイプに変更するかどうか
                checkBoxExtTimeCard.Checked = SettingManager.CheckExtTimeCard;          // タイムカードと連携するかどうか
                //foreach (ComboBoxBrowserItem browserItem in comboBoxBrowser.Items)      // 指定ブラウザの設定
                //{
                //    if (SettingManager.BrowserName.Equals(browserItem.Name))
                //    {
                //        comboBoxBrowser.SelectedItem = browserItem;
                //    }
                //}
                textBoxBrowser.Text = SettingManager.BrowserPath;                       // ブラウザのパス
                checkBoxExtTimeCardUpdateCkeckout.Checked = SettingManager.CheckExtTimeCardUpdateCheckout;  // 終了時に退勤の時刻を更新するかどうか

                // Aipoのバージョン依存項目の設定
                InitAipoVersionConfig();

                // 設定を保存
                SettingManager.WhatsnewInfoSave();

                return true;
            }
            catch (TypeInitializationException e)
            {
                LogUtility.WriteLogError(e.Message);
                // エラーメッセージ
                MessageBox.Show(MessageConstants.ERR_SETTING, 
                                MessageConstants.MSG_CAPTION_003, 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// ログイン
        /// </summary>
        /// <returns></returns>
        private bool Login()
        {
            string userId = SettingManager.UserId;
            string loginName = SettingManager.LoginName;
            string password = "";

            if (!String.IsNullOrEmpty(SettingManager.UserPassword))
            {
                password = Security.EncryptPassword(SettingManager.UserPassword);
                textBoxPassword.Text = "******";
            }

            try
            {
                // ユーザ一覧を取得
                TurbineUserDataSet data = this.GetTurbineUserData(userId, loginName, password);

                if (data.turbine_user.Count > 0)
                {
                    // ログイン名
                    textBoxUserName.Text = data.turbine_user[0].login_name;
                    // パスワード
                    textBoxPassword.Text = "******";

                    // 自動ログイン用html作成
                    SettingManager.AutoLoginHtml = String.Format(Properties.Resources.autologin,
                                                                 String.Format("{0:D4}", SettingManager.Url.Length),
                                                                 SettingManager.Url,
                                                                 SettingManager.LoginName,
                                                                 SettingManager.UserPassword);

                    // ログイン出来たことを覚えておく
                    isLogin = true;
                }
            }
            catch (DBException ex)
            {
                Debug.Print(ex.toString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// タイムカード連携(起動時)
        /// </summary>
        /// <returns></returns>
        private bool LoginTimeCard()
        {
            try
            {
                ExtTimeCardDataSet data = new ExtTimeCardDataSet();
                ExtTimeCardModel m = new ExtTimeCardModel();

                DateTime dt = DateTime.Now;
                DateTime dtUpdateDate = DateTime.Now;
                // -----------------------
                // 日付の変わる時刻を取得
                // -----------------------
                ExtTimeCardDataSet.search_eip_t_ext_timecard_systemRow systemRow = data.search_eip_t_ext_timecard_system.Newsearch_eip_t_ext_timecard_systemRow();
                systemRow.user_id = SettingManager.UserId;
                data.search_eip_t_ext_timecard_system.Rows.Add(systemRow);
                int result = m.Execute(m.GetChangeHour, data);
                if (result > 0)
                {
                    string hour = data.eip_t_ext_timecard_system[0].change_hour;
                    if (!String.IsNullOrEmpty(hour))
                    {
                        if (dt.Hour <= int.Parse(hour))
                        {
                            dt = dt.AddDays(-1);
                        }
                    }
                }
                else
                {
                    m.Execute(m.GetChangeHourDefault, data);
                    string hour = data.eip_t_ext_timecard_system[0].change_hour;
                    if (!String.IsNullOrEmpty(hour))
                    {
                        if (dt.Hour <= int.Parse(hour))
                        {
                            dt = dt.AddDays(-1);
                        }
                    }
                }

                // ------------------------------------------------------------
                // 今日の日付のタイムカードのデータが存在するかチェックする
                // ------------------------------------------------------------
                ExtTimeCardDataSet.search_eip_t_ext_timecardRow paramRow = data.search_eip_t_ext_timecard.Newsearch_eip_t_ext_timecardRow();
                paramRow.user_id = SettingManager.UserId;
                paramRow.punch_date = dt.ToString("yyyy-MM-dd");
                data.search_eip_t_ext_timecard.Rows.Add(paramRow);
                m.Execute(m.GetTimeCardInfo, data);
                if (data.eip_t_ext_timecard.Count == 0)
                {
                    ExtTimeCardDataSet.update_eip_t_ext_timecardRow updateRow = data.update_eip_t_ext_timecard.Newupdate_eip_t_ext_timecardRow();
                    updateRow.user_id = SettingManager.UserId;
                    updateRow.punch_date = dt.ToString("yyyy-MM-dd");
                    updateRow.type = "P";
                    updateRow.clock_in_time = dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    updateRow.create_date = dtUpdateDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    updateRow.update_date = dtUpdateDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    data.update_eip_t_ext_timecard.Rows.Add(updateRow);
                    m.Execute(m.InsertTimeCard, data);
                }
            }
            catch (DBException ex)
            {
                Debug.Print(ex.toString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// タイムカード連携(終了時)
        /// </summary>
        /// <returns></returns>
        private bool LogoutTimeCard()
        {
            try
            {
                ExtTimeCardDataSet data = new ExtTimeCardDataSet();
                ExtTimeCardModel m = new ExtTimeCardModel();

                DateTime dt = DateTime.Now;
                DateTime dtUpdateDate = DateTime.Now;

                // -----------------------
                // 日付の変わる時刻を取得
                // -----------------------
                ExtTimeCardDataSet.search_eip_t_ext_timecard_systemRow systemRow = data.search_eip_t_ext_timecard_system.Newsearch_eip_t_ext_timecard_systemRow();
                systemRow.user_id = SettingManager.UserId;
                data.search_eip_t_ext_timecard_system.Rows.Add(systemRow);
                int result = m.Execute(m.GetChangeHour, data);
                if (result > 0)
                {
                    string hour = data.eip_t_ext_timecard_system[0].change_hour;
                    if (!String.IsNullOrEmpty(hour))
                    {
                        if (dt.Hour <= int.Parse(hour))
                        {
                            dt = dt.AddDays(-1);
                        }
                    }
                }
                else
                {
                    m.Execute(m.GetChangeHourDefault, data);
                    string hour = data.eip_t_ext_timecard_system[0].change_hour;
                    if (!String.IsNullOrEmpty(hour))
                    {
                        if (dt.Hour <= int.Parse(hour))
                        {
                            dt = dt.AddDays(-1);
                        }
                    }
                }

                // ------------------------------------------------------------
                // 今日の日付のタイムカードのデータが存在するかチェックする
                // ------------------------------------------------------------
                ExtTimeCardDataSet.search_eip_t_ext_timecardRow paramRow = data.search_eip_t_ext_timecard.Newsearch_eip_t_ext_timecardRow();
                paramRow.user_id = SettingManager.UserId;
                paramRow.punch_date = dt.ToString("yyyy-MM-dd");
                data.search_eip_t_ext_timecard.Rows.Add(paramRow);
                m.Execute(m.GetTimeCardInfo, data);
                if (data.eip_t_ext_timecard.Count == 0)
                {
                    ExtTimeCardDataSet.update_eip_t_ext_timecardRow updateRow = data.update_eip_t_ext_timecard.Newupdate_eip_t_ext_timecardRow();
                    updateRow.user_id = SettingManager.UserId;
                    updateRow.punch_date = dt.ToString("yyyy-MM-dd");
                    updateRow.type = "P";
                    updateRow.clock_out_time = dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    updateRow.create_date = dtUpdateDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    updateRow.update_date = dtUpdateDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    data.update_eip_t_ext_timecard.Rows.Add(updateRow);
                    m.Execute(m.InsertTimeCard, data);
                }
                else if (SettingManager.CheckExtTimeCardUpdateCheckout || 
                    (!SettingManager.CheckExtTimeCardUpdateCheckout && String.IsNullOrEmpty(data.eip_t_ext_timecard[0].clock_out_time)))
                {
                    ExtTimeCardDataSet.update_eip_t_ext_timecardRow updateRow = data.update_eip_t_ext_timecard.Newupdate_eip_t_ext_timecardRow();
                    updateRow.user_id = SettingManager.UserId;
                    updateRow.clock_out_time = dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    updateRow.update_date = dtUpdateDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    updateRow.timecard_id = data.eip_t_ext_timecard[0].timecard_id;
                    data.update_eip_t_ext_timecard.Rows.Add(updateRow);
                    m.Execute(m.UpdateTimeCard, data);
                }
            }
            catch (DBException ex)
            {
                Debug.Print(ex.toString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// ログイン試行
        /// </summary>
        private void timerLogin_Tick(object sender, EventArgs e)
        {
            this.challengeLoginCount++;

            if (this.challengeLoginCount == 1)
            {
                this.timerLogin.Interval = 10000;
            }

            // 入力項目を非活性にする
            InitBasicConfigEnabled(false);

            if (this.Login())
            {
                // タイムカード連携
                if (checkBoxExtTimeCard.Enabled && SettingManager.CheckExtTimeCard)
                {
                    this.LoginTimeCard();
                }

                // 新着情報をチェック
                this.PreWhatsnewProcess(true, true);
                this.timerLogin.Stop();
                this.timerLogin.Enabled = false;
                buttonDataReset.Enabled = true;
                this.challengeLoginCount = 0;
            }

            if (this.challengeLoginCount > Form1.CHALLENGE_LOGIN_MAX_COUNT)
            {
                // エラーメッセージ
                MessageBox.Show(MessageConstants.ERR_DB_ACCESS, 
                                MessageConstants.MSG_CAPTION_001, 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Warning);
                // ウィンドウを表示
                this.ActiveWindow();
                // タイマー停止
                this.timerLogin.Stop();
                this.timerLogin.Enabled = false;
                // パスワードを空白に設定
                textBoxPassword.Text = "";

                // 入力項目を活性にする
                InitBasicConfigEnabled(true);

                this.challengeLoginCount = 0;
            }
        }

        /// <summary>
        /// 基本設定の活性/非活性化
        /// </summary>
        /// <param name="isEnabled">true:活性、false:非活性</param>
        private void InitBasicConfigEnabled(bool isEnabled)
        {
            textBoxUserName.Enabled = isEnabled;
            textBoxPassword.Enabled = isEnabled;
            textBoxURL.Enabled = isEnabled;
            comboBoxAipoVersion.Enabled = isEnabled;
            buttonDataSave.Enabled = isEnabled;
            textBoxServerIP.Enabled = isEnabled;
            textBoxServerPort.Enabled = isEnabled;
            textBoxDbUserId.Enabled = isEnabled;
            textBoxDbPassword.Enabled = isEnabled;
            textBoxDbName.Enabled = isEnabled;
            buttonDataReset.Enabled = isEnabled;
        }

        /// <summary>
        /// 新着情報のチェックボックスの設定
        /// Aipoのバージョンによって活性/非活性を切り替える
        /// </summary>
        private void InitWhatsnewConfigEnabled()
        {
            switch (SettingManager.AipoVersion)
            {
                case 4:
                case 5:
                    InitWhatsnewConfigEnabled(true);
                    break;
                case 6:
                case 7:
                    InitWhatsnewConfigCheck(false);
                    InitWhatsnewConfigEnabled(false);
                    break;
            }
        }

        /// <summary>
        /// 新着情報のチェックボックスのチェックを切り替える
        /// </summary>
        /// <param name="isChecked">true:ON、false:OFF</param>
        private void InitWhatsnewConfigCheck(bool isChecked)
        {
            checkBoxBlog.Checked = isChecked;
            checkBoxBlogComment.Checked = isChecked;
            checkBoxMsgboard.Checked = isChecked;
            checkBoxSchedule.Checked = isChecked;
            checkBoxWorkflow.Checked = isChecked;
            checkBoxMemo.Checked = isChecked;
        }

        /// <summary>
        /// 新着情報のチェックボックスの活性/非活性を切り替える
        /// </summary>
        /// <param name="isEnabled">true:活性、false:非活性</param>
        private void InitWhatsnewConfigEnabled(bool isEnabled)
        {
            checkBoxBlog.Enabled = isEnabled;
            checkBoxBlogComment.Enabled = isEnabled;
            checkBoxMsgboard.Enabled = isEnabled;
            checkBoxSchedule.Enabled = isEnabled;
            checkBoxWorkflow.Enabled = isEnabled;
            checkBoxMemo.Enabled = isEnabled;
        }

        ///// <summary>
        ///// グループ一覧をComboBoxに設定
        ///// </summary>
        //private void SetComboBoxBrowserItems()
        //{
        //    ComboBoxBrowserItem defaultItem = new ComboBoxBrowserItem(0, "デフォルト", "");
        //    comboBoxBrowser.Items.Add(defaultItem);
        //    comboBoxBrowser.SelectedIndex = 0;

        //    AipoReminder.Utility.RegistryUtility regUtil = new AipoReminder.Utility.RegistryUtility();
        //    List<ComboBoxBrowserItem> listBrowserItem = regUtil.getBrowserComboItem();

        //    for (int i = 0; i < listBrowserItem.Count; i++)
        //    {
        //        ComboBoxBrowserItem item = listBrowserItem[i];
        //        item.Id = i + 1;
        //        comboBoxBrowser.Items.Add(item);
        //    }
        //}

        private void initTempDir()
        {
            string tmpPath = Path.GetTempPath();
            tmpPath += @"AipoReminder\";

            if (!Directory.Exists(tmpPath))
            {
                Directory.CreateDirectory(tmpPath);
            }
        }

        private void terminateTempDir()
        {
            string tmpPath = Path.GetTempPath();
            tmpPath += @"AipoReminder\";

            if (Directory.Exists(tmpPath))
            {
                string[] files = Directory.GetFiles(tmpPath);
                foreach (string s in files)
                {
                    File.Delete(s);
                }
            }
        }

#endregion

#region メインウィンドウ クリックイベント関連

        /// <summary>
        /// 保存ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDataSave_Click(object sender, EventArgs e)
        {
            // 入力チェック
            if (!this.InputCheck())
            {
                return;
            }

            try
            {
                // DB設定を保存
                SettingManager.NpgsqlConnectionServer = textBoxServerIP.Text;
                SettingManager.NpgsqlConnectionPort = textBoxServerPort.Text;
                SettingManager.NpgsqlConnectionUserId = textBoxDbUserId.Text;
                SettingManager.NpgsqlConnectionPassword = textBoxDbPassword.Text;
                SettingManager.NpgsqlConnectionDatabase = textBoxDbName.Text;
                SettingManager.ConnectionSettingPreSave();

                TurbineUserDataSet data = this.GetTurbineUserData("", textBoxUserName.Text, Security.EncryptPassword(textBoxPassword.Text));

                if (data.turbine_user.Count > 0)
                {
                    // ログイン名とパスワードが一致
                    isLogin = true;

                    // ユーザ情報を保存
                    SettingManager.UserId = data.turbine_user[0].user_id;
                    SettingManager.LoginName = data.turbine_user[0].login_name;
                    SettingManager.UserPassword = textBoxPassword.Text;
                    SettingManager.Url = textBoxURL.Text;
                    SettingManager.AipoVersion = int.Parse(comboBoxAipoVersion.SelectedItem.ToString()); ;      // AipoVersion

                    // 入力項目を非活性にする
                    textBoxUserName.Enabled = false;
                    textBoxPassword.Enabled = false;
                    textBoxURL.Enabled = false;
                    buttonDataSave.Enabled = false;
                    textBoxServerIP.Enabled = false;
                    textBoxServerPort.Enabled = false;
                    textBoxDbUserId.Enabled = false;
                    textBoxDbPassword.Enabled = false;
                    textBoxDbName.Enabled = false;
                    comboBoxAipoVersion.Enabled = false;

                    // 自動ログイン用html作成
                    SettingManager.AutoLoginHtml = String.Format(Properties.Resources.autologin,
                                                                 String.Format("{0:D4}", SettingManager.Url.Length),
                                                                 SettingManager.Url,
                                                                 SettingManager.LoginName,
                                                                 SettingManager.UserPassword);

                    // 自動起動
                    if (checkBoxAutoRun.Checked)
                    {
                        SettingManager.AutoRun = true;

                        // ショートカットを作成
                        this.CreateShortcut();
                    }
                    else
                    {
                        SettingManager.AutoRun = false;

                        // ショートカットを削除
                        this.DeleteShortcut();
                    }

                    // 自動ログイン
                    if (checkBoxAutoLogin.Checked)
                    {
                        SettingManager.AutoLogin = true;
                    }
                    else
                    {
                        SettingManager.AutoLogin = false;
                    }

                    // 設定を保存
                    SettingManager.AccountInfoSave();

                    // ステータスバーにメッセージを表示
                    ((ToolStripLabelEx)this.statusStrip1.Items[0]).DisplayMessage(MessageConstants.INFO_ACCOUNT_SETTING_OK);

                    // タイムカード連携
                    if (checkBoxExtTimeCard.Enabled && SettingManager.CheckExtTimeCard)
                    {
                        this.LoginTimeCard();
                    }

                    // 新着情報をチェック
                    this.PreWhatsnewProcess(true, false);
                }
                else
                {
                    // ログイン名とパスワードが不一致
                    MessageBox.Show(MessageConstants.ERR_PASSWORD, 
                                    MessageConstants.MSG_CAPTION_001, 
                                    MessageBoxButtons.OK, 
                                    MessageBoxIcon.Warning);
                }
            }
            catch (DBException ex)
            {
                Debug.Print(ex.toString());
                // エラーメッセージ
                MessageBox.Show(MessageConstants.ERR_DB_ACCESS, 
                                MessageConstants.MSG_CAPTION_001, 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// リセットボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDataReset_Click(object sender, EventArgs e)
        {
            // パスワード
            textBoxPassword.Text = "";
            SettingManager.UserPassword = "";

            // 入力項目を活性にする
            InitBasicConfigEnabled(true);

            // Aipoのバージョン依存項目の設定
            InitAipoVersionConfig();

            // タスクトレイアイコンの点滅を停止
            this.AnimatedTasktrayIcon(false);

            // AlertWindowを閉じる
            alertWindow1.Close();

            isLogin = false;
        }

        /// <summary>
        /// URLテキストボックスからフォーカスが外れた場合、
        /// サーバIPの値をセットする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxURL_Leave(object sender, EventArgs e)
        {
            try
            {
                Uri url = new Uri(textBoxURL.Text);
                textBoxServerIP.Text = url.Host;
            }
            catch (UriFormatException ex)
            {
                Debug.Print(ex.ToString());
            }
        }

        /// <summary>
        /// お知らせ設定を保存する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonInfoSettings_Click(object sender, EventArgs e)
        {
            SettingManager.CheckTime = int.Parse(comboBoxCheckTime.SelectedItem.ToString()); ;      // スケジュールのチェック間隔
            SettingManager.AutoRun = checkBoxAutoRun.Checked;                       // 自動起動
            SettingManager.AutoLogin = checkBoxAutoLogin.Checked;                   // 自動ログイン
            SettingManager.CheckBlog = checkBoxBlog.Checked;                        // ブログの新着記事チェック
            SettingManager.CheckBlogComment = checkBoxBlogComment.Checked;          // ブログの新着コメントチェック
            SettingManager.CheckMsgboard = checkBoxMsgboard.Checked;                // 掲示板の新しい書き込みチェック
            SettingManager.CheckSchedule = checkBoxSchedule.Checked;                // スケジュールの新着予定チェック
            SettingManager.CheckWorkflow = checkBoxWorkflow.Checked;                // ワークフローの新着依頼チェック
            SettingManager.CheckMemo = checkBoxMemo.Checked;                        // 伝言メモの新着メモチェック
            SettingManager.CheckOtherSchedule = checkBoxOtherSchedule.Checked;      // 他のユーザのスケジュールのチェックするかどうか
            SettingManager.CheckInformation = checkBoxInformation.Checked;          // お知らせを吹き出しからウィンドウタイプに変更するかどうか
            SettingManager.CheckExtTimeCard = checkBoxExtTimeCard.Checked;          // タイムカードと連携するかどうか
//            ComboBoxBrowserItem bItem = (ComboBoxBrowserItem)comboBoxBrowser.SelectedItem;
//            SettingManager.BrowserName = bItem.Name;                                // 指定ブラウザ名
            SettingManager.BrowserPath = textBoxBrowser.Text;                       // ブラウザのパス
            SettingManager.CheckExtTimeCardUpdateCheckout = checkBoxExtTimeCardUpdateCkeckout.Checked;  // 終了時に退勤の時刻を更新

            if (isSetGroupUserId)
            {
                SettingManager.GroupUserId = groupUserId;                           // スケジュールをチェックするユーザ一覧(カンマ区切り)
            }

            if (checkBoxAutoRun.Checked)
            {
                SettingManager.AutoRun = true;

                // ショートカットを作成
                this.CreateShortcut();
            }
            else
            {
                SettingManager.AutoRun = false;

                // ショートカットを削除
                this.DeleteShortcut();
            }

            // 設定を保存
            SettingManager.WhatsnewInfoSave();

            // ステータスバーにメッセージを表示
            ((ToolStripLabelEx)this.statusStrip1.Items[0]).DisplayMessage(MessageConstants.INFO_WHATSNEW_SETTING_OK);

            if (isLogin)
            {
                // タイムカード連携
                if (checkBoxExtTimeCard.Enabled && SettingManager.CheckExtTimeCard)
                {
                    this.LoginTimeCard();
                }

                // 新着情報をチェック
                this.PreWhatsnewProcess(true, false);
            }
        }

        /// <summary>
        /// ユーザ選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                groupUserId = f.GroupId;
                isSetGroupUserId = true;
            }
            f.Dispose();
        }

        /// <summary>
        /// ブラウザ参照ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == this.openFileDialog1.ShowDialog())
            {
                this.textBoxBrowser.Text = this.openFileDialog1.FileName;
            }
        }

        /// <summary>
        /// Aipoのバージョンを変更したときに、タイムカード連携のチェックボックスの有効/無効を変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxAipoVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aipoのバージョン依存項目の設定
            InitAipoVersionConfig();
        }
        
        /// <summary>
        /// Aipoのバージョン依存機能の初期化
        /// Aipoのバージョンによって初期化する内容を変更する
        /// </summary>
        private void InitAipoVersionConfig()
        {
            int selectedAipoVersion = int.Parse(comboBoxAipoVersion.SelectedItem.ToString());
            
            // タイムカード連携はAipoのバージョンが5以上のみ
            if (5 <= selectedAipoVersion)
            {
                checkBoxExtTimeCard.Enabled = true;
            }
            else
            {
                checkBoxExtTimeCard.Enabled = false;
            }
            // タイムカード連携に合わせて退勤時刻連携も連動させる
            if (SettingManager.CheckExtTimeCard)
            {
                checkBoxExtTimeCardUpdateCkeckout.Enabled = true;
            }
            else
            {
                checkBoxExtTimeCardUpdateCkeckout.Enabled = false;
            }

            // Aipoのバージョンが6以上の場合は新着情報のチェックボックスを非活性にする
            if (6 <= selectedAipoVersion)
            {
                InitWhatsnewConfigCheck(false);
                InitWhatsnewConfigEnabled(false);
            }
            else
            {
                InitWhatsnewConfigEnabled(true);
            }
        }

        /// <summary>
        /// テキストボックスでEnterを押すと次のテキストボックスへフォーカスを移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Shift)
                {
                    ProcessTabKey(false);
                }
                else
                {
                    ProcessTabKey(true);
                }
            }
        }

        /// <summary>
        /// タイムカード連携のチェックボックスのオン・オフで退勤時刻を更新するかどうかのチェックボックスも連動させる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxExtTimeCard_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            if (cb.Checked)
            {
                checkBoxExtTimeCardUpdateCkeckout.Enabled = true;
            }
            else
            {
                checkBoxExtTimeCardUpdateCkeckout.Enabled = false;
            }
        }

#endregion

#region DB関連処理

        /// <summary>
        /// ユーザ情報取得
        /// </summary>
        /// <returns></returns>
        private TurbineUserDataSet GetTurbineUserData(string userId, string login_name, string password)
        {
            TurbineUserDataSet data = new TurbineUserDataSet();

            TurbineUserDataSet.search_turbine_userRow paramRow = data.search_turbine_user.Newsearch_turbine_userRow();

            paramRow.user_id = userId;
            paramRow.login_name = login_name;
            paramRow.password_value = password;

            data.search_turbine_user.Rows.Add(paramRow);

            UserModel m = new UserModel();
            m.Execute(m.GetTurbineUserInfo, data);

            return data;
        }

#endregion

#region タスクトレイのメニュー関連

        /// <summary>
        /// バルーンをクリックしたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            // タスクトレイアイコンの点滅を停止
            this.AnimatedTasktrayIcon(false);

            // トップページを開く
            this.ShowAipoTopPage();
        }

        /// <summary>
        /// 終了メニュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.shutdown();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        private void shutdown()
        {
            // タイムカード連携
            if (checkBoxExtTimeCard.Enabled && SettingManager.CheckExtTimeCard)
            {
                this.LogoutTimeCard();
            }
            // タスクトレイからアイコンを取り除く
            this.notifyIcon1.Visible = false;
            // アプリケーション終了
            Application.Exit();
        }

        /// <summary>
        /// メイン画面を起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.ActiveWindow();
        }

        /// <summary>
        /// トップページを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.AnimatedTasktrayIcon(false);
            this.ShowAipoTopPage();
        }

        /// <summary>
        /// 今すぐチェックする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            // 新着情報をチェック
            this.PreWhatsnewProcess(true, false);
        }

        /// <summary>
        /// タスクトレイのアイコンをダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.AnimatedTasktrayIcon(false);
            this.ShowAipoTopPage();
        }

        /// <summary>
        /// ウィンドウのアクティブ化
        /// </summary>
        private void ActiveWindow()
        {
            this.Visible = true;                // フォームの表示
            this.ShowInTaskbar = true;          // タスクバーへの表示
            if (this.WindowState == FormWindowState.Minimized)
            {
                // 最小化をやめる
                this.WindowState = FormWindowState.Normal;
            }
            this.Activate();                    // フォームをアクティブにする
        }

        /// <summary>
        /// トップページを開くためにスレッドを起動
        /// </summary>
        private void ShowAipoTopPage()
        {
            if (!String.IsNullOrEmpty(SettingManager.BrowserPath)) 
            {
                ThreadingManager threadingManager = new ThreadingManager();
                //ComboBoxBrowserItem item = (ComboBoxBrowserItem)comboBoxBrowser.SelectedItem;
                //threadingManager.setBrowserPath(item.Path);
                threadingManager.setBrowserPath(SettingManager.BrowserPath);
                Thread thread = new Thread(new ThreadStart(threadingManager.Run));

                thread.Start();
            }
        }

        /// <summary>
        /// AlertWindowをクリックしたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void alertWindow1_Click(object sender, EventArgs e)
        {
            // タスクトレイアイコンの点滅を停止
            this.AnimatedTasktrayIcon(false);

            // トップページを開く
            this.ShowAipoTopPage();

            // AlertWindowを閉じる
            alertWindow1.Close();
        }

#endregion

#region メイン処理

        /// <summary>
        /// 定期実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PreWhatsnewProcess(bool isCheckNow, bool isStartup)
        {
            switch (SettingManager.AipoVersion)
            {
                case 4:
                case 5:
                    this.WhatsnewProcess(isCheckNow, isStartup);
                    break;
                case 6:
                case 7:
                    this.WhatsnewProcessV7(isCheckNow, isStartup);
                    break;
            }
        }

        /// <summary>
        /// 定期実行(Aipo4,5)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WhatsnewProcess(bool isCheckNow, bool isStartup)
        {
            if (!isLogin)
            {
                return;
            }
            if (String.IsNullOrEmpty(SettingManager.UserId) ||
                String.IsNullOrEmpty(SettingManager.LoginName) ||
                String.IsNullOrEmpty(SettingManager.UserPassword))
            {
                return;
            }

            DateTime dt = DateTime.Now;
            /*
             * タイマーで5分間隔で新着情報をチェックするため、現在の時刻が5分、10分…55分かチェックし、
             * 5分、10分…55分の場合のみ、以降の処理を実行する。
             * ただし、isCheckNowがtrueの場合(起動時や設定保存時、今すぐチェックする場合)はそのまま以降の処理を実行する。
             */
            if (!isCheckNow)
            {
                if (dt.Minute != 0 &&
                    dt.Minute != 5 &&
                    dt.Minute != 10 &&
                    dt.Minute != 15 &&
                    dt.Minute != 20 &&
                    dt.Minute != 25 &&
                    dt.Minute != 30 &&
                    dt.Minute != 35 &&
                    dt.Minute != 40 &&
                    dt.Minute != 45 &&
                    dt.Minute != 50 &&
                    dt.Minute != 55)
                {
                    return;
                }
            }

            try
            {
                notificationDic = new Dictionary<string, BalloonItem>();

                ScheduleManager sm = new ScheduleManager(dt);
                List<ScheduleItem> oneDayScheduleList = null;
                // 終日スケジュールをチェック
                if (isStartup)
                {
                    oneDayScheduleList = sm.CheckOneDaySchedule();
                }

                // もうすぐ始まるスケジュールをチェック
                List<ScheduleItem> scheduleList = new List<ScheduleItem>();
                scheduleList = sm.CheckSchedule();

                if (scheduleList != null && scheduleList.Count > 0 || oneDayScheduleList != null && oneDayScheduleList.Count > 0)
                {
                    Form2 f = new Form2();
                    if (scheduleList != null && scheduleList.Count > 0)
                    {
                        f.ScheduleList = scheduleList;
                    }
                    if (oneDayScheduleList != null && oneDayScheduleList.Count > 0)
                    {
                        f.OneDayScheduleList = oneDayScheduleList;
                    }
                    f.Show();
                }

                // 新着情報を取得するためのDataSetと検索条件を設定
                WhatsnewDataSet data = new WhatsnewDataSet();
                WhatsnewDataSet.search_eip_t_whatsnewRow paramRow = data.search_eip_t_whatsnew.Newsearch_eip_t_whatsnewRow();
                paramRow.user_id = SettingManager.UserId;
                data.search_eip_t_whatsnew.Rows.Add(paramRow);
                WhatsnewModel m = new WhatsnewModel();
                m.Execute(m.GetWhatsnewInfo, data);

                // 新着情報を取り出す
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < data.eip_t_whatsnew_blog.Count; i++)
                {
                    if (i == 0)
                    {
                        sb.AppendLine("[ ブログ ] 新着記事");
                    }
                    string whatsnew_id = data.eip_t_whatsnew_blog[i].whatsnew_id;
                    string title = data.eip_t_whatsnew_blog[i].title;
                    string last_name = data.eip_t_whatsnew_blog[i].last_name;
                    string first_name = data.eip_t_whatsnew_blog[i].first_name;
                    string update_date = data.eip_t_whatsnew_blog[i].update_date;

                    // リストに詰める
                    List<UserNameObject> list = new List<UserNameObject>();
                    list.Add(new UserNameObject(last_name, first_name));
                    BalloonItemBlog item = new BalloonItemBlog(whatsnew_id, title, null, list, update_date);
                    notificationDic.Add("blog" + i.ToString(), item);
                    sb.AppendLine(item.ToString());
                }

                for (int i = 0; i < data.eip_t_whatsnew_blog_comment.Count; i++)
                {
                    if (i == 0)
                    {
                        sb.AppendLine("[ ブログ ] 新着コメント");
                    }

                    // 同じブログに対して複数のコメントがある場合、カンマ区切りで表示する。
                    string entry_id = data.eip_t_whatsnew_blog_comment[i].entry_id;
                    DataRow[] row = data.eip_t_whatsnew_blog_comment.Select("entry_id = " + entry_id, "update_date desc");

                    List<UserNameObject> list = new List<UserNameObject>();

                    StringBuilder sbName = new StringBuilder();
                    for (int j = 0; j < row.Length; j++)
                    {
                        string last_name = (string)row[j].ItemArray[3];
                        string first_name = (string)row[j].ItemArray[4];
                        if (sbName.Length != 0)
                        {
                            sbName.Append(",");
                        }
                        sbName.Append(last_name);
                        sbName.Append(" ");
                        sbName.Append(first_name);

                        list.Add(new UserNameObject(last_name, first_name));
                    }

                    string whatsnew_id = data.eip_t_whatsnew_blog_comment[i].whatsnew_id;
                    string title = data.eip_t_whatsnew_blog_comment[i].title;
                    string update_date = data.eip_t_whatsnew_blog_comment[i].update_date;

                    i += row.Length;

                    BalloonItemBlogComment item = new BalloonItemBlogComment(whatsnew_id, title, null, list, update_date);
                    notificationDic.Add("blog_comment" + i.ToString(), item);
                    sb.AppendLine(item.ToString());
                }

                for (int i = 0; i < data.eip_t_whatsnew_msgboard.Count; i++)
                {
                    if (i == 0)
                    {
                        sb.AppendLine("[ 掲示板 ]  新しい書き込み");
                    }
                    string whatsnew_id = data.eip_t_whatsnew_msgboard[i].whatsnew_id;
                    string topic_id = data.eip_t_whatsnew_msgboard[i].topic_id;
                    string topic_name = data.eip_t_whatsnew_msgboard[i].topic_name;
                    string last_name = data.eip_t_whatsnew_msgboard[i].last_name;
                    string first_name = data.eip_t_whatsnew_msgboard[i].first_name;
                    string update_date = data.eip_t_whatsnew_msgboard[i].update_date;

                    // リストに詰める
                    List<UserNameObject> list = new List<UserNameObject>();
                    list.Add(new UserNameObject(last_name, first_name));
                    BalloonItemMsgboard item = new BalloonItemMsgboard(topic_id, topic_name, null, list, update_date);
                    notificationDic.Add("msgboard" + i.ToString(), item);
                    sb.AppendLine(item.ToString());
                }

                for (int i = 0; i < data.eip_t_whatsnew_schedule.Count; i++)
                {
                    if (i == 0)
                    {
                        sb.AppendLine("[ スケジュール ] 新着予定");
                    }
                    string whatsnew_id = data.eip_t_whatsnew_schedule[i].whatsnew_id;
                    string name = data.eip_t_whatsnew_schedule[i].name;
                    string last_name = data.eip_t_whatsnew_schedule[i].last_name;
                    string first_name = data.eip_t_whatsnew_schedule[i].first_name;
                    string update_date = data.eip_t_whatsnew_schedule[i].update_date;

                    // リストに詰める
                    List<UserNameObject> list = new List<UserNameObject>();
                    list.Add(new UserNameObject(last_name, first_name));
                    BalloonItemSchedule item = new BalloonItemSchedule(whatsnew_id, name, null, list, update_date);
                    notificationDic.Add("schedule" + i.ToString(), item);
                    sb.AppendLine(item.ToString());
                }

                for (int i = 0; i < data.eip_t_whatsnew_workflow.Count; i++)
                {
                    if (i == 0)
                    {
                        sb.AppendLine("[ ワークフロー ] 新着依頼");
                    }
                    string whatsnew_id = data.eip_t_whatsnew_workflow[i].whatsnew_id;
                    string request_name = data.eip_t_whatsnew_workflow[i].request_name;
                    string category_name = data.eip_t_whatsnew_workflow[i].category_name;
                    string last_name = data.eip_t_whatsnew_workflow[i].last_name;
                    string first_name = data.eip_t_whatsnew_workflow[i].first_name;
                    string update_date = data.eip_t_whatsnew_workflow[i].update_date;

                    // リストに詰める
                    List<UserNameObject> list = new List<UserNameObject>();
                    list.Add(new UserNameObject(last_name, first_name));
                    BalloonItemWorkflow item = new BalloonItemWorkflow(whatsnew_id, category_name, request_name, list, update_date);
                    notificationDic.Add("workflow" + i.ToString(), item);
                    sb.AppendLine(item.ToString());
                }

                for (int i = 0; i < data.eip_t_whatsnew_memo.Count; i++)
                {
                    if (i == 0)
                    {
                        sb.AppendLine("[ 伝言メモ ]  新着メモ");
                    }
                    string whatsnew_id = data.eip_t_whatsnew_memo[i].whatsnew_id;
                    string client_name = data.eip_t_whatsnew_memo[i].client_name;
                    string subject_type = data.eip_t_whatsnew_memo[i].subject_type;
                    string custom_subject = data.eip_t_whatsnew_memo[i].custom_subject;
                    string last_name = data.eip_t_whatsnew_memo[i].last_name;
                    string first_name = data.eip_t_whatsnew_memo[i].first_name;
                    string update_date = data.eip_t_whatsnew_memo[i].update_date;
                    string subject = "";

                    switch (subject_type)
                    {
                        case "0":
                            subject = custom_subject;
                            break;
                        case "1":
                            subject = MessageConstants.SUBJECT_TYPE1;
                            break;
                        case "2":
                            subject = MessageConstants.SUBJECT_TYPE2;
                            break;
                        case "3":
                            subject = MessageConstants.SUBJECT_TYPE3;
                            break;
                        case "4":
                            subject = MessageConstants.SUBJECT_TYPE4;
                            break;
                    }

                    // リストに詰める
                    List<UserNameObject> list = new List<UserNameObject>();
                    list.Add(new UserNameObject(last_name, first_name));
                    BalloonItemMemo item = new BalloonItemMemo(whatsnew_id, client_name, subject, list, update_date);
                    notificationDic.Add("memo" + i.ToString(), item);
                    sb.AppendLine(item.ToString());
                }

                if (!String.IsNullOrEmpty(sb.ToString()))
                {
                    // タスクトレイアイコンを点滅させる
                    this.AnimatedTasktrayIcon(true);

                    if (checkBoxInformation.Checked)
                    {
                        if (!alertWindow1.isShow())
                        {
                            alertWindow1.ShowCloseButton = true;
                            alertWindow1.BackColor = Color.White;
                            alertWindow1.BackColor2 = Color.FromArgb(255, 136, 0);
                            alertWindow1.Time = 10000;
                            alertWindow1.Icon = AlertIcons.Custom;
                            alertWindow1.CustomIcon = Properties.Resources.information_orange;
                            alertWindow1.Show(sb.ToString(), MessageConstants.MSG_INFORMATION_01);
                        }
                    }
                    else
                    {
                        // バルーンを表示する
                        this.notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                        this.notifyIcon1.BalloonTipTitle = MessageConstants.MSG_INFORMATION_02;
                        this.notifyIcon1.BalloonTipText = sb.ToString();
                        this.notifyIcon1.ShowBalloonTip(10000);
                    }
                }
                else
                {
                    // タスクトレイアイコンの点滅を停止
                    this.AnimatedTasktrayIcon(false);
                }
            }
            catch (DBException ex)
            {
                Debug.Print(ex.toString());
            }
        }

        /// <summary>
        /// 定期実行(Aipo6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WhatsnewProcessV7(bool isCheckNow, bool isStartup)
        {
            if (!isLogin)
            {
                return;
            }
            if (String.IsNullOrEmpty(SettingManager.UserId) ||
                String.IsNullOrEmpty(SettingManager.LoginName) ||
                String.IsNullOrEmpty(SettingManager.UserPassword))
            {
                return;
            }

            DateTime dt = DateTime.Now;
            /*
             * タイマーで5分間隔で新着情報をチェックするため、現在の時刻が5分、10分…55分かチェックし、
             * 5分、10分…55分の場合のみ、以降の処理を実行する。
             * ただし、isCheckNowがtrueの場合(起動時や設定保存時、今すぐチェックする場合)はそのまま以降の処理を実行する。
             */
            if (!isCheckNow)
            {
                if (dt.Minute != 0 &&
                    dt.Minute != 5 &&
                    dt.Minute != 10 &&
                    dt.Minute != 15 &&
                    dt.Minute != 20 &&
                    dt.Minute != 25 &&
                    dt.Minute != 30 &&
                    dt.Minute != 35 &&
                    dt.Minute != 40 &&
                    dt.Minute != 45 &&
                    dt.Minute != 50 &&
                    dt.Minute != 55)
                {
                    return;
                }
            }

            try
            {
                notificationDic = new Dictionary<string, BalloonItem>();

                ScheduleManager sm = new ScheduleManager(dt);
                List<ScheduleItem> oneDayScheduleList = null;
                // 終日スケジュールをチェック
                if (isStartup)
                {
                    oneDayScheduleList = sm.CheckOneDaySchedule();
                }

                // もうすぐ始まるスケジュールをチェック
                List<ScheduleItem> scheduleList = new List<ScheduleItem>();
                scheduleList = sm.CheckSchedule();

                if (scheduleList != null && scheduleList.Count > 0 || oneDayScheduleList != null && oneDayScheduleList.Count > 0)
                {
                    Form2 f = new Form2();
                    if (scheduleList != null && scheduleList.Count > 0)
                    {
                        f.ScheduleList = scheduleList;
                    }
                    if (oneDayScheduleList != null && oneDayScheduleList.Count > 0)
                    {
                        f.OneDayScheduleList = oneDayScheduleList;
                    }
                    f.Show();
                }

                // あなた宛てのお知らせを取得するためのDataSetと検索条件を設定
                ActivityDataSet data = new ActivityDataSet();
                ActivityDataSet.search_activityRow paramRow = data.search_activity.Newsearch_activityRow();
                paramRow.login_name = SettingManager.LoginName;
                data.search_activity.Rows.Add(paramRow);
                ActivityModel m = new ActivityModel();
                m.Execute(m.GetActivityInfo, data);

                // 新着情報を取り出す
                StringBuilder sb = new StringBuilder();
                foreach (ActivityDataSet.activityRow activity in data.activity)
                {
                    // リストに詰める
                    ActivityItem item = new ActivityItem(activity.id,
                                                         activity.last_name,
                                                         activity.first_name,
                                                         activity.title, 
                                                         activity.update_date);
                    sb.AppendLine(item.ToString());
                }

                if (!String.IsNullOrEmpty(sb.ToString()))
                {
                    // タスクトレイアイコンを点滅させる
                    this.AnimatedTasktrayIcon(true);

                    if (checkBoxInformation.Checked)
                    {
                        if (!alertWindow1.isShow())
                        {
                            alertWindow1.ShowCloseButton = true;
                            alertWindow1.BackColor = Color.White;
                            alertWindow1.BackColor2 = Color.FromArgb(255, 136, 0);
                            alertWindow1.Time = 10000;
                            alertWindow1.Icon = AlertIcons.Custom;
                            alertWindow1.CustomIcon = Properties.Resources.information_orange;
                            alertWindow1.Show(sb.ToString(), MessageConstants.MSG_INFORMATION_01);
                        }
                    }
                    else
                    {
                        // バルーンを表示する
                        this.notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                        this.notifyIcon1.BalloonTipTitle = MessageConstants.MSG_INFORMATION_02;
                        this.notifyIcon1.BalloonTipText = sb.ToString();
                        this.notifyIcon1.ShowBalloonTip(10000);
                    }
                }
                else
                {
                    // タスクトレイアイコンの点滅を停止
                    this.AnimatedTasktrayIcon(false);
                }
            }
            catch (DBException ex)
            {
                Debug.Print(ex.toString());
            }
        }

#endregion

#region 入力チェック関連

        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <returns></returns>
        private bool InputCheck()
        {
            int ret = 0;

            // ログイン名
            ret = Validator.InputCheck(textBoxUserName.Text, true, textBoxUserName.MaxLength, Validator.HALF_ALPHA_NUM_SYMBOL);
            if (ret == Validator.UNINPUT)
            {
                // 必須
                string msg = String.Format(MessageConstants.CHK_MUST, MessageConstants.ITEM_LOGIN);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_001, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (ret == Validator.LENGTH)
            {
                // 文字数
                string msg = String.Format(MessageConstants.CHK_HALF_LENGTH, MessageConstants.ITEM_LOGIN, textBoxUserName.MaxLength);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_001, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (ret == Validator.HALF_ALPHA_NUM_SYMBOL)
            {
                // 文字種
                string msg = String.Format(MessageConstants.CHK_HALF_ALPHA_NUM_SYMBOL, MessageConstants.ITEM_LOGIN);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_001, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // パスワード
            ret = Validator.InputCheck(textBoxPassword.Text, true, textBoxPassword.MaxLength, Validator.HALF_ALPHA_NUM_SYMBOL);
            if (ret == Validator.UNINPUT)
            {
                // 必須
                string msg = String.Format(MessageConstants.CHK_MUST, MessageConstants.ITEM_PASSWORD);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_001, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (ret == Validator.LENGTH)
            {
                // 文字数
                string msg = String.Format(MessageConstants.CHK_HALF_LENGTH, MessageConstants.ITEM_PASSWORD, textBoxPassword.MaxLength);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_001, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (ret == Validator.HALF_ALPHA_NUM_SYMBOL)
            {
                // 文字種
                string msg = String.Format(MessageConstants.CHK_HALF_ALPHA_NUM_SYMBOL, MessageConstants.ITEM_PASSWORD);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_001, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // URL
            ret = Validator.InputCheck(textBoxURL.Text, true, textBoxURL.MaxLength, Validator.URL);
            if (ret == Validator.UNINPUT)
            {
                // 必須
                string msg = String.Format(MessageConstants.CHK_MUST, MessageConstants.ITEM_URL);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_001, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (ret == Validator.LENGTH)
            {
                // 文字数
                string msg = String.Format(MessageConstants.CHK_HALF_LENGTH, MessageConstants.ITEM_URL, textBoxURL.MaxLength);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_001, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (ret == Validator.URL)
            {
                // 文字種
                string msg = String.Format(MessageConstants.CHK_URL, MessageConstants.ITEM_URL);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_001, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // サーバIP
            ret = Validator.InputCheck(textBoxServerIP.Text, true, textBoxServerIP.MaxLength, 0);
            if (ret == Validator.UNINPUT)
            {
                // 必須
                string msg = String.Format(MessageConstants.CHK_MUST, MessageConstants.ITEM_DB_SERVER_IP);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_002, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (ret == Validator.LENGTH)
            {
                // 文字数
                string msg = String.Format(MessageConstants.CHK_HALF_LENGTH, MessageConstants.ITEM_DB_SERVER_IP, textBoxServerIP.MaxLength);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_002, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // ポート
            ret = Validator.InputCheck(textBoxServerPort.Text, true, textBoxServerPort.MaxLength, Validator.HALF_NUM);
            if (ret == Validator.UNINPUT)
            {
                // 必須
                string msg = String.Format(MessageConstants.CHK_MUST, MessageConstants.ITEM_DB_PORT);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_002, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (ret == Validator.LENGTH)
            {
                // 文字数
                string msg = String.Format(MessageConstants.CHK_HALF_LENGTH, MessageConstants.ITEM_DB_PORT, textBoxServerPort.MaxLength);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_002, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (ret == Validator.HALF_NUM)
            {
                // 文字種
                string msg = String.Format(MessageConstants.CHK_HALF_NUM, MessageConstants.ITEM_DB_PORT);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_002, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // DBユーザID
            ret = Validator.InputCheck(textBoxDbUserId.Text, true, textBoxDbUserId.MaxLength, Validator.HALF_ALPHA_NUM_SYMBOL);
            if (ret == Validator.UNINPUT)
            {
                // 必須
                string msg = String.Format(MessageConstants.CHK_MUST, MessageConstants.ITEM_DB_USER_ID);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_002, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (ret == Validator.LENGTH)
            {
                // 文字数
                string msg = String.Format(MessageConstants.CHK_HALF_LENGTH, MessageConstants.ITEM_DB_USER_ID, textBoxDbUserId.MaxLength);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_002, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (ret == Validator.HALF_ALPHA_NUM_SYMBOL)
            {
                // 文字種
                string msg = String.Format(MessageConstants.CHK_HALF_ALPHA_NUM_SYMBOL, MessageConstants.ITEM_DB_USER_ID);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_002, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // DBパスワード
            ret = Validator.InputCheck(textBoxDbPassword.Text, true, textBoxDbPassword.MaxLength, Validator.HALF_ALPHA_NUM_SYMBOL);
            if (ret == Validator.UNINPUT)
            {
                // 必須
                string msg = String.Format(MessageConstants.CHK_MUST, MessageConstants.ITEM_DB_PASSWORD);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_002, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (ret == Validator.LENGTH)
            {
                // 文字数
                string msg = String.Format(MessageConstants.CHK_HALF_LENGTH, MessageConstants.ITEM_DB_PASSWORD, textBoxDbPassword.MaxLength);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_002, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (ret == Validator.HALF_ALPHA_NUM_SYMBOL)
            {
                // 文字種
                string msg = String.Format(MessageConstants.CHK_HALF_ALPHA_NUM_SYMBOL, MessageConstants.ITEM_DB_PASSWORD);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_002, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // データベース
            ret = Validator.InputCheck(textBoxDbName.Text, true, textBoxDbName.MaxLength, Validator.HALF_ALPHA_NUM_SYMBOL);
            if (ret == Validator.UNINPUT)
            {
                // 必須
                string msg = String.Format(MessageConstants.CHK_MUST, MessageConstants.ITEM_DB_NAME);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_002, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (ret == Validator.LENGTH)
            {
                // 文字数
                string msg = String.Format(MessageConstants.CHK_HALF_LENGTH, MessageConstants.ITEM_DB_NAME, textBoxDbName.MaxLength);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_002, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (ret == Validator.HALF_ALPHA_NUM_SYMBOL)
            {
                // 文字種
                string msg = String.Format(MessageConstants.CHK_HALF_ALPHA_NUM_SYMBOL, MessageConstants.ITEM_DB_NAME);
                MessageBox.Show(msg, MessageConstants.MSG_CAPTION_002, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

#endregion

#region ショートカット関連

        /// <summary>
        /// ショートカットを作成
        /// </summary>
        private void CreateShortcut()
        {
            ShellLinkUtility shortcut = new ShellLinkUtility();

            shortcut.TargetPath = Assembly.GetEntryAssembly().Location;
            shortcut.DisplayMode = ShellLinkUtility.ShellLinkDisplayMode.Minimized;
            shortcut.WorkingDirectory = Environment.CurrentDirectory;

            shortcut.Save(shortcutPath);
            shortcut.Dispose();
            shortcut = null;
        }

        /// <summary>
        /// ショートカットを削除
        /// </summary>
        private void DeleteShortcut()
        {
            File.Delete(shortcutPath);
        }

#endregion

#region 新着情報取得処理

        private void timerWhatsnew_Tick(object sender, EventArgs e)
        {
            this.PreWhatsnewProcess(false, false);
        }

#endregion

#region タスクトレイアイコンの点滅処理

        /// <summary>
        /// タスクトレイアイコンの点滅処理
        /// </summary>
        /// <param name="isEnabled">
        /// true:有効
        /// false:無効
        /// </param>
        private void AnimatedTasktrayIcon(bool isEnabled)
        {
            this.currentTasktrayIconIndex = 0;

            // タイマーが動いている時は止め、止まっているときは動かす
            this.timerTasktrayIcon.Enabled = isEnabled;

            if (this.timerTasktrayIcon.Enabled)
            {
                // アニメを開始したときは、初めのアイコンを表示する
                this.ChangeAnimatedTasktrayIcon();
            }
            else
            {
                // アニメが終了した時は、アイコンを元に戻す
                this.notifyIcon1.Icon = Properties.Resources.favicon_16;
            }

        }

        /// <summary>
        /// timerTasktrayIconのTickイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerTasktrayIcon_Tick(object sender, EventArgs e)
        {
            this.ChangeAnimatedTasktrayIcon();
        }

        /// <summary>
        /// アニメ表示時にタスクトレイアイコンを変更する
        /// </summary>
        private void ChangeAnimatedTasktrayIcon()
        {
            // タスクトレイアイコンを変更する
            this.notifyIcon1.Icon = this.tasktrayIcons[this.currentTasktrayIconIndex];

            // 次に表示するアイコンを決める
            this.currentTasktrayIconIndex++;
            if (this.currentTasktrayIconIndex >= this.tasktrayIcons.Length)
                this.currentTasktrayIconIndex = 0;
        }

#endregion

    }
}
