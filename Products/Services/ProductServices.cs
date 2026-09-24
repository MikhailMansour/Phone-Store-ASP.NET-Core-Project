using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Products.Helpers;
using Products.Infrastructure;
using Products.Models;
using Products.ViewModels.Product;
using System.Security.Claims;

namespace Products.Services
{
    public class ProductServices
    {
        private readonly ProductRepository _productRepository;
        private readonly FileServices _fileServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
       // private readonly BuyerRepository _buyerRepository;
     
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProductServices(ProductRepository productRepository, FileServices fileServices,
                               IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager
                               , SignInManager<ApplicationUser> signInManager)
        {
            _productRepository = productRepository;
            _fileServices = fileServices;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
           // _buyerRepository = buyerRepository;
            _signInManager = signInManager;
        }

        public async Task AddProductAsync(CreateProductViewModel viewModel)
        {
            var productImagesPathes = new List<string>();
            foreach (var image in viewModel.Images)
            {
                var imageName = await _fileServices.Upload(image, "images");
                productImagesPathes.Add(imageName);
            }

            var headerImage = await _fileServices.Upload(viewModel.HeaderImage, "images");
            var newProduct = viewModel.Adapt<Product>();
            newProduct.HasOffer = viewModel.HasOffer;
            newProduct.DiscountPrice = viewModel.HasOffer ? viewModel.DiscountPrice : null;

            newProduct.HeaderImage = headerImage;
            newProduct.IsBought = newProduct.Quantity > 0 ? false : true;
            newProduct.Images = new List<ProductImages>();
            int userId = 0;
            int.TryParse(_httpContextAccessor.HttpContext?.User?
                                 .FindFirstValue(ClaimTypes.NameIdentifier), out userId);
            newProduct.SellerId = userId;
            newProduct.NumOfSoldItems = 0;
            foreach (var image in productImagesPathes)
            {
                newProduct.Images.Add(new ProductImages
                {
                    ImagePath = image,
                    ProductId = newProduct.Id,
                });
            }
            var result = await _productRepository.AddAsync(newProduct);
            await _productRepository.Save();
        }

        public async Task<PaginationResult<ProductsViewModel>> ProductsAsync(int size, int index)
        {
            var products = await _productRepository.Products()
                          .Select(p => new ProductsViewModel
                          {
                              Id = p.Id,
                              Description = p.Description,
                              HeaderImage = p.HeaderImage,
                              Name = p.Name,
                              Price = p.Price,
                              IsBought = p.IsBought,
                              Brand = p.Brand,
                              NumOfSoldItems = p.NumOfSoldItems,
                              HasOffer=p.HasOffer
                          }).Where(c=>c.HasOffer!=true).Paginate(index, size);
            return products;
        }

        public async Task<ProductViewModel?> ProductAsync(int id)
        {
            var model = await _productRepository.Products()
                       .Where(p => p.Id == id)
                       .ProjectToType<ProductViewModel>()
                       .AsNoTracking()
                       .FirstOrDefaultAsync();
            return model;
        }

        public async Task<List<ProductsViewModel>> GetBySallerIdAsync()
        {
            int.TryParse(_httpContextAccessor.HttpContext?
                         .User.FindFirstValue(ClaimTypes.NameIdentifier), out int sallerId);
            var model = await _productRepository.Products()
           .Where(p => p.SellerId == sallerId)
           .ProjectToType<ProductsViewModel>()
           .ToListAsync();
            return model;
        }
        public async Task<UpdateProductViewModel> GetProductForUpdateAsync(int id)
        {
            var oldProduct = await _productRepository.ProductWithoutTrackingAsync(id);
            if (oldProduct is null)
            {
                return new UpdateProductViewModel { Quantity = 0 };
            }
            var model = oldProduct.Adapt<UpdateProductViewModel>();
            return model;
        }

        //public async Task<bool> UpdateAsync(UpdateProductViewModel viewModel)
        //{
        //    var oldProduct = await _productRepository.ProductAsync(viewModel.Id);
        //    var newProduct = viewModel.Adapt<Product>();
        //    if (viewModel.Header is not null && viewModel.Header.Length > 0)
        //    {
        //        newProduct.HeaderImage = await _fileServices.Upload(viewModel.Header, "images");
        //    }
        //    if (viewModel.Quantity > oldProduct.Quantity)
        //    {
        //        var countOfNewItems = viewModel.Quantity - viewModel.OriginalQuantity;
        //        newProduct.NumberOfExisitItems = newProduct.NumberOfExisitItems + countOfNewItems;
        //    }
        //    else if (viewModel.Quantity < viewModel.OriginalQuantity)
        //    {
        //        var countOfMiniusItems = viewModel.OriginalQuantity - viewModel.Quantity;
        //        newProduct.NumberOfExisitItems = newProduct.NumberOfExisitItems - countOfMiniusItems >= 0 ?
        //                                         newProduct.NumberOfExisitItems - countOfMiniusItems :
        //                                         0;
        //    }
        //    var result = await _productRepository.UpdateAsync(newProduct);
        //    await _productRepository.Save();
        //    return result;
        //}


        public async Task<bool> UpdateAsync(UpdateProductViewModel viewModel)
        {
            var oldProduct = await _productRepository.ProductAsync(viewModel.Id);
            if (oldProduct is not null)
            {
                var oldQuantity = oldProduct.Quantity;
                var oldNumOfSoldItems = oldProduct.NumOfSoldItems;
                viewModel.Adapt(oldProduct);
                if (viewModel.Header is not null && viewModel.Header.Length > 0)
                {
                    oldProduct.HeaderImage = await _fileServices.Upload(viewModel.Header, "images");
                }
                oldProduct.IsBought = oldProduct.Quantity > 0 ? false : true;
                oldProduct.NumOfSoldItems = oldNumOfSoldItems;
                oldProduct.HasOffer = viewModel.HasOffer;
                oldProduct.DiscountPrice = viewModel.DiscountPrice;
                var result = _productRepository.Update(oldProduct);
                await _productRepository.Save();
            }
            return true;
        }


        public async Task<bool> DelteAsync(int id)
        {
            var result = await _productRepository.DeleteAsync(id);
            await _productRepository.Save();
            return result;
        }
        [Authorize(Roles = "Buyer")]

        public async Task<ProductBuyer> PurchaseAsync(BuyerDetails buyerDetails)
        {

            int.TryParse(_httpContextAccessor.HttpContext?
                      .User.FindFirstValue(ClaimTypes.NameIdentifier), out int BuyerId);
            var product = await _productRepository.ProductAsync(buyerDetails.ProductId);
            //var buyer = await _buyerRepository.BuyerByEmailAsync(buyerDetails.Email);
            var PurchaseModel = buyerDetails.Adapt<ProductBuyer>();
            if (product.Quantity >= buyerDetails.Quantity)
            {
                
                product.NumOfSoldItems += buyerDetails.Quantity;
                if (product.NumOfSoldItems == product.Quantity)
                    product.IsBought = true;
                if(product.HasOffer)
                {
                    PurchaseModel.Price = product.DiscountPrice;

                }
                else
                {
                    PurchaseModel.Price = product.Price;
                }
                    //if (buyer == null)
                    //{
                    //    buyer = new Buyer
                    //    {
                    //        Email = buyerDetails.Email,
                    //        FullName = buyerDetails.FullName,
                    //        PhoneNumber = buyerDetails.PhoneNumber,
                    //    };
                    //    //PurchaseModel.Buyer = buyer;
                    //    var PurchaseResult = await _productRepository.AddProductBuyerAsync(PurchaseModel);
                    //    return PurchaseResult;
                    //}
                    //else
                    //{
                    PurchaseModel.UserId = BuyerId;
                    var PurchaseResult = await _productRepository.AddProductBuyerAsync(PurchaseModel);
                    return PurchaseResult;
               // }
                if (product.NumOfSoldItems == product.Quantity)
                    product.IsBought = true;
                _productRepository.Update(product);
                await _productRepository.Save(); 
                
            }
            return PurchaseModel;

        }



    }
}
