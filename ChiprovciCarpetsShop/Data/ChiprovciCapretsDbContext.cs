using ChiprovciCarpetsShop.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChiprovciCarpetsShop.Data
{
    public class ChiprovciCapretsDbContext : IdentityDbContext
    {
        public ChiprovciCapretsDbContext(DbContextOptions<ChiprovciCapretsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Dealer> Dealers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Product>()
                .HasOne(p => p.Type)
                .WithMany(pt => pt.Products)
                .HasForeignKey(p => p.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasOne(p => p.Dealer)
                .WithMany(d => d.Products)
                .HasForeignKey(p => p.DealerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Dealer>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Dealer>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
