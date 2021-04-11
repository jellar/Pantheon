using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PantheonTest.Application.Contracts.Persistence;
using PantheonTest.Domain.Entities;

namespace PantheonTest.Persistence.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(PantheonDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Account> GetUserAccount(Guid userId)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.UserId == userId);
            return account;
        }
    }
}
