using BlazorPwaApp.Shared.Entities;

namespace BlazorPwaApp.Client.Services.FacilityService
{
   public interface IFacilityService
   {
      List<Country> Countries { get; set; }

      List<Province> Provinces { get; set; }   

      List<District> Districts { get; set; }

      List<Facility> Facilities { get; set; }

      Task GetCountries();

      Task GetProvinces();

      Task GetDistricts();

      Task<List<Facility>> GetFacilities();

      Task<Facility> GetSingleFacility(int id);

      Task CreateFacility(Facility facility);

      Task UpdateFacility(Facility facility);

      Task DeleteFacility(int id);
   }
}