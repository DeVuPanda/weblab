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
    public class PromotionsApiController : ControllerBase
    {
        private readonly CardsDeckShopWebContext _context;

        public PromotionsApiController(CardsDeckShopWebContext context)
        {
            _context = context;
        }

        // GET: api/PromotionsApi
        [HttpGet]
        public async Task<ActionResult> GetPromotions([FromQuery] int skip = 0, [FromQuery] int limit = 2)
        {
            var totalCount = await _context.Promotions.CountAsync();

            var promotions = await _context.Promotions
                .Skip(skip)
                .Take(limit)
                .ToListAsync();

            string nextLink = null;
            if (skip + limit < totalCount)
            {
                nextLink = Url.Action("GetPromotions", new { skip = skip + limit, limit });
            }

            return Ok(new { Items = promotions, NextLink = nextLink });
        }

        // GET: api/PromotionsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Promotion>> GetPromotion(int id)
        {
            var promotion = await _context.Promotions.FindAsync(id);

            if (promotion == null)
            {
                return NotFound();
            }

            return promotion;
        }

        // PUT: api/PromotionsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPromotion(int id, Promotion promotion)
        {
            if (id != promotion.Id)
            {
                return BadRequest();
            }

            _context.Entry(promotion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromotionExists(id))
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

        // POST: api/PromotionsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostPromotion(Promotion promotion)
        {
            try
            {
                _context.Promotions.Add(promotion);
                await _context.SaveChangesAsync();

                return Ok(new { status = "Ok" });
            }
            catch (Exception)
            {
                return BadRequest(new { status = "Error" });
            }
        }


        // DELETE: api/PromotionsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            var promotion = await _context.Promotions.FindAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }

            _context.Promotions.Remove(promotion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PromotionExists(int id)
        {
            return _context.Promotions.Any(e => e.Id == id);
        }
    }
}
