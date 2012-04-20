using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AipoReminder.DataSet;
using AipoReminder.Utility;
using NpgsqlTypes;
using WinFramework.Utility;

namespace AipoReminder.DAO
{
    class ActivityDAO
    {
        private DBHelper dbHelper;

        public ActivityDAO(DBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public int GetActivityInfo(System.Data.DataSet data)
        {
            System.Text.StringBuilder sql = new System.Text.StringBuilder();

            sql.AppendLine("select");
            sql.AppendLine(" t1.id");
            sql.AppendLine(", t1.login_name");
            sql.AppendLine(", t3.last_name");
            sql.AppendLine(", t3.first_name");
            sql.AppendLine(", t1.title");
            sql.AppendLine(", to_char(t1.update_date, 'YYYY/MM/DD') as update_date");
            sql.AppendLine("from");
            sql.AppendLine("    activity t1 join activity_map t2");
            sql.AppendLine("        on t1.id = t2.activity_id");
            sql.AppendLine("    left join turbine_user t3");
            sql.AppendLine("        on t1.login_name = t3.login_name");
            sql.AppendLine("where 1 = 1");
            sql.AppendLine("and t2.is_read = 0");
            sql.AppendLine("and t2.login_name = :login_name");
            sql.AppendLine("order by t1.update_date desc");

            ActivityDataSet.search_activityRow param = ((ActivityDataSet)data).search_activity[0];

            ArrayList paramList = new ArrayList();
            if (!String.IsNullOrEmpty(param.login_name))
            {
                paramList.Add(DBUtility.MakeParameter("login_name", param.login_name, NpgsqlDbType.Text));
            }

            return this.dbHelper.Select(((ActivityDataSet)data).activity, sql.ToString(), paramList);
        }

    }
}
