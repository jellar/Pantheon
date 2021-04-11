using System;
using PantheonTest.Domain.Entities;

namespace PantheonTest.ApplicationTests
{
    public static class TestData
    {
        public static Account MockAccount(Guid userId)
        {
            return new Account
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Balance = 100m,
                CreatedOn = DateTime.Now,
                Name = "Test Account"
            };
        }
    }
}
