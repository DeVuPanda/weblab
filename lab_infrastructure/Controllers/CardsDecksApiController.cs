using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab_domain.Model;
using lab_infrastructure;

namespace lab_infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsDecksApiController : ControllerBase
    {
        private readonly CardsDeckShopWebContext _context;

        public CardsDecksApiController(CardsDeckShopWebContext context)
        {
            _context = context;
        }

        // GET: api/CardsDecksApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardsDeck>>> GetCardsDecks()
        {
            return await _context.CardsDecks.ToListAsync();
        }

        // GET: api/CardsDecksApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardsDeck>> GetCardsDeck(int id)
        {
            var cardsDeck = await _context.CardsDecks.FindAsync(id);

            if (cardsDeck == null)
            {
                return NotFound();
            }

            return cardsDeck;
        }

        // PUT: api/CardsDecksApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardsDeck(int id, CardsDeck cardsDeck)
        {
            if (id != cardsDeck.Id)
            {
                return BadRequest();
            }

            _context.Entry(cardsDeck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardsDeckExists(id))
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

        // POST: api/CardsDecksApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCardsDeck(CardsDeck cardsDeck)
        {
            try
            {
                _context.CardsDecks.Add(cardsDeck);
                await _context.SaveChangesAsync();

                return Ok(new { status = "Ok" });
            }
            catch (Exception)
            {
                return BadRequest(new { status = "Error" });
            }
        }


        // DELETE: api/CardsDecksApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardsDeck(int id)
        {
            var cardsDeck = await _context.CardsDecks.FindAsync(id);
            if (cardsDeck == null)
            {
                return NotFound();
            }

            _context.CardsDecks.Remove(cardsDeck);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardsDeckExists(int id)
        {
            return _context.CardsDecks.Any(e => e.Id == id);
        }
    }
}
