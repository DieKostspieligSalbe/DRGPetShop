using Microsoft.AspNetCore.Mvc.Rendering;

namespace DRGPetShop.MVC.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem>? CategorySelectList { get; set; }
        public IEnumerable<SelectListItem>? BehaviourSelectList { get; set; }
    }
}
