using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Encord.Common.Models
{
    public class Account : IdentityUser
    {
        public string UserTag { get; set; }

        public DateTime JoinDate { get; set; }
        public int test { get; set; }
    }
}
