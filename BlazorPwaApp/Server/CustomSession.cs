using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BlazorPwaApp.Server
{
   public interface ISessionService
   {
      ClaimsPrincipal User { get; }
      Task InitializeSessionAsync();
      Task SignInAsync(ClaimsPrincipal user);
      Task SignOutAsync();
   }

   public class CustomSession : ISessionService
   {
      private readonly IHttpContextAccessor _httpContextAccessor;

      public CustomSession(IHttpContextAccessor httpContextAccessor)
      {
         _httpContextAccessor = httpContextAccessor;
      }

      public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

      public async Task InitializeSessionAsync()
      {
         // Initialize session data if needed, e.g., load user data from a cookie
      }

      public async Task SignInAsync(ClaimsPrincipal user)
      {
         await _httpContextAccessor.HttpContext.SignInAsync(
             CookieAuthenticationDefaults.AuthenticationScheme,
             new ClaimsPrincipal(user.Identity));
      }

      public async Task SignOutAsync()
      {
         await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      }
   }
}
