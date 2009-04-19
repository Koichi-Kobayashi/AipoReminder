using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AipoReminder.ValueObject
{
    class BalloonItemWorkflow : BalloonItem
    {
        public BalloonItemWorkflow(string key, string title, string subTitle, List<UserNameObject> userNameObject, string date)
            : base(key, title, subTitle, userNameObject, date)
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
