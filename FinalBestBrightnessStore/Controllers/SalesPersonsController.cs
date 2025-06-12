using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalBestBrightnessStore;
using FinalBestBrightnessStore.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace FinalBestBrightnessStore.Controllers
{
    public class SalesPersonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesPersonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> SalespersonDashboard()
        {
            var products = await _context.Products.ToListAsync();

            var viewModel = new SalespersonDashboard
            {
                Products = products,
                CartItems = new List<CartItem>(),
                TotalPrice = 0
            };

            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetString("UserRole");

            if (userId == null || userRole != "SalesPerson")
            {
                return RedirectToAction("Index", "Login");
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOrder([FromBody] List<OrderItem> orderItems)
        {
            if (orderItems == null || !orderItems.Any())
            {
                return BadRequest("No products in the order.");
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return BadRequest("Invalid user ID.");
            }

            var salesPerson = await _context.SalesPersons.FindAsync(userId);
            if (salesPerson == null)
            {
                return NotFound("SalesPerson not found");
            }

            var customerOrder = new CustomerOrder
            {
                SalePersonId = userId.Value,
                DateOfOrder = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in orderItems)
            {
                var product = await _context.Products.FindAsync(item.prodId);
                if (product == null || product.quantinty < item.Quantity)
                {
                    return BadRequest("Invalid product or insufficient quantity.");
                }

                product.quantinty -= item.Quantity;

                var orderItem = new OrderItem
                {
                    prodId = item.prodId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Product = product,
                    CustomerOrder = customerOrder,
                    // Assign the ProductName here
                    ProductName = product.productName
                };

                customerOrder.OrderItems.Add(orderItem);
            }

            await _context.CustomerOrders.AddAsync(customerOrder);

            decimal totalAmount = orderItems.Sum(i => i.Quantity * i.Price);

            var finance = new Finance
            {
                dateOfSale = DateTime.Now,
                salePersonId = salesPerson.salePersonId,
                amount = totalAmount
            };

            await _context.Finances.AddAsync(finance);
            await _context.SaveChangesAsync();

            return Ok(new { orderId = customerOrder.Id });
        }

        public async Task<IActionResult> SalesSlip(int orderId)
        {
            var order = await _context.CustomerOrders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            var salesPerson = await _context.SalesPersons.FindAsync(order.SalePersonId);

            var totalAmount = order.OrderItems.Sum(i => i.Quantity * i.Price);
            var amountPaid = 0m; // Fetch or calculate this value based on your logic
            var change = amountPaid - totalAmount;

            var viewModel = new SalesSlipViewModel
            {
                SalesPersonName = salesPerson.name,
                SalesPersonId = salesPerson.salePersonId,
                OrderId = order.Id,
                DateOfSale = order.DateOfOrder,
                Order = order,
                TotalAmount = totalAmount,
                AmountPaid = amountPaid,
                Change = change,
                Items = order.OrderItems.Select(oi => new OrderItem
                {
                    ProductName = oi.Product.productName,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };

            return View(viewModel);
        }




        //niyasebenza you are the first to bere
        /*/[HttpPost]
        public async Task<IActionResult> ConfirmOrder([FromBody] List<OrderItem> orderItems)
        {
            if (orderItems == null || !orderItems.Any())
            {
                return BadRequest("No products in the order.");
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return BadRequest("Invalid user ID.");
            }

            var salesPerson = await _context.SalesPersons.FindAsync(userId);
            if (salesPerson == null)
            {
                return NotFound("SalesPerson not found");
            }

            var customerOrder = new CustomerOrder
            {
                SalePersonId = userId.Value,
                DateOfOrder = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in orderItems)
            {
                var product = await _context.Products.FindAsync(item.prodId);
                if (product == null || product.quantinty < item.Quantity)
                {
                    return BadRequest("Invalid product or insufficient quantity.");
                }

                product.quantinty -= item.Quantity;

                var orderItem = new OrderItem
                {
                    prodId = item.prodId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Product = product,
                    CustomerOrder = customerOrder
                };

                customerOrder.OrderItems.Add(orderItem);
            }

            await _context.CustomerOrders.AddAsync(customerOrder);

            decimal totalAmount = orderItems.Sum(i => i.Quantity * i.Price);

            var finance = new Finance
            {
                dateOfSale = DateTime.Now,
                salePersonId = salesPerson.salePersonId,
                amount = totalAmount
            };

            await _context.Finances.AddAsync(finance);
            await _context.SaveChangesAsync();

            return Ok(new { orderId = customerOrder.Id });
        }

        public async Task<IActionResult> SalesSlip(int orderId)
        {
            var order = await _context.CustomerOrders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            var salesPerson = await _context.SalesPersons.FindAsync(order.SalePersonId);

            var totalAmount = order.OrderItems.Sum(i => i.Quantity * i.Price);
            var amountPaid = 0m; // Fetch or calculate this value based on your logic
            var change = amountPaid - totalAmount;

            var viewModel = new SalesSlipViewModel
            {
                SalesPersonName = salesPerson.name,
                SalesPersonId = salesPerson.salePersonId,
                Order = order,
                TotalAmount = totalAmount,
                AmountPaid = amountPaid,
                Change = change
            };

            return View(viewModel);
        }*/

        public IActionResult ViewBarcode()
        {
            return View();
        }

        // GET: SalesPersons
        public async Task<IActionResult> Index()
        {
            return View(await _context.SalesPersons.ToListAsync());
        }

        // GET: SalesPersons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPerson = await _context.SalesPersons
                .FirstOrDefaultAsync(m => m.salePersonId == id);
            if (salesPerson == null)
            {
                return NotFound();
            }

            return View(salesPerson);
        }

        // GET: SalesPersons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalesPersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("salePersonId,name,surname,dateOfbirth,address,phone,email,password")] SalesPerson salesPerson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesPerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesPerson);
        }

        // GET: SalesPersons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPerson = await _context.SalesPersons.FindAsync(id);
            if (salesPerson == null)
            {
                return NotFound();
            }
            return View(salesPerson);
        }

        // POST: SalesPersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("salePersonId,name,surname,dateOfbirth,address,phone,email,password")] SalesPerson salesPerson)
        {
            if (id != salesPerson.salePersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesPerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesPersonExists(salesPerson.salePersonId))
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
            return View(salesPerson);
        }

        // GET: SalesPersons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPerson = await _context.SalesPersons
                .FirstOrDefaultAsync(m => m.salePersonId == id);
            if (salesPerson == null)
            {
                return NotFound();
            }

            return View(salesPerson);
        }

        // POST: SalesPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesPerson = await _context.SalesPersons.FindAsync(id);
            if (salesPerson != null)
            {
                _context.SalesPersons.Remove(salesPerson);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesPersonExists(int id)
        {
            return _context.SalesPersons.Any(e => e.salePersonId == id);
        }
    }
}
