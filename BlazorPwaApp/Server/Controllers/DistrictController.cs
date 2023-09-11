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
   public class DistrictController : ControllerBase
   {
      private readonly DataContext _context;

      public DistrictController(DataContext context)
      {
         _context = context;
      }

      [HttpGet]
      public async Task<ActionResult<List<District>>> GetDistricts()
      {
         try
         {
            var districtInDb = await _context.Districts.Include(p => p.Province).Include(p => p.Province.Country).ToListAsync();
            return Ok(districtInDb);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpGet("provinces")]
      public async Task<ActionResult<List<Province>>> GetProvinces()
      {
         try
         {
            var provinceInDb = await _context.Provinces.ToListAsync();
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
      public async Task<ActionResult<District>> GetSingleDistricts(int id)
      {
         try
         {
            var districtInDb = await _context.Districts
             .Include(h => h.Province)
             .FirstOrDefaultAsync(h => h.Oid == id);

            if (districtInDb == null)
               //return NotFound("Sorry, no district here. :/");
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(districtInDb);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpPost]
      public async Task<ActionResult<List<District>>> CreateDistrict(District district)
      {
         try
         {
            district.Province = null;

            _context.Districts.Add(district);
            await _context.SaveChangesAsync();

            return Ok(await GetDbDistricts());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpPut("{id}")]
      public async Task<ActionResult<List<District>>> UpdateDistrict(District district, int id)
      {
         try
         {
            var districtInDb = await _context.Districts
             .Include(sh => sh.Province)
             .FirstOrDefaultAsync(sh => sh.Oid == id);

            if (districtInDb == null)
               //return NotFound("Sorry, but no district for you. :/");
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            districtInDb.DistrictName = district.DistrictName;
            districtInDb.ProvinceId = district.ProvinceId;

            await _context.SaveChangesAsync();

            return Ok(await GetDbDistricts());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpDelete("{id}")]
      public async Task<ActionResult<List<District>>> DeleteDistrict(int id)
      {
         try
         {
            var districtInDb = await _context.Districts
             .Include(p => p.Province)
             .FirstOrDefaultAsync(p => p.Oid == id);

            if (districtInDb == null)
               //return NotFound("Sorry, but no district for you. :/");
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            _context.Districts.Remove(districtInDb);
            await _context.SaveChangesAsync();

            return Ok(await GetDbDistricts());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      private async Task<List<District>> GetDbDistricts()
      {
         return await _context.Districts.Include(p => p.Province).ToListAsync();
      }
   }
}