using AipoReminder.Logic;
using WinFramework.Model;

namespace AipoReminder.Model
{
    class GroupModel : DbConnectionModel
    {
        public void GetTurbineGroupInfo(System.Data.DataSet data)
        {
            TurbineGroupLogic logic = new TurbineGroupLogic(this.dbHelper);
            logic.GetTurbineGroupInfo(data);
        }
    }
}
