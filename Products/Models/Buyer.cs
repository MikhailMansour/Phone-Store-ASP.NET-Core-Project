using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Models
{
    public class Buyer
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; } // ApplicationUser يرث من IdentityUser

        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Product> products { get; set; } = new List<Product>();
    }
}
