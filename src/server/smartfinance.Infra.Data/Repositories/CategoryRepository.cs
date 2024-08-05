using Microsoft.EntityFrameworkCore;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories;
using smartfinance.Infra.Data.Data;
using smartfinance.Infra.Data.Repositories.Shared;

namespace smartfinance.Infra.Data.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        protected readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> NameExists(int id, string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(w => w.Id != id &&  w.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) != null;
        }
    }
}
