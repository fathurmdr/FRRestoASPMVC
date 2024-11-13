using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FRResto.Models
{
    public class CartItemAdditional
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CartItemId { get; set; }

        [ForeignKey("CartItemId")]
        public CartItem CartItem { get; set; }

        [Required]
        public int AdditionalOptionId { get; set; }

        [ForeignKey("AdditionalOptionId")]
        public AdditionalOption Option { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
