using smartfinance.Domain.Common;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Models.AccountMovement.Create;
using smartfinance.Domain.Models.AccountMovement.Model;

namespace smartfinance.Application.Interfaces
{
    public interface IMovementApp
    {
        Task<OperationResult<MovementViewModel>> FindByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<OperationResult<IEnumerable<MovementViewModel>>> FindByRangeAsync(string initialDate, string finalDate, int skip, int take, CancellationToken cancellationToken = default);

        Task<OperationResult<int>> CreateAsync(MovementCreateViewModel model, CancellationToken cancellationToken = default);

        Task<OperationResult<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);

        

    }
}
