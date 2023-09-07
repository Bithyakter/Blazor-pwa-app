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
   public class ProvincesController : ControllerBase
   {
      private readonly DataContext _context;

      public ProvincesController(DataContext context)
      {
         _context = context;
      }

      [HttpGet]
      public async Task<ActionResult<List<Province>>> GetProvinces()
      {
         try
         {
            var provinceInDb = await _context.Provinces.Include(p => p.Country).ToListAsync();
            return Ok(provinceInDb);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpGet("countries")]
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
      public async Task<ActionResult<Province>> GetSingleProvince(int id)
      {
         try
         {
            var provinceInDb = await _context.Provinces
             .Include(h => h.Country)
             .FirstOrDefaultAsync(h => h.Oid == id);

            if (provinceInDb == null)
               //return NotFound("Sorry, no province here. :/");
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(provinceInDb);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpPost]
      public async Task<ActionResult<List<Province>>> CreateProvince(Province province)
      {
         try
         {
            province.Country = null;

            _context.Provinces.Add(province);
            await _context.SaveChangesAsync();

            return Ok(await GetDbProvinces());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpPut("{id}")]
      public async Task<ActionResult<List<Province>>> UpdateProvince(Province province, int id)
      {
         try
         {
            var provinceInDb = await _context.Provinces
             .Include(sh => sh.Country)
             .FirstOrDefaultAsync(sh => sh.Oid == id);

            if (provinceInDb == null)
               //return NotFound("Sorry, but no province for you. :/");
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            provinceInDb.ProvinceName = province.ProvinceName;
            provinceInDb.CountryId = province.CountryId;

            await _context.SaveChangesAsync();

            return Ok(await GetDbProvinces());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpDelete("{id}")]
      public async Task<ActionResult<List<Province>>> DeleteProvince(int id)
      {
         try
         {
            var provinceInDb = await _context.Provinces
             .Include(p => p.Country)
             .FirstOrDefaultAsync(p => p.Oid == id);

            if (provinceInDb == null)
               //return NotFound("Sorry, but no province for you. :/");
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            _context.Provinces.Remove(provinceInDb);
            await _context.SaveChangesAsync();

            return Ok(await GetDbProvinces());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      private async Task<List<Province>> GetDbProvinces()
      {
         return await _context.Provinces.Include(p => p.Country).ToListAsync();
      }
   }
}