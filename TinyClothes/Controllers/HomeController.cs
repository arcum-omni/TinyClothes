using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyClothes.Models;

namespace TinyClothes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CustomError(string code)
        {
            switch (code)
            {
                case "400":
                    ViewData["ErrorMsg"] = "Hey \"Halt & Catch Fire\" the URL you tried to hack does not exist";
                    break;
                case "404":
                    ViewData["ErrorMsg"] = "Page Not Found";
                    break;
            }

            return View();
        }
    }
}
