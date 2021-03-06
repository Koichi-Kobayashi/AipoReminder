﻿using System;
using System.Collections;
using AipoReminder.Constants;
using AipoReminder.DataSet;
using AipoReminder.Utility;
using NpgsqlTypes;
using WinFramework.Utility;

namespace AipoReminder.DAO
{
    class ExtTimeCardDAO
    {
        private DBHelper dbHelper;

        public ExtTimeCardDAO(DBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        /// <summary>
        /// ユーザに紐付いているタイムカードの日付変更時刻を取得
        /// </summary>
        /// <param name="data"></param>
        public int GetChangeHour(System.Data.DataSet data)
        {
            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();

            sqlbldr.AppendLine("select t1.change_hour ");
            sqlbldr.AppendLine("from eip_t_ext_timecard_system t1");
            sqlbldr.AppendLine("    join eip_t_ext_timecard_system_map t2");
            sqlbldr.AppendLine("        on t1.system_id = t2.system_id");
            sqlbldr.AppendLine("where 1 = 1");
            sqlbldr.AppendLine("and t2.user_id = :user_id");
            sqlbldr.AppendLine("order by t1.system_id desc");
            sqlbldr.AppendLine("offset 0 limit 1");

            ExtTimeCardDataSet.search_eip_t_ext_timecard_systemRow param = ((ExtTimeCardDataSet)data).search_eip_t_ext_timecard_system[0];

            ArrayList paramList = new ArrayList();
            if (!String.IsNullOrEmpty(param.user_id))
            {
                paramList.Add(DBUtility.MakeParameter("user_id", param.user_id, NpgsqlDbType.Integer));
            }

            return this.dbHelper.Select(((ExtTimeCardDataSet)data).eip_t_ext_timecard_system, sqlbldr.ToString(), paramList);
        }

        /// <summary>
        /// 標準のタイムカードの日付変更時刻を取得
        /// </summary>
        /// <param name="data"></param>
        public int GetChangeHourDefault(System.Data.DataSet data)
        {
            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();

            sqlbldr.AppendLine("select t1.change_hour ");
            sqlbldr.AppendLine("from eip_t_ext_timecard_system t1");
            sqlbldr.AppendLine("where 1 = 1");
            sqlbldr.AppendLine("and t1.system_id = '1'");

            return this.dbHelper.Select(((ExtTimeCardDataSet)data).eip_t_ext_timecard_system, sqlbldr.ToString(), null);
        }

        /// <summary>
        /// タイムカード情報を取得
        /// </summary>
        /// <param name="data"></param>
        public int GetTimeCardInfo(System.Data.DataSet data)
        {
            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();

            sqlbldr.AppendLine("select ");
            sqlbldr.AppendLine("    t1.timecard_id ");
            sqlbldr.AppendLine("    ,t1.clock_in_time ");
            sqlbldr.AppendLine("    ,t1.clock_out_time ");
            sqlbldr.AppendLine("from eip_t_ext_timecard t1");
            sqlbldr.AppendLine("where 1 = 1");
            sqlbldr.AppendLine("and t1.user_id = :user_id");
            sqlbldr.AppendLine("and t1.punch_date = to_date(:punch_date, 'YYYY-MM-DD')");

            ExtTimeCardDataSet.search_eip_t_ext_timecardRow param = ((ExtTimeCardDataSet)data).search_eip_t_ext_timecard[0];

            ArrayList paramList = new ArrayList();
            if (!String.IsNullOrEmpty(param.user_id))
            {
                paramList.Add(DBUtility.MakeParameter("user_id", param.user_id, NpgsqlDbType.Integer));
            }
            if (!String.IsNullOrEmpty(param.punch_date))
            {
                paramList.Add(DBUtility.MakeParameter("punch_date", param.punch_date, NpgsqlDbType.Varchar));
            }

            return this.dbHelper.Select(((ExtTimeCardDataSet)data).eip_t_ext_timecard, sqlbldr.ToString(), paramList);
        }

        /// <summary>
        /// タイムカードの出社時刻を登録
        /// Aipo4～5
        /// </summary>
        /// <param name="data"></param>
        public int InsertTimeCard(System.Data.DataSet data)
        {
            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();
            ExtTimeCardDataSet.update_eip_t_ext_timecardRow param = ((ExtTimeCardDataSet)data).update_eip_t_ext_timecard[0];
            ArrayList paramList = new ArrayList();

            sqlbldr.AppendLine("insert");
            sqlbldr.AppendLine(" into eip_t_ext_timecard (");
            sqlbldr.AppendLine("      user_id");
            sqlbldr.AppendLine("    , punch_date");
            sqlbldr.AppendLine("    , type");
            if (!String.IsNullOrEmpty(param.clock_in_time))
            {
                sqlbldr.AppendLine("    , clock_in_time");
            }
            if (!String.IsNullOrEmpty(param.clock_out_time))
            {
                sqlbldr.AppendLine("    , clock_out_time");
            }
            sqlbldr.AppendLine("    , create_date");
            sqlbldr.AppendLine("    , update_date");
            sqlbldr.AppendLine(") values (");
            sqlbldr.AppendLine("      :user_id");
            sqlbldr.AppendLine("    , to_date(:punch_date, 'YYYY-MM-DD')");
            sqlbldr.AppendLine("    , :type");
            if (!String.IsNullOrEmpty(param.clock_in_time))
            {
                sqlbldr.AppendLine("    , to_timestamp(:clock_in_time, 'YYYY-MM-DD HH24:MI:SS.MS')");
                paramList.Add(DBUtility.MakeParameter("clock_in_time", param.clock_in_time, NpgsqlDbType.Varchar));
            }
            if (!String.IsNullOrEmpty(param.clock_out_time))
            {
                sqlbldr.AppendLine("    , to_timestamp(:clock_out_time, 'YYYY-MM-DD HH24:MI:SS.MS')");
                paramList.Add(DBUtility.MakeParameter("clock_out_time", param.clock_out_time, NpgsqlDbType.Varchar));
            }
            sqlbldr.AppendLine("    , to_date(:create_date, 'YYYY-MM-DD')");
            sqlbldr.AppendLine("    , to_timestamp(:update_date, 'YYYY-MM-DD HH24:MI:SS.MS')");
            sqlbldr.AppendLine(");");

            paramList.Add(DBUtility.MakeParameter("user_id", param.user_id, NpgsqlDbType.Integer));
            paramList.Add(DBUtility.MakeParameter("punch_date", param.punch_date, NpgsqlDbType.Varchar));
            paramList.Add(DBUtility.MakeParameter("type", param.type, NpgsqlDbType.Char));
            paramList.Add(DBUtility.MakeParameter("create_date", param.create_date, NpgsqlDbType.Varchar));
            paramList.Add(DBUtility.MakeParameter("update_date", param.update_date, NpgsqlDbType.Varchar));

            return this.dbHelper.Insert(((ExtTimeCardDataSet)data).eip_t_ext_timecard, sqlbldr.ToString(), paramList);
        }

        /// <summary>
        /// タイムカードの出社時刻を登録
        /// Aipo6
        /// </summary>
        /// <param name="data"></param>
        public int InsertTimeCardv6(System.Data.DataSet data)
        {
            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();
            ExtTimeCardDataSet.update_eip_t_ext_timecardRow param = ((ExtTimeCardDataSet)data).update_eip_t_ext_timecard[0];
            ArrayList paramList = new ArrayList();

            sqlbldr.AppendLine("insert");
            sqlbldr.AppendLine(" into eip_t_ext_timecard (");
            sqlbldr.AppendLine("      timecard_id");
            sqlbldr.AppendLine("    , user_id");
            sqlbldr.AppendLine("    , punch_date");
            sqlbldr.AppendLine("    , type");
            if (!String.IsNullOrEmpty(param.clock_in_time))
            {
                sqlbldr.AppendLine("    , clock_in_time");
            }
            if (!String.IsNullOrEmpty(param.clock_out_time))
            {
                sqlbldr.AppendLine("    , clock_out_time");
            }
            sqlbldr.AppendLine("    , create_date");
            sqlbldr.AppendLine("    , update_date");
            sqlbldr.AppendLine(") values (");
            sqlbldr.AppendLine("      (select nextval('pk_eip_t_ext_timecard'))");
            sqlbldr.AppendLine("    , :user_id");
            sqlbldr.AppendLine("    , to_date(:punch_date, 'YYYY-MM-DD')");
            sqlbldr.AppendLine("    , :type");
            if (!String.IsNullOrEmpty(param.clock_in_time))
            {
                sqlbldr.AppendLine("    , to_timestamp(:clock_in_time, 'YYYY-MM-DD HH24:MI:SS.MS')");
                paramList.Add(DBUtility.MakeParameter("clock_in_time", param.clock_in_time, NpgsqlDbType.Varchar));
            }
            if (!String.IsNullOrEmpty(param.clock_out_time))
            {
                sqlbldr.AppendLine("    , to_timestamp(:clock_out_time, 'YYYY-MM-DD HH24:MI:SS.MS')");
                paramList.Add(DBUtility.MakeParameter("clock_out_time", param.clock_out_time, NpgsqlDbType.Varchar));
            }
            sqlbldr.AppendLine("    , to_date(:create_date, 'YYYY-MM-DD')");
            sqlbldr.AppendLine("    , to_timestamp(:update_date, 'YYYY-MM-DD HH24:MI:SS.MS')");
            sqlbldr.AppendLine(");");

            paramList.Add(DBUtility.MakeParameter("user_id", param.user_id, NpgsqlDbType.Integer));
            paramList.Add(DBUtility.MakeParameter("punch_date", param.punch_date, NpgsqlDbType.Varchar));
            paramList.Add(DBUtility.MakeParameter("type", param.type, NpgsqlDbType.Char));
            paramList.Add(DBUtility.MakeParameter("create_date", param.create_date, NpgsqlDbType.Varchar));
            paramList.Add(DBUtility.MakeParameter("update_date", param.update_date, NpgsqlDbType.Varchar));

            return this.dbHelper.Insert(((ExtTimeCardDataSet)data).eip_t_ext_timecard, sqlbldr.ToString(), paramList);
        }

        /// <summary>
        /// タイムカードの退社時刻を更新
        /// </summary>
        /// <param name="data"></param>
        public int UpdateTimeCard(System.Data.DataSet data)
        {
            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();
            ExtTimeCardDataSet.update_eip_t_ext_timecardRow param = ((ExtTimeCardDataSet)data).update_eip_t_ext_timecard[0];
            ArrayList paramList = new ArrayList();

            sqlbldr.AppendLine("update eip_t_ext_timecard set ");
            sqlbldr.AppendLine("clock_out_time = to_timestamp(:clock_out_time, 'YYYY-MM-DD HH24:MI:SS.MS')");
            sqlbldr.AppendLine(", update_date = to_timestamp(:update_date, 'YYYY-MM-DD HH24:MI:SS.MS')");
            sqlbldr.AppendLine("where 1 = 1");
            sqlbldr.AppendLine("and timecard_id = :timecard_id");

            paramList.Add(DBUtility.MakeParameter("clock_out_time", param.clock_out_time, NpgsqlDbType.Varchar));
            paramList.Add(DBUtility.MakeParameter("update_date", param.update_date, NpgsqlDbType.Varchar));
            paramList.Add(DBUtility.MakeParameter("timecard_id", param.timecard_id, NpgsqlDbType.Integer));

            return this.dbHelper.Update(((ExtTimeCardDataSet)data).eip_t_ext_timecard, sqlbldr.ToString(), paramList);
        }
    }
}
