using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encord.ChannelService.Interfaces;
using Encord.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Encord.ChannelService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelContext _channelContext;

        public ChannelController(IChannelContext channelContext)
        {
            _channelContext = channelContext;
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