using DRGPetShop.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace DRGPetShop.MVC.Data
{
    public class DrgContext : DbContext
    {
        public DrgContext(DbContextOptions<DrgContext> options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
    }
}
