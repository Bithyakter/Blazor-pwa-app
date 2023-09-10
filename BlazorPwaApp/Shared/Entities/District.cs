using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPwaApp.Shared.Entities
{
   public class District
   {
      [Key]
      public int Oid { get; set; }

      [Required(ErrorMessage = "Required.")]
      public string DistrictName { get; set; }

      public Province? Province { get; set; }
      public int ProvinceId { get; set; }
   }
}