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
    public class CardTagsApiController : ControllerBase
    {
        private readonly CardsDeckShopWebContext _context;

        public CardTagsApiController(CardsDeckShopWebContext context)
        {
            _context = context;
        }

        // GET: api/CardTagsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardTag>>> GetCardTags()
        {
            return await _context.CardTags.ToListAsync();
        }

        // GET: api/CardTagsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardTag>> GetCardTag(int id)
        {
            var cardTag = await _context.CardTags.FindAsync(id);

            if (cardTag == null)
            {
                return NotFound();
            }

            return cardTag;
        }

        // PUT: api/CardTagsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardTag(int id, CardTag cardTag)
        {
            if (id != cardTag.Id)
            {
                return BadRequest();
            }

            _context.Entry(cardTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardTagExists(id))
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

        // POST: api/CardTagsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCardTag(CardTag cardTag)
        {
            try
            {
                _context.CardTags.Add(cardTag);
                await _context.SaveChangesAsync();

                return Ok(new { status = "Ok" });
            }
            catch (Exception)
            {
                return BadRequest(new { status = "Error" });
            }
        }


        // DELETE: api/CardTagsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardTag(int id)
        {
            var cardTag = await _context.CardTags.FindAsync(id);
            if (cardTag == null)
            {
                return NotFound();
            }

            _context.CardTags.Remove(cardTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardTagExists(int id)
        {
            return _context.CardTags.Any(e => e.Id == id);
        }
    }
}
