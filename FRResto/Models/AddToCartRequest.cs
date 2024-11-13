namespace FRResto.Models
{
    public class AddToCartRequest
    {
        public Guid? CartId { get; set; }
        public int MenuId { get; set; }
        public int Quantity { get; set; }
        public List<AdditionalRequest> Additionals { get; set; }
    }
}
