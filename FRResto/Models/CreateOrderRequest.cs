using System.ComponentModel.DataAnnotations;

namespace FRResto.Models
{
    public class CreateOrderRequest
    {
        [Required]
        public string TableNumber { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public Guid CartId { get; set; }


        public string PromoCode { get; set; }

        public string PaymentMethodCode { get; set; }
    }
}
