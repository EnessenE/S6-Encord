using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encord.Common;
using Encord.Common.Models;
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

        public GuildController(IGuildContext guildContext)
        {
            _guildContext = guildContext;
        }

        /// <summary>
        /// Retrieve all guilds
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Guild>> GetGuildAsync()
        {
            return await _guildContext.GetAllGuilds();
        }

        /// <summary>
        /// Retrieve a specific guild
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Guild> GetGuildAsync(string id)
        {
            return await _guildContext.GetGuild(id);
        }

        /// <summary>
        /// Retrieve guilds the user is connected to
        /// </summary>
        /// <returns></returns>
        [HttpGet("user")]
        public async Task<List<Guild>> GetUserGuilds(string userId)
        {
            return await _guildContext.GetUserGuilds(userId);
        }

        /// <summary>
        /// Create a guild
        /// </summary>
        /// <param name="newGuild"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Guild> CreateGuild(Guild newGuild)
        {
            newGuild.CreationDate = DateTime.Now;
            return await _guildContext.CreateGuild(newGuild);
        }

    }
}