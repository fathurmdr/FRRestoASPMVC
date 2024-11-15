using FRResto.Data;
using FRResto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FRResto.Controllers
{
    public class OrdersController : Controller
    {
        private readonly FRRestoContext _context;

        public OrdersController(FRRestoContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Detail(string Branch, string? Id)
        {
            var restaurantBranch = await _context.RestaurantBranches.Include(rb => rb.Orders)
                .FirstOrDefaultAsync(rb => rb.Slug == Branch);

            if (restaurantBranch == null)
            {
                NotFound();
            }

            var order = await _context.Orders.Include(o => o.Items)
                .ThenInclude(i => i.Additionals)
                .FirstOrDefaultAsync(o => o.Number == Id && o.RestaurantBranchId == restaurantBranch.Id);

            if (order == null)
            {
                NotFound();
            }
            return View(new ViewOrderDetail
            {
                RestaurantBranch = restaurantBranch,
                Order = order
            });
        }
    }
}
