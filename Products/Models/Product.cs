namespace Products.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SellerId { get; set; }
        public ApplicationUser? Seller { get; set; }
        public string Brand { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public int? Weight { get; set; }
        public string Dimensions { get; set; } = null!;
        public string Processor { get; set; } = null!;
        public int? Ram { get; set; }
        public string Storage { get; set; } = null!;
        public string Os { get; set; } = null!;
        public string Camera { get; set; } = null!;
        public int? Battery { get; set; }
        public string Display { get; set; } = null!;
        public string Colors { get; set; } = null!;
        //public List<Buyer>? Buyers { get; set; } = new List<Buyer>();
        public bool IsBought { get; set; }
        public List<ProductImages>? Images { get; set; } = new List<ProductImages>();
        public int Quantity { get; set; }
        public string HeaderImage { get; set; } = null!;
        public int NumOfSoldItems { get; set; }
        public bool HasOffer { get; set; } = false;
        public double? DiscountPrice { get; set; }

    }
}
