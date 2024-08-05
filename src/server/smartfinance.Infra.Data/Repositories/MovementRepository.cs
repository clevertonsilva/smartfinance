using Microsoft.EntityFrameworkCore;
using smartfinance.Domain.Common;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories;
using smartfinance.Domain.Models.AccountMovement.Model;
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

        public async Task<IEnumerable<Movement>> FindByRangeAsync(string initialDate, string finalDate, int skip, int take, CancellationToken cancellation = default)
        {
            return await _context.Movements
                .Where(m => m.MovementDate.Value >= Convert.ToDateTime(initialDate)
                            && m.MovementDate.Value <= Convert.ToDateTime(finalDate))
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellation);                                
        }
    }
}
