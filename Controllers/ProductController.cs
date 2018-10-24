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
    public class ProductController : Controller
    {
        // Context
        private EContext dbContext;
        public ProductController(EContext context)
        {
            dbContext = context;
        }

        // ROUTES
        [HttpGet("/products")]
        public IActionResult Product()
        {
            List<Product> AllProducts = dbContext.Products.OrderByDescending(p => p.CreatedAt).ToList();
            ViewBag.AllProducts = AllProducts;
            return View("Product");
        }

        [HttpPost("/products/new")]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(product);
                dbContext.SaveChanges();
                return RedirectToAction("Product");
            }
            return View("Product");
        }

        [HttpGet("/products/{id}")]
        public IActionResult ProductDetail(int id)
        {
            Product selectedProduct = dbContext.Products.FirstOrDefault(p => p.ProductId == id);
            ViewBag.product = selectedProduct;
            System.Console.WriteLine($"Selected Product: {selectedProduct.Name}");
            System.Console.WriteLine($"Selected Product: {selectedProduct.Img}");
            System.Console.WriteLine($"Selected Product: {selectedProduct.Description}");
            return View("Detail");
        }
    }
}

