using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyClothes.Models;

namespace TinyClothes.Data
{
    /// <summary>
    /// Contains DB Helper methods for <see cref="Models.Clothing"/>
    /// </summary>
    public static class ClothingDB
    {
        public static List<Clothing> GetAllClothing()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds clothing object to database
        /// returns object with id populated
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static async Task<Clothing> Add(Clothing c, StoreContext context)  // added async & wrapped return type with task<>
        {
            // New is GLUE, take things as parameters & Make DatabaseCode async, boost performance
            // https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-3.1#create-controller-and-views

            //context.Add(c);       // Prepares insert query
            //context.SaveChanges();// Execute insert query

            await context.AddAsync(c);       // Prepare insert query, Similar to above but Async
            await context.SaveChangesAsync();// Execute insert query, Similar to above but Async

            return c;
        }
    }
}
