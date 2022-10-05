﻿using System.ComponentModel.DataAnnotations;

namespace Project.PL.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(4,ErrorMessage ="Minimum length of password is 4 chars")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
