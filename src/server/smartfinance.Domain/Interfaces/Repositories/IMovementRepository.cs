using smartfinance.Domain.Common;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories.Shared;
using smartfinance.Domain.Models.AccountMovement.Model;

namespace smartfinance.Domain.Interfaces.Repositories
{
    public interface IMovementRepository : IRepositoryBase<Movement>
    {
        Task<IEnumerable<Movement>> FindByRangeAsync(string initialDate, string finalDate, int skip, int take, CancellationToken cancellationToken = default);
    }
}
