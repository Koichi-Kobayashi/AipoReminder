using System;
using System.Collections;
using AipoReminder.DataSet;
using Npgsql;
using NpgsqlTypes;
using WinFramework.Utility;
using System.Data;
using AipoReminder.Utility;

namespace AipoReminder.DAO
{
    class ScheduleDAO
    {
        private DBHelper dbHelper;

        public ScheduleDAO(DBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public void GetScheduleInfo(System.Data.DataSet data)
        {
            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();

            sqlbldr.AppendLine("select");
            sqlbldr.AppendLine(" t1.schedule_id");
            sqlbldr.AppendLine(", t2.name");
            sqlbldr.AppendLine(", t1.user_id");
            sqlbldr.AppendLine(", t3.last_name");
            sqlbldr.AppendLine(", t3.first_name");
            sqlbldr.AppendLine(", t2.start_date");
            sqlbldr.AppendLine(", t2.end_date");
            sqlbldr.AppendLine("from eip_t_schedule_map t1");
            sqlbldr.AppendLine("    left join eip_t_schedule t2");
            sqlbldr.AppendLine("        on t1.schedule_id = t2.schedule_id");
            sqlbldr.AppendLine("    left join turbine_user t3");
            sqlbldr.AppendLine("        on t1.user_id = t3.user_id");
            sqlbldr.AppendLine("where 1 = 1");
            sqlbldr.AppendLine("and t2.public_flag = :public_flag");
            sqlbldr.AppendLine("and t2.start_date = :start_date");
            sqlbldr.AppendLine("and t1.user_id != 1");
            sqlbldr.AppendLine("and exists(");
            sqlbldr.AppendLine("        select");
            sqlbldr.AppendLine("         *");
            sqlbldr.AppendLine("        from eip_t_schedule_map t1");
            sqlbldr.AppendLine("            left join eip_t_schedule t2");
            sqlbldr.AppendLine("                on t1.schedule_id = t2.schedule_id");
            sqlbldr.AppendLine("            left join turbine_user t3");
            sqlbldr.AppendLine("                on t1.user_id = t3.user_id");
            sqlbldr.AppendLine("        where 1 = 1");
            sqlbldr.AppendLine("        and t1.user_id = :user_id");
            sqlbldr.AppendLine("        and t2.public_flag = :public_flag");
            sqlbldr.AppendLine("        and t2.start_date = :start_date");
            sqlbldr.AppendLine("    )");

            ScheduleDataSet.search_eip_t_scheduleRow param = ((ScheduleDataSet)data).search_eip_t_schedule[0];

            ArrayList paramList = new ArrayList();
            if (!String.IsNullOrEmpty(param.user_id))
            {
                paramList.Add(DBUtility.MakeParameter("user_id", param.user_id, NpgsqlDbType.Integer));
            }
            if (!String.IsNullOrEmpty(param.public_flag))
            {
                paramList.Add(DBUtility.MakeParameter("public_flag", param.public_flag, NpgsqlDbType.Varchar));
            }
            if (!String.IsNullOrEmpty(param.start_date))
            {
                paramList.Add(DBUtility.MakeParameter("start_date", param.start_date, NpgsqlDbType.Timestamp));
            }

            this.dbHelper.Select(((ScheduleDataSet)data).eip_t_schedule, sqlbldr.ToString(), paramList);
        }

        private ArrayList MakeParam(ScheduleDataSet.search_eip_t_scheduleRow row)
        {
            ArrayList paramList = new ArrayList();

            if (!String.IsNullOrEmpty(row.user_id))
            {
                NpgsqlParameter param = new NpgsqlParameter(":user_id", NpgsqlDbType.Integer);
                param.Direction = ParameterDirection.Input;
                param.SourceColumn = "user_id";
                param.Value = row.user_id;
                paramList.Add(param);
            }

            if (!String.IsNullOrEmpty(row.public_flag))
            {
                NpgsqlParameter param = new NpgsqlParameter(":public_flag", NpgsqlDbType.Varchar);
                param.Direction = ParameterDirection.Input;
                param.SourceColumn = "public_flag";
                param.Value = row.public_flag;
                paramList.Add(param);
            }

            if (!String.IsNullOrEmpty(row.start_date))
            {
                NpgsqlParameter param = new NpgsqlParameter(":start_date", NpgsqlDbType.Timestamp);
                param.Direction = ParameterDirection.Input;
                param.SourceColumn = "start_date";
                param.Value = row.start_date;
                paramList.Add(param);
            }

            return paramList;
        }
    }
}
