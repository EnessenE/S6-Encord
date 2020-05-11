using System.Collections.Generic;
using System.Linq;
using Encord.ChannelService.Interfaces;
using Encord.Common.Configuration;
using Encord.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Encord.ChannelService.Context
{
    public class ChannelContext : DbContext, IChannelContext
    {
        private readonly IOptions<SQLSettings> _sqlSettings;
        public DbSet<Channel> Channels { get; set; }

        public ChannelContext(IOptions<SQLSettings> _SqlSettings, ILogger<ChannelContext> logger)
        {
            _sqlSettings = _SqlSettings;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_sqlSettings.Value.ConnectionString);
        }

        public Channel GetChannel(string id)
        {
            Channel asset = Channels
                .OrderBy(b => b.Id)
                .First();
            return asset;
        }

        public List<Channel> GetAllChannelsInGuild(string guildId)
        {
            var x = Channels.Where(x => x.GuildID == guildId); 
            return x.ToList();

        }

        public Channel CreateChannel(Channel channel)
        {
            Add(channel);
            SaveChanges();
            return channel; //Id is filled by entity
        }

        public bool DeleteChannel(Channel channel)
        {
            Remove(channel);
            SaveChanges();
            return true;
        }
    }
}
