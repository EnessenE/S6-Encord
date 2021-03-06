﻿using System.ComponentModel.DataAnnotations;

namespace Encord.AccountService.Models
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
