
using Microsoft.AspNetCore.Identity;

namespace Products.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        // Remove or comment this line
        // public string SSN { get; set; }


        //public string? SSN { get; set; } = null!;
        
            public String? PhotoPath { get; set; }
        public List<Product>? SallesProducts { get; set; } = new List<Product>();
        public List<ProductBuyer> ProductBuyers { get; set; } = new List<ProductBuyer>();

    }
}
