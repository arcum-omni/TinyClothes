using Microsoft.EntityFrameworkCore;
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
        /// <summary>
        /// The quantity/count of unique clothing items in the DB
        /// </summary>
        /// <returns></returns>
        public async static Task<int> GetNumClothing(StoreContext context)
        {
            // LINQ Method Syntax
            return await context.Clothing.CountAsync();

            // LINQ Query Syntax
            //return await (from c in context.Clothing select c).CountAsync();
        }


        /// <summary>
        /// Returns a specific page of clothing items by ItemId in ascending order.
        /// </summary>
        /// <param name="context">The DB Context</param>
        /// <param name="pageNum">The number of the page requested</param>
        /// <param name="pageSize">Number of clothing items per page</param>
        public async static Task<List<Clothing>> GetClothingByPage(StoreContext context, int pageNum, int pageSize)
        {
            // If user wanted page 1, to avoid skipping any rows, we must offset by 1
            const int PageOffset = 1;

            // LINQ Method Syntax, trickier to modify later
            List<Clothing> clothes = await context.Clothing
                                        .OrderBy(c => c.ItemID)
                                        .Skip((pageNum - PageOffset) * pageSize) // skip/take order matters, must do skip before take
                                        .Take(pageSize)
                                        .ToListAsync();

            // LINQ Query Syntax
            // Same functionality as method syntax above
            // Saved for notes
            //List<Clothing> clothes =
            //    await (from c in context.Clothing orderby c.ItemID ascending select c)
            //                            .Skip((pageNum - PageOffset) * pageSize) // skip/take order matters, must do skip before take
            //                            .Take(pageSize)
            //                            .ToListAsync();

            return clothes;
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

        /// <summary>
        /// Returns a single clothing item, or null if there is no match.
        /// </summary>
        /// <param name="id">The ID of the item</param>
        /// <param name="context">DB Context</param>
        public static async Task<Clothing> GetClothingById(int id, StoreContext context)
        {
            Clothing c = await (from clothing in context.Clothing
                                where clothing.ItemID == id
                                select clothing).SingleOrDefaultAsync();

            return c;

            // same as above without a variable
            //return await (from clothing in context.Clothing
            //                    where clothing.ItemID == id
            //                    select clothing).SingleOrDefaultAsync();
        }


        public static async Task<Clothing> Edit(Clothing c, StoreContext context)
        {
            await context.AddAsync(c);
            context.Entry(c).State = EntityState.Modified;

            // if SaveChanges/SaveChangesAsync isn't called, db will not be updated
            await context.SaveChangesAsync();

            return c;
        }
    }
}
