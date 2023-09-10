using BlazorPwaApp.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorPwaApp.Client.Services.CountryService
{
   public class CountryService : ICountryService
   {
      private readonly HttpClient _http;
      private readonly NavigationManager _navigationManager;

      public CountryService(HttpClient http, NavigationManager navigationManager)
      {
         _http = http;
         _navigationManager = navigationManager;
      }

      public List<Country> Countries { get; set; } = new List<Country>();
      public string ErrorMessage { get; set; }

      //CREATE
      public async Task CreateCountry(Country country)
      {
         if (country == null)
            throw new CountryNotFoundException();

         var result = await _http.PostAsJsonAsync("api/country", country);
         await SetCountries(result);
      }     

      //UPDATE
      public async Task UpdateCountry(Country country)
      {
         var result = await _http.PutAsJsonAsync($"api/country/{country.Oid}", country);
         await SetCountries(result);
      }

      //DELETE
      public async Task DeleteCountry(int id)
      {
         var result = await _http.DeleteAsync($"api/country/{id}");
         await SetCountries(result);
      }

      //GET SINGLE COUNTRY
      public async Task<Country> GetSingleCountry(int id)
      {
         var result = await _http.GetFromJsonAsync<Country>($"api/country/{id}");

         if (result != null)
            return result;

         throw new Exception("Country not found!");
      }

      //GET ALL COUNTRIES
      public async Task GetCountries()
      {
         var result = await _http.GetFromJsonAsync<List<Country>>("api/country");

         if (result != null)
            Countries = result;
      }

      //SET COUNTRY
      private async Task SetCountries(HttpResponseMessage result)
      {
         var response = await result.Content.ReadFromJsonAsync<List<Country>>();
         Countries = response;
         _navigationManager.NavigateTo("countries");
      }

      public class CountryNotFoundException : Exception
      {
         public CountryNotFoundException() : base("Country not found!") { }
      }    
   }
}