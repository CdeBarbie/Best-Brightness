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
    public class StockManangersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockManangersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult StockManDashboard()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // GET: StockManangers
        public async Task<IActionResult> Index()
        {
            return View(await _context.StockManangers.ToListAsync());
        }

        // GET: StockManangers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockMananger = await _context.StockManangers
                .FirstOrDefaultAsync(m => m.stockManId == id);
            if (stockMananger == null)
            {
                return NotFound();
            }

            return View(stockMananger);
        }

        // GET: StockManangers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockManangers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("stockManId,name,surname,dateOfbirth,address,phone,email,password")] StockMananger stockMananger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockMananger);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockMananger);
        }

        // GET: StockManangers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockMananger = await _context.StockManangers.FindAsync(id);
            if (stockMananger == null)
            {
                return NotFound();
            }
            return View(stockMananger);
        }

        // POST: StockManangers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("stockManId,name,surname,dateOfbirth,address,phone,email,password")] StockMananger stockMananger)
        {
            if (id != stockMananger.stockManId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockMananger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockManangerExists(stockMananger.stockManId))
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
            return View(stockMananger);
        }

        // GET: StockManangers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockMananger = await _context.StockManangers
                .FirstOrDefaultAsync(m => m.stockManId == id);
            if (stockMananger == null)
            {
                return NotFound();
            }

            return View(stockMananger);
        }

        // POST: StockManangers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockMananger = await _context.StockManangers.FindAsync(id);
            if (stockMananger != null)
            {
                _context.StockManangers.Remove(stockMananger);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockManangerExists(int id)
        {
            return _context.StockManangers.Any(e => e.stockManId == id);
        }
    }
}
