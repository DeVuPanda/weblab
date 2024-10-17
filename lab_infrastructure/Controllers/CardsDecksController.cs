using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab_domain.Model;
using lab_infrastructure;

namespace lab_infrastructure.Controllers
{
    public class CardsDecksController : Controller
    {
        private readonly CardsDeckShopWebContext _context;

        public CardsDecksController(CardsDeckShopWebContext context)
        {
            _context = context;
        }

        // GET: CardsDecks
        public async Task<IActionResult> Index()
        {
            var cardsDeckShopWebContext = _context.CardsDecks
                .Include(c => c.Brand)
                .Include(c => c.Type)
                .Include(c => c.CardTags)      
                .Include(c => c.Promotions)    
                .Include(c => c.Discounts);
            return View(await cardsDeckShopWebContext.ToListAsync());
        }

        // GET: CardsDecks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardsDeck = await _context.CardsDecks
                .Include(c => c.Brand)
                .Include(c => c.Type)
                .Include(c => c.CardTags)
                .Include(c => c.Promotions)
                .Include(c => c.Discounts)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cardsDeck == null)
            {
                return NotFound();
            }

            return View(cardsDeck);
        }

        // GET: CardsDecks/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["TypeId"] = new SelectList(_context.CardTypes, "Id", "TypeName");
            return View();
        }

        // POST: CardsDecks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BrandId,TypeId,ReleaseDate,Price,Stock,Description,Rating,ImageUrl")] CardsDeck cardsDeck)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardsDeck);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", cardsDeck.BrandId);
            ViewData["TypeId"] = new SelectList(_context.CardTypes, "Id", "TypeName", cardsDeck.TypeId);
            return View(cardsDeck);
        }

        // GET: CardsDecks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardsDeck = await _context.CardsDecks.FindAsync(id);
            if (cardsDeck == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", cardsDeck.BrandId);
            ViewData["TypeId"] = new SelectList(_context.CardTypes, "Id", "TypeName", cardsDeck.TypeId);
            return View(cardsDeck);
        }

        // POST: CardsDecks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BrandId,TypeId,ReleaseDate,Price,Stock,Description,Rating,ImageUrl")] CardsDeck cardsDeck)
        {
            if (id != cardsDeck.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardsDeck);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardsDeckExists(cardsDeck.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", cardsDeck.BrandId);
            ViewData["TypeId"] = new SelectList(_context.CardTypes, "Id", "TypeName", cardsDeck.TypeId);
            return View(cardsDeck);
        }

        // GET: CardsDecks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardsDeck = await _context.CardsDecks
                .Include(c => c.Brand)
                .Include(c => c.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cardsDeck == null)
            {
                return NotFound();
            }

            return View(cardsDeck);
        }

        // POST: CardsDecks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cardsDeck = await _context.CardsDecks.FindAsync(id);
            if (cardsDeck != null)
            {
                _context.CardsDecks.Remove(cardsDeck);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardsDeckExists(int id)
        {
            return _context.CardsDecks.Any(e => e.Id == id);
        }
    }
}
