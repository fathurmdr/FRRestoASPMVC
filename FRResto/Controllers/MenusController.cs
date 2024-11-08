using FRResto.Models;
using Microsoft.AspNetCore.Mvc;

namespace FRResto.Controllers
{
    public class MenusController : Controller
    {
        // GET: MenusController
        public ActionResult Index()
        {
            // Fake data menu
            var menus = new List<Menu>
            {
                new Menu
                {
                    Id = 1,
                    Category = "Most Popular",
                    Title = "Tenderloin Steak",
                    Description = "Tenderloin steak terbaik, juicy dan empuk.",
                    Price = 150000,
                    Options = new List<Option>
                    {
                        new Option
                        {
                            Name = "Size",
                            IsMultiple = false,
                            Choices = new List<OptionChoice>
                            {
                                new OptionChoice { Value = "Regular", Price = 0 },
                                new OptionChoice { Value = "Large", Price = 20000 },
                                new OptionChoice { Value = "Extra Large", Price = 40000 }
                            }
                        },
                        new Option
                        {
                            Name = "Cooking Level",
                            IsMultiple = false,
                            Choices = new List<OptionChoice>
                            {
                                new OptionChoice { Value = "Rare", Price = 0 },
                                new OptionChoice { Value = "Medium Rare", Price = 0 },
                                new OptionChoice { Value = "Well Done", Price = 0 }
                            }
                        },
                        new Option
                        {
                            Name = "Add-ons",
                            IsMultiple = true,
                            Choices = new List<OptionChoice>
                            {
                                new OptionChoice { Value = "Cheese", Price = 5000 },
                                new OptionChoice { Value = "Mushroom Sauce", Price = 7000 },
                                new OptionChoice { Value = "Extra Butter", Price = 3000 }
                            }
                        }
                    }
                },
                new Menu
                {
                    Id = 2,
                    Category = "Most Popular",
                    Title = "Grilled Salmon",
                    Description = "Fillet salmon segar dengan bumbu khusus.",
                    Price = 120000,
                    Options = new List<Option>
                    {
                        new Option
                        {
                            Name = "Size",
                            IsMultiple = false,
                            Choices = new List<OptionChoice>
                            {
                                new OptionChoice { Value = "Small", Price = 0 },
                                new OptionChoice { Value = "Medium", Price = 15000 },
                                new OptionChoice { Value = "Large", Price = 30000 }
                            }
                        },
                        new Option
                        {
                            Name = "Sauce",
                            IsMultiple = true,
                            Choices = new List<OptionChoice>
                            {
                                new OptionChoice { Value = "Lemon Butter", Price = 5000 },
                                new OptionChoice { Value = "Garlic", Price = 5000 },
                                new OptionChoice { Value = "Honey Glaze", Price = 7000 }
                            }
                        }
                    }
                },
                new Menu
                {
                    Id = 3,
                    Category = "Salad",
                    Title = "Caesar Salad",
                    Description = "Salad segar dengan potongan ayam panggang.",
                    Price = 50000,
                    Options = new List<Option>
                    {
                        new Option
                        {
                            Name = "Protein",
                            IsMultiple = false,
                            Choices = new List<OptionChoice>
                            {
                                new OptionChoice { Value = "Chicken", Price = 0 },
                                new OptionChoice { Value = "Shrimp", Price = 10000 },
                                new OptionChoice { Value = "Salmon", Price = 20000 }
                            }
                        },
                        new Option
                        {
                            Name = "Extra Toppings",
                            IsMultiple = true,
                            Choices = new List<OptionChoice>
                            {
                                new OptionChoice { Value = "Croutons", Price = 3000 },
                                new OptionChoice { Value = "Parmesan", Price = 5000 },
                                new OptionChoice { Value = "Avocado", Price = 7000 }
                            }
                        }
                    }
                },
                new Menu
                {
                    Id = 4,
                    Category = "Pasta",
                    Title = "Spaghetti Bolognese",
                    Description = "Pasta dengan saus bolognese spesial.",
                    Price = 70000,
                    Options = new List<Option>
                    {
                        new Option
                        {
                            Name = "Portion Size",
                            IsMultiple = false,
                            Choices = new List<OptionChoice>
                            {
                                new OptionChoice { Value = "Regular", Price = 0 },
                                new OptionChoice { Value = "Large", Price = 10000 }
                            }
                        },
                        new Option
                        {
                            Name = "Add-ons",
                            IsMultiple = true,
                            Choices = new List<OptionChoice>
                            {
                                new OptionChoice { Value = "Extra Cheese", Price = 3000 },
                                new OptionChoice { Value = "Meatballs", Price = 5000 }
                            }
                        }
                    }
                },
                new Menu
                {
                    Id = 5,
                    Category = "Steak",
                    Title = "BBQ Ribs",
                    Description = "Iga sapi panggang dengan bumbu BBQ khas.",
                    Price = 160000,
                    Options = new List<Option>
                    {
                        new Option
                        {
                            Name = "Serving Size",
                            IsMultiple = false,
                            Choices = new List<OptionChoice>
                            {
                                new OptionChoice { Value = "Half Rack", Price = 0 },
                                new OptionChoice { Value = "Full Rack", Price = 50000 }
                            }
                        },
                        new Option
                        {
                            Name = "Side Dish",
                            IsMultiple = true,
                            Choices = new List<OptionChoice>
                            {
                                new OptionChoice { Value = "French Fries", Price = 5000 },
                                new OptionChoice { Value = "Mashed Potatoes", Price = 7000 },
                                new OptionChoice { Value = "Coleslaw", Price = 3000 }
                            }
                        }
                    }
                } ,
                 new Menu
            {
                Id = 6,
                Category = "Pizza",
                Title = "Margherita Pizza",
                Description = "Pizza klasik dengan saus tomat, mozzarella, dan daun basil.",
                Price = 80000,
                Options = new List<Option>
                {
                    new Option
                    {
                        Name = "Size",
                        IsMultiple = false,
                        Choices = new List<OptionChoice>
                        {
                            new OptionChoice { Value = "Medium", Price = 0 },
                            new OptionChoice { Value = "Large", Price = 20000 }
                        }
                    },
                    new Option
                    {
                        Name = "Extra Toppings",
                        IsMultiple = true,
                        Choices = new List<OptionChoice>
                        {
                            new OptionChoice { Value = "Pepperoni", Price = 5000 },
                            new OptionChoice { Value = "Olives", Price = 3000 },
                            new OptionChoice { Value = "Mushrooms", Price = 4000 }
                        }
                    }
                }
            },
            new Menu
            {
                Id = 7,
                Category = "Burger",
                Title = "Chicken Burger",
                Description = "Burger ayam dengan saus mayo dan sayuran segar.",
                Price = 50000,
                Options = new List<Option>
                {
                    new Option
                    {
                        Name = "Bread Type",
                        IsMultiple = false,
                        Choices = new List<OptionChoice>
                        {
                            new OptionChoice { Value = "Regular", Price = 0 },
                            new OptionChoice { Value = "Whole Wheat", Price = 3000 }
                        }
                    },
                    new Option
                    {
                        Name = "Add-ons",
                        IsMultiple = true,
                        Choices = new List<OptionChoice>
                        {
                            new OptionChoice { Value = "Extra Cheese", Price = 3000 },
                            new OptionChoice { Value = "Bacon", Price = 5000 },
                            new OptionChoice { Value = "Jalapeno", Price = 2000 }
                        }
                    }
                }
            },
            new Menu
            {
                Id = 8,
                Category = "Snack",
                Title = "Shrimp Tacos",
                Description = "Taco isi udang segar dengan saus guacamole.",
                Price = 60000,
                Options = new List<Option>
                {
                    new Option
                    {
                        Name = "Sauce",
                        IsMultiple = false,
                        Choices = new List<OptionChoice>
                        {
                            new OptionChoice { Value = "Spicy", Price = 0 },
                            new OptionChoice { Value = "Mild", Price = 0 }
                        }
                    },
                    new Option
                    {
                        Name = "Toppings",
                        IsMultiple = true,
                        Choices = new List<OptionChoice>
                        {
                            new OptionChoice { Value = "Avocado", Price = 5000 },
                            new OptionChoice { Value = "Sour Cream", Price = 3000 },
                            new OptionChoice { Value = "Lettuce", Price = 2000 }
                        }
                    }
                }
            },
            new Menu
            {
                Id = 9,
                Category = "Steak",
                Title = "Beef Lasagna",
                Description = "Lasagna daging sapi dengan saus bolognese dan keju.",
                Price = 90000,
                Options = new List<Option>
                {
                    new Option
                    {
                        Name = "Size",
                        IsMultiple = false,
                        Choices = new List<OptionChoice>
                        {
                            new OptionChoice { Value = "Regular", Price = 0 },
                            new OptionChoice { Value = "Large", Price = 15000 }
                        }
                    },
                    new Option
                    {
                        Name = "Extra Toppings",
                        IsMultiple = true,
                        Choices = new List<OptionChoice>
                        {
                            new OptionChoice { Value = "Extra Cheese", Price = 4000 },
                            new OptionChoice { Value = "Mushrooms", Price = 3000 }
                        }
                    }
                }
            },
            new Menu
            {
                Id = 10,
                Category = "Drinks",
                Title = "Mango Smoothie",
                Description = "Smoothie mangga segar dengan yogurt.",
                Price = 30000,
                Options = new List<Option>
                {
                    new Option
                    {
                        Name = "Size",
                        IsMultiple = false,
                        Choices = new List<OptionChoice>
                        {
                            new OptionChoice { Value = "Small", Price = 0 },
                            new OptionChoice { Value = "Large", Price = 5000 }
                        }
                    },
                    new Option
                    {
                        Name = "Add-ons",
                        IsMultiple = true,
                        Choices = new List<OptionChoice>
                        {
                            new OptionChoice { Value = "Chia Seeds", Price = 2000 },
                            new OptionChoice { Value = "Protein Powder", Price = 3000 }
                        }
                    }
                }
            },
            new Menu
            {
                Id = 11,
                Category = "Soup",
                Title = "Tom Yum Soup",
                Description = "Sup khas Thailand dengan rasa asam pedas.",
                Price = 55000,
                Options = new List<Option>
                {
                    new Option
                    {
                        Name = "Spice Level",
                        IsMultiple = false,
                        Choices = new List<OptionChoice>
                        {
                            new OptionChoice { Value = "Mild", Price = 0 },
                            new OptionChoice { Value = "Medium", Price = 0 },
                            new OptionChoice { Value = "Spicy", Price = 0 }
                        }
                    }
                }
            },
            new Menu
            {
                Id = 12,
                Category = "Snack",
                Title = "Prawn Tempura",
                Description = "Udang tempura renyah dengan saus celup.",
                Price = 70000,
                Options = new List<Option>
                {
                    new Option
                    {
                        Name = "Serving Size",
                        IsMultiple = false,
                        Choices = new List<OptionChoice>
                        {
                            new OptionChoice { Value = "Regular", Price = 0 },
                            new OptionChoice { Value = "Large", Price = 10000 }
                        }
                    }
                }
            },
            new Menu
            {
                Id = 13,
                Category = "Pasta",
                Title = "Carbonara Pasta",
                Description = "Pasta creamy dengan saus carbonara dan bacon.",
                Price = 75000,
                Options = new List<Option>
                {
                    new Option
                    {
                        Name = "Size",
                        IsMultiple = false,
                        Choices = new List<OptionChoice>
                        {
                            new OptionChoice { Value = "Regular", Price = 0 },
                            new OptionChoice { Value = "Large", Price = 10000 }
                        }
                    },
                    new Option
                    {
                        Name = "Add-ons",
                        IsMultiple = true,
                        Choices = new List<OptionChoice>
                        {
                            new OptionChoice { Value = "Extra Bacon", Price = 5000 },
                            new OptionChoice { Value = "Parmesan Cheese", Price = 4000 }
                        }
                    }
                }
            },
            };


            // Kirim data ke view menggunakan model
            return View(menus);
        }

        // GET: MenusController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MenusController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenusController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenusController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenusController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenusController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenusController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
