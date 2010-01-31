using System;
using WinFramework.Utility;

namespace AipoReminder.Utility
{
    public static class SettingManager
    {
        // パスワード
        private const string aipoPassword = "reminder";

        /// <summary>
        /// ユーザID
        /// </summary>
        public static string UserId { get; set; }

        /// <summary>
        /// ログイン名
        /// </summary>
        public static string LoginName { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        private static string _UserPassword;

        /// <summary>
        /// Aipoバージョン
        /// </summary>
        public static int AipoVersion { get; set; }

        /// <summary>
        /// 定期チェック(分)
        /// </summary>
        public static int CheckTime { get; set; }

        /// <summary>
        /// AipoのURL
        /// </summary>
        public static string Url { get; set; }

        /// <summary>
        /// 自動ログイン用html
        /// </summary>
        public static string AutoLoginHtml { get; set; }

        /// <summary>
        /// 自動起動フラグ
        /// </summary>
        public static bool AutoRun { get; set; }

        /// <summary>
        /// 自動ログインフラグ
        /// </summary>
        public static bool AutoLogin { get; set; }

        /// <summary>
        /// 接続文字列(サーバIP)
        /// </summary>
        public static string NpgsqlConnectionServer { get; set; }

        /// <summary>
        /// 接続文字列(ポート)
        /// </summary>
        public static string NpgsqlConnectionPort { get; set; }

        /// <summary>
        /// 接続文字列(ユーザID)
        /// </summary>
        public static string NpgsqlConnectionUserId { get; set; }

        /// <summary>
        /// 接続文字列(パスワード)
        /// </summary>
        private static string _NpgsqlConnectionPassword;

        /// <summary>
        /// 接続文字列(データベース名)
        /// </summary>
        public static string NpgsqlConnectionDatabase { get; set; }

        /// <summary>
        /// 接続文字列(タイムアウト)
        /// </summary>
        public static string NpgsqlConnectionTimeout { get; set; }

        /// <summary>
        /// ブログの新着記事をチェックする
        /// </summary>
        public static bool CheckBlog { get; set; }

        /// <summary>
        /// ブログの新着コメントをチェックする
        /// </summary>
        public static bool CheckBlogComment { get; set; }

        /// <summary>
        /// 掲示板の新しい書き込みをチェックする
        /// </summary>
        public static bool CheckMsgboard { get; set; }

        /// <summary>
        /// スケジュールの新着予定をチェックする
        /// </summary>
        public static bool CheckSchedule { get; set; }

        /// <summary>
        /// ワークフローの新着依頼をチェックする
        /// </summary>
        public static bool CheckWorkflow { get; set; }

        /// <summary>
        /// 伝言メモの新着メモをチェックする
        /// </summary>
        public static bool CheckMemo { get; set; }

        /// <summary>
        /// 他のユーザのスケジュールもチェックするかどうか
        /// </summary>
        public static bool CheckOtherSchedule { get; set; }

        /// <summary>
        /// チェックするユーザID一覧
        /// </summary>
        public static string GroupUserId { get; set; }

        /// <summary>
        /// お知らせを吹き出しからウィンドウタイプに変更するかどうか
        /// </summary>
        public static bool CheckInformation { get; set; }

        static SettingManager()
        {
            UserId = Properties.Settings.Default.userId;
            LoginName = Properties.Settings.Default.loginName;
            _UserPassword = Properties.Settings.Default.userPassword;
            AipoVersion = Properties.Settings.Default.aipoVersion;
            CheckTime = Properties.Settings.Default.checkTime;
            Url = Properties.Settings.Default.url;
            AutoRun = Properties.Settings.Default.checkAutoRun;
            AutoLogin = Properties.Settings.Default.checkAutoLogin;
            NpgsqlConnectionServer = Properties.Settings.Default.NpgsqlConnectionServer;
            NpgsqlConnectionPort = Properties.Settings.Default.NpgsqlConnectionPort;
            NpgsqlConnectionUserId = Properties.Settings.Default.NpgsqlConnectionUserId;
            _NpgsqlConnectionPassword = Properties.Settings.Default.NpgsqlConnectionPassword;
            NpgsqlConnectionDatabase = Properties.Settings.Default.NpgsqlConnectionDatabase;
            NpgsqlConnectionTimeout = Properties.Settings.Default.NpgsqlConnectionTimeout;
            CheckBlog = Properties.Settings.Default.checkBlog;
            CheckBlogComment = Properties.Settings.Default.checkBlogComment;
            CheckMsgboard = Properties.Settings.Default.checkMsgboard;
            CheckSchedule = Properties.Settings.Default.checkSchedule;
            CheckWorkflow = Properties.Settings.Default.checkWorkflow;
            CheckMemo = Properties.Settings.Default.checkMemo;
            CheckOtherSchedule = Properties.Settings.Default.checkOtherSchedule;
            CheckInformation = Properties.Settings.Default.checkInformation;

            // カンマ区切りのユーザIDの中に数字ではない文字が含まれていた場合、ユーザIDを取得しない
            bool isNotDigit = false;
            if (!String.IsNullOrEmpty(Properties.Settings.Default.checkGroupUserId))
            {
                foreach (string str in Properties.Settings.Default.checkGroupUserId.Split(','))
                {
                    for (int i = 0; i < str.Length; i++ )
                    {
                        if (!Char.IsDigit(str, i))
                        {
                            isNotDigit = true;
                            break;
                        }
                    }
                }
            }
            if (!isNotDigit)
            {
                GroupUserId = Properties.Settings.Default.checkGroupUserId;
            }
        }

        /// <summary>
        /// パスワードを取得または設定する
        /// </summary>
        public static string UserPassword
        {
            set
            {
                _UserPassword = Security.EncryptData(value);
            }
            get
            {
                if (String.IsNullOrEmpty(_UserPassword))
                {
                    return "";
                }
                else
                {
                    return Security.UnEncryptData(_UserPassword);
                }
            }        
        }

        /// <summary>
        /// 接続文字列(パスワード)を取得または設定する
        /// </summary>
        public static string NpgsqlConnectionPassword
        {
            set
            {
                _NpgsqlConnectionPassword = Security.EncryptData(value);
            }
            get
            {
                if (String.IsNullOrEmpty(_NpgsqlConnectionPassword))
                {
                    _NpgsqlConnectionPassword = Security.EncryptData(aipoPassword);
                    return aipoPassword;
                }
                else
                {
                    return Security.UnEncryptData(_NpgsqlConnectionPassword);
                }
            }
        }

        /// <summary>
        /// アカウント情報設定を保存する
        /// </summary>
        public static void AccountInfoSave()
        {
            // ユーザIDとパスワードを保存
            if (!String.IsNullOrEmpty(SettingManager.UserId) &&
                !String.IsNullOrEmpty(SettingManager.LoginName) &&
                !String.IsNullOrEmpty(SettingManager.UserPassword) &&
                !String.IsNullOrEmpty(SettingManager.Url))
            {
                Properties.Settings.Default.userId = SettingManager.UserId;
                Properties.Settings.Default.loginName = SettingManager.LoginName;
                Properties.Settings.Default.userPassword = Security.EncryptData(SettingManager.UserPassword);
                Properties.Settings.Default.url = SettingManager.Url;
                Properties.Settings.Default.aipoVersion = SettingManager.AipoVersion;

                Properties.Settings.Default.NpgsqlConnectionServer = SettingManager.NpgsqlConnectionServer;
                Properties.Settings.Default.NpgsqlConnectionPort = SettingManager.NpgsqlConnectionPort;
                Properties.Settings.Default.NpgsqlConnectionUserId = SettingManager.NpgsqlConnectionUserId;
                Properties.Settings.Default.NpgsqlConnectionPassword = Security.EncryptData(SettingManager.NpgsqlConnectionPassword);
                Properties.Settings.Default.NpgsqlConnectionDatabase = SettingManager.NpgsqlConnectionDatabase;
                Properties.Settings.Default.NpgsqlConnectionTimeout = SettingManager.NpgsqlConnectionTimeout;

                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// アカウント情報設定を保存する
        /// </summary>
        public static void WhatsnewInfoSave()
        {
            // 自動起動
            Properties.Settings.Default.checkAutoRun = SettingManager.AutoRun;
            // 自動ログイン
            Properties.Settings.Default.checkAutoLogin = SettingManager.AutoLogin;
            // スケジュールチェック間隔
            Properties.Settings.Default.checkTime = SettingManager.CheckTime;
            // 新着情報
            Properties.Settings.Default.checkBlog = SettingManager.CheckBlog;
            Properties.Settings.Default.checkBlogComment = SettingManager.CheckBlogComment;
            Properties.Settings.Default.checkMsgboard = SettingManager.CheckMsgboard;
            Properties.Settings.Default.checkSchedule = SettingManager.CheckSchedule;
            Properties.Settings.Default.checkWorkflow = SettingManager.CheckWorkflow;
            Properties.Settings.Default.checkMemo = SettingManager.CheckMemo;
            // 他のユーザのスケジュール確認
            Properties.Settings.Default.checkOtherSchedule = SettingManager.CheckOtherSchedule;
            Properties.Settings.Default.checkGroupUserId = SettingManager.GroupUserId;
            // お知らせの表示形式(吹き出しかウィンドウか)
            Properties.Settings.Default.checkInformation = SettingManager.CheckInformation;

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 接続設定を一時保存する
        /// </summary>
        public static void ConnectionSettingPreSave()
        {
            
            Properties.Settings.Default.NpgsqlConnectionServer = SettingManager.NpgsqlConnectionServer;
            Properties.Settings.Default.NpgsqlConnectionPort = SettingManager.NpgsqlConnectionPort;
            Properties.Settings.Default.NpgsqlConnectionUserId = SettingManager.NpgsqlConnectionUserId;
            Properties.Settings.Default.NpgsqlConnectionPassword = Security.EncryptData(SettingManager.NpgsqlConnectionPassword);
            Properties.Settings.Default.NpgsqlConnectionDatabase = SettingManager.NpgsqlConnectionDatabase;
            Properties.Settings.Default.NpgsqlConnectionTimeout = SettingManager.NpgsqlConnectionTimeout;

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 設定をリセットする
        /// </summary>
        public static void Reset()
        {
            Properties.Settings.Default.Reset();
        }
    }
}
