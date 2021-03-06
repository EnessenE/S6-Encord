﻿using System.ComponentModel.DataAnnotations;

namespace Encord.AccountService.Models
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Token { get; set; }
    }
}
