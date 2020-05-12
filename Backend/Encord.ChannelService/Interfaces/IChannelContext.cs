using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encord.ChannelService.Models;

namespace Encord.ChannelService.Interfaces
{
    public interface IChannelContext
    {
        Channel GetChannel(string id);
        List<Channel> GetAllChannelsInGuild(string guildId);
        Channel CreateChannel(Channel guild);
        bool DeleteChannel(Channel channel);
    }
}
