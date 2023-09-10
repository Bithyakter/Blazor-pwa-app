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

      [Required(ErrorMessage = "Required.")]

      public string CountryName { get; set; }

      [Required(ErrorMessage = "Required")]
      public string CountryCode { get; set; } 

      [Required(ErrorMessage = "Required")]
      public string ISOCode2 { get; set; }
   }
}