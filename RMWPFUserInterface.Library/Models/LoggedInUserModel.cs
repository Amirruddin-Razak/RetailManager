using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMWPFUserInterface.Library.Models
{
    public class LoggedInUserModel : ILoggedInUserModel
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateCreated { get; set; }

        public void LogOut()
        {
            Token = "";
            Id = "";
            FirstName = "";
            LastName = "";
            PhoneNumber = "";
            EmailAddress = "";
            DateCreated = DateTime.MinValue;
        }
    }
}
