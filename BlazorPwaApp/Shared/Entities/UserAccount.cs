using BlazorPwaApp.Shared.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlazorPwaApp.Shared.Constants.Enums;

namespace BlazorPwaApp.Shared.Entities
{
   public class UserAccount
   {
      [Key]
      public int Oid { get; set; }

      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      public string FirstName { get; set; }

      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      public string Lastname { get; set; }

      public DateTime? DOB { get; set; }

      public Sex Sex { get; set; }

      public string Username { get; set; }

      [Required(ErrorMessage = MessageConstants.PasswordLengthError)]
      //[RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$",
      //      ErrorMessage = "Password should have minimum 8 characters, at least 1 uppercase letter, 1 lowercase letter and 1 number.")]
      [Display(Name = "Password")]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      [NotMapped]
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Compare("Password", ErrorMessage = MessageConstants.PasswordMatchError)]
      [Display(Name = "Confirm password")]
      [DataType(DataType.Password)]
      public string ConfirmPassword { get; set; }
   }
}

//[Key]
//public int Oid { get; set; }

//[Required(ErrorMessage = MessageConstants.RequiredFieldError)]
//[StringLength(60)]
//[Display(Name = "First name")]
//public string FirstName { get; set; }

//[Required(ErrorMessage = MessageConstants.RequiredFieldError)]
//[StringLength(60)]
//[Display(Name = "last name")]
//public string Lastname { get; set; }

//[Required(ErrorMessage = MessageConstants.RequiredFieldError)]
//[DataType(DataType.DateTime)]
//[Column(TypeName = "smalldatetime")]
//[Display(Name = "Date of birth")]
//public DateTime DOB { get; set; }

//public Sex Sex { get; set; }

//[Required(ErrorMessage = MessageConstants.RequiredFieldError)]
//[StringLength(4)]
//[Display(Name = "Country code")]
//public string CountryCode { get; set; }

//[Required(ErrorMessage = MessageConstants.RequiredFieldError)]
//[StringLength(11)]
//[Display(Name = "Cellphone number")]
//public string Cellphone { get; set; }

//public string Designation { get; set; }

//public UserType UserType { get; set; }

//[Required(ErrorMessage = MessageConstants.RequiredFieldError)]
//[StringLength(30)]
//[Display(Name = "Username")]
//public string Username { get; set; }

//[Required(ErrorMessage = MessageConstants.PasswordLengthError)]
//[Display(Name = "Password")]
//[DataType(DataType.Password)]
//public string Password { get; set; }

//[NotMapped]
//[Required(ErrorMessage = MessageConstants.RequiredFieldError)]
//[Compare("Password", ErrorMessage = MessageConstants.PasswordMatchError)]
//[Display(Name = "Confirm password")]
//[DataType(DataType.Password)]
//public string ConfirmPassword { get; set; }