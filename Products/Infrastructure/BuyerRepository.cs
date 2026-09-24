//using Microsoft.EntityFrameworkCore;
//using Products.Models;

//namespace Products.Infrastructure
//{
//    public class BuyerRepository
//    {
//        private readonly ApplicationContext _applicationContext;
//        private readonly DbSet<Buyer> _buyers;

//        public BuyerRepository(ApplicationContext applicationContext)
//        {
//            _applicationContext = applicationContext;
//            _buyers = _applicationContext.Buyers;
//        }

//        public async Task<Buyer?> BuyerByEmailAsync(string email)
//        {
//            var buyer = await _buyers.Where(b => b.Email == email)
//                               .FirstOrDefaultAsync();
//            return buyer;
//        }

//        public async Task<Buyer> AddAsync(Buyer buyer)
//        {
//            var result = await _applicationContext.AddAsync(buyer);
//            return result.Entity;
//        }

//    }
//}
