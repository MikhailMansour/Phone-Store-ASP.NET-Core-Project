using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Products.Helpers;
using Products.Infrastructure;
using Products.Models;
using Products.Services;

namespace Products
{
    namespace Products.Models
    {
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            // ... (باقي خصائص المنتج الخاصة بك) ...

            // أضف هذا السطر هنا داخل كلاس Product:
            public List<Review>? Reviews { get; set; } = new List<Review>();
        }
    }
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("Dev")));

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Configure identity options if needed
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationContext>()
              .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Authentication/SignIn";
            });

            builder.Services.AddScoped<ProductRepository, ProductRepository>();
            builder.Services.AddScoped<FileServices, FileServices>();
            builder.Services.AddScoped<ProductServices, ProductServices>();
            builder.Services.AddScoped<DataSeeder, DataSeeder>();
            builder.Services.AddTransient<DataSeeder>();
            builder.Services.AddMapster();
            builder.Services.RegisterMapsterConfiguration();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication(); // تمت إضافة خطوة التحقق من الهوية لضمان عمل [Authorize] بشكل سليم
            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seeder = services.GetRequiredService<DataSeeder>();
                await seeder.CreateAllRoles();
            }

            app.Run();
        }
    }
}
