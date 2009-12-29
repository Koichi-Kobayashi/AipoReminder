
using System;
namespace AipoReminder.ValueObject
{
    class ComboItemCheckTime
    {
        // スケジュール確認時間
        public int CheckTime { get; set; }

        public ComboItemCheckTime(int checkTime)
        {
            CheckTime = checkTime;
        }

        /// <summary>
        /// コンボボックスに表示
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0, 2}", CheckTime) + "分前";
        }
    }
}
