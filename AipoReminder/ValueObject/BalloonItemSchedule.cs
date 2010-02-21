using System;
using System.Collections.Generic;
using System.Text;

namespace AipoReminder.ValueObject
{
    class BalloonItemSchedule : BalloonItem
    {
        public BalloonItemSchedule(string key, string title, string subTitle, List<UserNameObject> userNameObject, string date)
            : base(key, title, subTitle, userNameObject, date)
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
