using FRResto.Data;
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
        // GET: MenusController
        public async Task<ActionResult> Index(string branch)
        {
            var restaurantBranch = await _context.RestaurantBranches
                .Include(rb => rb.Categories)
                .ThenInclude(c => c.Menus)
                .FirstOrDefaultAsync(rb => rb.Name == branch);
            if (restaurantBranch == null)
            {
                return NotFound();
            }
            return View(restaurantBranch);

        }

        public async Task<ActionResult> GetMenu(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.Include(m => m.Additionals).ThenInclude(am => am.Options).FirstOrDefaultAsync(m => m.Id == Id);

            if (menu == null)
            {
                return NotFound();
            }

            return Ok(menu);
        }

        public async Task<ActionResult> GetCart(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.Include(c => c.Items)
                .ThenInclude(ci => ci.Additionals)
                .ThenInclude(cia => cia.Option)
                .ThenInclude(o => o.Additional)
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Menu)
                .FirstOrDefaultAsync(c => c.Id == Id);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    errorMsg = "Request not valid!"
                });
            }

            var menu = await _context.Menus.Include(m => m.Additionals).ThenInclude(ma => ma.Options).FirstOrDefaultAsync(m => m.Id == request.MenuId);
            if (menu == null)
            {
                return BadRequest(new
                {
                    errorMsg = "Menu not found!"
                });
            }

            foreach (var additional in menu.Additionals)
            {
                if (additional.IsRequired && !additional.Options.Any(o => request.Additionals.Any(add => add.optionId == o.Id)))
                {
                    return BadRequest(new
                    {
                        errorMsg = additional.Name + " is required!"
                    });
                }
            }

            var restaurantBranch = await _context.RestaurantBranches.FirstOrDefaultAsync(b => b.Menus.Contains(menu));
            if (restaurantBranch == null)
            {
                return BadRequest("Cabang restoran tidak ditemukan!");
            }

            var cart = await _context.Carts.Include(c => c.Items)
                .ThenInclude(ci => ci.Additionals)
                .FirstOrDefaultAsync(c => c.Id == request.CartId && c.RestaurantBranchId == restaurantBranch.Id);
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

            var additionals = request.Additionals.Select(item => new CartItemAdditional
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
                    Quantity = request.Quantity,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Additionals = additionals,
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity = cartItem.Quantity + request.Quantity;
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
                .FirstOrDefaultAsync(c => c.Id == cart.Id);

            return Json(cart);
        }
    }
}
