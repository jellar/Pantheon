using System;
using System.Collections.Generic;
using System.Text;
using PantheonTest.Application.Features.Transaction.Commands.Export;

namespace PantheonTest.Application.Features.Transaction.Queries.GetTransactions
{
    public class PagedTransactionsVm
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public ICollection<TransactionExportDto> Transactions { get; set; }
    }
}
