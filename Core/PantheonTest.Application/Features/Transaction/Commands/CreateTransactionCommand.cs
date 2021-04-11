using System;
using MediatR;
using PantheonTest.Application.Features.Account.Queries;
using PantheonTest.Domain.Entities;

namespace PantheonTest.Application.Features.Transaction.Commands
{
    public class CreateTransactionCommand : IRequest<AccountDetailsVm>
    {
        public Guid AccountId { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public string CurrencyType { get; set; }
    }
}
