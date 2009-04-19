using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AipoReminder.ValueObject
{
    class BalloonItemMsgboard : BalloonItem
    {
        public BalloonItemMsgboard(string key, string title, string subTitle, List<UserNameObject> userNameObject, string date)
            : base(key, title, subTitle, userNameObject, date)
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
