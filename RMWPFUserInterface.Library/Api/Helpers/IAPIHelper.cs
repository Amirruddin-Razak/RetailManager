using RMWPFUserInterface.Library.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace RMWPFUserInterface.Library.Api.Helpers
{
    public interface IApiHelper
    {
        HttpClient ApiClient { get; }
        Task<AuthenticatedUser> Authenticate(string username, string password);
        void LogOffUser();
        Task GetLoggedInUserInfo();
    }
}