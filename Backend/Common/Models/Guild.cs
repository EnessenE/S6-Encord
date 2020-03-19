using System;
using System.Collections.Generic;

namespace Encord.Common.Models
{
    public class Guild
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Account> Users { get; set; }
    }
}
