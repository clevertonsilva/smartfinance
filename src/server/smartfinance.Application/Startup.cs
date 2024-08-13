using Microsoft.Extensions.DependencyInjection;
using smartfinance.Application.Apps;
using smartfinance.Application.Interfaces;

namespace smartfinance.Application
{
    public static class Startup
    {
        public static void AddApplication(this IServiceCollection services)
        {
            #region Aplication

            services.AddScoped<IAccountApp, AccountApp>();
            services.AddScoped<ICategoryApp, CategoryApp>();
            services.AddScoped<IMovementApp, MovementApp>();

            #endregion
        }
    }
}
