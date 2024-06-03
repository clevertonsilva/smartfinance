﻿using Microsoft.EntityFrameworkCore;
using smartfinance.Domain.Entities.Shared;
using smartfinance.Domain.Interfaces.Services.Authentication;
using smartfinance.Domain.Interfaces.Utils;

namespace smartfinance.Infra.Data.Data
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        private readonly ICurrentUserService _currentUserService;
        public AppDbContext(DbContextOptions options,
            ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        public async Task<bool> Commit(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving(_currentUserService.Name);

            var result = await base.SaveChangesAsync(cancellationToken) > 0;

            return result;
        }

        private void OnBeforeSaving(string username)
        {
            foreach (var entry in base.ChangeTracker.Entries<EntityBase>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.CreatedBy = username;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedAt = DateTime.Now;
                        entry.Entity.ModifiedBy = username;
                        break;
                }
            }
        }
    }
}
