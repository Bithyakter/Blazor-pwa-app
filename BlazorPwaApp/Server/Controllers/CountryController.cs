using BlazorPwaApp.Server.AppDbContext;
using BlazorPwaApp.Shared.Constants;
using BlazorPwaApp.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorPwaApp.Server.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CountryController : ControllerBase
   {
      private readonly DataContext _context;

      public CountryController(DataContext context)
      {
         _context = context;
      }

      [HttpGet]
      public async Task<ActionResult<List<Country>>> GetCountries()
      {
         try
         {
            var countryInDb = await _context.Countries.ToListAsync();
            return Ok(countryInDb);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<Country>> GetSingleCountry(int id)
      {
         try
         {
            var countryInDb = await _context.Countries.FirstOrDefaultAsync(h => h.Oid == id);

            if (countryInDb == null)
               //return NotFound("Country not found. :/");
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(countryInDb);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }    
      }

      [HttpPost]
      public async Task<ActionResult<List<Country>>> CreateComic(Country country)
      {
         try
         {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return Ok(await GetDbCountries());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpPut("{id}")]
      public async Task<ActionResult<List<Country>>> UpdateCountry(Country country, int id)
      {
         try
         {
            var countryInDb = await _context.Countries
             .FirstOrDefaultAsync(sh => sh.Oid == id);

            if (countryInDb == null)
               //return NotFound("Country not found. :/");
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            countryInDb.CountryName = country.CountryName;

            await _context.SaveChangesAsync();

            return Ok(await GetDbCountries());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpDelete("{id}")]
      public async Task<ActionResult<List<Country>>> DeleteCountry(int id)
      {
         try
         {
            var countryInDb = await _context.Countries
             .FirstOrDefaultAsync(sh => sh.Oid == id);

            if (countryInDb == null)
               //return NotFound("Country not found. :/");
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            _context.Countries.Remove(countryInDb);
            await _context.SaveChangesAsync();

            return Ok(await GetDbCountries());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      private async Task<List<Country>> GetDbCountries()
      {
         return await _context.Countries.ToListAsync();
      }
   }
}