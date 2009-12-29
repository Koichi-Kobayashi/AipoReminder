using System;
using System.Collections;
using AipoReminder.DataSet;
using AipoReminder.Utility;
using NpgsqlTypes;
using WinFramework.Utility;

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
            sqlbldr.AppendLine("and disabled = 'F'");

            TurbineUserDataSet.search_turbine_userRow param = ((TurbineUserDataSet)data).search_turbine_user[0];

            ArrayList paramList = new ArrayList();
            if (!String.IsNullOrEmpty(param.user_id))
            {
                sqlbldr.AppendLine("and user_id = :user_id");
                paramList.Add(DBUtility.MakeParameter("user_id", param.user_id, NpgsqlDbType.Integer));
            }

            if (!String.IsNullOrEmpty(param.login_name))
            {
                sqlbldr.AppendLine("and login_name = :login_name");
                paramList.Add(DBUtility.MakeParameter("login_name", param.login_name, NpgsqlDbType.Varchar));
            }

            if (!String.IsNullOrEmpty(param.password_value))
            {
                sqlbldr.AppendLine("and password_value = :password_value");
                paramList.Add(DBUtility.MakeParameter("password_value", param.password_value, NpgsqlDbType.Varchar));
            }

            sqlbldr.AppendLine("order by user_id");

            this.dbHelper.Select(((TurbineUserDataSet)data).turbine_user, sqlbldr.ToString(), paramList);

        }

        public void GetTurbineGroupUserInfo(System.Data.DataSet data)
        {
            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();

            sqlbldr.AppendLine("select");
            sqlbldr.AppendLine("t3.user_id");
            sqlbldr.AppendLine(",t3.last_name");
            sqlbldr.AppendLine(",t3.first_name");
            sqlbldr.AppendLine("from turbine_group t1");
            sqlbldr.AppendLine("    left join turbine_user_group_role t2");
            sqlbldr.AppendLine("        on t1.group_id = t2.group_id");
            sqlbldr.AppendLine("    left join turbine_user t3");
            sqlbldr.AppendLine("        on t2.user_id = t3.user_id");
            sqlbldr.AppendLine("where 1 = 1");
            sqlbldr.AppendLine("and t3.disabled = 'F'");

            TurbineUserDataSet.search_turbine_groupRow param = ((TurbineUserDataSet)data).search_turbine_group[0];

            ArrayList paramList = new ArrayList();
            if (!String.IsNullOrEmpty(param.group_id))
            {
                if ("0".Equals(param.group_id))
                {
                    sqlbldr.AppendLine("and t1.group_id = '2'");
                }
                else
                {
                    sqlbldr.AppendLine("and t1.group_id = :group_id");
                    paramList.Add(DBUtility.MakeParameter("group_id", param.group_id, NpgsqlDbType.Integer));
                }
            }

            if (!String.IsNullOrEmpty(param.not_disp_user_id))
            {
                sqlbldr.AppendLine("and t3.user_id != :not_disp_user_id");
                paramList.Add(DBUtility.MakeParameter("not_disp_user_id", param.not_disp_user_id, NpgsqlDbType.Integer));
            }

            sqlbldr.AppendLine("order by t3.user_id");

            this.dbHelper.Select(((TurbineUserDataSet)data).turbine_group_user, sqlbldr.ToString(), paramList);

        }

    }
}
