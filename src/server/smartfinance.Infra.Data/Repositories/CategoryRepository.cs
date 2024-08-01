using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories;
using smartfinance.Infra.Data.Data;
using smartfinance.Infra.Data.Repositories.Shared;

namespace smartfinance.Infra.Data.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context)
            : base(context)
        {
            
        }
    }
}
