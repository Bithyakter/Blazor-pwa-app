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

      [Required(ErrorMessage = "The Name field is required.")]

      public string FacilityName { get; set; } = string.Empty;
      public string HMISCode { get; set; } = string.Empty;
      public string Longitude { get; set; } = string.Empty;
      public string Latitude { get; set; } = string.Empty;
      public string IsPrivateFacility { get; set; } = string.Empty;
      public District? District { get; set; }
      public int DistrictId { get; set; }
   }
}