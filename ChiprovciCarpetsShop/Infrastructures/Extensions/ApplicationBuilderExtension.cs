using System;
using System.Linq;
using System.Threading.Tasks;
using ChiprovciCarpetsShop.Data;
using ChiprovciCarpetsShop.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using static ChiprovciCarpetsShop.Areas.Admin.AdminConstants;

namespace ChiprovciCarpetsShop.Infrastructures.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedTypes(services);
            SeedAdministrator(services);

            return app;
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@carpets.com";
                    const string adminPassword = "admin12";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = "Admin"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedTypes(IServiceProvider services)
        {
            var data = services.GetRequiredService<ChiprovciCarpetsDbContext>();

            if (data.ProductTypes.Any())
            {
                return;
            }

            data.ProductTypes.AddRange(new[]
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
            var data = services.GetRequiredService<ChiprovciCarpetsDbContext>();

            data.Database.Migrate();
        }
    }
}
