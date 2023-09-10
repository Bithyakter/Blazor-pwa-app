using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPwaApp.Shared.Entities
{
   public class Province
   {
      [Key]
      public int Oid { get; set; }

      [Required(ErrorMessage = "Required")]

      public string ProvinceName { get; set; } = string.Empty;

      public Country? Country { get; set; }
      public int CountryId { get; set; }
   }
}