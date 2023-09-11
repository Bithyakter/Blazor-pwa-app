using BlazorPwaApp.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorPwaApp.Client.Services.UserAccountService
{
   public class UserAccountService : IUserAccountService
   {

      private readonly HttpClient _http;
      private readonly NavigationManager _navigationManager;

      public UserAccountService(HttpClient http, NavigationManager navigationManager)
      {
         _http = http;
         _navigationManager = navigationManager;
      }

      public List<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
      public string ErrorMessage { get; set; }

      //CREATE
      public async Task CreateUserAccount(UserAccount userAccount)
      {
         if (userAccount == null)
            throw new UserAccountNotFoundException();

         var result = await _http.PostAsJsonAsync("api/userAccount", userAccount);
         await SetUserAccounts(result);
      }

      //UPDATE
      public async Task UpdateUserAccount(UserAccount userAccount)
      {
         var result = await _http.PutAsJsonAsync($"api/userAccount/{userAccount.Oid}", userAccount);
         await SetUserAccounts(result);
      }

      //DELETE
      public async Task DeleteUserAccount(int id)
      {
         var result = await _http.DeleteAsync($"api/userAccount/{id}");
         await SetUserAccounts(result);
      }

      //GET SINGLE USER ACCOUNT
      public async Task<UserAccount> GetUserAccountByKey(int id)
      {
         var result = await _http.GetFromJsonAsync<UserAccount>($"api/userAccount/{id}");

         if (result != null)
            return result;

         throw new Exception("UserAccount not found!");
      }

      //GET ALL USER ACCOUNTS
      public async Task GetUserAccounts()
      {
         var result = await _http.GetFromJsonAsync<List<UserAccount>>("api/userAccount");

         if (result != null)
            UserAccounts = result;
      }

      //SET USER ACCOUNT
      private async Task SetUserAccounts(HttpResponseMessage result)
      {
         var response = await result.Content.ReadFromJsonAsync<List<UserAccount>>();
         UserAccounts = response;
         _navigationManager.NavigateTo("userAccounts");
      }

      public class UserAccountNotFoundException : Exception
      {
         public UserAccountNotFoundException() : base("UserAccount not found!") { }
      }
   }
}