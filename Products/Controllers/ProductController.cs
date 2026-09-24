using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Models;
using Products.Services;
using Products.ViewModels.Product;

namespace Products.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductServices _productServices;

        public ProductController(ProductServices productServices)
        {
            _productServices = productServices;
        }
        public async Task<IActionResult> Index(int size = 10, int index = 1)
        {
            var result = await _productServices.ProductsAsync(size, index);
            return View(result);
        }
        [Authorize(Roles = "Saller")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [Authorize(Roles = "Saller")]
        [HttpPost]
        public async Task<IActionResult> Add(CreateProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                
                return View(viewModel);
            }
            await _productServices.AddProductAsync(viewModel);
            return RedirectToAction("Index", "Saller");
        }

        [HttpGet]
        public async Task<IActionResult> Product(int productId)
        {

            // Check if the user is logged in
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the Sign-In page if not logged in
                return RedirectToAction("SignIn", "Authentication");
            }

            // Check if the user has the "Buyer" role
            if (!User.IsInRole("Buyer"))
            {
                // Redirect to an unauthorized page or another view if the user is not a Buyer
                return RedirectToAction("SignIn", "Authentication");
            }

            var product = await _productServices.ProductAsync(productId);
            var viewModel = new PurchaseProductViewModel
            {
                Product = product,
                Buyer = new BuyerDetails()
            };
            viewModel.Buyer.Quantity = viewModel.Product.Quantity;
            viewModel.Buyer.NumOfSoldItems = viewModel.Product.NumOfSoldItems;
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Purchase(BuyerDetails viewModel)
        {
            viewModel.ZIPCode = "";
            var PurchaseOrder = await _productServices.PurchaseAsync(viewModel);
            return RedirectToAction("Receipt", "Purchase", new { id = PurchaseOrder.Id });
            // return RedirectToAction("Index");
        }
        [Authorize(Roles = "Saller")]
        public async Task<IActionResult> Update(int id)
        {
            var oldProduct = await _productServices.GetProductForUpdateAsync(id);
            return View(oldProduct);
        }
        [Authorize(Roles = "Saller")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductViewModel model)
        {
            if (model.HasOffer && (model.DiscountPrice == null || model.DiscountPrice >= model.Price))
            {
                ModelState.AddModelError("DiscountPrice", "Discount price must be less than the original price.");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var update = await _productServices.UpdateAsync(model);
            return RedirectToAction("Index", "Saller");
        }
        [Authorize(Roles = "Saller")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productServices.DelteAsync(id);
            return RedirectToAction("Index", "Saller");
        }
        [Authorize(Roles = "Saller")]
        public async Task<IActionResult> GetStatistics()
        {
            var products = await _productServices.GetBySallerIdAsync();
            return View(products);
        }

    }
}
