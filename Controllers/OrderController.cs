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
    public class OrderController : Controller
    {
        // Context
        private EContext dbContext;
        public OrderController(EContext context)
        {
            dbContext = context;
        }

        // ROUTES
        [HttpGet("/orders")]
        public IActionResult Order()
        {
            List<Order> AllOrders = dbContext.Orders.Include(o => o.Product).Include(o => o.Customer).OrderByDescending(p => p.CreatedAt).ToList();
            List<Product> AllProducts = dbContext.Products.ToList();
            List<Customer> AllCustomers = dbContext.Customers.ToList();
            ViewBag.AllOrders = AllOrders;
            ViewBag.AllProducts = AllProducts;
            ViewBag.AllCustomers = AllCustomers;
            return View("Order");
        }

        [HttpPost("/orders/new")]
        public IActionResult AddOrder(Order order)
        {
            // if order amount exceeds current qty -> error
            Product SelectedProduct = dbContext.Products.FirstOrDefault(p => p.ProductId == order.ProductId);
            int CurrentQty = SelectedProduct.Qty;
            if (ModelState.IsValid)
            {
                if (CurrentQty - order.Amount < 0)
                {
                    ModelState.AddModelError("Amount", $"We only have {CurrentQty} products left in stock");
                }
                else 
                {
                    Order connection = new Order()
                    {
                        Amount = order.Amount,
                        ProductId = order.ProductId,
                        CustomerId = order.CustomerId
                    };   
                    // update product qty
                    SelectedProduct.Qty -= order.Amount;
                    dbContext.Orders.Add(connection);
                    dbContext.SaveChanges();
                    return RedirectToAction("Order");
                }
            }
            // to render View
            List<Order> AllOrders = dbContext.Orders.Include(o => o.Product).Include(o => o.Customer).OrderByDescending(p => p.CreatedAt).ToList();
            List<Product> AllProducts = dbContext.Products.ToList();
            List<Customer> AllCustomers = dbContext.Customers.ToList();
            ViewBag.AllOrders = AllOrders;
            ViewBag.AllProducts = AllProducts;
            ViewBag.AllCustomers = AllCustomers;
            return View("Order");
        }
    }
}