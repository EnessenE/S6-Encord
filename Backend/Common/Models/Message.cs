using System;
using System.Collections.Generic;
using System.Text;

namespace Encord.Common.Models
{
    public class Message
    {
        public string Content { get; set; }

        public DateTime TimeSent { get; set; }

        public DateTime LastEdit { get; set; }
    }
}
