using smartfinance.Domain.Common;
using smartfinance.Domain.Models.AccountMovementCategory.Create;
using smartfinance.Domain.Models.AccountMovementCategory.Model;
using smartfinance.Domain.Models.AccountMovementCategory.Update;
using smartfinance.Domain.Models.Shared;

namespace smartfinance.Application.Interfaces
{
    public interface ICategoryApp
    {
        Task<OperationResult<CategoryViewModel>> FindByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<OperationResult<PagedListViewModel<CategoryViewModel>>> FindAllAsync(int accountId, string? searchNameTerm, string? sortOrder, int page, int pageSize, CancellationToken cancellationToken = default);

        Task<OperationResult<int>> CreateAsync(CategoryCreateViewModel model, CancellationToken cancellationToken = default);

        Task<OperationResult<bool>> UpdateAsync(int id, CategoryUpdateViewModel model, CancellationToken cancellationToken = default);

        Task<OperationResult<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
