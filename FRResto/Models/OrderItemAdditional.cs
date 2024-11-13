using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FRResto.Models
{
    public class OrderItemAdditional
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderItemId { get; set; }

        [ForeignKey("OrderItemId")]
        public OrderItem OrderItem { get; set; }

        [Required]
        [MaxLength(50)]
        public string AdditionalSku { get; set; }

        [MaxLength(100)]
        public string AdditionalName { get; set; }

        [MaxLength(100)]
        public string AdditionalValue { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
