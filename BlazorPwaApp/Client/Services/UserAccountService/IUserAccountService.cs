using BlazorPwaApp.Shared.Dto;
using BlazorPwaApp.Shared.Entities;

namespace BlazorPwaApp.Client.Services.UserAccountService
{
   public interface IUserAccountService
   {
      List<UserAccount> UserAccounts { get; set; }
      
      Task<List<UserAccount>> GetUserAccounts();

      Task<UserAccount> GetUserAccountByKey(int id);

      Task CreateUserAccount(UserAccount userAccount);

      Task UpdateUserAccount(UserAccount userAccount);

      Task DeleteUserAccount(int id);

      Task<UserAccount> UserLogin(LoginDto login);

      Task ChangePassword(ChangedPasswordDto changedPassword);
   }
}