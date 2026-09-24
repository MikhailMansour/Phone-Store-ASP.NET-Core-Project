using Microsoft.EntityFrameworkCore;
using Products.Models;

namespace Products.Infrastructure
{
    public class ProductRepository
    {
        private readonly ApplicationContext _ApplicationContext;
        private readonly DbSet<Product> _Products;

        public ProductRepository(ApplicationContext applicationContext)
        {
            _ApplicationContext = applicationContext;
            _Products = _ApplicationContext.Products;
        }

        public async Task<bool> AddAsync(Product product)
        {
            var addedResult = await _Products.AddAsync(product);
            return true;
        }

        public IQueryable<Product> Products()
        {
            return _Products.AsNoTracking();
        }
        public async Task<Product> ProductAsync(int id)
        {
            return await _Products.SingleOrDefaultAsync(p => p.Id == id);
        }

        public bool Update(Product product)
        {
            var isChanged = true;
            //_ApplicationContext.Update(product);
            _ApplicationContext.Entry(product).State = EntityState.Modified;
            isChanged = true;
            return isChanged;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var isDelted = false;
            var oldProd = await this.ProductAsync(id);
            if (oldProd == null)
                return isDelted;
            _ApplicationContext.Remove(oldProd);
            isDelted = true;
            return isDelted;
        }

        //public async Task AddProductBuyerAsync(ProductBuyer productBuyer)
        //{
        //    await _ApplicationContext.ProductsBuyers.AddAsync(productBuyer);
        //}
        public async Task<ProductBuyer> AddProductBuyerAsync(ProductBuyer productBuyer)
        {
            await _ApplicationContext.ProductsBuyers.AddAsync(productBuyer);
            await _ApplicationContext.SaveChangesAsync(); // This ensures the ID is generated
            return productBuyer;
        }
        public async Task Save()
        {
            await _ApplicationContext.SaveChangesAsync();
        }

        public async Task<Product?> ProductWithoutTrackingAsync(int id)
        {
            var oldProduct = await _Products.AsNoTracking()
                                      .SingleOrDefaultAsync(p => p.Id == id);
            return oldProduct;
        }

    }
}
