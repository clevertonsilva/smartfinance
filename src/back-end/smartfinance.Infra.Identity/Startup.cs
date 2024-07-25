using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using smartfinance.Domain.Interfaces.Services.Authentication;
using smartfinance.Infra.Identity.Data;
using smartfinance.Infra.Identity.Entities;
using smartfinance.Infra.Identity.Localization;
using smartfinance.Infra.Identity.Services;

namespace smartfinance.Infra.Identity
{
    public static class Startup
    {
        public static void AddInfraIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            string _connectionString = configuration.GetConnectionString("AppDbConntectionString");

            services.AddDbContext<AuthDbContext>(options => options.UseMySql(
                _connectionString
                , ServerVersion.AutoDetect(_connectionString)
                    , o => o.SchemaBehavior(MySqlSchemaBehavior.Ignore)));

            services.AddIdentity<AppIdentityUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));
            services.AddScoped(typeof(IIdentityUserService), typeof(IdentityUserService));
        }
    }
}
