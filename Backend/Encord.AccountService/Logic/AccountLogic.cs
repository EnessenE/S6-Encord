using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Encord.AccountService.Context;
using Encord.AccountService.Models;
using Encord.Common.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Encord.AccountService.Logic
{
    public class AccountLogic
    {
        private readonly UserManager<Account> _userManager;
        private readonly IdentityContext _context;
        private readonly JWTSettings _jwtOptions;

        public AccountLogic(UserManager<Account> userManager, IdentityContext context, IOptions<JWTSettings> jwtOptions)
        {
            _userManager = userManager;
            _context = context;
            _jwtOptions = jwtOptions.Value;
        }

        /// <summary>
        /// For unit tests
        /// </summary>
        public AccountLogic(UserManager<Account> userManager, IdentityContext context, JWTSettings jwtOptions)
        {
            _userManager = userManager;
            _context = context;
            _jwtOptions = jwtOptions;
        }

        /// <summary>
        /// Get accounts that have this role attached. Checks on ROLENAME!
        /// </summary>
        /// <param name="role">Role required</param>
        /// <returns></returns>
        public List<Account> GetAccounts(Role role)
        {
            var result = _userManager.GetUsersInRoleAsync(role.Name).Result;

            return result.ToList();
        }

        public async Task<string> GenerateJWTToken(Account user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            if (_userManager != null)
            {
                var roles = await _userManager.GetRolesAsync(user);

                foreach (var role in roles)
                {
                    var roleClaim = new Claim(ClaimTypes.Role, role);
                    claims.Add(roleClaim);
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtOptions.ExpireDays));

            var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
