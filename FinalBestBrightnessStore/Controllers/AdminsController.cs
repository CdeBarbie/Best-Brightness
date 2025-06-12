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
    public class AdminsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AdminDashboard()
        {
            var products = await _context.Products.ToListAsync();
            var categories = products.Select(p => p.Category).Distinct().ToList();

            var model = new AdminDashboard
            {
                ProductCount = products.Count,
                TotalIncome = Math.Round(_context.Finances.Sum(f => f.amount), 2),
                SalesPersonCount = _context.SalesPersons.Count(),
                StockManagerCount = _context.StockManangers.Count(),
                CategoryCount = categories.Count,
                Categories = categories,
                ProductsInCategoryCount = 0 // default value
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AdminDashboard(AdminDashboard model)
        {
            var products = await _context.Products.ToListAsync();
            var categories = products.Select(p => p.Category).Distinct().ToList();

            model.ProductCount = products.Count;
            model.TotalIncome = Math.Round(_context.Finances.Sum(f => f.amount), 2);
            model.SalesPersonCount = _context.SalesPersons.Count();
            model.StockManagerCount = _context.StockManangers.Count();
            model.CategoryCount = categories.Count;
            model.Categories = categories;
            model.ProductsInCategoryCount = products.Count(p => p.Category == model.SelectedCategory);

            return View(model);
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            return View(await _context.Admins.ToListAsync());
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.adminId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("adminId,name,surname,dateOfbirth,address,phone,email,password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("adminId,name,surname,dateOfbirth,address,phone,email,password")] Admin admin)
        {
            if (id != admin.adminId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.adminId))
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
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.adminId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.adminId == id);
        }
    }
}
