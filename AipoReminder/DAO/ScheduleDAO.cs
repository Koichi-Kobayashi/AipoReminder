using System;
using System.Collections;
using AipoReminder.DataSet;
using AipoReminder.Utility;
using NpgsqlTypes;
using WinFramework.Utility;
using System.Data;

namespace AipoReminder.DAO
{
    class ScheduleDAO
    {
        private DBHelper dbHelper;

        public ScheduleDAO(DBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        /// <summary>
        /// 終日スケジュールの取得
        /// </summary>
        /// <param name="data"></param>
        public void GetOneDayScheduleInfo(System.Data.DataSet data)
        {
            ScheduleDataSet.search_eip_t_scheduleRow param = ((ScheduleDataSet)data).search_eip_t_schedule[0];

            string nowDateTime = DateTime.Now.ToString("yyyy-MM-dd");

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
            sqlbldr.AppendLine("and t1.user_id = :user_id");
            sqlbldr.AppendLine("and t1.status != 'D'");
            sqlbldr.AppendLine("and t2.start_date = '" + nowDateTime + " 00:00:00'");
            sqlbldr.AppendLine("and t2.repeat_pattern = 'S'");

            ArrayList paramList = new ArrayList();
            if (!String.IsNullOrEmpty(param.user_id))
            {
                paramList.Add(DBUtility.MakeParameter("user_id", param.user_id, NpgsqlDbType.Integer));
            }

            this.dbHelper.Select(((ScheduleDataSet)data).eip_t_schedule, sqlbldr.ToString(), paramList);
        }

        /// <summary>
        /// もうすぐ始まるスケジュールを取得
        /// </summary>
        /// <param name="data"></param>
        public void GetScheduleInfo(System.Data.DataSet data)
        {
            ScheduleDataSet.search_eip_t_scheduleRow param = ((ScheduleDataSet)data).search_eip_t_schedule[0];

            string nowDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();

            DateTime dt = DateTime.Parse(param.start_date);
            DayOfWeek weekday = dt.DayOfWeek;

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
            sqlbldr.AppendLine("and t1.status != 'D'");
            sqlbldr.AppendLine("and t2.start_date = :start_date");
            sqlbldr.AppendLine("and t2.repeat_pattern = 'N'");

            // 毎日繰り返すスケジュール(繰り返し期間なし)
            sqlbldr.AppendLine("or ");
            sqlbldr.AppendLine("    (");
            sqlbldr.AppendLine("            t1.user_id = :user_id");
            sqlbldr.AppendLine("        and t1.status != 'D'");
            sqlbldr.AppendLine("        and t2.start_date like :like_start_date");
            sqlbldr.AppendLine("        and t2.repeat_pattern = 'DN'");
            sqlbldr.AppendLine("        and not exists");
            sqlbldr.AppendLine("            (");
            sqlbldr.AppendLine("                select * from eip_t_schedule t4");
            sqlbldr.AppendLine("                where 1 = 1");
            sqlbldr.AppendLine("                and t4.parent_id = t1.schedule_id");
            sqlbldr.AppendLine("            )");
            sqlbldr.AppendLine("    )");

            // 毎日繰り返すスケジュール(繰り返し期間あり)
            sqlbldr.AppendLine("or ");
            sqlbldr.AppendLine("    (");
            sqlbldr.AppendLine("            t1.user_id = :user_id");
            sqlbldr.AppendLine("        and t1.status != 'D'");
            sqlbldr.AppendLine("        and t2.start_date like :like_start_date");
            sqlbldr.AppendLine("        and '" + nowDateTime + "' between cast(t2.start_date - interval '" + param.check_time + " minutes' as timestamp) and cast(t2.end_date - interval '" + param.check_time + " minutes' as timestamp)");
            sqlbldr.AppendLine("        and t2.repeat_pattern = 'DL'");
            sqlbldr.AppendLine("        and not exists");
            sqlbldr.AppendLine("            (");
            sqlbldr.AppendLine("                select * from eip_t_schedule t4");
            sqlbldr.AppendLine("                where 1 = 1");
            sqlbldr.AppendLine("                and t4.parent_id = t1.schedule_id");
            sqlbldr.AppendLine("                and '" + nowDateTime + "' between cast(t4.start_date - interval '" + param.check_time + " minutes' as timestamp) and cast(t4.end_date - interval '" + param.check_time + " minutes' as timestamp)");
            sqlbldr.AppendLine("            )");
            sqlbldr.AppendLine("    )");

            // 曜日毎に繰り返すスケジュール(繰り返し期間なし)
            sqlbldr.AppendLine("or ");
            sqlbldr.AppendLine("    (");
            sqlbldr.AppendLine("            t1.user_id = :user_id");
            sqlbldr.AppendLine("        and t1.status != 'D'");
            sqlbldr.AppendLine("        and t2.start_date = :start_date");
            switch (weekday)
            {
                case DayOfWeek.Sunday:
                    sqlbldr.AppendLine("        and t2.repeat_pattern like 'W1______N'");
                    break;
                case DayOfWeek.Monday:
                    sqlbldr.AppendLine("        and t2.repeat_pattern like 'W_1_____N'");
                    break;
                case DayOfWeek.Tuesday:
                    sqlbldr.AppendLine("        and t2.repeat_pattern like 'W__1____N'");
                    break;
                case DayOfWeek.Wednesday:
                    sqlbldr.AppendLine("        and t2.repeat_pattern like 'W___1___N'");
                    break;
                case DayOfWeek.Thursday:
                    sqlbldr.AppendLine("        and t2.repeat_pattern like 'W____1__N'");
                    break;
                case DayOfWeek.Friday:
                    sqlbldr.AppendLine("        and t2.repeat_pattern like 'W_____1_N'");
                    break;
                case DayOfWeek.Saturday:
                    sqlbldr.AppendLine("        and t2.repeat_pattern like 'W______1N'");
                    break;
            }
            sqlbldr.AppendLine("        and not exists");
            sqlbldr.AppendLine("            (");
            sqlbldr.AppendLine("                select * from eip_t_schedule t4");
            sqlbldr.AppendLine("                where 1 = 1");
            sqlbldr.AppendLine("                and t4.parent_id = t1.schedule_id");
            sqlbldr.AppendLine("            )");
            sqlbldr.AppendLine("    )");

            // 曜日毎に繰り返すスケジュール(繰り返し期間あり)
            sqlbldr.AppendLine("or ");
            sqlbldr.AppendLine("    (");
            sqlbldr.AppendLine("            t1.user_id = :user_id");
            sqlbldr.AppendLine("        and t1.status != 'D'");
            sqlbldr.AppendLine("        and t2.start_date like :like_start_date");
            sqlbldr.AppendLine("        and '" + nowDateTime + "' between cast(t2.start_date - interval '" + param.check_time + " minutes' as timestamp) and cast(t2.end_date - interval '" + param.check_time + " minutes' as timestamp)");
            switch (weekday)
            {
                case DayOfWeek.Sunday:
                    sqlbldr.AppendLine("        and t2.repeat_pattern like 'W1______L'");
                    break;
                case DayOfWeek.Monday:
                    sqlbldr.AppendLine("        and t2.repeat_pattern like 'W_1_____L'");
                    break;
                case DayOfWeek.Tuesday:
                    sqlbldr.AppendLine("        and t2.repeat_pattern like 'W__1____L'");
                    break;
                case DayOfWeek.Wednesday:
                    sqlbldr.AppendLine("        and t2.repeat_pattern like 'W___1___L'");
                    break;
                case DayOfWeek.Thursday:
                    sqlbldr.AppendLine("        and t2.repeat_pattern like 'W____1__L'");
                    break;
                case DayOfWeek.Friday:
                    sqlbldr.AppendLine("        and t2.repeat_pattern like 'W_____1_L'");
                    break;
                case DayOfWeek.Saturday:
                    sqlbldr.AppendLine("        and t2.repeat_pattern like 'W______1L'");
                    break;
            }
            sqlbldr.AppendLine("        and not exists");
            sqlbldr.AppendLine("            (");
            sqlbldr.AppendLine("                select * from eip_t_schedule t4");
            sqlbldr.AppendLine("                where 1 = 1");
            sqlbldr.AppendLine("                and t4.parent_id = t1.schedule_id");
            sqlbldr.AppendLine("                and '" + nowDateTime + "' between cast(t4.start_date - interval '" + param.check_time + " minutes' as timestamp) and cast(t4.end_date - interval '" + param.check_time + " minutes' as timestamp)");
            sqlbldr.AppendLine("            )");
            sqlbldr.AppendLine("    )");

            // 毎月1回繰り返すスケジュール(繰り返し期間なし)
            sqlbldr.AppendLine("or ");
            sqlbldr.AppendLine("    (");
            sqlbldr.AppendLine("            t1.user_id = :user_id");
            sqlbldr.AppendLine("        and t1.status != 'D'");
            sqlbldr.AppendLine("        and t2.start_date like :like_start_date");
            sqlbldr.AppendLine("        and t2.repeat_pattern = 'M" + String.Format("{0:D2}", dt.Day) + "N'");
            sqlbldr.AppendLine("        and not exists");
            sqlbldr.AppendLine("            (");
            sqlbldr.AppendLine("                select * from eip_t_schedule t4");
            sqlbldr.AppendLine("                where 1 = 1");
            sqlbldr.AppendLine("                and t4.parent_id = t1.schedule_id");
            sqlbldr.AppendLine("            )");
            sqlbldr.AppendLine("    )");

            // 毎月1回繰り返すスケジュール(繰り返し期間あり)
            sqlbldr.AppendLine("or ");
            sqlbldr.AppendLine("    (");
            sqlbldr.AppendLine("            t1.user_id = :user_id");
            sqlbldr.AppendLine("        and t1.status != 'D'");
            sqlbldr.AppendLine("        and t2.start_date like :like_start_date");
            sqlbldr.AppendLine("        and '" + nowDateTime + "' between cast(t2.start_date - interval '" + param.check_time + " minutes' as timestamp) and cast(t2.end_date - interval '" + param.check_time + " minutes' as timestamp)");
            sqlbldr.AppendLine("        and t2.repeat_pattern = 'M" + String.Format("{0:D2}", dt.Day) + "L'");
            sqlbldr.AppendLine("        and not exists");
            sqlbldr.AppendLine("            (");
            sqlbldr.AppendLine("                select * from eip_t_schedule t4");
            sqlbldr.AppendLine("                where 1 = 1");
            sqlbldr.AppendLine("                and t4.parent_id = t1.schedule_id");
            sqlbldr.AppendLine("                and '" + nowDateTime + "' between cast(t4.start_date - interval '" + param.check_time + " minutes' as timestamp) and cast(t4.end_date - interval '" + param.check_time + " minutes' as timestamp)");
            sqlbldr.AppendLine("            )");
            sqlbldr.AppendLine("    )");

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
                // 日付部分と'%HH:MM:SS'形式に分ける
                String[] start_date_split = param.start_date.Split(' ');
                string start_date_time = "%" + start_date_split[1];

                paramList.Add(DBUtility.MakeParameter("like_start_date", start_date_time, NpgsqlDbType.Varchar));
            }

            if (!String.IsNullOrEmpty(param.other_user_id_list))
            {
                // 単発スケジュール
                sqlbldr.AppendLine("union");
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
                sqlbldr.AppendLine("and t1.user_id in (" + param.other_user_id_list + ")");
                sqlbldr.AppendLine("and t1.status != 'D'");
                sqlbldr.AppendLine("and t2.public_flag = 'O'");
                sqlbldr.AppendLine("and t2.start_date = :start_date");
                sqlbldr.AppendLine("and t2.repeat_pattern = 'N'");

                // 毎日繰り返すスケジュール(繰り返し期間なし)
                sqlbldr.AppendLine("or ");
                sqlbldr.AppendLine("    (");
                sqlbldr.AppendLine("            t1.user_id in (" + param.other_user_id_list + ")");
                sqlbldr.AppendLine("        and t1.status != 'D'");
                sqlbldr.AppendLine("        and t2.public_flag = 'O'");
                sqlbldr.AppendLine("        and t2.start_date like :like_start_date");
                sqlbldr.AppendLine("        and t2.repeat_pattern = 'DN'");
                sqlbldr.AppendLine("        and not exists");
                sqlbldr.AppendLine("            (");
                sqlbldr.AppendLine("                select * from eip_t_schedule t4");
                sqlbldr.AppendLine("                where 1 = 1");
                sqlbldr.AppendLine("                and t4.parent_id = t1.schedule_id");
                sqlbldr.AppendLine("            )");
                sqlbldr.AppendLine("    )");

                // 毎日繰り返すスケジュール(繰り返し期間あり)
                sqlbldr.AppendLine("or ");
                sqlbldr.AppendLine("    (");
                sqlbldr.AppendLine("            t1.user_id in ( " + param.other_user_id_list + ")");
                sqlbldr.AppendLine("        and t1.status != 'D'");
                sqlbldr.AppendLine("        and t2.public_flag = 'O'");
                sqlbldr.AppendLine("        and t2.start_date like :like_start_date");
                sqlbldr.AppendLine("        and '" + nowDateTime + "' between cast(t2.start_date - interval '" + param.check_time + " minutes' as timestamp) and cast(t2.end_date - interval '" + param.check_time + " minutes' as timestamp)");
                sqlbldr.AppendLine("        and t2.repeat_pattern = 'DL'");
                sqlbldr.AppendLine("        and not exists");
                sqlbldr.AppendLine("            (");
                sqlbldr.AppendLine("                select * from eip_t_schedule t4");
                sqlbldr.AppendLine("                where 1 = 1");
                sqlbldr.AppendLine("                and t4.parent_id = t1.schedule_id");
                sqlbldr.AppendLine("                and '" + nowDateTime + "' between cast(t4.start_date - interval '" + param.check_time + " minutes' as timestamp) and cast(t4.end_date - interval '" + param.check_time + " minutes' as timestamp)");
                sqlbldr.AppendLine("            )");
                sqlbldr.AppendLine("    )");

                // 曜日毎に繰り返すスケジュール(繰り返し期間なし)
                sqlbldr.AppendLine("or ");
                sqlbldr.AppendLine("    (");
                sqlbldr.AppendLine("            t1.user_id in ( " + param.other_user_id_list + ")");
                sqlbldr.AppendLine("        and t1.status != 'D'");
                sqlbldr.AppendLine("        and t2.public_flag = 'O'");
                sqlbldr.AppendLine("        and t2.start_date = :start_date");
                switch (weekday)
                {
                    case DayOfWeek.Sunday:
                        sqlbldr.AppendLine("        and t2.repeat_pattern like 'W1______N'");
                        break;
                    case DayOfWeek.Monday:
                        sqlbldr.AppendLine("        and t2.repeat_pattern like 'W_1_____N'");
                        break;
                    case DayOfWeek.Tuesday:
                        sqlbldr.AppendLine("        and t2.repeat_pattern like 'W__1____N'");
                        break;
                    case DayOfWeek.Wednesday:
                        sqlbldr.AppendLine("        and t2.repeat_pattern like 'W___1___N'");
                        break;
                    case DayOfWeek.Thursday:
                        sqlbldr.AppendLine("        and t2.repeat_pattern like 'W____1__N'");
                        break;
                    case DayOfWeek.Friday:
                        sqlbldr.AppendLine("        and t2.repeat_pattern like 'W_____1_N'");
                        break;
                    case DayOfWeek.Saturday:
                        sqlbldr.AppendLine("        and t2.repeat_pattern like 'W______1N'");
                        break;
                }
                sqlbldr.AppendLine("        and not exists");
                sqlbldr.AppendLine("            (");
                sqlbldr.AppendLine("                select * from eip_t_schedule t4");
                sqlbldr.AppendLine("                where 1 = 1");
                sqlbldr.AppendLine("                and t4.parent_id = t1.schedule_id");
                sqlbldr.AppendLine("            )");
                sqlbldr.AppendLine("    )");

                // 曜日毎に繰り返すスケジュール(繰り返し期間あり)
                sqlbldr.AppendLine("or ");
                sqlbldr.AppendLine("    (");
                sqlbldr.AppendLine("            t1.user_id in ( " + param.other_user_id_list + ")");
                sqlbldr.AppendLine("        and t1.status != 'D'");
                sqlbldr.AppendLine("        and t2.public_flag = 'O'");
                sqlbldr.AppendLine("        and t2.start_date like :like_start_date");
                sqlbldr.AppendLine("        and '" + nowDateTime + "' between cast(t2.start_date - interval '" + param.check_time + " minutes' as timestamp) and cast(t2.end_date - interval '" + param.check_time + " minutes' as timestamp)");
                switch (weekday)
                {
                    case DayOfWeek.Sunday:
                        sqlbldr.AppendLine("        and t2.repeat_pattern like 'W1______L'");
                        break;
                    case DayOfWeek.Monday:
                        sqlbldr.AppendLine("        and t2.repeat_pattern like 'W_1_____L'");
                        break;
                    case DayOfWeek.Tuesday:
                        sqlbldr.AppendLine("        and t2.repeat_pattern like 'W__1____L'");
                        break;
                    case DayOfWeek.Wednesday:
                        sqlbldr.AppendLine("        and t2.repeat_pattern like 'W___1___L'");
                        break;
                    case DayOfWeek.Thursday:
                        sqlbldr.AppendLine("        and t2.repeat_pattern like 'W____1__L'");
                        break;
                    case DayOfWeek.Friday:
                        sqlbldr.AppendLine("        and t2.repeat_pattern like 'W_____1_L'");
                        break;
                    case DayOfWeek.Saturday:
                        sqlbldr.AppendLine("        and t2.repeat_pattern like 'W______1L'");
                        break;
                }
                sqlbldr.AppendLine("        and not exists");
                sqlbldr.AppendLine("            (");
                sqlbldr.AppendLine("                select * from eip_t_schedule t4");
                sqlbldr.AppendLine("                where 1 = 1");
                sqlbldr.AppendLine("                and t4.parent_id = t1.schedule_id");
                sqlbldr.AppendLine("                and '" + nowDateTime + "' between cast(t4.start_date - interval '" + param.check_time + " minutes' as timestamp) and cast(t4.end_date - interval '" + param.check_time + " minutes' as timestamp)");
                sqlbldr.AppendLine("            )");
                sqlbldr.AppendLine("    )");

                // 毎月1回繰り返すスケジュール(繰り返し期間なし)
                sqlbldr.AppendLine("or ");
                sqlbldr.AppendLine("    (");
                sqlbldr.AppendLine("            t1.user_id in ( " + param.other_user_id_list + ")");
                sqlbldr.AppendLine("        and t1.status != 'D'");
                sqlbldr.AppendLine("        and t2.public_flag = 'O'");
                sqlbldr.AppendLine("        and t2.start_date like :like_start_date");
                sqlbldr.AppendLine("        and t2.repeat_pattern = 'M" + String.Format("{0:D2}", dt.Day) + "N'");
                sqlbldr.AppendLine("        and not exists");
                sqlbldr.AppendLine("            (");
                sqlbldr.AppendLine("                select * from eip_t_schedule t4");
                sqlbldr.AppendLine("                where 1 = 1");
                sqlbldr.AppendLine("                and t4.parent_id = t1.schedule_id");
                sqlbldr.AppendLine("            )");
                sqlbldr.AppendLine("    )");

                // 毎月1回繰り返すスケジュール(繰り返し期間あり)
                sqlbldr.AppendLine("or ");
                sqlbldr.AppendLine("    (");
                sqlbldr.AppendLine("            t1.user_id in ( " + param.other_user_id_list + ")");
                sqlbldr.AppendLine("        and t1.status != 'D'");
                sqlbldr.AppendLine("        and t2.public_flag = 'O'");
                sqlbldr.AppendLine("        and t2.start_date like :like_start_date");
                sqlbldr.AppendLine("        and '" + nowDateTime + "' between cast(t2.start_date - interval '" + param.check_time + " minutes' as timestamp) and cast(t2.end_date - interval '" + param.check_time + " minutes' as timestamp)");
                sqlbldr.AppendLine("        and t2.repeat_pattern = 'M" + String.Format("{0:D2}", dt.Day) + "L'");
                sqlbldr.AppendLine("        and not exists");
                sqlbldr.AppendLine("            (");
                sqlbldr.AppendLine("                select * from eip_t_schedule t4");
                sqlbldr.AppendLine("                where 1 = 1");
                sqlbldr.AppendLine("                and t4.parent_id = t1.schedule_id");
                sqlbldr.AppendLine("                and '" + nowDateTime + "' between cast(t4.start_date - interval '" + param.check_time + " minutes' as timestamp) and cast(t4.end_date - interval '" + param.check_time + " minutes' as timestamp)");
                sqlbldr.AppendLine("            )");
                sqlbldr.AppendLine("    )");
            }

            sqlbldr.AppendLine("order by start_date, end_date, schedule_id");

            this.dbHelper.Select(((ScheduleDataSet)data).eip_t_schedule, sqlbldr.ToString(), paramList);
        }

    }
}
