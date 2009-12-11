using System;
using System.Data;
using WinFramework.Utility;
using WinFramework.Exceptions;

#if Npgsql
using Npgsql;
#else
using Npgsql;
#endif

namespace WinFramework.Model
{
    public class AbstractModel
    {
        public delegate void DelegateMethod(DataSet data);

        protected DBHelper dbHelper;

        public AbstractModel()
        {
            this.dbHelper = new DBHelper();
        }

        public AbstractModel(string server, string port, string userid, string password, string database)
        {
            this.dbHelper = new DBHelper(server, port, userid, password, database);
        }

        public AbstractModel(string server, string port, string userid, string password, string database, string timeout)
        {
            this.dbHelper = new DBHelper(server, port, userid, password, database, timeout);
        }

        public AbstractModel(string server, string port, string userid, string password, string database, string timeout, string commandTimeout)
        {
            this.dbHelper = new DBHelper(server, port, userid, password, database, timeout, commandTimeout);
        }

        public AbstractModel(string server, string port, string userid, string password, string database, string timeout, string commandTimeout, string connectionLifeTime)
        {
            this.dbHelper = new DBHelper(server, port, userid, password, database, timeout, commandTimeout, connectionLifeTime);
        }

        public void Execute(DelegateMethod method, DataSet data)
        {
            try
            {
                this.dbHelper.Open();
                this.dbHelper.BeginTransaction();
                method.Invoke(data);
                this.dbHelper.Commit();
            }
#if Npgsql
            catch (NpgsqlException e)
            {
                this.dbHelper.Rollback();
                LogUtility.WriteLogError("", e);
                throw new DBException("", e);
            }
#else
            catch (NpgsqlException e)
            {
                this.dbHelper.Rollback();
                LogUtility.WriteLogError("", e);
                throw new DBException("", e);
            }
#endif
            catch (Exception e)
            {
                this.dbHelper.Rollback();
                LogUtility.WriteLogError("", e);
                throw new DBException("", e);
            }
            finally
            {
                this.dbHelper.Close();
            }
        }
    }
}
