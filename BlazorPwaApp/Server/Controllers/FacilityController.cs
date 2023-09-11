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
   public class FacilityController : ControllerBase
   {
      private readonly DataContext _context;

      public FacilityController(DataContext context)
      {
         _context = context;
      }

      [HttpGet]
      public async Task<ActionResult<List<Facility>>> GetFacilities()
      {
         try
         {
            var facilityInDb = await _context.Facilities.Include(p => p.District).ThenInclude(d => d.Province).ThenInclude(p => p.Country).ToListAsync();
            return Ok(facilityInDb);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpGet("districts")]
      public async Task<ActionResult<List<District>>> GetDistricts()
      {
         try
         {
            var districtInDb = await _context.Districts.ToListAsync();
            return Ok(districtInDb);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<Facility>> GetSingleFacility(int id)
      {
         try
         {
            var facilityInDb = await _context.Facilities
             .Include(h => h.District)
             .FirstOrDefaultAsync(h => h.Oid == id);

            if (facilityInDb == null)
               //return NotFound("Sorry, no facility here. :/");
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(facilityInDb);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpPost]
      public async Task<ActionResult<List<Facility>>> CreateFacility(Facility facility)
      {
         try
         {
            facility.District = null;

            _context.Facilities.Add(facility);
            await _context.SaveChangesAsync();

            return Ok(await GetDbFacilities());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpPut("{id}")]
      public async Task<ActionResult<List<Facility>>> UpdateFacility(Facility facility, int id)
      {
         try
         {
            var facilityInDb = await _context.Facilities
             .Include(sh => sh.District)
             .FirstOrDefaultAsync(sh => sh.Oid == id);

            if (facilityInDb == null)
               //return NotFound("Sorry, but no facility for you. :/");
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            facilityInDb.FacilityName = facility.FacilityName;
            facilityInDb.HMISCode = facility.HMISCode;
            facilityInDb.Longitude = facility.Longitude;
            facilityInDb.Latitude = facility.Latitude;
            facilityInDb.IsPrivateFacility = facility.IsPrivateFacility;

            facilityInDb.DistrictId = facility.DistrictId;
            await _context.SaveChangesAsync();

            return Ok(await GetDbFacilities());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpDelete("{id}")]
      public async Task<ActionResult<List<Facility>>> DeleteFacility(int id)
      {
         try
         {
            var facilityInDb = await _context.Facilities
             .Include(p => p.District)
             .FirstOrDefaultAsync(p => p.Oid == id);

            if (facilityInDb == null)
               //return NotFound("Sorry, but no facility for you. :/");
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            _context.Facilities.Remove(facilityInDb);
            await _context.SaveChangesAsync();

            return Ok(await GetDbFacilities());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      private async Task<List<Facility>> GetDbFacilities()
      {
         return await _context.Facilities.Include(p => p.District).ToListAsync();
      }
   }
}