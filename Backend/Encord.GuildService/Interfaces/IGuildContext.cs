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
        Guild GetGuild(string id);
        List<Guild> GetAllGuilds();

        List<Guild> GetUserGuilds(string userId);
        Guild CreateGuild(Guild guild);
        bool DeleteGuild(Guild guild);

    }
}
