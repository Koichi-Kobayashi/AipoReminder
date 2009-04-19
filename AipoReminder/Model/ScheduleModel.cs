using AipoReminder.Logic;
using WinFramework.Model;

namespace AipoReminder.Model
{
    class ScheduleModel : DbConnectionModel
    {
        public void GetScheduleInfo(System.Data.DataSet data)
        {
            ScheduleLogic logic = new ScheduleLogic(this.dbHelper);
            logic.GetScheduleInfo(data);
        }
    }
}
