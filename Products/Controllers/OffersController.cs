using Microsoft.AspNetCore.Mvc;
using Products.Infrastructure;
using Products.Services;
using Products.ViewModels.Product;
using System.Linq;

namespace Products.Controllers
{
    public class OffersController : Controller
    {
        private readonly ProductServices _productServices;
        private readonly ApplicationContext _context;

        public OffersController(ProductServices productServices, ApplicationContext context)
        {
            _productServices = productServices;
            _context = context;
        }

        public IActionResult Index()
        {
            var items = _context.Products
                .Where(p => p.HasOffer && p.DiscountPrice < p.Price)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    HeaderImage = p.HeaderImage,
                    Price = p.Price,
                    DiscountPrice = p.DiscountPrice
                })
                .ToList();

            var result = new PaginationResult<ProductViewModel>
            {
                Items = items,
                PageNumber = 1,
                PageSize = items.Count > 0 ? items.Count : 1,
                TotalCount = items.Count
            };

            return View(result);
        }
    }
}
