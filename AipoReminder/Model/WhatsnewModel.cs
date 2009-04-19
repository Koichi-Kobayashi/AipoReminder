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
        public void GetWhatsnewInfo(System.Data.DataSet data)
        {
            WhatsnewLogic logic = new WhatsnewLogic(this.dbHelper);

            // [ ブログ ] 新着記事を取得
            if (SettingManager.CheckBlog)
            {
                logic.GetWhatsnewBlogInfo(data);
            }

            // [ ブログ ] 新着コメントを取得
            if (SettingManager.CheckBlogComment)
            {
                logic.GetWhatsnewBlogCommentInfo(data);
            }

            // [ 掲示板 ]  新しい書き込みを取得
            if (SettingManager.CheckMsgboard)
            {
                logic.GetWhatsnewMsgboardInfo(data);
            }

            // [ スケジュール ] 新着予定を取得
            if (SettingManager.CheckSchedule)
            {
                logic.GetWhatsnewScheduleInfo(data);
            }

            // [ ワークフロー ] 新着依頼を取得
            if (SettingManager.CheckWorkflow)
            {
                logic.GetWhatsnewWorkflowInfo(data);
            }

            // [ 伝言メモ ]  新着メモを取得
            if (SettingManager.CheckMemo)
            {
                logic.GetWhatsnewMemoInfo(data);
            }
        }
    }
}
