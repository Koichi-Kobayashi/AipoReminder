using System;
using System.Collections.Generic;
using System.Text;
using WinFramework.Model;
using AipoReminder.Logic;
using AipoReminder.Utility;

namespace AipoReminder.Model
{
    class WhatsnewModel : DbConnectionModel
    {
        /// <summary>
        /// 新着情報を取得
        /// </summary>
        /// <param name="data"></param>
        public int GetWhatsnewInfo(System.Data.DataSet data)
        {
            int ret = -1;
            WhatsnewLogic logic = new WhatsnewLogic(this.dbHelper);

            // [ ブログ ] 新着記事を取得
            if (SettingManager.CheckBlog)
            {
                ret = logic.GetWhatsnewBlogInfo(data);
            }

            // [ ブログ ] 新着コメントを取得
            if (SettingManager.CheckBlogComment)
            {
                ret = logic.GetWhatsnewBlogCommentInfo(data);
            }

            // [ 掲示板 ]  新しい書き込みを取得
            if (SettingManager.CheckMsgboard)
            {
                ret = logic.GetWhatsnewMsgboardInfo(data);
            }

            // [ スケジュール ] 新着予定を取得
            if (SettingManager.CheckSchedule)
            {
                ret = logic.GetWhatsnewScheduleInfo(data);
            }

            // [ ワークフロー ] 新着依頼を取得
            if (SettingManager.CheckWorkflow)
            {
                ret = logic.GetWhatsnewWorkflowInfo(data);
            }

            // [ 伝言メモ ]  新着メモを取得
            if (SettingManager.CheckMemo)
            {
                ret = logic.GetWhatsnewMemoInfo(data);
            }

            return ret;
        }
    }
}
