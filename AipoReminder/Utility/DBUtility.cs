using System.Data;
using Npgsql;
using NpgsqlTypes;

namespace AipoReminder.Utility
{
    public static class DBUtility
    {
        public static NpgsqlParameter MakeParameter(string paramName, string value, DbType dbType)
        {
            NpgsqlParameter param = new NpgsqlParameter(":" + paramName, dbType);
            param.Direction = ParameterDirection.Input;
            param.SourceColumn = paramName;
            param.Value = value;

            return param;
        }

        public static NpgsqlParameter MakeParameter(string paramName, string value, NpgsqlDbType dbType)
        {
            NpgsqlParameter param = new NpgsqlParameter(":" + paramName, dbType);
            param.Direction = ParameterDirection.Input;
            param.SourceColumn = paramName;
            param.Value = value;

            return param;
        }

        public static NpgsqlParameter MakeParameter(string paramName, string value, NpgsqlDbType dbType, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(":" + paramName, dbType);
            param.Direction = direction;
            param.SourceColumn = paramName;
            param.Value = value;

            return param;
        }
    }
}
