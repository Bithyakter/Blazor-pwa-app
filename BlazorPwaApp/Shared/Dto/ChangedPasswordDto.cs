﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPwaApp.Shared.Dto
{
   public class ChangedPasswordDto
   {
      public string Username { get; set; }
      public string Password { get; set; }
      public string ConfirmPassword { get; set; }
   }
}