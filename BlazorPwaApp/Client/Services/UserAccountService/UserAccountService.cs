using BlazorPwaApp.Shared.Constants;
using BlazorPwaApp.Shared.Dto;
using BlazorPwaApp.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using BlazorPwaApp.Client.Pages;

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
      //public async Task GetUserAccounts()
      //{
      //   var result = await _http.GetFromJsonAsync<List<UserAccount>>("api/userAccount");

      //   if (result != null)
      //      UserAccounts = result;
      //}

      public async Task<List<UserAccount>> GetUserAccounts()
      {
         var result = await _http.GetFromJsonAsync<List<UserAccount>>("api/userAccount");

         if (result != null)
         {
            UserAccounts = result;
            return result;
         }

         return new List<UserAccount>();
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

      //USER LOGIN
      public async Task<UserAccount> UserLogin(LoginDto login)
      {
         try
         {
            var response = await _http.PostAsJsonAsync("api/userAccount/userLogin", login);

            if (response.IsSuccessStatusCode)
            {
               return await response.Content.ReadFromJsonAsync<UserAccount>();
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
               throw new ApiException(MessageConstants.NoMatchFoundError);
            }
            else
            {
               throw new ApiException(MessageConstants.GenericError);
            }
         }
         catch (HttpRequestException ex)
         {
            // Handle any network-related errors
            throw new ApiException("Network Error: Unable to communicate with the server.", ex);
         }
      }
   }

   // ApiException.cs (Custom Exception)
   public class ApiException : Exception
   {
      public ApiException(string? message) : base(message)
      {
      }

      public ApiException(string message, HttpRequestException ex) : base(message)
      {
      }
   }
}