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
        public void GetWhatsnewBlogInfo(System.Data.DataSet data)
        {
            WhatsnewDAO dao = new WhatsnewDAO(this.dbHelper);
            dao.GetWhatsnewBlogInfo(data);
        }

        /// <summary>
        /// [ ブログ ] 新着コメントを取得
        /// </summary>
        /// <param name="data"></param>
        public void GetWhatsnewBlogCommentInfo(System.Data.DataSet data)
        {
            WhatsnewDAO dao = new WhatsnewDAO(this.dbHelper);
            dao.GetWhatsnewBlogCommentInfo(data);
        }

        /// <summary>
        /// [ ワークフロー ] 新着依頼を取得
        /// </summary>
        /// <param name="data"></param>
        public void GetWhatsnewWorkflowInfo(System.Data.DataSet data)
        {
            WhatsnewDAO dao = new WhatsnewDAO(this.dbHelper);
            dao.GetWhatsnewWorkflowInfo(data);
        }

        /// <summary>
        /// [ 掲示板 ]  新しい書き込みを取得
        /// </summary>
        /// <param name="data"></param>
        public void GetWhatsnewMsgboardInfo(System.Data.DataSet data)
        {
            WhatsnewDAO dao = new WhatsnewDAO(this.dbHelper);
            dao.GetWhatsnewMsgboardInfo(data);
        }

        /// <summary>
        /// [ 伝言メモ ]  新着メモを取得
        /// </summary>
        /// <param name="data"></param>
        public void GetWhatsnewMemoInfo(System.Data.DataSet data)
        {
            WhatsnewDAO whatsnewDao = new WhatsnewDAO(this.dbHelper);
            whatsnewDao.GetWhatsnewMemoInfo(data);
        }

        /// <summary>
        /// [ スケジュール ] 新着予定を取得
        /// </summary>
        /// <param name="data"></param>
        public void GetWhatsnewScheduleInfo(System.Data.DataSet data)
        {
            WhatsnewDAO dao = new WhatsnewDAO(this.dbHelper);
            dao.GetWhatsnewScheduleInfo(data);
        }

    }
}
