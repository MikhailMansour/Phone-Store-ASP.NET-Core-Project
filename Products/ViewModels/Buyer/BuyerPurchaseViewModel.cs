namespace Products.ViewModels.Buyer
{
    // ViewModels/BuyerPurchaseViewModel.cs
    public class BuyerPurchaseViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        
        public string ShippingStreet { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingZipCode { get; set; }
        public string ShippingCountry { get; set; }
        public string PaymentMethod { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }
        public string NameOnCard { get; set; }
        public string BuyerLoginId { get; set; }
    }

}
