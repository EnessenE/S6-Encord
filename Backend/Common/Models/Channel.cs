using System;
using System.Collections.Generic;
using System.Text;
using Encord.Common.Enums;

namespace Encord.Common.Models
{
    public class Channel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ChannelType Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public string GuildID { get; set; }
    }
}
