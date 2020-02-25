using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TinyClothes.Controllers
{
    public class CartController : Controller
    {
        /// <summary>
        /// Display all products in cart.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Add a single product to the cart.
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Summary/checkout page.
        /// </summary>
        /// <returns></returns>
        public IActionResult CheckOut()
        {
            return View();
        }
    }
}