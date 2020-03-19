using System;
using System.Collections.Generic;
using Encord.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Encord.Common
{
    public class Guild
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Account> Users { get; set; }
    }
}
