using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Encord.Common
{
    public class Guild
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public List<IdentityUser> Users { get; set; }
    }
}
