using RMWPFUserInterface.Library.Models;
using System.Threading.Tasks;

namespace RMWPFUserInterface.Library.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUserInfo();
    }
}