using Microsoft.EntityFrameworkCore;
using Products.ViewModels.Product;

namespace Products.Helpers
{
    public static class Pagination
    {
        public static async Task<PaginationResult<T>> Paginate<T>(this IQueryable<T> values, int index, int size = 5)
        {
            var totalEntities = await values.CountAsync();
            if (totalEntities == 0)
            {
                return new PaginationResult<T>();
            }
            size = size > 0 ? size : 10;
            size = size > totalEntities ? totalEntities : size;
            index = index > 0 ? index : 1;
            var totalPages = (int)Math.Ceiling((decimal)totalEntities / size);
            var currentIndex = index > totalPages ? totalPages : index;
            var numberOfExcluded = (currentIndex - 1) * size;
            var result = await values.Skip(numberOfExcluded).Take(size).ToListAsync();
            return new PaginationResult<T>
            {
                PageSize = size,
                PageNumber = currentIndex,
                TotalCount = totalPages,
                //TotalCount = totalEntities,
                Items = result
            };
        }
    }
}
