using smartfinance.Domain.Common;
using smartfinance.Domain.Models.AccountMovement.Create;
using smartfinance.Domain.Models.AccountMovement.Model;
using smartfinance.Domain.Models.Shared;

namespace smartfinance.Application.Interfaces
{
    public interface IMovementApp
    {
        Task<OperationResult<MovementViewModel>> FindByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<OperationResult<PagedListViewModel<MovementViewModel>>> FindAllAsync(int accountId, string? searchDescriptionTerm, string? initialDate, string? finalDate, int page, int pageSize, CancellationToken cancellationToken = default);

        Task<OperationResult<int>> CreateAsync(MovementCreateViewModel model, CancellationToken cancellationToken = default);

        Task<OperationResult<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);   

    }
}
