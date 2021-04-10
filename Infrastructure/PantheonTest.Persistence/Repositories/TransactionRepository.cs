using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
