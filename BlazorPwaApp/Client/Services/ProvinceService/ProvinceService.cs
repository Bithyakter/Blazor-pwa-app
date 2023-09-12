using BlazorPwaApp.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorPwaApp.Client.Services.ProvinceService
{
   public class ProvinceService : IProvinceService
   {
      private readonly HttpClient _http;
      private readonly NavigationManager _navigationManager;

      public ProvinceService(HttpClient http, NavigationManager navigationManager)
      {
         _http = http;
         _navigationManager = navigationManager;
      }

      public List<Province> Provinces { get; set; } = new List<Province>();
      public List<BlazorPwaApp.Shared.Entities.Country> Countries { get; set; } = new List<BlazorPwaApp.Shared.Entities.Country>();

      public async Task CreateProvince(Province province)
      {
         var result = await _http.PostAsJsonAsync("api/province", province);
         await SetProvinces(result);
      }

      private async Task SetProvinces(HttpResponseMessage result)
      {
         var response = await result.Content.ReadFromJsonAsync<List<Province>>();
         Provinces = response;
         _navigationManager.NavigateTo("provinces");
      }

      public async Task DeleteProvince(int id)
      {
         var result = await _http.DeleteAsync($"api/province/{id}");
         await SetProvinces(result);
      }

      public async Task GetCountries()
      {
         var result = await _http.GetFromJsonAsync<List<BlazorPwaApp.Shared.Entities.Country>>("api/province/countries");

         if (result != null)
            Countries = result;
      }

      public async Task<Province> GetSingleProvince(int id)
      {
         var result = await _http.GetFromJsonAsync<Province>($"api/province/{id}");

         if (result != null)
            return result;

         throw new Exception("Province not found!");
      }

      //public async Task GetProvinces()
      //{
      //   var result = await _http.GetFromJsonAsync<List<Province>>("api/province");

      //   if (result != null)
      //      Provinces = result;
      //}

      public async Task<List<Province>> GetProvinces()
      {
         var result = await _http.GetFromJsonAsync<List<Province>>("api/province");

         if (result != null)
         {
            Provinces = result;
            return result;
         }

         return new List<Province>();
      }

      public async Task UpdateProvince(Province province)
      {
         var result = await _http.PutAsJsonAsync($"api/province/{province.Oid}", province);
         await SetProvinces(result);
      }
   }
}