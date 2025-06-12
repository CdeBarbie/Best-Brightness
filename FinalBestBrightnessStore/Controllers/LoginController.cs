using FinalBestBrightnessStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalBestBrightnessStore.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login model)
        {
            if (ModelState.IsValid)
            {
                string role = model.UserRole;
                bool isValidUser = false;
                int userId = 0;
                string userName = string.Empty;

                if (role == "Admin")
                {
                    var admin = _context.Admins.FirstOrDefault(a => a.email == model.Email && a.password == model.Password);
                    isValidUser = admin != null;
                    if (isValidUser)
                    {
                        userId = admin.adminId;
                        userName = admin.name; // Assuming the Admin model has a name property
                        HttpContext.Session.SetString("UserRole", "Admin");
                    }
                }
                else if (role == "SalesPerson")
                {
                    var salesPerson = _context.SalesPersons.FirstOrDefault(sp => sp.email == model.Email && sp.password == model.Password);
                    isValidUser = salesPerson != null;
                    if (isValidUser)
                    {
                        userId = salesPerson.salePersonId;
                        userName = salesPerson.name; // Assuming the SalesPerson model has a name property
                        HttpContext.Session.SetString("UserRole", "SalesPerson");
                        HttpContext.Session.SetString("SalespersonName", userName); // Setting the SalespersonName in session
                    }
                }
                else if (role == "StockManager")
                {
                    var stockManager = _context.StockManangers.FirstOrDefault(sm => sm.email == model.Email && sm.password == model.Password);
                    isValidUser = stockManager != null;
                    if (isValidUser)
                    {
                        userId = stockManager.stockManId;
                        userName = stockManager.name; // Assuming the StockManager model has a name property
                        HttpContext.Session.SetString("UserRole", "StockManager");
                    }
                }

                if (isValidUser)
                {
                    HttpContext.Session.SetInt32("UserId", userId);
                    HttpContext.Session.SetString("UserName", userName); // Set the userName in session
                    return RedirectToDashboard(role);
                }
                else
                {
                    // Incorrect email or password
                    ViewBag.ErrorMessage = "Incorrect email or password.";
                }
            }

            return View(model);
        }

        private IActionResult RedirectToDashboard(string role)
        {
            if (role == "Admin")
            {
                return RedirectToAction("AdminDashboard", "Admins");
            }
            else if (role == "SalesPerson")
            {
                return RedirectToAction("SalespersonDashboard", "SalesPersons");
            }
            else if (role == "StockManager")
            {
                return RedirectToAction("StockManDashboard", "StockManangers");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        //Working but does not display username
        /*[HttpPost]
        public IActionResult Index(Login model)
        {
            if (ModelState.IsValid)
            {
                string role = model.UserRole;
                bool isValidUser = false;
                int userId = 0;

                if (role == "Admin")
                {
                    var admin = _context.Admins.FirstOrDefault(a => a.email == model.Email && a.password == model.Password);
                    isValidUser = admin != null;
                    if (isValidUser)
                    {
                        userId = admin.adminId;
                        HttpContext.Session.SetString("UserRole", "Admin");
                    }
                }
                else if (role == "SalesPerson")
                {
                    var salesPerson = _context.SalesPersons.FirstOrDefault(sp => sp.email == model.Email && sp.password == model.Password);
                    isValidUser = salesPerson != null;
                    if (isValidUser)
                    {
                        userId = salesPerson.salePersonId;
                        HttpContext.Session.SetString("UserRole", "SalesPerson");
                    }
                }
                else if (role == "StockManager")
                {
                    var stockManager = _context.StockManangers.FirstOrDefault(sm => sm.email == model.Email && sm.password == model.Password);
                    isValidUser = stockManager != null;
                    if (isValidUser)
                    {
                        userId = stockManager.stockManId;
                        HttpContext.Session.SetString("UserRole", "StockManager");
                    }
                }

                if (isValidUser)
                {
                    HttpContext.Session.SetInt32("UserId", userId);
                    return RedirectToDashboard(role);
                }

                else
                {
                    // Incorrect email or password
                    ViewBag.ErrorMessage = "Incorrect email or password.";
                }
            }

            return View(model);
        }

        private IActionResult RedirectToDashboard(string role)
        {
            if (role == "Admin")
            {
                return RedirectToAction("AdminDashboard", "Admins");
            }
            else if (role == "SalesPerson")
            {
                return RedirectToAction("SalespersonDashboard", "SalesPersons");
            }
            else if (role == "StockManager")
            {
                return RedirectToAction("StockManDashboard", "StockManangers");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }*/

    }
}
