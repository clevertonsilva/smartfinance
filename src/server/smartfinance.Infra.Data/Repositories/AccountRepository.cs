using Microsoft.EntityFrameworkCore;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories;
using smartfinance.Infra.Data.Data;
using smartfinance.Infra.Data.Repositories.Shared;

namespace smartfinance.Infra.Data.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        protected readonly AppDbContext _context;
        public AccountRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Account> FindByEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(f => f.Email == email);
        }

        public async Task<bool> EmailExistsAsync(int id, string email)
        {
            return await _context.Accounts.AnyAsync(f => f.Id != id && f.Email.Contains(email));
        }
    }
}
