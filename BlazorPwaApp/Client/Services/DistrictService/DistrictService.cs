using BlazorPwaApp.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorPwaApp.Client.Services.DistrictService
{
   public class DistrictService : IDistrictService
   {
      private readonly HttpClient _http;
      private readonly NavigationManager _navigationManager;

      public DistrictService(HttpClient http, NavigationManager navigationManager)
      {
         _http = http;
         _navigationManager = navigationManager;
      }

      public List<District> Districts { get; set; } = new List<District>();
      public List<Province> Provinces { get; set; } = new List<Province>();

      public async Task CreateDistrict(District district)
      {
         var result = await _http.PostAsJsonAsync("api/district", district);
         await SetDistricts(result);
      }

      private async Task SetDistricts(HttpResponseMessage result)
      {
         var response = await result.Content.ReadFromJsonAsync<List<District>>();
         Districts = response;
         _navigationManager.NavigateTo("districts");
      }

      public async Task DeleteDistrict(int id)
      {
         var result = await _http.DeleteAsync($"api/district/{id}");
         await SetDistricts(result);
      }

      public async Task GetProvinces()
      {
         var result = await _http.GetFromJsonAsync<List<Province>>("api/district/provinces");

         if (result != null)
            Provinces = result;
      }

      public async Task<District> GetSingleDistrict(int id)
      {
         var result = await _http.GetFromJsonAsync<District>($"api/district/{id}");

         if (result != null)
            return result;

         throw new Exception("District not found!");
      }

      public async Task GetDistricts()
      {
         var result = await _http.GetFromJsonAsync<List<District>>("api/district");

         if (result != null)
            Districts = result;
      }

      public async Task UpdateDistrict(District district)
      {
         var result = await _http.PutAsJsonAsync($"api/district/{district.Oid}", district);
         await SetDistricts(result);
      }
   }
}