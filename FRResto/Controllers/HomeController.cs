using FRResto.Data;
using FRResto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FRResto.Controllers
{
    public class HomeController : Controller
    {
        private readonly FRRestoContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, FRRestoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string branch)
        {
            var restaurantBranch = await _context.RestaurantBranches.FirstOrDefaultAsync(rb => rb.Name == branch);
            if (restaurantBranch == null)
            {
                return NotFound();
            }
            return View(restaurantBranch);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
