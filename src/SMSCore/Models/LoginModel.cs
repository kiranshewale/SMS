﻿using System.ComponentModel.DataAnnotations;

namespace SMSCore.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Name is required")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
