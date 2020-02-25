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
        /// <summary>
        /// Add an item to the shopping cart using cookies.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="http"></param>
        public static void Add(Clothing c, IHttpContextAccessor http)
        {
            string data = JsonConvert.SerializeObject(c);

            CookieOptions options = new CookieOptions
            {
                MaxAge = TimeSpan.FromDays(30),
                //Expires = DateTime.Now.AddDays(30),
                IsEssential = true,
                Secure = true,
                
            };

            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-3.1
            // response server writes data (create cookie) [key value pair]
            http.HttpContext.Response.Cookies.Append("CartCookie", data);
        }

        /// <summary>
        /// Get number of items in the shopping cart.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="http"></param>
        public static int GetCount(IHttpContextAccessor http)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all items from the shopping cart.
        /// </summary>
        /// <param name="http"></param>
        /// <returns></returns>
        public static List<Clothing> GetAllClothes(IHttpContextAccessor http)
        {
            throw new NotImplementedException();
        }
    }
}