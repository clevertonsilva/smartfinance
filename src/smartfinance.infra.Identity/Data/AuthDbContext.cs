using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using smartfinance.Infra.Identity.Entities;

namespace smartfinance.Infra.Identity.Data
{
    public class AuthDbContext : IdentityDbContext<AppIdentityUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }
    }
}
