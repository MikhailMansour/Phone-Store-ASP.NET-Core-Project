using Products.Models;
using System.ComponentModel.DataAnnotations;

namespace Products.ViewModels.Product
{
    public class BuyerDetails
    {
        public string FullName { get; set; } = null!;
        public int ProductId { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int Quantity { get; set; }
        public int NumOfSoldItems { get; set; }

        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ZIPCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        [Display(Name = "1234 5678 9012 3456")]
        public string CardNumber { get; set; } = null!;
        public DateOnly ExpiryDate { get; set; }
        public int CVV { get; set; }
        public string NameonCard { get; set; } = null!;
        public PaymentMethod PaymentMethod { get; set; }
    }
}
