using System.ComponentModel.DataAnnotations;

namespace Products.ViewModels.Product
{
    public class UpdateProductViewModel: IValidatableObject
    {

        public int Id { get; set; }
        [Display(Name = "e.g. Iphone Pro Max")]
        [Required(ErrorMessage = "ProductName is Required")]
        public string ProductName { get; set; } = null!;
        [Display(Name = "e.g. Apple")]
        [Required(ErrorMessage = "Brand is Required")]

        public string Brand { get; set; } = null!;
        [Display(Name = "e.g. 999")]
        [Required(ErrorMessage = "Price is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Price Can not be Minus")]
        public double Price { get; set; }
        [Display(Name = "Detailed Description of The Product")]
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; } = null!;
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
        [Display(Name = "e.g. 240")]
        [Required(ErrorMessage = "Weight is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Weight Can not be Minus")]
        public int Weight { get; set; }

        [Display(Name = "e.g. 160.8 x 78.1 x 7.65")]
        [Required(ErrorMessage = "Dimensions is Required")]

        public string Dimensions { get; set; } = null!;

        [Display(Name = "e.g. A18 Bionic")]
        [Required(ErrorMessage = "Processor is Required")]
        public string Processor { get; set; } = null!;


        [Display(Name = "e.g. 8")]
        [Required(ErrorMessage = "RAM is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "RAM Can not be Minus")]

        public int RAM { get; set; }

        [Display(Name = "e.g. 128GB , 256GB , 512GB")]
        [Required(ErrorMessage = "Storage Options is Required")]

        public string StorageOptions { get; set; } = null!;

        [Display(Name = "e.g. IOS 18")]
        [Required(ErrorMessage = "Operating System is Required")]

        public string OperatingSystem { get; set; } = null!;

        [Display(Name = "e.g. Triple 48MP + 12MP + 12MP")]
        [Required(ErrorMessage = "Camera is Required")]
        public string Camera { get; set; } = null!;


        [Required(ErrorMessage = "Battery is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Battery Can not be Minus")]
        [Display(Name = "e.g. 4500")]
        public int Battery { get; set; }


        [Display(Name = "e.g. 6.7-inch Super Retina XDR")]
        [Required(ErrorMessage = "Display is Required")]
        public string Display { get; set; } = null!;



        [Display(Name = "e.g. Black , Silver , Gold")]
        [Required(ErrorMessage = "Available Colors is Required")]
        public string AvailableColors { get; set; } = null!;


        [Range(0, int.MaxValue, ErrorMessage = "Quantity Can not be Minus")]
        [Required(ErrorMessage = "Quantity Colors is Required")]
        public int Quantity { get; set; }
        public IFormFile? Header { get; set; }
        public string? HeaderImage { get; set; }
        public int NumOfSoldItems { get; set; }
        public bool HasOffer { get; set; }
        public double? DiscountPrice { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HasOffer)
            {
                if (DiscountPrice == null)
                {
                    yield return new ValidationResult("Discount Price is required when the product has an offer.", new[] { nameof(DiscountPrice) });
                }
                else if (DiscountPrice >= Price)
                {
                    yield return new ValidationResult("Discount Price must be less than the original Price.", new[] { nameof(DiscountPrice) });
                }
            }
        }
    }
}
