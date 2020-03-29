using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    /// <summary>
    /// Helper class to manage user shopping cart data,
    /// using cookies and json.
    /// </summary>
    public class CartHelper
    {
        private const string CartCookie = "CartCookie";

        /// <summary>
        /// Add an item to the shopping cart using cookies.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="http"></param>
        public static void Add(ClothingCartViewModel c, IHttpContextAccessor http)
        {
            List<ClothingCartViewModel> cartItems = GetAllClothes(http);
            cartItems.Add(c);

            string data = JsonConvert.SerializeObject(cartItems);

            CookieOptions options = new CookieOptions
            {
                MaxAge = TimeSpan.FromDays(30),
                //Expires = DateTime.Now.AddDays(30),
                IsEssential = true,
                Secure = true,
            };

            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-3.1
            // response server writes data (create cookie) [key value pair]
            http.HttpContext.Response.Cookies.Append(CartCookie, data);
        }

        /// <summary>
        /// Get number of items in the shopping cart.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="http"></param>
        public static int GetCount(IHttpContextAccessor http)
        {
            List<ClothingCartViewModel> allClothes = GetAllClothes(http);

            return allClothes.Count;
        }

        /// <summary>
        /// Returns all items currently stored in the users cookie, IE shopping cart.
        /// If no items are present, an empty list is returned.
        /// </summary>
        /// <param name="http"></param>
        /// <returns></returns>
        public static List<ClothingCartViewModel> GetAllClothes(IHttpContextAccessor http)
        {
            // get data from the cookie
            string data = http.HttpContext.Request.Cookies[CartCookie];
            if (string.IsNullOrWhiteSpace(data))
            {
                return new List<ClothingCartViewModel>();
            }
            else
            {
                return JsonConvert.DeserializeObject<List<ClothingCartViewModel>>(data);
            }
        }
    }
}