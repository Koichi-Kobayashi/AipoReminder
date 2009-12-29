﻿using AipoReminder.DAO;
using WinFramework.Utility;

namespace AipoReminder.Logic
{
    class TurbineGroupLogic
    {
        private DBHelper dbHelper;

        public TurbineGroupLogic(DBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public void GetTurbineGroupInfo(System.Data.DataSet data)
        {
            TurbineGroupDAO dao = new TurbineGroupDAO(this.dbHelper);
            dao.GetTurbineGroupInfo(data);
        }
    }
}
