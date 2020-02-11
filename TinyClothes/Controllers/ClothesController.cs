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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // pass primary key value, like canvas
            Clothing c = await ClothingDB.GetClothingById(id, _context);

            if (c == null) // item not in DB
            {
                return NotFound(); // returns HTTP 404 - Not Found Error

                //return RedirectToAction("ShowAll");
                //viewdata, item not found in db
            }

            return View(c);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">Clothing Object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(Clothing c)
        {
            if (ModelState.IsValid)
            {
                await ClothingDB.Edit(c, _context);
                //return View(c);

                TempData["Message"] = $"{c.Title}, ID#: {c.ItemID}, Updated Successfully"; // TempData lasts for one redirect, stays in memory
                return RedirectToAction("ShowAll");
            }

            return View(c);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Clothing c = await ClothingDB.GetClothingById(id, _context);

            // Check if item does not exist
            if (c == null)
            {
                return NotFound();
            }

            return View(c);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            Clothing c = await ClothingDB.GetClothingById(id, _context);

            await ClothingDB.Delete(c, _context);
            TempData["Message"] = $"{c.Title}, ID#: {c.ItemID}, Deleted Successfully";
            return RedirectToAction(nameof(ShowAll));
        }
    }
}