using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlazorPwaApp.Shared.Constants
{
   public static class Enums
   {
      public enum YesNo : byte
      {
         Yes = 1,

         No = 2
      }

      public enum Sex : byte
      {
         Male = 1,
         Female = 2
      }

      public enum UserType : byte
      {
         [Display(Name = "System Administrator")]
         SystemAdministrator = 1,

         [Display(Name = "Facility Administrator")]
         FacilityAdministrator = 2,

         Clinician = 3
      }
   }
}