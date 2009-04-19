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
        private static string m_userId;

        /// <summary>
        /// ログイン名
        /// </summary>
        private static string m_loginName;

        /// <summary>
        /// パスワード
        /// </summary>
        private static string m_userPassword;

        /// <summary>
        /// 定期チェック(分)
        /// </summary>
        private static int m_checkTime;

        /// <summary>
        /// AipoのURL
        /// </summary>
        private static string m_url;

        /// <summary>
        /// 自動ログイン用html
        /// </summary>
        private static string m_autoLoginHtml;

        /// <summary>
        /// 自動起動フラグ
        /// </summary>
        private static bool m_checkAutoRun;

        /// <summary>
        /// 自動ログインフラグ
        /// </summary>
        private static bool m_checkAutoLogin;

        /// <summary>
        /// 接続文字列(サーバIP)
        /// </summary>
        private static string m_NpgsqlConnectionServer;

        /// <summary>
        /// 接続文字列(ポート)
        /// </summary>
        private static string m_NpgsqlConnectionPort;

        /// <summary>
        /// 接続文字列(ユーザID)
        /// </summary>
        private static string m_NpgsqlConnectionUserId;

        /// <summary>
        /// 接続文字列(パスワード)
        /// </summary>
        private static string m_NpgsqlConnectionPassword;

        /// <summary>
        /// 接続文字列(データベース名)
        /// </summary>
        private static string m_NpgsqlConnectionDatabase;

        /// <summary>
        /// ブログの新着記事をチェックする
        /// </summary>
        private static bool m_checkBlog;

        /// <summary>
        /// ブログの新着コメントをチェックする
        /// </summary>
        private static bool m_checkBlogComment;

        /// <summary>
        /// 掲示板の新しい書き込みをチェックする
        /// </summary>
        private static bool m_checkMsgboard;

        /// <summary>
        /// スケジュールの新着予定をチェックする
        /// </summary>
        private static bool m_checkSchedule;

        /// <summary>
        /// ワークフローの新着依頼をチェックする
        /// </summary>
        private static bool m_checkWorkflow;

        /// <summary>
        /// 伝言メモの新着メモをチェックする
        /// </summary>
        private static bool m_checkMemo;

        static SettingManager()
        {
            m_userId = Properties.Settings.Default.userId;
            m_loginName = Properties.Settings.Default.loginName;
            m_userPassword = Properties.Settings.Default.userPassword;
            m_checkTime = Properties.Settings.Default.checkTime;
            m_url = Properties.Settings.Default.url;
            m_checkAutoRun = Properties.Settings.Default.checkAutoRun;
            m_checkAutoLogin = Properties.Settings.Default.checkAutoLogin;
            m_NpgsqlConnectionServer = Properties.Settings.Default.NpgsqlConnectionServer;
            m_NpgsqlConnectionPort = Properties.Settings.Default.NpgsqlConnectionPort;
            m_NpgsqlConnectionUserId = Properties.Settings.Default.NpgsqlConnectionUserId;
            m_NpgsqlConnectionPassword = Properties.Settings.Default.NpgsqlConnectionPassword;
            m_NpgsqlConnectionDatabase = Properties.Settings.Default.NpgsqlConnectionDatabase;
            m_checkBlog = Properties.Settings.Default.checkBlog;
            m_checkBlogComment = Properties.Settings.Default.checkBlogComment;
            m_checkMsgboard = Properties.Settings.Default.checkMsgboard;
            m_checkSchedule = Properties.Settings.Default.checkSchedule;
            m_checkWorkflow = Properties.Settings.Default.checkWorkflow;
            m_checkMemo = Properties.Settings.Default.checkMemo;
        }

        /// <summary>
        /// ユーザIDを取得または設定する
        /// </summary>
        public static string UserId
        {
            set
            {
                m_userId = value;
            }
            get
            {
                return m_userId;
            }
        }

        /// <summary>
        /// ログイン名を取得または設定する
        /// </summary>
        public static string LoginName
        {
            set
            {
                m_loginName = value;
            }
            get
            {
                return m_loginName;
            }
        }

        /// <summary>
        /// パスワードを取得または設定する
        /// </summary>
        public static string UserPassword
        {
            set
            {
                m_userPassword = Security.EncryptData(value);
            }
            get
            {
                if (String.IsNullOrEmpty(m_userPassword))
                {
                    return "";
                }
                else
                {
                    return Security.UnEncryptData(m_userPassword);
                }
            }        
        }

        /// <summary>
        /// 定期チェック(分)を取得または設定する
        /// </summary>
        public static int CheckTime
        {
            set
            {
                m_checkTime = value;
            }
            get
            {
                return m_checkTime;
            }
        }

        /// <summary>
        /// AipoのURLを取得または設定する
        /// </summary>
        public static string URL
        {
            set
            {
                m_url = value;
            }
            get
            {
                return m_url;
            }
        }

        /// <summary>
        /// 自動ログイン用htmlを取得または設定する
        /// </summary>
        public static string AutoLoginHtml
        {
            set
            {
                m_autoLoginHtml = value;
            }
            get
            {
                return m_autoLoginHtml;
            }
        }

        /// <summary>
        /// 自動起動フラグを取得または設定する
        /// </summary>
        public static bool AutoRun
        {
            set
            {
                m_checkAutoRun = value;
            }
            get
            {
                return m_checkAutoRun;
            }
        }

        /// <summary>
        /// 自動ログインフラグを取得または設定する
        /// </summary>
        public static bool AutoLogin
        {
            set
            {
                m_checkAutoLogin = value;
            }
            get
            {
                return m_checkAutoLogin;
            }
        }

        /// <summary>
        /// 接続文字列(サーバIP)を取得または設定する
        /// </summary>
        public static string NpgsqlConnectionServer
        {
            set
            {
                m_NpgsqlConnectionServer = value;
            }
            get
            {
                return m_NpgsqlConnectionServer;
            }
        }

        /// <summary>
        /// 接続文字列(ポート)を取得または設定する
        /// </summary>
        public static string NpgsqlConnectionPort
        {
            set
            {
                m_NpgsqlConnectionPort = value;
            }
            get
            {
                return m_NpgsqlConnectionPort;
            }
        }

        /// <summary>
        /// 接続文字列(ユーザID)を取得または設定する
        /// </summary>
        public static string NpgsqlConnectionUserId
        {
            set
            {
                m_NpgsqlConnectionUserId = value;
            }
            get
            {
                return m_NpgsqlConnectionUserId;
            }
        }

        /// <summary>
        /// 接続文字列(パスワード)を取得または設定する
        /// </summary>
        public static string NpgsqlConnectionPassword
        {
            set
            {
                m_NpgsqlConnectionPassword = Security.EncryptData(value);
            }
            get
            {
                if (String.IsNullOrEmpty(m_NpgsqlConnectionPassword))
                {
                    m_NpgsqlConnectionPassword = Security.EncryptData(aipoPassword);
                    return aipoPassword;
                }
                else
                {
                    return Security.UnEncryptData(m_NpgsqlConnectionPassword);
                }
            }
        }

        /// <summary>
        /// 接続文字列(データベース名)を取得または設定する
        /// </summary>
        public static string NpgsqlConnectionDatabase
        {
            set
            {
                m_NpgsqlConnectionDatabase = value;
            }
            get
            {
                return m_NpgsqlConnectionDatabase;
            }
        }

        /// <summary>
        /// ブログの新着記事チェックフラグを取得または設定する
        /// </summary>
        public static bool CheckBlog
        {
            set
            {
                m_checkBlog = value;
            }
            get
            {
                return m_checkBlog;
            }
        }

        /// <summary>
        /// ブログの新着コメントチェックフラグを取得または設定する
        /// </summary>
        public static bool CheckBlogComment
        {
            set
            {
                m_checkBlogComment = value;
            }
            get
            {
                return m_checkBlogComment;
            }
        }

        /// <summary>
        /// 掲示板の新しい書き込みチェックフラグを取得または設定する
        /// </summary>
        public static bool CheckMsgboard
        {
            set
            {
                m_checkMsgboard = value;
            }
            get
            {
                return m_checkMsgboard;
            }
        }

        /// <summary>
        /// スケジュールの新着予定チェックフラグを取得または設定する
        /// </summary>
        public static bool CheckSchedule
        {
            set
            {
                m_checkSchedule = value;
            }
            get
            {
                return m_checkSchedule;
            }
        }

        /// <summary>
        /// ワークフローの新着依頼チェックフラグを取得または設定する
        /// </summary>
        public static bool CheckWorkflow
        {
            set
            {
                m_checkWorkflow = value;
            }
            get
            {
                return m_checkWorkflow;
            }
        }

        /// <summary>
        /// 伝言メモの新着メモチェックフラグを取得または設定する
        /// </summary>
        public static bool CheckMemo
        {
            set
            {
                m_checkMemo = value;
            }
            get
            {
                return m_checkMemo;
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
                !String.IsNullOrEmpty(SettingManager.URL))
            {
                Properties.Settings.Default.userId = SettingManager.UserId;
                Properties.Settings.Default.loginName = SettingManager.LoginName;
                Properties.Settings.Default.userPassword = Security.EncryptData(SettingManager.UserPassword);
                Properties.Settings.Default.url = SettingManager.URL;

                Properties.Settings.Default.NpgsqlConnectionServer = SettingManager.NpgsqlConnectionServer;
                Properties.Settings.Default.NpgsqlConnectionPort = SettingManager.NpgsqlConnectionPort;
                Properties.Settings.Default.NpgsqlConnectionUserId = SettingManager.NpgsqlConnectionUserId;
                Properties.Settings.Default.NpgsqlConnectionPassword = Security.EncryptData(SettingManager.NpgsqlConnectionPassword);
                Properties.Settings.Default.NpgsqlConnectionDatabase = SettingManager.NpgsqlConnectionDatabase;

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
