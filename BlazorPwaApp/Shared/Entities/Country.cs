using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPwaApp.Shared.Entities
{
   public class Country
   {
      [Key]
      public int Oid { get; set; }

      [Required(ErrorMessage = "The Name field is required.")]

      public string CountryName { get; set; } = string.Empty;
      public string CountryCode { get; set; } = string.Empty;
      public string ISOCode2 { get; set; } = string.Empty;
   }
}