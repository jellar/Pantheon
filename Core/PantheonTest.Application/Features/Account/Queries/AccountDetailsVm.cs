using System;

namespace PantheonTest.Application.Features.Account.Queries
{
    public class AccountDetailsVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}