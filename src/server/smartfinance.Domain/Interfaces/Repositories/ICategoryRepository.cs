using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories.Shared;

namespace smartfinance.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<bool> NameExists(int id, string name);
    }
}
