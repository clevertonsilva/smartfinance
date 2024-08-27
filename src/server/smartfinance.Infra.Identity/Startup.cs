using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using smartfinance.Domain.Interfaces.Services.Authentication;
using smartfinance.Infra.Identity.Customs;
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

            services.TryAddScoped<IUserValidator<AppIdentityUser>, UserValidator<AppIdentityUser>>();

            services.AddIdentityApiEndpoints<AppIdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            services.Configure<EmailConfirmationTokenProviderOptions>(opt => 
                opt.TokenLifespan = TimeSpan.FromMinutes(5));            

            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IIdentityUserService, IdentityUserService>();
            
        }
    }
}
