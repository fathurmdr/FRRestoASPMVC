using FRResto.Models;
using Microsoft.EntityFrameworkCore;

namespace FRResto.Data
{
    public class FRRestoContext : DbContext
    {
        public FRRestoContext(DbContextOptions<FRRestoContext> options)
            : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RestaurantBranch> RestaurantBranches { get; set; }
        public DbSet<Promo> Promos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<AdditionalMenu> AdditionalMenus { get; set; }
        public DbSet<AdditionalOption> AdditionalOptions { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CartItemAdditional> CartItemAdditionals { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderItemAdditional> OrderItemAdditionals { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentHistory> PaymentHistories { get; set; }
    }
}
