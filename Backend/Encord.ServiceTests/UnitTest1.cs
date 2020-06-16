using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Encord.AccountService.Logic;
using Encord.AccountService.Models;
using Encord.Common.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Encord.ServiceTests
{
    [TestClass]
    public class UnitTest1
    {
        private AccountLogic _logic;
        private Account _testAccount;
        private JWTSettings _jwtSettings;


        public UnitTest1()
        {
            _jwtSettings = new JWTSettings()
            {
                ExpireDays = 30,
                Issuer = "http://localhost/unittest",
                Key = "unittest-testkey"
            };
            _logic = new AccountLogic(null, null, _jwtSettings);
        }

        [TestInitialize]
        public void Init()
        {
            Console.WriteLine("TEST!");
            _testAccount = new Account()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "unit@test.com",
                UserName = "unit1"
            };
        }

        private void TokenExistsCheck(string token)
        {
            Console.WriteLine("Generated token: " + token);
            Assert.IsFalse(string.IsNullOrEmpty(token));
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Issuer,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_jwtSettings.Key)) // The same key as the one that generate the token
            };
        }

        [TestMethod]
        public async Task GenerateToken()
        {
            string token = await _logic.GenerateJWTToken(_testAccount);
            TokenExistsCheck(token);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(45)]
        [DataRow(100)]
        [DataRow(1000)]
        public async Task GenerateMultipleTokens(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                string email = "unit" + i + "@test.com";
                Account account = new Account()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    UserName = "unit1"
                };
                string token = await _logic.GenerateJWTToken(account);
                TokenExistsCheck(token);
            }

        }

        [TestMethod]
        public async Task AreValidTokens()
        {
            string token = await _logic.GenerateJWTToken(_testAccount);
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            if (principal == null)
            {
                Assert.Fail("Prinical is empty, token invalid");
            }
            else
            {
                if (validatedToken == null)
                {
                    Assert.Fail("Exported token empty, invalid");
                }
            }

            TokenExistsCheck(token);
        }
    }

}