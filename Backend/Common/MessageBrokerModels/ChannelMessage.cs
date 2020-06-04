using Encord.Common.Models;

namespace Encord.Common.MessageBrokerModels
{
    public class ChannelMessage
    {
        public bool Deletion { get; set; }
        public Guild Guild { get; set; }
    }
}
