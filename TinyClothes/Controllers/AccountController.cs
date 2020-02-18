﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
                bool nameUnique = !await AccountDB.IsUserNameTaken(reg.UserName, _context);
                bool emailUnique = !await AccountDB.IsEmailTaken(reg.Email, _context);
                if (!nameUnique)// if username is not unique, add error msg
                {
                    ModelState.AddModelError(nameof(Account.UserName), "Username is in use, create a unique username.");
                }
                if (!emailUnique)// if email is taken, add error msg
                {
                    ModelState.AddModelError(nameof(Account.Email), "Email is associated with another account, enter a different email address.");
                }
                if (nameUnique && emailUnique)
                {
                    Account acc = new Account()
                    {
                        Email = reg.Email,
                        FullName = reg.FullName,
                        Password = reg.Password,
                        UserName = reg.UserName
                    };

                    // Add Account to DB, EF populates entity field
                    await AccountDB.Register(acc, _context);

                    // Create user session
                    HttpContext.Session.SetInt32("ID" , acc.AccountID);
                    HttpContext.Session.SetString("Username", acc.UserName);

                    // Redirect to homepage
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(reg);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                bool loginMatches = await AccountDB.DoesUserMatch(login, _context);

                // TODO: Create session

                return RedirectToAction("Index", "Home");
            }

            return View(login);
        }
    }
}