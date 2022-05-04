using Microsoft.AspNetCore.Identity;

namespace DRGPetShop.MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
