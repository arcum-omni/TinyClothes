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
        public async Task<IActionResult> ShowAll(int? page) // method must begin with await, await req's async after public, and a task return.: ? is nullable
        {
            const int PageSize = 2; // default number of items per page

            //int pageNumber = page ?? 1; // null coalescing operator ?? (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator) 
            int pageNumber = page.HasValue ? page.Value : 1; // conditional statement ?
            ViewData["CurrentPage"] = pageNumber;

            int maxPage = await GetMaxPage(PageSize); // How many pages, always rounds up

            ViewData["MaxPage"] = maxPage; // put in view data so we can use it in our view

            List<Clothing> clothes = await ClothingDB.GetClothingByPage(_context, pageNum: pageNumber, pageSize: PageSize);

            return View(clothes);
        }

        /// <summary>
        /// Supports ShowAll Method
        /// </summary>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        private async Task<int> GetMaxPage(int PageSize)
        {
            int numProducts = await ClothingDB.GetNumClothing(_context);

            int maxPage = Convert.ToInt32(Math.Ceiling((double)numProducts / PageSize));
            return maxPage;
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