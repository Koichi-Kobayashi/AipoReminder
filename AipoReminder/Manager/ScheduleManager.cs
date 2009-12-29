using System.Diagnostics;
using AipoReminder.Constants;
using AipoReminder.DataSet;
using AipoReminder.Model;
using System;
using System.Text;
using AipoReminder.ValueObject;
using System.Collections.Generic;

namespace AipoReminder.Utility
{
    class ScheduleManager
    {
        DateTime dt;

        public ScheduleManager(DateTime dt)
        {
            this.dt = dt;
        }

        public List<ScheduleItem> CheckSchedule()
        {
            ScheduleDataSet data = new ScheduleDataSet();

            if (dt.Minute >= 0 && dt.Minute < 5)
            {
                // 現在時刻[分]と定期チェック[分]の合計が 5 以上の場合
                if (dt.Minute + SettingManager.CheckTime >= 5)
                {
                    // スケジュールの確認
                    data = this.GetScheduleData(this.GetCheckScheduleTime(dt, 0, SettingManager.CheckTime));
                }
            }
            else if (dt.Minute >= 5 && dt.Minute < 10)
            {
                if (dt.Minute + SettingManager.CheckTime >= 10)
                {
                    data = this.GetScheduleData(this.GetCheckScheduleTime(dt, 5, SettingManager.CheckTime));
                }
            }
            else if (dt.Minute >= 10 && dt.Minute < 15)
            {
                if (dt.Minute + SettingManager.CheckTime >= 15)
                {
                    data = this.GetScheduleData(this.GetCheckScheduleTime(dt, 10, SettingManager.CheckTime));
                }
            }
            else if (dt.Minute >= 15 && dt.Minute < 20)
            {
                if (dt.Minute + SettingManager.CheckTime >= 20)
                {
                    data = this.GetScheduleData(this.GetCheckScheduleTime(dt, 15, SettingManager.CheckTime));
                }
            }
            else if (dt.Minute >= 20 && dt.Minute < 25)
            {
                if (dt.Minute + SettingManager.CheckTime >= 25)
                {
                    data = this.GetScheduleData(this.GetCheckScheduleTime(dt, 20, SettingManager.CheckTime));
                }
            }
            else if (dt.Minute >= 25 && dt.Minute < 30)
            {
                if (dt.Minute + SettingManager.CheckTime >= 30)
                {
                    data = this.GetScheduleData(this.GetCheckScheduleTime(dt, 25, SettingManager.CheckTime));
                }
            }
            else if (dt.Minute >= 30 && dt.Minute < 35)
            {
                if (dt.Minute + SettingManager.CheckTime >= 35)
                {
                    data = this.GetScheduleData(this.GetCheckScheduleTime(dt, 30, SettingManager.CheckTime));
                }
            }
            else if (dt.Minute >= 35 && dt.Minute < 40)
            {
                if (dt.Minute + SettingManager.CheckTime >= 40)
                {
                    data = this.GetScheduleData(this.GetCheckScheduleTime(dt, 35, SettingManager.CheckTime));
                }
            }
            else if (dt.Minute >= 40 && dt.Minute < 45)
            {
                if (dt.Minute + SettingManager.CheckTime >= 45)
                {
                    data = this.GetScheduleData(this.GetCheckScheduleTime(dt, 40, SettingManager.CheckTime));
                }
            }
            else if (dt.Minute >= 45 && dt.Minute < 50)
            {
                if (dt.Minute + SettingManager.CheckTime >= 50)
                {
                    data = this.GetScheduleData(this.GetCheckScheduleTime(dt, 45, SettingManager.CheckTime));
                }
            }
            else if (dt.Minute >= 50 && dt.Minute < 55)
            {
                if (dt.Minute + SettingManager.CheckTime >= 55)
                {
                    data = this.GetScheduleData(this.GetCheckScheduleTime(dt, 50, SettingManager.CheckTime));
                }
            }
            else if (dt.Minute >= 55 && dt.Minute <= 59)
            {
                if (dt.Minute + SettingManager.CheckTime >= 59)
                {
                    data = this.GetScheduleData(this.GetCheckScheduleTime(dt, 55, SettingManager.CheckTime));
                }
            }

            if (data.eip_t_schedule.Count > 0)
            {
                // もうすぐ始まるスケジュールを取得
                return this.GetSchedule(data);
            }

            return null;
        }

        /// <summary>
        /// スケジュールをチェックする日時を取得する。
        /// </summary>
        /// <param name="dt">現在日時</param>
        /// <param name="checkMinute">5分間隔</param>
        /// <returns></returns>
        private string GetCheckScheduleTime(DateTime dt, int nowMinute, int checkMinute)
        {
            DateTime correctionDateTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, nowMinute, 0);
            DateTime tmpDateTime = correctionDateTime.AddMinutes(checkMinute);
            DateTime checkDateTime = new DateTime(tmpDateTime.Year, tmpDateTime.Month, tmpDateTime.Day, tmpDateTime.Hour, tmpDateTime.Minute, 0);

            string year = checkDateTime.Year.ToString();
            string month = String.Format("{0:D2}", checkDateTime.Month);
            string day = String.Format("{0:D2}", checkDateTime.Day);
            string hour = String.Format("{0:D2}", checkDateTime.Hour);
            string minute = String.Format("{0:D2}", checkDateTime.Minute);

            StringBuilder sb = new StringBuilder();
            sb.Append(year);
            sb.Append("-");
            sb.Append(month);
            sb.Append("-");
            sb.Append(day);
            sb.Append(" ");
            sb.Append(hour);
            sb.Append(":");
            sb.Append(minute);
            sb.Append(":");
            sb.Append("00");

            return sb.ToString();
        }
        
        /// <summary>
        /// スケジュールを検索
        /// </summary>
        /// <param name="start_date"></param>
        /// <returns></returns>
        private ScheduleDataSet GetScheduleData(string start_date)
        {
            ScheduleDataSet data = new ScheduleDataSet();

            ScheduleDataSet.search_eip_t_scheduleRow paramRow = data.search_eip_t_schedule.Newsearch_eip_t_scheduleRow();

            paramRow.user_id = SettingManager.UserId;
            paramRow.start_date = start_date;
            paramRow.check_time = SettingManager.CheckTime.ToString();
            paramRow.other_user_id_list = SettingManager.GroupUserId;

            data.search_eip_t_schedule.Rows.Add(paramRow);

            ScheduleModel m = new ScheduleModel();
            m.Execute(m.GetScheduleInfo, data);

            return data;
        }

        /// <summary>
        /// スケジュール情報の取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<ScheduleItem> GetSchedule(ScheduleDataSet data)
        {
            List<ScheduleItem> list = new List<ScheduleItem>();
            StringBuilder sb = new StringBuilder();

            string scheduleId = "";

            foreach (ScheduleDataSet.eip_t_scheduleRow row in data.eip_t_schedule)
            {
                if (!scheduleId.Equals(row.schedule_id))
                {
                    ScheduleItem scheduleItem = new ScheduleItem();
                    scheduleItem.ScheduleId = row.schedule_id;
                    scheduleItem.ScheduleName = row.name;
                    scheduleItem.StartDate = row.start_date;
                    scheduleItem.EndDate = row.end_date;
                    scheduleItem.UserName = row.last_name + " " + row.first_name; 
                    if (SettingManager.UserId.Equals(row.user_id))
                    {
                        scheduleItem.isMySchedule = true;
                    }
                    list.Add(scheduleItem);
                }
                scheduleId = row.schedule_id;
            }
            return list;
        }

    }
}
