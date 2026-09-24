using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Products.Models;
using System.Reflection;

namespace Products.Infrastructure
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductBuyer> ProductsBuyers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // يفضل وضعها في البداية

            // منع تداخل الحذف المتتالي لجدول ProductBuyer
            builder.Entity<ProductBuyer>()
                .HasOne(pb => pb.Product)
                .WithMany()
                .HasForeignKey(pb => pb.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // إضافة قاعدة لمنع تداخل الحذف المتتالي لجدول التقييمات (Reviews) مع المنتجات
            builder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany()
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}