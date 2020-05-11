using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Encord.Common.Enums;

namespace Encord.Common.Models
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
