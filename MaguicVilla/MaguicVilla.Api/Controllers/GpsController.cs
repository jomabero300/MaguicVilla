using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MaguicVilla.Api.Data;
using MaguicVilla.Api.Models;
using MaguicVilla.Api.Models.Dto;
using MaguicVilla.Api.Repository.IRepositories;
using System.Net;

namespace MaguicVilla.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GpsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IGpsRepository _gps;

        public GpsController(ApplicationDbContext context, IGpsRepository gps)
        {
            _context = context;
            _gps = gps;
        }

        // GET: api/Gps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gps>>> GetGps()
        {
          if (_context.Gps == null)
          {
              return NotFound();
          }
            return await _context.Gps.ToListAsync();
        }

        // GET: api/Gps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gps>> GetGps(int id)
        {
          if (_context.Gps == null)
          {
              return NotFound();
          }
            var gps = await _context.Gps.FindAsync(id);

            if (gps == null)
            {
                return NotFound();
            }

            return gps;
        }

        // PUT: api/Gps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGps(int id, Gps gps)
        {
            if (id != gps.Id)
            {
                return BadRequest();
            }

            _context.Entry(gps).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GpsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Gps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Gps>> PostGps(Gps gps)
        {
          if (_context.Gps == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Gps'  is null.");
          }

            gps.Keyaceso = Guid.NewGuid().ToString().ToUpper().Replace('-', 'A');

            _context.Gps.Add(gps);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGps", new { id = gps.Id }, gps);
        }

        // DELETE: api/Gps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGps(int id)
        {
            if (_context.Gps == null)
            {
                return NotFound();
            }
            var gps = await _context.Gps.FindAsync(id);
            if (gps == null)
            {
                return NotFound();
            }

            _context.Gps.Remove(gps);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{Keyaceso},{Latitud},{Longitud}")]
        
        public async Task<string> RegisgroGps(string Keyaceso, string Latitud, string? Longitud)
        {

            Gps gpsModel = _context.Gps.Where(X => X.Keyaceso == Keyaceso).FirstOrDefault();

            if (gpsModel == null)
            {
                return "Datos invalidos";
            }

            GpsTrasabilidad models = new()
            {
                Gps = gpsModel,
                Latitud = Latitud,
                Longitud = Longitud,
            };

            await _gps.Create(models);

            return "Ok";
        }

        private bool GpsExists(int id)
        {
            return (_context.Gps?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
