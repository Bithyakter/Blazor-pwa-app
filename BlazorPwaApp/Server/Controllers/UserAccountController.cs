﻿using BlazorPwaApp.Server.AppDbContext;
using BlazorPwaApp.Server.Dto;
using BlazorPwaApp.Shared.Constants;
using BlazorPwaApp.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utilities.Encryptions;

namespace BlazorPwaApp.Server.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class UserAccountController : ControllerBase
   {
      private readonly DataContext _context;
      private UserAccount user;

      public UserAccountController(DataContext context)
      {
         _context = context;
      }

      [HttpPost]
      public async Task<ActionResult<List<UserAccount>>> CreateUserAccount(UserAccount userAccount)
      {
         try
         {
            _context.UserAccounts.Add(userAccount);
            await _context.SaveChangesAsync();

            return Ok(await GetDbUserAccounts());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpGet]
      public async Task<ActionResult<List<UserAccount>>> GetUserAccounts()
      {
         try
         {
            var userAccountInDb = await _context.UserAccounts.ToListAsync();
            return Ok(userAccountInDb);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<UserAccount>> GetUserAccountByKey(int id)
      {
         try
         {
            var userAccountInDb = await _context.UserAccounts.FirstOrDefaultAsync(u => u.Oid == id);

            if (userAccountInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(userAccountInDb);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpPut("{id}")]
      public async Task<ActionResult<List<UserAccount>>> UpdateCountry(UserAccount userAccount, int id)
      {
         try
         {
            var userAccountInDb = await _context.UserAccounts.FirstOrDefaultAsync(sh => sh.Oid == id);

            if (userAccountInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            userAccountInDb.Username = userAccount.Username;
            userAccountInDb.FirstName = userAccount.FirstName;
            userAccountInDb.Lastname = userAccount.Lastname;
            userAccountInDb.DOB = userAccount.DOB;
            userAccountInDb.Sex = userAccount.Sex;
            userAccountInDb.Password = userAccount.Password;

            await _context.SaveChangesAsync();

            return Ok(await GetDbUserAccounts());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpDelete("{id}")]
      public async Task<ActionResult<List<UserAccount>>> DeleteUserAccount(int id)
      {
         try
         {
            var userAccountInDb = await _context.UserAccounts.FirstOrDefaultAsync(sh => sh.Oid == id);

            if (userAccountInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            _context.UserAccounts.Remove(userAccountInDb);
            await _context.SaveChangesAsync();

            return Ok(await GetDbUserAccounts());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      //[HttpPost]
      //public async Task<IActionResult> Login(LoginDto login)
      //{
      //   try
      //   {

      //   }
      //   catch (Exception ex)
      //   {
      //      return BadRequest(ex.Message);
      //   }
      //}

      //[HttpPost("login")]
      //public async Task<IActionResult> Login(LoginDto login)
      //{
      //   try
      //   {
      //      // Find the user by their username
      //      var user = await _userManager.FindByNameAsync(login.Username);

      //      if (user == null)
      //      {
      //         return BadRequest("Invalid login attempt.");
      //      }

      //      // Check if the provided password is valid
      //      var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, lockoutOnFailure: false);

      //      if (result.Succeeded)
      //      {
      //         // Generate an access token or return a success message
      //         // You can use JWT authentication or another method to generate tokens

      //         // For example, using JWT:
      //         var token = GenerateJwtToken(user);

      //         return Ok(new
      //         {
      //            Message = "Login successful",
      //            Token = token
      //         });
      //      }

      //      if (result.IsLockedOut)
      //      {
      //         return BadRequest("User account locked out.");
      //      }
      //      else
      //      {
      //         return BadRequest("Invalid login attempt.");
      //      }
      //   }
      //   catch (Exception ex)
      //   {
      //      return BadRequest(ex.Message);
      //   }
      //}

      private async Task<List<UserAccount>> GetDbUserAccounts()
      {
         return await _context.UserAccounts.ToListAsync();
      }

      public UserAccount? GetUserByUserName(string UserName)
      {
         try
         {
            var user = _context.UserAccounts.AsNoTracking().FirstOrDefault(x => x.Username == UserName);
            return user;
         }
         catch (Exception)
         {
            throw;
         }
      }

      //private string GenerateJwtToken(UserAccount user)
      //{
      //   var claims = new List<Claim>
      //    {
      //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
      //        new Claim(ClaimTypes.Name, user.UserName),
      //        // Add more claims as needed
      //    };

      //   var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"]));
      //   var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      //   var token = new JwtSecurityToken(
      //       issuer: Configuration["JwtIssuer"],
      //       audience: Configuration["JwtIssuer"],
      //       claims: claims,
      //       expires: DateTime.UtcNow.AddMinutes(30), // Set the token expiration time
      //       signingCredentials: creds
      //   );

      //   return new JwtSecurityTokenHandler().WriteToken(token);
      //}

      //[HttpPost]
      //public async Task<ActionResult<UserAccount>> CreateUserAccount(UserAccount userAccount)
      //{
      //   try
      //   {
      //      EncryptionHelpers encryptionHelpers = new EncryptionHelpers();
      //      string encryptedPassword = encryptionHelpers.Encrypt(userAccount.Password);
      //      userAccount.Password = encryptedPassword;

      //      _context.UserAccounts.Add(userAccount);
      //      await _context.SaveChangesAsync();

      //      return Ok(userAccount);
      //   }
      //   catch (Exception ex)
      //   {
      //      return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
      //   }
      //}

      //[HttpPost("login")]
      //public async Task<IActionResult> Login([FromBody] LoginDto model)
      //{
      //   var result = await _context.UserAccounts.FindAsync(model.Username, model.Password, false);

      //   if (result != null)
      //   {
      //      return Ok(new { Message = "Login successful" });
      //   }

      //   return BadRequest(new { Message = "Login failed" });
      //}

      //UserAccount? GetUserByuserNameAndpassword(string UserName, string Password);

      //UserAccount? GetUserByuserName(string UserName);      

      //UserAccount IsLogin(int FacilitiesId, string userName, string password);
   }
}