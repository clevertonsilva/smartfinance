using smartfinance.Domain.Common;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories;
using smartfinance.Domain.Models.AccountMovement.Model;
using smartfinance.Infra.Data.Data;
using smartfinance.Infra.Data.Repositories.Shared;

namespace smartfinance.Infra.Data.Repositories
{
    public class MovementRepository : RepositoryBase<Movement>, IMovementRepository
    {
        public MovementRepository(AppDbContext context)
           : base(context)
        {

        }

        public Task<OperationResult<IEnumerable<MovementViewModel>>> FindByRangeAsync(string initialDate, string finalDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
