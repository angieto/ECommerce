using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Models;
// add these lines for validation
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
// add these lines for session
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        // Context
        private EContext dbContext;
        public HomeController(EContext context)
        {
            dbContext = context;
        }

        // ROUTES
        [HttpGet("/")]
        public IActionResult HomePage()
        {
            return RedirectToAction("Index");
        }

        [HttpGet("/dashboard")]
        public IActionResult Index()
        {
            List<Product> Products = dbContext.Products.OrderByDescending(p => p.CreatedAt).Take(4).ToList();
            List<Order> Orders = dbContext.Orders
                                .Include(o => o.Product)   
                                .Include(o => o.Customer)
                                .OrderByDescending(p => p.CreatedAt).Take(5).ToList();
            List<Customer> Customers = dbContext.Customers.OrderByDescending(p => p.CreatedAt).Take(5).ToList();
            ViewBag.Products = Products;
            ViewBag.Orders = Orders;
            ViewBag.Customers = Customers;
            return View("Dashboard");
        }
    }
}
