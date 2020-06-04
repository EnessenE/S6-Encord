using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encord.ChannelService.Models;
using Encord.Common.Models;

namespace Encord.ChannelService.Interfaces
{
    public interface IChannelContext
    {
        TextChannel GetTextChannel(string id);
        VoiceChannel GetVoiceChannel(string id);

        void AddMessage(TextChannel channel, Message message);
        List<Channel> GetAllChannelsInGuild(string guildId);

        bool DeleteAllChannelsInGuild(Guild guild);

        Channel CreateChannel(Channel guild);
        bool DeleteChannel(Channel channel);
    }
}
