using AipoReminder.DAO;
using WinFramework.Utility;

namespace AipoReminder.Logic
{
    class TurbineUserLogic
    {
        private DBHelper dbHelper;

        public TurbineUserLogic(DBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public int GetTurbineUserInfo(System.Data.DataSet data)
        {
            TurbineUserDAO dao = new TurbineUserDAO(this.dbHelper);
            return dao.GetTurbineUserInfo(data);
        }

        public int GetTurbineGroupUserInfo(System.Data.DataSet data)
        {
            TurbineUserDAO dao = new TurbineUserDAO(this.dbHelper);
            return dao.GetTurbineGroupUserInfo(data);
        }
    }
}
