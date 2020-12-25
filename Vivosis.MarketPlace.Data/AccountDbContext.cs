using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Data
{
    public class AccountDbContext :IdentityDbContext<SystemUser, SystemRole, int>
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreUser> StoreUsers { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<CategoryFromStore> CategoryFromStores { get; set; }
        public DbSet<CategoryFromStoreAttribute> CategoryFromStoreAttributes { get; set; }
        public DbSet<CategoryToAttributeFromStore> CategoryToAttributeFromStores { get; set; }
        public DbSet<CategoryFromStoreAttributeValue> CategoryFromStoreAttributeValues { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StoreUser>()
                .HasKey(x => new { x.store_id, x.user_id });
            modelBuilder.Entity<StoreUser>()
                .HasOne(x => x.User)
                .WithMany(m => m.UserStores)
                .HasForeignKey(x => x.user_id);
            modelBuilder.Entity< StoreUser> ()
                .HasOne(x => x.Store)
                .WithMany(e => e.UserStores)
                .HasForeignKey(x => x.store_id);

            modelBuilder.Entity<Store>().Ignore(s => s.StoreCategories);
            modelBuilder.Entity<Store>().Ignore(s => s.StoreProducts);
        }
    }
}
