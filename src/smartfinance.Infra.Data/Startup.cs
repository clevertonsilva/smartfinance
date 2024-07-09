using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using smartfinance.Domain.Interfaces.Repositories;
using smartfinance.Domain.Interfaces.Repositories.Shared;
using smartfinance.Infra.Data.Data;
using smartfinance.Infra.Data.Repositories;
using smartfinance.Infra.Data.Repositories.Shared;

namespace smartfinance.Infra.Data
{
    public static class Startup
    {
        public static void AddInfraData(this IServiceCollection services, IConfiguration configuration)
        {
            string _connectionString = configuration.GetConnectionString("AppDbConntectionString");

            services.AddDbContext<AppDbContext>(options => options.UseMySql(
                _connectionString
                , ServerVersion.AutoDetect(_connectionString)
                    , o => o.SchemaBehavior(MySqlSchemaBehavior.Ignore)));

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped(typeof(IAccountRepository), typeof(AccountRepository));
        }
    }
}
