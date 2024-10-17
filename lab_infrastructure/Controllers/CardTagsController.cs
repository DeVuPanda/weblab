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
    public class CardTagsController : Controller
    {
        private readonly CardsDeckShopWebContext _context;

        public CardTagsController(CardsDeckShopWebContext context)
        {
            _context = context;
        }

        // GET: CardTags
        public async Task<IActionResult> Index()
        {
            var cardsDeckShopWebContext = _context.CardTags.Include(c => c.Card);
            return View(await cardsDeckShopWebContext.ToListAsync());
        }

        // GET: CardTags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardTag = await _context.CardTags
                .Include(c => c.Card)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cardTag == null)
            {
                return NotFound();
            }

            return View(cardTag);
        }

        // GET: CardTags/Create
        public IActionResult Create()
        {
            ViewData["CardId"] = new SelectList(_context.CardsDecks, "Id", "Name");
            return View();
        }

        // POST: CardTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CardId,Tag")] CardTag cardTag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardId"] = new SelectList(_context.CardsDecks, "Id", "Name", cardTag.CardId);
            return View(cardTag);
        }

        // GET: CardTags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardTag = await _context.CardTags.FindAsync(id);
            if (cardTag == null)
            {
                return NotFound();
            }
            ViewData["CardId"] = new SelectList(_context.CardsDecks, "Id", "Name", cardTag.CardId);
            return View(cardTag);
        }

        // POST: CardTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CardId,Tag")] CardTag cardTag)
        {
            if (id != cardTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardTagExists(cardTag.Id))
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
            ViewData["CardId"] = new SelectList(_context.CardsDecks, "Id", "Name", cardTag.CardId);
            return View(cardTag);
        }

        // GET: CardTags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardTag = await _context.CardTags
                .Include(c => c.Card)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cardTag == null)
            {
                return NotFound();
            }

            return View(cardTag);
        }

        // POST: CardTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cardTag = await _context.CardTags.FindAsync(id);
            if (cardTag != null)
            {
                _context.CardTags.Remove(cardTag);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardTagExists(int id)
        {
            return _context.CardTags.Any(e => e.Id == id);
        }
    }
}
