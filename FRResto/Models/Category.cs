using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FRResto.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RestaurantBranchId { get; set; }

        [ForeignKey("RestaurantBranchId")]
        public RestaurantBranch RestaurantBranch { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public ICollection<Menu> Menus { get; set; } = new List<Menu>();
    }
}
