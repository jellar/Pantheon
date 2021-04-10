using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PantheonTest.Domain.Entities;

namespace PantheonTest.Application.Contracts.Persistence
{
    public interface ITransactionRepository : IAsyncRepository<Transaction>
    {
        Task<Guid> Add(Guid accountId, TransactionType transactionType, string reference, decimal amount);
    }
}
