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
                // check username & email are not taken, ie unique
                if (!await AccountDb.IsUserNameTaken(reg.UserName, _context))
                {
                    Account acc = new Account()
                    {
                        Email = reg.Email,
                        FullName = reg.FullName,
                        Password = reg.Password,
                        UserName = reg.UserName
                    };

                    // add acc to DB
                    await AccountDb.Register(acc, _context);
                    return RedirectToAction("Index", "Home");
                    // redirect to homepage
                }
                else // if username is taken, add error msg
                {
                    ModelState.AddModelError(nameof(Account.UserName), "Username is taken, choose a different username.");
                }
            }

            return View(reg);
        }
    }
}