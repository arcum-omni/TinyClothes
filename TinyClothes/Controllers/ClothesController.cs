using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes.Controllers
{
    public class ClothesController : Controller
    {
        private readonly StoreContext _context;

        public ClothesController(StoreContext context)
        {
            // this._context = context; // "this" is optional
            _context = context; 
        }

        [HttpGet]
        public IActionResult ShowAll()
        {
            List<Clothing> clothes = new List<Clothing>();
            return View(clothes);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(Clothing c) // Asynchronous Method
        {
            if (ModelState.IsValid)
            {
                await ClothingDB.Add(c, _context);
                // TempData lasts for one redirect, stays in memory
                TempData["Message"] = $"{c.Title} Added Successfully";
                return RedirectToAction("ShowAll");
            }

            // return same view with validation error messages
            return View(c);
        }
    }
}