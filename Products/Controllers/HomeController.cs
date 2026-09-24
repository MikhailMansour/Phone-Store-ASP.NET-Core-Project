using Microsoft.AspNetCore.Mvc;
using Products.Services;

namespace Products.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductServices _productServices;

        public HomeController(ProductServices productServices)
        {
            _productServices = productServices;
        }

        public async Task<IActionResult> Index()
        {
            // جلب كل المنتجات بدون قيود (تمرير حجم كبير جداً لجلب الكل دفعة واحدة)
            var result = await _productServices.ProductsAsync(int.MaxValue, 1);
            return View(result);
        }

        public async Task<IActionResult> FeedBack()
        {
            return View();
        }
    }
}