using System;
using System.Collections.Generic;
using System.Text;

namespace AipoReminder.ValueObject
{
    class ActivityItem
    {
        private string id;
        private string last_name;
        private string first_name;
        private string title;
        private string update_date;

        public ActivityItem(string id, string last_name, string first_name, string title, string update_date)
        {
            this.id = id;
            this.last_name = last_name;
            this.first_name = first_name;
            this.title = title;
            this.update_date = update_date;
        }

        public string ToStringSender()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.last_name + " " + this.first_name + "さんより");

            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.title);
            sb.Append(" (");
            sb.Append(this.last_name + " " + this.first_name);
            sb.Append(") ");
            sb.Append(" ");
            sb.Append(this.update_date);

            return sb.ToString();
        }
    }
}
