using System;
using MediatR;

namespace PantheonTest.Application.Features.Transaction.Queries.Export
{
    public class GetTransactionExportQuery : IRequest<TransactionExportVm>
    {
        public Guid Id { get; set; }
    }
}
