using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;
using Products.Infrastructure;
using Products.Models;

namespace Products.Helpers
{
    public class DataSeeder
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationContext _applicationContext;

        private List<ApplicationUser> Sallers { get; set; }
        private List<ProductBuyer> productBuyers { get; set; }
        private List<Product> Products { get; set; }

        public DataSeeder(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager
                          , ApplicationContext applicationContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _applicationContext = applicationContext;
        }

        public DataSeeder(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        private async Task CreateSallerRole()
        {
            var isExsist = await _roleManager.FindByNameAsync("Saller") == null ? false : true;
            if (!isExsist)
            {
                var sallerRole = new ApplicationRole { Name = "Saller" };
                await _roleManager.CreateAsync(sallerRole);
            }
        }
        public async Task CreateAllRoles()
        {
            //var isExsistA = await _roleManager.FindByNameAsync("Admin") == null ? false : true;
            //if (!isExsist)
            //{
            //    var sallerRole = new ApplicationRole { Name = "Admin" };
            //    await _roleManager.CreateAsync(sallerRole);
            //}
            string[] roles = { "Admin", "Saller", "Buyer" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new ApplicationRole { Name = role });
                }
            }
        }
        private void GenerateSallers()
        {
            Sallers = new List<ApplicationUser>
            {
                 new ApplicationUser
                 {
                     UserName = "saller1",
                     Email = "saller1@example.com",
                     //SSN = "123-45-6789",
                 },
                 new ApplicationUser
                 {
                     UserName = "saller2",
                     Email = "saller2@example.com",
                     //SSN = "987-65-4321",
                 },
                 new ApplicationUser
                 {
                     UserName = "saller3",
                     Email = "saller3@example.com",
                     //SSN = "987-65-4321",
                 },
            };
        }
        private void GenerateProducts()
        {
            Products = new List<Product> {
                new Product
                {
                   Name = "iPhone 15 Pro Max",
                   SellerId = 1,
                   Brand = "Apple",
                   Description = "Latest iPhone with A17 Pro chip and titanium design",
                   Price = 1199.99,
                   Weight = 221,
                   Dimensions = "159.9 x 76.7 x 8.25 mm",
                   Processor = "A17 Pro",
                   Ram = 8,
                   Storage = "256GB",
                   Os = "iOS 17",
                   Camera = "48MP Main + 12MP Ultra Wide + 12MP Telephoto",
                   Battery = 4422,
                   Display = "6.7-inch Super Retina XDR",
                   Colors = "Black Titanium, White Titanium, Blue Titanium, Natural Titanium",
                   IsBought = false,
                   Quantity = 100,
                   HeaderImage = "iphone15pro_header.jpg",
                   NumOfSoldItems = 0,
                   Images = new List<ProductImages>
                   {
                       new ProductImages { ImagePath = "iphone15pro_1.jpg" },
                       new ProductImages { ImagePath = "iphone15pro_2.jpg" },
                       new ProductImages { ImagePath = "iphone15pro_3.jpg" }
                   }
                },
                new Product
                {
                    Name = "Samsung Galaxy S23 Ultra",
                    SellerId = 1,
                    Brand = "Samsung",
                    Description = "Flagship Android phone with 200MP camera and S Pen",
                    Price = 1299.99,
                    Weight = 234,
                    Dimensions = "163.4 x 78.1 x 8.9 mm",
                    Processor = "Snapdragon 8 Gen 2",
                    Ram = 12,
                    Storage = "512GB",
                    Os = "Android 13",
                    Camera = "200MP + 12MP + 10MP + 10MP",
                    Battery = 5000,
                    Display = "6.8-inch Dynamic AMOLED 2X",
                    Colors = "Phantom Black, Cream, Green, Lavender",
                    IsBought = false,
                    Quantity = 80,
                    HeaderImage = "s23ultra_header.jpg",
                    NumOfSoldItems = 0,
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "s23ultra_1.jpg" },
                        new ProductImages { ImagePath = "s23ultra_2.jpg" },
                        new ProductImages { ImagePath = "s23ultra_3.jpg" }
                    }
                },
                
                // Laptops
                new Product
                {
                    Name = "MacBook Pro 16\" M2 Max",
                    SellerId = 2,
                    Brand = "Apple",
                    Description = "Professional laptop with M2 Max chip and Liquid Retina XDR display",
                    Price = 2499.00,
                    Weight = 2200,
                    Dimensions = "355.7 x 248.1 x 16.8 mm",
                    Processor = "Apple M2 Max",
                    Ram = 32,
                    Storage = "1TB",
                    Os = "macOS Ventura",
                    Camera = "1080p FaceTime HD",
                    Battery = 100,
                    Display = "16.2-inch Liquid Retina XDR",
                    Colors = "Space Gray, Silver",
                    IsBought = false,
                    Quantity = 50,
                    HeaderImage = "macbookpro16_header.jpg",
                    NumOfSoldItems = 0,
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "macbookpro16_1.jpg" },
                        new ProductImages { ImagePath = "macbookpro16_2.jpg" },
                        new ProductImages { ImagePath = "macbookpro16_3.jpg" }
                    }
                },
                new Product
                {
                    Name = "Dell XPS 17",
                    SellerId = 2,
                    Brand = "Dell",
                    Description = "Premium Windows laptop with 4K touch display",
                    Price = 2199.99,
                    Weight = 2300,
                    Dimensions = "374.5 x 248.1 x 19.5 mm",
                    Processor = "Intel Core i9-13900H",
                    Ram = 32,
                    Storage = "2TB",
                    Os = "Windows 11 Pro",
                    Camera = "720p HD",
                    Battery = 97,
                    Display = "17-inch 4K UHD+",
                    Colors = "Platinum Silver",
                    IsBought = false,
                    Quantity = 40,
                    HeaderImage = "xps17_header.jpg",
                    NumOfSoldItems = 0,
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "xps17_1.jpg" },
                        new ProductImages { ImagePath = "xps17_2.jpg" },
                        new ProductImages { ImagePath = "xps17_3.jpg" }
                    }
                },
                
                // Tablets
                new Product
                {
                    Name = "iPad Pro 12.9\" M2",
                    SellerId = 3,
                    Brand = "Apple",
                    Description = "Professional tablet with M2 chip and Liquid Retina XDR display",
                    Price = 1099.00,
                    Weight = 682,
                    Dimensions = "280.6 x 214.9 x 6.4 mm",
                    Processor = "M2",
                    Ram = 8,
                    Storage = "256GB",
                    Os = "iPadOS 16",
                    Camera = "12MP Wide + 10MP Ultra Wide",
                    Battery = 10758,
                    Display = "12.9-inch Liquid Retina XDR",
                    Colors = "Space Gray, Silver",
                    IsBought = false,
                    Quantity = 60,
                    HeaderImage = "ipadpro_header.jpg",
                    NumOfSoldItems = 0,
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "ipadpro_1.jpg" },
                        new ProductImages { ImagePath = "ipadpro_2.jpg" },
                        new ProductImages { ImagePath = "ipadpro_3.jpg" }
                    }
                },
                new Product
                {
                    Name = "iPhone 15 Pro Max",
                    SellerId = 1,
                    Brand = "Apple",
                    Description = "Titanium design, A17 Pro chip, 5x telephoto camera",
                    Price = 1199.99,
                    Weight = 221,
                    Dimensions = "159.9 x 76.7 x 8.25 mm",
                    Processor = "A17 Pro",
                    Ram = 8,
                    Storage = "256GB",
                    Os = "iOS 17",
                    Camera = "48MP + 12MP + 12MP",
                    Battery = 4422,
                    Display = "6.7\" Super Retina XDR",
                    Colors = "Black Titanium, Natural Titanium",
                    Quantity = 85,
                    HeaderImage = "iphone15pro_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "iphone15pro_1.jpg" },
                        new ProductImages { ImagePath = "iphone15pro_2.jpg" }
                    }
                },
                new Product
                {
                    Name = "Samsung Galaxy S24 Ultra",
                    SellerId = 1,
                    Brand = "Samsung",
                    Description = "Flat display, titanium frame, 200MP camera",
                    Price = 1299.99,
                    Weight = 233,
                    Dimensions = "162.3 x 79 x 8.6 mm",
                    Processor = "Snapdragon 8 Gen 3",
                    Ram = 12,
                    Storage = "512GB",
                    Os = "Android 14",
                    Camera = "200MP + 50MP + 10MP + 12MP",
                    Battery = 5000,
                    Display = "6.8\" Dynamic AMOLED 2X",
                    Colors = "Titanium Black, Titanium Gray",
                    Quantity = 72,
                    HeaderImage = "s24ultra_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "s24ultra_1.jpg" },
                        new ProductImages { ImagePath = "s24ultra_2.jpg" }
                    }
                },
                new Product
                {
                    Name = "Google Pixel 8 Pro",
                    SellerId = 1,
                    Brand = "Google",
                    Description = "Tensor G3 chip, best-in-class computational photography",
                    Price = 999.00,
                    Weight = 213,
                    Dimensions = "162.6 x 76.5 x 8.8 mm",
                    Processor = "Google Tensor G3",
                    Ram = 12,
                    Storage = "128GB",
                    Os = "Android 14",
                    Camera = "50MP + 48MP + 48MP",
                    Battery = 5050,
                    Display = "6.7\" LTPO OLED",
                    Colors = "Obsidian, Porcelain",
                    Quantity = 58,
                    HeaderImage = "pixel8pro_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "pixel8pro_1.jpg" }
                    }
                },
                new Product
                {
                    Name = "1",
                    SellerId = 2,
                    Brand = "OnePlus",
                    Description = "Hasselblad camera, 100W fast charging",
                    Price = 799.99,
                    Weight = 220,
                    Dimensions = "164.3 x 75.8 x 9.2 mm",
                    Processor = "Snapdragon 8 Gen 3",
                    Ram = 16,
                    Storage = "256GB",
                    Os = "OxygenOS 14",
                    Camera = "50MP + 64MP + 48MP",
                    Battery = 5400,
                    Display = "6.82\" LTPO AMOLED",
                    Colors = "Flowy Emerald, Silky Black",
                    Quantity = 64,
                    HeaderImage = "oneplus12_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "oneplus12_1.jpg" },
                        new ProductImages { ImagePath = "oneplus12_2.jpg" }
                    }
                },
                
                // ========== LAPTOPS ==========
                new Product
                {
                    Name = "MacBook Pro 16\" M3 Max",
                    SellerId = 2,
                    Brand = "Apple",
                    Description = "Space Black finish, 40-core GPU, 128GB RAM option",
                    Price = 3499.00,
                    Weight = 2200,
                    Dimensions = "355.7 x 248.1 x 16.8 mm",
                    Processor = "M3 Max",
                    Ram = 48,
                    Storage = "1TB",
                    Os = "macOS Sonoma",
                    Display = "16.2\" Liquid Retina XDR",
                    Colors = "Space Black, Silver",
                    Quantity = 32,
                    HeaderImage = "mbp16m3_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "mbp16m3_1.jpg" }
                    }
                },
                new Product
                {
                    Name = "Dell XPS 15 (2024)",
                    SellerId = 2,
                    Brand = "Dell",
                    Description = "OLED touch display, latest Intel Core processors",
                    Price = 2299.99,
                    Weight = 1900,
                    Dimensions = "344.4 x 230.1 x 18 mm",
                    Processor = "Intel Core i9-14900H",
                    Ram = 32,
                    Storage = "2TB",
                    Os = "Windows 11 Pro",
                    Display = "15.6\" 4K OLED",
                    Colors = "Platinum Silver",
                    Quantity = 28,
                    HeaderImage = "xps15_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "xps15_1.jpg" }
                    }
                },
                new Product
                {
                    Name = "Lenovo ThinkPad X1 Carbon Gen 11",
                    SellerId = 2,
                    Brand = "Lenovo",
                    Description = "Ultra-light business laptop with military-grade durability",
                    Price = 1899.00,
                    Weight = 1110,
                    Dimensions = "315.6 x 222.5 x 15.4 mm",
                    Processor = "Intel Core i7-1365U",
                    Ram = 16,
                    Storage = "1TB",
                    Os = "Windows 11 Pro",
                    Display = "14\" WUXGA IPS",
                    Colors = "Black",
                    Quantity = 45,
                    HeaderImage = "thinkpad_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "thinkpad_1.jpg" }
                    }
                },
                new Product
                {
                    Name = "ASUS ROG Zephyrus G16",
                    SellerId = 2,
                    Brand = "ASUS",
                    Description = "Gaming laptop with Mini LED display, RTX 4090",
                    Price = 2999.99,
                    Weight = 2100,
                    Dimensions = "354 x 264 x 19.9 mm",
                    Processor = "Intel Core i9-13900H",
                    Ram = 32,
                    Storage = "2TB",
                    Os = "Windows 11 Home",
                    Display = "16\" Mini LED 240Hz",
                    Colors = "Eclipse Gray",
                    Quantity = 18,
                    HeaderImage = "zephyrus_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "zephyrus_1.jpg" }
                    }
                },
                
                // ========== TABLETS ==========
                new Product
                {
                    Name = "iPad Pro 12.9\" M2",
                    SellerId = 2,
                    Brand = "Apple",
                    Description = "Liquid Retina XDR display, Thunderbolt support",
                    Price = 1099.00,
                    Weight = 682,
                    Dimensions = "280.6 x 214.9 x 6.4 mm",
                    Processor = "M2",
                    Ram = 8,
                    Storage = "256GB",
                    Os = "iPadOS 17",
                    Display = "12.9\" Liquid Retina XDR",
                    Colors = "Space Gray, Silver",
                    Quantity = 53,
                    HeaderImage = "ipadpro_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "ipadpro_1.jpg" }
                    }
                },
                new Product
                {
                    Name = "Samsung Galaxy Tab S9 Ultra",
                    SellerId = 2,
                    Brand = "Samsung",
                    Description = "14.6\" AMOLED, included S Pen, IP68 rating",
                    Price = 1199.99,
                    Weight = 732,
                    Dimensions = "326.4 x 208.6 x 5.5 mm",
                    Processor = "Snapdragon 8 Gen 2",
                    Ram = 12,
                    Storage = "512GB",
                    Os = "Android 13",
                    Display = "14.6\" Dynamic AMOLED 2X",
                    Colors = "Graphite, Beige",
                    Quantity = 37,
                    HeaderImage = "tabs9ultra_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "tabs9ultra_1.jpg" }
                    }
                },
                
                // ========== SMARTWATCHES ==========
                new Product
                {
                    Name = "Apple Watch Ultra 2",
                    SellerId = 3,
                    Brand = "Apple",
                    Description = "Titanium case, precision dual-frequency GPS",
                    Price = 799.00,
                    Weight = 61,
                    Dimensions = "49 x 44 x 14.4 mm",
                    Processor = "Apple S9",
                    Os = "watchOS 10",
                    Display = "1.92\" LTPO OLED",
                    Colors = "Titanium",
                    Quantity = 89,
                    HeaderImage = "watchultra_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "watchultra_1.jpg" }
                    }
                },
                new Product
                {
                    Name = "Samsung Galaxy Watch 6 Classic",
                    SellerId = 3,
                    Brand = "Samsung",
                    Description = "Rotating bezel, Wear OS 4, sapphire crystal",
                    Price = 429.99,
                    Weight = 59,
                    Dimensions = "46.5 x 46.5 x 10.9 mm",
                    Processor = "Exynos W930",
                    Os = "Wear OS 4",
                    Display = "1.5\" Super AMOLED",
                    Colors = "Black, Silver",
                    Quantity = 76,
                    HeaderImage = "watch6_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "watch6_1.jpg" }
                    }
                },
                
                // ========== ACCESSORIES ==========
                new Product
                {
                    Name = "AirPods Pro (2nd Gen)",
                    SellerId = 3,
                    Brand = "Apple",
                    Description = "Active Noise Cancellation, USB-C charging",
                    Price = 249.00,
                    Weight = 5,
                    Dimensions = "30.9 x 21.8 x 24 mm (each)",
                    Colors = "White",
                    Quantity = 124,
                    HeaderImage = "airpods_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "airpods_1.jpg" }
                    }
                },
                new Product
                {
                    Name = "Sony WH-1000XM5",
                    SellerId = 3,
                    Brand = "Sony",
                    Description = "Industry-leading noise cancellation",
                    Price = 399.99,
                    Weight = 250,
                    Dimensions = "185 x 168 x 72 mm (folded)",
                    Battery = 30,
                    Colors = "Black, Silver",
                    Quantity = 68,
                    HeaderImage = "sonyxm5_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "sonyxm5_1.jpg" }
                    }
                },
                new Product
                {
                    Name = "Logitech MX Keys Mini",
                    SellerId = 3,
                    Brand = "Logitech",
                    Description = "Compact wireless keyboard for multi-device use",
                    Price = 99.99,
                    Weight = 506,
                    Dimensions = "293 x 136 x 20 mm",
                    Colors = "Graphite, Pale Gray",
                    Quantity = 92,
                    HeaderImage = "mxkeys_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "mxkeys_1.jpg" }
                    }
                },
                new Product
                {
                    Name = "Anker 737 Power Bank",
                    SellerId = 3,
                    Brand = "Anker",
                    Description = "140W GaN charger with 24,000mAh capacity",
                    Price = 179.99,
                    Weight = 355,
                    Dimensions = "108 x 74 x 30 mm",
                    Battery = 24000,
                    Colors = "Black",
                    Quantity = 41,
                    HeaderImage = "anker737_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "anker737_1.jpg" }
                    }
                },
                
                // ========== SPECIALTY ITEMS ==========
                new Product
                {
                    Name = "DJI Mavic 3 Pro",
                    SellerId = 3,
                    Brand = "DJI",
                    Description = "Tri-camera drone with 5.1K video",
                    Price = 2199.00,
                    Weight = 958,
                    Dimensions = "231.1 x 98 x 95.4 mm (folded)",
                    Camera = "20MP + 70mm + 166mm",
                    Battery = 5000,
                    Colors = "Gray",
                    Quantity = 15,
                    HeaderImage = "mavic3_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "mavic3_1.jpg" }
                    }
                },
                new Product
                {
                    Name = "Kindle Oasis (2023)",
                    SellerId = 3,
                    Brand = "Amazon",
                    Description = "7\" e-reader with page-turn buttons",
                    Price = 249.99,
                    Weight = 188,
                    Dimensions = "159 x 141 x 3.4-8.4 mm",
                    Display = "7\" 300ppi E Ink",
                    Colors = "Graphite, Champagne",
                    Quantity = 57,
                    HeaderImage = "oasis_header.jpg",
                    Images = new List<ProductImages>
                    {
                        new ProductImages { ImagePath = "oasis_1.jpg" }
                    }
                }
            };
        }

        public async Task SeedData()
        {

           // await CreateSallerRole();
            await CreateAllRoles();
            //GenerateSallers();
            //GenerateProducts();
            //await SeedSaller();
            //await SeedProducts();
        }

        private async Task SeedSaller()
        {
            foreach (var item in Sallers)
            {
                var isExsist = await _userManager.FindByEmailAsync(item.Email) == null ? false : true;
                if (!isExsist)
                {
                    await _userManager.CreateAsync(item, $"{item.Email}");
                    await _userManager.AddToRoleAsync(item, "Saller");
                }
            }
        }

        private async Task SeedProducts()
        {
            if (!_applicationContext.Products.Any())
            {
                foreach (var item in Products)
                {
                    await _applicationContext.Products.AddAsync(item);
                }
            }
        }


    }
}
