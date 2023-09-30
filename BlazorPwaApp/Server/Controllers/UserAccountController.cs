using BlazorPwaApp.Server.AppDbContext;
using BlazorPwaApp.Shared.Constants;
using BlazorPwaApp.Shared.Dto;
using BlazorPwaApp.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

      #region Create
      [HttpPost]
      public async Task<ActionResult<List<UserAccount>>> CreateUserAccount(UserAccount userAccount)
      {
         try
         {
            EncryptionHelpers encryptionHelpers = new EncryptionHelpers();
            string encryptedPassword = encryptionHelpers.Encrypt(userAccount.Password);
            userAccount.Password = encryptedPassword;

            _context.UserAccounts.Add(userAccount);
            await _context.SaveChangesAsync();

            return Ok(await GetDbUserAccounts());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }
      #endregion

      #region Get UserAccounts
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
      #endregion

      #region Update
      [HttpPut("{id}")]
      public async Task<ActionResult<List<UserAccount>>> UpdateUserAccount(UserAccount userAccount, int id)
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
      #endregion

      #region Delete
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
      #endregion

      #region Login
      [HttpPost("userLogin")]
      public async Task<IActionResult> UserLogin(LoginDto login)
      {
         try
         {
            EncryptionHelpers encryptionHelpers = new EncryptionHelpers();
            string encryptedPassword = encryptionHelpers.Encrypt(login.Password);

            var userInDb = await _context.UserAccounts.SingleOrDefaultAsync(u => u.Username == login.Username && u.Password == encryptedPassword);

            if (userInDb != null)
               return Ok(userInDb);
            else
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }
      #endregion 

      #region Change Password
      [HttpPost("change-password")]
      public async Task<IActionResult> ChangedPassword(ChangedPasswordDto changePassword)
      {
         try
         {
            user = await _context.UserAccounts.SingleOrDefaultAsync(u => u.Username.ToLower().Trim() == changePassword.Username.ToLower().Trim());

            EncryptionHelpers encryptionHelpers = new EncryptionHelpers();
            string encryptedOldPassword = encryptionHelpers.Encrypt(changePassword.Password);

            if (user.Password != encryptedOldPassword)
               return BadRequest(MessageConstants.OldPasswordMatchError);

            string encryptedPassword = encryptionHelpers.Encrypt(changePassword.NewPassword);
            user.Password = encryptedPassword;

            _context.UserAccounts.Update(user);
            await _context.SaveChangesAsync();

            return Ok(user);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }
      #endregion

      #region Read All
      private async Task<List<UserAccount>> GetDbUserAccounts()
      {
         return await _context.UserAccounts.ToListAsync();
      }

      [HttpGet("IsAccountDuplicate/{username}")]
      public async Task<ActionResult<bool>> IsAccountDuplicate(string username)
      {
         try
         {
            var userAccountInDb = await _context.UserAccounts.SingleOrDefaultAsync(u => u.Username == username);

            if (userAccountInDb != null)
               return true;

            return false;
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError);
         }
      }
      #endregion

      //[HttpPost("userLogin")]
      //public async Task<IActionResult> UserLogin(LoginDto login)
      //{
      //   try
      //   {
      //      EncryptionHelpers encryptionHelpers = new EncryptionHelpers();
      //      string encryptedPassword = encryptionHelpers.Encrypt(login.Password);

      //      var userInDb = await _context.UserAccounts.SingleOrDefaultAsync(u => u.Username == login.Username && u.Password == encryptedPassword);

      //      if (userInDb != null)
      //      {
      //         var token = GenerateJwtToken(userInDb.Username);
      //         return Ok(new { token });
      //      }
      //      else
      //      {
      //         return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);
      //      }
      //   }
      //   catch (Exception ex)
      //   {
      //      return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
      //   }
      //}

      //private string GenerateJwtToken(string username, IConfiguration configuration)
      //{
      //   var claims = new[]
      //   {
      //  new Claim(ClaimTypes.Name, username),
      //  // Add more claims as needed
      //};

      //   var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]));
      //   var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      //   var token = new JwtSecurityToken(
      //       issuer: configuration["JwtSettings:Issuer"],
      //       audience: configuration["JwtSettings:Audience"],
      //       claims: claims,
      //       expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["JwtSettings:ExpirationInMinutes"])),
      //       signingCredentials: creds
      //   );

      //   return new JwtSecurityTokenHandler().WriteToken(token);
      //}
   }
}

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