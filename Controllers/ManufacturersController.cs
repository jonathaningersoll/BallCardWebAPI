using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BBCards.Models;
using BBCards.Models.ManufacturerModels;
using BBCards.Models.SetModels;

namespace BBCards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ManufacturersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Manufacturers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManufacturerListItem>>> GetManufacturer()
        {
            var result = await _context.Manufacturer.Select(m => new ManufacturerListItem
            {
                ManufacturerId = m.ManufacturerId,
                ManufacturerName = m.ManufacturerName
            }).ToListAsync();

            return result;
        }

        // GET: api/Manufacturers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ManufacturerDetail>> GetManufacturer(int id)
        {
            var manufacturer = await _context.Manufacturer
                .Include(m => m.Sets)
                .SingleAsync(m => m.ManufacturerId == id);
            
            if (manufacturer == null)
            {
                return NotFound();
            }

            return new ManufacturerDetail
            {
                ManufacturerId = manufacturer.ManufacturerId,
                ManufacturerName = manufacturer.ManufacturerName,
                Sets = manufacturer.Sets.Select(m => new SetListItem
                {
                    SetId = m.SetId,
                    SetName = m.SetName,
                    SetYear = m.SetYear
                })
            };
        }

        // PUT: api/Manufacturers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManufacturer(int id, Manufacturer manufacturer)
        {
            if (id != manufacturer.ManufacturerId)
            {
                return BadRequest();
            }

            _context.Entry(manufacturer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerExists(id))
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

        // POST: api/Manufacturers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Manufacturer>> PostManufacturer(Manufacturer manufacturer)
        {
            _context.Manufacturer.Add(manufacturer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetManufacturer", new { id = manufacturer.ManufacturerId }, manufacturer);
        }

        // DELETE: api/Manufacturers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManufacturer(int id)
        {
            var manufacturer = await _context.Manufacturer.FindAsync(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            _context.Manufacturer.Remove(manufacturer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ManufacturerExists(int id)
        {
            return _context.Manufacturer.Any(e => e.ManufacturerId == id);
        }
    }
}
