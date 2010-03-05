using AipoReminder.Logic;
using WinFramework.Model;

namespace AipoReminder.Model
{
    class UserModel : DbConnectionModel
    {
        public int GetTurbineUserInfo(System.Data.DataSet data)
        {
            TurbineUserLogic logic = new TurbineUserLogic(this.dbHelper);
            return logic.GetTurbineUserInfo(data);
        }

        public int GetTurbineGroupUserInfo(System.Data.DataSet data)
        {
            TurbineUserLogic logic = new TurbineUserLogic(this.dbHelper);
            return logic.GetTurbineGroupUserInfo(data);
        }
    }
}
