using System;

namespace RMWPFUserInterface.Library.Models
{
    public interface ILoggedInUserModel
    {
        DateTime DateCreated { get; set; }
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string Id { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string Token { get; set; }

        void LogOut();
    }
}