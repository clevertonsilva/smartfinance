using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories.Shared;

namespace smartfinance.Domain.Interfaces.Repositories
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Task<bool> EmailExistsAsync(int id, string name);
    }
}
