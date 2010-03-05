using AipoReminder.Logic;
using WinFramework.Model;

namespace AipoReminder.Model
{
    class ExtTimeCardModel : DbConnectionModel
    {
        public int GetChangeHour(System.Data.DataSet data)
        {
            ExtTimeCardLogic logic = new ExtTimeCardLogic(this.dbHelper);
            return logic.GetChangeHour(data);
        }

        public int GetTimeCardInfo(System.Data.DataSet data)
        {
            ExtTimeCardLogic logic = new ExtTimeCardLogic(this.dbHelper);
            return logic.GetTimeCardInfo(data);
        }

        public int InsertTimeCard(System.Data.DataSet data)
        {
            ExtTimeCardLogic logic = new ExtTimeCardLogic(this.dbHelper);
            return logic.InsertTimeCard(data);
        }

        public int UpdateTimeCard(System.Data.DataSet data)
        {
            ExtTimeCardLogic logic = new ExtTimeCardLogic(this.dbHelper);
            return logic.UpdateTimeCard(data);
        }
    }
}
