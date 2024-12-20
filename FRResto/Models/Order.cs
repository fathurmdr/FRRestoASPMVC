﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FRResto.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RestaurantBranchId { get; set; }

        [ForeignKey("RestaurantBranchId")]
        public RestaurantBranch RestaurantBranch { get; set; }

        [Required]
        [MaxLength(10)]
        public string TableNumber { get; set; }

        public int? PromoId { get; set; }

        [ForeignKey("PromoId")]
        public Promo? Promo { get; set; }

        [Required]
        [MaxLength(50)]
        public string Number { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [Phone]
        public string CustomerPhone { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPromo { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountItems { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        public PaymentHistory PaymentHistory { get; set; } = new PaymentHistory();
    }
}
