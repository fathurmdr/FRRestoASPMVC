using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FRResto.Models
{
    public class AdditionalOption
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AdditionalMenuId { get; set; }

        [ForeignKey("AdditionalMenuId")]
        public AdditionalMenu Additional { get; set; }

        [Required]
        [MaxLength(50)]
        public string Sku { get; set; }

        [Required]
        [MaxLength(100)]
        public string Value { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
