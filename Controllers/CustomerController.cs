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
    public class CustomerController : Controller
    {
        // Context
        private EContext dbContext;
        public CustomerController(EContext context)
        {
            dbContext = context;
        }

        // ROUTES
        [HttpGet("/customers")]
        public IActionResult Customer()
        {
            var AllCustomers = dbContext.Customers.OrderByDescending(c => c.CreatedAt).ToList();
            ViewBag.AllCustomers = AllCustomers;
            return View("Customer");
        }

        [HttpPost("/customers/new")]
        public IActionResult AddCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                // if customer has already existed, display error
                if (dbContext.Customers.Any(c => c.Name == customer.Name)) 
                {
                    ModelState.AddModelError("Name", "Customer already exists!");
                    return View("Customer");
                }
                dbContext.Add(customer);
                dbContext.SaveChanges();
                return RedirectToAction("Customer");
            }
            return View("Customer");
        }

        [HttpGet("/search")]
        public IActionResult Search(string searchStr)
        {
            Customer customer = dbContext.Customers.FirstOrDefault(c => c.Name == searchStr);
            if (!String.IsNullOrEmpty(searchStr))
            {
                if (customer != null)
                {
                    return RedirectToAction("Detail", new { id = customer.CustomerId });
                }
            }
            return View("Customer");
        }

        [HttpPost("/delete/{id}")]
        public IActionResult Remove(int id)
        {
            Customer SelectedCustomer = dbContext.Customers.FirstOrDefault(c => c.CustomerId == id);
            dbContext.Remove(SelectedCustomer);
            dbContext.SaveChanges();
            return RedirectToAction("Customer");
        }
    }
}