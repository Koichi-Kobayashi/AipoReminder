using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace AipoReminder.ValueObject
{
    public class BalloonItem
    {
        private string key;
        private string title;
        private string subTitle;
        private List<UserNameObject> userNameObject;
        private string date;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">whatsnew_id</param>
        /// <param name="title">
        /// ブログタイトル(ブログ/ブログコメント) or 
        /// トピック(掲示板) or 
        /// 予定(スケジュール) or 
        /// 表題カテゴリ(ワークフロー) or
        /// 依頼者名(伝言メモ)
        /// </param>
        /// <param name="subTitle">
        /// 表題内容(ワークフロー) or
        /// 要件(伝言メモ)
        /// </param>
        /// <param name="userNameObject">ユーザ名</param>
        /// <param name="date">更新日付</param>
        public BalloonItem(string key, string title, string subTitle, List<UserNameObject> userNameObject, string date)
        {
            this.key = key;
            this.title = title;
            this.subTitle = subTitle;
            this.userNameObject = userNameObject;
            this.date = date;
        }

        /// <summary>
        /// key
        /// </summary>
        public string Key
        {
            get
            {
                return key;
            }
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("・");

            if (!String.IsNullOrEmpty(this.subTitle))
            {
                sb.Append("【");
                sb.Append(this.title);
                sb.Append(" 】");
                sb.Append(this.subTitle);
            }
            else
            {
                sb.Append(this.title);
            }

            sb.Append(" (");

            if (userNameObject != null)
            {
                for (int i = 0; i < this.userNameObject.Count; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append(this.userNameObject[i].LastName);
                    sb.Append(" ");
                    sb.Append(this.userNameObject[i].FirstName);
                }
            }

            sb.Append(") ");
            sb.Append(" ");
            sb.Append(this.date);

            return sb.ToString();
        }
    }
}
