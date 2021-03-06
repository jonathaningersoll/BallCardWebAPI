using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BBCards.Models;
using BBCards.Models.CardModels;
using BBCards.Models.SetModels;
using BBCards.Models.ManufacturerModels;

namespace BBCards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SetsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Sets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Set>>> GetSets()
        {
            return await _context.Sets.ToListAsync();
        }

        // GET: api/Sets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SetDetail>> GetSet(int id)
        {
            var cardSet = await _context.Sets
                .Include(s => s.Cards)
                .ThenInclude(c => c.Player)
                .Include(c => c.Cards)
                .ThenInclude(c => c.Team)
                .Include(s => s.Manufacturer)
                .SingleAsync(s => s.SetId == id);

            if (cardSet == null)
            {
                return NotFound();
            }
            return new SetDetail
            {
                SetId = cardSet.SetId,
                SetName = cardSet.SetName,
                SetYear = cardSet.SetYear,
                Cards = cardSet.Cards.Select(c => new CardListItem
                {
                    Id = c.Id,
                    CardIdentifier = c.CardIdentifier,
                    Player = c.Player,
                    Team = c.Team,
                }),
                Manufacturer = new ManufacturerListItem
                {
                    ManufacturerId = cardSet.Manufacturer.ManufacturerId,
                    ManufacturerName = cardSet.Manufacturer.ManufacturerName
                }
            };
        }

        // PUT: api/Sets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSet(int id, Set aSet)
        {
            if (id != aSet.SetId)
            {
                return BadRequest();
            }

            _context.Entry(aSet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetExists(id))
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

        // POST: api/Sets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Set>> PostSet(Set aSet)
        {
            _context.Sets.Add(aSet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSet", new { id = aSet.SetId }, aSet);
        }

        // DELETE: api/Sets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSet(int id)
        {
            var @set = await _context.Sets.FindAsync(id);
            if (@set == null)
            {
                return NotFound();
            }

            _context.Sets.Remove(@set);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SetExists(int id)
        {
            return _context.Sets.Any(e => e.SetId == id);
        }
    }
}
