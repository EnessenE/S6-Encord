using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Encord.ChannelService.Enums;

namespace Encord.ChannelService.Models
{
    public class Channel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Name { get; set; }
        public ChannelType Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public string GuildID { get; set; }
    }
}
