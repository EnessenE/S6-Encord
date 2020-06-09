using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Encord.ChannelService.Interfaces;
using Encord.ChannelService.Models;
using Encord.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Encord.ChannelService.Handlers
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private IChannelContext _channelContext;

        public ChatHub(ILogger<ChatHub> logger, IChannelContext channelContext)
        {
            _logger = logger;
            _channelContext = channelContext;
        }

        public async Task NewMessage(Message msg)
        {
            _logger.LogInformation("Message from {Name}", Context.User.GetName());
            msg.ClientId = Context.User.GetUserId();
            msg.Name = Context.User.GetName();
            msg.CreatedAt = DateTime.Now;
            msg.LastUpdate = DateTime.Now;
            TextChannel channel = new TextChannel()
            {
                Id = msg.ChannelId
            };
            await _channelContext.AddMessageAsync(channel, msg);
            await Clients.All.SendAsync("MessageReceived", msg);
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogInformation("A new client has connected");
            return base.OnConnectedAsync();
        }
    }
}
