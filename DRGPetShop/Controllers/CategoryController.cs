﻿using DRGPetShop.MVC.Data;
using DRGPetShop.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DRGPetShop.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DrgContext _context;
        public CategoryController(DrgContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _context.Category;
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category item)
        {
            if (ModelState.IsValid)
            {
                _context.Category.Add(item);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var dbItem = _context.Category.Find(id);
            if (dbItem is null)
            {
                return NotFound();
            }
            return View(dbItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category item)
        {
            if (ModelState.IsValid)
            {
                _context.Category.Update(item);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var dbItem = _context.Category.Find(id);
            if (dbItem is null)
            {
                return NotFound();
            }
            return View(dbItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var dbItem = _context.Category.Find(id);
            if (dbItem is null)
            {
                return NotFound();
            }
            _context.Category.Remove(dbItem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
