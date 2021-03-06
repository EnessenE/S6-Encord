﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Encord.AccountService.Logic;
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
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;
        private readonly IConfiguration _configuration;
        private AccountLogic _accountLogic;

        public AccountController(
            UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            IConfiguration configuration, AccountLogic accountLogic)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _accountLogic = accountLogic;
        }

        [HttpPost("login")]
        public async Task<Object> Login(LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.NormalizedUserName == model.Username.ToUpper());
                LoginDto login = new LoginDto();
                login.Username = model.Username;
                login.Token = await _accountLogic.GenerateJWTToken(appUser);
                return login;
            }

            return Unauthorized();
        }

        [HttpPost, Route("register")]
        public async Task<LoginDto> Register(RegisterDto model)
        {
            var user = new Account
            {
                Email = model.Email,
                UserName = model.UserName,
                JoinDate = DateTime.Now
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                LoginDto login = new LoginDto()
                {
                    Username = user.UserName,
                    Token = await _accountLogic.GenerateJWTToken(user)
            };
                return login;
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        [Authorize]
        [HttpPut("email")]
        public async Task<string> ChangeEmail(string newEmail)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(id);
            currentUser.Email = newEmail;
            await _userManager.UpdateAsync(currentUser); //For the whole user you can just call updateasync
            return "Success";
        }

        [Authorize]
        [HttpGet]
        public async Task<Account> GetUser()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var currentUser = await _userManager.FindByIdAsync(id);
            if (currentUser != null)
            {
#warning move to [jsonignore]
                currentUser.PasswordHash = null;
                currentUser.SecurityStamp = null;

            }

            return currentUser;
        }
    }
}
