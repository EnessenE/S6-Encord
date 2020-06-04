using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encord.Common;
using Encord.Common.MessageBrokerModels;
using Encord.Common.Models;
using Encord.GuildService.Context;
using Encord.GuildService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Encord.GuildService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class GuildController : ControllerBase
    {
        private IGuildContext _guildContext;
        private MessageBrokerContext _messageBroker;

        public GuildController(IGuildContext guildContext, MessageBrokerContext messageBroker)
        {
            _guildContext = guildContext;
            _messageBroker = messageBroker;
        }

        /// <summary>
        /// Retrieve all guilds
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Guild> GetGuild()
        {
            return _guildContext.GetAllGuilds();
        }

        /// <summary>
        /// Retrieve a specific guild
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Guild GetGuild(string id)
        {
            return _guildContext.GetGuild(id);
        }

        /// <summary>
        /// Retrieve guilds the user is connected to
        /// </summary>
        /// <returns></returns>
        [HttpGet("user")]
        public List<Guild> GetUserGuilds(string userId)
        {
            return _guildContext.GetUserGuilds(userId);
        }

        /// <summary>
        /// Create a guild
        /// </summary>
        /// <param name="newGuild"></param>
        /// <returns></returns>
        [HttpPost]
        public Guild CreateGuild(Guild newGuild)
        {
            newGuild.CreationDate = DateTime.Now;
            return _guildContext.CreateGuild(newGuild);
        }

        /// <summary>
        /// Delete a guild
        /// </summary>
        /// <param name="newGuild"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool DeleteGuild(Guild newGuild)
        {
            bool result = _guildContext.DeleteGuild(newGuild);
            ChannelMessage message = new ChannelMessage()
            {
                Guild =  newGuild,
                Deletion = true
            };
            _messageBroker.CreateMessage("Channel", message);
            return result;
        }
    }
}