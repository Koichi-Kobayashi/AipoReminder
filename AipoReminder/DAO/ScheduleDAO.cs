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

            // 単発スケジュール
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
            sqlbldr.AppendLine("and t1.user_id = :user_id");
            sqlbldr.AppendLine("and t2.public_flag in ('O', 'C', 'P')");
            sqlbldr.AppendLine("and t2.start_date = :start_date");
            sqlbldr.AppendLine("and t2.repeat_pattern = 'N'");

            sqlbldr.AppendLine("union all");

            // 毎日繰り返すスケジュール
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
            sqlbldr.AppendLine("and t1.user_id = :user_id");
            sqlbldr.AppendLine("and t2.public_flag in ('O', 'C', 'P')");
            sqlbldr.AppendLine("and t2.start_date like :like_start_date");
            sqlbldr.AppendLine("and t2.repeat_pattern = 'DN'");

            ScheduleDataSet.search_eip_t_scheduleRow param = ((ScheduleDataSet)data).search_eip_t_schedule[0];

            ArrayList paramList = new ArrayList();
            if (!String.IsNullOrEmpty(param.user_id))
            {
                paramList.Add(DBUtility.MakeParameter("user_id", param.user_id, NpgsqlDbType.Integer));
            }
            if (!String.IsNullOrEmpty(param.start_date))
            {
                paramList.Add(DBUtility.MakeParameter("start_date", param.start_date, NpgsqlDbType.Timestamp));
            }
            if (!String.IsNullOrEmpty(param.start_date))
            {
                // '%HH:MM:SS'形式にする
                String[] start_date_split = param.start_date.Split(' ');
                string start_date = "%" + start_date_split[1];
                paramList.Add(DBUtility.MakeParameter("like_start_date", start_date, NpgsqlDbType.Varchar));
            }

            this.dbHelper.Select(((ScheduleDataSet)data).eip_t_schedule, sqlbldr.ToString(), paramList);
        }

    }
}
