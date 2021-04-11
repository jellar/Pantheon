using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PantheonTest.Domain.Entities;

namespace PantheonTest.Application.Contracts.Persistence
{
    public interface ITransactionRepository : IAsyncRepository<Transaction>
    {
        Task<Guid> Add(Transaction transaction);

        Task<List<Transaction>> GetAccountTransactions(Guid accountId);

        Task<IReadOnlyList<Transaction>> GetPagedTransactionsAsync(Guid accountId, int page, int size);
    }
}
