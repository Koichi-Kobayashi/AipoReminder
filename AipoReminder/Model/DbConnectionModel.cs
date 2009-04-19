using System;
using System.Collections.Generic;
using System.Linq;
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
                   SettingManager.NpgsqlConnectionDatabase)
        {
        }
    }
}
