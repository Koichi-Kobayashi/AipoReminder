using AipoReminder.Logic;
using WinFramework.Model;

namespace AipoReminder.Model
{
    class ScheduleModel : DbConnectionModel
    {
        public int GetOneDayScheduleInfo(System.Data.DataSet data)
        {
            ScheduleLogic logic = new ScheduleLogic(this.dbHelper);
            return logic.GetOneDayScheduleInfo(data);
        }

        public int GetScheduleInfo(System.Data.DataSet data)
        {
            ScheduleLogic logic = new ScheduleLogic(this.dbHelper);
            return logic.GetScheduleInfo(data);
        }
    }
}
