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

        public int GetOneDayScheduleInfo(System.Data.DataSet data)
        {
            ScheduleDAO dao = new ScheduleDAO(this.dbHelper);
            return dao.GetOneDayScheduleInfo(data);
        }

        public int GetScheduleInfo(System.Data.DataSet data)
        {
            ScheduleDAO dao = new ScheduleDAO(this.dbHelper);
            return dao.GetScheduleInfo(data);
        }
    }
}
