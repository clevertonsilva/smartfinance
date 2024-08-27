using Microsoft.EntityFrameworkCore;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Entities.Shared;
using smartfinance.Domain.Interfaces.Utils;

namespace smartfinance.Infra.Data.Data
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();

            var result = await base.SaveChangesAsync(cancellationToken) > 0;

            return result;
        }

        private void OnBeforeSaving()
        {
            foreach (var entry in base.ChangeTracker.Entries<EntityBase>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedAt = DateTime.Now;
                        break;
                }
            }
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<Category> Categories { get; set; }
        
    }
}
