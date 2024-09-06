using Microsoft.EntityFrameworkCore;
using smartfinance.Infra.Data.Data;
using smartfinance.Infra.Identity.Data;

namespace smartfinance.Api.Extensions
{
    public static class DataBaseExtension
    {
        public static void UseDatabaseConfiguration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.Migrate();
                context.Database.EnsureCreated();

                using var contextAuth = serviceScope.ServiceProvider.GetService<AuthDbContext>();
                context.Database.Migrate();
                context.Database.EnsureCreated();

            }
            //using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            //context.Database.Migrate();
            //context.Database.EnsureCreated();
        }
    }
}
