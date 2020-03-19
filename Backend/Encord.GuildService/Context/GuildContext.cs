using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encord.Common;
using Encord.Common.Configuration;
using Encord.GuildService.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Encord.GuildService.Context
{
    public class GuildContext : SqlDbContext, IGuildContext
    {
        public GuildContext(IOptions<SQLSettings> _SqlSettings, ILogger<SqlDbContext> logger) : base(_SqlSettings, logger)
        {
        }

        public async Task<Guild> GetGuild(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Guild>> GetUserGuilds(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Guild> CreateGuild(Guild guild)
        {
            throw new NotImplementedException();
        }
    }
}
