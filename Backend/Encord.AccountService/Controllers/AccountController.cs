using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Encord.AccountService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Encord.AccountService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<object> Login(LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.NormalizedUserName == model.Username.ToUpper());
                return await GenerateJwtToken(model.Username, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        [HttpPost, Route("register")]
        public async Task<object> Register(RegisterDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return await GenerateJwtToken(model.Email, user);
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        private async Task<object> GenerateJwtToken(string userName, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                claims.Add(roleClaim);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Authorize(Roles = "TestRole")]
        [HttpGet("test")]
        public async Task<object> Protected()
        {
            return "Protected area";
        }

        [Authorize]
        [HttpGet, Route("all")]
        public async Task<IList<string>> GetAll()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(id);
            var roleList = await _userManager.GetRolesAsync(currentUser);
            return roleList;
        }


        [Authorize]
        [HttpPost("changeemail")]
        public async Task<string> ChangeEmail(string newEmail)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(id);
            currentUser.Email = newEmail;
            await _userManager.UpdateAsync(currentUser); //For the whole user you can just call updateasync
            return "wooloo";
        }

        [Authorize]
        [HttpGet("getuser")]
        public async Task<IdentityUser> getuser()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(id);
            return currentUser;
        }

        /// <summary>
        /// after this a new jwt token has to be generated
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost, Route("role-user")]
        public async Task<string> AddRoleToUser(string roleName)
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(email);

            var roleresult = await _userManager.AddToRoleAsync(currentUser, roleName);
            return roleresult.ToString();

        }

        [Authorize]
        [HttpPost, Route("role-create")]
        public async Task<string> CreateRole(string roleName)
        {
            if (!(await _roleManager.RoleExistsAsync(roleName)))
            {
                var role = new IdentityRole();
                role.Name = roleName;
                await _roleManager.CreateAsync(role);
                return "created!";
            }
            else
            {
                return "exists";
            }
        }
    }
}
