using DinkToPdf;
using DinkToPdf.Contracts;
//using DinkToPdf.Native.Chrome;
using FinalBestBrightnessStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalBestBrightnessStore.Controllers
{
    public class ReportsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Report()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetSalesReport(string period)
        {
            DateTime startDate = DateTime.Now;
            switch (period)
            {
                case "daily":
                    startDate = DateTime.Today;
                    break;
                case "weekly":
                    startDate = DateTime.Today.AddDays(-7);
                    break;
                case "monthly":
                    startDate = DateTime.Today.AddMonths(-1);
                    break;
            }

            var salesReport = _context.CustomerOrders
                .Where(o => o.DateOfOrder >= startDate)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Select(o => new
                {
                    o.Id,
                    SalesPersonName = _context.SalesPersons.FirstOrDefault(sp => sp.salePersonId == o.SalePersonId).name,
                    o.DateOfOrder,
                    OrderItems = o.OrderItems.Select(oi => new
                    {
                        oi.Quantity,
                        oi.Price
                    })
                })
                .ToList();

            return Json(salesReport);

            /* var salesReport = new List<CustomerOrder>();

             switch (period.ToLower())
             {
                 case "daily":
                     salesReport = _context.CustomerOrders
                         .Include(o => o.OrderItems)
                         .ThenInclude(oi => oi.Product)
                         .Where(o => o.DateOfOrder.Date == DateTime.Today)
                         .ToList();
                     break;
                 case "weekly":
                     var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                     salesReport = _context.CustomerOrders
                         .Include(o => o.OrderItems)
                         .ThenInclude(oi => oi.Product)
                         .Where(o => o.DateOfOrder.Date >= startOfWeek && o.DateOfOrder.Date <= DateTime.Today)
                         .ToList();
                     break;
                 case "monthly":
                     salesReport = _context.CustomerOrders
                         .Include(o => o.OrderItems)
                         .ThenInclude(oi => oi.Product)
                         .Where(o => o.DateOfOrder.Month == DateTime.Today.Month && o.DateOfOrder.Year == DateTime.Today.Year)
                         .ToList();
                     break;
             }

             // Fixed the property name and join condition
             var result = salesReport.Select(order => new {
                 order.Id,
                 SalesPersonName = _context.SalesPersons.FirstOrDefault(u => u.salePersonId == order.SalePersonId)?.name, // Fixed matching field
                 order.DateOfOrder,
                 OrderItems = order.OrderItems.Select(item => new {
                     item.Quantity,
                     item.Price
                 })
             }).ToList();

             return Json(result);*/
        }

        // Added SaleSlip method to return view for order details
        public IActionResult SaleSlip(int orderId)
        {
            /*var order = _context.CustomerOrders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);*/
            var order = _context.CustomerOrders
           .Include(o => o.OrderItems)
           .ThenInclude(oi => oi.Product)
           .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            var salesperson = _context.SalesPersons.FirstOrDefault(s => s.salePersonId == order.SalePersonId);

            var viewModel = new SaleSlipViewModel
            {
                Order = order,
                SalesPersonName = salesperson?.name,
                SalesPersonId = salesperson?.salePersonId,
                TotalAmount = order.OrderItems.Sum(item => item.Quantity * item.Price)
            };

            return View(viewModel);
        }
    }
}
