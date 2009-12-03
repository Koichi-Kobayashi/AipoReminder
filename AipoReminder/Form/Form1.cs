using System;
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
using AipoReminder.DataSet;
using AipoReminder.Manager;
using AipoReminder.Model;
using AipoReminder.Utility;
using AipoReminder.ValueObject;
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
            // タスクトレイアイコン用タイマーを無効にしておく（初めはアニメしない）
            this.timerTasktrayIcon.Enabled = false;
            // アニメ時は、1秒毎にアイコンを変更する
            this.timerTasktrayIcon.Interval = 1000;

            // タスクトレイにアニメで表示するアイコンを指定する
            this.currentTasktrayIconIndex = 0;
            this.tasktrayIcons = new Icon[] {
                Properties.Resources.favicon_16_red,
                Properties.Resources.favicon_16_blue};

            // 設定を読み込む
            this.SetUserData();

            // 新着情報をチェック
            this.WhatsnewProcess(true);
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
            }
        }

        /// <summary>
        /// 設定情報からログイン名を取得し、ログイン名、パスワードを非活性にする
        /// </summary>
        private void SetUserData()
        {
            // ウィンドウ非表示
            this.Visible = false;
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

            // AipoVersionコンボボックスのSelectedIndexを設定
            switch (SettingManager.AipoVersion)
            {
                case 4:
                    comboBoxAipoVersion.SelectedIndex = 0;
                    break;
                case 5:
                    comboBoxAipoVersion.SelectedIndex = 1;
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

            // バージョン情報
            Assembly asm = Assembly.GetExecutingAssembly();
            Version ver = asm.GetName().Version;
//            textBoxVersionInfo.Text = Application.ProductName + " Version " + ver.Major + "." + ver.Minor + "." + ver.Build;
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

            if (String.IsNullOrEmpty(SettingManager.UserId) ||
                String.IsNullOrEmpty(SettingManager.LoginName) ||
                String.IsNullOrEmpty(SettingManager.UserPassword))
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

                this.ActiveWindow();
                return;
            }

            textBoxUserName.Text = SettingManager.LoginName;                // ログイン名
            textBoxURL.Text = SettingManager.URL;                           // URL
            comboBoxAipoVersion.SelectedItem = SettingManager.AipoVersion;     // AipoVersion
            comboBoxCheckTime.SelectedItem = SettingManager.CheckTime;      // スケジュールのチェック間隔
            checkBoxAutoRun.Checked = SettingManager.AutoRun;               // 自動起動
            checkBoxAutoLogin.Checked = SettingManager.AutoLogin;           // 自動ログイン
            checkBoxBlog.Checked = SettingManager.CheckBlog;                // ブログの新着記事チェック
            checkBoxBlogComment.Checked = SettingManager.CheckBlogComment;  // ブログの新着コメントチェック
            checkBoxMsgboard.Checked = SettingManager.CheckMsgboard;        // 掲示板の新しい書き込みチェック
            checkBoxSchedule.Checked = SettingManager.CheckSchedule;        // スケジュールの新着予定チェック
            checkBoxWorkflow.Checked = SettingManager.CheckWorkflow;        // ワークフローの新着依頼チェック
            checkBoxMemo.Checked = SettingManager.CheckMemo;                // 伝言メモの新着メモチェック

            string userId = SettingManager.UserId;
            string loginName = SettingManager.LoginName;
            string password = "";

            if (!String.IsNullOrEmpty(SettingManager.UserPassword))
            {
                password = Security.EncryptPassword(SettingManager.UserPassword);
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

                    // 入力項目を非活性にする
                    textBoxUserName.Enabled = false;
                    textBoxPassword.Enabled = false;
                    textBoxURL.Enabled = false;
                    comboBoxAipoVersion.Enabled = false;
                    buttonDataSave.Enabled = false;
                    textBoxServerIP.Enabled = false;
                    textBoxServerPort.Enabled = false;
                    textBoxDbUserId.Enabled = false;
                    textBoxDbPassword.Enabled = false;
                    textBoxDbName.Enabled = false;

                    // 自動ログイン用html作成
                    SettingManager.AutoLoginHtml = String.Format(Properties.Resources.autologin,
                                                                 String.Format("{0:D4}", SettingManager.URL.Length),
                                                                 SettingManager.URL,
                                                                 SettingManager.LoginName,
                                                                 SettingManager.UserPassword);

                }
            }
            catch (DBException ex)
            {
                Debug.Print(ex.toString());

                // エラーメッセージ
                MessageBox.Show(MessageConstants.ERR_DB_ACCESS, MessageConstants.MSG_CAPTION_001, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // ウィンドウを表示
                this.ActiveWindow();
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

                    // ユーザ情報を保存
                    SettingManager.UserId = data.turbine_user[0].user_id;
                    SettingManager.LoginName = data.turbine_user[0].login_name;
                    SettingManager.UserPassword = textBoxPassword.Text;
                    SettingManager.URL = textBoxURL.Text;
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

                    // 自動ログイン用html作成
                    SettingManager.AutoLoginHtml = String.Format(Properties.Resources.autologin,
                                                                 String.Format("{0:D4}", SettingManager.URL.Length),
                                                                 SettingManager.URL,
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

                    MessageBox.Show(MessageConstants.INFO_ACCOUNT_SETTING_OK, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 新着情報をチェック
                    this.WhatsnewProcess(true);
                }
                else
                {
                    // ログイン名とパスワードが不一致
                    MessageBox.Show(MessageConstants.ERR_PASSWORD, MessageConstants.MSG_CAPTION_001, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (DBException ex)
            {
                Debug.Print(ex.toString());
                // エラーメッセージ
                MessageBox.Show(MessageConstants.ERR_DB_ACCESS, MessageConstants.MSG_CAPTION_001, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            textBoxUserName.Enabled = true;
            textBoxPassword.Enabled = true;
            textBoxURL.Enabled = true;
            comboBoxAipoVersion.Enabled = true;
            buttonDataSave.Enabled = true;
            textBoxServerIP.Enabled = true;
            textBoxServerPort.Enabled = true;
            textBoxDbUserId.Enabled = true;
            textBoxDbPassword.Enabled = true;
            textBoxDbName.Enabled = true;
        }

        ///// <summary>
        ///// ブラウザ起動時に自動ログインする
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void checkBoxAutoLogin_CheckedChanged(object sender, EventArgs e)
        //{
        //    SettingManager.AutoLogin = checkBoxAutoLogin.Checked;
        //}

        ///// <summary>
        ///// Windows起動時にAipoリマインダーを起動する
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void checkBoxAutoRun_CheckedChanged(object sender, EventArgs e)
        //{
        //    SettingManager.AutoRun = checkBoxAutoRun.Checked;

        //    // 自動起動
        //    if (checkBoxAutoRun.Checked)
        //    {
        //        SettingManager.AutoRun = true;

        //        // ショートカットを作成
        //        this.CreateShortcut();
        //    }
        //    else
        //    {
        //        SettingManager.AutoRun = false;

        //        // ショートカットを削除
        //        this.DeleteShortcut();
        //    }
        //}

        ///// <summary>
        ///// スケジュール確認コンボボックスの値を変更した場合の処理
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void comboBoxCheckTime_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //SettingManager.CheckTime = ((ComboItemCheckTime)comboBoxCheckTime.SelectedItem).CheckTime;
        //    SettingManager.CheckTime = int.Parse(comboBoxCheckTime.SelectedItem.ToString());
        //}

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
            SettingManager.AutoRun = checkBoxAutoRun.Checked;               // 自動起動
            SettingManager.AutoLogin = checkBoxAutoLogin.Checked;           // 自動ログイン
            SettingManager.CheckBlog = checkBoxBlog.Checked;                // ブログの新着記事チェック
            SettingManager.CheckBlogComment = checkBoxBlogComment.Checked;  // ブログの新着コメントチェック
            SettingManager.CheckMsgboard = checkBoxMsgboard.Checked;        // 掲示板の新しい書き込みチェック
            SettingManager.CheckSchedule = checkBoxSchedule.Checked;        // スケジュールの新着予定チェック
            SettingManager.CheckWorkflow = checkBoxWorkflow.Checked;        // ワークフローの新着依頼チェック
            SettingManager.CheckMemo = checkBoxMemo.Checked;                // 伝言メモの新着メモチェック

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

            MessageBox.Show(MessageConstants.INFO_WHATSNEW_SETTING_OK, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

#region old

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    //Clock(null, null);

        //    // 作成先
        //    string shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "Aipo4 リマインダ.lnk" );

        //    ShellLinkUtility shortcut = new ShellLinkUtility();

        //    //shortcut.Description = "Aipo4 リマインダのショートカットです。";
        //    shortcut.TargetPath = Assembly.GetEntryAssembly().Location;
        //    shortcut.DisplayMode = ShellLinkUtility.ShellLinkDisplayMode.Minimized;
        //    shortcut.WorkingDirectory = Environment.CurrentDirectory;

        //    shortcut.Save(shortcutPath);
        //    shortcut.Dispose();
        //    shortcut = null;
        //}

        //private void buttonChangeSetting_Click(object sender, EventArgs e)
        //{

        //    if (comboBoxUserName.SelectedIndex > 0)
        //    {
        //        ComboBoxItem item = (ComboBoxItem)comboBoxUserName.SelectedItem;
        //        string password = Security.EncryptPassword(textBoxPassword.Text);

        //        if (item.Password.Equals(password))
        //        {
        //            // ComboBoxで選択されたユーザのパスワードと入力されたパスワードが一致
        //            SettingManager.UserId = item.Id;
        //            SettingManager.UserPassword = item.Password;

        //            MessageBox.Show(MessageConstants.INFO_SETTING_OK, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            // パスワードが不一致
        //            MessageBox.Show(MessageConstants.ERR_PASSWORD, MessageConstants.MSG_CAPTION_001, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //    }

        //}

#endregion

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
            this.WhatsnewProcess(true);
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
            ThreadingManager threadingManager = new ThreadingManager();

            Thread thread = new Thread(new ThreadStart(threadingManager.Run));

            thread.Start();
        }

#endregion

#region メイン処理

        /// <summary>
        /// 定期実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WhatsnewProcess(bool isCheckNow)
        {
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

                // もうすぐ始まるスケジュールをチェック
                ScheduleManager sm = new ScheduleManager(dt);
                string msg = sm.CheckSchedule();

                StringBuilder sb = new StringBuilder();
                if (!String.IsNullOrEmpty(msg))
                {
                    Form2 f = new Form2();
                    f.Show();
                    f.TextBoxScheduleInfoText = msg;
                }

                // 新着情報を取得するためのDataSetと検索条件を設定
                WhatsnewDataSet data = new WhatsnewDataSet();
                WhatsnewDataSet.search_eip_t_whatsnewRow paramRow = data.search_eip_t_whatsnew.Newsearch_eip_t_whatsnewRow();
                paramRow.user_id = SettingManager.UserId;
                data.search_eip_t_whatsnew.Rows.Add(paramRow);
                WhatsnewModel m = new WhatsnewModel();
                m.Execute(m.GetWhatsnewInfo, data);

                // 新着情報を取り出す
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

                    // バルーンを表示する
                    this.notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                    this.notifyIcon1.BalloonTipTitle = "お知らせ";
                    this.notifyIcon1.BalloonTipText = sb.ToString();
                    this.notifyIcon1.ShowBalloonTip(10000);
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

#region old

        ///// <summary>
        ///// コンボボックスにユーザ情報をセット
        ///// </summary>
        //private void SetComboBox()
        //{
        //    try
        //    {
        //        // ユーザ一覧を取得
        //        TurbineUserDataSet data = this.GetTurbineUserData("", "", "");

        //        int selectedIndex = -1;
        //        for (int i = 0; i < data.turbine_user.Count; i++)
        //        {
        //            TurbineUserDataSet.turbine_userRow row = data.turbine_user[i];
        //            ComboBoxItem item = new ComboBoxItem(row.user_id, row.last_name + " " + row.first_name, row.password_value);
        //            //comboBoxUserName.Items.Add(item);

        //            if (row.user_id.Equals(SettingManager.UserId))
        //            {
        //                selectedIndex = i;
        //            }
        //        }

        //        if (!String.IsNullOrEmpty(SettingManager.UserId) && !String.IsNullOrEmpty(SettingManager.UserPassword) && selectedIndex != -1)
        //        {
        //            // 既に設定ファイルが存在する場合
        //            //comboBoxUserName.SelectedIndex = selectedIndex;
        //        }

        //    }
        //    catch (DBException ex)
        //    {
        //        Debug.Print(ex.toString());
        //    }

        //}

#endregion

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
            this.WhatsnewProcess(false);
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
