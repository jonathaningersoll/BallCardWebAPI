using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BBCards.Models;
using BBCards.Models.CardModels;

namespace BBCards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CardsController(AppDbContext context)
        {
            _context = context;
        }

        //// GET: api/Cards
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Card>>> GetCards()
        //{
        //    return await _context.Cards.ToListAsync();
        //}

        [HttpGet]
        public async Task<IEnumerable<CardListItem>> GetCards()
        {
            var cards = await _context.Cards.Select(c => new CardListItem
            {
                Id = c.Id,
                Player = c.Player,
                Team = c.Team
            }).ToListAsync();
            return cards;
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardDetail>> GetCard(int id)
        {
            var card = await _context.Cards
                .Include(c => c.Team)
                .Include(c => c.Player)
                .SingleAsync(c => c.Id == id);

            //var card = await _context.Cards
            //    .Where(c => c.Id == id)
            //    .Include(c => c.Player)
            //    .Include(c => c.Team)
            //    .SingleOrDefaultAsync();

            if (card == null)
            {
                return NotFound();
            }
            return new CardDetail
            {
                Id = card.Id,
                CardIdentifier = card.CardIdentifier,
                Feature = card.Feature,
                Position = card.Position,
                IsMainSet = card.IsMainSet,
                IsInsert = card.IsInsert,
                ParalellColor = card.ParalellColor,
                IsRookieCard = card.IsRookieCard,
                FlavorText = card.FlavorText,
                SetId = card.SetId,
                Player = card.Player,
                Team = card.Team
            };
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(int id, Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(Card card)
        {
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCard", new { id = card.Id }, card);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(int id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}
