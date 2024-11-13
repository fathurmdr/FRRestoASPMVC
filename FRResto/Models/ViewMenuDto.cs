namespace FRResto.Models
{
    public class ViewMenuDto
    {
        public RestaurantBranch RestaurantBranch { get; set; }

        public List<Restaurant> restaurants { get; set; }
    }
}
