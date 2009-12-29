using System;
using System.Collections;
using AipoReminder.DataSet;
using AipoReminder.Utility;
using NpgsqlTypes;
using WinFramework.Utility;

namespace AipoReminder.DAO
{
    class TurbineGroupDAO
    {
        private DBHelper dbHelper;

        public TurbineGroupDAO(DBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public void GetTurbineGroupInfo(System.Data.DataSet data)
        {
            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();

            sqlbldr.AppendLine("select");
            sqlbldr.AppendLine(" group_id");
            sqlbldr.AppendLine(", group_name");
            sqlbldr.AppendLine(", objectdata");
            sqlbldr.AppendLine(", owner_id");
            sqlbldr.AppendLine(", group_alias_name");
            sqlbldr.AppendLine(", public_flag");
            sqlbldr.AppendLine("from turbine_group");
            sqlbldr.AppendLine("where 1 = 1");
            sqlbldr.AppendLine("and public_flag = '1'");

            TurbineGroupDataSet.search_turbine_groupRow param = ((TurbineGroupDataSet)data).search_turbine_group[0];

            ArrayList paramList = new ArrayList();
            if (!String.IsNullOrEmpty(param.group_id))
            {
                sqlbldr.AppendLine("and group_id = :group_id");
                paramList.Add(DBUtility.MakeParameter("group_id", param.group_id, NpgsqlDbType.Integer));
            }

            if (!String.IsNullOrEmpty(param.owner_id))
            {
                sqlbldr.AppendLine("and owner_id = :owner_id");
                paramList.Add(DBUtility.MakeParameter("owner_id", param.owner_id, NpgsqlDbType.Integer));
            }

            sqlbldr.AppendLine("order by group_id");

            this.dbHelper.Select(((TurbineGroupDataSet)data).turbine_group, sqlbldr.ToString(), paramList);

        }
    }
}
