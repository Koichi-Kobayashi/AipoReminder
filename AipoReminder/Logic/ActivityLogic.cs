using AipoReminder.DAO;
using WinFramework.Utility;

namespace AipoReminder.Logic
{
    class ActivityLogic
    {
        private DBHelper dbHelper;

        public ActivityLogic(DBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        /// <summary>
        /// [ ブログ ] 新着記事を取得
        /// </summary>
        /// <param name="data"></param>
        public int GetActivityInfo(System.Data.DataSet data)
        {
            ActivityDAO dao = new ActivityDAO(this.dbHelper);
            return dao.GetActivityInfo(data);
        }

    }
}
