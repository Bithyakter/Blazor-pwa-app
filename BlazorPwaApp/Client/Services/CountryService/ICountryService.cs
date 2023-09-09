using BlazorPwaApp.Shared.Entities;

namespace BlazorPwaApp.Client.Services.CountryService
{
   public interface ICountryService
   {
      List<Country> Countries { get; set; }

      Task GetCountries();

      Task<Country> GetSingleCountry(int id);

      Task CreateCountry(Country country);

      Task UpdateCountry(Country country);

      Task DeleteCountry(int id);
   }
}