using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PantheonTest.Domain.Entities;

namespace PantheonTest.Application.Contracts.Persistence
{
    public interface IAccountRepository : IAsyncRepository<Account>
    {
        Task<Account> GetUserAccount(Guid userId);
    }
}
