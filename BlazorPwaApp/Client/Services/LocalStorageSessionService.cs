using Microsoft.JSInterop;
using System.Text.Json;

namespace BlazorPwaApp.Client.Services
{
   public interface ISessionService
   {
      Task SetItemAsync<T>(string key, T value);
      Task<T?> GetItemAsync<T>(string key);
      Task RemoveItemAsync(string key);
   }

   public class LocalStorageSessionService : ISessionService
   {
      private readonly IJSRuntime _jsRuntime;

      public LocalStorageSessionService(IJSRuntime jsRuntime)
      {
         _jsRuntime = jsRuntime;
      }

      public async Task SetItemAsync<T>(string key, T value)
      {
         var jsonString = JsonSerializer.Serialize(value);
         await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, jsonString);
      }

      public async Task<T?> GetItemAsync<T>(string key)
      {
         var jsonString = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
         return string.IsNullOrEmpty(jsonString)
             ? default(T)
             : JsonSerializer.Deserialize<T>(jsonString);
      }

      public async Task RemoveItemAsync(string key)
      {
         await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
      }
   }
}