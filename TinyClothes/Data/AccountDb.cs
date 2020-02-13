using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes
{
    public static class AccountDb
    {
        public static async Task<bool> IsUserNameTaken(string username, StoreContext context)
        {
            bool isTaken = await (from acc in context.Accounts where username == acc.UserName select acc).AnyAsync();
            
            return isTaken;
        }

        public static async Task<Account> Register(Account acc, StoreContext context)
        {
            await context.Accounts.AddAsync(acc);
            await context.SaveChangesAsync();

            return acc;
        }
    }
}