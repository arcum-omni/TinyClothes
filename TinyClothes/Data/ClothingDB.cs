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
        public static Clothing Add(Clothing c, StoreContext context)
        {
            context.Add(c);       // Prepares insert query
            context.SaveChanges();// Execute insert query

            return c;
        }
    }
}
