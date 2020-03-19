using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encord.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Encord.GuildService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GuildController : ControllerBase
    {
        public Guild GetGuild()
        {
            throw new NotImplementedException();
        }

        public Guild CreateGuild()
        {
            throw new NotImplementedException();
        }
    }
}