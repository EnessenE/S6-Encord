using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encord.ChannelService.Models
{
    public class TextChannel: Channel
    {
        public List<Message> Messages { get; set; }
    }
}
