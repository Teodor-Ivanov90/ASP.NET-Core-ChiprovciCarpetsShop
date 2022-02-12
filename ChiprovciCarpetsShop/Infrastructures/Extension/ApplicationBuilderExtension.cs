using ChiprovciCarpetsShop.Data;
using ChiprovskiKilimi.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ChiprovskiKilimi.Infrastructures.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

           MigrateDatabase(services);
            SeedTypes(services);

            return app;
        }

        private static void SeedTypes(IServiceProvider services)
        {
            var data = services.GetRequiredService<ChiprovciCapretsDbContext>();

            if (data.ProductTypes.Any())
            {
                return;
            }

            data.ProductTypes.AddRange(new []
            {
                new ProductType{Name = "Carpet"},
                new ProductType{Name = "Rug"},
                new ProductType{Name = "Panel"},
                new ProductType{Name = "Bag"},
                new ProductType{Name = "Souvenir"},
            });

            data.SaveChanges();
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
             var data = services.GetRequiredService<ChiprovciCapretsDbContext>();

            data.Database.Migrate();
        }
    }
}
