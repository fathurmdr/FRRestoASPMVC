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
                // Ekstrak nomor dari format "ODR00000001" dan tambahkan 1
                var lastNumber = lastOrder.Number.Substring(3); // "00000001"
                newOrderNumber = int.Parse(lastNumber) + 1;
            }

            // Format menjadi "ODR" + angka dengan 10 karakter (7 karakter angka)
            return $"ODR{restaurantBranchId.ToString("D3")}{newOrderNumber.ToString("D5")}";
        }
    }
}
