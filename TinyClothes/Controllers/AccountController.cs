using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes.Controllers
{
    public class AccountController : Controller
    {
        private readonly StoreContext _context;

        public AccountController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel reg)
        {
            if (ModelState.IsValid)
            {
                if (await AccountDB.IsUserNameTaken(reg.UserName, _context))// if username is taken, add error msg
                {
                    ModelState.AddModelError(nameof(Account.UserName), "Username is in use, create a unique username.");
                }
                if (await AccountDB.IsEmailTaken(reg.Email, _context))// if email is taken, add error msg
                {
                    ModelState.AddModelError(nameof(Account.Email), "Email is associated with another account, enter a different email address.");
                }
                if (!await AccountDB.IsUserNameTaken(reg.UserName, _context) && !await AccountDB.IsEmailTaken(reg.Email, _context))
                {
                    Account acc = new Account()
                    {
                        Email = reg.Email,
                        FullName = reg.FullName,
                        Password = reg.Password,
                        UserName = reg.UserName
                    };

                    await AccountDB.Register(acc, _context); // Add Account to DB
                    return RedirectToAction("Index", "Home"); // Redirect to homepage
                }
            }

            return View(reg);
        }
    }
}