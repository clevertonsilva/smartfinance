﻿namespace smartfinance.Domain.Models.Authentication.Model
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ResetToken { get; set; }
    }
}
