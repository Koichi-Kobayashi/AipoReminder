using AipoReminder.DAO;
using WinFramework.Utility;

namespace AipoReminder.Logic
{
    class ScheduleLogic
    {
        private DBHelper dbHelper;

        public ScheduleLogic(DBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public void GetOneDayScheduleInfo(System.Data.DataSet data)
        {
            ScheduleDAO dao = new ScheduleDAO(this.dbHelper);
            dao.GetOneDayScheduleInfo(data);
        }

        public void GetScheduleInfo(System.Data.DataSet data)
        {
            ScheduleDAO dao = new ScheduleDAO(this.dbHelper);
            dao.GetScheduleInfo(data);
        }
    }
}
