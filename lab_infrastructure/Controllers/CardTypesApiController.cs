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
    public class CardTypesApiController : ControllerBase
    {
        private readonly CardsDeckShopWebContext _context;

        public CardTypesApiController(CardsDeckShopWebContext context)
        {
            _context = context;
        }

        // GET: api/CardTypesApi
        [HttpGet]
        public async Task<ActionResult> GetCardTypes([FromQuery] int skip = 0, [FromQuery] int limit = 2)
        {
            var totalCount = await _context.CardTypes.CountAsync();

            var cardTypes = await _context.CardTypes
                .Skip(skip)
                .Take(limit)
                .ToListAsync();

            string nextLink = null;
            if (skip + limit < totalCount)
            {
                nextLink = Url.Action("GetCardTypes", new { skip = skip + limit, limit });
            }

            return Ok(new { Items = cardTypes, NextLink = nextLink });
        }

        // GET: api/CardTypesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardType>> GetCardType(int id)
        {
            var cardType = await _context.CardTypes.FindAsync(id);

            if (cardType == null)
            {
                return NotFound();
            }

            return cardType;
        }

        // PUT: api/CardTypesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardType(int id, CardType cardType)
        {
            if (id != cardType.Id)
            {
                return BadRequest();
            }

            _context.Entry(cardType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardTypeExists(id))
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

        // POST: api/CardTypesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCardType(CardType cardType)
        {
            try
            {
                _context.CardTypes.Add(cardType);
                await _context.SaveChangesAsync();

                return Ok(new { status = "Ok" });
            }
            catch (Exception)
            {
                return BadRequest(new { status = "Error" });
            }
        }


        // DELETE: api/CardTypesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardType(int id)
        {
            var cardType = await _context.CardTypes.FindAsync(id);
            if (cardType == null)
            {
                return NotFound();
            }

            _context.CardTypes.Remove(cardType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardTypeExists(int id)
        {
            return _context.CardTypes.Any(e => e.Id == id);
        }
    }
}
