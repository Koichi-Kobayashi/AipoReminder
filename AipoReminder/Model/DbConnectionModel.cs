using System;
using System.Collections.Generic;
using System.Text;
using WinFramework.Model;
using AipoReminder.Utility;

namespace AipoReminder.Model
{
    class DbConnectionModel : AbstractModel
    {
        public DbConnectionModel()
            : base(SettingManager.NpgsqlConnectionServer, 
                    SettingManager.NpgsqlConnectionPort, 
                    SettingManager.NpgsqlConnectionUserId, 
                    SettingManager.NpgsqlConnectionPassword, 
                    SettingManager.NpgsqlConnectionDatabase,
                    SettingManager.NpgsqlConnectionTimeout,
                    SettingManager.NpgsqlConnectionTimeout + 5,
                    SettingManager.NpgsqlConnectionTimeout)
        {
        }
    }
}
