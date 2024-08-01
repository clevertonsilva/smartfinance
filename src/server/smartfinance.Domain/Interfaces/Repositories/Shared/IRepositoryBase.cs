using smartfinance.Domain.Entities.Shared;
using smartfinance.Domain.Interfaces.Utils;

namespace smartfinance.Domain.Interfaces.Repositories.Shared
{
    public interface IRepositoryBase<TEntity> :
        IDisposable where TEntity : EntityBase
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken);

        Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken);

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);
    }
}
