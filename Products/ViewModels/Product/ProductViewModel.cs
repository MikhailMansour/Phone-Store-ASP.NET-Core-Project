namespace Products.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
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
        public bool IsBought { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public int NumberOfExisitItems { get; set; }
        public int Quantity { get; set; }
        public string HeaderImage { get; set; } = null!;
        public int NumOfSoldItems { get; set; }
        public bool HasOffer { get; set; }
        public double? DiscountPrice { get; set; }

    }
}
