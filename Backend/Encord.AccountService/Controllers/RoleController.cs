using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Encord.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Encord.AccountService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RoleController(UserManager<Account> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Add a role to the specific user
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>If it has completed successfully, a new JWT token has to be generated for the authorization to fully work</returns>
        [Authorize(Roles = "Administrator, RoleAdministrator")]
        [HttpPost, Route("add")]
        public async Task<string> AddRoleToUser(string userid, string roleName)
        {
            var currentUser = await _userManager.FindByIdAsync(userid);

            var roleresult = await _userManager.AddToRoleAsync(currentUser, roleName);
            return roleresult.ToString();

        }

        /// <summary>
        /// Create a role with this specific name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator, RoleAdministrator")]
        [HttpPost]
        public async Task<string> CreateRole(string roleName)
        {
            if (!(await _roleManager.RoleExistsAsync(roleName)))
            {
                var role = new Role();
                role.Name = roleName;
                await _roleManager.CreateAsync(role);
                return "created!";
            }
            else
            {
                return "exists";
            }
        }

        /// <summary>
        /// Get the current user roles
        /// </summary>
        /// <returns>A list of the role names</returns>
        [Authorize]
        [HttpGet("current/all")]
        public async Task<IList<string>> GetAllUserRoles()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(id);
            var roleList = await _userManager.GetRolesAsync(currentUser);
            return roleList;
        }

        /// <summary>
        /// Get all roles in the database
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Administrator, RoleAdministrator")]
        [HttpGet("all")]
        public async Task<List<Role>> GetAllRoles()
        {
            var roleList = _roleManager.Roles.ToList();
            return roleList;
        }
    }
}