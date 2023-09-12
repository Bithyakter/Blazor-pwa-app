using BlazorPwaApp.Shared.Entities;

namespace BlazorPwaApp.Client.Services.ProvinceService
{
   public interface IProvinceService
   {
      List<Province> Provinces { get; set; }

      List<Country> Countries { get; set; }

      Task GetCountries();

      //Task GetProvinces();

      Task<List<Province>> GetProvinces();

      Task<Province> GetSingleProvince(int id);

      Task CreateProvince(Province province);

      Task UpdateProvince(Province province);

      Task DeleteProvince(int id);
   }
}