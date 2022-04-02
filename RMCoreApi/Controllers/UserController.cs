using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RMCoreApi.Data;
using RMCoreApi.Models;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RMCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public UserDBModel GetById()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserData data = new UserData();

            return data.GetUserById(userId);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Admin/GetAllUsers")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            List<ApplicationUserModel> output = new List<ApplicationUserModel>();

            var users = _context.Users.ToList();
            var userRoles = from ur in _context.UserRoles
                            join r in _context.Roles on ur.RoleId equals r.Id
                            select new { ur.UserId, ur.RoleId, r.Name };

            foreach (var user in users)
            {
                ApplicationUserModel userModel = new ApplicationUserModel
                {
                    Id = user.Id,
                    EmailAddress = user.Email
                };

                userModel.Roles = userRoles.Where(x => x.UserId == userModel.Id).ToDictionary(key => key.RoleId, val => val.Name);

                output.Add(userModel);
            }

            return output;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Admin/GetAllRoles")]
        public Dictionary<string, string> GetAllRoles()
        {
            Dictionary<string, string> roles = _context.Roles.ToDictionary(x => x.Id, x => x.Name);

            return roles;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Admin/AddRole")]
        public async Task AddARole(UserRolePairModel pairModel)
        {
            var user = await _userManager.FindByIdAsync(pairModel.UserId);
            await _userManager.AddToRoleAsync(user, pairModel.Role);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Admin/RemoveRole")]
        public async Task RemoveARole(UserRolePairModel pairModel)
        {
            var user = await _userManager.FindByIdAsync(pairModel.UserId);
            await _userManager.RemoveFromRoleAsync(user, pairModel.Role);
        }
    }
}
