using smartfinance.Domain.Common;
using smartfinance.Domain.Models.Account.Create;
using smartfinance.Domain.Models.Account.Model;
using smartfinance.Domain.Models.Account.Update;
using smartfinance.Domain.Models.Authentication.Model;

namespace smartfinance.Application.Interfaces
{
    public interface IAccountApp
    {
        Task<OperationResult<AccountViewModel>> FindByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<OperationResult<int>> CreateAsync(AccountCreateViewModel model, CancellationToken cancellationToken = default);

        Task<OperationResult<bool>> UpdateAsync(AccountUpdateViewModel model, CancellationToken cancellationToken = default);

        Task<OperationResult<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);

        Task<OperationResult<bool>> ActiveAsync(int id, CancellationToken cancellationToken = default);

        //Task<OperationResult<IdentityUserViewModel>> Login(LoginViewModel model);

        //Task<OperationResult<bool>> ConfirmEmailAsync(ConfirmEmailViewModel model);

    }
}
