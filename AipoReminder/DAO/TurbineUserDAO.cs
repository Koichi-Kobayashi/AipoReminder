using AipoReminder.DataSet;
using WinFramework.Utility;
using System.Collections;
using System;
using Npgsql;
using System.Data;
using NpgsqlTypes;
using AipoReminder.Utility;

namespace AipoReminder.DAO
{
    class TurbineUserDAO
    {
        private DBHelper dbHelper;

        public TurbineUserDAO(DBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public void GetTurbineUserInfo(System.Data.DataSet data)
        {
            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();

            sqlbldr.AppendLine("select");
            sqlbldr.AppendLine(" user_id");
            sqlbldr.AppendLine(", login_name");
            sqlbldr.AppendLine(", password_value");
            sqlbldr.AppendLine(", first_name");
            sqlbldr.AppendLine(", last_name");
            sqlbldr.AppendLine(", email");
            sqlbldr.AppendLine(", confirm_value");
            sqlbldr.AppendLine(", modified");
            sqlbldr.AppendLine(", created");
            sqlbldr.AppendLine(", last_login");
            sqlbldr.AppendLine(", disabled");
            sqlbldr.AppendLine(", objectdata");
            sqlbldr.AppendLine(", password_changed");
            sqlbldr.AppendLine(", company_id");
            sqlbldr.AppendLine(", position_id");
            sqlbldr.AppendLine(", in_telephone");
            sqlbldr.AppendLine(", out_telephone");
            sqlbldr.AppendLine(", cellular_phone");
            sqlbldr.AppendLine(", cellular_mail");
            sqlbldr.AppendLine(", cellular_uid");
            sqlbldr.AppendLine(", first_name_kana");
            sqlbldr.AppendLine(", last_name_kana");
            sqlbldr.AppendLine(", photo");
            sqlbldr.AppendLine(", created_user_id");
            sqlbldr.AppendLine(", updated_user_id");
            sqlbldr.AppendLine("from turbine_user");
            sqlbldr.AppendLine("where 1 = 1");

            TurbineUserDataSet.search_turbine_userRow param = ((TurbineUserDataSet)data).search_turbine_user[0];

            ArrayList paramList = new ArrayList();
            if (!String.IsNullOrEmpty(param.user_id))
            {
                sqlbldr.AppendLine("    and user_id = :user_id");
                paramList.Add(DBUtility.MakeParameter("user_id", param.user_id, NpgsqlDbType.Integer));
            }

            if (!String.IsNullOrEmpty(param.login_name))
            {
                sqlbldr.AppendLine("    and login_name = :login_name");
                paramList.Add(DBUtility.MakeParameter("login_name", param.login_name, NpgsqlDbType.Varchar));
            }

            if (!String.IsNullOrEmpty(param.password_value))
            {
                sqlbldr.AppendLine("    and password_value = :password_value");
                paramList.Add(DBUtility.MakeParameter("password_value", param.password_value, NpgsqlDbType.Varchar));
            }

            this.dbHelper.Select(((TurbineUserDataSet)data).turbine_user, sqlbldr.ToString(), paramList);

        }

    }
}
