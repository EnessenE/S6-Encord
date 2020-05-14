using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Encord.ChannelService.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Used for transferring the message between the client and the server
        /// </summary>
        [NotMapped]
        public string ChannelId { get; set; }
    }
}
