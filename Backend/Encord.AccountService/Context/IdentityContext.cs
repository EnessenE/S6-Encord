using Encord.AccountService.Models;
using Encord.Common.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Encord.AccountService.Context
{
    public class IdentityContext : IdentityDbContext<Account, Role, string>
    {
        private readonly string _connectionString;
        private ILogger<IdentityContext> _logger;

        public IdentityContext(IOptions<SQLSettings> _SqlSettings, ILogger<IdentityContext> logger)
        {
            _logger = logger;
            _connectionString = _SqlSettings.Value.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
