﻿using BlazorPwaApp.Shared.Entities;

namespace BlazorPwaApp.Client.Services.UserAccountService
{
   public interface IUserAccountService
   {
      List<UserAccount> UserAccounts { get; set; }

      Task GetUserAccounts();

      Task<UserAccount> GetUserAccountByKey(int id);

      Task CreateUserAccount(UserAccount userAccount);

      Task UpdateUserAccount(UserAccount userAccount);

      Task DeleteUserAccount(int id);
   }
}