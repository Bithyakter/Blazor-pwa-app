﻿using BlazorPwaApp.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorPwaApp.Client.Services.FacilityService
{
   public class FacilityService : IFacilityService
   {
      private readonly HttpClient _http;
      private readonly NavigationManager _navigationManager;

      public FacilityService(HttpClient http, NavigationManager navigationManager)
      {
         _http = http;
         _navigationManager = navigationManager;
      }

      public List<Facility> Facilities { get; set; } = new List<Facility>();
      public List<District> Districts { get; set; } = new List<District>();
      public List<Province> Provinces { get; set; } = new List<Province>();
      public List<Country> Countries { get; set; } = new List<Country>();

      public async Task CreateFacility(Facility facility)
      {
         var result = await _http.PostAsJsonAsync("api/facility", facility);
         await SetFacilities(result);
      }

      public async Task DeleteFacility(int id)
      {
         var result = await _http.DeleteAsync($"api/facility/{id}");
         await SetFacilities(result);
      }

      public async Task GetProvinces()
      {
         var result = await _http.GetFromJsonAsync<List<Province>>("api/province");

         if (result != null)
            Provinces = result;
      }

      public async Task GetCountries()
      {
         var result = await _http.GetFromJsonAsync<List<Country>>("api/country");

         if (result != null)
            Countries = result;
      }

      public async Task GetDistricts()
      {
         var result = await _http.GetFromJsonAsync<List<District>>("api/facility/districts");

         if (result != null)
            Districts = result;
      }

      public async Task<Facility> GetSingleFacility(int id)
      {
         var result = await _http.GetFromJsonAsync<Facility>($"api/facility/{id}");

         if (result != null)
            return result;

         throw new Exception("Facility not found!");
      }  

      public async Task<List<Facility>> GetFacilities()
      {
         var result = await _http.GetFromJsonAsync<List<Facility>>("api/facility");

         if (result != null)
         {
            Facilities = result;
            return result;
         }

         return new List<Facility>();
      }

      public async Task UpdateFacility(Facility facility)
      {
         var result = await _http.PutAsJsonAsync($"api/facility/{facility.Oid}", facility);
         await SetFacilities(result);
      }

      private async Task SetFacilities(HttpResponseMessage result)
      {
         var response = await result.Content.ReadFromJsonAsync<List<Facility>>();
         Facilities = response;
         _navigationManager.NavigateTo("facilities");
      }
   }
}