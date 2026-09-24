using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Infrastructure;
using Products.Models;
using Products.ViewModels.Authantication;

namespace Products.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationContext context, SignInManager<ApplicationUser> signInManager)
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Dashboard()
        {
            var buyers = await _userManager.GetUsersInRoleAsync("Buyer");
            var sellers = await _userManager.GetUsersInRoleAsync("Saller");

            var stats = new
            {
                ProductsCount = await _context.Products.CountAsync(),
                BuyersCount = buyers.Count,
                SellersCount = sellers.Count,
                OrdersCount = await _context.ProductsBuyers.CountAsync()
            };

            ViewBag.Stats = stats;

            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Products()
        {
            var products = _context.Products.Include(p => p.Seller).ToList();
            return View(products);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ClientMessage()
        {
            var clientMessages = _context.ContactMessages.ToList();
            return View(clientMessages);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users()
        {
            var buyers = await _userManager.GetUsersInRoleAsync("Buyer");
            return View(buyers);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Sallers()
        {
            var sellers = await _userManager.GetUsersInRoleAsync("Saller");
            return View(sellers);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Orders()
        {
            var sales = await _context.ProductsBuyers
                .Include(pb => pb.Product)
                .Include(pb => pb.User)
                .Include(pb => pb.Address)
                .OrderByDescending(pb => pb.UserId)
                .ToListAsync();

            return View(sales);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var appUser = viewModel.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(appUser, viewModel.Password);

            if (!result.Succeeded)
            {
                string errorMessage = "";
                foreach (var item in result.Errors)
                {
                    errorMessage += item.Description + "<br>";
                }
                ModelState.AddModelError("Something Went Wrong", $"Can't Register: " + errorMessage);
                return View(viewModel);
            }

            var roleResult = await _userManager.AddToRoleAsync(appUser, viewModel.Role);
            if (!roleResult.Succeeded)
            {
                ModelState.AddModelError("Something Went Wrong", "Can't Register");
                return View(viewModel);
            }

            return RedirectToAction("Register", "Admin");
        }
    }
}