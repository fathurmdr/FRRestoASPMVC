using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FRResto.Models
{
    public class RestaurantBranch
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Slug { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public ICollection<Menu> Menus { get; set; } = new List<Menu>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
        public ICollection<Promo> Promos { get; set; } = new List<Promo>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
