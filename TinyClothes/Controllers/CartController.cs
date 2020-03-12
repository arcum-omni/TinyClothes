using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes.Controllers
{
    public class CartController : Controller
    {
        // To access the DB
        private readonly StoreContext _context;

        // To read cookie data
        private readonly IHttpContextAccessor _http;

        public CartController(StoreContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }

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
        public async Task<IActionResult> Add(int id, string prevUrl)
        {
            Clothing c = await ClothingDB.GetClothingById(id, _context);

            if (c != null)
            {
                CartHelper.Add(c, _http);
            }

            TempData["Message"] = $"{c.Title}, ID#: {c.ItemID}, Added Successfully";
            //return RedirectToAction("Index", "Home");
            return Redirect(prevUrl);
        }

        public async void AddJS(int id)
        {
            // TODO: get itemId to be added
            // TODO: add item to cart
            // TODO: send success response
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