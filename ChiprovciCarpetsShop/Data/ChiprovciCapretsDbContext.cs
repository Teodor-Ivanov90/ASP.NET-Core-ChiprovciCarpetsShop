using ChiprovskiKilimi.Data.Models;
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
    }
}
