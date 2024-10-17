using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace lab_infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartApiController : ControllerBase
    {
        private readonly CardsDeckShopWebContext _context;

        public ChartApiController(CardsDeckShopWebContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStockData()
        {
            var stockData = await _context.CardsDecks
                .Select(deck => new { deck.Name, deck.Stock })
                .ToListAsync();

            return Ok(stockData);  
        }
    }
}

