using System.ComponentModel.DataAnnotations;

namespace FRResto.Models
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [MaxLength(25)]
        public string Type { get; set; }

        public string? Account { get; set; }

        public string? Image { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public ICollection<PaymentHistory> PaymentHistories { get; set; } = new List<PaymentHistory>();
    }
}
