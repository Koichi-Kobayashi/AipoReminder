using AipoReminder.DAO;
using WinFramework.Utility;
using AipoReminder.Utility;

namespace AipoReminder.Logic
{
    class ExtTimeCardLogic
    {
        private DBHelper dbHelper;

        public ExtTimeCardLogic(DBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public int GetChangeHour(System.Data.DataSet data)
        {
            ExtTimeCardDAO dao = new ExtTimeCardDAO(this.dbHelper);
            return dao.GetChangeHour(data);
        }

        public int GetChangeHourDefault(System.Data.DataSet data)
        {
            ExtTimeCardDAO dao = new ExtTimeCardDAO(this.dbHelper);
            return dao.GetChangeHourDefault(data);
        }

        public int GetTimeCardInfo(System.Data.DataSet data)
        {
            ExtTimeCardDAO dao = new ExtTimeCardDAO(this.dbHelper);
            return dao.GetTimeCardInfo(data);
        }

        public int InsertTimeCard(System.Data.DataSet data)
        {
            ExtTimeCardDAO dao = new ExtTimeCardDAO(this.dbHelper);
            switch (SettingManager.AipoVersion)
            {
                case 4:
                case 5:
                    return dao.InsertTimeCard(data);
                case 6:
                    return dao.InsertTimeCardv6(data);
                default:
                    return dao.InsertTimeCardv6(data);
            }
        }

        public int UpdateTimeCard(System.Data.DataSet data)
        {
            ExtTimeCardDAO dao = new ExtTimeCardDAO(this.dbHelper);
            return dao.UpdateTimeCard(data);
        }
    }
}
