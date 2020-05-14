using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encord.ChannelService.Interfaces;
using Encord.Common.Models;
using Microsoft.Extensions.Logging;

namespace Encord.ChannelService.Handlers
{
    public class MessageHandler
    {
        private readonly ILogger<MessageHandler> _logger;
        private readonly IChannelContext _channelContext;

        public MessageHandler(ILogger<MessageHandler> logger, IChannelContext channelContext)
        {
            _logger = logger;
            _channelContext = channelContext;
        }

        public void HandleMessage(ChannelMessage channelMessage)
        {
            _logger.LogInformation("Handling a received Content");

            if (channelMessage.Deletion)
            {
                _channelContext.DeleteAllChannelsInGuild(channelMessage.GuildId);
            }
        }
    }
}
