using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Infrastructure;
using Products.Services;

namespace Products.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly ProductServices _productServices;
        private readonly ApplicationContext _context;
        public PurchaseController(ProductServices productServices,   ApplicationContext context)
        {
            _productServices = productServices;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Receipt(int id)
        {
            var purchase = await _context.ProductsBuyers.Where(a=>a.Id==id)
                .Include(pb => pb.Product)
                .Include(pb => pb.User)
                .Include(pb => pb.Address)
                .OrderByDescending(pb => pb.Id)
                
                .FirstOrDefaultAsync();

            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }
    }


}

