using FRResto.Models;

namespace FRResto.Helpers
{
    public class OrderHelper
    {
        public static string GenerateOrderNumber(int restaurantBranchId, Order? lastOrder)
        {

            int newOrderNumber = 1;

            if (lastOrder != null && !string.IsNullOrEmpty(lastOrder.Number))
            {
                var lastNumber = lastOrder.Number.Substring(6);
                newOrderNumber = int.Parse(lastNumber) + 1;
            }

            return $"ODR{restaurantBranchId.ToString("D3")}{newOrderNumber.ToString("D6")}";
        }
    }
}
