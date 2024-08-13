using Microsoft.EntityFrameworkCore;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories;
using smartfinance.Infra.Data.Data;
using smartfinance.Infra.Data.Repositories.Shared;

namespace smartfinance.Infra.Data.Repositories
{
    public class MovementRepository : RepositoryBase<Movement>, IMovementRepository
    {
        protected readonly AppDbContext _context;
        public MovementRepository(AppDbContext context)
           : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movement>> FindAllAsync(int accountId, string? searchDescriptionTerm, string? initialDate, string? finalDate, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            IQueryable<Movement> movementQuery = _context.Movements.Where(m => m.AccountId == accountId);

            if (!string.IsNullOrWhiteSpace(searchDescriptionTerm))
            {
                movementQuery = movementQuery.Where(w => w.Description.Contains(searchDescriptionTerm));
            }

            if (DateTime.TryParse(initialDate, out var _initialDate) && DateTime.TryParse(finalDate, out var _finalDate))
            {
                movementQuery = movementQuery.Where(w => w.MovementDate >= _initialDate && w.MovementDate <= _finalDate);
            }

            movementQuery = movementQuery.OrderByDescending(w => w.MovementDate);

            var movements = await movementQuery
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return movements;
        }
    }
}
