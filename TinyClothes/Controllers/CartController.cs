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
                var item = new ClothingCartViewModel()
                {
                    ItemId = c.ItemID,
                    Title = c.Title,
                    Price = c.Price,
                    DateAdded = DateTime.Now
                };
                CartHelper.Add(item, _http);
            }

            TempData["Message"] = $"{c.Title}, ID#: {c.ItemID}, Added Successfully";
            //return RedirectToAction("Index", "Home");
            return Redirect(prevUrl);
        }

        // https://prod.liveshare.vsengsaas.visualstudio.com/join?11520D56B900E64EF9597DAA2A347C8D6C89
        public async Task<JsonResult> AddJS(int id)
        {

            Clothing c = await ClothingDB.GetClothingById(id, _context);

            // TODO: add item to cart
            if (c == null)
            {
                // return item not found message (404)
                JsonResult notFound = new JsonResult("Not Found");
                notFound.StatusCode = 404;
                return notFound;
            }

            var item = new ClothingCartViewModel()
            {
                ItemId = c.ItemID,
                Title = c.Title,
                Price = c.Price,
                DateAdded = DateTime.Now
            };

            CartHelper.Add(item, _http);

            // TODO: send success response
            JsonResult result = new JsonResult("Item Added");
            result.StatusCode = 200; // HTTP okay

            return result;
        }

        /// <summary>
        /// Summary/checkout page.
        /// </summary>
        /// <returns></returns>
        public IActionResult CheckOut()
        {
            return View(CartHelper.GetAllClothes(_http));
        }
    }
}