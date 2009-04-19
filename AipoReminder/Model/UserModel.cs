using AipoReminder.Logic;
using WinFramework.Model;

namespace AipoReminder.Model
{
    class UserModel : DbConnectionModel
    {
        public void GetTurbineUserInfo(System.Data.DataSet data)
        {
            TurbineUserLogic logic = new TurbineUserLogic(this.dbHelper);
            logic.GetTurbineUserInfo(data);
        }
    }
}
