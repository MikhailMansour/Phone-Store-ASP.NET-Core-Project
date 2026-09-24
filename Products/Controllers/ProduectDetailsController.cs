using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Infrastructure;
using Products.Models;
using Products.Services;
using Products.ViewModels.Product;
using System.Security.Claims;

namespace Products.Controllers
{
    public class ProduectDetailsController : Controller
    {
        private readonly ProductServices _productServices;
        private readonly ApplicationContext _context;

        public ProduectDetailsController(ProductServices productServices, ApplicationContext context)
        {
            _productServices = productServices;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int productId)
        {
            var product = await _productServices.ProductAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            // جلب التقييمات الخاصة بهذا المنتج
            var reviews = await _context.Reviews
                .Where(r => r.ProductId == productId)
                .ToListAsync();

            // حساب المتوسط والعدد
            double averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0.0;
            int totalRatings = reviews.Count;

            // تخزين القيم في ViewBag لعرضها في الـ View
            ViewBag.Reviews = reviews;
            ViewBag.AverageRating = averageRating;
            ViewBag.TotalRatings = totalRatings;

            var viewModel = new PurchaseProductViewModel
            {
                Product = product,
                Buyer = new BuyerDetails()
            };

            return View(viewModel);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview(int productId, int rating, string comment)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdStr, out var userId))
            {
                // التحقق أن التقييم ضمن النطاق الصحيح (من 1 إلى 5)
                if (rating < 1) rating = 1;
                if (rating > 5) rating = 5;

                var review = new Review
                {
                    ProductId = productId,
                    UserId = userId,
                    Rating = rating,
                    Comment = comment
                };

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "ProduectDetails", new { productId = productId });
        }
    }
}
        