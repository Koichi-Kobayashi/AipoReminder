using System;
using System.Collections.Generic;
using System.Text;
using AipoReminder.Logic;

namespace AipoReminder.Model
{
    class ActivityModel : DbConnectionModel
    {
        /// <summary>
        /// 新着情報を取得
        /// </summary>
        /// <param name="data"></param>
        public int GetActivityInfo(System.Data.DataSet data)
        {
            int ret = -1;
            ActivityLogic logic = new ActivityLogic(this.dbHelper);
            ret = logic.GetActivityInfo(data);
            return ret;
        }

    }
}
