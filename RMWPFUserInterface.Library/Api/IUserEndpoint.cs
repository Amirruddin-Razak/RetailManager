using RMWPFUserInterface.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMWPFUserInterface.Library.Api
{
    public interface IUserEndpoint
    {
        Task<List<UserModel>> GetAll();
        Task<Dictionary<string, string>> GetAllRoles();
        Task AddUserToRole(string userId, string role);
        Task RemoveUserFromRole(string userId, string role);
    }
}