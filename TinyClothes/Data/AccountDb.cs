using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes
{
    public static class AccountDB
    {
        public static async Task<bool> IsUserNameTaken(string username, StoreContext context)
        {
            bool isTaken = await (from acc in context.Accounts where username == acc.UserName select acc).AnyAsync();
            
            return isTaken;
        }

        public static async Task<bool> IsEmailTaken(string email, StoreContext context)
        {
            bool isTaken = await (from acc in context.Accounts where email == acc.Email select acc).AnyAsync();

            return isTaken;
        }

        public static async Task<Account> Register(Account acc, StoreContext context)
        {
            await context.Accounts.AddAsync(acc);
            await context.SaveChangesAsync();

            return acc;
        }

        /// <summary>
        /// Return true if the username/email and password match a record in the database
        /// </summary>
        /// <param name="login"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<bool> DoesUserMatch(LoginViewModel login, StoreContext context)
        {
            return await (from user in context.Accounts
            where (user.Email == login.UserNameOrEmail || user.UserName == login.UserNameOrEmail) && user.Password == login.Password
            select user).AnyAsync();
        }
    }
}