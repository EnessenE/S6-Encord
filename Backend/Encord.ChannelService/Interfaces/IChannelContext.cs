using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encord.ChannelService.Models;

namespace Encord.ChannelService.Interfaces
{
    public interface IChannelContext
    {
        TextChannel GetTextChannel(string id);
        VoiceChannel GetVoiceChannel(string id);

        List<Channel> GetAllChannelsInGuild(string guildId);

        bool DeleteAllChannelsInGuild(string guildId);

        Channel CreateChannel(Channel guild);
        bool DeleteChannel(Channel channel);
    }
}
