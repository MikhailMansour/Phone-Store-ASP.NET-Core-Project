using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Infrastructure;
using Products.Models;
using Products.ViewModels.Buyer;

namespace Products.Controllers
{
    public class BuyerController : Controller
    {
        private readonly ApplicationContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public BuyerController(UserManager<ApplicationUser> userManager, ApplicationContext context, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        
        [Authorize(Roles = "Buyer")]

        public async Task<IActionResult> Dashboard()
        {
            var userId = int.Parse(_userManager.GetUserId(User));

            var purchase = await _context.ProductsBuyers.Where(a => a.UserId == userId)
                .Include(pb => pb.Product)
                .Include(pb => pb.User)
                .Include(pb => pb.Address)
                .OrderByDescending(pb => pb.Id)

                .ToListAsync();

            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

    }
}
