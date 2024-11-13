using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FRResto.Models
{
    public class AdditionalMenu
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MenuId { get; set; }

        [ForeignKey("MenuId")]
        public Menu Menu { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public bool IsMultiple { get; set; }

        [Required]
        public bool IsRequired { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public ICollection<AdditionalOption> Options { get; set; } = new List<AdditionalOption>();
    }
}
