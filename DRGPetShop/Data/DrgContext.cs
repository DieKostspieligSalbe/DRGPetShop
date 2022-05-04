using DRGPetShop.MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DRGPetShop.MVC.Data
{
    public class DrgContext : IdentityDbContext
    {
        public DrgContext(DbContextOptions<DrgContext> options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Behaviour> Behaviour { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
