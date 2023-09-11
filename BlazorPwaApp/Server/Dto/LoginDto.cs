﻿using System.ComponentModel.DataAnnotations;

namespace BlazorPwaApp.Server.Dto
{
   public class LoginDto
   {
      [Required]
      public string Username { get; set; }

      [Required]
      public string Password { get; set; }

      public string ConfirmPassword { get; set; }
   }
}