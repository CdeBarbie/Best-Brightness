using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalBestBrightnessStore;
using FinalBestBrightnessStore.Models;

namespace FinalBestBrightnessStore.Controllers
{
    public class StockTrackingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockTrackingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StockTrackings
        public async Task<IActionResult> Index()
        {
            return View(await _context.StockTrack.ToListAsync());
        }

        // GET: StockTrackings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockTracking = await _context.StockTrack
                .FirstOrDefaultAsync(m => m.stockTrackId == id);
            if (stockTracking == null)
            {
                return NotFound();
            }

            return View(stockTracking);
        }

        // GET: StockTrackings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockTrackings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("stockTrackId,stockManId,prodId,dateOfstock")] StockTracking stockTracking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockTracking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockTracking);
        }

        // GET: StockTrackings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockTracking = await _context.StockTrack.FindAsync(id);
            if (stockTracking == null)
            {
                return NotFound();
            }
            return View(stockTracking);
        }

        // POST: StockTrackings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("stockTrackId,stockManId,prodId,dateOfstock")] StockTracking stockTracking)
        {
            if (id != stockTracking.stockTrackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockTracking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockTrackingExists(stockTracking.stockTrackId))
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
            return View(stockTracking);
        }

        // GET: StockTrackings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockTracking = await _context.StockTrack
                .FirstOrDefaultAsync(m => m.stockTrackId == id);
            if (stockTracking == null)
            {
                return NotFound();
            }

            return View(stockTracking);
        }

        // POST: StockTrackings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockTracking = await _context.StockTrack.FindAsync(id);
            if (stockTracking != null)
            {
                _context.StockTrack.Remove(stockTracking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockTrackingExists(int id)
        {
            return _context.StockTrack.Any(e => e.stockTrackId == id);
        }
    }
}
