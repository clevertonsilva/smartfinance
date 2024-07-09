using smartfinance.Domain.Common;
using smartfinance.Domain.Models.Account.Create;
using smartfinance.Domain.Models.Account.Model;
using smartfinance.Domain.Models.Account.Update;

namespace smartfinance.Application.Interfaces
{
    public interface IAccountApp
    {
        Task<OperationResult<AccountViewModel>> FindByIdAsync(int id, CancellationToken cancellationToken);

        Task<OperationResult<int>> CreateAsync(AccountCreateViewModel model, CancellationToken cancellationToken);

        Task<OperationResult<bool>> UpdateAsync(AccountUpdateViewModel model, CancellationToken cancellationToken);

        Task<OperationResult<bool>> DeleteAsync(int id, CancellationToken cancellationToken);

        Task<OperationResult<bool>> ActiveAsync(int id, CancellationToken cancellationToken);
    }
}
