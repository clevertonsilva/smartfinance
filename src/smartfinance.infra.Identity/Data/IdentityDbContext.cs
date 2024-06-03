using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using smartfinance.infra.Identity.Entities;

namespace smartfinance.infra.Identity.Data
{
    public class IdentityDbContext : IdentityDbContext<ApplicationIdentityUser>
    {
        public IdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
