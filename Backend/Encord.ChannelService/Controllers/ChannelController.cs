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

        [HttpGet("text/{id}")]
        public TextChannel GetTextChannel(string id)
        {
            return _channelContext.GetTextChannel(id);
        }

        [HttpGet("voice/{id}")]
        public VoiceChannel GetVoiceChannel(string id)
        {
            return _channelContext.GetVoiceChannel(id);
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
        public async Task<Channel> CreateChannel(Channel channel)
        {
            channel.CreatedDate = DateTime.Now;
            return await _channelContext.CreateChannelAsync(channel);
        }

        [HttpDelete]
        public async Task<bool> DeleteChannel(Channel channel)
        {
            return await _channelContext.DeleteChannelAsync(channel);
        }
    }
}