using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories.Shared;

namespace smartfinance.Domain.Interfaces.Repositories
{
    public interface IMovementRepository : IRepositoryBase<Movement>
    {
        Task<IEnumerable<Movement>> FindAllAsync(int accountId, string? searchDescriptionTerm, string? initialDate, string? finalDate, int page, int pageSize, CancellationToken cancellationToken = default);
    }
}
