using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Encord.Common;
using Encord.Common.Configuration;
using Encord.Common.Models;
using Encord.GuildService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Encord.GuildService.Context
{
    public class GuildContext : DbContext, IGuildContext
    {
        private readonly IOptions<SQLSettings> _sqlSettings;
        private readonly ILogger<GuildContext> _logger;
        public DbSet<Guild> Guilds { get; set; }

        public GuildContext(IOptions<SQLSettings> _SqlSettings, ILogger<GuildContext> logger)
        {
            _sqlSettings = _SqlSettings;
            _logger = logger;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_sqlSettings.Value.ConnectionString);
        }

        public Guild GetGuild(string id)
        {
            var result = Guilds.Where(a => a.Id == id);
            Guild guild = null;
            if (result.Any())
            {
                guild = result.First();
            }
            return guild;

        }

        public List<Guild> GetAllGuilds()
        {
            return Guilds.ToList();
        }

        public List<Guild> GetUserGuilds(string userId)
        {
            throw new NotImplementedException();
        }

        public Guild CreateGuild(Guild guild)
        {
            guild.Id = null;
            guild.Deletable = true;
            Add(guild);
            SaveChanges();
            return guild; //GuildId is filled by entity
        }

        public bool DeleteGuild(Guild guild)
        {
            guild = GetGuild(guild.Id);

            if (guild.Deletable)
            {
                Remove(guild);
                SaveChanges();
                return true;
            }
            else
            {
                _logger.LogWarning("There was an attempt to delete a guild that isn't deletable. Guild ID: {guild}", guild.Id);
            }

            return false;
        }
    }
}
