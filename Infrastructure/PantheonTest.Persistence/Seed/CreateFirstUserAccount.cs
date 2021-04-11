﻿using System;
using System.Threading.Tasks;
using PantheonTest.Application.Contracts.Persistence;
using PantheonTest.Domain.Entities;

namespace PantheonTest.Persistence.Seed
{
    public static class CreateFirstUserAccount
    {
        public static async Task SeedAsync(IAsyncRepository<Account> repository, string userId)
        {
            var accountGuid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var firstAccount = new Account()
            {
                Id = accountGuid,
                Name = "Current",
                Number = "12345678",
                UserId = Guid.Parse(userId),
                Balance = 100m,
                CreatedOn = DateTime.Now,
                Currency = "GBP"
            };

            var account = await repository.GetByIdAsync(accountGuid);
            if (account == null)
            {
                await repository.AddAsync(firstAccount);
            }
            
        }
    }
}
