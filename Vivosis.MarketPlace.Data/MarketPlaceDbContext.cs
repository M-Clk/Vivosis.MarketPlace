using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Data
{
    public class MarketPlaceDbContext :DbContext
    {
        public MarketPlaceDbContext() :base() { }
        public MarketPlaceDbContext(DbContextOptions<MarketPlaceDbContext> options) :base(options) { }

        #region DbSets

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryOption> CategoryOptions { get; set; }
        public DbSet<CategoryOptionValue> CategoryOptionValues { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<OptionValue> OptionValues { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<ProductOptionValue> ProductOptionValues { get; set; }
        public DbSet<StoreCategory> StoreCategories { get; set; }
        public DbSet<StoreProduct> StoreProducts { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ShipmentTemplate> ShipmentTemplates { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Declare Relations

            builder.Entity<ProductCategory>()
                .HasOne(x => x.Category)
                .WithMany(m => m.CategoryProducts)
                .HasForeignKey(x => x.category_id);
            builder.Entity<ProductCategory>()
                .HasOne(x => x.Product)
                .WithMany(e => e.ProductCategories)
                .HasForeignKey(x => x.product_id);

            builder.Entity<StoreCategory>()
                .HasOne(x => x.Category)
                .WithMany(m => m.CategoryStores)
                .HasForeignKey(x => x.category_id);
            builder.Entity<StoreCategory>()
                .HasOne(x => x.Store)
                .WithMany(e => e.StoreCategories)
                .HasForeignKey(x => x.store_id);

            builder.Entity<StoreProduct>()
                .HasOne(x => x.Product)
                .WithMany(m => m.ProductStores)
                .HasForeignKey(x => x.product_id);
            builder.Entity<StoreProduct>()
                .HasOne(x => x.Store)
                .WithMany(e => e.StoreProducts)
                .HasForeignKey(x => x.store_id);

            builder.Entity<StoreUser>()
                .HasOne(x => x.User)
                .WithMany(m => m.UserStores)
                .HasForeignKey(x => x.user_id);
            builder.Entity<StoreUser>()
                .HasOne(x => x.Store)
                .WithMany(e => e.UserStores)
                .HasForeignKey(x => x.store_id);

            builder.Entity<OptionValue>()
                .HasOne(x => x.Option)
                .WithMany(m => m.OptionValues)
                .HasForeignKey(x => x.option_id);

            builder.Entity<ProductOption>()
                .HasOne(x => x.Option)
                .WithMany(m => m.ProductOptions)
                .HasForeignKey(x => x.option_id);
            builder.Entity<ProductOption>()
                .HasOne(x => x.Product)
                .WithMany(m => m.ProductOptions)
                .HasForeignKey(x => x.product_id);
            
            builder.Entity<CategoryOption>()
                .HasOne(x => x.Option)
                .WithMany(m => m.CategoryOptions)
                .HasForeignKey(x => x.option_id);
            builder.Entity<CategoryOption>()
                .HasOne(x => x.StoreCategory)
                .WithMany(m => m.CategoryOptions)
                .HasForeignKey(x => x.store_category_id);

            builder.Entity<ProductOptionValue>()
                .HasOne(x => x.ProductOption)
                .WithMany(m => m.ProductOptionValues)
                .HasForeignKey(x => x.product_option_id);
            builder.Entity<ProductOptionValue>()
                .HasOne(x => x.OptionValue)
                .WithMany(m => m.ProductOptionValues)
                .HasForeignKey(x => x.option_value_id);

            builder.Entity<CategoryOptionValue>()
                .HasOne(x => x.CategoryOption)
                .WithMany(m => m.CategoryOptionValues)
                .HasForeignKey(x => x.category_option_id);
            builder.Entity<CategoryOptionValue>()
                .HasOne(x => x.OptionValue)
                .WithMany(m => m.CategoryOptionValues)
                .HasForeignKey(x => x.option_value_id);

            #endregion

            #region Declare Keys

            builder.Entity<StoreCategory>()
                .HasIndex(x => new { x.store_id, x.category_id }).IsUnique();

            builder.Entity<StoreProduct>()
                .HasKey(x => new { x.store_id, x.product_id });

            builder.Entity<StoreUser>()
                .HasKey(x => new { x.store_id, x.user_id });

            builder.Entity<ProductCategory>()
                .HasKey(x => new { x.product_id, x.category_id });

            builder.Entity<CategoryOptionValue>()
                .HasIndex(p => new { p.category_option_id, p.option_value_id }).IsUnique();
            
            builder.Entity<ProductOptionValue>()
                .HasIndex(p => new { p.product_option_id, p.option_value_id }).IsUnique();

            builder.Entity<StoreUser>().HasIndex(s => s.api_key).IsUnique();

            #endregion

            #region Declare To Be Ignored Properties

            builder.Entity<SystemUser>().Ignore(s => s.Settings);
            builder.Ignore<CategoryFromStore>();
            builder.Ignore<CategoryFromStoreAttribute>();
            builder.Ignore<CategoryFromStoreAttributeValue>();
            builder.Ignore<CategoryToAttributeFromStore>();
            builder.Ignore<StoreUser>();
            builder.Ignore<SystemUser>();

            #endregion

            #region Declare Formating All Of Decimal Properties

            var entitiesHasDecimalProperty = builder.Model.GetEntityTypes().Where(prop => prop.ClrType.GetProperties().Any(p => p.PropertyType.IsAssignableFrom(typeof(decimal))));
            foreach(var entityType in entitiesHasDecimalProperty)
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType.IsAssignableFrom(typeof(decimal)) && !p.CustomAttributes.Any(cA => cA.AttributeType.IsAssignableFrom(typeof(NotMappedAttribute))));
                foreach(var prop in properties)
                    builder.Entity(entityType.ClrType).Property(prop.Name).HasColumnType("decimal(18,2)");
            }

            #endregion

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseMySql("Server=localhost; Database=DefaultMarketPlaceDbForMigrations; Uid=root; Pwd=;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
