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
    public class SalesTrackingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesTrackingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SalesTrackings
        public async Task<IActionResult> Index()
        {
            return View(await _context.SaleTrack.ToListAsync());
        }

        // GET: SalesTrackings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesTracking = await _context.SaleTrack
                .FirstOrDefaultAsync(m => m.salesTrackId == id);
            if (salesTracking == null)
            {
                return NotFound();
            }

            return View(salesTracking);
        }

        // GET: SalesTrackings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalesTrackings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("salesTrackId,salePersonId,prodId,dateOfSale")] SalesTracking salesTracking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesTracking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesTracking);
        }

        // GET: SalesTrackings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesTracking = await _context.SaleTrack.FindAsync(id);
            if (salesTracking == null)
            {
                return NotFound();
            }
            return View(salesTracking);
        }

        // POST: SalesTrackings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("salesTrackId,salePersonId,prodId,dateOfSale")] SalesTracking salesTracking)
        {
            if (id != salesTracking.salesTrackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesTracking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesTrackingExists(salesTracking.salesTrackId))
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
            return View(salesTracking);
        }

        // GET: SalesTrackings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesTracking = await _context.SaleTrack
                .FirstOrDefaultAsync(m => m.salesTrackId == id);
            if (salesTracking == null)
            {
                return NotFound();
            }

            return View(salesTracking);
        }

        // POST: SalesTrackings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesTracking = await _context.SaleTrack.FindAsync(id);
            if (salesTracking != null)
            {
                _context.SaleTrack.Remove(salesTracking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesTrackingExists(int id)
        {
            return _context.SaleTrack.Any(e => e.salesTrackId == id);
        }
    }
}
