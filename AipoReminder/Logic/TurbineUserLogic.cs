﻿using AipoReminder.DAO;
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

        public void GetTurbineUserInfo(System.Data.DataSet data)
        {
            TurbineUserDAO dao = new TurbineUserDAO(this.dbHelper);
            dao.GetTurbineUserInfo(data);
        }
    }
}