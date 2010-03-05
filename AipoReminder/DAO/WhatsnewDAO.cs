using System;
using System.Collections;
using AipoReminder.Constants;
using AipoReminder.DataSet;
using AipoReminder.Utility;
using NpgsqlTypes;
using WinFramework.Utility;

namespace AipoReminder.DAO
{
    class WhatsnewDAO
    {
        private DBHelper dbHelper;

        public WhatsnewDAO(DBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        /// <summary>
        /// [ ブログ ] 新着記事を取得
        /// </summary>
        /// <param name="data"></param>
        public int GetWhatsnewBlogInfo(System.Data.DataSet data)
        {
            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();

            sqlbldr.AppendLine("select");
            sqlbldr.AppendLine(" t1.whatsnew_id");
            sqlbldr.AppendLine(", t2.title");
            sqlbldr.AppendLine(", t3.last_name");
            sqlbldr.AppendLine(", t3.first_name");
            sqlbldr.AppendLine(", to_char(t1.update_date, 'YYYY/MM/DD') as update_date");
            sqlbldr.AppendLine("from eip_t_whatsnew t1");
            sqlbldr.AppendLine("    join eip_t_blog_entry t2");
            sqlbldr.AppendLine("        on t1.entity_id = t2.entry_id");
            sqlbldr.AppendLine("    left join turbine_user t3");
            sqlbldr.AppendLine("        on t2.owner_id = t3.user_id");
            sqlbldr.AppendLine("where 1 = 1");
            sqlbldr.AppendLine("and t1.user_id != :user_id");
            sqlbldr.AppendLine("and t1.portlet_type = " + DBConstants.WHATS_NEW_TYPE_BLOG_ENTRY);
            sqlbldr.AppendLine("and t1.parent_id = 0 ");
            sqlbldr.AppendLine("and t1.whatsnew_id not in ");
            sqlbldr.AppendLine("    (");
            sqlbldr.AppendLine("        select");
            sqlbldr.AppendLine("         t4.parent_id");
            sqlbldr.AppendLine("        from eip_t_whatsnew t4");
            sqlbldr.AppendLine("        where 1 = 1");
            sqlbldr.AppendLine("        and t4.portlet_type = " + DBConstants.WHATS_NEW_TYPE_BLOG_ENTRY);
            sqlbldr.AppendLine("        and t4.user_id = :user_id");
            sqlbldr.AppendLine("    )");
            sqlbldr.AppendLine("order by t1.update_date desc");

            WhatsnewDataSet.search_eip_t_whatsnewRow param = ((WhatsnewDataSet)data).search_eip_t_whatsnew[0];

            ArrayList paramList = new ArrayList();
            if (!String.IsNullOrEmpty(param.user_id))
            {
                paramList.Add(DBUtility.MakeParameter("user_id", param.user_id, NpgsqlDbType.Integer));
            }

            return this.dbHelper.Select(((WhatsnewDataSet)data).eip_t_whatsnew_blog, sqlbldr.ToString(), paramList);
        }

        /// <summary>
        /// [ ブログ ] 新着コメントを取得
        /// </summary>
        /// <param name="data"></param>
        public int GetWhatsnewBlogCommentInfo(System.Data.DataSet data)
        {
            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();

            sqlbldr.AppendLine("select");
            sqlbldr.AppendLine(" t1.whatsnew_id");
            sqlbldr.AppendLine(", t3.entry_id");
            sqlbldr.AppendLine(", t3.title");
            sqlbldr.AppendLine(", t4.last_name");
            sqlbldr.AppendLine(", t4.first_name");
            sqlbldr.AppendLine(", to_char(t1.update_date, 'YYYY/MM/DD') as update_date");
            sqlbldr.AppendLine("from eip_t_whatsnew t1");
            sqlbldr.AppendLine("    join eip_t_blog_comment t2");
            sqlbldr.AppendLine("        on t1.entity_id = t2.comment_id");
            sqlbldr.AppendLine("    left join eip_t_blog_entry t3");
            sqlbldr.AppendLine("        on t2.entry_id = t3.entry_id");
            sqlbldr.AppendLine("    left join turbine_user t4");
            sqlbldr.AppendLine("        on t2.owner_id = t4.user_id");
            sqlbldr.AppendLine("where 1 = 1");
            sqlbldr.AppendLine("and t1.user_id = :user_id");
            sqlbldr.AppendLine("and t1.portlet_type = " + DBConstants.WHATS_NEW_TYPE_BLOG_COMMENT);
            sqlbldr.AppendLine("order by t1.update_date desc");
            
            WhatsnewDataSet.search_eip_t_whatsnewRow param = ((WhatsnewDataSet)data).search_eip_t_whatsnew[0];

            ArrayList paramList = new ArrayList();
            if (!String.IsNullOrEmpty(param.user_id))
            {
                paramList.Add(DBUtility.MakeParameter("user_id", param.user_id, NpgsqlDbType.Integer));
            }

            return this.dbHelper.Select(((WhatsnewDataSet)data).eip_t_whatsnew_blog_comment, sqlbldr.ToString(), paramList);
        }

        /// <summary>
        /// [ ワークフロー ] 新着依頼を取得
        /// </summary>
        /// <param name="data"></param>
        public int GetWhatsnewWorkflowInfo(System.Data.DataSet data)
        {
            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();

            if (SettingManager.AipoVersion == DBConstants.AIPO_VERSION_5)
            {
                sqlbldr.AppendLine("select ");
                sqlbldr.AppendLine(" t1.whatsnew_id");
                sqlbldr.AppendLine(", t2.request_name");
                sqlbldr.AppendLine(", t3.category_name");
                sqlbldr.AppendLine(", t4.last_name");
                sqlbldr.AppendLine(", t4.first_name");
                sqlbldr.AppendLine(", to_char(t1.update_date, 'YYYY/MM/DD') as update_date");
                sqlbldr.AppendLine("from eip_t_whatsnew t1");
                sqlbldr.AppendLine("    join eip_t_workflow_request t2");
                sqlbldr.AppendLine("        on t1.entity_id = t2.request_id");
                sqlbldr.AppendLine("    left join eip_t_workflow_category t3");
                sqlbldr.AppendLine("        on t2.category_id = t3.category_id");
                sqlbldr.AppendLine("    left join turbine_user t4");
                sqlbldr.AppendLine("        on t1.user_id = t4.user_id");
                sqlbldr.AppendLine("where 1 = 1");
                sqlbldr.AppendLine("and t1.user_id = :user_id");
                sqlbldr.AppendLine("and t1.portlet_type = " + DBConstants.WHATS_NEW_TYPE_WORKFLOW_REQUEST);
                sqlbldr.AppendLine("and t2.request_id in ");
                sqlbldr.AppendLine("    (");
                sqlbldr.AppendLine("        select");
                sqlbldr.AppendLine("         t1.entity_id");
                sqlbldr.AppendLine("        from eip_t_whatsnew t1");
                sqlbldr.AppendLine("        where 1 = 1");
                sqlbldr.AppendLine("        and t1.user_id = :user_id");
                sqlbldr.AppendLine("        and t1.portlet_type = " + DBConstants.WHATS_NEW_TYPE_WORKFLOW_REQUEST);
                sqlbldr.AppendLine("    )");
                sqlbldr.AppendLine("order by t1.update_date desc");
                sqlbldr.AppendLine("");
            }
            else if (SettingManager.AipoVersion == DBConstants.AIPO_VERSION_4)
            {
                sqlbldr.AppendLine("select ");
                sqlbldr.AppendLine(" t1.whatsnew_id");
                sqlbldr.AppendLine(", t3.request_name");
                sqlbldr.AppendLine(", t4.category_name");
                sqlbldr.AppendLine(", t5.last_name");
                sqlbldr.AppendLine(", t5.first_name");
                sqlbldr.AppendLine(", to_char(t2.update_date, 'YYYY/MM/DD') as update_date");
                sqlbldr.AppendLine("from eip_t_whatsnew t1");
                sqlbldr.AppendLine("    join eip_t_workflow_request_map t2");
                sqlbldr.AppendLine("        on t2.request_id = t1.entity_id");
                sqlbldr.AppendLine("    join eip_t_workflow_request t3");
                sqlbldr.AppendLine("        on t2.request_id = t3.request_id");
                sqlbldr.AppendLine("    left join eip_t_workflow_category t4");
                sqlbldr.AppendLine("        on t3.category_id = t4.category_id");
                sqlbldr.AppendLine("    left join turbine_user t5");
                sqlbldr.AppendLine("        on t2.user_id = t5.user_id");
                sqlbldr.AppendLine("where 1 = 1");
                sqlbldr.AppendLine("and t1.user_id = :user_id");
                sqlbldr.AppendLine("and t1.portlet_type = " + DBConstants.WHATS_NEW_TYPE_WORKFLOW_REQUEST);
                sqlbldr.AppendLine("and t2.user_id != :user_id");
                sqlbldr.AppendLine("and t2.request_id in ");
                sqlbldr.AppendLine("    (");
                sqlbldr.AppendLine("        select");
                sqlbldr.AppendLine("         t1.entity_id");
                sqlbldr.AppendLine("        from eip_t_whatsnew t1");
                sqlbldr.AppendLine("        where 1 = 1");
                sqlbldr.AppendLine("        and t1.user_id = :user_id");
                sqlbldr.AppendLine("        and t1.portlet_type = " + DBConstants.WHATS_NEW_TYPE_WORKFLOW_REQUEST);
                sqlbldr.AppendLine("    )");
                sqlbldr.AppendLine("order by t1.update_date desc");
                sqlbldr.AppendLine("");
            }
            WhatsnewDataSet.search_eip_t_whatsnewRow param = ((WhatsnewDataSet)data).search_eip_t_whatsnew[0];

            ArrayList paramList = new ArrayList();
            if (!String.IsNullOrEmpty(param.user_id))
            {
                paramList.Add(DBUtility.MakeParameter("user_id", param.user_id, NpgsqlDbType.Integer));
            }

            return this.dbHelper.Select(((WhatsnewDataSet)data).eip_t_whatsnew_workflow, sqlbldr.ToString(), paramList);
        }

        /// <summary>
        /// [ 掲示板 ]  新しい書き込みを取得
        /// </summary>
        /// <param name="data"></param>
        public int GetWhatsnewMsgboardInfo(System.Data.DataSet data)
        {
            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();

            sqlbldr.AppendLine("select ");
            sqlbldr.AppendLine(" t1.topic_id");
            sqlbldr.AppendLine(", t1.topic_name");
            sqlbldr.AppendLine(", t2.last_name");
            sqlbldr.AppendLine(", t2.first_name");
            sqlbldr.AppendLine(", to_char(t1.update_date, 'YYYY/MM/DD') as update_date");
            sqlbldr.AppendLine("from eip_t_msgboard_topic t1");
            sqlbldr.AppendLine("    left join turbine_user t2");
            sqlbldr.AppendLine("        on t1.owner_id = t2.user_id");
            sqlbldr.AppendLine("where 1 = 1");
            sqlbldr.AppendLine("and t1.owner_id != :user_id");
            sqlbldr.AppendLine("and t1.topic_id in");
            sqlbldr.AppendLine("    (");
            sqlbldr.AppendLine("        select");
            sqlbldr.AppendLine("         t3.entity_id");
            sqlbldr.AppendLine("        from eip_t_whatsnew t3");
            sqlbldr.AppendLine("        where 1 = 1");
            sqlbldr.AppendLine("        and t3.user_id = :user_id");
            sqlbldr.AppendLine("        and t3.parent_id = -1");
            sqlbldr.AppendLine("        and t3.portlet_type = " + DBConstants.WHATS_NEW_TYPE_MSGBOARD_TOPIC);
            sqlbldr.AppendLine("        or (");
            sqlbldr.AppendLine("            t3.parent_id = 0");
            sqlbldr.AppendLine("            and t3.portlet_type = " + DBConstants.WHATS_NEW_TYPE_MSGBOARD_TOPIC);
            sqlbldr.AppendLine("        )");
            sqlbldr.AppendLine("        and not exists ");
            sqlbldr.AppendLine("        (");
            sqlbldr.AppendLine("            select ");
            sqlbldr.AppendLine("             'X' ");
            sqlbldr.AppendLine("            from eip_t_whatsnew t4 ");
            sqlbldr.AppendLine("            where 1 = 1 ");
            sqlbldr.AppendLine("            and t3.whatsnew_id = t4.parent_id");
            sqlbldr.AppendLine("            and t4.user_id = :user_id");
            sqlbldr.AppendLine("            and t4.portlet_type = " + DBConstants.WHATS_NEW_TYPE_MSGBOARD_TOPIC);
            sqlbldr.AppendLine("        )");
            sqlbldr.AppendLine("    )");
            sqlbldr.AppendLine("and exists");
            sqlbldr.AppendLine("    (");
            sqlbldr.AppendLine("        select");
            sqlbldr.AppendLine("         'X' ");
            sqlbldr.AppendLine("        from eip_t_msgboard_category_map t5 ");
            sqlbldr.AppendLine("        where 1 = 1 ");
            sqlbldr.AppendLine("        and t5.category_id = t1.category_id");
            sqlbldr.AppendLine("        and t5.user_id = :user_id");
            sqlbldr.AppendLine("    )");
            sqlbldr.AppendLine("union ");
            sqlbldr.AppendLine("select ");
            sqlbldr.AppendLine(" t1.topic_id");
            sqlbldr.AppendLine(", t1.topic_name");
            sqlbldr.AppendLine(", t2.last_name");
            sqlbldr.AppendLine(", t2.first_name");
            sqlbldr.AppendLine(", to_char(t1.update_date, 'YYYY/MM/DD') as update_date");
            sqlbldr.AppendLine("from eip_t_msgboard_topic t1");
            sqlbldr.AppendLine("    left join turbine_user t2");
            sqlbldr.AppendLine("        on t1.owner_id = t2.user_id");
            sqlbldr.AppendLine("    left join eip_t_msgboard_category t6");
            sqlbldr.AppendLine("        on t1.category_id = t6.category_id");
            sqlbldr.AppendLine("where 1 = 1");
            sqlbldr.AppendLine("and t1.owner_id != :user_id");
            sqlbldr.AppendLine("and t6.public_flag = 'T'");
            sqlbldr.AppendLine("and t1.topic_id in");
            sqlbldr.AppendLine("    (");
            sqlbldr.AppendLine("        select");
            sqlbldr.AppendLine("         t3.entity_id");
            sqlbldr.AppendLine("        from eip_t_whatsnew t3");
            sqlbldr.AppendLine("        where 1 = 1");
            sqlbldr.AppendLine("        and t3.user_id = :user_id");
            sqlbldr.AppendLine("        and t3.parent_id = -1");
            sqlbldr.AppendLine("        and t3.portlet_type = " + DBConstants.WHATS_NEW_TYPE_MSGBOARD_TOPIC);
            sqlbldr.AppendLine("        or (");
            sqlbldr.AppendLine("            t3.parent_id = 0");
            sqlbldr.AppendLine("            and t3.portlet_type = " + DBConstants.WHATS_NEW_TYPE_MSGBOARD_TOPIC);
            sqlbldr.AppendLine("        )");
            sqlbldr.AppendLine("        and not exists ");
            sqlbldr.AppendLine("        (");
            sqlbldr.AppendLine("            select ");
            sqlbldr.AppendLine("             'X' ");
            sqlbldr.AppendLine("            from eip_t_whatsnew t4 ");
            sqlbldr.AppendLine("            where 1 = 1 ");
            sqlbldr.AppendLine("            and t3.whatsnew_id = t4.parent_id");
            sqlbldr.AppendLine("            and t4.user_id = :user_id");
            sqlbldr.AppendLine("            and t4.portlet_type = " + DBConstants.WHATS_NEW_TYPE_MSGBOARD_TOPIC);
            sqlbldr.AppendLine("        )");
            sqlbldr.AppendLine("    )");

            sqlbldr.AppendLine("order by update_date desc, topic_id desc");

            WhatsnewDataSet.search_eip_t_whatsnewRow param = ((WhatsnewDataSet)data).search_eip_t_whatsnew[0];

            ArrayList paramList = new ArrayList();
            if (!String.IsNullOrEmpty(param.user_id))
            {
                paramList.Add(DBUtility.MakeParameter("user_id", param.user_id, NpgsqlDbType.Integer));
            }

            return this.dbHelper.Select(((WhatsnewDataSet)data).eip_t_whatsnew_msgboard, sqlbldr.ToString(), paramList);
        }

        /// <summary>
        /// [ 伝言メモ ]  新着メモを取得
        /// </summary>
        /// <param name="data"></param>
        public int GetWhatsnewMemoInfo(System.Data.DataSet data)
        {
            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();

            sqlbldr.AppendLine("select");
            sqlbldr.AppendLine(" t1.whatsnew_id");
            sqlbldr.AppendLine(", t2.client_name");
            sqlbldr.AppendLine(", t2.subject_type");
            sqlbldr.AppendLine(", t2.custom_subject");
            sqlbldr.AppendLine(", t3.last_name");
            sqlbldr.AppendLine(", t3.first_name");
            sqlbldr.AppendLine(", to_char(t1.update_date, 'YYYY/MM/DD') as update_date");
            sqlbldr.AppendLine("from eip_t_whatsnew t1");
            sqlbldr.AppendLine("    join eip_t_note t2");
            sqlbldr.AppendLine("        on t1.entity_id = t2.note_id");
            sqlbldr.AppendLine("    left join turbine_user t3");
            sqlbldr.AppendLine("        on t2.owner_id = t3.user_id");
            sqlbldr.AppendLine("where 1 = 1");
            sqlbldr.AppendLine("and t1.user_id = :user_id");
            sqlbldr.AppendLine("and t1.portlet_type = " + DBConstants.WHATS_NEW_TYPE_NOTE);
            sqlbldr.AppendLine("order by t1.update_date desc");

            WhatsnewDataSet.search_eip_t_whatsnewRow param = ((WhatsnewDataSet)data).search_eip_t_whatsnew[0];

            ArrayList paramList = new ArrayList();
            if (!String.IsNullOrEmpty(param.user_id))
            {
                paramList.Add(DBUtility.MakeParameter("user_id", param.user_id, NpgsqlDbType.Integer));
            }

            return this.dbHelper.Select(((WhatsnewDataSet)data).eip_t_whatsnew_memo, sqlbldr.ToString(), paramList);
        }

        /// <summary>
        /// [ スケジュール ] 新着予定を取得
        /// </summary>
        /// <param name="data"></param>
        public int GetWhatsnewScheduleInfo(System.Data.DataSet data)
        {
            System.Text.StringBuilder sqlbldr = new System.Text.StringBuilder();

            sqlbldr.AppendLine("select");
            sqlbldr.AppendLine(" t1.whatsnew_id");
            sqlbldr.AppendLine(", t2.name");
            sqlbldr.AppendLine(", t3.last_name");
            sqlbldr.AppendLine(", t3.first_name");
            sqlbldr.AppendLine(", to_char(t2.start_date, 'YYYY/MM/DD') as update_date");
            sqlbldr.AppendLine("from eip_t_whatsnew t1");
            sqlbldr.AppendLine("    join eip_t_schedule t2");
            sqlbldr.AppendLine("        on t1.entity_id = t2.schedule_id");
            sqlbldr.AppendLine("    left join turbine_user t3");
            sqlbldr.AppendLine("        on t2.owner_id = t3.user_id");
            sqlbldr.AppendLine("where 1 = 1");
            sqlbldr.AppendLine("and t1.user_id = :user_id");
            sqlbldr.AppendLine("and t1.portlet_type = " + DBConstants.WHATS_NEW_TYPE_SCHEDULE);
            sqlbldr.AppendLine("and t1.parent_id not in (");
            sqlbldr.AppendLine("    select");
            sqlbldr.AppendLine("     t4.whatsnew_id");
            sqlbldr.AppendLine("    from eip_t_whatsnew t4");
            sqlbldr.AppendLine("    where 1 = 1");
            sqlbldr.AppendLine("    and t4.portlet_type = " + DBConstants.WHATS_NEW_TYPE_SCHEDULE);
            sqlbldr.AppendLine("    and t4.parent_id = 0 )");
            sqlbldr.AppendLine("order by t2.update_date desc");

            WhatsnewDataSet.search_eip_t_whatsnewRow param = ((WhatsnewDataSet)data).search_eip_t_whatsnew[0];

            ArrayList paramList = new ArrayList();
            if (!String.IsNullOrEmpty(param.user_id))
            {
                paramList.Add(DBUtility.MakeParameter("user_id", param.user_id, NpgsqlDbType.Integer));
            }

            return this.dbHelper.Select(((WhatsnewDataSet)data).eip_t_whatsnew_schedule, sqlbldr.ToString(), paramList);
        }

    }
}
