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

        Task AddMessageAsync(TextChannel channel, Message message);
        List<Channel> GetAllChannelsInGuild(string guildId);

        Task<bool> DeleteAllChannelsInGuildAsync(Guild guild);

        Task<Channel> CreateChannelAsync(Channel guild);
       Task<bool> DeleteChannelAsync(Channel channel);
    }
}
