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
    public class DiscountsController : Controller
    {
        private readonly CardsDeckShopWebContext _context;

        public DiscountsController(CardsDeckShopWebContext context)
        {
            _context = context;
        }

        // GET: Discounts
        public async Task<IActionResult> Index()
        {
            var cardsDeckShopWebContext = _context.Discounts.Include(d => d.Cardsdeck);
            return View(await cardsDeckShopWebContext.ToListAsync());
        }

        // GET: Discounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts
                .Include(d => d.Cardsdeck)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // GET: Discounts/Create
        public IActionResult Create()
        {
            ViewData["CardsdeckId"] = new SelectList(_context.CardsDecks, "Id", "Name");
            return View();
        }

        // POST: Discounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CardsdeckId,DiscountPercentage,StartDate,EndDate")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardsdeckId"] = new SelectList(_context.CardsDecks, "Id", "Name", discount.CardsdeckId);
            return View(discount);
        }

        // GET: Discounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }
            ViewData["CardsdeckId"] = new SelectList(_context.CardsDecks, "Id", "Name", discount.CardsdeckId);
            return View(discount);
        }

        // POST: Discounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CardsdeckId,DiscountPercentage,StartDate,EndDate")] Discount discount)
        {
            if (id != discount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountExists(discount.Id))
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
            ViewData["CardsdeckId"] = new SelectList(_context.CardsDecks, "Id", "Name", discount.CardsdeckId);
            return View(discount);
        }

        // GET: Discounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts
                .Include(d => d.Cardsdeck)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // POST: Discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount != null)
            {
                _context.Discounts.Remove(discount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscountExists(int id)
        {
            return _context.Discounts.Any(e => e.Id == id);
        }
    }
}
