namespace Products.Models
{
    public class ProductBuyer
    {
        public int Id { get; set; }
        public Product? Product { get; set; }
        //public Buyer? Buyer { get; set; }

        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
        public Address Address { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public BankCard? BankCard { get; set; }
        public double? Price { get; set; }

        public int UserId { get; set; }

        public ApplicationUser User { get; set; } = null!;

    }
}
