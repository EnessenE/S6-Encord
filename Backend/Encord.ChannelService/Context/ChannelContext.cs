using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Encord.ChannelService.Enums;
using Encord.ChannelService.Interfaces;
using Encord.ChannelService.Models;
using Encord.Common.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Encord.ChannelService.Context
{
    public class ChannelContext : DbContext, IChannelContext
    {
        private readonly IOptions<SQLSettings> _sqlSettings;
        private readonly ILogger<ChannelContext> _logger;
        private readonly IMapper _mapper;

        public DbSet<TextChannel> TextChannels { get; set; }
        public DbSet<VoiceChannel> VoiceChannels { get; set; }

        public ChannelContext(IOptions<SQLSettings> _SqlSettings, ILogger<ChannelContext> logger, IMapper mapper)
        {
            _sqlSettings = _SqlSettings;
            _logger = logger;
            _mapper = mapper;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_sqlSettings.Value.ConnectionString);
        }

        public TextChannel GetTextChannel(string id)
        {
            var result = TextChannels.Where(a => a.Id == id);
            TextChannel channel = null;
            if (result.Any())
            {
                channel = result.First();
            }
            return channel;
        }

        public void AddMessage(TextChannel channel, Message message)
        {
            channel = GetTextChannel(channel.Id);
            if (channel != null)
            {
                if (channel.Messages == null)
                {
                    channel.Messages = new List<Message>();
                }

                channel.Messages.Add(message);
                SaveChanges();
            }
        }

        public VoiceChannel GetVoiceChannel(string id)
        {
            var result = VoiceChannels.Where(a => a.Id == id);
            VoiceChannel channel = null;
            if (result.Any())
            {
                channel = result.First();
            }
            return channel;
        }

        public List<Channel> GetAllChannelsInGuild(string guildId)
        {
            List<Channel> channels = new List<Channel>();
            var text = TextChannels.Where(x => x.GuildID == guildId);
            var voice = VoiceChannels.Where(x => x.GuildID == guildId);
            channels.AddRange(text.ToList());
            channels.AddRange(voice.ToList());
            return channels;

        }

        public bool DeleteAllChannelsInGuild(string guildId)
        {
            var text = TextChannels.Where(x => x.GuildID == guildId);

            if (text.Any())
            {
                foreach (var channel in text)
                {
                    Remove(channel);
                }
            }

            var voice = VoiceChannels.Where(x => x.GuildID == guildId);

            if (voice.Any())
            {
                foreach (var channel in voice)
                {
                    Remove(channel);
                }
            }
            
            SaveChanges();
            return true;
        }

        public Channel CreateChannel(Channel channel)
        {
            switch (channel.Type)
            {
                case ChannelType.TextChannel:
                    var text = _mapper.Map<TextChannel>(channel);
                    Add((object) text);
                    break;
                case ChannelType.VoiceChannel:
                    var voice = _mapper.Map<VoiceChannel>(channel);
                    Add((object) voice);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SaveChanges();
            return channel; //GuildId is filled by entity
        }

        public bool DeleteChannel(Channel channel)
        {
            Remove(channel);
            SaveChanges();
            return true;
        }
    }
}
