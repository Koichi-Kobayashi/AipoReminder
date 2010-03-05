using AipoReminder.DAO;
using WinFramework.Utility;

namespace AipoReminder.Logic
{
    class WhatsnewLogic
    {
        private DBHelper dbHelper;

        public WhatsnewLogic(DBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        /// <summary>
        /// [ ブログ ] 新着記事を取得
        /// </summary>
        /// <param name="data"></param>
        public int GetWhatsnewBlogInfo(System.Data.DataSet data)
        {
            WhatsnewDAO dao = new WhatsnewDAO(this.dbHelper);
            return dao.GetWhatsnewBlogInfo(data);
        }

        /// <summary>
        /// [ ブログ ] 新着コメントを取得
        /// </summary>
        /// <param name="data"></param>
        public int GetWhatsnewBlogCommentInfo(System.Data.DataSet data)
        {
            WhatsnewDAO dao = new WhatsnewDAO(this.dbHelper);
            return dao.GetWhatsnewBlogCommentInfo(data);
        }

        /// <summary>
        /// [ ワークフロー ] 新着依頼を取得
        /// </summary>
        /// <param name="data"></param>
        public int GetWhatsnewWorkflowInfo(System.Data.DataSet data)
        {
            WhatsnewDAO dao = new WhatsnewDAO(this.dbHelper);
            return dao.GetWhatsnewWorkflowInfo(data);
        }

        /// <summary>
        /// [ 掲示板 ]  新しい書き込みを取得
        /// </summary>
        /// <param name="data"></param>
        public int GetWhatsnewMsgboardInfo(System.Data.DataSet data)
        {
            WhatsnewDAO dao = new WhatsnewDAO(this.dbHelper);
            return dao.GetWhatsnewMsgboardInfo(data);
        }

        /// <summary>
        /// [ 伝言メモ ]  新着メモを取得
        /// </summary>
        /// <param name="data"></param>
        public int GetWhatsnewMemoInfo(System.Data.DataSet data)
        {
            WhatsnewDAO dao = new WhatsnewDAO(this.dbHelper);
            return dao.GetWhatsnewMemoInfo(data);
        }

        /// <summary>
        /// [ スケジュール ] 新着予定を取得
        /// </summary>
        /// <param name="data"></param>
        public int GetWhatsnewScheduleInfo(System.Data.DataSet data)
        {
            WhatsnewDAO dao = new WhatsnewDAO(this.dbHelper);
            return dao.GetWhatsnewScheduleInfo(data);
        }

    }
}
