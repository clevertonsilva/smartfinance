using smartfinance.Api.Middleware;

namespace smartfinance.Api
{
    public static class Startup
    {
        public static void AddApi(this IServiceCollection services)
        {
            services.AddTransient<ErrorHandlingMiddleware>();
        }
    }
}
