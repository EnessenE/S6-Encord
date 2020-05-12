using System;
using Microsoft.AspNetCore.Identity;

namespace Encord.AccountService.Models
{
    public class Account : IdentityUser
    {
        public string UserTag { get; set; }

        public DateTime JoinDate { get; set; }
    }
}
