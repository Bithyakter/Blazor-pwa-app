using BlazorPwaApp.Shared.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlazorPwaApp.Shared.Dto
{
   public class ChangedPasswordDto
   {
      public string Username { get; set; }

      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [DataType(DataType.Password)]
      public string NewPassword { get; set; }

      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [DataType(DataType.Password)]
      [Compare("NewPassword", ErrorMessage = MessageConstants.PasswordMatchError)]
      [Display(Name = "Confirm password")]
      public string ConfirmPassword { get; set; }
   }
}