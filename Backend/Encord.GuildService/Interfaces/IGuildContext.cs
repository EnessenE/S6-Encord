using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encord.Common;
using Encord.Common.Models;

namespace Encord.GuildService.Interfaces
{
    public interface IGuildContext
    {
        Task<Guild> GetGuild(string id);
        Task<List<Guild>> GetAllGuilds();

        Task<List<Guild>> GetUserGuilds(string userId);
        Task<Guild> CreateGuild(Guild guild);
    }
}
