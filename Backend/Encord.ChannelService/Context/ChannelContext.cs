using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Encord.ChannelService.Enums;
using Encord.ChannelService.Handlers;
using Encord.ChannelService.Interfaces;
using Encord.ChannelService.Models;
using Encord.Common.Configuration;
using Encord.Common.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        private readonly IHubContext<ChatHub> _chatHub;

        public DbSet<TextChannel> TextChannels { get; set; }
        public DbSet<VoiceChannel> VoiceChannels { get; set; }

        public ChannelContext(IOptions<SQLSettings> _SqlSettings, ILogger<ChannelContext> logger, IMapper mapper, IHubContext<ChatHub> chatHub)
        {
            _sqlSettings = _SqlSettings;
            _logger = logger;
            _mapper = mapper;
            _chatHub = chatHub;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_sqlSettings.Value.ConnectionString);
        }

        public TextChannel GetTextChannel(string id)
        {
            var result = TextChannels.Where(a => a.Id == id).Include(m => m.Messages);
            TextChannel channel = null;
            if (result.Any())
            {
                channel = result.First();
            }
            return channel;
        }

        public async Task AddMessageAsync(TextChannel channel, Message message)
        {
            channel = GetTextChannel(channel.Id);
            if (channel != null)
            {
                message.ChannelId = channel.Id;
                if (channel.Messages == null)
                {
                    channel.Messages = new List<Message>();
                }
                channel.Messages.Add(message);

                Add(message);
            } 
            await SaveChangesAsync();
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

        public async Task<bool> DeleteAllChannelsInGuildAsync(Guild guild)
        {
            _chatHub.Clients.All.SendAsync("GuildDeleted", guild);
            var text = TextChannels.Where(x => x.GuildID == guild.Id);

            if (text.Any())
            {
                foreach (var channel in text)
                {
                    RemoveChannel(channel);
                }
            }

            var voice = VoiceChannels.Where(x => x.GuildID == guild.Id);

            if (voice.Any())
            {
                foreach (var channel in voice)
                {
                    RemoveChannel(channel);
                }
            }

            await SaveChangesAsync();

            return true;
        }

        public async Task<Channel> CreateChannelAsync(Channel channel)
        {
            switch (channel.Type)
            {
                case ChannelType.TextChannel:
                    var text = _mapper.Map<TextChannel>(channel);
                    Add((object)text);
                    break;
                case ChannelType.VoiceChannel:
                    var voice = _mapper.Map<VoiceChannel>(channel);
                    Add((object)voice);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            await SaveChangesAsync();
            return channel; //GuildId is filled by entity
        }

        public async Task<bool> DeleteChannelAsync(Channel channel)
        {
            TextChannel textChannel = GetTextChannel(channel.Id);
            if (textChannel != null)
            {
                RemoveChannel(textChannel);
                await SaveChangesAsync();
                return true;
            }
            VoiceChannel voiceChannel = GetVoiceChannel(channel.Id);
            if (voiceChannel != null)
            {
                RemoveChannel(voiceChannel);
                await SaveChangesAsync();
                return true;
            }

            return false;
        }

        private async Task RemoveAllMessages(TextChannel channel)
        {
            if (channel.Messages != null)
            {
                _logger.LogInformation("About to clear {msg} messages from textchannel {channel}",
                    channel.Messages.Count, channel.Name);
                foreach (var message in channel.Messages)
                {
                    Remove(message);
                }
            }
        }

        private async Task RemoveChannel(TextChannel channel)
        {
            _chatHub.Clients.All.SendAsync("ChannelDeleted", channel);
            await RemoveAllMessages(channel);
            Remove(channel);
        }

        private void RemoveChannel(VoiceChannel channel)
        {
            _chatHub.Clients.All.SendAsync("ChannelDeleted", channel);
            Remove(channel);
        }

        private Channel getChannel(Channel channel)
        {
            Channel result = null;

            result = GetTextChannel(channel.Id);
            if (result == null)
            {
                result = GetVoiceChannel(channel.Id);
            }

            return result;
        }
    }
}
