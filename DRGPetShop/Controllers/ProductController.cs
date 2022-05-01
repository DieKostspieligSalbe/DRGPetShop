using DRGPetShop.MVC.Data;
using DRGPetShop.MVC.Models;
using DRGPetShop.MVC.Models.ViewModels;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DRGPetShop.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly DrgContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(DrgContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _context.Product.Include(p => p.Category).Include(p => p.Behaviour);
            return View(productList);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductViewModel productViewModel = new()
            {
                Product = new(),
                CategorySelectList = _context.Category.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                BehaviourSelectList = _context.Behaviour.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            if (id is null)
            {
                return View(productViewModel);
            }
            else
            {
                productViewModel.Product = _context.Product.Find(id);
                if (productViewModel.Product is null)
                {
                    return NotFound();
                }
                return View(productViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var imageFile = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                //remove summernote html tags
                HtmlDocument htmlDoc = new();
                htmlDoc.LoadHtml(productVM.Product.Description);
                productVM.Product.Description = htmlDoc.DocumentNode.InnerText;
                _context.Product.Add(productVM.Product);

                if (productVM.Product.Id == 0)
                {
                    //create product
                    string uploadPath = webRootPath + Constants.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(imageFile[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName + extension), FileMode.Create))
                    {
                        imageFile[0].CopyTo(fileStream);
                    }

                    productVM.Product.Image = fileName + extension;
                }
                else //update product
                {
                    var itemFromDb = _context.Product.AsNoTracking().FirstOrDefault(x => x.Id == productVM.Product.Id);
                    if (imageFile.Count > 0 && itemFromDb is not null) //new pic to update
                    {
                        string uploadPath = webRootPath + Constants.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(imageFile[0].FileName);

                        if (itemFromDb.Image is not null)
                        {
                            var oldImagePath = Path.Combine(uploadPath, itemFromDb.Image);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }
                        
                        using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName + extension), FileMode.Create))
                        {
                            imageFile[0].CopyTo(fileStream);
                        }
                        productVM.Product.Image = fileName + extension;

                    }
                    else if (itemFromDb is not null) //image didn't change
                    {
                        productVM.Product.Image = itemFromDb.Image;
                    }
                    _context.Product.Update(productVM.Product); //since itemFromDb and productVM have the same id, it will update all VM properties into db

                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            productVM.CategorySelectList = _context.Category.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            productVM.BehaviourSelectList = _context.Behaviour.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(productVM);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var dbItem = _context.Product.Include(p => p.Category).Include(p => p.Behaviour).FirstOrDefault(p => p.Id == id);
            if (dbItem is null)
            {
                return NotFound();
            }
            return View(dbItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var dbItem = _context.Product.Find(id);
            if (dbItem is null)
            {
                return NotFound();
            }

            string uploadPath = _webHostEnvironment.WebRootPath + Constants.ImagePath;
            var imagePath = Path.Combine(uploadPath, dbItem.Image);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _context.Product.Remove(dbItem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
