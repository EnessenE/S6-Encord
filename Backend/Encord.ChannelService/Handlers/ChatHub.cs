using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        public async Task NewMessage(Message msg)
        {
            _logger.LogInformation("Message from {Name}", Context.User.GetName());
            msg.ClientId = Context.User.GetUserId();
            msg.Name = Context.User.GetName();
            await Clients.All.SendAsync("MessageReceived", msg);
        }


        public override Task OnConnectedAsync()
        {
            _logger.LogInformation("A new client has connected");
            return base.OnConnectedAsync();
        }
    }
}
