using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encord.ChannelService.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Encord.ChannelService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private IHubContext<ChatHub> _hub;

        public ChatController(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var timerManager =_hub.Clients.All.SendAsync("transferchartdata", "SUCCESS!");

            return Ok(new { Message = "Request Completed" });
        }
    }
}