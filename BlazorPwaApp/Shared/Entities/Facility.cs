using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPwaApp.Shared.Entities
{
   public class Facility
   {
      [Key]
      public int Oid { get; set; }

      [Required(ErrorMessage = "Required")]
      public string FacilityName { get; set; }

      [Required(ErrorMessage = "Required")]
      public string HMISCode { get; set; }

      public string? Longitude { get; set; }

      public string? Latitude { get; set; }

      public bool IsPrivateFacility { get; set; }

      public District? District { get; set; }
      public int DistrictId { get; set; }
   }
}