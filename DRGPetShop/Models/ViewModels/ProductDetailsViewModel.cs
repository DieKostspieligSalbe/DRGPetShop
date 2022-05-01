namespace DRGPetShop.MVC.Models.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public bool ExistsInCart { get; set; }

        public ProductDetailsViewModel()
        {
            Product = new Product();
        }
    }
}
