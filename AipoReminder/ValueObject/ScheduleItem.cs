using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AipoReminder.ValueObject
{
    public class ScheduleItem
    {
        public string ScheduleId { set; get; }
        public string ScheduleName { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public string UserName { set; get; }
        public bool isMySchedule { set; get; }
        public bool isOneDaySchedule { set; get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (isMySchedule)
            {
                sb.AppendLine("【" + ScheduleName + "】");
            }
            else
            {
                sb.AppendLine("【" + ScheduleName + "】(" + UserName + ")");
            }
            if (!isOneDaySchedule)
            {
                sb.AppendLine(String.Format("{0:D2}", StartDate.Hour) + ":" + String.Format("{0:D2}", StartDate.Minute) + "～" + String.Format("{0:D2}", EndDate.Hour) + ":" + String.Format("{0:D2}", EndDate.Minute));
            }
            return sb.ToString();
        }
    }
}
