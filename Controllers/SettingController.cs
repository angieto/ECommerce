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
    public class SettingController : Controller
    {
        // Context
        private EContext dbContext;
        public SettingController(EContext context)
        {
            dbContext = context;
        }

        // ROUTES
        [HttpGet("/settings")]
        public IActionResult Setting()
        {
            return View("Setting");
        }
    }
}
