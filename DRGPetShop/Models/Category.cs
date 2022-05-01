using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DRGPetShop.MVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "This can't be zero")]
        public int DisplayOrder { get; set; }
        [Required]
        public string Color { get; set; }
    }
}
