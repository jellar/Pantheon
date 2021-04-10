using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PantheonTest.Application.Contracts.Persistence;
using PantheonTest.Domain.Entities;

namespace PantheonTest.Persistence.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(PantheonDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Guid> Add(Guid accountId, TransactionType transactionType, string reference, decimal amount)
        {
            var transaction = new Transaction()
            {
                AccountId = accountId,
                Reference = reference,
                Amount = amount,
                TransactionType = transactionType,
                DateOn = DateTime.Now
            };

            await _dbContext.Transactions.AddAsync(transaction);

            return transaction.AccountId;
        }

        public async Task<List<Transaction>> GetAccountTransactions(Guid accountId)
        {
            return await _dbContext.Transactions.Where(t => t.AccountId == accountId).ToListAsync();
        }

        public async Task<IReadOnlyList<Transaction>> GetPagedTransactionsAsync(Guid accountId, int page, int size)
        {
            return await _dbContext.Transactions.Where(t=>t.AccountId == accountId).Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public async Task<Guid> Withdraw(Guid accountId, string reference, decimal amount)
        {
            var transaction = new Transaction()
            {
                AccountId = accountId,
                Reference = reference,
                Amount = amount,
                TransactionType = TransactionType.Withdraw,
                DateOn = DateTime.Now
            };

            await _dbContext.Transactions.AddAsync(transaction);

            return transaction.AccountId;
        }
    }
}
