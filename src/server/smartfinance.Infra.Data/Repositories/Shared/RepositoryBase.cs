using Microsoft.EntityFrameworkCore;
using smartfinance.Domain.Entities.Shared;
using smartfinance.Domain.Interfaces.Repositories.Shared;
using smartfinance.Domain.Interfaces.Utils;
using smartfinance.Infra.Data.Data;

namespace smartfinance.Infra.Data.Repositories.Shared
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : EntityBase
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private bool disposedValue;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);

            return entity;
        }

        public async Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(id, cancellationToken);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            
            return await Task.FromResult(entity);
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);

            return await Task.FromResult(entity);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (this == null)
                    {
                        Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
