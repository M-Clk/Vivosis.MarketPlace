using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Data
{
    public class MarketPlaceDbContext :IdentityDbContext<SystemUser, SystemRole, int>
    {
        public MarketPlaceDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Store> Stores { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductCategory>()
                .HasKey(x => new { x.product_id, x.category_id });
            builder.Entity<ProductCategory>()
                .HasOne(x => x.Category)
                .WithMany(m => m.CategoryProducts)
                .HasForeignKey(x => x.category_id);
            builder.Entity<ProductCategory>()
                .HasOne(x => x.Product)
                .WithMany(e => e.ProductCategories)
                .HasForeignKey(x => x.product_id);

            builder.Entity<StoreCategory>()
                .HasKey(x => new { x.store_id, x.category_id });
            builder.Entity<StoreCategory>()
                .HasOne(x => x.Category)
                .WithMany(m => m.CategoryStores)
                .HasForeignKey(x => x.category_id);
            builder.Entity<StoreCategory>()
                .HasOne(x => x.Store)
                .WithMany(e => e.StoreCategories)
                .HasForeignKey(x => x.store_id);

            builder.Entity<StoreProduct>()
                .HasKey(x => new { x.store_id, x.product_id });
            builder.Entity<StoreProduct>()
                .HasOne(x => x.Product)
                .WithMany(m => m.ProductStores)
                .HasForeignKey(x => x.product_id);
            builder.Entity<StoreProduct>()
                .HasOne(x => x.Store)
                .WithMany(e => e.StoreProducts)
                .HasForeignKey(x => x.store_id);

            builder.Entity<Store>().HasIndex(s => s.api_key).IsUnique();
            builder.Entity<SystemUser>().HasIndex(s => s.Server).IsUnique();
        }
    }
}
