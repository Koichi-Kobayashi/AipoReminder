using System;
using System.Collections.Generic;
using System.Text;

namespace AipoReminder.ValueObject
{
    public class UserNameObject
    {
        private string lastName = "";
        private string firstName = "";

        public UserNameObject(string lastName, string firstName)
        {
            this.lastName = lastName;
            this.firstName = firstName;
        }

        /// <summary>
        /// LastName
        /// </summary>
        public string LastName
        {
            get
            {
                return lastName;
            }
        }

        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName
        {
            get
            {
                return firstName;
            }
        }
    }
}
