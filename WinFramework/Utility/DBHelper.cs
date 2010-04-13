using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

#if Npgsql
using Npgsql;
using WinFramework.Exceptions;
#else
using Npgsql;
using WinFramework.Exceptions;
#endif

namespace WinFramework.Utility
{
    /// <summary>
    /// 汎用DBクラス
    /// </summary>
    public class DBHelper
    {

#region メンバ変数

        private string server;
        private string port;
        private string userid;
        private string password;
        private string database;
        private string timeout;
        private string commandTimeout;
        private string connectionLifeTime;

        ConnectionStringSettings settings = null;
#if Npgsql
        NpgsqlConnection connection = null;
        NpgsqlTransaction transaction = null;
#else
        NpgsqlConnection connection = null;
        NpgsqlTransaction transaction = null;
#endif

#endregion

#region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBHelper()
        {
            DbConnect();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="server">サーバIP</param>
        /// <param name="port">ポート</param>
        /// <param name="userid">ユーザID</param>
        /// <param name="password">パスワード</param>
        /// <param name="database">データベース名</param>
        public DBHelper(string server, string port, string userid, string password, string database)
        {
            DbConnect(server, port, userid, password, database);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="server">サーバIP</param>
        /// <param name="port">ポート</param>
        /// <param name="userid">ユーザID</param>
        /// <param name="password">パスワード</param>
        /// <param name="database">データベース名</param>
        /// <param name="timeout">接続が開くまでの秒単位の待ち時間。デフォルトは１５</param>
        public DBHelper(string server, string port, string userid, string password, string database, string timeout)
        {
            DbConnect(server, port, userid, password, database, timeout);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="server">サーバIP</param>
        /// <param name="port">ポート</param>
        /// <param name="userid">ユーザID</param>
        /// <param name="password">パスワード</param>
        /// <param name="database">データベース名</param>
        /// <param name="timeout">接続が開くまでの秒単位の待ち時間。デフォルトは１５</param>
        /// <param name="commandTimeout">例外を投げる前にコマンドが実効を終了するまでの待ち時間。秒単位。デフォルトは２０</param>
        public DBHelper(string server, string port, string userid, string password, string database, string timeout, string commandTimeout)
        {
            DbConnect(server, port, userid, password, database, timeout, commandTimeout);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="server">サーバIP</param>
        /// <param name="port">ポート</param>
        /// <param name="userid">ユーザID</param>
        /// <param name="password">パスワード</param>
        /// <param name="database">データベース名</param>
        /// <param name="timeout">接続が開くまでの秒単位の待ち時間。デフォルトは１５</param>
        /// <param name="commandTimeout">例外を投げる前にコマンドが実効を終了するまでの待ち時間。秒単位。デフォルトは２０</param>
        /// <param name="connectionLifeTime">秒単位の、プール内非使用接続を閉じるまでの待ち時間。デフォルトは１５ </param>
        public DBHelper(string server, string port, string userid, string password, string database, string timeout, string commandTimeout, string connectionLifeTime)
        {
            DbConnect(server, port, userid, password, database, timeout, commandTimeout, connectionLifeTime);
        }
#endregion

#region DB接続

        /// <summary>
        /// DB接続
        /// </summary>
        /// <returns></returns>
        public DbConnection GetConnection()
        {
            try
            {
#if Npgsql
            this.connection = new NpgsqlConnection();
            this.connection.ConnectionString = this.settings.ConnectionString;
#else
            this.connection = new NpgsqlConnection();
            this.connection.ConnectionString = this.settings.ConnectionString;
#endif
            }
            catch (NullReferenceException ex)
            {
                throw new DBException("", ex);
            }
            return this.connection;
        }

        /// <summary>
        /// DBオープン
        /// </summary>
        public void Open()
        {
            this.GetConnection().Open();
        }

        /// <summary>
        /// DBクローズ
        /// </summary>
        public void Close()
        {
            if (this.transaction != null)
            {
                this.transaction.Dispose();
                this.transaction = null;
            }

            if (this.connection != null)
            {
                this.connection.Close();
            }
        }

        /// <summary>
        /// トランザクション開始
        /// </summary>
        public void BeginTransaction()
        {
            this.transaction = this.connection.BeginTransaction();
        }

        /// <summary>
        /// コミット
        /// </summary>
        public void Commit()
        {
            if (this.transaction != null)
            {
                this.transaction.Commit();
            }
        }

        /// <summary>
        /// ロールバック
        /// </summary>
        public void Rollback()
        {
            if (this.transaction != null)
            {
                this.transaction.Rollback();
            }
        }

#endregion

#region DB接続設定

        /// <summary>
        /// DB接続設定読み込み
        /// </summary>
        /// <param name="server">サーバIP</param>
        /// <param name="port">ポート</param>
        /// <param name="userid">ユーザID</param>
        /// <param name="password">パスワード</param>
        /// <param name="database">データベース名</param>
        public void DbConnect(string server, string port, string userid, string password, string database)
        {
            this.server = server;
            this.port = port;
            this.userid = userid;
            this.password = password;
            this.database = database;
            this.timeout = "15";
            this.commandTimeout = "20";
            this.connectionLifeTime = "15";

            DbConnect();
        }

        /// <summary>
        /// DB接続設定読み込み
        /// </summary>
        /// <param name="server">サーバIP</param>
        /// <param name="port">ポート</param>
        /// <param name="userid">ユーザID</param>
        /// <param name="password">パスワード</param>
        /// <param name="database">データベース名</param>
        /// <param name="timeout">接続が開くまでの秒単位の待ち時間。デフォルトは１５</param>
        public void DbConnect(string server, string port, string userid, string password, string database, string timeout)
        {
            this.server = server;
            this.port = port;
            this.userid = userid;
            this.password = password;
            this.database = database;
            if (String.IsNullOrEmpty(timeout))
            {
                this.timeout = "15";
            }
            else
            {
                this.timeout = timeout;
            }
            this.commandTimeout = "20";
            this.connectionLifeTime = "15";

            DbConnect();
        }

        /// <summary>
        /// DB接続設定読み込み
        /// </summary>
        /// <param name="server">サーバIP</param>
        /// <param name="port">ポート</param>
        /// <param name="userid">ユーザID</param>
        /// <param name="password">パスワード</param>
        /// <param name="database">データベース名</param>
        /// <param name="timeout">接続が開くまでの秒単位の待ち時間。デフォルトは１５</param>
        /// <param name="commandTimeout">例外を投げる前にコマンドが実効を終了するまでの待ち時間。秒単位。デフォルトは２０</param>
        public void DbConnect(string server, string port, string userid, string password, string database, string timeout, string commandTimeout)
        {
            this.server = server;
            this.port = port;
            this.userid = userid;
            this.password = password;
            this.database = database;
            if (String.IsNullOrEmpty(timeout))
            {
                this.timeout = "15";
            }
            else
            {
                this.timeout = timeout;
            }
            if (String.IsNullOrEmpty(commandTimeout))
            {
                this.commandTimeout = "20";
            }
            else
            {
                this.commandTimeout = commandTimeout;
            }
            this.connectionLifeTime = "15";

            DbConnect();
        }

        /// <summary>
        /// DB接続設定読み込み
        /// </summary>
        /// <param name="server">サーバIP</param>
        /// <param name="port">ポート</param>
        /// <param name="userid">ユーザID</param>
        /// <param name="password">パスワード</param>
        /// <param name="database">データベース名</param>
        /// <param name="timeout">接続が開くまでの秒単位の待ち時間。デフォルトは１５</param>
        /// <param name="commandTimeout">例外を投げる前にコマンドが実効を終了するまでの待ち時間。秒単位。デフォルトは２０</param>
        /// <param name="connectionLifeTime">秒単位の、プール内非使用接続を閉じるまでの待ち時間。デフォルトは１５ </param>
        public void DbConnect(string server, string port, string userid, string password, string database, string timeout, string commandTimeout, string connectionLifeTime)
        {
            this.server = server;
            this.port = port;
            this.userid = userid;
            this.password = password;
            this.database = database;
            if (String.IsNullOrEmpty(timeout))
            {
                this.timeout = "15";
            }
            else
            {
                this.timeout = timeout;
            }
            if (String.IsNullOrEmpty(commandTimeout))
            {
                this.commandTimeout = "20";
            }
            else
            {
                this.commandTimeout = commandTimeout;
            }
            if (String.IsNullOrEmpty(connectionLifeTime))
            {
                this.connectionLifeTime = "15";
            }
            else
            {
                this.connectionLifeTime = connectionLifeTime;
            }

            DbConnect();
        }

        /// <summary>
        /// DB接続設定読み込み
        /// </summary>
        private void DbConnect()
        {
#if Npgsql
            if (String.IsNullOrEmpty(this.server) ||
                String.IsNullOrEmpty(this.port) ||
                String.IsNullOrEmpty(this.userid) ||
                String.IsNullOrEmpty(this.password) ||
                String.IsNullOrEmpty(this.database))
            {
                this.settings = ConfigurationManager.ConnectionStrings["PostgreSql"];
            }
            else
            {
                ConnectionStringSettings con = new ConnectionStringSettings();
                con.ConnectionString = "Server=" + this.server
                                                + ";Port=" + this.port
                                                + ";User Id=" + this.userid
                                                + ";Password=" + this.password
                                                + ";Database=" + this.database
                                                + ";Timeout=" + this.timeout
                                                + ";CommandTimeout=" + this.commandTimeout
                                                + ";ConnectionLifeTime=" + this.connectionLifeTime + ";";
                con.Name = "PostgreSql";
                con.ProviderName = "Npgsql";
                this.settings = con;
            }
#else
            if (String.IsNullOrEmpty(this.server)   ||
                String.IsNullOrEmpty(this.port) ||
                String.IsNullOrEmpty(this.userid) ||
                String.IsNullOrEmpty(this.password) ||
                String.IsNullOrEmpty(this.database))
            {
               this.settings = ConfigurationManager.ConnectionStrings["PostgreSql"];
            }
            else
            {
                ConnectionStringSettings con = new ConnectionStringSettings();
                con.ConnectionString = "Server=" + this.server + ";Port=" + this.port + ";User Id=" + this.userid + ";Password=" + this.password + ";Database=" + this.database + ";";
                con.Name = "PostgreSql";
                con.ProviderName = "Npgsql";
                this.settings = con;
            }
#endif
        }

#endregion

#region Select実行

        /// <summary>
        /// Select文発行
        /// </summary>
        /// <param name="data"></param>
        /// <param name="commandText"></param>
        /// <param name="param"></param>
        public int Select(DataTable data, string commandText, ArrayList paramList)
        {
#if Npgsql
            NpgsqlCommand command;
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter();
#else
            NpgsqlCommand command;
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter();
#endif

            try
            {
                command = this.connection.CreateCommand();
                command.Transaction = this.transaction;
                command.CommandText = commandText;

                if (paramList != null)
                {
#if Npgsql
                    foreach (NpgsqlParameter param in paramList)
#else
                    foreach (NpgsqlParameter param in paramList)
#endif
                    {
                        command.Parameters.Add(param);
                    }
                }

                dataAdapter.SelectCommand = command;

#if DEBUG
                Debug.WriteLine("【SQL】");
                Debug.WriteLine(ParamForLogString(command));
#endif
                return dataAdapter.Fill(data);

            }
#if Npgsql
            catch (NpgsqlException e)
            {
                LogUtility.WriteLogError("" , e);
                return -1;
            }
#endif
            catch (Exception e)
            {
                LogUtility.WriteLogError("", e);
                return -1;
            }

        }

#endregion

#region Insert実行

        /// <summary>
        /// Insert文発行
        /// </summary>
        /// <param name="data"></param>
        /// <param name="commandText"></param>
        /// <param name="param"></param>
        public int Insert(DataTable data, string commandText, ArrayList paramList)
        {
#if Npgsql
            NpgsqlCommand command;
#else
            NpgsqlCommand command;
#endif

            try
            {
                command = this.connection.CreateCommand();
                command.Transaction = this.transaction;
                command.CommandText = commandText;

                if (paramList != null)
                {
#if Npgsql
                    foreach (NpgsqlParameter param in paramList)
#else
                    foreach (NpgsqlParameter param in paramList)
#endif
                    {
                        command.Parameters.Add(param);
                    }
                }

#if DEBUG
                Debug.WriteLine("【SQL】");
                Debug.WriteLine(ParamForLogString(command));
#endif
                return command.ExecuteNonQuery();

            }
#if Npgsql
            catch (NpgsqlException e)
            {
                LogUtility.WriteLogError("", e);
                return -1;
            }
#endif
            catch (Exception e)
            {
                LogUtility.WriteLogError("", e);
                return -1;
            }

        }

#endregion

#region Update実行

        /// <summary>
        /// Insert文発行
        /// </summary>
        /// <param name="data"></param>
        /// <param name="commandText"></param>
        /// <param name="param"></param>
        public int Update(DataTable data, string commandText, ArrayList paramList)
        {
#if Npgsql
            NpgsqlCommand command;
#else
            NpgsqlCommand command;
#endif

            try
            {
                command = this.connection.CreateCommand();
                command.Transaction = this.transaction;
                command.CommandText = commandText;

                if (paramList != null)
                {
#if Npgsql
                    foreach (NpgsqlParameter param in paramList)
#else
                    foreach (NpgsqlParameter param in paramList)
#endif
                    {
                        command.Parameters.Add(param);
                    }
                }

#if DEBUG
                Debug.WriteLine("【SQL】");
                Debug.WriteLine(ParamForLogString(command));
#endif
                return command.ExecuteNonQuery();

            }
#if Npgsql
            catch (NpgsqlException e)
            {
                LogUtility.WriteLogError("", e);
                return -1;
            }
#endif
            catch (Exception e)
            {
                LogUtility.WriteLogError("", e);
                return -1;
            }

        }

#endregion

#region SQLログ

#if Npgsql
    /// <summary>
    /// SQLのバインド変数を値に変換する
    /// </summary>
    /// <param name="command">NpgsqlCommand</param>
    /// <returns></returns>
    private string ParamForLogString(NpgsqlCommand command)
    {
        string str = command.CommandText;

        foreach (DbParameter param in command.Parameters)
        {
            str = str.Replace(param.ParameterName, param.Value.ToString());
        }

        return str;
    }
#else
    /// <summary>
    /// SQLのバインド変数を値に変換する
    /// </summary>
    /// <param name="command">NpgsqlCommand</param>
    /// <returns></returns>
    private string ParamForLogString(NpgsqlCommand command)
    {
        string str = command.CommandText;

        foreach (DbParameter param in command.Parameters)
        {
            str = str.Replace(param.ParameterName, param.Value.ToString());
        }

        return str;
    }
#endif

#endregion

    }
}
