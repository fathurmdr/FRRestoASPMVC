using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FRResto.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid CartId { get; set; }

        [ForeignKey("CartId")]
        public Cart Cart { get; set; }

        [Required]
        public int MenuId { get; set; }

        [ForeignKey("MenuId")]
        public Menu Menu { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public ICollection<CartItemAdditional> Additionals { get; set; } = new List<CartItemAdditional>();
    }
}
