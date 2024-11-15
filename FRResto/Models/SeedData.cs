using FRResto.Data;
using FRResto.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FRResto.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FRRestoContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FRRestoContext>>()))
            {
                // Look for any movies.
                if (context.Restaurants.Any())
                {
                    return;   // DB has been seeded
                }
                context.Restaurants.AddRange(
                    new Restaurant
                    {
                        Name = "FR Resto",
                        Description = "FR Restaurant",

                        Users = new List<User> {
                            new User
                            {
                                Name = "John Doe",
                                Email = "john.doe@email.com",
                                Phone = "1234567890",
                                Password = PasswordHasher.HashPassword("123456"),
                                Role = "Owner",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                            }
                        },

                        Branches = new List<RestaurantBranch>
                        {
                            new RestaurantBranch
                            {
                                Name = "FR Resto",
                                Slug = "FRResto",
                                Description = "FR Restaurant",

                                Menus = new List<Menu> {
                                    new Menu
                                    {
                                        Id = 1,
                                        Sku = "10001",
                                        Name = "Tenderloin Steak",
                                        Description = "Tenderloin steak terbaik, juicy dan empuk.",
                                        Image = "/img/food.webp",
                                        Price= 150000,
                                        Discount = 0,
                                        CreatedAt = DateTime.UtcNow,
                                        UpdatedAt = DateTime.UtcNow,

                                        Additionals = new List<AdditionalMenu>
                                        {
                                            new AdditionalMenu
                                            {
                                                Name = "Size",
                                                IsMultiple = false,
                                                IsRequired = true,
                                                CreatedAt = DateTime.UtcNow,
                                                UpdatedAt = DateTime.UtcNow,

                                                Options = new List<AdditionalOption>
                                                {
                                                    new AdditionalOption
                                                    {
                                                        Sku = "10001-101",
                                                        Value = "Regular",
                                                        Price = 0,
                                                        Discount = 0,
                                                        CreatedAt = DateTime.UtcNow,
                                                        UpdatedAt = DateTime.UtcNow,
                                                    },
                                                    new AdditionalOption
                                                    {
                                                        Sku = "10001-102",
                                                        Value = "Large",
                                                        Price = 20000,
                                                        Discount = 0,
                                                        CreatedAt = DateTime.UtcNow,
                                                        UpdatedAt = DateTime.UtcNow,
                                                    },
                                                     new AdditionalOption
                                                    {
                                                        Sku = "10001-103",
                                                        Value = "Extra Large",
                                                        Price = 40000,
                                                        Discount = 0,
                                                        CreatedAt = DateTime.UtcNow,
                                                        UpdatedAt = DateTime.UtcNow,
                                                    },
                                                }
                                            },
                                            new AdditionalMenu
                                            {
                                                Name = "Cooking Level",
                                                IsMultiple = false,
                                                IsRequired = true,
                                                CreatedAt = DateTime.UtcNow,
                                                UpdatedAt = DateTime.UtcNow,

                                                Options = new List<AdditionalOption>
                                                {
                                                    new AdditionalOption
                                                    {
                                                        Sku = "10001-201",
                                                        Value = "Rare",
                                                        Price = 0,
                                                        Discount = 0,
                                                        CreatedAt = DateTime.UtcNow,
                                                        UpdatedAt = DateTime.UtcNow,
                                                    },
                                                    new AdditionalOption
                                                    {
                                                        Sku = "10001-202",
                                                        Value = "Medium Rare",
                                                        Price = 0,
                                                        Discount = 0,
                                                        CreatedAt = DateTime.UtcNow,
                                                        UpdatedAt = DateTime.UtcNow,
                                                    },
                                                     new AdditionalOption
                                                    {
                                                        Sku = "10001-203",
                                                        Value = "Well Done",
                                                        Price = 0,
                                                        Discount = 0,
                                                        CreatedAt = DateTime.UtcNow,
                                                        UpdatedAt = DateTime.UtcNow,
                                                    },
                                                }
                                            },
                                            new AdditionalMenu
                                            {
                                                Name = "Add-ons",
                                                IsMultiple = false,
                                                IsRequired = true,
                                                CreatedAt = DateTime.UtcNow,
                                                UpdatedAt = DateTime.UtcNow,

                                                Options = new List<AdditionalOption>
                                                {
                                                    new AdditionalOption
                                                    {
                                                        Sku = "10001-301",
                                                        Value = "Cheese",
                                                        Price = 5000,
                                                        Discount = 0,
                                                        CreatedAt = DateTime.UtcNow,
                                                        UpdatedAt = DateTime.UtcNow,
                                                    },
                                                    new AdditionalOption
                                                    {
                                                        Sku = "10001-302",
                                                        Value = "Mushroom Sauce",
                                                        Price = 7000,
                                                        Discount = 0,
                                                        CreatedAt = DateTime.UtcNow,
                                                        UpdatedAt = DateTime.UtcNow,
                                                    },
                                                     new AdditionalOption
                                                    {
                                                        Sku = "10001-303",
                                                        Value = "Extra Butter",
                                                        Price = 3000,
                                                        Discount = 0,
                                                        CreatedAt = DateTime.UtcNow,
                                                        UpdatedAt = DateTime.UtcNow,
                                                    },
                                                }
                                            },
                                        }
                                    },
                                },

                                Categories = new List<Category> {
                                            new Category
                                            {
                                                Id = 1,
                                                Name= "Most Popular",
                                            },
                                        },

                                PaymentMethods = new List<PaymentMethod> {
                                    new PaymentMethod
                                        {
                                            Name = "Pay at Cashier",
                                            Code = "CASH",
                                            Type = "Cash",
                                            CreatedAt = DateTime.UtcNow,
                                            UpdatedAt = DateTime.UtcNow,
                                    }
                                },

                                Promos = new List<Promo> {
                                    new Promo
                                        {
                                            Code = "FRDISC",
                                            Status = "Active",
                                            MaxDiscount = 50000,
                                            Type = "Percent",
                                            Discount= 10,
                                            Description = "Discount 10% up to Rp 50.000,00",
                                            StartDate = DateTime.UtcNow,
                                            EndDate = DateTime.UtcNow.AddMonths(12),
                                            CreatedAt = DateTime.UtcNow,
                                            UpdatedAt = DateTime.UtcNow,
                                    }
                                },
                            }
                        }
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
