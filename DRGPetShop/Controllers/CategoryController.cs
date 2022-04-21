using DRGPetShop.MVC.Data;
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
            _context.Category.Add(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
