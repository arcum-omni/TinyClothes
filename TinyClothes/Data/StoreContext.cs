using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyClothes.Models;

namespace TinyClothes.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        // Add a DbSet for each entity to be tracked by DB
        // https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-3.1#create-the-database-context
        public DbSet<Clothing> Clothing { get; set; }

        // need a migration to create an account table
        public DbSet<Account> Accounts { get; set; }
    }
}
