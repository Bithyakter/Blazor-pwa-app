using BlazorPwaApp.Shared.Entities;

namespace BlazorPwaApp.Client.Services.DistrictService
{
   public interface IDistrictService
   {
      List<District> Districts { get; set; }

      List<Province> Provinces { get; set; }

      Task GetProvinces();

      Task GetDistricts();

      Task<District> GetSingleDistrict(int id);

      Task CreateDistrict(District district);

      Task UpdateDistrict(District district);

      Task DeleteDistrict(int id);
   }
}