using AipoReminder.Logic;
using WinFramework.Model;

namespace AipoReminder.Model
{
    class GroupModel : DbConnectionModel
    {
        public int GetTurbineGroupInfo(System.Data.DataSet data)
        {
            TurbineGroupLogic logic = new TurbineGroupLogic(this.dbHelper);
            return logic.GetTurbineGroupInfo(data);
        }
    }
}
