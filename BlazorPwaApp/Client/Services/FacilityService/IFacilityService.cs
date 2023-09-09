using BlazorPwaApp.Shared.Entities;

namespace BlazorPwaApp.Client.Services.FacilityService
{
   public interface IFacilityService
   {
      List<Facility> Facilities { get; set; }

      List<District> Districts { get; set; }

      Task GetDistricts();

      Task GetFacilities();

      Task<Facility> GetSingleFacility(int id);

      Task CreateFacility(Facility facility);

      Task UpdateFacility(Facility facility);

      Task DeleteFacility(int id);
   }
}