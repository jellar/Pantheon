using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using PantheonTest.Domain.Entities;

namespace PantheonTest.Application.Features.Transaction.Commands
{
    public class CreateTransactionCommand : IRequest<Guid>
    {
        public Guid AccountId { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
