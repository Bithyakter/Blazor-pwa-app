using BlazorPwaApp.Shared.Entities;

namespace BlazorPwaApp.Client.Services.DistrictService
{
   public interface IDistrictService
   {
      List<District> Districts { get; set; }

      List<Province> Provinces { get; set; }

      List<Country> Countries { get; set; }

      Task<List<District>> GetDistricts();

      Task GetProvinces();     

      Task GetCountries();

      Task<District> GetSingleDistrict(int id);

      Task CreateDistrict(District district);

      Task UpdateDistrict(District district);

      Task DeleteDistrict(int id);
   }
}