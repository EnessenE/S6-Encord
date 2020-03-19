using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Encord.Common;
using Encord.Common.Configuration;
using Encord.Common.Extensions;
using Encord.Common.Models;
using Encord.GuildService.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Encord.GuildService.Context
{
    public class GuildContext : SqlDbContext, IGuildContext
    {
        public GuildContext(IOptions<SQLSettings> _SqlSettings, ILogger<SqlDbContext> logger) : base(_SqlSettings, logger)
        {
        }

        public async Task<Guild> GetGuild(string id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Id", id);
            var data = await GetDataAsync("GetGuildOnId", cmd);
            var result = data.Tables[0].Rows[0].ToGuild();
            return result;
        }

        public async Task<List<Guild>> GetAllGuilds()
        {
            SqlCommand cmd = new SqlCommand();
            var data = await GetDataAsync("GetallGuilds", cmd);
            var result = data.Tables[0].ToGuildList();
            return result;
        }

        public async Task<List<Guild>> GetUserGuilds(string userId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Id", userId);
            var data = await GetDataAsync("GetGuildOnUserId", cmd);
            var result = data.Tables[0].ToGuildList();
            return result;
        }

        public async Task<Guild> CreateGuild(Guild guild)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@Name", guild.Name);
            command.Parameters.AddWithValue("@CreationDate", guild.CreationDate);

            var ds = await GetDataAsync("CreateGuild", command);
            var result = new Guild();
            if (ds.HasData())
            {
                result = ds.Tables[0].Rows[0].ToGuild();
            }

            return result;
        }
    }
}
