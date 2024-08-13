using Microsoft.EntityFrameworkCore;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories;
using smartfinance.Infra.Data.Data;
using smartfinance.Infra.Data.Repositories.Shared;
using System.Linq;

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

        public async Task<List<Category>> FindAllAsync(string? searchNameTerm, string? sortOrder, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            IQueryable<Category> categoryQuery = _context.Categories;

            if (!string.IsNullOrWhiteSpace(searchNameTerm)) 
            {
                categoryQuery = categoryQuery.Where(w => w.Name.Contains(searchNameTerm));
            }
            
            if (sortOrder?.ToLower() == "desc")
            {
                categoryQuery = categoryQuery.OrderByDescending(o => o.Name);
            }
            else
            {
                categoryQuery = categoryQuery.OrderBy(w => w.Name);
            }

            var categories = await categoryQuery
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return categories;
        }

        public async Task<bool> NameExists(int id, string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(w => w.Id != id &&  w.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) != null;
        }
    }
}
