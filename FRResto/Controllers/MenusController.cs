using FRResto.Data;
using FRResto.Helpers;
using FRResto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FRResto.Controllers
{
    public class MenusController : Controller
    {
        private readonly FRRestoContext _context;

        public MenusController(FRRestoContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index(string Branch)
        {
            var restaurantBranch = await _context.RestaurantBranches
                .Include(rb => rb.Categories)
                .ThenInclude(c => c.Menus)
                .FirstOrDefaultAsync(rb => rb.Slug == Branch);
            if (restaurantBranch == null)
            {
                return NotFound();
            }
            return View(restaurantBranch);

        }

        public async Task<ActionResult> GetMenu(string Branch, int? Id)
        {
            if (Branch == null || Id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .Include(m => m.Additionals)
                .ThenInclude(am => am.Options)
                .FirstOrDefaultAsync(m => m.Id == Id && m.RestaurantBranch.Slug == Branch);

            if (menu == null)
            {
                return NotFound();
            }

            return Ok(menu);
        }

        public async Task<ActionResult> GetCart(string Branch, Guid? Id)
        {
            if (Branch == null || Id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.Include(c => c.Items)
                .ThenInclude(ci => ci.Additionals)
                .ThenInclude(cia => cia.Option)
                .ThenInclude(o => o.Additional)
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Menu)
                .FirstOrDefaultAsync(c => c.Id == Id && c.RestaurantBranch.Slug == Branch);

            if (cart == null)
            {
                return NotFound();
            }

            cart.Items = cart.Items.OrderBy(ci => ci.CreatedAt).ToList();

            return Ok(cart);
        }

        public async Task<ActionResult> GetPaymentMethods(string Branch)
        {
            if (Branch == null)
            {
                return NotFound();
            }

            var paymentMethods = await _context.PaymentMethods.OrderBy(ci => ci.CreatedAt).ToListAsync();

            return Ok(paymentMethods);
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart(string Branch, [FromBody] AddToCartRequest Request)
        {
            if (Branch == null)
            {
                return NotFound();
            }
            if (Request == null)
            {
                return BadRequest(new
                {
                    errorMsg = "Request invalid!"
                });
            }

            var menu = await _context.Menus
                .Include(m => m.Additionals)
                .ThenInclude(ma => ma.Options)
                .FirstOrDefaultAsync(m => m.Id == Request.MenuId && m.RestaurantBranch.Slug == Branch);
            if (menu == null)
            {
                return BadRequest(new
                {
                    errorMsg = "Menu not found!"
                });
            }

            foreach (var additional in menu.Additionals)
            {
                if (additional.IsRequired && !additional.Options.Any(o => Request.Additionals.Any(add => add.optionId == o.Id)))
                {
                    return BadRequest(new
                    {
                        errorMsg = additional.Name + " is required!"
                    });
                }
            }

            var restaurantBranch = await _context.RestaurantBranches.FirstOrDefaultAsync(rb => rb.Menus.Contains(menu) && rb.Slug == Branch);
            if (restaurantBranch == null)
            {
                return BadRequest("Cabang restoran tidak ditemukan!");
            }

            var cart = await _context.Carts.Include(c => c.Items)
                .ThenInclude(ci => ci.Additionals)
                .FirstOrDefaultAsync(c => c.Id == Request.CartId && c.RestaurantBranchId == restaurantBranch.Id);
            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    RestaurantBranchId = restaurantBranch.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };
                _context.Carts.Add(cart);
            }

            var additionals = Request.Additionals.Select(item => new CartItemAdditional
            {
                AdditionalOptionId = item.optionId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            }).ToList();

            var cartItem = cart.Items
                .FirstOrDefault(item => item.MenuId == menu.Id &&
                    item.Additionals.All(a => additionals.Any(add => add.AdditionalOptionId == a.AdditionalOptionId)));


            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    MenuId = menu.Id,
                    Quantity = Request.Quantity,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Additionals = additionals,
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity = cartItem.Quantity + Request.Quantity;
                cartItem.UpdatedAt = DateTime.UtcNow;
                _context.CartItems.Update(cartItem);
            }

            await _context.SaveChangesAsync();

            cart = await _context.Carts.Include(c => c.Items)
                .ThenInclude(ci => ci.Additionals)
                .ThenInclude(cia => cia.Option)
                .ThenInclude(o => o.Additional)
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Menu)
                .FirstOrDefaultAsync(c => c.Id == cart.Id && c.RestaurantBranch.Slug == Branch);

            if (cart != null)
            {
                cart.Items = cart.Items.OrderBy(ci => ci.CreatedAt).ToList();
            }

            return Ok(cart);
        }

        [HttpPatch]
        public async Task<IActionResult> AddQuantityCartItem(string Branch, int Id, int Quantity)
        {
            if (Branch == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.Id == Id);

            if (cartItem == null)
            {
                return BadRequest(new
                {
                    errorMsg = "Item not found!"
                });
            }

            cartItem.Quantity = Math.Max(cartItem.Quantity + Quantity, 0);
            cartItem.UpdatedAt = DateTime.UtcNow;

            if (cartItem.Quantity > 0)
            {
                _context.CartItems.Update(cartItem);
            }
            else
            {
                _context.CartItems.Remove(cartItem);
            }

            await _context.SaveChangesAsync();

            var cart = await _context.Carts.Include(c => c.Items)
                .ThenInclude(ci => ci.Additionals)
                .ThenInclude(cia => cia.Option)
                .ThenInclude(o => o.Additional)
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Menu)
                .FirstOrDefaultAsync(c => c.Id == cartItem.CartId && c.RestaurantBranch.Slug == Branch);

            if (cart != null)
            {
                cart.Items = cart.Items.OrderBy(ci => ci.CreatedAt).ToList();
            }

            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(string Branch, [FromBody] CreateOrderRequest Request)
        {
            if (Branch == null)
            {
                return NotFound();
            }

            var restaurantBranch = await _context.RestaurantBranches.FirstOrDefaultAsync(rb => rb.Slug == Branch);

            if (restaurantBranch == null)
            {
                return NotFound();
            }

            if (Request == null || Request.TableNumber == null || Request.FullName == null || Request.PhoneNumber == null || Request.PaymentMethodCode == null)
            {
                return BadRequest(new
                {
                    errorMsg = "Please fill all fields!"
                });
            }

            var cart = await _context.Carts.Include(c => c.Items)
                .ThenInclude(ci => ci.Additionals)
                .ThenInclude(cia => cia.Option)
                .ThenInclude(o => o.Additional)
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Menu)
                .FirstOrDefaultAsync(c => c.Id == Request.CartId && c.RestaurantBranchId == restaurantBranch.Id);
            if (cart == null)
            {
                return BadRequest(new
                {
                    errorMsg = "Cart not found!"
                });
            }

            var paymentMethod = await _context.PaymentMethods.FirstOrDefaultAsync(pm => pm.Code == Request.PaymentMethodCode && pm.RestaurantBranchId == restaurantBranch.Id);
            if (paymentMethod == null)
            {
                return BadRequest(new
                {
                    errorMsg = "Payment method not found!"
                });
            }

            Promo? promo = null;
            if (Request.PromoCode != null)
            {
                promo = await _context.Promos.FirstOrDefaultAsync(p => p.Code == Request.PromoCode && p.RestaurantBranchId == restaurantBranch.Id);
                if (promo == null)
                {
                    return BadRequest(new
                    {
                        errorMsg = "Promo not found!"
                    });
                }

                if (promo.Status != "Active" || promo.StartDate > DateTime.UtcNow || promo.EndDate < DateTime.UtcNow)
                {
                    return BadRequest(new
                    {
                        errorMsg = "Promo invalid!"
                    });
                }
            }

            decimal subtotal = 0;
            decimal discountItems = 0;
            decimal discountPromo = 0;
            decimal total = 0;

            var orderItems = new List<OrderItem>();

            foreach (var cartItem in cart.Items)
            {
                var orderItemAdditionals = new List<OrderItemAdditional>();

                foreach (var cartItemAdditonal in cartItem.Additionals)
                {
                    subtotal += cartItemAdditonal.Option.Price * cartItem.Quantity;
                    discountItems += cartItemAdditonal.Option.Discount * cartItem.Quantity;

                    orderItemAdditionals.Add(new OrderItemAdditional
                    {
                        AdditionalName = cartItemAdditonal.Option.Additional.Name,
                        AdditionalValue = cartItemAdditonal.Option.Value,
                        AdditionalSku = cartItemAdditonal.Option.Sku,
                        Price = cartItemAdditonal.Option.Price,
                        Discount = cartItemAdditonal.Option.Discount,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    });
                }

                subtotal += cartItem.Menu.Price * cartItem.Quantity;
                discountItems += cartItem.Menu.Discount * cartItem.Quantity;

                orderItems.Add(new OrderItem
                {
                    MenuSku = cartItem.Menu.Sku,
                    MenuName = cartItem.Menu.Name,
                    MenuDescription = cartItem.Menu.Description,
                    MenuImage = cartItem.Menu.Image,
                    Price = cartItem.Menu.Price,
                    Discount = cartItem.Menu.Discount,
                    Quantity = cartItem.Quantity,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Additionals = orderItemAdditionals,
                });
            }

            if (promo != null)
            {
                if (promo.Type == "Percentage")
                {
                    discountPromo = Math.Min((subtotal - discountItems) * promo.Discount / 100, promo.MaxDiscount);
                }
                if (promo.Type == "Amount")
                {
                    discountPromo = Math.Min(promo.Discount, promo.MaxDiscount);
                }
            }

            total = subtotal - discountItems - discountPromo;

            if (total <= 0)
            {
                return BadRequest(new
                {
                    errorMsg = "Cart data invalid!"
                });
            }


            var lastOrder = await _context.Orders
                .OrderByDescending(o => o.Id)
                .FirstOrDefaultAsync(o => o.Number.Contains($"ODR{restaurantBranch.Id.ToString("D3")}"));

            string orderNumber = OrderHelper.GenerateOrderNumber(restaurantBranch.Id, lastOrder);

            var paymentHistory = new PaymentHistory
            {
                Status = "Unpaid",
                PaymentMethodId = paymentMethod.Id,
                Amount = total,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var order = new Order
            {
                TableNumber = Request.TableNumber,
                CustomerName = Request.FullName,
                CustomerPhone = Request.PhoneNumber,
                Number = orderNumber,
                Subtotal = subtotal,
                DiscountItems = discountItems,
                DiscountPromo = discountPromo,
                Total = total,
                RestaurantBranchId = restaurantBranch.Id,
                PromoId = promo?.Id,
                Status = "Waiting for payment",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Items = orderItems,
                PaymentHistory = paymentHistory
            };

            _context.Orders.Add(order);

            _context.Carts.Remove(cart);

            await _context.SaveChangesAsync();

            return Json(new { OrderNumber = orderNumber });
        }
    }
}
