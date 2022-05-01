using DRGPetShop.Models;
using DRGPetShop.MVC;
using DRGPetShop.MVC.Data;
using DRGPetShop.MVC.Models;
using DRGPetShop.MVC.Models.ViewModels;
using DRGPetShop.MVC.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DRGPetShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DrgContext _context;

        public HomeController(ILogger<HomeController> logger, DrgContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel homeVM = new()
            {
                Products = _context.Product.Include(p => p.Category).Include(p => p.Behaviour),
                Categories = _context.Category
            };
            return View(homeVM);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            List<ShoppingCart> shopCartList = new();
            List<ShoppingCart> sessionInfoCartList = HttpContext.Session.Get<List<ShoppingCart>>(Constants.SessionCart);
            if (sessionInfoCartList != null && sessionInfoCartList.Count > 0)
            {
                shopCartList = sessionInfoCartList;
            }
           
            Product? dbProduct = _context.Product.Include(p => p.Category).Include(p => p.Behaviour).FirstOrDefault(p => p.Id == id);
            if (dbProduct is null)
            {
                return NotFound();
            }
            ProductDetailsViewModel details = new()
            {
                Product = dbProduct,
                ExistsInCart = false
            };
            foreach (var item in shopCartList)
            {
                if (item.ProductId == id)
                {
                    details.ExistsInCart = true;
                }
            }

            return View(details);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public IActionResult DetailsPost(int id)
        {
            List<ShoppingCart> shopCartList = new();
            List<ShoppingCart> sessionInfoCartList = HttpContext.Session.Get<List<ShoppingCart>>(Constants.SessionCart);
            if (sessionInfoCartList != null && sessionInfoCartList.Count > 0)
            {
                shopCartList = sessionInfoCartList;
            }
            shopCartList.Add(new ShoppingCart { ProductId = id });
            HttpContext.Session.Set(Constants.SessionCart, shopCartList);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            List<ShoppingCart> shopCartList = new();
            List<ShoppingCart> sessionInfoCartList = HttpContext.Session.Get<List<ShoppingCart>>(Constants.SessionCart);
            if (sessionInfoCartList != null && sessionInfoCartList.Count > 0)
            {
                shopCartList = sessionInfoCartList;
            }
            var itemToRemove = shopCartList.SingleOrDefault(s => s.ProductId == id);
            if (itemToRemove is not null)
            {
                shopCartList.Remove(itemToRemove);
            }
            HttpContext.Session.Set(Constants.SessionCart, shopCartList);
            return RedirectToAction(nameof(Index));
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}