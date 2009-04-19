
using System;
namespace AipoReminder.ValueObject
{
    class ComboItemCheckTime
    {
        private int checkTime;

        public ComboItemCheckTime(int checkTime)
        {
            this.checkTime = checkTime;
        }

        /// <summary>
        /// スケジュール確認時間
        /// </summary>
        public int CheckTime
        {
            get
            {
                return checkTime;
            }
        }

        /// <summary>
        /// コンボボックスに表示
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0, 2}", checkTime) + "分前";
        }
    }
}
