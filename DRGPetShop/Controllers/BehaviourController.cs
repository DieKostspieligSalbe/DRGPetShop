using DRGPetShop.MVC.Data;
using DRGPetShop.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DRGPetShop.MVC.Controllers
{
    public class BehaviourController : Controller
    {
        private readonly DrgContext _context;
        public BehaviourController(DrgContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Behaviour> categoryList = _context.Behaviour;
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Behaviour item)
        {
            if (ModelState.IsValid)
            {
                _context.Behaviour.Add(item);
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
            var dbItem = _context.Behaviour.Find(id);
            if (dbItem is null)
            {
                return NotFound();
            }
            return View(dbItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Behaviour item)
        {
            if (ModelState.IsValid)
            {
                _context.Behaviour.Update(item);
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
            var dbItem = _context.Behaviour.Find(id);
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
            var dbItem = _context.Behaviour.Find(id);
            if (dbItem is null)
            {
                return NotFound();
            }
            _context.Behaviour.Remove(dbItem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
