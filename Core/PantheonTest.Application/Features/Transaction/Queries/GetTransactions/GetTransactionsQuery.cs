using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace PantheonTest.Application.Features.Transaction.Queries.GetTransactions
{
    public class GetTransactionsQuery : IRequest<PagedTransactionsVm>
    {
        public Guid AccountId { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
