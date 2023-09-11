using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPwaApp.Shared.Constants
{
   public static class MessageConstants
   {
      public const string GenericError = "Something went wrong! Please try after sometime.";

      public const string DuplicateError = "Duplicate data found!";

      public const string NoMatchFoundError = "No match found!";

      public const string InvalidParameterError = "Invalid parameter(s)!";

      public const string RequiredFieldError = "Required.";

      public const string RecordSavedSuccessfully = "Record saved successfully!";

      public const string RecordUpdatedSuccessfully = "Record updated successfully!";

      public const string RecordDeletedSuccessfully = "Record deleted successfully!";

      public const string PasswordLengthError = "Password should be at least 6 characters!";

      public const string PasswordMatchError = "The Passwords do not match!";
   }
}