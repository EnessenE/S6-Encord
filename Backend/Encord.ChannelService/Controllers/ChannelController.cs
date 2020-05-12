using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encord.ChannelService.Context;
using Encord.ChannelService.Enums;
using Encord.ChannelService.Interfaces;
using Encord.ChannelService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Encord.ChannelService.Controllers
{
    [Route("[controller]")]
    [ApiController]
   // [Authorize]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelContext _channelContext;
        private MessageBrokerContext _messageBroker;

        public ChannelController(IChannelContext channelContext, MessageBrokerContext messageBroker)
        {
            _channelContext = channelContext;
            _messageBroker = messageBroker;
        }

        [HttpGet("{id}")]
        public Channel GetChannel(string id)
        {
            return _channelContext.GetChannel(id);
        }

        [HttpGet("guild/{guildId}")]
        public List<Channel> GetAllChannelsInGuild(string guildId)
        {
            return _channelContext.GetAllChannelsInGuild(guildId);
        }

        [HttpGet("types")]
        public List<string> GetTypes()
        {
            return Enum.GetNames(typeof(ChannelType)).ToList();
        }

        [HttpPost]
        public Channel CreateChannel(Channel channel)
        {
            channel.CreatedDate = DateTime.Now;
            return _channelContext.CreateChannel(channel);
        }

        [HttpDelete]
        public bool DeleteChannel(Channel channel)
        {
            return _channelContext.DeleteChannel(channel);
        }
    }
}